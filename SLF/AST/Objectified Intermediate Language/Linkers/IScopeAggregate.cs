using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public interface IScopeAggregate :
        INamedSymbolLookupTable
    {
        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> of 
        /// <see cref="IScopeCoercion"/> elements from which
        /// the current <see cref="IScopeAggregate"/> is derived.
        /// </summary>
        IEnumerable<IScopeCoercion> Coercions { get; }
    }
}
