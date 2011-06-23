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
    /// Defines properties and methods for working with a 
    /// nondeterministic finite state automation which has a
    /// fixed set of transitions from one state to another.
    /// </summary>
    /// <typeparam name="TCheck">The type of set used
    /// to represent the transition from state set to state set.</typeparam>
    /// <typeparam name="TState">The kind of <see cref="INFAState{TCheck, TState, TDFA, TSourceElement}"/>
    /// used to represent the non-deterministic elements of the
    /// automation.</typeparam>
    /// <typeparam name="TDFA">The type used to construct
    /// a deterministic model of the current nondeterministic 
    /// automation.</typeparam>
    /// <typeparam name="TSourceElement">The kind of elements from the original
    /// parse tree which denote the source of the
    /// <see cref="INFAState{TCheck, TState, TDFA, TSourceElement}"/>.</typeparam>
    public interface INFAState<TCheck, TState, TDFA, TSourceElement> :
        IFiniteAutomataState<TCheck, TState, List<TState>, TSourceElement>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TState :
            INFAState<TCheck, TState, TDFA, TSourceElement>
        where TDFA :
            IDFAState<TCheck, TDFA, TSourceElement>,
            new()
        where TSourceElement :
            IFiniteAutomataSource
    {
        /// <summary>
        /// Creates a version of the current <see cref="INFAState{TCheck, TState, TDFA, TSourceElement}"/>
        /// which is deterministic by creating a left-side union on elements which overlap
        /// on their <typeparamref name="TCheck"/> transition requirements.
        /// </summary>
        /// <returns></returns>
        TDFA DeterminateAutomata();
        new IFiniteAutomataMultiTargetTransitionTable<TCheck, TState> OutTransitions { get; }

        void Concat(TState target);
        void Union(TState target);
        void RelativeComplement(TState target);

    }
}
