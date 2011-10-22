using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
    public interface ICoercibleType<TTypeIdentifier, TType> :
        ICoercibleType<ITypeCoercionUniqueIdentifier, TTypeIdentifier, ITypeCoercionMember<TTypeIdentifier, TType>, TType>,
        ICoercibleType<IBinaryOperatorUniqueIdentifier, TTypeIdentifier, IBinaryOperatorCoercionMember<TTypeIdentifier, TType>, TType>,
        ICoercibleType<IUnaryOperatorUniqueIdentifier, TTypeIdentifier, IUnaryOperatorCoercionMember<TTypeIdentifier, TType>, TType>,
        ICoercibleType
        where TTypeIdentifier :
            ITypeUniqueIdentifier<TTypeIdentifier>
        where TType :
            ICoercibleType<TTypeIdentifier, TType>
    {
        /// <summary>
        /// Returns the 
        /// <see cref="IBinaryOperatorCoercionMemberDictionary{TCoercionParentIdentifier, TCoercionParent}"/>
        /// assocaited to the <typeparamref name="TType"/>.
        /// </summary>
        new IBinaryOperatorCoercionMemberDictionary<TTypeIdentifier, TType> BinaryOperatorCoercions { get; }
        /// <summary>
        /// Returns the 
        /// <see cref="ITypeCoercionMemberDictionary{TCoercionParentIdentifier, TCoercionParent}"/>
        /// assocaited to the <typeparamref name="TType"/>.
        /// </summary>
        new ITypeCoercionMemberDictionary<TTypeIdentifier, TType> TypeCoercions { get; }
        /// <summary>
        /// Returns the
        /// <see cref="IUnaryOperatorCoercionMemberDictionary{TCoercionParent}"/>
        /// assocaited to the <typeparamref name="TType"/>.
        /// </summary>
        new IUnaryOperatorCoercionMemberDictionary<TTypeIdentifier, TType> UnaryOperatorCoercions { get; }
    }
}
