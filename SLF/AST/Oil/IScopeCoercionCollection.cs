using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with a series of
    /// <see cref="IScopeCoercion"/> instances which aid in the 
    /// identity resolution (linking) phase of compilation.
    /// </summary>
    public interface IScopeCoercionCollection :
        IControlledStateCollection<IScopeCoercion>,
        IProtectableComponent
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
        /// Adds a <see cref="IStaticInclusionScopeCoercion"/> via the type's <see cref="String"/> name.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name representing the identity of the type
        /// to include.</param>
        /// <returns></returns>
        IStaticInclusionScopeCoercion AddStaticName(string name);
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
        /// <summary>
        /// Adds a series of <see cref="INamedInclusionScopeCoercion"/> elements
        /// to the <see cref="IScopeCoercionCollection"/> with the <paramref name="names"/>
        /// provided.
        /// </summary>
        /// <param name="names">The series of <see cref="String"/> values which
        /// represent the names to include within the scope to aid in identity
        /// resolution.</param>
        /// <returns>A <see cref="INamedInclusionScopeCoercion"/>.</returns>
        INamedInclusionScopeCoercion[] AddNames(params string[] names);

        /// <summary>
        /// Removes a <see cref="String"/> based namespace inclusion scope
        /// coercion.
        /// </summary>
        /// <param name="namespaceName">The name of the namespace
        /// which was included by the <see cref="INamespaceInclusionScopeCoercion"/>.</param>
        /// <returns>true if the namespace name was found and removed; false, otherwise.</returns>
        bool Remove(string namespaceName);
        /// <summary>
        /// Removes a <see cref="String"/> based <see cref="INamedInclusionScopeCoercion"/>
        /// based off of the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value associated to the 
        /// <see cref="INamedInclusionScopeCoercion"/>.</param>
        /// <returns>returns true if a named inclusion was found and removed; false, otherwise.</returns>
        bool RemoveName(string name);
        /// <summary>
        /// Removes an <paramref name="includedType"/> from the <see cref="IScopeCoercionCollection"/>
        /// provided it is present.
        /// </summary>
        /// <param name="includedType">The <see cref="IType"/> to remove.</param>
        /// <returns>true if the <paramref name="includedType"/> was found within the 
        /// <see cref="IScopeCoercionCollection"/>.</returns>
        bool Remove(IType includedType);
    }
}
