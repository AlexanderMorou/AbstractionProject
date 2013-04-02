using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines generic properties and methods for working with an intermediate type which supports
    /// expression coercion against it.
    /// </summary>
    /// <typeparam name="TCoercionParentIdentifier">The kind of identifier used to 
    /// differentiate the <typeparamref name="TIntermediateCoercionParent"/>
    /// instance from its siblings.</typeparam>
    /// <typeparam name="TCoercionParent">The kind of coercible type in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCoercionParent">The kind of coercible type in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateCoercibleType<TCoercionParent, TIntermediateCoercionParent> :
        IIntermediateCoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, IIntermediateTypeCoercionMember<TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>,
        IIntermediateCoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, IIntermediateBinaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>,
        IIntermediateCoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, IIntermediateUnaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>,
        ICoercibleType<TCoercionParent>,
        IIntermediateCoercibleType
        where TCoercionParent :
            ICoercibleType<TCoercionParent>
        where TIntermediateCoercionParent :
            IIntermediateCoercibleType<TCoercionParent, TIntermediateCoercionParent>,
            TCoercionParent
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateBinaryOperatorCoercionMemberDictionary{TCoercionParent, TIntermediateCoercionParent}"/> 
        /// assocaited to the <typeparamref name="TCoercionParent"/>.
        /// </summary>
        new IIntermediateBinaryOperatorCoercionMemberDictionary<TCoercionParent, TIntermediateCoercionParent> BinaryOperatorCoercions { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateTypeCoercionMemberDictionary{TCoercionParent, TIntermediateCoercionParent}"/> 
        /// assocaited to the <typeparamref name="TCoercionParent"/>.
        /// </summary>
        new IIntermediateTypeCoercionMemberDictionary<TCoercionParent, TIntermediateCoercionParent> TypeCoercions { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateUnaryOperatorCoercionMemberDictionary{TCoercionParent, TIntermediateCoercionParent}"/> 
        /// assocaited to the <typeparamref name="TCoercionParent"/>.
        /// </summary>
        new IIntermediateUnaryOperatorCoercionMemberDictionary<TCoercionParent, TIntermediateCoercionParent> UnaryOperatorCoercions { get; }
    }

    /// <summary>
    /// Defines generic properties and methods for working with an intermediate type which supports
    /// base expression coercion against it.
    /// </summary>
    /// <typeparam name="TCoercionIdentifier">The kind of identifier used to
    /// differentiate the <typeparamref name="TIntermediateCoercion"/> from its 
    /// siblings.</typeparam>
    /// <typeparam name="TCoercion">The type of coercion in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCoercion">The type of coercion in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TCoercionParent">The kind of coercible type in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCoercionParent">The kind of coercible type in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateCoercibleType<TCoercionIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent> :
        IIntermediateType,
        IIntermediateMemberParent,
        ICoercibleType<TCoercionIdentifier, TCoercion, TCoercionParent>,
        IIntermediateCoercibleType
        where TCoercionIdentifier :
            IMemberUniqueIdentifier,
            IGeneralMemberUniqueIdentifier
        where TCoercion :
            ICoercionMember<TCoercionIdentifier, TCoercion, TCoercionParent>
        where TIntermediateCoercion :
            IIntermediateCoercionMember<TCoercionIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent>,
            TCoercion
        where TCoercionParent :
            ICoercibleType<TCoercionIdentifier, TCoercion, TCoercionParent>
        where TIntermediateCoercionParent :
            IIntermediateCoercibleType<TCoercionIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent>,
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
