using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public class SourceRelatedMessage :
        ISourceRelatedMessage
    {

        protected SourceRelatedMessage(string message, int line, int column, string fileName)
        {
            this.Message = message;
            this.Location = new LineColumnPair(line, column);
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
