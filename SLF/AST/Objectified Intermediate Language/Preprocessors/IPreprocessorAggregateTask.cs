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
    /// <summary>
    /// Defines properties and methods for working with an aggregate task
    /// which combines a series of <see cref="IPreprocessorTask"/>
    /// instances by priority.
    /// </summary>
    public interface IPreprocessorAggregateTask :
        IDictionary<PreprocessorTaskPriority, IEnumerable<IPreprocessorTask>>
    {
    }
}
