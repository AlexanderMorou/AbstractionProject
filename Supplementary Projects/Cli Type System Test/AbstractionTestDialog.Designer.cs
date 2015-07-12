namespace AllenCopeland.Abstraction.Slf.SupplementaryProjects.TestCli
{
    partial class AbstractionTestDialog
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
            this.imageMainMenu1 = new AllenCopeland.Abstraction.OwnerDrawnControls.ImageMainMenu(this.components);
            this.FileMenuItem = new AllenCopeland.Abstraction.OwnerDrawnControls.ImageMenuItem();
            this.FileExitMenuItem = new AllenCopeland.Abstraction.OwnerDrawnControls.ImageMenuItem();
            this.ViewMenuItem = new AllenCopeland.Abstraction.OwnerDrawnControls.ImageMenuItem();
            this.ViewCheckedMenuItem = new AllenCopeland.Abstraction.OwnerDrawnControls.ImageMenuItem();
            this.SuspendLayout();
            // 
            // imageMainMenu1
            // 
            this.imageMainMenu1.ImageList = null;
            this.imageMainMenu1.Items.AddRange(new AllenCopeland.Abstraction.OwnerDrawnControls.ImageMenuItem[] {
            this.FileMenuItem,
            this.ViewMenuItem});
            this.imageMainMenu1.LooseTransparencyColor = System.Drawing.Color.Empty;
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.Index = 0;
            this.FileMenuItem.Items.AddRange(new AllenCopeland.Abstraction.OwnerDrawnControls.ImageMenuItem[] {
            this.FileExitMenuItem});
            this.FileMenuItem.OwnerDraw = true;
            this.FileMenuItem.Text = "&File";
            // 
            // FileExitMenuItem
            // 
            this.FileExitMenuItem.Index = 0;
            this.FileExitMenuItem.OwnerDraw = true;
            this.FileExitMenuItem.Text = "E&xit";
            // 
            // ViewMenuItem
            // 
            this.ViewMenuItem.Index = 1;
            this.ViewMenuItem.Items.AddRange(new AllenCopeland.Abstraction.OwnerDrawnControls.ImageMenuItem[] {
            this.ViewCheckedMenuItem});
            this.ViewMenuItem.OwnerDraw = true;
            this.ViewMenuItem.Text = "&View";
            // 
            // ViewCheckedMenuItem
            // 
            this.ViewCheckedMenuItem.Checked = true;
            this.ViewCheckedMenuItem.Index = 0;
            this.ViewCheckedMenuItem.OwnerDraw = true;
            this.ViewCheckedMenuItem.Text = "Checked MenuItem";
            // 
            // AbstractionTestDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 331);
            this.Menu = this.imageMainMenu1;
            this.Name = "AbstractionTestDialog";
            this.Text = "AbstractionTestDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private OwnerDrawnControls.ImageMainMenu imageMainMenu1;
        private OwnerDrawnControls.ImageMenuItem FileMenuItem;
        private OwnerDrawnControls.ImageMenuItem FileExitMenuItem;
        private OwnerDrawnControls.ImageMenuItem ViewMenuItem;
        private OwnerDrawnControls.ImageMenuItem ViewCheckedMenuItem;
    }
}