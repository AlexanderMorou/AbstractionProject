using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class VisualBasicVendorProvider
    {
        public static IVisualBasicLanguage GetLanguageVisualBasic(this IMicrosoftLanguageVendor vendor)
        {
            return VisualBasicLanguage.Singleton;
        }
    }
}
