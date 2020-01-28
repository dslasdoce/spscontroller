namespace ISM.Modules
{
    partial class RptFrmStockSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RptFrmStockSearch));
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panelHeader = new DevExpress.XtraEditors.PanelControl();
            this.groupControlSelection = new DevExpress.XtraEditors.GroupControl();
            this.txtBin = new DevExpress.XtraEditors.TextEdit();
            this.lblBin = new DevExpress.XtraEditors.LabelControl();
            this.lblLocUID = new DevExpress.XtraEditors.LabelControl();
            this.dateEditFrom = new DevExpress.XtraEditors.DateEdit();
            this.dateEditTo = new DevExpress.XtraEditors.DateEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtGrid = new DevExpress.XtraEditors.TextEdit();
            this.lblGrid = new DevExpress.XtraEditors.LabelControl();
            this.txtShortName = new DevExpress.XtraEditors.TextEdit();
            this.lblShortName = new DevExpress.XtraEditors.LabelControl();
            this.btnSearcher = new DevExpress.XtraEditors.SimpleButton();
            this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.txtStockCode = new DevExpress.XtraEditors.TextEdit();
            this.txtSerialNo = new DevExpress.XtraEditors.TextEdit();
            this.lblSerialNo = new DevExpress.XtraEditors.LabelControl();
            this.txtBatchLotNo = new DevExpress.XtraEditors.TextEdit();
            this.lblBatchLotNo = new DevExpress.XtraEditors.LabelControl();
            this.lblStockCode = new DevExpress.XtraEditors.LabelControl();
            this.lblCurLocUID = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditLocationUID = new DevExpress.XtraEditors.LookUpEdit();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnReport = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).BeginInit();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).BeginInit();
            this.groupControlSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGrid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShortName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStockCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerialNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBatchLotNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditLocationUID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(26, 10);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(382, 29);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "ISM 8000 - Stock Search Report";
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.panelHeader);
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(1209, 594);
            this.groupControl1.TabIndex = 5;
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.labelControl2);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(2, 2);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1205, 49);
            this.panelHeader.TabIndex = 0;
            // 
            // groupControlSelection
            // 
            this.groupControlSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlSelection.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlSelection.Appearance.Options.UseFont = true;
            this.groupControlSelection.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlSelection.AppearanceCaption.Options.UseFont = true;
            this.groupControlSelection.Controls.Add(this.txtBin);
            this.groupControlSelection.Controls.Add(this.lblBin);
            this.groupControlSelection.Controls.Add(this.lblLocUID);
            this.groupControlSelection.Controls.Add(this.dateEditFrom);
            this.groupControlSelection.Controls.Add(this.dateEditTo);
            this.groupControlSelection.Controls.Add(this.labelControl4);
            this.groupControlSelection.Controls.Add(this.txtGrid);
            this.groupControlSelection.Controls.Add(this.lblGrid);
            this.groupControlSelection.Controls.Add(this.txtShortName);
            this.groupControlSelection.Controls.Add(this.lblShortName);
            this.groupControlSelection.Controls.Add(this.btnSearcher);
            this.groupControlSelection.Controls.Add(this.txtStockCode);
            this.groupControlSelection.Controls.Add(this.txtSerialNo);
            this.groupControlSelection.Controls.Add(this.lblSerialNo);
            this.groupControlSelection.Controls.Add(this.txtBatchLotNo);
            this.groupControlSelection.Controls.Add(this.lblBatchLotNo);
            this.groupControlSelection.Controls.Add(this.lblStockCode);
            this.groupControlSelection.Controls.Add(this.lblCurLocUID);
            this.groupControlSelection.Controls.Add(this.lookUpEditLocationUID);
            this.groupControlSelection.Controls.Add(this.btnClear);
            this.groupControlSelection.Controls.Add(this.btnReport);
            this.groupControlSelection.Location = new System.Drawing.Point(2, 55);
            this.groupControlSelection.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControlSelection.Name = "groupControlSelection";
            this.groupControlSelection.Size = new System.Drawing.Size(1207, 622);
            this.groupControlSelection.TabIndex = 0;
            this.groupControlSelection.Text = "Selection Criteria";
            // 
            // txtBin
            // 
            this.txtBin.EditValue = "";
            this.txtBin.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.txtBin, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtBin.Location = new System.Drawing.Point(169, 278);
            this.txtBin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBin.Name = "txtBin";
            this.txtBin.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBin.Properties.Appearance.Options.UseFont = true;
            this.txtBin.Properties.MaxLength = 9;
            this.txtBin.Size = new System.Drawing.Size(229, 26);
            this.txtBin.TabIndex = 7;
            // 
            // lblBin
            // 
            this.lblBin.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBin.Appearance.Options.UseFont = true;
            this.lblBin.Location = new System.Drawing.Point(13, 282);
            this.lblBin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblBin.Name = "lblBin";
            this.lblBin.Size = new System.Drawing.Size(37, 19);
            this.lblBin.TabIndex = 50;
            this.lblBin.Text = "Bin :";
            // 
            // lblLocUID
            // 
            this.lblLocUID.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocUID.Appearance.Options.UseFont = true;
            this.lblLocUID.Location = new System.Drawing.Point(13, 319);
            this.lblLocUID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblLocUID.Name = "lblLocUID";
            this.lblLocUID.Size = new System.Drawing.Size(137, 20);
            this.lblLocUID.TabIndex = 45;
            this.lblLocUID.Text = "Stocktake From :";
            // 
            // dateEditFrom
            // 
            this.dateEditFrom.EditValue = null;
            this.dxErrorProvider.SetIconAlignment(this.dateEditFrom, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dateEditFrom.Location = new System.Drawing.Point(169, 315);
            this.dateEditFrom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateEditFrom.Name = "dateEditFrom";
            this.dateEditFrom.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEditFrom.Properties.Appearance.Options.UseFont = true;
            this.dateEditFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditFrom.Size = new System.Drawing.Size(168, 26);
            this.dateEditFrom.TabIndex = 8;
            this.dateEditFrom.EditValueChanged += new System.EventHandler(this.dateEditFrom_EditValueChanged);
            // 
            // dateEditTo
            // 
            this.dateEditTo.EditValue = null;
            this.dxErrorProvider.SetIconAlignment(this.dateEditTo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dateEditTo.Location = new System.Drawing.Point(169, 352);
            this.dateEditTo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateEditTo.Name = "dateEditTo";
            this.dateEditTo.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEditTo.Properties.Appearance.Options.UseFont = true;
            this.dateEditTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditTo.Size = new System.Drawing.Size(168, 26);
            this.dateEditTo.TabIndex = 9;
            this.dateEditTo.EditValueChanged += new System.EventHandler(this.dateEditTo_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(13, 356);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(116, 20);
            this.labelControl4.TabIndex = 48;
            this.labelControl4.Text = "Stocktake To :";
            // 
            // txtGrid
            // 
            this.txtGrid.EditValue = "";
            this.txtGrid.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.txtGrid, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtGrid.Location = new System.Drawing.Point(169, 241);
            this.txtGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGrid.Name = "txtGrid";
            this.txtGrid.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrid.Properties.Appearance.Options.UseFont = true;
            this.txtGrid.Properties.MaxLength = 3;
            this.txtGrid.Size = new System.Drawing.Size(229, 26);
            this.txtGrid.TabIndex = 6;
            // 
            // lblGrid
            // 
            this.lblGrid.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrid.Appearance.Options.UseFont = true;
            this.lblGrid.Location = new System.Drawing.Point(13, 245);
            this.lblGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblGrid.Name = "lblGrid";
            this.lblGrid.Size = new System.Drawing.Size(45, 19);
            this.lblGrid.TabIndex = 44;
            this.lblGrid.Text = "Grid :";
            // 
            // txtShortName
            // 
            this.txtShortName.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.txtShortName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtShortName.Location = new System.Drawing.Point(169, 130);
            this.txtShortName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtShortName.Name = "txtShortName";
            this.txtShortName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShortName.Properties.Appearance.Options.UseFont = true;
            this.txtShortName.Properties.MaxLength = 50;
            this.txtShortName.Size = new System.Drawing.Size(229, 26);
            this.txtShortName.TabIndex = 3;
            // 
            // lblShortName
            // 
            this.lblShortName.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShortName.Appearance.Options.UseFont = true;
            this.lblShortName.Location = new System.Drawing.Point(13, 134);
            this.lblShortName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblShortName.Name = "lblShortName";
            this.lblShortName.Size = new System.Drawing.Size(107, 19);
            this.lblShortName.TabIndex = 41;
            this.lblShortName.Text = "Short Name :";
            // 
            // btnSearcher
            // 
            this.btnSearcher.ImageIndex = 5;
            this.btnSearcher.ImageList = this.imgSmallImageCollection;
            this.btnSearcher.Location = new System.Drawing.Point(402, 94);
            this.btnSearcher.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSearcher.Name = "btnSearcher";
            this.btnSearcher.Size = new System.Drawing.Size(31, 27);
            this.btnSearcher.TabIndex = 2;
            this.btnSearcher.Text = "simpleButton1";
            this.btnSearcher.Click += new System.EventHandler(this.btnSearcher_Click);
            // 
            // imgSmallImageCollection
            // 
            this.imgSmallImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgSmallImageCollection.ImageStream")));
            this.imgSmallImageCollection.Images.SetKeyName(0, "refresh22.png");
            this.imgSmallImageCollection.Images.SetKeyName(1, "saveas22.png");
            this.imgSmallImageCollection.Images.SetKeyName(2, "readcomments.gif");
            this.imgSmallImageCollection.Images.SetKeyName(3, "notifications_cctitle.gif");
            this.imgSmallImageCollection.Images.SetKeyName(4, "excel.gif");
            this.imgSmallImageCollection.Images.SetKeyName(5, "search.gif");
            // 
            // txtStockCode
            // 
            this.txtStockCode.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.txtStockCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtStockCode.Location = new System.Drawing.Point(169, 94);
            this.txtStockCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStockCode.Name = "txtStockCode";
            this.txtStockCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStockCode.Properties.Appearance.Options.UseFont = true;
            this.txtStockCode.Properties.MaxLength = 9;
            this.txtStockCode.Size = new System.Drawing.Size(229, 26);
            this.txtStockCode.TabIndex = 1;
            // 
            // txtSerialNo
            // 
            this.txtSerialNo.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.txtSerialNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtSerialNo.Location = new System.Drawing.Point(169, 167);
            this.txtSerialNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSerialNo.Name = "txtSerialNo";
            this.txtSerialNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerialNo.Properties.Appearance.Options.UseFont = true;
            this.txtSerialNo.Properties.MaxLength = 30;
            this.txtSerialNo.Size = new System.Drawing.Size(229, 26);
            this.txtSerialNo.TabIndex = 4;
            // 
            // lblSerialNo
            // 
            this.lblSerialNo.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialNo.Appearance.Options.UseFont = true;
            this.lblSerialNo.Location = new System.Drawing.Point(13, 171);
            this.lblSerialNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblSerialNo.Name = "lblSerialNo";
            this.lblSerialNo.Size = new System.Drawing.Size(139, 19);
            this.lblSerialNo.TabIndex = 40;
            this.lblSerialNo.Text = "Serial/Equip No :";
            // 
            // txtBatchLotNo
            // 
            this.txtBatchLotNo.EditValue = "";
            this.txtBatchLotNo.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.txtBatchLotNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtBatchLotNo.Location = new System.Drawing.Point(169, 204);
            this.txtBatchLotNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBatchLotNo.Name = "txtBatchLotNo";
            this.txtBatchLotNo.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchLotNo.Properties.Appearance.Options.UseFont = true;
            this.txtBatchLotNo.Properties.MaxLength = 10;
            this.txtBatchLotNo.Size = new System.Drawing.Size(229, 26);
            this.txtBatchLotNo.TabIndex = 5;
            // 
            // lblBatchLotNo
            // 
            this.lblBatchLotNo.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchLotNo.Appearance.Options.UseFont = true;
            this.lblBatchLotNo.Location = new System.Drawing.Point(13, 208);
            this.lblBatchLotNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblBatchLotNo.Name = "lblBatchLotNo";
            this.lblBatchLotNo.Size = new System.Drawing.Size(119, 19);
            this.lblBatchLotNo.TabIndex = 39;
            this.lblBatchLotNo.Text = "Batch/Lot No :";
            // 
            // lblStockCode
            // 
            this.lblStockCode.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockCode.Appearance.Options.UseFont = true;
            this.lblStockCode.Location = new System.Drawing.Point(13, 97);
            this.lblStockCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblStockCode.Name = "lblStockCode";
            this.lblStockCode.Size = new System.Drawing.Size(102, 19);
            this.lblStockCode.TabIndex = 38;
            this.lblStockCode.Text = "Stock Code :";
            // 
            // lblCurLocUID
            // 
            this.lblCurLocUID.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurLocUID.Appearance.Options.UseFont = true;
            this.lblCurLocUID.Location = new System.Drawing.Point(13, 60);
            this.lblCurLocUID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblCurLocUID.Name = "lblCurLocUID";
            this.lblCurLocUID.Size = new System.Drawing.Size(116, 19);
            this.lblCurLocUID.TabIndex = 32;
            this.lblCurLocUID.Text = "Location UID :";
            // 
            // lookUpEditLocationUID
            // 
            this.lookUpEditLocationUID.EnterMoveNextControl = true;
            this.dxErrorProvider.SetIconAlignment(this.lookUpEditLocationUID, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.lookUpEditLocationUID.Location = new System.Drawing.Point(169, 57);
            this.lookUpEditLocationUID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lookUpEditLocationUID.Name = "lookUpEditLocationUID";
            this.lookUpEditLocationUID.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditLocationUID.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditLocationUID.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditLocationUID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditLocationUID.Properties.MaxLength = 13;
            this.lookUpEditLocationUID.Properties.NullText = "";
            this.lookUpEditLocationUID.Properties.PopupSizeable = false;
            this.lookUpEditLocationUID.Properties.PopupWidth = 430;
            this.lookUpEditLocationUID.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpEditLocationUID.Size = new System.Drawing.Size(229, 26);
            this.lookUpEditLocationUID.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageIndex = 0;
            this.btnClear.ImageList = this.imgSmallImageCollection;
            this.btnClear.Location = new System.Drawing.Point(272, 415);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(126, 43);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnReport
            // 
            this.btnReport.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Appearance.Options.UseFont = true;
            this.btnReport.ImageIndex = 2;
            this.btnReport.ImageList = this.imgSmallImageCollection;
            this.btnReport.Location = new System.Drawing.Point(139, 415);
            this.btnReport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(126, 43);
            this.btnReport.TabIndex = 10;
            this.btnReport.Text = "Get Report";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // groupControl
            // 
            this.groupControl.Controls.Add(this.groupControlSelection);
            this.groupControl.Controls.Add(this.groupControl1);
            this.groupControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl.Location = new System.Drawing.Point(0, 0);
            this.groupControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControl.Name = "groupControl";
            this.groupControl.ShowCaption = false;
            this.groupControl.Size = new System.Drawing.Size(1214, 679);
            this.groupControl.TabIndex = 7;
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // RptFrmStockSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl);
            this.LookAndFeel.SkinName = "Black";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "RptFrmStockSearch";
            this.Size = new System.Drawing.Size(1214, 679);
            this.Load += new System.EventHandler(this.RptFrmStockSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelection)).EndInit();
            this.groupControlSelection.ResumeLayout(false);
            this.groupControlSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGrid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShortName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStockCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerialNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBatchLotNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditLocationUID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControlSelection;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.Utils.ImageCollection imgSmallImageCollection;
        public DevExpress.XtraEditors.SimpleButton btnReport;
        private DevExpress.XtraEditors.PanelControl panelHeader;
        private DevExpress.XtraEditors.GroupControl groupControl;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.XtraEditors.SimpleButton btnSearcher;
        private DevExpress.XtraEditors.TextEdit txtStockCode;
        private DevExpress.XtraEditors.TextEdit txtSerialNo;
        private DevExpress.XtraEditors.LabelControl lblSerialNo;
        private DevExpress.XtraEditors.TextEdit txtBatchLotNo;
        private DevExpress.XtraEditors.LabelControl lblBatchLotNo;
        private DevExpress.XtraEditors.LabelControl lblStockCode;
        private DevExpress.XtraEditors.LabelControl lblCurLocUID;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditLocationUID;
        private DevExpress.XtraEditors.TextEdit txtGrid;
        private DevExpress.XtraEditors.LabelControl lblGrid;
        private DevExpress.XtraEditors.TextEdit txtShortName;
        private DevExpress.XtraEditors.LabelControl lblShortName;
        private DevExpress.XtraEditors.LabelControl lblLocUID;
        private DevExpress.XtraEditors.DateEdit dateEditFrom;
        private DevExpress.XtraEditors.DateEdit dateEditTo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtBin;
        private DevExpress.XtraEditors.LabelControl lblBin;
    }
}
