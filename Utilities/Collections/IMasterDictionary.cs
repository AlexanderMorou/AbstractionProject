using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Utilities.Collections
{
    public interface IMasterDictionary :
        IDictionary
    {
        /// <summary>
        /// Returns an enumerable entity that contains the 
        /// <see cref="ISubordinateDictionary"/> instances
        /// managed by the current <see cref="IMasterDictionary"/>
        /// </summary>
        IEnumerable Subordinates { get; }
    }
}
