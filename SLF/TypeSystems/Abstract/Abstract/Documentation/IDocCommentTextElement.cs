using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    public interface IDocCommentTextElement :
        IDocCommentElement
    {
        /// <summary>
        /// Returns the <see cref="String"/> value represented by the
        /// <see cref="IDocCommentTextElement"/>.
        /// </summary>
        string Text { get; }
    }
}
