using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System.Reflection;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// The direction of variance allowed on the 
    /// generic parameter.
    /// </summary>
    public enum GenericParameterVariance
    {
        /// <summary>
        /// The parameter is invariant and is accepted in both input and
        /// output positions, which leads to types that are tightly bound
        /// and can neither be more or less derived.
        /// </summary>
        Invariant = 0,
        /// <summary>
        /// The parameter is covariant and can appear in an output
        /// position, the results of which can be more-derived than
        /// the specified type.
        /// </summary>
        Covariant = 1,
        /// <summary>
        /// The parameter is contravariant and can appear in an input
        /// position, the input of which can be less derived than the
        /// specified type.
        /// </summary>
        Contravariant = 2,
    }
    /// <summary>
    /// Defines generic properties and methods for working with a generic's
    /// type parameter.
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of <see cref="IGenericParameter{TGenericParameter, TParent}"/>
    /// that is contained by the <see cref="IGenericParamParent{TGenericParameter, TParent}"/>.</typeparam>
    /// <typeparam name="TParent">The type of <see cref="IGenericParamParent{TGenericParameter, TParent}"/> in the
    /// current implmentation.</typeparam>
    public interface IGenericParameter<TGenericParameter, TParent> :
        IGenericParameter<TGenericParameter>
        where TGenericParameter :
            IGenericParameter<TGenericParameter, TParent>
        where TParent :
            IGenericParamParent<TGenericParameter, TParent>
    {
        /// <summary>
        /// Returns the <typeparamref name="TParent"/> which
        /// contains the <see cref="IGenericParameter{TGenericParameter, TParent}"/>.
        /// </summary>
        new TParent Parent { get; }
    }
    /// <summary>
    /// Defines generic properties and methods for working 
    /// with a generic parameter.
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of 
    /// <see cref="IGenericParameter{TGenericParameter}"/> in the current implementation.
    /// </typeparam>
    public interface IGenericParameter<TGenericParameter> :
        ICreatableType<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter>,
        //ISignatureParent<IGenericParameterMethodMember<TGenericParameter>, IGenericParameterMethodParameterMember<TGenericParameter>, TGenericParameter>,
        IMethodSignatureParent<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>,
        IEventSignatureParent<IGenericParameterEventMember<TGenericParameter>, TGenericParameter>,
        IIndexerSignatureParent<IGenericParameterIndexerMember<TGenericParameter>, TGenericParameter>,
        IPropertySignatureParentType<IGenericParameterPropertyMember<TGenericParameter>, TGenericParameter>,
        IGenericParameter
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {
        /// <summary>
        /// Returns the <see cref="IGenericParameterConstructorMemberDictionary{TGenericParameter}"/>
        /// associated to the current <see cref="IGenericParameter{TGenericParameter}"/>.
        /// </summary>
        new IGenericParameterConstructorMemberDictionary<TGenericParameter> Constructors { get; }
        /// <summary>
        /// Returns the <see cref="IGenericParameterEventMemberDictionary{TGenericParameter}"/>
        /// associated to the current <see cref="IGenericParameter{TGenericParameter}"/>.
        /// </summary>
        new IGenericParameterEventMemberDictionary<TGenericParameter> Events { get; }
        /// <summary>
        /// Returns the <see cref="IGenericParameterIndexerMemberDictionary{TGenericParameter}"/>
        /// associated to the current <see cref="IGenericParameter{TGenericParameter}"/>.
        /// </summary>
        new IGenericParameterIndexerMemberDictionary<TGenericParameter> Indexers { get; }
        /// <summary>
        /// Returns the <see cref="IGenericParameterMethodMemberDictionary{TGenericParameter}"/>
        /// associated to the current <see cref="IGenericParameter{TGenericParameter}"/>.
        /// </summary>
        new IGenericParameterMethodMemberDictionary<TGenericParameter> Methods { get; }
        /// <summary>
        /// Returns the <see cref="IGenericParameterPropertyMemberDictionary{TGenericParameter}"/>
        /// associated to the current <see cref="IGenericParameter{TGenericParameter}"/>.
        /// </summary>
        new IGenericParameterPropertyMemberDictionary<TGenericParameter> Properties { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a generic's parameter.
    /// </summary>
    public interface IGenericParameter :
        ICreatableType,
        IMethodSignatureParent,
        IEventSignatureParent,
        IIndexerSignatureParent,
        IPropertySignatureParentType
    {
        GenericParameterVariance Variance { get; }
        /// <summary>
        /// Returns whether the <see cref="IGenericParameter"/> requires a
        /// new constructor constraint.
        /// </summary>
        bool RequiresNewConstructor { get; }
        /// <summary>
        /// Returns the special constraint placed upon the <see cref="IGenericParameter"/>.
        /// </summary>
        GenericTypeParameterSpecialConstraint SpecialConstraint { get; }
        /// <summary>
        /// Returns the index of the <see cref="IGenericParameter"/>.
        /// </summary>
        int Position { get; }
        /// <summary>
        /// Returns a <see cref="ILockedTypeCollection"/> which relates to the
        /// current <see cref="IGenericParameter"/>'s
        /// constraints.
        /// </summary>
        ILockedTypeCollection Constraints { get; }
        /// <summary>
        /// Returns the name of the <see cref="IGenericParameter"/>.
        /// </summary>
        new string Name { get; }
        /// <summary>
        /// Returns the <see cref="IGenericParamParent"/> which
        /// contains the <see cref="IGenericParameter"/>.
        /// </summary>
        IGenericParamParent Parent { get; }
        /// <summary>
        /// Returns the <see cref="IGenericParameterConstructorMemberDictionary"/>
        /// associated to the current <see cref="IGenericParameter"/>.
        /// </summary>
        new IGenericParameterConstructorMemberDictionary Constructors { get; }
        /// <summary>
        /// Returns the <see cref="IGenericParameterEventMemberDictionary"/> 
        /// associated to the current <see cref="IGenericParameter"/>.
        /// </summary>
        new IGenericParameterEventMemberDictionary Events { get; }
        /// <summary>
        /// Returns the <see cref="IGenericParameterIndexerMemberDictionary"/>
        /// associated to the current <see cref="IGenericParameter"/>.
        /// </summary>
        new IGenericParameterIndexerMemberDictionary Indexers { get; }
        /// <summary>
        /// Returns the <see cref="IGenericParameterMethodMemberDictionary"/>
        /// associated to the current <see cref="IGenericParameter"/>.
        /// </summary>
        new IGenericParameterMethodMemberDictionary Methods { get; }
        /// <summary>
        /// Returns the <see cref="IGenericParameterPropertyMemberDictionary"/>
        /// associated to the current <see cref="IGenericParameter"/>.
        /// </summary>
        new IGenericParameterPropertyMemberDictionary Properties { get; }
    }
}
