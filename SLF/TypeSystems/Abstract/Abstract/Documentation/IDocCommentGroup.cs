using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{

    public interface IDocCommentGroup<TKey, TItem, TSection> :
        IControlledStateDictionary<TKey, TSection>
    {

    }
}
