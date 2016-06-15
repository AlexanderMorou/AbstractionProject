using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
#if DEBUG
    [VisitorImplementationProcessAsGroup]
#endif
    public interface IIntermediateTypeParent :
        ITypeParent
    {
        bool HasScopeCoercions { get; }
        /// <summary>
        /// Returns the <see cref="IScopeCoercionCollection"/>
        /// associated to the scope coercions of the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
#if DEBUG
        [VisitorPropertyRequirement("HasScopeCoercions")]
#endif
        IScopeCoercionCollection ScopeCoercions { get; }
        /// <summary>Returns whether the <see cref="IIntermediateTypeParent"/> contains classes.</summary>
        bool HasClasses { get; }
        /// <summary>
        /// Returns the classes associated
        /// to the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
#if DEBUG
        [VisitorPropertyRequirement("HasClasses")]
#endif
        new IIntermediateClassTypeDictionary Classes { get; }
        bool HasDelegates { get; }
        /// <summary>
        /// Returns the delegates associated
        /// to the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
#if DEBUG
        [VisitorPropertyRequirement("HasDelegates")]
#endif
        new IIntermediateDelegateTypeDictionary Delegates { get; }
        bool HasEnums { get; }
        /// <summary>
        /// Returns the enumerations associated
        /// to the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
#if DEBUG
        [VisitorPropertyRequirement("HasEnums")]
#endif
        new IIntermediateEnumTypeDictionary Enums { get; }
        bool HasInterfaces { get; }
        /// <summary>
        /// Returns the interfaces associated
        /// to the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
#if DEBUG
        [VisitorPropertyRequirement("HasInterfaces")]
#endif
        new IIntermediateInterfaceTypeDictionary Interfaces { get; }
        bool HasStructs { get; }
        /// <summary>
        /// Returns the data structures associated
        /// to the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
#if DEBUG
        [VisitorPropertyRequirement("HasStructs")]
#endif
        new IIntermediateStructTypeDictionary Structs { get; }
        bool HasTypes { get; }
        /// <summary>
        /// Returns the full set of types associated to
        /// the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
#if DEBUG
        [VisitorPropertyRequirement("HasTypes")]
#endif
        new IIntermediateFullTypeDictionary Types { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> in which the
        /// <see cref="IIntermediateTypeParent"/> is defined.
        /// </summary>
#if DEBUG
        [VisitorImplementationIgnoreProperty]
#endif
        new IIntermediateAssembly Assembly { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateIdentityManager"/> which
        /// helps resolve type identities.
        /// </summary>
        IIntermediateIdentityManager IdentityManager { get; }
        /// <summary>
        /// Returns an <see cref="IEnumerable{IType}"/> which steps through the types
        /// within the type parent.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{IType}"/> which steps through the types
        /// within the type parent.</returns>
        IEnumerable<IType> GetTypes();
    }
}
