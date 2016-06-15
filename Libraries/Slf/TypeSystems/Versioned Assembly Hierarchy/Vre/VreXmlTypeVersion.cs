using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Vre
{
    /// <summary>
    /// Provides a base implementation for working with a type from a versioned environment from a specific
    /// version of that environment.
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
    public class VreXmlTypeVersion<TEnvironment, TVersion, TIdentityManager> :
        VreXmlTypeParentVersion<TEnvironment, TVersion, TIdentityManager>,
        IVreTypeVersion<TEnvironment, TVersion, TIdentityManager>
        where TEnvironment :
            VreXmlEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        /// <summary>
        /// Data member for <see cref="RootType"/>.
        /// </summary>
        private VreXmlType<TEnvironment, TVersion, TIdentityManager> _rootType;
        /// <summary>
        /// Data member for <see cref="UniqueIdentifier"/>.
        /// </summary>
        private ITypeUniqueIdentifier _uniqueIdentifier;
        /// <summary>
        /// Data member for <see cref="Library"/>.
        /// </summary>
        private IVreLibraryVersion<TEnvironment, TVersion, TIdentityManager> _library;

        /// <summary>
        /// Creates a new <see cref="VreXmlTypeVersion{TEnvironment, TVersion, TIdentityManager"/>.
        /// </summary>
        /// <param name="rootType">The <see cref="VreXmlType{TEnvironment, TVersion, TIdentityManager}"/> from which
        /// the current <see cref="VreXmlTypeVersion{TEnvironment, TVersion, TIdentityManager"/> is derived.</param>
        /// <param name="version">The <typeparamref name="TVersion"/> of the <paramref name="rootType"/> identified
        /// by the <see cref="VreXmlTypeVersion{TEnvironment, TVersion, TIdentityManager"/>.</param>
        internal VreXmlTypeVersion(VreXmlType<TEnvironment, TVersion, TIdentityManager> rootType, TVersion version, IVreLibraryVersion<TEnvironment, TVersion, TIdentityManager> library)
            : base(version)
        {
            this._rootType = rootType;
            this._library = library;
        }

        /// <summary>
        /// Returns the <typeparamref name="TEnvironment"/> from which the
        /// <see cref="VreXmlTypeVersion{TEnvironment, TVersion, TIdentityManager}"/> was derived.
        /// </summary>
        public TEnvironment Environment
        {
            get { return this.RootType.Environment; }
        }

        /// <summary>
        /// Returns the <see cref="IVreType{TEnvironment, TVersion, TIdentityManager}"/>
        /// <see cref="VreXmlTypeVersion{TEnvironment, TVersion, TIdentityManager}"/> was derived.
        /// </summary>
        public IVreType<TEnvironment, TVersion, TIdentityManager> RootType
        {
            get { return this._rootType; }
        }

        /// <summary>
        /// Returns the <see cref="TypeKind"/> of the <see cref="VreXmlTypeVersion{TEnvironment, TVersion, TIdentityManager}"/>.
        /// </summary>
        public TypeKind Type
        {
            get { return this.RootType.Type; }
        }

        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> of the <see cref="VreXmlTypeVersion{TEnvironment, TVersion, TIdentityManager}"/>.
        /// </summary>
        public ITypeUniqueIdentifier UniqueIdentifier
        {
            get
            {
                return this._uniqueIdentifier ?? (this._uniqueIdentifier = InitializeUniqueIdentifier());
            }
        }

        /// <summary>
        /// Returns the <see cref="IVreLibraryVersion{TEnvironment, TVersion, TIdentityManager}"/> which currently contains
        /// the <see cref="IVreTypeVersion"/>.
        /// </summary>
        public IVreLibraryVersion<TEnvironment, TVersion, TIdentityManager> Library
        {
            get { return this._library; }
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", this.RootType, this.Library.UniqueIdentifier);
        }

        #region Initialization

        private ITypeUniqueIdentifier InitializeUniqueIdentifier()
        {
            if (this._rootType != null && this._rootType.Parent is IVreType<TEnvironment, TVersion, TIdentityManager>)
            {
                ITypeUniqueIdentifier parentUId = ((IVreType<TEnvironment, TVersion, TIdentityManager>)(this._rootType)).Versions[this.Version].UniqueIdentifier;
                if (parentUId is IGeneralTypeUniqueIdentifier)
                {
                    var gParUId = (IGeneralTypeUniqueIdentifier)parentUId;
                    if (this._rootType.IsGenericType)
                        return gParUId.GetNestedIdentifier(this.RootType.Name, this._rootType.GenericParameterCount);
                    else
                        return gParUId.GetNestedIdentifier(this.RootType.Name);
                }
            }
            var nsParent = (this._rootType.Parent) as IVreNamespace<TEnvironment, TVersion, TIdentityManager>;
            if (nsParent != null)
            {
                if (this._rootType.IsGenericType)
                    return this.Library.UniqueIdentifier.GetTypeIdentifier(nsParent.FullName, this._rootType.Name, this._rootType.GenericParameterCount);
                else
                    return this.Library.UniqueIdentifier.GetTypeIdentifier(nsParent.FullName, this._rootType.Name);
            }
            else
            {
                if (this._rootType.IsGenericType)
                    return this.Library.UniqueIdentifier.GetTypeIdentifier((string)null, this._rootType.Name, this._rootType.GenericParameterCount);
                else
                    return this.Library.UniqueIdentifier.GetTypeIdentifier((string)null, this._rootType.Name);
            }
        }
        #endregion

        protected override bool FilterType(IVreTypeVersion<TEnvironment, TVersion, TIdentityManager> type)
        {
            if (type == null ||
                type.RootType == null)
                return false;
            return type.RootType.Parent == this.RootType;
        }
    }
}
