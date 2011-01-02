 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a parent that contains method signatures.
    /// </summary>
    /// <typeparam name="TSignature">The type of signature in the current implementation.</typeparam>
    /// <typeparam name="TSignatureParent">The type of parent that contains teh <typeparamref name="TSignature"/>
    /// instances.</typeparam>
    public interface IMethodSignatureParent<TSignature, TSignatureParent> :
        ISignatureParent<TSignature, IMethodSignatureParameterMember<TSignature, TSignatureParent>, TSignatureParent>,
        IMethodSignatureParent
        where TSignature :
            IMethodSignatureMember<TSignature, TSignatureParent>
        where TSignatureParent :
            IMethodSignatureParent<TSignature, TSignatureParent>
    {
        /// <summary>
        /// Returns the <see cref="IMethodSignatureMemberDictionary{TSignature, TSignatureParent}"/> that 
        /// contain the <see cref="IMethodSignatureMember{TSignature, TSignatureParent}"/> instances
        /// contained within the <typeparamref name="TSignatureParent"/>.
        /// </summary>
        new IMethodSignatureMemberDictionary<TSignature, TSignatureParent> Methods { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with an <see cref="IMethodSignatureMember"/> parent.
    /// </summary>
    public interface IMethodSignatureParent :
        ISignatureParent
    {
        /// <summary>
        /// Returns the <see cref="IMethodSignatureMemberDictionary"/> that contain the <see cref="IMethodSignatureMember"/> instances
        /// contained within the <see cref="IMethodSignatureParent"/>.
        /// </summary>
        IMethodSignatureMemberDictionary Methods { get; }
    }
}
