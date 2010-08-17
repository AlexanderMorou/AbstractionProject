using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
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
    /// Provides a base class for owner drawn elements.
    /// </summary>
    /// <typeparam name="TDrawnItem">Thek ind of <see cref="IOwnerDrawnItem"/>
    /// in the current implementation.</typeparam>
    /// <typeparam name="TParent">The kind of <see cref="IOwnerDrawn{TDrawnItem, TParent}"/> in the current
    /// implementation which contains the <typeparamref name="TDrawnItem"/>
    /// elements.</typeparam>
    public abstract class OwnerDrawnItem<TDrawnItem, TParent> : IOwnerDrawnItem<TDrawnItem, TParent> 
        where TDrawnItem :
            OwnerDrawnItem<TDrawnItem, TParent>
        where TParent :
            class,
            IOwnerDrawn<TDrawnItem, TParent>
    {
        private string text = null;
        private object item = null;
        private TParent parent;
        /// <summary>
        /// Returns the <see cref="Int32"/> value associated to the
        /// <see cref="OwnerDrawnItem{TDrawnItem, TParent}"/> which designates
        /// the ordinal index of the item relative to its siblings within
        /// <typeparamref name="TParent"/>.
        /// </summary>
        public abstract int Index { get; }
        internal void link(TParent parent)
        {
            if (this.parent != null && parent != this.parent)
                return;
            this.parent = parent;
        }
        /// <summary>
        /// The Image associated with the Item
        /// </summary>
        /// <value>Image accesses the value of the image data member.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public abstract System.Drawing.Bitmap Image
        {
            get;
        }

        /// <summary>
        /// The Item's Shadow Image
        /// </summary>
        /// <value>ShadowImage accesses the value of the imageshadow data member.  Should the value be null but the normalized image not be, it processes the styles of the images and returns.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public abstract System.Drawing.Bitmap ShadowImage
        {
            get;
        }
        /// <summary>
        /// The Item's Disabled Image
        /// </summary>
        /// <value>DisabledImage accesses the value of the imagedisabled data member.  Should the value be null but the normalized image not be, it processes the styles of the images and returns.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public abstract System.Drawing.Bitmap DisabledImage
        {
            get;
        }

        /// <summary>
        /// Craetes a new <see cref="OwnerDrawnItem{TDrawnItem, TParent}"/> with the
        /// <paramref name="text"/> and <paramref name="item"/> provided.
        /// </summary>
        /// <param name="text">The <see cref="String"/> value representing the
        /// text to display on the <see cref="OwnerDrawnItem{TDrawnItem, TParent}"/></param>
        /// <param name="item">The <see cref="Object"/> relative to the 
        /// item that the <see cref="OwnerDrawnItem{TDrawnItem, TParent}"/> exists
        /// to represent.</param>
        public OwnerDrawnItem(string text = null, object item = null)
        {
            this.text = text;
            this.item = item;
        }
        /// <summary>
        /// Returns/sets the <see cref="String"/> value representing the
        /// text to display on the <see cref="OwnerDrawnItem{TDrawnItem, TParent}"/>.
        /// </summary>
        [DefaultValue(null)]
        public virtual string Text
        {
            get
            {
                if (this.text == null && this.item != null)
                    return this.item.ToString();
                if (this.text == null)
                    return "";
                return this.text;
            }
            set
            {
                OnSetText(value);
                OnChange();
            }
        }

        /// <summary>
        /// Occurs when the text of the <see cref="OwnerDrawnItem{TDrawnItem, TParent}"/>
        /// is changed and the visible state of the object should not be changed.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value representing the
        /// text to display on the <see cref="OwnerDrawnItem{TDrawnItem, TParent}"/>.</param>
        /// <remarks>Intended for use by initializers.</remarks>
        protected void OnSetText(string value)
        {
            this.text = value;
        }

        /// <summary>
        /// Returns the <typeparamref name="TParent"/> which contains the
        /// <see cref="OwnerDrawnItem{TDrawnItem, TParent}"/>.
        /// </summary>
        public TParent Parent
        {
            get
            {
                return this.parent;
            }
        }

        /// <summary>
        /// Returns the <see cref="Object"/> relative to the 
        /// item that the <see cref="OwnerDrawnItem{TDrawnItem, TParent}"/> exists
        /// to represent.
        /// </summary>
        [Browsable(false)]
        public object Item
        {
            get
            {
                return this.item;
            }
            set
            {
                OnSetItem(value);
                OnChange();
            }
        }

        /// <summary>
        /// Occurs when the item relative to the element is changed
        /// and the visible state of the object should not be updated.
        /// </summary>
        /// <param name="value">The <see cref="Object"/> relative to the
        /// item that the <see cref="OwnerDrawnItem{TDrawnItem, TParent}"/> exists
        /// to represent.</param>
        /// <remarks>Intended for use by initializers.</remarks>
        protected void OnSetItem(object value)
        {
            this.item = value;
        }

        /// <summary>
        /// Converts the <see cref="OwnerDrawnItem{TDrawnItem, TParent}"/> to a
        /// <see cref="String"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> value representing the 
        /// <see cref="OwnerDrawnItem{TDrawnItem, TParent}"/>.</returns>
        public override string ToString()
        {
            return this.Text;
        }
        /// <summary>
        /// Occurs when the item changes.
        /// </summary>
        protected abstract void OnChange();

        #region IOwnerDrawnItem<object> Members

        object IOwnerDrawnItem.Parent
        {
            get
            {
                return this.parent;
            }
        }

        #endregion
        
    }
}
