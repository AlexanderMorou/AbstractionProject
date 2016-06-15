using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
        /// <paramref name="end"/> and <paramref name="source"/> provided.
        /// </summary>
        /// <param name="message">The <see cref="String"/> which denotes the message </param>
        /// <param name="start">The <see cref="LineColumnPair"/> which denotes
        /// the start point of the message.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> which denotes
        /// the end point of the message.</param>
        /// <param name="source">The <see cref="Uri"/> instance
        /// which denotes where the origin of the <see cref="SourceRelatedMessage"/>.</param>
        protected SourceRelatedMessage(string message, LineColumnPair start, LineColumnPair end, Uri source)
        {
            this.Message = message;
            this.Start = start;
            this.End = end;
            this.Source = source;
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
        /// Returns the <see cref="Uri"/> which denotes the specific source
        /// in which the source related message pertains to.
        /// </summary>
        public Uri Source { get; private set; }

        #endregion
    }
}
