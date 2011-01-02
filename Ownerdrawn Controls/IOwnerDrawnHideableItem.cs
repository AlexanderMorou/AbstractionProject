using System;
using System.Collections.Generic;
using System.Text;
 /*----------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.     |
 |-----------------------------------------|
 | The Abstraction Project's code is prov- |
 | -ided under a contract-release basis.   |
 | DO NOT DISTRIBUTE and do not use beyond |
 | the contract terms.                     |
 \--------------------------------------- */

namespace AllenCopeland.Abstraction.OwnerDrawnControls
{
    /// <summary>
    /// Defines properties and methods for working with an
    /// owner drawn element with variable visibility.
    /// </summary>
    public interface IOwnerDrawnHideableItem : IOwnerDrawnItem
    {
        /// <summary>
        /// Returns/sets whether the <see cref="IOwnerDrawnHideableItem"/> is visible.
        /// </summary>
        bool Visible { get; set; }
    }
}
