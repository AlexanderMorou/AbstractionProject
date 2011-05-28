using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
