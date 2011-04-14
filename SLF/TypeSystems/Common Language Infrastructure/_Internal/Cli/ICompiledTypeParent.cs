using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal interface _ICompiledTypeParent :
        ITypeParent
    {
        /// <summary>
        /// Returns the <see cref="Type"/> series which is associated
        /// to the underlying implementation of the <see cref="_ICompiledTypeParent"/>.
        /// </summary>
        Type[] UnderlyingSystemTypes { get; }
    }
}
