using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    /// <summary>
    /// Defines properties and methods for working with a generic
    /// transition table for a <see cref="IFiniteAutomataState{TCheck, TState}"/>.
    /// </summary>
    /// <typeparam name="TCheck">The type of set used in the
    /// automation.</typeparam>
    /// <typeparam name="TState">The type of state which contains
    /// the <see cref="IFiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/>.</typeparam>
    /// <typeparam name="TNodeTarget">The type used to denote the target
    /// container for the transition table.</typeparam>
    public interface IFiniteAutomataTransitionTable<TCheck, TState, TNodeTarget> :
        IControlledStateDictionary<TCheck, TNodeTarget>,
        IFiniteAutomataTransitionTable<TCheck, TState>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TState :
            IFiniteAutomataState<TCheck, TState>
    {
        /// <summary>
        /// Performs a table wide memberwise intersection
        /// looking for overlaps on the <paramref name="condition"/> provided;
        /// yields a dictionary of <paramref name="colliders"/>
        /// which denote the points of overlap.
        /// </summary>
        /// <param name="condition">The <typeparamref name="TCheck"/>
        /// to look for collisions of.</param>
        /// <param name="colliders">The outgoing dictionary of elements that denote the points of
        /// overlap.</param>
        /// <returns>A <typeparamref name="TCheck"/> value that denotes 
        /// the symmetric difference of the points of intersection and
        /// the original <paramref name="condition"/>.</returns>
        TCheck GetColliders(TCheck condition, out  IDictionary<TCheck, IFiniteAutomataTransitionNode<TCheck, TNodeTarget>> colliders);
        new IControlledStateCollection<TCheck> Keys { get; }
        void Add(TCheck check, TNodeTarget target);
        IFiniteAutomataTransitionNode<TCheck, TNodeTarget> GetNode(TCheck key);
    }
    /// <summary>
    /// Defines properties and methods for working with a generalized
    /// transition table for a <see cref="IFiniteAutomataState{TCheck, TState}"/>.
    /// </summary>
    /// <typeparam name="TCheck">The type of set used in the
    /// automation.</typeparam>
    /// <typeparam name="TState">The type of state which contains
    /// the <see cref="IFiniteAutomataTransitionTable{TCheck, TState}"/>.</typeparam>
    public interface IFiniteAutomataTransitionTable<TCheck, TState>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TState :
            IFiniteAutomataState<TCheck, TState>
    {
        IControlledStateCollection<TCheck> Keys { get; }
        /// <summary>
        /// Adds a state to the transition table by the 
        /// <paramref name="check"/> required for the transition
        /// and the <paramref name="target"/> that results after
        /// the transition.
        /// </summary>
        /// <param name="check">The <typeparamref name="TCheck"/>
        /// which denotes the transition requirement for the change in state.
        /// </param>
        /// <param name="target">The <typeparamref name="TState"/>
        /// that acts as the target of the transition.</param>
        void AddState(TCheck check, TState target);
        /// <summary>
        /// Removes the transition that equals the <typeparamref name="TCheck"/>
        /// provided.
        /// </summary>
        /// <param name="check">The <typeparamref name="TCheck"/>
        /// that denotes the transition requirement to remove.</param>
        void Remove(TCheck check);
        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> of
        /// targets contained within the current transition table.
        /// </summary>
        IEnumerable<TState> Targets { get; }
        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> of 
        /// conditions which are required to transition into the 
        /// <see cref="Targets"/> of the <see cref="IFiniteAutomataTransitionTable{TCheck, TState}"/>.
        /// </summary>
        IEnumerable<TCheck> Checks { get; }
        /// <summary>
        /// Returns the <typeparamref name="TCheck"/> of all of the transition checks
        /// combined into one.
        /// </summary>
        TCheck FullCheck { get; }
    }
}
