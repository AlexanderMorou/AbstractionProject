using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Forms;
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
    /// Provides a <see cref="Collection{T}"/> of <see cref="ImageMenuItem"/> instances.
    /// </summary>
    public class ImageMenuItemCollection : Collection<ImageMenuItem>
    {
        private IOwnerDrawnMenu<ImageMenuItem> owner;
        /// <summary>
        /// Creates a new <see cref="ImageMenuItemCollection"/> with the <paramref name="owner"/> 
        /// provided.
        /// </summary>
        /// <param name="owner">The <see cref="IOwnerDrawnMenu{TDrawnItem}"/>
        /// which contains the <see cref="ImageMenuItemCollection"/>.</param>
        public ImageMenuItemCollection(IOwnerDrawnMenu<ImageMenuItem> owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// Inserts an <paramref name="item"/> at the <paramref name="index"/>
        /// provided.
        /// </summary>
        /// <param name="index">The <see cref="Int32"/> ordinal index of the <paramref name="item"/>
        /// to add.</param>
        /// <param name="item">The <see cref="ImageMenuItem"/> to insert.</param>
        protected override void InsertItem(int index, ImageMenuItem item)
        {
            if ((this.owner != null) && (this.owner.MenuItems != null))
                this.owner.MenuItems.Add(index, item);
            base.InsertItem(index, item);
        }
        /// <summary>
        /// Clears the <see cref="ImageMenuItemCollection"/>.
        /// </summary>
        protected override void ClearItems()
        {
            if ((this.owner != null) && (this.owner.MenuItems != null))
                this.owner.MenuItems.Clear();
            base.ClearItems();
        }

        /// <summary>
        /// Sets an <paramref name="item"/> by the <paramref name="index"/> 
        /// provided.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <exception cref="System.NotSupportedException">this method is not supported.</exception>
        protected override void SetItem(int index, ImageMenuItem item)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Removes an <see cref="ImageMenuItem"/> at the <paramref name="index"/>
        /// provided.
        /// </summary>
        /// <param name="index">The <see cref="Int32"/> ordinal index of the
        /// <see cref="ImageMenuItem"/> to remove.</param>
        protected override void RemoveItem(int index)
        {
            ImageMenuItem item = this[index];
            if (this.owner != null && this.owner.MenuItems != null)
                if (this.owner.MenuItems.Contains(item))
                    this.owner.MenuItems.Remove(item);
            item = null;
            base.RemoveItem(index);
        }

        /// <summary>
        /// Adds a series of <paramref name="items"/>.
        /// </summary>
        /// <param name="items">The <see cref="ImageMenuItem"/> series to add.</param>
        public void AddRange(ImageMenuItem[] items)
        {
            if (items == null)
                return;
            int indexBase = this.Items.Count;
            foreach (ImageMenuItem imi in items)
                this.InsertItem(indexBase++,imi);
        }
    }
}
