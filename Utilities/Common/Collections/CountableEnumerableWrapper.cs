using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    partial class GeneralExtensions
    {
        private class CountableEnumerableWrapper<T> :
            ICountableEnumerable<T>
        {
            internal IEnumerable<T> source;
            internal Func<int> count;

            #region IEnumerable<T> Members

            public IEnumerator<T> GetEnumerator()
            {
                return source.GetEnumerator();
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #region ICountableEnumerable Members

            public int Count
            {
                get { return this.count(); }
            }

            #endregion
        }
        public static ICountableEnumerable<T> GetCountable<T>(this IEnumerable<T> source, Func<int> count)
        {
            return new CountableEnumerableWrapper<T>() { source = source, count = count };
        }
    }
}
