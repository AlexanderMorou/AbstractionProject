using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines generic properties and methods for working with a bound
    /// pointer to a method.
    /// </summary>
    /// <typeparam name="TSignatureParameter">The type of parameter used in the <typeparamref name="TSignature"/>.</typeparam>
    /// <typeparam name="TSignature">The type of signature used as a parent of <typeparamref name="TSignatureParameter"/> instances.</typeparam>
    /// <typeparam name="TParent">The parent that contains the <typeparamref name="TSignature"/> 
    /// instances.</typeparam>
    public interface IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent> :
        IMethodPointerReferenceExpression
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
        where TParent :
            ISignatureParent<TSignature, TSignatureParameter, TParent>
    {
        /// <summary>
        /// Returns the <see cref="IMethodReferenceStub{TSignatureParameter, TSignature, TParent}"/>
        /// associated to the <see cref="IMethodPointerReferenceExpression{TSignatureParameter, TSignature, TParent}"/>.
        /// </summary>
        /// <remarks>Used to provide initial context data 
        /// for the lookup.</remarks>
        new IMethodReferenceStub<TSignatureParameter, TSignature, TParent> Reference { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a 
    /// pointer to a method.
    /// </summary>
    public interface IMethodPointerReferenceExpression :
        IExpression,
        IFusionCommaTargetExpression
        /* ILinkableExpression*/
    {
        /*
        /// <summary>
        /// Returns the member associated to the 
        /// <see cref="IMemberReferenceExpression"/>.
        /// </summary>
        IMethodSignatureMember AssociatedMember { get; }
         */
        /// <summary>
        /// Returns the <see cref="IMethodReferenceStub"/>
        /// associated to the <see cref="IMethodPointerReferenceExpression"/>.
        /// </summary>
        /// <remarks>Used to provide initial context data 
        /// for the lookup.</remarks>
        IMethodReferenceStub Reference { get; }
        /// <summary>
        /// Returns the <see cref="ITypeCollection"/> of
        /// <see cref="IType"/> instances which relate to 
        /// the types of parameters used to
        /// bind to the method.
        /// </summary>
        ITypeCollection Signature { get; }
        /// <summary>
        /// Obtains a <see cref="IMethodInvokeExpression"/>
        /// by evaluating the <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="parameters">A series of 
        /// <see cref="IExpression"/> elements 
        /// which relate to the data of the 
        /// parameters of the invoke, and the 
        /// types of the parameters.</param>
        /// <returns>A new <see cref="IMethodInvokeExpression"/> 
        /// relative to the signature and data of 
        /// the <paramref name="parameters"/> 
        /// provided.</returns>
        IMethodInvokeExpression Invoke(IExpressionCollection<IExpression> parameters);
        /// <summary>
        /// Obtains a <see cref="IMethodInvokeExpression"/>
        /// by evaluating the <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="parameters">A series of
        /// <see cref="IExpression"/> elements 
        /// which relate to the data of the 
        /// parameters of the invoke, and the types 
        /// of the parameters.</param>
        /// <returns>A new <see cref="IMethodInvokeExpression"/> 
        /// relative to the signature and data of 
        /// the <paramref name="parameters"/> 
        /// provided.</returns>
        IMethodInvokeExpression Invoke(params IExpression[] parameters);
    }
}
