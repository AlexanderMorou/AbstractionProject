using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal partial class _InterfaceTypeBase :
        _GenericTypeBase<IGeneralGenericTypeUniqueIdentifier, IInterfaceType>,
        IInterfaceType
    {
        private bool checkedFullMembers;
        private _FullMembersBase _members;
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
        private IPropertySignatureMemberDictionary<IInterfacePropertyMember, IInterfaceType> properties;
        private IEventSignatureMemberDictionary<IInterfaceEventMember, IInterfaceType> events;
        private IMethodSignatureMemberDictionary<IInterfaceMethodMember, IInterfaceType> methods;
        private IIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IInterfaceType> indexers;
        internal _InterfaceTypeBase(IInterfaceType original, IControlledTypeCollection genericParameters)
            : base(original, genericParameters)
        {
        }

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Interface; }
        }

        protected override IFullMemberDictionary OnGetMembers()
        {
            this.CheckMembers();
            return this._Members;
        }


        private _FullMembersBase _Members
        {
            get
            {
                this.Check_Members();
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
            if (this._members == null)
                this._members = this.Initialize_Members();
        }

        /// <summary>
        /// Returns the <see cref="_members"/>'s default value.
        /// </summary>
        /// <remarks>
        /// Used by <see cref="Check_Members"/>.
        /// </remarks>
        /// <returns>
        /// A <see cref="IFullMemberDictionary"/> instance.
        /// </returns>
        private _FullMembersBase Initialize_Members()
        {
            return new _FullMembersBase();
        }


        private void CheckMembers()
        {
            if (!this.checkedFullMembers)
            {
                this.Check_Members();
                this.CheckEvents();
                this.CheckIndexers();
                this.CheckMethods();
                this.CheckProperties();
                this.checkedFullMembers = true;
            }
        }

        #region IMethodSignatureParent<IInterfaceMethodMember,IInterfaceType> Members

        public IMethodSignatureMemberDictionary<IInterfaceMethodMember, IInterfaceType> Methods
        {
            get
            {
                this.CheckMethods();
                return this.methods;
            }
        }

        private void CheckMethods()
        {
            if (this.methods == null)
                this.methods = this.InitializeMethods();
        }

        private IMethodSignatureMemberDictionary<IInterfaceMethodMember, IInterfaceType> InitializeMethods()
        {
            return new _MethodsBase(this._Members, this.Original.Methods, this);
        }

        #endregion

        #region IMethodSignatureParent Members

        IMethodSignatureMemberDictionary IMethodSignatureParent.Methods
        {
            get { return ((IMethodSignatureMemberDictionary)(this.Methods)); }
        }

        #endregion

        #region IEventSignatureParent<IInterfaceEventMember,IEventSignatureParameterMember<IInterfaceEventMember,IInterfaceType>,IInterfaceType> Members

        IEventSignatureMemberDictionary<IInterfaceEventMember, IEventSignatureParameterMember<IInterfaceEventMember, IInterfaceType>, IInterfaceType> IEventSignatureParent<IInterfaceEventMember, IEventSignatureParameterMember<IInterfaceEventMember, IInterfaceType>, IInterfaceType>.Events
        {
            get
            {
                return this.Events;
            }
        }

        private void CheckEvents()
        {
            if (this.events == null)
                this.events = InitializeEvents();
        }

        private IEventSignatureMemberDictionary<IInterfaceEventMember, IInterfaceType> InitializeEvents()
        {
            return new _Events(this._Members, this.Original.Events, this);
        }

        #endregion

        #region IEventSignatureParent Members

        IEventSignatureMemberDictionary IEventSignatureParent.Events
        {
            get {return ((IEventSignatureMemberDictionary)(this.Events)); }
        }

        #endregion

        #region IIndexerSignatureParent<IInterfaceIndexerMember,IInterfaceType> Members

        public IIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IInterfaceType> Indexers
        {
            get
            {
                this.CheckIndexers();
                return this.indexers;
            }
        }

        private void CheckIndexers()
        {
            if (this.indexers == null)
                this.indexers = this.InitializeIndexers();
        }

        #endregion

        private IIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IInterfaceType> InitializeIndexers()
        {
            return new _IndexersBase(this._Members, this.Original.Indexers, this);
        }

        #region IIndexerSignatureParent Members

        IIndexerSignatureMemberDictionary IIndexerSignatureParent.Indexers
        {
            get { return ((IIndexerSignatureMemberDictionary)(this.Indexers)); }
        }

        #endregion

        #region IPropertySignatureParent<IInterfacePropertyMember,IInterfaceType> Members

        public IPropertySignatureMemberDictionary<IInterfacePropertyMember, IInterfaceType> Properties
        {
            get
            {
                this.CheckProperties();
                return this.properties;
            }
        }

        private void CheckProperties()
        {
            if (this.properties == null)
                this.properties = this.InitializeProperties();
        }

        private IPropertySignatureMemberDictionary<IInterfacePropertyMember, IInterfaceType> InitializeProperties()
        {
            return new _PropertiesBase(this._Members, this.Original.Properties, this);
        }

        #endregion

        #region IPropertySignatureParent Members

        IPropertySignatureMemberDictionary IPropertySignatureParent.Properties
        {
            get { return ((IPropertySignatureMemberDictionary)(this.Properties)); }
        }

        #endregion

        #region IEventSignatureParent<IInterfaceEventMember,IInterfaceType> Members

        public IEventSignatureMemberDictionary<IInterfaceEventMember, IInterfaceType> Events
        {
            get
            {
                this.CheckEvents();
                return this.events;
            }
        }

        #endregion

        #region ITypeParent Members

        public IClassTypeDictionary Classes
        {
            get
            {
                this.CheckClasses();
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
            if (this.classes == null)
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
            if (this.delegates == null)
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
            if (this.enums == null)
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
            if (this.interfaces == null)
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
            if (this.structs == null)
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
            if (this.types == null)
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

        protected override IEnumerable<IDeclaration> OnGetDeclarations()
        {
            return GetTypeParentDeclarations(this);
        }

        protected override IGeneralGenericTypeUniqueIdentifier OnGetUniqueIdentifier()
        {
            return this.Original.UniqueIdentifier;
        }
    }
}
