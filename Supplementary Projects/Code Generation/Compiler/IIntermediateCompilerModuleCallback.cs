using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Utilities.Collections;
using AllenCopeland.Abstraction.OldCodeGen.FileModel;

namespace AllenCopeland.Abstraction.OldCodeGen.Compiler
{
    public interface IIntermediateCompilerCallbackModule :
        IIntermediateCompilerModule
    {
        IIntermediateCompilerModuleActionResult PrepareResources(IControlledStateCollection<TemporaryFile> resources);
        IIntermediateCompilerModuleActionResult PrepareSource(IControlledStateCollection<TemporaryFile> files);
        IIntermediateCompilerModuleActionResult PrepareKey(TemporaryFile pfxFile);
        IIntermediateCompilerResults Compile();
    }
}
