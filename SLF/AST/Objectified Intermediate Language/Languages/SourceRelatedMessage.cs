using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public class SourceRelatedMessage :
        ISourceRelatedMessage
    {

        protected SourceRelatedMessage(string message, int line, int column, string fileName)
        {
            this.Message = message;
            this.Line = line;
            this.Column = column;
            this.FileName = fileName;
        }

        #region ISourceRelatedMessage Members

        /// <summary>
        /// Returns the <see cref="String"/> associated to the 
        /// <see cref="ISourceRelatedMessage"/>.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the <see cref="Int32"/> value associated to the 
        /// column the message pertains to.
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// Gets the <see cref="Int32"/> value associated to the line
        /// the message pertains to.
        /// </summary>
        public int Line { get; private set; }

        /// <summary>
        /// Returns the <see cref="String"/> which denotes the specific file
        /// in which the source related message pertains to.
        /// </summary>
        public string FileName { get; private set; }

        #endregion
    }
}
