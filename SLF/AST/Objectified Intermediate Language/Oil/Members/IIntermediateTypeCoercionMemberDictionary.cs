using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a series of 
    /// intermediate type coercion members.
    /// </summary>
    /// <typeparam name="TType">The type of parent that contains the 
    /// type coercion member in abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The type of parent that contains the 
    /// type coercion member in intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateTypeCoercionMemberDictionary<TType, TIntermediateType> :
        IIntermediateGroupedMemberDictionary<TType, TIntermediateType, ITypeCoercionMember<TType>, IIntermediateTypeCoercionMember<TType, TIntermediateType>>,
        ITypeCoercionMemberDictionary<TType>
        where TType :
            ICoercibleType<ITypeCoercionMember<TType>, TType>
        where TIntermediateType :
            TType,
            IIntermediateCoercibleType<ITypeCoercionMember<TType>, IIntermediateTypeCoercionMember<TType, TIntermediateType>, TType, TIntermediateType>
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateTypeCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/>
        /// which meets the <paramref name="requirement"/> in
        /// the <paramref name="direction"/> for
        /// the <paramref name="target"/> specified.
        /// </summary>
        /// <param name="requirement">The <see cref="TypeConversionRequirement"/>
        /// necessary for the coercion, either 
        /// explicit or implicit.</param>
        /// <param name="direction">The coercion direction;
        /// either from or to the type.</param>
        /// <param name="target">
        /// The <see cref="IType"/> which is coerced.</param>
        /// <returns>The <see cref="IIntermediateTypeCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/> 
        /// which met the <paramref name="requirement"/> in
        /// the <paramref name="direction"/> for
        /// the <paramref name="target"/> specified</returns>
        /// <exception cref="System.ArgumentException">
        /// thrown when there is no explicit/implicit coercion
        /// from/to <typeparamref name="target"/>.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// thrown when either <paramref name="requirement"/> or 
        /// <paramref name="direction"/> is out of range.</exception>
        new IIntermediateTypeCoercionMember<TType, TIntermediateType> this[
            TypeConversionRequirement requirement,
            TypeConversionDirection direction,
            IType target] { get; }
        /// <summary>
        /// Adds a new <see cref="IIntermediateTypeCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/>
        /// with the conversion <paramref name="requirement"/>, <paramref name="direction"/> and target.
        /// </summary>
        /// <param name="requirement">The <see cref="TypeConversionRequirement"/>
        /// which determines how the cast is applied.</param>
        /// <param name="direction">The coercion direction;
        /// either from or to the type.</param>
        /// <param name="target">
        /// The <see cref="IType"/> which is coerced.</param>
        /// <returns>A new <see cref="IIntermediateTypeCoercionMember{TType, TIntermediateType}"/>
        /// instance.</returns>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="target"/>
        /// is an interface.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// thrown when either <paramref name="requirement"/> or 
        /// <paramref name="direction"/> is out of range.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
        IIntermediateTypeCoercionMember<TType, TIntermediateType> Add(TypeConversionRequirement requirement, TypeConversionDirection direction, IType target);
    }
    /// <summary>
    /// Defines properties and methods for working with a series of 
    /// intermediate type coercion members.
    /// </summary>
    public interface IIntermediateTypeCoercionMemberDictionary :
        IIntermediateGroupedMemberDictionary,
        ITypeCoercionMemberDictionary
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateTypeCoercionMember"/>
        /// which meets the <paramref name="requirement"/> in
        /// the <paramref name="direction"/> for
        /// the <paramref name="target"/> specified.
        /// </summary>
        /// <param name="requirement">The <see cref="TypeConversionRequirement"/>
        /// necessary for the coercion, either 
        /// explicit or implicit.</param>
        /// <param name="direction">The coercion direction;
        /// either from or to the type.</param>
        /// <param name="target">
        /// The <see cref="IType"/> which is coerced.</param>
        /// <returns>The <see cref="IIntermediateTypeCoercionMember"/> 
        /// which met the <paramref name="requirement"/> in
        /// the <paramref name="direction"/> for
        /// the <paramref name="target"/> specified</returns>
        /// <exception cref="System.ArgumentException">
        /// thrown when there is no explicit/implicit coercion
        /// from/to <typeparamref name="target"/>.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// thrown when either <paramref name="requirement"/> or 
        /// <paramref name="direction"/> is out of range.</exception>
        new IIntermediateTypeCoercionMember this[
            TypeConversionRequirement requirement,
            TypeConversionDirection direction,
            IType target] { get; }
        /// <summary>
        /// Adds a new <see cref="IIntermediateTypeCoercionMember"/>
        /// with the conversion <paramref name="requirement"/>, <paramref name="direction"/> and target.
        /// </summary>
        /// <param name="requirement">The <see cref="TypeConversionRequirement"/>
        /// which determines how the cast is applied.</param>
        /// <param name="direction">The coercion direction;
        /// either from or to the type.</param>
        /// <param name="target">
        /// The <see cref="IType"/> which is coerced.</param>
        /// <returns>A new <see cref="IIntermediateTypeCoercionMember"/>
        /// instance.</returns>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="target"/>
        /// is an interface.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// thrown when either <paramref name="requirement"/> or 
        /// <paramref name="direction"/> is out of range.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
        IIntermediateTypeCoercionMember Add(TypeConversionRequirement requirement, TypeConversionDirection direction, IType target);
    }
}
