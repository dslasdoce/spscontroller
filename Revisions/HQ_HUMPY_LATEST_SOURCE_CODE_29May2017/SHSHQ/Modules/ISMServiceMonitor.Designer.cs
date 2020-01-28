namespace ISM.Modules
{
    partial class ISMServiceMonitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ISMServiceMonitor));
            this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.wtDoIt = new System.ComponentModel.BackgroundWorker();
            this.wtBackup = new System.ComponentModel.BackgroundWorker();
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblProgressLable = new DevExpress.XtraEditors.LabelControl();
            this.gcCreateTask = new DevExpress.XtraEditors.GroupControl();
            this.bntActiveBackup = new DevExpress.XtraEditors.SimpleButton();
            this.lblActiveServiceStatus = new DevExpress.XtraEditors.LabelControl();
            this.lblOperatorID = new DevExpress.XtraEditors.LabelControl();
            this.btnActiveStart = new DevExpress.XtraEditors.SimpleButton();
            this.btnActiveStop = new DevExpress.XtraEditors.SimpleButton();
            this.BackupprogressBar = new System.Windows.Forms.ProgressBar();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.gcStockData = new DevExpress.XtraEditors.GroupControl();
            this.bntPassiveBackup = new DevExpress.XtraEditors.SimpleButton();
            this.btnPassiveStart = new DevExpress.XtraEditors.SimpleButton();
            this.btnPassiveStop = new DevExpress.XtraEditors.SimpleButton();
            this.lblPassiveServiceStatus = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.panelFooter = new DevExpress.XtraEditors.PanelControl();
            this.picStatus = new DevExpress.XtraEditors.PictureEdit();
            this.txtStatusMsg = new DevExpress.XtraEditors.MemoEdit();
            this.panelHeader = new DevExpress.XtraEditors.PanelControl();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCreateTask)).BeginInit();
            this.gcCreateTask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcStockData)).BeginInit();
            this.gcStockData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).BeginInit();
            this.panelFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatusMsg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).BeginInit();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // imgSmallImageCollection
            // 
            this.imgSmallImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgSmallImageCollection.ImageStream")));
            this.imgSmallImageCollection.Images.SetKeyName(0, "add22.png");
            this.imgSmallImageCollection.Images.SetKeyName(1, "delete22.png");
            this.imgSmallImageCollection.Images.SetKeyName(2, "SerivceStop.png");
            this.imgSmallImageCollection.Images.SetKeyName(3, "ServiceStart.png");
            this.imgSmallImageCollection.Images.SetKeyName(4, "Logfile.png");
            // 
            // wtDoIt
            // 
            this.wtDoIt.WorkerReportsProgress = true;
            this.wtDoIt.WorkerSupportsCancellation = true;
            this.wtDoIt.DoWork += new System.ComponentModel.DoWorkEventHandler(this.wtDoIt_DoWork);
            this.wtDoIt.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.wtDoIt_ProgressChanged);
            this.wtDoIt.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.wtDoIt_RunWorkerCompleted);
            // 
            // wtBackup
            // 
            this.wtBackup.WorkerReportsProgress = true;
            this.wtBackup.WorkerSupportsCancellation = true;
            this.wtBackup.DoWork += new System.ComponentModel.DoWorkEventHandler(this.wtBackup_DoWork);
            this.wtBackup.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.wtBackup_ProgressChanged);
            this.wtBackup.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.wtBackup_RunWorkerCompleted);
            // 
            // groupControl
            // 
            this.groupControl.Controls.Add(this.groupControl1);
            this.groupControl.Controls.Add(this.panelFooter);
            this.groupControl.Controls.Add(this.panelHeader);
            this.groupControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl.Location = new System.Drawing.Point(0, 0);
            this.groupControl.Name = "groupControl";
            this.groupControl.ShowCaption = false;
            this.groupControl.Size = new System.Drawing.Size(0, 0);
            this.groupControl.TabIndex = 2;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lblProgressLable);
            this.groupControl1.Controls.Add(this.gcCreateTask);
            this.groupControl1.Controls.Add(this.BackupprogressBar);
            this.groupControl1.Controls.Add(this.progressBar);
            this.groupControl1.Controls.Add(this.gcStockData);
            this.groupControl1.Location = new System.Drawing.Point(0, 44);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(693, 244);
            this.groupControl1.TabIndex = 46;
            // 
            // lblProgressLable
            // 
            this.lblProgressLable.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressLable.Appearance.Options.UseFont = true;
            this.lblProgressLable.Location = new System.Drawing.Point(7, 220);
            this.lblProgressLable.Name = "lblProgressLable";
            this.lblProgressLable.Size = new System.Drawing.Size(117, 16);
            this.lblProgressLable.TabIndex = 55;
            this.lblProgressLable.Text = "Backup Progress :";
            // 
            // gcCreateTask
            // 
            this.gcCreateTask.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcCreateTask.Appearance.Options.UseFont = true;
            this.gcCreateTask.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcCreateTask.AppearanceCaption.Options.UseFont = true;
            this.gcCreateTask.Controls.Add(this.bntActiveBackup);
            this.gcCreateTask.Controls.Add(this.lblActiveServiceStatus);
            this.gcCreateTask.Controls.Add(this.lblOperatorID);
            this.gcCreateTask.Controls.Add(this.btnActiveStart);
            this.gcCreateTask.Controls.Add(this.btnActiveStop);
            this.gcCreateTask.Location = new System.Drawing.Point(7, 3);
            this.gcCreateTask.Name = "gcCreateTask";
            this.gcCreateTask.Size = new System.Drawing.Size(339, 186);
            this.gcCreateTask.TabIndex = 0;
            this.gcCreateTask.Text = "aRFID";
            // 
            // bntActiveBackup
            // 
            this.bntActiveBackup.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntActiveBackup.Appearance.Options.UseFont = true;
            this.bntActiveBackup.ImageIndex = 4;
            this.bntActiveBackup.ImageList = this.imgSmallImageCollection;
            this.bntActiveBackup.Location = new System.Drawing.Point(17, 140);
            this.bntActiveBackup.Name = "bntActiveBackup";
            this.bntActiveBackup.Size = new System.Drawing.Size(292, 35);
            this.bntActiveBackup.TabIndex = 28;
            this.bntActiveBackup.Text = "Backup aRFID Log File";
            this.bntActiveBackup.Click += new System.EventHandler(this.bntActiveBackup_Click);
            // 
            // lblActiveServiceStatus
            // 
            this.lblActiveServiceStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveServiceStatus.Appearance.Options.UseFont = true;
            this.lblActiveServiceStatus.Location = new System.Drawing.Point(134, 36);
            this.lblActiveServiceStatus.Name = "lblActiveServiceStatus";
            this.lblActiveServiceStatus.Size = new System.Drawing.Size(0, 16);
            this.lblActiveServiceStatus.TabIndex = 27;
            // 
            // lblOperatorID
            // 
            this.lblOperatorID.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperatorID.Appearance.Options.UseFont = true;
            this.lblOperatorID.Location = new System.Drawing.Point(17, 36);
            this.lblOperatorID.Name = "lblOperatorID";
            this.lblOperatorID.Size = new System.Drawing.Size(106, 16);
            this.lblOperatorID.TabIndex = 26;
            this.lblOperatorID.Text = "Current Status :";
            // 
            // btnActiveStart
            // 
            this.btnActiveStart.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActiveStart.Appearance.Options.UseFont = true;
            this.btnActiveStart.ImageIndex = 3;
            this.btnActiveStart.ImageList = this.imgSmallImageCollection;
            this.btnActiveStart.Location = new System.Drawing.Point(17, 77);
            this.btnActiveStart.Name = "btnActiveStart";
            this.btnActiveStart.Size = new System.Drawing.Size(117, 35);
            this.btnActiveStart.TabIndex = 0;
            this.btnActiveStart.Text = "Start";
            this.btnActiveStart.Click += new System.EventHandler(this.btnActiveStart_Click);
            // 
            // btnActiveStop
            // 
            this.btnActiveStop.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActiveStop.Appearance.Options.UseFont = true;
            this.btnActiveStop.ImageIndex = 2;
            this.btnActiveStop.ImageList = this.imgSmallImageCollection;
            this.btnActiveStop.Location = new System.Drawing.Point(179, 77);
            this.btnActiveStop.Name = "btnActiveStop";
            this.btnActiveStop.Size = new System.Drawing.Size(128, 35);
            this.btnActiveStop.TabIndex = 1;
            this.btnActiveStop.Text = "Stop";
            this.btnActiveStop.Click += new System.EventHandler(this.btnActiveStop_Click);
            // 
            // BackupprogressBar
            // 
            this.BackupprogressBar.Location = new System.Drawing.Point(141, 218);
            this.BackupprogressBar.Name = "BackupprogressBar";
            this.BackupprogressBar.Size = new System.Drawing.Size(547, 23);
            this.BackupprogressBar.Step = 1;
            this.BackupprogressBar.TabIndex = 54;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(7, 191);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(681, 23);
            this.progressBar.TabIndex = 45;
            // 
            // gcStockData
            // 
            this.gcStockData.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcStockData.Appearance.Options.UseFont = true;
            this.gcStockData.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcStockData.AppearanceCaption.Options.UseFont = true;
            this.gcStockData.Controls.Add(this.bntPassiveBackup);
            this.gcStockData.Controls.Add(this.btnPassiveStart);
            this.gcStockData.Controls.Add(this.btnPassiveStop);
            this.gcStockData.Controls.Add(this.lblPassiveServiceStatus);
            this.gcStockData.Controls.Add(this.labelControl3);
            this.gcStockData.Location = new System.Drawing.Point(349, 3);
            this.gcStockData.Name = "gcStockData";
            this.gcStockData.Size = new System.Drawing.Size(339, 186);
            this.gcStockData.TabIndex = 8;
            this.gcStockData.Text = "pRFID";
            // 
            // bntPassiveBackup
            // 
            this.bntPassiveBackup.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntPassiveBackup.Appearance.Options.UseFont = true;
            this.bntPassiveBackup.ImageIndex = 4;
            this.bntPassiveBackup.ImageList = this.imgSmallImageCollection;
            this.bntPassiveBackup.Location = new System.Drawing.Point(19, 140);
            this.bntPassiveBackup.Name = "bntPassiveBackup";
            this.bntPassiveBackup.Size = new System.Drawing.Size(292, 35);
            this.bntPassiveBackup.TabIndex = 32;
            this.bntPassiveBackup.Text = "Backup pRFID Log File";
            this.bntPassiveBackup.Click += new System.EventHandler(this.bntPassiveBackup_Click);
            // 
            // btnPassiveStart
            // 
            this.btnPassiveStart.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPassiveStart.Appearance.Options.UseFont = true;
            this.btnPassiveStart.ImageIndex = 3;
            this.btnPassiveStart.ImageList = this.imgSmallImageCollection;
            this.btnPassiveStart.Location = new System.Drawing.Point(19, 77);
            this.btnPassiveStart.Name = "btnPassiveStart";
            this.btnPassiveStart.Size = new System.Drawing.Size(117, 35);
            this.btnPassiveStart.TabIndex = 30;
            this.btnPassiveStart.Text = "Start";
            this.btnPassiveStart.Click += new System.EventHandler(this.btnPassiveStart_Click);
            // 
            // btnPassiveStop
            // 
            this.btnPassiveStop.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPassiveStop.Appearance.Options.UseFont = true;
            this.btnPassiveStop.ImageIndex = 2;
            this.btnPassiveStop.ImageList = this.imgSmallImageCollection;
            this.btnPassiveStop.Location = new System.Drawing.Point(183, 77);
            this.btnPassiveStop.Name = "btnPassiveStop";
            this.btnPassiveStop.Size = new System.Drawing.Size(128, 35);
            this.btnPassiveStop.TabIndex = 31;
            this.btnPassiveStop.Text = "Stop";
            this.btnPassiveStop.Click += new System.EventHandler(this.btnPassiveStop_Click);
            // 
            // lblPassiveServiceStatus
            // 
            this.lblPassiveServiceStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassiveServiceStatus.Appearance.Options.UseFont = true;
            this.lblPassiveServiceStatus.Location = new System.Drawing.Point(141, 36);
            this.lblPassiveServiceStatus.Name = "lblPassiveServiceStatus";
            this.lblPassiveServiceStatus.Size = new System.Drawing.Size(0, 16);
            this.lblPassiveServiceStatus.TabIndex = 29;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(19, 36);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(106, 16);
            this.labelControl3.TabIndex = 28;
            this.labelControl3.Text = "Current Status :";
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.picStatus);
            this.panelFooter.Controls.Add(this.txtStatusMsg);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, -50);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(0, 50);
            this.panelFooter.TabIndex = 9;
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
            this.txtStatusMsg.Size = new System.Drawing.Size(0, 45);
            this.txtStatusMsg.TabIndex = 0;
            this.txtStatusMsg.TabStop = false;
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(0, 40);
            this.panelHeader.TabIndex = 3;
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Appearance.Options.UseFont = true;
            this.lblHeader.Location = new System.Drawing.Point(21, 8);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(334, 23);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "ISM 9400 - Manage Portal Services";
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // ISMServiceMonitor
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl);
            this.LookAndFeel.SkinName = "Black";
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "ISMServiceMonitor";
            this.Size = new System.Drawing.Size(0, 0);
            this.Load += new System.EventHandler(this.ISMServiceMonitor_Load);
            this.Leave += new System.EventHandler(this.ISMServiceMonitor_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCreateTask)).EndInit();
            this.gcCreateTask.ResumeLayout(false);
            this.gcCreateTask.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcStockData)).EndInit();
            this.gcStockData.ResumeLayout(false);
            this.gcStockData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).EndInit();
            this.panelFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatusMsg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl;
        private DevExpress.XtraEditors.PanelControl panelFooter;
        private DevExpress.XtraEditors.PictureEdit picStatus;
        private DevExpress.XtraEditors.MemoEdit txtStatusMsg;
        private DevExpress.XtraEditors.GroupControl gcCreateTask;
        private DevExpress.XtraEditors.SimpleButton bntActiveBackup;
        private DevExpress.Utils.ImageCollection imgSmallImageCollection;
        private DevExpress.XtraEditors.LabelControl lblActiveServiceStatus;
        private DevExpress.XtraEditors.LabelControl lblOperatorID;
        private DevExpress.XtraEditors.SimpleButton btnActiveStart;
        private DevExpress.XtraEditors.SimpleButton btnActiveStop;
        private DevExpress.XtraEditors.GroupControl gcStockData;
        private DevExpress.XtraEditors.SimpleButton bntPassiveBackup;
        private DevExpress.XtraEditors.SimpleButton btnPassiveStart;
        private DevExpress.XtraEditors.SimpleButton btnPassiveStop;
        private DevExpress.XtraEditors.LabelControl lblPassiveServiceStatus;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.PanelControl panelHeader;
        public DevExpress.XtraEditors.LabelControl lblHeader;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private System.ComponentModel.BackgroundWorker wtDoIt;
        private System.Windows.Forms.ProgressBar progressBar;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl lblProgressLable;
        private System.Windows.Forms.ProgressBar BackupprogressBar;
        private System.ComponentModel.BackgroundWorker wtBackup;
    }
}
