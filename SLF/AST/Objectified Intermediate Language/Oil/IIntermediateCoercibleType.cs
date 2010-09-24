using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines generic properties and methods for working with an intermediate type which supports
    /// expression coercion against it.
    /// </summary>
    /// <typeparam name="TType">The kind of coercible type in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The kind of coercible type in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateCoercibleType<TType, TIntermediateType> :
        IIntermediateCoercibleType<ITypeCoercionMember<TType>, IIntermediateTypeCoercionMember<TType, TIntermediateType>, TType, TIntermediateType>,
        IIntermediateCoercibleType<IBinaryOperatorCoercionMember<TType>, IIntermediateBinaryOperatorCoercionMember<TType, TIntermediateType>, TType, TIntermediateType>,
        IIntermediateCoercibleType<IUnaryOperatorCoercionMember<TType>, IIntermediateUnaryOperatorCoercionMember<TType, TIntermediateType>, TType, TIntermediateType>,
        ICoercibleType<TType>,
        IIntermediateCoercibleType
        where TType :
            ICoercibleType<TType>
        where TIntermediateType :
            TType,
            IIntermediateCoercibleType<TType, TIntermediateType>
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateBinaryOperatorCoercionMemberDictionary{TType, TIntermediateType}"/> 
        /// assocaited to the <typeparamref name="TType"/>.
        /// </summary>
        new IIntermediateBinaryOperatorCoercionMemberDictionary<TType, TIntermediateType> BinaryOperatorCoercions { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateTypeCoercionMemberDictionary{TType, TIntermediateType}"/> 
        /// assocaited to the <typeparamref name="TType"/>.
        /// </summary>
        new IIntermediateTypeCoercionMemberDictionary<TType, TIntermediateType> TypeCoercions { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateUnaryOperatorCoercionMemberDictionary{TType, TIntermediateType}"/> 
        /// assocaited to the <typeparamref name="TType"/>.
        /// </summary>
        new IIntermediateUnaryOperatorCoercionMemberDictionary<TType, TIntermediateType> UnaryOperatorCoercions { get; }
    }

    /// <summary>
    /// Defines generic properties and methods for working with an intermediate type which supports
    /// base expression coercion against it.
    /// </summary>
    /// <typeparam name="TCoercion">The type of coercion in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCoercion">The type of coercion in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TType">The kind of coercible type in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The kind of coercible type in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateCoercibleType<TCoercion, TIntermediateCoercion, TType, TIntermediateType> :
        IIntermediateType,
        IIntermediateMemberParent,
        ICoercibleType<TCoercion, TType>,
        IIntermediateCoercibleType
        where TCoercion :
            ICoercionMember<TCoercion, TType>
        where TIntermediateCoercion :
            TCoercion,
            IIntermediateCoercionMember<TCoercion, TIntermediateCoercion, TType, TIntermediateType>
        where TType :
            ICoercibleType<TCoercion, TType>
        where TIntermediateType :
            TType,
            IIntermediateCoercibleType<TCoercion, TIntermediateCoercion, TType, TIntermediateType>
    {
    }

    /// <summary>
    /// Defines properties and methods for working with an intermediate type which supports
    /// expression coercion against it.
    /// </summary>
    public interface IIntermediateCoercibleType :
        ICoercibleType
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateBinaryOperatorCoercionMemberDictionary"/> 
        /// assocaited to the <see cref="IIntermediateCoercibleType"/>.
        /// </summary>
        new IIntermediateBinaryOperatorCoercionMemberDictionary BinaryOperatorCoercions { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateTypeCoercionMemberDictionary"/> 
        /// assocaited to the <see cref="IIntermediateCoercibleType"/>.
        /// </summary>
        new IIntermediateTypeCoercionMemberDictionary TypeCoercions { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateUnaryOperatorCoercionMemberDictionary"/> 
        /// assocaited to the <see cref="IIntermediateCoercibleType"/>.
        /// </summary>
        new IIntermediateUnaryOperatorCoercionMemberDictionary UnaryOperatorCoercions { get; }
    }
}
