using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright � 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    /// <summary>
    /// Defines properties and methods for working with 
    /// a read-only collection.
    /// </summary>
    public interface IReadOnlyCollection :
        IControlledStateCollection
    {
    }
}