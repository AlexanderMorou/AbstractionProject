using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a jump statement which can redirect the
    /// execution point elsewhere.
    /// </summary>
    public interface IJumpStatement :
        IStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="IJumpTarget"/> of the <see cref="IJumpStatement"/>.
        /// </summary>
        IJumpTarget Target { get; set; }
    }
}
