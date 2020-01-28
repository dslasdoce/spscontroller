namespace SHSHQ.Modules
{
    partial class StationLogs
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
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.pnlHeader = new DevExpress.XtraEditors.PanelControl();
            this.gvStationLogs = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SELECT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.DESCRIPTION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.luLoginId = new DevExpress.XtraEditors.LookUpEdit();
            this.lblLoginID = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cmdSearch = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvStationLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luLoginId.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(6, 6);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(120, 23);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Station Logs";
            // 
            // pnlHeader
            // 
            this.pnlHeader.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlHeader.Appearance.Options.UseBackColor = true;
            this.pnlHeader.AutoSize = true;
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Location = new System.Drawing.Point(0, 3);
            this.pnlHeader.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.pnlHeader.LookAndFeel.UseDefaultLookAndFeel = false;
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(2217, 48);
            this.pnlHeader.TabIndex = 26;
            // 
            // gvStationLogs
            // 
            this.gvStationLogs.Location = new System.Drawing.Point(6, 116);
            this.gvStationLogs.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.gvStationLogs.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gvStationLogs.MainView = this.gridView;
            this.gvStationLogs.Name = "gvStationLogs";
            this.gvStationLogs.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.gvStationLogs.Size = new System.Drawing.Size(799, 369);
            this.gvStationLogs.TabIndex = 27;
            this.gvStationLogs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
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
            this.gridView.GridControl = this.gvStationLogs;
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
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(399, 81);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker1.TabIndex = 28;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dateTimePicker2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(605, 81);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker2.TabIndex = 29;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(9, 79);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(196, 24);
            this.comboBox1.Sorted = true;
            this.comboBox1.TabIndex = 32;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // luLoginId
            // 
            this.luLoginId.Enabled = false;
            this.luLoginId.Location = new System.Drawing.Point(211, 81);
            this.luLoginId.Name = "luLoginId";
            this.luLoginId.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.luLoginId.Properties.Appearance.Options.UseFont = true;
            this.luLoginId.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.luLoginId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luLoginId.Properties.NullText = "";
            this.luLoginId.Properties.PopupSizeable = false;
            this.luLoginId.Properties.PopupWidth = 320;
            this.luLoginId.Size = new System.Drawing.Size(185, 22);
            this.luLoginId.TabIndex = 33;
            // 
            // lblLoginID
            // 
            this.lblLoginID.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginID.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            this.lblLoginID.Location = new System.Drawing.Point(399, 57);
            this.lblLoginID.Name = "lblLoginID";
            this.lblLoginID.Size = new System.Drawing.Size(136, 16);
            this.lblLoginID.TabIndex = 54;
            this.lblLoginID.Text = "Date and Time From :";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            this.labelControl1.Location = new System.Drawing.Point(605, 57);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(120, 16);
            this.labelControl1.TabIndex = 55;
            this.labelControl1.Text = "Date and Time To :";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            this.labelControl2.Location = new System.Drawing.Point(9, 57);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(118, 16);
            this.labelControl2.TabIndex = 56;
            this.labelControl2.Text = "Search Category :";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            this.labelControl3.Location = new System.Drawing.Point(211, 57);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(81, 16);
            this.labelControl3.TabIndex = 57;
            this.labelControl3.Text = "Search List :";
            // 
            // cmdSearch
            // 
            this.cmdSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(62)))), ((int)(((byte)(115)))));
            this.cmdSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdSearch.Appearance.Options.UseBackColor = true;
            this.cmdSearch.Appearance.Options.UseFont = true;
            this.cmdSearch.Appearance.Options.UseForeColor = true;
            this.cmdSearch.Location = new System.Drawing.Point(6, 491);
            this.cmdSearch.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.cmdSearch.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(95, 35);
            this.cmdSearch.TabIndex = 58;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // StationLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.cmdSearch);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lblLoginID);
            this.Controls.Add(this.luLoginId);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.gvStationLogs);
            this.Controls.Add(this.pnlHeader);
            this.Name = "StationLogs";
            this.Size = new System.Drawing.Size(2192, 621);
            this.Load += new System.EventHandler(this.StationLogs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvStationLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luLoginId.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.LabelControl lblHeader;
        private DevExpress.XtraEditors.PanelControl pnlHeader;
        private DevExpress.XtraGrid.GridControl gvStationLogs;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn CODE;
        private DevExpress.XtraGrid.Columns.GridColumn SELECT;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn DESCRIPTION;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.ComboBox comboBox1;
        private DevExpress.XtraEditors.LookUpEdit luLoginId;
        private DevExpress.XtraEditors.LabelControl lblLoginID;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton cmdSearch;
    }
}
