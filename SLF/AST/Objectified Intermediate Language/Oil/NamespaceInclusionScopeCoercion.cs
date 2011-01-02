using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
