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

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines properties and methods for working with a series of intermediate unary operator
    /// coercion members.
    /// </summary>
    /// <typeparam name="TCoercionParentIdentifier">The kind of identifier used
    /// to differentiate the <typeparamref name="TIntermediateCoercionParent"/>
    /// instance from its siblings.</typeparam>
    /// <typeparam name="TCoercionParent">The kind of coercible type in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCoercionParent">The kind of coercible type in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateUnaryOperatorCoercionMemberDictionary<TCoercionParent, TIntermediateCoercionParent> :
        IIntermediateGroupedMemberDictionary<TCoercionParent, TIntermediateCoercionParent, IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, IIntermediateUnaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent>>,
        IUnaryOperatorCoercionMemberDictionary<TCoercionParent>
        where TCoercionParent :
            ICoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
        where TIntermediateCoercionParent :
            TCoercionParent,
            IIntermediateCoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, IIntermediateUnaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateUnaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/>
        /// which coerces the <paramref name="op"/>erator provided.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleUnaryOperators"/> constant
        /// relative to the <see cref="IIntermediateUnaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/> to 
        /// return.</param>
        /// <returns>A <see cref="IIntermediateUnaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/>
        /// instance relative to <paramref name="op"/>.</returns>
        new IIntermediateUnaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent> this[CoercibleUnaryOperators op] { get; }
        /// <summary>
        /// Adds a <see cref="IIntermediateUnaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/> with the
        /// <paramref name="op"/> provided.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleUnaryOperators"/> which the
        /// new <see cref="IIntermediateUnaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/> will coerce.</param>
        /// <returns>A new <see cref="IIntermediateUnaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/>, if successful.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="op"/> is invalid.</exception>
        IIntermediateUnaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent> Add(CoercibleUnaryOperators op);
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
