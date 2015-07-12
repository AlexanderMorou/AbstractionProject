using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines generic properties and methods for working with
    /// a type which can be coerced in special expression contexts.
    /// </summary>
    /// <typeparam name="TType">The type of <see cref="ICoercibleType{TType}"/>
    /// that needs the coercion members.</typeparam>
    public interface ICoercibleType<TType> :
        ICoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TType>, TType>,
        ICoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TType>, TType>,
        ICoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TType>, TType>,
        ICoercibleType
        where TType :
            ICoercibleType<TType>
    {
        /// <summary>
        /// Returns the 
        /// <see cref="IBinaryOperatorCoercionMemberDictionary{TCoercionParent}"/>
        /// assocaited to the <typeparamref name="TType"/>.
        /// </summary>
        new IBinaryOperatorCoercionMemberDictionary<TType> BinaryOperatorCoercions { get; }
        /// <summary>
        /// Returns the 
        /// <see cref="ITypeCoercionMemberDictionary{TCoercionParent}"/>
        /// assocaited to the <typeparamref name="TType"/>.
        /// </summary>
        new ITypeCoercionMemberDictionary<TType> TypeCoercions { get; }
        /// <summary>
        /// Returns the
        /// <see cref="IUnaryOperatorCoercionMemberDictionary{TCoercionParent}"/>
        /// assocaited to the <typeparamref name="TType"/>.
        /// </summary>
        new IUnaryOperatorCoercionMemberDictionary<TType> UnaryOperatorCoercions { get; }
    }
}
