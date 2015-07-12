using AllenCopeland.Abstraction.Slf._Internal.Vre;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AllenCopeland.Abstraction.Slf.Vre
{
    public abstract class VreXmlEnvironment<TEnvironment, TVersion, TIdentityManager> :
        VreXmlNamespaceParent<TEnvironment, TVersion, TIdentityManager>,
        IVreEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TEnvironment :
            VreXmlEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        private readonly XmlNamespaceManager xmlNamespaceManager;
        private IControlledCollection<TVersion> versions;
        private TVersion currentVersion;
        private Dictionary<int, byte[]> _knownPublicKeyTokens;
        private IVreLibraryDictionary<TEnvironment, TVersion, TIdentityManager> _libraries;

        public VreXmlEnvironment(XmlDocument document, XmlNamespaceManager xmlNamespaceManager)
            : base(document, XmlNamespaceTree.GenerateTree(document.DocumentElement, xmlNamespaceManager))
        {
            this.xmlNamespaceManager = xmlNamespaceManager;
        }

        public XmlNamespaceManager XmlNamespaceManager { get { return this.xmlNamespaceManager; } }

        protected internal new XmlDocument XmlNode { get { return (XmlDocument)base.XmlNode; } }

        public IControlledCollection<TVersion> Versions
        {
            get
            {
                return this.versions ?? (this.versions = this.InitializeVersions());
            }
        }

        protected abstract TVersion GetVersion(TVersion previousVersion, XmlElement derivedVersion);
        protected abstract TVersion GetVersion(XmlElement xmlNode);

        private IControlledCollection<TVersion> InitializeVersions()
        {
            var versionsTopNode = this.XmlNode.DocumentElement.SelectSingleNode("./vre:Versions", this.xmlNamespaceManager);
            if (versionsTopNode != null)
            {
                var initialVersion = versionsTopNode.SelectSingleNode("./vre:InitialVersion", this.xmlNamespaceManager);
                if (initialVersion == null || !(initialVersion is XmlElement))
                    throw new InvalidOperationException("Xml Schema malformed, no initial version defined.");
                var minorVersions = ConvertNodeListToList<XmlElement>(versionsTopNode.SelectNodes("./vre:MinorVersion", this.xmlNamespaceManager));
                var breakingVersions = ConvertNodeListToList<XmlElement>(versionsTopNode.SelectNodes("./vre:BreakingVersion", this.xmlNamespaceManager));
                var servicePacks = ConvertNodeListToList<XmlElement>(versionsTopNode.SelectNodes("./vre:ServicePack", this.xmlNamespaceManager));
                Stack<XmlElement> versionsToReview = new Stack<XmlElement>();
                versionsToReview.Push((XmlElement)initialVersion);
                TVersion currentVersion = null;
                List<TVersion> results = new List<TVersion>(minorVersions.Count + breakingVersions.Count + servicePacks.Count + 1);
                while (versionsToReview.Count > 0)
                {
                    var currentReviewTarget = versionsToReview.Pop();
                    if (currentVersion == null)
                        currentVersion = GetVersion(currentReviewTarget);
                    else
                        currentVersion = GetVersion(currentVersion, currentReviewTarget);
                    var nextInLine = FindNextVersion(currentReviewTarget, minorVersions, breakingVersions, servicePacks);
                    if (nextInLine != null && nextInLine is XmlElement)
                        versionsToReview.Push((XmlElement)nextInLine);
                    results.Add(currentVersion);
                }
                var toDelete = (from v in results
                                where !v.HintPaths.Any()
                                select v).ToArray();
                foreach (var deletionTarget in toDelete)
                    using (deletionTarget)
                    {
                        deletionTarget.Delete();
                        results.Remove(deletionTarget);
                    }
                return new ControlledCollection<TVersion>(results);
            }
            else
                return new ControlledCollection<TVersion>();
        }

        private XmlNode FindNextVersion(XmlElement currentReviewTarget, List<XmlElement> minorVersions, List<XmlElement> breakingVersions, List<XmlElement> servicePacks)
        {
            switch (currentReviewTarget.Name)
            {
                case "vre:InitialVersion":
                    return FindNextVersion(currentReviewTarget, minorVersions, breakingVersions, servicePacks, false, true);
                case "vre:BreakingVersion":
                    return FindNextVersion(currentReviewTarget, minorVersions, breakingVersions, servicePacks, false, true);
                case "vre:MinorVersion":
                    return FindNextVersion(currentReviewTarget, minorVersions, breakingVersions, servicePacks, false, true);
                case "vre:ServicePack":
                    return FindNextVersion(currentReviewTarget, minorVersions, breakingVersions, servicePacks, true, false);
            }
            return null;
        }

        private XmlNode FindNextVersion(XmlElement currentReviewTarget, List<XmlElement> minorVersions, List<XmlElement> breakingVersions, List<XmlElement> servicePacks, bool useSpCallout, bool useIdCallout)
        {
            string servicePackTarget = useSpCallout ? currentReviewTarget.GetAttribute("ServicePack") : null;
            string lockTarget =
                useIdCallout ?
                    currentReviewTarget.GetAttribute("Id") :
                    currentReviewTarget.GetAttribute("BuiltUpon");
            var minorFind =
                FindNextVersion(currentReviewTarget, minorVersions, mv => SpOrMinorFind(useSpCallout, servicePackTarget, lockTarget, mv));
            if (minorFind == null)
            {
                var servicePackFind =
                    FindNextVersion(currentReviewTarget, servicePacks, sp => SpOrMinorFind(useSpCallout, servicePackTarget, lockTarget, sp));
                if (servicePackFind == null)
                {
                    var breakingFind =
                        FindNextVersion(currentReviewTarget, breakingVersions, bv =>
                        {
                            if (bv.GetAttribute("Breaks") == lockTarget)
                                if (useSpCallout)
                                {
                                    if (bv.GetAttribute("BreaksServicePack") == servicePackTarget)
                                        return true;
                                }
                                else
                                    return string.IsNullOrEmpty(bv.GetAttribute("BreaksServicePack"));
                            return false;
                        });
                    return breakingFind;
                }
                return servicePackFind;
            }
            return minorFind;
        }

        private static bool SpOrMinorFind(bool useSpCallout, string servicePackTarget, string lockTarget, XmlElement spOrMv)
        {
            if (spOrMv.GetAttribute("BuiltUpon") == lockTarget)
                if (useSpCallout)
                {
                    if (spOrMv.GetAttribute("BuiltUponServicePack") == servicePackTarget)
                        return true;
                }
                else
                    return string.IsNullOrEmpty(spOrMv.GetAttribute("BuiltUponServicePack"));
            return false;
        }

        private XmlNode FindNextVersion(XmlElement currentReviewTarget, List<XmlElement> targetSet, Predicate<XmlElement> lockPredicate)
        {
            foreach (var element in targetSet)
                if (lockPredicate(element))
                    return element;
            return null;
        }

        private List<TNode> ConvertNodeListToList<TNode>(XmlNodeList list)
            where TNode :
                XmlNode
        {
            if (list == null)
                return new List<TNode>();
            List<TNode> result = new List<TNode>();
            foreach (XmlNode node in list)
                if (node is TNode)
                    result.Add((TNode)node);
            return result;
        }

        public string Name { get { return this.XmlNode.DocumentElement.GetAttribute("Name"); } }

        public override string ToString()
        {
            return string.Format("Versioned Environment: {0}", this.Name);
        }

        public TVersion CurrentVersion { get { return this.currentVersion ?? (this.currentVersion = this.InitializeCurrentVersion()); } }

        private TVersion InitializeCurrentVersion()
        {
            var currentVersionText = this.XmlNode.DocumentElement.GetAttribute("CurrentVersion");
            if (string.IsNullOrEmpty(currentVersionText))
                return null;
            var currentServicePackText = this.XmlNode.DocumentElement.GetAttribute("CurrentServicePack");
            if (!string.IsNullOrEmpty(currentServicePackText))
                currentVersionText = string.Format("{0} {1}", currentVersionText, currentServicePackText);
            return (from v in this.Versions
                    where v.VersionQualifier == currentVersionText
                    select v).FirstOrDefault();

        }

        public override void Dispose()
        {
            if (this.versions != null)
            {
                foreach (var version in this.versions)
                    version.Dispose();
                this.versions = null;
            }
            base.Dispose();
        }
        protected override TEnvironment OnGetEnvironment()
        {
            return (TEnvironment)this;
        }


        public IVreLibraryDictionary<TEnvironment, TVersion, TIdentityManager> Libraries
        {
            get { return this._libraries ?? (this._libraries = new VreXmlLibraryDictionary<TEnvironment, TVersion, TIdentityManager>((TEnvironment)this)); }
        }

        internal byte[] GetPublicKeyToken(int index)
        {
            if (this._knownPublicKeyTokens == null)
                this.InitializePublicKeyTokens();
            byte[] result;
            if (this._knownPublicKeyTokens.TryGetValue(index, out result))
                return result;
            throw new KeyNotFoundException("There was no public key with the identifier provided found.");
        }

        private void InitializePublicKeyTokens()
        {
            this._knownPublicKeyTokens = new Dictionary<int, byte[]>();
            var keysNode = this.XmlNode.DocumentElement.SelectSingleNode("./vre:PublicKeys", this.XmlNamespaceManager);
            if (keysNode != null)
            {
                var xnlKeys = keysNode.SelectNodes("./vre:PublicKey", this.XmlNamespaceManager);
                if (xnlKeys != null && xnlKeys.Count > 0)
                    foreach (XmlElement element in xnlKeys)
                    {
                        int id = XmlExtensions.ParseAttribute(element, "Id");
                        if (id != -1)
                        {
                            byte[] token = XmlExtensions.ParseAttribute<byte[]>(element, "Token", (str, def) =>
                                {
                                    try
                                    {
                                        return StringHandling.FromFormatHexadecimal(str);
                                    }
                                    catch (Exception e)
                                    {
                                        return def;
                                    }
                                }, new byte[0]);
                            if (token != null && token.Length == 8)
                                this._knownPublicKeyTokens.Add(id, token);
                        }
                    }
            }
        }
    }
}
