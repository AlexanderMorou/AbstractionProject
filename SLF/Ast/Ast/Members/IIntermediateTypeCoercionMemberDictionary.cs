using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a series of 
    /// intermediate type coercion members.
    /// </summary>
    /// <typeparam name="TCoercionParentIdentifier">The kind of identifier used
    /// to differentiate the <typeparamref name="TIntermediateCoercionParent"/>
    /// instance from its siblings.</typeparam>
    /// <typeparam name="TCoercionParent">The type of parent that contains the 
    /// type coercion member in abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCoercionParent">The type of parent that contains the 
    /// type coercion member in intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateTypeCoercionMemberDictionary<TCoercionParent, TIntermediateCoercionParent> :
        IIntermediateGroupedMemberDictionary<TCoercionParent, TIntermediateCoercionParent, ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, IIntermediateTypeCoercionMember<TCoercionParent, TIntermediateCoercionParent>>,
        ITypeCoercionMemberDictionary<TCoercionParent>
        where TCoercionParent :
            ICoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, TCoercionParent>
        where TIntermediateCoercionParent :
            TCoercionParent,
            IIntermediateCoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, IIntermediateTypeCoercionMember<TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>
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
        /// from/to <paramref name="target"/>.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// thrown when either <paramref name="requirement"/> or 
        /// <paramref name="direction"/> is out of range.</exception>
        new IIntermediateTypeCoercionMember<TCoercionParent, TIntermediateCoercionParent> this[
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
        /// <returns>A new <see cref="IIntermediateTypeCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/>
        /// instance.</returns>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="target"/>
        /// is an interface.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// thrown when either <paramref name="requirement"/> or 
        /// <paramref name="direction"/> is out of range.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
        IIntermediateTypeCoercionMember<TCoercionParent, TIntermediateCoercionParent> Add(TypeConversionRequirement requirement, TypeConversionDirection direction, IType target);
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
        /// from/to <paramref name="target"/>.
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
