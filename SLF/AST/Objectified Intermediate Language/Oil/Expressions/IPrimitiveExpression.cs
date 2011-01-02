using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a primitive expression that represents a literal value.
    /// </summary>
    public interface IPrimitiveExpression :
        IMemberParentReferenceExpression,
        IUnaryOperationPrimaryTerm,
        IFusionCommaTargetExpression
    {
        /// <summary>
        /// Returns/sets the value represented by the <see cref="IPrimitiveExpression"/>.
        /// </summary>
        object Value { get; set; }
        /// <summary>
        /// Returns the <see cref="PrimitiveType"/> the <see cref="IPrimitiveExpression"/> is.
        /// </summary>
        PrimitiveType PrimitiveType { get; }
        void Visit(IIntermediatePrimitiveVisitor visitor);
    }
}
