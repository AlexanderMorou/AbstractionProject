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
    /// Defines properties and methods for working with a type from a versioned environment.
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
    public interface IVreType<TEnvironment, TVersion, TIdentityManager> :
        IVreTypeParent<TEnvironment, TVersion, TIdentityManager>
        where TEnvironment :
            IVreEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            IVreEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        /// <summary>
        /// Returns the <typeparamref name="TEnvironment"/> instance from which the
        /// <see cref="IVreType{TEnvironment, TVersion, TIdentityManager}"/> was derived.
        /// </summary>
        TEnvironment Environment { get; }
        /// <summary>
        /// Returns the <see cref="IControlledDictionary{TKey, TValue}"/> which contains the set
        /// of version specific types of the current <see cref="IVreType{TEnvironment, TVersion, TIdentityManager}"/>.
        /// </summary>
        IControlledDictionary<TVersion, IVreTypeVersion<TEnvironment, TVersion, TIdentityManager>> Versions { get; }
        /// <summary>
        /// Returns the <see cref="IVreTypeParent{TEnvironment, TVersion, TIdentityManager}"/>
        /// which contains the current <see cref="IVreType{TEnvironment, TVersion, TIdentityManager}"/>.
        /// </summary>
        IVreTypeParent<TEnvironment, TVersion, TIdentityManager> Parent { get; }
        /// <summary>
        /// Returns the <typeparamref name="TVersion"/> at which the current <see cref="IVreType{TEnvironment, TVersion, TIdentityManager}"/>
        /// was introduced.
        /// </summary>
        TVersion Introduced { get; }
        /// <summary>
        /// Returns the <typeparamref name="TVersion"/> at which the current <see cref="IVreType{TEnvironment, TVersion, TIdentityManager}"/>
        /// was deprecated.
        /// </summary>
        IEnumerable<TVersion> Deprecated { get; }
        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> of <typeparamref name="TVersion"/>s in which the 
        /// <see cref="IVreType{TEnvironment, TVersion, TIdentityManager}"/> is supported by the runtime.
        /// </summary>
        /// <remarks>This is the set of versions from the type in which it is not deprecated.</remarks>
        IEnumerable<TVersion> Supported { get; }
        /// <summary>
        /// Returns the <see cref="String"/> value denoting the name of the current <see cref="IVreType{TEnvironment, TVersion, TIdentityManager}"/>.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Returns the <see cref="TypeKind"/> of the type.
        /// </summary>
        TypeKind Type { get; }
    }
    public interface IVreTypeParent<TEnvironment, TVersion, TIdentityManager>
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
        /// types of the <see cref="IVreTypeParent{TEnvironment, TVersion, TIdentityManager}"/>.
        /// </summary>
        IControlledDictionary<string, IVreType<TEnvironment, TVersion, TIdentityManager>> Types { get; }
    }
}
