using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class VisualBasicVendorProvider
    {
        public static IVisualBasicLanguage GetVisualBasicLanguage(this IMicrosoftLanguageVendor vendor)
        {
            return VisualBasicLanguage.Singleton;
        }
    }
}
