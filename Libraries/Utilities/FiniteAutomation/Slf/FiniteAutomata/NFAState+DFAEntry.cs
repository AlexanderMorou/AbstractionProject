using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
        private class DFAEntry :
            HashSet<TNFAState>
        {
            public TDFAState DFA { get; private set; }
            public TCheck Check { get; private set; }
            public DFAEntry(List<TNFAState> states, TCheck check, TDFAState state)
                : base(states)
            {
                this.DFA = state;
                state.PushContext(check, states);
                bool noEdge = states.Any(p => p.ForcedNoEdge);
                bool isEdge = states.Any(p => p.IsEdge);
                if (noEdge)
                    this.DFA.ForcedNoEdge = true;
                else if (isEdge)
                    this.DFA.IsEdge = true;
            }

            public bool Equals(List<TNFAState> sequence)
            {
                return this.Count == sequence.Count &&
                       sequence.All(p => this.Contains(p));
            }

        }
    }
}
