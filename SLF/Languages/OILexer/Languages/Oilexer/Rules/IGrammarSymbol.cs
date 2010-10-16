using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer.Rules
{
    /// <summary>
    /// Defines properties and methods for working with
    /// a grammar symbol.
    /// </summary>
    public interface IGrammarSymbol
    {
        /// <summary>
        /// Returns the <see cref="String"/> value which
        /// represents the symbol's name.
        /// </summary>
        string ElementName { get; }
    }
}
