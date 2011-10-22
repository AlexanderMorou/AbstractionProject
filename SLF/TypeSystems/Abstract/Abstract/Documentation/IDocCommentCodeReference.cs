using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// documentation comment reference to an element of code.
    /// </summary>
    public interface IDocCommentCodeReference
    {
        /// <summary>
        /// Returns the <see cref="String"/> value used to
        /// resolve the <see cref="Reference"/>.
        /// </summary>
        string ReferencePath { get; }
    }
}
