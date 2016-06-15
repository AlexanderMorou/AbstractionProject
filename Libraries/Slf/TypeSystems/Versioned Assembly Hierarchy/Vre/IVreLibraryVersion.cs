using AllenCopeland.Abstraction.Slf.Abstract;
using System;
using System.Collections.Generic;
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
    public interface IVreLibraryVersion<TEnvironment, TVersion, TIdentityManager> :
        IVreNamespaceParentVersion<TEnvironment, TVersion, TIdentityManager>
        where TEnvironment :
            IVreEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            IVreEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        /// <summary>
        /// Returns the <typeparamref name="TVersion"/> from which the
        /// <see cref="IVreLibraryVersion{TEnvironment, TVersion, TIdentityManager}"/> is derived.
        /// </summary>
        TVersion Version { get; }
        /// <summary>
        /// Returns the root <see cref="IVreLibrary{TEnvironment, TVersion, TIdentityManager}"/> from
        /// which the <see cref="IVreLibraryVersion{TEnvironment, TVersion, TIdentityManager}"/> is a versioned
        /// derivative ofs.
        /// </summary>
        IVreLibrary<TEnvironment, TVersion, TIdentityManager> RootLibrary { get; }
        /// <summary>
        /// Returns the <see cref="System.Version"/> which denotes the specifics of the assembly version within the
        /// current <typeparamref name="TEnvironment"/>'s <typeparamref name="TVersion"/>.
        /// </summary>
        Version AssemblyVersion { get; }
        /// <summary>
        /// Returns the <see cref="IAssemblyUniqueIdentifier"/> which represents the library at the current <see cref="Version"/>.
        /// </summary>
        IAssemblyUniqueIdentifier UniqueIdentifier { get; }
    }
}
