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
        private class DFAEntrySet :
            List<DFAEntry>
        {
            public TCheck Condition { get; private set; }
            public DFAEntrySet(TCheck condition)
            {
                this.Condition = condition;
            }
            public bool Contains(List<TNFAState> set)
            {
                return this.Any(p => p.Equals(set));
            }
            public DFAEntry this[List<TNFAState> set]
            {
                get
                {
                    return this.First(p => p.Equals(set));
                }
            }
        }
    }
}
