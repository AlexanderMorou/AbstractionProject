using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
