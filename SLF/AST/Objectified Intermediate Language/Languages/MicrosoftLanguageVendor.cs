using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.SymbolStore;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /* *
     * The vendor for microsoft which exposes the VB.NET and
     * C# languages.
     * */
    internal sealed class MicrosoftLanguageVendor :
        LanguageVendorBase,
        IMicrosoftLanguageVendor
    {
        internal static IMicrosoftLanguageVendor Singleton = new MicrosoftLanguageVendor();

        private MicrosoftLanguageVendor()
            : base("Microsoft", SymLanguageVendor.Microsoft)
        {

        }

        #region IMicrosoftLanguageVendor Members

        public ICSharpLanguage GetLanguageCSharp()
        {
            return CSharpLanguage.Singleton;
        }
        
        #endregion
    }
}
