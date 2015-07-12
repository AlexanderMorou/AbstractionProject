using AllenCopeland.Abstraction.Slf.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public class SourceRelatedWarning :
        SourceRelatedMessage,
        ISourceRelatedWarning
    {

        /// <summary>
        /// Creates a new <see cref="SourceRelatedWarning"/> with the
        /// <paramref name="message"/>, <paramref name="start"/>,
        /// <paramref name="end"/> and <paramref name="source"/> provided.
        /// </summary>
        /// <param name="message">The <see cref="String"/> which denotes the message </param>
        /// <param name="start">The <see cref="LineColumnPair"/> which denotes
        /// the start point of the message.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> which denotes
        /// the end point of the message.</param>
        /// <param name="source">The <see cref="Uri"/> instance
        /// which denotes where the origin of the <see cref="SourceRelatedWarning"/>.</param>
        public SourceRelatedWarning(string warningText, LineColumnPair start, LineColumnPair end, Uri source)
            : base(warningText, start, end, source)
        {
        }
    }
}
