namespace ISM.Modules
{
    partial class RptFrmIssue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RptFrmIssue));
            this.groupControlSelection = new DevExpress.XtraEditors.GroupControl();
            this.lookUpEditCategory = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditIssuedBy = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lblLocUID = new DevExpress.XtraEditors.LabelControl();
            this.dateEditFrom = new DevExpress.XtraEditors.DateEdit();
            this.dateEditTo = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.btnReport = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panelHeader = new DevExpress.XtraEditors.PanelControl();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).BeginInit();
            this.groupControlSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditIssuedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).BeginInit();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            this.SuspendLayout();
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
            this.groupControlSelection.Controls.Add(this.lookUpEditIssuedBy);
            this.groupControlSelection.Controls.Add(this.labelControl5);
            this.groupControlSelection.Controls.Add(this.lblLocUID);
            this.groupControlSelection.Controls.Add(this.dateEditFrom);
            this.groupControlSelection.Controls.Add(this.dateEditTo);
            this.groupControlSelection.Controls.Add(this.labelControl1);
            this.groupControlSelection.Controls.Add(this.btnClear);
            this.groupControlSelection.Controls.Add(this.btnReport);
            this.groupControlSelection.Location = new System.Drawing.Point(0, 43);
            this.groupControlSelection.Name = "groupControlSelection";
            this.groupControlSelection.Size = new System.Drawing.Size(0, 0);
            this.groupControlSelection.TabIndex = 0;
            this.groupControlSelection.Text = "Selection Criteria";
            // 
            // lookUpEditCategory
            // 
            this.lookUpEditCategory.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.lookUpEditCategory, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.lookUpEditCategory.Location = new System.Drawing.Point(162, 76);
            this.lookUpEditCategory.Name = "lookUpEditCategory";
            this.lookUpEditCategory.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditCategory.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditCategory.Properties.NullText = "";
            this.lookUpEditCategory.Properties.PopupSizeable = false;
            this.lookUpEditCategory.Properties.PopupWidth = 300;
            this.lookUpEditCategory.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpEditCategory.Size = new System.Drawing.Size(196, 22);
            this.lookUpEditCategory.TabIndex = 1;
            this.lookUpEditCategory.EditValueChanged += new System.EventHandler(this.lookUpEditCategory_EditValueChanged);
            // 
            // labelControl19
            // 
            this.labelControl19.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl19.Appearance.Options.UseFont = true;
            this.labelControl19.Location = new System.Drawing.Point(22, 51);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(71, 16);
            this.labelControl19.TabIndex = 54;
            this.labelControl19.Text = "Category :";
            // 
            // lookUpEditIssuedBy
            // 
            this.lookUpEditIssuedBy.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.lookUpEditIssuedBy, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.lookUpEditIssuedBy.Location = new System.Drawing.Point(162, 46);
            this.lookUpEditIssuedBy.Name = "lookUpEditIssuedBy";
            this.lookUpEditIssuedBy.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditIssuedBy.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditIssuedBy.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditIssuedBy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditIssuedBy.Properties.MaxLength = 55;
            this.lookUpEditIssuedBy.Properties.NullText = "";
            this.lookUpEditIssuedBy.Properties.PopupSizeable = false;
            this.lookUpEditIssuedBy.Properties.PopupWidth = 300;
            this.lookUpEditIssuedBy.Size = new System.Drawing.Size(197, 22);
            this.lookUpEditIssuedBy.TabIndex = 0;
            this.lookUpEditIssuedBy.EditValueChanged += new System.EventHandler(this.lookUpEditIssuedBy_EditValueChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(22, 21);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(76, 16);
            this.labelControl5.TabIndex = 42;
            this.labelControl5.Text = "Issued By :";
            // 
            // lblLocUID
            // 
            this.lblLocUID.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocUID.Appearance.Options.UseFont = true;
            this.lblLocUID.Location = new System.Drawing.Point(22, 81);
            this.lblLocUID.Name = "lblLocUID";
            this.lblLocUID.Size = new System.Drawing.Size(130, 16);
            this.lblLocUID.TabIndex = 35;
            this.lblLocUID.Text = "Issued From Date :";
            // 
            // dateEditFrom
            // 
            this.dateEditFrom.EditValue = null;
            this.dxErrorProvider.SetIconAlignment(this.dateEditFrom, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dateEditFrom.Location = new System.Drawing.Point(162, 106);
            this.dateEditFrom.Name = "dateEditFrom";
            this.dateEditFrom.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEditFrom.Properties.Appearance.Options.UseFont = true;
            this.dateEditFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditFrom.Size = new System.Drawing.Size(144, 22);
            this.dateEditFrom.TabIndex = 2;
            this.dateEditFrom.EditValueChanged += new System.EventHandler(this.dateEditFrom_EditValueChanged);
            // 
            // dateEditTo
            // 
            this.dateEditTo.EditValue = null;
            this.dxErrorProvider.SetIconAlignment(this.dateEditTo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dateEditTo.Location = new System.Drawing.Point(162, 136);
            this.dateEditTo.Name = "dateEditTo";
            this.dateEditTo.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEditTo.Properties.Appearance.Options.UseFont = true;
            this.dateEditTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditTo.Size = new System.Drawing.Size(144, 22);
            this.dateEditTo.TabIndex = 3;
            this.dateEditTo.EditValueChanged += new System.EventHandler(this.dateEditTo_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(22, 111);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(114, 16);
            this.labelControl1.TabIndex = 39;
            this.labelControl1.Text = "Issued To Date :";
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageIndex = 0;
            this.btnClear.ImageList = this.imgSmallImageCollection;
            this.btnClear.Location = new System.Drawing.Point(251, 186);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(108, 35);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
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
            // btnReport
            // 
            this.btnReport.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Appearance.Options.UseFont = true;
            this.btnReport.ImageIndex = 2;
            this.btnReport.ImageList = this.imgSmallImageCollection;
            this.btnReport.Location = new System.Drawing.Point(92, 186);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(108, 35);
            this.btnReport.TabIndex = 4;
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
            this.groupControl.Size = new System.Drawing.Size(0, 0);
            this.groupControl.TabIndex = 10;
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.panelHeader);
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(1, 0);
            this.groupControl1.TabIndex = 5;
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(1, 1);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(0, 40);
            this.panelHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Appearance.Options.UseFont = true;
            this.lblHeader.Location = new System.Drawing.Point(21, 8);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(304, 23);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "ISM 8000 - Stock Issues Report";
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // RptFrmIssue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl);
            this.LookAndFeel.SkinName = "Black";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "RptFrmIssue";
            this.Size = new System.Drawing.Size(0, 0);
            this.Load += new System.EventHandler(this.RptFrmIssue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).EndInit();
            this.groupControlSelection.ResumeLayout(false);
            this.groupControlSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditIssuedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControlSelection;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditIssuedBy;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl lblLocUID;
        private DevExpress.XtraEditors.DateEdit dateEditFrom;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.XtraEditors.GroupControl groupControl;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelHeader;
        public DevExpress.XtraEditors.LabelControl lblHeader;
        private DevExpress.XtraEditors.DateEdit dateEditTo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.Utils.ImageCollection imgSmallImageCollection;
        public DevExpress.XtraEditors.SimpleButton btnReport;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditCategory;
        private DevExpress.XtraEditors.LabelControl labelControl19;
    }
}
