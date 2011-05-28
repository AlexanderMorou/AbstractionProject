using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    public interface IPropertyDocComment :
        ICodeMemberDocComment
    {
        /// <summary>
        /// Returns the <see cref="IDocCommentSection"/>
        /// which describes a basic summary about the value
        /// stored within the member associated to the 
        /// <see cref="IDocComment"/>.
        /// </summary>
        IDocCommentSection Value { get; }
    }
}
