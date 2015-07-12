using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    /// <summary>
    /// Defines properties and methods for working with an adapter
    /// for a non-deterministic finite automation to store additional
    /// information or functionality beyond the scope of the initial
    /// automation.
    /// </summary>
    /// <typeparam name="TCheck">The type of set used
    /// to represent the transition from state set to state set.</typeparam>
    /// <typeparam name="TNFAState">The kind of <see cref="INFAState{TCheck, TNFAState, TDFAState, TSourceElement}"/>
    /// used to represent the non-deterministic elements of the
    /// automation.</typeparam>
    /// <typeparam name="TDFAState">The type used to construct
    /// a deterministic model of the current nondeterministic 
    /// automation.</typeparam>
    /// <typeparam name="TSourceElement">The kind of elements from the original
    /// parse tree which denote the source of the
    /// <see cref="INFAState{TCheck, TNFAState, TDFAState, TSourceElement}"/>.</typeparam>
    /// <typeparam name="TAdapter"></typeparam>
    /// <typeparam name="TAdapterContext"></typeparam>
    public interface INFAAdapter<TCheck, TNFAState, TDFAState, TSourceElement, TAdapter, TAdapterContext> :
        IFiniteAutomataAdapter<TCheck, TNFAState, List<TNFAState>, TSourceElement, TAdapter, List<TAdapter>, TAdapterContext>
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
        where TAdapter :
            INFAAdapter<TCheck, TNFAState, TDFAState, TSourceElement, TAdapter, TAdapterContext>,
            new()
        where TAdapterContext :
            new()
    {
        /// <summary>
        /// Returns the <see cref="IFiniteAutomataMultiTargetTransitionTable{TCheck, TAdapter}"/>
        /// which was adapted from the <typeparamref name="TNFAState"/> outgoing transitions.
        /// </summary>
        new IFiniteAutomataMultiTargetTransitionTable<TCheck, TAdapter> OutgoingTransitions { get; }
    }
}
