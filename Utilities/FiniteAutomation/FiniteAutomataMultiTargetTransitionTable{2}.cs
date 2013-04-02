using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Provides a base implementation of a multiple target transition table
    /// which yields a non-deterministic state machine.
    /// </summary>
    /// <typeparam name="TCheck">The type of set used in the
    /// automation.</typeparam>
    /// <typeparam name="TState">The type of state which contains
    /// the <see cref="FiniteAutomataMultiTargetTransitionTable{TCheck, TState}"/>.</typeparam>
    public class FiniteAutomataMultiTargetTransitionTable<TCheck, TState> :
        FiniteAutomataTransitionTable<TCheck, TState, List<TState>>,
        IFiniteAutomataMultiTargetTransitionTable<TCheck, TState>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TState :
            IFiniteAutomataState<TCheck, TState>
    {
        private bool autoSegment;

        /// <summary>
        /// Creates a new <see cref="FiniteAutomataMultiTargetTransitionTable{TCheck, TState}"/>
        /// initialized to its default state.
        /// </summary>
        public FiniteAutomataMultiTargetTransitionTable()
            : this(true)
        {
        }

        /// <summary>
        /// Creates a new <see cref="FiniteAutomataMultiTargetTransitionTable{TCheck, TState}"/>
        /// with the <paramref name="autoSegment"/> condition provided.
        /// </summary>
        /// <param name="autoSegment">Determines whether the transition table should 
        /// automatically break up transitions if an intersection occurs.</param>
        public FiniteAutomataMultiTargetTransitionTable(bool autoSegment)
        {
            this.autoSegment = autoSegment;
        }

        /// <summary>
        /// Adds a <typeparamref name="TCheck"/> condition with the series
        /// of states provided as the non-deterministic target.
        /// </summary>
        /// <param name="check">The <typeparamref name="TCheck"/>
        /// that determines the transitionary condition.</param>
        /// <param name="target">The series of <typeparamref name="TState"/>
        /// instances which denote the non-deterministic target.</param>
        public override void Add(TCheck check, List<TState> target)
        {
            if (autoSegment)
            {
                IDictionary<TCheck, IFiniteAutomataTransitionNode<TCheck, List<TState>>> colliders;
                var remainder = base.GetColliders(check, out colliders);
                /* *
                 * If the intersection is the full condition, check to see
                 * if there's a node that exactly matches, if so, just add the
                 * state to the node target.
                 * */
                if (colliders.Count == 1 && remainder.IsEmpty)
                {
                    var first = colliders.First();
                    if (!base.ContainsKey(first.Key))
                        goto alternate;
                    var currentNode = base.GetNode(check);
                    foreach (var state in target)
                    {
                        if (!currentNode.Target.Contains(state))
                            currentNode.Target.Add(state);
                    }
                    goto altSkip;
                }
            alternate:
                /* *
                 * Otherwise, iterate the collision sections
                 * and break apart the current entry with
                 * the intersection, repeat until all
                 * colliding nodes are finished.
                 * */
                foreach (var intersection in colliders.Keys)
                {
                    var currentNode = colliders[intersection];
                    base.Remove(currentNode.Check);
                    var nodeRemainder = currentNode.Check.SymmetricDifference(intersection);
                    if (!nodeRemainder.IsEmpty)
                        base.AddInternal(nodeRemainder, currentNode.Target);
                    List<TState> targetSet = new List<TState>(currentNode.Target);
                    foreach (var subTarget in target)
                        if (!targetSet.Contains(subTarget))
                            targetSet.Add(subTarget);
                    base.AddInternal(intersection, targetSet);
                }
                if (!remainder.IsEmpty)
                    base.AddInternal(remainder, new List<TState>(target));
            altSkip: ;
            }
            else if (base.ContainsKey(check))
            {
                /* *
                 * Obtain the current node, and simply add the target
                 * elements to the node.
                 * */
                var currentNode = base.GetNode(check);
                foreach (var state in target)
                    if (!currentNode.Target.Contains(state))
                        currentNode.Target.Add(state);
            }
            else
                /* *
                 * If auto-segmentation isn't active and the node
                 * isn't exactly present in the current setup...
                 * */
                base.AddInternal(check, target);
        }

        protected override List<TState> GetStateTarget(TState state)
        {
            return new List<TState>() { state };
        }

        public override IEnumerable<TState> Targets
        {
            get
            {
                HashSet<TState> targetsPassed = new HashSet<TState>();
                foreach (var set in this.Values)
                    foreach (var state in set)
                        if (!targetsPassed.Contains(state))
                        {
                            targetsPassed.Add(state);
                            yield return state;
                        }
            }
        }

        /// <summary>
        /// Removes a state by the given target.
        /// </summary>
        /// <param name="check">The <typeparamref name="TCheck"/>
        /// denoting the condition for the transition.</param>
        /// <param name="target">The <typeparamref name="TState"/>
        /// to remove from the set.</param>
        public void Remove(TCheck check, TState target)
        {
            if (!this.ContainsKey(check))
                throw new KeyNotFoundException();
            var currentNode = base.GetNode(check);
            if (currentNode.Target.Count > 1)
                if (!currentNode.Target.Contains(target))
                    throw new ArgumentOutOfRangeException("target");
                else
                    currentNode.Target.Remove(target);
            else
                base.Remove(check);
        }
    }
}
