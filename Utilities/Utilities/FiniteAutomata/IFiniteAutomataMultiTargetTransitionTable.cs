using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    /// <summary>
    /// Defines properties and methods for working 
    /// </summary>
    /// <typeparam name="TCheck"></typeparam>
    /// <typeparam name="TState"></typeparam>
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
