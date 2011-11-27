using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public interface IOilIntermediateCompiler :
        IIntermediateCompiler<IIntermediateAssembly> 
    {
    }
}
