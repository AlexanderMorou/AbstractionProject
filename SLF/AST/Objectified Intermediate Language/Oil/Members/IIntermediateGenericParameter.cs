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
    /// Defines generic properties and methods for working with an intermediate
    /// generic parameter.
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of generic parameter in the abstract
    /// type system.</typeparam>
    /// <typeparam name="TIntermediateGenericParameter">The type of generic parameter in
    /// the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TParent">The type which owns the generic parameters in the abstract
    /// type system.</typeparam>
    /// <typeparam name="TIntermediateParent">The type which owns the generic parameters in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> :
        IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter>,
        IGenericParameter<TGenericParameter, TParent>
        where TGenericParameter :
            IGenericParameter<TGenericParameter, TParent>
        where TIntermediateGenericParameter :
            TGenericParameter,
            IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
        where TParent :
            IGenericParamParent<TGenericParameter, TParent>
        where TIntermediateParent :
            TParent,
            IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
    {
        /// <summary>
        /// Returns the <typeparamref name="TIntermediateParent"/> which
        /// contains the <see cref="IIntermediateGenericParameter{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>.
        /// </summary>
        new TIntermediateParent Parent { get; }
    }
    /// <summary>
    /// Defines generic properties and methods for working with an intermediate
    /// generic parameter.
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of generic parameter in the abstract
    /// type system.</typeparam>
    /// <typeparam name="TIntermediateGenericParameter">The type of generic parameter in
    /// the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter> :
        IIntermediateCreatableSignatureType<IGenericParameterConstructorMember<TGenericParameter>, IIntermediateGenericParameterConstructorMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
        IIntermediateMethodSignatureParent<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
        IIntermediateEventSignatureParent<IGenericParameterEventMember<TGenericParameter>, IIntermediateGenericParameterEventMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
        IIntermediateIndexerSignatureParent<IGenericParameterIndexerMember<TGenericParameter>, IIntermediateGenericParameterIndexerMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
        IIntermediatePropertySignatureParentType<IGenericParameterPropertyMember<TGenericParameter>, IIntermediateGenericParameterPropertyMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
        IIntermediateGenericParameter,
        IGenericParameter<TGenericParameter>
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
        where TIntermediateGenericParameter :
            TGenericParameter,
            IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter>
    {
        /// <summary>
        /// Returns the <see cref="IGenericParameterConstructorMemberDictionary{TGenericParameter}"/>
        /// associated to the current <see cref="IGenericParameter{TGenericParameter}"/>.
        /// </summary>
        new IIntermediateGenericParameterConstructorMemberDictionary<TGenericParameter, TIntermediateGenericParameter> Constructors { get; }
        /// <summary>
        /// Returns the <see cref="IGenericParameterEventMemberDictionary{TGenericParameter}"/>
        /// associated to the current <see cref="IGenericParameter{TGenericParameter}"/>.
        /// </summary>
        new IIntermediateGenericParameterEventMemberDictionary<TGenericParameter, TIntermediateGenericParameter> Events { get; }
        /// <summary>
        /// Returns the <see cref="IGenericParameterIndexerMemberDictionary{TGenericParameter}"/>
        /// associated to the current <see cref="IGenericParameter{TGenericParameter}"/>.
        /// </summary>
        new IIntermediateGenericParameterIndexerMemberDictionary<TGenericParameter, TIntermediateGenericParameter> Indexers { get; }
        /// <summary>
        /// Returns the <see cref="IGenericParameterMethodMemberDictionary{TGenericParameter}"/>
        /// associated to the current <see cref="IGenericParameter{TGenericParameter}"/>.
        /// </summary>
        new IIntermediateGenericParameterMethodMemberDictionary<TGenericParameter, TIntermediateGenericParameter> Methods { get; }
        /// <summary>
        /// Returns the <see cref="IGenericParameterPropertyMemberDictionary{TGenericParameter}"/>
        /// associated to the current <see cref="IGenericParameter{TGenericParameter}"/>.
        /// </summary>
        new IIntermediateGenericParameterPropertyMemberDictionary<TGenericParameter, TIntermediateGenericParameter> Properties { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with an intermediate generic
    /// parameter.
    /// </summary>
    public interface IIntermediateGenericParameter :
        IIntermediateCreatableSignatureType,
        IIntermediateMethodSignatureParent,
        IIntermediateEventSignatureParent,
        IIntermediateIndexerSignatureParent,
        IIntermediatePropertySignatureParentType,
        IGenericParameter
    {
        /// <summary>
        /// Returns the <see cref="IGenericParameterConstructorMemberDictionary"/>
        /// associated to the current <see cref="IGenericParameter"/>.
        /// </summary>
        new IIntermediateGenericParameterConstructorMemberDictionary Constructors { get; }
        /// <summary>
        /// Returns the <see cref="IGenericParameterEventMemberDictionary"/> 
        /// associated to the current <see cref="IGenericParameter"/>.
        /// </summary>
        new IIntermediateGenericParameterEventMemberDictionary Events { get; }
        /// <summary>
        /// Returns the <see cref="IGenericParameterIndexerMemberDictionary"/>
        /// associated to the current <see cref="IGenericParameter"/>.
        /// </summary>
        new IIntermediateGenericParameterIndexerMemberDictionary Indexers { get; }
        /// <summary>
        /// Returns the <see cref="IGenericParameterMethodMemberDictionary"/>
        /// associated to the current <see cref="IGenericParameter"/>.
        /// </summary>
        new IIntermediateGenericParameterMethodMemberDictionary Methods { get; }
        /// <summary>
        /// Returns the <see cref="IGenericParameterPropertyMemberDictionary"/>
        /// associated to the current <see cref="IGenericParameter"/>.
        /// </summary>
        new IIntermediateGenericParameterPropertyMemberDictionary Properties { get; }
        /// <summary>
        /// Returns/sets whether the <see cref="IIntermediateGenericParameter"/> requires a new
        /// constructor constraint.
        /// </summary>
        new bool RequiresNewConstructor { get; set; }
        /// <summary>
        /// Returns/sets the special constraint placed upon the <see cref="IIntermediateGenericParameter"/>.
        /// </summary>
        new GenericTypeParameterSpecialConstraint SpecialConstraint { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="Int32"/> value representing the ordinal index of the 
        /// <see cref="IIntermediateGenericParameter"/>.
        /// </summary>
        new int Position { get; set; }
        /// <summary>
        /// Returns a <see cref="ITypeCollection"/> which relates to the
        /// current <see cref="IIntermediateGenericParameter"/>'s
        /// constraints.
        /// </summary>
        new ITypeCollection Constraints { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateGenericParameterParent"/> which
        /// contains the <see cref="IIntermediateGenericParameter"/>.
        /// </summary>
        new IIntermediateGenericParameterParent Parent { get; }
    }
}
