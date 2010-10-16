using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Parsers
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
        /// Gets the <see cref="Int32"/> value associated to the 
        /// column the message pertains to.
        /// </summary>
        int Column { get; }
        /// <summary>
        /// Gets the <see cref="Int32"/> value associated to the line
        /// the message pertains to.
        /// </summary>
        int Line { get; }
        
        /// <summary>
        /// Returns the <see cref="String"/> which denotes the specific file
        /// in which the source related message pertains to.
        /// </summary>
        string FileName { get; }
    }
}
