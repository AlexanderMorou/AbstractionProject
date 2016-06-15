using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    partial class ControlledDictionary<TKey, TValue>
    {
        private class SharedLocals
        {
            internal Dictionary<TKey, int> orderings = new Dictionary<TKey, int>();
            internal KeyValuePair<TKey, TValue>[] entries;
            internal object syncObject = new object();
            private KeysCollection keys;
            private ValuesCollection values;
            Func<KeysCollection> keysInitializer;
            Func<ValuesCollection> valuesInitializer;
            public SharedLocals(Func<KeysCollection> keysInitializer, Func<ValuesCollection> valuesInitializer)
            {
                this.entries = new KeyValuePair<TKey, TValue>[0];
                this.keysInitializer = keysInitializer;
                this.valuesInitializer = valuesInitializer;
            }

            internal void EnsureSpaceExists(int newCount)
            {
                lock (this.syncObject)
                {
                    if (this.entries == null)
                    {
                        this.entries = new KeyValuePair<TKey, TValue>[Math.Max(newCount, 1) * 2];
                        return;
                    }
                }
                if (this.entries.Length < newCount)
                {
                    lock (this.syncObject)
                    {
                        int doubleCount = Math.Max(this.orderings.Count, 2) * 2;
                        KeyValuePair<TKey, TValue>[] newEntries = new KeyValuePair<TKey, TValue>[Math.Max(doubleCount, newCount)];
                        Array.ConstrainedCopy(entries, 0, newEntries, 0, this.orderings.Count);
                        this.entries = newEntries;
                    }
                }
            }

            public int Count
            {
                get
                {
                    lock (this.syncObject)
                        return this.orderings.Count;
                }
            }

            public KeysCollection Keys
            {
                get
                {
                    lock (this.syncObject)
                    {
                        if (this.keys == null)
                            this.keys = this.keysInitializer();
                        return this.keys;
                    }
                }
            }

            public ValuesCollection Values
            {
                get
                {
                    lock (this.syncObject)
                    {
                        if (this.values == null)
                            this.values = this.valuesInitializer();
                        return this.values;
                    }
                }
            }

            internal void _Add(KeyValuePair<TKey, TValue> item)
            {
                lock (this.syncObject)
                {
                    int count = this.orderings.Count;
                    this.EnsureSpaceExists(count + 1);
                    this.entries[count] = item;
                    this.orderings.Add(item.Key, this.Count);
                }
            }

            internal virtual void _AddRange(KeyValuePair<TKey, TValue>[] elements)
            {
                if (elements == null)
                    throw new ArgumentNullException("elements");
                if (elements.Length == 0)
                    return;
                lock (this.syncObject)
                {
                    int count = this.orderings.Count;
                    EnsureSpaceExists(count + elements.Length);
                    for (int i = 0; i < elements.Length; i++)
                    {
                        var newElement = elements[i];
                        int index = orderings.Count;
                        this.entries[index] = newElement;
                        this.orderings.Add(newElement.Key, index);
                    }
                }
            }

            internal bool _Remove(int index)
            {
                if (index < 0 || index >= this.Count)
                    throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                lock (this.syncObject)
                {
                    var removed = this.entries[index];
                    for (int i = index; i < this.Count - 1; i++)
                    {
                        var next = this.entries[i + 1];
                        this.orderings[next.Key] = i;
                        this.entries[i] = next;
                    }
                    this.orderings.Remove(removed.Key);
                    this.entries[this.Count] = default(KeyValuePair<TKey, TValue>);
                }
                return true;
            }


            internal void Clear()
            {
                lock (this.syncObject)
                {
                    this.orderings.Clear();
                    this.entries = null;
                }
            }

            internal KeysCollection keysInstance
            {
                get
                {
                    lock (this.syncObject)
                        return this.keys;
                }
                set
                {
                    lock (this.syncObject)
                        this.keys = value;
                }
            }

            internal ValuesCollection valuesInstance
            {
                get
                {
                    lock (this.syncObject)
                        return this.values;
                }
                set
                {
                    lock (this.syncObject)
                        this.values = value;
                }
            }

        }
    }
}
