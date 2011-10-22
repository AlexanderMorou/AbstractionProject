using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Slf.Languages.Cil;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with the Microsoft vendor.
    /// </summary>
    public interface IMicrosoftLanguageVendor :
        ILanguageVendor
    {
        /// <summary>
        /// Returns the <see cref="ICSharpLanguage">C&#9839; language</see>
        /// associated to the vendor.
        /// </summary>
        /// <returns>The Singleton <see cref="ICSharpLanguage">C&#9839; language</see>
        /// associated to the vendor.</returns>
        ICSharpLanguage GetCSharpLanguage();
        /// <summary>
        /// Returns the <see cref="ICommonIntermediateLanguage"/> associated
        /// to the vendor.
        /// </summary>
        /// <returns>The singleton <see cref="ICommonIntermediateLanguage"/>
        /// associated to the vendor.</returns>
        ICommonIntermediateLanguage GetCommonIntermediateLanguage();
    }
}
