using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer.Rules
{
    /// <summary>
    /// Defines properties and methods for working with a grammar
    /// symbol which references a rule.
    /// </summary>
    public interface IGrammarRuleSymbol :
        IGrammarSymbol
    {
        /// <summary>
        /// Returns the <see cref="IProductionRuleEntry"/>
        /// on which the <see cref="IGrammarRuleSymbol"/> is based.
        /// </summary>
        IProductionRuleEntry Source { get; }
    }
}
