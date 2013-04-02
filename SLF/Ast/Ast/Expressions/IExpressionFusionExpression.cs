using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with an expression
    /// fusion expression which fuses
    /// an <see cref="IFusionTargetExpression"/>
    /// with a <see cref="IFusionTermExpression"/>.
    /// </summary>
    public interface IExpressionFusionExpression :
        IFusionTypeCollectionTargetExpression,
        IFusionCommaTargetExpression,
        IUnaryOperationPrimaryTerm,
        IMemberParentReferenceExpression
    {
        /// <summary>
        /// The <see cref="IFusionTargetExpression"/>
        /// which acts as the left side of
        /// the expression.
        /// </summary>
        IFusionTargetExpression Left { get; set; }
        /// <summary>
        /// The <see cref="IFusionTermExpression"/>
        /// which acts as the right side of
        /// the expression.
        /// </summary>
        IFusionTermExpression Right { get; set; }
    }
}
