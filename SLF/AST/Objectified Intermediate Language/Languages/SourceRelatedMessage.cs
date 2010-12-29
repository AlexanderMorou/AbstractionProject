using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public class SourceRelatedMessage :
        ISourceRelatedMessage
    {

        protected SourceRelatedMessage(string message, int line, int column, string fileName)
        {
            this.Message = message;
            this.Location = new LineColumnPair() { Line = line, Column = column };
            this.FileName = fileName;
        }

        #region ISourceRelatedMessage Members

        /// <summary>
        /// Returns the <see cref="String"/> associated to the 
        /// <see cref="ISourceRelatedMessage"/>.
        /// </summary>
        public string Message { get; private set; }

        public LineColumnPair Location { get; private set; }

        /// <summary>
        /// Returns the <see cref="String"/> which denotes the specific file
        /// in which the source related message pertains to.
        /// </summary>
        public string FileName { get; private set; }

        #endregion
    }
}
