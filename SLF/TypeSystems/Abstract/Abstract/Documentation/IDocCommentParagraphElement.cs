using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with a
    /// paragraph element of a documentation comment.
    /// </summary>
    public interface IDocCommentParagraphElement :
        IDocCommentSection, 
        IDocCommentElement
    {
    }
}
