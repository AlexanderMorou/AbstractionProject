using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
 /*----------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.     |
 |-----------------------------------------|
 | The Abstraction Project's code is prov- |
 | -ided under a contract-release basis.   |
 | DO NOT DISTRIBUTE and do not use beyond |
 | the contract terms.                     |
 \--------------------------------------- */

namespace AllenCopeland.Abstraction.OwnerDrawnControls
{
    /// <summary>
    /// Provides a base implementation of <see cref="IOwnerDrawnItemDrawEventArgs{TDrawnItem}"/>
    /// which defines generic properties and methods for working with
    /// an owner drawn items draw item event's arguments.
    /// </summary>
    /// <typeparam name="TDrawnItem">The type of 
    /// <see cref="IOwnerDrawnItem{TDrawnItem}"/>
    /// in the current implementation.</typeparam>
    public class OwnerDrawnItemDrawEventArgs<TDrawnItem> : IOwnerDrawnItemDrawEventArgs<TDrawnItem>
        where TDrawnItem :
            IOwnerDrawnItem<TDrawnItem>
    {
        private DrawItemEventArgs originalArgs;
        private DrawItemState state;
        private Rectangle bounds;
        private Graphics graphics;
        private TDrawnItem item;
        private Font font;
        private bool topLevel;

        /// <summary>
        /// Creates a new <see cref="OwnerDrawnItemDrawEventArgs{TDrawnItem}"/>
        /// with the <paramref name="original"/>, <paramref name="bounds"/>, <paramref name="graphics"/>
        /// <paramref name="item"/>, <paramref name="font"/> and <paramref name="topLevel"/> provided.
        /// </summary>
        /// <param name="original">The <see cref="DrawItemEventArgs"/> from which the 
        /// <see cref="OwnerDrawnItemDrawEventArgs{TDrawnItem}"/> is sourced.</param>
        /// <param name="bounds">The <see cref="Rectangle"/> that determines the bounds
        /// of the draw operation.</param>
        /// <param name="graphics">The <see cref="Graphics"/> instance to which drawing should 
        /// be performed on.</param>
        /// <param name="item">The <typeparamref name="TDrawnItem"/> to draw.</param>
        /// <param name="font">The <see cref="Font"/> which dictates the format the text of the
        /// <see cref="Item"/> should be drawn in.</param>
        /// <param name="topLevel">Whether the <see cref="Item"/> is a top-level element; false by default.</param>
        [DebuggerHidden]
        public OwnerDrawnItemDrawEventArgs(DrawItemEventArgs original, Rectangle bounds, Graphics graphics, TDrawnItem item, Font font, bool topLevel = false)
        {
            this.state = original.State;
            this.originalArgs = original;
            this.bounds = bounds;
            this.graphics = graphics;
            this.item = item;
            this.font = font;
            this.topLevel = topLevel;
        }

        #region IOwnerDrawnItemEventArgs<TItem,TDrawnItem> Members

        /// <summary>
        /// Returns the <see cref="Font"/> which dictates the format the text of the
        /// <see cref="Item"/> should be drawn in.
        /// </summary>
        public Font Font
        {
            [DebuggerHidden]
            get
            {
                return this.font;
            }
        }
        /// <summary>
        /// The <see cref="DrawItemState"/> which denotes whether the
        /// <see cref="Item"/> is enabled, disabled, selected and so on.
        /// </summary>
        public DrawItemState State
        {
            [DebuggerHidden]
            get { return state; }
        }

        /// <summary>
        /// The <typeparamref name="TDrawnItem"/> to draw.
        /// </summary>
        public TDrawnItem Item
        {
            [DebuggerHidden]
            get { return item; }
        }

        /// <summary>
        /// Returns the <see cref="Rectangle"/> that determines the bounds
        /// of the draw operation.
        /// </summary>
        public Rectangle Bounds
        {
            [DebuggerHidden]
            get { return bounds; }
        }

        /// <summary>
        /// Returns the <see cref="Graphics"/> instance to which drawing should 
        /// be performed on.
        /// </summary>
        public Graphics Graphics
        {
            [DebuggerHidden]
            get { return graphics; }
        }

        /// <summary>
        /// Returns whether the <see cref="Item"/> is a top-level element.
        /// </summary>
        public bool TopLevel
        {
            [DebuggerHidden]
            get
            {
                return this.topLevel;
            }
        }

        /// <summary>
        /// Returns the <see cref="DrawItemEventArgs"/>
        /// from the original system event.
        /// </summary>
        public DrawItemEventArgs OriginalArgs
        {
            [DebuggerHidden]
            get
            {
                return this.originalArgs;
            }
        }
        #endregion
    }
}
