using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public class NamespaceInclusionScopeCoercion :
        INamespaceInclusionScopeCoercion
    {
        #region INamespaceInclusionScopeCoercion Members

        /// <summary>
        /// Returns/sets the <see cref="String"/> value associated to the 
        /// namespace to include to coerce identity resolution.
        /// </summary>
        public string Namespace { get; set; }

        #endregion
    }
}
