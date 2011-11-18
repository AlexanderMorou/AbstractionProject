using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Provides a base implementation of <see cref="IOwnerDrawnItemMeasureEventArgs{TDrawnItem}"/> which
    /// defines generic properties and methods for working with an owner drawn item
    /// measure event arguments.
    /// </summary>
    /// <typeparam name="TDrawnItem">The type of <see cref="IOwnerDrawnItem{TDrawnItem}"/>
    /// in the current implementation.</typeparam>
    public class OwnerDrawnItemMeasureEventArgs<TDrawnItem> : IOwnerDrawnItemMeasureEventArgs<TDrawnItem>
        where TDrawnItem :
            IOwnerDrawnItem<TDrawnItem>
    {
        private int height, width = 0;
        private Graphics graphics;
        private Font font;
        private TDrawnItem item;
        private bool topLevel;
        /// <summary>
        /// Creates a new <see cref="OwnerDrawnItemMeasureEventArgs{TDrawnItem}"/> with the
        /// <paramref name="height"/>, <paramref name="graphics"/>, <paramref name="item"/>, <paramref name="font"/>,
        /// <paramref name="topLevel"/> provided.
        /// </summary>
        /// <param name="height">The <see cref="Int32"/> value associated to the <typeparamref name="TDrawnItem"/>
        /// in its original state.</param>
        /// <param name="graphics">The <see cref="Graphics"/> instance to which drawing should 
        /// be performed on.</param>
        /// <param name="item">The <typeparamref name="TDrawnItem"/> to draw.</param>
        /// <param name="font">The <see cref="Font"/> which dictates the format the text of the
        /// <see cref="Item"/> should be drawn in.</param>
        /// <param name="topLevel">Whether the <see cref="Item"/> is a top-level element; false by default.</param>
        public OwnerDrawnItemMeasureEventArgs(int height, Graphics graphics, TDrawnItem item, Font font, bool topLevel = false)
        {
            this.item = item;
            this.height = height;
            this.font = font;
            this.graphics = graphics;
            this.topLevel = topLevel;
        }
        #region IOwnerDrawnItemMeasureEventArgs<TItem,TDrawnItem> Members

        /// <summary>
        /// Returns the <typeparamref name="TDrawnItem"/> to be drawn.
        /// </summary>
        public TDrawnItem Item
        {
            get { return item; }
        }

        /// <summary>
        /// Returns whether the item is a top-level item.
        /// </summary>
        public bool TopLevel
        {
            get
            {
                return this.topLevel;
            }
        }
        /// <summary>
        /// Returns/sets the <see cref="Item"/> height associated to the metric calculation event.
        /// </summary>
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                this.height = value;
            }
        }

        /// <summary>
        /// Returns/sets the <see cref="Item"/> width associated to the metric calculation event.
        /// </summary>
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                this.width = value;
            }
        }

        /// <summary>
        /// Represents the <see cref="Graphics"/> object on which to perform metric
        /// calculations.
        /// </summary>
        public Graphics Graphics
        {
            get { return graphics; }
        }

        /// <summary>
        /// Returns the <see cref="Font"/> in which to perform metric calculations on the text of the item.
        /// </summary>
        public Font Font
        {
            get
            {
                return this.font;
            }
        }
        #endregion
    }
}
