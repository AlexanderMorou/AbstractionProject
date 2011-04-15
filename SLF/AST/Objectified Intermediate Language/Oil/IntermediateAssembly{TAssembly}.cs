using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
using AllenCopeland.Abstraction.Slf.Linkers;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{

    /// <summary>
    /// Provides a generic base type for an intermediate assembly.
    /// </summary>
    /// <typeparam name="TAssembly">The kind of assembly represented by the </typeparam>
    public abstract partial class IntermediateAssembly<TAssembly> :
        AssemblyBase,
        IIntermediateAssembly,
        IIntermediateSegmentableDeclaration<IIntermediateAssembly>
        where TAssembly :
            IntermediateAssembly<TAssembly>
    {
        /// <summary>
        /// Data member for <see cref="CustomAttributes"/>.
        /// </summary>
        private ICustomAttributeDefinitionCollectionSeries attributes;
        /// <summary>
        /// Data member for <see cref="Parts"/>.
        /// </summary>
        private PartsCollection parts;
        /// <summary>
        /// Data member for <see cref="AssemblyInformation"/>.
        /// </summary>
        private IntermediateAssemblyInformation<TAssembly> assemblyInformation;
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
        private IntermediateFullMemberDictionary members;
        private IScopeCoercionCollection scopeCoercions;
        /* *
         * Placeholders for ensuring that only the root instance contains 
         * references to the event.
         * */
        private event EventHandler<DeclarationNameChangedEventArgs> _Renamed;
        private event EventHandler<DeclarationRenamingEventArgs> _Renaming;
        private IAssemblyReferenceCollection references;
        private IMalleableCompilationContext compilationContext;
        /// <summary>
        /// Creates a new <see cref="IntermediateAssembly{TAssembly}"/> with the 
        /// <paramref name="rootAssembly"/> provided.
        /// </summary>
        /// <param name="rootAssembly">The <typeparamref name="TAssembly"/> from which
        /// the current <see cref="IntermediateAssembly{TAssembly}"/> is a part of.</param>
        internal protected IntermediateAssembly(TAssembly rootAssembly)
        {
            this.rootAssembly = rootAssembly;
        }
        /// <summary>
        /// Creates a new <see cref="IntermediateAssembly{TAssembly}"/> with the
        /// <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the name of the
        /// assembly.</param>
        internal protected IntermediateAssembly(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Returns whether the manifest module of the <see cref="IntermediateAssembly{TAssembly}"/>
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
        /// current <see cref="IntermediateAssembly{TAssembly}"/>.
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
        protected override IModule OnGetManifestModule()
        {
            return this.ManifestModule;
        }

        private void Check_Types()
        {
            if (this.types == null)
                this.types = this.Initialize_Types();
        }

        protected override IModuleDictionary InitializeModules()
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

        /// <summary>
        /// Initializes the <see cref="IClassTypeDictionary"/> for holding
        /// the classes defined outside of a namespace.
        /// </summary>
        /// <returns>A new <see cref="IClassTypeDictionary"/> instance.</returns>
        protected override IClassTypeDictionary InitializeClasses()
        {
            if (this.IsRoot)
                return new IntermediateClassTypeDictionary(this, this._Types);
            else
                return new IntermediateClassTypeDictionary(this, this._Types, (IntermediateClassTypeDictionary)this.GetRoot().Classes);
        }

        /// <summary>
        /// Initializes the <see cref="IDelegateTypeDictionary"/> for holding
        /// the delegates defined outside of a namespace.
        /// </summary>
        /// <returns>A new <see cref="IDelegateTypeDictionary"/> instance.</returns>
        protected override IDelegateTypeDictionary InitializeDelegates()
        {
            if (this.IsRoot)
                return new IntermediateDelegateTypeDictionary(this, this._Types);
            else
                return new IntermediateDelegateTypeDictionary(this, this._Types, (IntermediateDelegateTypeDictionary)this.GetRoot().Delegates);
        }

        /// <summary>
        /// Initializes the <see cref="IEnumTypeDictionary"/> for holding
        /// the enumerations defined outside of a namespace.
        /// </summary>
        /// <returns>A new <see cref="IEnumTypeDictionary"/> instance.</returns>
        protected override IEnumTypeDictionary InitializeEnums()
        {
            if (this.IsRoot)
                return new IntermediateEnumTypeDictionary(this, this._Types);
            else
                return new IntermediateEnumTypeDictionary(this, this._Types, (IntermediateEnumTypeDictionary)this.GetRoot().Enums);
        }

        /// <summary>
        /// Initializes the <see cref="IInterfaceTypeDictionary"/> for holding
        /// the interfaces defined outside of a namespace.
        /// </summary>
        /// <returns>A new <see cref="IInterfaceTypeDictionary"/> instance.</returns>
        protected override IInterfaceTypeDictionary InitializeInterfaces()
        {
            if (this.IsRoot)
                return new IntermediateInterfaceTypeDictionary(this, this._Types);
            else
                return new IntermediateInterfaceTypeDictionary(this, this._Types, (IntermediateInterfaceTypeDictionary)this.GetRoot().Interfaces);
        }

        /// <summary>
        /// Initializes the <see cref="IStructTypeDictionary"/> for holding
        /// the data structures defined outside of a namespace.
        /// </summary>
        /// <returns>A new <see cref="IntermediateStructTypeDictionary"/> instance.</returns>
        protected override IStructTypeDictionary InitializeStructs()
        {
            if (this.IsRoot)
                return new IntermediateStructTypeDictionary(this, this._Types);
            else
                return new IntermediateStructTypeDictionary(this, this._Types, (IntermediateStructTypeDictionary)this.GetRoot().Structs);
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
        protected override IFullTypeDictionary InitializeTypes()
        {
            return this._Types;
        }

        protected override INamespaceDictionary InitializeNamespaces()
        {
            if (this.IsRoot)
                return new IntermediateNamespaceDictionary(this);
            else
                return new IntermediateNamespaceDictionary(this, ((IntermediateNamespaceDictionary)((this.GetRoot()).Namespaces)));
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

        public new IIntermediateModuleDictionary Modules
        {
            get
            {
                return (IIntermediateModuleDictionary)base.Modules;
            }
        }

        /// <summary>
        /// Returns the <see cref="IIntermediateAssemblyInformation"/> about 
        /// the current <see cref="IntermediateAssembly{TAssembly}"/> instance.
        /// </summary>
        public new IIntermediateAssemblyInformation AssemblyInformation
        {
            get
            {
                if (this.IsRoot)
                {
                    if (this.assemblyInformation == null)
                        this.assemblyInformation = new IntermediateAssemblyInformation<TAssembly>((TAssembly)this);
                    return this.assemblyInformation;
                }
                else
                    return this.GetRoot().AssemblyInformation;
            }
        }

        /// <summary>
        /// Returns the <see cref="IIntermediateModule"/> which exposes
        /// the manifest data for the current 
        /// <see cref="IntermediateAssembly{TAssembly}"/>.
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
                        throw new ArgumentException("Target manifest module must belong to the current assembly.");
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
                    this.privateImplementationDetails = new PrivateImplementationDetails(this);
                return this.privateImplementationDetails;
            }
        }

        public IIntermediateNamespaceDeclaration DefaultNamespace { get; set; }

        public IAssemblyReferenceCollection References
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

        public new IIntermediateClassTypeDictionary Classes
        {
            get { return ((IIntermediateClassTypeDictionary)(base.Classes)); }
        }

        public new IIntermediateDelegateTypeDictionary Delegates
        {
            get { return ((IIntermediateDelegateTypeDictionary)(base.Delegates)); }
        }

        public new IIntermediateEnumTypeDictionary Enums
        {
            get { return ((IIntermediateEnumTypeDictionary)(base.Enums)); }
        }

        public new IIntermediateInterfaceTypeDictionary Interfaces
        {
            get { return ((IIntermediateInterfaceTypeDictionary)(base.Interfaces)); }
        }

        public new IIntermediateStructTypeDictionary Structs
        {
            get { return ((IIntermediateStructTypeDictionary)(base.Structs)); }
        }


        public new IIntermediateFullTypeDictionary Types
        {
            get { return ((IIntermediateFullTypeDictionary)(base.Types)); }
        }

        #endregion

        #region IIntermediateNamespaceParent Members

        public new IIntermediateNamespaceDictionary Namespaces
        {
            get { return ((IIntermediateNamespaceDictionary)(base.Namespaces)); }
        }

        #endregion

        protected override ICustomAttributeCollection InitializeCustomAttributes()
        {
            return ((CustomAttributeDefinitionCollectionSeries)(this.CustomAttributes)).GetWrapper();
        }

        /// <summary>
        /// Disposes the current <see cref="IntermediateAssembly{TAssembly}"/>.
        /// </summary>
        /// <param name="disposing">whether to dispose managed data or not.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (this.attributes != null)
                    {
                        this.attributes.Dispose();
                        this.attributes = null;
                    }
                    if (this.assemblyInformation != null)
                        this.assemblyInformation.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #region IIntermediateDeclaration Members

        /// <summary>
        /// Returns/sets the name of the <see cref="IntermediateDeclarationBase"/>.
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
        /// indicate the old and new name of the <see cref="IntermediateDeclarationBase"/> and
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
        /// indicate the old and new name of the <see cref="IntermediateAssembly{TAssembly}"/>.</param>
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
        /// Occurs when the name of the <see cref="IntermediateAssembly{TAssembly}"/>
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
        /// Occurs when the name of the <see cref="IntermediateAssembly{TAssembly}"/>
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

        #region IIntermediateCustomAttributedDeclaration Members
        /// <summary>
        /// Returns the custom attribute definition collection series associated
        /// to the current assembly.
        /// </summary>
        public new ICustomAttributeDefinitionCollectionSeries CustomAttributes
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
                    return this.GetRoot().CustomAttributes;
            }
        }

        internal ICustomAttributeDefinitionCollectionSeries _CustomAttributes
        {
            get
            {
                if (this.IsRoot)
                {
                    if (this.attributes == null)
                        this.attributes = new CustomAttributeDefinitionCollectionSeries(this);
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
        /// <see cref="IntermediateAssembly{TAssembly}"/>.
        /// </summary>
        public IIntermediateSegmentableDeclarationPartCollection<IIntermediateAssembly> Parts
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

        IIntermediateAssembly IIntermediateSegmentableDeclaration<IIntermediateAssembly>.GetRoot()
        {
            return this.GetRoot();
        }

        #endregion

        #region IIntermediateSegmentableDeclaration Members

        /// <summary>
        /// Returns whether or not the current 
        /// <see cref="IntermediateAssembly{TAssembly}"/> is the root
        /// instance.
        /// </summary>
        public bool IsRoot
        {
            get { return this.rootAssembly == null; }
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
        /// represents the current <see cref="IntermediateAssembly{TAssembly}"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/>which 
        /// represents the current <see cref="IntermediateAssembly{TAssembly}"/>.</returns>
        public override string ToString()
        {
            return string.Format("{0}, Version={1}, Culture={2}, PublicKeyToken={3}", this.AssemblyInformation.AssemblyName, this.AssemblyInformation.AssemblyVersion, string.IsNullOrEmpty(this.AssemblyInformation.Culture.Name) ? "neutral" : this.AssemblyInformation.Culture.Name, "null");
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

        public IScopeCoercionCollection ScopeCoercions
        {
            get
            {
                if (this.scopeCoercions == null)
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
                        this.compilationContext = new MalleableCompilationContext();
                    return this.compilationContext;
                }
                else
                    return this.GetRoot().CompilationContext;
            }
        }

        public override IEnumerable<string> AggregateIdentifiers
        {
            get { return this.GetNamespaceParentIdentifiers(); }
        }

        protected sealed override IMethodMemberDictionary<ITopLevelMethodMember, INamespaceParent> InitializeMethods()
        {
            if (this.IsRoot)
                return new IntermediateTopLevelMethodMemberDictionary(this._Members, this);
            else
                return new IntermediateTopLevelMethodMemberDictionary(this._Members, this, this.GetRoot().Methods as IntermediateTopLevelMethodMemberDictionary);
        }

        protected sealed override IFieldMemberDictionary<ITopLevelFieldMember, INamespaceParent> InitializeFields()
        {
            if (this.IsRoot)
                return new IntermediateTopLevelFieldMemberDictionary(this._Members, this);
            else
                return new IntermediateTopLevelFieldMemberDictionary(this._Members, this, this.GetRoot().Fields as IntermediateTopLevelFieldMemberDictionary);
        }

        protected override IFullMemberDictionary InitializeMembers()
        {
            this.CheckFields();
            this.CheckMethods();
            return this._Members;
        }

        private IntermediateFullMemberDictionary _Members
        {
            get
            {
                if (this.members == null)
                    this.members = new IntermediateFullMemberDictionary();
                return this.members;
            }
        }

        #region IIntermediateFieldParent<ITopLevelFieldMember,IIntermediateTopLevelFieldMember,INamespaceParent,IIntermediateNamespaceParent> Members

        public new IIntermediateFieldMemberDictionary<ITopLevelFieldMember, IIntermediateTopLevelFieldMember, INamespaceParent, IIntermediateNamespaceParent> Fields
        {
            get
            {
                this.CheckFields();
                return (IIntermediateFieldMemberDictionary<ITopLevelFieldMember, IIntermediateTopLevelFieldMember, INamespaceParent, IIntermediateNamespaceParent>)base.Fields;
            }
        }

        #endregion

        #region IFieldParent<ITopLevelFieldMember,INamespaceParent> Members

        IFieldMemberDictionary<ITopLevelFieldMember, INamespaceParent> IFieldParent<ITopLevelFieldMember, INamespaceParent>.Fields
        {
            get { return this.Fields; }
        }

        #endregion

        #region IFieldParent Members

        IFieldMemberDictionary IFieldParent.Fields
        {
            get { return (IFieldMemberDictionary)this.Fields; }
        }

        #endregion

        #region IIntermediateFieldParent Members

        IIntermediateFieldMemberDictionary IIntermediateFieldParent.Fields
        {
            get { return (IIntermediateFieldMemberDictionary)this.Fields; }
        }

        #endregion

        #region IIntermediateMethodParent<ITopLevelMethodMember,IIntermediateTopLevelMethodMember,INamespaceParent,IIntermediateNamespaceParent> Members

        public new IIntermediateMethodMemberDictionary<ITopLevelMethodMember, IIntermediateTopLevelMethodMember, INamespaceParent, IIntermediateNamespaceParent> Methods
        {
            get
            {
                this.CheckMethods();
                return (IIntermediateMethodMemberDictionary<ITopLevelMethodMember, IIntermediateTopLevelMethodMember, INamespaceParent, IIntermediateNamespaceParent>)base.Methods;
            }
        }

        #endregion

        #region IIntermediateMethodParent Members

        IIntermediateMethodMemberDictionary IIntermediateMethodParent.Methods
        {
            get { return (IIntermediateMethodMemberDictionary)this.Methods; }
        }

        #endregion

        #region IMethodParent Members

        IMethodMemberDictionary IMethodParent.Methods
        {
            get { return (IMethodMemberDictionary)this.Methods; }
        }

        #endregion

        #region IMethodParent<ITopLevelMethodMember,INamespaceParent> Members

        IMethodMemberDictionary<ITopLevelMethodMember, INamespaceParent> IMethodParent<ITopLevelMethodMember, INamespaceParent>.Methods
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
    }
}
