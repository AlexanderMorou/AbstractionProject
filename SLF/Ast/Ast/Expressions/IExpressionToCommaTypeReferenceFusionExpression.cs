using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with an expression
    /// which an be fused with a series of type-parameters.
    /// </summary>
    public interface IExpressionToCommaTypeReferenceFusionExpression :
        IFusionTargetExpression,
        IFusionCommaTargetExpression
    {
        /// <summary>
        /// Returns/sets the expression, which is fusable to the comma delimited
        /// series of type-references, associated to the 
        /// <see cref="IExpressionToCommaTypeReferenceFusionExpression"/>
        /// </summary>
        IFusionTypeCollectionTargetExpression Left { get; set; }
        /// <summary>
        /// Returns the <see cref="ITypeCollection"/> which represents the
        /// type-reference series fused to the <see cref="Left"/> portion
        /// of the <see cref="IExpressionToCommaTypeReferenceFusionExpression"/>.
        /// </summary>
        ITypeCollection Right { get; }
    }
}
