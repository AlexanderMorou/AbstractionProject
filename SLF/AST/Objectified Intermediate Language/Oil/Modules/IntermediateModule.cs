using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Modules
{
    /// <summary>
    /// Provides a base implementation of an intermediate module.
    /// </summary>
    public class IntermediateModule :
        ModuleBase,
        IIntermediateModule
    {
        /// <summary>
        /// Occurs just after the <see cref="IntermediateModule"/>
        /// is renamed.
        /// </summary>
        public event EventHandler<DeclarationNameChangedEventArgs> Renamed;

        /// <summary>
        /// Occurs just before the <see cref="IntermediateModule"/>
        /// is renamed.
        /// </summary>
        public event EventHandler<DeclarationRenamingEventArgs> Renaming;
        
        /// <summary>
        /// Data member for <see cref="Name"/>.
        /// </summary>
        private string name;

        /// <summary>
        /// Data member for <see cref="_Members"/> and 
        /// <see cref="Members"/>.
        /// </summary>
        private IntermediateFullMemberDictionary _members;
        /// <summary>
        /// Creates a new <see cref="IntermediateModule"/> with the <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="name">The string value representing the unique name of the <see cref="IntermediateModule"/>.</param>
        /// <param name="parent">The <see cref="IIntermediateAssembly"/> which contains the <see cref="IntermediateModule"/></param>
        public IntermediateModule(string name, IIntermediateAssembly parent)
            : base(parent)
        {
            this.name = name;
        }

        /// <summary>
        /// Initializes the <see cref="ModuleBase.Methods"/> property.
        /// </summary>
        /// <returns>A new <see cref="IModuleGlobalMethods"/> instance.</returns>
        protected override IModuleGlobalMethods InitializeMethods()
        {
            return new IntermediateModuleGlobalMethodDictionary(this._Members, this);
        }

        /// <summary>
        /// Initializes the <see cref="ModuleBase.Fields"/> property.
        /// </summary>
        /// <returns>A new <see cref="IModuleGlobalFields"/> instance.</returns>
        protected override IModuleGlobalFields InitializeFields()
        {
            return new IntermediateModuleGlobalFieldDictionary(this._Members, this);
        }

        /// <summary>
        /// Obtains the name of the <see cref="IntermediateModule"/>.
        /// </summary>
        /// <returns>The name of the module if it is not the manifest module;
        /// or the name of the containing assembly if it is the manifest module.</returns>
        protected override string OnGetName()
        {
            if (this.IsManifestModule)
                return this.Parent.Name;
            else
                return this.name;
        }

        /// <summary>
        /// Returns the <see cref="String"/> representing the
        /// name by which the module is uniquely referred to.
        /// </summary>
        public override string UniqueIdentifier
        {
            get { return this.Name; }
        }

        #region IIntermediateModule Members

        /// <summary>
        /// Returns the full member dictionary which contains the combined
        /// set of <see cref="Methods"/> and <see cref="Fields"/>
        /// associated to the <see cref="IntermediateModule"/>.
        /// </summary>
        public IIntermediateFullMemberDictionary Members
        {
            get {
                this.CheckMethods();
                this.CheckFields();
                return this._Members;
            }
        }


        /// <summary>
        /// Returns the global methods defined on the current <see cref="IntermediateModule"/>.
        /// </summary>
        public new IIntermediateModuleGlobalMethodDictionary Methods
        {
            get { return ((IIntermediateModuleGlobalMethodDictionary)(base.Methods)); }
        }

        /// <summary>
        /// Returns the global fields defined on the current <see cref="IIntermediateModule"/>.
        /// </summary>
        public new IIntermediateModuleGlobalFieldDictionary Fields
        {
            get { return (IIntermediateModuleGlobalFieldDictionary)base.Fields; }
        }

        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> to which the current
        /// <see cref="IntermediateModule"/> belongs.
        /// </summary>
        public new IIntermediateAssembly Parent
        {
            get { return ((IIntermediateAssembly)(base.Parent)); }
        }

        /// <summary>
        /// Returns/sets whether the current <see cref="IntermediateModule"/>
        /// is the assembly's manifest module.
        /// </summary>
        public bool IsManifestModule
        {
            get
            {
                return this.Parent.ManifestModule == this;
            }
            set
            {
                this.Parent.ManifestModule = this;
            }
        }

        #endregion

        #region IIntermediateMethodParent<IModuleGlobalMethod,IIntermediateModuleGlobalMethod,IModule,IIntermediateModule> Members

        IIntermediateMethodMemberDictionary<IModuleGlobalMethod, IIntermediateModuleGlobalMethod, IModule, IIntermediateModule> AllenCopeland.Abstraction.Slf.Oil.Members.IIntermediateMethodParent<IModuleGlobalMethod, IIntermediateModuleGlobalMethod, IModule, IIntermediateModule>.Methods
        {
            get { return this.Methods; }
        }

        #endregion

        #region IIntermediateMethodParent Members

        IIntermediateMethodMemberDictionary IIntermediateMethodParent.Methods
        {
            get { return (IIntermediateMethodMemberDictionary)this.Methods; }
        }

        #endregion

        #region IMethodParent<IModuleGlobalMethod,IModule> Members

        IMethodMemberDictionary<IModuleGlobalMethod, IModule> IMethodParent<IModuleGlobalMethod, IModule>.Methods
        {
            get { return this.Methods; }
        }

        #endregion

        #region IMethodParent Members

        IMethodMemberDictionary IMethodParent.Methods
        {
            get { return (IMethodMemberDictionary)this.Methods; }
        }

        #endregion

        #region IIntermediateFieldParent<IModuleGlobalField,IIntermediateModuleGlobalField,IModule,IIntermediateModule> Members

        IIntermediateFieldMemberDictionary<IModuleGlobalField, IIntermediateModuleGlobalField, IModule, IIntermediateModule> IIntermediateFieldParent<IModuleGlobalField, IIntermediateModuleGlobalField, IModule, IIntermediateModule>.Fields
        {
            get { return this.Fields; }
        }

        #endregion

        #region IFieldParent<IModuleGlobalField,IModule> Members

        IFieldMemberDictionary<IModuleGlobalField, IModule> IFieldParent<IModuleGlobalField, IModule>.Fields
        {
            get { return this.Fields; }
        }

        #endregion

        #region IFieldParent Members

        IFieldMemberDictionary IFieldParent.Fields
        {
            get { return ((IFieldMemberDictionary)(this.Fields)); }
        }

        #endregion

        #region IIntermediateFieldParent Members

        IIntermediateFieldMemberDictionary IIntermediateFieldParent.Fields
        {
            get { return ((IIntermediateFieldMemberDictionary)(this.Fields)); }
        }

        #endregion

        private IntermediateFullMemberDictionary _Members
        {
            get
            {
                if (this._members == null)
                    this._members = new IntermediateFullMemberDictionary(); 
                return this._members;
            }
        }


        #region IIntermediateDeclaration Members

        /// <summary>
        /// Returns/sets the name of the <see cref="IntermediateModule"/>.
        /// </summary>
        public new string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                string oN = this.Name;
                if (oN == value)
                    return;
                
                if (!this.OnRenaming(value, oN))
                    return;
                if (this.IsManifestModule)
                    this.Parent.Name = value;
                else
                    this.name = value;
                this.OnRenamed(value, oN);
            }
        }

        /// <summary>
        /// Fires the <see cref="Renamed"/> event.
        /// </summary>
        /// <param name="newValue">The <see cref="String"/> representing 
        /// the name after the change.</param>
        /// <param name="oldValue">The <see cref="String"/> representing
        /// what the name was before the change.</param>
        protected virtual void OnRenamed(string newValue, string oldValue)
        {
            if (this.Renamed != null)
                this.Renamed(this, new DeclarationNameChangedEventArgs(oldValue, newValue));
        }

        /// <summary>
        /// Fires the <see cref="Renaming"/> event and returns whether
        /// the name should change.
        /// </summary>
        /// <param name="newValue">The <see cref="String"/> representing the 
        /// value the name is being changed to.</param>
        /// <param name="oldValue">The <see cref="String"/> representing the
        /// name is before the change.</param>
        /// <returns>true if the value should change; false, otherwise.</returns>
        /// <remarks>
        /// Notes to inheritors, to ensure the event is fired, call 
        /// the base implementation.
        /// </remarks>
        protected virtual bool OnRenaming(string newValue, string oldValue)
        {
            if (this.Renaming != null)
            {
                var p = new DeclarationRenamingEventArgs(oldValue, newValue);
                this.Renaming(this, p);
                return p.Change;
            }
            return true;
        }

        #endregion

        /// <summary>
        /// Converts the current <see cref="IntermediateModule"/> into a <see cref="String"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> value which represents the <see cref="IntermediateModule"/>.</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
