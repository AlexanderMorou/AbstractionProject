using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with 
    /// an element of a documentation comment.
    /// </summary>
    public interface IDocCommentElement
    {
        /// <summary>
        /// Returns the previous <see cref="IDocCommentElement"/>.
        /// </summary>
        /// <remarks>Can be null if the <see cref="IDocCommentElement"/>
        /// is the first element.</remarks>
        IDocCommentElement Previous { get; }
        /// <summary>
        /// Returns the next <see cref="IDocCommentElement"/>.
        /// </summary>
        /// <remarks>Can be null if the <see cref="IDocCommentElement"/>
        /// is the final element.</remarks>
        IDocCommentElement Next { get; }
        /// <summary>
        /// Returns the <see cref="IDocCommentSection"/> which
        /// defines the <see cref="IDocCommentElement"/>.
        /// </summary>
        IDocCommentSection Section { get; }
    }
}
