using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer
{
    public interface IPreprocessorDefineSymbolDirective :
        IPreprocessorDirective
    {
        string SymbolName { get; }

        string Value { get; }
    }
}
