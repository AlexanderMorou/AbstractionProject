﻿using System;
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