using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with the
    /// provider for the common intermediate language.
    /// </summary>
    public interface ICommonIntermediateProvider :
        ILanguageProvider
    {
        /// <summary>
        /// Returns the <see cref="ICommonIntermediateLanguage"/> 
        /// associated to the <see cref="ICommonIntermediateProvider"/>.
        /// </summary>
        new ICommonIntermediateLanguage Language { get; }
    }
}
