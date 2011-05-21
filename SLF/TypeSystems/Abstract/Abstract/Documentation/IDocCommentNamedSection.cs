using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with a named
    /// documentation comment section.
    /// </summary>
    public interface IDocCommentNamedSection :
        IDocCommentSection
    {
        /// <summary>
        /// Returns the <see cref="String"/> value associated
        /// to the name.
        /// </summary>
        string Name { get; }
    }
}
