using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Utilities.Collections
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// master dictionary that contains a series of subordinate 
    /// sub-type dictionaries using the same <typeparamref name="TKey"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of key to use in the <see cref="IMasterDictionary{TKey, TValue}"/>.</typeparam>
    /// <typeparam name="TValue">The type of value to use in the 
    /// <see cref="MasterDictionaryEntry{TEntry}"/> values.</typeparam>
    public interface IMasterDictionary<TKey, TValue> :
        IControlledDictionary<TKey, MasterDictionaryEntry<TValue>>
        where TValue :
            class
    {
        /// <summary>
        /// Returns an enumerable entity that contains the 
        /// <see cref="ISubordinateDictionary"/> instances
        /// managed by the current <see cref="IMasterDictionary{TKey, TValue}"/>
        /// </summary>
        IEnumerable<ISubordinateDictionary> Subordinates { get; }
    }
}
