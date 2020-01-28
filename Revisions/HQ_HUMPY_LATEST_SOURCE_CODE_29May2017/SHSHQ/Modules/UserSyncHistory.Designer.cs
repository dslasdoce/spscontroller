namespace SHSHQ.Modules
{
    partial class UserSyncHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserSyncHistory));
            this.pnlHeader = new DevExpress.XtraEditors.PanelControl();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.panelFooter = new DevExpress.XtraEditors.PanelControl();
            this.txtStatusMsg = new DevExpress.XtraEditors.MemoEdit();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gvSyncHistory = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SELECT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.DESCRIPTION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.cmdSearch = new DevExpress.XtraEditors.SimpleButton();
            this.dtFilter = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).BeginInit();
            this.panelFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatusMsg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSyncHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
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
            this.pnlHeader.TabIndex = 28;
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(6, 6);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(170, 23);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "User Sync History";
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.txtStatusMsg);
            this.panelFooter.Controls.Add(this.pictureEdit1);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 535);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1233, 50);
            this.panelFooter.TabIndex = 61;
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
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(5, 4);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ReadOnly = true;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Size = new System.Drawing.Size(45, 41);
            this.pictureEdit1.TabIndex = 10;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtFilter);
            this.groupBox2.Controls.Add(this.lblDate);
            this.groupBox2.Controls.Add(this.gvSyncHistory);
            this.groupBox2.Controls.Add(this.cmdSearch);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(6, 47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(681, 420);
            this.groupBox2.TabIndex = 62;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sync History";
            // 
            // gvSyncHistory
            // 
            this.gvSyncHistory.Location = new System.Drawing.Point(9, 80);
            this.gvSyncHistory.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.gvSyncHistory.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gvSyncHistory.MainView = this.gridView;
            this.gvSyncHistory.Name = "gvSyncHistory";
            this.gvSyncHistory.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.gvSyncHistory.Size = new System.Drawing.Size(667, 320);
            this.gvSyncHistory.TabIndex = 1;
            this.gvSyncHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
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
            this.gridView.GridControl = this.gvSyncHistory;
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
            // cmdSearch
            // 
            this.cmdSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(62)))), ((int)(((byte)(115)))));
            this.cmdSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdSearch.Appearance.Options.UseBackColor = true;
            this.cmdSearch.Appearance.Options.UseFont = true;
            this.cmdSearch.Appearance.Options.UseForeColor = true;
            this.cmdSearch.Location = new System.Drawing.Point(342, 37);
            this.cmdSearch.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.cmdSearch.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(84, 37);
            this.cmdSearch.TabIndex = 5;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // dtFilter
            // 
            this.dtFilter.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFilter.CustomFormat = "MMM-dd-yyyy dddd";
            this.dtFilter.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFilter.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFilter.Location = new System.Drawing.Point(86, 44);
            this.dtFilter.Name = "dtFilter";
            this.dtFilter.Size = new System.Drawing.Size(250, 23);
            this.dtFilter.TabIndex = 6;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.DarkGray;
            this.lblDate.Location = new System.Drawing.Point(6, 44);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(83, 16);
            this.lblDate.TabIndex = 50;
            this.lblDate.Text = "Sync Date :";
            // 
            // UserSyncHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.pnlHeader);
            this.Name = "UserSyncHistory";
            this.Size = new System.Drawing.Size(1233, 585);
            this.Load += new System.EventHandler(this.UserSyncHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).EndInit();
            this.panelFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtStatusMsg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSyncHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlHeader;
        public DevExpress.XtraEditors.LabelControl lblHeader;
        private DevExpress.XtraEditors.PanelControl panelFooter;
        public DevExpress.XtraEditors.MemoEdit txtStatusMsg;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl gvSyncHistory;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn CODE;
        private DevExpress.XtraGrid.Columns.GridColumn SELECT;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn DESCRIPTION;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.SimpleButton cmdSearch;
        private System.Windows.Forms.DateTimePicker dtFilter;
        private System.Windows.Forms.Label lblDate;
    }
}
