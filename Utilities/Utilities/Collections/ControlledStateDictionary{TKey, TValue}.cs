using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    /// <summary>
    /// A generic dictionary whose keys and values are tightly controlled.
    /// </summary>
    /// <typeparam name="TKey">The type of element used as a key.</typeparam>
    /// <typeparam name="TValue">The type of element used as the values associated to the keys.</typeparam>
    [Serializable]
    public partial class ControlledStateDictionary<TKey, TValue> :
        IControlledStateDictionary<TKey, TValue>,
        IControlledStateDictionary
    {
        private SharedLocals locals;

        /// <summary>
        /// Creates a new <see cref="ControlledStateDictionary{TKey, TValue}"/> initialized
        /// to a default state.
        /// </summary>
        protected internal ControlledStateDictionary()
        {
            this.locals = new SharedLocals(InitializeKeysCollection, InitializeValuesCollection);
        }

        /// <summary>
        /// Creates a new <see cref="ControlledStateDictionary{TKey, TValue}"/>
        /// which contains the same data set as the <paramref name="sibling"/>
        /// provided.
        /// </summary>
        /// <param name="sibling">The <see cref="ControlledStateDictionary{TKey, TValue}"/>
        /// to mirror the elements of.</param>
        public ControlledStateDictionary(ControlledStateDictionary<TKey, TValue> sibling)
        {
            if (sibling == null)
                throw new ArgumentNullException("sibling");
            this.locals = sibling.locals;
        }

        /// <summary>
        /// Creates a new <see cref="ControlledStateDictionary{TKey, TValue}"/>
        /// with the <paramref name="entries"/> provided.
        /// </summary>
        /// <param name="entries"></param>
        public ControlledStateDictionary(IEnumerable<KeyValuePair<TKey, TValue>> entries)
        {
            if (entries == null)
                throw new ArgumentNullException("entries");
            this.locals = new SharedLocals(InitializeKeysCollection, InitializeValuesCollection);
            this.locals._AddRange(entries.ToArray());
        }

        /// <summary>
        /// Gets a <see cref="KeysCollection"/> containing the 
        /// <see cref="ControlledStateDictionary{TKey, TValue}"/>'s keys.
        /// </summary>
        /// <returns>
        /// A <see cref="KeysCollection"/> with the keys of the 
        /// <see cref="ControlledStateDictionary{TKey, TValue}"/>.
        /// </returns>
        public KeysCollection Keys
        {
            get
            {
                return this.locals.Keys;
            }
        }

        /// <summary>
        /// Obtains the <see cref="KeysCollection"/> which contains
        /// the set of keys pertinent to the <see cref="ControlledStateDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <returns>A <see cref="KeysCollection"/> which will maintain the
        /// keys of the <see cref="ControlledStateDictionary{TKey, TValue}"/>.</returns>
        protected virtual KeysCollection InitializeKeysCollection()
        {
            return new KeysCollection(this);
        }

        /// <summary>
        /// Gets a <see cref="ValuesCollection"/> containing the 
        /// <see cref="ControlledStateDictionary{TKey, TValue}"/>'s values.
        /// </summary>
        /// <returns>
        /// A <see cref="ValuesCollection"/> with the values of the 
        /// <see cref="ControlledStateDictionary{TKey, TValue}"/>.
        /// </returns>
        public ValuesCollection Values
        {
            get
            {
                return this.locals.Values;
            }
        }


        /// <summary>
        /// Obtains the <see cref="ValuesCollection"/> which contains
        /// the set of values pertinent to the <see cref="ControlledStateDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <returns>A <see cref="ValuesCollection"/> which will maintain the
        /// values of the <see cref="ControlledStateDictionary{TKey, TValue}"/>.</returns>
        protected virtual ValuesCollection InitializeValuesCollection()
        {
            return new ValuesCollection(this);
        }

        #region IControlledStateDictionary<TKey,TValue> Members

        IControlledStateCollection<TKey> IControlledStateDictionary<TKey,TValue>.Keys
        {
            get {
                return this.Keys;
            }
        }

        IControlledStateCollection<TValue> IControlledStateDictionary<TKey,TValue>.Values
        {
            get { return this.Values; }
        }

        /// <summary>
        /// Returns the element of the <see cref="ControlledStateDictionary{TKey, TValue}"/> with the 
        /// given <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> of the element to get.</param>
        /// <returns>The element with the specified <paramref name="key"/>.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">
        /// There was no element in the <see cref="ControlledStateDictionary{TKey, TValue}"/> 
        /// containing the <paramref name="key"/> provided.
        /// </exception>
        public TValue this[TKey key]
        {
            get
            {
                return OnGetThis(key);
            }
            protected set
            {
                OnSetThis(key, value);
            }
        }

        protected virtual void OnSetThis(TKey key, TValue value)
        {
            int index;
            if (this.locals.orderings.TryGetValue(key, out index))
                this.locals.entries[index] = new KeyValuePair<TKey, TValue>(key, value);
            else
                this.locals._Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        protected virtual TValue OnGetThis(TKey key)
        {
            return this.locals.entries[locals.orderings[key]].Value;
        }

        public virtual bool ContainsKey(TKey key)
        {
            return this.locals.orderings.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            int index;
            if (locals.orderings.TryGetValue(key, out index))
            {
                value = locals.entries[index].Value;
                return true;
            }
            value = default(TValue);
            return false;
        }

        #endregion

        #region IControlledStateCollection<KeyValuePair<TKey,TValue>> Members

        public int IndexOf(KeyValuePair<TKey, TValue> element)
        {
            int index;
            if (this.locals.orderings.TryGetValue(element.Key, out index))
            {
                if (this.locals.entries[index].Equals(element.Value))
                    return index;
            }
            return -1;
        }

        /// <summary>
        /// Gets the number of elements contained in the
        /// <see cref="ControlledStateDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the 
        /// <see cref="ControlledStateDictionary{TKey, TValue}"/>.
        /// </returns>
        public virtual int Count
        {
            get { return this.locals.orderings.Count; }
        }

        /// <summary>
        /// Determines whether the <see cref="ControlledStateDictionary{TKey, TValue}"/> contains a specific 
        /// value.
        /// </summary>
        /// <param name="item">
        /// The <see cref="KeyValuePair{TKey, TValue}"/> to locate in the <see cref="ControlledStateDictionary{TKey, TValue}"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the
        /// <see cref="ControlledStateDictionary{TKey, TValue}"/>;
        /// otherwise, false.
        /// </returns>
        public virtual bool Contains(KeyValuePair<TKey, TValue> item)
        {
            TValue checkedValue;
            if (TryGetValue(item.Key, out checkedValue))
            {
                if (checkedValue.Equals(item.Value))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Copies the elements of the <see cref="ControlledStateDictionary{TKey, TValue}"/> to an
        /// <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> 
        /// index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="System.Array"/> that is the destination of the 
        /// elements copied from <see cref="ControlledStateDictionary{TKey, TValue}"/>. The 
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
        /// number of elements in the source <see cref="ControlledStateDictionary{TKey, TValue}"/> is greater 
        /// than the available space from <paramref name="arrayIndex"/> to the 
        /// end of the destination <paramref name="array"/>.-or-the underlying <see cref="Type"/>
        /// of the elements of the <see cref="ControlledStateDictionary{TKey, TValue} "/>
        /// cannot be cast automatically to the type of the destination.
        /// <paramref name="array"/>.</exception>
        /// <exception cref="System.ArrayTypeMismatchException">thrown when the type
        /// of the <paramref name="array"/> does not match the underlying type of
        /// the elements of the <see cref="ControlledStateDictionary{TKey, TValue}"/>.</exception>
        public virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (this.Count == 0)
                return;
            Array.ConstrainedCopy(this.locals.entries, 0, array, arrayIndex, this.Count);
        }

        /// <summary>
        /// Returns the element at the <paramref name="index"/> provided.
        /// </summary>
        /// <param name="index">The index of the member to find.</param>
        /// <returns>The instance of <see cref="System.Object"/> at the <paramref name="index"/>
        /// provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is  beyond the range of the 
        /// <see cref="ControlledStateDictionary{TKey, TValue}"/>.
        /// </exception>
        public KeyValuePair<TKey, TValue> this[int index]
        {
            get
            {
                return OnGetThis(index);
            }
            protected set
            {
                if (index < 0 ||
                    index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                this.locals.entries[index] = value;
            }
        }

        /// <summary>
        /// Occurs when an element is retrieved by index.
        /// </summary>
        /// <param name="index">The <see cref="Int32"/> ordinal index
        /// of the <see cref="KeyValuePair{TKey, TValue}"/> entry
        /// to return.</param>
        /// <returns>A <see cref="KeyValuePair{TKey, TValue}"/> relative to the
        /// <paramref name="index"/> provided.</returns>
        protected virtual KeyValuePair<TKey, TValue> OnGetThis(int index)
        {
            if (index < 0 ||
                index >= this.Count)
                throw new ArgumentOutOfRangeException("index");
            return this.locals.entries[index];
        }

        /// <summary>
        /// Obtains the elements of the <see cref="ControlledStateDictionary{TKey, TValue}"/>
        /// as an <see cref="Array"/> of <see cref="KeyValuePair{TKey, TValue}"/>
        /// elements..
        /// </summary>
        /// <returns>An <see cref="Array"/> of 
        /// <see cref="KeyValuePair{TKey, TValue}"/>.
        /// </returns>
        public virtual KeyValuePair<TKey, TValue>[] ToArray()
        {
            KeyValuePair<TKey, TValue>[] result = new KeyValuePair<TKey, TValue>[this.Count];
            this.CopyTo(result, 0);
            return result;
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        /// <summary>
        /// Obtains an <see cref="IEnumerator{T}"/> which iterates the
        /// elements of the <see cref="ControlledStateDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
                yield return this.locals.entries[i];
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IControlledStateDictionary Members

        IControlledStateCollection IControlledStateDictionary.Keys
        {
            get { return this.Keys; }
        }

        IControlledStateCollection IControlledStateDictionary.Values
        {
            get { return this.Values; }
        }

        object IControlledStateDictionary.this[object key]
        {
            get {
                if (!(key is TKey))
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.key, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.key), key.GetType().ToString(), typeof(TKey).ToString());
                return this[(TKey)key];
            }
        }

        bool IControlledStateDictionary.ContainsKey(object key)
        {
            if (!(key is TKey))
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.key, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.key), key.GetType().ToString(), typeof(TKey).ToString());
            return this.ContainsKey((TKey)key);
        }

        IDictionaryEnumerator IControlledStateDictionary.GetEnumerator()
        {
            return new SimpleDictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
        }

        #endregion

        #region IControlledStateCollection Members

        bool IControlledStateCollection.Contains(object item)
        {
            if (!(item is KeyValuePair<TKey, TValue>))
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.item, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.item), item.GetType().ToString(), typeof(KeyValuePair<TKey, TValue>).ToString());
            return this.Contains((KeyValuePair<TKey, TValue>)item);
        }

        void IControlledStateCollection.CopyTo(Array array, int arrayIndex)
        {
            CopyToArray(array, arrayIndex);
        }

        /// <summary>
        /// Copies the elements of the <see cref="ControlledStateDictionary{TKey, TValue}"/>
        /// to an <see cref="System.Array"/>, starting at a particular
        /// <paramref name="arrayIndex"/>.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="System.Array"/> that is the destination of the 
        /// elements copied from <see cref="ControlledStateDictionary{TKey, TValue}"/>. The 
        /// <see cref="System.Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">
        /// The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="arrayIndex"/> is less than 0.</exception>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="array"/> is null.</exception>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="array"/> is multidimensional.-or-<paramref name="arrayIndex"/> 
        /// is equal to or greater than the length of <paramref name="array"/>.-or-The 
        /// number of elements in the source <see cref="ControlledStateDictionary{TKey, TValue}"/> is greater 
        /// than the available space from <paramref name="arrayIndex"/> to the 
        /// end of the destination <paramref name="array"/>.-or-the underlying <see cref="Type"/>
        /// of the elements of the <see cref="IControlledStateCollection "/>
        /// cannot be cast automatically to the type of the destination.
        /// <paramref name="array"/>.</exception>
        /// <exception cref="System.ArrayTypeMismatchException">thrown when the type
        /// of the <paramref name="array"/> does not match the underlying type of
        /// the elements of the <see cref="ControlledStateDictionary{TKey, TValue}"/>.</exception>
        protected virtual void CopyToArray(Array array, int arrayIndex = 0)
        {
            Array.ConstrainedCopy(this.locals.entries, 0, array, arrayIndex, this.Count);
        }

        object IControlledStateCollection.this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                return this.locals.entries[index];
            }
        }

        int IControlledStateCollection.IndexOf(object element)
        {
            if (element is KeyValuePair<TKey, TValue>)
                return this.IndexOf((KeyValuePair<TKey, TValue>)element);
            return -1;
        }

        /// <summary>
        /// Gets a value indicating whether access to the 
        /// <see cref="ControlledStateDictionary{TKey, TValue}"/>
        /// is synchronized (thread safe).
        /// </summary>
        /// <returns>true if access to the 
        /// <see cref="ControlledStateDictionary{TKey, TValue}"/>
        /// is synchronized (thread safe); false, otherwise.</returns>
        public bool IsSynchronized
        {
            get { return true; }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the 
        /// <see cref="ControlledStateDictionary{TKey, TValue}"/>.
        /// </summary>
        public object SyncRoot
        {
            get { return this.locals.syncObject; }
        }

        #endregion

        /// <summary>
        /// Adds an element to the <see cref="ControlledStateDictionary{TKey, TValue}"/>
        /// with the <paramref name="key"/> and <paramref name="value"/>.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> of the <paramref name="value"/>
        /// to add.</param>
        /// <param name="value">The <typeparamref name="TValue"/> to insert.</param>
        /// <remarks>Protected to ensure the controlled nature of the dictionary.</remarks>
        protected internal virtual void _Add(TKey key, TValue value)
        {
            this.locals._Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        /// <summary>
        /// Adds a <paramref name="item"/> to the <see cref="ControlledStateDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="item">The <see cref="KeyValuePair{TKey, TValue}"/>
        /// which denotes the <typeparamref name="TKey"/> and <typeparamref name="TValue"/>
        /// pair of the item to insert.</param>
        /// <remarks>Protected to ensure the controlled nature of the dictionary.</remarks>
        protected internal void _Add(KeyValuePair<TKey, TValue> item)
        {
            this._Add(item.Key, item.Value);
        }

        /// <summary>
        /// Adds a set of <paramref name="elements"/> to the 
        /// <see cref="ControlledStateDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="elements">The <see cref="IEnumerable{T}"/> of
        /// <see cref="KeyValuePair{TKey, TValue}"/> elements to insert.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="elements"/>
        /// is null.</exception>
        /// <remarks>Protected to ensure the controlled nature of the dictionary.</remarks>
        protected void _AddRange(IEnumerable<KeyValuePair<TKey, TValue>> elements)
        {
            if (elements == null)
                throw new ArgumentNullException("elements");
            this._AddRange(elements.ToArray());
        }

        /// <summary>
        /// Adds a set of <paramref name="elements"/>
        /// <see cref="ControlledStateDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="elements">The <see cref="KeyValuePair{TKey, TValue}"/>
        /// array of items to insert.</param>
        /// <remarks>Protected to ensure the controlled nature of the dictionary.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="elements"/>
        /// is null.</exception>
        /// <remarks>Protected to ensure the controlled nature of the dictionary.</remarks>
        protected virtual void _AddRange(KeyValuePair<TKey, TValue>[] elements)
        {
            if (elements == null)
                throw new ArgumentNullException("elements");
            this.locals._AddRange(elements);
        }

        /// <summary>
        /// Removes an element from the <see cref="ControlledStateDictionary{TKey, TValue}"/>
        /// by the <paramref name="key"/> provided.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/>
        /// of the element to remove.</param>
        /// <returns>true if the element was removed; false, otherwise.</returns>
        /// <remarks>Protected to ensure the controlled nature of the dictionary.</remarks>
        protected internal bool _Remove(TKey key)
        {
            int index;
            if (this.locals.orderings.TryGetValue(key, out index))
                return this._Remove(index);
            return false;
        }

        /// <summary>
        /// Removes a series of elements from the
        /// <see cref="ControlledStateDictionary{TKey, TValue}"/>
        /// by their <typeparamref name="TKey"/>.
        /// </summary>
        /// <param name="keys">The <see cref="IEnumerable{T}"/>
        /// of entries to remove from the <see cref="ControlledStateDictionary{TKey, TValue}"/>.</param>
        /// <remarks>Protected to ensure the controlled nature of the dictionary.</remarks>
        protected internal void _RemoveSet(IEnumerable<TKey> keys)
        {
            this._RemoveSet(from key in keys
                            where this.locals.orderings.ContainsKey(key)
                            let index = this.locals.orderings[key]
                            orderby index descending
                            select index);
        }

        private void _RemoveSet(IEnumerable<int> elements)
        {
            this.locals._RemoveSet(elements);
        }

        /// <summary>
        /// Removes an element from the <see cref="ControlledStateDictionary{TKey, TValue}"/>
        /// by its <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The <see cref="Int32"/> ordinal index of the element
        /// to remove.</param>
        /// <returns>true, if the element was removed; false, otherwise.</returns>
        /// <remarks>Protected to ensure the controlled nature of the dictionary.</remarks>
        protected internal virtual bool _Remove(int index)
        {
            return this.locals._Remove(index);
        }

        /// <summary>
        /// Clears the <see cref="ControlledStateDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <remarks>Protected to ensure the controlled nature of the dictionary.</remarks>
        protected internal virtual void _Clear()
        {
            this.locals.Clear();
        }

        #region Key/Value Collection Instance Helpers

        /* *
         * Used by true inheritors to ensure proper disposal of
         * references.
         * */

        internal KeysCollection keysInstance
        {
            get
            {
                return this.locals.keysInstance;
            }
            set
            {
                this.locals.keysInstance = value;
            }
        }

        internal ValuesCollection valuesInstance
        {
            get
            {
                return this.locals.valuesInstance;
            }
            set
            {
                this.locals.valuesInstance = value;
            }
        }
        #endregion
    }
}
