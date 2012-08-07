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
        CliGenericTypeBase<IGeneralGenericTypeUniqueIdentifier, TType>,
        IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, IGeneralGenericTypeUniqueIdentifier, TType>,
        _ICliTypeParent,
        _ICliMethodParent
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
        /// <summary>
        /// Data member for <see cref="Classes"/>.
        /// </summary>
        private CliClassTypeDictionary classes;
        /// <summary>
        /// Data member for <see cref="Delegates"/>.
        /// </summary>
        private CliDelegateTypeDictionary delegates;
        /// <summary>
        /// Data member for <see cref="Enums"/>
        /// </summary>
        private CliEnumTypeDictionary enums;
        /// <summary>
        /// Data member for <see cref="Interfaces"/>
        /// </summary>
        private CliInterfaceTypeDictionary interfaces;
        /// <summary>
        /// Data member for <see cref="Structs"/>.
        /// </summary>
        private CliStructTypeDictionary structs;
        /// <summary>
        /// Data member for <see cref="Types"/>.
        /// </summary>
        private CliFullTypeDictionary types;
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
            get { throw new NotImplementedException(); }
        }

        public TCtor TypeInitializer
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region ICreatableParent Members

        IConstructorMemberDictionary ICreatableParent.Constructors
        {
            get { throw new NotImplementedException(); }
        }

        IConstructorMember ICreatableParent.TypeInitializer
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region ICoercibleType<TType> Members

        public IBinaryOperatorCoercionMemberDictionary<TType> BinaryOperatorCoercions
        {
            get { throw new NotImplementedException(); }
        }

        public ITypeCoercionMemberDictionary<TType> TypeCoercions
        {
            get { throw new NotImplementedException(); }
        }

        public IUnaryOperatorCoercionMemberDictionary<TType> UnaryOperatorCoercions
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region ICoercibleType Members

        IBinaryOperatorCoercionMemberDictionary ICoercibleType.BinaryOperatorCoercions
        {
            get { throw new NotImplementedException(); }
        }

        ITypeCoercionMemberDictionary ICoercibleType.TypeCoercions
        {
            get { throw new NotImplementedException(); }
        }

        IUnaryOperatorCoercionMemberDictionary ICoercibleType.UnaryOperatorCoercions
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IFieldParent<TField,TType> Members

        public IFieldMemberDictionary<TField, TType> Fields
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IFieldParent Members

        IFieldMemberDictionary IFieldParent.Fields
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IEventParent<TEvent,TType> Members

        public IEventMemberDictionary<TEvent, TType> Events
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IEventSignatureParent<TEvent,IEventParameterMember<TEvent,TType>,TType> Members

        IEventSignatureMemberDictionary<TEvent, IEventParameterMember<TEvent, TType>, TType> IEventSignatureParent<TEvent, IEventParameterMember<TEvent, TType>, TType>.Events
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IEventSignatureParent Members

        IEventSignatureMemberDictionary IEventSignatureParent.Events
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IMethodParent<TMethod,TType> Members

        public IMethodMemberDictionary<TMethod, TType> Methods
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IMethodParent Members

        IMethodMemberDictionary IMethodParent.Methods
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IIndexerParent<TIndexer,TType> Members

        public IIndexerMemberDictionary<TIndexer, TType> Indexers
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IIndexerParent Members

        IIndexerMemberDictionary IIndexerParent.Indexers
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IPropertyParent<TProperty,TType> Members

        public IPropertyMemberDictionary<TProperty, TType> Properties
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IPropertyParent Members

        IPropertyMemberDictionary IPropertyParent.Properties
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region ITypeParent Members

        public IClassTypeDictionary Classes
        {
            get { throw new NotImplementedException(); }
        }

        public IDelegateTypeDictionary Delegates
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumTypeDictionary Enums
        {
            get { throw new NotImplementedException(); }
        }

        public IInterfaceTypeDictionary Interfaces
        {
            get { throw new NotImplementedException(); }
        }

        public IStructTypeDictionary Structs
        {
            get { throw new NotImplementedException(); }
        }

        public IFullTypeDictionary Types
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region _ICliTypeParent Members

        public new _ICliManager Manager
        {
            get { throw new NotImplementedException(); }
        }

        public new _ICliAssembly Assembly
        {
            get { throw new NotImplementedException(); }
        }

        public IReadOnlyCollection<ICliMetadataTypeDefinitionTableRow> _Types
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region ICliTypeParent Members

        public ICliMetadataTypeDefinitionTableRow FindType(string @namespace, string name)
        {
            throw new NotImplementedException();
        }

        public ICliMetadataTypeDefinitionTableRow FindType(string @namespace, string name, string moduleName)
        {
            throw new NotImplementedException();
        }

        public ICliMetadataTypeDefinitionTableRow FindType(IGeneralTypeUniqueIdentifier uniqueIdentifier)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
