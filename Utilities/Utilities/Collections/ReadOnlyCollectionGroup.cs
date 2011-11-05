using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    public class ReadOnlyCollectionGroup<TCollection, TItem> : 
        IReadOnlyCollection<TItem>
        where TCollection :
            IControlledStateCollection
    {
        private IControlledStateCollection[] collections;
        public ReadOnlyCollectionGroup(IControlledStateCollection[] collections)
        {
            if (collections == null)
                throw new ArgumentNullException("collections");
            for (int i = 0; i < collections.Length; i++)
                if (collections[i] == null)
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.collections, ArgumentExceptionMessage.MemberOfSeriesNull, ThrowHelper.GetArgumentName(ArgumentWithException.collections));
            this.collections = (IControlledStateCollection[])collections.Clone();
        }
        #region IControlledStateCollection<TItem> Members

        /// <summary>
        /// Returns the number of elements within the 
        /// <see cref="ReadOnlyCollectionGroup{TCollection, TItem}"/>.
        /// </summary>
        public int Count
        {
            get {
                int result = 0;
                for (int i = 0; i < collections.Length; i++)
                    result += collections[i].Count;
                return result;
            }
        }

        public bool Contains(TItem item)
        {
            foreach (IControlledStateCollection col in this.collections)
                if (col.Contains(item))
                    return true;
            return false;
        }

        public void CopyTo(TItem[] array, int arrayIndex = 0)
        {
            IControlledStateCollection current = this.collections[0];
            for (int i = 0, offset = 0, len = this.collections.Length; i < this.collections.Length; current = (i + 1) < len ? this.collections[++i] : null, offset += current.Count)
            {
                if (offset + current.Count > array.Length)
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.array, ArgumentExceptionMessage.InsufficientSpaceForCopy, ThrowHelper.GetArgumentName(ArgumentWithException.array));
                current.CopyTo(array, offset);
            }
        }

        public TItem this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                for (int i = 0, rangeStart = 0,
                        rangeEnd = (this.collections.Length > 0) ?
                            this.collections[0].Count : 0;
                        i < this.collections.Length;
                        rangeStart = rangeEnd, i++, rangeEnd += (i < this.collections.Length) ? this.collections[i].Count : 0)
                {
                    if (index >= rangeStart && index < rangeEnd)
                        return (TItem)collections[i][index - rangeStart];
                }
                return default(TItem);
            }
        }

        public int IndexOf(TItem element)
        {
            for (int i = 0, offset = 0; i < this.collections.Length; offset += this.collections[i++].Count)
            {
                int index = this.collections[i].IndexOf(element);
                if (index == -1)
                    continue;
                return offset + index;
            }
            return -1;
        }
        #endregion

        #region IEnumerable<TItem> Members

        public IEnumerator<TItem> GetEnumerator()
        {
            foreach (IControlledStateCollection col in this.collections)
                foreach (TItem item in col)
                    yield return item;
            yield break;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        public TItem[] ToArray()
        {
            TItem[] result = new TItem[this.Count];
            this.CopyTo(result);
            return result;
        }

    }
}
