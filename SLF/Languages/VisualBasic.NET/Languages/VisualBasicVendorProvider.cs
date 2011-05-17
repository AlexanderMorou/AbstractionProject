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
        public static ILanguage GetLanguageVisualBasic(this IMicrosoftLanguageVendor vendor)
        {
            throw new NotImplementedException();
        }
    }
}
