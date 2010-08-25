using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Preprocessors
{
    public interface IPreprocessorGenerationTask :
        IPreprocessorTask
    {
        /// <summary>
        /// Returns the <see cref="PreprocessorGenerationKind"/>
        /// specifying the kind of generation that will occur
        /// associated to the <see cref="IPreprocessorTask"/>.
        /// </summary>
        PreprocessorGenerationKind Kind { get; }
    }
}
