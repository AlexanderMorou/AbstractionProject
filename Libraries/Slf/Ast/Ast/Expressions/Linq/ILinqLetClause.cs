using System;
using System.Collections.Generic;
using System.Text;
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
    /// query let clause for assigning a range variable to a given value for
    /// later use in the query.
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
    public interface ILinqLetClause :
        ILinqClause
    {
        /// <summary>
        /// Returns the <see cref="ILinqRangeVariable"/> defined by the
        /// <see cref="ILinqLetClause"/>.
        /// </summary>
        ILinqRangeVariable RangeVariable { get; }
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> to assign to the
        /// range variable.
        /// </summary>
        IExpression RangeSource { get; set; }
    }
}
