using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Defines properties and methods for working with a command line
    /// compiler's results.
    /// </summary>
    public interface ICommandLineCompilerResults :
        ICompilerResults
    {
        /// <summary>
        /// Returns the native return value the command line compiler recieves as
        /// a result of the operation.
        /// </summary>
        int NativeReturnValue { get; }
    }
}
