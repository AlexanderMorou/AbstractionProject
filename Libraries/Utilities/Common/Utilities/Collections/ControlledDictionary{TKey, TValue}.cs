using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
    /// A generic dictionary whose keys and values are tightly controlled.
    /// </summary>
    /// <typeparam name="TKey">The type of element used as a key.</typeparam>
    /// <typeparam name="TValue">The type of element used as the 
    /// values associated to the keys.</typeparam>
    [Serializable]
    public partial class ControlledDictionary<TKey, TValue> :
        IControlledDictionary<TKey, TValue>,
        IControlledDictionary
    {
        private SharedLocals locals;

        /// <summary>
        /// Creates a new <see cref="ControlledDictionary{TKey, TValue}"/> initialized
        /// to a default state.
        /// </summary>
        protected internal ControlledDictionary()
        {
            this.locals = new SharedLocals(InitializeKeysCollection, InitializeValuesCollection);
        }

        /// <summary>
        /// Creates a new <see cref="ControlledDictionary{TKey, TValue}"/>
        /// which contains the same data set as the <paramref name="sibling"/>
        /// provided.
        /// </summary>
        /// <param name="sibling">The <see cref="ControlledDictionary{TKey, TValue}"/>
        /// to mirror the elements of.</param>
        public ControlledDictionary(ControlledDictionary<TKey, TValue> sibling)
        {
            if (sibling == null)
                throw new ArgumentNullException("sibling");
            this.locals = sibling.locals;
        }

        /// <summary>
        /// Creates a new <see cref="ControlledDictionary{TKey, TValue}"/>
        /// with the <paramref name="entries"/> provided.
        /// </summary>
        /// <param name="entries">The <see cref="IEnumerable{T}"/> of 
        /// <see cref="KeyValuePair{TKey, TValue}"/> elements
        /// to initialize the <see cref="ControlledDictionary{TKey, TValue}"/>.</param>
        /// <exception cref="System.ArgumentNullException">thrown when the 
        /// <paramref name="entries"/> provided are null.</exception>
        public ControlledDictionary(IEnumerable<KeyValuePair<TKey, TValue>> entries)
        {
            if (entries == null)
                throw new ArgumentNullException("entries");
            this.locals = new SharedLocals(InitializeKeysCollection, InitializeValuesCollection);
            this.locals._AddRange(entries.ToArray());
        }

        /// <summary>
        /// Gets a <see cref="KeysCollection"/> containing the 
        /// <see cref="ControlledDictionary{TKey, TValue}"/>'s keys.
        /// </summary>
        /// <returns>
        /// A <see cref="KeysCollection"/> with the keys of the 
        /// <see cref="ControlledDictionary{TKey, TValue}"/>.
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
        /// the set of keys pertinent to the <see cref="ControlledDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <returns>A <see cref="KeysCollection"/> which will maintain the
        /// keys of the <see cref="ControlledDictionary{TKey, TValue}"/>.</returns>
        protected virtual KeysCollection InitializeKeysCollection()
        {
            return new KeysCollection(this);
        }

        /// <summary>
        /// Gets a <see cref="ValuesCollection"/> containing the 
        /// <see cref="ControlledDictionary{TKey, TValue}"/>'s values.
        /// </summary>
        /// <returns>
        /// A <see cref="ValuesCollection"/> with the values of the 
        /// <see cref="ControlledDictionary{TKey, TValue}"/>.
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
        /// the set of values pertinent to the <see cref="ControlledDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <returns>A <see cref="ValuesCollection"/> which will maintain the
        /// values of the <see cref="ControlledDictionary{TKey, TValue}"/>.</returns>
        protected virtual ValuesCollection InitializeValuesCollection()
        {
            return new ValuesCollection(this);
        }

        #region IControlledDictionary<TKey,TValue> Members

        IControlledCollection<TKey> IControlledDictionary<TKey, TValue>.Keys
        {
            get
            {
                return this.Keys;
            }
        }

        IControlledCollection<TValue> IControlledDictionary<TKey, TValue>.Values
        {
            get { return this.Values; }
        }

        /// <summary>
        /// Returns the element of the <see cref="ControlledDictionary{TKey, TValue}"/> with the 
        /// given <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> of the element to get.</param>
        /// <returns>The element with the specified <paramref name="key"/>.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">
        /// There was no element in the <see cref="ControlledDictionary{TKey, TValue}"/> 
        /// containing the <paramref name="key"/> provided.
        /// </exception>
        public TValue this[TKey key]
        {
            get
            {
                return OnGetThis(key);
            }
            internal protected set
            {
                OnSetThis(key, value);
            }
        }

        /// <summary>
        /// Sets the <paramref name="value"/> at the <paramref name="key"/> provided.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> of the 
        /// <paramref name="value"/> to set.</param>
        /// <param name="value">The <typeparamref name="TValue"/> to set at the 
        /// <paramref name="key"/> provided.</param>
        protected virtual void OnSetThis(TKey key, TValue value)
        {
            int index;
            lock (this.SyncRoot)
                if (this.locals.orderings.TryGetValue(key, out index))
                    this.locals.entries[index] = new KeyValuePair<TKey, TValue>(key, value);
                else
                    this.locals._Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        /// <summary>
        /// Obtians the <typeparamref name="TValue"/> using the
        /// <paramref name="key"/> provided.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> of the
        /// <typeparamref name="TValue"/> to retrieve.</param>
        /// <returns>The <typeparamref name="TValue"/> given the 
        /// <paramref name="key"/> of the <see cref="KeyValuePair{TKey, TValue}"/>.</returns>
        protected virtual TValue OnGetThis(TKey key)
        {
            lock (this.SyncRoot)
                return this.locals.entries[locals.orderings[key]].Value;
        }

        /// <summary>
        /// Returns whether an element of the given <paramref name="key"/>
        /// exists within the <see cref="ControlledDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> of the element
        /// to find.</param>
        /// <returns></returns>
        public virtual bool ContainsKey(TKey key)
        {
            lock (this.SyncRoot)
                return this.locals.orderings.ContainsKey(key);
        }

        /// <summary>
        /// Returns whether an element of the given <paramref name="key"/> 
        /// exists within the <see cref="ControlledDictionary{TKey, TValue}"/>,
        /// if it does, it sets <paramref name="value"/> accordingly, or sets
        /// it to the default value for <typeparamref name="TValue"/>.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> to attempt to
        /// retrieve.</param>
        /// <param name="value">The <typeparamref name="TValue"/> to receive
        /// the value within the <see cref="ControlledDictionary{TKey, TValue}"/>
        /// if it exists.</param>
        /// <returns>true, if an element was found and <paramref name="value"/> was 
        /// set to its value; false, otherwise where <paramref name="value"/>
        /// is set to the default value of <typeparamref name="TValue"/>.</returns>
        public virtual bool TryGetValue(TKey key, out TValue value)
        {
            int index;
            lock (this.SyncRoot)
                if (locals.orderings.TryGetValue(key, out index))
                {
                    value = locals.entries[index].Value;
                    return true;
                }
            value = default(TValue);
            return false;
        }

        #endregion

        #region IControlledCollection<KeyValuePair<TKey,TValue>> Members

        /// <summary>
        /// Returns the <see cref="Int32"/> value of the <paramref name="element"/>
        /// provided.
        /// </summary>
        /// <param name="element">The <see cref="KeyValuePair{TKey, TValue}"/>
        /// which contains the information to look for.</param>
        /// <returns>true, if an element contains both the key and value of the 
        /// <paramref name="element"/> provided.</returns>
        public int IndexOf(KeyValuePair<TKey, TValue> element)
        {
            int index;
            lock (this.SyncRoot)
                if (this.locals.orderings.TryGetValue(element.Key, out index))
                {
                    if (this.locals.entries[index].Equals(element.Value))
                        return index;
                }
            return -1;
        }

        /// <summary>
        /// Gets the number of elements contained in the
        /// <see cref="ControlledDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the 
        /// <see cref="ControlledDictionary{TKey, TValue}"/>.
        /// </returns>
        public virtual int Count
        {
            get
            {
                lock (this.SyncRoot)
                    return this.locals.orderings.Count;
            }
        }

        /// <summary>
        /// Determines whether the <see cref="ControlledDictionary{TKey, TValue}"/>
        /// contains a specific 
        /// value.
        /// </summary>
        /// <param name="item">
        /// The <see cref="KeyValuePair{TKey, TValue}"/> to locate in
        /// the <see cref="ControlledDictionary{TKey, TValue}"/>.
        /// </param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the
        /// <see cref="ControlledDictionary{TKey, TValue}"/>;
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
        /// Copies the elements of the <see cref="ControlledDictionary{TKey, TValue}"/>
        /// to an <see cref="System.Array"/>, starting at a particular
        /// <see cref="System.Array"/> index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="System.Array"/> that is the
        /// destination of the elements copied from <see
        /// cref="ControlledDictionary{TKey, TValue}"/>. The
        /// <see cref="System.Array"/> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">
        /// The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="arrayIndex"/> is less than 0.</exception>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="array"/> is null.</exception>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="array"/> is multidimensional.-or-<paramref name="arrayIndex"/> 
        /// is equal to or greater than the length of <paramref name="array"/>.-or-The 
        /// number of elements in the source <see cref="ControlledDictionary{TKey, TValue}"/> is greater 
        /// than the available space from <paramref name="arrayIndex"/> to the 
        /// end of the destination <paramref name="array"/>.-or-the underlying <see cref="Type"/>
        /// of the elements of the <see cref="ControlledDictionary{TKey, TValue} "/>
        /// cannot be cast automatically to the type of the destination.
        /// <paramref name="array"/>.</exception>
        /// <exception cref="System.ArrayTypeMismatchException">thrown when the type
        /// of the <paramref name="array"/> does not match the underlying type of
        /// the elements of the <see cref="ControlledDictionary{TKey, TValue}"/>.</exception>
        public virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            int c;
            lock (this.SyncRoot)
                c = this.locals.orderings.Count;
            ThrowHelper.CopyToCheck(array, arrayIndex, c);
            if (c == 0)
                return;
            lock (this.SyncRoot)
                Array.ConstrainedCopy(this.locals.entries, 0, array, arrayIndex, c);
        }

        /// <summary>
        /// Returns the element at the <paramref name="index"/> provided.
        /// </summary>
        /// <param name="index">The index of the member to find.</param>
        /// <returns>The instance of <see cref="System.Object"/> at the <paramref name="index"/>
        /// provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is beyond the range of the 
        /// <see cref="ControlledDictionary{TKey, TValue}"/>.
        /// </exception>
        public KeyValuePair<TKey, TValue> this[int index]
        {
            get
            {
                return OnGetThis(index);
            }
            protected set
            {
                OnSetThis(index, value);
            }
        }

        /// <summary>
        /// Sets the <see cref="KeyValuePair{TKey, TValue}"/> at the <paramref name="index"/> 
        /// provided.
        /// </summary>
        /// <param name="index">The <see cref="Int32"/> ordinal index of the
        /// <paramref name="value"/> to set.</param>
        /// <param name="value">The <see cref="KeyValuePair{TKey, TValue}"/>
        /// to set.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown
        /// when the <paramref name="index"/> is less than zero, or
        /// greater than or equal to the number of items within the
        /// <see cref="ControlledDictionary{TKey, TValue}"/>.</exception>
        protected virtual void OnSetThis(int index, KeyValuePair<TKey, TValue> value)
        {
            int c;
            lock (this.SyncRoot)
                c = this.locals.orderings.Count;

            if (index < 0 ||
                index >= c)
                throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
            var newKey = value.Key;
            var newValue = value.Value;
            lock (this.SyncRoot)
            {
                var oldKey = this.Keys[index];
                if (oldKey.Equals(newKey))
                    this.locals.entries[index] = value;
                else if (!this.ContainsKey(newKey))
                {
                    this.locals.orderings.Remove(oldKey);
                    this.locals.orderings.Add(newKey, index);
                    this.locals.entries[index] = value;
                }
                else
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.value, ExceptionMessageId.DuplicateKeyExists);
            }
        }

        /// <summary>
        /// Retrieves an element by its ordinal index.
        /// </summary>
        /// <param name="index">The <see cref="Int32"/> ordinal index
        /// of the <see cref="KeyValuePair{TKey, TValue}"/> entry
        /// to return.</param>
        /// <returns>A <see cref="KeyValuePair{TKey, TValue}"/> relative to the
        /// <paramref name="index"/> provided.</returns>
        protected virtual KeyValuePair<TKey, TValue> OnGetThis(int index)
        {
            int c;
            lock (this.SyncRoot)
                c = this.locals.orderings.Count;
            if (index < 0 ||
                index >= c)
                throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
            lock (this.SyncRoot)
                return this.locals.entries[index];
        }

        /// <summary>
        /// Obtains the elements of the <see cref="ControlledDictionary{TKey, TValue}"/>
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
        /// elements of the <see cref="ControlledDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> which enumerates
        /// the values of the <see cref="ControlledDictionary{TKey, TValue}"/>.
        /// </returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.OnGetEnumerator();
        }

        protected virtual IEnumerator<KeyValuePair<TKey, TValue>> OnGetEnumerator()
        {
            int c;
            lock (this.SyncRoot)
                c = this.locals.orderings.Count;
            for (int i = 0; i < c; i++)
                yield return this.locals.entries[i];
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IControlledDictionary Members

        IControlledCollection IControlledDictionary.Keys
        {
            get { return this.Keys; }
        }

        IControlledCollection IControlledDictionary.Values
        {
            get { return this.Values; }
        }

        object IControlledDictionary.this[object key]
        {
            get
            {
                if (!(key is TKey))
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.key, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.key), key.GetType().ToString(), typeof(TKey).ToString());
                return this[(TKey)key];
            }
        }

        bool IControlledDictionary.ContainsKey(object key)
        {
            if (!(key is TKey))
                return false;
            return this.ContainsKey((TKey)key);
        }

        IDictionaryEnumerator IControlledDictionary.GetEnumerator()
        {
            return new SimpleDictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
        }

        #endregion

        #region IControlledCollection Members

        bool IControlledCollection.Contains(object item)
        {
            if (!(item is KeyValuePair<TKey, TValue>))
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.item, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.item), item.GetType().ToString(), typeof(KeyValuePair<TKey, TValue>).ToString());
            return this.Contains((KeyValuePair<TKey, TValue>)item);
        }

        void IControlledCollection.CopyTo(Array array, int arrayIndex)
        {
            CopyToArray(array, arrayIndex);
        }

        /// <summary>
        /// Copies the elements of the <see cref="ControlledDictionary{TKey, TValue}"/>
        /// to an <see cref="System.Array"/>, starting at a particular
        /// <paramref name="arrayIndex"/>.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="System.Array"/> that is the destination of the 
        /// elements copied from <see cref="ControlledDictionary{TKey, TValue}"/>. The 
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
        /// number of elements in the source <see cref="ControlledDictionary{TKey, TValue}"/> is greater 
        /// than the available space from <paramref name="arrayIndex"/> to the 
        /// end of the destination <paramref name="array"/>.-or-the underlying <see cref="Type"/>
        /// of the elements of the <see cref="IControlledCollection "/>
        /// cannot be cast automatically to the type of the destination.
        /// <paramref name="array"/>.</exception>
        /// <exception cref="System.ArrayTypeMismatchException">thrown when the type
        /// of the <paramref name="array"/> does not match the underlying type of
        /// the elements of the <see cref="ControlledDictionary{TKey, TValue}"/>.</exception>
        protected virtual void CopyToArray(Array array, int arrayIndex = 0)
        {
            lock (this.SyncRoot)
                Array.ConstrainedCopy(this.locals.entries, 0, array, arrayIndex, this.Count);
        }

        object IControlledCollection.this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                    throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                lock (this.SyncRoot)
                    return this.locals.entries[index];
            }
        }

        int IControlledCollection.IndexOf(object element)
        {
            if (element is KeyValuePair<TKey, TValue>)
                return this.IndexOf((KeyValuePair<TKey, TValue>)element);
            return -1;
        }

        /// <summary>
        /// Gets a value indicating whether access to the 
        /// <see cref="ControlledDictionary{TKey, TValue}"/>
        /// is synchronized (thread safe).
        /// </summary>
        /// <returns>true if access to the 
        /// <see cref="ControlledDictionary{TKey, TValue}"/>
        /// is synchronized (thread safe); false, otherwise.</returns>
        public bool IsSynchronized
        {
            get { return true; }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the 
        /// <see cref="ControlledDictionary{TKey, TValue}"/>.
        /// </summary>
        public object SyncRoot
        {
            get { return this.locals.syncObject; }
        }

        #endregion

        /// <summary>
        /// Adds an element to the <see cref="ControlledDictionary{TKey, TValue}"/>
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
        /// Adds a <paramref name="item"/> to the <see cref="ControlledDictionary{TKey, TValue}"/>.
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
        /// <see cref="ControlledDictionary{TKey, TValue}"/>.
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
        /// <see cref="ControlledDictionary{TKey, TValue}"/>.
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
        /// Removes an element from the <see cref="ControlledDictionary{TKey, TValue}"/>
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
        /// <see cref="ControlledDictionary{TKey, TValue}"/>
        /// by their <typeparamref name="TKey"/>.
        /// </summary>
        /// <param name="keys">The <see cref="IEnumerable{T}"/>
        /// of entries to remove from the <see cref="ControlledDictionary{TKey, TValue}"/>.</param>
        /// <remarks>Protected to ensure the controlled nature of the dictionary.</remarks>
        protected internal void _RemoveSet(IEnumerable<TKey> keys)
        {
            this._RemoveSet(from key in keys
                            where this.locals.orderings.ContainsKey(key)
                            let index = this.locals.orderings[key]
                            orderby index descending
                            select index);
        }

        private void _RemoveSet(IEnumerable<int> indices)
        {
            foreach (var index in indices.ToArray())
                _Remove(index);
        }
        /// <summary>
        /// Removes an element from the <see cref="ControlledDictionary{TKey, TValue}"/>
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
        /// Clears the <see cref="ControlledDictionary{TKey, TValue}"/>.
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
