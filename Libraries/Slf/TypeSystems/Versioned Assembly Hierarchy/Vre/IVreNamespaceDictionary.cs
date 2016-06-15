using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Vre
{
    public interface IVreNamespaceDictionary<TEnvironment, TVersion, TIdentityManager> :
        IControlledDictionary<string, IVreNamespace<TEnvironment, TVersion, TIdentityManager>>
        where TEnvironment :
            IVreEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            IVreEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        /// <summary>
        /// Returns a <see cref="IVreNamespace{TEnvironment, TVersion, TIdentityManager}"/>
        /// with the <paramref name="dottedName"/> provided.
        /// </summary>
        /// <param name="dottedName">The <see cref="String"/> representing the dotted name of the namespace
        /// from the current point in the hierarchy.</param>
        /// <returns>A <see cref="IVreNamespace{TEnvironment, TVersion, TIdentityManager}"/>
        /// from the <paramref name="dottedName"/> provided.</returns>
        IVreNamespace<TEnvironment, TVersion, TIdentityManager> GetNamespace(string dottedName);
    }
    public interface IVreNamespaceDictionaryVersion<TEnvironment, TVersion, TIdentityManager> :
        IControlledDictionary<string, IVreNamespaceVersion<TEnvironment, TVersion, TIdentityManager>>
        where TEnvironment :
            IVreEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            IVreEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        /// <summary>
        /// Returns a <see cref="IVreNamespaceVersion{TEnvironment, TVersion, TIdentityManager}"/>
        /// with the <paramref name="dottedName"/> provided.
        /// </summary>
        /// <param name="dottedName">The <see cref="String"/> representing the dotted name of the namespace
        /// from the current point in the hierarchy.</param>
        /// <returns>A <see cref="IVreNamespaceVersion{TEnvironment, TVersion, TIdentityManager}"/>
        /// from the <paramref name="dottedName"/> provided.</returns>
        IVreNamespaceVersion<TEnvironment, TVersion, TIdentityManager> GetNamespace(string dottedName);
    }
}
