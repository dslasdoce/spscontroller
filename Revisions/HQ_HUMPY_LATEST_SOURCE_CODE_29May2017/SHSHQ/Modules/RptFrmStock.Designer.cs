namespace ISM.Modules
{
    partial class RptFrmStock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RptFrmStock));
            this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.groupControlSelection = new DevExpress.XtraEditors.GroupControl();
            this.lookUpEditCategory = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.lblCurLocUID = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditLocationUID = new DevExpress.XtraEditors.LookUpEdit();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnReport = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panelHeader = new DevExpress.XtraEditors.PanelControl();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).BeginInit();
            this.groupControlSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditLocationUID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).BeginInit();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            this.SuspendLayout();
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
            // groupControl
            // 
            this.groupControl.Controls.Add(this.groupControlSelection);
            this.groupControl.Controls.Add(this.groupControl1);
            this.groupControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl.Location = new System.Drawing.Point(0, 0);
            this.groupControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControl.Name = "groupControl";
            this.groupControl.ShowCaption = false;
            this.groupControl.Size = new System.Drawing.Size(1433, 672);
            this.groupControl.TabIndex = 8;
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
            this.groupControlSelection.Controls.Add(this.lookUpEditCategory);
            this.groupControlSelection.Controls.Add(this.labelControl19);
            this.groupControlSelection.Controls.Add(this.lblCurLocUID);
            this.groupControlSelection.Controls.Add(this.lookUpEditLocationUID);
            this.groupControlSelection.Controls.Add(this.btnClear);
            this.groupControlSelection.Controls.Add(this.btnReport);
            this.groupControlSelection.Location = new System.Drawing.Point(2, 55);
            this.groupControlSelection.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControlSelection.Name = "groupControlSelection";
            this.groupControlSelection.Size = new System.Drawing.Size(1426, 615);
            this.groupControlSelection.TabIndex = 0;
            this.groupControlSelection.Text = "Selection Criteria";
            // 
            // lookUpEditCategory
            // 
            this.lookUpEditCategory.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.lookUpEditCategory, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.lookUpEditCategory.Location = new System.Drawing.Point(168, 103);
            this.lookUpEditCategory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lookUpEditCategory.Name = "lookUpEditCategory";
            this.lookUpEditCategory.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditCategory.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditCategory.Properties.NullText = "";
            this.lookUpEditCategory.Properties.PopupSizeable = false;
            this.lookUpEditCategory.Properties.PopupWidth = 300;
            this.lookUpEditCategory.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpEditCategory.Size = new System.Drawing.Size(229, 26);
            this.lookUpEditCategory.TabIndex = 49;
            this.lookUpEditCategory.EditValueChanged += new System.EventHandler(this.lookUpEditCategory_EditValueChanged);
            // 
            // labelControl19
            // 
            this.labelControl19.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl19.Appearance.Options.UseFont = true;
            this.labelControl19.Location = new System.Drawing.Point(13, 107);
            this.labelControl19.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(85, 19);
            this.labelControl19.TabIndex = 50;
            this.labelControl19.Text = "Category :";
            // 
            // lblCurLocUID
            // 
            this.lblCurLocUID.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurLocUID.Appearance.Options.UseFont = true;
            this.lblCurLocUID.Location = new System.Drawing.Point(13, 60);
            this.lblCurLocUID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblCurLocUID.Name = "lblCurLocUID";
            this.lblCurLocUID.Size = new System.Drawing.Size(116, 19);
            this.lblCurLocUID.TabIndex = 32;
            this.lblCurLocUID.Text = "Location UID :";
            // 
            // lookUpEditLocationUID
            // 
            this.lookUpEditLocationUID.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.lookUpEditLocationUID, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.lookUpEditLocationUID.Location = new System.Drawing.Point(169, 57);
            this.lookUpEditLocationUID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lookUpEditLocationUID.Name = "lookUpEditLocationUID";
            this.lookUpEditLocationUID.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditLocationUID.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditLocationUID.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditLocationUID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditLocationUID.Properties.MaxLength = 13;
            this.lookUpEditLocationUID.Properties.NullText = "Select a Location UID";
            this.lookUpEditLocationUID.Properties.PopupSizeable = false;
            this.lookUpEditLocationUID.Properties.PopupWidth = 430;
            this.lookUpEditLocationUID.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpEditLocationUID.Size = new System.Drawing.Size(229, 26);
            this.lookUpEditLocationUID.TabIndex = 0;
            this.lookUpEditLocationUID.EditValueChanged += new System.EventHandler(this.lookUpEditLocationUID_EditValueChanged);
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageIndex = 0;
            this.btnClear.ImageList = this.imgSmallImageCollection;
            this.btnClear.Location = new System.Drawing.Point(271, 172);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(126, 43);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnReport
            // 
            this.btnReport.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Appearance.Options.UseFont = true;
            this.btnReport.ImageIndex = 2;
            this.btnReport.ImageList = this.imgSmallImageCollection;
            this.btnReport.Location = new System.Drawing.Point(114, 172);
            this.btnReport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(126, 43);
            this.btnReport.TabIndex = 10;
            this.btnReport.Text = "Get Report";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.panelHeader);
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(1429, 587);
            this.groupControl1.TabIndex = 5;
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(2, 2);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1425, 49);
            this.panelHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Appearance.Options.UseFont = true;
            this.lblHeader.Location = new System.Drawing.Point(26, 10);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(294, 29);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "ISM 8000 - Stock Report";
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // RptFrmStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl);
            this.LookAndFeel.SkinName = "Black";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "RptFrmStock";
            this.Size = new System.Drawing.Size(1433, 672);
            this.Load += new System.EventHandler(this.RptFrmStock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).EndInit();
            this.groupControlSelection.ResumeLayout(false);
            this.groupControlSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditLocationUID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ImageCollection imgSmallImageCollection;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.XtraEditors.GroupControl groupControl;
        private DevExpress.XtraEditors.GroupControl groupControlSelection;
        private DevExpress.XtraEditors.LabelControl lblCurLocUID;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditLocationUID;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        public DevExpress.XtraEditors.SimpleButton btnReport;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelHeader;
        public DevExpress.XtraEditors.LabelControl lblHeader;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditCategory;
        private DevExpress.XtraEditors.LabelControl labelControl19;
    }
}
