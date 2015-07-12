using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with a
    /// documentation comment section that contains a <typeparamref name="TItem"/>
    /// that represents the <see cref="IDeclaration"/> that the
    /// <see cref="IDocCommentItemedSection{TItem}"/> describes.
    /// </summary>
    /// <typeparam name="TItem">The type of <see cref="IDeclaration"/>
    /// described by the <see cref="IDocCommentItemedSection{TItem}"/>.</typeparam>
    public interface IDocCommentItemedSection<TItem> :
        IDocCommentSection
        where TItem :
            IDeclaration
    {
        /// <summary>
        /// Returns the <typeparamref name="TItem"/>
        /// associated to the <see cref="IDocCommentNamedSection{TItem}"/>
        /// </summary>
        /// <remarks>Can be null if the reference could not be
        /// resolved.</remarks>
        TItem SectionTarget { get; }
    }
}
