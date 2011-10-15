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
    public interface IMethodReferenceStub<TSignatureParameter, TSignature, TParent> :
        IMethodReferenceStub,
        IBoundMemberReference
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
        where TParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TParent>
    {
        /// <summary>
        /// Returns the <typeparamref name="TSignature"/>
        /// associated to the current <see cref="IMethodReferenceStub{TSignatureParameter, TSignature, TParent}"/>
        /// </summary>
        new TSignature Member { get; }
        /// <summary>
        /// Obtains a <see cref="IMethodPointerReferenceExpression{TSignatureParameter, TSignature, TParent}"/>
        /// with the <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="signature">The <see cref="ITypeCollection"/>
        /// relative to the type-signature of the <see cref="IMethodPointerReferenceExpression{TSignatureParameter, TSignature, TParent}"/>
        /// to obtain.</param>
        /// <returns>A new <see cref="IMethodPointerReferenceExpression{TSignatureParameter, TSignature, TParent}"/>
        /// relative to the <paramref name="signature"/>
        /// provided.</returns>
        new IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent> GetPointer(ITypeCollection signature);
        /// <summary>
        /// Obtains a <see cref="IMethodPointerReferenceExpression{TSignatureParameter, TSignature, TParent}"/>
        /// with the <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="signature">The series if <see cref="IType"/>
        /// elements relative to the type-signature of the 
        /// <see cref="IMethodPointerReferenceExpression{TSignatureParameter, TSignature, TParent}"/>
        /// to obtain.</param>
        /// <returns>A new <see cref="IMethodPointerReferenceExpression{TSignatureParameter, TSignature, TParent}"/>
        /// relative to the <paramref name="signature"/>
        /// provided.</returns>
        new IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent> GetPointer(params IType[] signature);

        /// <summary>
        /// Obtains a <see cref="IMethodPointerReferenceExpression{TSignatureParameter, TSignature, TParent}"/>
        /// using the default signature associated to the <see cref="Member"/>.
        /// </summary>
        /// <returns>A <see cref="IMethodPointerReferenceExpression{TSignatureParameter, TSignature, TParent}"/>
        /// instance relative to the <see cref="Member"/>.</returns>
        new IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent> GetPointer();

    }

    public interface IUnboundMethodReferenceStub :
        IMethodReferenceStub
    {
        /// <summary>
        /// Returns/sets the type of reference the 
        /// <see cref="IUnboundMethodReferenceStub"/> is.
        /// </summary>
        new MethodReferenceType ReferenceType { get; set; }
        /// <summary>
        /// Returns/sets the name of the method associated
        /// to the <see cref="IUnboundMethodReferenceStub"/>.
        /// </summary>
        new string Name { get; set; }
    }

    /// <summary>
    /// Defines properties and methods for working 
    /// with a refernece to a method.
    /// </summary>
    /// <remarks>Simpler form of 
    /// <see cref="IMethodPointerReferenceExpression"/>,
    /// used to obtain initial context data
    /// used to make a lookup.</remarks>
    public interface IMethodReferenceStub
    {
        /// <summary>
        /// Returns/sets the type of reference the 
        /// <see cref="IMethodReferenceStub"/> is.
        /// </summary>
        MethodReferenceType ReferenceType { get; }
        /// <summary>
        /// Returns the <see cref="ITypeCollection"/> of 
        /// <see cref="IType"/> instances used to replace
        /// the generic parameters of the method.
        /// </summary>
        ILockedTypeCollection GenericParameters { get; }
        /// <summary>
        /// Returns the name of the method associated
        /// to the <see cref="IMethodReferenceStub"/>.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/>
        /// that sourced the <see cref="IMethodReferenceStub"/>.
        /// </summary>
        IMemberParentReferenceExpression Source { get; }
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
        /// <summary>
        /// Obtains a <see cref="IMethodPointerReferenceExpression"/>
        /// with the <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="signature">The <see cref="ITypeCollection"/>
        /// relative to the type-signature of the <see cref="IMethodPointerReferenceExpression"/>
        /// to obtain.</param>
        /// <returns>A new <see cref="IMethodPointerReferenceExpression"/>
        /// relative to the <paramref name="signature"/>
        /// provided.</returns>
        IMethodPointerReferenceExpression GetPointer(ITypeCollection signature);
        /// <summary>
        /// Obtains a <see cref="IMethodPointerReferenceExpression"/>
        /// with the <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="signature">The series if <see cref="IType"/>
        /// elements relative to the type-signature of the 
        /// <see cref="IMethodPointerReferenceExpression"/>
        /// to obtain.</param>
        /// <returns>A new <see cref="IMethodPointerReferenceExpression"/>
        /// relative to the <paramref name="signature"/>
        /// provided.</returns>
        IMethodPointerReferenceExpression GetPointer(params IType[] signature);
        /// <summary>
        /// Obtains a <see cref="IMethodPointerReferenceExpression"/>
        /// where the signature needs to be discovered during compile.
        /// </summary>
        /// <returns>A <see cref="IMethodPointerReferenceExpression"/>
        /// with no signature.</returns>
        IMethodPointerReferenceExpression GetPointer();
    }
}
