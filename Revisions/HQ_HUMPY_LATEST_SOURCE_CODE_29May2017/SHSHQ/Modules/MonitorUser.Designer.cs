namespace ISM.Modules
{
  partial class MonitorUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorUser));
            this.groupControlSelection = new DevExpress.XtraEditors.GroupControl();
            this.btnExcelReport = new DevExpress.XtraEditors.SimpleButton();
            this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.rdoDataView = new DevExpress.XtraEditors.RadioGroup();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.lookUpEditTaskStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditOperatorID = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lblOperatorID = new DevExpress.XtraEditors.LabelControl();
            this.lblLocUID = new DevExpress.XtraEditors.LabelControl();
            this.dateEditFrom = new DevExpress.XtraEditors.DateEdit();
            this.btnReport = new DevExpress.XtraEditors.SimpleButton();
            this.dateEditTo = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControlGrid = new DevExpress.XtraEditors.GroupControl();
            this.gCOperatorMonitor = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.panelHeader = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).BeginInit();
            this.groupControlSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoDataView.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTaskStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditOperatorID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlGrid)).BeginInit();
            this.groupControlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gCOperatorMonitor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).BeginInit();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            this.SuspendLayout();
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
            this.groupControlSelection.Controls.Add(this.rdoDataView);
            this.groupControlSelection.Controls.Add(this.btnClear);
            this.groupControlSelection.Controls.Add(this.lookUpEditTaskStatus);
            this.groupControlSelection.Controls.Add(this.lookUpEditOperatorID);
            this.groupControlSelection.Controls.Add(this.labelControl5);
            this.groupControlSelection.Controls.Add(this.labelControl4);
            this.groupControlSelection.Controls.Add(this.lblOperatorID);
            this.groupControlSelection.Controls.Add(this.lblLocUID);
            this.groupControlSelection.Controls.Add(this.dateEditFrom);
            this.groupControlSelection.Controls.Add(this.btnReport);
            this.groupControlSelection.Controls.Add(this.dateEditTo);
            this.groupControlSelection.Controls.Add(this.labelControl1);
            this.groupControlSelection.Location = new System.Drawing.Point(1, 341);
            this.groupControlSelection.Name = "groupControlSelection";
            this.groupControlSelection.Size = new System.Drawing.Size(898, 142);
            this.groupControlSelection.TabIndex = 0;
            this.groupControlSelection.Text = "Selection Criteria";
            // 
            // btnExcelReport
            // 
            this.btnExcelReport.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcelReport.Appearance.Options.UseFont = true;
            this.btnExcelReport.ImageIndex = 4;
            this.btnExcelReport.ImageList = this.imgSmallImageCollection;
            this.btnExcelReport.Location = new System.Drawing.Point(535, 87);
            this.btnExcelReport.Name = "btnExcelReport";
            this.btnExcelReport.Size = new System.Drawing.Size(112, 35);
            this.btnExcelReport.TabIndex = 7;
            this.btnExcelReport.Text = "Export Excel";
            this.btnExcelReport.Click += new System.EventHandler(this.btnExcelReport_Click);
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
            // rdoDataView
            // 
            this.rdoDataView.Location = new System.Drawing.Point(348, 37);
            this.rdoDataView.Name = "rdoDataView";
            this.rdoDataView.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.rdoDataView.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoDataView.Properties.Appearance.Options.UseBackColor = true;
            this.rdoDataView.Properties.Appearance.Options.UseFont = true;
            this.rdoDataView.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rdoDataView.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "Task"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "Exceptions")});
            this.rdoDataView.Size = new System.Drawing.Size(181, 21);
            this.rdoDataView.TabIndex = 3;
            this.rdoDataView.SelectedIndexChanged += new System.EventHandler(this.rdoDataView_SelectedIndexChanged);
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageIndex = 0;
            this.btnClear.ImageList = this.imgSmallImageCollection;
            this.btnClear.Location = new System.Drawing.Point(655, 37);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(112, 35);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lookUpEditTaskStatus
            // 
            this.lookUpEditTaskStatus.EnterMoveNextControl = true;
            this.lookUpEditTaskStatus.Location = new System.Drawing.Point(104, 70);
            this.lookUpEditTaskStatus.Name = "lookUpEditTaskStatus";
            this.lookUpEditTaskStatus.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditTaskStatus.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditTaskStatus.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditTaskStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditTaskStatus.Properties.NullText = "";
            this.lookUpEditTaskStatus.Properties.PopupSizeable = false;
            this.lookUpEditTaskStatus.Properties.PopupWidth = 200;
            this.lookUpEditTaskStatus.Size = new System.Drawing.Size(145, 22);
            this.lookUpEditTaskStatus.TabIndex = 2;
            this.lookUpEditTaskStatus.EditValueChanged += new System.EventHandler(this.lookUpEditTaskStatus_EditValueChanged);
            // 
            // lookUpEditOperatorID
            // 
            this.lookUpEditOperatorID.EnterMoveNextControl = true;
            this.lookUpEditOperatorID.Location = new System.Drawing.Point(104, 34);
            this.lookUpEditOperatorID.Name = "lookUpEditOperatorID";
            this.lookUpEditOperatorID.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditOperatorID.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditOperatorID.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditOperatorID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditOperatorID.Properties.NullText = "";
            this.lookUpEditOperatorID.Properties.PopupSizeable = false;
            this.lookUpEditOperatorID.Properties.PopupWidth = 200;
            this.lookUpEditOperatorID.Size = new System.Drawing.Size(145, 22);
            this.lookUpEditOperatorID.TabIndex = 0;
            this.lookUpEditOperatorID.EditValueChanged += new System.EventHandler(this.lookUpEditOperatorID_EditValueChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(264, 39);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(78, 16);
            this.labelControl5.TabIndex = 32;
            this.labelControl5.Text = "Data View :";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(2, 73);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(90, 16);
            this.labelControl4.TabIndex = 30;
            this.labelControl4.Text = "Task Status :";
            // 
            // lblOperatorID
            // 
            this.lblOperatorID.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperatorID.Location = new System.Drawing.Point(2, 37);
            this.lblOperatorID.Name = "lblOperatorID";
            this.lblOperatorID.Size = new System.Drawing.Size(60, 16);
            this.lblOperatorID.TabIndex = 1;
            this.lblOperatorID.Text = "User ID :";
            // 
            // lblLocUID
            // 
            this.lblLocUID.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocUID.Location = new System.Drawing.Point(264, 73);
            this.lblLocUID.Name = "lblLocUID";
            this.lblLocUID.Size = new System.Drawing.Size(80, 16);
            this.lblLocUID.TabIndex = 3;
            this.lblLocUID.Text = "From Date :";
            // 
            // dateEditFrom
            // 
            this.dateEditFrom.EditValue = null;
            this.dxErrorProvider.SetIconAlignment(this.dateEditFrom, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dateEditFrom.Location = new System.Drawing.Point(353, 70);
            this.dateEditFrom.Name = "dateEditFrom";
            this.dateEditFrom.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEditFrom.Properties.Appearance.Options.UseFont = true;
            this.dateEditFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditFrom.Size = new System.Drawing.Size(123, 22);
            this.dateEditFrom.TabIndex = 4;
            this.dateEditFrom.EditValueChanged += new System.EventHandler(this.dateEditFrom_EditValueChanged);
            // 
            // btnReport
            // 
            this.btnReport.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Appearance.Options.UseFont = true;
            this.btnReport.ImageIndex = 2;
            this.btnReport.ImageList = this.imgSmallImageCollection;
            this.btnReport.Location = new System.Drawing.Point(535, 37);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(112, 35);
            this.btnReport.TabIndex = 6;
            this.btnReport.Text = "Get Report";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // dateEditTo
            // 
            this.dateEditTo.EditValue = null;
            this.dxErrorProvider.SetIconAlignment(this.dateEditTo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dateEditTo.Location = new System.Drawing.Point(353, 103);
            this.dateEditTo.Name = "dateEditTo";
            this.dateEditTo.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEditTo.Properties.Appearance.Options.UseFont = true;
            this.dateEditTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditTo.Size = new System.Drawing.Size(123, 22);
            this.dateEditTo.TabIndex = 5;
            this.dateEditTo.EditValueChanged += new System.EventHandler(this.dateEditTo_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(264, 106);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 16);
            this.labelControl1.TabIndex = 25;
            this.labelControl1.Text = "To Date :";
            // 
            // groupControlGrid
            // 
            this.groupControlGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlGrid.Controls.Add(this.gCOperatorMonitor);
            this.groupControlGrid.Location = new System.Drawing.Point(0, 47);
            this.groupControlGrid.Name = "groupControlGrid";
            this.groupControlGrid.ShowCaption = false;
            this.groupControlGrid.Size = new System.Drawing.Size(898, 484);
            this.groupControlGrid.TabIndex = 2;
            this.groupControlGrid.Text = "Trace Data";
            // 
            // gCOperatorMonitor
            // 
            this.gCOperatorMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gCOperatorMonitor.Location = new System.Drawing.Point(3, 3);
            this.gCOperatorMonitor.MainView = this.gridView;
            this.gCOperatorMonitor.Name = "gCOperatorMonitor";
            this.gCOperatorMonitor.Size = new System.Drawing.Size(892, 478);
            this.gCOperatorMonitor.TabIndex = 0;
            this.gCOperatorMonitor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gridView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.gridView.GridControl = this.gCOperatorMonitor;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsBehavior.ReadOnly = true;
            this.gridView.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView.OptionsSelection.UseIndicatorForSelection = false;
            this.gridView.OptionsView.EnableAppearanceEvenRow = true;
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
            this.groupControl.Size = new System.Drawing.Size(898, 484);
            this.groupControl.TabIndex = 4;
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.labelControl2);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(3, 3);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(892, 40);
            this.panelHeader.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(21, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(236, 23);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "ISM 7000 - User Monitor";
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // MonitorUser
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl);
            this.LookAndFeel.SkinName = "Blue";
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "MonitorUser";
            this.Size = new System.Drawing.Size(898, 484);
            this.Load += new System.EventHandler(this.MonitorOperator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).EndInit();
            this.groupControlSelection.ResumeLayout(false);
            this.groupControlSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoDataView.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTaskStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditOperatorID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlGrid)).EndInit();
            this.groupControlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gCOperatorMonitor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private DevExpress.XtraEditors.GroupControl groupControlSelection;
    private DevExpress.XtraEditors.SimpleButton btnClear;
    private DevExpress.XtraEditors.LookUpEdit lookUpEditTaskStatus;
    private DevExpress.XtraEditors.LookUpEdit lookUpEditOperatorID;
    private DevExpress.XtraEditors.LabelControl labelControl5;
    private DevExpress.XtraEditors.LabelControl labelControl4;
    private DevExpress.XtraEditors.LabelControl lblOperatorID;
    private DevExpress.XtraEditors.LabelControl lblLocUID;
    private DevExpress.XtraEditors.DateEdit dateEditFrom;
    private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
    private DevExpress.XtraEditors.GroupControl groupControl;
    private DevExpress.XtraEditors.GroupControl groupControlGrid;
    private DevExpress.XtraGrid.GridControl gCOperatorMonitor;
    private DevExpress.XtraGrid.Views.Grid.GridView gridView;
    private DevExpress.XtraEditors.PanelControl panelHeader;
    public DevExpress.XtraEditors.LabelControl labelControl2;
    public DevExpress.XtraEditors.SimpleButton btnReport;
    private DevExpress.XtraEditors.DateEdit dateEditTo;
    private DevExpress.XtraEditors.LabelControl labelControl1;
    private DevExpress.XtraEditors.RadioGroup rdoDataView;
    public DevExpress.XtraEditors.SimpleButton btnExcelReport;
    private DevExpress.Utils.ImageCollection imgSmallImageCollection;

  }
}
