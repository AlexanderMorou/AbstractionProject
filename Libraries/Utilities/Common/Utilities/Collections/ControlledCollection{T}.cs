using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    /// <summary>
    /// A generic collection which is tightly controlled.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="ControlledCollection{T}"/></typeparam>
    public class ControlledCollection<T> :
        IControlledCollection<T>,
        IControlledCollection
    {
        /// <summary>
        /// The list to wrap.
        /// </summary>
        internal protected IList<T> baseList;

        #region ControlledCollection Constructors

        /// <summary>
        /// Creates a new instance of <see cref="ControlledCollection{T}"/> with the 
        /// <paramref name="baseList"/> to wrap provided.
        /// </summary>
        /// <param name="baseList">The list to wrap.</param>
        public ControlledCollection(IList<T> baseList)
        {
            this.baseList = baseList;
        }

        /// <summary>
        /// Creates a new <see cref="ControlledCollection{T}"/> with a default state.
        /// </summary>
        public ControlledCollection()
            : this(new List<T>())
        {

        }

        #endregion

        #region IControlledCollection<T> Members
        /// <summary>
        /// Gets the number of elements contained in the <see cref="ControlledCollection{T}"/>.
        /// </summary>
        ///
        /// <returns>
        /// The number of elements contained in the <see cref="ControlledCollection{T}"/>.
        /// </returns>
        public virtual int Count
        {
            get
            {
                lock (this.baseList)
                    return this.baseList.Count;
            }
        }

        /// <summary>
        /// Determines whether the <see cref="ControlledCollection{T}"/> contains a specific 
        /// value.</summary>
        /// <param name="item">
        /// The object to locate in the <see cref="ControlledCollection{T}"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="ControlledCollection{T}"/>;
        /// otherwise, false.
        /// </returns>
        public virtual bool Contains(T item)
        {
            lock (this.baseList)
                return baseList.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the <see cref="ControlledCollection{T}"/> to an
        /// <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> 
        /// index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="System.Array"/> that is the destination of the 
        /// elements copied from <see cref="ControlledCollection{T}"/>. The 
        /// <see cref="System.Array"/> must
        /// have zero-based indexing.</param>
        /// <param name="arrayIndex">
        /// The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="arrayIndex"/> is less than 0.</exception>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="array"/> is null.</exception>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="array"/> is multidimensional.-or-<paramref name="arrayIndex"/> 
        /// is equal to or greater than the length of <paramref name="array"/>.-or-The 
        /// number of elements in the source <see cref="ControlledCollection{T}"/> is greater 
        /// than the available space from <paramref name="arrayIndex"/> to the 
        /// end of the destination <paramref name="array"/>.-or-Type <typeparamref name="T"/> 
        /// cannot be cast automatically to the type of the destination
        /// <paramref name="array"/>.</exception>
        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            lock (this.baseList)
                this.baseList.CopyTo(array, arrayIndex);
        }

        public int IndexOf(T element)
        {
            lock (this.baseList)
                return baseList.IndexOf(element);
        }
        #endregion

        #region IEnumerable<T> Members

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="ControlledCollection{T}"/>.
        /// </summary>
        /// <returns>A <see cref="IEnumerator{T}"/> that can be used to iterate through
        /// the <see cref="ControlledCollection{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return OnGetEnumerator();
        }

        protected virtual IEnumerator<T> OnGetEnumerator()
        {
            return this.baseList.GetEnumerator();
        }

        #endregion

        #region IControlledCollection Members
        bool IControlledCollection.Contains(object item)
        {
            if (!(item is T))
                return false;
            return this.Contains((T)item);
        }
        object IControlledCollection.this[int index]
        {
            get
            {
                return this[index];
            }
        }
        void IControlledCollection.CopyTo(Array array, int index)
        {
            this.CopyTo((T[])array, index);
        }


        int IControlledCollection.IndexOf(object element)
        {
            if (element is T)
                return this.IndexOf((T)element);
            return -1;
        }
        #endregion

        /// <summary>
        /// Translates the <see cref="ControlledCollection{T}"/> into a flat <see cref="System.Array"/>
        /// of <typeparamref name="T"/> elements.
        /// </summary>
        /// <returns>A new <see cref="System.Array"/> of <typeparamref name="T"/> instances.</returns>
        public virtual T[] ToArray()
        {
            T[] r = new T[this.Count];
            this.CopyTo(r, 0);
            return r;
        }
        
        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns the element at the index provided
        /// </summary>
        /// <param name="index">The index of the member to find.</param>
        /// <returns>The instance of <typeparamref name="T"/> at the index provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is  beyond the range of the 
        /// <see cref="ControlledCollection{T}"/>.
        /// </exception>
        public virtual T this[int index]
        {
            get
            {
                return this.baseList[index];
            }
        }
        #endregion

        protected virtual void AddImpl(T expression)
        {
            lock (baseList)
                this.baseList.Add(expression);
        }

        protected virtual void InsertItem(int index, T item)
        {
            lock (baseList)
                this.baseList.Insert(index, item);
        }


        internal void AddRange(T[] target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            lock (baseList)
            {
                for (int i = 0; i < target.Length; i++)
                    this.baseList.Add(target[i]);
            }
        }
    }
}
