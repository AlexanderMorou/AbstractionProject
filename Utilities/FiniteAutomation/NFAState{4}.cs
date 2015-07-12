using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
    /// Provides a basic non-deterministic finite automation.
    /// </summary>
    /// <typeparam name="TCheck">The type of set used
    /// to represent the transition from state set to state set.</typeparam>
    /// <typeparam name="TNFAState">The kind of <see cref="NFAState{TCheck, TNFAState, TDFAState, TSourceElement}"/>
    /// used to represent the non-deterministic elements of the
    /// automation.</typeparam>
    /// <typeparam name="TDFAState">The type used to construct
    /// a deterministic model of the current nondeterministic 
    /// automation.</typeparam>
    /// <typeparam name="TSourceElement">The kind of elements from the original
    /// parse tree which denote the source of the
    /// <see cref="NFAState{TCheck, TNFAState, TDFAState, TSourceElement}"/>.</typeparam>
    public partial class NFAState<TCheck, TNFAState, TDFAState, TSourceElement> :
        FiniteAutomataState<TCheck, TNFAState, List<TNFAState>, TSourceElement>,
        INFAState<TCheck, TNFAState, TDFAState, TSourceElement>,
        IEquatable<TNFAState>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TNFAState :
            NFAState<TCheck, TNFAState, TDFAState, TSourceElement>
        where TDFAState :
            DFAState<TCheck, TNFAState, TDFAState, TSourceElement>,
            IDFAState<TCheck, TNFAState, TDFAState, TSourceElement>,
            new()
        where TSourceElement :
            IFiniteAutomataSource
    {
        private static Dictionary<int, List<TNFAState>> flatlined = new Dictionary<int, List<TNFAState>>();
        private static List<TNFAState> ToStringStack = new List<TNFAState>();

        /// <summary>
        /// Initializes the <see cref="IFiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/>
        /// for the current non-deterministic state.
        /// </summary>
        /// <returns>A new <see cref="FiniteAutomataMultiTargetTransitionTable{TCheck, TState}"/>
        /// representing the non-deterministic targets of the state's
        /// transitions.</returns>
        protected override IFiniteAutomataTransitionTable<TCheck, TNFAState, List<TNFAState>> InitializeOutTransitionTable()
        {
            return new FiniteAutomataMultiTargetTransitionTable<TCheck, TNFAState>();
        }

        /// <summary>
        /// Returns a multi-target transition table for the states
        /// leaving the automation.
        /// </summary>
        public new IFiniteAutomataMultiTargetTransitionTable<TCheck, TNFAState> OutTransitions
        {
            get
            {
                return (IFiniteAutomataMultiTargetTransitionTable<TCheck, TNFAState>)(base.OutTransitions);
            }
        }

        #region INFAState<TCheck,TState,TDFA> Members

        /// <summary>
        /// Creates a version of the current
        /// <see cref="NFAState{TCheck, TNFAState, TDFAState, TSourceElement}"/> which is 
        /// deterministic by creating a left-side union on elements
        /// which overlap on their <typeparamref name="TCheck"/> 
        /// transition requirements.
        /// </summary>
        /// <returns>A new <typeparamref name="TDFAState"/> 
        /// instance which represents the current automation
        /// in a deterministic manner.</returns>
        public TDFAState DeterminateAutomata()
        {
            DFAEntryTable entrySet = new DFAEntryTable(this.GetDFAState);
            TDFAState result = GetRootDFAState();
            if (!this.IgnoreSources)
                ReplicateSourcesToAlt<TDFAState, TDFAState>(result);
            result.IsReductionSite = this.IsReductionSite;
            ReplicateStateTransitions(result, this.OutTransitions, entrySet);
            result.IsEdge = this.IsEdge;
            return result;
        }

        protected virtual TDFAState GetDFAState()
        {
            return new TDFAState();
        }

        protected virtual TDFAState GetRootDFAState()
        {
            return this.GetDFAState();
        }

        private void ReplicateStateTransitions(TDFAState result, IFiniteAutomataMultiTargetTransitionTable<TCheck, TNFAState> table, DFAEntryTable entrySet)
        {
            /* *
             * For every transition on the current transition table,
             * construct a new version of the target state via merging
             * the full transition tables of the NFA states.
             * *
             * This process is repeated with this merged table.
             * */
            foreach (var transition in table.Keys)
            {
                var set = table[transition].Distinct().ToList();
                if (entrySet.ContiansEntryFor(transition, set))
                    result.MoveTo(transition, entrySet[transition][set].DFA);
                else
                {
                    var newElement = entrySet.Add(transition, set);
                    FiniteAutomataMultiTargetTransitionTable<TCheck, TNFAState> mergedTable = new FiniteAutomataMultiTargetTransitionTable<TCheck, TNFAState>();
                    foreach (var subState in set)
                        foreach (var subTransition in subState.OutTransitions.Keys.ToArray())
                            mergedTable.Add(subTransition, subState.OutTransitions[subTransition]);
                    var curDFA = newElement.DFA;
                    result.MoveTo(transition, curDFA);
                    foreach (var item in set)
                        if (!item.IgnoreSources)
                            item.ReplicateSourcesToAlt<TDFAState, TDFAState>(curDFA);

                    curDFA.ReduceSources();

                    ReplicateStateTransitions(newElement.DFA, mergedTable, entrySet);
                }
            }
        }

        #endregion 

        /// <summary>
        /// Obtaines the edges of the current finite automation
        /// state.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> which iterates
        /// the edge states of the current
        /// <see cref="NFAState{TCheck, TNFAState, TDFAState, TSourceElement}"/>.</returns>
        public override sealed IEnumerable<TNFAState> ObtainEdges()
        {
            Stack<TNFAState> toCheck = new Stack<TNFAState>();
            toCheck.Push((TNFAState)this);
            List<TNFAState> considered = new List<TNFAState>();
            /* *
             * To avoid cyclic models, track the states passed
             * and yield only the edge states.
             * */
            while (toCheck.Count > 0)
            {
                var current = toCheck.Pop();
                considered.Add(current);
                if (current.IsEdge)
                    yield return current;
                foreach (var transition in current.OutTransitions.Values)
                    foreach (var state in transition)
                        if (!considered.Contains(state))
                            toCheck.Push(state);
            }
            yield break;
        }


        #region INFAState<TCheck,TState,TDFA> Members

        /// <summary>
        /// Creates a concatination between the current state's edges and
        /// the <paramref name="target"/> <typeparamref name="TNFAState"/>.
        /// </summary>
        /// <param name="target">The <typeparamref name="TNFAState"/>
        /// to concatenate with the current state.</param>
        public void Concat(TNFAState target)
        {
            var edges = this.ObtainEdges().ToArray();
            this.IsReductionSite = target.IsReductionSite || this.IsReductionSite;
            foreach (var transition in target.OutTransitions)
                foreach (var edge in edges)
                {
                    if (!target.IgnoreSources)
                        target.ReplicateSources(edge);
                    foreach (var state in transition.Value)
                        edge.MoveTo(transition.Key, state);
                }
            foreach (var edge in edges)
                if (edge.IsEdge ^ target.IsEdge)
                    edge.IsEdge = target.IsEdge;
        }

        /// <summary>
        /// Creates a union between the current state's transitions and
        /// the <paramref name="target"/> <typeparamref name="TNFAState"/>.
        /// </summary>
        /// <param name="target">The <typeparamref name="TNFAState"/>
        /// to create a union with.</param>
        public virtual void Union(TNFAState target)
        {
            this.IsEdge = target.IsEdge || this.IsEdge;
            this.IsReductionSite = target.IsReductionSite || this.IsReductionSite;
            foreach (var transition in target.OutTransitions)
                foreach (var state in transition.Value)
                    this.MoveTo(transition.Key, state);
            base.UnifySources(target);
        }

        protected override void UnifySources(TNFAState target)
        {
            if (!(this.IgnoreSources || target.IgnoreSources))
                base.UnifySources(target);
        }

        /// <summary>
        /// Creates a relative compliment between the current state's
        /// transitions and the edge points of the <paramref name="target"/>.
        /// </summary>
        /// <param name="target">The <typeparamref name="TNFAState"/>
        /// to create a relative compliment on.</param>
        /// <remarks>The edges of the <paramref name="target"/>
        /// and the current state that overlap are explicitly marked
        /// as non-edges.</remarks>
        /// <exception cref="System.NotImplementedException">
        /// thrown because it's not implemented.</exception>
        public void RelativeComplement(TNFAState target)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Creates a transition from the current 
        /// <see cref="NFAState{TCheck, TNFAState, TDFAState, TSourceElement}"/>
        /// to the <paramref name="target"/> with the
        /// <paramref name="condition"/> for transition provided.
        /// </summary>
        /// <param name="condition">The <typeparamref name="TCheck"/>
        /// which restricts the move.</param>
        /// <param name="target">The <typeparamref name="TNFAState"/>
        /// to move into.</param>
        public override void MoveTo(TCheck condition, TNFAState target)
        {
            this.OutTransitions.Add(condition, new List<TNFAState>() { target });
            target.MovedInto(condition, (TNFAState)this);
        }

        #region IEquatable<TState> Members
        /// <summary>
        /// Determines whether the current 
        /// <see cref="NFAState{TCheck, TNFAState, TDFAState, TSourceElement}"/>
        /// is equal to the <paramref name="other"/>.
        /// </summary>
        /// <param name="other">The <typeparamref name="TNFAState"/>
        /// to compare to.</param>
        /// <returns>true if the current state and the <paramref name="other"/>
        /// are equal; false, otherwise.</returns>
        public bool Equals(TNFAState other) { return object.ReferenceEquals(this, other); }

        #endregion

        public override int CountStates()
        {
            return CountStates((TNFAState)this, new HashSet<TNFAState>());
        }

        private int CountStates(TNFAState target, HashSet<TNFAState> covered)
        {
            if (covered.Contains(target))
                return 0;
            covered.Add(target);
            int count = 1;
            foreach (var transition in target.OutTransitions.Keys)
                foreach (var transitionTarget in target.OutTransitions[transition])
                    if (!covered.Contains(transitionTarget))
                        count += CountStates(transitionTarget, covered);
            return count;
        }

        public override string ToString()
        {
            var thisTState = this as TNFAState;
            bool firstOnStack = ToStringStack.Count == 0;
            if (ToStringStack.Contains(thisTState))
                return string.Format("* ({0})", this.StateValue);
            ToStringStack.Add(thisTState);
            try
            {
                MemoryStream ms = new MemoryStream();
                TextWriter tw = new StreamWriter(ms);
                IndentedTextWriter itw = new IndentedTextWriter(tw);
                itw.Indent++;
                if (this.OutTransitions != null && this.OutTransitions.Count > 0)
                {
                    if (this.IsEdge)
                    {
                        itw.Write("<END>");
                        itw.WriteLine();
                    }
                    foreach (var item in this.OutTransitions)
                    {
                        foreach (var subItem in item.Value)
                        {
                            string current = subItem.ToString();
                            string[] currentLines = current.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                            itw.Write("{0}", item.Key);
                            for (int i = 0; i < currentLines.Length; i++)
                            {
                                string line = currentLines[i];
                                if (subItem.IsEdge)
                                    itw.Write("->{{{0}}}", subItem.StateValue);
                                else
                                    itw.Write("->[{0}]", subItem.StateValue);
                                itw.Write(line.TrimStart());
                                itw.WriteLine();
                            }
                        }
                    }
                }
                else
                {
                    itw.Write("<END>");
                    itw.WriteLine();
                }
                itw.Indent--;
                itw.Flush();
                TextReader sr = new StreamReader(ms);
                ms.Seek(0, SeekOrigin.Begin);
                string result = "    " + sr.ReadToEnd();
                itw.Close();
                tw.Close();
                sr.Close();
                sr.Dispose();
                tw.Dispose();
                ms.Close();
                ms.Dispose();
                itw.Dispose();
                if (firstOnStack)
                    result = string.Format("Regular State Count: {0}\r\n {{Start {1}}}", this.CountStates().ToString(), this.StateValue) + "\r\n" + result;
                return result;
            }
            finally
            {
                if (firstOnStack)
                    ToStringStack.Clear();
            }
        }

        public bool IgnoreSources { get; set; }

        public static void FlatlineState(TNFAState state, List<TNFAState> result)
        {
            Stack<TNFAState> targets = new Stack<TNFAState>();
            targets.Push(state);
            while (targets.Count > 0)
            {
                var current = targets.Pop();
                if (result.Contains(current))
                    continue;
                result.Add(current);
                foreach (var transitionTargets in current.OutTransitions.Values)
                {
                    foreach (var target in transitionTargets)
                        if (target.Equals(current))
                            continue;
                        else if (!(targets.Contains(target) || result.Contains(target)))
                            targets.Push(target);
                }
            }
            /* *
             * The state doesn't place itself, but it does insert the transition
             * states: this ensures the flatline set doesn't contain the initial 
             * state, it would be bad to replace state 0.
             * */
            //foreach (var subStateSet in state.OutTransitions.Values)
            //    foreach (var subState in subStateSet)
            //        if (!result.Contains(subState))
            //        {
            //            result.Add(subState);
            //            FlatlineState(state, result);
            //        }
        }
        /// <summary>
        /// Returns/sets whether the resulted state is a reduction point.
        /// </summary>
        /// <remarks>Reduction sites reduce to their smallest form at all
        /// times.</remarks>
        public bool IsReductionSite { get; set; }

    }
}
