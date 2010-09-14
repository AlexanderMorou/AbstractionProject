using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer.Tokens;
using AllenCopeland.Abstraction.Slf.FiniteAutomata;
namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer.Tokens
{
    /// <summary>
    /// Provides a regular language deterministic state.
    /// </summary>
    public class RegularLanguageDFAState :
        DFAState<RegularLanguageSet, RegularLanguageDFAState, ITokenSource>
    {
        public RegularLanguageDFAState()
        {
        }

        protected override bool SourceSetPredicate(ITokenSource source)
        {
            if (!(source is ITokenItem))
                return false;
            var tSource = (ITokenItem)source;
            return !string.IsNullOrEmpty(tSource.Name);
        }

        protected override IFiniteAutomataTransitionTable<RegularLanguageSet, RegularLanguageDFAState, RegularLanguageDFAState> InitializeOutTransitionTable()
        {
            return new RegularLanguageDFATransitionTable();
        }
    }
}
