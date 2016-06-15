using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with a documentation
    /// comment element that references a field.
    /// </summary>
    public interface IDocCommentFieldReferenceElement :
        IDocCommentReferenceElement
    {
        /// <summary>
        /// Returns the <see cref="IFieldMember"/>
        /// associated to the 
        /// <see cref="IDocCommentFieldReferenceElement"/>.
        /// </summary>
        new IFieldMember ReferencedElement { get; }
    }
}
