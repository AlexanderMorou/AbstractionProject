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
    /// query expression from clause which includes a data source for
    /// iteration into the series.
    /// </summary>
#if DEBUG
    [VisitorTargetAttribute("LinqClauseVisitor", ContextualVisitor  = true)]
    [VisitorTargetAttribute("LinqClauseVisitor", YieldingVisitor    = true)]
    [VisitorTargetAttribute("LinqClauseVisitor", ContextualVisitor  = true,
                                                 YieldingVisitor    = true)]
    [VisitorTargetAttribute("LinqClauseVisitor")]
    [VisitorTargetAttribute("ExpressionVisitor", DerivedThroughInheriting = "LinqClauseVisitor")]
    [VisitorTargetAttribute("CommonExpressionVisitor", DerivedThroughInheriting = "LinqClauseVisitor", YieldingVisitor = true, ContextualVisitor = true)]
    [VisitorTargetAttribute("IntermediateCodeVisitor", DerivedThroughInheriting = "ExpressionVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateCodeVisitor")]
#endif
    public interface ILinqFromClause  :
        ILinqClause
    {
        /// <summary>
        /// Returns the <see cref="ILinqRangeVariable"/> associated
        /// to the <see cref="ILinqFromClause"/>.
        /// </summary>
        ILinqRangeVariable RangeVariable { get; }
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> from which
        /// the langauge integrated query obtains its data from
        /// for the current clause.
        /// </summary>
        IExpression RangeSource { get; set; }
    }
}
