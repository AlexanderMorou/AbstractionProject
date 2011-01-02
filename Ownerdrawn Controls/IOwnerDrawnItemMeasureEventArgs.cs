using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
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
    /// Defines generic properties and methods for working with an owner drawn item
    /// measure event arguments.
    /// </summary>
    /// <typeparam name="TDrawnItem">The type of <see cref="IOwnerDrawnItem{TDrawnItem}"/> in
    /// the current implementation.</typeparam>
    public interface IOwnerDrawnItemMeasureEventArgs<TDrawnItem>
        where TDrawnItem :
            IOwnerDrawnItem<TDrawnItem>
    {
        /// <summary>
        /// Returns the <typeparamref name="TDrawnItem"/> to be drawn.
        /// </summary>
        TDrawnItem Item { get; }
        /// <summary>
        /// Represents the <see cref="Item"/> height associated to the draw event.
        /// </summary>
        int Height { get; set;}
        /// <summary>
        /// Represents the <see cref="Item"/> width associated to the draw event.
        /// </summary>
        int Width { get; set;}
        /// <summary>
        /// Represents the <see cref="Graphics"/> object on which to perform drawing
        /// operations.
        /// </summary>
        Graphics Graphics { get; }
        /// <summary>
        /// Returns the <see cref="Font"/> in which to render the text of the item.
        /// </summary>
        Font Font { get; }
        /// <summary>
        /// Returns whether the item is a top-level item.
        /// </summary>
        bool TopLevel { get; }
    }
}
