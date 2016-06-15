using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{

    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// enumeration which defines a series of constant fields of a 
    /// specified common data type.
    /// </summary>
#if DEBUG
    [VisitorTargetAttribute("IntermediateTypeVisitor", ContextualVisitor  = true)]
    [VisitorTargetAttribute("IntermediateTypeVisitor", YieldingVisitor    = true)]
    [VisitorTargetAttribute("IntermediateTypeVisitor", ContextualVisitor  = true,
                                                       YieldingVisitor    = true)]
    [VisitorTargetAttribute("IntermediateTypeVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateTypeVisitor")]
#endif
    public interface IIntermediateEnumType :
        IIntermediateType<IGeneralTypeUniqueIdentifier, IEnumType, IIntermediateEnumType>,
        IIntermediateMemberParent,
        IFieldParent<IEnumFieldMember, IEnumType>,
        IEnumType
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateEnumFieldMemberDictionary"/>
        /// for the current <see cref="IIntermediateEnumType"/>.
        /// </summary>
#if DEBUG
        [VisitorPropertyRequirement("HasFields")]
#endif
        new IIntermediateEnumFieldMemberDictionary Fields { get; }
        /// <summary>
        /// Returns/sets the <see cref="EnumerationBaseType"/> for the 
        /// <see cref="IIntermediateEnumType"/>.
        /// </summary>
        new EnumerationBaseType ValueType { get; set; }
        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> in which
        /// the <see cref="IIntermediateEnumType"/> is declared
        /// </summary>
#if DEBUG
        [VisitorImplementationIgnoreProperty]
#endif
        new IIntermediateAssembly Assembly { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateIdentityManager"/> which
        /// helps resolve type identities.
        /// </summary>
        new IIntermediateIdentityManager IdentityManager { get; }
        bool HasFields { get; }
    }
}
