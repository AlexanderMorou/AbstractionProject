using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
    /// <typeparam name="TNFAState">The kind of <see cref="INFAState{TCheck, TNFAState, TDFAState, TSourceElement}"/>
    /// used to represent the non-deterministic elements of the
    /// automation.</typeparam>
    /// <typeparam name="TDFAState">The type used to construct
    /// a deterministic model of the current nondeterministic 
    /// automation.</typeparam>
    /// <typeparam name="TSourceElement">The kind of elements from 
    /// which the 
    /// <see cref="INFAState{TCheck, TNFAState, TDFAState, TSourceElement}"/>
    /// was derived.</typeparam>
    public interface INFAState<TCheck, TNFAState, TDFAState, TSourceElement> :
        IFiniteAutomataState<TCheck, TNFAState, List<TNFAState>, TSourceElement>
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
    {
        /// <summary>
        /// Creates a version of the current <see cref="INFAState{TCheck, TNFAState, TDFAState, TSourceElement}"/>
        /// which is deterministic by creating a left-side union on elements which overlap
        /// on their <typeparamref name="TCheck"/> transition requirements.
        /// </summary>
        /// <returns></returns>
        TDFAState DeterminateAutomata();
        /// <summary>
        /// Returns a multi-target transition table for the states
        /// leaving the automation.
        /// </summary>
        new IFiniteAutomataMultiTargetTransitionTable<TCheck, TNFAState> OutTransitions { get; }
        /// <summary>
        /// Creates a concatination between the current state's edges and
        /// the <paramref name="target"/> <typeparamref name="TNFAState"/>.
        /// </summary>
        /// <param name="target">The <typeparamref name="TNFAState"/>
        /// to concatenate with the current state.</param>
        void Concat(TNFAState target);
        /// <summary>
        /// Creates a union between the current state's transitions and
        /// the <paramref name="target"/> <typeparamref name="TNFAState"/>.
        /// </summary>
        /// <param name="target">The <typeparamref name="TNFAState"/>
        /// to create a union with.</param>
        void Union(TNFAState target);
        /// <summary>
        /// Creates a relative compliment between the current state's
        /// transitions and the edge points of the <paramref name="target"/>.
        /// </summary>
        /// <param name="target">The <typeparamref name="TNFAState"/>
        /// to create a relative compliment on.</param>
        /// <remarks>The edges of the <paramref name="target"/>
        /// and the current state that overlap are explicitly marked
        /// as non-edges.</remarks>
        void RelativeComplement(TNFAState target);

    }
}
