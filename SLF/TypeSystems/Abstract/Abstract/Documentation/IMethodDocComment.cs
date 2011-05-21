using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// documentation comment associated to a method.
    /// </summary>
    public interface IMethodDocComment : 
        IDocComment
    {
        /// <summary>
        /// Returns the <see cref="IDocCommentGroup"/>
        /// associated to the parameters of the 
        /// <see cref="IMethodDocComment"/>.
        /// </summary>
        IDocCommentGroup Parameters { get; }
        /// <summary>
        /// Returns the <see cref="IDocCommentGroup"/>
        /// associated to the type-parameters of the 
        /// <see cref="IMethodDocComment"/>.
        /// </summary>
        IDocCommentGroup TypeParameters { get; }
        /// <summary>
        /// Returns the <see cref="IDocCommentSection"/>
        /// which denotes information about the return
        /// value of the method.
        /// </summary>
        IDocCommentSection Returns { get; }
    }
}
