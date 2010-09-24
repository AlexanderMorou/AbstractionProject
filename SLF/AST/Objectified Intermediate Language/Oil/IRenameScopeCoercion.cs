using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public interface IRenameScopeCoercion :
        IScopeCoercion
    {
        /// <summary>
        /// Returns/sets the new name of the entity included.
        /// </summary>
        string NewName { get; set; }
    }
}
