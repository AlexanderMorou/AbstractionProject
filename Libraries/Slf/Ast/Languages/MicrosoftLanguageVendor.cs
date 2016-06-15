using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.SymbolStore;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
    public sealed partial class MicrosoftLanguageVendor :
        LanguageVendorBase,
        IMicrosoftLanguageVendor
    {
        internal static IMicrosoftLanguageVendor Singleton = new MicrosoftLanguageVendor();
        internal static readonly byte[] stdLibPublicKeyToken = new byte[] { 0xb0, 0x3f, 0x5f, 0x7f, 0x11, 0xd5, 0x0a, 0x3a };
        private MicrosoftLanguageVendor()
            : base("Microsoft", LanguageGuids.Vendors.Microsoft)
        {
        }

        public static IEnumerable<byte> StandardLibraryPublicKeyToken
        {
            get
            {
                for (int i = 0; i < stdLibPublicKeyToken.Length; i++)
                    yield return stdLibPublicKeyToken[i];
            }
        }

    }
}
