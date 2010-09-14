using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer.Tokens;
using AllenCopeland.Abstraction.Slf.FiniteAutomata;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer.Tokens
{
    public class RegularLanguageNFAState :
        NFAState<RegularLanguageSet, RegularLanguageNFAState, RegularLanguageDFAState, ITokenSource>
    {

        public RegularLanguageNFAState()
        {
        }


    }
}
