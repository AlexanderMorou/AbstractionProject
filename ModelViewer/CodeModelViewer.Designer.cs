namespace AllenCopeland.Abstraction.Slf.ModelViewer
{
    partial class CodeModelViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeModelViewer));
            this.ClassSelector = new AllenCopeland.Abstraction.OwnerDrawnControls.ImageComboBox();
            this.ClassMemberSelector = new AllenCopeland.Abstraction.OwnerDrawnControls.ImageComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ClassAndNamespaceView = new System.Windows.Forms.TreeView();
            this.ClassAndMemberStatementView = new System.Windows.Forms.TreeView();
            this.CodeModelImages = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ClassSelector
            // 
            this.ClassSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClassSelector.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ClassSelector.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ClassSelector.FormattingEnabled = true;
            this.ClassSelector.IntegralHeight = false;
            this.ClassSelector.Location = new System.Drawing.Point(210, 0);
            this.ClassSelector.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.ClassSelector.Name = "ClassSelector";
            this.ClassSelector.Size = new System.Drawing.Size(260, 30);
            this.ClassSelector.TabIndex = 0;
            // 
            // ClassMemberSelector
            // 
            this.ClassMemberSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClassMemberSelector.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ClassMemberSelector.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ClassMemberSelector.FormattingEnabled = true;
            this.ClassMemberSelector.IntegralHeight = false;
            this.ClassMemberSelector.Location = new System.Drawing.Point(476, 0);
            this.ClassMemberSelector.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.ClassMemberSelector.Name = "ClassMemberSelector";
            this.ClassMemberSelector.Size = new System.Drawing.Size(260, 30);
            this.ClassMemberSelector.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 207F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.ClassSelector, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ClassMemberSelector, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.ClassAndNamespaceView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ClassAndMemberStatementView, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(739, 479);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // ClassAndNamespaceView
            // 
            this.ClassAndNamespaceView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClassAndNamespaceView.ImageIndex = 0;
            this.ClassAndNamespaceView.ImageList = this.CodeModelImages;
            this.ClassAndNamespaceView.Location = new System.Drawing.Point(0, 0);
            this.ClassAndNamespaceView.Margin = new System.Windows.Forms.Padding(0);
            this.ClassAndNamespaceView.Name = "ClassAndNamespaceView";
            this.tableLayoutPanel1.SetRowSpan(this.ClassAndNamespaceView, 2);
            this.ClassAndNamespaceView.SelectedImageIndex = 0;
            this.ClassAndNamespaceView.Size = new System.Drawing.Size(207, 479);
            this.ClassAndNamespaceView.TabIndex = 2;
            // 
            // ClassAndMemberStatementView
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.ClassAndMemberStatementView, 2);
            this.ClassAndMemberStatementView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClassAndMemberStatementView.Location = new System.Drawing.Point(210, 33);
            this.ClassAndMemberStatementView.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.ClassAndMemberStatementView.Name = "ClassAndMemberStatementView";
            this.ClassAndMemberStatementView.Size = new System.Drawing.Size(529, 446);
            this.ClassAndMemberStatementView.TabIndex = 3;
            // 
            // CodeModelImages
            // 
            this.CodeModelImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("CodeModelImages.ImageStream")));
            this.CodeModelImages.TransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.CodeModelImages.Images.SetKeyName(0, "Assembly");
            // 
            // CodeModelViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 479);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CodeModelViewer";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AllenCopeland.Abstraction.OwnerDrawnControls.ImageComboBox ClassSelector;
        private AllenCopeland.Abstraction.OwnerDrawnControls.ImageComboBox ClassMemberSelector;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView ClassAndNamespaceView;
        private System.Windows.Forms.TreeView ClassAndMemberStatementView;
        private System.Windows.Forms.ImageList CodeModelImages;
    }
}

