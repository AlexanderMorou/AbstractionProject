using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Defines properties and methods for working with a deterministic
    /// automation which yields a single target per transition.
    /// </summary>
    /// <typeparam name="TCheck">The type of set used
    /// to represent the transition from state set to state set.</typeparam>
    /// <typeparam name="TNFAState">The kind of 
    /// <see cref="INFAState{TCheck, TNFAState, TDFAState, TSourceElement}"/>
    /// used to represent the non-deterministic elements of the
    /// automation.</typeparam>
    /// <typeparam name="TDFAState">The type used to construct
    /// a deterministic model of the current nondeterministic 
    /// automation.</typeparam>
    /// <typeparam name="TSourceElement">The kind of elements from 
    /// which the 
    /// <see cref="IDFAState{TCheck, TNFAState, TDFAState, TSourceElement}"/>
    /// was derived.</typeparam>
    public interface IDFAState<TCheck, TNFAState, TDFAState, TSourceElement> :
        IFiniteAutomataState<TCheck, TDFAState, TDFAState, TSourceElement>
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
        /// Returns the <see cref="IFiniteAutomataSingleTargetTransitionTable{TCheck, TState}"/>
        /// which denotes the table of single-target transition nodes going away
        /// from the current state.
        /// </summary>
        new IFiniteAutomataSingleTargetTransitionTable<TCheck, TDFAState> OutTransitions { get; }
        void ReduceSources();
        /// <summary>
        /// Returns/sets whether the resulted state is a reduction point.
        /// </summary>
        /// <remarks>Reduction sites reduce to their smallest form at all
        /// times.</remarks>
        bool IsReductionSite { get; set; }
        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> of <see cref="IDFAConstructionContext{TCheck, TNFAState, TDFAState, TSourceElement}"/>
        /// which denote where the <see cref="IDFAState{TCheck, TNFAState, TDFAState, TSourceElement}"/>
        /// was derived from.
        /// </summary>
        IEnumerable<IDFAConstructionContext<TCheck, TNFAState, TDFAState, TSourceElement>> ConstructionSources { get; }
    }
    public class ReductionEventArgs : 
        EventArgs
    {
        /// <summary>
        /// Returns the <see cref="Int32"/> value representing
        /// the current state count on the root deterministic
        /// state.
        /// </summary>
        public int StateCount { get; private set; }

        public ReductionEventArgs(int stateCount)
        {
            this.StateCount = stateCount;
        }
    }
    public class ReductionStepEventArgs :
        ReductionEventArgs
    {
        public ReductionStepEventArgs(int stateCount, int previousStateCount)
            : base(stateCount)
        {
            this.PreviousStateCount = previousStateCount;
        }

        /// <summary>
        /// Returns the <see cref="Int32"/> value representing the state count
        /// on the previous reduction step.
        /// </summary>
        public int PreviousStateCount { get; private set; }
    }
}
