using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Utilities.Collections;

namespace AllenCopeland.Abstraction.OldCodeGen.Compiler
{
    public interface IIntermediateCompilerModuleActionResult :
        IIntermediateCompilerResultsBase
    {
        /// <summary>
        /// Returns whether the compiler module action was successful.
        /// </summary>
        bool Successful { get; }
    }
}
