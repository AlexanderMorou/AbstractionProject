using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    internal class CovariantReadOnlyCollection<TLower, THigher> :
        IControlledCollection<TLower>
        where THigher :
            TLower
    {
        private IControlledCollection<THigher> covariantSource;

        public CovariantReadOnlyCollection(IControlledCollection<THigher> covariantSource)
        {
            if (covariantSource == null)
                throw new ArgumentNullException("covariantSource");
            this.covariantSource = covariantSource;
        }

        #region IControlledCollection<TLower> Members

        public int Count
        {
            get { return this.covariantSource.Count; }
        }

        public bool Contains(TLower item)
        {
            if (item is THigher)
                return this.covariantSource.Contains((THigher)item);
            return false;
        }

        public void CopyTo(TLower[] array, int arrayIndex = 0)
        {
            this.ToArray().CopyTo(array, arrayIndex);
        }

        public TLower this[int index]
        {
            get { return this.covariantSource[index]; }
        }

        public TLower[] ToArray()
        {
            var result = new TLower[this.Count];
            var higherCopy = this.covariantSource.ToArray();
            for (int i = 0; i < this.Count; i++)
                result[i] = higherCopy[i];
            return result;
        }

        public int IndexOf(TLower element)
        {
            if (element is THigher)
                return this.covariantSource.IndexOf((THigher)element);
            return -1;
        }

        #endregion

        #region IEnumerable<TLower> Members

        public IEnumerator<TLower> GetEnumerator()
        {
            foreach (var element in this.covariantSource)
                yield return element;
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
