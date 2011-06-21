using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
