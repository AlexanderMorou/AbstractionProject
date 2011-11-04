using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with a documentation
    /// comment which defines information about a field member.
    /// </summary>
    public interface IFieldDocComment :
        IDocComment
    {
        IDocCommentSection Value { get; }
    }
}
