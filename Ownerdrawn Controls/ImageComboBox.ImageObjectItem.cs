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
    partial class ImageComboBox
    {
        /// <summary>
        /// Provides a base owner drawn item implementation for the
        /// <see cref="ImageComboBox"/>.
        /// </summary>
        public class ImageObjectItem : OwnerDrawnItem<ImageObjectItem, ImageComboBox>, IOwnerDrawnStatedItem
        {
            private string imageKey = "";
            private int imageIndex = -1;
            private bool enabled;

            /// <summary>
            /// Creates a new <see cref="ImageObjectItem"/>
            /// </summary>
            /// <param name="text">The <see cref="String"/> value representing the 
            /// text to display in the <see cref="ImageObjectItem"/>.</param>
            /// <param name="item">The <see cref="Object"/> value relative to the
            /// <see cref="ImageObjectItem"/> that is represtented.</param>
            /// <param name="imageIndex">The <see cref="Int32"/> index
            /// of the image associated to the <see cref="ImageObjectItem"/>.</param>
            public ImageObjectItem(string text = null, object item = null, int imageIndex = -1)
                : base(text, item)
            {
                this.imageIndex = imageIndex;
                this.enabled = true;
            }

            /// <summary>
            /// Creates a new <see cref="ImageObjectItem"/> with the <paramref name="text"/>,
            /// <paramref name="item"/> and <paramref name="imageKey"/> provided.
            /// </summary>
            /// <param name="text">The <see cref="String"/> value representing the 
            /// text to display in the <see cref="ImageObjectItem"/>.</param>
            /// <param name="item">The <see cref="Object"/> value relative to the
            /// <see cref="ImageObjectItem"/> that is represtented.</param>
            /// <param name="imageKey">The <see cref="String"/> value associated to the
            /// <see cref="ImageObjectItem"/> that represents the image list key to use
            /// to obtain the image for the item.</param>
            public ImageObjectItem(string text = null, object item = null, string imageKey = "")
                : base(text, item)
            {
                this.imageKey = imageKey;
                this.enabled = true;
            }

            /// <summary>
            /// Returns the <see cref="Int32"/> value associated to the
            /// <see cref="ImageObjectItem"/> relative to its location within the
            /// <see cref="OwnerDrawnItem{TDrawnTtem, TParent}.Parent"/> list.
            /// </summary>
            public override int Index
            {
                get
                {
                    if (this.Parent == null || this.Parent.items == null)
                        return -1;
                    int index = 0;
                    foreach (object o in this.Parent.items)
                    {
                        if (o == this)
                            return index;
                        index++;
                    }
                    return -1;
                }
            }

            /// <summary>
            /// Returns/sets the <see cref="String"/> value relative to the
            /// key of the image to display along side the <see cref="ImageObjectItem"/>.
            /// </summary>
            [RefreshPropertiesAttribute(RefreshProperties.Repaint)]
            [LocalizableAttribute(true)]
            [DefaultValueAttribute("")]
            [TypeConverterAttribute(typeof(ImageKeyConverter))]
            public string ImageKey
            {
                get
                {
                    return this.imageKey;
                }
                set
                {
                    if (this.imageIndex != -1)
                        this.imageIndex = -1;
                    this.imageKey = value;
                }
            }

            /// <summary>
            /// Returns/sets the <see cref="Int32"/> value relative to the index
            /// of the image to display along side the <see cref="ImageObjectItem"/>.
            /// </summary>
            [TypeConverterAttribute(typeof(ImageIndexConverter))]
            [DefaultValueAttribute(-1)]
            [RefreshPropertiesAttribute(RefreshProperties.Repaint)]
            [LocalizableAttribute(true)]
            public int ImageIndex
            {
                get
                {
                    return this.imageIndex;
                }
                set
                {
                    if (this.imageKey != "")
                        this.imageKey = "";
                    this.imageIndex = value;
                }
            }
            /// <summary>
            /// The Image associated with the Item
            /// </summary>
            /// <value>Image accesses the value of the image data member.</value>
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            [Browsable(false)]
            public override System.Drawing.Bitmap Image
            {
                get
                {
                    if (this.Parent != null)
                    {
                        if (this.imageIndex != -1)
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.Parent.imageList, this.imageIndex).Normal;
                        else if (this.imageKey != null && this.imageKey != "")
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.Parent.imageList, this.imageKey).Normal;
                    }
                    return null;
                }
            }

            /// <summary>
            /// The Item's Shadow Image
            /// </summary>
            /// <value>ShadowImage accesses the value of the imageshadow data member.  Should the value be null but the normalized image not be, it processes the styles of the images and returns.</value>
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            [Browsable(false)]
            public override System.Drawing.Bitmap ShadowImage
            {
                get {
                    if (this.Parent != null)
                    {
                        if (this.imageIndex != -1)
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.Parent.imageList, this.imageIndex).Shadowed;
                        else if (this.imageKey != null && this.imageKey != "")
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.Parent.imageList, this.imageKey).Shadowed;
                    }
                    return null;
                }
            }
            /// <summary>
            /// The Item's Disabled Image
            /// </summary>
            /// <value>DisabledImage accesses the value of the imagedisabled data member.  Should the value be null but the normalized image not be, it processes the styles of the images and returns.</value>
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            [Browsable(false)]
            public override System.Drawing.Bitmap DisabledImage
            {
                get
                {
                    if (this.Parent != null)
                    {
                        if (this.imageIndex != -1)
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.Parent.imageList, this.imageIndex).Disabled;
                        else if (this.imageKey != null && this.imageKey != "")
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.Parent.imageList, this.imageKey).Disabled;
                    }
                    return null;
                }
            }

            /// <summary>
            /// Occurs when an element associated to the <see cref="ImageObjectItem"/> changes.
            /// </summary>
            protected override void OnChange()
            {
                RefreshItem();
            }

            private void RefreshItem()
            {
                if (this.Parent != null && this.Parent.items != null && this.Parent.Created)
                    if (this.Index != -1)
                        this.Parent.RefreshItem(this.Index);
            }
            #region IOwnerDrawnStatedItem Members
            /// <summary>
            /// Returns/sets the state of the 
            /// <see cref="ImageObjectItem"/>.
            /// </summary>
            public bool Enabled
            {
                get
                {
                    return this.enabled;
                }
                set
                {
                    this.enabled = value;
                }
            }

            #endregion

        }
    }
}
