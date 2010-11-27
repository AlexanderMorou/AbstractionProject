using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with
    /// a point within a source file which is flagged
    /// as a point of interest: either due to side-effects,
    /// or non-halting errors.
    /// </summary>
    public interface ISourceRelatedWarning :
        ISourceRelatedMessage
    {
    }
}
