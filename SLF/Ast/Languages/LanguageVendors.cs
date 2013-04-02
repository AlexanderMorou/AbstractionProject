using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.SymbolStore;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public static class LanguageVendors
    {
        /// <summary>
        /// Specifies information about the <see cref="IMicrosoftLanguageVendor"/>.
        /// </summary>
        public static readonly     IMicrosoftLanguageVendor     Microsoft =     MicrosoftLanguageVendor.Singleton;
        /// <summary>
        /// Specifies information about the <see cref="IAllenCopelandLanguageVendor"/>.
        /// </summary>
        public static readonly IAllenCopelandLanguageVendor AllenCopeland = AllenCopelandLanguageVendor.Singleton;
    }
}
