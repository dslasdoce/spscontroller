namespace ISM.Modules
{
    partial class RptFrmActiveReaderActy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RptFrmActiveReaderActy));
            this.panelHeader = new DevExpress.XtraEditors.PanelControl();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.groupControlSelection = new DevExpress.XtraEditors.GroupControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditReader = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditPortal = new DevExpress.XtraEditors.LookUpEdit();
            this.lblFromDate = new DevExpress.XtraEditors.LabelControl();
            this.dateEditFrom = new DevExpress.XtraEditors.DateEdit();
            this.dateEditTo = new DevExpress.XtraEditors.DateEdit();
            this.lblToDate = new DevExpress.XtraEditors.LabelControl();
            this.lblLocUID1 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditLocationUID = new DevExpress.XtraEditors.LookUpEdit();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.btnReport = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).BeginInit();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).BeginInit();
            this.groupControlSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditReader.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditPortal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditLocationUID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
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
            this.lblHeader.Size = new System.Drawing.Size(384, 23);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "ISM 8000 - aRFID Portal Activity Report";
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
            // groupControlSelection
            // 
            this.groupControlSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlSelection.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlSelection.Appearance.Options.UseFont = true;
            this.groupControlSelection.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlSelection.AppearanceCaption.Options.UseFont = true;
            this.groupControlSelection.Controls.Add(this.labelControl2);
            this.groupControlSelection.Controls.Add(this.labelControl1);
            this.groupControlSelection.Controls.Add(this.lookUpEditReader);
            this.groupControlSelection.Controls.Add(this.lookUpEditPortal);
            this.groupControlSelection.Controls.Add(this.lblFromDate);
            this.groupControlSelection.Controls.Add(this.dateEditFrom);
            this.groupControlSelection.Controls.Add(this.dateEditTo);
            this.groupControlSelection.Controls.Add(this.lblToDate);
            this.groupControlSelection.Controls.Add(this.lblLocUID1);
            this.groupControlSelection.Controls.Add(this.lookUpEditLocationUID);
            this.groupControlSelection.Controls.Add(this.btnClear);
            this.groupControlSelection.Controls.Add(this.btnReport);
            this.groupControlSelection.Location = new System.Drawing.Point(0, 43);
            this.groupControlSelection.Name = "groupControlSelection";
            this.groupControlSelection.Size = new System.Drawing.Size(0, 0);
            this.groupControlSelection.TabIndex = 1;
            this.groupControlSelection.Text = "Selection Criteria";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(22, 83);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(95, 16);
            this.labelControl2.TabIndex = 72;
            this.labelControl2.Text = "Reader Name :";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(22, 52);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(87, 16);
            this.labelControl1.TabIndex = 71;
            this.labelControl1.Text = "Portal Name :";
            // 
            // lookUpEditReader
            // 
            this.lookUpEditReader.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.lookUpEditReader, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.lookUpEditReader.Location = new System.Drawing.Point(127, 108);
            this.lookUpEditReader.Name = "lookUpEditReader";
            this.lookUpEditReader.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditReader.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditReader.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditReader.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditReader.Properties.MaxLength = 13;
            this.lookUpEditReader.Properties.NullText = "";
            this.lookUpEditReader.Properties.PopupSizeable = false;
            this.lookUpEditReader.Properties.PopupWidth = 430;
            this.lookUpEditReader.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpEditReader.Size = new System.Drawing.Size(196, 22);
            this.lookUpEditReader.TabIndex = 2;
            this.lookUpEditReader.EditValueChanged += new System.EventHandler(this.lookUpEditReader_EditValueChanged);
            // 
            // lookUpEditPortal
            // 
            this.lookUpEditPortal.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.lookUpEditPortal, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.lookUpEditPortal.Location = new System.Drawing.Point(127, 77);
            this.lookUpEditPortal.Name = "lookUpEditPortal";
            this.lookUpEditPortal.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditPortal.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditPortal.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditPortal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditPortal.Properties.MaxLength = 13;
            this.lookUpEditPortal.Properties.NullText = "";
            this.lookUpEditPortal.Properties.PopupSizeable = false;
            this.lookUpEditPortal.Properties.PopupWidth = 430;
            this.lookUpEditPortal.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpEditPortal.Size = new System.Drawing.Size(196, 22);
            this.lookUpEditPortal.TabIndex = 1;
            this.lookUpEditPortal.EditValueChanged += new System.EventHandler(this.lookUpEditPortal_EditValueChanged);
            // 
            // lblFromDate
            // 
            this.lblFromDate.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.Appearance.Options.UseFont = true;
            this.lblFromDate.Location = new System.Drawing.Point(22, 114);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(80, 16);
            this.lblFromDate.TabIndex = 67;
            this.lblFromDate.Text = "From Date :";
            // 
            // dateEditFrom
            // 
            this.dateEditFrom.EditValue = null;
            this.dxErrorProvider.SetIconAlignment(this.dateEditFrom, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dateEditFrom.Location = new System.Drawing.Point(127, 139);
            this.dateEditFrom.Name = "dateEditFrom";
            this.dateEditFrom.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEditFrom.Properties.Appearance.Options.UseFont = true;
            this.dateEditFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditFrom.Size = new System.Drawing.Size(144, 22);
            this.dateEditFrom.TabIndex = 3;
            this.dateEditFrom.EditValueChanged += new System.EventHandler(this.dateEditFrom_EditValueChanged);
            // 
            // dateEditTo
            // 
            this.dateEditTo.EditValue = null;
            this.dxErrorProvider.SetIconAlignment(this.dateEditTo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dateEditTo.Location = new System.Drawing.Point(127, 170);
            this.dateEditTo.Name = "dateEditTo";
            this.dateEditTo.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEditTo.Properties.Appearance.Options.UseFont = true;
            this.dateEditTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditTo.Size = new System.Drawing.Size(144, 22);
            this.dateEditTo.TabIndex = 4;
            this.dateEditTo.EditValueChanged += new System.EventHandler(this.dateEditTo_EditValueChanged);
            // 
            // lblToDate
            // 
            this.lblToDate.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.Appearance.Options.UseFont = true;
            this.lblToDate.Location = new System.Drawing.Point(22, 145);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(64, 16);
            this.lblToDate.TabIndex = 68;
            this.lblToDate.Text = "To Date :";
            // 
            // lblLocUID1
            // 
            this.lblLocUID1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocUID1.Appearance.Options.UseFont = true;
            this.lblLocUID1.Location = new System.Drawing.Point(22, 21);
            this.lblLocUID1.Name = "lblLocUID1";
            this.lblLocUID1.Size = new System.Drawing.Size(90, 16);
            this.lblLocUID1.TabIndex = 61;
            this.lblLocUID1.Text = "Location UID :";
            // 
            // lookUpEditLocationUID
            // 
            this.lookUpEditLocationUID.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.lookUpEditLocationUID, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.lookUpEditLocationUID.Location = new System.Drawing.Point(127, 46);
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
            this.lookUpEditLocationUID.Size = new System.Drawing.Size(196, 22);
            this.lookUpEditLocationUID.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageIndex = 0;
            this.btnClear.ImageList = this.imgSmallImageCollection;
            this.btnClear.Location = new System.Drawing.Point(215, 221);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(108, 35);
            this.btnClear.TabIndex = 6;
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
            this.btnReport.Location = new System.Drawing.Point(56, 221);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(108, 35);
            this.btnReport.TabIndex = 5;
            this.btnReport.Text = "Get Report";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
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
            this.groupControl1.Size = new System.Drawing.Size(0, 0);
            this.groupControl1.TabIndex = 5;
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // RptFrmActiveReaderActy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl);
            this.LookAndFeel.SkinName = "Black";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "RptFrmActiveReaderActy";
            this.Size = new System.Drawing.Size(0, 0);
            this.Load += new System.EventHandler(this.RptFrmActiveReaderActy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).EndInit();
            this.groupControlSelection.ResumeLayout(false);
            this.groupControlSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditReader.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditPortal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditLocationUID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelHeader;
        public DevExpress.XtraEditors.LabelControl lblHeader;
        private DevExpress.XtraEditors.GroupControl groupControl;
        private DevExpress.XtraEditors.GroupControl groupControlSelection;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.Utils.ImageCollection imgSmallImageCollection;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl lblFromDate;
        private DevExpress.XtraEditors.DateEdit dateEditFrom;
        private DevExpress.XtraEditors.DateEdit dateEditTo;
        private DevExpress.XtraEditors.LabelControl lblToDate;
        private DevExpress.XtraEditors.LabelControl lblLocUID1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditLocationUID;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        public DevExpress.XtraEditors.SimpleButton btnReport;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditReader;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditPortal;
    }
}
