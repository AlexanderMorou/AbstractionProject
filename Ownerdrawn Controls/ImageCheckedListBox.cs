using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Collections.ObjectModel;
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
    /// Provides a <see cref="ListBox"/> extension which is owner drawn
    /// and can contain images associated to each individual item within.
    /// </summary>
    public partial class ImageCheckedListBox : ListBox,
        IOwnerDrawn<ImageCheckedListBox.ImageObjectItem, ImageCheckedListBox>
    {
        private ImageList imageList;
        private ImageObjectCollection items;
        private OwnerDrawnStyle<ImageObjectItem> style;
        private OwnerDrawnStyleSource source;
        private Color looseTransparencyColor;
        private ImageObjectItem hoveredItem;
        /// <summary>
        /// Occurs when an <see cref="ImageObjectItem"/> is checked.
        /// </summary>
        public event EventHandler<ImageCheckedListBoxItemCheckChangedEventArgs> ItemCheckChanged;
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
        /// Creates a new <see cref="ImageCheckedListBox"/>
        /// initialized to its default state.
        /// </summary>
        public ImageCheckedListBox()
            : base()
        {
            this.source = OwnerDrawnStyleSource.ManagerSource;
            InitializeComponent();
            items = new ImageCheckedListBox.ImageObjectCollection(this);
            this.SetItemsCore(items);
            this.looseTransparencyColor = Color.Transparent;
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
        /// Returns/sets the <see cref="Color"/> assoicated to the <see cref="ImageCheckedListBox"/>
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
        internal ListBox.ObjectCollection baseItems
        {
            get
            {
                return base.Items;
            }
        }
        /// <summary>
        /// Returns the <see cref="ImageObjectCollection"/> of the elements contained within
        /// the <see cref="ImageCheckedListBox"/>.
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
        /// Occurs when an element within the <see cref="ImageCheckedListBox"/> is measured.
        /// </summary>
        /// <param name="e">The <see cref="MeasureItemEventArgs"/>
        /// which determines the specific item to be measured.</param>
        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            if (e.Index < base.Items.Count)
            {
                object item = base.Items[e.Index];
                if (item is ImageObjectItem)
                {
                    ImageObjectItem imageItem = item as ImageObjectItem;
                    SizeF stringSize = e.Graphics.MeasureString(imageItem.Text, this.Font);
                    e.ItemHeight = this.GetBaseItemHeight(e.Graphics, stringSize);
                    try
                    {
                        if (this.Style != null)
                            this.Style.OnMeasureItemEvent(imageItem, ref e, this.Font);
                    }
                    catch (Exception er) { MessageBox.Show(System.String.Format("{0}\r\n\r\n{1}", er.Message, er.StackTrace)); }
                }
            }
            base.OnMeasureItem(e);
        }
        /// <summary>
        /// Occurs when an element within the <see cref="ImageCheckedListBox"/> is drawn.
        /// </summary>
        /// <param name="e">The <see cref="DrawItemEventArgs"/>
        /// which determines which element is being drawn.</param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if ((e.Index != -1) && e.Index < base.Items.Count)
            {
                object item = base.Items[e.Index];
                if (item is ImageObjectItem)
                {
                    try
                    {
                        DrawItemState diState = e.State;
                        if ((item as ImageObjectItem).Checked)
                            diState |= DrawItemState.Checked;
                        DrawItemEventArgs newArgs = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, diState);
                        if (this.Style != null)
                            this.Style.OnDrawItemEvent(item as ImageObjectItem, newArgs, this.Font);
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.Message + "\r\n" + ee.StackTrace);
                    }
                }
            }
            base.OnDrawItem(e);
        }
        /// <summary>
        /// Occurs when a key is released within the focus of the
        /// <see cref="ImageCheckedListBox"/>.
        /// </summary>
        /// <param name="e">The <see cref="KeyEventArgs"/>
        /// which specifies information about the key released.</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    for (int i = 0; i < this.SelectedIndices.Count; i++)
                    {
                        var item = this.items[this.SelectedIndices[i]];
                        item.Checked = !item.Checked;
                        this.OnItemCheckChanged(item);
                    }
                    break;
            }
            base.OnKeyUp(e);
        }

        private void OnItemCheckChanged(ImageObjectItem item)
        {
            if (this.ItemCheckChanged != null)
                this.ItemCheckChanged(this, new ImageCheckedListBoxItemCheckChangedEventArgs(item));
        }

        /// <summary>
        /// Occurs when the mouse is released within the client area
        /// of the <see cref="ImageCheckedListBox"/>.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> which specify
        /// the information about the mouse event.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            ImageObjectItem checkedItem;
            SizeF imageMetric;
            if (e.Button == MouseButtons.Left)
            {
                int itemUnderCursor = this.IndexFromPoint(new Point(e.X, e.Y));
                if (itemUnderCursor != -1 && itemUnderCursor != 65535)
                {
                    Rectangle itemRect = this.GetItemRectangle(itemUnderCursor);
                    checkedItem = this.items[itemUnderCursor];
                    if (this.imageList == null)
                        imageMetric = new SizeF(20, 20);
                    else
                        imageMetric = new SizeF(this.imageList.ImageSize.Width + 4, this.imageList.ImageSize.Height + 4);
                    if ((e.X >= itemRect.Left + 2 && e.X < itemRect.Left + 2 + imageMetric.Width) && (e.Y >= itemRect.Top + ((itemRect.Height - imageMetric.Height) / 2) && e.Y < itemRect.Top + ((itemRect.Height + imageMetric.Height) / 2)))
                    {
                        checkedItem.Checked = !checkedItem.Checked;
                        this.OnItemCheckChanged(checkedItem);
                    }
                }
            }
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Occurs when the mouse moves within the scope of the
        /// <see cref="ImageCheckedListBox"/>.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/>
        /// which specify pertinent information about the mouse event.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            ImageObjectItem newHoveredItem;
            SizeF imageMetric;
            if (e.Button == MouseButtons.None)
            {
                int itemUnderCursor = this.IndexFromPoint(new Point(e.X, e.Y));
                if (itemUnderCursor != -1 && itemUnderCursor != 65535)
                {
                    Rectangle itemRect = this.GetItemRectangle(itemUnderCursor);
                    newHoveredItem = this.items[itemUnderCursor];
                    if ((newHoveredItem != this.hoveredItem) && (this.hoveredItem != null))
                    {
                        if (this.hoveredItem.CheckAreaHovered)
                            this.hoveredItem.CheckAreaHovered = false;
                    }
                    if (this.imageList == null)
                        imageMetric = new SizeF(16, 16);
                    else
                        imageMetric = new SizeF(this.imageList.ImageSize.Width, this.imageList.ImageSize.Height);
                    if ((e.X >= itemRect.Left + 3 && e.X < itemRect.Left + 3 + imageMetric.Width) && (e.Y >= itemRect.Top + ((itemRect.Height - imageMetric.Height) / 2) && e.Y < itemRect.Top + ((itemRect.Height + imageMetric.Height) / 2)))
                    {
                        if (!newHoveredItem.CheckAreaHovered)
                            newHoveredItem.CheckAreaHovered = true;
                    }
                    else
                        if (newHoveredItem.CheckAreaHovered)
                            newHoveredItem.CheckAreaHovered = false;
                    this.hoveredItem = newHoveredItem;
                }
                else
                {
                    if (hoveredItem != null && hoveredItem.CheckAreaHovered)
                        hoveredItem.CheckAreaHovered = false;
                    hoveredItem = null;
                }
            }
            base.OnMouseMove(e);
        }

        /// <summary>
        /// Occurs when an element needs redrawn.
        /// </summary>
        /// <param name="index">The <see cref="Int32"/> value representing
        /// the ordinal index of the element to redraw.</param>
        protected void RedrawItem(int index)
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

        /// <summary>
        /// Occurs when the mouse leaves the scope of the <see cref="ImageCheckedListBox"/>.
        /// </summary>
        /// <param name="e">The empty <see cref="EventArgs"/>.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            if (hoveredItem != null)
            {
                if (hoveredItem.CheckAreaHovered)
                {
                    hoveredItem.CheckAreaHovered = false;
                }
            }

            base.OnMouseLeave(e);
        }
        #region IOwnerDrawn<object,ImageObjectItem> Members

        IOwnerDrawnStyle<ImageCheckedListBox.ImageObjectItem> IOwnerDrawn<ImageCheckedListBox.ImageObjectItem>.Style
        {
            get { return this.Style; }
        }

        #endregion
    }
}
