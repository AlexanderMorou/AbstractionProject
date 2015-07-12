using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    public class FiniteAutomataFlowGraphNode<TCheck, TState, TForwardNodeTarget, TSourceElement, TFlowGraphNode, TFlowGraphRoot> :
        KeyedTree<TCheck, TState, TFlowGraphNode>,
        IFiniteAutomataFlowGraphNode<TCheck, TState, TForwardNodeTarget, TSourceElement, TFlowGraphNode>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TState :
            IFiniteAutomataState<TCheck, TState, TForwardNodeTarget, TSourceElement>
        where TSourceElement :
            IFiniteAutomataSource
        where TFlowGraphNode :
            IFiniteAutomataFlowGraphNode<TCheck, TState, TForwardNodeTarget, TSourceElement, TFlowGraphNode>,
            new()
    {

        public bool PluralTarget
        {
            get
            {
                return (from iTransitionSet in this.Value.InTransitions.Values
                        from iTransition in iTransitionSet
                        select iTransition).Count() > 1;
            }
        }

        public TFlowGraphRoot Root { get; internal set; }

        public TState Value { get; set; }
    }
    public class FiniteAutomataFlowGraph<TCheck, TState, TForwardNodeTarget, TSourceElement, TFlowGraphNode, TFlowGraphRoot>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TState :
            IFiniteAutomataState<TCheck, TState, TForwardNodeTarget, TSourceElement>
        where TSourceElement :
            IFiniteAutomataSource
        where TFlowGraphNode :
            IFiniteAutomataFlowGraphNode<TCheck, TState, TForwardNodeTarget, TSourceElement, TFlowGraphNode>,
            new()
    {
        public IControlledCollection<TFlowGraphNode> Singletons { get; internal set; }

        public IControlledCollection<TFlowGraphNode> PluralTargets { get; internal set; }

        internal FiniteAutomataFlowGraph()
        {
        }
    }
    public class FiniteAutomataDFAFlowGraphNode<TCheck, TNFAState, TDFAState, TSourceElement, TFlowGraphNode, TFlowGraphRoot> :
        FiniteAutomataFlowGraphNode<TCheck, TDFAState, TDFAState, TSourceElement, TFlowGraphNode, TFlowGraphRoot>
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
        where TFlowGraphNode :
            FiniteAutomataFlowGraphNode<TCheck, TDFAState, TDFAState, TSourceElement, TFlowGraphNode, TFlowGraphRoot>,
            new()
        where TFlowGraphRoot :
            FiniteAutomataDFAFlowGraph<TCheck, TNFAState, TDFAState, TSourceElement, TFlowGraphNode, TFlowGraphRoot>,
            new()
    {
        
    }
    public class FiniteAutomataDFAFlowGraph<TCheck, TNFAState, TDFAState, TSourceElement, TFlowGraphNode, TFlowGraphRoot> :
        FiniteAutomataFlowGraph<TCheck, TDFAState, TDFAState, TSourceElement, TFlowGraphNode, TFlowGraphRoot>
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
        where TFlowGraphNode :
            FiniteAutomataFlowGraphNode<TCheck, TDFAState, TDFAState, TSourceElement, TFlowGraphNode, TFlowGraphRoot>,
            new()
        where TFlowGraphRoot :
            FiniteAutomataDFAFlowGraph<TCheck, TNFAState, TDFAState, TSourceElement, TFlowGraphNode, TFlowGraphRoot>,
            new()
    {

        public static TFlowGraphRoot CreateFlowGraph(TDFAState rootState)
        {
            var flatform = new List<TDFAState>();
            DFAState<TCheck, TNFAState, TDFAState, TSourceElement>.FlatlineState(rootState, flatform);
            if (!flatform.Contains(rootState))
                flatform.Insert(0, rootState);
            var resultLookup = new Dictionary<TDFAState, TFlowGraphNode>();
            TFlowGraphRoot rootGraph = new TFlowGraphRoot();
            foreach (var state in flatform)
            {
                TFlowGraphNode current = new TFlowGraphNode() { Value = state };
                resultLookup.Add(state, (TFlowGraphNode)(object)(current));
                current.Root = rootGraph;
            }
            foreach (var state in flatform)
                foreach (var check in state.OutTransitions.Keys)
                    resultLookup[state].InternalCast()._Add(check, resultLookup[state.OutTransitions[check]]);
            var pluralTargets =
                (from n in resultLookup.Values
                 where n.PluralTarget || n.Value == rootState
                 select n).ToArray();
            var singletons =
                resultLookup.Values.Except(pluralTargets).ToArray();
            rootGraph.PluralTargets = new ControlledCollection<TFlowGraphNode>(pluralTargets);
            rootGraph.Singletons = new ControlledCollection<TFlowGraphNode>(singletons);
            return rootGraph;
        }
    }
}
