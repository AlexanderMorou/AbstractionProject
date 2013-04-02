using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public class SourceRelatedMessage :
        ISourceRelatedMessage
    {
        /// <summary>
        /// Creates a new <see cref="SourceRelatedMessage"/> with the
        /// <paramref name="message"/>, <paramref name="start"/>,
        /// <paramref name="end"/> and <paramref name="fileName"/> provided.
        /// </summary>
        /// <param name="message">The <see cref="String"/> which denotes the message </param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="fileName"></param>
        protected SourceRelatedMessage(string message, LineColumnPair start, LineColumnPair end, string fileName)
        {
            this.Message = message;
            this.Start = start;
            this.End = end;
            this.FileName = fileName;
        }

        #region ISourceRelatedMessage Members

        /// <summary>
        /// Returns the <see cref="String"/> associated to the 
        /// <see cref="SourceRelatedMessage"/>.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Returns the <see cref="LineColumnPair"/> which denotes the position, within <see cref="FileName"/>,
        /// in which the <see cref="SourceRelatedMessage"/> starts.
        /// </summary>
        public LineColumnPair Start { get; private set; }
        /// <summary>
        /// Returns the <see cref="LineColumnPair"/> which denotes the position, within the <see cref="FileName"/>,
        /// at which the <see cref="SourceRelatedMessage"/> ends.
        /// </summary>
        public LineColumnPair End { get; private set; }

        /// <summary>
        /// Returns the <see cref="String"/> which denotes the specific file
        /// in which the source related message pertains to.
        /// </summary>
        public string FileName { get; private set; }

        #endregion
    }
}
