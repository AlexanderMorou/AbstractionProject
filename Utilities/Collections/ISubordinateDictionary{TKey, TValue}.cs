using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
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
    /// <typeparam name="TValue">
    /// The type of value used in the subordinate dictionary.
    /// </typeparam>
    /// <remarks>At this level of generic the type of the 
    /// master elements is not defined.</remarks>
    public interface ISubordinateDictionary<TKey, TValue> :
        IControlledStateDictionary<TKey, TValue>
    {
    }
}
