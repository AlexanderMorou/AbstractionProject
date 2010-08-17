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
    /// Defines properties and methods for working with a series of intermediate unary operator
    /// coercion members.
    /// </summary>
    /// <typeparam name="TType">The kind of coercible type in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The kind of coercible type in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateUnaryOperatorCoercionMemberDictionary<TType, TIntermediateType> :
        IIntermediateGroupedMemberDictionary<TType, TIntermediateType, IUnaryOperatorCoercionMember<TType>, IIntermediateUnaryOperatorCoercionMember<TType, TIntermediateType>>,
        IUnaryOperatorCoercionMemberDictionary<TType>
        where TType :
            ICoercibleType<IUnaryOperatorCoercionMember<TType>, TType>
        where TIntermediateType :
            TType,
            IIntermediateCoercibleType<IUnaryOperatorCoercionMember<TType>, IIntermediateUnaryOperatorCoercionMember<TType, TIntermediateType>, TType, TIntermediateType>
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateUnaryOperatorCoercionMember{TType, TIntermediateType}"/>
        /// which coerces the <paramref name="op"/>erator provided.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleUnaryOperators"/> constant
        /// relative to the <see cref="IIntermediateUnaryOperatorCoercionMember{TType, TIntermediateType}"/> to 
        /// return.</param>
        /// <returns>A <see cref="IIntermediateUnaryOperatorCoercionMember{TType, TIntermediateType}"/>
        /// instance relative to <paramref name="op"/>.</returns>
        new IIntermediateUnaryOperatorCoercionMember<TType, TIntermediateType> this[CoercibleUnaryOperators op] { get; }
        /// <summary>
        /// Adds a <see cref="IIntermediateUnaryOperatorCoercionMember{TType, TIntermediateType}"/> with the
        /// <paramref name="op"/> provided.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleUnaryOperators"/> which the
        /// new <see cref="IIntermediateUnaryOperatorCoercionMember{TType, TIntermediateType}"/> will coerce.</param>
        /// <returns>A new <see cref="IIntermediateUnaryOperatorCoercionMember{TType, TIntermediateType}"/>, if successful.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="op"/> is invalid.</exception>
        IIntermediateUnaryOperatorCoercionMember<TType, TIntermediateType> Add(CoercibleUnaryOperators op);
    }
    /// <summary>
    /// Defines properties and methods for working with a series of intermediate unary operator
    /// coercion members.
    /// </summary>
    public interface IIntermediateUnaryOperatorCoercionMemberDictionary :
        IUnaryOperatorCoercionMemberDictionary
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateUnaryOperatorCoercionMember"/>
        /// which coerces the <paramref name="op"/>erator provided.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleUnaryOperators"/> constant
        /// relative to the <see cref="IIntermediateUnaryOperatorCoercionMember"/> to 
        /// return.</param>
        /// <returns>A <see cref="IIntermediateUnaryOperatorCoercionMember"/>
        /// instance relative to <paramref name="op"/>.</returns>
        new IIntermediateUnaryOperatorCoercionMember this[CoercibleUnaryOperators op] { get; }
        /// <summary>
        /// Adds a <see cref="IIntermediateUnaryOperatorCoercionMember"/> with the
        /// <paramref name="op"/> provided.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleUnaryOperators"/> which the
        /// new <see cref="IIntermediateUnaryOperatorCoercionMember"/> will coerce.</param>
        /// <returns>A new <see cref="IIntermediateUnaryOperatorCoercionMember"/>, if successful.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="op"/> is invalid.</exception>
        IIntermediateUnaryOperatorCoercionMember Add(CoercibleUnaryOperators op);
    }
}
