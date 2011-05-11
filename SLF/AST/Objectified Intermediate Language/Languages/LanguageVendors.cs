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
        /// Specifies information about the language vendor <see cref="Microsoft"/>.
        /// </summary>
        public static readonly IMicrosoftLanguageVendor Microsoft = new LanguageVendorBase("Microsoft", SymLanguageVendor.Microsoft);
        public static readonly ILanguageVendor AllenCopeland = new LanguageVendorBase("Allen C. Copeland Jr.", new Guid(0x6A9AFE0B, 0x5C4A, 0x4FE8, 0x9F, 0x2B, 0x91, 0x43, 0x4C, 0xE1, 0x29, 0xEB));
    }
}
