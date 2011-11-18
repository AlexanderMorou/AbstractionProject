using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
#if MKD_SEVEN
    public class MultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue> :
        MutableDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>>,
        IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>
    {
        #region IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTreeLevel2<TKey3,TKey4,TKey5,TKey6,TKey7,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTreeLevel2<TKey4,TKey5,TKey6,TKey7,TValue,MultikeyedTreeLevel2<TKey3,TKey4,TKey5,TKey6,TKey7,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTreeLevel2<TKey5,TKey6,TKey7,TValue,MultikeyedTreeLevel2<TKey4,TKey5,TKey6,TKey7,TValue,MultikeyedTreeLevel2<TKey3,TKey4,TKey5,TKey6,TKey7,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTreeLevel2<TKey6,TKey7,TValue,MultikeyedTreeLevel2<TKey5,TKey6,TKey7,TValue,MultikeyedTreeLevel2<TKey4,TKey5,TKey6,TKey7,TValue,MultikeyedTreeLevel2<TKey3,TKey4,TKey5,TKey6,TKey7,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTreeLevel2<TKey7,TValue,MultikeyedTreeLevel2<TKey6,TKey7,TValue,MultikeyedTreeLevel2<TKey5,TKey6,TKey7,TValue,MultikeyedTreeLevel2<TKey4,TKey5,TKey6,TKey7,TValue,MultikeyedTreeLevel2<TKey3,TKey4,TKey5,TKey6,TKey7,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,TValue> Members

        public void Add(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TValue value)
        {
            IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>> six;
            IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>> five;
            IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>> four;
            IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>> three;
            IMultikeyedTreeLevel2<TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>> two;
            IMultikeyedTreeLevel2<TKey7, TValue, IMultikeyedTreeLevel2<TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>> one;
            if (this.TryGetValue(key1, out six))
                if (six.TryGetValue(key2, out five))
                    if (five.TryGetValue(key3, out four))
                        if (four.TryGetValue(key4, out three))
                            if (three.TryGetValue(key5, out two))
                                if (two.TryGetValue(key6, out one))
                                    if (one.ContainsKey(key7))
                                        throw new InvalidOperationException();
                                    else
                                        one._Add(key7, value);
                                else
                                {
                                    two._Add(key6, one = new MultikeyedTreeLevel2<TKey7, TValue, IMultikeyedTreeLevel2<TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(two));
                                    ((MultikeyedTreeLevel2<TKey7, TValue, IMultikeyedTreeLevel2<TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>)one).Add(key7, value);
                                }
                            else
                            {
                                three._Add(key5, two = new MultikeyedTreeLevel2<TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(three));
                                two._Add(key6, one = new MultikeyedTreeLevel2<TKey7, TValue, IMultikeyedTreeLevel2<TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(two));
                                one._Add(key7, value);
                            }
                        else
                        {
                            four._Add(key4, three = new MultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(four));
                            three._Add(key5, two = new MultikeyedTreeLevel2<TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(three));
                            two._Add(key6, one = new MultikeyedTreeLevel2<TKey7, TValue, IMultikeyedTreeLevel2<TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(two));
                            one._Add(key7, value);
                        }
                    else
                    {
                        five._Add(key3, four = new MultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(five));
                        four._Add(key4, three = new MultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(four));
                        three._Add(key5, two = new MultikeyedTreeLevel2<TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(three));
                        two._Add(key6, one = new MultikeyedTreeLevel2<TKey7, TValue, IMultikeyedTreeLevel2<TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(two));
                        one._Add(key7, value);
                    }
                else
                {
                    six._Add(key2, five = new MultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(six));
                    five._Add(key3, four = new MultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(five));
                    four._Add(key4, three = new MultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(four));
                    three._Add(key5, two = new MultikeyedTreeLevel2<TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(three));
                    two._Add(key6, one = new MultikeyedTreeLevel2<TKey7, TValue, IMultikeyedTreeLevel2<TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(two));
                    one._Add(key7, value);
                }
            else
            {
                this._Add(key1, six = new MultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(this));
                six._Add(key2, five = new MultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(six));
                five._Add(key3, four = new MultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(five));
                four._Add(key4, three = new MultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(four));
                three._Add(key5, two = new MultikeyedTreeLevel2<TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(three));
                two._Add(key6, one = new MultikeyedTreeLevel2<TKey7, TValue, IMultikeyedTreeLevel2<TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>(two));
                one._Add(key7, value);
            }

        }

        public bool Remove(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            foreach (var item in this.Values)
                item._Clear();
            this._Clear();
        }

        public TValue this[TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7]
        {
            get
            {
                return this[key1][key2][key3][key4][key5][key6][key7];
            }
            set
            {
                ((MutableDictionary<TKey7, TValue>)this[key1][key2][key3][key4][key5][key6])[key7] = value;
            }
        }

        #endregion

        #region IControlledStateIMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>,IMultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTreeLevel2<TKey3,TKey4,TKey5,TKey6,TKey7,TValue,IMultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTreeLevel2<TKey4,TKey5,TKey6,TKey7,TValue,IMultikeyedTreeLevel2<TKey3,TKey4,TKey5,TKey6,TKey7,TValue,IMultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTreeLevel2<TKey5,TKey6,TKey7,TValue,IMultikeyedTreeLevel2<TKey4,TKey5,TKey6,TKey7,TValue,IMultikeyedTreeLevel2<TKey3,TKey4,TKey5,TKey6,TKey7,TValue,IMultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTreeLevel2<TKey6,TKey7,TValue,IMultikeyedTreeLevel2<TKey5,TKey6,TKey7,TValue,IMultikeyedTreeLevel2<TKey4,TKey5,TKey6,TKey7,TValue,IMultikeyedTreeLevel2<TKey3,TKey4,TKey5,TKey6,TKey7,TValue,IMultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTreeLevel2<TKey7,TValue,IMultikeyedTreeLevel2<TKey6,TKey7,TValue,IMultikeyedTreeLevel2<TKey5,TKey6,TKey7,TValue,IMultikeyedTreeLevel2<TKey4,TKey5,TKey6,TKey7,TValue,IMultikeyedTreeLevel2<TKey3,TKey4,TKey5,TKey6,TKey7,TValue,IMultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7,TValue>>,TValue> Members

        public int ContainsKeys(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7)
        {
            IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>> six;
            IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>> five;
            IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>> four;
            IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>> three;
            IMultikeyedTreeLevel2<TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>> two;
            IMultikeyedTreeLevel2<TKey7, TValue, IMultikeyedTreeLevel2<TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>> one;
            if (this.TryGetValue(key1, out six))
                if (six.TryGetValue(key2, out five))
                    if (five.TryGetValue(key3, out four))
                        if (four.TryGetValue(key4, out three))
                            if (three.TryGetValue(key5, out two))
                                if (two.TryGetValue(key6, out one))
                                    if (one.ContainsKey(key7))
                                        return 7;
                                    else
                                        return 6;
                                else
                                    return 5;
                            else
                                return 4;
                        else
                            return 3;
                    else
                        return 2;
                else
                    return 1;
            else
                return 0;
        }

        public bool TryGetValue(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, out TValue value)
        {
            IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>> six;
            IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>> five;
            IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>> four;
            IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>> three;
            IMultikeyedTreeLevel2<TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>> two;
            IMultikeyedTreeLevel2<TKey7, TValue, IMultikeyedTreeLevel2<TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>> one;
            if (this.TryGetValue(key1, out six) &&
                six.TryGetValue(key2, out five) &&
                five.TryGetValue(key3, out four) &&
                four.TryGetValue(key4, out three) &&
                three.TryGetValue(key5, out two) &&
                two.TryGetValue(key6, out one) &&
                one.TryGetValue(key7, out value))
                return true;
            value = default(TValue);
            return false;
        }

        #endregion

        #region IMultikeyedTreeLevel2 Members

        public int Level
        {
            get { return 1; }
        }

        public IMultikeyedTreeTopLevel2 TopLevel
        {
            get { return this.TopLevel; }
        }

        #endregion
    }
