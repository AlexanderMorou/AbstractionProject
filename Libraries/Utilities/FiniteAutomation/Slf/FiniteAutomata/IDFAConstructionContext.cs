using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    public interface IDFAConstructionContext<TCheck, TNFAState, TDFAState, TSourceElement> :
        IEnumerable<TNFAState>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TNFAState :
            INFAState<TCheck, TNFAState, TDFAState, TSourceElement>
        where TDFAState :
            IDFAState<TCheck, TNFAState, TDFAState, TSourceElement>,
            new()
        where TSourceElement :
            IFiniteAutomataSource
    {
        /// <summary>
        /// Returns the <see cref="TCheck"/> which constrained
        /// the deterministic state's creation.
        /// </summary>
        TCheck ConstructionCondition { get; }
    }
}
