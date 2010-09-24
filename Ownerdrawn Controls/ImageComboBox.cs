using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
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
    /// Provides an implementation of a combo box which contains elements 
    /// that have images associated with them.
    /// </summary>
    [ToolboxItemAttribute(true)]
    [DesignTimeVisibleAttribute(true)]
    [DefaultPropertyAttribute("Items")]
    public partial class ImageComboBox : 
        ComboBox,
        IOwnerDrawn<ImageComboBox.ImageObjectItem, ImageComboBox>
    {
        private ImageList imageList;
        private ImageObjectCollection items;
        private OwnerDrawnStyle<ImageObjectItem> style;
        private OwnerDrawnStyleSource source;
        /// <summary>
        /// Obtains the base item height to perform initial metric calculations.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object used to
        /// perform the calculations upon.</param>
        /// <param name="stringSize">The <see cref="SizeF"/>
        /// which denotes the current metrics of the string associated to the item.</param>
        /// <returns>A <see cref="Int32"/> value associated to the default
        /// size of any given <see cref="ImageObjectItem"/>.</returns>
        protected internal int GetBaseItemHeight(Graphics g, SizeF stringSize)
        {
            if (this.imageList == null)
                return Math.Max(this.ItemHeight, (int)(stringSize.Height + 5));
            else
                return Math.Max(Math.Max(this.ItemHeight, this.imageList.ImageSize.Height + 5), ((int)(stringSize.Height + 5)));
        }
        /// <summary>
        /// Creates a new <see cref="ImageComboBox"/> 
        /// initialized to its default state.
        /// </summary>
        public ImageComboBox()
            : base()
        {
            this.source = OwnerDrawnStyleSource.ManagerSource;
            InitializeComponent();
            items = new ImageComboBox.ImageObjectCollection(this);
            this.SetItemsCore(items);
        }

        /// <summary>
        /// Returns/sets the <see cref="ImageList"/> associated to the
        /// <see cref="ImageComboBox"/> from which the <see cref="Items"/>
        /// source their images.
        /// </summary>
        [DefaultValue(null)]
        public ImageList ImageList
        {
            get
            {
                return this.imageList;
            }
            set
            {
                this.imageList = value;
            }
        }
        internal ComboBox.ObjectCollection baseItems
        {
            get
            {
                return base.Items;
            }
        }
        /// <summary>
        /// Returns/sets the <see cref="Int32"/> value representing
        /// how tall each element is.
        /// </summary>
        [DefaultValue(24)]
        public new int ItemHeight
        {
            get
            {
                return base.ItemHeight;
            }
            set
            {
                base.ItemHeight = value;
            }
        }
        /// <summary>
        /// Returns the <see cref="ImageObjectCollection"/> of the elements contained within
        /// the <see cref="ImageComboBox"/>.
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        public virtual new ImageObjectCollection Items
        {
            get
            {
                return items;
            }
        }
        /// <summary>
        /// Returns/sets the <see cref="ComboBoxStyle"/> associated to the 
        /// <see cref="ImageComboBox"/>.
        /// </summary>
        [DefaultValue(ComboBoxStyle.DropDownList)]
        public new ComboBoxStyle DropDownStyle
        {
            get
            {
                return base.DropDownStyle;
            }
            set
            {
                base.DropDownStyle = value;
            }
        }
        /// <summary>
        /// The Style instance that's responsible for rendering the items.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OwnerDrawnStyle<ImageObjectItem> Style
        {
            get
            {
                switch (this.source)
                {
                    case OwnerDrawnStyleSource.GradatedSource:
                        return OwnerDrawnManager<ImageObjectItem>.GradatedStyle;
                    case OwnerDrawnStyleSource.SimpleSource:
                        return OwnerDrawnManager<ImageObjectItem>.SimpleStyle;
                    case OwnerDrawnStyleSource.ManagerSource:
                        return OwnerDrawnManager<ImageObjectItem>.Style;
                    case OwnerDrawnStyleSource.CustomSource:
                        return this.style;
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    if (value == OwnerDrawnManager<ImageObjectItem>.GradatedStyle)
                    {
                        this.style = null;
                        this.source = OwnerDrawnStyleSource.GradatedSource;
                    }
                    else if (value == OwnerDrawnManager<ImageObjectItem>.SimpleStyle)
                    {
                        this.style = null;
                        this.source = OwnerDrawnStyleSource.SimpleSource;
                    }
                    else if (value == OwnerDrawnManager<ImageObjectItem>.Style)
                    {
                        this.style = null;
                        this.source = OwnerDrawnStyleSource.ManagerSource;
                    }
                    else
                    {
                        this.style = value;
                        this.source = OwnerDrawnStyleSource.CustomSource;
                    }
                }
                else
                    throw new NullReferenceException("value cannot be null.");
            }
        }

        /// <summary>
        /// The place that the Style is sourced from.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(OwnerDrawnStyleSource.ManagerSource)]
        public OwnerDrawnStyleSource StyleSource
        {
            get
            {
                return this.source;
            }
            set
            {
                switch (this.source)
                {
                    case OwnerDrawnStyleSource.GradatedSource:
                    case OwnerDrawnStyleSource.SimpleSource:
                    case OwnerDrawnStyleSource.ManagerSource:
                        this.source = value;
                        break;
                    case OwnerDrawnStyleSource.CustomSource:
                        throw new ArgumentException("Value cannot set directly to 'OwnerDrawnStyleSource.CustomSource', you must set the style reference to a custom derived OwnerDrawnStyle object instance.");
                }
            }
        }

        /// <summary>
        /// Occurs when an element within the <see cref="ImageComboBox"/> is measured.
        /// </summary>
        /// <param name="e">The <see cref="MeasureItemEventArgs"/>
        /// which determines the specific item to be measured.</param>
        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            var index = e.Index;
            if (index != -1)
            {
                object item = base.Items[index];
                if (item is ImageObjectItem)
                {
                    ImageObjectItem imageItem = item as ImageObjectItem;
                    SizeF stringSize = e.Graphics.MeasureString(imageItem.Text, this.Font);
                    e.ItemHeight = this.GetBaseItemHeight(e.Graphics, stringSize);
                    if (this.Style != null)
                        this.Style.OnMeasureItemEvent(imageItem, ref e, this.Font);
                }
            }
            base.OnMeasureItem(e);
        }

        /// <summary>
        /// Occurs when an element within the <see cref="ImageComboBox"/> is drawn.
        /// </summary>
        /// <param name="e">The <see cref="DrawItemEventArgs"/>
        /// which determines which element is being drawn.</param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            var index = e.Index;
            if (index != -1)
            {
                object item = base.Items[index];
                if (item is ImageObjectItem)
                {
                    DrawItemState newState = e.State;
                    if (!((ImageObjectItem)(item)).Enabled)
                        newState |= DrawItemState.Disabled;
                    if (newState != e.State)
                        e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, newState);
                    if (this.Style != null)
                        this.Style.OnDrawItemEvent(item as ImageObjectItem, e, this.Font);
                }
            }
            base.OnDrawItem(e);
        }

        #region IOwnerDrawn<object,ImageObjectItem> Members

        IOwnerDrawnStyle<ImageComboBox.ImageObjectItem> IOwnerDrawn<ImageObjectItem>.Style
        {
            get { return this.style; }
        }

        #endregion

    }
    internal enum ComboHoverState
    {
        /// <summary>
        /// The combobox has focus, therefore the hover state
        /// of the current item is un-necessary.
        /// </summary>
        /* *
         * Hover is always 255
         * */
        Focused,
        /// <summary>
        /// The combobox doesn't have focus, therefore
        /// the hover state of the current item is 
        /// necessary and controlled by the mouse's
        /// location inside the combobox.
        /// </summary>
        /* *
         * Maximum hover is 128
         * */
        Unfocused,
        /// <summary>
        /// Hover is variable, 255 for
        /// the current item in the combo view,
        /// varied for each other item up to 255.
        /// </summary>
        DroppedDown,
        /// <summary>
        /// Hover is disabled.
        /// </summary>
        /* *
         * Always 255 for the disabled 
         * item in view, if any.
         * */
        Disabled,
    }
}