#endif
#if MKD_SIX
    public class MultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue> :
        MutableDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>>,
        IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>
    {
        #region IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTreeLevel2<TKey3,TKey4,TKey5,TKey6,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTreeLevel2<TKey4,TKey5,TKey6,TValue,MultikeyedTreeLevel2<TKey3,TKey4,TKey5,TKey6,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTreeLevel2<TKey5,TKey6,TValue,MultikeyedTreeLevel2<TKey4,TKey5,TKey6,TValue,MultikeyedTreeLevel2<TKey3,TKey4,TKey5,TKey6,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTreeLevel2<TKey6,TValue,MultikeyedTreeLevel2<TKey5,TKey6,TValue,MultikeyedTreeLevel2<TKey4,TKey5,TKey6,TValue,MultikeyedTreeLevel2<TKey3,TKey4,TKey5,TKey6,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,TValue> Members

        public void Add(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TValue value)
        {
            IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>> five;
            IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>> four;
            IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>> three;
            IMultikeyedTreeLevel2<TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>> two;
            IMultikeyedTreeLevel2<TKey6, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>> one;
            if (this.TryGetValue(key1, out five))
                if (five.TryGetValue(key2, out four))
                    if (four.TryGetValue(key3, out three))
                        if (three.TryGetValue(key4, out two))
                            if (two.TryGetValue(key5, out one))
                                if (one.ContainsKey(key6))
                                    throw new InvalidOperationException();
                                else
                                    one._Add(key6, value);
                            else
                            {

                                two._Add(key5, one = new MultikeyedTreeLevel2<TKey6, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>(two));
                                one._Add(key6, value);
                            }
                        else
                        {
                            three._Add(key4, two = new MultikeyedTreeLevel2<TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>(three));
                            two._Add(key5, one = new MultikeyedTreeLevel2<TKey6, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>(two));
                            one._Add(key6, value);
                        }
                    else
                    {
                        four._Add(key3, three = new MultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>(four));
                        three._Add(key4, two = new MultikeyedTreeLevel2<TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>(three));
                        two._Add(key5, one = new MultikeyedTreeLevel2<TKey6, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>(two));
                        one._Add(key6, value);
                    }
                else
                {
                    five._Add(key2, four = new MultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>(five));
                    four._Add(key3, three = new MultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>(four));
                    three._Add(key4, two = new MultikeyedTreeLevel2<TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>(three));
                    two._Add(key5, one = new MultikeyedTreeLevel2<TKey6, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>(two));
                    one._Add(key6, value);
                }
            else
            {
                this._Add(key1, five = new MultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>(this));
                five._Add(key2, four = new MultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>(five));
                four._Add(key3, three = new MultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>(four));
                three._Add(key4, two = new MultikeyedTreeLevel2<TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>(three));
                two._Add(key5, one = new MultikeyedTreeLevel2<TKey6, TValue, IMultikeyedTreeLevel2<TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>(two));
                one._Add(key6, value);
            }
        }

        public bool Remove(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            foreach (var item in this.Values)
                item._Clear();
            this._Clear();
        }

        public TValue this[TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6]
        {
            get
            {
                return this[key1][key2][key3][key4][key5][key6];
            }
            set
            {
                ((MutableDictionary<TKey6, TValue>)this[key1][key2][key3][key4][key5])[key6] = value;
            }
        }

        #endregion

        #region IMultikeyedTreeLevel2 Members

        public IMultikeyedTreeTopLevel2 TopLevel
        {
            get { return this; }
        }

        public int Level
        {
            get { return 1; }
        }

        #endregion

        #region IControlledStateMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTreeLevel2<TKey3,TKey4,TKey5,TKey6,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTreeLevel2<TKey4,TKey5,TKey6,TValue,MultikeyedTreeLevel2<TKey3,TKey4,TKey5,TKey6,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTreeLevel2<TKey5,TKey6,TValue,MultikeyedTreeLevel2<TKey4,TKey5,TKey6,TValue,MultikeyedTreeLevel2<TKey3,TKey4,TKey5,TKey6,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTreeLevel2<TKey6,TValue,MultikeyedTreeLevel2<TKey5,TKey6,TValue,MultikeyedTreeLevel2<TKey4,TKey5,TKey6,TValue,MultikeyedTreeLevel2<TKey3,TKey4,TKey5,TKey6,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TKey6,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TValue>>,TValue> Members

        public int ContainsKeys(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6)
        {
           IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>> five;
           IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>> four;
           IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>> three;
           IMultikeyedTreeLevel2<TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>> two;
           IMultikeyedTreeLevel2<TKey6, TValue,IMultikeyedTreeLevel2<TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>> one;
            if (this.TryGetValue(key1, out five))
                if (five.TryGetValue(key2, out four))
                    if (four.TryGetValue(key3, out three))
                        if (three.TryGetValue(key4, out two))
                            if (two.TryGetValue(key5, out one))
                                if (one.ContainsKey(key6))
                                    return 6;
                                else
                                    return 5;
                            else
                                return 4;
                        else
                            return 3;
                    else
                        return 2;
                else
                    return 1;
            else
                return 0;

        }

        public bool TryGetValue(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, out TValue value)
        {
           IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>> five;
           IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>> four;
           IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>> three;
           IMultikeyedTreeLevel2<TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>> two;
           IMultikeyedTreeLevel2<TKey6, TValue,IMultikeyedTreeLevel2<TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey4, TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>,IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>> one;
            if (this.TryGetValue(key1, out five) &&
                five.TryGetValue(key2, out four) &&
                four.TryGetValue(key3, out three) &&
                three.TryGetValue(key4, out two) &&
                two.TryGetValue(key5, out one) &&
                one.TryGetValue(key6, out value))
                return true;
            value = default(TValue);
            return false;
        }

        #endregion

    }
