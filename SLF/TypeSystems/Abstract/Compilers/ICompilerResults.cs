using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Defines properties and methods for working with the results of a 
    /// compile operation.
    /// </summary>
    public interface ICompilerResults :
        IDisposable
    {
        /// <summary>
        /// Returns the <see cref="Assembly"/> that resulted
        /// from a compile operation.
        /// </summary>
        Assembly Results { get; }
        /// <summary>
        /// Returns/sets whether to keep the associated temp files
        /// relative to the compile operation.
        /// </summary>
        /// <remarks>Not always applicable.</remarks>
        bool KeepFiles { get; }
    }
}
