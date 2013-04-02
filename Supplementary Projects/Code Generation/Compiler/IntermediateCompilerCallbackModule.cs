using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.FileModel;
using AllenCopeland.Abstraction.OldCodeGen.Utilities.Collections;

namespace AllenCopeland.Abstraction.OldCodeGen.Compiler
{
    public abstract class IntermediateCompilerCallbackModule :
        IntermediateCompilerModuleBase,
        IIntermediateCompilerCallbackModule
    {
        #region IIntermediateCompilerModuleCallback Members

        public abstract IIntermediateCompilerModuleActionResult PrepareResources(IControlledStateCollection<TemporaryFile> resources);

        public abstract IIntermediateCompilerModuleActionResult PrepareSource(IControlledStateCollection<TemporaryFile> files);

        public abstract IIntermediateCompilerModuleActionResult PrepareKey(TemporaryFile pfxFile);

        public abstract IIntermediateCompilerResults Compile();

        #endregion


        public override CompilerModuleType Type
        {
            get { return CompilerModuleType.Callback; }
        }
    }
}
