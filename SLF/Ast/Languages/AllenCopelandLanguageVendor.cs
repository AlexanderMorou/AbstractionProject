using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// The base <see cref="IAllenCopelandLanguageVendor"/> implementation
    /// which provides access, though a library and extension method, 
    /// to the OILexer language.
    /// </summary>
    internal class AllenCopelandLanguageVendor :
        LanguageVendorBase,
        IAllenCopelandLanguageVendor
    {
        public static readonly IAllenCopelandLanguageVendor Singleton = new AllenCopelandLanguageVendor();
        private AllenCopelandLanguageVendor()
            : base("Allen C. Copeland Jr.", LanguageGuids.Vendors.AllenCopeland)
        {

        }
    }
}
