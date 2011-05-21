using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with a section of a 
    /// documentation
    /// </summary>
    public interface IDocCommentSection :
        IControlledStateCollection<IDocCommentElement>
    {
        /// <summary>
        /// Returns the <see cref="String"/> value which
        /// represents the name of the section.
        /// </summary>
        string SectionName { get; }
    }
}
