using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
    /// <summary>
    /// Defines properties and methods for working with a language integrated
    /// query from clause with an explicit type assigned to the range variable.
    /// </summary>
#if DEBUG
    [VisitorTargetAttribute("LinqClauseVisitor", ContextualVisitor  = true)]
    [VisitorTargetAttribute("LinqClauseVisitor", YieldingVisitor    = true)]
    [VisitorTargetAttribute("LinqClauseVisitor", ContextualVisitor  = true,
                                                 YieldingVisitor    = true)]
    [VisitorTargetAttribute("LinqClauseVisitor")]
    [VisitorTargetAttribute("ExpressionVisitor", DerivedThroughInheriting = "LinqClauseVisitor")]
    [VisitorTargetAttribute("IntermediateCodeVisitor", DerivedThroughInheriting = "ExpressionVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateCodeVisitor")]
#endif
    public interface ILinqTypedFromClause :
        ILinqFromClause
    {
        /// <summary>
        /// Returns the <see cref="ILinqTypedRangeVariable"/> associated
        /// to the <see cref="ILinqTypedFromClause"/>.
        /// </summary>
        new ILinqTypedRangeVariable RangeVariable { get; }
    }
}
