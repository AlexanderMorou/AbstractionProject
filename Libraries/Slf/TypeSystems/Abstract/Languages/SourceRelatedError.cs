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
    public class SourceRelatedError :
        SourceRelatedMessage,
        ISourceRelatedError
    {

        /// <summary>
        /// Creates a new <see cref="SourceRelatedError"/> with the
        /// <paramref name="message"/>, <paramref name="start"/>,
        /// <paramref name="end"/> and <paramref name="source"/> provided.
        /// </summary>
        /// <param name="message">The <see cref="String"/> which denotes the message </param>
        /// <param name="start">The <see cref="LineColumnPair"/> which denotes
        /// the start point of the message.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> which denotes
        /// the end point of the message.</param>
        /// <param name="source">The <see cref="Uri"/> instance
        /// which denotes where the origin of the <see cref="SourceRelatedError"/>.</param>
        public SourceRelatedError(string errorText, LineColumnPair start, LineColumnPair end, Uri source)
            : base(errorText, start, end, source)
        {
        }
    }
}
