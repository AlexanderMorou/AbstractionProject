using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
