using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
