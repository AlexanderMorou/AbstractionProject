using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <typeparam name="TIntermediateType"></typeparam>
    public interface IIntermediateBinaryOperatorCoercionMemberDictionary<TType, TIntermediateType> :
        IIntermediateGroupedMemberDictionary<TType, TIntermediateType, IBinaryOperatorCoercionMember<TType>, IIntermediateBinaryOperatorCoercionMember<TType, TIntermediateType>>,
        IBinaryOperatorCoercionMemberDictionary<TType>
        where TType :
            ICoercibleType<IBinaryOperatorCoercionMember<TType>, TType>
        where TIntermediateType :
            TType,
            IIntermediateCoercibleType<IBinaryOperatorCoercionMember<TType>, IIntermediateBinaryOperatorCoercionMember<TType, TIntermediateType>, TType, TIntermediateType>
    {
        /// <summary>
        /// Adds a <see cref="IIntermediateBinaryOperatorCoercionMember{TType, TIntermediateType}"/>, to the
        /// <see cref="IIntermediateBinaryOperatorCoercionMemberDictionary{TType, TIntermediateType}"/>,
        /// with the <paramref name="op"/>, <paramref name="containingSide"/>,
        /// <paramref name="otherSide"/>, <paramref name="returnType"/> provided.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleBinaryOperators"/> that is coerced by the <see cref="IIntermediateBinaryOperatorCoercionMember{TType, TIntermediateType}"/>.</param>
        /// <param name="containingSide">Which side the containing type of the <see cref="IIntermediateBinaryOperatorCoercionMember{TType, TIntermediateType}"/>
        /// resides on in the overload.</param>
        /// <param name="otherSide">The <see cref="IType"/> which exists on the other side of the <paramref name="containingSide"/>.</param>
        /// <param name="returnType">The type that results from the binary operation coercion.</param>
        /// <returns>An instance of <see cref="IIntermediateBinaryOperatorCoercionMember{TType, TIntermediateType}"/>, if successful.</returns>
        /// <remarks>If <paramref name="containingSide"/> is <see cref="BinaryOpCoercionContainingSide.Both"/>,
        /// <paramref name="otherSide"/> is ignored.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="otherSide"/> is null and <paramref name="containingSide"/> is NOT <see cref="BinaryOpCoercionContainingSide.Both"/>; -or- <paramref name="returnType"/>
        /// is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when either <paramref name="op"/> or <paramref name="containingSide"/> is invalid.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="otherSide"/>
        /// is an interface, a static class, or an unknown kind of type; or <paramref name="returnType"/> is a static class, or an unknown kind of type.</exception>
        IIntermediateBinaryOperatorCoercionMember<TType, TIntermediateType> Add(CoercibleBinaryOperators op, BinaryOpCoercionContainingSide containingSide, IType otherSide, IType returnType);
        /// <summary>
        /// Adds a <see cref="IIntermediateBinaryOperatorCoercionMember{TType, TIntermediateType}"/>, to the
        /// <see cref="IIntermediateBinaryOperatorCoercionMemberDictionary{TType, TIntermediateType}"/>,
        /// with the <paramref name="op"/> and <paramref name="returnType"/> provided.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleBinaryOperators"/> that is coerced by the <see cref="IIntermediateBinaryOperatorCoercionMember{TType, TIntermediateType}"/>.</param>
        /// <param name="returnType">The type that results from the binary operation coercion.</param>
        /// <returns>An instance of <see cref="IIntermediateBinaryOperatorCoercionMember{TType, TIntermediateType}"/>, if successful.</returns>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="returnType"/> is a static class, or an unknown kind of type.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="returnType"/> is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="op"/> is invalid.</exception>
        IIntermediateBinaryOperatorCoercionMember<TType, TIntermediateType> Add(CoercibleBinaryOperators op, IType returnType);

        /// <summary>
        /// Returns the <see cref="IIntermediateBinaryOperatorCoercionMember{TType, TIntermediateType}"/> which
        /// coerces the <paramref name="op"/> provided with the
        /// parent contained on the <see cref="side"/> provided with
        /// <paramref name="otherSide"/> provided.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleBinaryOperators"/>
        /// coerced by the <see cref="IIntermediateBinaryOperatorCoercionMember{TType, TIntermediateType}"/> 
        /// to find.</param>
        /// <param name="side">The side at which the containing type
        /// is in the coercion.</param>
        /// <param name="otherSide">The type of the other side in the coercion.</param>
        /// <returns>A <see cref="IIntermediateBinaryOperatorCoercionMember{TType, TIntermediateType}"/> instance
        /// that coerces the <paramref name="op"/> provided.</returns>
        new IIntermediateBinaryOperatorCoercionMember<TType, TIntermediateType> this[CoercibleBinaryOperators op, BinaryOpCoercionContainingSide side, IType otherSide] { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateBinaryOperatorCoercionMember{TType, TIntermediateType}"/> which
        /// coerces the <paramref name="op"/> provided where
        /// the containing type is used as both operands.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleBinaryOperators"/>
        /// coerced by the <see cref="IIntermediateBinaryOperatorCoercionMember{TType, TIntermediateType}"/> 
        /// to find.</param>
        /// <returns>A <see cref="IIntermediateBinaryOperatorCoercionMember{TType, TIntermediateType}"/> instance
        /// that coerces the <paramref name="op"/> provided.</returns>
        new IIntermediateBinaryOperatorCoercionMember<TType, TIntermediateType> this[CoercibleBinaryOperators op] { get; }

    }
    /// <summary>
    /// Defines properties and methods for working with a series of intermediate
    /// binary operation coercion members.
    /// </summary>
    public interface IIntermediateBinaryOperatorCoercionMemberDictionary :
        IBinaryOperatorCoercionMemberDictionary
    {
        /// <summary>
        /// Adds a <see cref="IIntermediateBinaryOperatorCoercionMember"/>, to the
        /// <see cref="IIntermediateBinaryOperatorCoercionMemberDictionary"/>,
        /// with the <paramref name="op"/>, <paramref name="containingSide"/>,
        /// <paramref name="otherSide"/>, <paramref name="returnType"/> provided.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleBinaryOperators"/> that is coerced by the <see cref="IIntermediateBinaryOperatorCoercionMember"/>.</param>
        /// <param name="containingSide">Which side the containing type of the <see cref="IIntermediateBinaryOperatorCoercionMember"/>
        /// resides on in the overload.</param>
        /// <param name="otherSide">The <see cref="IType"/> which exists on the other side of the <paramref name="containingSide"/>.</param>
        /// <param name="returnType">The type that results from the binary operation coercion.</param>
        /// <returns>An instance of <see cref="IIntermediateBinaryOperatorCoercionMember"/>, if successful.</returns>
        /// <remarks>If <paramref name="containingSide"/> is <see cref="BinaryOpCoercionContainingSide.Both"/>,
        /// <paramref name="otherSide"/> is ignored.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="otherSide"/> is null and <paramref name="containingSide"/> is NOT <see cref="BinaryOpCoercionContainingSide.Both"/>; -or- <paramref name="returnType"/>
        /// is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when either <paramref name="op"/> or <paramref name="containingSide"/> is invalid.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="otherSide"/>
        /// is an interface, a static class, or an unknown kind of type; or <paramref name="returnType"/> is a static class, or an unknown kind of type.</exception>
        IIntermediateBinaryOperatorCoercionMember Add(CoercibleBinaryOperators op, BinaryOpCoercionContainingSide containingSide, IType otherSide, IType returnType);
        /// <summary>
        /// Adds a <see cref="IIntermediateBinaryOperatorCoercionMember"/>, to the
        /// <see cref="IIntermediateBinaryOperatorCoercionMemberDictionary"/>,
        /// with the <paramref name="op"/> and <paramref name="returnType"/> provided.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleBinaryOperators"/> that is coerced by the <see cref="IIntermediateBinaryOperatorCoercionMember"/>.</param>
        /// <param name="returnType">The type that results from the binary operation coercion.</param>
        /// <returns>An instance of <see cref="IIntermediateBinaryOperatorCoercionMember"/>, if successful.</returns>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="returnType"/> is a static class, or an unknown kind of type.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="returnType"/> is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="op"/> is invalid.</exception>
        IIntermediateBinaryOperatorCoercionMember Add(CoercibleBinaryOperators op, IType returnType);
    }
}
