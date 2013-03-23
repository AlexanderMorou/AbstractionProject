namespace AllenCopeland.Abstraction.Slf.SupplementaryProjects.TestCli
{
    partial class AnamorphicPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnamorphicPreview));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.imgCboAnamorphisms = new AllenCopeland.Abstraction.OwnerDrawnControls.ImageComboBox();
            this.TypesAndMembers = new System.Windows.Forms.ImageList(this.components);
            this.imgLstVariations = new AllenCopeland.Abstraction.OwnerDrawnControls.ImageListBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.imgCboGenericParameters = new AllenCopeland.Abstraction.OwnerDrawnControls.ImageComboBox();
            this.lblGenericParameters = new System.Windows.Forms.Label();
            this.lblGenericTypeName = new System.Windows.Forms.Label();
            this.lblGenericTypeHeader = new System.Windows.Forms.Label();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.lblGenericVariationName = new System.Windows.Forms.Label();
            this.lblGenericVariation = new System.Windows.Forms.Label();
            this.imgLstGenericReplacements = new AllenCopeland.Abstraction.OwnerDrawnControls.ImageListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(1027, 523);
            this.splitContainer1.SplitterDistance = 426;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.imgCboAnamorphisms);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.imgLstVariations);
            this.splitContainer2.Size = new System.Drawing.Size(426, 523);
            this.splitContainer2.SplitterDistance = 31;
            this.splitContainer2.TabIndex = 0;
            // 
            // imgCboAnamorphisms
            // 
            this.imgCboAnamorphisms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgCboAnamorphisms.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.imgCboAnamorphisms.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.imgCboAnamorphisms.FormattingEnabled = true;
            this.imgCboAnamorphisms.ImageList = this.TypesAndMembers;
            this.imgCboAnamorphisms.IntegralHeight = false;
            this.imgCboAnamorphisms.Location = new System.Drawing.Point(0, 0);
            this.imgCboAnamorphisms.Name = "imgCboAnamorphisms";
            this.imgCboAnamorphisms.Size = new System.Drawing.Size(426, 30);
            this.imgCboAnamorphisms.StyleSource = AllenCopeland.Abstraction.OwnerDrawnControls.OwnerDrawnStyleSource.SimpleSource;
            this.imgCboAnamorphisms.TabIndex = 0;
            this.imgCboAnamorphisms.SelectedValueChanged += new System.EventHandler(this.imgCboAnamorphisms_SelectedValueChanged);
            // 
            // TypesAndMembers
            // 
            this.TypesAndMembers.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TypesAndMembers.ImageStream")));
            this.TypesAndMembers.TransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(174)))), ((int)(((byte)(201)))));
            this.TypesAndMembers.Images.SetKeyName(0, "Class");
            this.TypesAndMembers.Images.SetKeyName(1, "Delegate");
            this.TypesAndMembers.Images.SetKeyName(2, "Enum");
            this.TypesAndMembers.Images.SetKeyName(3, "Interface");
            this.TypesAndMembers.Images.SetKeyName(4, "Struct");
            // 
            // imgLstVariations
            // 
            this.imgLstVariations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgLstVariations.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.imgLstVariations.FormattingEnabled = true;
            this.imgLstVariations.Location = new System.Drawing.Point(0, 0);
            this.imgLstVariations.LooseTransparencyColor = System.Drawing.Color.Empty;
            this.imgLstVariations.Name = "imgLstVariations";
            this.imgLstVariations.Size = new System.Drawing.Size(426, 488);
            this.imgLstVariations.StyleSource = AllenCopeland.Abstraction.OwnerDrawnControls.OwnerDrawnStyleSource.SimpleSource;
            this.imgLstVariations.TabIndex = 0;
            this.imgLstVariations.SelectedValueChanged += new System.EventHandler(this.imgLstVariations_SelectedValueChanged);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.imgCboGenericParameters);
            this.splitContainer3.Panel1.Controls.Add(this.lblGenericParameters);
            this.splitContainer3.Panel1.Controls.Add(this.lblGenericTypeName);
            this.splitContainer3.Panel1.Controls.Add(this.lblGenericTypeHeader);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(597, 523);
            this.splitContainer3.SplitterDistance = 81;
            this.splitContainer3.TabIndex = 0;
            // 
            // imgCboGenericParameters
            // 
            this.imgCboGenericParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgCboGenericParameters.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.imgCboGenericParameters.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.imgCboGenericParameters.FormattingEnabled = true;
            this.imgCboGenericParameters.IntegralHeight = false;
            this.imgCboGenericParameters.Location = new System.Drawing.Point(6, 43);
            this.imgCboGenericParameters.Name = "imgCboGenericParameters";
            this.imgCboGenericParameters.Size = new System.Drawing.Size(588, 30);
            this.imgCboGenericParameters.StyleSource = AllenCopeland.Abstraction.OwnerDrawnControls.OwnerDrawnStyleSource.SimpleSource;
            this.imgCboGenericParameters.TabIndex = 4;
            // 
            // lblGenericParameters
            // 
            this.lblGenericParameters.AutoSize = true;
            this.lblGenericParameters.Location = new System.Drawing.Point(3, 27);
            this.lblGenericParameters.Name = "lblGenericParameters";
            this.lblGenericParameters.Size = new System.Drawing.Size(103, 13);
            this.lblGenericParameters.TabIndex = 3;
            this.lblGenericParameters.Text = "Generic Parameters:";
            // 
            // lblGenericTypeName
            // 
            this.lblGenericTypeName.AutoSize = true;
            this.lblGenericTypeName.Location = new System.Drawing.Point(83, 9);
            this.lblGenericTypeName.Name = "lblGenericTypeName";
            this.lblGenericTypeName.Size = new System.Drawing.Size(117, 13);
            this.lblGenericTypeName.TabIndex = 1;
            this.lblGenericTypeName.Text = "%Generic Name Here%";
            // 
            // lblGenericTypeHeader
            // 
            this.lblGenericTypeHeader.AutoSize = true;
            this.lblGenericTypeHeader.Location = new System.Drawing.Point(3, 9);
            this.lblGenericTypeHeader.Name = "lblGenericTypeHeader";
            this.lblGenericTypeHeader.Size = new System.Drawing.Size(74, 13);
            this.lblGenericTypeHeader.TabIndex = 0;
            this.lblGenericTypeHeader.Text = "Generic Type:";
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.lblGenericVariationName);
            this.splitContainer4.Panel1.Controls.Add(this.lblGenericVariation);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.imgLstGenericReplacements);
            this.splitContainer4.Size = new System.Drawing.Size(597, 438);
            this.splitContainer4.SplitterDistance = 25;
            this.splitContainer4.TabIndex = 0;
            // 
            // lblGenericVariationName
            // 
            this.lblGenericVariationName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGenericVariationName.Location = new System.Drawing.Point(131, 0);
            this.lblGenericVariationName.Name = "lblGenericVariationName";
            this.lblGenericVariationName.Size = new System.Drawing.Size(463, 25);
            this.lblGenericVariationName.TabIndex = 3;
            this.lblGenericVariationName.Text = "%Generic Variation Name Here%";
            // 
            // lblGenericVariation
            // 
            this.lblGenericVariation.AutoSize = true;
            this.lblGenericVariation.Location = new System.Drawing.Point(3, 0);
            this.lblGenericVariation.Name = "lblGenericVariation";
            this.lblGenericVariation.Size = new System.Drawing.Size(122, 13);
            this.lblGenericVariation.TabIndex = 2;
            this.lblGenericVariation.Text = "Generic Variation Name:";
            // 
            // imgLstGenericReplacements
            // 
            this.imgLstGenericReplacements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgLstGenericReplacements.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.imgLstGenericReplacements.FormattingEnabled = true;
            this.imgLstGenericReplacements.Location = new System.Drawing.Point(0, 0);
            this.imgLstGenericReplacements.LooseTransparencyColor = System.Drawing.Color.Empty;
            this.imgLstGenericReplacements.Name = "imgLstGenericReplacements";
            this.imgLstGenericReplacements.Size = new System.Drawing.Size(597, 409);
            this.imgLstGenericReplacements.StyleSource = AllenCopeland.Abstraction.OwnerDrawnControls.OwnerDrawnStyleSource.SimpleSource;
            this.imgLstGenericReplacements.TabIndex = 3;
            // 
            // AnamorphicPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 523);
            this.Controls.Add(this.splitContainer1);
            this.Name = "AnamorphicPreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AnamorphicPreview";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private OwnerDrawnControls.ImageComboBox imgCboAnamorphisms;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label lblGenericTypeName;
        private System.Windows.Forms.Label lblGenericTypeHeader;
        private OwnerDrawnControls.ImageComboBox imgCboGenericParameters;
        private System.Windows.Forms.Label lblGenericParameters;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label lblGenericVariationName;
        private System.Windows.Forms.Label lblGenericVariation;
        private OwnerDrawnControls.ImageListBox imgLstGenericReplacements;
        private System.Windows.Forms.ImageList TypesAndMembers;
        private OwnerDrawnControls.ImageListBox imgLstVariations;
    }
}