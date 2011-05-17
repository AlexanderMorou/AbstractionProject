using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.SymbolStore;

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
