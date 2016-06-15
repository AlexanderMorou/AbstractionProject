using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a decorating expression.
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
    public interface IDecoratingExpression :
        IUnaryOperationPrimaryTerm
    {
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which
        /// is decorated by the <see cref="IDecoratingExpression"/>.
        /// </summary>
        IExpression ContainedExpression { get; set; }
        /// <summary>
        /// Returns the <see cref="IMalleableExpressionCollection{T}"/>
        /// containing the <see cref="IDecorationExpression"/> elements
        /// to decorate the sides of the <see cref="ContainedExpression"/>.
        /// </summary>
        IMalleableExpressionCollection<IDecorationExpression> Decorations { get; }
    }
}
