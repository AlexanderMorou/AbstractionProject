using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public interface ISymbolLookupTable<TSymbolKey>
    {
        /// <summary>
        /// Returns the <see cref="ILinkerSymbol"/> relative to the
        /// <paramref name="key"/> provided.
        /// </summary>
        /// <param name="key">The <typeparamref name="TSymbolKey"/>
        /// which aids in symbol lookup.</param>
        /// <returns>The <see cref="ILinkerSymbol"/> relative to the
        /// <paramref name="key"/> provided.</returns>
        ILinkerSymbol this[TSymbolKey key] { get; }
    }
}
