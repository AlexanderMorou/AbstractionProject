using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Data;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    /// <summary>
    /// Provides a read-only dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of key used in the
    /// <see cref="ReadOnlyDictionary{TKey, TValue}"/>.</typeparam>
    /// <typeparam name="TValue">
    /// The type of value used in the <see cref="ReadOnlyDictionary{TKey, TValue}"/>.
    /// </typeparam>
    public class ReadOnlyDictionary<TKey, TValue> :
        ControlledStateDictionary<TKey, TValue>,
        IReadOnlyDictionary<TKey, TValue>,
        IReadOnlyDictionary
    {
        /// <summary>
        /// Creates a new instance of <see cref="ReadOnlyDictionary{TKey, TValue}"/>.
        /// </summary>
        public ReadOnlyDictionary()
            : base()
        {
        }
        /// <summary>
        /// Creates a new instance of <see cref="ReadOnlyDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="dictionaryCopy">The <see cref="Dictionary{TKey, TValue}"/> to
        /// wrap.</param>
        public ReadOnlyDictionary(Dictionary<TKey, TValue> dictionaryCopy)
            : base(dictionaryCopy)
        {
        }

        /// <summary>
        /// Removes the element under the provided <paramref name="key"/>
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> of the element to remove.</param>
        /// <exception cref="System.Data.ReadOnlyException">Remove not allowed on a read-only collection.</exception>
        protected override bool RemoveImpl(TKey key)
        {
            throw new ReadOnlyException(string.Format("ReadOnlyCollection<{0}, {1}> is read-only", typeof(TKey).Name, typeof(TValue).Name));
        }

        /// <summary>
        /// Removes the item at the provided <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based index which designates the <see cref="KeyValuePair{TKey, TItem}"/> to 
        /// remove from the <see cref="ReadOnlyDictionary{TKey, TValue}"/></param>
        /// <exception cref="System.Data.ReadOnlyException">Remove not allowed on a read-only collection.</exception>
        protected override bool RemoveImpl(int index)
        {
            throw new ReadOnlyException(string.Format("ReadOnlyCollection<{0}, {1}> is read-only", typeof(TKey).Name, typeof(TValue).Name));
        }
        /// <summary>
        /// Adds an element with the given <paramref name="key"/> and <paramref name="value"/>.
        /// </summary>
        /// <param name="key">The lookup key from which the <paramref name="value"/> is referred to
        /// later.</param>
        /// <param name="value">The value of the element.</param>
        /// <exception cref="System.Data.ReadOnlyException">Remove not allowed on a read-only collection.</exception>
        protected override void Add(TKey key, TValue value)
        {
            throw new ReadOnlyException(string.Format("ReadOnlyCollection<{0}, {1}> is read-only", typeof(TKey).Name, typeof(TValue).Name));
        }
    }
}
