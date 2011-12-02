using System;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using System.Collections.Generic;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
        private object syncObject = new object();
        private byte isDisposed = 0;

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
        protected object SyncObject
        {
            get
            {
                return this.GetSyncObject();
            }
        }

        internal virtual object GetSyncObject()
        {
            return this.syncObject;
        }

        /// <summary>
        /// Checks the initialization status of the <see cref="Fields"/>
        /// associated to the <see cref="AssemblyBase"/>.
        /// </summary>
        internal void CheckFields()
        {
            lock (this.SyncObject)
                if (this.fields == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.fields = this.InitializeFields();
                }
        }

        /// <summary>
        /// Checks the initialization status of the <see cref="Methods"/>
        /// associated to the <see cref="AssemblyBase"/>.
        /// </summary>
        internal void CheckMethods()
        {
            lock (this.SyncObject)
                if (this.methods == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.methods = this.InitializeMethods();
                }
        }

        /// <summary>
        /// Checks the initialization status of the <see cref="Classes"/>
        /// associated to the <see cref="AssemblyBase"/>.
        /// </summary>
        internal void CheckClasses()
        {
            lock (this.SyncObject)
                if (this.classes == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.classes = this.InitializeClasses();
                }
        }

        /// <summary>
        /// Checks the initialization status of the <see cref="Delegates"/>
        /// associated to the <see cref="AssemblyBase"/>.
        /// </summary>
        internal void CheckDelegates()
        {
            lock (this.SyncObject)
                if (this.delegates == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.delegates = this.InitializeDelegates();
                }
        }

        /// <summary>
        /// Checks the initialization status of the <see cref="Enumerators"/>
        /// associated to the <see cref="AssemblyBase"/>.
        /// </summary>
        internal void CheckEnumerators()
        {
            lock (this.SyncObject)
                if (this.enums == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.enums = this.InitializeEnums();
                }
        }

        /// <summary>
        /// Checks the initialization status of the <see cref="Interfaces"/>
        /// associated to the <see cref="AssemblyBase"/>.
        /// </summary>
        internal void CheckInterfaces()
        {
            lock (this.SyncObject)
                if (this.interfaces == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.interfaces = this.InitializeInterfaces();
                }
        }

        /// <summary>
        /// Checks the initialization status of the <see cref="Structs"/>
        /// associated to the <see cref="AssemblyBase"/>.
        /// </summary>
        internal void CheckStructs()
        {
            lock (this.SyncObject)
                if (this.structs == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.structs = this.InitializeStructs();
                }
        }

        /// <summary>
        /// Checks the initialization status of the <see cref="Types"/>
        /// associated to the <see cref="AssemblyBase"/>.
        /// </summary>
        internal void CheckTypes()
        {
            lock (this.SyncObject)
                if (this.types == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.types = this.InitializeTypes();
                }
        }

        /// <summary>
        /// Checks the initialization status of the <see cref="Modules"/>
        /// associated to the <see cref="AssemblyBase"/>.
        /// </summary>
        internal void CheckModules()
        {
            lock (this.SyncObject)
                if (this.modules == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.modules = this.InitializeModules();
                }
        }

        /// <summary>
        /// Checks the initialization status of the <see cref="Members"/>
        /// associated to the <see cref="AssemblyBase"/>.
        /// </summary>
        internal void CheckMembers()
        {
            lock (this.SyncObject)
                if (this.members == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.members = this.InitializeMembers();
                }
        }

        #endregion

        #endregion

        #region ITypeParent Members

        /// <summary>
        /// Returns a series of string values which relate to the 
        /// identifiers contained within the <see cref="AssemblyBase"/>
        /// </summary>
        public abstract IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers { get; }

        /// <summary>
        /// Returns the <see cref="IClassTypeDictionary"/> associated
        /// to the <see cref="AssemblyBase"/>.
        /// </summary>
        public IClassTypeDictionary Classes
        {
            get
            {
                CheckClasses();
                lock (this.SyncObject)
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
                lock (this.SyncObject)
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
                lock (this.SyncObject)
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
                lock (this.SyncObject)
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
                lock (this.SyncObject)
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
                lock (this.SyncObject)
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
                    lock (this.SyncObject)
                        if (this.manifestModule == null)
                            this.manifestModule = this.OnGetManifestModule();
                    return this.manifestModule;
                }
                else
                    return this.OnGetManifestModule();
            }
        }

        /// <summary>
        /// Returns the <see cref="IModuleDictionary"/> which denotes
        /// the individual modules the <see cref="AssemblyBase"/>
        /// consists of.
        /// </summary>
        public IModuleDictionary Modules
        {
            get
            {
                this.CheckModules();
                return this.modules;
            }
        }
        #endregion

        /// <summary>
        /// Obtains the <see cref="IModuleDictionary"/> instance which
        /// initializes the <see cref="Modules"/> of the 
        /// <see cref="AssemblyBase"/>.
        /// </summary>
        /// <returns>A <see cref="IModuleDictionary"/> instance
        /// which initializes the <see cref="Modules"/>.</returns>
        protected abstract IModuleDictionary InitializeModules();

        /// <summary>
        /// Obtains the <see cref="IFullMemberDictionary"/> instance
        /// which initializes the <see cref="Members"/> of the
        /// <see cref="AssemblyBase"/>.
        /// </summary>
        /// <returns>The <see cref="IFullMemberDictionary"/>
        /// instance which initializes the <see cref="Members"/>.</returns>
        protected abstract IFullMemberDictionary InitializeMembers();

        #region IDisposable Members

        /// <summary>
        /// Disposes the <see cref="AssemblyBase"/> instance.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Disposes the resources and references associated to the 
        /// <see cref="AssemblyBase"/>.
        /// </summary>
        /// <param name="disposing">Whether to release the managed resources
        /// as well as the unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                lock (this.SyncObject)
                {
                    this.manifestModule = null;
                    if (this.attributes != null)
                        this.attributes.Dispose();
                    if (this.namespaces != null)
                    {
                        this.namespaces.Dispose();
                        this.namespaces = null;
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
                    this.isDisposed = 1;
                }
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

        /// <summary>
        /// Returns the <see cref="IFullMemberDictionary"/> associated to the
        /// <see cref="AssemblyBase"/> and the grouped series of members
        /// associated to the fields and methods.
        /// </summary>
        public IFullMemberDictionary Members
        {
            get
            {
                this.CheckMembers();
                return this.members;
            }
        }


        /// <summary>
        /// Returns the <see cref="INamespaceDictionary"/>
        /// of <see cref="INamespaceDeclaration"/> instances
        /// contained within the <see cref="AssemblyBase"/>.
        /// </summary>
        public INamespaceDictionary Namespaces
        {
            get
            {
                if (this.namespaces == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
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

        /// <summary>
        /// Returns the <see cref="ICustomAttributeCollection"/> 
        /// associated to the <see cref="AssemblyBase"/>.
        /// </summary>
        public ICustomAttributeCollection CustomAttributes
        {
            get
            {
                if (this.attributes == null)
                    this.attributes = this.InitializeCustomAttributes();
                return this.attributes;
            }
        }

        /// <summary>
        /// Determines whether the <paramref name="attributeType"/> 
        /// is defined on the current <see cref="AssemblyBase"/>.
        /// </summary>
        /// <param name="attributeType">The <see cref="IType"/> of
        /// the attribute to check the presence of.</param>
        /// <returns>true if an attribute of the given 
        /// <paramref name="attributeType"/> is defined
        /// on the current <see cref="AssemblyBase"/>; false,
        /// otherwise.
        /// </returns>
        public bool IsDefined(IType attributeType)
        {
            foreach (var item in this.CustomAttributes)
                if (attributeType.IsAssignableFrom(item.Type))
                    return true;
            return false;
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

        IGeneralDeclarationUniqueIdentifier IDeclaration.UniqueIdentifier
        {
            get { return this.UniqueIdentifier; }
        }

        public abstract IAssemblyUniqueIdentifier UniqueIdentifier { get; }

        /// <summary>
        /// Occurs when the <see cref="AssemblyBase"/> is disposed.
        /// </summary>
        /// <remarks>Refer to <see cref="Dispose(bool)"/> for more details.</remarks>
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

        IAssembly ITypeParent.Assembly
        {
            get
            {
                return this;
            }
        }

        #region IMethodParent<ITopLevelMethodMember,INamespaceParent> Members

        /// <summary>
        /// Returns the methods defined on the <see cref="AssemblyBase"/>.
        /// </summary>
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

        /// <summary>
        /// Returns the <see cref="IFieldMemberDictionary{TField, TParent}"/> defined on the current 
        /// <see cref="AssemblyBase"/>.
        /// </summary>
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

        /// <summary>
        /// Returns whether the <see cref="AssemblyBase"/> has been 
        /// disposed.
        /// </summary>
        /// <returns>true, if the <see cref="AssemblyBase"/> is already
        /// disposed; false, otherwise.</returns>
        public bool IsDisposed
        {
            get
            {
                lock (this.SyncObject)
                    return this.isDisposed == 1;
            }
        }

        /// <summary>
        /// Returns whether the <see cref="Classes"/> have been initialized
        /// for the <see cref="AssemblyBase"/>.
        /// </summary>
        protected bool AreClassesInitialized
        {
            get
            {
                return classes != null;
            }
        }
        /// <summary>
        /// Returns whether the <see cref="Delegates"/> have been initialized
        /// for the <see cref="AssemblyBase"/>.
        /// </summary>
        protected bool AreDelegatesInitialized
        {
            get
            {
                return delegates != null;
            }
        }

        /// <summary>
        /// Returns whether the <see cref="Enums"/> have been initialized
        /// for the <see cref="AssemblyBase"/>.
        /// </summary>
        protected bool AreEnumsInitialized
        {
            get
            {
                return enums != null;
            }
        }

        /// <summary>
        /// Returns whether the <see cref="Interfaces"/> have been initialized
        /// for the <see cref="AssemblyBase"/>.
        /// </summary>
        protected bool AreInterfacesInitialized
        {
            get
            {
                return interfaces != null;
            }
        }

        /// <summary>
        /// Returns whether the <see cref="Structs"/> have been initialized
        /// for the <see cref="AssemblyBase"/>.
        /// </summary>
        protected bool AreStructsInitialized
        {
            get
            {
                return structs != null;
            }
        }

        /// <summary>
        /// Returns whether the <see cref="Types"/> have been initialized
        /// for the <see cref="AssemblyBase"/>.
        /// </summary>
        protected bool AreTypesInitialized
        {
            get
            {
                return types != null;
            }
        }
        /// <summary>
        /// Returns whether the <see cref="ManifestModule"/> has been
        /// initialized for the <see cref="AssemblyBase"/>.
        /// </summary>
        protected bool IsManifestModuleInitialized
        {
            get
            {
                return manifestModule != null;
            }
        }

        /// <summary>
        /// Returns whether the <see cref="Namespaces"/> have been initialized
        /// for the <see cref="AssemblyBase"/>.
        /// </summary>
        protected bool AreNamespacesInitialized
        {
            get
            {
                return namespaces != null;
            }
        }
        /// <summary>
        /// Returns whether the <see cref="CustomAttributes"/> have been initialized
        /// for the <see cref="AssemblyBase"/>.
        /// </summary>
        protected bool AreCustomAttributesInitialized
        {
            get
            {
                return attributes != null;
            }
        }

        /// <summary>
        /// Returns  whether the <see cref="Modules"/> have been initialized
        /// for the <see cref="AssemblyBase"/>.
        /// </summary>
        protected bool AreModulesInitialized
        {
            get
            {
                return modules != null;
            }
        }

        /// <summary>
        /// Returns whether the <see cref="Members"/> have been initialized
        /// for the <see cref="AssemblyBase"/>.
        /// </summary>
        protected bool AreMembersInitialized
        {
            get
            {
                return members != null;
            }
        }

        /// <summary>
        /// Returns whether the <see cref="Methods"/> have been initialized
        /// for the <see cref="AssemblyBase"/>.
        /// </summary>
        protected bool AreMethodsInitialized
        {
            get
            {
                return methods != null;
            }
        }

        /// <summary>
        /// Returns whether the <see cref="Fields"/> have been initialized 
        /// for the <see cref="AssemblyBase"/>.
        /// </summary>
        protected bool AreFieldsInitialized
        {
            get
            {
                return fields != null;
            }
        }

    }
}
