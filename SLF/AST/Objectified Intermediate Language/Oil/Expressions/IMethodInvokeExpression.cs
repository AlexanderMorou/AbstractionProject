using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with
    /// a method call.
    /// </summary>
    public interface IMethodInvokeExpression :
        IMemberParentReferenceExpression,
        IUnaryOperationPrimaryTerm,
        IFusionCommaTargetExpression,
        IFusionTermExpression,
        IStatementExpression
    {
        /// <summary>
        /// Returns the <see cref="IMethodPointerReferenceExpression"/>
        /// which identifies the name and 
        /// type-parameters of the method 
        /// to use as well as the type-signature
        /// used for the parameters.
        /// </summary>
        IMethodPointerReferenceExpression Reference { get; }
        /// <summary>
        /// The <see cref="IMalleableExpressionCollection"/> used
        /// to invoke the method.
        /// </summary>
        /// <remarks>Does not necessarily have to
        /// coincide with the <see cref="IMethodPointerReferenceExpression.Signature"/>
        /// exactly; however, it does need to have necessary
        /// implicit operators if it does not.</remarks>
        IMalleableExpressionCollection Parameters { get; }
    }
}