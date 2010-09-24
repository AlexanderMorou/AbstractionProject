using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with a name
    /// that has been included in the scope which has not been 
    /// resolved as either a type or a namespace, which has 
    /// been given an alias, or renamed.
    /// </summary>
    public interface INamedInclusionRenameScopeCoercion :
        INamedInclusionScopeCoercion,
        IRenameScopeCoercion
    {
    }
}
