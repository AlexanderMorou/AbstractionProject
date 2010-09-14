using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer
{
    public interface IPreprocessorStringTerminalDirective :
        IPreprocessorDirective
    {
        string Literal { get; }
        StringTerminalKind Kind { get; }
    }
}
