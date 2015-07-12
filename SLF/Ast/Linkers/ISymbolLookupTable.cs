using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
