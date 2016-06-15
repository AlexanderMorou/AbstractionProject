using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
#if DEBUG
    [VisitorTargetAttribute("ExpressionVisitor", ContextualVisitor  = true)]
    [VisitorTargetAttribute("ExpressionVisitor", YieldingVisitor    = true)]
    [VisitorTargetAttribute("ExpressionVisitor", ContextualVisitor  = true,
                                                 YieldingVisitor    = true)]
    [VisitorTargetAttribute("ExpressionVisitor")]
    [VisitorTargetAttribute("IntermediateCodeVisitor", DerivedThroughInheriting = "ExpressionVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateCodeVisitor")]
#endif
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
#if DEBUG
    [VisitorTargetAttribute("ExpressionVisitor", ContextualVisitor  = true)]
    [VisitorTargetAttribute("ExpressionVisitor", YieldingVisitor    = true)]
    [VisitorTargetAttribute("ExpressionVisitor", ContextualVisitor  = true,
                                                 YieldingVisitor    = true)]
    [VisitorTargetAttribute("ExpressionVisitor")]
    [VisitorTargetAttribute("IntermediateCodeVisitor", DerivedThroughInheriting = "ExpressionVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateCodeVisitor")]
#endif
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
        [VisitorPropertyRequirement("GenericParameters != null")] 
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
        /// <summary>
        /// Visits the elements of the <see cref="IMethodReferenceStub{TSignatureParameter, TSignature, TParent}"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IExpressionVisitor"/>
        /// to which the <see cref="IMethodReferenceStub{TSignatureParameter, TSignature, TParent}"/> needs to repay the visit
        /// to.</param>
        void Accept(IExpressionVisitor visitor);
        /// <summary>
        /// Visits the elements of the <see cref="IMethodReferenceStub{TSignatureParameter, TSignature, TParent}"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="ICommonExpressionVisitor{TResult, TContext}"/>
        /// to which the <see cref="IMethodReferenceStub{TSignatureParameter, TSignature, TParent}"/> needs to repay the visit
        /// to.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <typeparam name="TResult">The type returned by the visit.</typeparam>
        /// <typeparam name="TContext">The type of context passed to the visitor.</typeparam>
        /// <returns>The <typeparamref name="TResult"/> of the 
        /// current implementation.</returns>
        TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context);
    }
}
