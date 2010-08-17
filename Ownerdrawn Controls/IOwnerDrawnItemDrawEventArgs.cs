using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
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
    /// Defines generic properties and methods for working with
    /// an owner drawn items draw item event's arguments.
    /// </summary>
    /// <typeparam name="TDrawnItem">The type of 
    /// <see cref="IOwnerDrawnItem{TDrawnItem}"/>
    /// in the current implementation.</typeparam>
    public interface IOwnerDrawnItemDrawEventArgs<TDrawnItem>
        where TDrawnItem : 
            IOwnerDrawnItem<TDrawnItem>
    {
        /// <summary>
        /// Returns the <see cref="DrawItemEventArgs"/>
        /// from the original system event.
        /// </summary>
        DrawItemEventArgs OriginalArgs { get; } 
        /// <summary>
        /// The <typeparamref name="TDrawnItem"/> to draw.
        /// </summary>
        TDrawnItem Item { get; }
        /// <summary>
        /// The <see cref="DrawItemState"/> which denotes whether the
        /// <see cref="Item"/> is enabled, disabled, selected and so on.
        /// </summary>
        DrawItemState State { get; }
        /// <summary>
        /// Returns the <see cref="Rectangle"/> that determines the bounds
        /// of the draw operation.
        /// </summary>
        Rectangle Bounds { get; }
        /// <summary>
        /// Returns the <see cref="Graphics"/> instance to which drawing should 
        /// be performed on.
        /// </summary>
        Graphics Graphics { get; }
        /// <summary>
        /// Returns the <see cref="Font"/> which dictates the format the text of the
        /// <see cref="Item"/> should be drawn in.
        /// </summary>
        Font Font { get; }
        /// <summary>
        /// Returns whether the <see cref="Item"/> is a top-level element.
        /// </summary>
        bool TopLevel { get; }
    }
}
