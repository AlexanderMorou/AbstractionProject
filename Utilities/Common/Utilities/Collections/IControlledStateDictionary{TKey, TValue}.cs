using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    /// <summary>
    /// Defines properties and methods for working with a generic dictionary whose keys and values
    /// are tightly controlled.
    /// </summary>
    /// <typeparam name="TKey">The type of element used as a key.</typeparam>
    /// <typeparam name="TValue">The type of element used as the values associated to the keys.</typeparam>
    public interface IControlledDictionary<TKey, TValue> :
        IControlledCollection<KeyValuePair<TKey, TValue>>
    {
        /// <summary>
        /// Gets a <see cref="IControlledCollection{T}"/> containing the 
        /// <see cref="IControlledDictionary{TKey, TValue}"/>'s keys.
        /// </summary>
        /// <returns>
        /// A <see cref="IControlledCollection{T}"/> with the keys of the 
        /// <see cref="IControlledDictionary{TKey, TValue}"/>.
        /// </returns>
        IControlledCollection<TKey> Keys { get; }
        /// <summary>
        /// Gets a <see cref="IControlledCollection{T}"/> containing the 
        /// <see cref="IControlledDictionary{TKey, TValue}"/>'s values.
        /// </summary>
        /// <returns>
        /// A <see cref="IControlledCollection{T}"/> with the values of the 
        /// <see cref="IControlledDictionary{TKey, TValue}"/>.
        /// </returns>
        IControlledCollection<TValue> Values { get; }

        /// <summary>
        /// Returns the element of the <see cref="IControlledDictionary{TKey, TValue}"/> with the 
        /// given <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The <typeparamref name="TKey"/> of the element to get.</param>
        /// <returns>The element with the specified <paramref name="key"/>.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">
        /// There was no element in the <see cref="IControlledDictionary{TKey, TValue}"/> 
        /// containing the <paramref name="key"/> provided.
        /// </exception>
        TValue this[TKey key] { get; }

        /// <summary>
        /// Determines whether the <see cref="IControlledDictionary{TKey, TValue}"/> contains 
        /// an element with the specified key.
        /// </summary>
        /// <param name="key">
        /// The <typeparamref name="TKey"/> to search for in the 
        /// <see cref="IControlledDictionary{TKey, TValue}"/>.
        /// </param>
        /// <returns>
        /// true, if the <see cref="IControlledDictionary{TKey, TValue}"/> contains an element 
        /// with the <paramref name="key"/>; false, otherwise.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="key"/> is null.
        /// </exception>
        bool ContainsKey(TKey key);
        /// <summary>
        /// Tries to obtain a value from the <see cref="IControlledDictionary{TKey, TValue}"/>
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="IControlledDictionary{TKey, TValue}"/></param>
        /// <param name="value">The value to return, if successful.</param>
        /// <returns>True, if the the element at <paramref name="key"/> is found; false otherwise.</returns>
        bool TryGetValue(TKey key, out TValue value);
    }
}
