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
    /// Defines properties and methods for working with a named
    /// documentation comment section.
    /// </summary>
    /// <typeparam name="TItem">The type of element represented
    /// by the section.</typeparam>
    public interface IDocCommentNamedSection<TItem> :
        IDocCommentItemedSection<TItem>
        where TItem :
            IDeclaration
    {
        /// <summary>
        /// Returns the <see cref="String"/> value associated
        /// to the name.
        /// </summary>
        string Name { get; }
    }
}
