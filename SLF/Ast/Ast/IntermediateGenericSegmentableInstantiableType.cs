using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Utilities.Properties;
using System.ComponentModel;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides a base implementation of a type which can have generic parameters,
    /// be segmented across multiple instances, and contains instance members such as
    /// constructors, events, fields, indexers, methods, properties, and type coercion members.
    /// </summary>
    /// <typeparam name="TCtor">The type of the constructor members in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCtor">The type of the constructor members in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TEvent">The type of the event members in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEvent">The type of the event members in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TIntermediateEventMethod">The type of
    /// method used within the events of the intermediate abstract 
    /// syntax tree.</typeparam>
    /// <typeparam name="TField">The type of the field members in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateField">The type of the field members in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TIndexer">The type of the indexer members in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexer">The type of the indexer members in the intermediate abstract
    /// syntax tree.</typeparam>
    /// <typeparam name="TIntermediateIndexerMethod">The type of 
    /// method used within the indexers of the intermediate abstract
    /// syntax tree.</typeparam>
    /// <typeparam name="TMethod">The type of the method members in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateMethod">The type of the method members in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TProperty">The type of the property members in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateProperty">The type of the property members in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TIntermediatePropertyMethod">The type of
    /// method used within the properties of the intermediate abstract
    /// syntax tree.</typeparam>
    /// <typeparam name="TType">The kind of type which contains all the members
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The kind of type which contains all the members
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TInstanceIntermediateType">The kind of type which implements the <typeparamref name="TIntermediateType"/>
    /// and will be instanced by the parts helper class.</typeparam>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract partial class IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> :
        IntermediateGenericSegmentableParentType<IGeneralGenericTypeUniqueIdentifier, TType, TIntermediateType, TInstanceIntermediateType>,
        IIntermediateInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, IGeneralGenericTypeUniqueIdentifier, TType, TIntermediateType>,
        IIntermediateInstantiableType
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            class, 
            IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>,
            TCtor
        where TEvent :
            IEventMember<TEvent, TType>
        where TIntermediateEvent :
            class,
            IIntermediateEventMember<TEvent, TIntermediateEvent, TType, TIntermediateType>,
            TEvent
        where TIntermediateEventMethod :
            class,
            TIntermediateMethod,
            IIntermediateEventMethodMember
        where TField :
            IFieldMember<TField, TType>,
            IInstanceMember
        where TIntermediateField :
            IIntermediateFieldMember<TField, TIntermediateField, TType, TIntermediateType>,
            TField,
            IIntermediateInstanceMember
        where TIndexer :
            IIndexerMember<TIndexer, TType>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TType, TIntermediateType>
        where TIntermediateIndexerMethod :
            class, 
            TIntermediateMethod,
            IIntermediatePropertyMethodMember
        where TMethod :
            class,
            IMethodMember<TMethod, TType>,
            IExtendedInstanceMember
        where TIntermediateMethod :
            class,
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TType, TIntermediateType>,
            TMethod
        where TProperty :
            IPropertyMember<TProperty, TType>
        where TIntermediateProperty :
            IIntermediatePropertyMember<TProperty, TIntermediateProperty, TType, TIntermediateType>,
            TProperty
        where TIntermediatePropertyMethod :
            class,
            TIntermediateMethod,
            IIntermediatePropertyMethodMember
        where TType :
            class,
            IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, IGeneralGenericTypeUniqueIdentifier, TType>,
            IGenericType<IGeneralGenericTypeUniqueIdentifier, TType>
        where TIntermediateType :
            class,
            IIntermediateInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, IGeneralGenericTypeUniqueIdentifier, TType, TIntermediateType>,
            IIntermediateGenericType<IGeneralGenericTypeUniqueIdentifier, TType, TIntermediateType>,
            IIntermediateSegmentableType<IGeneralGenericTypeUniqueIdentifier, TType, TIntermediateType>,
            TType
        where TInstanceIntermediateType :
            IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType>,
            TIntermediateType
    {
        #region IntermediateGenericSegmentableInstantiableType Data members

        #region Member Data Members

        /// <summary>
        /// Data member for <see cref="ImplementedInterfaces"/>
        /// </summary>
        private ImplementedInterfacesDictionary<TEvent, TIntermediateEvent, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType> implementedInterfaces;

        /// <summary>
        /// Data member for holding the full member dictionary in verbatim order.
        /// </summary>
        private IntermediateFullMemberDictionary _members;
        /// <summary>
        /// Data member for the binary operator coercion members.
        /// </summary>
        private BinaryOperatorDictionary binaryOperatorCoercions;
        /// <summary>
        /// Data member for the instance constructors for the type.
        /// </summary>
        private ConstructorDictionary constructors;
        /// <summary>
        /// Data member for the events for the type.
        /// </summary>
        private EventDictionary events;
        /// <summary>
        /// Data member for the fields defined in the type.
        /// </summary>
        private IntermediateFieldMemberDictionary<TField, TIntermediateField, TType, TIntermediateType> fields;
        /// <summary>
        /// Data member for the indexers defined in the type.
        /// </summary>
        private IntermediateIndexerMemberDictionary<TIndexer, TIntermediateIndexer, TType, TIntermediateType> indexers;
        /// <summary>
        /// Data member for the methods defined within the type.
        /// </summary>
        private MethodDictionary methods;
        /// <summary>
        /// Data member for the properties defined within the type.
        /// </summary>
        private IntermediatePropertyMemberDictionary<TProperty, TIntermediateProperty, TType, TIntermediateType> properties;
        /// <summary>
        /// Data member for the type coercions defined within the type.
        /// </summary>
        private TypeCoercionDictionary typeCoercions;
        /// <summary>
        /// Data member for the unary operator coercions defined within the type.
        /// </summary>
        private UnaryOperatorDictionary unaryOperatorCoercions;

        /// <summary>
        /// The <see cref="ConstructorMember"/> which represents the type-initializer or static
        /// constructor.
        /// </summary>
        private ConstructorMember typeInitializer;
        #endregion

        private int suspendLevel = 0;

        /// <summary>
        /// Data member for <see cref="TypeBase{TIdentifier}.UniqueIdentifier"/> via
        /// <see cref="OnGetUniqueIdentifier()"/>.
        /// </summary>
        private IGeneralGenericTypeUniqueIdentifier uniqueIdentifier;

        #endregion

        /// <summary>
        /// Creates a new <see cref="IntermediateGenericSegmentableInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// instance with the <paramref name="root"/> and <paramref name="parent"/> 
        /// provided.
        /// </summary>
        /// <param name="root">The root <typeparamref name="TInstanceIntermediateType"/>
        /// which acts as the central member for the type.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/> which contains the 
        /// <see cref="IntermediateGenericSegmentableInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType}"/>.</param>
        protected IntermediateGenericSegmentableInstantiableType(TInstanceIntermediateType root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }
        protected IntermediateGenericSegmentableInstantiableType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
        }

        protected IntermediateGenericSegmentableInstantiableType(IIntermediateTypeParent parent)
            : base(parent)
        {

        }

        public bool HasTypeInitializer { get { return this.typeInitializer != null; } }

        #region IIntermediateCreatableParent<TCtor,TIntermediateCtor,TType,TIntermediateType> Members

        /// <summary>
        /// Returns the constructors contained by the 
        /// <see cref="IntermediateGenericSegmentableInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType}"/>.
        /// </summary>
        public IIntermediateConstructorMemberDictionary<TCtor, TIntermediateCtor, TType, TIntermediateType> Constructors
        {
            get
            {
                this.CheckConstructors();
                lock (this.SyncObject)
                    return this.constructors;
            }
        }

        /// <summary>
        /// Returns the <typeparamref name="TIntermediateCtor"/> which 
        /// represents the type-initializer (static constructor)
        /// for the type.
        /// </summary>
        public TIntermediateCtor TypeInitializer
        {
            get
            {
                if (this.typeInitializer == null)
                {
                    this.typeInitializer = this.GetTypeInitializer();
                    if (typeInitializer != null)
                        ((ConstructorDictionary)this.Constructors).AddDeclaration((TIntermediateCtor)(object)typeInitializer);
                }
                return (TIntermediateCtor)(object)this.typeInitializer;
            }
        }


        #endregion

        #region IIntermediateCreatableSignatureParent<TCtor,TIntermediateCtor,TType,TIntermediateType> Members

        IIntermediateConstructorSignatureMemberDictionary<TCtor, TIntermediateCtor, TType, TIntermediateType> IIntermediateCreatableSignatureParent<TCtor, TIntermediateCtor, TType, TIntermediateType>.Constructors
        {
            get
            {
                return this.Constructors;
            }
        }

        #endregion

        #region IIntermediateCreatableSignatureParent Members

        IIntermediateConstructorSignatureMemberDictionary IIntermediateCreatableSignatureParent.Constructors
        {
            get { return (IIntermediateConstructorSignatureMemberDictionary)this.Constructors; }
        }

        #endregion
        #region IIntermediateCreatableParent Members

        IIntermediateConstructorMemberDictionary IIntermediateCreatableParent.Constructors
        {
            get { return (IIntermediateConstructorMemberDictionary)this.Constructors; }
        }

        IIntermediateConstructorMember IIntermediateCreatableParent.TypeInitializer
        {
            get
            {
                return this.TypeInitializer;
            }
        }

        #endregion

        #region ICreatableParent Members

        IConstructorMemberDictionary ICreatableParent.Constructors
        {
            get { return (IConstructorMemberDictionary)this.Constructors; }
        }

        IConstructorMember ICreatableParent.TypeInitializer
        {
            get { return this.TypeInitializer; }
        }

        #endregion

        #region ICreatableParent<TCtor,TType> Members

        IConstructorMemberDictionary<TCtor, TType> ICreatableParent<TCtor, TType>.Constructors
        {
            get
            {
                return this.Constructors;
            }
        }

        TCtor ICreatableParent<TCtor, TType>.TypeInitializer
        {
            get { return this.TypeInitializer; }
        }

        #endregion

        #region IIntermediateMethodParent<TMethod,TIntermediateMethod,TType,TIntermediateType> Members

        public IIntermediateMethodMemberDictionary<TMethod, TIntermediateMethod, TType, TIntermediateType> Methods
        {
            get
            {
                this.CheckMethods();
                lock (this.SyncObject)
                    return this.methods;
            }
        }

        #endregion

        #region IMethodParent<TMethod,TType> Members

        IMethodMemberDictionary<TMethod, TType> IMethodParent<TMethod, TType>.Methods
        {
            get { return this.Methods; }
        }

        #endregion

        #region IMethodParent Members

        IMethodMemberDictionary IMethodParent.Methods
        {
            get { return (IMethodMemberDictionary)this.Methods; }
        }

        #endregion

        #region IIntermediatePropertyParent<TProperty,TIntermediateProperty,TType,TIntermediateType> Members

        public IIntermediatePropertyMemberDictionary<TProperty, TIntermediateProperty, TType, TIntermediateType> Properties
        {
            get
            {
                this.CheckProperties();
                lock (this.SyncObject)
                    return this.properties;
            }
        }

        #endregion

        #region IPropertyParent<TProperty,TType> Members

        IPropertyMemberDictionary<TProperty, TType> IPropertyParent<TProperty, TType>.Properties
        {
            get { return this.Properties; }
        }

        #endregion

        #region IPropertyParent Members

        IPropertyMemberDictionary IPropertyParent.Properties
        {
            get { return (IPropertyMemberDictionary)this.Properties; }
        }

        #endregion

        #region IIntermediatePropertyParent Members

        IIntermediatePropertyMemberDictionary IIntermediatePropertyParent.Properties
        {
            get { return ((IIntermediatePropertyMemberDictionary)(this.Properties)); }
        }

        #endregion

        #region IIntermediateFieldParent<TField,TIntermediateField,TType,TIntermediateType> Members

        public IIntermediateFieldMemberDictionary<TField, TIntermediateField, TType, TIntermediateType> Fields
        {
            get
            {
                this.CheckFields();
                lock (this.SyncObject)
                    return this.fields;
            }
        }

        #endregion

        #region IFieldParent<TField,TType> Members

        IFieldMemberDictionary<TField, TType> IFieldParent<TField, TType>.Fields
        {
            get { return this.Fields; }
        }

        #endregion

        #region IFieldParent Members

        IFieldMemberDictionary IFieldParent.Fields
        {
            get { return ((IFieldMemberDictionary)(this.Fields)); }
        }

        #endregion

        #region IIntermediateFieldParent Members

        IIntermediateFieldMemberDictionary IIntermediateFieldParent.Fields
        {
            get { return (IIntermediateFieldMemberDictionary)this.Fields; }
        }

        #endregion

        #region IIntermediateCoercibleType<TType,TIntermediateType> Members

        public IIntermediateBinaryOperatorCoercionMemberDictionary<TType, TIntermediateType> BinaryOperatorCoercions
        {
            get
            {
                this.CheckBinaryOperatorCoercions();
                lock (this.SyncObject)
                    return this.binaryOperatorCoercions;
            }
        }

        public IIntermediateTypeCoercionMemberDictionary<TType, TIntermediateType> TypeCoercions
        {
            get
            {
                this.CheckTypeCoercions();
                lock (this.SyncObject)
                    return this.typeCoercions;
            }
        }

        public IIntermediateUnaryOperatorCoercionMemberDictionary<TType, TIntermediateType> UnaryOperatorCoercions
        {
            get
            {
                this.CheckUnaryOperatorCoercions();
                lock (this.SyncObject)
                    return this.unaryOperatorCoercions;
            }
        }

        #endregion

        #region ICoercibleType Members

        IBinaryOperatorCoercionMemberDictionary ICoercibleType.BinaryOperatorCoercions
        {
            get { return (IBinaryOperatorCoercionMemberDictionary)this.BinaryOperatorCoercions; }
        }

        ITypeCoercionMemberDictionary ICoercibleType.TypeCoercions
        {
            get { return (ITypeCoercionMemberDictionary)this.TypeCoercions; }
        }

        IUnaryOperatorCoercionMemberDictionary ICoercibleType.UnaryOperatorCoercions
        {
            get { return (IUnaryOperatorCoercionMemberDictionary)this.UnaryOperatorCoercions; }
        }

        #endregion

        #region IIntermediateCoercibleType Members

        IIntermediateBinaryOperatorCoercionMemberDictionary IIntermediateCoercibleType.BinaryOperatorCoercions
        {
            get { return (IIntermediateBinaryOperatorCoercionMemberDictionary)this.BinaryOperatorCoercions; }
        }

        IIntermediateTypeCoercionMemberDictionary IIntermediateCoercibleType.TypeCoercions
        {
            get { return (IIntermediateTypeCoercionMemberDictionary)this.TypeCoercions; }
        }

        IIntermediateUnaryOperatorCoercionMemberDictionary IIntermediateCoercibleType.UnaryOperatorCoercions
        {
            get { return (IIntermediateUnaryOperatorCoercionMemberDictionary)this.UnaryOperatorCoercions; }
        }

        #endregion

        #region ICoercibleType<TType> Members

        IBinaryOperatorCoercionMemberDictionary<TType> ICoercibleType<TType>.BinaryOperatorCoercions
        {
            get { return this.BinaryOperatorCoercions; }
        }

        ITypeCoercionMemberDictionary<TType> ICoercibleType<TType>.TypeCoercions
        {
            get { return this.TypeCoercions; }
        }

        IUnaryOperatorCoercionMemberDictionary<TType> ICoercibleType<TType>.UnaryOperatorCoercions
        {
            get { return this.unaryOperatorCoercions; }
        }

        #endregion

        #region IIntermediateEventParent<TEvent,TIntermediateEvent,TType,TIntermediateType> Members

        public IIntermediateEventMemberDictionary<TEvent, TIntermediateEvent, TType, TIntermediateType> Events
        {
            get
            {
                this.CheckEvents();
                lock (this.SyncObject)
                    return this.events;
            }
        }

        #endregion

        #region IIntermediateEventSignatureParent<TEvent,TIntermediateEvent,IEventParameterMember<TEvent,TType>,IIntermediateEventParameterMember<TEvent,TIntermediateEvent,TType,TIntermediateType>,TType,TIntermediateType> Members

        IIntermediateEventSignatureMemberDictionary<TEvent, TIntermediateEvent, IEventParameterMember<TEvent, TType>, IIntermediateEventParameterMember<TEvent, TIntermediateEvent, TType, TIntermediateType>, TType, TIntermediateType> IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, IEventParameterMember<TEvent, TType>, IIntermediateEventParameterMember<TEvent, TIntermediateEvent, TType, TIntermediateType>, TType, TIntermediateType>.Events
        {
            get { return this.Events; }
        }

        #endregion

        #region IEventSignatureParent<TEvent,IEventParameterMember<TEvent,TType>,TType> Members

        IEventSignatureMemberDictionary<TEvent, IEventParameterMember<TEvent, TType>, TType> IEventSignatureParent<TEvent, IEventParameterMember<TEvent, TType>, TType>.Events
        {
            get { return this.Events; }
        }

        #endregion

        #region IEventSignatureParent Members

        IEventSignatureMemberDictionary IEventSignatureParent.Events
        {
            get { return (IEventSignatureMemberDictionary)this.Events; }
        }

        #endregion

        #region IIntermediateEventSignatureParent Members

        IIntermediateEventSignatureMemberDictionary IIntermediateEventSignatureParent.Events
        {
            get { return (IIntermediateEventSignatureMemberDictionary)this.Events; }
        }

        #endregion

        #region IEventParent<TEvent,TType> Members

        IEventMemberDictionary<TEvent, TType> IEventParent<TEvent, TType>.Events
        {
            get { return this.Events; }
        }

        #endregion

        #region IIntermediateEventParent Members

        IIntermediateEventMemberDictionary IIntermediateEventParent.Events
        {
            get { return (IIntermediateEventMemberDictionary)this.Events; }
        }

        #endregion

        #region IIntermediateIndexerParent<TIndexer,TIntermediateIndexer,TType,TIntermediateType> Members

        public IIntermediateIndexerMemberDictionary<TIndexer, TIntermediateIndexer, TType, TIntermediateType> Indexers
        {
            get
            {
                this.CheckIndexers();
                lock (this.SyncObject)
                    return this.indexers;
            }
        }

        #endregion

        #region IIndexerParent<TIndexer,TType> Members

        IIndexerMemberDictionary<TIndexer, TType> IIndexerParent<TIndexer, TType>.Indexers
        {
            get { return this.Indexers; }
        }

        #endregion

        #region IIndexerParent Members

        IIndexerMemberDictionary IIndexerParent.Indexers
        {
            get { return (IIndexerMemberDictionary)this.Indexers; }
        }

        #endregion

        #region IIntermediateIndexerParent Members

        IIntermediateIndexerMemberDictionary IIntermediateIndexerParent.Indexers
        {
            get { return (IIntermediateIndexerMemberDictionary)this.Indexers; }
        }

        #endregion

        #region IInstantiableType<TCtor,TEvent,TField,TIndexer,TMethod,TProperty,TType> Members

        public IInterfaceMemberMapping<TMethod, TProperty, TEvent, TIndexer, TType> GetInterfaceMap(IInterfaceType type)
        {

            throw new NotImplementedException();
        }

        #endregion

        #region IIntermediateMethodParent Members

        IIntermediateMethodMemberDictionary IIntermediateMethodParent.Methods
        {
            get { return (IIntermediateMethodMemberDictionary)this.Methods; }
        }

        #endregion

        protected IntermediateFullMemberDictionary _Members
        {
            get
            {

                lock (this.SyncObject)
                {
                    if (this._members == null)
                    {
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                        if (this.IsRoot)
                            this._members = new IntermediateFullMemberDictionary();
                        else
                            this._members = new IntermediateFullMemberDictionary((IntermediateFullMemberDictionary)((TInstanceIntermediateType)this.GetRoot())._Members);
                    }
                    return this._members;
                }
            }
        }

        protected override IIntermediateFullMemberDictionary OnGetIntermediateMembers()
        {
            /* *
             * Ensure that each member subordinate is present as a subordinate
             * in the master member dictionary.
             * */
            this.CheckBinaryOperatorCoercions();
            this.CheckConstructors();
            this.CheckEvents();
            this.CheckFields();
            this.CheckIndexers();
            this.CheckMethods();
            this.CheckProperties();
            this.CheckTypeCoercions();
            this.CheckUnaryOperatorCoercions();
            return this._Members;
        }

        /// <summary>
        /// Obtains a new <see cref="PropertyMember"/> with the <paramref name="nameAndType"/>
        /// provided.
        /// </summary>
        /// <param name="nameAndType">The <see cref="TypedName"/> which determines
        /// the name of the property and its associated type.</param>
        /// <returns>A new <see cref="PropertyMember"/> which corresponds to the 
        /// <paramref name="nameAndType"/>.</returns>
        protected abstract PropertyMember GetNewProperty(TypedName nameAndType);


        /// <summary>
        /// Obtains a new <see cref="ConstructorMember"/> to act as the <see cref="TypeInitializer"/>
        /// for the type.
        /// </summary>
        /// <returns>A <see cref="ConstructorMember"/> which is noted to be
        /// the static constructor.</returns>
        /// <remarks>The <see cref="ConstructorMember"/> returned must inherit
        /// <typeparamref name="TIntermediateCtor"/>.</remarks>
        protected abstract ConstructorMember GetTypeInitializer();

        /// <summary>
        /// Obtains a new <see cref="ConstructorMember"/> with no parameters.
        /// </summary>
        /// <returns>A new <see cref="ConstructorMember"/>, if successful.</returns>
        /// <remarks>Required by design, due to further inheritance on constructors being necessary.</remarks>
        /// <remarks>The <see cref="ConstructorMember"/> returned must inherit
        /// <typeparamref name="TIntermediateCtor"/>.</remarks>
        protected abstract ConstructorMember GetNewConstructor();
        /// <summary>
        /// Obtains a new <see cref="EventMember"/> which designates the 
        /// name and delegate type of the new event to add.
        /// </summary>
        /// <param name="nameAndDelegateType">The <see cref="String"/> name of the
        /// event and the <see cref="IType"/> related to its delegate type.</param>
        /// <returns>A new <see cref="EventMember"/>.</returns>
        protected abstract EventMember GetNewEvent(TypedName nameAndDelegateType);
        /// <summary>
        /// Obtains a new <see cref="MethodMember"/> which designates the name
        /// of the method member to add.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the method to obtain.</param>
        /// <returns>A new <see cref="MethodMember"/> instance that derives from 
        /// <typeparamref name="TIntermediateMethod"/>.</returns>
        protected abstract MethodMember GetNewMethod(string name);
        /// <summary>
        /// Obtains a new <see cref="IndexerMember"/> which designates the 
        /// <paramref name="nameAndReturn"/> of the indexer.
        /// </summary>
        /// <param name="nameAndReturn">The <see cref="TypedName"/>
        /// representing the indexer's name and its return type.</param>
        protected abstract IndexerMember GetNewIndexer(TypedName nameAndReturn);
        /// <summary>
        /// Obtains a new <see cref="EventMember"/> which designates
        /// the <paramref name="name"/> and <paramref name="eventSignature"/> of
        /// the event to add.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the event.</param>
        /// <param name="eventSignature">The series of names and types which relate to the
        /// auto-generated delegate that's associated to the event.</param>
        /// <returns>A new <see cref="EventMember"/>.</returns>
        protected abstract EventMember GetNewEvent(string name, TypedNameSeries eventSignature);

        /// <summary>
        /// Obtains a new <see cref="FieldMember"/> with the 
        /// <paramref name="nameAndType"/> of the member to create.
        /// </summary>
        /// <param name="nameAndType">The <see cref="TypedName"/>
        /// which provides the <see cref="String"/>
        /// name and the <see cref="IType"/> field type.</param>
        /// <returns>A new <see cref="FieldMember"/> instance.</returns>
        protected abstract FieldMember GetNewField(TypedName nameAndType);
        #region Member Check Methods


        private void CheckImplementedInterfaces()
        {
            if (this.implementedInterfaces == null)
                this.implementedInterfaces = InitializeImplementedInterfaces();
        }

        private static void SuspendCheck<TMemberParent, TIntermediateMemberParent, TMemberIdentifier, TMember, TIntermediateMember>(IntermediateGroupedMemberDictionary<TMemberParent, TIntermediateMemberParent, TMemberIdentifier, TMember, TIntermediateMember> target, int suspendLevel)
            where TMemberParent :
                IMemberParent
            where TIntermediateMemberParent :
                class,
                IIntermediateMemberParent,
                TMemberParent
            where TMemberIdentifier :
                IMemberUniqueIdentifier,
                IGeneralMemberUniqueIdentifier
            where TMember :
                IMember<TMemberIdentifier, TMemberParent>
            where TIntermediateMember :
                IIntermediateMember<TMemberIdentifier, TMemberParent, TIntermediateMemberParent>,
                TMember
        {
            if (suspendLevel <= 0)
                return;
            if (target == null)
                throw new ArgumentNullException("target");
            for (int i = 0; i < suspendLevel; i++)
                target.Suspend();
        }

        private void CheckBinaryOperatorCoercions()
        {
            lock (this.SyncObject)
                if (this.binaryOperatorCoercions == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.binaryOperatorCoercions = InitializeBinaryOperatorCoercions();
                    SuspendCheck(binaryOperatorCoercions, suspendLevel);
                }
        }

        private void CheckConstructors()
        {
            lock (this.SyncObject)
                if (this.constructors == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.constructors = this.InitializeConstructors();
                    SuspendCheck(constructors, suspendLevel);
                }
        }

        private void CheckEvents()
        {
            lock (this.SyncObject)
                if (this.events == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.events = this.InitializeEvents();
                    SuspendCheck(events, suspendLevel);
                }
        }

        private void CheckFields()
        {
            lock (this.SyncObject)
                if (this.fields == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.fields = this.InitializeFields();
                    SuspendCheck(fields, suspendLevel);
                }
        }

        private void CheckIndexers()
        {
            lock (this.SyncObject)
                if (this.indexers == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.indexers = this.InitializeIndexers();
                    SuspendCheck(indexers, suspendLevel);
                }
        }

        private void CheckMethods()
        {
            lock (this.SyncObject)
                if (this.methods == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.methods = this.InitializeMethods();
                    SuspendCheck(methods, suspendLevel);
                }
        }

        private void CheckProperties()
        {
            lock (this.SyncObject)
                if (this.properties == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.properties = this.InitializeProperties();
                    SuspendCheck(properties, suspendLevel);
                }
        }

        private void CheckTypeCoercions()
        {
            lock (this.SyncObject)
                if (this.typeCoercions == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.typeCoercions = this.InitializeTypeCoercions();
                    SuspendCheck(typeCoercions, suspendLevel);
                }
        }

        private void CheckUnaryOperatorCoercions()
        {
            lock (this.SyncObject)
                if (this.unaryOperatorCoercions == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.unaryOperatorCoercions = this.InitializeUnaryOperatorCoercions();
                    SuspendCheck(unaryOperatorCoercions, suspendLevel);
                }
        }

        #endregion

        #region Initializers

        protected abstract ImplementedInterfacesDictionary<TEvent, TIntermediateEvent, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType> InitializeImplementedInterfaces();

        protected virtual BinaryOperatorDictionary InitializeBinaryOperatorCoercions()
        {
            BinaryOperatorDictionary result;
            if (this.IsRoot)
                result = new BinaryOperatorDictionary(this._Members, this);
            else
                result = new BinaryOperatorDictionary(this._Members, this, (BinaryOperatorDictionary)this.GetRoot().BinaryOperatorCoercions);
            if (this.IsLocked)
                result.Lock();
            return result;
        }

        protected virtual ConstructorDictionary InitializeConstructors()
        {
            ConstructorDictionary result;
            if (this.IsRoot)
                result = new ConstructorDictionary(this._Members, this);
            else
                result = new ConstructorDictionary(this._Members, this, (ConstructorDictionary)this.GetRoot().Constructors);
            if (this.IsLocked)
                result.Lock();
            return result;
        }

        protected virtual EventDictionary InitializeEvents()
        {
            EventDictionary result;
            if (this.IsRoot)
                result = new EventDictionary(this._Members, this);
            else
                result = new EventDictionary(this._Members, this, (EventDictionary)this.GetRoot().Events);
            if (this.IsLocked)
                result.Lock();
            return result;
        }

        protected virtual IntermediateFieldMemberDictionary<TField, TIntermediateField, TType, TIntermediateType> InitializeFields()
        {
            FieldDictionary result;
            if (this.IsRoot)
                result = new FieldDictionary(this._Members, (TInstanceIntermediateType)this);
            else
                result = new FieldDictionary(this._Members, (TInstanceIntermediateType)this, (FieldDictionary)this.GetRoot().Fields);
            if (this.IsLocked)
                result.Lock();
            return result;
        }

        protected virtual IndexerDictionary InitializeIndexers()
        {
            IndexerDictionary result;
            if (this.IsRoot)
                result = new IndexerDictionary(this._Members, (TInstanceIntermediateType)this);
            else
                result = new IndexerDictionary(this._Members, (TInstanceIntermediateType)this, (IndexerDictionary)(this.GetRoot().Indexers));
            if (this.IsLocked)
                result.Lock();
            return result;
        }

        protected virtual MethodDictionary InitializeMethods()
        {
            MethodDictionary result;
            if (this.IsRoot)
                result = new MethodDictionary(this._Members, (TInstanceIntermediateType)this, this.IdentityManager);
            else
                result = new MethodDictionary(this._Members, (TInstanceIntermediateType)this, (MethodDictionary)this.GetRoot().Methods);
            if (this.IsLocked)
                result.Lock();
            return result;
        }

        protected virtual PropertyDictionary InitializeProperties()
        {
            PropertyDictionary result;
            if (this.IsRoot)
                result = new PropertyDictionary(this._Members, (TInstanceIntermediateType)this);
            else
                result = new PropertyDictionary(this._Members, (TInstanceIntermediateType)this, (PropertyDictionary)this.GetRoot().Properties);
            if (this.IsLocked)
                result.Lock();
            return result;
        }

        protected virtual TypeCoercionDictionary InitializeTypeCoercions()
        {
            TypeCoercionDictionary result;
            if (this.IsRoot)
                result = new TypeCoercionDictionary(this._Members, this);
            else
                result = new TypeCoercionDictionary(this._Members, this, (TypeCoercionDictionary)this.GetRoot().TypeCoercions);
            if (this.IsLocked)
                result.Lock();
            return result;
        }

        protected virtual UnaryOperatorDictionary InitializeUnaryOperatorCoercions()
        {
            UnaryOperatorDictionary result;
            if (this.IsRoot)
                result = new UnaryOperatorDictionary(this._Members, this);
            else
                result = new UnaryOperatorDictionary(this._Members, this, (UnaryOperatorDictionary)this.GetRoot().UnaryOperatorCoercions);
            if (this.IsLocked)
                result.Lock();
            return result;
        }

        #endregion

        #region IIntermediateInstantiableType<TCtor,TIntermediateCtor,TEvent,TIntermediateEvent,TField,TIntermediateField,TIndexer,TIntermediateIndexer,TMethod,TIntermediateMethod,TProperty,TIntermediateProperty,TType,TIntermediateType> Members

        public new ImplementedInterfacesDictionary<TEvent, TIntermediateEvent, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType> ImplementedInterfaces
        {
            get
            {
                CheckImplementedInterfaces();
                return this.implementedInterfaces;
            }
        }

        IIntermediateInstantiableTypeImplementedInterfaces<TEvent, TIntermediateEvent, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType> IIntermediateInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, IGeneralGenericTypeUniqueIdentifier, TType, TIntermediateType>.ImplementedInterfaces
        {
            get
            {
                return this.ImplementedInterfaces;
            }
        }

        #endregion

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            return this.ImplementedInterfaces.GetLocked();
        }

        protected override ILockedTypeCollection OnGetDirectImplementedInterfaces()
        {
            return this.ImplementedInterfaces.GetLocked(true);
        }

        #region IIntermediateInstantiableType Members
        private IBoundSpecialReferenceExpression thisReference;

        public IBoundSpecialReferenceExpression GetThis()
        {
            if (this.thisReference == null)
                this.thisReference = new BoundSpecialReferenceExpression(this, SpecialReferenceKind.This);
            return this.thisReference;
        }

        public void SuspendDualLayout()
        {
            this.suspendLevel++;
            base.SuspendTypeContainers();
            if (this.binaryOperatorCoercions != null)
                this.binaryOperatorCoercions.Suspend();
            if (this.constructors != null)
                this.constructors.Suspend();
            if (this.events != null)
                this.events.Suspend();
            if (this.fields != null)
                this.fields.Suspend();
            if (this.indexers != null)
                this.indexers.Suspend();
            if (this.methods != null)
                this.methods.Suspend();
            if (this.properties != null)
                this.properties.Suspend();
            if (this.typeCoercions != null)
                this.typeCoercions.Suspend();
            if (this.unaryOperatorCoercions != null)
                this.unaryOperatorCoercions.Suspend();
        }

        public void ResumeDualLayout()
        {
            if (this.suspendLevel == 0)
                return;
            this.suspendLevel--;
            base.ResumeTypeContainers();
            if (this.binaryOperatorCoercions != null)
                this.binaryOperatorCoercions.Resume();
            if (this.constructors != null)
                this.constructors.Resume();
            if (this.events != null)
                this.events.Resume();
            if (this.fields != null)
                this.fields.Resume();
            if (this.indexers != null)
                this.indexers.Resume();
            if (this.methods != null)
                this.methods.Resume();
            if (this.properties != null)
                this.properties.Resume();
            if (this.typeCoercions != null)
                this.typeCoercions.Resume();
            if (this.unaryOperatorCoercions != null)
                this.unaryOperatorCoercions.Resume();
        }

        public bool Suspended
        {
            get { return this.suspendLevel > 0; }
        }

        #endregion
        /// <summary>
        /// Frees the managed resources used by the
        /// <see cref="IntermediateGenericSegmentableInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType}"/>.
        /// </summary>
        public override void Dispose()
        {
            try
            {
                lock (this.SyncObject)
                {
                    if (this.thisReference != null)
                    {
                        this.thisReference.Dispose();
                        this.thisReference = null;
                    }
                    if (this.binaryOperatorCoercions != null)
                    {
                        this.binaryOperatorCoercions.Dispose();
                        this.binaryOperatorCoercions = null;
                    }
                    if (this.constructors != null)
                    {
                        this.constructors.Dispose();
                        this.constructors = null;
                    }
                    if (this.events != null)
                    {
                        this.events.Dispose();
                        this.events = null;
                    }
                    if (this.fields != null)
                    {
                        this.fields.Dispose();
                        this.fields = null;
                    }
                    if (this.indexers != null)
                    {
                        this.indexers.Dispose();
                        this.indexers = null;
                    }
                    if (this.methods != null)
                    {
                        this.methods.Dispose();
                        this.methods = null;
                    }
                    if (this.properties != null)
                    {
                        this.properties.Dispose();
                        this.properties = null;
                    }
                    if (this.typeCoercions != null)
                    {
                        this.typeCoercions.Dispose();
                        this.typeCoercions = null;
                    }
                    if (this.unaryOperatorCoercions != null)
                    {
                        this.unaryOperatorCoercions.Dispose();
                        this.unaryOperatorCoercions = null;
                    }
                    if (this._members != null)
                    {
                        if (this.IsRoot)
                            this._members.Dispose();
                        else
                            this._members.ConditionalRemove(this);
                        this._members = null;
                    }
                }
            }
            finally
            {
                base.Dispose();
            }
        }

        /* *
         * Locking is different than suspending the structure
         * of an intermediate type, used to set the structure
         * of a mutable model as immutable.
         * */
        internal override void OnLocked()
        {
            try
            {
                lock (this.SyncObject)
                {
                    if (this.binaryOperatorCoercions != null)
                        this.binaryOperatorCoercions.Lock();
                    if (this.constructors != null)
                        this.constructors.Lock();
                    if (this.events != null)
                        this.events.Lock();
                    if (this.fields != null)
                        this.fields.Lock();
                    if (this.indexers != null)
                        this.indexers.Lock();
                    if (this.methods != null)
                        this.methods.Lock();
                    if (this.properties != null)
                        this.properties.Lock();
                    if (this.typeCoercions != null)
                        this.typeCoercions.Lock();
                    if (this.unaryOperatorCoercions != null)
                        this.unaryOperatorCoercions.Lock();
                }
            }
            finally
            {
                base.OnLocked();
            }
        }

        internal override void OnUnlocked()
        {
            try
            {
                lock (this.SyncObject)
                {
                    if (this.binaryOperatorCoercions != null)
                        this.binaryOperatorCoercions.Unlock();
                    if (this.constructors != null)
                        this.constructors.Unlock();
                    if (this.events != null)
                        this.events.Unlock();
                    if (this.fields != null)
                        this.fields.Unlock();
                    if (this.indexers != null)
                        this.indexers.Unlock();
                    if (this.methods != null)
                        this.methods.Unlock();
                    if (this.properties != null)
                        this.properties.Unlock();
                    if (this.typeCoercions != null)
                        this.typeCoercions.Unlock();
                    if (this.unaryOperatorCoercions != null)
                        this.unaryOperatorCoercions.Unlock();
                }
            }
            finally
            {
                base.OnUnlocked();
            }
        }

        protected override IGeneralGenericTypeUniqueIdentifier OnGetUniqueIdentifier()
        {
            lock (this.SyncObject)
                if (this.uniqueIdentifier == null)
                    if (this.Parent is IType)
                    {
                        if (this.TypeParametersInitialized)
                            this.uniqueIdentifier = ((IType)this.Parent).UniqueIdentifier.GetNestedIdentifier(this.Name, this.TypeParameters.Count);
                        else
                            this.uniqueIdentifier = ((IType)this.Parent).UniqueIdentifier.GetNestedIdentifier(this.Name, 0);
                    }
                    else if (this.Parent is INamespaceDeclaration)
                    {
                        if (this.TypeParametersInitialized)
                            this.uniqueIdentifier = this.Assembly.UniqueIdentifier.GetTypeIdentifier(((INamespaceDeclaration)this.Parent).FullName, this.Name, this.TypeParameters.Count);
                        else
                            this.uniqueIdentifier = this.Assembly.UniqueIdentifier.GetTypeIdentifier(((INamespaceDeclaration)this.Parent).FullName, this.Name, 0);
                    }
                    else if (this.TypeParametersInitialized)
                        this.uniqueIdentifier = this.Assembly.UniqueIdentifier.GetTypeIdentifier((IGeneralDeclarationUniqueIdentifier)null, this.Name, this.TypeParameters.Count);
                    else
                        this.uniqueIdentifier = this.Assembly.UniqueIdentifier.GetTypeIdentifier((IGeneralDeclarationUniqueIdentifier)null, this.Name, 0);
            return this.uniqueIdentifier;
        }

        /// <summary>
        /// Returns whether the binary operator coercions of the 
        /// <see cref="IntermediateGenericSegmentableInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// have been initialized.
        /// </summary>
        protected bool AreBinaryOperatorCoercionsInitialized
        {
            get
            {
                lock (this.SyncObject)
                    return this.binaryOperatorCoercions != null;
            }
        }

        /// <summary>
        /// Returns whether the constructors of the 
        /// <see cref="IntermediateGenericSegmentableInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// have been initialized.
        /// </summary>
        protected bool AreConstructorsInitialized
        {
            get
            {
                lock (this.SyncObject)
                    return this.constructors != null;
            }
        }

        /// <summary>
        /// Returns whether the events of the 
        /// <see cref="IntermediateGenericSegmentableInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// have been initialized.
        /// </summary>
        protected bool AreEventsInitialized
        {
            get
            {
                lock (this.SyncObject)
                    return this.events != null;
            }
        }

        /// <summary>
        /// Returns whether the fields of the 
        /// <see cref="IntermediateGenericSegmentableInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// have been initialized.
        /// </summary>
        protected bool AreFieldsInitialized
        {
            get
            {
                lock (this.SyncObject)
                    return this.fields != null;
            }
        }

        /// <summary>
        /// Returns whether the indexers of the 
        /// <see cref="IntermediateGenericSegmentableInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// have been initialized.
        /// </summary>
        protected bool AreIndexersInitialized
        {
            get
            {
                lock (this.SyncObject)
                    return this.indexers != null;
            }
        }

        /// <summary>
        /// Returns whether the methods of the 
        /// <see cref="IntermediateGenericSegmentableInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// have been initialized.
        /// </summary>
        protected bool AreMethodsInitialized
        {
            get
            {
                lock (this.SyncObject)
                    return this.methods != null;
            }
        }

        /// <summary>
        /// Returns whether the properties of the 
        /// <see cref="IntermediateGenericSegmentableInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// have been initialized.
        /// </summary>
        protected bool ArePropertiesInitialized
        {
            get
            {
                lock (this.SyncObject)
                    return this.properties != null;
            }
        }

        /// <summary>
        /// Returns whether the type coercions of the 
        /// <see cref="IntermediateGenericSegmentableInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// have been initialized.
        /// </summary>
        protected bool AreTypeCoercionsInitialized
        {
            get
            {
                lock (this.SyncObject)
                    return this.typeCoercions != null;
            }
        }

        /// <summary>
        /// Returns whether the unary operator coercions of the 
        /// <see cref="IntermediateGenericSegmentableInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// have been initialized.
        /// </summary>
        protected bool AreUnaryOperatorCoercionsInitialized
        {
            get
            {
                lock (this.SyncObject)
                    return this.unaryOperatorCoercions != null;
            }
        }

        /// <summary>
        /// Returns whether the members of the 
        /// <see cref="IntermediateGenericSegmentableInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// have been initialized.
        /// </summary>
        protected bool AreMembersInitialized
        {
            get
            {
                lock (this.SyncObject)
                    return this._members != null;
            }
        }

        protected override void OnIdentifierChanged(IGeneralGenericTypeUniqueIdentifier oldIdentifier, DeclarationChangeCause cause)
        {
            base.OnIdentifierChanged(oldIdentifier, cause);
        }

        public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get
            {
                lock (this.SyncObject)
                    if (this.AreMembersInitialized)
                        if (this.AreTypesInitialized)
                            if (this.TypeParametersInitialized)
                                return this._Members.Keys.Concat(this._Types.Keys.Cast<IGeneralDeclarationUniqueIdentifier>()).Concat(this.TypeParameters.Keys);
                            else
                                return this._Members.Keys.Concat(this._Types.Keys.Cast<IGeneralDeclarationUniqueIdentifier>());
                        else
                            if (this.TypeParametersInitialized)
                                return this._Members.Keys.Concat(this.TypeParameters.Keys);
                            else
                                return this._Members.Keys;
                    else
                        if (this.AreTypesInitialized)
                            if (this.TypeParametersInitialized)
                                return this._Types.Keys.Concat(this.TypeParameters.Keys);
                            else
                                return this._Types.Keys;
                        else
                            if (this.TypeParametersInitialized)
                                return this.TypeParameters.Keys;
                            else
                                return TypeBase<IGeneralGenericTypeUniqueIdentifier, TType>.EmptyIdentifiers;
            }
        }
        public override IEnumerable<IType> GetTypes()
        {
            return base.GetTypes().Concat(this.binaryOperatorCoercions.GetTypes().Concat(this.events.GetTypes()).Concat(this.indexers.GetTypes()).Concat(this.methods.GetTypes()).Concat(this.properties.GetTypes()).Concat(this.typeCoercions.GetTypes()).Concat(this.unaryOperatorCoercions.GetTypes()));
        }

        protected override sealed void ClearIdentifier()
        {
            lock (this.SyncObject)
                if (this.uniqueIdentifier != null)
                    this.uniqueIdentifier = null;
        }

    }
}
