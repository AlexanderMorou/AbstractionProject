using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with a group
    /// of named documentation comment sections.
    /// </summary>
    public interface IDocCommentGroup :
        IControlledStateDictionary<string, IDocCommentNamedSection>
    {
    }
}
