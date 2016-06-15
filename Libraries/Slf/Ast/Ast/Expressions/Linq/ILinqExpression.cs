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
    /// Defines properties and methods for working with a language integrated query.
    /// </summary>
#if DEBUG
    [VisitorTargetAttribute("ExpressionVisitor", ContextualVisitor  = true)]
    [VisitorTargetAttribute("ExpressionVisitor", YieldingVisitor    = true)]
    [VisitorTargetAttribute("ExpressionVisitor", ContextualVisitor  = true,
                                                 YieldingVisitor    = true)]
    [VisitorTargetAttribute("ExpressionVisitor")]
    [VisitorTargetAttribute("IntermediateCodeVisitor", DerivedThroughInheriting = "ExpressionVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateCodeVisitor")]
#endif
    public interface ILinqExpression :
        IExpression
    {
        /// <summary>
        /// Returns the <see cref="ILinqFromClause"/> which
        /// specifies the first part of the linq expression.
        /// </summary>
        ILinqFromClause From { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="ILinqBody"/> which denotes
        /// the clauses and potential further fusion bodies
        /// associated to the <see cref="ILinqExpression"/>.
        /// </summary>
        ILinqBody Body { get; set; }
    }
}
