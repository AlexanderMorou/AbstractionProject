using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Modules
{
    /// <summary>
    /// Provides a basic partial abstract implementation of
    /// <see cref="IModule"/>.
    /// </summary>
    public abstract class ModuleBase :
        DeclarationBase,
        IModule
    {
        #region Data members

        /// <summary>
        /// Data member for <see cref="Methods"/>.
        /// </summary>
        private IModuleGlobalMethods methods;
        /// <summary>
        /// Data member for <see cref="Fields"/>.
        /// </summary>
        private IModuleGlobalFields fields;
        /// <summary>
        /// Data member for <see cref="Parent"/>.
        /// </summary>
        private IAssembly parent;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="ModuleBase"/> instance
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IAssembly"/>
        /// that created the <see cref="ModuleBase"/>.</param>
        protected ModuleBase(IAssembly parent)
        {
            this.parent = parent;
        }

        #endregion

        #region IModule Members

        /// <summary>
        /// Returns the <see cref="IAssembly"/> parent
        /// associated to the current <see cref="ModuleBase"/>.
        /// </summary>
        public IAssembly Parent
        {
            get { return this.parent; }
        }

        /// <summary>
        /// Returns the global methods defined on the current <see cref="ModuleBase"/>.
        /// </summary>
        public IModuleGlobalMethods Methods
        {
            get
            {
                CheckMethods();
                return this.methods;
            }
        }

        /// <summary>
        /// Returns the global fields defined on the current <see cref="ModuleBase"/>.
        /// </summary>
        public IModuleGlobalFields Fields
        {
            get
            {
                CheckFields();
                return this.fields;
            }
        }

        internal void CheckMethods()
        {
            if (this.methods == null)
                this.methods = this.InitializeMethods();
        }

        internal void CheckFields()
        {
            if (this.fields == null)
                this.fields = this.InitializeFields();
        }

        #endregion

        #region Initialization Members
        /// <summary>
        /// Initializes the <see cref="Methods"/> property.
        /// </summary>
        /// <returns>A new <see cref="IModuleGlobalMethods"/> instance.</returns>
        protected abstract IModuleGlobalMethods InitializeMethods();

        /// <summary>
        /// Initializes the <see cref="Fields"/> property.
        /// </summary>
        /// <returns>A new <see cref="IModuleGlobalFields"/> instance.</returns>
        protected abstract IModuleGlobalFields InitializeFields();
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

        #region IFieldParent<IModuleGlobalField,IModule> Members

        IFieldMemberDictionary<IModuleGlobalField, IModule> IFieldParent<IModuleGlobalField, IModule>.Fields
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

        /// <summary>
        /// Disposes the <see cref="ModuleBase"/>.
        /// </summary>
        public override void Dispose()
        {
            if (this.methods != null)
            {
                this.methods.Dispose();
                this.methods = null;
            }
            if (this.fields != null)
            {
                this.fields.Dispose();
                this.fields = null;
            }
            this.parent = null;

        }
    }
}
