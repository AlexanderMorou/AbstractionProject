﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public class LanguageVendorBase :
        ILanguageVendor
    {
        internal LanguageVendorBase(string name, Guid guid)
        {
            this.Name = name;
            this.Guid = guid;
        }
        #region ILanguageVendor Members

        public string Name { get; private set; }

        public Guid Guid { get; private set; }

        #endregion

    }
}