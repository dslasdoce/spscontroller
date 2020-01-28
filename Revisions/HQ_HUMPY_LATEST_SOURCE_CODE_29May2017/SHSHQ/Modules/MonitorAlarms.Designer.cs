namespace ISM.Modules
{
  partial class MonitorAlarms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorAlarms));
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControlSelection = new DevExpress.XtraEditors.GroupControl();
            this.btnExcelReport = new DevExpress.XtraEditors.SimpleButton();
            this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.lookUpEditLoc = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditDevice = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lblDeviceName = new DevExpress.XtraEditors.LabelControl();
            this.lblLocUID = new DevExpress.XtraEditors.LabelControl();
            this.dateEditFrom = new DevExpress.XtraEditors.DateEdit();
            this.btnReport = new DevExpress.XtraEditors.SimpleButton();
            this.dateEditTo = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControlGrid = new DevExpress.XtraEditors.GroupControl();
            this.gvAlarmsMonitor = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelHeader = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).BeginInit();
            this.groupControlSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditLoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditDevice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlGrid)).BeginInit();
            this.groupControlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvAlarmsMonitor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).BeginInit();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl
            // 
            this.groupControl.Controls.Add(this.groupControl1);
            this.groupControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl.Location = new System.Drawing.Point(0, 0);
            this.groupControl.Name = "groupControl";
            this.groupControl.ShowCaption = false;
            this.groupControl.Size = new System.Drawing.Size(0, 0);
            this.groupControl.TabIndex = 5;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.groupControlSelection);
            this.groupControl1.Controls.Add(this.groupControlGrid);
            this.groupControl1.Controls.Add(this.panelHeader);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(0, 0);
            this.groupControl1.TabIndex = 5;
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
            this.groupControlSelection.Controls.Add(this.lookUpEditLoc);
            this.groupControlSelection.Controls.Add(this.lookUpEditDevice);
            this.groupControlSelection.Controls.Add(this.labelControl3);
            this.groupControlSelection.Controls.Add(this.lblDeviceName);
            this.groupControlSelection.Controls.Add(this.lblLocUID);
            this.groupControlSelection.Controls.Add(this.dateEditFrom);
            this.groupControlSelection.Controls.Add(this.btnReport);
            this.groupControlSelection.Controls.Add(this.dateEditTo);
            this.groupControlSelection.Controls.Add(this.labelControl1);
            this.groupControlSelection.Location = new System.Drawing.Point(-1, -124);
            this.groupControlSelection.Name = "groupControlSelection";
            this.groupControlSelection.Size = new System.Drawing.Size(0, 125);
            this.groupControlSelection.TabIndex = 0;
            this.groupControlSelection.Text = "Selection Criteria";
            // 
            // btnExcelReport
            // 
            this.btnExcelReport.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcelReport.Appearance.Options.UseFont = true;
            this.btnExcelReport.ImageIndex = 4;
            this.btnExcelReport.ImageList = this.imgSmallImageCollection;
            this.btnExcelReport.Location = new System.Drawing.Point(536, 86);
            this.btnExcelReport.Name = "btnExcelReport";
            this.btnExcelReport.Size = new System.Drawing.Size(115, 35);
            this.btnExcelReport.TabIndex = 5;
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
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageIndex = 0;
            this.btnClear.ImageList = this.imgSmallImageCollection;
            this.btnClear.Location = new System.Drawing.Point(657, 38);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(115, 35);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lookUpEditLoc
            // 
            this.lookUpEditLoc.EnterMoveNextControl = true;
            this.lookUpEditLoc.Location = new System.Drawing.Point(132, 78);
            this.lookUpEditLoc.Name = "lookUpEditLoc";
            this.lookUpEditLoc.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditLoc.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditLoc.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditLoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditLoc.Properties.MaxLength = 13;
            this.lookUpEditLoc.Properties.NullText = "";
            this.lookUpEditLoc.Properties.PopupSizeable = false;
            this.lookUpEditLoc.Properties.PopupWidth = 500;
            this.lookUpEditLoc.Size = new System.Drawing.Size(149, 22);
            this.lookUpEditLoc.TabIndex = 1;
            this.lookUpEditLoc.EditValueChanged += new System.EventHandler(this.lookUpEditLoc_EditValueChanged);
            // 
            // lookUpEditDevice
            // 
            this.lookUpEditDevice.EnterMoveNextControl = true;
            this.lookUpEditDevice.Location = new System.Drawing.Point(132, 41);
            this.lookUpEditDevice.Name = "lookUpEditDevice";
            this.lookUpEditDevice.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditDevice.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditDevice.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditDevice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditDevice.Properties.MaxLength = 50;
            this.lookUpEditDevice.Properties.NullText = "";
            this.lookUpEditDevice.Properties.PopupSizeable = false;
            this.lookUpEditDevice.Properties.PopupWidth = 200;
            this.lookUpEditDevice.Size = new System.Drawing.Size(149, 22);
            this.lookUpEditDevice.TabIndex = 0;
            this.lookUpEditDevice.EditValueChanged += new System.EventHandler(this.lookUpEditDevice_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(21, 81);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(100, 16);
            this.labelControl3.TabIndex = 28;
            this.labelControl3.Text = "Loc/Seal UID :";
            // 
            // lblDeviceName
            // 
            this.lblDeviceName.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeviceName.Appearance.Options.UseFont = true;
            this.lblDeviceName.Location = new System.Drawing.Point(21, 44);
            this.lblDeviceName.Name = "lblDeviceName";
            this.lblDeviceName.Size = new System.Drawing.Size(102, 16);
            this.lblDeviceName.TabIndex = 1;
            this.lblDeviceName.Text = "Device Name :";
            // 
            // lblLocUID
            // 
            this.lblLocUID.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocUID.Appearance.Options.UseFont = true;
            this.lblLocUID.Location = new System.Drawing.Point(312, 41);
            this.lblLocUID.Name = "lblLocUID";
            this.lblLocUID.Size = new System.Drawing.Size(80, 16);
            this.lblLocUID.TabIndex = 3;
            this.lblLocUID.Text = "From Date :";
            // 
            // dateEditFrom
            // 
            this.dateEditFrom.EditValue = null;
            this.dxErrorProvider.SetIconAlignment(this.dateEditFrom, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dateEditFrom.Location = new System.Drawing.Point(399, 38);
            this.dateEditFrom.Name = "dateEditFrom";
            this.dateEditFrom.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEditFrom.Properties.Appearance.Options.UseFont = true;
            this.dateEditFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditFrom.Size = new System.Drawing.Size(119, 22);
            this.dateEditFrom.TabIndex = 2;
            // 
            // btnReport
            // 
            this.btnReport.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Appearance.Options.UseFont = true;
            this.btnReport.ImageIndex = 2;
            this.btnReport.ImageList = this.imgSmallImageCollection;
            this.btnReport.Location = new System.Drawing.Point(536, 38);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(115, 35);
            this.btnReport.TabIndex = 4;
            this.btnReport.Text = "Get Report";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // dateEditTo
            // 
            this.dateEditTo.EditValue = null;
            this.dxErrorProvider.SetIconAlignment(this.dateEditTo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dateEditTo.Location = new System.Drawing.Point(399, 75);
            this.dateEditTo.Name = "dateEditTo";
            this.dateEditTo.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEditTo.Properties.Appearance.Options.UseFont = true;
            this.dateEditTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditTo.Size = new System.Drawing.Size(119, 22);
            this.dateEditTo.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(312, 78);
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
            this.groupControlGrid.Controls.Add(this.gvAlarmsMonitor);
            this.groupControlGrid.Location = new System.Drawing.Point(0, 47);
            this.groupControlGrid.Name = "groupControlGrid";
            this.groupControlGrid.ShowCaption = false;
            this.groupControlGrid.Size = new System.Drawing.Size(0, 0);
            this.groupControlGrid.TabIndex = 2;
            this.groupControlGrid.Text = "Trace Data";
            // 
            // gvAlarmsMonitor
            // 
            this.gvAlarmsMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvAlarmsMonitor.Location = new System.Drawing.Point(0, 0);
            this.gvAlarmsMonitor.MainView = this.gridView;
            this.gvAlarmsMonitor.Name = "gvAlarmsMonitor";
            this.gvAlarmsMonitor.Size = new System.Drawing.Size(0, 0);
            this.gvAlarmsMonitor.TabIndex = 0;
            this.gvAlarmsMonitor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gridView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.gridView.GridControl = this.gvAlarmsMonitor;
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
            this.labelControl2.Size = new System.Drawing.Size(259, 23);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "ISM 7000 - Alarms Monitor";
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // MonitorAlarms
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl);
            this.LookAndFeel.SkinName = "Blue";
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LookAndFeel.UseWindowsXPTheme = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MonitorAlarms";
            this.Size = new System.Drawing.Size(0, 0);
            this.Load += new System.EventHandler(this.MonitorAlarms_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).EndInit();
            this.groupControlSelection.ResumeLayout(false);
            this.groupControlSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditLoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditDevice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlGrid)).EndInit();
            this.groupControlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvAlarmsMonitor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private DevExpress.XtraEditors.GroupControl groupControl;
    private DevExpress.XtraEditors.GroupControl groupControl1;
    private DevExpress.XtraEditors.GroupControl groupControlSelection;
    private DevExpress.XtraEditors.SimpleButton btnClear;
    private DevExpress.XtraEditors.LookUpEdit lookUpEditLoc;
    private DevExpress.XtraEditors.LookUpEdit lookUpEditDevice;
    private DevExpress.XtraEditors.LabelControl labelControl3;
    private DevExpress.XtraEditors.LabelControl lblDeviceName;
    private DevExpress.XtraEditors.LabelControl lblLocUID;
    private DevExpress.XtraEditors.DateEdit dateEditFrom;
    private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
    public DevExpress.XtraEditors.SimpleButton btnReport;
    private DevExpress.XtraEditors.DateEdit dateEditTo;
    private DevExpress.XtraEditors.LabelControl labelControl1;
    private DevExpress.XtraEditors.GroupControl groupControlGrid;
    private DevExpress.XtraGrid.GridControl gvAlarmsMonitor;
    private DevExpress.XtraGrid.Views.Grid.GridView gridView;
    private DevExpress.XtraEditors.PanelControl panelHeader;
    public DevExpress.XtraEditors.LabelControl labelControl2;
    public DevExpress.XtraEditors.SimpleButton btnExcelReport;
    private DevExpress.Utils.ImageCollection imgSmallImageCollection;
  }
}
