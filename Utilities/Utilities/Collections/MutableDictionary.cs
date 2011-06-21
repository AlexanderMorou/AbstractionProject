using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
