using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
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
    /// The MDIChildWindowsDialog, interface used to manipulate MDI Children contained in an MDIContainer.
    /// </summary>
    public abstract partial class MDIChildWindowsDialog : Form
    {
        /// <summary>
        /// The action to perform.  
        /// </summary>
        /// <remarks>Similar in concept to DialogResult except it is paired with 
        /// an aray of the MDIChildren associated with the action.</remarks>
        public enum ActionToPerform
        {
            /// <summary>
            /// Either the System Menu Close Menu Item or the OK Button were clicked.
            /// </summary>
            None,
            /// <summary>
            /// Activate the Selected MDIChild.
            /// </summary>
            Activate,
            /// <summary>
            /// Close the Selected MDIChildren
            /// </summary>
            Close,
            /// <summary>
            /// Tile the Selected MDIChildren Vertically
            /// </summary>
            TileVertically,
            /// <summary>
            /// Tile the Selected MDIChildren Horizontally
            /// </summary>
            TileHorizontally,
            /// <summary>
            /// Custom Action on the Selected MDIChildren.
            /// </summary>
            Custom
        }
        internal System.Windows.Forms.Form[] currentSelection = new Form[0];
        /// <summary>
        /// The custom action code.
        /// </summary>
        internal protected int CustomActionCode = 0;
        /// <summary>
        /// The action to perform
        /// </summary>
        internal protected ActionToPerform Action;
        /// <summary>
        /// Standard Inheritors .ctor.
        /// </summary>
        protected MDIChildWindowsDialog()
        {
            InitializeComponent();
            this.Action = ActionToPerform.None;
        }

        /// <summary>
        /// Releases the extra data retained after the dialog's closure.
        /// </summary>
        internal void Release()
        {
            this.WindowsImageList.Images.Clear();
            this.WindowsListView.Items.Clear();
            this.currentSelection = null;
            this.Dispose();
        }

        /// <summary>
        /// Indicates a child window is being added to the Windows ListView.
        /// </summary>
        /// <param name="childWindow">The child window added.</param>
        private void OnWindowAdded(System.Windows.Forms.Form childWindow)
        {
            ListViewItem item = null;
            OnWindowAdded(childWindow, ref item);
        }

        /// <summary>
        /// Indicates a child window is being added to the Windows ListView.
        /// </summary>
        /// <param name="childWindow">The child window added to the Windows ListView</param>
        /// <param name="item">The ListViewItem added to the Windows ListView, if null on call, a new instance is created.</param>
        /// <exception cref="ArgumentNullException">If childWindow is null.</exception>
        protected virtual void OnWindowAdded(System.Windows.Forms.Form childWindow, ref System.Windows.Forms.ListViewItem item)
        {
            if (childWindow == null)
                throw new ArgumentNullException("childWindow");
            if (item == null)
                item = new ListViewItem();
            item.Tag = childWindow;
            item.Text = childWindow.Text;
            if (childWindow.Icon != null)
            {
                WindowsImageList.Images.Add(childWindow.Icon);
                item.ImageIndex = WindowsImageList.Images.Count - 1;
            }
            WindowsListView.Items.Add(item);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selection">The new Selection</param>
        /// <remarks>Notes to inheritors: When overriding OnWindowSelectionChange be sure to call the base class' 
        /// OnWindowSelectionChange so that it updates the base button states accordingly.</remarks>
        protected virtual void OnWindowSelectionChange(System.Windows.Forms.Form[] selection)
        {
            //Button enable states. Don't need to tile less then one,
            //close zero, or activate more then one window(s).
            bool enableClose = selection.Length != 0;
            bool enableTile = selection.Length > 1;
            bool enableActivate = selection.Length == 1;

            //Only update the states if they change.
            if (WindowsCloseButton.Enabled != enableClose)
                WindowsCloseButton.Enabled = enableClose;
            if (WindowsActivateButton.Enabled != enableActivate)
                WindowsActivateButton.Enabled = enableActivate;

            if (WindowsTileHorizontallyButton.Enabled != enableTile)
                WindowsTileHorizontallyButton.Enabled = enableTile;
            if (WindowsTileVerticallyButton.Enabled != enableTile)
                WindowsTileVerticallyButton.Enabled = enableTile;
        }

        /// <summary>
        /// Initializes the Windows Dialog.
        /// </summary>
        /// <param name="childWindows">The childWindows array from which to source the MDIChildren.</param>
        /// <exception cref="ArgumentException">If childWindows is multi-dimensional</exception>
        internal void InitializeWindowsDialog(System.Windows.Forms.Form[] childWindows)
        {
            //Ensure we're dealing with a single dimensional array.
            if (childWindows.Rank == 1)
            {
                //Ready the dialog to be shown, clear the image dictionary, the items, and re-add them.
                this.WindowsListView.Items.Clear();
                this.WindowsListView.SmallImageList = null;
                this.WindowsImageList.Images.Clear();
                foreach (System.Windows.Forms.Form form in childWindows)
                {
                    this.OnWindowAdded(form);
                }
                this.WindowsListView.SmallImageList = WindowsImageList;
            }
            else
            {
                throw new ArgumentException("Array must be of a single dimension.", "childWindows");
            }
        }

        /// <summary>
        /// The actively selected MDIChildren collection.
        /// </summary>
        internal protected System.Windows.Forms.Form[] Selection
        {
            get
            {
                return currentSelection;
            }
        }

        /// <summary>
        /// Event handler when the Windows ListView selection changes.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.  This parameter contains no event data.</param>
        protected virtual void WindowsListView_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
        {
            //If there is no selection, notify update the selection accordingly.
            if (this.WindowsListView.SelectedItems == null)
            {

                this.OnWindowSelectionChange(new Form[0]);
                return;
            }
            //Allocate an array of the appropriate size, and propagate its members, then update the
            //selection.
            System.Windows.Forms.Form[] selectedForms = new Form[this.WindowsListView.SelectedItems.Count];
            int i = 0;
            foreach (ListViewItem lvi in this.WindowsListView.SelectedItems)
            {
                if (lvi.Tag != null && lvi.Tag is System.Windows.Forms.Form)
                    selectedForms[i++] = lvi.Tag as System.Windows.Forms.Form;
            }
            this.currentSelection = selectedForms;
            this.OnWindowSelectionChange(selectedForms);
        }

        /// <summary>
        /// The Activate Button Click Event Handler.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.  This parameter contains no event data.</param>
        protected virtual void WindowsActivateButton_Click(object sender, EventArgs e)
        {
            //Retrieve the active selection, and ensure that inheritors adhered to the 
            //base functionality behind the Activate Button.
            Form[] selection = this.Selection;
            if (selection.Length == 1)
            {
                /* *
                 * If for some reason, the entry added to the ListView either didn't set the ListViewItem's 
                 * tag appropriately, or they didn't set it to the proper type, base functionality
                 * ignores types differing from System.Windows.Forms.Form.
                 * */
                if (selection[0] != null)
                    this.Action = ActionToPerform.Activate;
                this.Close();
            }
        }

        /// <summary>
        /// The Close Button Click Event Handler.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.  This parameter contains no event data.</param>
        protected virtual void WindowsCloseButton_Click(object sender, EventArgs e)
        {
            Form[] selection = this.Selection;
            if (selection == null)
            {
                this.Close();
                return;
            }
            this.Action = ActionToPerform.Close;
            this.Close();
            return;
        }
        /// <summary>The TileHorizontally Button Click Event Handler</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.  This parameter contains no event data.</param>
        protected virtual void WindowsTileHorizontallyButton_Click(object sender, EventArgs e)
        {
            Form[] selection = this.Selection;
            if (selection == null || selection.Length < 2)
            {
                this.Close();
                return;
            }
            this.Action = ActionToPerform.TileHorizontally;
            this.Close();
            return;
        }
        /// <summary>
        /// The TileVertically Button Click Event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.  This parameter contains no event data.</param>
        protected virtual void WindowsTileVerticallyButton_Click(object sender, EventArgs e)
        {
            Form[] selection = this.Selection;
            if (selection == null || selection.Length < 2)
            {
                this.Close();
                return;
            }
            this.Action = ActionToPerform.TileVertically;
            this.Close();
            return;
        }

        /// <summary>
        /// The ListView Column Click Event Handler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.  This parameter contains no event data.</param>
        /// <remarks>Notes to Inheritors: When overriding WindowsListView_ColumnClick, be
        /// sure to call the base class' WindowsListView_ColumnClick to utilize the default column
        /// sorting.</remarks>
        protected virtual void WindowsListView_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            //First we validate the column to be sure it's a valid event.
            ItemSorter itemSort = null;
            if (e.Column < WindowsListView.Columns.Count)
            {
                /* *
                 * We need to check if we've sorted before, and if the sorter is ours.
                 * */
                if (WindowsListView.ListViewItemSorter != null && WindowsListView.ListViewItemSorter is ItemSorter)
                {
                    itemSort = WindowsListView.ListViewItemSorter as ItemSorter;
                    /* *
                     * If the Column matches the one being used on the active sorter, 
                     * invert the SortOrder used on the active Sorter and have the 
                     * ListView sort the items again.
                     * 
                     * Otherwise create a new sorter for the active column 
                     * */
                    if (itemSort.ColumnHeader == WindowsListView.Columns[e.Column])
                    {
                        if (itemSort.Sorting == SortOrder.Ascending)
                            itemSort.Sorting = SortOrder.Descending;
                        else if (itemSort.Sorting == SortOrder.Descending)
                            itemSort.Sorting = SortOrder.Ascending;
                        else //SortOrder.None
                            itemSort.Sorting = SortOrder.Ascending;
                        WindowsListView.Sort();
                        return;
                    }
                }
                /* *
                 * If it's null -or- not ours, create a new one;
                 * sort it Ascending.
                 * */
                itemSort = new ItemSorter(WindowsListView.Columns[e.Column]);
                itemSort.Sorting = SortOrder.Ascending;
                WindowsListView.ListViewItemSorter = itemSort;
                WindowsListView.Sort();
            }
        }

        /// <summary>
        /// When the Window is activated.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.  This parameter contains no event data.</param>
        protected virtual void MDIChildWindowsDialog_Activated(object sender, System.EventArgs e)
        {
            if (WindowsNameColumn != null)
                WindowsNameColumn.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        /// <summary>
        /// The Windows ListView sorter class used to sort the solumns on the ListView.
        /// </summary>
        protected class ItemSorter : IComparer
        {
            /// <summary>
            /// The column header associated with the sorter.
            /// </summary>
            public readonly ColumnHeader ColumnHeader = null;
            /// <summary>
            /// Local storage for the Sorting Property.
            /// </summary>
            private SortOrder sortOrder;
            /// <summary>
            /// The order in which to sort the items.
            /// </summary>
            /// <value>Accesses the value of the sortOrder data member.</value>
            public SortOrder Sorting
            {
                get
                {
                    return sortOrder;
                }
                set
                {
                    sortOrder = value;
                }
            }

            /// <summary>
            /// Public .ctor
            /// </summary>
            /// <param name="column">The column to sort.</param>
            public ItemSorter(ColumnHeader column)
            {
                ColumnHeader = column;
            }

            #region IComparer Members

            /// <summary>
            /// Performs a ListViewItem to ListViewItem comparison of the entries in the ListView.
            /// Values provided by the ListView.
            /// </summary>
            /// <param name="x">First ListViewItem to compare</param>
            /// <param name="y">Second ListViewItem to compare</param>
            /// <returns>If a is less than b, -1. If a is equal to b, 0.  If a is greater than b, 1.</returns>
            public int Compare(object x, object y)
            {
                //Cast the objects accordingly and return according to the sortOrder.  The ListView
                //Doesn't seem to pay attention to its own Sorting Order when a custom sorter is used.
                ListViewItem lvix = x as ListViewItem, lviy = y as ListViewItem;
                if (sortOrder == SortOrder.Ascending)
                    return string.Compare(
                        //strA
                                            lvix.SubItems[ColumnHeader.Index].Text,
                        //strB
                                            lviy.SubItems[ColumnHeader.Index].Text
                                         );
                else if (sortOrder == SortOrder.Descending)
                {
                    int result = string.Compare(
                        //strA
                                            lvix.SubItems[ColumnHeader.Index].Text,
                        //strB
                                            lviy.SubItems[ColumnHeader.Index].Text
                                         );
                    if (result == -1)
                        return 1;
                    else if (result == 1)
                        return -1;
                    else
                        return 0;
                }
                return 0;
            }

            #endregion
        }
    }
}