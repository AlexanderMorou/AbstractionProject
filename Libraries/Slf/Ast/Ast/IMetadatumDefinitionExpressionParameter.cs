using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with a metadatum
    /// parameter whose constant value is derived from an expression.
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
    public interface IMetadatumDefinitionExpressionParameter :
        IMetadatumDefinitionParameter
    {
        /// <summary>
        /// Returns the <see cref="IExpression"/> which denotes the
        /// constant value of the parameter.
        /// </summary>
        new IExpression Value { get; set; }
    }
    /// <summary>
    /// Defines properties and methods for working with a metadatum
    /// parameter whose constant value is derived from an expression.
    /// </summary>
    public interface IMetadatumDefinitionNamedExpressionParameter :
        IMetadatumDefinitionNamedParameter
    {
        /// <summary>
        /// Returns the <see cref="IExpression"/> which denotes the
        /// constant value of the parameter.
        /// </summary>
        new IExpression Value { get; set; }
    }
}
