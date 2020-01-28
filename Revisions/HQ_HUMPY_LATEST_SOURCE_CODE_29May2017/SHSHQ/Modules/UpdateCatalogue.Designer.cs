namespace ISM.Modules
{
    partial class UpdateCatalogue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateCatalogue));
            this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.wtDoIt = new System.ComponentModel.BackgroundWorker();
            this.gcOperatorCreate = new DevExpress.XtraEditors.GroupControl();
            this.panelFooter = new DevExpress.XtraEditors.PanelControl();
            this.picStatus = new DevExpress.XtraEditors.PictureEdit();
            this.txtStatusMsg = new DevExpress.XtraEditors.MemoEdit();
            this.gcOperator = new DevExpress.XtraEditors.GroupControl();
            this.lblIgnored = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lblUpdated = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.Label4 = new DevExpress.XtraEditors.LabelControl();
            this.lblCSVErrors = new DevExpress.XtraEditors.LabelControl();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.Label2 = new DevExpress.XtraEditors.LabelControl();
            this.Label1 = new DevExpress.XtraEditors.LabelControl();
            this.lblInserted = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalRecordsRead = new DevExpress.XtraEditors.LabelControl();
            this.btnStartDataTransfer = new DevExpress.XtraEditors.SimpleButton();
            this.pnlHeader = new DevExpress.XtraEditors.PanelControl();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcOperatorCreate)).BeginInit();
            this.gcOperatorCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).BeginInit();
            this.panelFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatusMsg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcOperator)).BeginInit();
            this.gcOperator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
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
            // 
            // wtDoIt
            // 
            this.wtDoIt.WorkerSupportsCancellation = true;
            this.wtDoIt.DoWork += new System.ComponentModel.DoWorkEventHandler(this.wtDoIt_DoWork);
            this.wtDoIt.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.wtDoIt_RunWorkerCompleted);
            // 
            // gcOperatorCreate
            // 
            this.gcOperatorCreate.Controls.Add(this.panelFooter);
            this.gcOperatorCreate.Controls.Add(this.gcOperator);
            this.gcOperatorCreate.Controls.Add(this.btnStartDataTransfer);
            this.gcOperatorCreate.Controls.Add(this.pnlHeader);
            this.gcOperatorCreate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcOperatorCreate.Location = new System.Drawing.Point(0, 0);
            this.gcOperatorCreate.Name = "gcOperatorCreate";
            this.gcOperatorCreate.ShowCaption = false;
            this.gcOperatorCreate.Size = new System.Drawing.Size(1183, 581);
            this.gcOperatorCreate.TabIndex = 1;
            this.gcOperatorCreate.Text = "groupControl1";
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.picStatus);
            this.panelFooter.Controls.Add(this.txtStatusMsg);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(2, 529);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1179, 50);
            this.panelFooter.TabIndex = 59;
            // 
            // picStatus
            // 
            this.picStatus.EditValue = global::SHSHQ.Properties.Resources.Information_icon;
            this.picStatus.Location = new System.Drawing.Point(5, 4);
            this.picStatus.Name = "picStatus";
            this.picStatus.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picStatus.Size = new System.Drawing.Size(45, 41);
            this.picStatus.TabIndex = 10;
            // 
            // txtStatusMsg
            // 
            this.txtStatusMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatusMsg.Location = new System.Drawing.Point(53, 2);
            this.txtStatusMsg.Name = "txtStatusMsg";
            this.txtStatusMsg.Properties.AllowFocused = false;
            this.txtStatusMsg.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtStatusMsg.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatusMsg.Properties.Appearance.Options.UseBackColor = true;
            this.txtStatusMsg.Properties.Appearance.Options.UseFont = true;
            this.txtStatusMsg.Properties.ReadOnly = true;
            this.txtStatusMsg.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtStatusMsg.Size = new System.Drawing.Size(1177, 45);
            this.txtStatusMsg.TabIndex = 0;
            this.txtStatusMsg.TabStop = false;
            // 
            // gcOperator
            // 
            this.gcOperator.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcOperator.Appearance.Options.UseFont = true;
            this.gcOperator.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcOperator.AppearanceCaption.Options.UseFont = true;
            this.gcOperator.Controls.Add(this.lblIgnored);
            this.gcOperator.Controls.Add(this.labelControl3);
            this.gcOperator.Controls.Add(this.lblUpdated);
            this.gcOperator.Controls.Add(this.labelControl2);
            this.gcOperator.Controls.Add(this.Label4);
            this.gcOperator.Controls.Add(this.lblCSVErrors);
            this.gcOperator.Controls.Add(this.progressBar);
            this.gcOperator.Controls.Add(this.Label2);
            this.gcOperator.Controls.Add(this.Label1);
            this.gcOperator.Controls.Add(this.lblInserted);
            this.gcOperator.Controls.Add(this.lblTotalRecordsRead);
            this.gcOperator.Location = new System.Drawing.Point(2, 45);
            this.gcOperator.Name = "gcOperator";
            this.gcOperator.Size = new System.Drawing.Size(440, 248);
            this.gcOperator.TabIndex = 0;
            this.gcOperator.Text = "Update Status";
            // 
            // lblIgnored
            // 
            this.lblIgnored.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIgnored.Appearance.Options.UseFont = true;
            this.lblIgnored.Location = new System.Drawing.Point(163, 180);
            this.lblIgnored.Name = "lblIgnored";
            this.lblIgnored.Size = new System.Drawing.Size(8, 16);
            this.lblIgnored.TabIndex = 50;
            this.lblIgnored.Text = "0";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(3, 179);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(117, 16);
            this.labelControl3.TabIndex = 49;
            this.labelControl3.Text = "Records Ignored :";
            // 
            // lblUpdated
            // 
            this.lblUpdated.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdated.Appearance.Options.UseFont = true;
            this.lblUpdated.Location = new System.Drawing.Point(162, 146);
            this.lblUpdated.Name = "lblUpdated";
            this.lblUpdated.Size = new System.Drawing.Size(8, 16);
            this.lblUpdated.TabIndex = 48;
            this.lblUpdated.Text = "0";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(3, 145);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(120, 16);
            this.labelControl2.TabIndex = 47;
            this.labelControl2.Text = "Records Updated :";
            // 
            // Label4
            // 
            this.Label4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Appearance.Options.UseFont = true;
            this.Label4.Location = new System.Drawing.Point(5, 74);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(128, 16);
            this.Label4.TabIndex = 46;
            this.Label4.Text = "CSV Record Errors :";
            // 
            // lblCSVErrors
            // 
            this.lblCSVErrors.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSVErrors.Appearance.Options.UseFont = true;
            this.lblCSVErrors.Location = new System.Drawing.Point(162, 75);
            this.lblCSVErrors.Name = "lblCSVErrors";
            this.lblCSVErrors.Size = new System.Drawing.Size(8, 16);
            this.lblCSVErrors.TabIndex = 45;
            this.lblCSVErrors.Text = "0";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(5, 213);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(406, 23);
            this.progressBar.TabIndex = 44;
            // 
            // Label2
            // 
            this.Label2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Appearance.Options.UseFont = true;
            this.Label2.Location = new System.Drawing.Point(5, 108);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(122, 16);
            this.Label2.TabIndex = 41;
            this.Label2.Text = "Records Inserted :";
            // 
            // Label1
            // 
            this.Label1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Appearance.Options.UseFont = true;
            this.Label1.Location = new System.Drawing.Point(5, 40);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(99, 16);
            this.Label1.TabIndex = 40;
            this.Label1.Text = "Records Read :";
            // 
            // lblInserted
            // 
            this.lblInserted.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInserted.Appearance.Options.UseFont = true;
            this.lblInserted.Location = new System.Drawing.Point(162, 109);
            this.lblInserted.Name = "lblInserted";
            this.lblInserted.Size = new System.Drawing.Size(8, 16);
            this.lblInserted.TabIndex = 39;
            this.lblInserted.Text = "0";
            // 
            // lblTotalRecordsRead
            // 
            this.lblTotalRecordsRead.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRecordsRead.Appearance.Options.UseFont = true;
            this.lblTotalRecordsRead.Location = new System.Drawing.Point(162, 41);
            this.lblTotalRecordsRead.Name = "lblTotalRecordsRead";
            this.lblTotalRecordsRead.Size = new System.Drawing.Size(8, 16);
            this.lblTotalRecordsRead.TabIndex = 38;
            this.lblTotalRecordsRead.Text = "0";
            // 
            // btnStartDataTransfer
            // 
            this.btnStartDataTransfer.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartDataTransfer.Appearance.Options.UseFont = true;
            this.btnStartDataTransfer.ImageIndex = 4;
            this.btnStartDataTransfer.ImageList = this.imgSmallImageCollection;
            this.btnStartDataTransfer.Location = new System.Drawing.Point(117, 299);
            this.btnStartDataTransfer.Name = "btnStartDataTransfer";
            this.btnStartDataTransfer.Size = new System.Drawing.Size(203, 35);
            this.btnStartDataTransfer.TabIndex = 1;
            this.btnStartDataTransfer.Text = "Start Data Transfer...";
            this.btnStartDataTransfer.Click += new System.EventHandler(this.btnStartDataTransfer_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(2, 2);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1179, 40);
            this.pnlHeader.TabIndex = 24;
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Appearance.Options.UseFont = true;
            this.lblHeader.Location = new System.Drawing.Point(22, 8);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(341, 23);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "ISM 9100 - Update Stock Catalogue";
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // UpdateCatalogue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcOperatorCreate);
            this.LookAndFeel.SkinName = "Black";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UpdateCatalogue";
            this.Size = new System.Drawing.Size(1183, 581);
            this.Load += new System.EventHandler(this.UpdateCatalogue_Load);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.UpdateCatalogue_ControlRemoved);
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcOperatorCreate)).EndInit();
            this.gcOperatorCreate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).EndInit();
            this.panelFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatusMsg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcOperator)).EndInit();
            this.gcOperator.ResumeLayout(false);
            this.gcOperator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.XtraEditors.GroupControl gcOperatorCreate;
        private DevExpress.XtraEditors.PanelControl panelFooter;
        private DevExpress.XtraEditors.PictureEdit picStatus;
        public DevExpress.XtraEditors.MemoEdit txtStatusMsg;
        private DevExpress.XtraEditors.GroupControl gcOperator;
        private DevExpress.XtraEditors.LabelControl Label2;
        private DevExpress.XtraEditors.LabelControl Label1;
        private DevExpress.XtraEditors.LabelControl lblInserted;
        private DevExpress.XtraEditors.LabelControl lblTotalRecordsRead;
        private DevExpress.Utils.ImageCollection imgSmallImageCollection;
        private DevExpress.XtraEditors.PanelControl pnlHeader;
        public DevExpress.XtraEditors.LabelControl lblHeader;
        private DevExpress.XtraEditors.SimpleButton btnStartDataTransfer;
        private System.ComponentModel.BackgroundWorker wtDoIt;
        private System.Windows.Forms.ProgressBar progressBar;
        private DevExpress.XtraEditors.LabelControl Label4;
        private DevExpress.XtraEditors.LabelControl lblCSVErrors;
        private DevExpress.XtraEditors.LabelControl lblUpdated;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblIgnored;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}
