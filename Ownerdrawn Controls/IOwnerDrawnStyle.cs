using System;
using System.Collections.Generic;
using System.Text;
 /*----------------------------------------\
 | Copyright © 2008 Allen Copeland Jr.     |
 |-----------------------------------------|
 | The Abstraction Project's code is prov- |
 | -ided under a contract-release basis.   |
 | DO NOT DISTRIBUTE and do not use beyond |
 | the contract terms.                     |
 \--------------------------------------- */

namespace AllenCopeland.Abstraction.OwnerDrawnControls
{
    /// <summary>
    /// Defines generic properties and methods for working with an owner drawn
    /// item style used to render specific elements of a given owner drawn system.
    /// </summary>
    /// <typeparam name="TDrawnItem">The type of
    /// <see cref="IOwnerDrawnItem{TDrawnItem}"/>
    /// in the current implementation.</typeparam>
    public interface IOwnerDrawnStyle<TDrawnItem>
        where TDrawnItem :
            IOwnerDrawnItem<TDrawnItem>
    {
        /// <summary>
        /// Informs the <see cref="IOwnerDrawnStyle{TDrawnItem}"/> to draw a
        /// specific element as defined within the arguments <paramref name="e"/>.
        /// </summary>
        /// <param name="e">The <see cref="IOwnerDrawnItemDrawEventArgs{TDrawnItem}"/>
        /// which denote the specific item and other elements
        /// associated with the drawing operation to be performed.</param>
        void OnDrawItem(IOwnerDrawnItemDrawEventArgs<TDrawnItem> e);
        /// <summary>
        /// Informs the <see cref="IOwnerDrawnStyle{TDrawnItem}"/> to draw
        /// a specific element as described within the arguments <paramref name="e"/>.
        /// </summary>
        /// <param name="e">The <see cref="IOwnerDrawnItemMeasureEventArgs{TDrawnItem}"/>
        /// which denote the specific item and other elements associated with the 
        /// metric calculation to be performed.</param>
        void OnMeasureItem(ref IOwnerDrawnItemMeasureEventArgs<TDrawnItem> e);
    }
}
