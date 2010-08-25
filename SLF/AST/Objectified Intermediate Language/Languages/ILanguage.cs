using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface ILanguage
    {
        /// <summary>
        /// Returns the <see cref="String"/> value representing
        /// the unique name of the language.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Returns a new <see cref="ILanguageProvider"/> associated to the current
        /// <see cref="ILanguage"/>.
        /// </summary>
        /// <returns>A new <see cref="ILanguageProvider"/> for the current
        /// <see cref="ILangauge"/>.</returns>
        ILanguageProvider GetProvider();
    }
}
