using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
//using AllenCopeland.Abstraction.OwnerDrawnControls.ListDynamics;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
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
    /// Provides a basic implementation of a MenuItem with OwnerDrawn and Imaged capacity.
    /// </summary>
    [DesignerCategory("Component")]
    [ToolboxItem(false)]
    public sealed partial class ImageMenuItem :
        MenuItem,
        IOwnerDrawnMenuItem<ImageMenuItem>,
        IOwnerDrawnHideableItem
    {
        /// <summary>
        /// The ImageMenuItemCollection that overrides the base functionality of MenuItems.
        /// </summary>
        private ImageMenuItemCollection menuItems;
        private bool mdiList;
        /// <summary>
        /// Initializes the <see cref="ImageMenuItem"/>.
        /// </summary>
        /// <remarks>Merely a common method used for constructors.</remarks>
        private void Initialize()
        {
            InitializeComponent();
            this.menuItems = new ImageMenuItemCollection(this);
            this.imageIndex = -1;
            this.imagecheckedIndex = -1;
        }

        #region ImageMenuItem Constructors
        /// <summary>
        /// Creates a new instance of <see cref="ImageMenuItem"/> class with the caption initialized to <paramref name="text"/>
        /// </summary>
        /// <param name="text">The caption for the image menu item.</param>
        public ImageMenuItem(string text) : base(text) { Initialize(); }
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMenuItem"/> class with a specified caption and event
        /// handler for the <see cref="MenuItem.Click"/> event of the image menu item.
        /// </summary>
        /// <param name="text">The caption for the image menu item.</param>
        /// <param name="onClick">The System.EventHandler that handles the System.Windows.Forms.MenuItem.Click
        /// event for this image menu item.</param>
        public ImageMenuItem(string text, EventHandler onClick) : base(text, onClick) { Initialize(); }
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMenuItem"/> class with a specified caption and an array
        /// of submenu items defined for the image menu item.
        /// </summary>
        /// <param name="text">The caption for the image menu item.</param>
        /// <param name="items">An array of <see cref="MenuItem"/> objects that contains the submenu
        /// items for this menu item.</param>
        public ImageMenuItem(string text, ImageMenuItem[] items) : base(text, items) { Initialize(); }
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMenuItem"/> class with a specified caption, event handler,
        /// and associated shortcut key for the menu item.
        /// </summary>
        /// <param name="text">The caption for the image menu item.</param>
        /// <param name="onClick">The System.EventHandler that handles the System.Windows.Forms.MenuItem.Click
        /// event for this menu item.</param>
        /// <param name="shortcut">One of the <see cref="Shortcut"/> values.</param>
        public ImageMenuItem(string text, EventHandler onClick, Shortcut shortcut) : base(text, onClick, shortcut) { Initialize(); }
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMenuItem"/> class with
        /// a specified caption; defined event-handlers for the <see cref="MenuItem.Click"/>,
        /// <see cref="MenuItem.Select"/> and <see cref="MenuItem.Popup"/>
        /// events; a shortcut key; a merge type; and order specified for the menu item.
        /// </summary>
        /// <param name="mergeType">One of the <see cref="MenuMerge"/> values.</param>
        /// <param name="mergeOrder">The relative position that this image menu item will take in a merged menu.</param>
        /// <param name="shortcut">One of the <see cref="Shortcut"/> values.</param>
        /// <param name="text">The caption for the image menu item.</param>
        /// <param name="onClick">The <see cref="System.EventHandler"/> that handles the <see cref="MenuItem.Click"/>
        /// event for this menu item.</param>
        /// <param name="onPopup">The <see cref="System.EventHandler"/> that handles the <see cref="MenuItem.Popup"/>
        /// event for this menu item.</param>
        /// <param name="onSelect">The <see cref="System.EventHandler"/> that handles the <see cref="MenuItem.Select"/>
        /// event for this menu item.</param>
        /// <param name="items">An array of <see cref="MenuItem"/> objects that contains the submenu
        /// items for this menu item.</param>
        public ImageMenuItem(MenuMerge mergeType, int mergeOrder, Shortcut shortcut, string text, EventHandler onClick, EventHandler onPopup, EventHandler onSelect, ImageMenuItem[] items) : base(mergeType, mergeOrder, shortcut, text, onClick, onPopup, onSelect, items) { Initialize(); }
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMenuItem"/> class.
        /// </summary>
        public ImageMenuItem()
            : base()
        {
            Initialize();
        }
        #endregion

        #region ImageMenuItem Loose Image Properties and data members
        private System.Drawing.Bitmap looseImage = null;
        private System.Drawing.Bitmap looseCheckedImage = null;

        /// <summary>
        /// Returns/sets the <see cref="Bitmap"/> associated to the
        /// <see cref="ImageMenuItem"/> in a loose-image form.
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
                if (looseImage != null)
                {
                    this.imageKey = "";
                    this.imageIndex = -1;
                }
            }
        }
        /// <summary>
        /// Returns/sets the checked <see cref="Bitmap"/> associated to the
        /// <see cref="ImageMenuItem"/> in a loose-image form.
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
        private Color LooseTransparencyColor
        {
            get
            {
                if (this.ParentImageMenu != null)
                    if (this.ParentImageMenu is ImageMenuItem)
                        return ((ImageMenuItem)this.ParentImageMenu).LooseTransparencyColor;
                    else if (this.ParentImageMenu is ImageMainMenu)
                        return ((ImageMainMenu)this.ParentImageMenu).LooseTransparencyColor;
                    else if (this.ParentImageMenu is ImageContextMenu)
                        return ((ImageContextMenu)this.ParentImageMenu).LooseTransparencyColor;
                return Color.Transparent;
            }
        }
        #endregion

        private IOwnerDrawnMenu<ImageMenuItem> ParentImageMenu
        {
            [DebuggerHidden]
            get
            {
                if (this.Parent != null && this.Parent is IOwnerDrawnMenu<ImageMenuItem>)
                    return (IOwnerDrawnMenu<ImageMenuItem>)Parent;
                return null;
            }
        }

        #region ImageMenuItem Checked Image Properties and Property Data members

        #region Checked Image Properties' Data Members
        /// <summary>
        /// Data member for <see cref="CheckedImageIndex"/>.
        /// </summary>
        private int imagecheckedIndex = 0;
        /// <summary>
        /// Data member for <see cref="CheckedImageKey"/>.
        /// </summary>
        private string imagecheckedKey = "";
        #endregion

        /// <summary>
        /// Returns/sets the <see cref="String"/> value relative to the
        /// key of the checked image to display along side the 
        /// <see cref="ImageMenuItem"/>.
        /// </summary>
        [RefreshPropertiesAttribute(RefreshProperties.Repaint)]
        [LocalizableAttribute(true)]
        [DefaultValueAttribute("")]
        [TypeConverterAttribute(typeof(ImageKeyConverter))]
        public string CheckedImageKey
        {
            [DebuggerHidden]
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
        /// <see cref="ImageMenuItem"/>.
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
        #region from IOwnerDrawnCheckableItem<ImageMenuItem> Members
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
                if (this.ParentImageMenu != null)
                {
                    if (this.imagecheckedIndex != -1)
                        return OwnerDrawnStyle.RetrieveOrCacheImage(this.ParentImageMenu.ImageList, this.imagecheckedIndex).Shadowed;
                    else if (this.imagecheckedKey != null && this.imagecheckedKey != "")
                        return OwnerDrawnStyle.RetrieveOrCacheImage(this.ParentImageMenu.ImageList, this.imagecheckedKey).Shadowed;
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
                if (this.ParentImageMenu != null)
                {
                    if (this.imagecheckedIndex != -1)
                        return OwnerDrawnStyle.RetrieveOrCacheImage(this.ParentImageMenu.ImageList, this.imagecheckedIndex).Disabled;
                    else if (this.imagecheckedKey != null && this.imagecheckedKey != "")
                        return OwnerDrawnStyle.RetrieveOrCacheImage(this.ParentImageMenu.ImageList, this.imagecheckedKey).Disabled;
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
                if (this.ParentImageMenu != null)
                {
                    if (this.imagecheckedIndex != -1)
                        return OwnerDrawnStyle.RetrieveOrCacheImage(this.ParentImageMenu.ImageList, this.imagecheckedIndex).Normal;
                    else if (this.imagecheckedKey != null && this.imagecheckedKey != "")
                        return OwnerDrawnStyle.RetrieveOrCacheImage(this.ParentImageMenu.ImageList, this.imagecheckedKey).Normal;
                    else if (this.looseCheckedImage != null)
                        return OwnerDrawnStyle.RetrieveOrCacheImage(this.looseCheckedImage, this.LooseTransparencyColor).Normal;
                }
                return null;
            }
        }
        #endregion
        #endregion

        #region ImageMenuItem Image Properties and data members

        #region Image Properties' Data Members
        /// <summary>
        /// Data member for <see cref="ImageIndex"/>.
        /// </summary>
        private int imageIndex = -1;
        /// <summary>
        /// Data member for <see cref="ImageKey"/>.
        /// </summary>
        private string imageKey = "";

        #endregion

        #region Image Key and Index Properties
        /// <summary>
        /// Gets or sets the name of the image assigned to the image menu item.
        /// </summary>
        /// <returns>The name of the System.Drawing.Bitmap assigned to the image menu item.</returns>
        [RefreshProperties(RefreshProperties.Repaint)]
        [Localizable(true)]
        [DefaultValue("")]
        [TypeConverter(typeof(ImageKeyConverter))]
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
        /// Gets or sets the index of the image assigned to the image menu item.
        /// </summary>
        /// <returns>The index of the System.Drawing.Bitmap assigned to the image menu item.</returns>
        [TypeConverter(typeof(ImageIndexConverter))]
        [DefaultValue(-1)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Localizable(true)]
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
        #endregion

        #region From IOwnerDrawnItem<ImageMenuItem> Members
        /// <summary>
        /// The Image associated with the Item
        /// </summary>
        /// <value>Image accesses the value of the image data member.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public System.Drawing.Bitmap Image
        {
            get
            {
                if (this.ParentImageMenu != null)
                {
                    if (this.imageIndex != -1)
                        return OwnerDrawnStyle.RetrieveOrCacheImage(this.ParentImageMenu.ImageList, this.imageIndex).Normal;
                    else if (this.imageKey != null && this.imageKey != "")
                        return OwnerDrawnStyle.RetrieveOrCacheImage(this.ParentImageMenu.ImageList, this.imageKey).Normal;
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
        public System.Drawing.Bitmap ShadowImage
        {
            get
            {
                if (this.ParentImageMenu != null)
                {
                    if (this.imageIndex != -1)
                        return OwnerDrawnStyle.RetrieveOrCacheImage(this.ParentImageMenu.ImageList, this.imageIndex).Shadowed;
                    else if (this.imageKey != null && this.imageKey != "")
                        return OwnerDrawnStyle.RetrieveOrCacheImage(this.ParentImageMenu.ImageList, this.imageKey).Shadowed;
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
        public System.Drawing.Bitmap DisabledImage
        {
            get
            {
                if (this.ParentImageMenu != null)
                {
                    if (this.imageIndex != -1)
                        return OwnerDrawnStyle.RetrieveOrCacheImage(this.ParentImageMenu.ImageList, this.imageIndex).Disabled;
                    else if (this.imageKey != null && this.imageKey != "")
                        return OwnerDrawnStyle.RetrieveOrCacheImage(this.ParentImageMenu.ImageList, this.imageKey).Disabled;
                    else if (this.looseImage != null)
                        return OwnerDrawnStyle.RetrieveOrCacheImage(this.looseImage, this.LooseTransparencyColor).Disabled;
                }
                return null;
            }
        }
        #endregion

        #endregion

        #region IOwnerDrawnMenuItem<object,ImageMenuItem> Members

        /// <summary>
        /// Returns whether the <see cref="ImageMenuItem"/> is
        /// a top-level menu item.
        /// </summary>
        public bool TopMost
        {
            [DebuggerHidden]
            get
            {
                return (this.Parent is MainMenu);
            }
        }

        IOwnerDrawnMenu<ImageMenuItem> IOwnerDrawnMenuItem<ImageMenuItem>.Parent
        {
            [DebuggerHidden]
            get
            {
                return ParentImageMenu;
            }
        }

        #endregion

        #region IOwnerDrawn<object,ImageMenuItem> Members

        IOwnerDrawnStyle<ImageMenuItem> IOwnerDrawn<ImageMenuItem>.Style
        {
            [DebuggerHidden]
            get
            {
                return this.Style;
            }
        }
        /// <summary>
        /// The style of the ImageMenuItem, if the parent is null, it returns the style of the ownerdrawn
        /// manager for this drawn type of <see cref="ImageMenuItem"/>.
        /// </summary>
        public OwnerDrawnStyle<ImageMenuItem> Style
        {
            [DebuggerHidden]
            [Browsable(false)]
            get
            {
                if (ParentImageMenu != null)
                    return (OwnerDrawnStyle<ImageMenuItem>)ParentImageMenu.Style;
                return OwnerDrawnManager<ImageMenuItem>.Style;
            }
        }
        #endregion

        #region IOwnerDrawnMenu<ImageMenuItem> Members

        Menu.MenuItemCollection IOwnerDrawnMenu<ImageMenuItem>.MenuItems
        {
            [DebuggerHidden]
            get
            {
                return base.MenuItems;
            }
        }

        ImageList IOwnerDrawnMenu<ImageMenuItem>.ImageList
        {
            [DebuggerHidden]
            get
            {
                if (this.Parent is IOwnerDrawnMenu<ImageMenuItem>)
                    return ((IOwnerDrawnMenu<ImageMenuItem>)Parent).ImageList;
                return null;
            }
        }
        /// <summary>
        /// The ImageMenuItemCollection associated with this menu item.  Overrides the base functionality of 
        /// <see cref="MenuItem"/>.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ImageMenuItemCollection Items
        {
            [DebuggerHidden]
            get
            {
                return this.menuItems;
            }
        }

        /// <summary>
        /// Returns a <see cref="Menu.MenuItemCollection"/> associated to the original
        /// <see cref="Menu"/>'s items.
        /// </summary>
        [Browsable(false)]
        // Hides the MenuItems associated with the base object.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        new public MenuItemCollection MenuItems
        {
            get
            {
                return base.MenuItems;
            }
        }
        #endregion

        #region IOwnerDrawnCheckableItem<ImageMenuItem> Members

        /// <summary>
        /// Returns the <see cref="Object"/> relative to the 
        /// item that the <see cref="ImageMenuItem"/> exists
        /// to represent.
        /// </summary>
        [Browsable(false)]
        public object Item
        {
            get { return null; }
        }

        #endregion

        #region IOwnerDrawnItem<ImageMenuItem> Members

        object IOwnerDrawnItem.Parent
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }


        #endregion

        #region ImageMenuItem Overloads
        /// <summary>
        /// Measures the <see cref="ImageMenuItem"/> with the arguments <paramref name="e"/>
        /// provided.
        /// </summary>
        /// <param name="e">The <see cref="MeasureItemEventArgs"/>
        /// which denotes information about the metric calculation.</param>
        [DebuggerHidden()]
        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            if (this.Style != null)
                this.Style.OnMeasureItemEvent(this, ref e, SystemFonts.MenuFont);
            base.OnMeasureItem(e);
        }
        /// <summary>
        /// Draws the <see cref="ImageMenuItem"/> with the arguments
        /// <paramref name="e"/> provided.
        /// </summary>
        /// <param name="e">The <see cref="DrawItemEventArgs"/>
        /// which denotes information about the draw operation.</param>
        [DebuggerHidden()]
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (this.Style != null)
                this.Style.OnDrawItemEvent(this, e, SystemFonts.MenuFont);
            base.OnDrawItem(e);
        }
        #endregion
    }

}