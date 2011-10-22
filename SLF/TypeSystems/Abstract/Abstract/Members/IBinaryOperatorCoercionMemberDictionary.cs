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
    /// Defines properties and methods for working 
    /// with a series of <see cref="IBinaryOperatorCoercionMember{TCoercionParent}"/>
    /// instances.
    /// </summary>
    /// <typeparam name="TCoercionParentIdentifier">The type of the identifier that represents
    /// the parent's uniqueness from the other types.</typeparam>
    /// <typeparam name="TCoercionParent">
    /// The type of <see cref="ICoercibleType{TType}"/>
    /// which contains the 
    /// <see cref="IBinaryOperatorCoercionMemberDictionary{TCoercionParentIdentifier, TCoercionParent}"/>.
    /// </typeparam>
    public interface IBinaryOperatorCoercionMemberDictionary<TCoercionParentIdentifier, TCoercionParent> :
        IGroupedMemberDictionary<TCoercionParent, IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent>>
        where TCoercionParentIdentifier :
            ITypeUniqueIdentifier<TCoercionParentIdentifier>
        where TCoercionParent :
            ICoercibleType<IBinaryOperatorUniqueIdentifier, TCoercionParentIdentifier, IBinaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent>, TCoercionParent>
    {
        /// <summary>
        /// Returns the <see cref="IBinaryOperatorCoercionMember{TCoercionParent}"/> which
        /// coerces the <paramref name="op"/> provided with the
        /// parent contained on the <paramref name="side"/> provided with
        /// <paramref name="otherSide"/> provided.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleBinaryOperators"/>
        /// coerced by the <see cref="IBinaryOperatorCoercionMember{TCoercionParent}"/> 
        /// to find.</param>
        /// <param name="side">The side at which the containing type
        /// is in the coercion.</param>
        /// <param name="otherSide">The type of the other side in the coercion.</param>
        /// <returns>A <see cref="IBinaryOperatorCoercionMember{TCoercionParent}"/> instance
        /// that coerces the <paramref name="op"/> provided.</returns>
        IBinaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent> this[CoercibleBinaryOperators op, BinaryOpCoercionContainingSide side, IType otherSide] { get; }
        /// <summary>
        /// Returns the <see cref="IBinaryOperatorCoercionMember{TCoercionParent}"/> which
        /// coerces the <paramref name="op"/> provided where
        /// the containing type is used as both operands.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleBinaryOperators"/>
        /// coerced by the <see cref="IBinaryOperatorCoercionMember{TCoercionParent}"/> 
        /// to find.</param>
        /// <returns>A <see cref="IBinaryOperatorCoercionMember{TCoercionParent}"/> instance
        /// that coerces the <paramref name="op"/> provided.</returns>
        IBinaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent> this[CoercibleBinaryOperators op] { get; }
    }

    /// <summary>
    /// Defines properties and methods for working 
    /// with a series of binary operator coercion
    /// members.
    /// </summary>
    public interface IBinaryOperatorCoercionMemberDictionary :
        IGroupedMemberDictionary
    {
        /// <summary>
        /// Returns the <see cref="IBinaryOperatorCoercionMember"/> which
        /// coerces the <paramref name="op"/> provided with the
        /// parent contained on the <paramref name="side"/> provided with
        /// <paramref name="otherSide"/> provided.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleBinaryOperators"/>
        /// coerced by the <see cref="IBinaryOperatorCoercionMember"/> 
        /// to find.</param>
        /// <param name="side">The side at which the containing type
        /// is in the coercion.</param>
        /// <param name="otherSide">The type of the other side in the coercion.</param>
        /// <returns>A <see cref="IBinaryOperatorCoercionMember"/> instance
        /// that coerces the <paramref name="op"/> provided.</returns>
        IBinaryOperatorCoercionMember this[CoercibleBinaryOperators op, BinaryOpCoercionContainingSide side, IType otherSide] { get; }
        /// <summary>
        /// Returns the <see cref="IBinaryOperatorCoercionMember"/> which
        /// coerces the <paramref name="op"/> provided where
        /// the containing type is used as both operands.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleBinaryOperators"/>
        /// coerced by the <see cref="IBinaryOperatorCoercionMember"/> 
        /// to find.</param>
        /// <returns>A <see cref="IBinaryOperatorCoercionMember"/> instance
        /// that coerces the <paramref name="op"/> provided.</returns>
        IBinaryOperatorCoercionMember this[CoercibleBinaryOperators op] { get; }
    }
}
