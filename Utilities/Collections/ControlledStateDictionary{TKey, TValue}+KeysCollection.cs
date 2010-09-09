using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    partial class ControlledStateDictionary<TKey, TValue>
    {
        public class KeysCollection :
            IControlledStateCollection<TKey>,
            IControlledStateCollection
        {
            private SharedLocals locals;
            public KeysCollection(SharedLocals locals)
            {
                this.locals = locals;
            }

            protected KeysCollection(ControlledStateDictionary<TKey, TValue> localOwner)
            {
                this.locals = localOwner.locals;
            }
            #region IControlledStateCollection<TKey> Members

            public virtual int Count
            {
                get { return this.locals.Count; }
            }

            public virtual bool Contains(TKey item)
            {
                return this.locals.orderings.ContainsKey(item);
            }

            public virtual void CopyTo(TKey[] array, int arrayIndex = 0)
            {
                if (this.Count == 0)
                    return;
                if (arrayIndex < 0 || arrayIndex >= array.Length)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                if (this.Count + arrayIndex > array.Length)
                    throw new ArgumentException("array");
                lock (this.locals.syncObject)
                    this.locals.orderings.Keys.CopyTo(array, arrayIndex);
            }

            public virtual TKey this[int index]
            {
                get
                {
                    return OnGetKey(index);
                }
                internal protected set
                {
                    OnSetKey(index, value);
                }
            }

            protected virtual TKey OnGetKey(int index)
            {
                if (index < 0 || index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                return this.locals.entries[index].Key;
            }

            protected virtual void OnSetKey(int index, TKey value)
            {
                if (index < 0 || index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                var currentElement = this.locals.entries[index];
                if (this.locals.orderings.ContainsKey(value))
                    throw new ArgumentException("element with key already exists", "value");
                this.locals.orderings.Remove(currentElement.Key);
                this.locals.orderings.Add(value, index);
                this.locals.entries[index] = new KeyValuePair<TKey, TValue>(value, currentElement.Value);
            }

            public virtual TKey[] ToArray()
            {
                TKey[] result;
                lock (this.locals.syncObject)
                {
                    result = new TKey[this.Count];
                    this.locals.orderings.Keys.CopyTo(result, 0);
                }
                return result;
            }

            #endregion

            #region IEnumerable<TKey> Members

            public virtual IEnumerator<TKey> GetEnumerator()
            {
                for (int i = 0; i < this.Count; i++)
                    yield return this.locals.entries[i].Key;
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #region IControlledStateCollection Members


            bool IControlledStateCollection.Contains(object item)
            {
                if (!(item is TKey))
                    throw new ArgumentException("item");
                return this.Contains((TKey)item);
            }

            void IControlledStateCollection.CopyTo(Array array, int arrayIndex)
            {
                ICollection_CopyTo(array, arrayIndex);
            }

            protected virtual void ICollection_CopyTo(Array array, int arrayIndex)
            {
                if (arrayIndex < 0 || arrayIndex >= array.Length)
                    throw new ArgumentException("arrayIndex");
                if (this.Count + arrayIndex >= array.Length)
                    throw new ArgumentException("array");
                for (int i = 0; i < this.Count; i++)
                    array.SetValue(this.locals.entries[i].Key, i + arrayIndex);
            }

            object IControlledStateCollection.this[int index]
            {
                get { return this[index]; }
            }

            #endregion

            #region ICollection Members

            void ICollection.CopyTo(Array array, int arrayIndex)
            {
                ((IControlledStateCollection)this).CopyTo(array, arrayIndex);
            }


            public bool IsSynchronized
            {
                get { return true; }
            }

            public object SyncRoot
            {
                get { return this.locals.syncObject; }
            }

            #endregion

            internal void Rekey(IEnumerable<Tuple<TKey, TKey>> oldNewSeries)
            {
                var cachedElements = oldNewSeries.ToArray();
                for (int i = 0; i < cachedElements.Length; i++)
                {
                    int currentIndex;
                    var current = cachedElements[i];
                    var oldKey = current.Item1;
                    if (this.locals.orderings.TryGetValue(oldKey, out currentIndex))
                    {
                        var newKey = current.Item2;
                        this.locals.orderings.Remove(oldKey);
                        this.locals.orderings.Add(newKey, currentIndex);
                        var currentElement = this.locals.entries[currentIndex].Value;
                        this.locals.entries[currentIndex] = new KeyValuePair<TKey, TValue>(newKey, currentElement);
                    }
                    else
                        throw new KeyNotFoundException(string.Format("The key in element {0} was not found.", i));
                }
            }

            public int IndexOf(TKey key)
            {
                return this.locals.orderings[key];
            }
        }

    }
}
