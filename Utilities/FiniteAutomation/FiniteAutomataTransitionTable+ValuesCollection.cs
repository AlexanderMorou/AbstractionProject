using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    partial class FiniteAutomataTransitionTable<TCheck, TState, TNodeTarget>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TState :
            IFiniteAutomataState<TCheck, TState>
    {
        protected class ValuesCollection :
            IControlledCollection<TNodeTarget>
        {
            private FiniteAutomataTransitionTable<TCheck, TState, TNodeTarget> owner;
            /// <summary>
            /// Creates a mew <see cref="ValuesCollection"/>
            /// with the <paramref name="owner"/> provided.
            /// </summary>
            /// <param name="owner">The <see cref="FiniteAutomataTransitionTable{TCheck, TState, TNodeTarget}"/>
            /// which contains the <see cref="ValuesCollection"/>.</param>
            public ValuesCollection(FiniteAutomataTransitionTable<TCheck, TState, TNodeTarget> owner)
            {
                this.owner = owner;
            }

            #region IControlledCollection<TNodeTarget> Members

            /// <summary>
            /// Gets the number of elements contained in the <see cref="ValuesCollection"/>.</summary>
            /// <returns>
            /// The number of elements contained in the <see cref="ValuesCollection"/>.</returns>
            public int Count
            {
                get { return this.owner.backup.Count; }
            }

            /// <summary>
            /// Determines whether the <see cref="ValuesCollection"/> contains a specific 
            /// value.</summary>
            /// <param name="item">
            /// The object to locate in the <see cref="ValuesCollection"/>.</param>
            /// <returns>
            /// true if <paramref name="item"/> is found in the <see cref="ValuesCollection"/>;
            /// otherwise, false.
            /// </returns>
            public virtual bool Contains(TNodeTarget item)
            {
                foreach (var node in this.owner.backup.Values)
                    if (object.ReferenceEquals(node.Target, item))
                        return true;
                return false;
            }

            public void CopyTo(TNodeTarget[] array, int arrayIndex = 0)
            {
                if (array == null)
                    throw new ArgumentNullException("array");
                if (arrayIndex + this.Count > array.Length)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                int index = 0;
                foreach (var node in this.owner.backup.Values)
                    array[arrayIndex + index++] = node.Target;
            }

            public TNodeTarget this[int index]
            {
                get {
                    if (index < 0 ||
                        index >= this.Count)
                        throw new ArgumentOutOfRangeException("index");
                    int i = 0;
                    foreach (var node in this.owner.backup.Values)
                        if (i++ == index)
                            return node.Target;
                    return default(TNodeTarget);
                }
            }

            public TNodeTarget[] ToArray()
            {
                TNodeTarget[] result = new TNodeTarget[this.Count];
                this.CopyTo(result, 0);
                return result;
            }

            #endregion

            #region IEnumerable<TNodeTarget> Members

            public IEnumerator<TNodeTarget> GetEnumerator()
            {
                ICollection<IFiniteAutomataTransitionNode<TCheck, TNodeTarget>> nodes = this.owner.backup.Values;
                foreach (var node in nodes)
                    yield return node.Target;
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #region IControlledCollection<TNodeTarget> Members


            public int IndexOf(TNodeTarget element)
            {
                int index = -1;
                int i = 0;
                foreach (var currentElement in this.owner.Values)
                    if (currentElement.Equals(element))
                    {
                        index = i;
                        break;
                    }
                    else
                        i++;
                return index;
            }

            #endregion
        }
    }
}
