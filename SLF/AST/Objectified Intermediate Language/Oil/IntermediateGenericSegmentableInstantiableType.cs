using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
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
    /// <typeparam name="TField">The type of the field members in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateField">The type of the field members in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TIndexer">The type of the indexer members in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexer">The type of the indexer members in the intermediate abstract
    /// syntax tree.</typeparam>
    /// <typeparam name="TMethod">The type of the method members in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateMethod">The type of the method members in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TProperty">The type of the property members in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateProperty">The type of the property members in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TType">The kind of type which contains all the members
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The kind of type which contains all the members
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TInstanceIntermediateType">The kind of type which implements the <typeparamref name="TIntermediateType"/>
    /// and will be instanced by the parts helper class.</typeparam>
    public abstract partial class IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> :
        IntermediateGenericSegmentableParentType<TType, TIntermediateType, TInstanceIntermediateType>,
        IIntermediateInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType>,
        IIntermediateInstantiableType
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
        where TIntermediateEventMethod :
            class,
            TIntermediateMethod,
            IIntermediateEventMethodMember
        where TField :
            IFieldMember<TField, TType>,
            IInstanceMember
        where TIntermediateField :
            TField,
            IIntermediateFieldMember<TField, TIntermediateField, TType, TIntermediateType>,
            IIntermediateInstanceMember
        where TIndexer :
            IIndexerMember<TIndexer, TType>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TType, TIntermediateType>
        where TIntermediateIndexerMethod :
            class, 
            TMethod,
            IIntermediatePropertyMethodMember
        where TMethod :
            class,
            IMethodMember<TMethod, TType>,
            IExtendedInstanceMember
        where TIntermediateMethod :
            class,
            TMethod,
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TType, TIntermediateType>
        where TProperty :
            IPropertyMember<TProperty, TType>
        where TIntermediateProperty :
            TProperty,
            IIntermediatePropertyMember<TProperty, TIntermediateProperty, TType, TIntermediateType>
        where TIntermediatePropertyMethod :
            class,
            TMethod,
            IIntermediatePropertyMethodMember
        where TType :
            class,
            IGenericType<TType>,
            IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType>
        where TIntermediateType :
            class,
            IIntermediateGenericType<TType, TIntermediateType>,
            IIntermediateSegmentableType<TType, TIntermediateType>,
            IIntermediateInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType>,
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
        private ImplementedInterfacesDictionary<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType> implementedInterfaces;

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
        private bool lockMembersAndTypes;
        #endregion

        private int suspendLevel = 0;

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

        #region IIntermediateCreatableType<TCtor,TIntermediateCtor,TType,TIntermediateType> Members

        /// <summary>
        /// Returns the constructors contained by the 
        /// <see cref="IntermediateGenericSegmentableInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType}"/>.
        /// </summary>
        public IIntermediateConstructorMemberDictionary<TCtor, TIntermediateCtor, TType, TIntermediateType> Constructors
        {
            get
            {
                this.CheckConstructors();
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
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IIntermediateCreatableSignatureType<TCtor,TIntermediateCtor,TType,TIntermediateType> Members

        IIntermediateConstructorSignatureMemberDictionary<TCtor, TIntermediateCtor, TType, TIntermediateType> IIntermediateCreatableSignatureType<TCtor, TIntermediateCtor, TType, TIntermediateType>.Constructors
        {
            get
            {
                return this.Constructors;
            }
        }

        #endregion

        #region IIntermediateCreatableSignatureType Members

        IIntermediateConstructorSignatureMemberDictionary IIntermediateCreatableSignatureType.Constructors
        {
            get { return (IIntermediateConstructorSignatureMemberDictionary)this.Constructors; }
        }

        #endregion
        #region IIntermediateCreatableType Members

        IIntermediateConstructorMemberDictionary IIntermediateCreatableType.Constructors
        {
            get { return (IIntermediateConstructorMemberDictionary)this.Constructors; }
        }

        IIntermediateConstructorMember IIntermediateCreatableType.TypeInitializer
        {
            get
            {
                return this.TypeInitializer;
            }
        }

        #endregion

        #region ICreatableType Members

        IConstructorMemberDictionary ICreatableType.Constructors
        {
            get { return (IConstructorMemberDictionary)this.Constructors; }
        }

        IConstructorMember ICreatableType.TypeInitializer
        {
            get { return this.TypeInitializer; }
        }

        #endregion

        #region ICreatableType<TCtor,TType> Members

        IConstructorMemberDictionary<TCtor, TType> ICreatableType<TCtor, TType>.Constructors
        {
            get
            {
                return this.Constructors;
            }
        }

        TCtor ICreatableType<TCtor, TType>.TypeInitializer
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

        #region IIntermediatePropertyParentType<TProperty,TIntermediateProperty,TType,TIntermediateType> Members

        public IIntermediatePropertyMemberDictionary<TProperty, TIntermediateProperty, TType, TIntermediateType> Properties
        {
            get
            {
                this.CheckProperties();
                return this.properties;
            }
        }

        #endregion

        #region IPropertyParentType<TProperty,TType> Members

        IPropertyMemberDictionary<TProperty, TType> IPropertyParentType<TProperty, TType>.Properties
        {
            get { return this.Properties; }
        }

        #endregion

        #region IPropertyParentType Members

        IPropertyMemberDictionary IPropertyParentType.Properties
        {
            get { return (IPropertyMemberDictionary)this.Properties; }
        }

        #endregion

        #region IIntermediatePropertyParentType Members

        IIntermediatePropertyMemberDictionary IIntermediatePropertyParentType.Properties
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
                return this.binaryOperatorCoercions;
            }
        }

        public IIntermediateTypeCoercionMemberDictionary<TType, TIntermediateType> TypeCoercions
        {
            get
            {
                this.CheckTypeCoercions();
                return this.typeCoercions;
            }
        }

        public IIntermediateUnaryOperatorCoercionMemberDictionary<TType, TIntermediateType> UnaryOperatorCoercions
        {
            get
            {
                this.CheckUnaryOperatorCoercions();
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

        public IInterfaceMemberMapping<TMethod, IInterfaceMethodMember, TProperty, IInterfacePropertyMember, TEvent, IInterfaceEventMember, TIndexer, IInterfaceIndexerMember, TType, IInterfaceType> GetInterfaceMap(IInterfaceType type)
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

                if (this._members == null)
                    if (this.IsRoot)
                        this._members = new IntermediateFullMemberDictionary();
                    else
                        this._members = new IntermediateFullMemberDictionary((IntermediateFullMemberDictionary)((TInstanceIntermediateType)this.GetRoot())._Members);
                return this._members;
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
        /// Obtains a new <see cref="ConstructorMember"/> with no parameters.
        /// </summary>
        /// <returns>A new <see cref="ConstructorMember"/>, if successful.</returns>
        /// <remarks>Required by design, due to further inheritance on constructors being necessary.</remarks>
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
        private static void SuspendCheck<TMemberParent, TIntermediateMemberParent, TMember, TIntermediateMember>(IntermediateGroupedMemberDictionary<TMemberParent, TIntermediateMemberParent, TMember, TIntermediateMember> target, int suspendLevel)
            where TMemberParent :
                IMemberParent
            where TIntermediateMemberParent :
                TMemberParent,
                IIntermediateMemberParent
            where TMember :
                IMember<TMemberParent>
            where TIntermediateMember :
                TMember,
                IIntermediateMember<TMemberParent, TIntermediateMemberParent>
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
            if (this.binaryOperatorCoercions == null)
            {
                this.binaryOperatorCoercions = InitializeBinaryOperatorCoercions();
                SuspendCheck(binaryOperatorCoercions, suspendLevel);
            }
        }

        private void CheckConstructors()
        {
            if (this.constructors == null)
            {
                this.constructors = this.InitializeConstructors();
                SuspendCheck(constructors, suspendLevel);
            }
        }

        private void CheckEvents()
        {
            if (this.events == null)
            {
                this.events = this.InitializeEvents();
                SuspendCheck(events, suspendLevel);
            }
        }

        private void CheckFields()
        {
            if (this.fields == null)
            {
                this.fields = this.InitializeFields();
                SuspendCheck(fields, suspendLevel);
            }
        }

        private void CheckIndexers()
        {
            if (this.indexers == null)
            {
                this.indexers = this.InitializeIndexers();
                SuspendCheck(indexers, suspendLevel);
            }
        }

        private void CheckMethods()
        {
            if (this.methods == null)
            {
                this.methods = this.InitializeMethods();
                SuspendCheck(methods, suspendLevel);
            }
        }

        private void CheckProperties()
        {
            if (this.properties == null)
            {
                this.properties = this.InitializeProperties();
                SuspendCheck(properties, suspendLevel);
            }
        }

        private void CheckTypeCoercions()
        {
            if (this.typeCoercions == null)
            {
                this.typeCoercions = this.InitializeTypeCoercions();
                SuspendCheck(typeCoercions, suspendLevel);
            }
        }

        private void CheckUnaryOperatorCoercions()
        {
            if (this.unaryOperatorCoercions == null)
            {
                this.unaryOperatorCoercions = this.InitializeUnaryOperatorCoercions();
                SuspendCheck(unaryOperatorCoercions, suspendLevel);
            }
        }

        #endregion

        #region Initializers

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
                result = new MethodDictionary(this._Members, (TInstanceIntermediateType)this);
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

        public new ImplementedInterfacesDictionary<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType> ImplementedInterfaces
        {
            get
            {
                if (this.implementedInterfaces == null)
                    this.implementedInterfaces = new ImplementedInterfacesDictionary<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType>((TInstanceIntermediateType)this);
                return this.implementedInterfaces;
            }
        }

        IIntermediateInstantiableTypeImplementedInterfaces<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType> IIntermediateInstantiableType<TCtor,TIntermediateCtor,TEvent,TIntermediateEvent,TField,TIntermediateField,TIndexer,TIntermediateIndexer,TMethod,TIntermediateMethod,TProperty,TIntermediateProperty,TType,TIntermediateType>.ImplementedInterfaces
        {
            get {
                return this.ImplementedInterfaces;
            }
        }

        #endregion

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            return this.ImplementedInterfaces.GetLocked();
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

        protected override void Dispose(bool dispose)
        {
            try
            {
                if (dispose)
                {
                    if (this.thisReference != null)
                        this.thisReference.Dispose();
                    if (this.binaryOperatorCoercions != null)
                        this.binaryOperatorCoercions.Dispose();
                    if (this.constructors != null)
                        this.constructors.Dispose();
                    if (this.events != null)
                        this.events.Dispose();
                    if (this.fields != null)
                        this.fields.Dispose();
                    if (this.indexers != null)
                        this.indexers.Dispose();
                    if (this.methods != null)
                        this.methods.Dispose();
                    if (this.properties != null)
                        this.properties.Dispose();
                    if (this.typeCoercions != null)
                        this.typeCoercions.Dispose();
                    if (this.unaryOperatorCoercions != null)
                        this.unaryOperatorCoercions.Dispose();
                }
            }
            finally
            {
                base.Dispose(dispose);
            }
        }

        internal override void OnLocked()
        {
            try
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
            finally
            {
                base.OnLocked();
            }
        }

        internal override void OnUnlocked()
        {
            try
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
            finally
            {
                base.OnUnlocked();
            }
        }
    }
}
