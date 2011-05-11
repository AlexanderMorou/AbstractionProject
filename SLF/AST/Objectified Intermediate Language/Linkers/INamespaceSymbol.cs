using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    /// <summary>
    /// Represents a symbol entry which denotes a namespace
    /// which contains a series of types, sub-namespaces, fields
    /// and/or methods.
    /// </summary>
    public interface INamespaceSymbol :
        INamedSymbolLookupTable,
        ILinkerSymbol
    {
    }
}
