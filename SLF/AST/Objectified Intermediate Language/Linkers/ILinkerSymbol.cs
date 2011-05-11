using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public enum LinkerSymbolKind
    {
        Namespace = 1,
        Type = 2,
        GlobalEntry = 4,
        GenericSymbolSet = 8,
    }
    public interface ILinkerSymbol
    {
    }
}
