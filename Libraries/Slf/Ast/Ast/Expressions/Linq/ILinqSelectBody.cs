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
    /// Defines properties and methods for working with a body of a
    /// language integrated query which ends with a select clause.
    /// </summary>
#if DEBUG
    [VisitorTargetAttribute("LinqBodyVisitor", ContextualVisitor    = true)]
    [VisitorTargetAttribute("LinqBodyVisitor", YieldingVisitor      = true)]
    [VisitorTargetAttribute("LinqBodyVisitor", ContextualVisitor    = true,
                                               YieldingVisitor      = true)]
    [VisitorTargetAttribute("LinqBodyVisitor")]
    [VisitorTargetAttribute("ExpressionVisitor", DerivedThroughInheriting = "LinqBodyVisitor")]
    [VisitorTargetAttribute("CommonExpressionVisitor", DerivedThroughInheriting = "LinqBodyVisitor", YieldingVisitor = true, ContextualVisitor = true)]
    [VisitorTargetAttribute("IntermediateCodeVisitor", DerivedThroughInheriting = "ExpressionVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateCodeVisitor")]
#endif
    public interface ILinqSelectBody :
        ILinqBody
    {
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which denotes what is selected
        /// as a result of the language integrated query.
        /// </summary>
        IExpression Selection { get; set; }
    }
}
