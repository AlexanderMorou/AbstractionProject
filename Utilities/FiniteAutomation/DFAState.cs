using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Arrays;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
/*---------------------------------------------------------------------\
| Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    /// <summary>
    /// Provides a base implementation of a deterministic finite automation state.
    /// </summary>
    /// <typeparam name="TCheck">The kind of set used as a condition for
    /// transitioning between states.</typeparam>
    /// <typeparam name="TNFAState">The kind of nfa state used within the automation.</typeparam>
    /// <typeparam name="TDFAState">The kind of dfa state used within the automation.</typeparam>
    /// <typeparam name="TSourceElement">The kind of element used to represent the sources
    /// within the deterministic finite automation.</typeparam>
    public abstract partial class DFAState<TCheck, TNFAState, TDFAState, TSourceElement> :
        FiniteAutomataState<TCheck, TDFAState, TDFAState, TSourceElement>,
        IDFAState<TCheck, TNFAState, TDFAState, TSourceElement>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TDFAState :
            DFAState<TCheck, TNFAState, TDFAState, TSourceElement>,
            new()
        where TNFAState :
            NFAState<TCheck, TNFAState, TDFAState, TSourceElement>
        where TSourceElement :
            IFiniteAutomataSource
    {
        private static List<TDFAState> ToStringStack = new List<TDFAState>();

        /// <summary>
        /// Initializes the <see cref="IFiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/>
        /// for the current <see cref="DFAState{TCheck, TDFAState, TNFAState, TSourceElement}"/>.
        /// </summary>
        /// <returns>A new <see cref="IFiniteAutomataTransitionTable{TCheck, TState, TForwardNodeTarget}"/>
        /// representing the state's outgoing transitions.</returns>
        protected override IFiniteAutomataTransitionTable<TCheck, TDFAState, TDFAState> InitializeOutTransitionTable()
        {
            return new FiniteAutomataSingleTargetTransitionTable<TCheck, TDFAState>();
        }

        /// <summary>
        /// Returns the <see cref="FiniteAutomataSingleTargetTransitionTable{TCheck, TState}"/>
        /// which denotes the table of single-target transition nodes going away
        /// from the current state.
        /// </summary>
        public new FiniteAutomataSingleTargetTransitionTable<TCheck, TDFAState> OutTransitions
        {
            get
            {
                return ((FiniteAutomataSingleTargetTransitionTable<TCheck, TDFAState>)base.OutTransitions);
            }
        }

        IFiniteAutomataSingleTargetTransitionTable<TCheck, TDFAState> IDFAState<TCheck, TNFAState, TDFAState, TSourceElement>.OutTransitions
        {
            get
            {
                return (IFiniteAutomataSingleTargetTransitionTable<TCheck, TDFAState>)(base.OutTransitions);
            }
        }

        /// <summary>
        /// Obtains the edges of the current
        /// <see cref="DFAState{TCheck, TDFAState, TNFAState, TSourceElement}"/>
        /// which are plausible points to terminate the current state.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> instance which
        /// yields the edges of the current 
        /// <see cref="DFAState{TCheck, TDFAState, TNFAState, TSourceElement}"/>.
        /// </returns>
        public override IEnumerable<TDFAState> ObtainEdges()
        {
            Stack<TDFAState> toCheck = new Stack<TDFAState>();
            toCheck.Push((TDFAState)this);
            List<TDFAState> considered = new List<TDFAState>();
            while (toCheck.Count > 0)
            {
                var current = toCheck.Pop();
                if (current.IsEdge)
                    yield return current;
                foreach (var state in current.OutTransitions.Values)
                    if (!considered.Contains(state))
                    {
                        considered.Add(state);
                        toCheck.Push(state);
                    }
            }
            yield break;
        }

        /// <summary>
        /// Creates a transition from the current 
        /// <see cref="DFAState{TCheck, TDFAState, TNFAState, TSourceElement}"/>
        /// to the <paramref name="target"/> with the
        /// <paramref name="condition"/> for transition provided.
        /// </summary>
        /// <param name="condition">The <typeparamref name="TCheck"/>
        /// which restricts the move.</param>
        /// <param name="target">The <typeparamref name="TDFAState"/>
        /// to move into.</param>
        public override void MoveTo(TCheck condition, TDFAState target)
        {
            this.OutTransitions.Add(condition, target);
            target.MovedInto(condition, (TDFAState)this);
        }

        private void MoveToInternal(TCheck condition, TDFAState target)
        {
            this.OutTransitions.AddInternal(condition, target);
            target.MovedInto(condition, (TDFAState)this);
        }

        /// <summary>
        /// Obtains the number of unique states within the current automation.
        /// </summary>
        /// <returns>An <see cref="Int32"/> value representing the 
        /// number of states in the deterministic automation.</returns>
        public override int CountStates()
        {
            return CountStates((TDFAState)this, new HashSet<TDFAState>());
        }

        private int CountStates(TDFAState target, HashSet<TDFAState> covered)
        {
            if (covered.Contains(target))
                return 0;
            else
            {
                covered.Add(target);
                int count = 1;
                foreach (var transition in target.OutTransitions.Keys)
                    count += CountStates(target.OutTransitions[transition], covered);
                return count;
            }
        }
        internal virtual string StringForm { get { return this.ToStringInternal(); } }

        /// <summary>
        /// Returns the string representation of the 
        /// <see cref="DFAState{TCheck, TDFAState, TNFAState, TSourceElement}"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> value representing
        /// the state.</returns>
        public override string ToString()
        {
            return ToStringInternal();
        }

        private string ToStringInternal()
        {
            var thisTState = this as TDFAState;
            bool firstOnStack = ToStringStack.Count == 0;
            if (ToStringStack.Contains(thisTState))
                return string.Format("* ({0})", this.StateValue);
            ToStringStack.Add(thisTState);
            try
            {
                MemoryStream ms = new MemoryStream();
                TextWriter tw = new StreamWriter(ms);
                IndentedTextWriter itw = new IndentedTextWriter(tw);
                var terminalSources = (from s in Sources
                                       where s.Item2 == FiniteAutomationSourceKind.Final
                                       select s.Item1).ToArray();
                itw.Indent++;
                if (this.OutTransitions != null && this.OutTransitions.Count > 0)
                {
                    if (this.IsEdge)
                    {
                        if (terminalSources.Length == 0)
                            itw.Write("<END>");
                        else
                            itw.Write(string.Format("<END for {{{0}}}>", string.Join(", ", GetTerminalStrings(terminalSources))));
                        itw.WriteLine();
                    }
                    foreach (var subItem in this.OutTransitions)
                    {
                        string current = subItem.Value.StringForm;
                        string[] currentLines = current.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        itw.Write("{0}", subItem.Key);
                        for (int i = 0; i < currentLines.Length; i++)
                        {
                            string line = currentLines[i];
                            if (subItem.Value.IsEdge)
                                itw.Write("->{{{0}}}", subItem.Value.StateValue);
                            else
                                itw.Write("->[{0}]", subItem.Value.StateValue);
                            itw.Write(line.TrimStart());
                            itw.WriteLine();
                        }
                    }
                }
                else
                {
                    if (terminalSources.Length == 0)
                        itw.Write("<END>");
                    else
                        itw.Write(string.Format("<END for {{{0}}}>", string.Join(", ", GetTerminalStrings(terminalSources))));
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
                    result = string.Format("Regular State Count: {0}\r\n {{Start {1}}}", this.CountStates(), this.StateValue) + "\r\n" + result;
                return result;
            }
            finally
            {
                if (firstOnStack)
                    ToStringStack.Clear();
            }
        }

        private static IEnumerable<string> GetTerminalStrings(TSourceElement[] sources)
        {
            foreach (var source in sources)
            {
                var s = source.ToString();
                if (s.Contains('\r') || s.Contains('\n'))
                {
                    int rIndex = s.IndexOf('\r');
                    int nIndex = s.IndexOf('\r');
                    int trimIndex = Math.Min(rIndex, nIndex);
                    yield return s.Substring(0, trimIndex) + "...";
                }
                else
                    yield return s;
            }
        }

        protected virtual IEnumerable<Tuple<TSourceElement, FiniteAutomationSourceKind>> ReduceSources(IEnumerable<Tuple<TSourceElement, FiniteAutomationSourceKind>> currentSources)
        {
            return currentSources;
        }

        private List<TDFAState> GetReductionZone()
        {
            List<TDFAState> reductionZone = new List<TDFAState>();
            GetReductionZone((TDFAState)(object)this, reductionZone, new List<TDFAState>());
            return reductionZone;
        }

        private static void GetReductionZone(TDFAState origin, List<TDFAState> reductionZone, List<TDFAState> visited)
        {
            if (visited.Contains(origin))
                return;
            visited.Add(origin);
            if (origin.IsReductionSite && !reductionZone.Contains(origin))
                reductionZone.Add(origin);
            foreach (var target in origin.OutTransitions.Values)
                GetReductionZone(target, reductionZone, visited);
        }

        protected static void Reduce(TDFAState target, bool recognizer, Func<TDFAState, TDFAState, bool> additionalReducer = null)
        {
            int last = 0;
        Repeat:
            Dictionary<TDFAState, List<TDFAState>> forwardDuplications = new Dictionary<TDFAState, List<TDFAState>>();
            List<TDFAState> flatForm = new List<TDFAState>();
            flatForm.Add(target);
            FlatlineState(target, flatForm);
            flatForm = flatForm.Distinct().ToList();
            bool[] found = new bool[flatForm.Count];
            Parallel.For(0, flatForm.Count, i =>
            //for (int i = 0; i < flatForm.Count; i++)
            {
                lock (found)
                {
                    if (found[i])
                        return;
                }
                TDFAState iItem = flatForm[i];
                /* *
                 * If the state is already being replaced,
                 * no need to check it again.
                 * */

                if (recognizer)
                    /* *
                     * Merge backwards as well as forwards on cases where the 
                     * exact way the match occurred is not important
                     * */
                    if (RecognizerTerminalEquivalencyCheck(iItem, i, flatForm, found, forwardDuplications))
                        return;
                //Merge forward.
                StateEquivalencyCheck(iItem, i, flatForm, found, recognizer, forwardDuplications, additionalReducer);
            });

            var maxReduces = (from f in flatForm
                              where f.IsReductionSite
                              where !forwardDuplications.Values.Any(reducedSet => reducedSet.Contains(f))
                              select f).ToArray();
            var reductionZones = new ControlledDictionary<TDFAState, List<TDFAState>>();

            foreach (var maximalReducer in maxReduces)
            {
                if (reductionZones.Values.Any(zone=>zone.Contains(maximalReducer)))
                    continue;
                reductionZones._Add(maximalReducer, (from k in maximalReducer.GetReductionZone()
                                                     where !forwardDuplications.Values.Any(reducedSet => reducedSet.Contains(k))
                                                     select k).ToList());
            }
            /* *
             * Now we have the zones which need reduced to the max.
             * *
             * These are for optimization purposes where you know
             * the order isn't important, but the data itself is.
             * */
            for (int i = 0; i < reductionZones.Count; i++)
            {
                var maxReduceSet = reductionZones.Values[i];
                bool[] maxFound = new bool[maxReduceSet.Count];
                for (int j = 0; j < maxReduceSet.Count; j++)
                {
                    var iItem = maxReduceSet[j];
                    if (maxFound[j])
                        continue;
                    if (RecognizerTerminalEquivalencyCheck(iItem, i, maxReduceSet, maxFound, forwardDuplications))
                        continue;
                    StateEquivalencyCheck(iItem, j, maxReduceSet, maxFound, true, forwardDuplications, additionalReducer);
                }
            }

            /* *
             * Replace the states that are considered extraneous.
             * */
            foreach (var masterState in forwardDuplications.Keys)
            {
                var currentSet = forwardDuplications[masterState];
                for (int i = 0; i < currentSet.Count; i++)
                    ReplaceState(masterState, currentSet[i]);
            }
            /* *
             * If there were reductions performed in this step, repeat
             * the process, since two states that didn't pass the equivalency
             * examination pointed to two different, yet similar, states,
             * the process can be repeated on these, now redundant, states.
             * */
            CondenseTransitions(target);
            last = flatForm.Count;
            flatForm.Clear();
            if (forwardDuplications.Count > 0)
            {
                forwardDuplications.Clear();
                //Reduces recursive dependency.  No need to call the method again.
                goto Repeat;
            }
        }

        private static void CondenseTransitions(TDFAState target)
        {
            List<TDFAState> condenseStack = new List<TDFAState>();
            CondenseTransitions(target, condenseStack);
        }

        private static void CondenseTransitions(TDFAState target, List<TDFAState> condenseStack)
        {
            if (condenseStack.Contains(target))
                return;
            condenseStack.Add(target);

            Dictionary<TDFAState, List<TCheck>> stateTransitionLookup = new Dictionary<TDFAState, List<TCheck>>();
            /* *
             * Enumerate the transitions and create a reverse lookup
             * for the transitions.
             * */
            foreach (var transition in target.OutTransitions.Keys)
            {
                var transitionTarget = target.OutTransitions[transition];
                CondenseTransitions(transitionTarget, condenseStack);
                if (!stateTransitionLookup.ContainsKey(transitionTarget))
                    stateTransitionLookup.Add(transitionTarget, new List<TCheck>());
                stateTransitionLookup[transitionTarget].Add(transition);
            }
            /* *
             * Next, in cases where the number of hits for a given transition is greater
             * than one, reduce where needed.
             * */
            var condenseQuery = from s in stateTransitionLookup
                                where s.Value.Count > 1
                                select s;
            foreach (var condensed in condenseQuery)
            {
                var transitionState = condensed.Key;
                TCheck fullRange = default(TCheck);
                foreach (var transition in condensed.Value)
                {
                    transitionState.InTransitions.Remove(transition, target);
                    target.OutTransitions.Remove(transition);//, transitionState);
                    if (fullRange == null)
                        fullRange = transition;
                    else
                        fullRange = fullRange.Union(transition);
                }
                target.MoveToInternal(fullRange, transitionState);
                //target.OutTransitions.Add(fullRange, transitionState);
                //transitionState.InTransitions.Add(fullRange, new List<TState>(new TState[1] { target }));
            }
            if (condenseStack[0] == target)
                condenseStack.Clear();
        }

        private static bool RecognizerTerminalEquivalencyCheck(TDFAState targetState, int targetCompareIndex, List<TDFAState> compare, bool[] found, Dictionary<TDFAState, List<TDFAState>> targetRedundancyLookup)
        {
            /* *
             * Merge behind.  Basically any edge which has equal incoming transitions
             * to another, is equal to that edge.  When those are replaced the forward
             * logic takes control and simplifies the rest.
             * */
            if (targetState.OutTransitions == null || targetState.OutTransitions.Count == 0)
            {
                //Parallel.For(targetCompareIndex + 1, compare.Count, j =>
                for (int j = targetCompareIndex + 1; j < compare.Count; j++)
                {
                    lock (found)
                        if (found[j])
                            continue;
                    TDFAState currentState = compare[j];
                    if (currentState.OutTransitions != null && currentState.OutTransitions.Count > 0)
                        continue;
                    if (currentState.InTransitions == null ||
                        currentState.InTransitions.Count != targetState.InTransitions.Count)
                        continue;
                    /* *
                     * A bit different than the reverse.
                     * Only worry about cases where 
                     * */
                    if (currentState.InTransitions.Checks.All(p => targetState.InTransitions.Checks.Contains(p)))
                    {
                        //Assume success.
                        bool match = true;
                        for (int k = 0; k < currentState.InTransitions.Count; k++)
                        {
                            var kTransition = currentState.InTransitions.ElementAt(k);
                            if (kTransition.Value.Count != targetState.InTransitions[kTransition.Key].Count)
                            {
                                match = false;
                                break;
                            }
                            var qTransition = targetState.InTransitions[kTransition.Key];
                            for (int q = 0; q < kTransition.Value.Count; q++)
                            {
                                if (kTransition.Value.Contains(qTransition.ElementAt(q)))
                                {
                                    //When the states are the same, do exit.
                                    match = false;
                                    break;
                                }
                            }
                            if (!match)
                                break;
                        }
                        if (!match)
                            continue;
                        if (targetState.IsEdge == currentState.IsEdge &&
                            currentState != targetState)
                        {
                            lock (found)
                                found[j] = true;
                            lock (targetRedundancyLookup)
                            {
                                if (!targetRedundancyLookup.ContainsKey(targetState))
                                    targetRedundancyLookup.Add(targetState, new List<TDFAState>());
                                if (!targetRedundancyLookup[targetState].Contains(currentState))
                                    targetRedundancyLookup[targetState].Add(currentState);
                            }
                        }
                    }
                }//);
                /* *
                 * The primary procedure that reduces the states
                 * utilizes a loop, states that contain no transitions
                 * don't need checked for out-state transition equivalency,
                 * so returning false omits checking the targetState further.
                 * */
                return true; //A terminal reduction check occurred.
            }
            return false; //Not a terminal reduction.
        }

        /// <summary>
        /// Obtains all of the states within the deterministic 
        /// automation and places them within the
        /// <paramref name="result"/> provided.
        /// </summary>
        /// <param name="state">The <typeparamref name="TDFAState"/>
        /// which represents the root state.</param>
        /// <param name="result">The <see cref="List{T}"/>
        /// of <typeparamref name="TDFAState"/>
        /// elements to populate.</param>
        public static void FlatlineState(TDFAState state, List<TDFAState> result)
        {
            if (state == null)
                throw new ArgumentNullException("state");
            /* *
             * The state doesn't place itself, but it does insert the transition
             * states: this ensures the flatline set doesn't contain the initial 
             * state, it would be bad to replace state 0.
             * */
            foreach (var subState in state.OutTransitions.Values)
                if (!result.Contains(subState))
                {
                    result.Add(subState);
                    FlatlineState(subState, result);
                }
        }


        private static void StateEquivalencyCheck(TDFAState targetState, int targetCompareIndex, List<TDFAState> compare, bool[] found, bool recognizer, Dictionary<TDFAState, List<TDFAState>> targetRedundancyLookup, Func<TDFAState, TDFAState, bool> additionalReducer)
        {
            Parallel.For(targetCompareIndex + 1, compare.Count, j =>
            //for (int j = targetCompareIndex + 1; j < compare.Count; j++)
            {
                /* *
                 * Allow the use of a custom reduction function.
                 * *
                 * This is to enable situations where the target machine
                 * is better understood than the cautious state equivalency
                 * performed here.
                 * */
                TDFAState currentState = compare[j];
                if (additionalReducer != null)
                {
                    if (additionalReducer(targetState, currentState))
                    {
                        found[j] = true;
                        lock (targetRedundancyLookup)
                        {
                            if (!targetRedundancyLookup.ContainsKey(targetState))
                                targetRedundancyLookup.Add(targetState, new List<TDFAState>());
                            targetRedundancyLookup[targetState].Add(currentState);
                        }
                    }
                }
                if (currentState.OutTransitions == null)
                    return;
                if (currentState.OutTransitions.Count == 0)
                    return;
                if (currentState.OutTransitions.Count != targetState.OutTransitions.Count)
                    return;
                /* *
                 * Only continue if every transition set in currentState exists in
                 * the target state.  This process occurs after DFA conversion
                 * so equivalency is more likely vs. the NFA setup that might
                 * have partial overlaps that would later transition into the
                 * redundant states being cleaned up here.
                 * */
                if (currentState.OutTransitions.Checks.All(p => targetState.OutTransitions.Checks.Contains(p)))
                {
                    //Assume success.
                    bool match = true;
                    for (int k = 0; k < currentState.OutTransitions.Count; k++)
                    {
                        var kTransition = currentState.OutTransitions.ElementAt(k);
                        /* *
                         * Obtain the transition for the target state,
                         * use the transition key vs. the index
                         * since index equivalency should exist, but 
                         * future changes to the code might alter this
                         * truth.
                         * */
                        var qTransition = targetState.OutTransitions.GetNode(kTransition.Key);
                        if (kTransition.Value != qTransition.Target)
                        {
                            match = false;
                            break;
                        }
                    }
                    if (!match)
                        return;
                    /* *
                     * If match is still assumed, only continue if their edge
                     * status is equal, replacing an edge state for a non edge
                     * state would be bad.  One last check for current vs. target
                     * state for good measure.  Purely for final assurance, since
                     * future changes cannot be predicted, and I might blunder
                     * and make a mistake (ie. send the target state that exists
                     * at an index later than the one indicated).
                     * */
                    if (targetState.IsEdge == currentState.IsEdge &&
                        currentState != targetState)
                    {
                        if (!recognizer && !(currentState.IsReductionSite && targetState.IsReductionSite))
                        {
                            if (!(currentState.SourceCount == targetState.SourceCount && currentState.Sources.All(s => targetState.Sources.Contains(s))))
                                return;
                        }
                        lock (found)
                            found[j] = true;
                        lock (targetRedundancyLookup)
                        {
                            if (!targetRedundancyLookup.ContainsKey(targetState))
                                targetRedundancyLookup.Add(targetState, new List<TDFAState>());
                            targetRedundancyLookup[targetState].Add(currentState);
                        }
                    }
                }
            });
        }
        /// <summary>
        /// Replaces the <paramref name="original"/> state with the
        /// <paramref name="replacement"/> provided.
        /// </summary>
        /// <param name="replacement">The master state which is noted to be 
        /// equivalent to the <paramref name="original"/> provided.</param>
        /// <param name="original">The <typeparamref name="TDFAState"/> 
        /// to replace.</param>
        private static void ReplaceState(TDFAState replacement, TDFAState original)
        {
            /* *
             * Build a table relative to the states which flow into
             * the original state and use it to retarget those values
             * to the replacement.
             * */

            original.IterateSources((source, kind) =>
                {
                    switch (kind)
                    {
                        case FiniteAutomationSourceKind.Initial:
                            replacement.SetInitial(source);
                            break;
                        case FiniteAutomationSourceKind.Intermediate:
                            replacement.SetIntermediate(source);
                            break;
                        case FiniteAutomationSourceKind.RepeatPoint:
                            replacement.SetRepeat(source);
                            break;
                        case FiniteAutomationSourceKind.Final:
                            replacement.SetFinal(source);
                            break;
                    }
                });
            if (original.contextSize > 0)
            {
                var originalOverlappingContexts =
                    (from replacementContext in (replacement.contexts ?? new ConstructionContext[0])
                     where replacementContext != null
                     from originalContext in original.contexts
                     where originalContext != null
                     where replacementContext.ConstructionCondition.Equals(originalContext.ConstructionCondition)
                     select originalContext).Distinct().ToArray();
                var originalNewContexts =
                    (from originalContext in original.contexts
                    where originalContext != null
                    select originalContext).Except(originalOverlappingContexts).ToArray();
                for (int i = 0; i < replacement.contextSize; i++)
                {
                    var replacementContext = replacement.contexts[i];
                    for (int j = 0; j < originalOverlappingContexts.Length; j++)
                    {
                        if (originalOverlappingContexts[j].ConstructionCondition.Equals(replacementContext.ConstructionCondition))
                            replacementContext.MergeWith(originalOverlappingContexts[j]);
                    }
                }
                if (originalNewContexts.Length > 0)
                {
                    replacement.contexts = replacement.contexts.EnsureSpaceExists(replacement.contextSize, originalNewContexts.Length);
                    originalNewContexts.CopyTo(replacement.contexts, replacement.contextSize);
                    replacement.contextSize += originalNewContexts.Length;
                }
            }

            Dictionary<TCheck, Dictionary<TCheck, HashSet<TDFAState>>> inStates = new Dictionary<TCheck, Dictionary<TCheck, HashSet<TDFAState>>>();
            foreach (var backCheck in original.InTransitions.Keys)
                foreach (var backTarget in original.InTransitions[backCheck])
                    foreach (var forwardCheck in backTarget.OutTransitions.Keys)
                        if (backTarget.OutTransitions[forwardCheck] == original)
                        {
                            /* *
                             * Because the number of states going into another state are
                             * 1:many, versus 1:1 for outgoing states, there's no guarantee
                             * that the same incoming condition for entering a state is the 
                             * same as the outgoing condition that's actually used.
                             * */
                            Dictionary<TCheck, HashSet<TDFAState>> firstSet;
                            if (!inStates.TryGetValue(forwardCheck, out firstSet))
                                inStates.Add(forwardCheck, firstSet = new Dictionary<TCheck, HashSet<TDFAState>>());
                            HashSet<TDFAState> subSet;
                            if (!firstSet.TryGetValue(backCheck, out subSet))
                                firstSet.Add(backCheck, subSet = new HashSet<TDFAState>());
                            if (!subSet.Contains(backTarget))
                                subSet.Add(backTarget);
                        }

            foreach (var forwardCheck in inStates.Keys)
                foreach (var backCheck in inStates[forwardCheck].Keys)
                    foreach (var backState in inStates[forwardCheck][backCheck])
                    {
                        backState.OutTransitions.Remove(forwardCheck);
                        /* *
                         * Collision checks on the DFA transition table are 
                         * performed to ensure that if a collision occurs
                         * an error is thrown to indicate a malformed
                         * request, since DFA states have one target
                         * per transition.
                         * *
                         * Perform an internal move-to to ensure
                         * un-necessary set computations aren't performed,
                         * since the logic above removes the un-needed
                         * targets from scope.
                         * */
                        backState.MoveToInternal(forwardCheck, replacement);
                    }
        }
        public IEnumerable<TSourceElement> SourceSet
        {
            get
            {
                List<TSourceElement> results = new List<TSourceElement>();
                PropagateSources((TDFAState)this, results, new List<TDFAState>(), SourceSetPredicate);
                foreach (var source in results)
                    yield return source;
            }
        }

        protected abstract bool SourceSetPredicate(TSourceElement source);

        public static void PropagateSources(TDFAState target, List<TSourceElement> results, List<TDFAState> observed, Predicate<TSourceElement> limiter)
        {
            if (observed.Contains(target))
                return;
            observed.Add(target);
            foreach (var source in target.Sources)
                if ((!results.Contains(source.Item1)) && limiter(source.Item1))
                    results.Add(source.Item1);
            foreach (var transition in target.OutTransitions)
                if (!(observed.Contains(transition.Value)))
                    PropagateSources(transition.Value, results, observed, limiter);
        }


        public void ReduceSources()
        {
            this.SetSources(this.ReduceSources(this.Sources).ToDictionary(p => p.Item1, p => p.Item2));
        }

        /// <summary>
        /// Returns/sets whether the resulted state is a reduction point.
        /// </summary>
        /// <remarks>Reduction sites reduce to their smallest form at all
        /// times.</remarks>
        public bool IsReductionSite { get; set; }
        internal void PushContext(TCheck condition, List<TNFAState> states)
        {
            this.contexts = this.contexts.EnsureSpaceExists(contextSize, 1);
            this.contexts[this.contextSize++] = new ConstructionContext(condition, states);
        }

        public IEnumerable<IDFAConstructionContext<TCheck, TNFAState, TDFAState, TSourceElement>> ConstructionSources
        {
            get
            {
                for (int i = 0; i < this.contextSize; i++)
                    if (this.contexts[i] != null)
                        yield return this.contexts[i];
            }
        }

    }
}
