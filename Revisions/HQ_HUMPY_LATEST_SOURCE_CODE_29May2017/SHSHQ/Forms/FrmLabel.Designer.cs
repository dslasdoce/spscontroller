namespace ISM.Forms
{
    partial class FrmLabel
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLabel));
          this.gcHeader = new DevExpress.XtraEditors.GroupControl();
          this.btnClose = new DevExpress.XtraEditors.SimpleButton();
          this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
          this.lblCategory = new DevExpress.XtraEditors.LabelControl();
          this.lookUpEditType = new DevExpress.XtraEditors.LookUpEdit();
          this.txtItemUID = new DevExpress.XtraEditors.TextEdit();
          this.lblLabelUID = new DevExpress.XtraEditors.LabelControl();
          this.btnClear = new DevExpress.XtraEditors.SimpleButton();
          this.btnSave = new DevExpress.XtraEditors.SimpleButton();
          this.lblLastUID = new DevExpress.XtraEditors.LabelControl();
          this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
          this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
          this.lblLastLabelUID = new DevExpress.XtraEditors.LabelControl();
          this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
          ((System.ComponentModel.ISupportInitialize)(this.gcHeader)).BeginInit();
          this.gcHeader.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.lookUpEditType.Properties)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.txtItemUID.Properties)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
          this.SuspendLayout();
          // 
          // gcHeader
          // 
          this.gcHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.gcHeader.Appearance.Options.UseFont = true;
          this.gcHeader.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.gcHeader.AppearanceCaption.Options.UseFont = true;
          this.gcHeader.Controls.Add(this.btnClose);
          this.gcHeader.Controls.Add(this.lblCategory);
          this.gcHeader.Controls.Add(this.lookUpEditType);
          this.gcHeader.Controls.Add(this.txtItemUID);
          this.gcHeader.Controls.Add(this.lblLabelUID);
          this.gcHeader.Controls.Add(this.btnClear);
          this.gcHeader.Controls.Add(this.btnSave);
          this.gcHeader.Controls.Add(this.lblLastUID);
          this.gcHeader.Controls.Add(this.labelControl3);
          this.gcHeader.Controls.Add(this.labelControl2);
          this.gcHeader.Controls.Add(this.lblLastLabelUID);
          this.gcHeader.Dock = System.Windows.Forms.DockStyle.Fill;
          this.gcHeader.Location = new System.Drawing.Point(0, 0);
          this.gcHeader.Name = "gcHeader";
          this.gcHeader.Size = new System.Drawing.Size(321, 240);
          this.gcHeader.TabIndex = 0;
          this.gcHeader.Text = "Label Data";
          // 
          // btnClose
          // 
          this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.btnClose.Appearance.Options.UseFont = true;
          this.btnClose.ImageIndex = 1;
          this.btnClose.ImageList = this.imgSmallImageCollection;
          this.btnClose.Location = new System.Drawing.Point(214, 193);
          this.btnClose.Name = "btnClose";
          this.btnClose.Size = new System.Drawing.Size(95, 35);
          this.btnClose.TabIndex = 4;
          this.btnClose.Text = "Close";
          this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
          // 
          // imgSmallImageCollection
          // 
          this.imgSmallImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgSmallImageCollection.ImageStream")));
          this.imgSmallImageCollection.Images.SetKeyName(0, "add22.png");
          this.imgSmallImageCollection.Images.SetKeyName(1, "delete22.png");
          this.imgSmallImageCollection.Images.SetKeyName(2, "refresh22.png");
          this.imgSmallImageCollection.Images.SetKeyName(3, "saveas22.png");
          this.imgSmallImageCollection.Images.SetKeyName(4, "admin_rformcustomizer.gif");
          this.imgSmallImageCollection.Images.SetKeyName(5, "search.gif");
          // 
          // lblCategory
          // 
          this.lblCategory.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lblCategory.Appearance.Options.UseFont = true;
          this.lblCategory.Location = new System.Drawing.Point(116, 118);
          this.lblCategory.Name = "lblCategory";
          this.lblCategory.Size = new System.Drawing.Size(85, 16);
          this.lblCategory.TabIndex = 12;
          this.lblCategory.Text = "labelControl4";
          // 
          // lookUpEditType
          // 
          this.lookUpEditType.EnterMoveNextControl = true;
          this.dxErrorProvider.SetIconAlignment(this.lookUpEditType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
          this.lookUpEditType.Location = new System.Drawing.Point(116, 151);
          this.lookUpEditType.Name = "lookUpEditType";
          this.lookUpEditType.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lookUpEditType.Properties.Appearance.Options.UseFont = true;
          this.lookUpEditType.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
          this.lookUpEditType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
          this.lookUpEditType.Properties.NullText = "";
          this.lookUpEditType.Properties.PopupSizeable = false;
          this.lookUpEditType.Properties.PopupWidth = 300;
          this.lookUpEditType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
          this.lookUpEditType.Size = new System.Drawing.Size(162, 22);
          this.lookUpEditType.TabIndex = 1;
          // 
          // txtItemUID
          // 
          this.txtItemUID.EnterMoveNextControl = true;
          this.dxErrorProvider.SetIconAlignment(this.txtItemUID, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
          this.txtItemUID.Location = new System.Drawing.Point(116, 79);
          this.txtItemUID.Name = "txtItemUID";
          this.txtItemUID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.txtItemUID.Properties.Appearance.Options.UseFont = true;
          this.txtItemUID.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
          this.txtItemUID.Properties.MaxLength = 13;
          this.txtItemUID.Size = new System.Drawing.Size(160, 22);
          this.txtItemUID.TabIndex = 0;
          this.txtItemUID.Leave += new System.EventHandler(this.txtItemUID_Leave);
          // 
          // lblLabelUID
          // 
          this.lblLabelUID.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lblLabelUID.Appearance.Options.UseFont = true;
          this.lblLabelUID.Location = new System.Drawing.Point(12, 82);
          this.lblLabelUID.Name = "lblLabelUID";
          this.lblLabelUID.Size = new System.Drawing.Size(69, 16);
          this.lblLabelUID.TabIndex = 8;
          this.lblLabelUID.Text = "Label UID :";
          // 
          // btnClear
          // 
          this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.btnClear.Appearance.Options.UseFont = true;
          this.btnClear.ImageIndex = 2;
          this.btnClear.ImageList = this.imgSmallImageCollection;
          this.btnClear.Location = new System.Drawing.Point(113, 193);
          this.btnClear.Name = "btnClear";
          this.btnClear.Size = new System.Drawing.Size(95, 35);
          this.btnClear.TabIndex = 3;
          this.btnClear.Text = "Clear";
          this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
          // 
          // btnSave
          // 
          this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.btnSave.Appearance.Options.UseFont = true;
          this.btnSave.ImageIndex = 3;
          this.btnSave.ImageList = this.imgSmallImageCollection;
          this.btnSave.Location = new System.Drawing.Point(12, 193);
          this.btnSave.Name = "btnSave";
          this.btnSave.Size = new System.Drawing.Size(95, 35);
          this.btnSave.TabIndex = 2;
          this.btnSave.Text = "Save";
          this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
          // 
          // lblLastUID
          // 
          this.lblLastUID.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lblLastUID.Appearance.Options.UseFont = true;
          this.lblLastUID.Location = new System.Drawing.Point(116, 49);
          this.lblLastUID.Name = "lblLastUID";
          this.lblLastUID.Size = new System.Drawing.Size(85, 16);
          this.lblLastUID.TabIndex = 3;
          this.lblLastUID.Text = "labelControl4";
          // 
          // labelControl3
          // 
          this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.labelControl3.Appearance.Options.UseFont = true;
          this.labelControl3.Location = new System.Drawing.Point(12, 154);
          this.labelControl3.Name = "labelControl3";
          this.labelControl3.Size = new System.Drawing.Size(40, 16);
          this.labelControl3.TabIndex = 2;
          this.labelControl3.Text = "Type :";
          // 
          // labelControl2
          // 
          this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.labelControl2.Appearance.Options.UseFont = true;
          this.labelControl2.Location = new System.Drawing.Point(12, 118);
          this.labelControl2.Name = "labelControl2";
          this.labelControl2.Size = new System.Drawing.Size(69, 16);
          this.labelControl2.TabIndex = 1;
          this.labelControl2.Text = "Category :";
          // 
          // lblLastLabelUID
          // 
          this.lblLastLabelUID.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lblLastLabelUID.Appearance.Options.UseFont = true;
          this.lblLastLabelUID.Location = new System.Drawing.Point(12, 49);
          this.lblLastLabelUID.Name = "lblLastLabelUID";
          this.lblLastLabelUID.Size = new System.Drawing.Size(101, 16);
          this.lblLastLabelUID.TabIndex = 0;
          this.lblLastLabelUID.Text = "Last Label UID :";
          // 
          // dxErrorProvider
          // 
          this.dxErrorProvider.ContainerControl = this;
          // 
          // FrmLabel
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(321, 240);
          this.ControlBox = false;
          this.Controls.Add(this.gcHeader);
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "FrmLabel";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
          this.Text = "Create New Label UID";
          this.Load += new System.EventHandler(this.FrmLabel_Load);
          ((System.ComponentModel.ISupportInitialize)(this.gcHeader)).EndInit();
          this.gcHeader.ResumeLayout(false);
          this.gcHeader.PerformLayout();
          ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.lookUpEditType.Properties)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.txtItemUID.Properties)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
          this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcHeader;
        private DevExpress.XtraEditors.LabelControl lblLastLabelUID;
        private DevExpress.XtraEditors.LabelControl lblLastUID;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.Utils.ImageCollection imgSmallImageCollection;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtItemUID;
        private DevExpress.XtraEditors.LabelControl lblLabelUID;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditType;
        private DevExpress.XtraEditors.LabelControl lblCategory;
        private DevExpress.XtraEditors.SimpleButton btnClose;
    }
}