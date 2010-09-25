using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    class TypeInclusionRenameScopeCoercion :
        TypeInclusionScopeCoercion,
        ITypeInclusionRenameScopeCoercion
    {
        #region IRenameScopeCoercion Members

        /// <summary>
        /// Returns/sets the new name of the type included.
        /// </summary>
        public string NewName { get; set; }

        #endregion
    }
}
