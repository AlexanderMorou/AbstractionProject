﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    internal class ArrayReadOnlyCollection<T> :
        IArrayReadOnlyCollection<T>,
        IControlledCollection
    {
        private T[] items;

        internal static readonly ArrayReadOnlyCollection<T> Empty = new ArrayReadOnlyCollection<T>();

        private ArrayReadOnlyCollection() : this(new T[0]) { }

        internal T[] Items { get { return this.items; } }

        internal ArrayReadOnlyCollection(T[] items)
        {
            this.items = items;
        }

        #region IControlledCollection<T> Members

        public int Count
        {
            get { return this.items.Length; }
        }

        public bool Contains(T item)
        {
            return this.items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex = 0)
        {
            this.items.CopyTo(array, arrayIndex);
        }

        public T this[int index]
        {
            get { return this.items[index]; }
        }

        public T[] ToArray()
        {
            return (T[])this.items.Clone();
        }

        public int IndexOf(T element)
        {
            return Array.IndexOf<T>(items, element);
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in this.items)
                yield return item;
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
            return Array.IndexOf(this.items, item) != -1;
        }

        void IControlledCollection.CopyTo(Array array, int arrayIndex)
        {
            Array.ConstrainedCopy(this.items, 0, array, arrayIndex, this.items.Length);
        }

        object IControlledCollection.this[int index]
        {
            get { return this[index]; }
        }

        public int IndexOf(object element)
        {
            return Array.IndexOf(this.items, element);
        }

        #endregion

        #region IArrayReadOnlyCollection<T> Members

        public T[] InternalArray
        {
            get { return this.items; }
        }

        public bool IsAggregate
        {
            get { return false; }
        }

        public T[][] AggregateArrays
        {
            get { return new T[1][] { this.items }; }
        }

        #endregion
    }
}
