using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    /// <summary>
    /// Defines properties and methods for working with a generalized finite
    /// automata state.
    /// </summary>
    /// <typeparam name="TCheck">The type of set used in the
    /// automation.</typeparam>
    /// <typeparam name="TState">The type of state used in the automation.</typeparam>
    /// <typeparam name="TForwardNodeTarget">The type used to denote the target
    /// container for the transition table.</typeparam>
    /// <typeparam name="TSourceElement">The type used to represent the
    /// source elements from which the automation is derived.</typeparam>
    public interface IFiniteAutomataState<TCheck, TState, TForwardNodeTarget, TSourceElement> :
        IFiniteAutomataState<TCheck, TState>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TState :
            IFiniteAutomataState<TCheck, TState, TForwardNodeTarget, TSourceElement>
        where TSourceElement :
            IFiniteAutomataSource
    {
        new IFiniteAutomataTransitionTable<TCheck, TState, TForwardNodeTarget> OutTransitions { get; }
    }

    /// <summary>
    /// Defines properties and methods for working with a generalized finite
    /// automata state.
    /// </summary>
    /// <typeparam name="TCheck">The type of set used in the
    /// automation.</typeparam>
    /// <typeparam name="TState">The type of state used in the automation.</typeparam>
    public interface IFiniteAutomataState<TCheck, TState>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TState :
            IFiniteAutomataState<TCheck, TState>
    {
        /// <summary>
        /// Returns/sets whether the current <see cref="IFiniteAutomataState{TCheck, TState}"/>
        /// is an edge state.
        /// </summary>
        bool IsEdge { get; set; }
        /// <summary>
        /// Returns whether the current <see cref="IFiniteAutomataState{TCheck, TState}"/> 
        /// is marked as an edge state regardless of the number of
        /// outgoing transitions.
        /// </summary>
        bool IsMarked { get; }
        /// <summary>
        /// Returns/sets whether the current <see cref="IFiniteAutomataState{TCheck, TState}"/> 
        /// has been forced into not being an edge state.
        /// </summary>
        bool ForcedNoEdge { get; set; }

        /// <summary>
        /// Returns the <see cref="IFiniteAutomataMultiTargetTransitionTable{TCheck, TState}"/>
        /// which denotes the conditions for and the states that target the current state.
        /// </summary>
        IFiniteAutomataMultiTargetTransitionTable<TCheck, TState> InTransitions { get; }
        /// <summary>
        /// Returns the <see cref="IFiniteAutomataTransitionTable{TCheck, TState}"/>
        /// that denotes the outgoing transitions from the <see cref="IFiniteAutomataState{TCheck, TState}"/>.
        /// </summary>
        IFiniteAutomataTransitionTable<TCheck, TState> OutTransitions { get; }
        /// <summary>
        /// Obtains the edges of the current
        /// <see cref="IFiniteAutomataState{TCheck, TState}"/>
        /// which are plausible points to terminate the current state.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> instance which
        /// yields the edges of the current 
        /// <see cref="IFiniteAutomataState{TCheck, TState}"/>.
        /// </returns>
        IEnumerable<TState> ObtainEdges();
        /// <summary>
        /// Creates a transition from the current 
        /// <see cref="IFiniteAutomataState{TCheck, TState}"/>
        /// to the <paramref name="target"/> with the
        /// <paramref name="condition"/> for transition provided.
        /// </summary>
        /// <param name="condition">The <typeparamref name="TCheck"/>
        /// which restricts the move.</param>
        /// <param name="target">The <typeparamref name="TState"/>
        /// to move into.</param>
        void MoveTo(TCheck condition, TState target);
        /// <summary>
        /// Returns the <see cref="Int32"/> value unique to the current
        /// state.
        /// </summary>
        int StateValue { get; }
    }
}
