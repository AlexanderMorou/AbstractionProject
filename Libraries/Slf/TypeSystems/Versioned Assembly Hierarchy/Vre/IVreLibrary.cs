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
    /// Defines properties and methods for working with a library from a <typeparamref name="TEnvironment"/>
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
    public interface IVreLibrary<TEnvironment, TVersion, TIdentityManager> /*:
        IVreNamespaceParent<TEnvironment, TVersion, TIdentityManager>*/
        where TEnvironment :
            IVreEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            IVreEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        /// <summary>
        /// Returns the <typeparamref name="TEnvironment"/> from which the current 
        /// <see cref="IVreLibrary{TEnvironment, TVersion, TIdentityManager}"/> is
        /// derived.
        /// </summary>
        TEnvironment Environment { get; }
        /// <summary>
        /// Returns the <see cref="String"/> value denoting the name of the library.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Returns the <typeparamref name="TVersion"/> in which the <see cref="IVreLibrary{TEnvironment, TVersion, TIdentityManager}"/>
        /// was introduced.
        /// </summary>
        TVersion InitialVersion { get; }
        /// <summary>
        /// Returns the <see cref="IControlledDictionary{TKey, TValue}"/> which contains the set of version specific
        /// libraries of the current <see cref="IVreLibrary{TEnvironment, TVersion, TIdentityManager}"/>.
        /// </summary>
        IControlledDictionary<TVersion, IVreLibraryVersion<TEnvironment, TVersion, TIdentityManager>> Versions { get; }
    }
}
