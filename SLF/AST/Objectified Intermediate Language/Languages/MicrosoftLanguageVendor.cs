using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.SymbolStore;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
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
            : base("Microsoft", SymLanguageVendor.Microsoft)
        {

        }

        #region IMicrosoftLanguageVendor Members

        /// <summary>
        /// Returns the <see cref="ICSharpLanguage">C&#9839; language</see>
        /// associated to the vendor.
        /// </summary>
        /// <returns>The Singleton <see cref="ICSharpLanguage">C&#9839; language</see>
        /// associated to the <see cref="MicrosoftLanguageVendor">vendor</see>.
        /// </returns>
        public ICSharpLanguage GetCSharpLanguage()
        {
            return CSharpLanguage.Singleton;
        }

        /// <summary>
        /// Returns the <see cref="ICommonIntermediateLanguage"/> associated
        /// to the vendor.
        /// </summary>
        /// <returns>The singleton <see cref="ICommonIntermediateLanguage"/>
        /// associated to the <see cref="MicrosoftLanguageVendor">vendor</see>.
        /// </returns>
        public ICommonIntermediateLanguage GetCommonIntermediateLanguage()
        {
            return CommonIntermediateLanguage.Singleton;
        }

        #endregion
    }
}
