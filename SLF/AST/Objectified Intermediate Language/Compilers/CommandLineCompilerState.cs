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
    /// The current state of a command line compiler.
    /// </summary>
    public enum CommandLineCompilerState
    {
        /// <summary>
        /// The compiler isn't doing anything and is ready for a command sequence
        /// to begin or a full sequence to begin.
        /// </summary>
        NeutralState,
        /// <summary>
        /// The compiler is accepting commands.
        /// </summary>
        AcceptingCommands,
        /// <summary>
        /// The compiler is busy compiling.
        /// </summary>
        Compiling,
    }
}
