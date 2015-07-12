using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    partial class NFAState<TCheck, TNFAState, TDFAState, TSourceElement>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TNFAState :
            NFAState<TCheck, TNFAState, TDFAState, TSourceElement>
        where TDFAState :
            DFAState<TCheck, TNFAState, TDFAState, TSourceElement>,
            IDFAState<TCheck, TNFAState, TDFAState, TSourceElement>,
            new()
        where TSourceElement :
            IFiniteAutomataSource
    {
        private class DFAEntryTable :
            Dictionary<TCheck, DFAEntrySet>
        {
            private Func<TDFAState> GetState;
            public DFAEntryTable(Func<TDFAState> getState)
            {
                this.GetState = getState;
            }
            public bool ContiansEntryFor(TCheck condition, List<TNFAState> set)
            {
                if (!this.ContainsKey(condition))
                    return false;
                return this[condition].Contains(set);
            }

            public DFAEntry Add(TCheck condition, List<TNFAState> set)
            {
                if (!this.ContainsKey(condition))
                    base.Add(condition, new DFAEntrySet(condition));
                if (!this[condition].Contains(set))
                {
                    var state = GetState();
                    state.IsReductionSite = set.Any(nfa => nfa.IsReductionSite);
                    this[condition].Add(new DFAEntry(set, condition, state));
                }
                return this[condition][set];
            }
        }
    }
}
