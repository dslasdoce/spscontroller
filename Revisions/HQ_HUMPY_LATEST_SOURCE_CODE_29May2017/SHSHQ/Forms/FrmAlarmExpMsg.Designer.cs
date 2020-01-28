namespace ISM.Forms
{
    partial class FrmAlarmExpMsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAlarmExpMsg));
            this.gcHeader = new DevExpress.XtraEditors.GroupControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.txtComment = new DevExpress.XtraEditors.MemoEdit();
            this.lblLabelUID = new DevExpress.XtraEditors.LabelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.lblExpType = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblLastLabelUID = new DevExpress.XtraEditors.LabelControl();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.lblExpDescription = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gcHeader)).BeginInit();
            this.gcHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblExpDescription.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcHeader
            // 
            this.gcHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcHeader.Appearance.Options.UseFont = true;
            this.gcHeader.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcHeader.AppearanceCaption.Options.UseFont = true;
            this.gcHeader.Controls.Add(this.lblExpDescription);
            this.gcHeader.Controls.Add(this.btnCancel);
            this.gcHeader.Controls.Add(this.txtComment);
            this.gcHeader.Controls.Add(this.lblLabelUID);
            this.gcHeader.Controls.Add(this.btnClear);
            this.gcHeader.Controls.Add(this.btnSave);
            this.gcHeader.Controls.Add(this.lblExpType);
            this.gcHeader.Controls.Add(this.labelControl2);
            this.gcHeader.Controls.Add(this.lblLastLabelUID);
            this.gcHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcHeader.Location = new System.Drawing.Point(0, 0);
            this.gcHeader.Name = "gcHeader";
            this.gcHeader.ShowCaption = false;
            this.gcHeader.Size = new System.Drawing.Size(446, 249);
            this.gcHeader.TabIndex = 1;
            this.gcHeader.Text = "Alarm && Exception ";
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.ImageIndex = 6;
            this.btnCancel.ImageList = this.imgSmallImageCollection;
            this.btnCancel.Location = new System.Drawing.Point(233, 202);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 35);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.imgSmallImageCollection.Images.SetKeyName(6, "Cancel.png");
            // 
            // txtComment
            // 
            this.dxErrorProvider.SetIconAlignment(this.txtComment, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtComment.Location = new System.Drawing.Point(127, 105);
            this.txtComment.Name = "txtComment";
            this.txtComment.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComment.Properties.Appearance.Options.UseFont = true;
            this.txtComment.Size = new System.Drawing.Size(307, 82);
            this.txtComment.TabIndex = 0;
            // 
            // lblLabelUID
            // 
            this.lblLabelUID.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabelUID.Appearance.Options.UseFont = true;
            this.lblLabelUID.Location = new System.Drawing.Point(12, 138);
            this.lblLabelUID.Name = "lblLabelUID";
            this.lblLabelUID.Size = new System.Drawing.Size(69, 16);
            this.lblLabelUID.TabIndex = 8;
            this.lblLabelUID.Text = "Comment :";
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageIndex = 2;
            this.btnClear.ImageList = this.imgSmallImageCollection;
            this.btnClear.Location = new System.Drawing.Point(339, 202);
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
            this.btnSave.Location = new System.Drawing.Point(127, 202);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 35);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblExpType
            // 
            this.lblExpType.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpType.Appearance.Options.UseFont = true;
            this.lblExpType.Location = new System.Drawing.Point(127, 14);
            this.lblExpType.Name = "lblExpType";
            this.lblExpType.Size = new System.Drawing.Size(53, 16);
            this.lblExpType.TabIndex = 3;
            this.lblExpType.Text = "ExpType";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(12, 61);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(82, 16);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Description :";
            // 
            // lblLastLabelUID
            // 
            this.lblLastLabelUID.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastLabelUID.Appearance.Options.UseFont = true;
            this.lblLastLabelUID.Location = new System.Drawing.Point(12, 14);
            this.lblLastLabelUID.Name = "lblLastLabelUID";
            this.lblLastLabelUID.Size = new System.Drawing.Size(40, 16);
            this.lblLastLabelUID.TabIndex = 0;
            this.lblLastLabelUID.Text = "Type :";
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // lblExpDescription
            // 
            this.dxErrorProvider.SetIconAlignment(this.lblExpDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.lblExpDescription.Location = new System.Drawing.Point(127, 39);
            this.lblExpDescription.Name = "lblExpDescription";
            this.lblExpDescription.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpDescription.Properties.Appearance.Options.UseFont = true;
            this.lblExpDescription.Properties.ReadOnly = true;
            this.lblExpDescription.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.lblExpDescription.Size = new System.Drawing.Size(307, 60);
            this.lblExpDescription.TabIndex = 13;
            // 
            // FrmAlarmExpMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 249);
            this.ControlBox = false;
            this.Controls.Add(this.gcHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAlarmExpMsg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alarm & Exception ";
            this.Load += new System.EventHandler(this.FrmAlarmExpMsg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcHeader)).EndInit();
            this.gcHeader.ResumeLayout(false);
            this.gcHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblExpDescription.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.XtraEditors.GroupControl gcHeader;
        private DevExpress.Utils.ImageCollection imgSmallImageCollection;
        private DevExpress.XtraEditors.LabelControl lblLabelUID;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl lblExpType;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblLastLabelUID;
        private DevExpress.XtraEditors.MemoEdit txtComment;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.MemoEdit lblExpDescription;
    }
}