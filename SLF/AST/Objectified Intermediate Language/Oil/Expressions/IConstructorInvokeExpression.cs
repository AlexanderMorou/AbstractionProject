using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public interface IConstructorInvokeExpression :
        IMemberParentReferenceExpression,
        IUnaryOperationPrimaryTerm,
        IFusionCommaTargetExpression,
        IFusionTermExpression,
        IStatementExpression
    {
        /// <summary>
        /// Returns the <see cref="IConstructorPointerReferenceExpression"/>
        /// which identifies the name and 
        /// type-parameters of the method 
        /// to use as well as the type-signature
        /// used for the parameters.
        /// </summary>
        IConstructorPointerReferenceExpression Reference { get; }
        /// <summary>
        /// The <see cref="ICallParameterSet"/> used
        /// to invoke the constructor.
        /// </summary>
        /// <remarks>Does not necessarily have to
        /// coincide with the <see cref="IConstructorPointerReferenceExpression.Signature"/>
        /// exactly; however, it does need to have necessary
        /// implicit operators for conversion if it does not.</remarks>
        ICallParameterSet Parameters { get; }
    }
}
