using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledAssembly
    {
        private partial class ModuleDictionary :
            ControlledStateDictionary<string, IModule>,
            IModuleDictionary
        {
            private CompiledAssembly parent;
            private Module[] moduleData;
            private ICompiledModule[] moduleCopy;
            public ModuleDictionary(CompiledAssembly parent)
            {
                this.moduleData = parent.UnderlyingAssembly.GetModules(true);
                this.moduleCopy = new ICompiledModule[this.moduleData.Length];
                
                this.parent = parent;
            }

            #region IModuleDictionary Members

            public IAssembly Parent
            {
                get { return this.parent; }
            }

            #endregion

            internal void CheckItemAt(int i)
            {
                if (this.moduleCopy[i] == null)
                    this.moduleCopy[i] = this.parent.GetCompiledModule(this.moduleData[i]);
            }

        }
    }
}
