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

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines properties and methods for working 
    /// with an expression that casts another
    /// expression to a series of potential types based 
    /// upon its runtime type.
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
    public interface IVariadicTypeCastExpression
    {
        /// <summary>
        /// Returns the <see cref="ITypeCollection"/> which denotes the
        /// types to cast the <see cref="Target"/> to.
        /// </summary>
        ITypeCollection CastTypes { get; }
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> the 
        /// <see cref="ITypeCastExpression"/> casts to one of
        /// the <see cref="IType"/> values from 
        /// <see cref="CastTypes"/>.
        /// </summary>
        IExpression Target { get; set; }
    }
}
