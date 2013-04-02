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
    internal class SimpleCollection<T> :
        ICollection
    {
        private ICollection<T> items;
        public SimpleCollection(ICollection<T> items)
        {
            this.items = items;
        }
        #region ICollection Members

        void ICollection.CopyTo(Array array, int index)
        {
            items.CopyTo((T[])array, index);
        }

        int ICollection.Count
        {
            get { return this.items.Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get { return null; }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (T t in this.items)
                yield return t;
            yield break;
        }

        #endregion
    }
}
