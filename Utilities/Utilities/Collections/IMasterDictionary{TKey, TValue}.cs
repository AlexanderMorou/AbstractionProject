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
        IControlledStateDictionary<TKey, MasterDictionaryEntry<TValue>>
        where TValue :
            class
    {
        /// <summary>
        /// Obtains a <see cref="ISubordinateDictionary{TSKey, TMKey, TSValue, TMValue}"/> dictionary at the given index.
        /// </summary>
        /// <typeparam name="TSValue">The specific type of value used in the subordinate dictionary, derives
        /// from the master dictionary's <typeparamref name="TValue"/>.</typeparam>
        /// <returns>A <see cref="ISubordinateDictionary{TSKey, TMKey, TSValue, TMValue}"/> contained 
        /// within the <see cref="IMasterDictionary{TKey, TValue}"/></returns>
        ISubordinateDictionary<TSKey, TKey, TSValue, TValue> GetSubordinate<TSKey, TSValue>()
            where TSKey :
                TKey
            where TSValue :
                TValue;
        /// <summary>
        /// Returns an enumerable entity that contains the 
        /// <see cref="ISubordinateDictionary"/> instances
        /// managed by the current <see cref="IMasterDictionary{TKey, TValue}"/>
        /// </summary>
        IEnumerable<ISubordinateDictionary> Subordinates { get; }
    }
}
