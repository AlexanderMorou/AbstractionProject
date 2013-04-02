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
    /// Defines properties and methods for working
    /// with a dictionary that is a subordinate of a 
    /// larger <see cref="IMasterDictionary"/>.
    /// </summary>
    public interface ISubordinateDictionary :
        IControlledDictionary
    {
        /// <summary>
        /// Returns the <see cref="IMasterDictionary"/> which
        /// contains and moderates the current
        /// <see cref="ISubordinateDictionary"/>.
        /// </summary>
        IMasterDictionary Master { get; }
    }
}
