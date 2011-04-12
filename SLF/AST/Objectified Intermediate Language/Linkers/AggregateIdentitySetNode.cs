using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public abstract partial class AggregateIdentitySetNode<TLimiter, TIdentity> :
        AggregateIdentityNode,
        IControlledStateDictionary<TLimiter, TIdentity>
    {
        private KeysCollection keysCollection;
        private ValuesCollection valuesCollection;

        #region IControlledStateDictionary<TLimiter,TIdentity> Members

        public IControlledStateCollection<TLimiter> Keys
        {
            get {
                if (this.keysCollection == null)
                    this.keysCollection = new KeysCollection(this);
                return this.keysCollection;
            }
        }

        public IControlledStateCollection<TIdentity> Values
        {
            get {
                if (this.valuesCollection == null)
                    this.valuesCollection = new ValuesCollection(this);
                return this.valuesCollection;
            }
        }

        public TIdentity this[TLimiter key]
        {
            get {
                TIdentity result;
                if (this.TryGetValue(key, out result))
                    return result;
                throw new KeyNotFoundException();
            }
        }

        public abstract bool ContainsKey(TLimiter key);

        public bool TryGetValue(TLimiter key, out TIdentity value)
        {
            for (int i = 0, c = this.Count; i < c; i++)
            {
                if (this.GetLimiter(i).Equals(key))
                {
                    value = this.GetIdentity(i);
                    return true;
                }
            }
            value = default(TIdentity);
            return false;
        }

        #endregion

        #region IControlledStateCollection<KeyValuePair<TLimiter,TIdentity>> Members

        public abstract int Count { get; }

        protected abstract TLimiter GetLimiter(int index);

        /// <summary>
        /// Obtains the <typeparamref name="TIdentity"/> at the 
        /// <paramref name="index"/> provided.
        /// </summary>
        /// <param name="index">The <see cref="Int32"/> value representing
        /// the ordinal index of the <typeparamref name="TIdentity"/> to retrieve.</param>
        /// <returns>A <typeparamref name="TIdentity"/>
        /// instance relative to the <paramref name="index"/> provided.</returns>
        protected abstract TIdentity GetIdentity(int index);

        public bool Contains(KeyValuePair<TLimiter, TIdentity> item)
        {
            for (int i = 0, c = this.Count; i < c; i++)
            {
                var k = this.GetLimiter(i);
                if (k.Equals(item.Key)
                    && this.GetIdentity(i).Equals(item.Value))
                    return true;
            }
            return false;
        }

        public void CopyTo(KeyValuePair<TLimiter, TIdentity>[] array, int arrayIndex = 0)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex");
            int c = this.Count;
            if (c + arrayIndex > array.Length)
                throw new ArgumentException("arrayIndex");
            for (int i = 0; i < c; i++)
                array[i + arrayIndex] = this[i];
        }

        public KeyValuePair<TLimiter, TIdentity> this[int index]
        {
            get
            {
                return new KeyValuePair<TLimiter, TIdentity>(this.GetLimiter(index), this.GetIdentity(index));
            }
        }

        public KeyValuePair<TLimiter, TIdentity>[] ToArray()
        {
            var result = new KeyValuePair<TLimiter, TIdentity>[this.Count];
            this.CopyTo(result);
            return result;
        }

        public int IndexOf(KeyValuePair<TLimiter, TIdentity> element)
        {
            for (int i = 0, c = this.Count; i < c; i++)
            {
                var k = this.GetLimiter(i);
                if (k.Equals(element.Key)
                    && this.GetIdentity(i).Equals(element.Value))
                    return i;
            }
            return -1;
        }

        #endregion

        #region IEnumerable<KeyValuePair<TLimiter,TIdentity>> Members

        public IEnumerator<KeyValuePair<TLimiter, TIdentity>> GetEnumerator()
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