#endif
#if MKD_FIVE
    public class MultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue> :
        MutableDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>>,
        IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>
    {
        #region IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,MultikeyedTreeLevel2<TKey3,TKey4,TKey5,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,MultikeyedTreeLevel2<TKey4,TKey5,TValue,MultikeyedTreeLevel2<TKey3,TKey4,TKey5,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,MultikeyedTreeLevel2<TKey5,TValue,MultikeyedTreeLevel2<TKey4,TKey5,TValue,MultikeyedTreeLevel2<TKey3,TKey4,TKey5,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,TValue> Members

        public void Add(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TValue value)
        {
            IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>> four;
            IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>> three;
            IMultikeyedTreeLevel2<TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>> two;
            IMultikeyedTreeLevel2<TKey5, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>> one;
            if (this.TryGetValue(key1, out four))
                if (four.TryGetValue(key2, out three))
                    if (three.TryGetValue(key3, out two))
                        if (two.TryGetValue(key4, out one))
                            if (one.ContainsKey(key5))
                                throw new InvalidOperationException();
                            else
                                one._Add(key5, value);
                        else
                        {
                            two._Add(key4, one = new MultikeyedTreeLevel2<TKey5, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>(two));
                            one._Add(key5, value);
                        }
                    else
                    {
                        three._Add(key3, two = new MultikeyedTreeLevel2<TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>(three));
                        two._Add(key4, one = new MultikeyedTreeLevel2<TKey5, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>(two));
                        one._Add(key5, value);
                    }
                else
                {
                    four._Add(key2, three = new MultikeyedTreeLevel2<TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>(four));
                    three._Add(key3, two = new MultikeyedTreeLevel2<TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>(three));
                    two._Add(key4, one = new MultikeyedTreeLevel2<TKey5, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>(two));
                    one._Add(key5, value);
                }
            else
            {
                this._Add(key1, four = new MultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>(this));
                four._Add(key2, three = new MultikeyedTreeLevel2<TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>(four));
                three._Add(key3, two = new MultikeyedTreeLevel2<TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>(three));
                two._Add(key4, one = new MultikeyedTreeLevel2<TKey5, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>(two));
                one._Add(key5, value);
            }
        }

        public bool Remove(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            foreach (var item in this.Values)
                item._Clear();
            this._Clear();
        }

        public TValue this[TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5]
        {
            get
            {
                return this[key1][key2][key3][key4][key5];
            }
            set
            {
                ((MutableDictionary<TKey5, TValue>)this[key1][key2][key3][key4])[key5] = value;
            }
        }

        #endregion

        #region IMultikeyedTreeLevel2 Members

        public IMultikeyedTreeTopLevel2 TopLevel
        {
            get { return this; }
        }

        public int Level
        {
            get { return 1; }
        }

        #endregion

        #region IControlledStateMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,MultikeyedTreeLevel2<TKey3,TKey4,TKey5,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,MultikeyedTreeLevel2<TKey4,TKey5,TValue,MultikeyedTreeLevel2<TKey3,TKey4,TKey5,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,MultikeyedTreeLevel2<TKey5,TValue,MultikeyedTreeLevel2<TKey4,TKey5,TValue,MultikeyedTreeLevel2<TKey3,TKey4,TKey5,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TKey5,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TKey5,TValue>>,TValue> Members

        public int ContainsKeys(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5)
        {
            IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>> four;
            IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>> three;
            IMultikeyedTreeLevel2<TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>> two;
            IMultikeyedTreeLevel2<TKey5, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>> one;
            if (this.TryGetValue(key1, out four))
                if (four.TryGetValue(key2, out three))
                    if (three.TryGetValue(key3, out two))
                        if (two.TryGetValue(key4, out one))
                            if (one.ContainsKey(key5))
                                return 5;
                            else
                                return 4;
                        else
                            return 3;
                    else
                        return 2;
                else
                    return 1;
            else
                return 0;
        }

        public bool TryGetValue(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, out TValue value)
        {
            IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>> four;
            IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>> three;
            IMultikeyedTreeLevel2<TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>> two;
            IMultikeyedTreeLevel2<TKey5, TValue, IMultikeyedTreeLevel2<TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>> one;
            if (this.TryGetValue(key1, out four) &&
                four.TryGetValue(key2, out three) &&
                three.TryGetValue(key3, out two) &&
                two.TryGetValue(key4, out one) &&
                one.TryGetValue(key5, out value))
                return true;
            value = default(TValue);
            return false;
        }

        #endregion
    }
