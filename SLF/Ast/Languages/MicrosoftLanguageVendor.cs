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
    /* *
     * The vendor for microsoft which exposes the VB.NET,
     * C# and Common Intermediate Languages.
     * */
    internal sealed class MicrosoftLanguageVendor :
        LanguageVendorBase,
        IMicrosoftLanguageVendor
    {
        internal static IMicrosoftLanguageVendor Singleton = new MicrosoftLanguageVendor();

        private MicrosoftLanguageVendor()
            : base("Microsoft", LanguageGuids.Vendors.Microsoft)
        {

        }

    }
}
