using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    partial class DFAState<TCheck, TNFAState, TDFAState, TSourceElement>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TDFAState :
            DFAState<TCheck, TNFAState, TDFAState, TSourceElement>,
            new()
        where TNFAState :
            NFAState<TCheck, TNFAState, TDFAState, TSourceElement>
        where TSourceElement :
            IFiniteAutomataSource
    {
        private ConstructionContext[] contexts;
        private int contextSize;

        private class ConstructionContext :
            IDFAConstructionContext<TCheck, TNFAState, TDFAState, TSourceElement>
        {
            private TCheck check;
            List<TNFAState> series;
            public ConstructionContext(TCheck check, List<TNFAState> series)
            {
                this.check = check;
                this.series = series.ToList();
            }

            public IEnumerator<TNFAState> GetEnumerator()
            {
                foreach (var state in series)
                    yield return state;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            public TCheck ConstructionCondition
            {
                get { return this.check; }
            }

            internal void MergeWith(ConstructionContext constructionContext)
            {
                foreach (var element in constructionContext.series)
                    if (!this.series.Contains(element))
                        this.series.Add(element);
            }
        }
    }
}