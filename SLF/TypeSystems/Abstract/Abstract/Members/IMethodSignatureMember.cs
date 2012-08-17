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


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for
    /// working with a method signature.
    /// </summary>
    /// <typeparam name="TSignature">The type of <see cref="IMethodSignatureMember{TSignature, TSignatureParent}"/> in
    /// the abstract type system.</typeparam>
    /// <typeparam name="TSignatureParent">The type of <see cref="IMethodSignatureParent{TSignature, TSignatureParent}"/>
    /// in the current implementation.</typeparam>
    public interface IMethodSignatureMember<TSignature, TSignatureParent> :
        IMethodSignatureMember<IMethodSignatureParameterMember<TSignature, TSignatureParent>, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignature, TSignatureParent>
        where TSignatureParent :
            IMethodSignatureParent<TSignature, TSignatureParent>
    {
    }
    /// <summary>
    /// Defines generic properties and methods for working with a method
    /// signature.
    /// </summary>
    /// <typeparam name="TSignatureParameter">The type of parameter used
    /// in the <typeparamref name="TSignature"/>.</typeparam>
    /// <typeparam name="TSignature">The type of signature used as a
    /// parent of <typeparamref name="TSignatureParameter"/> instances.</typeparam>
    /// <typeparam name="TSignatureParent">The parent that contains the
    /// <typeparamref name="TSignature"/> instances.</typeparam>
    public interface IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent> :
        ISignatureMember<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>,
        IMethodSignatureMember
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
    {
        /// <summary>
        /// Obtains a variant of the current
        /// <see cref="IMethodSignatureMember{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// with the current generic type-parameters 
        /// replaced with the elements within 
        /// <paramref name="genericReplacements"/>.
        /// </summary>
        /// <param name="genericReplacements">
        /// The <see cref="IType"/> series to replace the
        /// original generic parameters with.</param>
        /// <returns>A <typeparamref name="TSignature"/>
        /// as a variant of the current <see cref="IMethodSignatureMember{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// with the current generic type-parameters
        /// replaced with the elements within
        /// <paramref name="genericReplacements"/>.
        /// </returns>
        new TSignature MakeGenericClosure(ITypeCollectionBase genericReplacements);
        /// <summary>
        /// Returns the original generic form of the current
        /// <see cref="IMethodSignatureMember{TSignatureParameter, TSignature, TSignatureParent}"/> 
        /// generic variant.
        /// </summary>
        /// <returns>A <typeparamref name="TSignature"/> 
        /// which denotes the original generic variant of the current
        /// <see cref="IMethodSignatureMember{TSignatureParameter, TSignature, TSignatureParent}"/>.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the <see cref="IMethodSignatureMember{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// is not a generic closure of the current generic method.
        /// </exception>
        new TSignature GetGenericDefinition();
        /// <summary>
        /// Returns a <typeparamref name="TSignature"/> instance that
        /// is the closed generic form of the current <see cref="IMethodSignatureMember{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// using the <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="IType"/>
        /// collection used to fill in the type-parameters.</param>
        /// <returns>A new closed <typeparamref name="TSignature"/> instance with
        /// the <paramref name="typeParameters"/> provided.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// <seealso cref="IGenericParamParent.IsGenericConstruct"/> of
        /// the current instance is false.</exception>
        new TSignature MakeGenericClosure(params IType[] typeParameters);
    }
    /// <summary>
    /// Defines properties and methods for working
    /// with a method signature member.
    /// </summary>
    public interface IMethodSignatureMember :
        ISignatureMember,
        IMetadataEntity,
        IGenericParamParent<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>,
        IMember
    {
        /// <summary>
        /// Returns the <see cref="IType"/> that the <see cref="IMethodSignatureMember"/>
        /// yields upon return.
        /// </summary>
        IType ReturnType { get; }
        /// <summary>
        /// Returns the <see cref="IMetadataCollection"/> which represents
        /// the <see cref="IMetadatum"/> elements defined upon the return type
        /// of the <see cref="IMethodSignatureMember"/>.
        /// </summary>
        IMetadataCollection ReturnTypeMetadata { get; }
        /// <summary>
        /// Returns the original generic form of the current
        /// <see cref="IMethodSignatureMember"/> generic variant.
        /// </summary>
        /// <returns>A <see cref="IMethodSignatureMember"/>
        /// which denotes the original generic variant
        /// of the current <see cref="IMethodSignatureMember"/>.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the <see cref="IMethodSignatureMember"/>
        /// is not a generic closure of the current generic method.</exception>
        IMethodSignatureMember GetGenericDefinition();
        /// <summary>
        /// Returns the <see cref="ILockedTypeCollection"/> of 
        /// <see cref="IType"/> elements that replace the 
        /// base definition's <see cref="IGenericParamParent.TypeParameters"/>.
        /// </summary>
        new ILockedTypeCollection GenericParameters { get; }
    }
}
