using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with an 
    /// intermediate type parent.
    /// </summary>
    public interface IIntermediateTypeParent :
        ITypeParent
    {
        /// <summary>
        /// Returns the <see cref="IScopeCoercionCollection"/>
        /// associated to the scope coercions of the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
        IScopeCoercionCollection ScopeCoercions { get; }
        /// <summary>
        /// Returns the classes associated
        /// to the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
        new IIntermediateClassTypeDictionary Classes { get; }
        /// <summary>
        /// Returns the delegates associated
        /// to the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
        new IIntermediateDelegateTypeDictionary Delegates { get; }
        /// <summary>
        /// Returns the enumerations associated
        /// to the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
        new IIntermediateEnumTypeDictionary Enums { get; }
        /// <summary>
        /// Returns the interfaces associated
        /// to the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
        new IIntermediateInterfaceTypeDictionary Interfaces { get; }
        /// <summary>
        /// Returns the data structures associated
        /// to the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
        new IIntermediateStructTypeDictionary Structs { get; }
        /// <summary>
        /// Returns the full set of types associated to
        /// the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
        new IIntermediateFullTypeDictionary Types { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> in which the
        /// <see cref="IIntermediateTypeParent"/> is defined.
        /// </summary>
        new IIntermediateAssembly Assembly { get; }
        /// <summary>
        /// Returns the <see cref="ITypeIdentityManager"/> which
        /// helps resolve type identities.
        /// </summary>
        ITypeIdentityManager IdentityManager { get; }
    }
}
