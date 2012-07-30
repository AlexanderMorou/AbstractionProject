using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables
{
    class CliMetadataLazySequentialSet<T> :
        IReadOnlyCollection<T>,
        IReadOnlyCollection
        where T :
            ICliMetadataIndexedRow
    {
        private uint startingIndex;
        private int count;
        private IControlledCollection<T> owningTable;

        /// <summary>
        /// Creates a new <see cref="CliMetadataLazySet{T}"/> with the 
        /// <paramref name="indices"/> provided.
        /// </summary>
        /// <param name="indices">The <see cref="UInt32"/> array</param>
        public CliMetadataLazySequentialSet(uint initialIndex, uint followIndex, IControlledCollection<T> owningTable)
        {
            this.startingIndex = initialIndex;
            this.count = (int) (followIndex - initialIndex);
            this.owningTable = owningTable;
        }
        #region IControlledCollection<T> Members

        public int Count
        {
            get { return this.count; }
        }

        public bool Contains(T item)
        {
            return item.Index >= this.startingIndex &&
                   item.Index < (this.startingIndex + this.count);
        }

        public void CopyTo(T[] array, int arrayIndex = 0)
        {
            ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
            for (int index = 0; index < this.Count; index++)
                array[arrayIndex + index] = this.owningTable[(int)(this.startingIndex + index)];
        }

        public T this[int index]
        {
            get { return this.owningTable[(int)(this.startingIndex + index)]; }
        }

        public T[] ToArray()
        {
            var result = new T[this.Count];
            this.CopyTo(result);
            return result;
        }

        public int IndexOf(T element)
        {
            uint index = element.Index;
            if (element.Index < this.startingIndex ||
                element.Index >= (this.startingIndex + this.Count))
                throw new ArgumentException("Element not present in set.");
            return (int) (index - this.startingIndex);
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            for (int index = 0; index < this.Count; index++)
                yield return this.owningTable[(int) (this.startingIndex + index)];
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IControlledCollection Members

        bool IControlledCollection.Contains(object item)
        {
            if (item is T)
                return this.Contains((T) item);
            return false;
        }

        void IControlledCollection.CopyTo(Array array, int arrayIndex)
        {
            if (array is T[])
                this.CopyTo((T[]) array, arrayIndex);
            else
            {
                ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
                for (int index = 0; index < this.Count; index++)
                    array.SetValue(this.owningTable[(int) (this.startingIndex + index)], arrayIndex + index);
            }
        }

        object IControlledCollection.this[int index]
        {
            get { return this[index]; }
        }

        int IControlledCollection.IndexOf(object element)
        {
            if (element is T)
                return this.IndexOf((T) element);
            return -1;
        }

        #endregion
    }
}