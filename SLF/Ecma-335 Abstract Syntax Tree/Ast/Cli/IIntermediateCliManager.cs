using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Ast.Cli
{

    public interface IIntermediateCliManager :
        IIntermediateIdentityManager,
        ICliManager
    {
    }
}
