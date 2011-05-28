using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// documentation comment reference to an element of code.
    /// </summary>
    public interface IDocCommentCodeReference
    {
        /// <summary>
        /// Returns the <see cref="String"/> value used to
        /// resolve the <see cref="Reference"/>.
        /// </summary>
        string ReferencePath { get; }
    }
}
