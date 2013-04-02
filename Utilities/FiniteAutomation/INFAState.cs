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
        /// <summary>
        /// Returns a multi-target transition table for the states
        /// leaving the automation.
        /// </summary>
        new IFiniteAutomataMultiTargetTransitionTable<TCheck, TState> OutTransitions { get; }
        /// <summary>
        /// Creates a concatination between the current state's edges and
        /// the <paramref name="target"/> <typeparamref name="TState"/>.
        /// </summary>
        /// <param name="target">The <typeparamref name="TState"/>
        /// to concatenate with the current state.</param>
        void Concat(TState target);
        /// <summary>
        /// Creates a union between the current state's transitions and
        /// the <paramref name="target"/> <typeparamref name="TState"/>.
        /// </summary>
        /// <param name="target">The <typeparamref name="TState"/>
        /// to create a union with.</param>
        void Union(TState target);
        /// <summary>
        /// Creates a relative compliment between the current state's
        /// transitions and the edge points of the <paramref name="target"/>.
        /// </summary>
        /// <param name="target">The <typeparamref name="TState"/>
        /// to create a relative compliment on.</param>
        /// <remarks>The edges of the <paramref name="target"/>
        /// and the current state that overlap are explicitly marked
        /// as non-edges.</remarks>
        void RelativeComplement(TState target);

    }
}
