using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    /// <summary>
    /// Represents a strongly typed series of <typeparamref name="T"/>
    /// instances that can be accessed via index, and the series
    /// as a whole contains a unique hash for the entire series.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    #if CODE_ANALYSIS
    /* *
     * Added suppression for Correct Suffix error; this list intends to act 
     * as a list; however, the lack of versioning in lists made it impossible
     * to tell when it has changed.
     * */
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    #endif
    [DebuggerVisualizer("{StringForm}")]
    public class HashList<T> :
        IList<T>,
        IEquatable<HashList<T>>
    {
        /// <summary>
        /// Data member which determines the current hash of the 
        /// <see cref="HashList{T}"/>.
        /// </summary>
        private int currentHash = 0;
        /// <summary>
        /// Determines whether the hash is out of date.
        /// </summary>
        private bool needsRehashed = false;
        /// <summary>
        /// Data member which represents the internal list 
        /// implementation.
        /// </summary>
        private List<T> coreList;

        /// <summary>
        /// Initializes a new <see cref="HashList{T}"/>
        /// initialized to default capacity.
        /// </summary>
        public HashList()
        {
            needsRehashed = true;
            this.coreList = new List<T>();
        }

        /// <summary>
        /// Initializes a new <see cref="HashList{T}"/>
        /// which contains the <paramref name="series"/>
        /// of <typeparamref name="T"/> elements 
        /// provided.
        /// </summary>
        /// <param name="series"></param>
        public HashList(IEnumerable<T> series)
        {
            needsRehashed = true;
            this.coreList = new List<T>(series);
        }

        /// <summary>
        /// Returns whether or not the <paramref name="obj"/>
        /// provided equals the current <see cref="HashList{T}"/>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/>
        /// to compare to.</param>
        /// <returns>True if <paramref name="obj"/> equals
        /// the current <see cref="HashList{T}"/>.</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as HashList<T>);
        }

        /// <summary>
        /// Obtains a hash code for the current <see cref="HashList{T}"/>
        /// which merges the hashes of the elements contained
        /// within the series stressing total length and 
        /// member order.
        /// </summary>
        /// <returns>A hash representing the current 
        /// <see cref="HashList{T}"/>.</returns>
        public override int GetHashCode()
        {
            if (this.needsRehashed)
            {
                this.currentHash = this.CreateHash();
                this.needsRehashed = false;
            }
            return this.currentHash;
        }

        private int CreateHash()
        {
            int count = this.Count;
            int startHash = count.GetHashCode() ^ 34502201,
                hash = 785532;
            for (int i = 0; i < count; i++)
            {
                switch (i)
                {
                    case 0:
                        hash += this[i].GetHashCode() ^ 4000222;
                        break;
                    case 1:
                    case 9:
                        hash += this[i].GetHashCode() ^ 9332;
                        break;
                    case 3:
                    case 5:
                        hash += this[i].GetHashCode() ^ 288112;
                        break;
                    case 4:
                    case 17:
                    case 33:
                        hash += this[i].GetHashCode() ^ 9933;
                        break;
                    case 11:
                        hash += this[i].GetHashCode() ^ 31127;
                        break;
                    case 6:
                        hash += this[i].GetHashCode() ^ 922;
                        break;
                    case 7:
                        hash += this[i].GetHashCode() ^ 229332;
                        break;
                    case 10:
                        hash += this[i].GetHashCode() ^ 883322;
                        break;
                    default:
                        if (i % 5 == 0)
                        {
                            hash += this[i].GetHashCode() * i;
                        }
                        else if (i % 4 == 0)
                        {
                            hash *= this[i].GetHashCode() ^ i;
                        }
                        else if (i % 3 == 0)
                        {
                            hash ^= this[i].GetHashCode() * i;
                        }
                        else if (i % 2 == 0)
                        {
                            hash |= this[i].GetHashCode() ^ i;
                        }
                        else
                            hash += this[i].GetHashCode() ^ i;
                        break;
                }
            }
            return hash ^ startHash;
        }

        #region IEquatable<HashList<T>> Members

        /// <summary>
        /// Returns whether the <paramref name="other"/>
        /// <see cref="HashList{T}"/> equals the current 
        /// instance.
        /// </summary>
        /// <param name="other">The <see cref="HashList"/>
        /// to compare to.</param>
        /// <returns>true if the <paramref name="other"/>
        /// <see cref="HashList{T}"/> equals the 
        /// current instance.</returns>
        public bool Equals(HashList<T> other)
        {
            if (other == null)
                return false;
            if (other.Count != this.Count)
                return false;
            for (int i = 0; i < this.Count; i++)
                if (!other[i].Equals(this[i]))
                    return false;
            return true;
        }

        #endregion

        /// <summary>
        /// Converts the current instance into a <see cref="String"/>
        /// representation.
        /// </summary>
        /// <returns>A <see cref="String"/> instance
        /// representing the current <see cref="HashList{T}"/>.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            bool first = true;
            sb.Append(typeof(T).ToString());
            sb.Append("[");
            sb.Append(this.Count.ToString(CultureInfo.InvariantCulture));
            sb.Append("] {");
            foreach (var t in this)
            {
                if (first)
                    first = false;
                else
                    sb.Append(", ");
                sb.Append(t.ToString());
            }
            sb.Append("}");
            return sb.ToString();
        }
        private string StringForm
        {
            get
            {
                return this.ToString();
            }
        }

        #region IList<T> Members
        /// <summary>
        /// Searches for the specified <paramref name="item"/> and 
        /// returns the zero-based index of the first occurrence 
        /// within the entire <see cref="HashList{T}"/>.
        /// </summary>
        /// <param name="item">The <typeparamref name="T"/>
        /// to locate in the <see cref="HashList{T}"/>. The value
        /// can be null for reference types.</param>
        /// <returns>The zero-based index of the first occurrence 
        /// of item within the entire <see cref="HashList{T}"/>,
        /// if found; otherwise, –1.
        /// </returns>
        public int IndexOf(T item)
        {
            return this.coreList.IndexOf(item);
        }

        /// <summary>
        /// Inserts an <paramref name="item"/> into the 
        /// <see cref="HashList{T}"/> at the specified 
        /// <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based index at which 
        /// <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The <typeparamref name="T"/> 
        /// instance to insert. The value can be null for 
        /// reference types.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than zero-or-
        /// greater than <paramref name="Count"/>
        /// </exception>
        public void Insert(int index, T item)
        {
            this.coreList.Insert(index, item);
            this.needsRehashed = true;
        }

        /// <summary>
        /// Removes the element at the specified 
        /// <paramref name="index"/> of the 
        /// <see cref="HashList{T}"/>.
        /// </summary>
        /// <param name="index">The zero-based index of 
        /// the <typeparamref name="T"/> instance to 
        /// remove.</param>
        public void RemoveAt(int index)
        {
            this.coreList.RemoveAt(index);
            this.needsRehashed = true;
        }

        /// <summary>
        /// Gets or sets the element at the specified 
        /// <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the 
        /// <typeparamref name="T"/> element to get or set.
        /// </param>
        /// <returns>The element at the specified 
        /// <typeparamref name="index"/>.</returns>
        public T this[int index]
        {
            get
            {
                return this.coreList[index];
            }
            set
            {
                this.coreList[index] = value;
                this.needsRehashed = true;
            }
        }

        #endregion

        #region ICollection<T> Members
        /// <summary>
        /// Adds a <typeparamref name="T"/> instance
        /// to the end of the <see cref="HashList{T}"/>.
        /// </summary>
        /// <param name="item">The <typeparamref name="T"/> 
        /// instance to be added to the end of the 
        /// <see cref="HashList{T}"/>.</param>
        public void Add(T item)
        {
            this.coreList.Add(item);
            this.needsRehashed = true;
        }

        /// <summary>
        /// Removes all <typeparamref name="T"/> instances 
        /// from the <see cref="HashList{T}"/>.
        /// </summary>
        public void Clear()
        {
            this.coreList.Clear();
            this.needsRehashed = true;
        }

        /// <summary>
        /// Determines whether the <typeparamref name="T"/>
        /// instance, <paramref name="item"/>, is in the 
        /// <see cref="HashList{T}"/>.
        /// </summary>
        /// <param name="item">The <typeparamref name="T"/> 
        /// instance to locate in the <see cref="HashList{T}"/>. 
        /// The value can be null for reference types.</param>
        /// <returns>true if item is found in the 
        /// <see cref="HashList{T}"/>; otherwise,
        /// false.</returns>
        public bool Contains(T item)
        {
            return this.coreList.Contains(item);
        }

        /// <summary>
        /// Copies the entire <see cref="HashList{T}"/> 
        /// to a compatible one-dimensional
        /// array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional 
        /// <see cref="System.Array"/> that is the destination 
        /// of the <typeparamref name="T"/> instances copied from 
        /// <see cref="HashList{T}"/>. The 
        /// <see cref="System.Array"/> must have zero-based 
        /// indexing.</param>
        /// <param name="arrayIndex">The zero-based index in 
        /// <paramref name="array"/> at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.coreList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of elements actually contained
        /// in the <see cref="HashList{T}"/>.
        /// </summary>
        /// <returns>The number of elements actually contained
        /// in the <see cref="HashList{T}"/>.</returns>
        public int Count
        {
            get { return this.coreList.Count; }
        }

        #if CODE_ANALYSIS
        /* *
         * Added suppress message for Explicit declaration not invokable by
         * child types.
         * *
         * Reason: This is purely a redirection towards the coreList.
         * */
        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        #endif
        bool ICollection<T>.IsReadOnly
        {
            get { return ((ICollection<T>)(this.coreList)).IsReadOnly; }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object 
        /// from the <see cref="HashList{T}"/>
        /// </summary>
        /// <param name="item">The <typeparamref name="T"/>
        /// instance to remove from the <see cref="HashList"/>. 
        /// The value can be null for reference types.</param>
        /// <returns>true if item is successfully removed; otherwise,
        /// false. This method also returns false if item was not found 
        /// in the <see cref="HashList{T}"/>.</returns>
        public bool Remove(T item)
        {
            bool removed = false;
            removed = this.coreList.Remove(item);
            this.needsRehashed = removed;
            return removed;
        }

        #endregion

        #region IEnumerable<T> Members

        /// <summary>
        /// Returns an enumerator that iterates through the 
        /// <see cref="HashList{T}"/>.
        /// </summary>
        /// <returns>A <see cref="List{T}.Enumerator"/> 
        /// for the <see cref="HashList{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.coreList.GetEnumerator();
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
