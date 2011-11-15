using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal abstract partial class _GenericInstantiableTypeBase<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType> :
        _GenericTypeBase<IGeneralGenericTypeUniqueIdentifier, TType>,
        IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, IGeneralGenericTypeUniqueIdentifier, TType>
        where TCtor :
            class,
            IConstructorMember<TCtor, TType>
        where TEvent :
            IEventMember<TEvent, TType>
        where TField :
            IFieldMember<TField, TType>,
            IInstanceMember
        where TIndexer :
            IIndexerMember<TIndexer, TType>
        where TMethod :
            IMethodMember<TMethod, TType>,
            IExtendedInstanceMember
        where TProperty :
            IPropertyMember<TProperty, TType>
        where TType :
            class,
            IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, IGeneralGenericTypeUniqueIdentifier, TType>,
            IGenericType<IGeneralGenericTypeUniqueIdentifier, TType>
    {
        private _FullMembersBase _members;
        private TCtor typeInitializer;
        private bool fullTypesChecked;
        /// <summary>
        /// Data member for <see cref="Classes"/>.
        /// </summary>
        private _ClassTypesBase classes;
        /// <summary>
        /// Data member for <see cref="Delegates"/>.
        /// </summary>
        private _DelegateTypesBase delegates;
        /// <summary>
        /// Data member for <see cref="Enums"/>
        /// </summary>
        private _EnumTypesBase enums;
        /// <summary>
        /// Data member for <see cref="Interfaces"/>
        /// </summary>
        private _InterfaceTypesBase interfaces;
        /// <summary>
        /// Data member for <see cref="Structs"/>.
        /// </summary>
        private _StructTypesBase structs;
        /// <summary>
        /// Data member for <see cref="Types"/>.
        /// </summary>
        private _FullTypesBase types;
        /// <summary>
        /// Data member for <see cref="BinaryOperatorCoercions"/>.
        /// </summary>
        private IBinaryOperatorCoercionMemberDictionary<TType> binaryOperatorCoercions;
        /// <summary>
        /// Data member for <see cref="TypeCoercions"/>.
        /// </summary>
        private ITypeCoercionMemberDictionary<TType> typeCoercions;
        /// <summary>
        /// Data member for <see cref="UnaryOperatorCoercions"/>.
        /// </summary>
        private IUnaryOperatorCoercionMemberDictionary<TType> unaryOperatorCoercions;
        /// <summary>
        /// Data member for <see cref="Constructors"/>.
        /// </summary>
        private IConstructorMemberDictionary<TCtor, TType> constructors;
        /// <summary>
        /// Data member for <see cref="Methods"/>.
        /// </summary>
        private IMethodMemberDictionary<TMethod, TType> methods;
        /// <summary>
        /// Data member for <see cref="Events"/>.
        /// </summary>
        private IEventMemberDictionary<TEvent, TType> events;
        /// <summary>
        /// Data member for <see cref="Fields"/>.
        /// </summary>
        private IFieldMemberDictionary<TField, TType> fields;
        /// <summary>
        /// Data member for <see cref="Indexers"/>.
        /// </summary>
        private IIndexerMemberDictionary<TIndexer, TType> indexers;
        /// <summary>
        /// Data member for <see cref="Properties"/>
        /// </summary>
        private IPropertyMemberDictionary<TProperty, TType> properties;
        protected _GenericInstantiableTypeBase(TType original, ITypeCollectionBase genericParameters)
            : base(original, genericParameters)
        {
        }

        #region IInstantiableType<TCtor,TEvent,TField,TIndexer,TMethod,TProperty,TType> Members

        public IInterfaceMemberMapping<TMethod, TProperty, TEvent, TIndexer, TType> GetInterfaceMap(IInterfaceType type)
        {
            return this.OnGetInterfaceMapping(type);
        }

        protected abstract IInterfaceMemberMapping<TMethod, TProperty, TEvent, TIndexer, TType> OnGetInterfaceMapping(IInterfaceType type);

        #endregion

        #region ICreatableParent<TCtor,TType> Members

        public IConstructorMemberDictionary<TCtor, TType> Constructors
        {
            get
            {
                this.CheckConstructors();
                lock (SyncObject)
                    return this.constructors;
            }
        }

        /// <summary>
        /// Checks to see whether the data member
        /// <see cref="constructors"/> is initialized 
        /// to its default state.
        /// </summary>
        private void CheckConstructors()
        {
            lock (SyncObject)
                if (this.constructors == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.constructors = this.InitializeConstructors();
                }
        }

        /// <summary>
        /// Returns the <see cref="constructors"/>'s default value.
        /// </summary>
        /// <remarks>
        /// Used by <see cref="CheckConstructors"/>.
        /// </remarks>
        /// <returns>
        /// A <see cref="IConstructorMemberDictionary{TCtor, TType}"/> instance.
        /// </returns>
        protected abstract IConstructorMemberDictionary<TCtor, TType> InitializeConstructors();

        #endregion

        #region ICreatableParent Members

        IConstructorMemberDictionary ICreatableParent.Constructors
        {
            get { return (IConstructorMemberDictionary)this.Constructors; }
        }

        #endregion

        #region IMethodParent<TMethod,TType> Members
        
        public IMethodMemberDictionary<TMethod, TType> Methods
        {
            get
            {
                this.CheckMethods();
                lock (SyncObject)
                    return this.methods;
            }
        }

        /// <summary>
        /// Checks to see whether the data member
        /// <see cref="methods"/> is initialized 
        /// to its default state.
        /// </summary>
        private void CheckMethods()
        {
            lock (SyncObject)
                if (this.methods == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.methods = this.InitializeMethods();
        }

        protected abstract IMethodMemberDictionary<TMethod, TType> InitializeMethods();
        
        #endregion

        #region IMethodParent Members

        IMethodMemberDictionary IMethodParent.Methods
        {
            get { return (IMethodMemberDictionary)this.Methods; }
        }

        #endregion

        #region IPropertyParent<TProperty,TType> Members

        public IPropertyMemberDictionary<TProperty, TType> Properties
        {
            get
            {
                this.CheckProperties();
                lock (SyncObject)
                    return this.properties;
            }
        }

        /// <summary>
        /// Checks to see whether the data member
        /// <see cref="properties"/> is initialized 
        /// to its default state.
        /// </summary>
        private void CheckProperties()
        {
            lock (SyncObject)
                if (this.properties == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.properties = this.InitializeProperties();
        }

        /// <summary>
        /// Returns the <see cref="properties"/>' default value.
        /// </summary>
        /// <remarks>
        /// Used by <see cref="CheckProperties"/>.
        /// </remarks>
        /// <returns>
        /// A <see cref="IPropertyMemberDictionary{TProperty, TType}"/> instance.
        /// </returns>
        protected abstract IPropertyMemberDictionary<TProperty, TType> InitializeProperties();


        #endregion

        #region IPropertyParent Members

        IPropertyMemberDictionary IPropertyParent.Properties
        {
            get { return (IPropertyMemberDictionary)this.Properties; }
        }

        #endregion

        #region IFieldParent<TField,TType> Members
        
        public IFieldMemberDictionary<TField, TType> Fields
        {
            get
            {
                this.CheckFields();
                lock (SyncObject)
                    return this.fields;
            }
        }

        /// <summary>
        /// Checks to see whether the data member
        /// <see cref="fields"/> is initialized 
        /// to its default state.
        /// </summary>
        private void CheckFields()
        {
            lock (SyncObject)
                if (this.fields == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.fields = this.InitializeFields();
        }

        /// <summary>
        /// Returns the <see cref="fields"/>' default value.
        /// </summary>
        /// <remarks>
        /// Used by <see cref="CheckFields"/>.
        /// </remarks>
        /// <returns>
        /// A <see cref="IFieldMemberDictionary{TField, TType}"/> instance.
        /// </returns>
        protected abstract IFieldMemberDictionary<TField, TType> InitializeFields();

        #endregion

        #region IFieldParent Members

        IFieldMemberDictionary IFieldParent.Fields
        {
            get { return ((IFieldMemberDictionary)(this.Fields)); }
        }

        #endregion

        #region ICoercibleType<TType> Members

        public IBinaryOperatorCoercionMemberDictionary<TType> BinaryOperatorCoercions
        {
            get
            {
                this.CheckBinaryOperatorCoercions();
                lock (SyncObject)
                    return this.binaryOperatorCoercions;
            }
        }

        /// <summary>
        /// Checks to see whether the data member
        /// <see cref="binaryOperatorCoercions"/> is initialized 
        /// to its default state.
        /// </summary>
        private void CheckBinaryOperatorCoercions()
        {
            lock (SyncObject)
                if (this.binaryOperatorCoercions == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.binaryOperatorCoercions = this.InitializeBinaryOperatorCoercions();
        }

        /// <summary>
        /// Returns the <see cref="binaryOperatorCoercions"/>' default value.
        /// </summary>
        /// <remarks>
        /// Used by <see cref="CheckBinaryOperatorCoercions"/>.
        /// </remarks>
        /// <returns>
        /// A <see cref="IBinaryOperatorCoercionMemberDictionary{TType}"/> instance.
        /// </returns>
        protected IBinaryOperatorCoercionMemberDictionary<TType> InitializeBinaryOperatorCoercions()
        {
            return new _BinaryOperatorsBase(this._Members, this.Original.BinaryOperatorCoercions, (TType)(object)this);
        }

        public ITypeCoercionMemberDictionary<TType> TypeCoercions
        {
            get
            {
                this.CheckTypeCoercions();
                lock (SyncObject)
                    return this.typeCoercions;
            }
        }

        /// <summary>
        /// Checks to see whether the data member
        /// <see cref="typeCoercions"/> is initialized 
        /// to its default state.
        /// </summary>
        private void CheckTypeCoercions()
        {
            lock (SyncObject)
                if (this.typeCoercions == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.typeCoercions = this.InitializeTypeCoercions();
        }

        /// <summary>
        /// Returns the <see cref="typeCoercions"/>' default value.
        /// </summary>
        /// <remarks>
        /// Used by <see cref="CheckTypeCoercions"/>.
        /// </remarks>
        /// <returns>
        /// A <see cref="ITypeCoercionMemberDictionary{TType}"/> instance.
        /// </returns>
        protected ITypeCoercionMemberDictionary<TType> InitializeTypeCoercions()
        {
            return new _TypeCoercionsBase(this._Members, this.Original.TypeCoercions, ((TType)(object)this));
        }

        public IUnaryOperatorCoercionMemberDictionary<TType> UnaryOperatorCoercions
        {
            get
            {
                this.CheckUnaryOperatorCoercions();
                lock (SyncObject)
                    return this.unaryOperatorCoercions;
            }
        }

        /// <summary>
        /// Checks to see whether the data member
        /// <see cref="unaryOperatorCoercions"/> is initialized 
        /// to its default state.
        /// </summary>
        private void CheckUnaryOperatorCoercions()
        {
            lock (SyncObject)
                if (this.unaryOperatorCoercions == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.unaryOperatorCoercions = this.InitializeUnaryOperatorCoercions();
        }

        /// <summary>
        /// Returns the <see cref="unaryOperatorCoercions"/>'s default value.
        /// </summary>
        /// <remarks>
        /// Used by <see cref="CheckUnaryOperatorCoercions"/>.
        /// </remarks>
        /// <returns>
        /// A <see cref="IUnaryOperatorCoercionMemberDictionary{TType}"/> instance.
        /// </returns>
        protected IUnaryOperatorCoercionMemberDictionary<TType> InitializeUnaryOperatorCoercions()
        {
            return new _UnaryOperatorsBase(master:this._Members, originalSet:this.Original.UnaryOperatorCoercions, parent:this);
        }


        #endregion

        #region ICoercibleType Members

        IBinaryOperatorCoercionMemberDictionary ICoercibleType.BinaryOperatorCoercions
        {
            get { return ((IBinaryOperatorCoercionMemberDictionary)(this.BinaryOperatorCoercions)); }
        }

        ITypeCoercionMemberDictionary ICoercibleType.TypeCoercions
        {
            get { return ((ITypeCoercionMemberDictionary)(this.TypeCoercions)); }
        }

        IUnaryOperatorCoercionMemberDictionary ICoercibleType.UnaryOperatorCoercions
        {
            get { return ((IUnaryOperatorCoercionMemberDictionary)(this.UnaryOperatorCoercions)); }
        }

        #endregion

        #region IEventParent<TEvent,TType> Members


        public IEventMemberDictionary<TEvent, TType> Events
        {
            get
            {
                this.CheckEvents();
                lock (SyncObject)
                    return this.events;
            }
        }

        /// <summary>
        /// Checks to see whether the data member
        /// <see cref="events"/> is initialized 
        /// to its default state.
        /// </summary>
        private void CheckEvents()
        {
            lock (SyncObject)
                if (this.events == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.events = this.InitializeEvents();
                }
        }

        /// <summary>
        /// Returns the <see cref="events"/>'s default value.
        /// </summary>
        /// <remarks>
        /// Used by <see cref="CheckEvents"/>.
        /// </remarks>
        /// <returns>
        /// A <see cref="IEventMemberDictionary{TEvent, TType}"/> instance.
        /// </returns>
        protected abstract IEventMemberDictionary<TEvent, TType> InitializeEvents();

        #endregion

        #region IEventSignatureParent<TEvent,IEventParameterMember<TEvent,TType>,TType> Members

        IEventSignatureMemberDictionary<TEvent, IEventParameterMember<TEvent, TType>, TType> IEventSignatureParent<TEvent, IEventParameterMember<TEvent, TType>, TType>.Events
        {
            get { return ((IEventSignatureMemberDictionary<TEvent, IEventParameterMember<TEvent, TType>, TType>)(this.Events)); }
        }

        #endregion

        #region IEventSignatureParent Members

        IEventSignatureMemberDictionary IEventSignatureParent.Events
        {
            get { return ((IEventSignatureMemberDictionary)(this.Events)); }
        }

        #endregion

        #region IIndexerParent<TIndexer,TType> Members

        public IIndexerMemberDictionary<TIndexer, TType> Indexers
        {
            get
            {
                this.CheckIndexers();
                lock (SyncObject)
                    return this.indexers;
            }
        }

        /// <summary>
        /// Checks to see whether the data member
        /// <see cref="indexers"/> is initialized 
        /// to its default state.
        /// </summary>
        private void CheckIndexers()
        {
            lock (SyncObject)
                if (this.indexers == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.indexers = this.InitializeIndexers();
        }

        /// <summary>
        /// Returns the <see cref="indexers"/>'s default value.
        /// </summary>
        /// <remarks>
        /// Used by <see cref="CheckIndexers"/>.
        /// </remarks>
        /// <returns>
        /// A <see cref="IIndexerMemberDictionary{TIndexer, TType}"/> instance.
        /// </returns>
        protected abstract IIndexerMemberDictionary<TIndexer, TType> InitializeIndexers();


        #endregion

        #region IIndexerParent Members

        IIndexerMemberDictionary IIndexerParent.Indexers
        {
            get { return ((IIndexerMemberDictionary)(this.Indexers)); }
        }

        #endregion

        #region ITypeParent Members
        public IClassTypeDictionary Classes
        {
            get
            {
                this.CheckClasses();
                lock (SyncObject)
                    return this.classes;
            }
        }

        /// <summary>
        /// Checks to see whether the data member
        /// <see cref="classes"/> is initialized 
        /// to its default state.
        /// </summary>
        private void CheckClasses()
        {
            lock (SyncObject)
                if (this.classes == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.classes = this.InitializeClasses();
        }

        /// <summary>
        /// Returns the <see cref="classes"/>' default value.
        /// </summary>
        /// <remarks>
        /// Used by <see cref="CheckClasses"/>.
        /// </remarks>
        /// <returns>
        /// A <see cref="IClassTypeDictionary"/> instance.
        /// </returns>
        private _ClassTypesBase InitializeClasses()
        {
            return new _ClassTypesBase(this._Types, this.Original.Classes, this);
        }
        
        public IDelegateTypeDictionary Delegates
        {
            get
            {
                this.CheckDelegates();
                lock (SyncObject)
                    return this.delegates;
            }
        }

        /// <summary>
        /// Checks to see whether the data member
        /// <see cref="delegates"/> is initialized 
        /// to its default state.
        /// </summary>
        private void CheckDelegates()
        {
            lock (SyncObject)
                if (this.delegates == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.delegates = this.InitializeDelegates();
        }

        /// <summary>
        /// Returns the <see cref="delegates"/>' default value.
        /// </summary>
        /// <remarks>
        /// Used by <see cref="CheckDelegates"/>.
        /// </remarks>
        /// <returns>
        /// A <see cref="IDelegateTypeDictionary"/> instance.
        /// </returns>
        private _DelegateTypesBase InitializeDelegates()
        {
            return new _DelegateTypesBase(this._Types, this.Original.Delegates, this);
        }

        
        public IEnumTypeDictionary Enums
        {
            get
            {
                this.CheckEnums();
                lock (SyncObject)
                    return this.enums;
            }
        }

        /// <summary>
        /// Checks to see whether the data member
        /// <see cref="enums"/> is initialized 
        /// to its default state.
        /// </summary>
        private void CheckEnums()
        {
            lock (SyncObject)
                if (this.enums == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.enums = this.InitializeEnums();
        }

        /// <summary>
        /// Returns the <see cref="enums"/>' default value.
        /// </summary>
        /// <remarks>
        /// Used by <see cref="CheckEnums"/>.
        /// </remarks>
        /// <returns>
        /// A <see cref="IEnumTypeDictionary"/> instance.
        /// </returns>
        private _EnumTypesBase InitializeEnums()
        {
            return new _EnumTypesBase(this._Types, this.Original.Enums, this);
        }

        
        public IInterfaceTypeDictionary Interfaces
        {
            get
            {
                this.CheckInterfaces();
                lock (SyncObject)
                    return this.interfaces;
            }
        }

        /// <summary>
        /// Checks to see whether the data member
        /// <see cref="interfaces"/> is initialized 
        /// to its default state.
        /// </summary>
        private void CheckInterfaces()
        {
            lock (SyncObject)
                if (this.interfaces == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.interfaces = this.InitializeInterfaces();
        }

        /// <summary>
        /// Returns the <see cref="interfaces"/>' default value.
        /// </summary>
        /// <remarks>
        /// Used by <see cref="CheckInterfaces"/>.
        /// </remarks>
        /// <returns>
        /// A <see cref="IInterfaceTypeDictionary"/> instance.
        /// </returns>
        private _InterfaceTypesBase InitializeInterfaces()
        {
            return new _InterfaceTypesBase(this._Types, this.Original.Interfaces, this);
        }

        
        public IStructTypeDictionary Structs
        {
            get
            {
                this.CheckStructs();
                lock (SyncObject)
                    return this.structs;
            }
        }

        /// <summary>
        /// Checks to see whether the data member
        /// <see cref="structs"/> is initialized 
        /// to its default state.
        /// </summary>
        private void CheckStructs()
        {
            lock (SyncObject)
                if (this.structs == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.structs = this.InitializeStructs();
        }

        /// <summary>
        /// Returns the <see cref="structs"/>' default value.
        /// </summary>
        /// <remarks>
        /// Used by <see cref="CheckStructs"/>.
        /// </remarks>
        /// <returns>
        /// A <see cref="IStructTypeDictionary"/> instance.
        /// </returns>
        private _StructTypesBase InitializeStructs()
        {
            return new _StructTypesBase(this._Types, this.Original.Structs, this);
        }

        
        public IFullTypeDictionary Types
        {
            get
            {
                this.CheckTypes();
                return this._Types;
            }
        }

        /// <summary>
        /// Checks to see whether the data member
        /// <see cref="types"/> is initialized 
        /// to its default state.
        /// </summary>
        private void CheckTypes()
        {
            if (!this.fullTypesChecked)
            {
                this.Check_Types();
                this.CheckClasses();
                this.CheckDelegates();
                this.CheckEnums();
                this.CheckInterfaces();
                this.CheckStructs();
                this.fullTypesChecked = true;
            }
        }

        public _FullTypesBase _Types
        {
            get
            {
                this.Check_Types();
                lock (SyncObject)
                    return this.types;
            }
        }

        /// <summary>
        /// Checks to see whether the data member
        /// <see cref="types"/> is initialized 
        /// to its default state.
        /// </summary>
        private void Check_Types()
        {
            lock (SyncObject)
                if (this.types == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.types = this.Initialize_Types();
        }

        /// <summary>
        /// Returns the <see cref="types"/>' default value.
        /// </summary>
        /// <remarks>
        /// Used by <see cref="Check_Types"/>.
        /// </remarks>
        /// <returns>
        /// A <see cref="IFullTypeDictionary"/> instance.
        /// </returns>
        private _FullTypesBase Initialize_Types()
        {
            return new _FullTypesBase();
        }

        #endregion

        public _FullMembersBase _Members
        {
            get
            {
                this.Check_Members();
                lock (SyncObject)
                    return this._members;
            }
        }

        /// <summary>
        /// Checks to see whether the data member
        /// <see cref="_members"/> is initialized 
        /// to its default state.
        /// </summary>
        private void Check_Members()
        {
            lock (SyncObject)
                if (this._members == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this._members = this.Initialize_Members();
        }

        /// <summary>
        /// Returns the <see cref="_members"/>' default value.
        /// </summary>
        /// <remarks>
        /// Used by <see cref="Check_Members"/>.
        /// </remarks>
        /// <returns>
        /// A <see cref="_FullMembersBase"/> instance.
        /// </returns>
        protected _FullMembersBase Initialize_Members()
        {
            return new _FullMembersBase();
        }

        protected override IFullMemberDictionary OnGetMembers()
        {
            this.CheckBinaryOperatorCoercions();
            this.CheckConstructors();
            this.CheckEvents();
            this.CheckFields();
            this.CheckIndexers();
            this.CheckMethods();
            this.CheckProperties();
            this.CheckTypeCoercions();
            this.CheckUnaryOperatorCoercions();
            lock (SyncObject)
                return this._Members;
        }

        #region ICreatableParent<TCtor,TType> Members


        public TCtor TypeInitializer
        {
            get
            {
                if (object.ReferenceEquals(this.typeInitializer, null))
                    if (!(object.ReferenceEquals(this.Original.TypeInitializer, null)))
                        this.typeInitializer = this.InitializeTypeInitializer(this.Original.TypeInitializer);
                return this.typeInitializer;
            }
        }

        protected abstract TCtor InitializeTypeInitializer(TCtor original);

        #endregion

        #region ICreatableParent Members

        IConstructorMember ICreatableParent.TypeInitializer
        {
            get { return this.TypeInitializer; }
        }

        #endregion

        protected override IEnumerable<IDeclaration> OnGetDeclarations()
        {
            return GetTypeParentDeclarations(this);
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
                return;
            try
            {
                lock (SyncObject)
                {
                    if (this.classes != null)
                    {
                        this.classes.Dispose();
                        this.classes = null;
                    }
                    if (this.delegates != null)
                    {
                        this.delegates.Dispose();
                        this.delegates = null;
                    }
                    if (this.enums != null)
                    {
                        this.enums.Dispose();
                        this.enums = null;
                    }
                    if (this.interfaces != null)
                    {
                        this.interfaces.Dispose();
                        this.interfaces = null;
                    }
                    if (this.structs != null)
                    {
                        this.structs.Dispose();
                        this.structs = null;
                    }
                    if (this.types != null)
                        this.types = null;
                    if (this.binaryOperatorCoercions != null)
                    {
                        this.binaryOperatorCoercions.Dispose();
                        this.binaryOperatorCoercions = null;
                    }
                    if (this.typeInitializer != null)
                    {
                        this.typeInitializer.Dispose();
                        this.typeInitializer = null;
                    }
                    if (this.constructors != null)
                    {
                        this.constructors.Dispose();
                        this.constructors = null;
                    }
                    if (this.methods != null)
                    {
                        this.methods.Dispose();
                        this.methods = null;
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
                        this._members = null;
                    fullTypesChecked = true;
                }
            }
            finally
            {
                base.Dispose();
            }
        }
        protected override IGeneralGenericTypeUniqueIdentifier OnGetUniqueIdentifier()
        {
            return AstIdentifier.Type(this.Name, this.Original.TypeParameters.Count);
        }
    }
}
