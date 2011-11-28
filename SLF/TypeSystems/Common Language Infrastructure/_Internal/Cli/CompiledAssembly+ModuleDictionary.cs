using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Modules;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledAssembly
    {
        private partial class ModuleDictionary :
            ControlledDictionary<IGeneralDeclarationUniqueIdentifier, IModule>,
            IModuleDictionary
        {
            private CompiledAssembly parent;
            private Module[] moduleData;
            private IGeneralDeclarationUniqueIdentifier[] identifierCopy;
            private CompiledModule[] moduleCopy;
            public ModuleDictionary(CompiledAssembly parent)
            {
                this.moduleData = parent.UnderlyingAssembly.GetModules(true);
                this.identifierCopy = new IGeneralDeclarationUniqueIdentifier[this.moduleData.Length];
                this.moduleCopy = new CompiledModule[this.moduleData.Length];
                for (int i = 0; i < identifierCopy.Length; i++)
                    identifierCopy[i] = AstIdentifier.Declaration(this.moduleData[i].Name);
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
