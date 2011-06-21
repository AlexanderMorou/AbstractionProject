using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
