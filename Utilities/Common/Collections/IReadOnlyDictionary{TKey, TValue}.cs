using System;
using System.Collections.Generic;
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
    /// Defines properties and methods for a generic read-only dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of key used in the
    /// <see cref="IReadOnlyDictionary{TKey, TValue}"/>.</typeparam>
    /// <typeparam name="TValue">
    /// The type of value used in the <see cref="IReadOnlyDictionary{TKey, TValue}"/>.
    /// </typeparam>
    public interface IReadOnlyDictionary<TKey, TValue> :
        IControlledDictionary<TKey, TValue>,
        IControlledCollection<KeyValuePair<TKey, TValue>>
    {

    }
}
