using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines properties and methods for working with a field
    /// defined on an intermediate enumeration.
    /// </summary>
#if DEBUG
    [VisitorTargetAttribute("IntermediateMemberVisitor", ContextualVisitor  = true)]
    [VisitorTargetAttribute("IntermediateMemberVisitor", YieldingVisitor    = true)]
    [VisitorTargetAttribute("IntermediateMemberVisitor", ContextualVisitor  = true,
                                                         YieldingVisitor    = true)]
    [VisitorTargetAttribute("IntermediateMemberVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateMemberVisitor")]
#endif
    public interface IIntermediateEnumFieldMember :
        IIntermediateMember<IGeneralMemberUniqueIdentifier, IEnumType, IIntermediateEnumType>,
        IIntermediateFieldMember,
        IEnumFieldMember
    {

        /// <summary>
        /// Returns/sets the <see cref="IIntermediateEnumFieldValue"/> the 
        /// <see cref="IIntermediateEnumFieldMember"/> is.
        /// </summary>
        IIntermediateEnumFieldValue Value { get; set; }
        /// <summary>
        /// Obtains a reference expression which refers to the current
        /// <see cref="IIntermediateEnumFieldMember"/>.
        /// </summary>
        /// <param name="source">Must be null.</param>
        /// <returns>A <see cref="IFieldReferenceExpression"/> which refers to the current
        /// <see cref="IIntermediateEnumFieldMember"/>.</returns>
        /// <remarks>Hides base definition, <paramref name="source"/> should always be null.</remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="source"/> is NOT null.</exception>
        new IFieldReferenceExpression GetReference(IMemberParentReferenceExpression source = null);
    }
}
