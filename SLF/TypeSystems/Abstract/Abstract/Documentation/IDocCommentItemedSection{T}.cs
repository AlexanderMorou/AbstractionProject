using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
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
