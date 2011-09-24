using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a locked type collection.
    /// </summary>
    public class LockedTypeCollection :
        ILockedTypeCollection,
        ITypeCollection,
        IEquatable<ITypeCollectionBase>
    {
        /// <summary>
        /// An empty series of types that is locked.
        /// </summary>
        public static readonly ILockedTypeCollection Empty = new LockedTypeCollection();
        internal List<IType> copy;

        internal LockedTypeCollection() { this.copy = new List<IType>(); }
        internal LockedTypeCollection(params IType[] source)
            : this((IEnumerable<IType>)source) { }
        /// <summary>
        /// Creates a new <see cref="LockedTypeCollection"/> with the <paramref name="source"/>
        /// provided.
        /// </summary>
        /// <param name="source">The source <see cref="ITypeCollection"/> which contains the 
        /// <see cref="IType"/> instances to lock.</param>
        public LockedTypeCollection(ITypeCollectionBase source)
            : this((IEnumerable<IType>)source)
        {
        }

        public LockedTypeCollection(IEnumerable<IType> sourceA, params IType[] sourceB)
        {
            this.copy = new List<IType>(sourceA);
            this.copy.AddRange(sourceB);
        }

        public LockedTypeCollection(IEnumerable<IType> source)
        {
            this.copy = new List<IType>(source);
        }

        #region ITypeCollection Members

        /// <summary>
        /// Inserts and returns the <see cref="IType"/> instances translated from <paramref name="types"/>.
        /// </summary>
        /// <param name="types">The <see cref="Type"/> array to insert as a series.</param>
        /// <returns>A <see cref="IType"/> array of the entries added.</returns>
        /// <exception cref="NotSupportedException">Type Collection locked.</exception>
        public IType[] AddRange(Type[] types)
        {
            throw new NotSupportedException("Type Collection locked.");
        }

        /// <summary>
        /// Inserts and returns the <see cref="IType"/> translated from the <paramref name="type"/>
        /// provided.
        /// </summary>
        /// <param name="type">A <see cref="System.Type"/> to insert and return as a <see cref="IType"/>.</param>
        /// <returns>A <see cref="IType"/> instance that was inserted in place of the <paramref name="type"/>.</returns>
        public IType Add(Type type)
        {
            throw new NotSupportedException("Type Collection locked.");
        }

        /// <summary>
        /// Inserts a series of <see cref="IType"/> through <paramref name="types"/>.
        /// </summary>
        /// <param name="types">The <see cref="IType"/> array of members to add.</param>
        /// <exception cref="NotSupportedException">Type Collection locked.</exception>
        public void AddRange(IType[] types)
        {
            throw new NotSupportedException("Type Collection locked.");
        }
        /// <summary>
        /// Inserts a series of <see cref="IType"/> through <paramref name="types"/>.
        /// </summary>
        /// <param name="types">The <see cref="IType"/> series to add.</param>
        /// <exception cref="NotSupportedException">Type Collection locked.</exception>
        public void AddRange(IEnumerable<IType> types)
        {
            throw new NotSupportedException("Type Collection locked.");
        }

        public IType[] ToArray()
        {
            if (this.IsDisposed)
                return new IType[0];
            //lock (this.copy)
            return this.copy.ToArray();
        }

        #endregion

        #region IList<IType> Members

        public int IndexOf(IType item)
        {
            return this.copy.IndexOf(item);
        }

        #endregion

        #region ICollection<IType> Members

        internal void _Clear(){
            copy.Clear();
        }

        internal void _AddRange(IType[] series)
        {
            copy.AddRange(series);
        }

        internal void _Add(IType type)
        {
            copy.Add(type);
        }

        public bool Contains(IType item)
        {
            return this.copy.Contains(item);
        }

        public void CopyTo(IType[] array, int arrayIndex = 0)
        {
            this.copy.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get {
                if (this.copy == null)
                    return 0;
                return this.copy.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        #endregion

        #region IEnumerable<IType> Members

        public IEnumerator<IType> GetEnumerator()
        {
            if (this.copy == null)
                yield break;
            foreach (var element in this.copy)
                yield return element;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region ITypeCollection Members

        public IType this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                return this.copy[index];
            }
            set
            {
                throw new  NotSupportedException();
            }
        }

        public void Insert(int index, IType item)
        {
            throw new NotSupportedException();
        }

        public void RemoveRange(int index, int count)
        {
            throw new NotSupportedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        public void Add(IType item)
        {
            throw new NotSupportedException();
        }

        public bool Remove(IType item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this.IsDisposed)
                return;
            lock (this.copy)
            {
                this.copy.Clear();
                this.copy = null;
            }
        }

        #endregion

        public bool IsDisposed
        {
            get
            {
                return this.copy == null;
            }
        }


        #region IEquatable<ITypeCollectionBase> Members

        public bool Equals(ITypeCollectionBase other)
        {
            if (other == null)
                return false;
            if (object.ReferenceEquals(other, this))
                return true;
            if (other.Count != this.Count)
                return false;
            return this.SequenceEqual(other);
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is ITypeCollectionBase)
                return this.Equals((ITypeCollectionBase)(obj));
            return false;
        }

        public override int GetHashCode()
        {
            return this.Count.GetHashCode();
        }
    }
}
