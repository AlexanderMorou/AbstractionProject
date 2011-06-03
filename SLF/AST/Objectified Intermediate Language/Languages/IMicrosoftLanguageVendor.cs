using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
