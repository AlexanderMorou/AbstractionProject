using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    public interface IFiniteAutomataFlowGraphNode<TCheck, TState, TForwardNodeTarget, TSourceElement, TFlowGraphNode> :
        IKeyedTreeNode<TCheck, TState, TFlowGraphNode>
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
        /// <summary>
        /// Returns whether the <see cref="IFiniteAutomataFlowGraphNode{TCheck, TState, TForwardNodeTarget, TSourceElement, TFlowGraphNode}"/>
        /// is targeted by multiple other nodes.
        /// </summary>
        /// <remarks>Useful for knowing, when building a direct code representation, which nodes can be injected inline.
        /// Multiple target states aren't a solid target because they might recurse, which would cause problems when
        /// generating the code tree inline.</remarks>
        bool PluralTarget { get; }
    }
}
