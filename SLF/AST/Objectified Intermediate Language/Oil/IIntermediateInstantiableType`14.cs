using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines generic properties and methods for working with an intermediate
    /// type that can be instantiated.
    /// </summary>
    /// <typeparam name="TCtor">The type used for the constructors in the current implementation.</typeparam>
    /// <typeparam name="TEvent">The type used for the events in the current implementation.</typeparam>
    /// <typeparam name="TField">The type used for the fields in the current implementation.</typeparam>
    /// <typeparam name="TIndexer">The type used for the indexers in the current implementation.</typeparam>
    /// <typeparam name="TMethod">The type used for the methods in the current implementation.</typeparam>
    /// <typeparam name="TProperty">The type used for the properties in the current implementation.</typeparam>
    /// <typeparam name="TType">The <see cref="IInstantiableType{TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType}"/> 
    /// in the implementation.</typeparam>
    public interface IIntermediateInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType> :
        IIntermediateCreatableType<TCtor, TIntermediateCtor, TType, TIntermediateType>,
        IIntermediateMethodParent<TMethod, TIntermediateMethod, TType, TIntermediateType>,
        IIntermediatePropertyParentType<TProperty, TIntermediateProperty, TType, TIntermediateType>,
        IIntermediateFieldParent<TField, TIntermediateField, TType, TIntermediateType>,
        IIntermediateCoercibleType<TType, TIntermediateType>,
        IIntermediateEventParent<TEvent, TIntermediateEvent, TType, TIntermediateType>,
        IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TType, TIntermediateType>,
        IIntermediateSegmentableType<TType, TIntermediateType>,
        IIntermediateInstantiableType,
        IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            TCtor,
            IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TEvent :
            IEventMember<TEvent, TType>
        where TIntermediateEvent :
            TEvent, 
            IIntermediateEventMember<TEvent, TIntermediateEvent, TType, TIntermediateType>
        where TField :
            IFieldMember<TField, TType>,
            IInstanceMember
        where TIntermediateField :
            TField, 
            IIntermediateFieldMember<TField, TIntermediateField, TType, TIntermediateType>
        where TIndexer :
            IIndexerMember<TIndexer, TType>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TType, TIntermediateType>
        where TMethod :
            IMethodMember<TMethod, TType>,
            IExtendedInstanceMember
        where TIntermediateMethod :
            TMethod,
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TType, TIntermediateType>
        where TProperty :
            IPropertyMember<TProperty, TType>
        where TIntermediateProperty :
            TProperty,
            IIntermediatePropertyMember<TProperty, TIntermediateProperty, TType, TIntermediateType>
        where TType :
            IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType>
        where TIntermediateType :
            TType,
            IIntermediateInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType>
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateInstantiableTypeImplementedInterfaces{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType}"/>
        /// which represents the interfaces implemented by the current 
        /// <see cref="IIntermediateInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType}"/>.
        /// </summary>
        new IIntermediateInstantiableTypeImplementedInterfaces<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType> ImplementedInterfaces { get; }
    }
    public interface IIntermediateInstantiableType :
        IIntermediateCreatableType,
        IIntermediatePropertyParentType,
        IIntermediateFieldParent,
        IIntermediateTypeParent,
        IIntermediateMethodParent,
        IInstantiableType
    {
        /// <summary>
        /// Suspends the duality in the type layout where members 
        /// inserted in methods, properties, events and so on are 
        /// dually inserted in a verbatim-order master set.
        /// </summary>
        /// <remarks>Incremental function, all resumes must
        /// be invoked prior to resuming the duality.</remarks>
        void SuspendDualLayout();
        /// <summary>
        /// Resumes the duality in the type layout where members
        /// inserted in methods, properties, events, and so on are
        /// dually inserted in a verbatim-order master set.
        /// </summary>
        /// <remarks>Incremental function, all resumes must
        /// be invoked prior to resuming the duality.</remarks>
        void ResumeDualLayout();
        /// <summary>
        /// Returns/sets whether the <see cref="IIntermediateInstantiableType"/>
        /// has had its duality suspended.
        /// </summary>
        bool Suspended { get; }
    }
}
