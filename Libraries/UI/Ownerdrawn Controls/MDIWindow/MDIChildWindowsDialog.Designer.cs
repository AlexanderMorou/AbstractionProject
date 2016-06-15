namespace AllenCopeland.Abstraction.OwnerDrawnControls
{
    partial class MDIChildWindowsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.WindowsListView = new System.Windows.Forms.ListView();
            this.WindowsNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WindowsImageList = new System.Windows.Forms.ImageList(this.components);
            this.WindowsActivateButton = new System.Windows.Forms.Button();
            this.WindowsCloseButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.WindowsTileHorizontallyButton = new System.Windows.Forms.Button();
            this.WindowsTileVerticallyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // WindowsListView
            // 
            this.WindowsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WindowsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.WindowsNameColumn});
            this.WindowsListView.Location = new System.Drawing.Point(12, 12);
            this.WindowsListView.Name = "WindowsListView";
            this.WindowsListView.ShowGroups = false;
            this.WindowsListView.Size = new System.Drawing.Size(375, 240);
            this.WindowsListView.SmallImageList = this.WindowsImageList;
            this.WindowsListView.TabIndex = 0;
            this.WindowsListView.UseCompatibleStateImageBehavior = false;
            this.WindowsListView.View = System.Windows.Forms.View.Details;
            this.WindowsListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.WindowsListView_ColumnClick);
            this.WindowsListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.WindowsListView_ItemSelectionChanged);
            // 
            // WindowsNameColumn
            // 
            this.WindowsNameColumn.Text = "Name";
            this.WindowsNameColumn.Width = 160;
            // 
            // WindowsImageList
            // 
            this.WindowsImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.WindowsImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.WindowsImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // WindowsActivateButton
            // 
            this.WindowsActivateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WindowsActivateButton.Enabled = false;
            this.WindowsActivateButton.Location = new System.Drawing.Point(394, 12);
            this.WindowsActivateButton.Name = "WindowsActivateButton";
            this.WindowsActivateButton.Size = new System.Drawing.Size(105, 21);
            this.WindowsActivateButton.TabIndex = 1;
            this.WindowsActivateButton.Text = "Activate";
            this.WindowsActivateButton.Click += new System.EventHandler(this.WindowsActivateButton_Click);
            // 
            // WindowsCloseButton
            // 
            this.WindowsCloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WindowsCloseButton.Enabled = false;
            this.WindowsCloseButton.Location = new System.Drawing.Point(394, 48);
            this.WindowsCloseButton.Name = "WindowsCloseButton";
            this.WindowsCloseButton.Size = new System.Drawing.Size(105, 21);
            this.WindowsCloseButton.TabIndex = 2;
            this.WindowsCloseButton.Text = "Close Window(s)";
            this.WindowsCloseButton.Click += new System.EventHandler(this.WindowsCloseButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(394, 261);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(105, 21);
            this.OKButton.TabIndex = 5;
            this.OKButton.Text = "OK";
            // 
            // WindowsTileHorizontallyButton
            // 
            this.WindowsTileHorizontallyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WindowsTileHorizontallyButton.Enabled = false;
            this.WindowsTileHorizontallyButton.Location = new System.Drawing.Point(394, 75);
            this.WindowsTileHorizontallyButton.Name = "WindowsTileHorizontallyButton";
            this.WindowsTileHorizontallyButton.Size = new System.Drawing.Size(105, 21);
            this.WindowsTileHorizontallyButton.TabIndex = 3;
            this.WindowsTileHorizontallyButton.Text = "Tile &Horizontally";
            this.WindowsTileHorizontallyButton.Click += new System.EventHandler(this.WindowsTileHorizontallyButton_Click);
            // 
            // WindowsTileVerticallyButton
            // 
            this.WindowsTileVerticallyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WindowsTileVerticallyButton.Enabled = false;
            this.WindowsTileVerticallyButton.Location = new System.Drawing.Point(393, 102);
            this.WindowsTileVerticallyButton.Name = "WindowsTileVerticallyButton";
            this.WindowsTileVerticallyButton.Size = new System.Drawing.Size(105, 21);
            this.WindowsTileVerticallyButton.TabIndex = 4;
            this.WindowsTileVerticallyButton.Text = "Tile &Vertically";
            this.WindowsTileVerticallyButton.Click += new System.EventHandler(this.WindowsTileVerticallyButton_Click);
            // 
            // MDIChildWindowsDialog
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 290);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.WindowsTileVerticallyButton);
            this.Controls.Add(this.WindowsTileHorizontallyButton);
            this.Controls.Add(this.WindowsCloseButton);
            this.Controls.Add(this.WindowsActivateButton);
            this.Controls.Add(this.WindowsListView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(519, 186);
            this.Name = "MDIChildWindowsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Windows";
            this.Activated += new System.EventHandler(this.MDIChildWindowsDialog_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// The ListView for the MDI Children
        /// </summary>
        protected System.Windows.Forms.ListView WindowsListView;
        /// <summary>
        /// The imagelist for the MDI Children's Icons.
        /// </summary>
        protected System.Windows.Forms.ImageList WindowsImageList;
        /// <summary>
        /// The Button used to send the "Activate" Action.
        /// </summary>
        protected System.Windows.Forms.Button WindowsActivateButton;
        /// <summary>
        /// The Button used to send the "Close" Action.
        /// </summary>
        protected System.Windows.Forms.Button WindowsCloseButton;
        /// <summary>
        /// The button used to send the Dialog Result of OK, returns None Action.
        /// </summary>
        protected System.Windows.Forms.Button OKButton;
        /// <summary>
        /// The button used to send the TileHorizontally Action
        /// </summary>
        protected System.Windows.Forms.Button WindowsTileHorizontallyButton;
        /// <summary>
        /// The button used to send the TileHorizontally Action.
        /// </summary>
        protected System.Windows.Forms.Button WindowsTileVerticallyButton;
        /// <summary>
        /// The name Column used for the MDI Children WindowsListView
        /// </summary>
        protected System.Windows.Forms.ColumnHeader WindowsNameColumn;

    }
}