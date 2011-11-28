using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;
/*----------------------------------------\
| Copyright © 2012 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a simple wrapper for simplifying passing multiple typed name series.
    /// </summary>
    public struct TypedNameSeries :
        IControlledCollection<TypedName>
    {
        /// <summary>
        /// An empty typed name series that cannot be modified.
        /// </summary>
        public static readonly TypedNameSeries Empty = new TypedNameSeries(true);

        private bool frozen;
        /// <summary>
        /// Data member for <see cref="Data"/>.
        /// </summary>
        private TypedName[] data;
        /// <summary>
        /// The actual number of items in the <see cref="TypedNameSeries"/>.
        /// </summary>
        private int actualLength;

        /// <summary>
        /// Creates a new <see cref="TypedNameSeries"/> with the 
        /// <paramref name="data"/> provided.
        /// </summary>
        /// <param name="data">The <see cref="TypedName"/> array that the
        /// <see cref="TypedNameSeries"/> represents.</param>
        public TypedNameSeries(params TypedName[] data)
            : this()
        {
            if (data == null)
                throw new ArgumentNullException("data");
            /* *
             * Ensure the typedname is legitimate.
             * */
            this.data = new TypedName[data.Length];
            for (int i = 0; i < data.Length; i++)
                if (data[i].Source == TypedNameSource.InvalidReference)
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.data, ExceptionMessageId.TypedName_InvalidElement, ThrowHelper.GetArgumentName(ArgumentWithException.data));
                else
                    this.data[i] = data[i];
            actualLength = data.Length;
            frozen = false;
        }

        private TypedNameSeries(bool frozen)
        {
            this.frozen = true;
            this.actualLength = 0;
            this.data = new TypedName[0];
        }

        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> for iterating
        /// the data represented by the <see cref="TypedNameSeries"/>.
        /// </summary>
        public IEnumerable<TypedName> Data
        {
            get
            {

                if (this.data == null)
                    this.data = Empty.data;
                for (int i = 0; i < this.actualLength; i++)
                    yield return this.data[i];
                yield break;
            }
        }

        /// <summary>
        /// Adds a <see cref="TypedName"/> to the
        /// <see cref="TypedNameSeries"/> with the
        /// <paramref name="name"/> and <paramref name="type"/>
        /// provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the typed name.</param>
        /// <param name="type">The <see cref="IType"/> of the typed name.</param>
        public void Add(string name, IType type)
        {
            if (this.frozen)
                throw new InvalidOperationException("Immutable.");
            if (this.data == null)
                this.data = new TypedName[1];
            this.data = this.data.EnsureSpaceExists(this.actualLength, 1);
            this.data[this.actualLength++] = new TypedName(name, type);
        }

        /// <summary>
        /// Adds a <see cref="TypedName"/> to the
        /// <see cref="TypedNameSeries"/> with the
        /// <paramref name="name"/> and <paramref name="symbolType"/>
        /// provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the typed name.</param>
        /// <param name="symbolType">The <see cref="String"/> representing
        /// the symbol type to be resolved later.</param>
        public void Add(string name, string symbolType)
        {
            if (this.frozen)
                throw new InvalidOperationException("Immutable.");
            if (this.data == null)
                this.data = new TypedName[1];
            this.data = this.data.EnsureSpaceExists(this.actualLength, 1);
            this.data[this.actualLength++] = new TypedName(name, symbolType);
        }

        /// <summary>
        /// Adds a <see cref="TypedName"/> to the 
        /// <see cref="TypedNameSeries"/> with the
        /// <paramref name="name"/>, <paramref name="type"/> and 
        /// <paramref name="direction"/> provided.
        /// </summary>
        /// <param name="name">The name of the type name pair.</param>
        /// <param name="type">The <see cref="IType"/> of the type name pair.</param>
        /// <param name="direction">The direction for the type name pair, 
        /// if used for a parameter.</param>
        public void Add(string name, IType type, ParameterDirection direction)
        {
            if (this.frozen)
                throw new InvalidOperationException("Immutable.");
            if (this.data == null)
                this.data = new TypedName[1];
            this.data = this.data.EnsureSpaceExists(this.actualLength, 1);
            this.data[this.actualLength++] = new TypedName(name, type, direction);
        }

        /// <summary>
        /// Adds a <see cref="TypedName"/> to the 
        /// <see cref="TypedNameSeries"/> with the
        /// <paramref name="name"/>, <paramref name="symbolType"/> and 
        /// <paramref name="direction"/> provided.
        /// </summary>
        /// <param name="name">The name of the type name pair.</param>
        /// <param name="symbolType">The <see cref="String"/> representing
        /// the symbol type to be resolved later.</param>
        /// <param name="direction">The direction for the type name pair, 
        /// if used for a parameter.</param>
        public void Add(string name, string symbolType, ParameterDirection direction)
        {
            if (this.frozen)
                throw new InvalidOperationException("Immutable.");
            if (this.data == null)
                this.data = new TypedName[1];
            this.data = this.data.EnsureSpaceExists(this.actualLength, 1);
            this.data[this.actualLength++] = new TypedName(name, symbolType, direction);
        }

        /// <summary>
        /// Adds a <see cref="TypedName"/> to the <see cref="TypedNameSeries"/>.
        /// </summary>
        /// <param name="value">The <see cref="TypedName"/> to add to the
        /// <see cref="TypedNameSeries"/>.</param>
        public void Add(TypedName value)
        {
            if (this.frozen)
                throw new InvalidOperationException("Immutable.");
            if (this.data == null)
                this.data = new TypedName[1];
            if (value.Source == TypedNameSource.InvalidReference)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.value, ExceptionMessageId.TypedName_Invalid, ThrowHelper.GetArgumentName(ArgumentWithException.value));
            this.data = this.data.EnsureSpaceExists(this.actualLength, 1);
            this.data[this.actualLength++] = value;
        }

        /// <summary>
        /// Adds a series of <see cref="TypedName"/> instances
        /// to the <see cref="TypedNameSeries"/>.
        /// </summary>
        /// <param name="data">The <see cref="TypedName"/> to add.</param>
        public void AddRange(params TypedName[] data)
        {
            if (this.frozen)
                throw new InvalidOperationException("Immutable.");
            if (data == null)
                throw new ArgumentNullException("data");
            if (this.data == null)
                this.data = new TypedName[data.Length];
            this.data = this.data.EnsureSpaceExists(this.actualLength, data.Length);
            for (int i = 0; i < data.Length; i++)
                if (data[i].Source == TypedNameSource.InvalidReference)
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.data, ExceptionMessageId.TypedName_InvalidElement, ThrowHelper.GetArgumentName(ArgumentWithException.data));
                else
                    this.data[this.actualLength++] = data[i];
        }

        #region IControlledCollection<TypedName> Members

        public int Count
        {
            get { return this.actualLength; }
        }

        public bool Contains(TypedName item)
        {
            foreach (var datum in this.data)
                if (datum.Equals(item))
                    return true;
            return false;
        }

        public void CopyTo(TypedName[] array, int arrayIndex = 0)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (this.Count + arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");
            for (int i = 0; i < this.Count; i++)
                array[i + arrayIndex] = this.data[i];
        }

        public TypedName this[int index]
        {
            get {
                if (index < 0 || index > this.Count)
                    throw new ArgumentOutOfRangeException("index");
                return this.data[index]; }
        }

        public TypedName[] ToArray()
        {
            TypedName[] result = new TypedName[Count];
            this.CopyTo(result, 0);
            return result;
        }

        #endregion

        #region IEnumerable<TypedName> Members

        public IEnumerator<TypedName> GetEnumerator()
        {
            if (this.data == null)
                yield break;
            for (int i = 0; i < this.Count; i++)
                yield return this.data[i];
            yield break;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        public static implicit operator TypedNameSeries(TypedName[] target)
        {
            return new TypedNameSeries(target);
        }

        #region IControlledCollection<TypedName> Members


        public int IndexOf(TypedName element)
        {
            for (int i = 0; i < this.Count; i++)
                if (this.data[i].Equals(element))
                    return i;
            return -1;
        }

        #endregion
    }
}
