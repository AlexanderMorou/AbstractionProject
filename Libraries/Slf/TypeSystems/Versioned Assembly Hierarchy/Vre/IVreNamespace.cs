﻿using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Vre
{
    /// <summary>
    /// Defines properties and methods for working with the namespace of a given
    /// versioned runtime environment.
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
    public interface IVreNamespace<TEnvironment, TVersion, TIdentityManager> :
        IVreNamespaceParent<TEnvironment, TVersion, TIdentityManager>
        where TEnvironment :
            IVreEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            IVreEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        /// <summary>
        /// Returns the <see cref="String"/> value denoting the name of the <see cref="IVreNamespace{TEnvironment, TVersion, TIdentityManager}"/>.
        /// </summary>
        string Name { get; }
        string FullName { get; }
        IVreNamespaceParent<TEnvironment, TVersion, TIdentityManager> Parent { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with the parent of a namespace of a given
    /// versioned runtime environment.
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
    public interface IVreNamespaceParent<TEnvironment, TVersion, TIdentityManager> :
        IVreTypeParent<TEnvironment, TVersion, TIdentityManager>,
        IDisposable
        where TEnvironment :
            IVreEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            IVreEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        IVreNamespaceDictionary<TEnvironment, TVersion, TIdentityManager> Namespaces { get; }
    }
}
