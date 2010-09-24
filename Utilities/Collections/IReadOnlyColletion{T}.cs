using System;
using System.Collections.Generic;
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
    /// Defines properties and methods for working with 
    /// a generic read-only collection.
    /// </summary>
    /// <typeparam name="T">The type of element used 
    /// in the <see cref="IReadOnlyCollection"/></typeparam>
    public interface IReadOnlyCollection<T> :
        IControlledStateCollection<T>
    {
    }
}
