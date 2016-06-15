using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    public interface IDocCommentTable
    {
        IEnumerable<IDocCommentSection> Headers { get; }
        IEnumerable<IDocCommentSection> Body { get; }
    }
}
