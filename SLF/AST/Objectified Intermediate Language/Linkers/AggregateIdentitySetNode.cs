using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public abstract class AggregateIdentitySetNode<TLimiter, TIdentity> :
        AggregateIdentityNode,
        IControlledStateDictionary<TLimiter, TIdentity>
    {

        #region IControlledStateDictionary<TLimiter,TIdentity> Members

        public IControlledStateCollection<TLimiter> Keys
        {
            get { throw new NotImplementedException(); }
        }

        public IControlledStateCollection<TIdentity> Values
        {
            get { throw new NotImplementedException(); }
        }

        public TIdentity this[TLimiter key]
        {
            get { throw new NotImplementedException(); }
        }

        public bool ContainsKey(TLimiter key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TLimiter key, out TIdentity value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IControlledStateCollection<KeyValuePair<TLimiter,TIdentity>> Members

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool Contains(KeyValuePair<TLimiter, TIdentity> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TLimiter, TIdentity>[] array, int arrayIndex = 0)
        {
            throw new NotImplementedException();
        }

        public KeyValuePair<TLimiter, TIdentity> this[int index]
        {
            get { throw new NotImplementedException(); }
        }

        public KeyValuePair<TLimiter, TIdentity>[] ToArray()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(KeyValuePair<TLimiter, TIdentity> element)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<KeyValuePair<TLimiter,TIdentity>> Members

        public IEnumerator<KeyValuePair<TLimiter, TIdentity>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
