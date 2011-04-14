using System;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using System.Collections.Generic;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a base implementation of <see cref="IAssembly"/>.
    /// </summary>
    public abstract class AssemblyBase :
        IAssembly
    {
        #region AssemblyBase data members
        /// <summary>
        /// Data member for <see cref="Classes"/>.
        /// </summary>
        private IClassTypeDictionary classes;
        /// <summary>
        /// Data member for <see cref="Delegates"/>.
        /// </summary>
        private IDelegateTypeDictionary delegates;
        /// <summary>
        /// Data member for <see cref="Enums"/>
        /// </summary>
        private IEnumTypeDictionary enums;
        /// <summary>
        /// Data member for <see cref="Interfaces"/>
        /// </summary>
        private IInterfaceTypeDictionary interfaces;
        /// <summary>
        /// Data member for <see cref="Structs"/>.
        /// </summary>
        private IStructTypeDictionary structs;
        /// <summary>
        /// Data member for <see cref="Types"/>.
        /// </summary>
        internal IFullTypeDictionary types;
        /// <summary>
        /// Data member for <see cref="ManifestModule"/>.
        /// </summary>
        private IModule manifestModule;
        /// <summary>
        /// Data member for <see cref="Namespaces"/>.
        /// </summary>
        private INamespaceDictionary namespaces;
        /// <summary>
        /// Data member for <see cref="CustomAttributes"/>.
        /// </summary>
        private ICustomAttributeCollection attributes;
        /// <summary>
        /// Data member for <see cref="Modules"/>.
        /// </summary>
        private IModuleDictionary modules;
        /// <summary>
        /// Data member for <see cref="Methods"/>.
        /// </summary>
        private IMethodMemberDictionary<ITopLevelMethodMember, INamespaceParent> methods;
        /// <summary>
        /// Data member for <see cref="Fields"/>.
        /// </summary>
        private IFieldMemberDictionary<ITopLevelFieldMember, INamespaceParent> fields;
        private IFullMemberDictionary members;
        #endregion
        
        #region Protected Members

        #region Behavior

        /// <summary>
        /// Returns whether the <see cref="AssemblyBase"/> can 
        /// cache the value obtained for <see cref="ManifestModule"/>
        /// from <see cref="OnGetManifestModule"/>.
        /// </summary>
        protected abstract bool CanCacheManifestModule { get; }

        #endregion

        #region Property Implementations

        /// <summary>
        /// Obtains the <see cref="IAssemblyInformation"/> for the
        /// current <see cref="AssemblyBase"/>.
        /// </summary>
        /// <returns></returns>
        protected abstract IAssemblyInformation OnGetAssemblyInformation();

        /// <summary>
        /// Obtains the <see cref="IModule"/> that contains the
        /// manifest information.
        /// </summary>
        /// <returns>An <see cref="IModule"/> instance that contains
        /// the assembly manifest information.</returns>
        protected abstract IModule OnGetManifestModule();
        
        #endregion

        #region InitializationMembers

        /// <summary>
        /// Initializes the <see cref="IMethodMemberDictionary{TMethod, TMethodParent}"/>
        /// for holding the methods defined outside of a namespace.
        /// </summary>
        /// <returns>A new <see cref="IMethodMemberDictionary{TMethod, TMethodParent}"/> instance.</returns>
        protected abstract IMethodMemberDictionary<ITopLevelMethodMember, INamespaceParent> InitializeMethods();
        /// <summary>
        /// Initializes the <see cref="IFieldMemberDictionary{TField, TFieldParent}"/>
        /// for holding the fields defined outside of a namespace.
        /// </summary>
        /// <returns>A new <see cref="IFieldMemberDictionary{TField, TFieldParent}"/> instance.</returns>
        protected abstract IFieldMemberDictionary<ITopLevelFieldMember, INamespaceParent> InitializeFields();
        /// <summary>
        /// Initializes the <see cref="IClassTypeDictionary"/> for holding
        /// the classes defined outside of a namespace.
        /// </summary>
        /// <returns>A new <see cref="IClassTypeDictionary"/> instance.</returns>
        protected abstract IClassTypeDictionary InitializeClasses();
        /// <summary>
        /// Initializes the <see cref="IDelegateTypeDictionary"/> for holding
        /// the delegates defined outside of a namespace.
        /// </summary>
        /// <returns>A new <see cref="IDelegateTypeDictionary"/> instance.</returns>
        protected abstract IDelegateTypeDictionary InitializeDelegates();
        /// <summary>
        /// Initializes the <see cref="IEnumTypeDictionary"/> for holding
        /// the enumerations defined outside of a namespace.
        /// </summary>
        /// <returns>A new <see cref="IEnumTypeDictionary"/> instance.</returns>
        protected abstract IEnumTypeDictionary InitializeEnums();
        /// <summary>
        /// Initializes the <see cref="IInterfaceTypeDictionary"/> for holding
        /// the interfaces defined outside of a namespace.
        /// </summary>
        /// <returns>A new <see cref="IInterfaceTypeDictionary"/> instance.</returns>
        protected abstract IInterfaceTypeDictionary InitializeInterfaces();
        /// <summary>
        /// Initializes the <see cref="IStructTypeDictionary"/> for holding
        /// the data structures defined outside of a namespace.
        /// </summary>
        /// <returns>A new <see cref="IStructTypeDictionary"/> instance.</returns>
        protected abstract IStructTypeDictionary InitializeStructs();
        /// <summary>
        /// Initializes the <see cref="Types"/> property.
        /// </summary>
        /// <returns>A <see cref="IFullTypeDictionary"/> instance
        /// relative to all the types contained within the assembly
        /// within either the default namespace or in the 
        /// hierarchy level of the module the type is defined within.</returns>
        protected abstract IFullTypeDictionary InitializeTypes();

        #endregion

        #endregion

        #region Internal members

        #region Check members

        internal void CheckFields()
        {
            if (this.fields == null)
                this.fields = this.InitializeFields();
        }

        internal void CheckMethods()
        {
            if (this.methods == null)
                this.methods = this.InitializeMethods();
        }

        internal void CheckClasses()
        {
            if (this.classes == null)
                this.classes = this.InitializeClasses();
        }

        internal void CheckDelegates()
        {
            if (this.delegates == null)
                this.delegates = this.InitializeDelegates();
        }

        internal void CheckEnumerators()
        {
            if (this.enums == null)
                this.enums = this.InitializeEnums();
        }

        internal void CheckInterfaces()
        {
            if (this.interfaces == null)
                this.interfaces = this.InitializeInterfaces();
        }

        internal void CheckStructs()
        {
            if (this.structs == null)
                this.structs = this.InitializeStructs();
        }

        internal void CheckTypes()
        {
            if (this.types == null)
                this.types = this.InitializeTypes();
        }

        internal void CheckModules()
        {
            if (this.modules == null)
                this.modules = this.InitializeModules();
        }

        internal void CheckMembers()
        {
            if (this.members == null)
                this.members = this.InitializeMembers();
        }

        #endregion

        #endregion

        #region ITypeParent Members

        public abstract IEnumerable<string> AggregateIdentifiers { get; }

        /// <summary>
        /// Returns the <see cref="IClassTypeDictionary"/> associated
        /// to the <see cref="AssemblyBase"/>.
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
        /// to the <see cref="AssemblyBase"/>.
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
        /// to the <see cref="AssemblyBase"/>.
        /// </summary>
        public IEnumTypeDictionary Enums
        {
            get
            {
                CheckEnumerators();
                return this.enums;
            }
        }

        /// <summary>
        /// Returns the <see cref="IInterfaceTypeDictionary"/> associated
        /// to the <see cref="AssemblyBase"/>.
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
        /// to the <see cref="AssemblyBase"/>.
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
        /// the <see cref="AssemblyBase"/>.
        /// </summary>
        public IFullTypeDictionary Types
        {
            get
            {
                CheckTypes();
                return this.types;
            }
        }

        #endregion

        #region IAssembly Members

        /// <summary>
        /// Returns the <see cref="IAssemblyInformation"/> about the current <see cref="AssemblyBase"/>
        /// instance.
        /// </summary>
        public IAssemblyInformation AssemblyInformation
        {
            get { return this.OnGetAssemblyInformation(); }
        }

        /// <summary>
        /// Returns the <see cref="IModule"/> which exposes
        /// the manifest data for the current <see cref="AssemblyBase"/>.
        /// </summary>
        public IModule ManifestModule
        {
            get
            {
                if (this.CanCacheManifestModule)
                {
                    if (this.manifestModule == null)
                        this.manifestModule =this.OnGetManifestModule();
                    return this.manifestModule;
                }
                else
                    return this.OnGetManifestModule();
            }
        }

        public IModuleDictionary Modules
        {
            get
            {
                this.CheckModules();
                return this.modules;
            }
        }
        #endregion

        protected abstract IModuleDictionary InitializeModules();

        protected abstract IFullMemberDictionary InitializeMembers();

        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                this.manifestModule = null;
                if (this.attributes != null)
                    this.attributes.Dispose();
                if (this.namespaces != null)
                    this.namespaces.Dispose();
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
                if (this.structs != null)
                {
                    this.structs.Dispose();
                    this.structs = null;
                }
                if (this.modules != null)
                {
                    foreach (var module in this.modules.Values)
                        module.Dispose();
                    this.modules = null;
                }
                this.types = null;
            }
            finally
            {
                this.OnDisposed();
                this.Disposed = null;
                GC.SuppressFinalize(this);
            }
        }

        #endregion

        #region INamespaceParent Members

        public IFullMemberDictionary Members
        {
            get {
                this.CheckMembers();
                return this.members;
            }
        }


        /// <summary>
        /// Returns the <see cref="INamespaceDictionary"/>
        /// of <see cref="INamespaceDeclaration"/> instances
        /// contained within the <see cref="INamespaceParent"/>.
        /// </summary>
        public INamespaceDictionary Namespaces
        {
            get
            {
                if (this.namespaces == null)
                    this.namespaces = this.InitializeNamespaces();
                return this.namespaces;
            }
        }

        #endregion

        /// <summary>
        /// Initializes the <see cref="Namespaces"/> instance
        /// of the <see cref="AssemblyBase"/>.
        /// </summary>
        /// <returns>A <see cref="INamespaceDictionary"/>
        /// associated to the current
        /// <see cref="AssemblyBase"/>.</returns>
        protected abstract INamespaceDictionary InitializeNamespaces();

        #region ICustomAttributedDeclaration Members

        public ICustomAttributeCollection CustomAttributes
        {
            get
            {
                if (this.attributes == null)
                    this.attributes = this.InitializeCustomAttributes();
                return this.attributes;
            }
        }

        #endregion

        /// <summary>
        /// Initializes the <see cref="ICustomAttributeCollection"/> relative to the 
        /// current <see cref="AssemblyBase"/>.
        /// </summary>
        /// <returns>A <see cref="ICustomAttributeCollection"/> instance.</returns>
        protected abstract ICustomAttributeCollection InitializeCustomAttributes();


        #region IDeclaration Members

        string IDeclaration.Name
        {
            get { return this.AssemblyInformation.AssemblyName; }
        }

        string IDeclaration.UniqueIdentifier
        {
            get { return this.ToString(); }
        }

        /// <summary>
        /// Occurs when the <see cref="AssemblyBase"/> is disposed.
        /// </summary>
        public event EventHandler Disposed;

        #endregion

        /// <summary>
        /// Invokes the <see cref="Disposed"/> event.
        /// </summary>
        /// <remarks>Inheritors which override this should call the base definition
        /// to ensure the event is invoked.</remarks>
        protected virtual void OnDisposed()
        {
            var disposeCopy = this.Disposed;
            if (disposeCopy != null)
                disposeCopy(this, EventArgs.Empty);
        }

        #region ICustomAttributedDeclaration Members

        public bool IsDefined(IType attributeType)
        {
            foreach (var item in this.CustomAttributes)
                if (attributeType.IsAssignableFrom(item.Type))
                    return true;
            return false;
        }

        #endregion

        IAssembly ITypeParent.Assembly
        {
            get
            {
                return this;
            }
        }

        #region IMethodParent<ITopLevelMethodMember,INamespaceParent> Members

        public IMethodMemberDictionary<ITopLevelMethodMember, INamespaceParent> Methods
        {
            get
            {
                CheckMethods();
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

        #region IFieldParent<ITopLevelFieldMember,INamespaceParent> Members

        public IFieldMemberDictionary<ITopLevelFieldMember, INamespaceParent> Fields
        {
            get
            {
                CheckFields();
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
    }
}
