using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Modules;
using AllenCopeland.Abstraction.Slf.Linkers;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Utilities.Properties;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Events;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{

    /// <summary>
    /// Provides a generic base type for an intermediate assembly.
    /// </summary>
    /// <typeparam name="TLanguage">The type of the language
    /// which yields information and services pertinent to the 
    /// tools which build the assembly.</typeparam>
    /// <typeparam name="TProvider">The language service provider
    /// which yields a set of services to aid in compilation
    /// of the <typeparamref name="TAssembly">assembly</typeparamref>.</typeparam>
    /// <typeparam name="TAssembly">The kind of assembly represented by the 
    /// intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TIdentityManager">The <see cref="IIdentityManager{TTypeIdentity, TAssemblyIdentity}"/>
    /// which maintains consistent type and assembly identity.</typeparam>
    /// <typeparam name="TAssemblyIdentity">The identity used to obtain assembly references.</typeparam>
    /// <typeparam name="TTypeIdentity">The identity used to denote types within the 
    /// identity manager.</typeparam>
    public abstract partial class IntermediateAssembly<TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity> :
        AssemblyBase,
        IIntermediateAssembly<TLanguage, TProvider>
        where TLanguage :
            ILanguage
        where TProvider :
            ILanguageProvider
        where TAssembly :
            IntermediateAssembly<TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity>
        where TIdentityManager :
            IIdentityManager<TTypeIdentity, TAssemblyIdentity>
    {
        /// <summary>
        /// Data member for <see cref="Metadata"/>.
        /// </summary>
        private IMetadataDefinitionCollection attributes;
        /// <summary>
        /// Data member for <see cref="Parts"/>.
        /// </summary>
        private PartsCollection parts;
        /// <summary>
        /// Data member for <see cref="AssemblyInformation"/>.
        /// </summary>
        private IntermediateAssemblyInformation<TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity> assemblyInformation;
        /// <summary>
        /// Data member for <see cref="PrivateImplementationDetails"/>
        /// </summary>
        private PrivateImplementationDetails privateImplementationDetails;
        /// <summary>
        /// Data member for <see cref="Name"/>.
        /// </summary>
        private string name;
        /// <summary>
        /// Data member for <see cref="GetRoot"/>.
        /// </summary>
        private TAssembly rootAssembly;
        /// <summary>
        /// Data member for <see cref="ManifestModule"/>.
        /// </summary>
        private IIntermediateModule manifestModule;
        /// <summary>
        /// Data member for <see cref="Types"/>.
        /// </summary>
        private new IntermediateFullTypeDictionary types;
        private IIntermediateNamespaceDeclaration defaultNamespace;
        private IntermediateFullMemberDictionary members;
        /// <summary>
        /// Data member for <see cref="ScopeCoercions"/>.
        /// </summary>
        private IScopeCoercionCollection scopeCoercions;

        /* *
         * Placeholders for ensuring that only the root instance contains 
         * references to the event.
         * */
        private event EventHandler<DeclarationNameChangedEventArgs> _Renamed;
        private event EventHandler<DeclarationRenamingEventArgs> _Renaming;
        private IAssemblyReferenceCollection references;
        private IMalleableCompilationContext compilationContext;

        private IAssemblyUniqueIdentifier uniqueIdentifier;
        private event EventHandler<DeclarationIdentifierChangeEventArgs<IGeneralDeclarationUniqueIdentifier>> _IdentifierChanged;
        internal override object GetSyncObject()
        {
            if (this.IsRoot)
                return base.GetSyncObject();
            else
                return this.GetRoot().GetSyncObject();
        }
        event EventHandler<DeclarationIdentifierChangeEventArgs<IGeneralDeclarationUniqueIdentifier>> IIntermediateDeclaration.IdentifierChanged
        {
            add
            {
                if (this.IsRoot)
                    this._IdentifierChanged += value;
                else
                {
                    var root = this.GetRoot();
                    if (root != null)
                        root._IdentifierChanged += value;
                }
            }
            remove
            {
                if (this.IsRoot)
                    this._IdentifierChanged -= value;
                else
                {
                    var root = this.GetRoot();
                    if (root != null)
                        root._IdentifierChanged -= value;
                }
            }
        }

        IGeneralDeclarationUniqueIdentifier IDeclaration.UniqueIdentifier
        {
            get { return this.UniqueIdentifier; }
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/> with the 
        /// <paramref name="rootAssembly"/> provided.
        /// </summary>
        /// <param name="rootAssembly">The <typeparamref name="TAssembly"/> from which
        /// the current <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/> is a part of.</param>
        internal protected IntermediateAssembly(TAssembly rootAssembly)
        {
            this.rootAssembly = rootAssembly;
        }
        /// <summary>
        /// Creates a new <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/> with the
        /// <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the name of the
        /// assembly.</param>
        internal protected IntermediateAssembly(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Returns whether the manifest module of the <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>
        /// can be cached.
        /// </summary>
        /// <remarks>Returns false, the manifest module can change in an intermediate
        /// framework.</remarks>
        protected override bool CanCacheManifestModule
        {
            /* *
             * The manifest module can be changed.
             * */
            get { return false; }
        }

        /// <summary>
        /// Returns the <see cref="IAssemblyInformation"/> associated to the
        /// current <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>.
        /// </summary>
        /// <returns>An instance of <see cref="IIntermediateAssemblyInformation"/> from
        /// the new member <see cref="AssemblyInformation"/>.</returns>
        protected override IAssemblyInformation OnGetAssemblyInformation()
        {
            return this.AssemblyInformation;
        }

        /// <summary>
        /// Returns the manifest module of the assembly.
        /// </summary>
        /// <returns></returns>
        protected sealed override IModule OnGetManifestModule()
        {
            return OnGetIntermediateManifestModule();
        }

        protected virtual IIntermediateModule OnGetIntermediateManifestModule()
        {
            return this.ManifestModule;
        }

        private void Check_Types()
        {
            if (this.types == null)
            {
                if (this.IsDisposed)
                    throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                this.types = this.Initialize_Types();
            }
        }

        private void CheckAssemblyInformation()
        {
            if (this.assemblyInformation == null)
            {
                if (this.IsDisposed)
                    throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                else
                {
                    this.assemblyInformation = InitializeAssemblyInformation();
                    this.assemblyInformation.AssemblyVersionChanged += new EventHandler<EventArgsR1R2<IVersion, IVersion>>(assemblyInformation_AssemblyVersionChanged);
                    this.assemblyInformation.CultureChanged += new EventHandler<EventArgsR1R2<ICultureIdentifier, ICultureIdentifier>>(assemblyInformation_CultureChanged);
                }
            }
        }

        void assemblyInformation_CultureChanged(object sender, EventArgsR1R2<ICultureIdentifier, ICultureIdentifier> e)
        {
            if (this.uniqueIdentifier == null)
                this.OnIdentifierChanged(TypeSystemIdentifiers.GetAssemblyIdentifier(this.Name, this.AssemblyInformation.AssemblyVersion, e.Arg1, this.PublicKey), DeclarationChangeCause.Signature);
            else
            {
                var uniqueIdBackup = this.uniqueIdentifier;
                this.uniqueIdentifier = null;
                this.OnIdentifierChanged(uniqueIdBackup, DeclarationChangeCause.Signature);
            }
        }

        void assemblyInformation_AssemblyVersionChanged(object sender, EventArgsR1R2<IVersion, IVersion> e)
        {
            if (this.uniqueIdentifier == null)
                this.OnIdentifierChanged(TypeSystemIdentifiers.GetAssemblyIdentifier(this.Name, e.Arg1, this.AssemblyInformation.Culture, this.PublicKey), DeclarationChangeCause.Signature);
            else
            {
                var uniqueIdBackup = this.uniqueIdentifier;
                this.uniqueIdentifier = null;
                this.OnIdentifierChanged(uniqueIdBackup, DeclarationChangeCause.Signature);
            }
        }


        protected sealed override IModuleDictionary InitializeModules()
        {
            return InitializeIntermediateModules();
        }

        protected virtual IntermediateModuleDictionary InitializeIntermediateModules()
        {
            if (this.IsRoot)
                return new IntermediateModuleDictionary(this);
            else
                return (IntermediateModuleDictionary)this.GetRoot().Modules;
        }

        private IntermediateFullTypeDictionary Initialize_Types()
        {
            if (this.IsRoot)
                return new IntermediateFullTypeDictionary(this);
            else
                return new IntermediateFullTypeDictionary(this, this.GetRoot()._Types);
        }

        protected virtual IntermediateTopLevelMethodMemberDictionary InitializeIntermediateMethods()
        {
            if (this.IsRoot)
                return new IntermediateTopLevelMethodMemberDictionary(this._Members, this);
            else
                return new IntermediateTopLevelMethodMemberDictionary(this._Members, this, this.GetRoot().Methods);
        }

        protected virtual IntermediateTopLevelFieldMemberDictionary InitializeIntermediateFields()
        {
            if (this.IsRoot)
                return new IntermediateTopLevelFieldMemberDictionary(this._Members, this);
            else
                return new IntermediateTopLevelFieldMemberDictionary(this._Members, this, this.GetRoot().Fields);
        }

        protected virtual IntermediateClassTypeDictionary InitializeIntermediateClasses()
        {
            if (this.IsRoot)
                return new IntermediateClassTypeDictionary(this, this._Types);
            else
                return new IntermediateClassTypeDictionary(this, this._Types, this.GetRoot().Classes);
        }

        protected virtual IntermediateDelegateTypeDictionary InitializeIntermediateDelegates()
        {
            if (this.IsRoot)
                return new IntermediateDelegateTypeDictionary(this, this._Types);
            else
                return new IntermediateDelegateTypeDictionary(this, this._Types, this.GetRoot().Delegates);
        }

        protected virtual IntermediateEnumTypeDictionary InitializeIntermediateEnums()
        {
            if (this.IsRoot)
                return new IntermediateEnumTypeDictionary(this, this._Types);
            else
                return new IntermediateEnumTypeDictionary(this, this._Types, this.GetRoot().Enums);
        }

        protected virtual IntermediateInterfaceTypeDictionary InitializeIntermediateInterfaces()
        {
            if (this.IsRoot)
                return new IntermediateInterfaceTypeDictionary(this, this._Types);
            else
                return new IntermediateInterfaceTypeDictionary(this, this._Types, this.GetRoot().Interfaces);
        }

        protected virtual IntermediateStructTypeDictionary InitializeIntermediateStructs()
        {
            if (this.IsRoot)
                return new IntermediateStructTypeDictionary(this, this._Types);
            else
                return new IntermediateStructTypeDictionary(this, this._Types, this.GetRoot().Structs);
        }

        protected virtual IIntermediateFullMemberDictionary InitializeIntermediateMembers()
        {
            this.CheckFields();
            this.CheckMethods();
            return this._Members;
        }

        protected virtual IntermediateAssemblyInformation<TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity> InitializeAssemblyInformation()
        {
            return new IntermediateAssemblyInformation<TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity>((TAssembly)this);
        }

        /// <summary>

        /// Initializes the <see cref="IClassTypeDictionary"/> for holding
        /// the classes defined outside of a namespace.
        /// </summary>
        /// <returns>A new <see cref="IClassTypeDictionary"/> instance.</returns>
        protected sealed override IClassTypeDictionary InitializeClasses()
        {
            return InitializeIntermediateClasses();
        }

        /// <summary>
        /// Initializes the <see cref="IDelegateTypeDictionary"/> for holding
        /// the delegates defined outside of a namespace.
        /// </summary>
        /// <returns>A new <see cref="IDelegateTypeDictionary"/> instance.</returns>
        protected sealed override IDelegateTypeDictionary InitializeDelegates()
        {
            return InitializeIntermediateDelegates();
        }

        /// <summary>
        /// Initializes the <see cref="IEnumTypeDictionary"/> for holding
        /// the enumerations defined outside of a namespace.
        /// </summary>
        /// <returns>A new <see cref="IEnumTypeDictionary"/> instance.</returns>
        protected sealed override IEnumTypeDictionary InitializeEnums()
        {
            return InitializeIntermediateEnums();
        }

        /// <summary>
        /// Initializes the <see cref="IInterfaceTypeDictionary"/> for holding
        /// the interfaces defined outside of a namespace.
        /// </summary>
        /// <returns>A new <see cref="IInterfaceTypeDictionary"/> instance.</returns>
        protected sealed override IInterfaceTypeDictionary InitializeInterfaces()
        {
            return InitializeIntermediateInterfaces();
        }

        /// <summary>
        /// Initializes the <see cref="IStructTypeDictionary"/> for holding
        /// the data structures defined outside of a namespace.
        /// </summary>
        /// <returns>A new <see cref="IntermediateStructTypeDictionary"/> instance.</returns>
        protected sealed override IStructTypeDictionary InitializeStructs()
        {
            return InitializeIntermediateStructs();
        }

        /// <summary>
        /// Initializes the <see cref="Types"/> property.
        /// </summary>
        /// <returns>
        /// An <see cref="IntermediateFullTypeDictionary"/> instance relative 
        /// to all the types contained within the intermediate assembly within
        /// either the default namespace or in the hierarchy level of the 
        /// module the type is defined within.
        /// </returns>
        protected sealed override IFullTypeDictionary InitializeTypes()
        {
            return InitializeIntermediateTypes();
        }

        protected virtual IntermediateFullTypeDictionary InitializeIntermediateTypes()
        {
            /* *
             * Ensure that each variation of the subordinate set
             * is present
             * */
            this.CheckClasses();
            this.CheckDelegates();
            this.CheckEnumerators();
            this.CheckInterfaces();
            this.CheckStructs();
            return this._Types;
        }

        protected sealed override INamespaceDictionary InitializeNamespaces()
        {
            return InitializeIntermediateNamespaces();
        }

        protected virtual IIntermediateNamespaceDictionary InitializeIntermediateNamespaces()
        {
            if (this.IsRoot)
                return new IntermediateNamespaceDictionary(this);
            else
                return new IntermediateNamespaceDictionary(this, this.GetRoot().Namespaces);
        }

        private IntermediateFullTypeDictionary _Types
        {
            get
            {
                this.Check_Types();
                return this.types;
            }
        }

        #region IIntermediateAssembly Members

        public void Visit(IIntermediateDeclarationVisitor visitor)
        {
            visitor.Visit(this);
        }

        IIntermediateModuleDictionary IIntermediateAssembly.Modules
        {
            get
            {
                return this.Modules;
            }
        }

        public new IntermediateModuleDictionary Modules
        {
            get
            {
                return (IntermediateModuleDictionary)base.Modules;
            }
        }
        /// <summary>
        /// Returns the <see cref="IIntermediateAssemblyInformation"/> about 
        /// the current <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/> instance.
        /// </summary>
        public new IIntermediateAssemblyInformation AssemblyInformation
        {
            get
            {
                if (this.IsRoot)
                {
                    CheckAssemblyInformation();
                    return this.assemblyInformation;
                }
                else
                    return this.GetRoot().AssemblyInformation;
            }
        }

        /// <summary>
        /// Returns the <see cref="IIntermediateModule"/> which exposes
        /// the manifest data for the current 
        /// <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>.
        /// </summary>
        public new IIntermediateModule ManifestModule
        {
            get
            {
                if (this.IsRoot)
                {
                    if (this.manifestModule == null)
                        this.manifestModule = this.Modules[0].Value;
                    return this.manifestModule;
                }
                else
                    return this.GetRoot().ManifestModule;
            }
            set
            {
                if (this.IsRoot)
                {
                    if (value == null)
                        throw new ArgumentNullException("value");
                    if (value.Parent != this)
                        throw ThrowHelper.ObtainArgumentException(ArgumentWithException.value, ExceptionMessageId.ManifestModuleTarget);
                    this.manifestModule = value;
                }
                else
                    this.GetRoot().ManifestModule = value;
            }
        }

        /// <summary>
        /// Returns the <see cref="IPrivateImplementationDetails"/> part of the assembly
        /// which denotes specific features, such as defined anonymous types, 
        /// and so on.
        /// </summary>
        public IPrivateImplementationDetails PrivateImplementationDetails
        {
            get
            {
                if (this.privateImplementationDetails == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.privateImplementationDetails = new PrivateImplementationDetails(this);
                return this.privateImplementationDetails;
            }
        }

        public IIntermediateNamespaceDeclaration DefaultNamespace
        {
            get
            {
                if (this.IsRoot)
                    return this.defaultNamespace;
                else
                    return this.GetRoot().DefaultNamespace;
            }
            set
            {
                if (this.IsRoot)
                    this.defaultNamespace = value;
                else
                    this.GetRoot().DefaultNamespace = value;
            }
        }

        public new IAssemblyReferenceCollection References
        {
            get
            {
                if (this.IsRoot)
                {
                    if (this.references == null)
                        this.references = new AssemblyReferenceCollection();
                    return this.references;
                }
                else
                    return this.GetRoot().References;
            }
        }

        #endregion

        #region IIntermediateTypeParent Members

        IIntermediateClassTypeDictionary IIntermediateTypeParent.Classes
        {
            get { return this.Classes; }
        }

        IIntermediateDelegateTypeDictionary IIntermediateTypeParent.Delegates
        {
            get { return this.Delegates; }
        }

        IIntermediateEnumTypeDictionary IIntermediateTypeParent.Enums
        {
            get { return this.Enums; }
        }

        IIntermediateInterfaceTypeDictionary IIntermediateTypeParent.Interfaces
        {
            get { return this.Interfaces; }
        }

        IIntermediateStructTypeDictionary IIntermediateTypeParent.Structs
        {
            get { return this.Structs; }
        }


        IIntermediateFullTypeDictionary IIntermediateTypeParent.Types
        {
            get { return this.Types; }
        }

        public new IntermediateClassTypeDictionary Classes
        {
            get { return ((IntermediateClassTypeDictionary)(base.Classes)); }
        }

        public new IntermediateDelegateTypeDictionary Delegates
        {
            get { return ((IntermediateDelegateTypeDictionary)(base.Delegates)); }
        }

        public new IntermediateEnumTypeDictionary Enums
        {
            get { return ((IntermediateEnumTypeDictionary)(base.Enums)); }
        }

        public new IntermediateInterfaceTypeDictionary Interfaces
        {
            get { return ((IntermediateInterfaceTypeDictionary)(base.Interfaces)); }
        }

        public new IntermediateStructTypeDictionary Structs
        {
            get { return ((IntermediateStructTypeDictionary)(base.Structs)); }
        }


        public new IntermediateFullTypeDictionary Types
        {
            get { return ((IntermediateFullTypeDictionary)(base.Types)); }
        }

        #endregion

        #region IIntermediateNamespaceParent Members

        IIntermediateNamespaceDictionary IIntermediateNamespaceParent.Namespaces
        {
            get { return this.Namespaces; }
        }

        public new IntermediateNamespaceDictionary Namespaces
        {
            get { return ((IntermediateNamespaceDictionary)(base.Namespaces)); }
        }

        #endregion

        protected sealed override IMetadataCollection InitializeCustomAttributes()
        {
            return ((MetadataDefinitionCollection)(this.Metadata)).GetWrapper();
        }

        /// <summary>
        /// Disposes the current <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>.
        /// </summary>
        /// <param name="disposing">whether to dispose managed data or not.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    this.types = null;
                    if (this.attributes != null)
                    {
                        this.attributes.Dispose();
                        this.attributes = null;
                    }
                    if (this.assemblyInformation != null)
                    {
                        this.assemblyInformation.Dispose();
                        this.assemblyInformation = null;
                    }
                    if (this.privateImplementationDetails != null)
                    {
                        this.privateImplementationDetails.Dispose();
                        this.privateImplementationDetails = null;
                    }
                    this.name = null;
                    this.uniqueIdentifier = null;
                    if (this.scopeCoercions != null)
                    {
                        this.scopeCoercions.Clear();
                        this.scopeCoercions = null;
                    }
                    if (this.IsRoot)
                    {
                        if (this.parts != null)
                        {
                            foreach (var part in this.parts)
                                part.Dispose();
                            this.parts = null;
                        }
                        this.defaultNamespace = null;
                    }
                    else
                        this.rootAssembly = null;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #region IIntermediateDeclaration Members

        /// <summary>
        /// Returns/sets the name of the <see cref="IntermediateDeclarationBase{TIdentifier}"/>.
        /// </summary>
        public string Name
        {
            get
            {
                if (this.IsRoot)
                    return this.name;
                return this.GetRoot().Name;
            }
            set
            {
                if (this.IsRoot)
                {
                    if (value == this.name)
                        return;
                    DeclarationRenamingEventArgs renaming = new DeclarationRenamingEventArgs(this.name, value);
                    this.OnRenaming(renaming);
                    if (!renaming.Change)
                        return;
                    this.name = value;
                    this.OnRenamed(new DeclarationNameChangedEventArgs(renaming.OldName, renaming.NewName));
                }
                else
                    this.GetRoot().Name = value;
            }
        }


        /// <summary>
        /// Raises the <see cref="Renaming"/> event.
        /// </summary>
        /// <param name="e">The <see cref="DeclarationRenamingEventArgs"/> which
        /// indicate the old and new name of the <see cref="IntermediateDeclarationBase{TIdentifier}"/> and
        /// whether the change should take place.</param>
        protected virtual void OnRenaming(DeclarationRenamingEventArgs e)
        {
            if (this.IsRoot)
            {
                var _renaming = this._Renaming;
                if (_renaming != null)
                    _renaming(this, e);
            }
            else
                ((TAssembly)(this.GetRoot())).OnRenaming(e);
        }

        /// <summary>
        /// Raises the <see cref="Renamed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="DeclarationNameChangedEventArgs"/> which
        /// indicate the old and new name of the <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>.</param>
        protected virtual void OnRenamed(DeclarationNameChangedEventArgs e)
        {
            if (this.IsRoot)
            {
                var _renamed = this._Renamed;
                if (_renamed != null)
                    _renamed(this, e);
            }
            else
                ((TAssembly)(this.GetRoot())).OnRenamed(e);
        }

        /// <summary>
        /// Occurs when the name of the <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>
        /// has changed.
        /// </summary>
        public event EventHandler<DeclarationNameChangedEventArgs> Renamed
        {
            add
            {
                /* *
                 * Ensures that only one instance holds the event handler.
                 * */
                if (this.IsRoot)
                    this._Renamed += value;
                else
                    ((TAssembly)(this.GetRoot()))._Renamed += value;
            }
            remove
            {
                if (this.IsRoot)
                    this._Renamed -= value;
                else
                    ((TAssembly)(this.GetRoot()))._Renamed -= value;
            }
        }

        /// <summary>
        /// Occurs when the name of the <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>
        /// is in the process of being changed.
        /// </summary>
        public event EventHandler<DeclarationRenamingEventArgs> Renaming
        {
            add
            {
                /* *
                 * Ensures that only one instance holds the event handler.
                 * */
                if (this.IsRoot)
                    this._Renaming += value;
                else
                    ((TAssembly)(this.GetRoot()))._Renaming += value;
            }
            remove
            {
                if (this.IsRoot)
                    this._Renaming -= value;
                else
                    ((TAssembly)(this.GetRoot()))._Renaming -= value;
            }
        }

        #endregion

        #region IIntermediateMetadataEntity Members
        /// <summary>
        /// Returns the custom attribute definition collection series associated
        /// to the current assembly.
        /// </summary>
        public new IMetadataDefinitionCollection Metadata
        {
            get
            {
                if (this.IsRoot)
                {
                    if (this.assemblyInformation != null)
                        this.assemblyInformation.ReadyAssemblyMetaData(false);
                    return this._CustomAttributes;
                }
                else
                    return this.GetRoot().Metadata;
            }
        }

        internal IMetadataDefinitionCollection _CustomAttributes
        {
            get
            {
                if (this.IsRoot)
                {
                    if (this.attributes == null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                        else
                            this.attributes = new MetadataDefinitionCollection(this, this.IdentityManager);
                    return this.attributes;
                }
                else
                    return ((TAssembly)(this.GetRoot()))._CustomAttributes;
            }
        }

        #endregion

        protected PartsCollection _Parts
        {
            get
            {
                if (this.IsRoot)
                {
                    if (this.parts == null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                        else
                            this.parts = this.InitializeParts();
                    return this.parts;
                }
                else
                    return this.GetRoot()._Parts;
            }
        }

        #region IIntermediateSegmentableDeclaration<IIntermediateAssembly> Members

        /// <summary>
        /// Returns the part collection associated to the
        /// <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>.
        /// </summary>
        public IIntermediateSegmentableDeclarationPartCollection<IAssemblyUniqueIdentifier, IIntermediateAssembly> Parts
        {
            get
            {
                return this._Parts;
            }
        }

        private PartsCollection InitializeParts()
        {
            return new PartsCollection((TAssembly)this);
        }

        public TAssembly GetRoot()
        {
            if (this.IsRoot)
                return ((TAssembly)(this));
            else
                return this.rootAssembly;
        }

        IIntermediateAssembly IIntermediateSegmentableDeclaration<IAssemblyUniqueIdentifier, IIntermediateAssembly>.GetRoot()
        {
            return this.GetRoot();
        }

        #endregion

        #region IIntermediateSegmentableDeclaration Members

        /// <summary>
        /// Returns whether or not the current 
        /// <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/> is the root
        /// instance.
        /// </summary>
        public bool IsRoot
        {
            get
            {
                return this.rootAssembly == null;
            }
        }

        IIntermediateSegmentableDeclaration IIntermediateSegmentableDeclaration.GetRoot()
        {
            return this.GetRoot();
        }

        IIntermediateSegmentableDeclarationPartCollection IIntermediateSegmentableDeclaration.Parts
        {
            get { return this._Parts; }
        }

        #endregion
        /// <summary>
        /// Returns a <see cref="System.String"/> which 
        /// represents the current <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/>which 
        /// represents the current <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>.</returns>
        public override string ToString()
        {
            return this.UniqueIdentifier.ToString();
        }

        IIntermediateAssembly IIntermediateTypeParent.Assembly
        {
            get
            {
                return this;
            }
        }

        IIntermediateAssembly IIntermediateAssembly.Assembly
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// Returns/sets the filename associated to the present 
        /// assembly partial instance.
        /// </summary>
        /// <remarks>Used to specify the filename associated to a specific
        /// instance of an assembly.</remarks>
        public string FileName { get; set; }

        /// <summary>
        /// Returns the <see cref="IScopeCoercionCollection"/>
        /// associated to the scope coercions of the <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>.
        /// </summary>
        public IScopeCoercionCollection ScopeCoercions
        {
            get
            {
                if (this.scopeCoercions == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.scopeCoercions = new ScopeCoercionCollection();
                return this.scopeCoercions;
            }
        }

        public IMalleableCompilationContext CompilationContext
        {
            get
            {
                if (this.IsRoot)
                {
                    if (this.compilationContext == null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                        else
                            this.compilationContext = new MalleableCompilationContext();
                    return this.compilationContext;
                }
                else
                    return this.GetRoot().CompilationContext;
            }
        }

        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> of 
        /// <see cref="IGeneralDeclarationUniqueIdentifier"/> instances
        /// which denote the identifiers of the active elements within scope.
        /// </summary>
        public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get { return this.GetNamespaceParentIdentifiers(this.AreNamespacesInitialized, this.AreIntermediateTypesInitialized, this.AreIntermediateMembersInitialized); }
        }

        /// <summary>
        /// Initializes the <see cref="IMethodMemberDictionary{TMethod, TMethodParent}"/>
        /// for holding the methods defined outside of a namespace.
        /// </summary>
        /// <returns>A new <see cref="IMethodMemberDictionary{TMethod, TMethodParent}"/> instance.</returns>
        /// <remarks>Calls <see cref="InitializeIntermediateMethods()"/>.</remarks>
        protected sealed override IMethodMemberDictionary<ITopLevelMethodMember, INamespaceParent> InitializeMethods()
        {
            return InitializeIntermediateMethods();
        }

        /// <summary>
        /// Initializes the <see cref="IFieldMemberDictionary{TField, TFieldParent}"/>
        /// for holding the fields defined outside of a namespace.
        /// </summary>
        /// <returns>A new <see cref="IFieldMemberDictionary{TField, TFieldParent}"/> instance.</returns>
        /// <remarks>Calls <see cref="InitializeIntermediateFields()"/>.</remarks>
        protected sealed override IFieldMemberDictionary<ITopLevelFieldMember, INamespaceParent> InitializeFields()
        {
            return InitializeIntermediateFields();
        }

        /// <summary>
        /// Obtains the <see cref="IFullMemberDictionary"/> instance
        /// which initializes the <see cref="Members"/> of the
        /// <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>.
        /// </summary>
        /// <returns>The <see cref="IFullMemberDictionary"/>
        /// instance which initializes the <see cref="Members"/>.</returns>
        /// <remarks>Calls <see cref="InitializeIntermediateMembers()"/>.</remarks>
        protected override sealed IFullMemberDictionary InitializeMembers()
        {
            return InitializeIntermediateMembers();
        }

        /* *
         * _Members versus Members is to allow lazily initialized
         * member groups.
         * */
        private IntermediateFullMemberDictionary _Members
        {
            get
            {
                if (this.members == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    this.members = Initialize_Members();
                }
                return this.members;
            }
        }

        private IntermediateFullMemberDictionary Initialize_Members()
        {
            if (this.IsRoot)
                return new IntermediateFullMemberDictionary();
            else
                return new IntermediateFullMemberDictionary(this.GetRoot()._Members);
        }

        #region IIntermediateFieldParent<ITopLevelFieldMember,IIntermediateTopLevelFieldMember,INamespaceParent,IIntermediateNamespaceParent> Members

        IIntermediateFieldMemberDictionary<ITopLevelFieldMember, IIntermediateTopLevelFieldMember, INamespaceParent, IIntermediateNamespaceParent> IIntermediateFieldParent<ITopLevelFieldMember, IIntermediateTopLevelFieldMember, INamespaceParent, IIntermediateNamespaceParent>.Fields
        {
            get
            {
                return this.Fields;
            }
        }

        public new IntermediateTopLevelFieldMemberDictionary Fields
        {
            get
            {
                this.CheckFields();
                return (IntermediateTopLevelFieldMemberDictionary)base.Fields;
            }
        }

        #endregion

        #region IIntermediateFieldParent Members

        IIntermediateFieldMemberDictionary IIntermediateFieldParent.Fields
        {
            get { return this.Fields; }
        }

        #endregion

        #region IIntermediateMethodParent<ITopLevelMethodMember,IIntermediateTopLevelMethodMember,INamespaceParent,IIntermediateNamespaceParent> Members

        IIntermediateMethodMemberDictionary<ITopLevelMethodMember, IIntermediateTopLevelMethodMember, INamespaceParent, IIntermediateNamespaceParent> IIntermediateMethodParent<ITopLevelMethodMember, IIntermediateTopLevelMethodMember, INamespaceParent, IIntermediateNamespaceParent>.Methods
        {
            get
            {
                this.CheckMethods();
                return (IIntermediateMethodMemberDictionary<ITopLevelMethodMember, IIntermediateTopLevelMethodMember, INamespaceParent, IIntermediateNamespaceParent>)base.Methods;
            }
        }

        public new IntermediateTopLevelMethodMemberDictionary Methods
        {
            get
            {
                this.CheckMethods();
                return (IntermediateTopLevelMethodMemberDictionary)base.Methods;
            }
        }

        #endregion

        #region IIntermediateMethodParent Members

        IIntermediateMethodMemberDictionary IIntermediateMethodParent.Methods
        {
            get { return this.Methods; }
        }

        #endregion

        public new IIntermediateFullMemberDictionary Members
        {
            get
            {
                return (IIntermediateFullMemberDictionary)base.Members;
            }
        }

        private event EventHandler<DeclarationIdentifierChangeEventArgs<IAssemblyUniqueIdentifier>> identifierChanged;

        /// <summary>
        /// Occurs after the 
        /// <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>
        /// has changed in a way which invalidates the previous unique
        /// identifier.
        /// </summary>
        public event EventHandler<DeclarationIdentifierChangeEventArgs<IAssemblyUniqueIdentifier>> IdentifierChanged
        {
            add
            {
                if (this.IsRoot)
                    this.identifierChanged += value;
                else
                {
                    var root = this.GetRoot();
                    if (root != null)
                        root.identifierChanged += value;
                }
            }
            remove
            {
                if (this.IsRoot)
                    this.identifierChanged -= value;
                else
                {
                    var root = this.GetRoot();
                    if (root != null)
                        root.identifierChanged -= value;
                }
            }
        }

        public override IAssemblyUniqueIdentifier UniqueIdentifier
        {
            get
            {
                if (this.IsDisposed)
                    throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                if (this.IsRoot)
                {
                    if (this.uniqueIdentifier == null)
                        this.uniqueIdentifier = TypeSystemIdentifiers.GetAssemblyIdentifier(this.Name, this.AssemblyInformation.AssemblyVersion, this.AssemblyInformation.Culture);
                    return this.uniqueIdentifier;
                }
                else
                    return this.GetRoot().UniqueIdentifier;
            }
        }

        protected virtual void OnIdentifierChanged(IAssemblyUniqueIdentifier oldIdentifier, DeclarationChangeCause cause)
        {
            if (!this.IsRoot)
                return;
            var newIdentifier = this.UniqueIdentifier;
            var _identifierChanged = this._IdentifierChanged;
            if (_identifierChanged != null)
                _identifierChanged(this, new DeclarationIdentifierChangeEventArgs<IGeneralDeclarationUniqueIdentifier>(oldIdentifier, newIdentifier, cause));
            var identifierChanged = this.identifierChanged;
            if (identifierChanged != null)
                identifierChanged(this, new DeclarationIdentifierChangeEventArgs<IAssemblyUniqueIdentifier>(oldIdentifier, newIdentifier, cause));
        }

        public byte[] PublicKey
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Returns whether the <see cref="Members"/> have been initialized
        /// for the <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>.
        /// </summary>
        protected bool AreIntermediateMembersInitialized
        {
            get
            {
                return this.members != null;
            }
        }

        /// <summary>
        /// Returns whether the <see cref="Types"/> have been initialized
        /// for the <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>.
        /// </summary>
        protected bool AreIntermediateTypesInitialized
        {
            get
            {
                return this.types != null;
            }
        }

        #region IIntermediateAssembly<TLanguage,TProvider> Members

        public abstract TLanguage Language { get; }

        public abstract TProvider Provider { get; }

        public IStrongNamePrivateKeyInfo PrivateKeyInfo
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        ILanguageProvider IIntermediateAssembly.Provider
        {
            get
            {
                return this.Provider;
            }
        }

        ILanguage IIntermediateAssembly.Language
        {
            get
            {
                return this.Language;
            }
        }

        protected override bool CanCachePublicKeyInfo
        {
            get { return false; }
        }

        protected override IStrongNamePublicKeyInfo OnGetPublicKeyInfo()
        {
            throw new NotImplementedException();
        }

        protected override IControlledDictionary<IAssemblyUniqueIdentifier, IAssembly> InitializeReferences()
        {
            return new IntermediateAssemblyReferenceDictionary(this.References);
        }

        public override IType GetType(IGeneralTypeUniqueIdentifier identifier)
        {
            throw new NotImplementedException();
        }

        ITypeIdentityManager IIntermediateTypeParent.IdentityManager { get { return this.IdentityManager; } }

        public TIdentityManager IdentityManager
        {
            get
            {
                if (this.IsRoot)
                    return (TIdentityManager)this.Provider.IdentityManager;
                else
                    return (TIdentityManager)this.GetRoot().Provider.IdentityManager;
            }
        }

        public override IEnumerable<IType> GetTypes()
        {
            return this._Types.GetTypes().Concat(this.Namespaces.GetTypes());
        }
    }
}
