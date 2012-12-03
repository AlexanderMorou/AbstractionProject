using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Modules;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal abstract partial class CliGenericInstantiableTypeBase<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType> :
        CliGenericParentType<IGeneralGenericTypeUniqueIdentifier, TType>,
        IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, IGeneralGenericTypeUniqueIdentifier, TType>,
        __ICliTypeParent,
        _ICliMethodParent,
        _ICliMemberParent
        where TIndexer :
            class,
            IIndexerMember<TIndexer, TType>
        where TMethod :
            class,
            IMethodMember<TMethod, TType>,
            IExtendedInstanceMember
        where TProperty :
            class,
            IPropertyMember<TProperty, TType>
        where TField :
            class,
            IFieldMember<TField, TType>,
            IInstanceMember
        where TCtor :
            class,
            IConstructorMember<TCtor, TType>
        where TEvent :
            class,
            IEventMember<TEvent, TType>
        where TType :
            class,
            IGenericType<IGeneralGenericTypeUniqueIdentifier, TType>,
            IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, IGeneralGenericTypeUniqueIdentifier, TType>
    {
        private IFullMemberDictionary _members;
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

        protected CliGenericInstantiableTypeBase(CliAssembly assembly, ICliMetadataTypeDefinitionTableRow metadata)
            : base(assembly, metadata)
        {
        }

        #region IInstantiableType<TCtor,TEvent,TField,TIndexer,TMethod,TProperty,IGeneralGenericTypeUniqueIdentifier,TType> Members

        public IInterfaceMemberMapping<TMethod, TProperty, TEvent, TIndexer, TType> GetInterfaceMap(IInterfaceType type)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICreatableParent<TCtor,TType> Members

        public IConstructorMemberDictionary<TCtor, TType> Constructors
        {
            get
            {
                if (this.constructors == null)
                    this.constructors = this.InitializeConstructors();
                return this.constructors;
            }
        }

        public TCtor TypeInitializer
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region ICreatableParent Members

        IConstructorMemberDictionary ICreatableParent.Constructors
        {
            get {
                return (IConstructorMemberDictionary)this.Constructors;
            }
        }

        IConstructorMember ICreatableParent.TypeInitializer
        {
            get {
                return this.TypeInitializer;
            }
        }

        #endregion

        #region ICoercibleType<TType> Members

        public IBinaryOperatorCoercionMemberDictionary<TType> BinaryOperatorCoercions
        {
            get {
                if (this.binaryOperatorCoercions == null)
                    this.binaryOperatorCoercions = this.InitializeBinaryOperatorCoercions();
                return this.binaryOperatorCoercions;
            }
        }

        public ITypeCoercionMemberDictionary<TType> TypeCoercions
        {
            get
            {
                if (this.typeCoercions == null)
                    this.typeCoercions = this.InitializeTypeCoercions();
                return this.typeCoercions;
            }
        }

        public IUnaryOperatorCoercionMemberDictionary<TType> UnaryOperatorCoercions
        {
            get
            {
                if (this.unaryOperatorCoercions == null)
                    this.unaryOperatorCoercions = this.InitializeUnaryOperatorCoercions();
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

        #region IFieldParent<TField,TType> Members

        public IFieldMemberDictionary<TField, TType> Fields
        {
            get
            {
                if (this.fields == null)
                    this.fields = this.InitializeFields();
                return this.fields;
            }
        }

        #endregion

        #region IFieldParent Members

        IFieldMemberDictionary IFieldParent.Fields
        {
            get { return (IFieldMemberDictionary)this.Fields; }
        }

        #endregion

        #region IEventParent<TEvent,TType> Members

        public IEventMemberDictionary<TEvent, TType> Events
        {
            get {
                if (this.events == null)
                    this.events = this.InitializeEvents();
                return this.events;
            }
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

        #region IMethodParent<TMethod,TType> Members

        public IMethodMemberDictionary<TMethod, TType> Methods
        {
            get {
                if (this.methods == null)
                    this.methods = this.InitializeMethods();
                return this.methods;
            }
        }

        #endregion

        #region IMethodParent Members

        IMethodMemberDictionary IMethodParent.Methods
        {
            get { return (IMethodMemberDictionary)this.Methods; }
        }

        #endregion

        #region IIndexerParent<TIndexer,TType> Members

        public IIndexerMemberDictionary<TIndexer, TType> Indexers
        {
            get
            {
                if (this.indexers == null)
                    this.indexers = this.InitializeIndexers();
                return this.indexers;
            }
        }

        #endregion

        #region IIndexerParent Members

        IIndexerMemberDictionary IIndexerParent.Indexers
        {
            get { return (IIndexerMemberDictionary)this.Indexers; }
        }

        #endregion

        #region IPropertyParent<TProperty,TType> Members

        public IPropertyMemberDictionary<TProperty, TType> Properties
        {
            get
            {
                if (this.properties == null)
                    this.properties = this.InitializeProperties();
                return this.properties;
            }
        }

        #endregion

        #region IPropertyParent Members

        IPropertyMemberDictionary IPropertyParent.Properties
        {
            get { return (IPropertyMemberDictionary)this.Properties; }
        }

        #endregion

        protected override sealed IFullMemberDictionary OnGetMembers()
        {
            if (this._members == null)
                this._members = this.Initialize_Members();
            return this._members;
        }

        private IConstructorMemberDictionary<TCtor, TType> InitializeConstructors()
        {
            throw new NotImplementedException();
        }

        private IBinaryOperatorCoercionMemberDictionary<TType> InitializeBinaryOperatorCoercions()
        {
            return new CliBinaryOperatorMemberDictionary<TType>((TType)(object)this, (CliFullMemberDictionary)this.Members);
        }

        private ITypeCoercionMemberDictionary<TType> InitializeTypeCoercions()
        {
            return new CliTypeCoercionMemberDictionary<TType>((TType)(object)this, (CliFullMemberDictionary)this.Members);
        }

        private IUnaryOperatorCoercionMemberDictionary<TType> InitializeUnaryOperatorCoercions()
        {
            return new CliUnaryOperatorMemberDictionary<TType>((TType)(object)this, (CliFullMemberDictionary)this.Members);
        }

        private IFieldMemberDictionary<TField, TType> InitializeFields()
        {
            return new CliFieldMemberDictionary<TField, TType>((TType)(object)this, (CliFullMemberDictionary)this.Members);
        }

        private IEventMemberDictionary<TEvent, TType> InitializeEvents()
        {
            throw new NotImplementedException();
        }

        private IMethodMemberDictionary<TMethod, TType> InitializeMethods()
        {
            throw new NotImplementedException();
        }

        private IIndexerMemberDictionary<TIndexer, TType> InitializeIndexers()
        {
            throw new NotImplementedException();
        }

        private IPropertyMemberDictionary<TProperty, TType> InitializeProperties()
        {
            throw new NotImplementedException();
        }

        private IFullMemberDictionary Initialize_Members()
        {
            return new CliFullMemberDictionary(this);
        }

        IControlledCollection<ICliMetadataPropertyTableRow> _ICliMemberParent._Properties
        {
            get { return this.MetadataEntry.Properties; }
        }

        IControlledCollection<ICliMetadataEventTableRow> _ICliMemberParent._Events
        {
            get { return this.MetadataEntry.Events; }
        }

        IControlledCollection<ICliMetadataMethodDefinitionTableRow> _ICliMemberParent._Methods
        {
            get { return this.MetadataEntry.Methods; }
        }

        IControlledCollection<ICliMetadataFieldTableRow> _ICliMemberParent._Fields
        {
            get { return this.MetadataEntry.Fields; }
        }

        ISubordinateDictionary _ICliMemberParent.BinaryOperators
        {
            get { return (ISubordinateDictionary)this.BinaryOperatorCoercions; }
        }

        ISubordinateDictionary _ICliMemberParent.Constructors
        {
            get { return (ISubordinateDictionary)this.Constructors; }
        }

        ISubordinateDictionary _ICliMemberParent.Events
        {
            get { return (ISubordinateDictionary)this.Events; }
        }

        ISubordinateDictionary _ICliMemberParent.Fields
        {
            get { return (ISubordinateDictionary)this.Fields; }
        }

        ISubordinateDictionary _ICliMemberParent.Indexers
        {
            get { return (ISubordinateDictionary)this.Indexers; }
        }

        ISubordinateDictionary _ICliMemberParent.Methods
        {
            get { return (ISubordinateDictionary)this.Methods; }
        }

        ISubordinateDictionary _ICliMemberParent.Properties
        {
            get { return (ISubordinateDictionary)this.Properties; }
        }

        ISubordinateDictionary _ICliMemberParent.TypeCoercions
        {
            get { return (ISubordinateDictionary)this.TypeCoercions; }
        }

        ISubordinateDictionary _ICliMemberParent.UnaryOperators
        {
            get { return (ISubordinateDictionary)this.UnaryOperatorCoercions; }
        }

        public ICliMetadataRoot MetadataRoot
        {
            get { return this.MetadataEntry.MetadataRoot; }
        }

        public IMember CreateItem(CliMemberType memberKind, ICliMetadataTableRow metadataEntry, IMemberUniqueIdentifier uniqueIdentifier, int index)
        {
            switch (memberKind)
            {
                case CliMemberType.BinaryOperator:
                    return this.GetBinaryOperator((ICliMetadataMethodDefinitionTableRow)metadataEntry, (IBinaryOperatorUniqueIdentifier)uniqueIdentifier);
                case CliMemberType.Constructor:
                    return this.GetConstructor((ICliMetadataMethodDefinitionTableRow)(metadataEntry));
                case CliMemberType.Event:
                    return this.GetEvent((ICliMetadataEventTableRow)metadataEntry);
                case CliMemberType.Field:
                    return this.GetField((ICliMetadataFieldTableRow)metadataEntry);
                case CliMemberType.Indexer:
                    return this.GetIndexer((ICliMetadataPropertyTableRow)metadataEntry);
                case CliMemberType.Method:
                    return this.GetMethod((ICliMetadataMethodDefinitionTableRow)metadataEntry);
                case CliMemberType.Property:
                    return (TProperty)this.GetProperty((ICliMetadataPropertyTableRow)metadataEntry);
                case CliMemberType.TypeCoercionOperator:
                    return (ITypeCoercionMember<TType>)this.GetTypeOperator((ICliMetadataMethodDefinitionTableRow)metadataEntry, (ITypeCoercionUniqueIdentifier)uniqueIdentifier);
                case CliMemberType.UnaryOperator:
                    return (IUnaryOperatorCoercionMember<TType>)this.GetUnaryOperator((ICliMetadataMethodDefinitionTableRow)metadataEntry, (IUnaryOperatorUniqueIdentifier)uniqueIdentifier);
                default:
                    throw new InvalidOperationException();
            }
        }

        protected abstract TCtor GetConstructor(ICliMetadataMethodDefinitionTableRow metadataEntry);

        protected abstract TEvent GetEvent(ICliMetadataEventTableRow metadataEntry);

        protected abstract TField GetField(ICliMetadataFieldTableRow metadataEntry);

        protected abstract TIndexer GetIndexer(ICliMetadataPropertyTableRow metadataEntry);

        protected abstract TMethod GetMethod(ICliMetadataMethodDefinitionTableRow metadataEntry);

        protected abstract TProperty GetProperty(ICliMetadataPropertyTableRow metadataEntry);

        protected virtual IBinaryOperatorCoercionMember<TType> GetBinaryOperator(ICliMetadataMethodDefinitionTableRow metadataEntry, IBinaryOperatorUniqueIdentifier uniqueIdentifier)
        {
            return new BinaryOperatorMember(uniqueIdentifier, metadataEntry, (TType)(object)this);
        }

        protected virtual ITypeCoercionMember<TType> GetTypeOperator(ICliMetadataMethodDefinitionTableRow metadataEntry, ITypeCoercionUniqueIdentifier uniqueIdentifier)
        {
            return new TypeCoercionMember(uniqueIdentifier, metadataEntry, (TType)(object)this);
        }

        protected virtual IUnaryOperatorCoercionMember<TType> GetUnaryOperator(ICliMetadataMethodDefinitionTableRow metadataEntry, IUnaryOperatorUniqueIdentifier uniqueIdentifier)
        {
            return new UnaryOperatorMember(uniqueIdentifier, metadataEntry, (TType)(object)(this));
        }
    }
}
