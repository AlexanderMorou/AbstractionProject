using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AllenCopeland.Abstraction.OwnerDrawnControls;
using System.Drawing;
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
    partial class ImageMenuItem
    {
        private MDIListHandler mdiHandler;

        /// <summary>
        /// Gets or sets a value indicating whether the menu item will be populated with
        /// a list of the Multiple Document Interface (MDI) child windows that are displayed
        /// within the associated form.
        /// </summary>
        public new bool MdiList
        {
            get
            {
                return this.mdiList;
            }
            set
            {
                this.mdiList = value;
            }
        }
        /// <summary>
        /// Raises the <see cref="MenuItem.Popup"/> event and handles
        /// the <see cref="MdiList"/> if appropriate.
        /// </summary>
        /// <param name="e">The empty event args.</param>
        protected override void OnPopup(EventArgs e)
        {
            if (this.MdiList && this.mdiHandler == null)
                if (this.GetMainMenu() != null && this.GetMainMenu().GetForm() != null)
                    this.mdiHandler = new MDIListHandler(this, this.GetMainMenu().GetForm());
            base.OnPopup(e);
        }

        
        internal class MDIListHandler :
            IDisposable
        {
            /// <summary>
            /// Data member which designates the menu watched for popup events.
            /// </summary>
            private ImageMenuItem windowMenu;
            private ImageMenuItem[] windowMenuItems;
            private ImageMenuItem separator;
            private ImageMenuItem moreWindows;
            private Dictionary<Form, Bitmap> imageCache = new Dictionary<Form, Bitmap>();
            private Form[] lastActiveMDIChildren;
            private int windowMenuItemCount = defaultWindowImageMenuItemCount;
            /// <summary>
            /// Constant which designates the default count of menu items relative to the MDI children of the
            /// mdi parent being watched.
            /// </summary>
            private const int defaultWindowImageMenuItemCount = 10;
            private Form mdiParent;
            /// <summary>
            /// A further cache of all the children which is used to fill in the gaps 
            /// </summary>
            private List<Form> activeCache = new List<Form>();

            public MDIListHandler(ImageMenuItem windowMenu, Form mdiParent)
            {
                this.mdiParent = mdiParent;
                this.windowMenu = windowMenu;
                this.mdiParent.MdiChildActivate += new EventHandler(mdiParent_MdiChildActivate);
                ResizeLists();
                if (windowMenu != null && windowMenu.GetMainMenu() != null && windowMenu.GetMainMenu().GetForm() != null)
                    activeCache.AddRange(windowMenu.GetMainMenu().GetForm().MdiChildren);
                PushActive(null);
            }

            /// <summary>
            /// Resizes the lists associated to the menu items which denote the last active MDI Children.
            /// </summary>
            private void ResizeLists()
            {
                if (windowMenuItemCount == 0)
                {
                    if (separator != null)
                    {
                        windowMenu.Items.Remove(separator);
                        separator.Dispose();
                        separator = null;
                    }
                    for (int i = 0; i < windowMenuItems.Length; i++)
                        if (windowMenuItems[i] != null)
                        {
                            windowMenu.Items.Remove(windowMenuItems[i]);
                            windowMenuItems[i].Dispose();
                        }
                    if (moreWindows != null)
                    {
                        windowMenu.Items.Remove(moreWindows);
                        moreWindows.Dispose();
                        moreWindows = null;
                    }
                }
                this.windowMenuItems = RedimPreserveArray(windowMenuItemCount, windowMenuItems);
                this.lastActiveMDIChildren = RedimPreserveArray(windowMenuItemCount, lastActiveMDIChildren);
                if (windowMenuItemCount > 0)
                {
                    if (separator == null)
                    {
                        separator = (ImageMenuItem)Activator.CreateInstance(windowMenu.GetType());//new ImageMenuItem("-Windows");
                        separator.Text = "-Windows";
                        windowMenu.Items.Add(separator);
                    }
                    for (int i = 0; i < windowMenuItems.Length; i++)
                        if (windowMenuItems[i] == null)
                        {
                            windowMenuItems[i] = (ImageMenuItem)Activator.CreateInstance(windowMenu.GetType());
                            windowMenu.Items.Add(windowMenuItems[i]);
                            windowMenuItems[i].Click += new EventHandler(windowMenuItemsAtI_Click);
                        }
                    if (moreWindows == null)
                    {
                        moreWindows = (ImageMenuItem)Activator.CreateInstance(windowMenu.GetType());//new ImageMenuItem("&Windows...");
                        moreWindows.Text = "&Windows...";
                        windowMenu.Items.Add(moreWindows);
                        moreWindows.Click += new EventHandler(moreWindows_Click);
                    }

                }
            }

            void windowMenuItemsAtI_Click(object sender, EventArgs e)
            {
                if (sender is ImageMenuItem)
                {
                    ImageMenuItem clickedItem = sender as ImageMenuItem;
                    if (clickedItem == null)
                        return;
                    if (clickedItem.Tag is Form)
                    {
                        Form clickedForm = (Form)clickedItem.Tag;
                        clickedForm.Activate();
                    }
                }
            }

            void moreWindows_Click(object sender, EventArgs e)
            {
                if (this.windowMenu != null && this.windowMenu.GetMainMenu() != null && this.windowMenu.GetMainMenu().GetForm() != null)
                {
                    MDIChildWindowsDefaultDialog windowsDialog = new MDIChildWindowsDefaultDialog();
                    Form[] forms = this.windowMenu.GetMainMenu().GetForm().MdiChildren;
                    windowsDialog.InitializeWindowsDialog(forms);
                    windowsDialog.ShowDialog(this.windowMenu.GetMainMenu().GetForm());
                    switch (windowsDialog.Action)
                    {
                        case MDIChildWindowsDialog.ActionToPerform.Activate:
                            if (windowsDialog.Selection.Length == 1)
                                if (windowsDialog.Selection[0] != null)
                                    windowsDialog.Selection[0].Activate();
                            break;
                        case MDIChildWindowsDialog.ActionToPerform.Close:
                            foreach (Form f in windowsDialog.Selection)
                                if (f != null)
                                    f.Close();
                            break;
                        case MDIChildWindowsDialog.ActionToPerform.TileVertically:
                            ReadyMoveAction(windowsDialog, forms);
                            this.windowMenu.GetMainMenu().GetForm().LayoutMdi(MdiLayout.TileVertical);
                            break;
                        case MDIChildWindowsDialog.ActionToPerform.TileHorizontally:
                            ReadyMoveAction(windowsDialog, forms);
                            this.windowMenu.GetMainMenu().GetForm().LayoutMdi(MdiLayout.TileHorizontal);
                            break;
                    }
                    windowsDialog.Release();
                    this.PushActive(this.windowMenu.GetMainMenu().GetForm().ActiveMdiChild);
                }
            }

            private static void ReadyMoveAction(MDIChildWindowsDefaultDialog windowsDialog, Form[] forms)
            {
                List<Form> formsList = new List<Form>(windowsDialog.Selection);
                foreach (Form f in forms)
                    if (!formsList.Contains(f))
                        f.WindowState = FormWindowState.Minimized;
                    else
                        f.WindowState = FormWindowState.Normal;
            }

            private static TItem[] RedimPreserveArray<TItem>(int newSize, TItem[] sourceArray)
            {
                if (sourceArray != null)
                {
                    TItem[] cachedItems = new TItem[newSize];
                    for (int i = 0; i < cachedItems.Length && i < sourceArray.Length; i++)
                        cachedItems[i] = sourceArray[i];
                    return cachedItems;
                }
                else
                    return new TItem[newSize];
            }

            void mdiParent_MdiChildActivate(object sender, EventArgs e)
            {
                PushActive(this.mdiParent.ActiveMdiChild);
            }

            private void PushActive(Form currentlyActiveChild)
            {
                List<Form> children = new List<Form>(this.mdiParent.MdiChildren);
                if (this.activeCache.Contains(currentlyActiveChild))
                    this.activeCache.Remove(currentlyActiveChild);
                if (currentlyActiveChild != null)
                    this.activeCache.Insert(0, currentlyActiveChild);
                List<Form> removed = new List<Form>(); ;
                foreach (Form child in activeCache)
                    if (!children.Contains(child) || child.Disposing || child.IsDisposed)
                        removed.Add(child);
                foreach (Form child in removed)
                {
                    imageCache.Remove(child);
                    activeCache.Remove(child);
                }
                for (int i = 0; i < windowMenuItemCount && i < activeCache.Count; i++)
                    lastActiveMDIChildren[i] = activeCache[i];
                for (int i = activeCache.Count; i < windowMenuItemCount; i++)
                    lastActiveMDIChildren[i] = null;
                if (windowMenuItemCount > 0 && windowMenuItems[0] != null)
                {
                    windowMenuItems[0].Visible = false;
                    windowMenuItems[0].Visible = true;
                }
                for (int i = 0; i < windowMenuItemCount; i++)
                {
                    if (windowMenuItems[i] == null)
                        continue;
                    if (lastActiveMDIChildren[i] == null)
                    {
                        if (windowMenuItems[i].Enabled)
                            windowMenuItems[i].Enabled = false;
                        if (windowMenuItems[i].Visible)
                            windowMenuItems[i].Visible = false;
                        windowMenuItems[i].Text = "---";
                        windowMenuItems[i].Tag = null;
                    }
                    else
                    {
                        if (!imageCache.ContainsKey(lastActiveMDIChildren[i]))
                            imageCache.Add(lastActiveMDIChildren[i], lastActiveMDIChildren[i].Icon.ToBitmap());
                        windowMenuItems[i].Text = string.Format("{0}{1} - {2}", (i + 1) < 10 ? "&" : "", i + 1, lastActiveMDIChildren[i].Text);
                        if (imageCache.ContainsKey(lastActiveMDIChildren[i]))
                            windowMenuItems[i].LooseImage = imageCache[lastActiveMDIChildren[i]];
                        else
                            windowMenuItems[i].LooseImage = null;
                        if (!windowMenuItems[i].Enabled)
                            windowMenuItems[i].Enabled = true;
                        if (!windowMenuItems[i].Visible)
                            windowMenuItems[i].Visible = true;
                        if (lastActiveMDIChildren[i] == this.mdiParent.ActiveMdiChild)
                        {
                            if (!windowMenuItems[i].Checked)
                                windowMenuItems[i].Checked = true;
                        }
                        else if (windowMenuItems[i].Checked)
                            windowMenuItems[i].Checked = false;
                        windowMenuItems[i].Tag = lastActiveMDIChildren[i];
                    }
                }
                if (separator != null)
                    if (activeCache.Count == 0)
                        separator.Visible = false;
                    else
                        separator.Visible = true;
                if (moreWindows != null)
                    if (activeCache.Count > windowMenuItemCount)
                        moreWindows.Visible = true;
                    else
                        moreWindows.Visible = false;
            }

            #region IDisposable Members

            public void Dispose()
            {
                this.windowMenuItemCount = 0;
                this.ResizeLists();
                if (this.mdiParent != null)
                    this.mdiParent.MdiChildActivate -= new EventHandler(mdiParent_MdiChildActivate);
                this.windowMenu = null;
                this.mdiParent = null;
            }

            #endregion
        }
    }
}
