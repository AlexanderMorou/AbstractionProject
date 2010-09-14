using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer.Tokens;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer.Rules
{
    /// <summary>
    /// Defines properties and methods for working with a language's
    /// symbols.
    /// </summary>
    public interface IGrammarSymbolSet :
        IControlledStateCollection<IGrammarSymbol>
    {
        IGrammarSymbol this[ITokenEntry entry] { get; }
        IGrammarSymbol this[ILiteralTokenItem entry] { get; }
        IGrammarSymbol this[IProductionRuleEntry entry] { get; }
    }
}
