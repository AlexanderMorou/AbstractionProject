using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
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
    partial class ImageCheckedListBox
    {
        /// <summary>
        /// Provides a base owner drawn item implementation for the
        /// <see cref="ImageCheckedListBox"/>.
        /// </summary>
        public class ImageObjectItem : 
            OwnerDrawnItem<ImageObjectItem, ImageCheckedListBox>,
            IOwnerDrawnCheckBoxItem<ImageObjectItem, ImageCheckedListBox>,
            IOwnerDrawnStatedItem
        {
            private bool enabled;
            private string imagecheckedKey = "";
            private int imagecheckedIndex = -1;
            private string imageKey = "";
            private bool isChecked;
            private int imageIndex = -1;
            private bool checkAreaHovered = false;
            private System.Drawing.Bitmap looseImage = null;
            private System.Drawing.Bitmap looseCheckedImage = null;

            public ImageObjectItem()
                : this(null, null, -1)
            {

            }

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
            /// Returns/sets the <see cref="Bitmap"/> associated to the
            /// <see cref="ImageObjectItem"/> in a loose-image form.
            /// </summary>
            [DefaultValue(null)]
            public Bitmap LooseImage
            {
                get
                {
                    return this.looseImage;
                }
                set
                {
                    this.looseImage = value;
                }
            }
            /// <summary>
            /// Returns/sets the checked <see cref="Bitmap"/> associated to the
            /// <see cref="ImageObjectItem"/> in a loose-image form.
            /// </summary>
            [DefaultValue(null)]
            public Bitmap LooseCheckedImage
            {
                get
                {
                    return this.looseCheckedImage;
                }
                set
                {
                    this.looseCheckedImage = value;
                }
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
            /// key of the checked image to display along side the 
            /// <see cref="ImageObjectItem"/>.
            /// </summary>
            [RefreshPropertiesAttribute(RefreshProperties.Repaint)]
            [LocalizableAttribute(true)]
            [DefaultValueAttribute("")]
            [TypeConverterAttribute(typeof(ImageKeyConverter))]
            public string CheckedImageKey
            {
                get
                {
                    return this.imagecheckedKey;
                }
                set
                {
                    if (this.imagecheckedIndex != -1)
                        this.imagecheckedIndex = -1;
                    this.imagecheckedKey = value;
                }
            }
            /// <summary>
            /// Returns/sets the <see cref="Int32"/> value relative to the index
            /// of the checked image to display along side the 
            /// <see cref="ImageObjectItem"/>.
            /// </summary>
            [TypeConverterAttribute(typeof(ImageIndexConverter))]
            [DefaultValueAttribute(-1)]
            [RefreshPropertiesAttribute(RefreshProperties.Repaint)]
            [LocalizableAttribute(true)]
            public int CheckedImageIndex
            {
                get
                {
                    return this.imagecheckedIndex;
                }
                set
                {
                    if (this.imagecheckedKey != "")
                        this.imagecheckedKey = "";
                    this.imagecheckedIndex = value;
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
                    OnChange();
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
                    OnChange();
                }
            }
            /// <summary>
            /// Occurs when an element associated to the <see cref="ImageObjectItem"/> changes.
            /// </summary>
            protected override void OnChange()
            {
                RefreshItem();
                RedrawItem();
            }
            private void RefreshItem()
            {
                if (this.Parent != null && this.Parent.items != null && this.Parent.Created)
                    if (this.Index != -1)
                        this.Parent.RefreshItem(this.Index);
            }
            private void RedrawItem()
            {
                if (this.Parent != null && this.Parent.items != null && this.Parent.Created)
                    if (this.Index != -1)
                        this.Parent.RedrawItem(this.Index);
            }
            #region IOwnerDrawnItem<object,ImageObjectItem> Members
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
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.Parent.ImageList, this.imageIndex).Normal;
                        else if (this.imageKey != null && this.imageKey != "")
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.Parent.ImageList, this.imageKey).Normal;
                        else if (this.looseImage != null)
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.looseImage, this.LooseTransparencyColor).Normal;
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
                get
                {
                    if (this.Parent != null)
                    {
                        if (this.imageIndex != -1)
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.Parent.ImageList, this.imageIndex).Shadowed;
                        else if (this.imageKey != null && this.imageKey != "")
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.Parent.ImageList, this.imageKey).Shadowed;
                        else if (this.looseImage != null)
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.looseImage, this.LooseTransparencyColor).Shadowed;
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
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.Parent.ImageList, this.imageIndex).Disabled;
                        else if (this.imageKey != null && this.imageKey != "")
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.Parent.ImageList, this.imageKey).Disabled;
                        else if (this.looseImage != null)
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.looseImage, this.LooseTransparencyColor).Disabled;
                    }
                    return null;
                }
            }


            #endregion

            #region IOwnerDrawnCheckableItem<object,ImageObjectItem> Members

            /// <summary>
            /// Returns/sets whether the <see cref="IOwnerDrawnCheckableItem{TDrawnItem}"/>
            /// is checked.
            /// </summary>
            public bool Checked
            {
                get
                {
                    return this.isChecked;
                }
                set
                {
                    this.isChecked = value;
                    OnChange();
                }
            }

            /// <summary>
            /// The Item's Checked Shadow Image
            /// </summary>
            /// <value>CheckedShadowImage accesses the value of the imagecheckedshadow data member.  Should the value be null but the normalized image not be, it processes the styles of the images and returns.</value>
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            [Browsable(false)]
            public System.Drawing.Bitmap CheckedShadowImage
            {
                get
                {
                    if (this.Parent != null)
                    {
                        if (this.imagecheckedIndex != -1)
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.Parent.ImageList, this.imagecheckedIndex).Shadowed;
                        else if (this.imagecheckedKey != null && this.imagecheckedKey != "")
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.Parent.ImageList, this.imagecheckedKey).Shadowed;
                        else if (this.looseCheckedImage != null)
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.looseCheckedImage, this.LooseTransparencyColor).Shadowed;
                    }
                    return null;
                }
            }
            /// <summary>
            /// The Item's Checked Disabled Image
            /// </summary>
            /// <value>CheckedDisabledImage accesses the value of the imagecheckeddisabled data member.  Should the value be null but the normalized image not be, it processes the styles of the images and returns.</value>
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            [Browsable(false)]
            public System.Drawing.Bitmap CheckedDisabledImage
            {
                get
                {
                    if (this.Parent != null)
                    {
                        if (this.imagecheckedIndex != -1)
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.Parent.ImageList, this.imagecheckedIndex).Disabled;
                        else if (this.imagecheckedKey != null && this.imagecheckedKey != "")
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.Parent.ImageList, this.imagecheckedKey).Disabled;
                        else if (this.looseCheckedImage != null)
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.looseCheckedImage, this.LooseTransparencyColor).Disabled;
                    }
                    return null;
                }
            }
            /// <summary>
            /// The Checked Image associated with the Item
            /// </summary>
            /// <value>Image accesses the value of the imagechecked data member.</value>
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            [Browsable(false)]
            public System.Drawing.Bitmap CheckedImage
            {
                get
                {
                    if (this.Parent != null)
                    {
                        if (this.imagecheckedIndex != -1)
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.Parent.ImageList, this.imagecheckedIndex).Normal;
                        else if (this.imagecheckedKey != null && this.imagecheckedKey != "")
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.Parent.ImageList, this.imagecheckedKey).Normal;
                        else if (this.looseCheckedImage != null)
                            return OwnerDrawnStyle.RetrieveOrCacheImage(this.looseCheckedImage, this.LooseTransparencyColor).Normal;
                    }
                    return null;
                }
            }
            /// <summary>
            /// Returns/sets the <see cref="Color"/> associated to the
            /// <see cref="LooseImage"/> for determining what color to set
            /// as the transparency color when rendering the image.
            /// </summary>
            protected Color LooseTransparencyColor
            {
                get
                {
                    if (this.Parent == null)
                        return Color.Transparent;
                    else
                        return this.Parent.LooseTransparencyColor;
                }
            }

            /// <summary>
            /// Returns/sets whether the check area of the <see cref="ImageObjectItem"/> has the 
            /// mouse over it.
            /// </summary>
            [Browsable(false)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public bool CheckAreaHovered
            {
                get
                {
                    return this.checkAreaHovered;
                }
                set
                {
                    this.checkAreaHovered = value;
                    OnChange();
                }
            }
            #endregion

            #region IOwnerDrawnStatedItem Members

            /// <summary>
            /// Returns/sets the enabled state of the 
            /// <see cref="IOwnerDrawnStatedItem"/>.
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
