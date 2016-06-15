using System;
using System.Collections;
using System.Collections.Generic;
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
    /// Defines properties and methods for working with a dictionary whose keys and values
    /// are tightly controlled.
    /// </summary>
    public interface IControlledDictionary :
        IControlledCollection
    {

        /// <summary>
        /// Gets a <see cref="IControlledCollection"/> containing the 
        /// <see cref="IControlledDictionary"/>'s keys.
        /// </summary>
        /// <returns>
        /// A <see cref="IControlledCollection"/> with the keys of the 
        /// <see cref="IControlledDictionary"/>.
        /// </returns>
        IControlledCollection Keys { get; }

        /// <summary>
        /// Gets a <see cref="IControlledCollection"/> containing the 
        /// <see cref="IControlledDictionary"/>'s values.
        /// </summary>
        /// <returns>
        /// A <see cref="IControlledCollection"/> with the values of the 
        /// <see cref="IControlledDictionary"/>.
        /// </returns>
        IControlledCollection Values { get; }

        /// <summary>
        /// Returns the element of the <see cref="IControlledDictionary"/> with the 
        /// given <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key of the element to get.</param>
        /// <returns>The element with the specified <paramref name="key"/>.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">
        /// There was no element in the <see cref="IControlledDictionary"/> 
        /// containing the <paramref name="key"/> provided.
        /// </exception>
        object this[object key] { get; }

        /// <summary>
        /// Determines whether the <see cref="IControlledDictionary"/> contains 
        /// an element with the specified key.
        /// </summary>
        /// <param name="key">
        /// The key to search for in the 
        /// <see cref="IControlledDictionary"/>.
        /// </param>
        /// <returns>
        /// true, if the <see cref="IControlledDictionary"/> contains an element 
        /// with the <paramref name="key"/>; false, otherwise.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="key"/> is null.
        /// </exception>
        bool ContainsKey(object key);

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="IControlledDictionary"/>.
        /// </summary>
        /// <returns>A <see cref="IDictionaryEnumerator"/> that can be used to iterate through
        /// the <see cref="IControlledDictionary"/>.</returns>
        new IDictionaryEnumerator GetEnumerator();

    }
}
