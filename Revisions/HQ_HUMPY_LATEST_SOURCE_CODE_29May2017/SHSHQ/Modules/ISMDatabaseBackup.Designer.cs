namespace ISM.Modules
{
    partial class ISMDatabaseBackup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ISMDatabaseBackup));
            this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.picStatus = new DevExpress.XtraEditors.PictureEdit();
            this.txtStatusMsg = new DevExpress.XtraEditors.MemoEdit();
            this.gcOperatorCreate = new DevExpress.XtraEditors.GroupControl();
            this.panelFooter = new DevExpress.XtraEditors.PanelControl();
            this.gcOperator = new DevExpress.XtraEditors.GroupControl();
            this.lblProgressLable = new DevExpress.XtraEditors.LabelControl();
            this.BackupprogressBar = new System.Windows.Forms.ProgressBar();
            this.Label4 = new DevExpress.XtraEditors.LabelControl();
            this.lblDbName = new DevExpress.XtraEditors.LabelControl();
            this.Label1 = new DevExpress.XtraEditors.LabelControl();
            this.lblDBServerName = new DevExpress.XtraEditors.LabelControl();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnStartDataTransfer = new DevExpress.XtraEditors.SimpleButton();
            this.pnlHeader = new DevExpress.XtraEditors.PanelControl();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.wtDoIt = new System.ComponentModel.BackgroundWorker();
            this.BackupDlg = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatusMsg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcOperatorCreate)).BeginInit();
            this.gcOperatorCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).BeginInit();
            this.panelFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOperator)).BeginInit();
            this.gcOperator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgSmallImageCollection
            // 
            this.imgSmallImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgSmallImageCollection.ImageStream")));
            this.imgSmallImageCollection.Images.SetKeyName(0, "add22.png");
            this.imgSmallImageCollection.Images.SetKeyName(1, "delete22.png");
            this.imgSmallImageCollection.Images.SetKeyName(2, "refresh22.png");
            this.imgSmallImageCollection.Images.SetKeyName(3, "saveas22.png");
            this.imgSmallImageCollection.Images.SetKeyName(4, "standby.png");
            this.imgSmallImageCollection.Images.SetKeyName(5, "exit.png");
            this.imgSmallImageCollection.Images.SetKeyName(6, "DBsave.png");
            // 
            // picStatus
            // 
            this.picStatus.EditValue = global::SHSHQ.Properties.Resources.Information_icon;
            this.picStatus.Location = new System.Drawing.Point(5, 4);
            this.picStatus.Name = "picStatus";
            this.picStatus.Properties.AllowFocused = false;
            this.picStatus.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picStatus.Properties.ReadOnly = true;
            this.picStatus.Properties.ShowMenu = false;
            this.picStatus.Size = new System.Drawing.Size(45, 41);
            this.picStatus.TabIndex = 10;
            // 
            // txtStatusMsg
            // 
            this.txtStatusMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatusMsg.Location = new System.Drawing.Point(52, 2);
            this.txtStatusMsg.Name = "txtStatusMsg";
            this.txtStatusMsg.Properties.AllowFocused = false;
            this.txtStatusMsg.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtStatusMsg.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatusMsg.Properties.Appearance.Options.UseBackColor = true;
            this.txtStatusMsg.Properties.Appearance.Options.UseFont = true;
            this.txtStatusMsg.Properties.ReadOnly = true;
            this.txtStatusMsg.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtStatusMsg.Size = new System.Drawing.Size(892, 45);
            this.txtStatusMsg.TabIndex = 0;
            this.txtStatusMsg.TabStop = false;
            // 
            // gcOperatorCreate
            // 
            this.gcOperatorCreate.Controls.Add(this.panelFooter);
            this.gcOperatorCreate.Controls.Add(this.gcOperator);
            this.gcOperatorCreate.Controls.Add(this.pnlHeader);
            this.gcOperatorCreate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcOperatorCreate.Location = new System.Drawing.Point(0, 0);
            this.gcOperatorCreate.Name = "gcOperatorCreate";
            this.gcOperatorCreate.ShowCaption = false;
            this.gcOperatorCreate.Size = new System.Drawing.Size(898, 484);
            this.gcOperatorCreate.TabIndex = 2;
            this.gcOperatorCreate.Text = "groupControl1";
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.picStatus);
            this.panelFooter.Controls.Add(this.txtStatusMsg);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(3, 431);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(892, 50);
            this.panelFooter.TabIndex = 59;
            // 
            // gcOperator
            // 
            this.gcOperator.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcOperator.Appearance.Options.UseFont = true;
            this.gcOperator.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcOperator.AppearanceCaption.Options.UseFont = true;
            this.gcOperator.Controls.Add(this.lblProgressLable);
            this.gcOperator.Controls.Add(this.BackupprogressBar);
            this.gcOperator.Controls.Add(this.Label4);
            this.gcOperator.Controls.Add(this.lblDbName);
            this.gcOperator.Controls.Add(this.Label1);
            this.gcOperator.Controls.Add(this.lblDBServerName);
            this.gcOperator.Controls.Add(this.progressBar);
            this.gcOperator.Controls.Add(this.btnStartDataTransfer);
            this.gcOperator.Location = new System.Drawing.Point(2, 45);
            this.gcOperator.Name = "gcOperator";
            this.gcOperator.Size = new System.Drawing.Size(423, 225);
            this.gcOperator.TabIndex = 0;
            this.gcOperator.Text = "Backup Status";
            // 
            // lblProgressLable
            // 
            this.lblProgressLable.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressLable.Location = new System.Drawing.Point(5, 109);
            this.lblProgressLable.Name = "lblProgressLable";
            this.lblProgressLable.Size = new System.Drawing.Size(117, 16);
            this.lblProgressLable.TabIndex = 53;
            this.lblProgressLable.Text = "Backup Progress :";
            // 
            // BackupprogressBar
            // 
            this.BackupprogressBar.Location = new System.Drawing.Point(139, 109);
            this.BackupprogressBar.Name = "BackupprogressBar";
            this.BackupprogressBar.Size = new System.Drawing.Size(279, 23);
            this.BackupprogressBar.Step = 1;
            this.BackupprogressBar.TabIndex = 52;
            // 
            // Label4
            // 
            this.Label4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(5, 73);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(71, 16);
            this.Label4.TabIndex = 50;
            this.Label4.Text = "Database :";
            // 
            // lblDbName
            // 
            this.lblDbName.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDbName.Location = new System.Drawing.Point(163, 72);
            this.lblDbName.Name = "lblDbName";
            this.lblDbName.Size = new System.Drawing.Size(0, 16);
            this.lblDbName.TabIndex = 49;
            // 
            // Label1
            // 
            this.Label1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(5, 41);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(119, 16);
            this.Label1.TabIndex = 48;
            this.Label1.Text = "Database Server :";
            // 
            // lblDBServerName
            // 
            this.lblDBServerName.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBServerName.Location = new System.Drawing.Point(163, 40);
            this.lblDBServerName.Name = "lblDBServerName";
            this.lblDBServerName.Size = new System.Drawing.Size(0, 16);
            this.lblDBServerName.TabIndex = 47;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(9, 144);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(409, 23);
            this.progressBar.TabIndex = 44;
            // 
            // btnStartDataTransfer
            // 
            this.btnStartDataTransfer.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartDataTransfer.Appearance.Options.UseFont = true;
            this.btnStartDataTransfer.ImageIndex = 6;
            this.btnStartDataTransfer.ImageList = this.imgSmallImageCollection;
            this.btnStartDataTransfer.Location = new System.Drawing.Point(107, 176);
            this.btnStartDataTransfer.Name = "btnStartDataTransfer";
            this.btnStartDataTransfer.Size = new System.Drawing.Size(203, 35);
            this.btnStartDataTransfer.TabIndex = 1;
            this.btnStartDataTransfer.Text = "Start Database Backup...";
            this.btnStartDataTransfer.Click += new System.EventHandler(this.btnStartDataTransfer_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(3, 3);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(892, 40);
            this.pnlHeader.TabIndex = 24;
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(21, 8);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(319, 23);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "ISM 9300 - ISM Database Backup";
            // 
            // wtDoIt
            // 
            this.wtDoIt.WorkerReportsProgress = true;
            this.wtDoIt.WorkerSupportsCancellation = true;
            this.wtDoIt.DoWork += new System.ComponentModel.DoWorkEventHandler(this.wtDoIt_DoWork);
            this.wtDoIt.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.wtDoIt_ProgressChanged);
            this.wtDoIt.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.wtDoIt_RunWorkerCompleted);
            // 
            // BackupDlg
            // 
            this.BackupDlg.Filter = "Backup File|*.bak";
            // 
            // ISMDatabaseBackup
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcOperatorCreate);
            this.LookAndFeel.SkinName = "Black";
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "ISMDatabaseBackup";
            this.Size = new System.Drawing.Size(898, 484);
            this.Load += new System.EventHandler(this.ISMDatabaseBackup_Load);
            this.Leave += new System.EventHandler(this.ISMDatabaseBackup_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatusMsg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcOperatorCreate)).EndInit();
            this.gcOperatorCreate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).EndInit();
            this.panelFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcOperator)).EndInit();
            this.gcOperator.ResumeLayout(false);
            this.gcOperator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ImageCollection imgSmallImageCollection;
        private DevExpress.XtraEditors.PictureEdit picStatus;
        public DevExpress.XtraEditors.MemoEdit txtStatusMsg;
        private DevExpress.XtraEditors.GroupControl gcOperatorCreate;
        private DevExpress.XtraEditors.PanelControl panelFooter;
        private DevExpress.XtraEditors.GroupControl gcOperator;
        private System.Windows.Forms.ProgressBar progressBar;
        private DevExpress.XtraEditors.SimpleButton btnStartDataTransfer;
        private DevExpress.XtraEditors.PanelControl pnlHeader;
        public DevExpress.XtraEditors.LabelControl lblHeader;
        private System.ComponentModel.BackgroundWorker wtDoIt;
        private System.Windows.Forms.SaveFileDialog BackupDlg;
        private DevExpress.XtraEditors.LabelControl Label4;
        private DevExpress.XtraEditors.LabelControl lblDbName;
        private DevExpress.XtraEditors.LabelControl Label1;
        private DevExpress.XtraEditors.LabelControl lblDBServerName;
        private System.Windows.Forms.ProgressBar BackupprogressBar;
        private DevExpress.XtraEditors.LabelControl lblProgressLable;
    }
}
