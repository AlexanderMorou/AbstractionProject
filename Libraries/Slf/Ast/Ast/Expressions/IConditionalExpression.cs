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
    /// Defines properties and methods for working with a conditional
    /// expression which denotes a boolean check expression
    /// that leads to either the true or false portion of the
    /// expression.
    /// </summary>
    /// <remarks>Essentially control flow result selection.</remarks>
#if DEBUG
    [VisitorTargetAttribute("ExpressionVisitor", ContextualVisitor  = true)]
    [VisitorTargetAttribute("ExpressionVisitor", YieldingVisitor    = true)]
    [VisitorTargetAttribute("ExpressionVisitor", ContextualVisitor  = true,
                                                 YieldingVisitor = true)]
    [VisitorTargetAttribute("ExpressionVisitor")]
    [VisitorTargetAttribute("IntermediateCodeVisitor", DerivedThroughInheriting = "ExpressionVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateCodeVisitor")]
#endif
    public interface IConditionalExpression :
        INaryOperandExpression
    {
        /// <summary>
        /// Returns/sets the check part of the conditional.
        /// </summary>
        IExpression CheckPart { get; set; }
        /// <summary>
        /// Returns/sets the true part of the conditional.
        /// </summary>
        IExpression TruePart { get; set; }
        /// <summary>
        /// Returns/sets the false part of the conditional.
        /// </summary>
        IExpression FalsePart { get; set; }
    }
}
