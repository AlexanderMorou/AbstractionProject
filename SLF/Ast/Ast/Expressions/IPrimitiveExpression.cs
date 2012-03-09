using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// Visits the <paramref name="visitor"/> provided using the appropriate overload.
        /// </summary>
        /// <param name="visitor">The <see cref="IPrimitiveVisitor"/> to visit.</param>
        void Visit(IPrimitiveVisitor visitor);
    }
}
