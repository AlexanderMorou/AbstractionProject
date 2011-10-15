using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
    /// <typeparam name="TCoercionParent">The kind of coercible type in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCoercionParent">The kind of coercible type in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateCoercibleType<TCoercionParentIdentifier, TCoercionParent, TIntermediateCoercionParent> :
        IIntermediateCoercibleType<ITypeCoercionUniqueIdentifier, TCoercionParentIdentifier, ITypeCoercionMember<TCoercionParentIdentifier, TCoercionParent>, IIntermediateTypeCoercionMember<TCoercionParentIdentifier, TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>,
        IIntermediateCoercibleType<IBinaryOperatorUniqueIdentifier, TCoercionParentIdentifier, IBinaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent>, IIntermediateBinaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>,
        IIntermediateCoercibleType<IUnaryOperatorUniqueIdentifier, TCoercionParentIdentifier, IUnaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent>, IIntermediateUnaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>,
        ICoercibleType<TCoercionParentIdentifier, TCoercionParent>,
        IIntermediateCoercibleType
        where TCoercionParentIdentifier :
            ITypeUniqueIdentifier<TCoercionParentIdentifier>
        where TCoercionParent :
            ICoercibleType<TCoercionParentIdentifier, TCoercionParent>
        where TIntermediateCoercionParent :
            IIntermediateCoercibleType<TCoercionParentIdentifier, TCoercionParent, TIntermediateCoercionParent>,
            TCoercionParent
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateBinaryOperatorCoercionMemberDictionary{TCoercionParentIdentifier, TCoercionParent, TIntermediateCoercionParent}"/> 
        /// assocaited to the <typeparamref name="TCoercionParent"/>.
        /// </summary>
        new IIntermediateBinaryOperatorCoercionMemberDictionary<TCoercionParentIdentifier, TCoercionParent, TIntermediateCoercionParent> BinaryOperatorCoercions { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateTypeCoercionMemberDictionary{TCoercionParentIdentifier, TCoercionParent, TIntermediateCoercionParent}"/> 
        /// assocaited to the <typeparamref name="TCoercionParent"/>.
        /// </summary>
        new IIntermediateTypeCoercionMemberDictionary<TCoercionParentIdentifier, TCoercionParent, TIntermediateCoercionParent> TypeCoercions { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateUnaryOperatorCoercionMemberDictionary{TCoercionParentIdentifier, TCoercionParent, TIntermediateCoercionParent}"/> 
        /// assocaited to the <typeparamref name="TCoercionParent"/>.
        /// </summary>
        new IIntermediateUnaryOperatorCoercionMemberDictionary<TCoercionParentIdentifier, TCoercionParent, TIntermediateCoercionParent> UnaryOperatorCoercions { get; }
    }

    /// <summary>
    /// Defines generic properties and methods for working with an intermediate type which supports
    /// base expression coercion against it.
    /// </summary>
    /// <typeparam name="TCoercion">The type of coercion in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCoercion">The type of coercion in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TCoercionParent">The kind of coercible type in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCoercionParent">The kind of coercible type in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateCoercibleType<TCoercionIdentifier, TCoercionParentIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent> :
        IIntermediateType,
        IIntermediateMemberParent,
        ICoercibleType<TCoercion, TCoercionParent>,
        IIntermediateCoercibleType
        where TCoercion :
            ICoercionMember<TCoercionIdentifier, TCoercionParentIdentifier, TCoercion, TCoercionParent>
        where TIntermediateCoercion :
            IIntermediateCoercionMember<TCoercionIdentifier, TCoercionParentIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent>,
            TCoercion
        where TCoercionParent :
            ICoercibleType<TCoercion, TCoercionParent>
        where TIntermediateCoercionParent :
            IIntermediateCoercibleType<TCoercionIdentifier, TCoercionParentIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent>,
            TCoercionParent
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
