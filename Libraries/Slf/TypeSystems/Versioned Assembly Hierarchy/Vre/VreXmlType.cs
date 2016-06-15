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
    public class VreXmlType<TEnvironment, TVersion, TIdentityManager> :
        VreXmlTypeParent<TEnvironment, TVersion, TIdentityManager>,
        IVreType<TEnvironment, TVersion, TIdentityManager>
        where TEnvironment :
            VreXmlEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        private IEnumerable<TVersion>                                       _deprecated;
        private string                                                      _initialLibrary;
        private TVersion                                                    _introduced;
        private bool?                                                       _isGenericType;
        private string                                                      _name;
        private VreXmlTypeParent<TEnvironment, TVersion, TIdentityManager>  _parent;
        private TypeKind?                                                   _type;
        private Dictionary<XmlElement, TVersion>                            _versionHistory;

        private ControlledDictionary<TVersion, IVreTypeVersion<TEnvironment, TVersion, TIdentityManager>> _versions;

        public VreXmlType(TEnvironment environment, XmlElement element, VreXmlTypeParent<TEnvironment, TVersion, TIdentityManager> parent)
            : base(element, environment) { this._parent = parent; }

        private string InitialLibrary { get { return this._initialLibrary ?? (this._initialLibrary = this.XmlNode.GetAttribute("InitialLibrary")); } }

        public IControlledDictionary<TVersion, IVreTypeVersion<TEnvironment, TVersion, TIdentityManager>> Versions { get { return this._versions ?? (this._versions = this.InitializeVersions()); } }

        public IVreTypeParent<TEnvironment, TVersion, TIdentityManager> Parent { get { return this._parent; } }

        public TVersion Introduced { get { return this._introduced ?? (this._introduced = XmlExtensions.GetVersion<TEnvironment, TVersion, TIdentityManager>(this.Environment, this.XmlNode.GetAttribute("Introduced"), this.XmlNode.GetAttribute("IntroducedServicePack"))); } }

        public IEnumerable<TVersion> Deprecated { get { return this._deprecated ?? (this._deprecated= this.InitializeDeprecated()); } }

        internal Dictionary<XmlElement, TVersion> VersionHistory { get { return this._versionHistory ?? (this._versionHistory = this.InitializeVersionHistory()); } }

        public string Name { get { return this._name ?? (this._name = this.XmlNode.GetAttribute("Name")); } }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}", this.Parent, this._parent is IVreType<TEnvironment, TVersion, TIdentityManager> ? "+" : ".", this.Name);
        }

        public IEnumerable<TVersion> Supported
        {
            get 
            {
                return (from v in this.Environment.Versions
                        where v >= this.Introduced
                        select v).Except(this.Deprecated);
            }
        }

        public TypeKind Type { get { return this._type ?? (this._type = this.InitializeType()).Value; } }

        internal bool IsGenericType { get { return (this._isGenericType ?? (this._isGenericType = this._name.Contains("`"))).Value; } }

        internal int GenericParameterCount
        {
            get
            {
                if (this.IsGenericType)
                {
                    string  beyondAccent = this.Name.Substring(this.Name.IndexOf('`')) + 1;
                    int     result;
                    if (int.TryParse(beyondAccent, out result))
                        return result;
                }
                return 0;
            }
        }

        protected new XmlElement XmlNode { get { return (XmlElement)base.XmlNode; } }

        private TypeKind InitializeType()
        {
            return this.XmlNode.ParseAttribute<TypeKind>("TypeKind", (s, d) =>
            {
                TypeKind result;
                if (Enum.TryParse<TypeKind>(s, out result))
                    return result;
                return d;
            });
        }

        private IEnumerable<TVersion> InitializeDeprecated()
        {
            var deprecated = new Dictionary<TVersion, bool?>();
            foreach (var version in from version in this.Environment.Versions
                                    orderby version
                                    select version)
                deprecated.Add(version, null);
            foreach (var node in this.VersionHistory.Keys)
            {
                var version = this.VersionHistory[node];
                if (node.LocalName == "Deprecated")
                    deprecated[version] = true;
                else if (node.LocalName == "Supported")
                    deprecated[version] = false;
            }
            bool lastStatus = false;
            foreach (var version in deprecated.Keys.ToArray())
            {
                if (deprecated[version] == null)
                    deprecated[version] = lastStatus;
                else
                    lastStatus = deprecated[version].Value;
            }
            return from v in deprecated.Keys
                   where deprecated[v].Value
                   select v;
        }

        private ControlledDictionary<TVersion, IVreTypeVersion<TEnvironment, TVersion, TIdentityManager>> InitializeVersions()
        {
            var result = new Dictionary<TVersion, IVreTypeVersion<TEnvironment, TVersion, TIdentityManager>>();
            string library = InitialLibrary;
            var currentLibrary = this.Environment.Libraries[library];
            var currentVersion = Introduced;

            while (currentVersion != null)
            {
                var versionHistoryDetail = (from vh in this.VersionHistory
                                            where vh.Value == currentVersion
                                            select vh).FirstOrDefault();
                if (versionHistoryDetail.Key != null && versionHistoryDetail.Key.LocalName == "ForwardedTo")
                {
                    library = versionHistoryDetail.Key.GetAttribute("Library");
                    currentLibrary = this.Environment.Libraries[library];
                }
                var libraryVersion = currentLibrary.Versions.Values.OrderByDescending(k => k.Version).FirstOrDefault(k => k.Version <= currentVersion);
                if (libraryVersion != null)
                    result.Add(currentVersion, new VreXmlTypeVersion<TEnvironment, TVersion, TIdentityManager>(this, currentVersion, libraryVersion));
                currentVersion = currentVersion.NextVersion;
            }
            return new ControlledDictionary<TVersion, IVreTypeVersion<TEnvironment, TVersion, TIdentityManager>>(result);
        }

        private Dictionary<XmlElement, TVersion> InitializeVersionHistory()
        {
            return XmlExtensions.ParseDictionary<Dictionary<XmlElement, TVersion>, XmlElement, TVersion, XmlElement>(this.XmlNode, this.Environment.XmlNamespaceManager, "VersionHistory", new string[] { "Deprecated", "Supported", "ForwardedTo" }, v => XmlExtensions.GetVersion<TEnvironment, TVersion, TIdentityManager>(this.Environment, v.Extra.GetAttribute("Version"), v.Extra.GetAttribute("ServicePack")), x => x.Extra.Item1);
        }

    }
}
