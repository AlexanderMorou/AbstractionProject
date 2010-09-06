using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.CodeAnalysis;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Utilities.Collections
{
    /// <summary>
    /// Defines properties and methods for working with a customized
    /// dictionary.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    #if CODE_ANALYSIS
    /* *
     * Added correct suffix suppression. 
     * This type utilizes a dictionary base; however, it does not derive from
     * the dictionary class due to its inability to tightly control 
     * dictionary insertions/deletions.
     * */
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    #endif

    public class CustomDictionaryBase<TKey, TValue> :
        IDictionary<TKey, TValue>,
        IDictionary
    {
        /// <summary>
        /// Data member for <see cref="IDictionary.Values"/>.
        /// </summary>
        private ICollection vSimp = null;
        /// <summary>
        /// Data member for <see cref="IDictionary.Keys"/>.
        /// </summary>
        private ICollection kSimp = null;
        /// <summary>
        /// Data member which contains the real implementation.
        /// </summary>
        internal IDictionary<TKey, TValue> backup = null;

        /// <summary>
        /// Creates a new <see cref="CustomDictionaryBase{TKey, TValue}"/>
        /// with the <paramref name="target"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IDictionary{TKey, TValue}"/> 
        /// to encapsulate.</param>
        public CustomDictionaryBase(IDictionary<TKey, TValue> target)
        {
            this.backup = target;
        }

        /// <summary>
        /// Creates a new <see cref="CustomDictionaryBase{TKey, TValue}"/> 
        /// initialized to a default state.
        /// </summary>
        public CustomDictionaryBase() 
            : this(new Dictionary<TKey, TValue>())
        {

        }

        #region IDictionary<TKey,TValue> Members

        /// <summary>
        /// Adds an element of the provided <paramref name="key"/> and <paramref name="value"/> to the
        /// <see cref="CustomDictionaryBase{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> of the
        /// current <paramref name="value"/> inserted.</param>
        /// <param name="value">The <typeparamref name="TValue"/>
        /// to insert.</param>
        public virtual void Add(TKey key, TValue value)
        {
            this.backup.Add(key, value);
        }

        /// <summary>
        /// Returns whether <paramref name="key"/> exists as an entry in <see cref="Keys"/>.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> of the <typeparamref name="TValue"/> to
        /// check for.</param>
        /// <returns>true if the <paramref name="key"/> exists in <see cref="Keys"/></returns>
        public virtual bool ContainsKey(TKey key)
        {
            return backup.ContainsKey(key);
        }

        /// <summary>
        /// Returns the <see cref="ICollection{T}"/> of 
        /// <typeparamref name="TKey"/> relative
        /// to the <see cref="Values"/>.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get { return GetKeys(); }
        }

        #if CODE_ANALYSIS
        /* *
         * Added suppression for 'Use properties where appropriate' since this is 
         * intended to allow inheritors to override the initial keys collection
         * as well as hide the type of the publically exposed keys collection.
         * */
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        #endif
        protected virtual ICollection<TKey> GetKeys()
        {
            return backup.Keys;
        }

        /// <summary>
        /// Removes an element with the specified <paramref name="key"/> from the 
        /// <see cref="CustomDictionaryBase{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the <typeparamref name="TValue"/> to remove.</param>
        /// <returns>true if the element was successfully removed; false otherwise.</returns>
        public virtual bool Remove(TKey key)
        {
            return this.backup.Remove(key);
        }

        /// <summary>
        /// Gets the value associated with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key of the <paramref name="value"/> to get.</param>
        /// <param name="value">The outgoing <typeparamref name="TValue"/> 
        /// which is set to the element of the <see cref="CustomDictionaryBase{TKey, TValue}"/>
        /// if it exists.</param>
        /// <returns>true, if the value exists; false otherwise.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return backup.TryGetValue(key, out value);
        }

        /// <summary>
        /// Returns the <see cref="ICollection{T}"/> of 
        /// <typeparamref name="TValue"/> series that 
        /// is contained within the 
        /// <see cref="CustomDictionaryBase{TKey, TValue}"/>.
        /// </summary>
        public ICollection<TValue> Values
        {
            get { return GetValues(); }
        }

        #if CODE_ANALYSIS
        /* *
         * Added suppression for 'Use properties where appropriate' since this is 
         * intended to allow inheritors to override the initial values collection
         * as well as hide the type of the publically exposed values collection.
         * */
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        #endif
        protected virtual ICollection<TValue> GetValues()
        {
            return backup.Values;
        }

        /// <summary>
        /// Gets/sets the value associated with the specified 
        /// <paramref name="key"/>.</summary>
        /// <param name="key">The 
        /// <typeparamref name="TKey"/> 
        /// to look for.</param>
        /// <returns>A <typeparamref name="TValue"/> relative 
        /// to <paramref name="key"/>.</returns>
        public virtual TValue this[TKey key]
        {
            get
            {
                return backup[key];
            }
            set
            {
                this.backup[key] = value;
            }
        }

        #endregion

        #region ICollection<KeyValuePair<TKey, TValue>> Members

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            this.Add(item.Key, item.Value);
        }

        /// <summary>
        /// Removes all entries from the <see cref="CustomDictionaryBase{TKey, TValue}"/>.
        /// </summary>
        public virtual void Clear()
        {
            this.backup.Clear();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)(backup)).Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ICollection_CopyTo(array, arrayIndex);
        }

        #if CODE_ANALYSIS
        /* *
         * Suppressed underscore message in light of this being the
         * ICollection.CopyTo functionality.  Used for special cases when
         * the core dictionary isn't used.
         * */
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        #endif
        protected virtual void ICollection_CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)(backup)).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="CustomDictionaryBase{TKey, TValue}"/>.
        /// </summary>
        public virtual int Count
        {
            get { return backup.Count; }
        }

        /// <summary>
        /// Returns whether the <see cref="CustomDictionaryBase{TKey, TValue}"/> is read-only.
        /// </summary>
        public virtual bool IsReadOnly
        {
            get { return this.backup.IsReadOnly; }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)(this.backup)).Remove(item);
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey, TValue>> Members

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="CustomDictionaryBase{TKey, TValue}"/>.
        /// </summary>
        /// <returns>A <see cref="IEnumerator{T}"/> which can be used to iterate the 
        /// <see cref="CustomDictionaryBase{TKey, TValue}"/>.</returns>
        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.backup.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IDictionary Members

        void IDictionary.Add(object key, object value)
        {
            bool isKey = key is TKey;
            if (!isKey ||
                !(value is TValue))
                throw new ArgumentException(isKey ? "value" : "key");
            this.Add((TKey)key, (TValue)value);
        }

        void IDictionary.Clear()
        {
            this.Clear();
        }

        bool IDictionary.Contains(object key)
        {
            return IDictionaryContains(key);
        }

        protected virtual bool IDictionaryContains(object key)
        {
            bool isKey = false;
            TKey k = (isKey = (key is TKey)) ? default(TKey) : (TKey)key;
            if (!isKey)
                throw new ArgumentException("key");
            return this.ContainsKey(k);
        }

        protected virtual IDictionaryEnumerator IDictionaryGetEnumerator()
        {
            return new SimpleDictionaryEnumerator<TKey, TValue>(this.backup.GetEnumerator());
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return IDictionaryGetEnumerator();
        }

        bool IDictionary.IsFixedSize
        {
            get { return true; }
        }

        bool IDictionary.IsReadOnly
        {
            get { return true; }
        }

        ICollection IDictionary.Keys
        {
            get
            {
                if (this.kSimp == null)
                    this.kSimp = this.Keys.Simplify();
                return this.kSimp;
            }
        }

        void IDictionary.Remove(object key)
        {
            if (!(key is TKey))
                throw new ArgumentException("key");
            this.backup.Remove(((TKey)(key)));
        }

        ICollection IDictionary.Values
        {
            get
            {
                if (this.vSimp == null)
                    this.vSimp = this.Values.Simplify();
                return this.vSimp;
            }
        }

        object IDictionary.this[object key]
        {
            get
            {
                if (!(key is TKey))
                    throw new ArgumentException("key");
                return this[(TKey)key];
            }
            set
            {
                bool isKey = key is TKey;
                if (!isKey ||
                    !(value is TValue))
                    throw new ArgumentException(isKey ? "value" : "key");
                this[((TKey)(key))] = ((TValue)(value));
            }
        }

        #endregion

        #region ICollection Members

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (array.Length - index < this.Count)
                throw new ArgumentOutOfRangeException("index");
            int i = 0;
            foreach (KeyValuePair<TKey, TValue> kvp in this)
                array.SetValue(kvp, index + (i++));
        }

        bool ICollection.IsSynchronized
        {
            get { return OnGetIsSynchronized(); }
        }

        object ICollection.SyncRoot
        {
            get { return OnGetSyncRoot(); }
        }

        #endregion

        protected virtual object OnGetSyncRoot()
        {
            return null;
        }

        protected virtual bool OnGetIsSynchronized()
        {
            return false;
        }
    }
}
