using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using System.Diagnostics;
/*---------------------------------------------------------------------\
| Copyright © 2010 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Modules
{
    /// <summary>
    /// Provides a dictionary for a module's global methods.
    /// </summary>
    [DebuggerDisplay("Global Methods: {Count}")]
    public class IntermediateModuleGlobalMethodDictionary :
        IntermediateMethodMemberDictionary<IModuleGlobalMethod, IIntermediateModuleGlobalMethod, IModule, IIntermediateModule>,
        IIntermediateModuleGlobalMethodDictionary
    {
        public IntermediateModuleGlobalMethodDictionary(IntermediateFullMemberDictionary master, IIntermediateModule parent)
            : base(master, parent)
        {
        }

        protected override IIntermediateModuleGlobalMethod OnGetNewMethod(string name)
        {
            var result = new IntermediateModuleGlobalMethod(this.Parent);
            result.Name = name;
            return result;
        }
    }
}
