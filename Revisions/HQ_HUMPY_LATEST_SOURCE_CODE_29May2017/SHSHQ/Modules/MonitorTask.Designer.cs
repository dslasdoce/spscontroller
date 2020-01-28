namespace ISM.Modules
{
  partial class MonitorTask
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorTask));
            this.panelHeader = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblTaskID = new DevExpress.XtraEditors.LabelControl();
            this.lblLocUID = new DevExpress.XtraEditors.LabelControl();
            this.btnReport = new DevExpress.XtraEditors.SimpleButton();
            this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateEditTo = new DevExpress.XtraEditors.DateEdit();
            this.dateEditFrom = new DevExpress.XtraEditors.DateEdit();
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.groupControlSelection = new DevExpress.XtraEditors.GroupControl();
            this.btnExcelReport = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.lookUpEditAssignee = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditTaskStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditTaskType = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditTaskID = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.groupControlGrid = new DevExpress.XtraEditors.GroupControl();
            this.gvTaskMonitor = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).BeginInit();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).BeginInit();
            this.groupControlSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditAssignee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTaskStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTaskType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTaskID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlGrid)).BeginInit();
            this.groupControlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTaskMonitor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.labelControl2);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(0, 40);
            this.panelHeader.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(21, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(237, 23);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "ISM 7000 - Task Monitor";
            // 
            // lblTaskID
            // 
            this.lblTaskID.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaskID.Appearance.Options.UseFont = true;
            this.lblTaskID.Location = new System.Drawing.Point(2, 37);
            this.lblTaskID.Name = "lblTaskID";
            this.lblTaskID.Size = new System.Drawing.Size(62, 16);
            this.lblTaskID.TabIndex = 1;
            this.lblTaskID.Text = "Task ID :";
            // 
            // lblLocUID
            // 
            this.lblLocUID.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocUID.Appearance.Options.UseFont = true;
            this.lblLocUID.Location = new System.Drawing.Point(329, 70);
            this.lblLocUID.Name = "lblLocUID";
            this.lblLocUID.Size = new System.Drawing.Size(80, 16);
            this.lblLocUID.TabIndex = 3;
            this.lblLocUID.Text = "From Date :";
            // 
            // btnReport
            // 
            this.btnReport.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Appearance.Options.UseFont = true;
            this.btnReport.ImageIndex = 2;
            this.btnReport.ImageList = this.imgSmallImageCollection;
            this.btnReport.Location = new System.Drawing.Point(564, 31);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(113, 35);
            this.btnReport.TabIndex = 6;
            this.btnReport.Text = "Get Report";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // imgSmallImageCollection
            // 
            this.imgSmallImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgSmallImageCollection.ImageStream")));
            this.imgSmallImageCollection.Images.SetKeyName(0, "refresh22.png");
            this.imgSmallImageCollection.Images.SetKeyName(1, "saveas22.png");
            this.imgSmallImageCollection.Images.SetKeyName(2, "readcomments.gif");
            this.imgSmallImageCollection.Images.SetKeyName(3, "notifications_cctitle.gif");
            this.imgSmallImageCollection.Images.SetKeyName(4, "excel.gif");
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(329, 103);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 16);
            this.labelControl1.TabIndex = 25;
            this.labelControl1.Text = "To Date :";
            // 
            // dateEditTo
            // 
            this.dateEditTo.EditValue = null;
            this.dxErrorProvider.SetIconAlignment(this.dateEditTo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dateEditTo.Location = new System.Drawing.Point(414, 100);
            this.dateEditTo.Name = "dateEditTo";
            this.dateEditTo.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEditTo.Properties.Appearance.Options.UseFont = true;
            this.dateEditTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditTo.Size = new System.Drawing.Size(144, 22);
            this.dateEditTo.TabIndex = 5;
            // 
            // dateEditFrom
            // 
            this.dateEditFrom.EditValue = null;
            this.dxErrorProvider.SetIconAlignment(this.dateEditFrom, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dateEditFrom.Location = new System.Drawing.Point(414, 67);
            this.dateEditFrom.Name = "dateEditFrom";
            this.dateEditFrom.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEditFrom.Properties.Appearance.Options.UseFont = true;
            this.dateEditFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditFrom.Size = new System.Drawing.Size(144, 22);
            this.dateEditFrom.TabIndex = 4;
            // 
            // groupControl
            // 
            this.groupControl.Controls.Add(this.groupControlSelection);
            this.groupControl.Controls.Add(this.groupControlGrid);
            this.groupControl.Controls.Add(this.panelHeader);
            this.groupControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl.Location = new System.Drawing.Point(0, 0);
            this.groupControl.Name = "groupControl";
            this.groupControl.ShowCaption = false;
            this.groupControl.Size = new System.Drawing.Size(0, 0);
            this.groupControl.TabIndex = 3;
            // 
            // groupControlSelection
            // 
            this.groupControlSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlSelection.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlSelection.Appearance.Options.UseFont = true;
            this.groupControlSelection.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlSelection.AppearanceCaption.Options.UseFont = true;
            this.groupControlSelection.Controls.Add(this.btnExcelReport);
            this.groupControlSelection.Controls.Add(this.btnClear);
            this.groupControlSelection.Controls.Add(this.lookUpEditAssignee);
            this.groupControlSelection.Controls.Add(this.lookUpEditTaskStatus);
            this.groupControlSelection.Controls.Add(this.lookUpEditTaskType);
            this.groupControlSelection.Controls.Add(this.lookUpEditTaskID);
            this.groupControlSelection.Controls.Add(this.labelControl5);
            this.groupControlSelection.Controls.Add(this.labelControl4);
            this.groupControlSelection.Controls.Add(this.labelControl3);
            this.groupControlSelection.Controls.Add(this.lblTaskID);
            this.groupControlSelection.Controls.Add(this.lblLocUID);
            this.groupControlSelection.Controls.Add(this.dateEditFrom);
            this.groupControlSelection.Controls.Add(this.btnReport);
            this.groupControlSelection.Controls.Add(this.dateEditTo);
            this.groupControlSelection.Controls.Add(this.labelControl1);
            this.groupControlSelection.Location = new System.Drawing.Point(0, -140);
            this.groupControlSelection.Name = "groupControlSelection";
            this.groupControlSelection.Size = new System.Drawing.Size(0, 138);
            this.groupControlSelection.TabIndex = 0;
            this.groupControlSelection.Text = "Selection Criteria";
            // 
            // btnExcelReport
            // 
            this.btnExcelReport.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcelReport.Appearance.Options.UseFont = true;
            this.btnExcelReport.ImageIndex = 4;
            this.btnExcelReport.ImageList = this.imgSmallImageCollection;
            this.btnExcelReport.Location = new System.Drawing.Point(564, 87);
            this.btnExcelReport.Name = "btnExcelReport";
            this.btnExcelReport.Size = new System.Drawing.Size(113, 35);
            this.btnExcelReport.TabIndex = 7;
            this.btnExcelReport.Text = "Export Excel";
            this.btnExcelReport.Click += new System.EventHandler(this.btnExcelReport_Click);
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageIndex = 0;
            this.btnClear.ImageList = this.imgSmallImageCollection;
            this.btnClear.Location = new System.Drawing.Point(683, 31);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(113, 35);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lookUpEditAssignee
            // 
            this.lookUpEditAssignee.EnterMoveNextControl = true;
            this.lookUpEditAssignee.Location = new System.Drawing.Point(414, 31);
            this.lookUpEditAssignee.Name = "lookUpEditAssignee";
            this.lookUpEditAssignee.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditAssignee.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditAssignee.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditAssignee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditAssignee.Properties.MaxLength = 55;
            this.lookUpEditAssignee.Properties.NullText = "";
            this.lookUpEditAssignee.Properties.PopupSizeable = false;
            this.lookUpEditAssignee.Properties.PopupWidth = 300;
            this.lookUpEditAssignee.Size = new System.Drawing.Size(144, 22);
            this.lookUpEditAssignee.TabIndex = 3;
            this.lookUpEditAssignee.EditValueChanged += new System.EventHandler(this.lookUpEditAssignee_EditValueChanged);
            // 
            // lookUpEditTaskStatus
            // 
            this.lookUpEditTaskStatus.EnterMoveNextControl = true;
            this.lookUpEditTaskStatus.Location = new System.Drawing.Point(124, 100);
            this.lookUpEditTaskStatus.Name = "lookUpEditTaskStatus";
            this.lookUpEditTaskStatus.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditTaskStatus.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditTaskStatus.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditTaskStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditTaskStatus.Properties.NullText = "";
            this.lookUpEditTaskStatus.Properties.PopupSizeable = false;
            this.lookUpEditTaskStatus.Properties.PopupWidth = 200;
            this.lookUpEditTaskStatus.Size = new System.Drawing.Size(197, 22);
            this.lookUpEditTaskStatus.TabIndex = 2;
            this.lookUpEditTaskStatus.EditValueChanged += new System.EventHandler(this.lookUpEditTaskStatus_EditValueChanged);
            // 
            // lookUpEditTaskType
            // 
            this.lookUpEditTaskType.EnterMoveNextControl = true;
            this.lookUpEditTaskType.Location = new System.Drawing.Point(124, 67);
            this.lookUpEditTaskType.Name = "lookUpEditTaskType";
            this.lookUpEditTaskType.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditTaskType.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditTaskType.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditTaskType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditTaskType.Properties.NullText = "";
            this.lookUpEditTaskType.Properties.PopupSizeable = false;
            this.lookUpEditTaskType.Properties.PopupWidth = 200;
            this.lookUpEditTaskType.Size = new System.Drawing.Size(197, 22);
            this.lookUpEditTaskType.TabIndex = 1;
            this.lookUpEditTaskType.EditValueChanged += new System.EventHandler(this.lookUpEditTaskType_EditValueChanged);
            // 
            // lookUpEditTaskID
            // 
            this.lookUpEditTaskID.EnterMoveNextControl = true;
            this.lookUpEditTaskID.Location = new System.Drawing.Point(124, 34);
            this.lookUpEditTaskID.Name = "lookUpEditTaskID";
            this.lookUpEditTaskID.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditTaskID.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditTaskID.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditTaskID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditTaskID.Properties.MaxLength = 20;
            this.lookUpEditTaskID.Properties.NullText = "";
            this.lookUpEditTaskID.Properties.PopupSizeable = false;
            this.lookUpEditTaskID.Properties.PopupWidth = 200;
            this.lookUpEditTaskID.Size = new System.Drawing.Size(197, 22);
            this.lookUpEditTaskID.TabIndex = 0;
            this.lookUpEditTaskID.EditValueChanged += new System.EventHandler(this.lookUpEditTaskID_EditValueChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(329, 34);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 16);
            this.labelControl5.TabIndex = 32;
            this.labelControl5.Text = "User ID :";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(2, 103);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(90, 16);
            this.labelControl4.TabIndex = 30;
            this.labelControl4.Text = "Task Status :";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(2, 70);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(116, 16);
            this.labelControl3.TabIndex = 28;
            this.labelControl3.Text = "Operation Type :";
            // 
            // groupControlGrid
            // 
            this.groupControlGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlGrid.Controls.Add(this.gvTaskMonitor);
            this.groupControlGrid.Location = new System.Drawing.Point(0, 47);
            this.groupControlGrid.Name = "groupControlGrid";
            this.groupControlGrid.ShowCaption = false;
            this.groupControlGrid.Size = new System.Drawing.Size(0, 0);
            this.groupControlGrid.TabIndex = 28;
            this.groupControlGrid.Text = "Trace Data";
            // 
            // gvTaskMonitor
            // 
            this.gvTaskMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvTaskMonitor.Location = new System.Drawing.Point(0, 0);
            this.gvTaskMonitor.MainView = this.gridView;
            this.gvTaskMonitor.Name = "gvTaskMonitor";
            this.gvTaskMonitor.Size = new System.Drawing.Size(0, 0);
            this.gvTaskMonitor.TabIndex = 0;
            this.gvTaskMonitor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gridView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.gridView.GridControl = this.gvTaskMonitor;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsBehavior.ReadOnly = true;
            this.gridView.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView.OptionsSelection.UseIndicatorForSelection = false;
            this.gridView.OptionsView.ColumnAutoWidth = false;
            this.gridView.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // MonitorTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl);
            this.LookAndFeel.SkinName = "Blue";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MonitorTask";
            this.Size = new System.Drawing.Size(0, 0);
            this.Load += new System.EventHandler(this.MonitorTask_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).EndInit();
            this.groupControlSelection.ResumeLayout(false);
            this.groupControlSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditAssignee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTaskStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTaskType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTaskID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlGrid)).EndInit();
            this.groupControlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvTaskMonitor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private DevExpress.XtraEditors.PanelControl panelHeader;
    private DevExpress.XtraEditors.LabelControl lblTaskID;
    private DevExpress.XtraEditors.LabelControl lblLocUID;
    public DevExpress.XtraEditors.SimpleButton btnReport;
    private DevExpress.XtraEditors.LabelControl labelControl1;
    private DevExpress.XtraEditors.DateEdit dateEditTo;
    private DevExpress.XtraEditors.DateEdit dateEditFrom;
    private DevExpress.XtraEditors.GroupControl groupControl;
    public DevExpress.XtraEditors.LabelControl labelControl2;
    private DevExpress.XtraEditors.GroupControl groupControlGrid;
    private DevExpress.XtraGrid.GridControl gvTaskMonitor;
    private DevExpress.XtraGrid.Views.Grid.GridView gridView;
    private DevExpress.XtraEditors.GroupControl groupControlSelection;
    private DevExpress.XtraEditors.LabelControl labelControl5;
    private DevExpress.XtraEditors.LabelControl labelControl4;
    private DevExpress.XtraEditors.LabelControl labelControl3;
    private DevExpress.XtraEditors.LookUpEdit lookUpEditTaskStatus;
    private DevExpress.XtraEditors.LookUpEdit lookUpEditTaskType;
    private DevExpress.XtraEditors.LookUpEdit lookUpEditTaskID;
    private DevExpress.XtraEditors.LookUpEdit lookUpEditAssignee;
    private DevExpress.XtraEditors.SimpleButton btnClear;
    private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
    public DevExpress.XtraEditors.SimpleButton btnExcelReport;
    private DevExpress.Utils.ImageCollection imgSmallImageCollection;
  }
}
