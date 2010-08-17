using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast
{
    /* *
     * ToDo: Update Intermediate Implemented Interfaces so that it 
     * merges the current level of elements with the parent of the 
     * type that declares it.
     * */
    internal class IntermediateImplementedInterfaces :
        ILockedTypeCollection
    {
        private ITypeCollection source;
        public IntermediateImplementedInterfaces(ITypeCollection source)
        {
            this.source = source;
        }

        #region ITypeCollectionBase Members

        public int IndexOf(IType item)
        {
            return this.source.IndexOf(item);
        }

        #endregion

        #region IControlledStateCollection<IType> Members

        public int Count
        {
            get { return this.source.Count; }
        }

        public bool Contains(IType item)
        {
            return this.source.Contains(item);
        }

        public void CopyTo(IType[] array, int arrayIndex)
        {
            this.source.CopyTo(array, arrayIndex);
        }

        public IType this[int index]
        {
            get { return this.source[index]; }
        }

        public IType[] ToArray()
        {
            return this.source.ToArray();
        }

        #endregion

        #region IEnumerable<IType> Members

        public IEnumerator<IType> GetEnumerator()
        {
            return this.source.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.source = null;
        }

        #endregion
    }
}
