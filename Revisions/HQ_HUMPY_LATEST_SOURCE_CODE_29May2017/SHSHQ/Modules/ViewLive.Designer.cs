namespace SHSHQ.Modules
{
    partial class ViewLive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewLive));
            this.pnlHeader = new DevExpress.XtraEditors.PanelControl();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.gvViewLive = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SELECT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.DESCRIPTION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.panelFooter = new DevExpress.XtraEditors.PanelControl();
            this.txtStatusMsg = new DevExpress.XtraEditors.MemoEdit();
            this.picStatus = new DevExpress.XtraEditors.PictureEdit();
            this.cmdReferesh = new DevExpress.XtraEditors.SimpleButton();
            this.cmdCheckIn = new DevExpress.XtraEditors.SimpleButton();
            this.cmdCheckOut = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvViewLive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).BeginInit();
            this.panelFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatusMsg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus.Properties)).BeginInit();
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
            this.pnlHeader.TabIndex = 26;
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(23, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(92, 23);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "View Live";
            // 
            // gvViewLive
            // 
            this.gvViewLive.Location = new System.Drawing.Point(14, 47);
            this.gvViewLive.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.gvViewLive.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gvViewLive.MainView = this.gridView;
            this.gvViewLive.Name = "gvViewLive";
            this.gvViewLive.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.gvViewLive.Size = new System.Drawing.Size(684, 362);
            this.gvViewLive.TabIndex = 27;
            this.gvViewLive.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
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
            this.gridView.GridControl = this.gvViewLive;
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
            this.gridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView_RowCellStyle);
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
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.txtStatusMsg);
            this.panelFooter.Controls.Add(this.picStatus);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 415);
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
            // cmdReferesh
            // 
            this.cmdReferesh.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(62)))), ((int)(((byte)(115)))));
            this.cmdReferesh.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReferesh.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdReferesh.Appearance.Options.UseBackColor = true;
            this.cmdReferesh.Appearance.Options.UseFont = true;
            this.cmdReferesh.Appearance.Options.UseForeColor = true;
            this.cmdReferesh.Location = new System.Drawing.Point(704, 47);
            this.cmdReferesh.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.cmdReferesh.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmdReferesh.Name = "cmdReferesh";
            this.cmdReferesh.Size = new System.Drawing.Size(127, 37);
            this.cmdReferesh.TabIndex = 62;
            this.cmdReferesh.Text = "Refresh";
            this.cmdReferesh.Click += new System.EventHandler(this.cmdReferesh_Click);
            // 
            // cmdCheckIn
            // 
            this.cmdCheckIn.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(62)))), ((int)(((byte)(115)))));
            this.cmdCheckIn.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCheckIn.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdCheckIn.Appearance.Options.UseBackColor = true;
            this.cmdCheckIn.Appearance.Options.UseFont = true;
            this.cmdCheckIn.Appearance.Options.UseForeColor = true;
            this.cmdCheckIn.Location = new System.Drawing.Point(704, 90);
            this.cmdCheckIn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.cmdCheckIn.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmdCheckIn.Name = "cmdCheckIn";
            this.cmdCheckIn.Size = new System.Drawing.Size(127, 37);
            this.cmdCheckIn.TabIndex = 63;
            this.cmdCheckIn.Text = "Check-In";
            this.cmdCheckIn.Click += new System.EventHandler(this.cmdCheckIn_Click);
            // 
            // cmdCheckOut
            // 
            this.cmdCheckOut.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(62)))), ((int)(((byte)(115)))));
            this.cmdCheckOut.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCheckOut.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdCheckOut.Appearance.Options.UseBackColor = true;
            this.cmdCheckOut.Appearance.Options.UseFont = true;
            this.cmdCheckOut.Appearance.Options.UseForeColor = true;
            this.cmdCheckOut.Location = new System.Drawing.Point(704, 133);
            this.cmdCheckOut.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.cmdCheckOut.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmdCheckOut.Name = "cmdCheckOut";
            this.cmdCheckOut.Size = new System.Drawing.Size(127, 37);
            this.cmdCheckOut.TabIndex = 64;
            this.cmdCheckOut.Text = "Check - Out";
            this.cmdCheckOut.Click += new System.EventHandler(this.cmdCheckOut_Click);
            // 
            // ViewLive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.cmdCheckOut);
            this.Controls.Add(this.cmdCheckIn);
            this.Controls.Add(this.cmdReferesh);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.gvViewLive);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ViewLive";
            this.Size = new System.Drawing.Size(1233, 465);
            this.Load += new System.EventHandler(this.ViewLive_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvViewLive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).EndInit();
            this.panelFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtStatusMsg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlHeader;
        public DevExpress.XtraEditors.LabelControl lblHeader;
        private DevExpress.XtraGrid.GridControl gvViewLive;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn CODE;
        private DevExpress.XtraGrid.Columns.GridColumn SELECT;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn DESCRIPTION;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.PanelControl panelFooter;
        public DevExpress.XtraEditors.MemoEdit txtStatusMsg;
        private DevExpress.XtraEditors.PictureEdit picStatus;
        private DevExpress.XtraEditors.SimpleButton cmdReferesh;
        private DevExpress.XtraEditors.SimpleButton cmdCheckIn;
        private DevExpress.XtraEditors.SimpleButton cmdCheckOut;
    }
}
