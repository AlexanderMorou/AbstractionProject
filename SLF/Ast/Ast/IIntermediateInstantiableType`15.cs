using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines generic properties and methods for working with an intermediate
    /// type that can be instantiated.
    /// </summary>
    /// <typeparam name="TCtor">The type used for the constructors in the current implementation.</typeparam>
    /// <typeparam name="TIntermediateCtor">The type of constructor member in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TEvent">The type used for the events in the current implementation.</typeparam>
    /// <typeparam name="TIntermediateEvent">The type of <see cref="IIntermediateEventMember{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/> in
    /// the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TField">The type used for the fields in the current implementation.</typeparam>
    /// <typeparam name="TIntermediateField">The type of field in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TIndexer">The type used for the indexers in the current implementation.</typeparam>
    /// <typeparam name="TIntermediateIndexer">The type of indexzer in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TMethod">The type used for the methods in the current implementation.</typeparam>
    /// <typeparam name="TIntermediateMethod">The type of methods in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TProperty">The type used for the properties in the current implementation.</typeparam>
    /// <typeparam name="TIntermediateProperty">The type of property member used in the
    /// intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TTypeIdentifier">The kind of type identifier used
    /// to differentiate the <typeparamref name="TIntermediateType"/>
    /// instance from its siblings.</typeparam>
    /// <typeparam name="TType">The <see cref="IInstantiableType{TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TTypeIdentifier, TType}"/> 
    /// in the implementation.</typeparam>
    /// <typeparam name="TIntermediateType">The type kind of type used within the intermediate abstract syntax
    /// tree to represent the malleable form of the type during design time.</typeparam>
    public interface IIntermediateInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TTypeIdentifier, TType, TIntermediateType> :
        IIntermediateSegmentableType<TTypeIdentifier, TType, TIntermediateType>,
        IIntermediateMethodParent<TMethod, TIntermediateMethod, TType, TIntermediateType>,
        IIntermediateCreatableParent<TCtor, TIntermediateCtor, TType, TIntermediateType>,
        IIntermediatePropertyParent<TProperty, TIntermediateProperty, TType, TIntermediateType>,
        IIntermediateFieldParent<TField, TIntermediateField, TType, TIntermediateType>,
        IIntermediateCoercibleType<TType, TIntermediateType>,
        IIntermediateEventParent<TEvent, TIntermediateEvent, TType, TIntermediateType>,
        IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TType, TIntermediateType>,
        IIntermediateInstantiableType,
        IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TTypeIdentifier, TType>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>,
            TCtor
        where TEvent :
            IEventMember<TEvent, TType>
        where TIntermediateEvent :
            IIntermediateEventMember<TEvent, TIntermediateEvent, TType, TIntermediateType>,
            TEvent
        where TField :
            IFieldMember<TField, TType>,
            IInstanceMember
        where TIntermediateField :
            IIntermediateFieldMember<TField, TIntermediateField, TType, TIntermediateType>,
            TField
        where TIndexer :
            IIndexerMember<TIndexer, TType>
        where TIntermediateIndexer :
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TType, TIntermediateType>,
            TIndexer
        where TMethod :
            IMethodMember<TMethod, TType>,
            IExtendedInstanceMember
        where TIntermediateMethod :
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TType, TIntermediateType>,
            TMethod
        where TProperty :
            IPropertyMember<TProperty, TType>
        where TIntermediateProperty :
            IIntermediatePropertyMember<TProperty, TIntermediateProperty, TType, TIntermediateType>,
            TProperty
        where TTypeIdentifier :
            ITypeUniqueIdentifier,
            IGeneralDeclarationUniqueIdentifier
        where TType :
            IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TTypeIdentifier, TType>
        where TIntermediateType :
            IIntermediateInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TTypeIdentifier, TType, TIntermediateType>,
            TType
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateInstantiableTypeImplementedInterfaces{TEvent, TIntermediateEvent, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TParent, TIntermediateParent}"/>
        /// which represents the interfaces implemented by the current 
        /// <see cref="IIntermediateInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TTypeIdentifier, TType, TIntermediateType}"/>.
        /// </summary>
        new IIntermediateInstantiableTypeImplementedInterfaces<TEvent, TIntermediateEvent, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType> ImplementedInterfaces { get; }
        new IIntermediateIdentityManager IdentityManager { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> in which the
        /// <see cref="IIntermediateTypeParent"/> is defined.
        /// </summary>
        new IIntermediateAssembly Assembly { get; }
    }
    public interface IIntermediateInstantiableType :
        IIntermediateTypeParent,
        IIntermediateMethodParent,
        IIntermediateCreatableParent,
        IIntermediatePropertyParent,
        IIntermediateFieldParent,
        IIntermediateEventParent,
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
        /// Returns whether the <see cref="IIntermediateInstantiableType"/>
        /// has had its duality suspended.
        /// </summary>
        bool Suspended { get; }
        /// <summary>
        /// Returns a <see cref="IBoundSpecialReferenceExpression"/>
        /// relative to the current type as an instance.
        /// </summary>
        /// <returns>A <see cref="IBoundSpecialReferenceExpression"/>
        /// which is bound to the current <see cref="IIntermediateInstantiableType"/>.</returns>
        IBoundSpecialReferenceExpression GetThis();
        /// <summary>
        /// Returns the <see cref="IIntermediateIdentityManager"/> which
        /// helps resolve type identities.
        /// </summary>
        new IIntermediateIdentityManager IdentityManager { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> in which the
        /// <see cref="IIntermediateTypeParent"/> is defined.
        /// </summary>
        new IIntermediateAssembly Assembly { get; }
        bool HasTypeInitializer { get; }
    }
}
