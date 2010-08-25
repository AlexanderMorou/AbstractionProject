using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
