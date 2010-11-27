using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

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
