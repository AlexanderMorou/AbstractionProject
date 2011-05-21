using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    public interface IDocCommentFieldReferenceElement :
        IDocCommentReferenceElement
    {
        /// <summary>
        /// Returns the <see cref="IFieldMember"/>
        /// associated to the 
        /// <see cref="IDocCommentFieldReferenceElement"/>.
        /// </summary>
        IFieldMember ReferencedElement { get; }
    }
}
