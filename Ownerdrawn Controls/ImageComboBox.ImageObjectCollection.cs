using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
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
    partial class ImageComboBox
    {
        /// <summary>
        /// Provides a <see cref="Collection{T}"/> of
        /// <see cref="ImageObjectItem"/> instances.
        /// </summary>
        public class ImageObjectCollection : Collection<ImageObjectItem>
        {

            private ImageComboBox owner;
            /// <summary>
            /// Creates a new <see cref="ImageObjectCollection"/> with the
            /// <paramref name="owner"/> provided.
            /// </summary>
            /// <param name="owner">The <see cref="ImageComboBox"/> which
            /// contains the series of <see cref="ImageObjectItem"/> instances.</param>
            public ImageObjectCollection(ImageComboBox owner)
                : base()
            {
                this.owner = owner;
            }
            /// <summary>
            /// Inserts a <see cref="ImageObjectItem"/> at the 
            /// <paramref name="index"/> provided.
            /// </summary>
            /// <param name="index">The <see cref="Int32"/> value associated to the
            /// <paramref name="item"/>.</param>
            /// <param name="item">The <see cref="ImageObjectItem"/> to insert.</param>
            protected override void InsertItem(int index, ImageObjectItem item)
            {
                item.link(owner);
                owner.baseItems.Insert(index, item);
                base.InsertItem(index, item);
            }
            /// <summary>
            /// Clears the <see cref="ImageObjectCollection"/>.
            /// </summary>
            protected override void ClearItems()
            {
                if (owner != null)
                    owner.baseItems.Clear();
                base.ClearItems();
            }
            /// <summary>
            /// Sets an <paramref name="item"/> at the 
            /// <paramref name="index"/> provided.
            /// </summary>
            /// <param name="index">The <see cref="Int32"/> ordinal index associated to the
            /// <paramref name="item"/>.</param>
            /// <param name="item">The <see cref="ImageObjectItem"/> to set.</param>
            protected override void SetItem(int index, ImageObjectItem item)
            {
                if (this.owner != null)
                    this.owner.baseItems[index] = item;
                base.SetItem(index, item);
            }
            /// <summary>
            /// Removes the <see cref="ImageObjectItem"/> at the
            /// <paramref name="index"/> provided.
            /// </summary>
            /// <param name="index">The <see cref="Int32"/> ordinal index
            /// of the <see cref="ImageObjectItem"/> to remove.</param>
            protected override void RemoveItem(int index)
            {
                if (owner != null)
                    owner.baseItems.Remove(this[index]);
                base.RemoveItem(index);
            }

        }
    }
}
