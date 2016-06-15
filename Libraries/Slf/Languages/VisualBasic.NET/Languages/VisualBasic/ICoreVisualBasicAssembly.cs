using AllenCopeland.Abstraction.Slf.Ast.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    public interface ICoreVisualBasicAssembly :
        IVisualBasicAssembly<ICoreVisualBasicAssembly, ICoreVisualBasicProvider>
    {
    }
}
