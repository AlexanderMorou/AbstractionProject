using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    internal class ConcatenatedArrayReadOnlyCollection<T> :
        IArrayReadOnlyCollection<T>
    {
        private T[][] sets;

        public ConcatenatedArrayReadOnlyCollection(T[] first, T[] second)
        {
            this.sets = new T[][] { first, second };
        }

        public ConcatenatedArrayReadOnlyCollection(T[][] first, T[][] second)
        {
            if (first == null)
                throw new ArgumentNullException("first");
            if (second == null)
                throw new ArgumentNullException("second");
            this.sets = new T[first.Length + second.Length][];
            first.CopyTo(sets, 0);
            second.CopyTo(sets, first.Length);
        }

        public ConcatenatedArrayReadOnlyCollection(T[][] first, T[] second)
        {
            if (first == null)
                throw new ArgumentNullException("first");
            if (second == null)
                throw new ArgumentNullException("second");
            this.sets = new T[first.Length + 1][];
            first.CopyTo(this.sets, 0);
            this.sets[first.Length] = second;
        }

        public ConcatenatedArrayReadOnlyCollection(T[] first, T[][] second)
            : this(second, first)
        {
        }

        #region IArrayReadOnlyCollection<T> Members

        public T[] InternalArray
        {
            get { throw new NotSupportedException(); }
        }

        public bool IsAggregate
        {
            get { return true; }
        }

        public T[][] AggregateArrays
        {
            get { return this.sets; }
        }

        #endregion

        #region IControlledCollection<T> Members

        public int Count
        {
            get
            {
                int result = 0;
                for (int i = 0; i < this.sets.Length; i++)
                    result += this.sets[i].Length;
                return result;
            }
        }

        public bool Contains(T item)
        {
            for (int currentSet = 0; currentSet < this.sets.Length; currentSet++)
            {
                var current = this.sets[currentSet];
                if (current.Contains(item))
                    return true;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex = 0)
        {
            for (int currentSet = 0, offset = 0; currentSet < this.sets.Length; offset += this.sets[currentSet++].Length)
                this.sets[currentSet].CopyTo(array, offset);
        }

        public T this[int index]
        {
            get
            {
                for (int currentSet = 0, offset = 0, currentLength; currentSet < this.sets.Length; offset += this.sets[currentSet++].Length)
                {
                    currentLength = this.sets[currentSet].Length;
                    if (offset >= index && index < offset + currentLength)
                        return this.sets[currentSet][index - offset];
                }
                throw new IndexOutOfRangeException();
            }
        }

        public T[] ToArray()
        {
            T[] result = new T[this.Count];
            this.CopyTo(result);
            return result;
        }

        public int IndexOf(T element)
        {
            for (int currentSet = 0, offset = 0; currentSet < this.sets.Length; offset += this.sets[currentSet++].Length)
            {
                int index = Array.IndexOf<T>(this.sets[currentSet], element);
                if (index != -1)
                    return index;
            }
            return -1;
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            for (int currentSetIndex = 0; currentSetIndex < this.sets.Length; currentSetIndex++)
            {
                var currentSet = this.sets[currentSetIndex];
                for (int currentIndex = 0; currentIndex < currentSet.Length; currentIndex++)
                    yield return currentSet[currentIndex];
            }
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
