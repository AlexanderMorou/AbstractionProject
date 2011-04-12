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
        private class ValuesCollection :
            IControlledStateCollection<TIdentity>
        {
            private AggregateIdentitySetNode<TLimiter, TIdentity> owner;
            public ValuesCollection(AggregateIdentitySetNode<TLimiter, TIdentity> owner)
            {
                this.owner = owner;
            }

            #region IControlledStateCollection<TIdentity> Members

            public int Count
            {
                get { return this.owner.Count; }
            }

            public bool Contains(TIdentity item)
            {
                for (int i = 0, c = this.Count; i < c; i++)
                    if (this.owner.GetIdentity(i).Equals(item))
                        return true;
                return false;
            }

            public void CopyTo(TIdentity[] array, int arrayIndex = 0)
            {
                if (arrayIndex < 0 || arrayIndex >= array.Length)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                if (arrayIndex + this.Count > array.Length)
                    throw new ArgumentException("array");

                int c = this.Count;
                TIdentity[] result = new TIdentity[c];
                for (int i = 0; i < c; i++)
                    result[i + arrayIndex] = this.owner.GetIdentity(i);
            }

            public TIdentity this[int index]
            {
                get { return this.owner.GetIdentity(index); }
            }

            public TIdentity[] ToArray()
            {
                var result = new TIdentity[this.Count];
                this.CopyTo(result);
                return result;
            }

            public int IndexOf(TIdentity element)
            {
                for (int i = 0, c = this.Count; i < c; i++)
                    if (this.owner.GetIdentity(i).Equals(element))
                        return i;
                return -1;
            }

            #endregion

            #region IEnumerable<TIdentity> Members

            public IEnumerator<TIdentity> GetEnumerator()
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
