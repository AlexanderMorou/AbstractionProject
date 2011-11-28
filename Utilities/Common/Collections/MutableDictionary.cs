using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    /// <summary>
    /// Provides a base implementation of a mutable dictionary
    /// which can quickly rekey elements, and is thread-safe.
    /// </summary>
    /// <typeparam name="TKey">The type used to represent
    /// the keys of the dictionary.</typeparam>
    /// <typeparam name="TValue">The type used to represent the values
    /// of the dictionary.</typeparam>
    public class MutableDictionary<TKey, TValue> :
        ControlledDictionary<TKey, TValue>
    {
        /// <summary>
        /// Creates a new <see cref="MutableDictionary{TKey, TValue}"/>
        /// initialized to a default state.
        /// </summary>
        public MutableDictionary()
        {

        }

        /// <summary>
        /// Adds a <paramref name="value"/> based
        /// off of the <paramref name="key"/> provided.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/>
        /// value representing the value used to represent
        /// <paramref name="value"/> within the dictionary.</param>
        /// <param name="value">The value to insert.</param>
        public void Add(TKey key, TValue value)
        {
            base._Add(key, value);
        }

        /// <summary>
        /// Removes the entry from within the 
        /// <see cref="MutableDictionary{TKey, TValue}"/>
        /// based off of the <paramref name="key"/> provided.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> of the element
        /// to remove.</param>
        /// <returns>true if the element was removed; false, otherwise.</returns>
        public bool Remove(TKey key)
        {
            return base._Remove(key);
        }

        /// <summary>
        /// Removes the entry at the <paramref name="index"/>
        /// provided from the <see cref="MutableDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="index">The <see cref="Int32"/> value
        /// representing the index of the element to remove.</param>
        /// <returns>true if the element was removed; false, otherwise.</returns>
        public bool Remove(int index)
        {
            return base._Remove(index);
        }

        /// <summary>
        /// Clears the <see cref="MutableDictionary{TKey, TValue}"/>.
        /// </summary>
        public void Clear()
        {
            base._Clear();
        }

        public new TValue this[TKey key]
        {
            get
            {
                return base[key];
            }
            set
            {
                base[key] = value;
            }
        }
    }
}
