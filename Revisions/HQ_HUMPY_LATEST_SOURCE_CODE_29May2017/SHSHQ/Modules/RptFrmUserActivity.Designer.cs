namespace ISM.Modules
{
    partial class RptFrmUserActivity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RptFrmUserActivity));
            this.panelHeader = new DevExpress.XtraEditors.PanelControl();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.groupControlSelection = new DevExpress.XtraEditors.GroupControl();
            this.lookUpEditTaskStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditOperator = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditTaskType = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lblLocUID = new DevExpress.XtraEditors.LabelControl();
            this.dateEditFrom = new DevExpress.XtraEditors.DateEdit();
            this.dateEditTo = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.btnReport = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).BeginInit();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).BeginInit();
            this.groupControlSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTaskStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditOperator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTaskType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(2, 2);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1416, 49);
            this.panelHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Appearance.Options.UseFont = true;
            this.lblHeader.Location = new System.Drawing.Point(26, 10);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(379, 29);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "ISM 8000 - User Activity Report";
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
            this.groupControlSelection.Controls.Add(this.lookUpEditTaskStatus);
            this.groupControlSelection.Controls.Add(this.labelControl2);
            this.groupControlSelection.Controls.Add(this.lookUpEditOperator);
            this.groupControlSelection.Controls.Add(this.lookUpEditTaskType);
            this.groupControlSelection.Controls.Add(this.labelControl5);
            this.groupControlSelection.Controls.Add(this.labelControl3);
            this.groupControlSelection.Controls.Add(this.lblLocUID);
            this.groupControlSelection.Controls.Add(this.dateEditFrom);
            this.groupControlSelection.Controls.Add(this.dateEditTo);
            this.groupControlSelection.Controls.Add(this.labelControl1);
            this.groupControlSelection.Controls.Add(this.btnClear);
            this.groupControlSelection.Controls.Add(this.btnReport);
            this.groupControlSelection.Location = new System.Drawing.Point(2, 55);
            this.groupControlSelection.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControlSelection.Name = "groupControlSelection";
            this.groupControlSelection.Size = new System.Drawing.Size(1418, 622);
            this.groupControlSelection.TabIndex = 0;
            this.groupControlSelection.Text = "Selection Criteria";
            // 
            // lookUpEditTaskStatus
            // 
            this.lookUpEditTaskStatus.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.lookUpEditTaskStatus, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.lookUpEditTaskStatus.Location = new System.Drawing.Point(169, 140);
            this.lookUpEditTaskStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lookUpEditTaskStatus.Name = "lookUpEditTaskStatus";
            this.lookUpEditTaskStatus.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditTaskStatus.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditTaskStatus.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditTaskStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditTaskStatus.Properties.NullText = "";
            this.lookUpEditTaskStatus.Properties.PopupSizeable = false;
            this.lookUpEditTaskStatus.Properties.PopupWidth = 200;
            this.lookUpEditTaskStatus.Size = new System.Drawing.Size(230, 26);
            this.lookUpEditTaskStatus.TabIndex = 2;
            this.lookUpEditTaskStatus.EditValueChanged += new System.EventHandler(this.lookUpEditTaskStatus_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(28, 144);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(63, 20);
            this.labelControl2.TabIndex = 44;
            this.labelControl2.Text = "Status :";
            // 
            // lookUpEditOperator
            // 
            this.lookUpEditOperator.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.lookUpEditOperator, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.lookUpEditOperator.Location = new System.Drawing.Point(169, 57);
            this.lookUpEditOperator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lookUpEditOperator.Name = "lookUpEditOperator";
            this.lookUpEditOperator.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditOperator.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditOperator.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditOperator.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditOperator.Properties.MaxLength = 55;
            this.lookUpEditOperator.Properties.NullText = "";
            this.lookUpEditOperator.Properties.PopupSizeable = false;
            this.lookUpEditOperator.Properties.PopupWidth = 300;
            this.lookUpEditOperator.Size = new System.Drawing.Size(230, 26);
            this.lookUpEditOperator.TabIndex = 0;
            this.lookUpEditOperator.EditValueChanged += new System.EventHandler(this.lookUpEditOperator_EditValueChanged);
            // 
            // lookUpEditTaskType
            // 
            this.lookUpEditTaskType.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.lookUpEditTaskType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.lookUpEditTaskType.Location = new System.Drawing.Point(169, 98);
            this.lookUpEditTaskType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lookUpEditTaskType.Name = "lookUpEditTaskType";
            this.lookUpEditTaskType.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditTaskType.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditTaskType.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditTaskType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditTaskType.Properties.NullText = "";
            this.lookUpEditTaskType.Properties.PopupSizeable = false;
            this.lookUpEditTaskType.Properties.PopupWidth = 200;
            this.lookUpEditTaskType.Size = new System.Drawing.Size(230, 26);
            this.lookUpEditTaskType.TabIndex = 1;
            this.lookUpEditTaskType.EditValueChanged += new System.EventHandler(this.lookUpEditTaskType_EditValueChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(28, 60);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(72, 20);
            this.labelControl5.TabIndex = 42;
            this.labelControl5.Text = "User ID :";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(28, 102);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(91, 20);
            this.labelControl3.TabIndex = 40;
            this.labelControl3.Text = "Task Type :";
            // 
            // lblLocUID
            // 
            this.lblLocUID.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocUID.Appearance.Options.UseFont = true;
            this.lblLocUID.Location = new System.Drawing.Point(28, 186);
            this.lblLocUID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblLocUID.Name = "lblLocUID";
            this.lblLocUID.Size = new System.Drawing.Size(95, 20);
            this.lblLocUID.TabIndex = 35;
            this.lblLocUID.Text = "From Date :";
            // 
            // dateEditFrom
            // 
            this.dateEditFrom.EditValue = null;
            this.dxErrorProvider.SetIconAlignment(this.dateEditFrom, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dateEditFrom.Location = new System.Drawing.Point(169, 182);
            this.dateEditFrom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateEditFrom.Name = "dateEditFrom";
            this.dateEditFrom.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEditFrom.Properties.Appearance.Options.UseFont = true;
            this.dateEditFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditFrom.Size = new System.Drawing.Size(168, 26);
            this.dateEditFrom.TabIndex = 3;
            this.dateEditFrom.EditValueChanged += new System.EventHandler(this.dateEditFrom_EditValueChanged);
            // 
            // dateEditTo
            // 
            this.dateEditTo.EditValue = null;
            this.dxErrorProvider.SetIconAlignment(this.dateEditTo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dateEditTo.Location = new System.Drawing.Point(169, 224);
            this.dateEditTo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateEditTo.Name = "dateEditTo";
            this.dateEditTo.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEditTo.Properties.Appearance.Options.UseFont = true;
            this.dateEditTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditTo.Size = new System.Drawing.Size(168, 26);
            this.dateEditTo.TabIndex = 4;
            this.dateEditTo.EditValueChanged += new System.EventHandler(this.dateEditTo_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(28, 228);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(74, 20);
            this.labelControl1.TabIndex = 39;
            this.labelControl1.Text = "To Date :";
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageIndex = 0;
            this.btnClear.ImageList = this.imgSmallImageCollection;
            this.btnClear.Location = new System.Drawing.Point(273, 283);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(126, 43);
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
            this.btnReport.Location = new System.Drawing.Point(103, 283);
            this.btnReport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(126, 43);
            this.btnReport.TabIndex = 5;
            this.btnReport.Text = "Get Report";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
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
            this.groupControl.Size = new System.Drawing.Size(1425, 679);
            this.groupControl.TabIndex = 9;
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
            this.groupControl1.Size = new System.Drawing.Size(1420, 594);
            this.groupControl1.TabIndex = 5;
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // RptFrmUserActivity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl);
            this.LookAndFeel.SkinName = "Black";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "RptFrmUserActivity";
            this.Size = new System.Drawing.Size(1425, 679);
            this.Load += new System.EventHandler(this.RptFrmUserActivity_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).EndInit();
            this.groupControlSelection.ResumeLayout(false);
            this.groupControlSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTaskStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditOperator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTaskType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelHeader;
        public DevExpress.XtraEditors.LabelControl lblHeader;
        private DevExpress.XtraEditors.GroupControl groupControlSelection;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.XtraEditors.GroupControl groupControl;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.Utils.ImageCollection imgSmallImageCollection;
        public DevExpress.XtraEditors.SimpleButton btnReport;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditOperator;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditTaskType;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl lblLocUID;
        private DevExpress.XtraEditors.DateEdit dateEditFrom;
        private DevExpress.XtraEditors.DateEdit dateEditTo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditTaskStatus;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}
