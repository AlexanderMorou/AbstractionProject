using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    /// <summary>
    /// Defines properties and methods for working with a transition table
    /// that has multiple targets for a given <typeparamref name="TCheck"/>
    /// condition, yielding a non-deterministic state machine.
    /// </summary>
    /// <typeparam name="TCheck">The type of set used in the
    /// automation.</typeparam>
    /// <typeparam name="TState">The type of state which contains
    /// the <see cref="IFiniteAutomataMultiTargetTransitionTable{TCheck, TState}"/>.</typeparam>
    public interface IFiniteAutomataMultiTargetTransitionTable<TCheck, TState> : 
        IFiniteAutomataTransitionTable<TCheck, TState, List<TState>>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TState :
            IFiniteAutomataState<TCheck, TState>
    {
        /// <summary>
        /// Removes a state by the given target.
        /// </summary>
        /// <param name="check">The <typeparamref name="TCheck"/>
        /// denoting the condition for the transition.</param>
        /// <param name="target">The <typeparamref name="TState"/>
        /// to remove from the set.</param>
        void Remove(TCheck check, TState target);
    }
}
