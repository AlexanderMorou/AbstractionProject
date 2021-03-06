using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright � 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    /// <summary>
    /// Provides a base implementation of a generic state within a, 
    /// transition based, finite automation.
    /// </summary>
    /// <typeparam name="TCheck">The type of <see cref="IFiniteAutomataSet{TCheck}"/>
    /// used to specify the condition for transition from one state
    /// to another (or others).</typeparam>
    /// <typeparam name="TState">The specific kind of <see cref="FiniteAutomataState{TCheck, TState, TForwardNodeTarget, TSourceElement}"/>
    /// used within the current automation.</typeparam>
    /// <typeparam name="TForwardNodeTarget">The kind of target used when the condition 
    /// put forth, by the <typeparamref name="TCheck"/> requirement, is met.</typeparam>
    /// <typeparam name="TSourceElement">The type of 
    /// <see cref="IFiniteAutomataSource"/> from which the states
    /// are derived.</typeparam>
    public abstract class FiniteAutomataState<TCheck, TState, TForwardNodeTarget, TSourceElement> :
        IFiniteAutomataState<TCheck, TState, TForwardNodeTarget, TSourceElement>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TState :
            FiniteAutomataState<TCheck, TState, TForwardNodeTarget, TSourceElement>
        where TSourceElement :
            IFiniteAutomataSource
    {
        private Dictionary<TSourceElement, FiniteAutomationSourceKind> sources = new Dictionary<TSourceElement, FiniteAutomationSourceKind>();
        internal static List<TState> enumStack = new List<TState>();
        internal static List<TState> sourceStack = new List<TState>();
        private bool isMarked = false;
        private bool forcedNoEdge = false;
        private IFiniteAutomataMultiTargetTransitionTable<TCheck, TState> inTransitions;
        private IFiniteAutomataTransitionTable<TCheck, TState, TForwardNodeTarget> outTransitions;
        /// <summary>
        /// Creates a new <see cref="FiniteAutomataState{TCheck, TState, TForwardNodeTarget, TSourceElement}"/>
        /// initialized to a default state.
        /// </summary>
        protected FiniteAutomataState()
        {
            this.StateValue = -1;
        }

        #region IFiniteAutomataState<TCheck,TState,TForwardNodeTarget,TBackwardNodeTarget> Members

        /// <summary>
        /// Returns the <see cref="IFiniteAutomataTransitionTable{TCheck, TState, TForwardNodeTarget}"/>
        /// that denotes the outgoing transitions from the <see cref="FiniteAutomataState{TCheck, TState, TForwardNodeTarget, TSourceElement}"/>.
        /// </summary>
        public IFiniteAutomataTransitionTable<TCheck, TState, TForwardNodeTarget> OutTransitions
        {
            get {
                if (this.outTransitions == null)
                    this.outTransitions = this.InitializeOutTransitionTable();
                return this.outTransitions;
            }
        }

        /// <summary>
        /// Initializes the <see cref="IFiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/>
        /// for the current <see cref="FiniteAutomataState{TCheck, TState, TForwardNodeTarget, TSourceElement}"/> state.
        /// </summary>
        /// <returns>A new <see cref="FiniteAutomataMultiTargetTransitionTable{TCheck, TState}"/>
        /// representing the state's incoming transitions.</returns>
        protected virtual IFiniteAutomataMultiTargetTransitionTable<TCheck, TState> InitializeInTransitionTable()
        {
            return new FiniteAutomataMultiTargetTransitionTable<TCheck, TState>(false);
        }

        /// <summary>
        /// Initializes the <see cref="IFiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/>
        /// for the current <see cref="FiniteAutomataState{TCheck, TState, TForwardNodeTarget, TSourceElement}"/>.
        /// </summary>
        /// <returns>A new <see cref="IFiniteAutomataTransitionTable{TCheck, TState, TForwardNodeTarget}"/>
        /// representing the state's outgoing transitions.</returns>
        protected abstract IFiniteAutomataTransitionTable<TCheck, TState, TForwardNodeTarget> InitializeOutTransitionTable();

        #endregion

        #region IFiniteAutomataState<TCheck,TState> Members

        /// <summary>
        /// Returns/sets whether the current <see cref="FiniteAutomataState{TCheck, TState, TForwardNodeTarget, TSourceElement}"/>
        /// is an edge state.
        /// </summary>
        public bool IsEdge
        {
            get
            {
                return (this.OutTransitions.Count == 0 && !forcedNoEdge) || isMarked;
            }
            set
            {
                if (value && this.forcedNoEdge)
                    forcedNoEdge = false;
                this.isMarked = value;
            }
        }

        /// <summary>
        /// Returns/sets whether the current <see cref="IFiniteAutomataState{TCheck, TState}"/> 
        /// has been forced into not being an edge state.
        /// </summary>
        /// <remarks>Used in instances where an edge is explicitly marked as a
        /// non-edge when a relative complement operation is performed between
        /// two automations.</remarks>
        public bool ForcedNoEdge
        {
            get
            {
                return this.forcedNoEdge;
            }
            set
            {
                this.forcedNoEdge = value;
            }
        }

        /// <summary>
        /// Returns whether the current <see cref="FiniteAutomataState{TCheck, TState, TForwardNodeTarget, TSourceElement}"/> 
        /// is marked as an edge state regardless of the number of
        /// outgoing transitions.
        /// </summary>
        public bool IsMarked
        {
            get {
                return this.isMarked;
            }
        }

        /// <summary>
        /// Returns the <see cref="IFiniteAutomataMultiTargetTransitionTable{TCheck, TState}"/>
        /// which denotes the conditions for and the states that target the current state.
        /// </summary>
        public IFiniteAutomataMultiTargetTransitionTable<TCheck, TState> InTransitions
        {
            get
            {
                if (this.inTransitions == null)
                    this.inTransitions = this.InitializeInTransitionTable();
                return this.inTransitions;
            }
        }

        IFiniteAutomataTransitionTable<TCheck, TState> IFiniteAutomataState<TCheck, TState>.OutTransitions
        {
            get { return this.OutTransitions; }
        }

        /// <summary>
        /// Obtains the edges of the current
        /// <see cref="FiniteAutomataState{TCheck, TState, TForwardNodeTarget, TSourceElement}"/>
        /// which are plausible points to terminate the current state.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> instance which
        /// yields the edges of the current 
        /// <see cref="FiniteAutomataState{TCheck, TState, TForwardNodeTarget, TSourceElement}"/>.
        /// </returns>
        public abstract IEnumerable<TState> ObtainEdges();


        #endregion

        internal void MovedInto(TCheck check, List<TState> target)
        {
            this.InTransitions.Add(check, target);
        }

        internal void MovedInto(TCheck check, TState target)
        {
            this.InTransitions.AddState(check, target);
        }


        #region IFiniteAutomataState<TCheck,TState> Members

        /// <summary>
        /// Creates a transition from the current 
        /// <see cref="FiniteAutomataState{TCheck, TState, TForwardNodeTarget, TSourceElement}"/>
        /// to the <paramref name="target"/> with the
        /// <paramref name="condition"/> for transition provided.
        /// </summary>
        /// <param name="condition">The <typeparamref name="TCheck"/>
        /// which restricts the move.</param>
        /// <param name="target">The <typeparamref name="TState"/>
        /// to move into.</param>
        public abstract void MoveTo(TCheck condition, TState target);

        #endregion

        /// <summary>
        /// Obtains the number of unique states within the current automation.
        /// </summary>
        /// <returns>An <see cref="Int32"/> value representing the 
        /// number of states in the automation.</returns>
        public abstract int CountStates();

        public void Enumerate()
        {
            int startIndex = 0;
            this.Enumerate(ref startIndex);
        }

        /// <summary>
        /// Iterates and assigns all elements within the state transition
        /// scope their uniqueness value.
        /// </summary>
        public void Enumerate(ref int startIndex)
        {
            List<TState> enumStack = new List<TState>();
            this.EnumerateSet(ref startIndex, enumStack);
            this.Enumerate(ref startIndex, enumStack);
            var edges = this.ObtainEdges();
            foreach (var edge in edges)
            {
                if (edge.StateValue == -1)
                    edge.StateValue = startIndex++;
            }
        }

        /// <summary>
        /// Sets the state-value of the current state and increments the
        /// <paramref name="stateValue"/>.
        /// </summary>
        /// <param name="stateValue">The most recently calculated state
        /// value.</param>
        /// <param name="enumStack">The <see cref="List{T}"/> of states
        /// that have already been enumerated.</param>
        /// <remarks>Does nothing if the state has already been enumerated 
        /// (is within the <paramref name="enumStack"/> list.</remarks>
        internal void EnumerateSet(ref int stateValue, List<TState> enumStack)
        {
            if (enumStack.Contains((TState)this))
                return;
            //Non-terminals only.
            if (this.OutTransitions.Count > 0 && this.StateValue == -1)
                this.StateValue = stateValue++;
        }

        /// <summary>
        /// Iterates and assigns all elements within the state transition
        /// scope their uniqueness value continuing from the 
        /// <paramref name="stateValue"/> provided.
        /// </summary>
        /// <param name="stateValue">The most recently calculated state
        /// value.</param>
        /// <param name="enumStack">The <see cref="List{T}"/> of states
        /// that have already been enumerated.</param>
        private void Enumerate(ref int stateValue, List<TState> enumStack)
        {
            var thisTState = this as TState;
            if (enumStack.Contains(thisTState))
                return;
            enumStack.Add(thisTState);
            if (this.OutTransitions.Count > 0)
            {
                /* *
                 * Index siblings relative to one another,
                 * then have them recurse their elements.
                 * */
                foreach (var item in this.OutTransitions.Targets)
                    item.EnumerateSet(ref stateValue, enumStack);
                foreach (var item in this.OutTransitions.Targets)
                    item.Enumerate(ref stateValue, enumStack);
            }
            if (enumStack[0] == this)
                enumStack.Clear();
        }

        /// <summary>
        /// Returns the <see cref="Int32"/> value unique 
        /// to the current state.
        /// </summary>
        public int StateValue { get; internal set; }

        public void SetInitial(TSourceElement source)
        {
            if (!sources.ContainsKey(source))
                sources.Add(source, FiniteAutomationSourceKind.None);
            this.sources[source] &= ~FiniteAutomationSourceKind.Intermediate;
            this.sources[source] |= FiniteAutomationSourceKind.Initial;
        }

        public void SetIntermediate(TSourceElement source)
        {
            if (!sources.ContainsKey(source))
                sources.Add(source, FiniteAutomationSourceKind.None);
            this.sources[source] |= FiniteAutomationSourceKind.Intermediate;
        }

        public void SetRepeat(TSourceElement source)
        {
            if (!sources.ContainsKey(source))
                sources.Add(source, FiniteAutomationSourceKind.None);
            this.sources[source] |= FiniteAutomationSourceKind.RepeatPoint;
        }

        public void SetFinal(TSourceElement source)
        {
            if (!sources.ContainsKey(source))
                sources.Add(source, FiniteAutomationSourceKind.None);
            this.sources[source] &= ~FiniteAutomationSourceKind.Intermediate;
            this.sources[source] |= FiniteAutomationSourceKind.Final;
        }

        protected virtual void UnifySources(TState target)
        {
            foreach (TSourceElement source in target.sources.Keys)
                if (!this.sources.ContainsKey(source))
                    this.sources.Add(source, target.sources[source]);
        }
#pragma warning disable 693
        internal void ReplicateSourcesToAlt<TState2, TForwardNodeTarget>(TState2 altTarget)
            where TState2 :
                FiniteAutomataState<TCheck, TState2, TForwardNodeTarget, TSourceElement>
        {
            foreach (var source in this.sources.Keys)
                if (!altTarget.sources.ContainsKey(source))
                    altTarget.sources.Add(source, this.sources[source]);
                else
                    altTarget.sources[source] |= this.sources[source];
        }
#pragma warning restore 693

        internal void ReplicateSources(TState target)
        {
            ReplicateSourcesToAlt<TState, TForwardNodeTarget>(target);
        }

        internal void IterateSources(Action<TSourceElement, FiniteAutomationSourceKind> iterator)
        {
            foreach (TSourceElement source in this.sources.Keys)
            {
                var kind = sources[source];
                if ((kind & FiniteAutomationSourceKind.Intermediate) == FiniteAutomationSourceKind.Intermediate)
                    iterator(source, FiniteAutomationSourceKind.Intermediate);
                if ((kind & FiniteAutomationSourceKind.Initial) == FiniteAutomationSourceKind.Initial)
                    iterator(source, FiniteAutomationSourceKind.Initial);
                if ((kind & FiniteAutomationSourceKind.RepeatPoint) == FiniteAutomationSourceKind.RepeatPoint)
                    iterator(source, FiniteAutomationSourceKind.RepeatPoint);
                if ((kind & FiniteAutomationSourceKind.Final) == FiniteAutomationSourceKind.Final)
                    iterator(source, FiniteAutomationSourceKind.Final);
            }
        }

        /// <summary>
        /// Obtains the sources from which the current state of the 
        /// automation derived.
        /// </summary>
        public IEnumerable<Tuple<TSourceElement, FiniteAutomationSourceKind>> Sources
        {
            get
            {
                foreach (var source in this.sources.Keys)
                    yield return new Tuple<TSourceElement, FiniteAutomationSourceKind>(source, this.sources[source]);
            }
        }

        /// <summary>
        /// Returns the number of sources within the current state.
        /// </summary>
        public int SourceCount
        {
            get
            {
                return this.sources.Count;
            }
        }
        internal void SetSources(Dictionary<TSourceElement, FiniteAutomationSourceKind> sources)
        {
            this.sources = sources;
        }
    }
}
