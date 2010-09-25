using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public class NamespaceInclusionRenameScopeCoercion :
        NamespaceInclusionScopeCoercion,
        INamespaceInclusionRenameScopeCoercion
    {
        #region IRenameScopeCoercion Members

        /// <summary>
        /// Returns/sets the new name of the namespace included.
        /// </summary>
        public string NewName { get; set; }

        #endregion
    }
}
