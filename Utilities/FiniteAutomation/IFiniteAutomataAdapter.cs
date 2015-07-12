using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    /// <summary>
    /// Defines properties and methods for working with an adapter
    /// for a finite automation to store additional information or 
    /// functionality beyond the scope of the initial automation.
    /// </summary>
    /// <remarks>For cases where an automation is used in multiple 
    /// contexts it might be useful to construct adapters to
    /// yield supplemental implementation for the different machines.</remarks>
    /// <typeparam name="TCheck">The type of set used in the
    /// automation.</typeparam>
    /// <typeparam name="TState">The type of state used in the automation.</typeparam>
    /// <typeparam name="TForwardNodeTarget">The type used to denote the target
    /// container for the transition table.</typeparam>
    /// <typeparam name="TSourceElement">The type used to represent the
    /// source elements from which the automation is derived.</typeparam>
    /// <typeparam name="TAdapter">The type of adapter used to 
    /// connect the finite automata concept with a supplemental context.</typeparam>
    /// <typeparam name="TAdapterContext">The creatable type which supplements
    /// the <typeparamref name="TState"/> instances with extra context.</typeparam>
    public interface IFiniteAutomataAdapter<TCheck, TState, TForwardNodeTarget, TSourceElement, TAdapter, TForwardAdapterTarget, TAdapterContext>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TState :
            IFiniteAutomataState<TCheck, TState, TForwardNodeTarget, TSourceElement>
        where TSourceElement :
            IFiniteAutomataSource
        where TAdapter :
            IFiniteAutomataAdapter<TCheck, TState, TForwardNodeTarget, TSourceElement, TAdapter, TForwardAdapterTarget, TAdapterContext>,
            new()
        where TAdapterContext :
            new()
    {
        /// <summary>
        /// Returns the <typeparamref name="TState"/> from which
        /// the <see cref="IFiniteAutomataAdapter{TCheck, TState, TForwardNodeTarget, TSourceElement, TAdapter, TAdapterContext}"/>
        /// was derived.
        /// </summary>
        TState AssociatedState { get; }
        /// <summary>
        /// Returns the <typeparamref name="TAdapterContext"/> which 
        /// is associated to the <see cref="AssociatedState"/>.
        /// </summary>
        TAdapterContext AssociatedContext { get; }
        /// <summary>
        /// Returns the <see cref="IFiniteAutomataTransitionTable{TCheck, TState, TForwardAdapterTarget}"/>
        /// which adapts the outgoing transitions of the <typeparamref name="TState"/>
        /// to the <typeparamref name="TForwardAdapterTarget"/>.
        /// </summary>
        IFiniteAutomataTransitionTable<TCheck, TAdapter, TForwardAdapterTarget> OutgoingTransitions { get; }

        void ConnectContext(object connectContext);
    }
}