#endif
#if MKD_FOUR
    public class MultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue> :
        MutableDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>>,
        IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>
    {
        #region IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>>,MultikeyedTreeLevel2<TKey3,TKey4,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>>,MultikeyedTreeLevel2<TKey4,TValue,MultikeyedTreeLevel2<TKey3,TKey4,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TKey4,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>>,TValue> Members

        public void Add(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TValue value)
        {
            IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>> three;
            IMultikeyedTreeLevel2<TKey3, TKey4, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>> two;
            IMultikeyedTreeLevel2<TKey4, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>> one;
            if (this.TryGetValue(key1, out three))
                if (three.TryGetValue(key2, out two))
                    if (two.TryGetValue(key3, out one))
                        if (one.ContainsKey(key4))
                            throw new InvalidOperationException();
                        else
                            one._Add(key4, value);
                    else
                    {
                        two._Add(key3, one = new MultikeyedTreeLevel2<TKey4, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>(two));
                        one._Add(key4, value);
                    }
                else
                {
                    three._Add(key2, two = new MultikeyedTreeLevel2<TKey3, TKey4, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>(three));
                    two._Add(key3, one = new MultikeyedTreeLevel2<TKey4, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>(two));
                    one._Add(key4, value);
                }
            else
            {
                this._Add(key1, three = new MultikeyedTreeLevel2<TKey2, TKey3, TKey4, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>(this));
                three._Add(key2, two = new MultikeyedTreeLevel2<TKey3, TKey4, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>(three));
                two._Add(key3, one = new MultikeyedTreeLevel2<TKey4, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>(two));
                one._Add(key4, value);
            }
        }

        public bool Remove(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            foreach (var item in this.Values)
                item._Clear();
            this._Clear();
        }

        public TValue this[TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4]
        {
            get
            {
                return this[key1][key2][key3][key4];
            }
            set
            {
                ((MutableDictionary<TKey4, TValue>)this[key1][key2][key3])[key4] = value;
            }
        }

        #endregion

        #region IMultikeyedTreeLevel2 Members

        public IMultikeyedTreeTopLevel2 TopLevel
        {
            get { return this; }
        }

        public int Level
        {
            get { return 1; }
        }

        #endregion

        #region IControlledStateIMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>,IMultikeyedTreeLevel2<TKey2,TKey3,TKey4,TValue,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>>,IMultikeyedTreeLevel2<TKey3,TKey4,TValue,IMultikeyedTreeLevel2<TKey2,TKey3,TKey4,TValue,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>>,IMultikeyedTreeLevel2<TKey4,TValue,IMultikeyedTreeLevel2<TKey3,TKey4,TValue,IMultikeyedTreeLevel2<TKey2,TKey3,TKey4,TValue,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>>,IMultikeyedTree2<TKey1,TKey2,TKey3,TKey4,TValue>>,TValue> Members

        public int ContainsKeys(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4)
        {
            IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>> three;
            IMultikeyedTreeLevel2<TKey3, TKey4, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>> two;
            IMultikeyedTreeLevel2<TKey4, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>> one;
            if (this.TryGetValue(key1, out three))
                if (three.TryGetValue(key2, out two))
                    if (two.TryGetValue(key3, out one))
                        if (one.ContainsKey(key4))
                            return 4;
                        else
                            return 3;
                    else
                        return 2;
                else
                    return 1;
            else
                return 0;
        }

        public bool TryGetValue(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, out TValue value)
        {
            IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>> three;
            IMultikeyedTreeLevel2<TKey3, TKey4, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>> two;
            IMultikeyedTreeLevel2<TKey4, TValue, IMultikeyedTreeLevel2<TKey3, TKey4, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>> one;
            if (this.TryGetValue(key1, out three) &&
                three.TryGetValue(key2, out two) &&
                two.TryGetValue(key3, out one) &&
                one.TryGetValue(key4, out value))
                return true;
            value = default(TValue);
            return false;
        }

        #endregion

    }
