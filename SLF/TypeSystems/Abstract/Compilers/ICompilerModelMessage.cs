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

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Defines properties and methods for working with a message, from the
    /// compiler, relative to an element, from the object-model, that defines
    /// the structure of general purpose code.
    /// </summary>
    public interface ICompilerModelMessage :
        ICompilerMessage
    {
        /// <summary>
        /// Returns the <see cref="String"/> associated to the 
        /// <see cref="ICompilerModelMessage"/>.
        /// </summary>
        string Message { get; }
    }
}
