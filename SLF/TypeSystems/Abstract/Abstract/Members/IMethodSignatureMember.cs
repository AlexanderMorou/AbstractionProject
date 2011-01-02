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


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a method signature.
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
    /// Defines generic properties and methods for working with a method signature.
    /// </summary>
    /// <typeparam name="TSignatureParameter">The type of parameter used in the <typeparamref name="TSignature"/>.</typeparam>
    /// <typeparam name="TSignature">The type of signature used as a parent of <typeparamref name="TSignatureParameter"/> instances.</typeparam>
    /// <typeparam name="TSignatureParent">The parent that contains the <typeparamref name="TSignature"/> 
    /// instances.</typeparam>
    public interface IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent> :
        ISignatureMember<TSignature, TSignatureParameter, TSignatureParent>,
        IMethodSignatureMember
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
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
        /// which denotes the original generic variant
        /// of the current <see cref="IMethodSignatureMember{TSignatureParameter, TSignature, TSignatureParent"/>.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the <see cref="IMethodSignatureMember{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// is not a generic method.</exception>
        new TSignature GetGenericDefinition();
    }
    /// <summary>
    /// Defines properties and methods for working
    /// with a method signature member.
    /// </summary>
    public interface IMethodSignatureMember :
        ISignatureMember,
        IGenericParamParent<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>,
        IMember
    {
        /// <summary>
        /// Returns the <see cref="IType"/> that the <see cref="IMethodSignatureMember"/>
        /// yields upon return.
        /// </summary>
        IType ReturnType { get; }
        /// <summary>
        /// Obtains a variant of the current 
        /// <see cref="IMethodSignatureMember"/>
        /// with the current generic type-parameters 
        /// replaced with the elements within 
        /// <paramref name="genericReplacements"/>.
        /// </summary>
        /// <param name="genericReplacements">
        /// The <see cref="IType"/> series to replace the 
        /// original generic parameters with.</param>
        /// <returns>A <see cref="IMethodSignatureMember"/> 
        /// as a variant of the current <see cref="IMethodSignatureMember"/>
        /// with the current generic type-parameters 
        /// replaced with  the elements within 
        /// <paramref name="genericReplacements"/>.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the <see cref="IMethodSignatureMember"/>
        /// is not a generic method.</exception>
        IMethodSignatureMember MakeGenericClosure(ITypeCollection genericReplacements);
        /// <summary>
        /// Returns the original generic form of the current
        /// <see cref="IMethodSignatureMember"/> generic variant.
        /// </summary>
        /// <returns>A <see cref="IMethodSignatureMember"/> 
        /// which denotes the original generic variant
        /// of the current <see cref="IMethodSignatureMember"/>.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the <see cref="IMethodSignatureMember"/>
        /// is not a generic variant.</exception>
        IMethodSignatureMember GetGenericDefinition();
        /// <summary>
        /// Returns the <see cref="ILockedTypeCollection"/> of 
        /// <see cref="IType"/> elements that replace the 
        /// base definition's <see cref="IGenericParamParent.TypeParameters"/>.
        /// </summary>
        ILockedTypeCollection GenericParameters { get; }
    }
}
