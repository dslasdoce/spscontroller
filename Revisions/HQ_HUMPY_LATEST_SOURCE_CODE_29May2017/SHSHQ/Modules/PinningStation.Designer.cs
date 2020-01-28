namespace SHSHQ.Modules
{
    partial class PinningStation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PinningStation));
            this.pnlHeader = new DevExpress.XtraEditors.PanelControl();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbMasterSlave = new DevExpress.XtraEditors.LookUpEdit();
            this.txtCraneGroup = new DevExpress.XtraEditors.TextEdit();
            this.txtIPAddress = new DevExpress.XtraEditors.TextEdit();
            this.txtStationID = new DevExpress.XtraEditors.TextEdit();
            this.cmdCancelClear = new DevExpress.XtraEditors.SimpleButton();
            this.cmdAdd = new DevExpress.XtraEditors.SimpleButton();
            this.lblMasterSlave = new System.Windows.Forms.Label();
            this.lblCraneGroup = new System.Windows.Forms.Label();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.lblStationID = new System.Windows.Forms.Label();
            this.cmdUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.panelFooter = new DevExpress.XtraEditors.PanelControl();
            this.txtStatusMsg = new DevExpress.XtraEditors.MemoEdit();
            this.picStatus = new DevExpress.XtraEditors.PictureEdit();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdDelete = new DevExpress.XtraEditors.SimpleButton();
            this.gvPinningStation = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SELECT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.DESCRIPTION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.txtWiFiIPAddress = new DevExpress.XtraEditors.TextEdit();
            this.lblWiFiIPAddress = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMasterSlave.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCraneGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIPAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStationID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).BeginInit();
            this.panelFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatusMsg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPinningStation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWiFiIPAddress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlHeader.Appearance.Options.UseBackColor = true;
            this.pnlHeader.AutoSize = true;
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.pnlHeader.LookAndFeel.UseDefaultLookAndFeel = false;
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1230, 41);
            this.pnlHeader.TabIndex = 25;
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(23, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(276, 23);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Pinning Station Management";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblWiFiIPAddress);
            this.groupBox1.Controls.Add(this.txtWiFiIPAddress);
            this.groupBox1.Controls.Add(this.cmbMasterSlave);
            this.groupBox1.Controls.Add(this.txtCraneGroup);
            this.groupBox1.Controls.Add(this.txtIPAddress);
            this.groupBox1.Controls.Add(this.txtStationID);
            this.groupBox1.Controls.Add(this.cmdCancelClear);
            this.groupBox1.Controls.Add(this.cmdAdd);
            this.groupBox1.Controls.Add(this.lblMasterSlave);
            this.groupBox1.Controls.Add(this.lblCraneGroup);
            this.groupBox1.Controls.Add(this.lblIPAddress);
            this.groupBox1.Controls.Add(this.lblStationID);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(12, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(457, 348);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pinning Station";
            // 
            // cmbMasterSlave
            // 
            this.dxErrorProvider.SetIconAlignment(this.cmbMasterSlave, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbMasterSlave.Location = new System.Drawing.Point(144, 154);
            this.cmbMasterSlave.Name = "cmbMasterSlave";
            this.cmbMasterSlave.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.cmbMasterSlave.Properties.Appearance.Options.UseFont = true;
            this.cmbMasterSlave.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.cmbMasterSlave.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.cmbMasterSlave.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMasterSlave.Properties.NullText = "Select if master or slave";
            this.cmbMasterSlave.Properties.PopupSizeable = false;
            this.cmbMasterSlave.Properties.PopupWidth = 320;
            this.cmbMasterSlave.Size = new System.Drawing.Size(289, 24);
            this.cmbMasterSlave.TabIndex = 4;
            // 
            // txtCraneGroup
            // 
            this.txtCraneGroup.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.txtCraneGroup, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCraneGroup.Location = new System.Drawing.Point(144, 124);
            this.txtCraneGroup.Name = "txtCraneGroup";
            this.txtCraneGroup.Properties.Appearance.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtCraneGroup.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtCraneGroup.Properties.Appearance.Options.UseBackColor = true;
            this.txtCraneGroup.Properties.Appearance.Options.UseFont = true;
            this.txtCraneGroup.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.txtCraneGroup.Properties.MaxLength = 50;
            this.txtCraneGroup.Size = new System.Drawing.Size(289, 24);
            this.txtCraneGroup.TabIndex = 3;
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.txtIPAddress, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtIPAddress.Location = new System.Drawing.Point(144, 64);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Properties.Appearance.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtIPAddress.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtIPAddress.Properties.Appearance.Options.UseBackColor = true;
            this.txtIPAddress.Properties.Appearance.Options.UseFont = true;
            this.txtIPAddress.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.txtIPAddress.Properties.MaxLength = 15;
            this.txtIPAddress.Size = new System.Drawing.Size(289, 24);
            this.txtIPAddress.TabIndex = 1;
            // 
            // txtStationID
            // 
            this.txtStationID.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.txtStationID, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtStationID.Location = new System.Drawing.Point(144, 31);
            this.txtStationID.Name = "txtStationID";
            this.txtStationID.Properties.Appearance.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtStationID.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtStationID.Properties.Appearance.Options.UseBackColor = true;
            this.txtStationID.Properties.Appearance.Options.UseFont = true;
            this.txtStationID.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.txtStationID.Properties.MaxLength = 50;
            this.txtStationID.Size = new System.Drawing.Size(289, 24);
            this.txtStationID.TabIndex = 0;
            // 
            // cmdCancelClear
            // 
            this.cmdCancelClear.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(62)))), ((int)(((byte)(115)))));
            this.cmdCancelClear.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancelClear.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdCancelClear.Appearance.Options.UseBackColor = true;
            this.cmdCancelClear.Appearance.Options.UseFont = true;
            this.cmdCancelClear.Appearance.Options.UseForeColor = true;
            this.cmdCancelClear.Location = new System.Drawing.Point(121, 222);
            this.cmdCancelClear.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.cmdCancelClear.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmdCancelClear.Name = "cmdCancelClear";
            this.cmdCancelClear.Size = new System.Drawing.Size(140, 37);
            this.cmdCancelClear.TabIndex = 6;
            this.cmdCancelClear.Text = "Clear";
            this.cmdCancelClear.Click += new System.EventHandler(this.cmdCancelClear_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(62)))), ((int)(((byte)(115)))));
            this.cmdAdd.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAdd.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdAdd.Appearance.Options.UseBackColor = true;
            this.cmdAdd.Appearance.Options.UseFont = true;
            this.cmdAdd.Appearance.Options.UseForeColor = true;
            this.cmdAdd.Location = new System.Drawing.Point(9, 222);
            this.cmdAdd.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.cmdAdd.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(106, 37);
            this.cmdAdd.TabIndex = 5;
            this.cmdAdd.Text = "Save";
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // lblMasterSlave
            // 
            this.lblMasterSlave.AutoSize = true;
            this.lblMasterSlave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMasterSlave.ForeColor = System.Drawing.Color.DarkGray;
            this.lblMasterSlave.Location = new System.Drawing.Point(20, 158);
            this.lblMasterSlave.Name = "lblMasterSlave";
            this.lblMasterSlave.Size = new System.Drawing.Size(106, 16);
            this.lblMasterSlave.TabIndex = 52;
            this.lblMasterSlave.Text = "Master/Salve :";
            // 
            // lblCraneGroup
            // 
            this.lblCraneGroup.AutoSize = true;
            this.lblCraneGroup.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCraneGroup.ForeColor = System.Drawing.Color.DarkGray;
            this.lblCraneGroup.Location = new System.Drawing.Point(20, 128);
            this.lblCraneGroup.Name = "lblCraneGroup";
            this.lblCraneGroup.Size = new System.Drawing.Size(97, 16);
            this.lblCraneGroup.TabIndex = 51;
            this.lblCraneGroup.Text = "Crane Group :";
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIPAddress.ForeColor = System.Drawing.Color.DarkGray;
            this.lblIPAddress.Location = new System.Drawing.Point(20, 68);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(110, 16);
            this.lblIPAddress.TabIndex = 50;
            this.lblIPAddress.Text = "Station LAN IP :";
            // 
            // lblStationID
            // 
            this.lblStationID.AutoSize = true;
            this.lblStationID.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStationID.ForeColor = System.Drawing.Color.DarkGray;
            this.lblStationID.Location = new System.Drawing.Point(20, 35);
            this.lblStationID.Name = "lblStationID";
            this.lblStationID.Size = new System.Drawing.Size(82, 16);
            this.lblStationID.TabIndex = 49;
            this.lblStationID.Text = "Station ID :";
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(62)))), ((int)(((byte)(115)))));
            this.cmdUpdate.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdUpdate.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdUpdate.Appearance.Options.UseBackColor = true;
            this.cmdUpdate.Appearance.Options.UseFont = true;
            this.cmdUpdate.Appearance.Options.UseForeColor = true;
            this.cmdUpdate.Location = new System.Drawing.Point(2, 245);
            this.cmdUpdate.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.cmdUpdate.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(166, 37);
            this.cmdUpdate.TabIndex = 8;
            this.cmdUpdate.Text = "Update Details";
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.txtStatusMsg);
            this.panelFooter.Controls.Add(this.picStatus);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 535);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1233, 50);
            this.panelFooter.TabIndex = 60;
            // 
            // txtStatusMsg
            // 
            this.txtStatusMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatusMsg.Location = new System.Drawing.Point(56, 3);
            this.txtStatusMsg.Name = "txtStatusMsg";
            this.txtStatusMsg.Properties.AllowFocused = false;
            this.txtStatusMsg.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtStatusMsg.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatusMsg.Properties.Appearance.Options.UseBackColor = true;
            this.txtStatusMsg.Properties.Appearance.Options.UseFont = true;
            this.txtStatusMsg.Properties.ReadOnly = true;
            this.txtStatusMsg.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtStatusMsg.Size = new System.Drawing.Size(1231, 45);
            this.txtStatusMsg.TabIndex = 0;
            this.txtStatusMsg.TabStop = false;
            // 
            // picStatus
            // 
            this.picStatus.EditValue = ((object)(resources.GetObject("picStatus.EditValue")));
            this.picStatus.Location = new System.Drawing.Point(5, 4);
            this.picStatus.Name = "picStatus";
            this.picStatus.Properties.AllowFocused = false;
            this.picStatus.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picStatus.Properties.ReadOnly = true;
            this.picStatus.Properties.ShowMenu = false;
            this.picStatus.Size = new System.Drawing.Size(45, 41);
            this.picStatus.TabIndex = 10;
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmdDelete);
            this.groupBox2.Controls.Add(this.gvPinningStation);
            this.groupBox2.Controls.Add(this.cmdUpdate);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(475, 47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(530, 348);
            this.groupBox2.TabIndex = 61;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pinning Station List";
            // 
            // cmdDelete
            // 
            this.cmdDelete.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(62)))), ((int)(((byte)(115)))));
            this.cmdDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDelete.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdDelete.Appearance.Options.UseBackColor = true;
            this.cmdDelete.Appearance.Options.UseFont = true;
            this.cmdDelete.Appearance.Options.UseForeColor = true;
            this.cmdDelete.Location = new System.Drawing.Point(174, 245);
            this.cmdDelete.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.cmdDelete.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(166, 37);
            this.cmdDelete.TabIndex = 9;
            this.cmdDelete.Text = "Delete Details";
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // gvPinningStation
            // 
            this.gvPinningStation.Location = new System.Drawing.Point(2, 32);
            this.gvPinningStation.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.gvPinningStation.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gvPinningStation.MainView = this.gridView;
            this.gvPinningStation.Name = "gvPinningStation";
            this.gvPinningStation.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.gvPinningStation.Size = new System.Drawing.Size(528, 207);
            this.gvPinningStation.TabIndex = 7;
            this.gvPinningStation.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gridView.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gridView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.CODE,
            this.SELECT,
            this.DESCRIPTION});
            this.gridView.GridControl = this.gvPinningStation;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gridView.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView.OptionsMenu.EnableFooterMenu = false;
            this.gridView.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView.OptionsSelection.UseIndicatorForSelection = false;
            this.gridView.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView.OptionsView.ShowGroupPanel = false;
            // 
            // CODE
            // 
            this.CODE.Caption = "CODE";
            this.CODE.FieldName = "CODE";
            this.CODE.Name = "CODE";
            this.CODE.OptionsColumn.AllowEdit = false;
            this.CODE.OptionsColumn.AllowMove = false;
            this.CODE.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            // 
            // SELECT
            // 
            this.SELECT.Caption = "SELECT";
            this.SELECT.ColumnEdit = this.repositoryItemCheckEdit1;
            this.SELECT.FieldName = "SELECT";
            this.SELECT.Name = "SELECT";
            this.SELECT.OptionsColumn.AllowMove = false;
            this.SELECT.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            this.SELECT.Visible = true;
            this.SELECT.VisibleIndex = 0;
            this.SELECT.Width = 46;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // DESCRIPTION
            // 
            this.DESCRIPTION.Caption = "DESCRIPTION";
            this.DESCRIPTION.FieldName = "DESCRIPTION";
            this.DESCRIPTION.Name = "DESCRIPTION";
            this.DESCRIPTION.OptionsColumn.AllowEdit = false;
            this.DESCRIPTION.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.DESCRIPTION.OptionsColumn.AllowMove = false;
            this.DESCRIPTION.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.DESCRIPTION.OptionsColumn.ReadOnly = true;
            this.DESCRIPTION.Visible = true;
            this.DESCRIPTION.VisibleIndex = 1;
            this.DESCRIPTION.Width = 455;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // txtWiFiIPAddress
            // 
            this.txtWiFiIPAddress.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.txtWiFiIPAddress, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtWiFiIPAddress.Location = new System.Drawing.Point(144, 94);
            this.txtWiFiIPAddress.Name = "txtWiFiIPAddress";
            this.txtWiFiIPAddress.Properties.Appearance.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtWiFiIPAddress.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtWiFiIPAddress.Properties.Appearance.Options.UseBackColor = true;
            this.txtWiFiIPAddress.Properties.Appearance.Options.UseFont = true;
            this.txtWiFiIPAddress.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.txtWiFiIPAddress.Properties.MaxLength = 15;
            this.txtWiFiIPAddress.Size = new System.Drawing.Size(289, 24);
            this.txtWiFiIPAddress.TabIndex = 2;
            // 
            // lblWiFiIPAddress
            // 
            this.lblWiFiIPAddress.AutoSize = true;
            this.lblWiFiIPAddress.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWiFiIPAddress.ForeColor = System.Drawing.Color.DarkGray;
            this.lblWiFiIPAddress.Location = new System.Drawing.Point(20, 98);
            this.lblWiFiIPAddress.Name = "lblWiFiIPAddress";
            this.lblWiFiIPAddress.Size = new System.Drawing.Size(116, 16);
            this.lblWiFiIPAddress.TabIndex = 54;
            this.lblWiFiIPAddress.Text = "Station Wi-Fi IP :";
            // 
            // PinningStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlHeader);
            this.Name = "PinningStation";
            this.Size = new System.Drawing.Size(1233, 585);
            this.Load += new System.EventHandler(this.PinningStation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMasterSlave.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCraneGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIPAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStationID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).EndInit();
            this.panelFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtStatusMsg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvPinningStation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWiFiIPAddress.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlHeader;
        public DevExpress.XtraEditors.LabelControl lblHeader;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMasterSlave;
        private System.Windows.Forms.Label lblCraneGroup;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.Label lblStationID;
        private DevExpress.XtraEditors.SimpleButton cmdCancelClear;
        private DevExpress.XtraEditors.SimpleButton cmdUpdate;
        private DevExpress.XtraEditors.SimpleButton cmdAdd;
        private DevExpress.XtraEditors.PanelControl panelFooter;
        private DevExpress.XtraEditors.PictureEdit picStatus;
        public DevExpress.XtraEditors.MemoEdit txtStatusMsg;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.XtraEditors.TextEdit txtStationID;
        private DevExpress.XtraEditors.TextEdit txtCraneGroup;
        private DevExpress.XtraEditors.TextEdit txtIPAddress;
        private DevExpress.XtraEditors.LookUpEdit cmbMasterSlave;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.GridControl gvPinningStation;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn CODE;
        private DevExpress.XtraGrid.Columns.GridColumn SELECT;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn DESCRIPTION;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.SimpleButton cmdDelete;
        private System.Windows.Forms.Label lblWiFiIPAddress;
        private DevExpress.XtraEditors.TextEdit txtWiFiIPAddress;
    }
}
