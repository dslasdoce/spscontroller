namespace ISM.Modules
{
  partial class MonitorDevice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorDevice));
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.groupControlSelection = new DevExpress.XtraEditors.GroupControl();
            this.rdoDeviceStatus = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnExcelReport = new DevExpress.XtraEditors.SimpleButton();
            this.lookUpEditDeviceName = new DevExpress.XtraEditors.LookUpEdit();
            this.lblDeviceName = new DevExpress.XtraEditors.LabelControl();
            this.btnReport = new DevExpress.XtraEditors.SimpleButton();
            this.gvDeviceMonitor = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelHeader = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupControlGrid = new DevExpress.XtraEditors.GroupControl();
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).BeginInit();
            this.groupControlSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoDeviceStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditDeviceName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDeviceMonitor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).BeginInit();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlGrid)).BeginInit();
            this.groupControlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageIndex = 0;
            this.btnClear.ImageList = this.imgSmallImageCollection;
            this.btnClear.Location = new System.Drawing.Point(423, 33);
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
            // 
            // groupControlSelection
            // 
            this.groupControlSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlSelection.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlSelection.Appearance.Options.UseFont = true;
            this.groupControlSelection.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlSelection.AppearanceCaption.Options.UseFont = true;
            this.groupControlSelection.Controls.Add(this.rdoDeviceStatus);
            this.groupControlSelection.Controls.Add(this.labelControl4);
            this.groupControlSelection.Controls.Add(this.btnExcelReport);
            this.groupControlSelection.Controls.Add(this.btnClear);
            this.groupControlSelection.Controls.Add(this.lookUpEditDeviceName);
            this.groupControlSelection.Controls.Add(this.lblDeviceName);
            this.groupControlSelection.Controls.Add(this.btnReport);
            this.groupControlSelection.Location = new System.Drawing.Point(0, 363);
            this.groupControlSelection.Name = "groupControlSelection";
            this.groupControlSelection.Size = new System.Drawing.Size(787, 117);
            this.groupControlSelection.TabIndex = 0;
            this.groupControlSelection.Text = "Selection Criteria";
            // 
            // rdoDeviceStatus
            // 
            this.rdoDeviceStatus.Location = new System.Drawing.Point(116, 76);
            this.rdoDeviceStatus.Name = "rdoDeviceStatus";
            this.rdoDeviceStatus.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.rdoDeviceStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoDeviceStatus.Properties.Appearance.Options.UseBackColor = true;
            this.rdoDeviceStatus.Properties.Appearance.Options.UseFont = true;
            this.rdoDeviceStatus.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rdoDeviceStatus.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "On"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "Off"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(2)), "Both")});
            this.rdoDeviceStatus.Size = new System.Drawing.Size(162, 21);
            this.rdoDeviceStatus.TabIndex = 1;
            this.rdoDeviceStatus.SelectedIndexChanged += new System.EventHandler(this.rdoDeviceStatus_SelectedIndexChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(5, 81);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(104, 16);
            this.labelControl4.TabIndex = 34;
            this.labelControl4.Text = "Device Status :";
            // 
            // btnExcelReport
            // 
            this.btnExcelReport.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcelReport.Appearance.Options.UseFont = true;
            this.btnExcelReport.ImageIndex = 4;
            this.btnExcelReport.ImageList = this.imgSmallImageCollection;
            this.btnExcelReport.Location = new System.Drawing.Point(299, 75);
            this.btnExcelReport.Name = "btnExcelReport";
            this.btnExcelReport.Size = new System.Drawing.Size(108, 35);
            this.btnExcelReport.TabIndex = 5;
            this.btnExcelReport.Text = "Export Excel";
            this.btnExcelReport.Click += new System.EventHandler(this.btnExcelReport_Click);
            // 
            // lookUpEditDeviceName
            // 
            this.lookUpEditDeviceName.EnterMoveNextControl = true;
            this.lookUpEditDeviceName.Location = new System.Drawing.Point(114, 33);
            this.lookUpEditDeviceName.Name = "lookUpEditDeviceName";
            this.lookUpEditDeviceName.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditDeviceName.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditDeviceName.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditDeviceName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditDeviceName.Properties.MaxLength = 50;
            this.lookUpEditDeviceName.Properties.NullText = "";
            this.lookUpEditDeviceName.Properties.PopupSizeable = false;
            this.lookUpEditDeviceName.Properties.PopupWidth = 200;
            this.lookUpEditDeviceName.Size = new System.Drawing.Size(162, 22);
            this.lookUpEditDeviceName.TabIndex = 0;
            this.lookUpEditDeviceName.EditValueChanged += new System.EventHandler(this.lookUpEditPortalName_EditValueChanged);
            // 
            // lblDeviceName
            // 
            this.lblDeviceName.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeviceName.Location = new System.Drawing.Point(5, 36);
            this.lblDeviceName.Name = "lblDeviceName";
            this.lblDeviceName.Size = new System.Drawing.Size(102, 16);
            this.lblDeviceName.TabIndex = 1;
            this.lblDeviceName.Text = "Device Name :";
            // 
            // btnReport
            // 
            this.btnReport.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Appearance.Options.UseFont = true;
            this.btnReport.ImageIndex = 2;
            this.btnReport.ImageList = this.imgSmallImageCollection;
            this.btnReport.Location = new System.Drawing.Point(299, 33);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(108, 35);
            this.btnReport.TabIndex = 4;
            this.btnReport.Text = "Get Report";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // gvDeviceMonitor
            // 
            this.gvDeviceMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvDeviceMonitor.Location = new System.Drawing.Point(3, 3);
            this.gvDeviceMonitor.MainView = this.gridView;
            this.gvDeviceMonitor.Name = "gvDeviceMonitor";
            this.gvDeviceMonitor.Size = new System.Drawing.Size(783, 472);
            this.gvDeviceMonitor.TabIndex = 0;
            this.gvDeviceMonitor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gridView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.gridView.GridControl = this.gvDeviceMonitor;
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
            this.panelHeader.Location = new System.Drawing.Point(3, 3);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(781, 40);
            this.panelHeader.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(21, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(256, 23);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "ISM 7000 - Device Monitor";
            // 
            // groupControlGrid
            // 
            this.groupControlGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlGrid.Controls.Add(this.gvDeviceMonitor);
            this.groupControlGrid.Location = new System.Drawing.Point(0, 47);
            this.groupControlGrid.Name = "groupControlGrid";
            this.groupControlGrid.ShowCaption = false;
            this.groupControlGrid.Size = new System.Drawing.Size(789, 478);
            this.groupControlGrid.TabIndex = 2;
            this.groupControlGrid.Text = "Trace Data";
            // 
            // groupControl
            // 
            this.groupControl.Controls.Add(this.groupControl1);
            this.groupControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl.Location = new System.Drawing.Point(0, 0);
            this.groupControl.Name = "groupControl";
            this.groupControl.ShowCaption = false;
            this.groupControl.Size = new System.Drawing.Size(793, 484);
            this.groupControl.TabIndex = 6;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.groupControlSelection);
            this.groupControl1.Controls.Add(this.groupControlGrid);
            this.groupControl1.Controls.Add(this.panelHeader);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(3, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(787, 478);
            this.groupControl1.TabIndex = 5;
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // MonitorDevice
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl);
            this.LookAndFeel.SkinName = "Blue";
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MonitorDevice";
            this.Size = new System.Drawing.Size(793, 484);
            this.Load += new System.EventHandler(this.MonitorDevice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).EndInit();
            this.groupControlSelection.ResumeLayout(false);
            this.groupControlSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoDeviceStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditDeviceName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDeviceMonitor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlGrid)).EndInit();
            this.groupControlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private DevExpress.XtraEditors.SimpleButton btnClear;
    private DevExpress.Utils.ImageCollection imgSmallImageCollection;
    private DevExpress.XtraEditors.GroupControl groupControlSelection;
    public DevExpress.XtraEditors.SimpleButton btnExcelReport;
    private DevExpress.XtraEditors.LookUpEdit lookUpEditDeviceName;
    private DevExpress.XtraEditors.LabelControl lblDeviceName;
    private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
    private DevExpress.XtraEditors.GroupControl groupControl;
    private DevExpress.XtraEditors.GroupControl groupControl1;
    private DevExpress.XtraEditors.GroupControl groupControlGrid;
    private DevExpress.XtraGrid.GridControl gvDeviceMonitor;
    private DevExpress.XtraGrid.Views.Grid.GridView gridView;
    private DevExpress.XtraEditors.PanelControl panelHeader;
    public DevExpress.XtraEditors.LabelControl labelControl2;
    public DevExpress.XtraEditors.SimpleButton btnReport;
    private DevExpress.XtraEditors.LabelControl labelControl4;
    private DevExpress.XtraEditors.RadioGroup rdoDeviceStatus;

  }
}
