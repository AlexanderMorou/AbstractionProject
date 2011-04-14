using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal sealed partial class CompiledInterfaceType :
        CompiledGenericTypeBase<IInterfaceType>,
        ICompiledInterfaceType,
        IEventSignatureParent,
        _ICompiledTypeParent
    {
        private IMethodSignatureMemberDictionary<IInterfaceMethodMember, IInterfaceType> methods;
        private IPropertySignatureMemberDictionary<IInterfacePropertyMember, IInterfaceType> properties;
        private IEventSignatureMemberDictionary<IInterfaceEventMember, IInterfaceType> events;
        private IIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IInterfaceType> indexers;
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
        private Type[] underlyingSystemTypes;

        /// <summary>
        /// Creates a new <see cref="CompiledInterfaceType"/> with the 
        /// <paramref name="underlyingSystemType"/> provided.
        /// </summary>
        /// <param name="underlyingSystemType">The <see cref="System.Type"/> from which the current
        /// <see cref="CompiledInterfaceType"/> is based.</param>
        internal CompiledInterfaceType(Type underlyingSystemType) 
            : base(underlyingSystemType)
        {
        }

        protected override IInterfaceType OnMakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            return new _InterfaceTypeBase(this, typeParameters);
        }

        #region IMethodSignatureParent<IInterfaceMethodMember,IInterfaceType> Members

        public IMethodSignatureMemberDictionary<IInterfaceMethodMember, IInterfaceType> Methods
        {
            get
            {
                MethodsCheck();
                return this.methods;
            }
        }

        private IMethodSignatureMemberDictionary<IInterfaceMethodMember, IInterfaceType> InitializeMethods()
        {
            return new LockedMethodSignatureMembersBase<IInterfaceMethodMember, IInterfaceType>(((LockedFullMembersBase)(this._Members)),
                ((IInterfaceType)(this)), UnderlyingSystemType.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static).Filter(method =>
                !method.IsSpecialName), new Func<MethodInfo,IInterfaceMethodMember>(GetMethod));
        }

        private IEventSignatureMemberDictionary<IInterfaceEventMember, IInterfaceType> InitializeEvents()
        {
            return new LockedEventSignatureMembersBase<IInterfaceEventMember, IInterfaceType>(this._Members, this, UnderlyingSystemType.GetEvents(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static).Filter(@event => !(@event.IsSpecialName || ((@event.Attributes & EventAttributes.RTSpecialName) == EventAttributes.RTSpecialName))), this.GetEvent);
        }

        private IIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IInterfaceType> InitializeIndexers()
        {
            return new LockedIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IInterfaceType>(this._Members, ((IInterfaceType)((object)(this))), UnderlyingSystemType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly).Filter(property =>
                !(property.IsSpecialName || property.IsDefined(typeof(CompilerGeneratedAttribute), true)) && property.GetIndexParameters().Length > 0), GetIndexer);
        }

        private IInterfaceEventMember GetEvent(EventInfo source)
        {
            return new EventMember(source, this);
        }

        private IInterfacePropertyMember GetProperty(PropertyInfo source)
        {
            return new PropertyMember(source, this);
        }

        private IInterfaceMethodMember GetMethod(MethodInfo source)
        {
            return new MethodMember(source, this);
        }

        private IInterfaceIndexerMember GetIndexer(PropertyInfo source)
        {
            return new IndexerMember(source, this);
        }

        #endregion

        #region IMethodSignatureParent Members

        IMethodSignatureMemberDictionary IMethodSignatureParent.Methods
        {
            get { return (IMethodSignatureMemberDictionary)this.Methods; }
        }

        #endregion

        protected override IFullMemberDictionary OnGetMembers()
        {
            this.EventsCheck();
            this.IndexersCheck();
            this.MethodsCheck();
            this.PropertiesCheck();
            return base.OnGetMembers();
        }

        private void EventsCheck()
        {
            if (this.events == null)
                this.events = this.InitializeEvents();
        }

        private void MethodsCheck()
        {
            if (this.methods == null)
                this.methods = this.InitializeMethods();
        }

        private void IndexersCheck()
        {
            if (this.indexers == null)
                this.indexers = this.InitializeIndexers();
        }

        #region IEventSignatureParent<IInterfaceEventMember,IEventSignatureParameterMember<IInterfaceEventMember,IInterfaceType>,IInterfaceType> Members

        IEventSignatureMemberDictionary<IInterfaceEventMember, IEventSignatureParameterMember<IInterfaceEventMember, IInterfaceType>, IInterfaceType> IEventSignatureParent<IInterfaceEventMember,IEventSignatureParameterMember<IInterfaceEventMember,IInterfaceType>,IInterfaceType>.Events
        {
            get {
                return this.Events;
            }
        }

        #endregion

        #region IEventSignatureParent Members

        IEventSignatureMemberDictionary IEventSignatureParent.Events
        {
            get { return (IEventSignatureMemberDictionary)this.Events; }
        }

        #endregion

        #region IIndexerSignatureParent<IInterfaceIndexerMember,IInterfaceType> Members

        public IIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IInterfaceType> Indexers
        {
            get
            {
                this.IndexersCheck();
                return this.indexers;
            }
        }

        #endregion

        #region IIndexerSignatureParent Members

        IIndexerSignatureMemberDictionary IIndexerSignatureParent.Indexers
        {
            get { return (IIndexerSignatureMemberDictionary)this.Indexers; }
        }

        #endregion

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Interface; }
        }

        #region IPropertySignatureParentType<IInterfacePropertyMember,IInterfaceType> Members

        public IPropertySignatureMemberDictionary<IInterfacePropertyMember, IInterfaceType> Properties
        {
            get
            {
                this.PropertiesCheck();
                return this.properties;
            }
        }

        private void PropertiesCheck()
        {
            if (this.properties == null)
                this.properties = this.InitializeProperties();
        }

        private IPropertySignatureMemberDictionary<IInterfacePropertyMember, IInterfaceType> InitializeProperties()
        {
            return new LockedPropertySignatureMemberDictionary<IInterfacePropertyMember, IInterfaceType>(this._Members, ((IInterfaceType)((object)(this))), UnderlyingSystemType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly).Filter(property =>
                !(property.IsSpecialName || property.IsDefined(typeof(CompilerGeneratedAttribute), true) || property.GetIndexParameters().Length != 0)), GetProperty);
        }

        #endregion

        #region IPropertySignatureParentType

        IPropertySignatureMemberDictionary IPropertySignatureParentType.Properties
        {
            get
            {
                return ((IPropertySignatureMemberDictionary)(this.Properties));
            }
        }

        #endregion

        public IEventSignatureMemberDictionary<IInterfaceEventMember, IInterfaceType> Events
        {
            get
            {
                this.EventsCheck();
                return this.events;
            }
        }

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

        #region _ICompiledTypeParent Members

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
        #endregion

        protected override IEnumerable<IDeclaration> OnGetDeclarations()
        {
            return GetTypeParentDeclarations(this);
        }

        public override IEnumerable<string> AggregateIdentifiers
        {
            get
            {
                return (from memberName in (this.Members as LockedFullMembersBase).GetAggregateIdentifiers()
                        select memberName).Concat(
                        from typeName in (this.Types as CompiledFullTypeDictionary).GetAggregateIdentifiers()
                        select typeName).Distinct();
            }
        }
    }
}
