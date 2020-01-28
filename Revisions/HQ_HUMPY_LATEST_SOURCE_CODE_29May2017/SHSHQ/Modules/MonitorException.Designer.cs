namespace ISM.Modules
{
  partial class MonitorException
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorException));
            this.btnExcelReport = new DevExpress.XtraEditors.SimpleButton();
            this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.groupControlSelection = new DevExpress.XtraEditors.GroupControl();
            this.rdoDataView = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.lookUpEditExcepType = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditOperatorID = new DevExpress.XtraEditors.LookUpEdit();
            this.lblOperatorID = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lblLocUID = new DevExpress.XtraEditors.LabelControl();
            this.dateEditFrom = new DevExpress.XtraEditors.DateEdit();
            this.btnReport = new DevExpress.XtraEditors.SimpleButton();
            this.dateEditTo = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControlGrid = new DevExpress.XtraEditors.GroupControl();
            this.gcExceptionMonitor = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.STARTDATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.EXCEPTION_DESCRIPTION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FK_ITEM_UID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FK_LOCATION_UID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FK_SEAL_UID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FK_TASK_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FK_STOCKCODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FK_USER_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CoA_UserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelHeader = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).BeginInit();
            this.groupControlSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoDataView.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditExcepType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditOperatorID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlGrid)).BeginInit();
            this.groupControlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcExceptionMonitor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).BeginInit();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExcelReport
            // 
            this.btnExcelReport.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(62)))), ((int)(((byte)(115)))));
            this.btnExcelReport.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcelReport.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnExcelReport.Appearance.Options.UseBackColor = true;
            this.btnExcelReport.Appearance.Options.UseFont = true;
            this.btnExcelReport.Appearance.Options.UseForeColor = true;
            this.btnExcelReport.ImageList = this.imgSmallImageCollection;
            this.btnExcelReport.Location = new System.Drawing.Point(523, 86);
            this.btnExcelReport.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnExcelReport.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnExcelReport.Name = "btnExcelReport";
            this.btnExcelReport.Size = new System.Drawing.Size(178, 35);
            this.btnExcelReport.TabIndex = 6;
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
            // groupControl
            // 
            this.groupControl.Controls.Add(this.groupControlSelection);
            this.groupControl.Controls.Add(this.groupControlGrid);
            this.groupControl.Controls.Add(this.panelHeader);
            this.groupControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl.Location = new System.Drawing.Point(0, 0);
            this.groupControl.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.groupControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl.Name = "groupControl";
            this.groupControl.ShowCaption = false;
            this.groupControl.Size = new System.Drawing.Size(0, 0);
            this.groupControl.TabIndex = 5;
            // 
            // groupControlSelection
            // 
            this.groupControlSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlSelection.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlSelection.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(62)))), ((int)(((byte)(115)))));
            this.groupControlSelection.Appearance.Options.UseFont = true;
            this.groupControlSelection.Appearance.Options.UseForeColor = true;
            this.groupControlSelection.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlSelection.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(62)))), ((int)(((byte)(115)))));
            this.groupControlSelection.AppearanceCaption.Options.UseFont = true;
            this.groupControlSelection.AppearanceCaption.Options.UseForeColor = true;
            this.groupControlSelection.Controls.Add(this.rdoDataView);
            this.groupControlSelection.Controls.Add(this.labelControl5);
            this.groupControlSelection.Controls.Add(this.btnExcelReport);
            this.groupControlSelection.Controls.Add(this.btnClear);
            this.groupControlSelection.Controls.Add(this.lookUpEditExcepType);
            this.groupControlSelection.Controls.Add(this.lookUpEditOperatorID);
            this.groupControlSelection.Controls.Add(this.lblOperatorID);
            this.groupControlSelection.Controls.Add(this.labelControl4);
            this.groupControlSelection.Controls.Add(this.lblLocUID);
            this.groupControlSelection.Controls.Add(this.dateEditFrom);
            this.groupControlSelection.Controls.Add(this.btnReport);
            this.groupControlSelection.Controls.Add(this.dateEditTo);
            this.groupControlSelection.Controls.Add(this.labelControl1);
            this.groupControlSelection.Location = new System.Drawing.Point(6, -194);
            this.groupControlSelection.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.groupControlSelection.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControlSelection.Name = "groupControlSelection";
            this.groupControlSelection.Size = new System.Drawing.Size(0, 188);
            this.groupControlSelection.TabIndex = 0;
            this.groupControlSelection.Text = "Selection Criteria";
            // 
            // rdoDataView
            // 
            this.rdoDataView.Location = new System.Drawing.Point(133, 35);
            this.rdoDataView.Name = "rdoDataView";
            this.rdoDataView.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoDataView.Properties.Appearance.Options.UseFont = true;
            this.rdoDataView.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "Task"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "Exception"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(3)), "Alarm"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(2)), "All")});
            this.rdoDataView.Size = new System.Drawing.Size(366, 26);
            this.rdoDataView.TabIndex = 35;
            this.rdoDataView.SelectedIndexChanged += new System.EventHandler(this.rdoDataView_SelectedIndexChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Appearance.Options.UseForeColor = true;
            this.labelControl5.Location = new System.Drawing.Point(13, 37);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(75, 16);
            this.labelControl5.TabIndex = 34;
            this.labelControl5.Text = "Data View :";
            // 
            // btnClear
            // 
            this.btnClear.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(62)))), ((int)(((byte)(115)))));
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnClear.Appearance.Options.UseBackColor = true;
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.Appearance.Options.UseForeColor = true;
            this.btnClear.ImageList = this.imgSmallImageCollection;
            this.btnClear.Location = new System.Drawing.Point(741, 35);
            this.btnClear.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnClear.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(186, 35);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lookUpEditExcepType
            // 
            this.lookUpEditExcepType.EnterMoveNextControl = true;
            this.lookUpEditExcepType.Location = new System.Drawing.Point(133, 69);
            this.lookUpEditExcepType.Name = "lookUpEditExcepType";
            this.lookUpEditExcepType.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditExcepType.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditExcepType.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditExcepType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditExcepType.Properties.MaxLength = 100;
            this.lookUpEditExcepType.Properties.NullText = "";
            this.lookUpEditExcepType.Properties.PopupSizeable = false;
            this.lookUpEditExcepType.Properties.PopupWidth = 200;
            this.lookUpEditExcepType.Size = new System.Drawing.Size(366, 22);
            this.lookUpEditExcepType.TabIndex = 0;
            this.lookUpEditExcepType.EditValueChanged += new System.EventHandler(this.lookUpEditExcepType_EditValueChanged);
            // 
            // lookUpEditOperatorID
            // 
            this.lookUpEditOperatorID.EnterMoveNextControl = true;
            this.lookUpEditOperatorID.Location = new System.Drawing.Point(133, 99);
            this.lookUpEditOperatorID.Name = "lookUpEditOperatorID";
            this.lookUpEditOperatorID.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditOperatorID.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditOperatorID.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditOperatorID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditOperatorID.Properties.MaxLength = 55;
            this.lookUpEditOperatorID.Properties.NullText = "";
            this.lookUpEditOperatorID.Properties.PopupSizeable = false;
            this.lookUpEditOperatorID.Properties.PopupWidth = 300;
            this.lookUpEditOperatorID.Size = new System.Drawing.Size(196, 22);
            this.lookUpEditOperatorID.TabIndex = 1;
            this.lookUpEditOperatorID.EditValueChanged += new System.EventHandler(this.lookUpEditOperatorID_EditValueChanged);
            // 
            // lblOperatorID
            // 
            this.lblOperatorID.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperatorID.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            this.lblOperatorID.Appearance.Options.UseFont = true;
            this.lblOperatorID.Appearance.Options.UseForeColor = true;
            this.lblOperatorID.Location = new System.Drawing.Point(12, 99);
            this.lblOperatorID.Name = "lblOperatorID";
            this.lblOperatorID.Size = new System.Drawing.Size(56, 16);
            this.lblOperatorID.TabIndex = 1;
            this.lblOperatorID.Text = "User ID :";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Location = new System.Drawing.Point(12, 69);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(91, 16);
            this.labelControl4.TabIndex = 30;
            this.labelControl4.Text = "Journal Type :";
            // 
            // lblLocUID
            // 
            this.lblLocUID.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocUID.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            this.lblLocUID.Appearance.Options.UseFont = true;
            this.lblLocUID.Appearance.Options.UseForeColor = true;
            this.lblLocUID.Location = new System.Drawing.Point(12, 129);
            this.lblLocUID.Name = "lblLocUID";
            this.lblLocUID.Size = new System.Drawing.Size(75, 16);
            this.lblLocUID.TabIndex = 3;
            this.lblLocUID.Text = "From Date :";
            // 
            // dateEditFrom
            // 
            this.dateEditFrom.EditValue = null;
            this.dxErrorProvider.SetIconAlignment(this.dateEditFrom, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dateEditFrom.Location = new System.Drawing.Point(133, 129);
            this.dateEditFrom.Name = "dateEditFrom";
            this.dateEditFrom.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEditFrom.Properties.Appearance.Options.UseFont = true;
            this.dateEditFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditFrom.Size = new System.Drawing.Size(196, 22);
            this.dateEditFrom.TabIndex = 2;
            // 
            // btnReport
            // 
            this.btnReport.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(62)))), ((int)(((byte)(115)))));
            this.btnReport.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnReport.Appearance.Options.UseBackColor = true;
            this.btnReport.Appearance.Options.UseFont = true;
            this.btnReport.Appearance.Options.UseForeColor = true;
            this.btnReport.ImageList = this.imgSmallImageCollection;
            this.btnReport.Location = new System.Drawing.Point(523, 35);
            this.btnReport.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnReport.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(178, 35);
            this.btnReport.TabIndex = 4;
            this.btnReport.Text = "Get Report";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // dateEditTo
            // 
            this.dateEditTo.EditValue = null;
            this.dxErrorProvider.SetIconAlignment(this.dateEditTo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dateEditTo.Location = new System.Drawing.Point(133, 159);
            this.dateEditTo.Name = "dateEditTo";
            this.dateEditTo.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEditTo.Properties.Appearance.Options.UseFont = true;
            this.dateEditTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditTo.Size = new System.Drawing.Size(196, 22);
            this.dateEditTo.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 159);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(59, 16);
            this.labelControl1.TabIndex = 25;
            this.labelControl1.Text = "To Date :";
            // 
            // groupControlGrid
            // 
            this.groupControlGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlGrid.Controls.Add(this.gcExceptionMonitor);
            this.groupControlGrid.Location = new System.Drawing.Point(6, 53);
            this.groupControlGrid.Name = "groupControlGrid";
            this.groupControlGrid.ShowCaption = false;
            this.groupControlGrid.Size = new System.Drawing.Size(0, 0);
            this.groupControlGrid.TabIndex = 2;
            this.groupControlGrid.Text = "Trace Data";
            // 
            // gcExceptionMonitor
            // 
            this.gcExceptionMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcExceptionMonitor.Location = new System.Drawing.Point(0, 0);
            this.gcExceptionMonitor.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.gcExceptionMonitor.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcExceptionMonitor.MainView = this.gridView;
            this.gcExceptionMonitor.Name = "gcExceptionMonitor";
            this.gcExceptionMonitor.Size = new System.Drawing.Size(0, 0);
            this.gcExceptionMonitor.TabIndex = 0;
            this.gcExceptionMonitor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView.Appearance.ViewCaption.Options.UseFont = true;
            this.gridView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gridView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.STARTDATE,
            this.EXCEPTION_DESCRIPTION,
            this.FK_ITEM_UID,
            this.FK_LOCATION_UID,
            this.FK_SEAL_UID,
            this.FK_TASK_ID,
            this.FK_STOCKCODE,
            this.FK_USER_ID,
            this.CoA_UserID});
            this.gridView.GridControl = this.gcExceptionMonitor;
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
            // STARTDATE
            // 
            this.STARTDATE.Caption = "Date";
            this.STARTDATE.DisplayFormat.FormatString = "dd-MMM-yy HH:mm:ss";
            this.STARTDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.STARTDATE.FieldName = "STARTDATE";
            this.STARTDATE.Name = "STARTDATE";
            this.STARTDATE.Visible = true;
            this.STARTDATE.VisibleIndex = 0;
            this.STARTDATE.Width = 130;
            // 
            // EXCEPTION_DESCRIPTION
            // 
            this.EXCEPTION_DESCRIPTION.Caption = "Description";
            this.EXCEPTION_DESCRIPTION.FieldName = "DESCRIPTION";
            this.EXCEPTION_DESCRIPTION.Name = "EXCEPTION_DESCRIPTION";
            this.EXCEPTION_DESCRIPTION.Visible = true;
            this.EXCEPTION_DESCRIPTION.VisibleIndex = 1;
            this.EXCEPTION_DESCRIPTION.Width = 370;
            // 
            // FK_ITEM_UID
            // 
            this.FK_ITEM_UID.Caption = "Item UID";
            this.FK_ITEM_UID.FieldName = "FK_ITEM_UID";
            this.FK_ITEM_UID.Name = "FK_ITEM_UID";
            this.FK_ITEM_UID.Visible = true;
            this.FK_ITEM_UID.VisibleIndex = 2;
            this.FK_ITEM_UID.Width = 120;
            // 
            // FK_LOCATION_UID
            // 
            this.FK_LOCATION_UID.Caption = "Location UID";
            this.FK_LOCATION_UID.FieldName = "FK_LOCATION_UID";
            this.FK_LOCATION_UID.Name = "FK_LOCATION_UID";
            this.FK_LOCATION_UID.Visible = true;
            this.FK_LOCATION_UID.VisibleIndex = 3;
            this.FK_LOCATION_UID.Width = 120;
            // 
            // FK_SEAL_UID
            // 
            this.FK_SEAL_UID.Caption = "Seal UID";
            this.FK_SEAL_UID.FieldName = "FK_SEAL_UID";
            this.FK_SEAL_UID.Name = "FK_SEAL_UID";
            this.FK_SEAL_UID.Visible = true;
            this.FK_SEAL_UID.VisibleIndex = 4;
            this.FK_SEAL_UID.Width = 120;
            // 
            // FK_TASK_ID
            // 
            this.FK_TASK_ID.Caption = "Task ID";
            this.FK_TASK_ID.FieldName = "FK_TASK_ID";
            this.FK_TASK_ID.Name = "FK_TASK_ID";
            this.FK_TASK_ID.Visible = true;
            this.FK_TASK_ID.VisibleIndex = 5;
            this.FK_TASK_ID.Width = 130;
            // 
            // FK_STOCKCODE
            // 
            this.FK_STOCKCODE.Caption = "Stock Code";
            this.FK_STOCKCODE.FieldName = "FK_STOCKCODE";
            this.FK_STOCKCODE.Name = "FK_STOCKCODE";
            this.FK_STOCKCODE.Visible = true;
            this.FK_STOCKCODE.VisibleIndex = 6;
            this.FK_STOCKCODE.Width = 100;
            // 
            // FK_USER_ID
            // 
            this.FK_USER_ID.Caption = "User ID";
            this.FK_USER_ID.FieldName = "LOGON_ID";
            this.FK_USER_ID.Name = "FK_USER_ID";
            this.FK_USER_ID.Visible = true;
            this.FK_USER_ID.VisibleIndex = 7;
            this.FK_USER_ID.Width = 140;
            // 
            // CoA_UserID
            // 
            this.CoA_UserID.Caption = "CoA Verifier User ID";
            this.CoA_UserID.FieldName = "CoA_UserID";
            this.CoA_UserID.Name = "CoA_UserID";
            this.CoA_UserID.Visible = true;
            this.CoA_UserID.VisibleIndex = 8;
            this.CoA_UserID.Width = 140;
            // 
            // panelHeader
            // 
            this.panelHeader.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelHeader.Appearance.Options.UseBackColor = true;
            this.panelHeader.Controls.Add(this.labelControl2);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(3, 3);
            this.panelHeader.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.panelHeader.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(0, 40);
            this.panelHeader.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(23, 9);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(152, 23);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Journal Monitor";
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // MonitorException
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl);
            this.LookAndFeel.SkinName = "Black";
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MonitorException";
            this.Size = new System.Drawing.Size(0, 0);
            this.Load += new System.EventHandler(this.MonitorException_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).EndInit();
            this.groupControlSelection.ResumeLayout(false);
            this.groupControlSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoDataView.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditExcepType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditOperatorID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlGrid)).EndInit();
            this.groupControlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcExceptionMonitor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    public DevExpress.XtraEditors.SimpleButton btnExcelReport;
    private DevExpress.Utils.ImageCollection imgSmallImageCollection;
    private DevExpress.XtraEditors.GroupControl groupControl;
    private DevExpress.XtraEditors.GroupControl groupControlSelection;
    private DevExpress.XtraEditors.SimpleButton btnClear;
    private DevExpress.XtraEditors.LookUpEdit lookUpEditExcepType;
    private DevExpress.XtraEditors.LookUpEdit lookUpEditOperatorID;
    private DevExpress.XtraEditors.LabelControl labelControl4;
    private DevExpress.XtraEditors.LabelControl lblOperatorID;
    private DevExpress.XtraEditors.LabelControl lblLocUID;
    private DevExpress.XtraEditors.DateEdit dateEditFrom;
    private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
    public DevExpress.XtraEditors.SimpleButton btnReport;
    private DevExpress.XtraEditors.DateEdit dateEditTo;
    private DevExpress.XtraEditors.LabelControl labelControl1;
    private DevExpress.XtraEditors.GroupControl groupControlGrid;
    private DevExpress.XtraGrid.GridControl gcExceptionMonitor;
    private DevExpress.XtraGrid.Views.Grid.GridView gridView;
    private DevExpress.XtraEditors.PanelControl panelHeader;
    public DevExpress.XtraEditors.LabelControl labelControl2;
    private DevExpress.XtraEditors.LabelControl labelControl5;
    private DevExpress.XtraEditors.RadioGroup rdoDataView;
    private DevExpress.XtraGrid.Columns.GridColumn STARTDATE;
    private DevExpress.XtraGrid.Columns.GridColumn EXCEPTION_DESCRIPTION;
    private DevExpress.XtraGrid.Columns.GridColumn FK_ITEM_UID;
    private DevExpress.XtraGrid.Columns.GridColumn FK_LOCATION_UID;
    private DevExpress.XtraGrid.Columns.GridColumn FK_SEAL_UID;
    private DevExpress.XtraGrid.Columns.GridColumn FK_TASK_ID;
    private DevExpress.XtraGrid.Columns.GridColumn FK_STOCKCODE;
    private DevExpress.XtraGrid.Columns.GridColumn FK_USER_ID;
    private DevExpress.XtraGrid.Columns.GridColumn CoA_UserID;

  }
}
