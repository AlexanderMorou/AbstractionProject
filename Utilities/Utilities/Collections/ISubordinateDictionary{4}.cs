using System;
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
    /// Defines generic properties and methods for working
    /// with a dictionary that is a subordinate of a 
    /// larger <see cref="IMasterDictionary{TKey, TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of key used in the subordinate
    /// and its master.</typeparam>
    /// <typeparam name="TSValue">The type of value used in the 
    /// current subordinate dictionary implementation.</typeparam>
    /// <typeparam name="TMValue">
    /// The type of value used in the master dictionary.
    /// </typeparam>
    public interface ISubordinateDictionary<TSKey, TMKey, TSValue, TMValue> :
        ISubordinateDictionary<TSKey, TSValue>
        where TSKey :
            TMKey
        where TMValue :
            class
        where TSValue :
            TMValue
    {
        /// <summary>
        /// Returns the <see cref="IMasterDictionary{TKey, TValue}"/> which
        /// contains and moderates the current
        /// <see cref="ISubordinateDictionary{TSKey, TMKey, TSValue, TMValue}"/>.
        /// </summary>
        IMasterDictionary<TMKey, TMValue> Master { get; }
    }
}
