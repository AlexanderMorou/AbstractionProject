using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    partial class AggregateIdentitySetNode<TLimiter, TIdentity>
    {
        private class KeysCollection :
            IControlledStateCollection<TLimiter>
        {
            private AggregateIdentitySetNode<TLimiter, TIdentity> owner;
            public KeysCollection(AggregateIdentitySetNode<TLimiter, TIdentity> owner)
            {
                this.owner = owner;
            }

            #region IControlledStateCollection<TLimiter> Members

            public int Count
            {
                get {return this.owner.Count; }
            }

            public bool Contains(TLimiter item)
            {
                for (int i = 0, c = this.Count; i < c; i++)
                    if (this.owner.GetLimiter(i).Equals(item))
                        return true;
                return false;
            }

            public void CopyTo(TLimiter[] array, int arrayIndex = 0)
            {
                if (arrayIndex < 0 || arrayIndex >= array.Length)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                if (arrayIndex + this.Count > array.Length)
                    throw new ArgumentException("array");

                int c = this.Count;
                TLimiter[] result = new TLimiter[c];
                for (int i = 0; i < c; i++)
                    result[i + arrayIndex] = this.owner.GetLimiter(i);
            }

            public TLimiter this[int index]
            {
                get { return this.owner.GetLimiter(index); }
            }

            public TLimiter[] ToArray()
            {
                var result = new TLimiter[this.Count];
                this.CopyTo(result);
                return result;
            }

            public int IndexOf(TLimiter element)
            {
                for (int i = 0, c = this.Count; i < c; i++)
                    if (this.owner.GetLimiter(i).Equals(element))
                        return i;
                return -1;
            }

            #endregion

            #region IEnumerable<TLimiter> Members

            public IEnumerator<TLimiter> GetEnumerator()
            {
                for (int i = 0, c = this.Count; i < c; i++)
                    yield return this[i];
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion
        }
    }
}
