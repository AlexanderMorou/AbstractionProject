using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer.Rules;
using AllenCopeland.Abstraction.Slf.Compilers.Oilexer;
using AllenCopeland.Abstraction.Slf.FiniteAutomata;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer.Rules
{
    public class SyntacticalNFAState :
        NFAState<GrammarVocabulary, SyntacticalNFAState, SyntacticalDFAState, IProductionRuleSource>
    {
        internal ParserBuilder builder;
        public SyntacticalNFAState(ParserBuilder builder)
        {
            this.builder = builder;
        }

        protected override SyntacticalDFAState GetDFAState()
        {
            lock (this.builder)
                return new SyntacticalDFAState(this.builder);
        }
    }
}
