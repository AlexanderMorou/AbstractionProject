using AllenCopeland.Abstraction.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface ILanguageService<TLanguage, TProvider> :
        ILanguageService
        where TProvider :
            ILanguageProvider<TLanguage, TProvider>
        where TLanguage :
            ILanguage<TLanguage, TProvider>
    {
        /// <summary>
        /// Returns the <typeparamref name="TProvider"/> which maintains the
        /// <see cref="ILanguageService"/>.
        /// </summary>
        new TProvider Provider { get; }
        /// <summary>
        /// Returns the <typeparamref name="TLanguage"/> on which the 
        /// <see cref="ILanguageService"/> provides the service for.
        /// </summary>
        new TLanguage Language { get; }
    }

    /// <summary>
    /// Defines properties and methods for working with a language service.
    /// </summary>
    public interface ILanguageService :
        IService<ILanguageService>
    {
        /// <summary>
        /// Returns the <see cref="ILanguageProvider"/> which maintains the
        /// <see cref="ILanguageService"/>.
        /// </summary>
        ILanguageProvider Provider { get; }
        /// <summary>
        /// Returns the <see cref="ILanguage"/> on which the 
        /// <see cref="ILanguageService"/> provides the service for.
        /// </summary>
        ILanguage Language { get; }
    }
}
