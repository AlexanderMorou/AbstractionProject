using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with a series of
    /// <see cref="IScopeCoercion"/> instances which aid in the 
    /// identity resolution (linking) phase of compilation.
    /// </summary>
    public interface IScopeCoercionCollection :
        IControlledStateCollection<IScopeCoercion>
    {
        /// <summary>
        /// Adds a <see cref="ITypeInclusionScopeCoercion"/> with the
        /// <paramref name="includedType"/> and <paramref name="mergeStaticScope"/>
        /// explicitly given.
        /// </summary>
        /// <param name="includedType">The <see cref="IType"/> which
        /// is included explicitly into the active scope.</param>
        /// <param name="mergeStaticScope">Determines whether the inclusion
        /// should merge the identity of the type, or the static declarations
        /// of the type, into the active scope.</param>
        /// <returns>A <see cref="ITypeInclusionScopeCoercion"/> instance
        /// which denotes the specific type to include, and whether its to
        /// merge the type identity or the static declarations of the type.
        /// </returns>
        ITypeInclusionScopeCoercion Add(IType includedType, bool mergeStaticScope);
        /// <summary>
        /// Adds a <see cref="ITypeInclusionScopeCoercion"/> with the 
        /// <paramref name="includedType"/>.
        /// </summary>
        /// <param name="includedType">The <see cref="IType"/> which
        /// is included explicitly into the active scope.</param>
        /// <param name="alternateName">The actual <see cref="String"/>
        /// name to refer to when performing identity resolution upon symbols
        /// representing a type.</param>
        /// <returns>a <see cref="ITypeInclusionRenameScopeCoercion"/>
        /// instance which denotes the specific type to include and the 
        /// <paramref name="alternateName"/> to refer to it by.</returns>
        ITypeInclusionRenameScopeCoercion Add(IType includedType, string alternateName);
        /// <summary>
        /// Adds a <see cref="INamespaceInclusionScopeCoercion"/> with the 
        /// <paramref name="namespaceName"/> identifying the target
        /// declarations to merge into the active scope.
        /// </summary>
        /// <param name="namespaceName">The <see cref="String"/> which represents
        /// the name of the namespace whose declarations should be merged into 
        /// the active scope during identity resolution.</param>
        /// <returns>A <see cref="INamespaceInclusionScopeCoercion"/> which
        /// denotes the details of the scope coercion.</returns>
        INamespaceInclusionScopeCoercion Add(string namespaceName);
        /// <summary>
        /// Adds a <see cref="INamespaceInclusionScopeCoercion"/> with the 
        /// <paramref name="namespaceName"/> to merge into the active scope
        /// as the <paramref name="alternateName"/> specifies.
        /// </summary>
        /// <param name="namespaceName">The <see cref="String"/> which represents
        /// the name of the namespace whose which should be alternately addressed
        /// as <paramref name="alternateName"/>.</param>
        /// <param name="alternateName">The <see cref="String"/> new name of the
        /// <paramref name="namespaceName"/>.</param>
        /// <returns>A <see cref="INamespaceInclusionRenameScopeCoercion"/>
        /// which denotes the details of the scope coercion.</returns>
        INamespaceInclusionRenameScopeCoercion Add(string namespaceName, string alternateName);
        /// <summary>
        /// Adds a <see cref="INamedInclusionScopeCoercion"/> which details the name
        /// of a namespace or a type whose identity needs resolved prior to further
        /// identity resolution.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the
        /// name to merge into the active scope during identity resolution.</param>
        /// <returns>A <see cref="INamedInclusionScopeCoercion"/> which denotes
        /// the details of the scope coercion.</returns>
        INamedInclusionScopeCoercion AddName(string name);
        /// <summary>
        /// Adds a <see cref="INamedInclusionScopeCoercion"/> which details the 
        /// <paramref name="name"/> of a namespace or a type whose identity needs 
        /// resolved prior to further identity resolution to be referred to by
        /// an <paramref name="alternateName"/>.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the 
        /// type or namespace to refer to by an <paramref name="alternateName"/>.</param>
        /// <param name="alternateName">The <see cref="String"/> value representing
        /// the actual name the type or namespace is referred to as during
        /// identity resolution.</param>
        /// <returns>A <see cref="INamedInclusionScopeCoercion"/> which denotes
        /// the details of the scope coercion.</returns>
        INamedInclusionRenameScopeCoercion AddName(string name, string alternateName);

    }
}
