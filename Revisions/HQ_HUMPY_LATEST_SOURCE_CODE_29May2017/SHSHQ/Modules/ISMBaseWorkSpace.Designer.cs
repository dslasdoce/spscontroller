namespace ISM.Modules
{
  partial class ISMBaseWorkSpace
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.SuspendLayout();
            // 
            // ISMBaseWorkSpace
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "ISMBaseWorkSpace";
            this.Size = new System.Drawing.Size(279, 215);
            this.Load += new System.EventHandler(this.ISMBaseWorkSpace_Load);
            this.Leave += new System.EventHandler(this.ISMBaseWorkSpace_Leave);
            this.ResumeLayout(false);

    }

    #endregion
  }
}
