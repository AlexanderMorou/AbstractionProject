using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public class NamedInclusionRenameScopeCoercion :
        NamedInclusionScopeCoercion,
        INamedInclusionRenameScopeCoercion
    {
        #region IRenameScopeCoercion Members

        /// <summary>
        /// Returns/sets the new name of the entity included.
        /// </summary>
        public string NewName { get; set; }

        #endregion
    }
}
