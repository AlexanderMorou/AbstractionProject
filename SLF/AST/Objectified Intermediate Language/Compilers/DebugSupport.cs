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
    /// The type of debugging support the compiler should give the output assembly on the
    /// compile operation.
    /// </summary>
    public enum DebugSupport
    {
        /// <summary>
        /// The compiler should not provide any debugging support for the output
        /// assembly.
        /// </summary>
        None,
        /// <summary>
        /// The compiler should support full debugging support for the output
        /// assembly.
        /// </summary>
        Full,
        /// <summary>
        /// The compiler should support only PDB debug support for the output assembly.
        /// </summary>
        PDBOnly,
    }
}
