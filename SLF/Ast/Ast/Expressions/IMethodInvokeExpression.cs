using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines generic properties and methods for working with
    /// a method call.
    /// </summary>
    /// <typeparam name="TSignatureParameter">The type of parameter used in the <typeparamref name="TSignature"/>.</typeparam>
    /// <typeparam name="TSignature">The type of signature used as a parent of <typeparamref name="TSignatureParameter"/> instances.</typeparam>
    /// <typeparam name="TParent">The parent that contains the <typeparamref name="TSignature"/> 
    /// instances.</typeparam>
    public interface IMethodInvokeExpression<TSignatureParameter, TSignature, TParent> :
        IMethodInvokeExpression
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
        where TParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TParent>
    {
        /// <summary>
        /// Returns the <see cref="IMethodPointerReferenceExpression{TSignatureParameter, TSignature, TParent}"/>
        /// which identifies the name and type-parameters of
        /// the method to use as well as the type-signature
        /// used for the parameters.
        /// </summary>
        new IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent> Reference { get; }
    }

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