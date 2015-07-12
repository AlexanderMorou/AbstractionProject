using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Slf._Internal.Vre;
using AllenCopeland.Abstraction.Slf.Abstract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Vre
{
    /// <summary>
    /// Defines properties and methods for working with a specific version of a verionsable library.
    /// </summary>
    /// <typeparam name="TEnvironment">
    /// The type used in place of the <see cref="IVreEnvironment{TEnvironment, TVersion, TIdentityManager}"/> 
    /// which implements <typeparamref name="IVreEnvironment{TEnvironment, TVersion, TIdentityManager}"/>.
    /// </typeparam>
    /// <typeparam name="TVersion">
    /// The type used in place of the <see cref="IVreEnvironmentVersion{TEnvironment, TVersion, TIdentityManager}"/> within the model
    /// that represent a unique version of the <see cref="IVreEnvironment{TEnvironment, TVersion, TIdentityManager}"/>.
    /// </typeparam>
    /// <typeparam name="TIdentityManager">The type of <see cref="IIdentityManager"/> used to resolve
    /// identities within the <typeparamref name="TEnvironment"/>.</typeparam>
    public class VreXmlLibraryVersion<TEnvironment, TVersion, TIdentityManager> :
        VreXmlNamespaceParentVersion<TEnvironment, TVersion, TIdentityManager>,
        IVreLibraryVersion<TEnvironment, TVersion, TIdentityManager>
        where TEnvironment :
            VreXmlEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        /// <summary>
        /// Data member for <see cref="Version"/>.
        /// </summary>
        private readonly TVersion version;
        /// <summary>
        /// Data member for <see cref="RootLibrary"/>.
        /// </summary>
        private readonly IVreLibrary<TEnvironment, TVersion, TIdentityManager> rootLibrary;
        /// <summary>
        /// Data member for <see cref="AssemblyVersion"/>.
        /// </summary>
        private readonly Version assemblyVersion;
        private IAssemblyUniqueIdentifier _uniqueIdentifier;
        private int _publicKeyId;
        internal VreXmlLibraryVersion(TVersion version, IVreLibrary<TEnvironment, TVersion, TIdentityManager> rootLibrary, Version assemblyVersion, int publicKeyId)
            : base(version, null)
        {
            this.version = version;
            this.rootLibrary = rootLibrary;
            this.assemblyVersion = assemblyVersion;
            this._publicKeyId = publicKeyId;
        }
        /// <summary>
        /// Returns the <typeparamref name="TVersion"/> from which the
        /// <see cref="VreXmlLibraryVersion{TEnvironment, TVersion, TIdentityManager}"/> is derived.
        /// </summary>
        public TVersion Version
        {
            get { return this.version; }
        }

        /// <summary>
        /// Returns the root <see cref="IVreLibrary{TEnvironment, TVersion, TIdentityManager}"/> from
        /// which the <see cref="VreXmlLibraryVersion{TEnvironment, TVersion, TIdentityManager}"/> is a versioned
        /// derivative ofs.
        /// </summary>
        public IVreLibrary<TEnvironment, TVersion, TIdentityManager> RootLibrary
        {
            get { return this.rootLibrary; }
        }

        /// <summary>
        /// Returns the <see cref="System.Version"/> which denotes the specifics of the assembly version within the
        /// current <typeparamref name="TEnvironment"/>'s <typeparamref name="TVersion"/>.
        /// </summary>
        public Version AssemblyVersion
        {
            get { return this.assemblyVersion; }
        }

        protected override bool FilterType(IVreTypeVersion<TEnvironment, TVersion, TIdentityManager> type)
        {
            return type.Version == this.version && type.Library == this && type.RootType.Parent == this.Version.Environment;
        }

        public IAssemblyUniqueIdentifier UniqueIdentifier
        {
            get 
            {
                return this._uniqueIdentifier ?? (this._uniqueIdentifier = this.InitializeUniqueIdentifier());
            }
        }

        private IAssemblyUniqueIdentifier InitializeUniqueIdentifier()
        {
            return TypeSystemIdentifiers.GetAssemblyIdentifier(this.RootLibrary.Name, TypeSystemIdentifiers.GetVersion(this.assemblyVersion.Major, this.assemblyVersion.Minor, this.assemblyVersion.Build, this.assemblyVersion.Revision), CultureIdentifiers.None, this.Version.Environment.GetPublicKeyToken(this._publicKeyId));
        }

        public override string ToString()
        {
            return this.UniqueIdentifier.ToString();
        }
    }
}
