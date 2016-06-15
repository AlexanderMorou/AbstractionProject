using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    public abstract class DFAAdapter<TCheck, TNFAState, TDFAState, TSourceElement, TAdapter, TAdapterContext> :
        FiniteAutomataAdapter<TCheck, TDFAState, TDFAState, TSourceElement, TAdapter, TAdapter, TAdapterContext>,
        IDFAAdapter<TCheck, TNFAState, TDFAState, TSourceElement, TAdapter, TAdapterContext>
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
            DFAAdapter<TCheck, TNFAState, TDFAState, TSourceElement, TAdapter, TAdapterContext>,
            new()
        where TAdapterContext :
            new()
    {
        public DFAAdapter()
        {
        }

        protected override IFiniteAutomataTransitionTable<TCheck, TAdapter, TAdapter> InitializeOutgoingTransitions()
        {
            return new FiniteAutomataSingleTargetTransitionTable<TCheck, TAdapter>();
        }

        public static TAdapter Adapt(TDFAState rootState, object connectionContext = null)
        {
            Dictionary<TDFAState, TAdapter> dummyLookup = null;
            var result = Adapt(rootState, ref dummyLookup, connectionContext);
            dummyLookup.Clear();
            return result;
        }

        public static TAdapter Adapt(TDFAState rootState, ref Dictionary<TDFAState, TAdapter> quickLookup, object connectionContext = null)
        {
            List<TDFAState> inputSet = new List<TDFAState>();
            bool connectRoot;
            DFAState<TCheck, TNFAState, TDFAState, TSourceElement>.FlatlineState(rootState, inputSet);
            if (connectRoot = !inputSet.Contains(rootState))
                inputSet.Insert(0, rootState);
            var resultSet = (from dfa in inputSet
                             let dfaContext = new TAdapterContext()
                             let dfaAdapter = new TAdapter() { AssociatedContext = dfaContext, AssociatedState = dfa }
                             select new { Adapter = dfaAdapter, State = dfa }).ToDictionary(dfAdapter => dfAdapter.State, dfAdapter => dfAdapter.Adapter);

            foreach (var dfa in inputSet)
                foreach (var transition in dfa.OutTransitions.Keys)
                    resultSet[dfa].OutgoingTransitions.Add(transition, resultSet[dfa.OutTransitions[transition]]);
            if (connectRoot)
                resultSet[rootState].ConnectContext(connectionContext);

            foreach (var dfa in inputSet)
            {
                foreach (var transition in dfa.OutTransitions.Keys)
                    resultSet[dfa.OutTransitions[transition]].ConnectContext(connectionContext);
            }
            quickLookup = resultSet;
            return resultSet[rootState];
        }

    }
}
