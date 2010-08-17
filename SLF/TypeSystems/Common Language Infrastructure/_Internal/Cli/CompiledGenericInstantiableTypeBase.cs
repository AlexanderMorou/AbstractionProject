using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    /// <summary>
    /// Provides a root implementation of a compiled generic instantiable type.
    /// </summary>
    /// <typeparam name="TCtor">The type used for the constructors in the current implementation.</typeparam>
    /// <typeparam name="TEvent">The type used for the events in the current implementation.</typeparam>
    /// <typeparam name="TField">The type used for the fields in the current implementation.</typeparam>
    /// <typeparam name="TIndexer">The type used for the indexers in the current implementation.</typeparam>
    /// <typeparam name="TMethod">The type used for the methods in the current implementation.</typeparam>
    /// <typeparam name="TProperty">The type used for the properties in the current implementation.</typeparam>
    /// <typeparam name="TType">The <see cref="IInstantiableType{TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType}"/> 
    /// in the implementation.</typeparam>
    internal abstract partial class CompiledGenericInstantiableTypeBase<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType> :
        CompiledGenericTypeBase<TType>,
        IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType>,
        IEventSignatureParent,
        ICompiledTypeParent
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
            IGenericType<TType>,
            IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType>
    {
        #region CompiledGenericInstantiableTypeBase<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType> Data Members
        /// <summary>
        /// Data member for <see cref="TypeInitializer"/>.
        /// </summary>
        private TCtor typeInitializer;
        private bool bCheckedInitializer;
        private IDictionary<IInterfaceType, IInterfaceMemberMapping<TMethod, IInterfaceMethodMember, TProperty, IInterfacePropertyMember, TEvent, IInterfaceEventMember, TIndexer, IInterfaceIndexerMember, TType, IInterfaceType>> interfaceMaps = null;
        /// <summary>
        /// Data member for <see cref="Classes"/>.
        /// </summary>
        private CompiledClassTypeDictionary classes;
        /// <summary>
        /// Data member for <see cref="Delegates"/>.
        /// </summary>
        private CompiledDelegateTypeDictionary delegates;
        /// <summary>
        /// Data member for <see cref="Enums"/>
        /// </summary>
        private CompiledEnumTypeDictionary enums;
        /// <summary>
        /// Data member for <see cref="Interfaces"/>
        /// </summary>
        private CompiledInterfaceTypeDictionary interfaces;
        /// <summary>
        /// Data member for <see cref="Structs"/>.
        /// </summary>
        private CompiledStructTypeDictionary structs;
        /// <summary>
        /// Data member for <see cref="Types"/>.
        /// </summary>
        private CompiledFullTypeDictionary types;
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

        /// <summary>
        /// Data member which holds the references to the nested child types
        /// of the current compiled generic instantiable type.
        /// </summary>
        private Type[] underlyingSystemTypes;
        #endregion 

        #region CompiledGenericInstantiableTypeBase<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType> Constructor Members
        /// <summary>
        /// Creates a new <see cref="CompiledGenericInstantiableTypeBase{TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType}"/> with the 
        /// <paramref name="underlyingSystemType"/> provided.
        /// </summary>
        /// <param name="underlyingSystemType">The <see cref="System.Type"/> from which the current
        /// <see cref="CompiledGenericInstantiableTypeBase{TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType}"/> is based.</param>
        internal CompiledGenericInstantiableTypeBase(Type underlyingSystemType)
            : base(underlyingSystemType)
        {
        }

        #endregion

        #region Member Initialization Checks
        private void CheckConstructor()
        {
            if (this.constructors == null)
                this.constructors = this.InitializeCtors();
        }

        private void CheckEvents()
        {
            try
            {
                this.events = this.InitializeEvents();
            }
            catch (NotImplementedException)
            {
            }
        }

        private void CheckFields()
        {
            if (this.fields == null)
                this.fields = this.InitializeFields();
        }

        private void CheckIndexers()
        {
            try
            {
                if (this.indexers == null)
                    this.indexers = this.InitializeIndexers();
            }
            catch (NotImplementedException)
            {
            }
        }

        private void CheckMethods()
        {
            if (this.methods == null)
                this.methods = this.InitializeMethods();
        }

        private void CheckProperties()
        {
            if (this.properties == null)
                this.properties = this.InitializeProperties();
        }
        #endregion

        #region Initialize Members


        private IBinaryOperatorCoercionMemberDictionary<TType> InitializeBinaryOperatorCoercions()
        {
            List<string> opNames = new List<string>() { "op_Addition", "op_Subtraction", "op_Multiply", "op_Division", "op_Modulus", "op_BitwiseAnd", "op_BitwiseOr", "op_ExclusiveOr", "op_LeftShift", "op_RightShift", "op_Equality", "op_Inequality", "op_LessThan", "op_GreaterThan", "op_LessThanOrEqual", "op_GreaterThanOrEqual" };
            return new LockedBinaryOperatorCoercionMembers<TType>(this._Members, ((TType)(object)(this)),
                UnderlyingSystemType.GetMethods().Filter(m => m.IsSpecialName && opNames.Contains(m.Name)).ToArray(), methInfo => new CompiledBinaryOperatorCoercionMemberBase<TType>(methInfo, ((TType)(object)(this))));
        }

        private ITypeCoercionMemberDictionary<TType> InitializeTypeCoercions()
        {
            List<string> opNames = new List<string>() { "op_Implicit", "op_Explicit" };
            return new LockedTypeCoercionMemberDictionary<TType>(this._Members, ((TType)(object)(this)),
                UnderlyingSystemType.GetMethods().Filter(m => m.IsSpecialName && opNames.Contains(m.Name)).ToArray(), methInfo => new CompiledTypeCoercionMemberBase<TType>(methInfo, ((TType)(object)(this))));
        }

        private IUnaryOperatorCoercionMemberDictionary<TType> InitializeUnaryOperatorCoercions()
        {
            /* *
             *  +       - op_UnaryPlus
             *  -       - op_UnaryNegation
             *  false   - op_False
             *  true    - op_True
             *  !       - op_LogicalNot
             *  ~       - op_OnesComplement
             * */
            List<string> opNames = new List<string>() { "op_UnaryPlus", "op_UnaryNegation", "op_False", "op_True", "op_LogicalNot", "op_OnesComplement" };
            return new LockedUnaryOperatorCoercionMembers<TType>(this._Members, ((TType)(object)(this)),
                UnderlyingSystemType.GetMethods().Filter(m => m.IsSpecialName && opNames.Contains(m.Name)).ToArray(), methInfo => new CompiledUnaryOperatorCoercionMemberBase<TType>(methInfo, ((TType)(object)(this))));
        }

        /// <summary>
        /// Creates the <see cref="IConstructorMemberDictionary{TCtor, TCtorParent}"/> used by the
        /// <see cref="CompiledGenericInstantiableTypeBase{TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType}"/>.
        /// </summary>
        /// <returns></returns>
        private IConstructorMemberDictionary<TCtor, TType> InitializeCtors()
        {
            return new LockedConstructorMembers<TCtor, TType>(this._Members,
                ((TType)((object)(this))),
                UnderlyingSystemType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Filter(constructor =>
                {
                    var accessModifiers = constructor.GetAccessModifiers();
                    switch (accessModifiers)
                    {
                        case AccessLevelModifiers.Private:
                        case AccessLevelModifiers.PrivateScope:
                            return false;
                        case AccessLevelModifiers.InternalProtected:
                        case AccessLevelModifiers.Internal:
                        case AccessLevelModifiers.Public:
                        case AccessLevelModifiers.Protected:
                        case AccessLevelModifiers.ProtectedInternal:
                        default:
                            return true;
                    }
                }), GetConstructor);
        }

        private IEventMemberDictionary<TEvent, TType> InitializeEvents()
        {
            throw new NotImplementedException();
        }

        private IFieldMemberDictionary<TField, TType> InitializeFields()
        {
            return new LockedFieldMembersBase<TField, TType>(this._Members, ((TType)((object)(this))), UnderlyingSystemType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly).Filter(field =>
            {

                var accessModifiers = field.GetAccessModifiers();
                switch (accessModifiers)
                {
                    case AccessLevelModifiers.Private:
                    case AccessLevelModifiers.PrivateScope:
                        return false;
                    case AccessLevelModifiers.InternalProtected:
                    case AccessLevelModifiers.Internal:
                    case AccessLevelModifiers.Public:
                    case AccessLevelModifiers.Protected:
                    case AccessLevelModifiers.ProtectedInternal:
                    default:
                        return !(field.IsSpecialName || field.IsDefined(typeof(CompilerGeneratedAttribute), true));
                }
            }), this.GetField);
        }

        private IIndexerMemberDictionary<TIndexer, TType> InitializeIndexers()
        {
            throw new NotImplementedException();
        }

        private IMethodMemberDictionary<TMethod, TType> InitializeMethods()
        {
            return new LockedMethodMembersBase<TMethod, TType>(this._Members, ((TType)((object)(this))), this.UnderlyingSystemType.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly).Filter(method =>{
                var accessModifiers = method.GetAccessModifiers();
                switch (accessModifiers)
                {
                    case AccessLevelModifiers.Private:
                    case AccessLevelModifiers.PrivateScope:
                        return false;
                    case AccessLevelModifiers.InternalProtected:
                    case AccessLevelModifiers.Internal:
                    case AccessLevelModifiers.Public:
                    case AccessLevelModifiers.Protected:
                    case AccessLevelModifiers.ProtectedInternal:
                    default:
                        return !(method.IsSpecialName || method.IsDefined(typeof(CompilerGeneratedAttribute), true));
                }
            }), GetMethod);
        }

        private IPropertyMemberDictionary<TProperty, TType> InitializeProperties()
        {
            return new LockedPropertyMembersBase<TProperty, TType>(this._Members, ((TType)((object)(this))), UnderlyingSystemType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly).Filter(property =>{
                var accessModifiers = property.GetAccessModifiers();

                switch (accessModifiers)
                {
                    case AccessLevelModifiers.Private:
                    case AccessLevelModifiers.PrivateScope:
                        return false;
                    case AccessLevelModifiers.InternalProtected:
                    case AccessLevelModifiers.Internal:
                    case AccessLevelModifiers.Public:
                    case AccessLevelModifiers.Protected:
                    case AccessLevelModifiers.ProtectedInternal:
                    default:
                        return !(property.IsSpecialName || property.IsDefined(typeof(CompilerGeneratedAttribute), true) || property.GetIndexParameters().Length != 0);
                }
            }), GetProperty);
        }

        #endregion

        #region ITypeParent Members

        /// <summary>
        /// Returns the <see cref="IClassTypeDictionary"/> associated
        /// to the <see cref="TypeBase{TType}"/>.
        /// </summary>
        public IClassTypeDictionary Classes
        {
            get
            {
                CheckClasses();
                return this.classes;
            }
        }

        /// <summary>
        /// Returns the <see cref="IDelegateTypeDictionary"/> associated
        /// to the <see cref="TypeBase{TType}"/>.
        /// </summary>
        public IDelegateTypeDictionary Delegates
        {
            get
            {
                CheckDelegates();
                return this.delegates;
            }
        }

        /// <summary>
        /// Returns the <see cref="IEnumTypeDictionary"/> associated
        /// to the <see cref="TypeBase{TType}"/>.
        /// </summary>
        public IEnumTypeDictionary Enums
        {
            get
            {
                CheckEnums();
                return this.enums;
            }
        }

        /// <summary>
        /// Returns the <see cref="IInterfaceTypeDictionary"/> associated
        /// to the <see cref="TypeBase{TType}"/>.
        /// </summary>
        public IInterfaceTypeDictionary Interfaces
        {
            get
            {
                CheckInterfaces();
                return this.interfaces;
            }
        }

        /// <summary>
        /// Returns the <see cref="IStructTypeDictionary"/> associated
        /// to the <see cref="TypeBase{TType}"/>.
        /// </summary>
        public IStructTypeDictionary Structs
        {
            get
            {
                CheckStructs();
                return this.structs;
            }
        }

        /// <summary>
        /// Returns the <see cref="IFullTypeDictionary"/>  associated to
        /// the <see cref="TypeBase{TType}"/>.
        /// </summary>
        public IFullTypeDictionary Types
        {
            get
            {
                this.CheckClasses();
                this.CheckDelegates();
                this.CheckEnums();
                this.CheckInterfaces();
                this.CheckStructs();
                return this._Types;
            }
        }

        private void CheckClasses()
        {
            if (this.classes == null)
                this.classes = this.InitializeClasses();
        }

        private void CheckDelegates()
        {
            if (this.delegates == null)
                this.delegates = this.InitializeDelegates();
        }

        private void CheckEnums()
        {
            if (this.enums == null)
                this.enums = this.InitializeEnums();
        }

        private void CheckInterfaces()
        {
            if (this.interfaces == null)
                this.interfaces = this.InitializeInterfaces();
        }

        private void CheckStructs()
        {
            if (this.structs == null)
                this.structs = this.InitializeStructs();
        }

        private CompiledFullTypeDictionary _Types
        {
            get
            {
                if (this.types == null)
                    this.types = new CompiledFullTypeDictionary(this);
                return this.types;
            }
        }

        private CompiledClassTypeDictionary InitializeClasses()
        {
            return new CompiledClassTypeDictionary(this, this._Types);
        }

        private CompiledDelegateTypeDictionary InitializeDelegates()
        {
            return new CompiledDelegateTypeDictionary(this, this._Types);
        }

        private CompiledEnumTypeDictionary InitializeEnums()
        {
            return new CompiledEnumTypeDictionary(this, this._Types);
        }

        private CompiledInterfaceTypeDictionary InitializeInterfaces()
        {
            return new CompiledInterfaceTypeDictionary(this, this._Types);
        }

        private CompiledStructTypeDictionary InitializeStructs()
        {
            return new CompiledStructTypeDictionary(this, this._Types);
        }

        #endregion

        #region GetMember Methods

        /// <summary>
        /// Instantiates the <see cref="Members"/> property.
        /// </summary>
        /// <returns>A new <see cref="IFullMemberDictionary"/> 
        /// instance that contains the members of 
        /// the <see cref="CompiledGenericInstantiableTypeBase{TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType}"/>.</returns>
        protected sealed override IFullMemberDictionary OnGetMembers()
        {
            this.CheckConstructor();
            this.CheckEvents();
            this.CheckFields();
            this.CheckIndexers();
            this.CheckMethods();
            this.CheckProperties();

            return base.OnGetMembers();
        }
        
        /// <summary>
        /// Obtains a <typeparamref name="TCtor"/> for the <paramref name="info"/> 
        /// provided.
        /// </summary>
        /// <param name="info">The <see cref="ConstructorInfo"/> to obtain the <typeparamref name="TCtor"/> 
        /// for.</param>
        /// <returns>A new <typeparamref name="TCtor"/> instance with the <paramref name="info"/>
        /// provided.</returns>
        protected abstract TCtor GetConstructor(ConstructorInfo info);
        /// <summary>
        /// Obtains a <typeparamref name="TMethod"/> for the <paramref name="info"/> 
        /// provided.
        /// </summary>
        /// <param name="info">The <see cref="MethodInfo"/> to obtain the <typeparamref name="TMethod"/> 
        /// for.</param>
        /// <returns>A new <typeparamref name="TMethod"/> instance with the <paramref name="info"/>
        /// provided.</returns>
        protected abstract TMethod GetMethod(MethodInfo info);
        /// <summary>
        /// Obtains a <typeparamref name="TProperty"/> for 
        /// the <paramref name="info"/> provided.
        /// </summary>
        /// <param name="info">The <see cref="PropertyInfo"/>
        /// to obtain the <typeparamref name="TProperty"/> for.
        /// </param>
        /// <returns>A new <typeparamref name="TProperty"/>
        /// instance with the <paramref name="info"/>
        /// provided.</returns>
        protected abstract TProperty GetProperty(PropertyInfo info);
        /// <summary>
        /// Obtains a <typeparamref name="TField"/> for the 
        /// <paramref name="FieldInfo"/> provided.
        /// </summary>
        /// <param name="info">The <see cref="FieldInfo"/> 
        /// to obtain the <typeparamref name="TField"/>
        /// for.</param>
        /// <returns>A new <typeparamref name="TField"/> 
        /// with the <paramref name="info"/> provided.</returns>
        protected abstract TField GetField(FieldInfo info);
        /// <summary>
        /// Obtains a <typeparamref name="TEvent"/> for the
        /// <paramref name="info"/> provided.
        /// </summary>
        /// <param name="info">The <see cref="EventInfo"/>
        /// to obtain the <typeparamref name="TEvent"/>
        /// for.</param>
        /// <returns>A new <typeparamref name="TEvent"/> 
        /// with the <paramref name="info"/> provided.</returns>
        protected abstract TEvent GetEvent(EventInfo info);
        /// <summary>
        /// Obtains a <typeparamref name="TIndexer"/> for the
        /// <paramref name="info"/> provided.
        /// </summary>
        /// <param name="info">The <see cref="PropertyInfo"/>
        /// to obtain the <typeparamref name="TIndexer"/>
        /// for.</param>
        /// <returns>A new <typeparamref name="TIndexer"/> 
        /// with the <paramref name="info"/> provided.</returns>
        protected abstract TIndexer GetIndexer(PropertyInfo info);
        #endregion

        #region ICreatableType<TCtor,TType> Members

        /// <summary>
        /// Returns the <see cref="IConstructorMemberDictionary{TCtor, TType}"/> 
        /// contained by the 
        /// <see cref="CompiledGenericInstantiableTypeBase{TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType}"/>.
        /// </summary>
        public IConstructorMemberDictionary<TCtor, TType> Constructors
        {
            get
            {
                CheckConstructor();
                return this.constructors;
            }
        }

        #endregion

        #region IMethodParent<TMethod,TType> Members

        public IMethodMemberDictionary<TMethod, TType> Methods
        {
            get {
                CheckMethods();
                return this.methods;
            }
        }
        #endregion

        #region IPropertyParent<TProperty,TType> Members

        public IPropertyMemberDictionary<TProperty, TType> Properties
        {
            get {
                CheckProperties();
                return this.properties; }
        }
        #endregion

        #region IFieldParent<TField,TType> Members

        public IFieldMemberDictionary<TField, TType> Fields
        {
            get {
                if (this.fields == null)
                    this.fields = this.InitializeFields();
                return this.fields;
            }
        }

        #endregion

        #region ICreatableType Members

        IConstructorMemberDictionary ICreatableType.Constructors
        {
            get { return (IConstructorMemberDictionary)this.Constructors; }
        }

        #endregion

        #region IMethodParent Members

        IMethodMemberDictionary IMethodParent.Methods
        {
            get { return (IMethodMemberDictionary)this.Methods; }
        }

        #endregion

        #region IPropertyParent Members

        IPropertyMemberDictionary IPropertyParentType.Properties
        {
            get { return (IPropertyMemberDictionary)this.Properties; }
        }

        #endregion

        #region IFieldParent Members

        IFieldMemberDictionary IFieldParent.Fields
        {
            get {
                return (IFieldMemberDictionary)this.Fields;
            }
        }

        #endregion

        #region ICoercibleType<TType> Members

        /// <summary>
        /// Returns the <see cref="IBinaryOperatorCoercionMemberDictionary{TCoercionParent}"/> 
        /// assocaited to the <typeparamref name="TType"/>.
        /// </summary>
        public IBinaryOperatorCoercionMemberDictionary<TType> BinaryOperatorCoercions
        {
            get {
                if (this.binaryOperatorCoercions == null)
                    this.binaryOperatorCoercions = this.InitializeBinaryOperatorCoercions();
                return this.binaryOperatorCoercions;
            }
        }

        /// <summary>
        /// Returns the <see cref="ITypeCoercionMemberDictionary{TCoercionParent}"/> 
        /// assocaited to the <typeparamref name="TType"/>.
        /// </summary>
        public ITypeCoercionMemberDictionary<TType> TypeCoercions
        {
            get
            {
                if (this.typeCoercions == null)
                    this.typeCoercions = this.InitializeTypeCoercions();
                return this.typeCoercions;
            }
        }

        /// <summary>
        /// Returns the <see cref="IUnaryOperatorCoercionMemberDictionary{TCoercionParent}"/> 
        /// assocaited to the <typeparamref name="TType"/>.
        /// </summary>
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

        #region IInstantiableType<TCtor,TEvent,TField,TIndexer,TMethod,TProperty,TType> Members

        /// <summary>
        /// Obtains a <see cref="IInterfaceMemberMapping{TMethod, TMethodSig, TProperty, TPropertySig, TEvent, TEventSig, TIndexer, TIndexerSig, TParent, TParentSig}"/> 
        /// related to the <paramref name="type"/> provided.
        /// </summary>
        /// <param name="type">The <see cref="IInterfaceType"/> 
        /// to obtain the map of.</param>
        /// <returns>A <see cref="IInterfaceMemberMapping{TMethod, TMethodSig, TProperty, TPropertySig, TEvent, TEventSig, TIndexer, TIndexerSig, TParent, TParentSig}"/> relative
        /// to the properties and methods implemented
        /// by the <typeparamref name="TType"/> with regards
        /// to <paramref name="type"/>.</returns>
        public IInterfaceMemberMapping<TMethod, IInterfaceMethodMember, TProperty, IInterfacePropertyMember, TEvent, IInterfaceEventMember, TIndexer, IInterfaceIndexerMember, TType, IInterfaceType> GetInterfaceMap(IInterfaceType type)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            this.CheckInterfaceMap();
            foreach (IInterfaceType iit in this.interfaceMaps.Keys)
                if (iit.Equals(type))
                    return this.interfaceMaps[iit];
            throw new ArgumentException(string.Format("{0} does not implement {1}", this.Name, type.Name), "type");
        }

        #endregion

        private void CheckInterfaceMap()
        {
            if (this.interfaceMaps == null)
                this.interfaceMaps = new Dictionary<IInterfaceType, IInterfaceMemberMapping<TMethod, IInterfaceMethodMember, TProperty, IInterfacePropertyMember, TEvent, IInterfaceEventMember, TIndexer, IInterfaceIndexerMember, TType, IInterfaceType>>();
            else
                return;
            foreach (IInterfaceType iit in this.ImplementedInterfaces)
                this.interfaceMaps.Add(iit, this.OnGetInterfaceMap(iit));
        }

        /// <summary>
        /// Implements <see cref="GetInterfaceMap(IInterfaceType)"/>'s functionality.
        /// </summary>
        /// <param name="type">The <see cref="IInterfaceType"/> 
        /// to obtain the map of.</param>
        /// <returns>A <see cref="IInterfaceMemberMapping{TMethod, TMethodSig, TProperty, TPropertySig, TEvent, TEventSig, TIndexer, TIndexerSig, TParent, TParentSig}"/> relative
        /// to the properties and methods implemented
        /// by the <typeparamref name="TType"/> with regards
        /// to <paramref name="type"/>.</returns>
        protected abstract IInterfaceMemberMapping<TMethod, IInterfaceMethodMember, TProperty, IInterfacePropertyMember, TEvent, IInterfaceEventMember, TIndexer, IInterfaceIndexerMember, TType, IInterfaceType> OnGetInterfaceMap(IInterfaceType type);

        #region IEventParent<TEvent,TType> Members

        public IEventMemberDictionary<TEvent, TType> Events
        {
            get
            {
                if (this.events != null)
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

        protected override void Disposed(bool dispose)
        {
            if (dispose)
            {
                if (this.binaryOperatorCoercions != null)
                {
                    this.binaryOperatorCoercions.Dispose();
                    this.binaryOperatorCoercions = null;
                }
                if (this.fields != null)
                {
                    this.fields.Dispose();
                    this.fields = null;
                } 
                if (this.events != null)
                {
                    this.events.Dispose();
                    this.events = null;
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
                if (this.unaryOperatorCoercions != null)
                {
                    this.unaryOperatorCoercions.Dispose();
                    this.unaryOperatorCoercions = null;
                } 
                if (this.typeCoercions != null)
                {
                    this.typeCoercions.Dispose();
                    this.typeCoercions = null;
                }
                if (this.classes != null)
                {
                    this.classes.Dispose();
                    this.classes = null;
                }
                if (this.enums != null)
                {
                    this.enums.Dispose();
                    this.enums = null;
                }
                if (this.delegates != null)
                {
                    this.delegates.Dispose();
                    this.delegates = null;
                }
                if (this.interfaces != null)
                {
                    this.interfaces.Dispose();
                    this.interfaces = null;
                }
                if (this.typeInitializer != null)
                {
                    this.typeInitializer.Dispose();
                    this.typeInitializer = null;
                }
                if (this.types != null)
                    this.types = null;
            }
            base.Disposed(dispose);
        }

        #region IEventSignatureParent Members

        IEventSignatureMemberDictionary IEventSignatureParent.Events
        {
            get { return (IEventSignatureMemberDictionary)this.Events; }
        }

        #endregion

        #region IIndexerParent<TIndexer,TType> Members

        public IIndexerMemberDictionary<TIndexer, TType> Indexers
        {
            get
            {
                this.CheckIndexers();
                return this.indexers;
            }
        }

        #endregion

        #region IIndexerParent Members

        IIndexerMemberDictionary IIndexerParent.Indexers
        {
            get {
                return (IIndexerMemberDictionary)this.Indexers;
            }
        }

        #endregion

        #region ICreatableType<TCtor,TType> Members

        public TCtor TypeInitializer
        {
            get
            {
                if (object.ReferenceEquals(this.typeInitializer, null))
                {
                    if (!bCheckedInitializer)
                    {
                        this.typeInitializer = this.InitializeTypeInitializer();
                        bCheckedInitializer = true;
                    }
                }
                return this.typeInitializer;
            }
        }

        /// <summary>
        /// Initializes the type initializer for the current 
        /// <see cref="CompiledGenericInstantiableTypeBase{TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType}"/>'s
        /// <see cref="CompiledTypeBase{TType}.UnderlyingSystemType"/>
        /// </summary>
        /// <returns>A <typeparamref name="TCtor"/> instance if the current type
        /// has a non-compiler-generated type initializer.</returns>
        protected TCtor InitializeTypeInitializer()
        {
            //Should only be one...
            try
            {
                ConstructorInfo ctorData = this.UnderlyingSystemType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).Filter(method =>
                    !(method.IsDefined(typeof(CompilerGeneratedAttribute), true))).First();
                return GetConstructor(ctorData);
            }
            //No non-compiler-generated type initializers existed
            catch (InvalidOperationException)
            {
                return default(TCtor);
            }
        }

        #endregion

        #region ICreatableType Members


        IConstructorMember ICreatableType.TypeInitializer
        {
            get { return this.TypeInitializer; }
        }

        #endregion

        public Type[] UnderlyingSystemTypes
        {
            get
            {
                if (this.underlyingSystemTypes == null)
                    this.underlyingSystemTypes = this.InitializeNestedTypes();
                return this.underlyingSystemTypes;
            }
        }

        private Type[] InitializeNestedTypes()
        {
            return this.UnderlyingSystemType.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly).Filter(
                filteredType =>
                {
                    var accessibilityFlags = filteredType.GetAccessModifiers();
                    switch (accessibilityFlags)
                    {
                        case AccessLevelModifiers.Private:
                        case AccessLevelModifiers.PrivateScope:
                            return false;
                        case AccessLevelModifiers.Public:
                        case AccessLevelModifiers.Protected:
                        case AccessLevelModifiers.ProtectedInternal:
                        case AccessLevelModifiers.InternalProtected:
                        case AccessLevelModifiers.Internal:
                        default:
                            return true;
                    }
                });
        }
    }
}
