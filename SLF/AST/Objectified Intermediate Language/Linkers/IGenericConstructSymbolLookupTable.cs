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
