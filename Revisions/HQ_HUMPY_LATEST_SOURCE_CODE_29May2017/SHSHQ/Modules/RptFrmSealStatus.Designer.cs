namespace ISM.Modules
{
    partial class RptFrmSealStatus
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RptFrmSealStatus));
            this.panelHeader = new DevExpress.XtraEditors.PanelControl();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControlSelection = new DevExpress.XtraEditors.GroupControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditSealStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.lblCurLocUID = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditLocationUID = new DevExpress.XtraEditors.LookUpEdit();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnReport = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).BeginInit();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).BeginInit();
            this.groupControlSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditSealStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditLocationUID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(2, 2);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1067, 40);
            this.panelHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Appearance.Options.UseFont = true;
            this.lblHeader.Location = new System.Drawing.Point(22, 8);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(290, 23);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "ISM 8000 - Seal Status Report";
            // 
            // imgSmallImageCollection
            // 
            this.imgSmallImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgSmallImageCollection.ImageStream")));
            this.imgSmallImageCollection.Images.SetKeyName(0, "refresh22.png");
            this.imgSmallImageCollection.Images.SetKeyName(1, "saveas22.png");
            this.imgSmallImageCollection.Images.SetKeyName(2, "readcomments.gif");
            this.imgSmallImageCollection.Images.SetKeyName(3, "notifications_cctitle.gif");
            this.imgSmallImageCollection.Images.SetKeyName(4, "excel.gif");
            this.imgSmallImageCollection.Images.SetKeyName(5, "search.gif");
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.panelHeader);
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(1071, 408);
            this.groupControl1.TabIndex = 5;
            // 
            // groupControlSelection
            // 
            this.groupControlSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlSelection.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlSelection.Appearance.Options.UseFont = true;
            this.groupControlSelection.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlSelection.AppearanceCaption.Options.UseFont = true;
            this.groupControlSelection.Controls.Add(this.labelControl3);
            this.groupControlSelection.Controls.Add(this.lookUpEditSealStatus);
            this.groupControlSelection.Controls.Add(this.lblCurLocUID);
            this.groupControlSelection.Controls.Add(this.lookUpEditLocationUID);
            this.groupControlSelection.Controls.Add(this.btnClear);
            this.groupControlSelection.Controls.Add(this.btnReport);
            this.groupControlSelection.Location = new System.Drawing.Point(2, 45);
            this.groupControlSelection.Name = "groupControlSelection";
            this.groupControlSelection.Size = new System.Drawing.Size(1069, 431);
            this.groupControlSelection.TabIndex = 0;
            this.groupControlSelection.Text = "Selection Criteria";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(21, 70);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(83, 16);
            this.labelControl3.TabIndex = 77;
            this.labelControl3.Text = "Seal Status :";
            // 
            // lookUpEditSealStatus
            // 
            this.lookUpEditSealStatus.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.lookUpEditSealStatus, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.lookUpEditSealStatus.Location = new System.Drawing.Point(132, 67);
            this.lookUpEditSealStatus.Name = "lookUpEditSealStatus";
            this.lookUpEditSealStatus.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditSealStatus.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditSealStatus.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditSealStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditSealStatus.Properties.MaxLength = 13;
            this.lookUpEditSealStatus.Properties.NullText = "";
            this.lookUpEditSealStatus.Properties.PopupSizeable = false;
            this.lookUpEditSealStatus.Properties.PopupWidth = 250;
            this.lookUpEditSealStatus.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpEditSealStatus.Size = new System.Drawing.Size(182, 22);
            this.lookUpEditSealStatus.TabIndex = 2;
            this.lookUpEditSealStatus.EditValueChanged += new System.EventHandler(this.lookUpEditSealStatus_EditValueChanged);
            // 
            // lblCurLocUID
            // 
            this.lblCurLocUID.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurLocUID.Appearance.Options.UseFont = true;
            this.lblCurLocUID.Location = new System.Drawing.Point(21, 41);
            this.lblCurLocUID.Name = "lblCurLocUID";
            this.lblCurLocUID.Size = new System.Drawing.Size(90, 16);
            this.lblCurLocUID.TabIndex = 72;
            this.lblCurLocUID.Text = "Location UID :";
            // 
            // lookUpEditLocationUID
            // 
            this.lookUpEditLocationUID.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.lookUpEditLocationUID, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.lookUpEditLocationUID.Location = new System.Drawing.Point(132, 38);
            this.lookUpEditLocationUID.Name = "lookUpEditLocationUID";
            this.lookUpEditLocationUID.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditLocationUID.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditLocationUID.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditLocationUID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditLocationUID.Properties.MaxLength = 13;
            this.lookUpEditLocationUID.Properties.NullText = "";
            this.lookUpEditLocationUID.Properties.PopupSizeable = false;
            this.lookUpEditLocationUID.Properties.PopupWidth = 430;
            this.lookUpEditLocationUID.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpEditLocationUID.Size = new System.Drawing.Size(182, 22);
            this.lookUpEditLocationUID.TabIndex = 0;
            this.lookUpEditLocationUID.EditValueChanged += new System.EventHandler(this.lookUpEditLocationUID_EditValueChanged);
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageIndex = 0;
            this.btnClear.ImageList = this.imgSmallImageCollection;
            this.btnClear.Location = new System.Drawing.Point(206, 114);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(108, 35);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnReport
            // 
            this.btnReport.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Appearance.Options.UseFont = true;
            this.btnReport.ImageIndex = 2;
            this.btnReport.ImageList = this.imgSmallImageCollection;
            this.btnReport.Location = new System.Drawing.Point(71, 114);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(108, 35);
            this.btnReport.TabIndex = 7;
            this.btnReport.Text = "Get Report";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // groupControl
            // 
            this.groupControl.Controls.Add(this.groupControlSelection);
            this.groupControl.Controls.Add(this.groupControl1);
            this.groupControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl.Location = new System.Drawing.Point(0, 0);
            this.groupControl.Name = "groupControl";
            this.groupControl.ShowCaption = false;
            this.groupControl.Size = new System.Drawing.Size(1075, 477);
            this.groupControl.TabIndex = 11;
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // RptFrmSealStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl);
            this.LookAndFeel.SkinName = "Black";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "RptFrmSealStatus";
            this.Size = new System.Drawing.Size(1075, 477);
            this.Load += new System.EventHandler(this.RptFrmSealStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).EndInit();
            this.groupControlSelection.ResumeLayout(false);
            this.groupControlSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditSealStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditLocationUID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.XtraEditors.GroupControl groupControl;
        private DevExpress.XtraEditors.GroupControl groupControlSelection;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.Utils.ImageCollection imgSmallImageCollection;
        public DevExpress.XtraEditors.SimpleButton btnReport;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelHeader;
        public DevExpress.XtraEditors.LabelControl lblHeader;
        private DevExpress.XtraEditors.LabelControl lblCurLocUID;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditLocationUID;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditSealStatus;
    }
}
