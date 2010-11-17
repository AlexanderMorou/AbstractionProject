using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public class NamedInclusionScopeCoercion :
        INamedInclusionScopeCoercion
    {
        #region INamedInclusionScopeCoercion Members

        /// <summary>
        /// Returns/sets the name included in the scope
        /// which coerces symbol table resolution.
        /// </summary>
        public string IncludedName { get; set; }

        #endregion

        public override string ToString()
        {
            return string.Format("include {0};", IncludedName);
        }
    }
}
