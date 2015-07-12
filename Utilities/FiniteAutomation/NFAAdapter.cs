using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    public abstract class NFAAdapter<TCheck, TNFAState, TDFAState, TSourceElement, TAdapter, TAdapterContext> :
        FiniteAutomataAdapter<TCheck, TNFAState, List<TNFAState>, TSourceElement, TAdapter, List<TAdapter>, TAdapterContext>,
        INFAAdapter<TCheck, TNFAState, TDFAState, TSourceElement, TAdapter, TAdapterContext>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TNFAState :
            NFAState<TCheck, TNFAState, TDFAState, TSourceElement>
        where TDFAState :
            DFAState<TCheck, TNFAState, TDFAState, TSourceElement>,
            new()
        where TSourceElement :
            IFiniteAutomataSource
        where TAdapter :
            NFAAdapter<TCheck, TNFAState, TDFAState, TSourceElement, TAdapter, TAdapterContext>,
            new()
        where TAdapterContext :
            new()
    {
        protected override IFiniteAutomataTransitionTable<TCheck, TAdapter, List<TAdapter>> InitializeOutgoingTransitions()
        {
            return new FiniteAutomataMultiTargetTransitionTable<TCheck, TAdapter>(false);
        }

        public new IFiniteAutomataMultiTargetTransitionTable<TCheck, TAdapter> OutgoingTransitions
        {
            get { return (FiniteAutomataMultiTargetTransitionTable<TCheck, TAdapter>)base.OutgoingTransitions; }
        }

        public static TAdapter Adapt(TNFAState rootState)
        {
            Dictionary<TNFAState, TAdapter> dummyLookup = null;
            var result = Adapt(rootState, ref dummyLookup);
            dummyLookup.Clear();
            return result;
        }

        public static TAdapter Adapt(TNFAState rootState, ref Dictionary<TNFAState, TAdapter> quickLookup)
        {
            List<TNFAState> inputSet = new List<TNFAState>();
            NFAState<TCheck, TNFAState, TDFAState, TSourceElement>.FlatlineState(rootState, inputSet);
            if (!inputSet.Contains(rootState))
                inputSet.Insert(0, rootState);
            var resultSet = (from nfa in inputSet
                             let nfaContext = new TAdapterContext()
                             let nfaAdapter = new TAdapter() { AssociatedContext = nfaContext, AssociatedState = nfa }
                             select new { Adapter = nfaAdapter, State = nfa }).ToDictionary(nfAdapter => nfAdapter.State, nfAdapter => nfAdapter.Adapter);

            foreach (var nfa in inputSet)
                foreach (var transition in nfa.OutTransitions.Keys)
                {
                    resultSet[nfa].OutgoingTransitions.Add(transition, (from nfaTarget in nfa.OutTransitions[transition]
                                                                        select resultSet[nfaTarget]).ToList());
                }
            quickLookup = resultSet;
            return resultSet[rootState];
        }

    }
}
