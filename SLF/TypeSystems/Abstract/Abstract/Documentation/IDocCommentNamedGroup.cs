using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with a group
    /// of named documentation comment sections.
    /// </summary>
    /// <typeparam name="TItem">The type of element represented
    /// by the section.</typeparam>
    /// <typeparam name="TSection">The type of section contained within
    /// the group.</typeparam>
    public interface IDocCommentNamedGroup<TItem, TSection> :
        IDocCommentGroup<string, TItem, TSection>
        where TItem :
            IDeclaration
        where TSection :
            IDocCommentNamedSection<TItem>
    {

    }
}
