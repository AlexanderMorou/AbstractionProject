using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Defines properties and methods for creating a pass for the 
    /// master dictionary to use.
    /// </summary>
    internal interface _ISubordinateDictionaryMasterPass
    {
        /// <summary>
        /// Used to bypass the controlled state of a subordinate dictionary.
        /// Adds an element to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        void Add(object key, object value);

        /// <summary>
        /// Used to bypass the controlled state of a subordinate dictionary.
        /// Clears the dictionary.
        /// </summary>
        void Clear();

        /// <summary>
        /// Used to bypass the controlled state of a subordinate dictionary.
        /// Removes an element from the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        void Remove(object key);

        /// <summary>
        /// Used to bypass the controlled state of a subordinate dictionary.
        /// Sets the value of an element in the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to set.</param>
        object this[object key] { set; }
    }
}
