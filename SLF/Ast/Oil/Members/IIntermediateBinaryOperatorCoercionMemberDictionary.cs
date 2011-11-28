using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines properties and methods for working with a dictionary that
    /// contains a series of intermediate binary operator coercion members
    /// used to coerce binary operations within mathematical expressions
    /// based off of the types of the operands.
    /// </summary>
    /// <typeparam name="TCoercionParent">The type of coercion parent which
    /// contains the binary operator coercion members.</typeparam>
    /// <typeparam name="TIntermediateCoercionParent">The type of the 
    /// intermediate coercion parent which contains the intermediate 
    /// (malleable) binary operator coercion members.</typeparam>
    public interface IIntermediateBinaryOperatorCoercionMemberDictionary<TCoercionParent, TIntermediateCoercionParent> :
        IIntermediateGroupedMemberDictionary<TCoercionParent, TIntermediateCoercionParent, IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, IIntermediateBinaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent>>,
        IBinaryOperatorCoercionMemberDictionary<TCoercionParent>
        where TCoercionParent :
            ICoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
        where TIntermediateCoercionParent :
            IIntermediateCoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, IIntermediateBinaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>,
            TCoercionParent
    {
        /// <summary>
        /// Adds a <see cref="IIntermediateBinaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/>, to the
        /// <see cref="IIntermediateBinaryOperatorCoercionMemberDictionary{TCoercionParent, TIntermediateCoercionParent}"/>,
        /// with the <paramref name="op"/>, <paramref name="containingSide"/>,
        /// <paramref name="otherSide"/>, <paramref name="returnType"/> provided.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleBinaryOperators"/> that is coerced by the <see cref="IIntermediateBinaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/>.</param>
        /// <param name="containingSide">Which side the containing type of the <see cref="IIntermediateBinaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/>
        /// resides on in the overload.</param>
        /// <param name="otherSide">The <see cref="IType"/> which exists on the other side of the <paramref name="containingSide"/>.</param>
        /// <param name="returnType">The type that results from the binary operation coercion.</param>
        /// <returns>An instance of <see cref="IIntermediateBinaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/>, if successful.</returns>
        /// <remarks>If <paramref name="containingSide"/> is <see cref="BinaryOpCoercionContainingSide.Both"/>,
        /// <paramref name="otherSide"/> is ignored.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="otherSide"/> is null and <paramref name="containingSide"/> is NOT <see cref="BinaryOpCoercionContainingSide.Both"/>; -or- <paramref name="returnType"/>
        /// is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when either <paramref name="op"/> or <paramref name="containingSide"/> is invalid.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="otherSide"/>
        /// is an interface, a static class, or an unknown kind of type; or <paramref name="returnType"/> is a static class, or an unknown kind of type.</exception>
        IIntermediateBinaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent> Add(CoercibleBinaryOperators op, BinaryOpCoercionContainingSide containingSide, IType otherSide, IType returnType);
        /// <summary>
        /// Adds a <see cref="IIntermediateBinaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/>, to the
        /// <see cref="IIntermediateBinaryOperatorCoercionMemberDictionary{TCoercionParent, TIntermediateCoercionParent}"/>,
        /// with the <paramref name="op"/> and <paramref name="returnType"/> provided.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleBinaryOperators"/> that is coerced by the <see cref="IIntermediateBinaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/>.</param>
        /// <param name="returnType">The type that results from the binary operation coercion.</param>
        /// <returns>An instance of <see cref="IIntermediateBinaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/>, if successful.</returns>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="returnType"/> is a static class, or an unknown kind of type.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="returnType"/> is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="op"/> is invalid.</exception>
        IIntermediateBinaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent> Add(CoercibleBinaryOperators op, IType returnType);

        /// <summary>
        /// Returns the <see cref="IIntermediateBinaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/> which
        /// coerces the <paramref name="op"/> provided with the
        /// parent contained on the <paramref name="side"/> provided with
        /// <paramref name="otherSide"/> provided.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleBinaryOperators"/>
        /// coerced by the <see cref="IIntermediateBinaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/> 
        /// to find.</param>
        /// <param name="side">The side at which the containing type
        /// is in the coercion.</param>
        /// <param name="otherSide">The type of the other side in the coercion.</param>
        /// <returns>A <see cref="IIntermediateBinaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/> instance
        /// that coerces the <paramref name="op"/> provided.</returns>
        new IIntermediateBinaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent> this[CoercibleBinaryOperators op, BinaryOpCoercionContainingSide side, IType otherSide] { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateBinaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/> which
        /// coerces the <paramref name="op"/> provided where
        /// the containing type is used as both operands.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleBinaryOperators"/>
        /// coerced by the <see cref="IIntermediateBinaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/> 
        /// to find.</param>
        /// <returns>A <see cref="IIntermediateBinaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/> instance
        /// that coerces the <paramref name="op"/> provided.</returns>
        new IIntermediateBinaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent> this[CoercibleBinaryOperators op] { get; }

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
