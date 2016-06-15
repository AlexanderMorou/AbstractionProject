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
    /// <summary>
    /// Provides a base implementation of the <see cref="IVreLibrary{TEnvironment, TVersion, TIdentityManager}"/> with the specific
    /// <paramref name="TEnvironment"/>, <typeparamref name="TVersion"/> and <typeparamref name="TIdentityManager"/> provided.
    /// </summary>
    /// <typeparam name="TEnvironment">
    /// The type used in place of the <see cref="IVreEnvironment{TEnvironment, TVersion, TIdentityManager}"/> 
    /// which implements <typeparamref name="IVreEnvironment{TEnvironment, TVersion, TIdentityManager}"/>.
    /// </typeparam>
    /// <typeparam name="TVersion">
    /// The type used in place of the <see cref="IVreEnvironmentVersion{TEnvironment, TVersion, TIdentityManager}"/> within the model
    /// that represent a unique version of the <see cref="IVreEnvironment{TEnvironment, TVersion, TIdentityManager}"/> or (<typeparamref name="TEnvironment"/>).
    /// </typeparam>
    /// <typeparam name="TIdentityManager">The type of <see cref="IIdentityManager"/> used to resolve
    /// identities within the <typeparamref name="TEnvironment"/>.</typeparam>
    public class VreXmlLibrary<TEnvironment, TVersion, TIdentityManager> :
        //VreXmlNamespaceParent<TEnvironment, TVersion, TIdentityManager>,
        VreXmlElement,
        IVreLibrary<TEnvironment, TVersion, TIdentityManager>
        where TEnvironment :
            VreXmlEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        /// <summary>
        /// Data member for <see cref="Environment"/>.
        /// </summary>
        private TEnvironment _environment;
        private TVersion _initialVersion;
        IControlledDictionary<TVersion, IVreLibraryVersion<TEnvironment, TVersion, TIdentityManager>> _versions;
        private Dictionary<XmlElement, TVersion> _versionHistory;

        /// <summary>
        /// Creates a new <see cref="VreXmlLibrary{TEnvironment, TVersion, TIdentityManager}"/> with the <paramref name="element"/> and <paramref name="environment"/>
        /// provided.
        /// </summary>
        /// <param name="element">The <see cref="XmlElement"/> from which the <see cref="VreXmlLibrary{TEnvironment, TVersion, TIdentityManager}"/> is derived.</param>
        /// <param name="environment">The <typeparamref name="TEnvironment"/> in which the current <see cref="VreXmlLibrary{TEnvironment, TVersion, TIdentityManager}"/>
        /// is owned by.</param>
        internal VreXmlLibrary(XmlElement element, TEnvironment environment)
            : base(element)
        {
            this._environment = environment;
        }

        /// <summary>
        /// Returns the <see cref="String"/> value denoting the name of the library.
        /// </summary>
        public string Name
        {
            get
            {
                return this.XmlNode.GetAttribute("Name");
            }
        }

        /// <summary>
        /// Returns the <typeparamref name="TEnvironment"/> from which the current 
        /// <see cref="VreXmlLibrary{TEnvironment, TVersion, TIdentityManager}"/> is
        /// derived.
        /// </summary>
        public TEnvironment Environment
        {
            get { return this._environment; }
        }

        public TVersion InitialVersion
        {
            get {
                return this._initialVersion ?? (this._initialVersion = this.InitializeInitialVersion());
            }
        }

        private TVersion InitializeInitialVersion()
        {
            var initialVersion = this.XmlNode.GetAttribute("InitialVersion");
            if (string.IsNullOrEmpty(initialVersion))
                return this.Environment.CurrentVersion.InitialVersion;
            return XmlExtensions.GetVersion<TEnvironment, TVersion, TIdentityManager>(this.Environment, this.XmlNode.GetAttribute("Introduced"), this.XmlNode.GetAttribute("IntroducedServicePack"));
        }

        public IControlledDictionary<TVersion, IVreLibraryVersion<TEnvironment, TVersion, TIdentityManager>> Versions
        {
            get {
                return this._versions ?? (this._versions = this.InitializeVersions());
            }
        }

        private IControlledDictionary<TVersion, IVreLibraryVersion<TEnvironment, TVersion, TIdentityManager>> InitializeVersions()
        {
            XmlElement lastVHistory = null;
            return new ControlledDictionary<TVersion, IVreLibraryVersion<TEnvironment, TVersion, TIdentityManager>>(
                (from v in this.Environment.Versions
                 where v >= this.InitialVersion
                 join vh in this.VersionHistory on v equals vh.Value into vhJoin
                 from vh in vhJoin.DefaultIfEmpty()
                 where vh.Key != null
                 let versionDetails = vh.Key == null ? lastVHistory : (lastVHistory = vh.Key)
                 select (IVreLibraryVersion<TEnvironment, TVersion, TIdentityManager>)new VreXmlLibraryVersion<TEnvironment, TVersion, TIdentityManager>(v, this, GetVersionFromHistoryEntry(versionDetails), XmlExtensions.ParseAttribute(versionDetails, "PublicKeyId"))).ToDictionary(k => k.Version, v => v));
        }

        private Version GetVersionFromHistoryEntry(XmlElement versionDetails)
        {
            var value = versionDetails.GetAttribute("Value");
            if (value == null)
                return null;
            Version result;
            if (Version.TryParse(value, out result))
                return result;
            return null;
        }


        internal Dictionary<XmlElement, TVersion> VersionHistory { get { return this._versionHistory ?? (this._versionHistory = this.InitializeVersionHistory()); } }

        private Dictionary<XmlElement, TVersion> InitializeVersionHistory()
        {
            return XmlExtensions.ParseDictionary<Dictionary<XmlElement, TVersion>, XmlElement, TVersion, XmlElement>(this.XmlNode, this.Environment.XmlNamespaceManager, "VersionHistory", "Version", v => XmlExtensions.GetVersion<TEnvironment, TVersion, TIdentityManager>(this.Environment, v.Extra.GetAttribute("Runtime"), v.Extra.GetAttribute("ServicePack")), x => x.Extra.Item1);
        }
    }
}
