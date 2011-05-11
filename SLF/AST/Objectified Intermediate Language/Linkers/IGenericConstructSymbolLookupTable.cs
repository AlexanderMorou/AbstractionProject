using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public interface IGenericConstructSymbolLookupTable :
        ISymbolLookupTable<int>
    {
        /// <summary>
        /// Returns whether the current <see cref="IGenericConstructSymbolLookupTable"/>
        /// is grouped with a non-generic construct variant under
        /// the same identity.
        /// </summary>
        bool HasNonGenericConstruct { get; }
    }
}
