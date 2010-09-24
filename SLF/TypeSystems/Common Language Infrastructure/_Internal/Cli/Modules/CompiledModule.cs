using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Modules
{
    /// <summary>
    /// Provides a base implementation of 
    /// <see cref="ICompiledModule"/>.
    /// </summary>
    internal class CompiledModule :
        ModuleBase,
        ICompiledModule
    {
        /// <summary>
        /// Data member storing the underlying module.
        /// </summary>
        private Module underlyingModule;

        internal CompiledModule(ICompiledAssembly parent, Module underlyingModule)
            : base(parent)
        {
            this.underlyingModule = underlyingModule;
        }

        protected override IModuleGlobalMethods InitializeMethods()
        {
            throw new NotImplementedException();
        }

        protected override IModuleGlobalFields InitializeFields()
        {
            throw new NotImplementedException();
        }

        protected override string OnGetName()
        {
            return this.UnderlyingModule.ScopeName;
        }
        public override string UniqueIdentifier
        {
            get { return this.Name; }
        }

        #region ICompiledModule Members

        public new ICompiledAssembly Parent
        {
            get
            {
                return ((ICompiledAssembly)(base.Parent));
            }
        }

        /// <summary>
        /// Returns the <see cref="System.Reflection.Module"/> 
        /// from which the <see cref="CompiledModule"/> is
        /// derived.
        /// </summary>
        public Module UnderlyingModule
        {
            get { return this.underlyingModule; }
        }

        #endregion


        /// <summary>
        /// Returns a <see cref="System.String"/>
        /// representing the current 
        /// <see cref="CompiledModule"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} ({1})", this.Name, this.Parent);
        }

        public override void Dispose()
        {
            this.underlyingModule = null;
            base.Dispose();
        }
    }
}
