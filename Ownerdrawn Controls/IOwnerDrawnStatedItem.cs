using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.OwnerDrawnControls
{
    /// <summary>
    /// Defines properties and methods for working with a
    /// stated item which is owner drawn.
    /// </summary>
    public interface IOwnerDrawnStatedItem
    {
        /// <summary>
        /// Returns/sets the state of the 
        /// <see cref="IOwnerDrawnStatedItem"/>.
        /// </summary>
        bool Enabled { get; set; }
    }
}
