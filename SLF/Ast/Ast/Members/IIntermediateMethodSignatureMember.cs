using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    public interface IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent> :
        IIntermediateSignatureMember<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TParent, TIntermediateParent>,
        IIntermediateMethodSignatureMember,
        IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignatureParameter :
            TSignatureParameter,
            IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature,TParent, TIntermediateParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter,  TParent>
        where TIntermediateParent :
            TParent,
            IIntermediateSignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TParent, TIntermediateParent>
    {
        new IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent> GetReference(IMemberParentReferenceExpression source = null);
        new IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent> GetReference(IMemberParentReferenceExpression source, IEnumerable<IType> typeParameters);
        new IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent> GetReference(IMemberParentReferenceExpression source, params IType[] typeParameters);
    }
    /// <summary>
    /// Defines generic properties and methods for working with an intermediate
    /// method signature.
    /// </summary>
    /// <typeparam name="TSignature">The <see cref="IMethodSignatureMember{TSignature, TSignatureParent}"/>
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignature">The <see cref="IIntermediateMethodSignatureMember{TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TParent">The <see cref="IMethodSignatureParent{TSignature, TParent}"/> in the
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateParent">The <see cref="IIntermediateMethodSignatureParent{TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> :
        IIntermediateMethodSignatureMember<IMethodSignatureParameterMember<TSignature, TParent>, IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>, TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
        IMethodSignatureMember<TSignature, TParent>
        where TSignature :
            IMethodSignatureMember<TSignature, TParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TParent :
            IMethodSignatureParent<TSignature, TParent>
        where TIntermediateParent :
            TParent,
            IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
    {

    }
    /// <summary>
    /// Defines properties and methods for working with an intermediate method 
    /// signature member.
    /// </summary>
    public interface IIntermediateMethodSignatureMember :
        IIntermediateSignatureMember,
        IIntermediateMetadataEntity,
        IIntermediateGenericParameterParent<IMethodSignatureGenericTypeParameterMember, IIntermediateMethodSignatureGenericTypeParameterMember, IMethodSignatureMember, IIntermediateMethodSignatureMember>,
        IIntermediateMember,
        IMethodSignatureMember
    {
        /// <summary>
        /// Returns the <see cref="IType"/> that the <see cref="IMethodSignatureMember"/>
        /// yields upon return.
        /// </summary>
        new IType ReturnType { get; set; }

        IMethodPointerReferenceExpression GetReference(IMemberParentReferenceExpression source = null);
        IMethodPointerReferenceExpression GetReference(IMemberParentReferenceExpression source, IEnumerable<IType> typeParameters);
        IMethodPointerReferenceExpression GetReference(IMemberParentReferenceExpression source, params IType[] typeParameters);
        /// <summary>
        /// Returns the <see cref="IIntermediateModifiersAndAttributesMetadata"/> which defines 
        /// meta-data about the return-type's custom attributes, and the return-type's
        /// required and optional parameters.
        /// </summary>
        new IIntermediateModifiersAndAttributesMetadata ReturnTypeMetadata { get; }

    }
}
