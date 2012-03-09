using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    [DebuggerDisplay("Count = {Count}")]
    public abstract partial class FiniteAutomataTransitionTable<TCheck, TState, TNodeTarget> :
        IControlledDictionary<TCheck, TNodeTarget>,
        IFiniteAutomataTransitionTable<TCheck, TState, TNodeTarget>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TState :
            IFiniteAutomataState<TCheck, TState>
    {
        private KeysCollection keys;
        private ValuesCollection values;
        private Dictionary<TCheck, IFiniteAutomataTransitionNode<TCheck, TNodeTarget>> backup = new Dictionary<TCheck,IFiniteAutomataTransitionNode<TCheck,TNodeTarget>>();
        private bool fullCheckObtained;
        private TCheck fullCheck;
        #region IFiniteAutomataTransitionTable<TCheck,TState,TNodeTarget> Members
        /// <summary>
        /// Adds a transition with the requirement <paramref name="check"/>
        /// and the <paramref name="target"/> of the transition provided.
        /// </summary>
        /// <param name="check">The <typeparamref name="TCheck"/>
        /// which denotes the transition requirement for the change in state.
        /// </param>
        /// <param name="target">The <typeparamref name="TNodeTarget"/>
        /// that denotes the target of the transition.</param>
        public abstract void Add(TCheck check, TNodeTarget target);

        #endregion

        #region IFiniteAutomataTransitionTable<TCheck,TState> Members

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
        public void AddState(TCheck check, TState target)
        {
            this.Add(check, GetStateTarget(target));
        }

        #endregion

        /// <summary>
        /// Obtains the target of a given state.
        /// </summary>
        /// <param name="state">The <typeparamref name="TState"/>
        /// which needs a target to make a transition.</param>
        /// <returns>The <typeparamref name="TNodeTarget"/>
        /// which will contain the <paramref name="state"/>.</returns>
        protected abstract TNodeTarget GetStateTarget(TState state);

        #region IControlledDictionary<TCheck,TNodeTarget> Members

        public IControlledCollection<TCheck> Keys
        {
            get
            {
                if (this.keys == null)
                    this.keys = this.InitializeKeys();
                return this.keys;
            }
        }

        public IControlledCollection<TNodeTarget> Values
        {
            get
            {
                if (this.values == null)
                    this.values = this.InitializeValues();
                return this.values;
            }
        }

        public TNodeTarget this[TCheck key]
        {
            get {
                if (!this.ContainsKey(key))
                    throw new KeyNotFoundException();
                return this.backup[key].Target;
            }
        }

        public bool ContainsKey(TCheck key)
        {
            return this.backup.ContainsKey(key);
        }

        public bool TryGetValue(TCheck key, out TNodeTarget value)
        {
            IFiniteAutomataTransitionNode<TCheck, TNodeTarget> node;
            if (this.backup.TryGetValue(key, out node))
            {
                value = node.Target;
                return true;
            }
            value = default(TNodeTarget);
            return false;
        }

        #endregion

        #region IControlledCollection<KeyValuePair<TCheck,TNodeTarget>> Members

        /// <summary>
        /// Gets the number of elements contained in the
        /// <see cref="FiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the
        /// <see cref="FiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/>.
        /// </returns>
        public int Count
        {
            get { return this.backup.Count; }
        }

        /// <summary>
        /// Determines whether the <see cref="FiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/> contains a specific 
        /// value.</summary>
        /// <param name="item">
        /// The object to locate in the <see cref="FiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="FiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/>;
        /// otherwise, false.
        /// </returns>
        public bool Contains(KeyValuePair<TCheck, TNodeTarget> item)
        {
            IFiniteAutomataTransitionNode<TCheck, TNodeTarget> node;
            if (this.backup.TryGetValue(item.Key, out node))
                return object.ReferenceEquals(node.Target, item.Value);
            return false;
        }

        /// <summary>
        /// Copies the elements of the <see cref="FiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/> to an
        /// <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> 
        /// index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="System.Array"/> that is
        /// the destination of the elements copied from
        /// <see cref="FiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/>. The 
        /// <see cref="System.Array"/> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">
        /// The zero-based index in <paramref name="array"/> at which
        /// copying begins.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="arrayIndex"/> is less than 0.</exception>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="array"/> is null.</exception>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="array"/> is multidimensional.-or-<paramref name="arrayIndex"/> 
        /// is equal to or greater than the length of <paramref name="array"/>.-or-The 
        /// number of elements in the source
        /// <see cref="FiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/>
        /// is greater than the available space from <paramref name="arrayIndex"/>
        /// to the end of the destination <paramref name="array"/>.</exception>
        public void CopyTo(KeyValuePair<TCheck, TNodeTarget>[] array, int arrayIndex = 0)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (arrayIndex + this.Count > array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");
            int i = 0;
            foreach (var item in this)
                array[arrayIndex + i++] = item;
        }

        /// <summary>
        /// Returns the element at the index provided
        /// </summary>
        /// <param name="index">The index of the element to get.</param>
        /// <returns>The <see cref="KeyValuePair{TKey, TValue}"/> at the <paramref name="index"/> provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is  beyond the range of the 
        /// <see cref="FiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/>.
        /// </exception>
        public KeyValuePair<TCheck, TNodeTarget> this[int index]
        {
            get
            {
                if (index < 0 ||
                    index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                int i = 0;
                foreach (var node in this.backup.Values)
                    if (i++ == index)
                        return new KeyValuePair<TCheck, TNodeTarget>(node.Check, node.Target);
                return default(KeyValuePair<TCheck, TNodeTarget>);
            }
        }

        /// <summary>
        /// Translates the <see cref="FiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/>
        /// into a flat <see cref="System.Array"/> of <see cref="KeyValuePair{TKey, TValue}"/>
        /// elements.
        /// </summary>
        /// <returns>
        /// A new <see cref="System.Array"/> of <see cref="KeyValuePair{TKey, TValue}"/>
        /// instances.</returns>
        public KeyValuePair<TCheck, TNodeTarget>[] ToArray()
        {
            KeyValuePair<TCheck, TNodeTarget>[] result = new KeyValuePair<TCheck, TNodeTarget>[this.Count];
            this.CopyTo(result, 0);
            return result;
        }

        /// <summary>
        /// Returns the <see cref="Int32"/> ordinal index of the 
        /// <paramref name="element"/> provided.
        /// </summary>
        /// <param name="element">The <see cref="KeyValuePair{TKey, TValue}"/>
        /// instance to find within the <see cref="FiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/>.</param>
        /// <returns>-1 if the <paramref name="element"/> was not found within
        /// the <see cref="FiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/>; a positive <see cref="Int32"/>
        /// value indicating the ordinal index of <paramref name="element"/>
        /// otherwise.</returns>
        public int IndexOf(KeyValuePair<TCheck, TNodeTarget> element)
        {
            var tC = element.Key;
            var tT = element.Value;
            var index = this.Keys.IndexOf(tC);
            if (index != -1 && this.values[index].Equals(tT))
                return index;
            return -1;
        }

        #endregion

        #region IEnumerable<KeyValuePair<TCheck,TNodeTarget>> Members

        /// <summary>
        /// Obtains an enumerator which can iterate the elements
        /// of the <see cref="FiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/>
        /// which can iterate the elements of the 
        /// <see cref="FiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/>.
        /// </returns>
        public IEnumerator<KeyValuePair<TCheck, TNodeTarget>> GetEnumerator()
        {
            foreach (var kvpNode in this.backup)
                yield return new KeyValuePair<TCheck, TNodeTarget>(kvpNode.Key, kvpNode.Value.Target);
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion


        private KeysCollection InitializeKeys()
        {
            return new KeysCollection(this);
        }

        private ValuesCollection InitializeValues()
        {
            return new ValuesCollection(this);
        }


        #region IFiniteAutomataTransitionTable<TCheck,TState> Members

        /// <summary>
        /// Removes the transition that equals the <typeparamref name="TCheck"/>
        /// provided.
        /// </summary>
        /// <param name="check">The <typeparamref name="TCheck"/>
        /// that denotes the transition requirement to remove.</param>
        public void Remove(TCheck check)
        {
            if (!this.ContainsKey(check))
                return;
            if (this.fullCheckObtained)
            {
                this.fullCheck = default(TCheck);
                this.fullCheckObtained = false;
            }
            this.backup.Remove(check);
        }

        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> of
        /// targets contained within the current transition table.
        /// </summary>
        public abstract IEnumerable<TState> Targets { get; }
        #endregion

        public TCheck GetColliders(TCheck condition, out  IDictionary<TCheck, IFiniteAutomataTransitionNode<TCheck, TNodeTarget>> colliders)
        {
            Dictionary<TCheck, IFiniteAutomataTransitionNode<TCheck, TNodeTarget>> result = new Dictionary<TCheck, IFiniteAutomataTransitionNode<TCheck, TNodeTarget>>();
            TCheck current = condition;
            foreach (TCheck key in this.backup.Keys)
            {
                var intersection = key.Intersect(current);
                if (intersection.IsEmpty)
                    continue;
                result.Add(intersection, this.backup[key]);
                current = current.SymmetricDifference(intersection);
                if (current.IsEmpty)
                    break;
            }
            colliders = result;
            return current;
        }

        public IFiniteAutomataTransitionNode<TCheck, TNodeTarget> GetNode(TCheck condition)
        {
            if (!this.ContainsKey(condition))
                throw new KeyNotFoundException();
            return this.backup[condition];
        }

        protected internal void AddInternal(TCheck check, TNodeTarget target)
        {
            lock (this.backup)
            {
                if (this.fullCheckObtained)
                {
                    this.fullCheck = default(TCheck);
                    this.fullCheckObtained = false;
                }
                this.backup.Add(check, new FiniteAutomataTransitionNode<TCheck, TNodeTarget>() { Check = check, Target = target });
            }
        }

        public IEnumerable<TCheck> Checks
        {
            get
            {
                return this.backup.Keys;   
            }
        }

        /// <summary>
        /// Returns the <typeparamref name="TCheck"/> of all of the transition requirements
        /// combined into one.
        /// </summary>
        public TCheck FullCheck
        {
            get
            {
                if (!this.fullCheckObtained)
                {
                    if (this.Count == 0)
                        return GetTCheck();
                    var fullCheck = default(TCheck);
                    foreach (var check in this.Checks)
                        fullCheck = check.Union(fullCheck);
                    this.fullCheck = fullCheck;
                    this.fullCheckObtained = true;
                }
                return this.fullCheck;
            }
        }

        protected virtual TCheck GetTCheck()
        {
            return new TCheck();
        }

    }
}
