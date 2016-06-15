using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Vre
{
    /// <summary>
    /// Defines properties and methods for working with a type from a versioned environment from a specific
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
    public interface IVreTypeVersion<TEnvironment, TVersion, TIdentityManager> :
        IVreTypeParentVersion<TEnvironment, TVersion, TIdentityManager>
        where TEnvironment :
            IVreEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            IVreEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        /// <summary>
        /// Returns the <typeparamref name="TEnvironment"/> from which the
        /// <see cref="IVreTypeVersion{TEnvironment, TVersion, TIdentityManager}"/> was derived.
        /// </summary>
        TEnvironment Environment { get; }
        /// <summary>
        /// Returns the <see cref="IVreType{TEnvironment, TVersion, TIdentityManager}"/>
        /// <see cref="IVreTypeVersion{TEnvironment, TVersion, TIdentityManager}"/> was derived.
        /// </summary>
        IVreType<TEnvironment, TVersion, TIdentityManager> RootType { get; }
        /// <summary>
        /// Returns the <see cref="TypeKind"/> of the <see cref="IVreTypeVersion{TEnvironment, TVersion, TIdentityManager}"/>.
        /// </summary>
        TypeKind Type { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> of the <see cref="IVreTypeVersion{TEnvironment, TVersion, TIdentityManager}"/>.
        /// </summary>
        ITypeUniqueIdentifier UniqueIdentifier { get; }
        /// <summary>
        /// Returns the <see cref="IVreLibraryVersion{TEnvironment, TVersion, TIdentityManager}"/> which currently contains
        /// the <see cref="IVreTypeVersion"/>.
        /// </summary>
        IVreLibraryVersion<TEnvironment, TVersion, TIdentityManager> Library { get; }
    }
    public interface IVreTypeParentVersion<TEnvironment, TVersion, TIdentityManager> :
        IDisposable
        where TEnvironment :
            IVreEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            IVreEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        /// <summary>
        /// Returns the <see cref="IControlledDictionary{TKey, TValue}"/> which contains the 
        /// types of the <see cref="IVreTypeParentVersion{TEnvironment, TVersion, TIdentityManager}"/>.
        /// </summary>
        IControlledDictionary<string, IVreTypeVersion<TEnvironment, TVersion, TIdentityManager>> Types { get; }
        /// <summary>
        /// Returns the <typeparamref name="TVersion"/> from which the
        /// <see cref="IVreTypeVersion{TEnvironment, TVersion, TIdentityManager}"/> was derived.
        /// </summary>
        TVersion Version { get; }
    }
}
