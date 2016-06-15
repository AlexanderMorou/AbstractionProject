using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
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
    /// Provides an extension of <see cref="ListBox"/> where the items
    /// are owner drawn and contain images.
    /// </summary>
    public sealed partial class ImageListBox : ListBox,
        IOwnerDrawn<ImageListBox.ImageObjectItem, ImageListBox>
    {
        private ImageList imageList;
        private ImageObjectCollection items;
        private OwnerDrawnStyle<ImageObjectItem> style;
        private OwnerDrawnStyleSource source;
        private Color looseTransparencyColor;

        /// <summary>
        /// Obtains the base item height to perform initial metric calculations.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object used to
        /// perform the calculations upon.</param>
        /// <param name="stringSize">The <see cref="SizeF"/>
        /// which denotes the current metrics of the string associated to the item.</param>
        /// <returns>A <see cref="Int32"/> value associated to the default
        /// size of any given <see cref="ImageObjectItem"/>.</returns>
        internal int GetBaseItemHeight(Graphics g, SizeF stringSize)
        {
            if (this.imageList == null)
                return Math.Max(this.ItemHeight, (int)(stringSize.Height + 5));
            else
                return Math.Max(Math.Max(this.ItemHeight, this.imageList.ImageSize.Height + 5), ((int)(stringSize.Height + 5)));
        }
        /// <summary>
        /// Creates a new <see cref="ImageListBox"/> initialized 
        /// to its default state.
        /// </summary>
        public ImageListBox()
            : base()
        {
            this.source = OwnerDrawnStyleSource.ManagerSource;
            InitializeComponent();
            items = new ImageListBox.ImageObjectCollection(this);
            this.SetItemsCore(items);
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
        /// Returns/sets the <see cref="ImageList"/> associated to the
        /// <see cref="ImageListBox"/> from which the <see cref="Items"/>
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
        /// <summary>
        /// Returns/sets the <see cref="Color"/> assoicated to the <see cref="ImageListBox"/>
        /// which sets the transparent color for loose images for the <see cref="ImageObjectItem"/> 
        /// elements contained within.
        /// </summary>
        public Color LooseTransparencyColor
        {
            get
            {
                return this.looseTransparencyColor;
            }
            set
            {
                this.looseTransparencyColor = value;
            }
        }
        private ImageObjectItem lastSelection = null;
        private bool voidingSelect = false;

        /// <summary>
        /// Occurs when the selection changes.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> which contains the parameters 
        /// on the event.</param>
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            ImageObjectItem selection = this.SelectedItem as ImageObjectItem;
            if (selection != null && !selection.Enabled)
            {
                voidingSelect = true;
                this.SelectedItem = lastSelection;
                voidingSelect = false;
            }
            else if (!voidingSelect)
                base.OnSelectedIndexChanged(e);
            if ((selection != null && selection.Enabled) ||
                selection == null)
                this.lastSelection = selection;
        }

        internal ListBox.ObjectCollection baseItems
        {
            get
            {
                return base.Items;
            }
        }

        /// <summary>
        /// Returns the <see cref="ImageObjectCollection"/> of the elements contained within
        /// the <see cref="ImageListBox"/>.
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        public new ImageObjectCollection Items
        {
            get
            {
                return items;
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
                    case OwnerDrawnStyleSource.SimpleSource:
                    case OwnerDrawnStyleSource.GradatedSource:
                    case OwnerDrawnStyleSource.ManagerSource:
                        this.source = value;
                        break;
                    case OwnerDrawnStyleSource.CustomSource:
                        throw new ArgumentException("Value cannot set directly to 'OwnerDrawnStyleSource.CustomSource', you must set the style reference to a custom derived OwnerDrawnStyle object instance.");
                }
            }
        }

        /// <summary>
        /// Occurs when an element within the <see cref="ImageListBox"/> is measured.
        /// </summary>
        /// <param name="e">The <see cref="MeasureItemEventArgs"/>
        /// which determines the specific item to be measured.</param>
        protected override void OnMeasureItem(System.Windows.Forms.MeasureItemEventArgs e)
        {
            if (e.Index < base.Items.Count)
            {
                object item = base.Items[e.Index];
                if (item is ImageObjectItem)
                {
                    ImageObjectItem imageItem = (ImageObjectItem)item;
                    SizeF stringSize = e.Graphics.MeasureString(imageItem.Text, this.Font);
                    e.ItemHeight = this.GetBaseItemHeight(e.Graphics, stringSize);
                    if (this.Style != null)
                        this.Style.OnMeasureItemEvent(imageItem, ref e, this.Font);
                }
            }
            base.OnMeasureItem(e);
        }

        /// <summary>
        /// Occurs when an element within the <see cref="ImageListBox"/> is drawn.
        /// </summary>
        /// <param name="e">The <see cref="DrawItemEventArgs"/>
        /// which determines which element is being drawn.</param>
        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            if ((e.Index != -1) && e.Index < base.Items.Count)
            {
                object item = base.Items[e.Index];
                if (item is ImageObjectItem)
                {
                    ImageObjectItem imageItem = (ImageObjectItem)item;
                    DrawItemState k = e.State;

                    if (!imageItem.Enabled)
                        k |= DrawItemState.Disabled;
                    if (!this.Enabled)
                        k |= DrawItemState.ComboBoxEdit;
                    if (k != e.State)
                        e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, k);
                    if (this.Style != null)
                        this.Style.OnDrawItemEvent(imageItem, e, this.Font);
                }
            }
            base.OnDrawItem(e);
        }

        /// <summary>
        /// Occurs when an element needs redrawn.
        /// </summary>
        /// <param name="index">The <see cref="Int32"/> value representing
        /// the ordinal index of the element to redraw.</param>
        private void RedrawItem(int index)
        {
            if ((index == -1) || (index >= this.items.Count))
                return;
            ImageObjectItem item = this.items[index];
            DrawItemState diState = DrawItemState.Default;
            Rectangle itemRect = this.GetItemRectangle(index);
            foreach (int idx in this.SelectedIndices)
                if (idx == index)
                    diState |= DrawItemState.Selected;
            if (this.Focused)
                diState |= DrawItemState.Focus;
            DrawItemEventArgs dieas = new DrawItemEventArgs(this.CreateGraphics(), this.Font, itemRect, index, diState, this.ForeColor, this.BackColor);
            this.OnDrawItem(dieas);
            dieas.Graphics.Dispose();
        }

        #region IOwnerDrawn<object,ImageObjectItem> Members

        IOwnerDrawnStyle<ImageListBox.ImageObjectItem> IOwnerDrawn<ImageListBox.ImageObjectItem>.Style
        {
            get { return this.Style; }
        }

        #endregion
}
}
