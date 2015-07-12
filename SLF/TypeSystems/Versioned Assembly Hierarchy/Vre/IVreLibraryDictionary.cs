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
    /// Defines properties and methods for working with a dictionary of libraries from a <typeparamref name="TEnvironment"/>
    /// which contains multiple variants of a given library based on the <typeparamref name="TVersion"/> of the
    /// <typeparamref name="TEnvironment"/>.
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
    public interface IVreLibraryDictionary<TEnvironment, TVersion, TIdentityManager> :
        IControlledDictionary<string, IVreLibrary<TEnvironment, TVersion, TIdentityManager>>
        where TEnvironment :
            IVreEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            IVreEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        /// <summary>
        /// Returns the <typeparamref name="TEnvironment"/> from which the <see cref="IVreLibraryDictionary{TEnvironment, TVersion, TIdentityManager}"/> 
        /// belongs.
        /// </summary>
        TEnvironment Environment { get; }
    }

    /// <summary>
    /// Defines properties and methods for working with a specific version of a verionsable dictionary of libraries.
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
    public interface IVreLibraryDictionaryVersion<TEnvironment, TVersion, TIdentityManager> :
        IControlledDictionary<string, IVreLibraryVersion<TEnvironment, TVersion, TIdentityManager>>
        where TEnvironment :
            IVreEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            IVreEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        /// <summary>
        /// Returns the <typeparamref name="TEnvironment"/> from which the <typeparamref name="TVersion"/> belongs.
        /// </summary>
        TEnvironment Environment { get; }
        /// <summary>
        /// Returns the <typeparamref name="TVersion"/> from which the <see cref="IVreLibraryDictionaryVersion{TEnvironment, TVersion, TIdentityManager}"/> 
        /// belongs.
        /// </summary>
        TVersion Version { get; }
    }
}
