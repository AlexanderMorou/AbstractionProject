using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with
    /// a fusion between a <see cref="IFusionCommaTargetExpression"/>
    /// and a <see cref="IMalleableExpressionCollection"/>.
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
    public interface IExpressionToCommaFusionExpression :
        IFusionCommaTargetExpression,
        IMemberParentReferenceExpression
    {
        /// <summary>
        /// The <see cref="IFusionCommaTargetExpression"/> which acts as the left side of the expression.
        /// </summary>
        IFusionCommaTargetExpression Left { get; set; }
        /// <summary>
        /// The <see cref="IMalleableExpressionCollection"/> which contains the expression set that is fused with
        /// the <see cref="Left"/> side.
        /// </summary>
        IMalleableExpressionCollection Right { get; }
    }
}
