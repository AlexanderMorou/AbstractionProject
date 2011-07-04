using System;
using System.Collections.Generic;
using System.Text;
/*---------------------------------------------------------------------\
| Copyright © 2011 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    /// <summary>
    /// Provides a base implementation of a single-target transition table.
    /// </summary>
    /// <typeparam name="TCheck">The kind of <see cref="IFiniteAutomataSet{TCheck}"/>
    /// which is used as a condition to changing to the
    /// next <typeparamref name="TState"/>.</typeparam>
    /// <typeparam name="TState">The type of state represented by the targets
    /// of the transition table.</typeparam>
    public class FiniteAutomataSingleTargetTransitionTable<TCheck, TState> :
        FiniteAutomataTransitionTable<TCheck, TState, TState>,
        IFiniteAutomataSingleTargetTransitionTable<TCheck, TState>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TState :
            IFiniteAutomataState<TCheck, TState>
    {
        /// <summary>
        /// Inserts a new item into the <see cref="FIniteAutomataSingleTargetTransitionTable{TCheck, TState}"/>
        /// with the <paramref name="check"/> and <paramref name="target"/> 
        /// provided.
        /// </summary>
        /// <param name="check">The <typeparamref name="TCheck"/> that denotes the 
        /// condition for transitioning into the <paramref name="target"/> state.</param>
        /// <param name="target">The <typeparamref name="TState"/> to be transitioned into
        /// by the <paramref name="check"/> provided.</param>
        public override void Add(TCheck check, TState target)
        {
            IDictionary<TCheck, IFiniteAutomataTransitionNode<TCheck, TState>> colliders;
            var remainder = base.GetColliders(check, out colliders);
            if (colliders.Count > 0)
                throw new ArgumentException("target");
            base.AddInternal(check, target);
        }

        /// <summary>
        /// Obtains the target state of a given transition.
        /// </summary>
        /// <param name="state">The <typeparamref name="TState"/>
        /// to obtain a target for.</param>
        /// <returns>The <paramref name="state"/> itself, given the 
        /// singleton nature of the targets of the table.</returns>
        protected override TState GetStateTarget(TState state)
        {
            return state;
        }

        /// <summary>
        /// The <see cref="IEnumerable{T}"/> series that denotes the
        /// full set of states transitioned into by the 
        /// <see cref="FIniteAutomataSingleTargetTransitionTable{TCheck, TState}"/>.
        /// </summary>
        public override IEnumerable<TState> Targets
        {
            get { return this.Values; }
        }

    }
}
