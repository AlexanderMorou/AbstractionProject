using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Diagnostics;
/*---------------------------------------------------------------------\
| Copyright © 2010 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Modules
{
    [DebuggerDisplay("Modules: {Count}")]
    public class IntermediateModuleDictionary :
        ControlledStateDictionary<string, IIntermediateModule>,
        IIntermediateModuleDictionary
    {

        private IIntermediateAssembly assembly;

        #region IIntermediateModuleDictionary Members

        public IIntermediateModule Add(string moduleName)
        {
            var result = new IntermediateModule(moduleName, this.assembly);
            this._Add(moduleName, result);
            return result;
        }

        #endregion

        internal IntermediateModuleDictionary(IIntermediateAssembly assembly)
        {
            this.assembly = assembly;
            this.Add("RootModule");
        }
    }
}
