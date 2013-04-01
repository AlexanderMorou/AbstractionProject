using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines properties and methods for visiting the various kinds of members
    /// definable in code.
    /// </summary>
    public interface IIntermediateMemberVisitor
    {
        /// <summary>
        /// Visits the <paramref name="local"/> provided.
        /// </summary>
        /// <param name="local">The <see cref="ILocalMember"/> to visit.</param>
        void Visit(ILocalMember local);
        #region Constructor coercion members
        /// <summary>
        /// Visits the <paramref name="ctor"/> provided.
        /// </summary>
        /// <typeparam name="TCtor">The type of <see cref="IConstructorMember{TCtor, TCtorParent}"/>
        /// in the current implementation.</typeparam>
        /// <typeparam name="TIntermediateCtor">The type of
        /// <see cref="IIntermediateConstructorSignatureMember{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// in the current implementation.</typeparam>
        /// <typeparam name="TType">The type of <see cref="ICreatableParent{TCtor, TCtorParent}"/>
        /// that contains the <typeparamref name="TCtor"/> instances.</typeparam>
        /// <typeparam name="TIntermediateType">The type of 
        /// <see cref="IIntermediateCreatableSignatureParent{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// which contains the <typeparamref name="TIntermediateCtor"/>.</typeparam>
        /// <param name="ctor">The <see cref="IIntermediateConstructorSignatureMember{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// to visit.</param>
        void Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
            where TCtor :
                IConstructorMember<TCtor, TType>
            where TIntermediateCtor :
                TCtor,
                IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
            where TType :
                ICreatableParent<TCtor, TType>
            where TIntermediateType :
                TType,
                IIntermediateCreatableSignatureParent<TCtor, TIntermediateCtor, TType, TIntermediateType>;
        /// <summary>
        /// Visits the <paramref name="ctor"/> provided.
        /// </summary>
        /// <typeparam name="TCtor">The type of the constructor in the abstract type system.</typeparam>
        /// <typeparam name="TIntermediateCtor">The type of the constructor in the intermediate type system.</typeparam>
        /// <typeparam name="TType">The type of the owning <see cref="ICreatableParent{TCtor, TIntermediateType}"/> in 
        /// the abstract type system.</typeparam>
        /// <typeparam name="TIntermediateType">The type of the owning <see cref="IIntermediateCreatableParent{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// in the intermediate abstract syntax tree.</typeparam>
        /// <param name="ctor">The <see cref="IIntermediateConstructorMember{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// to visit.</param>
        void Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
            where TCtor :
                IConstructorMember<TCtor, TType>
            where TIntermediateCtor :
                TCtor,
                IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
            where TType :
                ICreatableParent<TCtor, TType>
            where TIntermediateType :
                TType,
                IIntermediateCreatableParent<TCtor, TIntermediateCtor, TType, TIntermediateType>;
        #endregion
        #region Events members
        /// <summary>
        /// Visits the <paramref name="event"/> provided.
        /// </summary>
        /// <typeparam name="TEvent">The type of event used in the abstract type system.</typeparam>
        /// <typeparam name="TIntermediateEvent">The type of event used in the intermediate
        /// abstract syntax tree.</typeparam>
        /// <typeparam name="TEventParent">The type which contains the <typeparamref name="TEvent"/>
        /// instances in the abstract type system.</typeparam>
        /// <typeparam name="TIntermediateEventParent">The type which contains the <typeparamref name="TIntermediateEvent"/>
        /// instances in the intermediate abstract syntax tree.</typeparam>
        /// <param name="event">The <see cref="IIntermediateEventMember{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/> to visit.</param>
        void Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
            where TEvent :
                IEventMember<TEvent, TEventParent>
            where TIntermediateEvent :
                TEvent,
                IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            where TEventParent :
                IEventParent<TEvent, TEventParent>
            where TIntermediateEventParent :
                TEventParent,
                IIntermediateEventParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>;
        /// <summary>
        /// Visits the <paramref name="event"/> provided.
        /// </summary>
        /// <typeparam name="TEvent">The type of event in the abstract type system.</typeparam>
        /// <typeparam name="TIntermediateEvent">The type of event in the intermediate
        /// abstract syntax tree.</typeparam>
        /// <typeparam name="TEventParent">The type which contains the <typeparamref name="TEvent"/>
        /// instances in the abstract type system.</typeparam>
        /// <typeparam name="TIntermediateEventParent">The type which contains the
        /// <typeparamref name="TIntermediateEvent"/> instances in the intermediate
        /// abstract syntax tree.</typeparam>
        /// <param name="event">The <see cref="IIntermediateEventSignatureMember{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/> to visit.</param>
        void Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
            where TEvent :
                IEventSignatureMember<TEvent, TEventParent>
            where TIntermediateEvent :
                TEvent,
                IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParent>
            where TIntermediateEventParent :
                TEventParent,
                IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>;
        #endregion
        #region Expression coercion members
        /// <summary>
        /// Visits the <paramref name="binaryCoercion"/> provided.
        /// </summary>
        /// <typeparam name="TCoercionParentIdentifier">The kind of identifier used to differentiate the
        /// <paramref name="binaryCoercion"/> instance from its siblings.</typeparam>
        /// <typeparam name="TCoercionParent">
        /// The type of parent that contains the binary operation 
        /// coercion member in the current implementation.</typeparam>
        /// <param name="binaryCoercion">The <see cref="IBinaryOperatorCoercionMember{TCoercionParent}"/>
        /// to visit.</param>
        void Visit<TCoercionParent>(IBinaryOperatorCoercionMember<TCoercionParent> binaryCoercion)
            where TCoercionParent :
                ICoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>;
        void Visit<TCoercionParent>(ITypeCoercionMember<TCoercionParent> typeCoercion)
            where TCoercionParent :
                ICoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, TCoercionParent>;
        void Visit<TCoercionParent>(IUnaryOperatorCoercionMember<TCoercionParent> unaryCoercion)
            where TCoercionParent :
                ICoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>;
        #endregion
        #region Field member
        void Visit<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>(IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> field)
            where TField :
                IFieldMember<TField, TFieldParent>
            where TIntermediateField :
                TField,
                IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
            where TFieldParent :
                IFieldParent<TField, TFieldParent>
            where TIntermediateFieldParent :
                TFieldParent,
                IIntermediateFieldParent<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>;
        void Visit(IIntermediateEnumFieldMember field);
        #endregion
        #region Indexer members
        void Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexer)
            where TIndexer :
                IIndexerMember<TIndexer, TIndexerParent>
            where TIntermediateIndexer :
                TIndexer,
                IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            where TIndexerParent :
                IIndexerParent<TIndexer, TIndexerParent>
            where TIntermediateIndexerParent :
                TIndexerParent,
                IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>;
        void Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexerSignature)
            where TIndexer :
                IIndexerSignatureMember<TIndexer, TIndexerParent>
            where TIntermediateIndexer :
                TIndexer,
                IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            where TIndexerParent :
                IIndexerSignatureParent<TIndexer, TIndexerParent>
            where TIntermediateIndexerParent :
                TIndexerParent,
                IIntermediateIndexerSignatureParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>;
        #endregion
        #region Method members
        void Visit<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>(IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> method)
            where TMethod :
                IMethodMember<TMethod, TMethodParent>
            where TIntermediateMethod :
                IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
                TMethod
            where TMethodParent :
                IMethodParent<TMethod, TMethodParent>
            where TIntermediateMethodParent :
                IIntermediateMethodParent<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
                TMethodParent;
        void Visit<TSignature, TIntermediateSignature, TParent, TIntermediateParent>(IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> methodSignature)
            where TSignature :
                IMethodSignatureMember<TSignature, TParent>
            where TIntermediateSignature :
                TSignature,
                IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
            where TParent :
                IMethodSignatureParent<TSignature, TParent>
            where TIntermediateParent :
                TParent,
                IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TParent, TIntermediateParent>;
        #endregion
        #region Property members
        void Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> propertySignature)
            where TProperty :
                IPropertySignatureMember<TProperty, TPropertyParent>
            where TIntermediateProperty :
                TProperty,
                IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
            where TPropertyParent :
                IPropertySignatureParent<TProperty, TPropertyParent>
            where TIntermediatePropertyParent :
                TPropertyParent,
                IIntermediatePropertySignatureParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>;
        void Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> property)
            where TProperty :
                IPropertyMember<TProperty, TPropertyParent>
            where TIntermediateProperty :
                TProperty,
                IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
            where TPropertyParent :
                IPropertyParent<TProperty, TPropertyParent>
            where TIntermediatePropertyParent :
                TPropertyParent,
                IIntermediatePropertyParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>;
        #endregion
        /// <summary>
        /// Visits the <paramref name="parameter"/> provided.
        /// </summary>
        /// <typeparam name="TParent">The <see cref="IParameterParent"/>
        /// type which owns the abstract definition of the current 
        /// <see cref="IIntermediateParameterMember{TParent, TIntermediateParent}"/>.</typeparam>
        /// <typeparam name="TIntermediateParent">The <see cref="IIntermediateParameterParent"/>
        /// which owns the current <see cref="IIntermediateParameterMember{TParent, TIntermediateParent}"/>.</typeparam>
        /// <param name="parameter">The <see cref="IIntermediateParameterMember{TParent, TIntermediateParent}"/> to visit.</param>
        void Visit<TParent, TIntermediateParent>(IIntermediateParameterMember<TParent, TIntermediateParent> parameter)
            where TParent :
                IParameterParent
            where TIntermediateParent :
                TParent,
                IIntermediateParameterParent;

        /// <summary>
        /// Visits the <paramref name="rangeVariable"/> provided.
        /// </summary>
        /// <param name="rangeVariable">The <see cref="ILinqRangeVariable"/>
        /// to visit.</param>
        void Visit(ILinqRangeVariable rangeVariable);
    }
    /// <summary>
    /// Defines properties and methods for visiting the various kinds of members
    /// definable in code.
    /// </summary>
    /// <typeparam name="TResult">The value returned upon visiting the expression.</typeparam>
    /// <typeparam name="TContext">The type of context passed to the visitor.</typeparam>
    public interface IIntermediateMemberVisitor<TResult, TContext>
    {
        /// <summary>
        /// Visits the <paramref name="local"/> provided.
        /// </summary>
        /// <param name="local">The <see cref="ILocalMember"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        TResult Visit(ILocalMember local, TContext context);
        #region Constructor coercion members
        /// <summary>
        /// Visits the <paramref name="ctor"/> provided.
        /// </summary>
        /// <typeparam name="TCtor">The type of <see cref="IConstructorMember{TCtor, TCtorParent}"/>
        /// in the current implementation.</typeparam>
        /// <typeparam name="TIntermediateCtor">The type of
        /// <see cref="IIntermediateConstructorSignatureMember{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// in the current implementation.</typeparam>
        /// <typeparam name="TType">The type of <see cref="ICreatableParent{TCtor, TCtorParent}"/>
        /// that contains the <typeparamref name="TCtor"/> instances.</typeparam>
        /// <typeparam name="TIntermediateType">The type of 
        /// <see cref="IIntermediateCreatableSignatureParent{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// which contains the <typeparamref name="TIntermediateCtor"/>.</typeparam>
        /// <param name="ctor">The <see cref="IIntermediateConstructorSignatureMember{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        TResult Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor, TContext context)
            where TCtor :
                IConstructorMember<TCtor, TType>
            where TIntermediateCtor :
                TCtor,
                IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
            where TType :
                ICreatableParent<TCtor, TType>
            where TIntermediateType :
                TType,
                IIntermediateCreatableSignatureParent<TCtor, TIntermediateCtor, TType, TIntermediateType>;
        /// <summary>
        /// Visits the <paramref name="ctor"/> provided.
        /// </summary>
        /// <typeparam name="TCtor">The type of the constructor in the abstract type system.</typeparam>
        /// <typeparam name="TIntermediateCtor">The type of the constructor in the intermediate type system.</typeparam>
        /// <typeparam name="TType">The type of the owning <see cref="ICreatableParent{TCtor, TIntermediateType}"/> in 
        /// the abstract type system.</typeparam>
        /// <typeparam name="TIntermediateType">The type of the owning <see cref="IIntermediateCreatableParent{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// in the intermediate abstract syntax tree.</typeparam>
        /// <param name="ctor">The <see cref="IIntermediateConstructorMember{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        TResult Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor, TContext context)
            where TCtor :
                IConstructorMember<TCtor, TType>
            where TIntermediateCtor :
                TCtor,
                IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
            where TType :
                ICreatableParent<TCtor, TType>
            where TIntermediateType :
                TType,
                IIntermediateCreatableParent<TCtor, TIntermediateCtor, TType, TIntermediateType>;
        #endregion
        #region Events members
        /// <summary>
        /// Visits the <paramref name="event"/> provided.
        /// </summary>
        /// <typeparam name="TEvent">The type of event used in the abstract type system.</typeparam>
        /// <typeparam name="TIntermediateEvent">The type of event used in the intermediate
        /// abstract syntax tree.</typeparam>
        /// <typeparam name="TEventParent">The type which contains the <typeparamref name="TEvent"/>
        /// instances in the abstract type system.</typeparam>
        /// <typeparam name="TIntermediateEventParent">The type which contains the <typeparamref name="TIntermediateEvent"/>
        /// instances in the intermediate abstract syntax tree.</typeparam>
        /// <param name="event">The <see cref="IIntermediateEventMember{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        TResult Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event, TContext context)
            where TEvent :
                IEventMember<TEvent, TEventParent>
            where TIntermediateEvent :
                TEvent,
                IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            where TEventParent :
                IEventParent<TEvent, TEventParent>
            where TIntermediateEventParent :
                TEventParent,
                IIntermediateEventParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>;
        /// <summary>
        /// Visits the <paramref name="event"/> provided.
        /// </summary>
        /// <typeparam name="TEvent">The type of event in the abstract type system.</typeparam>
        /// <typeparam name="TIntermediateEvent">The type of event in the intermediate
        /// abstract syntax tree.</typeparam>
        /// <typeparam name="TEventParent">The type which contains the <typeparamref name="TEvent"/>
        /// instances in the abstract type system.</typeparam>
        /// <typeparam name="TIntermediateEventParent">The type which contains the
        /// <typeparamref name="TIntermediateEvent"/> instances in the intermediate
        /// abstract syntax tree.</typeparam>
        /// <param name="event">The <see cref="IIntermediateEventSignatureMember{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent}"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        TResult Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event, TContext context)
            where TEvent :
                IEventSignatureMember<TEvent, TEventParent>
            where TIntermediateEvent :
                TEvent,
                IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParent>
            where TIntermediateEventParent :
                TEventParent,
                IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>;
        #endregion
        #region Expression coercion members
        /// <summary>
        /// Visits the <paramref name="binaryCoercion"/> provided.
        /// </summary>
        /// <typeparam name="TCoercionParent">
        /// The type of parent that contains the binary operation 
        /// coercion member in the current implementation.</typeparam>
        /// <param name="binaryCoercion">The <see cref="IBinaryOperatorCoercionMember{TCoercionParent}"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        TResult Visit<TCoercionParent>(IBinaryOperatorCoercionMember<TCoercionParent> binaryCoercion, TContext context)
            where TCoercionParent :
                ICoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>;
        TResult Visit<TCoercionParent>(ITypeCoercionMember<TCoercionParent> typeCoercion, TContext context)
            where TCoercionParent :
                ICoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, TCoercionParent>;
        TResult Visit<TCoercionParent>(IUnaryOperatorCoercionMember<TCoercionParent> unaryCoercion, TContext context)
            where TCoercionParent :
                ICoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>;
        #endregion
        #region Field member
        TResult Visit<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>(IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> field, TContext context)
            where TField :
                IFieldMember<TField, TFieldParent>
            where TIntermediateField :
                TField,
                IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
            where TFieldParent :
                IFieldParent<TField, TFieldParent>
            where TIntermediateFieldParent :
                TFieldParent,
                IIntermediateFieldParent<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>;
        TResult Visit(IIntermediateEnumFieldMember field, TContext context);
        #endregion
        #region Indexer members
        TResult Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexer, TContext context)
            where TIndexer :
                IIndexerMember<TIndexer, TIndexerParent>
            where TIntermediateIndexer :
                TIndexer,
                IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            where TIndexerParent :
                IIndexerParent<TIndexer, TIndexerParent>
            where TIntermediateIndexerParent :
                TIndexerParent,
                IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>;
        TResult Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexerSignature, TContext context)
            where TIndexer :
                IIndexerSignatureMember<TIndexer, TIndexerParent>
            where TIntermediateIndexer :
                TIndexer,
                IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            where TIndexerParent :
                IIndexerSignatureParent<TIndexer, TIndexerParent>
            where TIntermediateIndexerParent :
                TIndexerParent,
                IIntermediateIndexerSignatureParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>;
        #endregion
        #region Method members
        TResult Visit<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>(IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> method, TContext context)
            where TMethod :
                IMethodMember<TMethod, TMethodParent>
            where TIntermediateMethod :
                IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
                TMethod
            where TMethodParent :
                IMethodParent<TMethod, TMethodParent>
            where TIntermediateMethodParent :
                IIntermediateMethodParent<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
                TMethodParent;
        TResult Visit<TSignature, TIntermediateSignature, TParent, TIntermediateParent>(IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> methodSignature, TContext context)
            where TSignature :
                IMethodSignatureMember<TSignature, TParent>
            where TIntermediateSignature :
                TSignature,
                IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
            where TParent :
                IMethodSignatureParent<TSignature, TParent>
            where TIntermediateParent :
                TParent,
                IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TParent, TIntermediateParent>;
        #endregion
        #region Property members
        TResult Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> propertySignature, TContext context)
            where TProperty :
                IPropertySignatureMember<TProperty, TPropertyParent>
            where TIntermediateProperty :
                TProperty,
                IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
            where TPropertyParent :
                IPropertySignatureParent<TProperty, TPropertyParent>
            where TIntermediatePropertyParent :
                TPropertyParent,
                IIntermediatePropertySignatureParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>;
        TResult Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> property, TContext context)
            where TProperty :
                IPropertyMember<TProperty, TPropertyParent>
            where TIntermediateProperty :
                TProperty,
                IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
            where TPropertyParent :
                IPropertyParent<TProperty, TPropertyParent>
            where TIntermediatePropertyParent :
                TPropertyParent,
                IIntermediatePropertyParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>;
        #endregion
        /// <summary>
        /// Visits the <paramref name="parameter"/> provided.
        /// </summary>
        /// <typeparam name="TParent">The <see cref="IParameterParent"/>
        /// type which owns the abstract definition of the current 
        /// <see cref="IIntermediateParameterMember{TParent, TIntermediateParent}"/>.</typeparam>
        /// <typeparam name="TIntermediateParent">The <see cref="IIntermediateParameterParent"/>
        /// which owns the current <see cref="IIntermediateParameterMember{TParent, TIntermediateParent}"/>.</typeparam>
        /// <param name="parameter">The <see cref="IIntermediateParameterMember{TParent, TIntermediateParent}"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        TResult Visit<TParent, TIntermediateParent>(IIntermediateParameterMember<TParent, TIntermediateParent> parameter, TContext context)
            where TParent :
                IParameterParent
            where TIntermediateParent :
                TParent,
                IIntermediateParameterParent;

        /// <summary>
        /// Visits the <paramref name="rangeVariable"/> provided.
        /// </summary>
        /// <param name="rangeVariable">The <see cref="ILinqRangeVariable"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        TResult Visit(ILinqRangeVariable rangeVariable, TContext context);
    }

}
