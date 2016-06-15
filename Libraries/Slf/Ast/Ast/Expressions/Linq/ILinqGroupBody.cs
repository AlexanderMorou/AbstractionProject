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
    /// Defines properties and methods for working with the body
    /// of a language integrated query in which the results are grouped
    /// via a key selector.
    /// </summary>
#if DEBUG
    [VisitorTargetAttribute("LinqBodyVisitor", ContextualVisitor    = true)]
    [VisitorTargetAttribute("LinqBodyVisitor", YieldingVisitor      = true)]
    [VisitorTargetAttribute("LinqBodyVisitor", ContextualVisitor    = true,
                                               YieldingVisitor      = true)]
    [VisitorTargetAttribute("LinqBodyVisitor")]
    [VisitorTargetAttribute("ExpressionVisitor",       DerivedThroughInheriting = "LinqBodyVisitor")]
    [VisitorTargetAttribute("IntermediateCodeVisitor", DerivedThroughInheriting = "ExpressionVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateCodeVisitor")]
#endif
    public interface ILinqGroupBody :
        ILinqSelectBody
    {
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which acts
        /// as the key for grouping the <see cref="ILinqSelectBody.Selection"/>s.
        /// </summary>
        IExpression Key { get; set; }
    }
}
