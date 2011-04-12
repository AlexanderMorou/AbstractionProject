using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    public class MutableDictionary<TKey, TValue> :
        ControlledStateDictionary<TKey, TValue>
    {
        public MutableDictionary()
        {

        }

        public void Add(TKey key, TValue value)
        {
            base._Add(key, value);
        }

        public bool Remove(TKey key)
        {
            return base._Remove(key);
        }

        public bool Remove(int index)
        {
            return base._Remove(index);
        }

        public void Clear()
        {
            base._Clear();
        }
    }
}
