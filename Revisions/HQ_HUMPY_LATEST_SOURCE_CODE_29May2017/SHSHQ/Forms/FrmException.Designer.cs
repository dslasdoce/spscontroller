namespace ISM.Forms
{
  partial class FrmException
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmException));
        this.btnSave = new DevExpress.XtraEditors.SimpleButton();
        this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
        this.btnClose = new DevExpress.XtraEditors.SimpleButton();
        this.LBExcpCode = new DevExpress.XtraEditors.CheckedListBoxControl();
        this.lblCaption = new DevExpress.XtraEditors.LabelControl();
        this.lblOther = new DevExpress.XtraEditors.LabelControl();
        this.txtOtherException = new DevExpress.XtraEditors.MemoEdit();
        ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.LBExcpCode)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtOtherException.Properties)).BeginInit();
        this.SuspendLayout();
        // 
        // btnSave
        // 
        this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnSave.Appearance.Options.UseFont = true;
        this.btnSave.ImageIndex = 3;
        this.btnSave.ImageList = this.imgSmallImageCollection;
        this.btnSave.Location = new System.Drawing.Point(29, 571);
        this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.btnSave.Name = "btnSave";
        this.btnSave.Size = new System.Drawing.Size(125, 43);
        this.btnSave.TabIndex = 6;
        this.btnSave.Text = "Save";
        this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        // 
        // imgSmallImageCollection
        // 
        this.imgSmallImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgSmallImageCollection.ImageStream")));
        this.imgSmallImageCollection.Images.SetKeyName(0, "add22.png");
        this.imgSmallImageCollection.Images.SetKeyName(1, "delete22.png");
        this.imgSmallImageCollection.Images.SetKeyName(2, "refresh22.png");
        this.imgSmallImageCollection.Images.SetKeyName(3, "saveas22.png");
        // 
        // btnClose
        // 
        this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnClose.Appearance.Options.UseFont = true;
        this.btnClose.ImageIndex = 1;
        this.btnClose.ImageList = this.imgSmallImageCollection;
        this.btnClose.Location = new System.Drawing.Point(344, 571);
        this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(125, 43);
        this.btnClose.TabIndex = 7;
        this.btnClose.Text = "Close";
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // LBExcpCode
        // 
        this.LBExcpCode.Appearance.BackColor = System.Drawing.Color.White;
        this.LBExcpCode.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.LBExcpCode.Appearance.Options.UseBackColor = true;
        this.LBExcpCode.Appearance.Options.UseFont = true;
        this.LBExcpCode.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
        this.LBExcpCode.HotTrackItems = true;
        this.LBExcpCode.Location = new System.Drawing.Point(29, 64);
        this.LBExcpCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.LBExcpCode.Name = "LBExcpCode";
        this.LBExcpCode.Size = new System.Drawing.Size(440, 404);
        this.LBExcpCode.TabIndex = 8;
        this.LBExcpCode.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.LBExcpCode_ItemCheck);
        // 
        // lblCaption
        // 
        this.lblCaption.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblCaption.Appearance.Options.UseFont = true;
        this.lblCaption.Location = new System.Drawing.Point(29, 28);
        this.lblCaption.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.lblCaption.Name = "lblCaption";
        this.lblCaption.Size = new System.Drawing.Size(180, 19);
        this.lblCaption.TabIndex = 9;
        this.lblCaption.Text = "Select Exception Type";
        // 
        // lblOther
        // 
        this.lblOther.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblOther.Appearance.Options.UseFont = true;
        this.lblOther.Location = new System.Drawing.Point(29, 475);
        this.lblOther.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.lblOther.Name = "lblOther";
        this.lblOther.Size = new System.Drawing.Size(142, 19);
        this.lblOther.TabIndex = 10;
        this.lblOther.Text = "Other Exception :";
        // 
        // txtOtherException
        // 
        this.txtOtherException.Location = new System.Drawing.Point(29, 502);
        this.txtOtherException.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.txtOtherException.Name = "txtOtherException";
        this.txtOtherException.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtOtherException.Properties.Appearance.Options.UseFont = true;
        this.txtOtherException.Properties.MaxLength = 100;
        this.txtOtherException.Size = new System.Drawing.Size(443, 44);
        this.txtOtherException.TabIndex = 11;
        // 
        // FrmException
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(498, 652);
        this.ControlBox = false;
        this.Controls.Add(this.txtOtherException);
        this.Controls.Add(this.lblOther);
        this.Controls.Add(this.lblCaption);
        this.Controls.Add(this.LBExcpCode);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnSave);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.KeyPreview = true;
        this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.Name = "FrmException";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Exception";
        this.Load += new System.EventHandler(this.FrmStockTakeException_Load);
        ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.LBExcpCode)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtOtherException.Properties)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private DevExpress.XtraEditors.SimpleButton btnClose;
    private DevExpress.XtraEditors.SimpleButton btnSave;
    private DevExpress.Utils.ImageCollection imgSmallImageCollection;
    private DevExpress.XtraEditors.CheckedListBoxControl LBExcpCode;
    private DevExpress.XtraEditors.LabelControl lblCaption;
    private DevExpress.XtraEditors.LabelControl lblOther;
    private DevExpress.XtraEditors.MemoEdit txtOtherException;

  }
}