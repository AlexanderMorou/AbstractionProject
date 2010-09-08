using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    partial class ControlledStateDictionary<TKey, TValue>
    {
        public class ValuesCollection :
            IControlledStateCollection<TValue>,
            IControlledStateCollection
        {
            private SharedLocals locals;
            public ValuesCollection(SharedLocals locals)
            {
                this.locals = locals;
            }

            protected ValuesCollection(ControlledStateDictionary<TKey, TValue> localOwner)
            {
                this.locals = localOwner.locals;
            }
            #region IControlledStateCollection<TValue> Members

            public virtual int Count
            {
                get { return this.locals.Count; }
            }

            public virtual bool Contains(TValue item)
            {
                for (int i = 0; i < this.Count; i++)
                    if (this.locals.entries[i].Value.Equals(item))
                        return true;
                return false;
            }

            public virtual void CopyTo(TValue[] array, int arrayIndex = 0)
            {
                if (arrayIndex < 0 || arrayIndex >= array.Length)
                    throw new ArgumentException("arrayIndex");
                if (this.Count + arrayIndex >= array.Length)
                    throw new ArgumentException("array");
                for (int i = 0; i < this.Count; i++)
                    array[arrayIndex + i] = this.locals.entries[i].Value;
            }

            public TValue this[int index]
            {
                get
                {
                    return OnGetThis(index);
                }
            }

            protected virtual TValue OnGetThis(int index)
            {
                if (index < 0 ||
                    index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                return this.locals.entries[index].Value;
            }

            public virtual TValue[] ToArray()
            {
                TValue[] result = new TValue[this.Count];
                this.CopyTo(result, 0);
                return result;
            }

            #endregion

            #region IEnumerable<TValue> Members

            public virtual IEnumerator<TValue> GetEnumerator()
            {
                for (int i = 0; i < this.Count; i++)
                    yield return this.locals.entries[i].Value;
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
                if (!(item is TValue))
                    throw new ArgumentException("item");
                return this.Contains((TValue)item);
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
                    array.SetValue(this.locals.entries[i].Value, i + arrayIndex);
            }

            object IControlledStateCollection.this[int index]
            {
                get { return this[index]; }
            }

            void ICollection.CopyTo(Array array, int arrayIndex)
            {
                ((IControlledStateCollection)this).CopyTo(array, arrayIndex);
            }

            #endregion

            #region ICollection Members

            public bool IsSynchronized
            {
                get { return true; }
            }

            public object SyncRoot
            {
                get { return this.locals.syncObject; }
            }

            #endregion
        }
    }
}
