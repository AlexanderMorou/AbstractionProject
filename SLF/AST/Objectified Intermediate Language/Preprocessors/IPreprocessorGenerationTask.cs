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
