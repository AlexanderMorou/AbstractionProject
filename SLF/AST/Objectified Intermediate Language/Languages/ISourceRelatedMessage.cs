using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with a message
    /// associated to a given source file.
    /// </summary>
    public interface ISourceRelatedMessage
    {
        /// <summary>
        /// Returns the <see cref="String"/> associated to the 
        /// <see cref="ISourceRelatedMessage"/>.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Returns the <see cref="LineColumnPair"/> which denotes the position within <see cref="FileName"/>
        /// the <see cref="ISourceRelatedMessage"/> refers to.
        /// </summary>
        LineColumnPair Location { get; }
        
        /// <summary>
        /// Returns the <see cref="String"/> which denotes the specific file
        /// in which the source related message pertains to.
        /// </summary>
        string FileName { get; }
    }
}