#endif
    public class MultikeyedTree2<TKey1, TKey2, TKey3, TValue> :
        MutableDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TKey3, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>>>,
        IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>
    {
        #region IMultikeyedTree2<TKey1,TKey2,TKey3,MultikeyedTree2<TKey1,TKey2,TKey3,TValue>,MultikeyedTreeLevel2<TKey2,TKey3,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TValue>>,MultikeyedTreeLevel2<TKey3,TValue,MultikeyedTreeLevel2<TKey2,TKey3,TValue,MultikeyedTree2<TKey1,TKey2,TKey3,TValue>,MultikeyedTree2<TKey1,TKey2,TKey3,TValue>>,MultikeyedTree2<TKey1,TKey2,TKey3,TValue>>,TValue> Members

        public void Add(TKey1 key1, TKey2 key2, TKey3 key3, TValue value)
        {
            IMultikeyedTreeLevel2<TKey2, TKey3, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>> two;
            IMultikeyedTreeLevel2<TKey3, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>> one;
            if (this.TryGetValue(key1, out two))
                if (two.TryGetValue(key2, out one))
                    if (one.ContainsKey(key3))
                        throw new InvalidOperationException();
                    else
                        one._Add(key3, value);
                else
                {
                    two._Add(key2, one = new MultikeyedTreeLevel2<TKey3, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>>(two));
                    one._Add(key3, value);
                }
            else
            {
                this._Add(key1, two = new MultikeyedTreeLevel2<TKey2, TKey3, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>>(this));
                two._Add(key2, one = new MultikeyedTreeLevel2<TKey3, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>>(two));
                one._Add(key3, value);
            }
        }

        public bool Remove(TKey1 key1, TKey2 key2, TKey3 key3)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            foreach (var item in this.Values)
                item._Clear();
            this._Clear();
        }

        public TValue this[TKey1 key1, TKey2 key2, TKey3 key3]
        {
            get
            {
                return this[key1][key2][key3];
            }
            set
            {
                ((MutableDictionary<TKey3, TValue>)this[key1][key2])[key3] = value;
            }
        }

        #endregion

        #region IMultikeyedTreeLevel2 Members

        public IMultikeyedTreeTopLevel2 TopLevel
        {
            get { return this; }
        }

        public int Level
        {
            get { return 1; }
        }

        #endregion

        #region IControlledStateIMultikeyedTree2<TKey1,TKey2,TKey3,IMultikeyedTree2<TKey1,TKey2,TKey3,TValue>,IMultikeyedTreeLevel2<TKey2,TKey3,TValue,IMultikeyedTree2<TKey1,TKey2,TKey3,TValue>,IMultikeyedTree2<TKey1,TKey2,TKey3,TValue>>,IMultikeyedTreeLevel2<TKey3,TValue,IMultikeyedTreeLevel2<TKey2,TKey3,TValue,IMultikeyedTree2<TKey1,TKey2,TKey3,TValue>,IMultikeyedTree2<TKey1,TKey2,TKey3,TValue>>,IMultikeyedTree2<TKey1,TKey2,TKey3,TValue>>,TValue> Members

        public int ContainsKeys(TKey1 key1, TKey2 key2, TKey3 key3)
        {
            IMultikeyedTreeLevel2<TKey2, TKey3, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>> two;
            IMultikeyedTreeLevel2<TKey3, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>> one;
            if (this.TryGetValue(key1, out two))
                if (two.TryGetValue(key2, out one))
                    if (one.ContainsKey(key3))
                        return 3;
                    else
                        return 2;
                else
                    return 1;
            else
                return 0;
        }

        public bool TryGetValue(TKey1 key1, TKey2 key2, TKey3 key3, out TValue value)
        {
            IMultikeyedTreeLevel2<TKey2, TKey3, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>> two;
            IMultikeyedTreeLevel2<TKey3, TValue, IMultikeyedTreeLevel2<TKey2, TKey3, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>>, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>> one;
            if (this.TryGetValue(key1, out two) &&
                two.TryGetValue(key2, out one) &&
                one.TryGetValue(key3, out value))
                return true;
            value = default(TValue);
            return false;
        }

        #endregion

    }
    public class MultikeyedTree2<TKey1, TKey2, TValue> :
        MutableDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TValue, IMultikeyedTree2<TKey1, TKey2, TValue>, IMultikeyedTree2<TKey1, TKey2, TValue>>>,
        IMultikeyedTree2<TKey1, TKey2, TValue>
    {
        #region IMultikeyedTree2<TKey1,TKey2,IMultikeyedTree2<TKey1,TKey2,TValue>,IMultikeyedTreeLevel2<TKey2,TValue,IMultikeyedTree2<TKey1,TKey2,TValue>,IMultikeyedTree2<TKey1,TKey2,TValue>>,TValue> Members

        public void Add(TKey1 key1, TKey2 key2, TValue value)
        {
            IMultikeyedTreeLevel2<TKey2, TValue, IMultikeyedTree2<TKey1, TKey2, TValue>, IMultikeyedTree2<TKey1, TKey2, TValue>> one;
            if (this.TryGetValue(key1, out one))
                if (one.ContainsKey(key2))
                    throw new InvalidOperationException();
                else
                    one._Add(key2, value);
            else
            {
                this._Add(key1, one = new MultikeyedTreeLevel2<TKey2, TValue, IMultikeyedTree2<TKey1, TKey2, TValue>, IMultikeyedTree2<TKey1, TKey2, TValue>>(this));
                one._Add(key2, value);
            }
        }

        public bool Remove(TKey1 key1, TKey2 key2)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            foreach (var item in this.Values)
                item._Clear();
            this._Clear();
        }

        public TValue this[TKey1 key1, TKey2 key2]
        {
            get
            {
                return this[key1][key2];
            }
            set
            {
                ((MutableDictionary<TKey2, TValue>)this[key1])[key2] = value;
            }
        }

        #endregion

        #region IMultikeyedTreeLevel2 Members

        public IMultikeyedTreeTopLevel2 TopLevel
        {
            get { return this; }
        }

        public int Level
        {
            get { return 1; }
        }

        #endregion

        #region IControlledStateMultikeyedTree2<TKey1,TKey2,MultikeyedTree2<TKey1,TKey2,TValue>,MultikeyedTreeLevel2<TKey2,TValue,MultikeyedTree2<TKey1,TKey2,TValue>,MultikeyedTree2<TKey1,TKey2,TValue>>,TValue> Members

        public int ContainsKeys(TKey1 key1, TKey2 key2)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey1 key1, TKey2 key2, out TValue value)
        {
            IMultikeyedTreeLevel2<TKey2, TValue, IMultikeyedTree2<TKey1, TKey2, TValue>, IMultikeyedTree2<TKey1, TKey2, TValue>> one;
            if (this.TryGetValue(key1, out one))
                if (one.TryGetValue(key2, out value))
                    return true;
            value = default(TValue);
            return false;
        }

        #endregion

    }

}
