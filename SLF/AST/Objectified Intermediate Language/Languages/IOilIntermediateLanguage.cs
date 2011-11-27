using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for the Objectified Intermediate Language
    /// pseudo language used to define default behavior for dynamic code.
    /// </summary>
    public interface IOilIntermediateLanguage :
        IHighLevelLanguage<IIntermediateAssembly>
    {
        /// <summary>
        /// Returns a new <see cref="IOilProvider"/> associated to the current
        /// <see cref="IOilIntermediateLanguage"/>.
        /// </summary>
        /// <returns>A new <see cref="IOilProvider"/> for the current
        /// <see cref="IOilIntermediateLanguage"/>.</returns>
        new IOilProvider GetProvider();
    }
}
