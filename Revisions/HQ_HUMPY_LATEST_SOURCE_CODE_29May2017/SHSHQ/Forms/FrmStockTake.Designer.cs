namespace ISM.Forms
{
  partial class FrmStockTake
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStockTake));
        this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
        this.panelFooter = new DevExpress.XtraEditors.PanelControl();
        this.picStatus = new DevExpress.XtraEditors.PictureEdit();
        this.txtStatusMsg = new DevExpress.XtraEditors.MemoEdit();
        this.btnException = new DevExpress.XtraEditors.SimpleButton();
        this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
        this.txtDBtxtSerialNo = new DevExpress.XtraEditors.TextEdit();
        this.txtDBTrackInd = new DevExpress.XtraEditors.TextEdit();
        this.txtSerialNo = new DevExpress.XtraEditors.TextEdit();
        this.lblSerialNo = new DevExpress.XtraEditors.LabelControl();
        this.btnClose = new DevExpress.XtraEditors.SimpleButton();
        this.txtStockName = new DevExpress.XtraEditors.TextEdit();
        this.lblStockName = new DevExpress.XtraEditors.LabelControl();
        this.dataNavigator = new DevExpress.XtraEditors.DataNavigator();
        this.txtDBQuanity = new DevExpress.XtraEditors.TextEdit();
        this.btnValidate = new DevExpress.XtraEditors.SimpleButton();
        this.txtQuanity = new DevExpress.XtraEditors.TextEdit();
        this.txtStockCode = new DevExpress.XtraEditors.TextEdit();
        this.lblQuantity = new DevExpress.XtraEditors.LabelControl();
        this.lblStockCode = new DevExpress.XtraEditors.LabelControl();
        this.txtItemUID = new DevExpress.XtraEditors.TextEdit();
        this.lblItem = new DevExpress.XtraEditors.LabelControl();
        this.txtLocationUID = new DevExpress.XtraEditors.TextEdit();
        this.lblLoc = new DevExpress.XtraEditors.LabelControl();
        this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
        ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
        this.groupControl1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).BeginInit();
        this.panelFooter.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.picStatus.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtStatusMsg.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtDBtxtSerialNo.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtDBTrackInd.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtSerialNo.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtStockName.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtDBQuanity.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtQuanity.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtStockCode.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtItemUID.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtLocationUID.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
        this.SuspendLayout();
        // 
        // groupControl1
        // 
        this.groupControl1.Controls.Add(this.panelFooter);
        this.groupControl1.Controls.Add(this.btnException);
        this.groupControl1.Controls.Add(this.txtDBtxtSerialNo);
        this.groupControl1.Controls.Add(this.txtDBTrackInd);
        this.groupControl1.Controls.Add(this.txtSerialNo);
        this.groupControl1.Controls.Add(this.lblSerialNo);
        this.groupControl1.Controls.Add(this.btnClose);
        this.groupControl1.Controls.Add(this.txtStockName);
        this.groupControl1.Controls.Add(this.lblStockName);
        this.groupControl1.Controls.Add(this.dataNavigator);
        this.groupControl1.Controls.Add(this.txtDBQuanity);
        this.groupControl1.Controls.Add(this.btnValidate);
        this.groupControl1.Controls.Add(this.txtQuanity);
        this.groupControl1.Controls.Add(this.txtStockCode);
        this.groupControl1.Controls.Add(this.lblQuantity);
        this.groupControl1.Controls.Add(this.lblStockCode);
        this.groupControl1.Controls.Add(this.txtItemUID);
        this.groupControl1.Controls.Add(this.lblItem);
        this.groupControl1.Controls.Add(this.txtLocationUID);
        this.groupControl1.Controls.Add(this.lblLoc);
        this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.groupControl1.Location = new System.Drawing.Point(0, 0);
        this.groupControl1.Name = "groupControl1";
        this.groupControl1.ShowCaption = false;
        this.groupControl1.Size = new System.Drawing.Size(367, 384);
        this.groupControl1.TabIndex = 0;
        this.groupControl1.Text = "groupControl1";
        // 
        // panelFooter
        // 
        this.panelFooter.Controls.Add(this.picStatus);
        this.panelFooter.Controls.Add(this.txtStatusMsg);
        this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.panelFooter.Location = new System.Drawing.Point(2, 332);
        this.panelFooter.Name = "panelFooter";
        this.panelFooter.Size = new System.Drawing.Size(363, 50);
        this.panelFooter.TabIndex = 22;
        // 
        // picStatus
        // 
        this.picStatus.EditValue = global::SHSHQ.Properties.Resources.Information_icon;
        this.picStatus.Location = new System.Drawing.Point(5, 4);
        this.picStatus.Name = "picStatus";
        this.picStatus.Properties.AllowFocused = false;
        this.picStatus.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        this.picStatus.Properties.ReadOnly = true;
        this.picStatus.Properties.ShowMenu = false;
        this.picStatus.Size = new System.Drawing.Size(45, 41);
        this.picStatus.TabIndex = 10;
        // 
        // txtStatusMsg
        // 
        this.txtStatusMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.txtStatusMsg.Enabled = false;
        this.txtStatusMsg.Location = new System.Drawing.Point(53, 2);
        this.txtStatusMsg.Name = "txtStatusMsg";
        this.txtStatusMsg.Properties.AllowFocused = false;
        this.txtStatusMsg.Properties.Appearance.BackColor = System.Drawing.Color.White;
        this.txtStatusMsg.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtStatusMsg.Properties.Appearance.Options.UseBackColor = true;
        this.txtStatusMsg.Properties.Appearance.Options.UseFont = true;
        this.txtStatusMsg.Properties.ReadOnly = true;
        this.txtStatusMsg.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
        this.txtStatusMsg.Size = new System.Drawing.Size(307, 45);
        this.txtStatusMsg.TabIndex = 0;
        // 
        // btnException
        // 
        this.btnException.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnException.Appearance.Options.UseFont = true;
        this.btnException.ImageIndex = 3;
        this.btnException.ImageList = this.imgSmallImageCollection;
        this.btnException.Location = new System.Drawing.Point(250, 277);
        this.btnException.Name = "btnException";
        this.btnException.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.btnException.Size = new System.Drawing.Size(95, 35);
        this.btnException.TabIndex = 5;
        this.btnException.Text = "Exception";
        this.btnException.Click += new System.EventHandler(this.btnException_Click);
        // 
        // imgSmallImageCollection
        // 
        this.imgSmallImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgSmallImageCollection.ImageStream")));
        this.imgSmallImageCollection.Images.SetKeyName(0, "delete22.png");
        this.imgSmallImageCollection.Images.SetKeyName(1, "accept.ico");
        this.imgSmallImageCollection.Images.SetKeyName(2, "samples_icon_start.gif");
        this.imgSmallImageCollection.Images.SetKeyName(3, "admin_rformcustomizer.gif");
        this.imgSmallImageCollection.Images.SetKeyName(4, "mslock.gif");
        this.imgSmallImageCollection.Images.SetKeyName(5, "readlocked.gif");
        this.imgSmallImageCollection.Images.SetKeyName(6, "buy2.gif");
        // 
        // txtDBtxtSerialNo
        // 
        this.txtDBtxtSerialNo.Location = new System.Drawing.Point(5, 251);
        this.txtDBtxtSerialNo.Name = "txtDBtxtSerialNo";
        this.txtDBtxtSerialNo.Size = new System.Drawing.Size(100, 20);
        this.txtDBtxtSerialNo.TabIndex = 18;
        // 
        // txtDBTrackInd
        // 
        this.txtDBTrackInd.Location = new System.Drawing.Point(5, 230);
        this.txtDBTrackInd.Name = "txtDBTrackInd";
        this.txtDBTrackInd.Size = new System.Drawing.Size(100, 20);
        this.txtDBTrackInd.TabIndex = 17;
        // 
        // txtSerialNo
        // 
        this.txtSerialNo.EnterMoveNextControl = true;
        this.dxErrorProvider.SetIconAlignment(this.txtSerialNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
        this.txtSerialNo.Location = new System.Drawing.Point(120, 149);
        this.txtSerialNo.Name = "txtSerialNo";
        this.txtSerialNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtSerialNo.Properties.Appearance.Options.UseFont = true;
        this.txtSerialNo.Properties.MaxLength = 30;
        this.txtSerialNo.Size = new System.Drawing.Size(224, 22);
        this.txtSerialNo.TabIndex = 0;
        // 
        // lblSerialNo
        // 
        this.lblSerialNo.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblSerialNo.Appearance.Options.UseFont = true;
        this.lblSerialNo.Location = new System.Drawing.Point(25, 156);
        this.lblSerialNo.Name = "lblSerialNo";
        this.lblSerialNo.Size = new System.Drawing.Size(83, 16);
        this.lblSerialNo.TabIndex = 15;
        this.lblSerialNo.Text = "Serial/Equip:";
        // 
        // btnClose
        // 
        this.btnClose.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnClose.Appearance.Options.UseFont = true;
        this.btnClose.ImageIndex = 0;
        this.btnClose.ImageList = this.imgSmallImageCollection;
        this.btnClose.Location = new System.Drawing.Point(135, 277);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(95, 35);
        this.btnClose.TabIndex = 4;
        this.btnClose.Text = "Close";
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // txtStockName
        // 
        this.txtStockName.Location = new System.Drawing.Point(119, 115);
        this.txtStockName.Name = "txtStockName";
        this.txtStockName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtStockName.Properties.Appearance.Options.UseFont = true;
        this.txtStockName.Properties.ReadOnly = true;
        this.txtStockName.Size = new System.Drawing.Size(225, 22);
        this.txtStockName.TabIndex = 13;
        this.txtStockName.TabStop = false;
        // 
        // lblStockName
        // 
        this.lblStockName.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblStockName.Appearance.Options.UseFont = true;
        this.lblStockName.Location = new System.Drawing.Point(25, 121);
        this.lblStockName.Name = "lblStockName";
        this.lblStockName.Size = new System.Drawing.Size(84, 16);
        this.lblStockName.TabIndex = 12;
        this.lblStockName.Text = "Short Name :";
        // 
        // dataNavigator
        // 
        this.dataNavigator.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.dataNavigator.Appearance.Options.UseFont = true;
        this.dataNavigator.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
        this.dataNavigator.Buttons.Append.Enabled = false;
        this.dataNavigator.Buttons.Append.Visible = false;
        this.dataNavigator.Buttons.CancelEdit.Enabled = false;
        this.dataNavigator.Buttons.CancelEdit.Visible = false;
        this.dataNavigator.Buttons.EndEdit.Enabled = false;
        this.dataNavigator.Buttons.EndEdit.Visible = false;
        this.dataNavigator.Buttons.First.Enabled = false;
        this.dataNavigator.Buttons.First.Visible = false;
        this.dataNavigator.Buttons.Last.Enabled = false;
        this.dataNavigator.Buttons.Last.Visible = false;
        this.dataNavigator.Buttons.Next.Visible = false;
        this.dataNavigator.Buttons.NextPage.Visible = false;
        this.dataNavigator.Buttons.Prev.Visible = false;
        this.dataNavigator.Buttons.PrevPage.Visible = false;
        this.dataNavigator.Buttons.Remove.Enabled = false;
        this.dataNavigator.Buttons.Remove.Visible = false;
        this.dataNavigator.Enabled = false;
        this.dataNavigator.Location = new System.Drawing.Point(120, 217);
        this.dataNavigator.Name = "dataNavigator";
        this.dataNavigator.Size = new System.Drawing.Size(224, 24);
        this.dataNavigator.TabIndex = 2;
        this.dataNavigator.Text = "dataNavigator";
        this.dataNavigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.End;
        // 
        // txtDBQuanity
        // 
        this.txtDBQuanity.Location = new System.Drawing.Point(5, 210);
        this.txtDBQuanity.Name = "txtDBQuanity";
        this.txtDBQuanity.Size = new System.Drawing.Size(100, 20);
        this.txtDBQuanity.TabIndex = 10;
        // 
        // btnValidate
        // 
        this.btnValidate.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnValidate.Appearance.Options.UseFont = true;
        this.btnValidate.ImageIndex = 1;
        this.btnValidate.ImageList = this.imgSmallImageCollection;
        this.btnValidate.Location = new System.Drawing.Point(20, 279);
        this.btnValidate.Name = "btnValidate";
        this.btnValidate.Size = new System.Drawing.Size(95, 35);
        this.btnValidate.TabIndex = 3;
        this.btnValidate.Text = "Validate";
        this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
        // 
        // txtQuanity
        // 
        this.txtQuanity.EnterMoveNextControl = true;
        this.dxErrorProvider.SetIconAlignment(this.txtQuanity, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
        this.txtQuanity.Location = new System.Drawing.Point(119, 183);
        this.txtQuanity.Name = "txtQuanity";
        this.txtQuanity.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtQuanity.Properties.Appearance.Options.UseFont = true;
        this.txtQuanity.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
        this.txtQuanity.Properties.Mask.IgnoreMaskBlank = false;
        this.txtQuanity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
        this.txtQuanity.Size = new System.Drawing.Size(225, 22);
        this.txtQuanity.TabIndex = 1;
        // 
        // txtStockCode
        // 
        this.txtStockCode.EnterMoveNextControl = true;
        this.txtStockCode.Location = new System.Drawing.Point(118, 81);
        this.txtStockCode.Name = "txtStockCode";
        this.txtStockCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtStockCode.Properties.Appearance.Options.UseFont = true;
        this.txtStockCode.Properties.ReadOnly = true;
        this.txtStockCode.Size = new System.Drawing.Size(228, 22);
        this.txtStockCode.TabIndex = 6;
        this.txtStockCode.TabStop = false;
        // 
        // lblQuantity
        // 
        this.lblQuantity.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblQuantity.Appearance.Options.UseFont = true;
        this.lblQuantity.Location = new System.Drawing.Point(25, 191);
        this.lblQuantity.Name = "lblQuantity";
        this.lblQuantity.Size = new System.Drawing.Size(65, 16);
        this.lblQuantity.TabIndex = 5;
        this.lblQuantity.Text = "Quantity :";
        // 
        // lblStockCode
        // 
        this.lblStockCode.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblStockCode.Appearance.Options.UseFont = true;
        this.lblStockCode.Location = new System.Drawing.Point(25, 86);
        this.lblStockCode.Name = "lblStockCode";
        this.lblStockCode.Size = new System.Drawing.Size(81, 16);
        this.lblStockCode.TabIndex = 4;
        this.lblStockCode.Text = "Stock Code :";
        // 
        // txtItemUID
        // 
        this.txtItemUID.EnterMoveNextControl = true;
        this.txtItemUID.Location = new System.Drawing.Point(119, 47);
        this.txtItemUID.Name = "txtItemUID";
        this.txtItemUID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtItemUID.Properties.Appearance.Options.UseFont = true;
        this.txtItemUID.Properties.ReadOnly = true;
        this.txtItemUID.Size = new System.Drawing.Size(227, 22);
        this.txtItemUID.TabIndex = 3;
        this.txtItemUID.TabStop = false;
        // 
        // lblItem
        // 
        this.lblItem.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblItem.Appearance.Options.UseFont = true;
        this.lblItem.Location = new System.Drawing.Point(25, 51);
        this.lblItem.Name = "lblItem";
        this.lblItem.Size = new System.Drawing.Size(65, 16);
        this.lblItem.TabIndex = 2;
        this.lblItem.Text = "Item UID :";
        // 
        // txtLocationUID
        // 
        this.txtLocationUID.EnterMoveNextControl = true;
        this.txtLocationUID.Location = new System.Drawing.Point(118, 13);
        this.txtLocationUID.Name = "txtLocationUID";
        this.txtLocationUID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtLocationUID.Properties.Appearance.Options.UseFont = true;
        this.txtLocationUID.Properties.ReadOnly = true;
        this.txtLocationUID.Size = new System.Drawing.Size(227, 22);
        this.txtLocationUID.TabIndex = 1;
        this.txtLocationUID.TabStop = false;
        // 
        // lblLoc
        // 
        this.lblLoc.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblLoc.Appearance.Options.UseFont = true;
        this.lblLoc.Location = new System.Drawing.Point(25, 16);
        this.lblLoc.Name = "lblLoc";
        this.lblLoc.Size = new System.Drawing.Size(90, 16);
        this.lblLoc.TabIndex = 0;
        this.lblLoc.Text = "Location UID :";
        // 
        // dxErrorProvider
        // 
        this.dxErrorProvider.ContainerControl = this;
        // 
        // FrmStockTake
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(367, 384);
        this.ControlBox = false;
        this.Controls.Add(this.groupControl1);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "FrmStockTake";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Stocktake ";
        this.Load += new System.EventHandler(this.FrmStockTake_Load);
        ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
        this.groupControl1.ResumeLayout(false);
        this.groupControl1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).EndInit();
        this.panelFooter.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.picStatus.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtStatusMsg.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtDBtxtSerialNo.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtDBTrackInd.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtSerialNo.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtStockName.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtDBQuanity.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtQuanity.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtStockCode.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtItemUID.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtLocationUID.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private DevExpress.XtraEditors.GroupControl groupControl1;
    private DevExpress.XtraEditors.SimpleButton btnValidate;
    private DevExpress.XtraEditors.TextEdit txtQuanity;
    private DevExpress.XtraEditors.TextEdit txtStockCode;
    private DevExpress.XtraEditors.LabelControl lblQuantity;
    private DevExpress.XtraEditors.LabelControl lblStockCode;
    private DevExpress.XtraEditors.TextEdit txtItemUID;
    private DevExpress.XtraEditors.LabelControl lblItem;
    private DevExpress.XtraEditors.TextEdit txtLocationUID;
    private DevExpress.XtraEditors.LabelControl lblLoc;
    private DevExpress.XtraEditors.TextEdit txtDBQuanity;
    private DevExpress.XtraEditors.DataNavigator dataNavigator;
    private DevExpress.XtraEditors.TextEdit txtStockName;
    private DevExpress.XtraEditors.LabelControl lblStockName;
    private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
    private DevExpress.XtraEditors.SimpleButton btnClose;
    private DevExpress.XtraEditors.TextEdit txtSerialNo;
    private DevExpress.XtraEditors.LabelControl lblSerialNo;
    private DevExpress.XtraEditors.TextEdit txtDBTrackInd;
    private DevExpress.XtraEditors.TextEdit txtDBtxtSerialNo;
    private DevExpress.XtraEditors.SimpleButton btnException;
    private DevExpress.Utils.ImageCollection imgSmallImageCollection;
    private DevExpress.XtraEditors.PanelControl panelFooter;
    private DevExpress.XtraEditors.PictureEdit picStatus;
    private DevExpress.XtraEditors.MemoEdit txtStatusMsg;
  }
}