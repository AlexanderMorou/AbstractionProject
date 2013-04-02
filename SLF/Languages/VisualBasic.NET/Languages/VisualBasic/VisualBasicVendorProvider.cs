using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class VisualBasicVendorProvider
    {
        /// <summary>
        /// Returns the <see cref="IVisualBasicLanguage"/> associated to 
        /// the vendor.
        /// </summary>
        /// <param name="vendor">The <see cref="IMicrosoftLanguageVendor"/> singleton
        /// which relates to the <see cref="IVisualBasicLanguage">Visual Basic.NET language</see>.</param>
        /// <returns></returns>
        public static IVisualBasicLanguage GetVisualBasicLanguage(this IMicrosoftLanguageVendor vendor)
        {
            return VisualBasicLanguage.Singleton;
        }
    }
}
