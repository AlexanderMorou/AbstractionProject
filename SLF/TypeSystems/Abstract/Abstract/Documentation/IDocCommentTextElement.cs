using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// small element of text that describes the text
    /// inbetween other sections, or as the main body of a
    /// section.
    /// </summary>
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
