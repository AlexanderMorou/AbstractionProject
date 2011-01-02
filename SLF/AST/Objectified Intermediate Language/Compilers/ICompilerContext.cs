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

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public interface ICompilerContext
    {
        /// <summary>
        /// Returns/sets the <see cref="String"/> value representing the name of the
        /// assembly used during compile.
        /// </summary>
        string AssemblyName { get; set; }
        /// <summary>
        /// Returns the series of <see cref="IAssembly"/> instances
        /// referenced by the workspace.
        /// </summary>
        IAssemblyReferenceCollection References { get; }
    }
}
