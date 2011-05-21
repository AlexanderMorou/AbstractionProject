using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with a general case 
    /// documentation comment reference.
    /// </summary>
    public interface IDocCommentReferenceElement :
        IDocCommentElement
    {
        /// <summary>
        /// Returns the <see cref="IDeclaration"/>
        /// associated to the 
        /// <see cref="IDocCommentReferenceElement"/>.
        /// </summary>
        IDeclaration ReferencedElement { get; }

    }
}
