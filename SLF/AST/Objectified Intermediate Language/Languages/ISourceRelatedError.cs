using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with a point 
    /// within a source file that represents a halting error which
    /// needs addressed before the operation upon the source file can
    /// continue.
    /// </summary>
    public interface ISourceRelatedError :
        ISourceRelatedMessage
    {
    }
}
