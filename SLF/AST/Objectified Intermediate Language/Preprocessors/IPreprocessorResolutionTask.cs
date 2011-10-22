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

namespace AllenCopeland.Abstraction.Slf.Preprocessors
{
    public interface IPreprocessorResolutionTask :
        IPreprocessorTask
    {
        /// <summary>
        /// Returns the scope of the resolution task.
        /// </summary>
        PreprocessorResolutionScope Scope { get; }
    }
}
