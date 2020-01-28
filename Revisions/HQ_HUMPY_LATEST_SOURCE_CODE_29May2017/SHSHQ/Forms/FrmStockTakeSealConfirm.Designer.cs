namespace ISM.Forms
{
  partial class FrmStockTakeSealConfirm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStockTakeSealConfirm));
        this.groupControl = new DevExpress.XtraEditors.GroupControl();
        this.btnException = new DevExpress.XtraEditors.SimpleButton();
        this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
        this.radioGroupSealType = new DevExpress.XtraEditors.RadioGroup();
        this.lblSealType = new DevExpress.XtraEditors.LabelControl();
        this.btnClose = new DevExpress.XtraEditors.SimpleButton();
        this.gcSealCodeConfirm = new DevExpress.XtraEditors.GroupControl();
        this.lblLocSeal = new DevExpress.XtraEditors.LabelControl();
        this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
        this.checkEditNo = new DevExpress.XtraEditors.CheckEdit();
        this.checkEditYes = new DevExpress.XtraEditors.CheckEdit();
        this.gcSealUIDConfirm = new DevExpress.XtraEditors.GroupControl();
        this.btnSealUIDConfirm = new DevExpress.XtraEditors.SimpleButton();
        this.txtSealUID = new DevExpress.XtraEditors.TextEdit();
        this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
        this.btnVerifyConfirm = new DevExpress.XtraEditors.SimpleButton();
        this.txtLocation = new DevExpress.XtraEditors.TextEdit();
        this.lblLocation = new DevExpress.XtraEditors.LabelControl();
        this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
        ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
        this.groupControl.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.radioGroupSealType.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.gcSealCodeConfirm)).BeginInit();
        this.gcSealCodeConfirm.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.checkEditNo.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.checkEditYes.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.gcSealUIDConfirm)).BeginInit();
        this.gcSealUIDConfirm.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.txtSealUID.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtLocation.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
        this.SuspendLayout();
        // 
        // groupControl
        // 
        this.groupControl.Controls.Add(this.btnException);
        this.groupControl.Controls.Add(this.radioGroupSealType);
        this.groupControl.Controls.Add(this.lblSealType);
        this.groupControl.Controls.Add(this.btnClose);
        this.groupControl.Controls.Add(this.gcSealCodeConfirm);
        this.groupControl.Controls.Add(this.gcSealUIDConfirm);
        this.groupControl.Controls.Add(this.btnVerifyConfirm);
        this.groupControl.Controls.Add(this.txtLocation);
        this.groupControl.Controls.Add(this.lblLocation);
        this.groupControl.Dock = System.Windows.Forms.DockStyle.Fill;
        this.groupControl.Location = new System.Drawing.Point(0, 0);
        this.groupControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.groupControl.Name = "groupControl";
        this.groupControl.ShowCaption = false;
        this.groupControl.Size = new System.Drawing.Size(451, 642);
        this.groupControl.TabIndex = 0;
        this.groupControl.Text = "Seal Status";
        // 
        // btnException
        // 
        this.btnException.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnException.Appearance.Options.UseFont = true;
        this.btnException.ImageIndex = 3;
        this.btnException.ImageList = this.imgSmallImageCollection;
        this.btnException.Location = new System.Drawing.Point(252, 537);
        this.btnException.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.btnException.Name = "btnException";
        this.btnException.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.btnException.Size = new System.Drawing.Size(111, 43);
        this.btnException.TabIndex = 5;
        this.btnException.Text = "Exception";
        this.btnException.Click += new System.EventHandler(this.btnException_Click);
        // 
        // imgSmallImageCollection
        // 
        this.imgSmallImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgSmallImageCollection.ImageStream")));
        this.imgSmallImageCollection.Images.SetKeyName(0, "delete22.png");
        this.imgSmallImageCollection.Images.SetKeyName(1, "accept.ico");
        this.imgSmallImageCollection.Images.SetKeyName(2, "createfavorite.gif");
        this.imgSmallImageCollection.Images.SetKeyName(3, "advertise.jpg");
        // 
        // radioGroupSealType
        // 
        this.radioGroupSealType.Location = new System.Drawing.Point(146, 109);
        this.radioGroupSealType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.radioGroupSealType.Name = "radioGroupSealType";
        this.radioGroupSealType.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
        this.radioGroupSealType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.radioGroupSealType.Properties.Appearance.Options.UseBackColor = true;
        this.radioGroupSealType.Properties.Appearance.Options.UseFont = true;
        this.radioGroupSealType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((long)(0)), "0 - Non Sealed Location"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((byte)(1)), "1 - Non Electronic Tamper Evident"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((byte)(2)), "2 - Electronic Seal")});
        this.radioGroupSealType.Properties.ReadOnly = true;
        this.radioGroupSealType.Size = new System.Drawing.Size(297, 107);
        this.radioGroupSealType.TabIndex = 5;
        // 
        // lblSealType
        // 
        this.lblSealType.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblSealType.Appearance.Options.UseFont = true;
        this.lblSealType.Location = new System.Drawing.Point(21, 140);
        this.lblSealType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.lblSealType.Name = "lblSealType";
        this.lblSealType.Size = new System.Drawing.Size(91, 19);
        this.lblSealType.TabIndex = 3;
        this.lblSealType.Text = "Seal Type :";
        // 
        // btnClose
        // 
        this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnClose.Appearance.Options.UseFont = true;
        this.btnClose.ImageIndex = 0;
        this.btnClose.ImageList = this.imgSmallImageCollection;
        this.btnClose.Location = new System.Drawing.Point(111, 537);
        this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(111, 43);
        this.btnClose.TabIndex = 4;
        this.btnClose.Text = "Close";
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // gcSealCodeConfirm
        // 
        this.gcSealCodeConfirm.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.gcSealCodeConfirm.AppearanceCaption.Options.UseFont = true;
        this.gcSealCodeConfirm.Controls.Add(this.lblLocSeal);
        this.gcSealCodeConfirm.Controls.Add(this.btnConfirm);
        this.gcSealCodeConfirm.Controls.Add(this.checkEditNo);
        this.gcSealCodeConfirm.Controls.Add(this.checkEditYes);
        this.gcSealCodeConfirm.Location = new System.Drawing.Point(6, 383);
        this.gcSealCodeConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.gcSealCodeConfirm.Name = "gcSealCodeConfirm";
        this.gcSealCodeConfirm.Size = new System.Drawing.Size(445, 137);
        this.gcSealCodeConfirm.TabIndex = 3;
        this.gcSealCodeConfirm.Text = "Seal Code Confirm";
        // 
        // lblLocSeal
        // 
        this.lblLocSeal.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblLocSeal.Appearance.Options.UseFont = true;
        this.lblLocSeal.Location = new System.Drawing.Point(35, 52);
        this.lblLocSeal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.lblLocSeal.Name = "lblLocSeal";
        this.lblLocSeal.Size = new System.Drawing.Size(101, 19);
        this.lblLocSeal.TabIndex = 0;
        this.lblLocSeal.Text = "Is it Sealed?";
        // 
        // btnConfirm
        // 
        this.btnConfirm.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnConfirm.Appearance.Options.UseFont = true;
        this.btnConfirm.ImageIndex = 1;
        this.btnConfirm.ImageList = this.imgSmallImageCollection;
        this.btnConfirm.Location = new System.Drawing.Point(146, 83);
        this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.btnConfirm.Name = "btnConfirm";
        this.btnConfirm.Size = new System.Drawing.Size(176, 43);
        this.btnConfirm.TabIndex = 2;
        this.btnConfirm.Text = "Confirm Seal";
        this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
        // 
        // checkEditNo
        // 
        this.dxErrorProvider.SetIconAlignment(this.checkEditNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
        this.checkEditNo.Location = new System.Drawing.Point(259, 52);
        this.checkEditNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.checkEditNo.Name = "checkEditNo";
        this.checkEditNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.checkEditNo.Properties.Appearance.Options.UseFont = true;
        this.checkEditNo.Properties.Caption = "No";
        this.checkEditNo.Size = new System.Drawing.Size(50, 24);
        this.checkEditNo.TabIndex = 1;
        this.checkEditNo.CheckedChanged += new System.EventHandler(this.checkEditNo_CheckedChanged);
        // 
        // checkEditYes
        // 
        this.dxErrorProvider.SetIconAlignment(this.checkEditYes, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
        this.checkEditYes.Location = new System.Drawing.Point(146, 52);
        this.checkEditYes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.checkEditYes.Name = "checkEditYes";
        this.checkEditYes.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.checkEditYes.Properties.Appearance.Options.UseFont = true;
        this.checkEditYes.Properties.Caption = "Yes";
        this.checkEditYes.Size = new System.Drawing.Size(57, 24);
        this.checkEditYes.TabIndex = 0;
        this.checkEditYes.CheckedChanged += new System.EventHandler(this.checkEditYes_CheckedChanged);
        // 
        // gcSealUIDConfirm
        // 
        this.gcSealUIDConfirm.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.gcSealUIDConfirm.AppearanceCaption.Options.UseFont = true;
        this.gcSealUIDConfirm.Controls.Add(this.btnSealUIDConfirm);
        this.gcSealUIDConfirm.Controls.Add(this.txtSealUID);
        this.gcSealUIDConfirm.Controls.Add(this.labelControl1);
        this.gcSealUIDConfirm.Location = new System.Drawing.Point(6, 224);
        this.gcSealUIDConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.gcSealUIDConfirm.Name = "gcSealUIDConfirm";
        this.gcSealUIDConfirm.Size = new System.Drawing.Size(445, 151);
        this.gcSealUIDConfirm.TabIndex = 2;
        this.gcSealUIDConfirm.Text = "Seal UID Confirm";
        // 
        // btnSealUIDConfirm
        // 
        this.btnSealUIDConfirm.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnSealUIDConfirm.Appearance.Options.UseFont = true;
        this.btnSealUIDConfirm.ImageIndex = 2;
        this.btnSealUIDConfirm.ImageList = this.imgSmallImageCollection;
        this.btnSealUIDConfirm.Location = new System.Drawing.Point(146, 94);
        this.btnSealUIDConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.btnSealUIDConfirm.Name = "btnSealUIDConfirm";
        this.btnSealUIDConfirm.Size = new System.Drawing.Size(176, 43);
        this.btnSealUIDConfirm.TabIndex = 1;
        this.btnSealUIDConfirm.Text = "Confirm Seal UID";
        this.btnSealUIDConfirm.Click += new System.EventHandler(this.btnSealUIDConfirm_Click);
        // 
        // txtSealUID
        // 
        this.txtSealUID.EnterMoveNextControl = true;
        this.dxErrorProvider.SetIconAlignment(this.txtSealUID, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
        this.txtSealUID.Location = new System.Drawing.Point(146, 47);
        this.txtSealUID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.txtSealUID.Name = "txtSealUID";
        this.txtSealUID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtSealUID.Properties.Appearance.Options.UseFont = true;
        this.txtSealUID.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
        this.txtSealUID.Properties.MaxLength = 13;
        this.txtSealUID.Size = new System.Drawing.Size(260, 26);
        this.txtSealUID.TabIndex = 0;
        this.txtSealUID.EditValueChanged += new System.EventHandler(this.txtSealUID_EditValueChanged);
        // 
        // labelControl1
        // 
        this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.labelControl1.Appearance.Options.UseFont = true;
        this.labelControl1.Location = new System.Drawing.Point(35, 50);
        this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.labelControl1.Name = "labelControl1";
        this.labelControl1.Size = new System.Drawing.Size(82, 19);
        this.labelControl1.TabIndex = 0;
        this.labelControl1.Text = "Seal UID :";
        // 
        // btnVerifyConfirm
        // 
        this.btnVerifyConfirm.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnVerifyConfirm.Appearance.Options.UseFont = true;
        this.btnVerifyConfirm.ImageIndex = 3;
        this.btnVerifyConfirm.ImageList = this.imgSmallImageCollection;
        this.btnVerifyConfirm.Location = new System.Drawing.Point(146, 44);
        this.btnVerifyConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.btnVerifyConfirm.Name = "btnVerifyConfirm";
        this.btnVerifyConfirm.Size = new System.Drawing.Size(176, 43);
        this.btnVerifyConfirm.TabIndex = 1;
        this.btnVerifyConfirm.Text = "Confirm Location";
        this.btnVerifyConfirm.Click += new System.EventHandler(this.btnVerifyConfirm_Click);
        // 
        // txtLocation
        // 
        this.txtLocation.EnterMoveNextControl = true;
        this.dxErrorProvider.SetIconAlignment(this.txtLocation, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
        this.txtLocation.Location = new System.Drawing.Point(146, 10);
        this.txtLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.txtLocation.Name = "txtLocation";
        this.txtLocation.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtLocation.Properties.Appearance.Options.UseFont = true;
        this.txtLocation.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
        this.txtLocation.Properties.MaxLength = 13;
        this.txtLocation.Size = new System.Drawing.Size(236, 26);
        this.txtLocation.TabIndex = 0;
        this.txtLocation.EditValueChanged += new System.EventHandler(this.txtLocation_EditValueChanged);
        // 
        // lblLocation
        // 
        this.lblLocation.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblLocation.Appearance.Options.UseFont = true;
        this.lblLocation.Location = new System.Drawing.Point(35, 13);
        this.lblLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.lblLocation.Name = "lblLocation";
        this.lblLocation.Size = new System.Drawing.Size(116, 19);
        this.lblLocation.TabIndex = 0;
        this.lblLocation.Text = "Location UID :";
        // 
        // dxErrorProvider
        // 
        this.dxErrorProvider.ContainerControl = this;
        // 
        // FrmStockTakeSealConfirm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(451, 642);
        this.ControlBox = false;
        this.Controls.Add(this.groupControl);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "FrmStockTakeSealConfirm";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Stocktake Seal Status";
        this.Load += new System.EventHandler(this.FrmStockTakeSealConfirm_Load);
        ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
        this.groupControl.ResumeLayout(false);
        this.groupControl.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.radioGroupSealType.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.gcSealCodeConfirm)).EndInit();
        this.gcSealCodeConfirm.ResumeLayout(false);
        this.gcSealCodeConfirm.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.checkEditNo.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.checkEditYes.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.gcSealUIDConfirm)).EndInit();
        this.gcSealUIDConfirm.ResumeLayout(false);
        this.gcSealUIDConfirm.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.txtSealUID.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtLocation.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private DevExpress.XtraEditors.GroupControl groupControl;
    private DevExpress.Utils.ImageCollection imgSmallImageCollection;
    private DevExpress.XtraEditors.SimpleButton btnConfirm;
    private DevExpress.XtraEditors.TextEdit txtLocation;
    private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
    private DevExpress.XtraEditors.LabelControl lblLocation;
    private DevExpress.XtraEditors.CheckEdit checkEditNo;
    private DevExpress.XtraEditors.CheckEdit checkEditYes;
    private DevExpress.XtraEditors.LabelControl lblLocSeal;
    private DevExpress.XtraEditors.SimpleButton btnVerifyConfirm;
    private DevExpress.XtraEditors.GroupControl gcSealUIDConfirm;
    private DevExpress.XtraEditors.GroupControl gcSealCodeConfirm;
    private DevExpress.XtraEditors.SimpleButton btnSealUIDConfirm;
    private DevExpress.XtraEditors.TextEdit txtSealUID;
    private DevExpress.XtraEditors.LabelControl labelControl1;
    private DevExpress.XtraEditors.SimpleButton btnClose;
    private DevExpress.XtraEditors.LabelControl lblSealType;
    private DevExpress.XtraEditors.RadioGroup radioGroupSealType;
    private DevExpress.XtraEditors.SimpleButton btnException;
  }
}