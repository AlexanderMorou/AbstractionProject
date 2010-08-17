namespace AllenCopeland.Abstraction.SupplimentaryProjects.BugTestApplication
{
    partial class ObjectBrowserDialog
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Assemblies");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectBrowserDialog));
            this.AssembliesContextMenu = new AllenCopeland.Abstraction.OwnerDrawnControls.ImageContextMenu(this.components);
            this.AssembliesAddMenuItem = new AllenCopeland.Abstraction.OwnerDrawnControls.ImageMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.AssemblyStructureTreeView = new System.Windows.Forms.TreeView();
            this.ObjectBrowserImageList = new System.Windows.Forms.ImageList(this.components);
            this.TypeDetailsListView = new System.Windows.Forms.ListView();
            this.NameColumn = new System.Windows.Forms.ColumnHeader();
            this.ModifiersColumn = new System.Windows.Forms.ColumnHeader();
            this.AssemblyContextMenu = new AllenCopeland.Abstraction.OwnerDrawnControls.ImageContextMenu(this.components);
            this.AssemblyDeleteMenuItem = new AllenCopeland.Abstraction.OwnerDrawnControls.ImageMenuItem();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AssembliesContextMenu
            // 
            this.AssembliesContextMenu.ImageList = null;
            this.AssembliesContextMenu.Items.AddRange(new AllenCopeland.Abstraction.OwnerDrawnControls.ImageMenuItem[] {
            this.AssembliesAddMenuItem});
            this.AssembliesContextMenu.LooseTransparencyColor = System.Drawing.Color.Empty;
            // 
            // AssembliesAddMenuItem
            // 
            this.AssembliesAddMenuItem.Index = 0;
            this.AssembliesAddMenuItem.OwnerDraw = true;
            this.AssembliesAddMenuItem.Text = "Add Assembly...";
            this.AssembliesAddMenuItem.Click += new System.EventHandler(this.AssembliesAddMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.AssemblyStructureTreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TypeDetailsListView);
            this.splitContainer1.Size = new System.Drawing.Size(541, 370);
            this.splitContainer1.SplitterDistance = 230;
            this.splitContainer1.TabIndex = 0;
            // 
            // AssemblyStructureTreeView
            // 
            this.AssemblyStructureTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AssemblyStructureTreeView.ImageIndex = 0;
            this.AssemblyStructureTreeView.ImageList = this.ObjectBrowserImageList;
            this.AssemblyStructureTreeView.Location = new System.Drawing.Point(0, 0);
            this.AssemblyStructureTreeView.Name = "AssemblyStructureTreeView";
            treeNode1.ContextMenu = this.AssembliesContextMenu;
            treeNode1.ImageKey = "ReferencesFolder";
            treeNode1.Name = "AssembliesNode";
            treeNode1.SelectedImageKey = "ReferencesFolder";
            treeNode1.Text = "Assemblies";
            this.AssemblyStructureTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.AssemblyStructureTreeView.SelectedImageIndex = 0;
            this.AssemblyStructureTreeView.Size = new System.Drawing.Size(230, 370);
            this.AssemblyStructureTreeView.TabIndex = 0;
            // 
            // ObjectBrowserImageList
            // 
            this.ObjectBrowserImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ObjectBrowserImageList.ImageStream")));
            this.ObjectBrowserImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ObjectBrowserImageList.Images.SetKeyName(0, "Method");
            this.ObjectBrowserImageList.Images.SetKeyName(1, "Struct");
            this.ObjectBrowserImageList.Images.SetKeyName(2, "Namespace");
            this.ObjectBrowserImageList.Images.SetKeyName(3, "Interface");
            this.ObjectBrowserImageList.Images.SetKeyName(4, "Enumerator");
            this.ObjectBrowserImageList.Images.SetKeyName(5, "Delegate");
            this.ObjectBrowserImageList.Images.SetKeyName(6, "Class");
            this.ObjectBrowserImageList.Images.SetKeyName(7, "Assembly");
            this.ObjectBrowserImageList.Images.SetKeyName(8, "ReferencesFolder");
            // 
            // TypeDetailsListView
            // 
            this.TypeDetailsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColumn,
            this.ModifiersColumn});
            this.TypeDetailsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TypeDetailsListView.Location = new System.Drawing.Point(0, 0);
            this.TypeDetailsListView.Name = "TypeDetailsListView";
            this.TypeDetailsListView.Size = new System.Drawing.Size(307, 370);
            this.TypeDetailsListView.SmallImageList = this.ObjectBrowserImageList;
            this.TypeDetailsListView.TabIndex = 0;
            this.TypeDetailsListView.UseCompatibleStateImageBehavior = false;
            this.TypeDetailsListView.View = System.Windows.Forms.View.Details;
            // 
            // NameColumn
            // 
            this.NameColumn.Text = "Name";
            this.NameColumn.Width = 139;
            // 
            // ModifiersColumn
            // 
            this.ModifiersColumn.Text = "Modifiers";
            this.ModifiersColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ModifiersColumn.Width = 131;
            // 
            // AssemblyContextMenu
            // 
            this.AssemblyContextMenu.ImageList = null;
            this.AssemblyContextMenu.Items.AddRange(new AllenCopeland.Abstraction.OwnerDrawnControls.ImageMenuItem[] {
            this.AssemblyDeleteMenuItem});
            this.AssemblyContextMenu.LooseTransparencyColor = System.Drawing.Color.Empty;
            // 
            // AssemblyDeleteMenuItem
            // 
            this.AssemblyDeleteMenuItem.Index = 0;
            this.AssemblyDeleteMenuItem.LooseImage = global::AllenCopeland.Abstraction.SupplimentaryProjects.BugTestApplication.Properties.Resources.EditDeleteImage;
            this.AssemblyDeleteMenuItem.OwnerDraw = true;
            this.AssemblyDeleteMenuItem.Text = "&Remove";
            // 
            // ObjectBrowserDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 370);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ObjectBrowserDialog";
            this.Text = "Object Browser";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView AssemblyStructureTreeView;
        private System.Windows.Forms.ListView TypeDetailsListView;
        private System.Windows.Forms.ColumnHeader NameColumn;
        private System.Windows.Forms.ColumnHeader ModifiersColumn;
        private System.Windows.Forms.ImageList ObjectBrowserImageList;
        private AllenCopeland.Abstraction.OwnerDrawnControls.ImageContextMenu AssembliesContextMenu;
        private AllenCopeland.Abstraction.OwnerDrawnControls.ImageMenuItem AssembliesAddMenuItem;
        private AllenCopeland.Abstraction.OwnerDrawnControls.ImageContextMenu AssemblyContextMenu;
        private AllenCopeland.Abstraction.OwnerDrawnControls.ImageMenuItem AssemblyDeleteMenuItem;
    }
}