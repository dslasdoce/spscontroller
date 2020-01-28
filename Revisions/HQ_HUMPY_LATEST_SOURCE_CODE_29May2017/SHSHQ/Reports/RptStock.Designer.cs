namespace ISM.Reports
{
    partial class RptStock
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.lblCategory = new DevExpress.XtraReports.UI.XRLabel();
            this.lblBatchNo = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSerNo = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSOH = new DevExpress.XtraReports.UI.XRLabel();
            this.lblShortDesc = new DevExpress.XtraReports.UI.XRLabel();
            this.lblStockCode = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
            this.lblSelCategoryCode = new DevExpress.XtraReports.UI.XRLabel();
            this.lblLocParUID = new DevExpress.XtraReports.UI.XRLabel();
            this.lblLocDesc = new DevExpress.XtraReports.UI.XRLabel();
            this.lblLocUID = new DevExpress.XtraReports.UI.XRLabel();
            this.lblRptRecCount = new DevExpress.XtraReports.UI.XRLabel();
            this.lblReportNameCap = new DevExpress.XtraReports.UI.XRLabel();
            this.lblUserName = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHeader1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHeader2 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCrossBandBox1 = new DevExpress.XtraReports.UI.XRCrossBandBox();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.lblPageInfoDate = new DevExpress.XtraReports.UI.XRPageInfo();
            this.lblPageInfoPageNo = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrCrossBandLine1 = new DevExpress.XtraReports.UI.XRCrossBandLine();
            this.xrCrossBandLine2 = new DevExpress.XtraReports.UI.XRCrossBandLine();
            this.xrCrossBandLine3 = new DevExpress.XtraReports.UI.XRCrossBandLine();
            this.xrCrossBandLine4 = new DevExpress.XtraReports.UI.XRCrossBandLine();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.xrCrossBandLine5 = new DevExpress.XtraReports.UI.XRCrossBandLine();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblCategory,
            this.lblBatchNo,
            this.lblSerNo,
            this.lblSOH,
            this.lblShortDesc,
            this.lblStockCode});
            this.Detail.HeightF = 28F;
            this.Detail.KeepTogether = true;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblCategory
            // 
            this.lblCategory.LocationFloat = new DevExpress.Utils.PointFloat(349.875F, 2F);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCategory.SizeF = new System.Drawing.SizeF(41.08334F, 22.99998F);
            this.lblCategory.StylePriority.UseTextAlignment = false;
            this.lblCategory.Text = "Cat";
            this.lblCategory.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblBatchNo
            // 
            this.lblBatchNo.LocationFloat = new DevExpress.Utils.PointFloat(636.125F, 2F);
            this.lblBatchNo.Name = "lblBatchNo";
            this.lblBatchNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblBatchNo.SizeF = new System.Drawing.SizeF(82.29169F, 23F);
            this.lblBatchNo.StylePriority.UseTextAlignment = false;
            this.lblBatchNo.Text = "BatchNo";
            this.lblBatchNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblSerNo
            // 
            this.lblSerNo.LocationFloat = new DevExpress.Utils.PointFloat(467.5834F, 2F);
            this.lblSerNo.Name = "lblSerNo";
            this.lblSerNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblSerNo.SizeF = new System.Drawing.SizeF(162.5F, 23F);
            this.lblSerNo.StylePriority.UseTextAlignment = false;
            this.lblSerNo.Text = "SerNo";
            this.lblSerNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblSOH
            // 
            this.lblSOH.LocationFloat = new DevExpress.Utils.PointFloat(396.9583F, 2F);
            this.lblSOH.Name = "lblSOH";
            this.lblSOH.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblSOH.SizeF = new System.Drawing.SizeF(67.70831F, 23F);
            this.lblSOH.StylePriority.UseTextAlignment = false;
            this.lblSOH.Text = "SOH";
            this.lblSOH.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblShortDesc
            // 
            this.lblShortDesc.LocationFloat = new DevExpress.Utils.PointFloat(119.875F, 2F);
            this.lblShortDesc.Multiline = true;
            this.lblShortDesc.Name = "lblShortDesc";
            this.lblShortDesc.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblShortDesc.SizeF = new System.Drawing.SizeF(225F, 25F);
            this.lblShortDesc.StylePriority.UseTextAlignment = false;
            this.lblShortDesc.Text = "Shot Name";
            this.lblShortDesc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblStockCode
            // 
            this.lblStockCode.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 2F);
            this.lblStockCode.Name = "lblStockCode";
            this.lblStockCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblStockCode.SizeF = new System.Drawing.SizeF(101.0416F, 22.99999F);
            this.lblStockCode.StylePriority.UseTextAlignment = false;
            this.lblStockCode.Text = "Stock Code";
            this.lblStockCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // TopMargin
            // 
            this.TopMargin.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TopMargin.HeightF = 75F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.StylePriority.UseFont = false;
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 75F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel2,
            this.lblRptRecCount,
            this.lblReportNameCap,
            this.lblUserName,
            this.lblHeader1,
            this.lblHeader2});
            this.ReportHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReportHeader.HeightF = 214.0001F;
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.StylePriority.UseFont = false;
            // 
            // xrPanel2
            // 
            this.xrPanel2.BackColor = System.Drawing.Color.LightSeaGreen;
            this.xrPanel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblSelCategoryCode,
            this.lblLocParUID,
            this.lblLocDesc,
            this.lblLocUID});
            this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(0.1249949F, 158.3334F);
            this.xrPanel2.Name = "xrPanel2";
            this.xrPanel2.SizeF = new System.Drawing.SizeF(725.9166F, 55.66666F);
            this.xrPanel2.StylePriority.UseBackColor = false;
            // 
            // lblSelCategoryCode
            // 
            this.lblSelCategoryCode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelCategoryCode.ForeColor = System.Drawing.Color.Black;
            this.lblSelCategoryCode.LocationFloat = new DevExpress.Utils.PointFloat(447.7917F, 4.708385F);
            this.lblSelCategoryCode.Name = "lblSelCategoryCode";
            this.lblSelCategoryCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblSelCategoryCode.SizeF = new System.Drawing.SizeF(265.1249F, 22.99998F);
            this.lblSelCategoryCode.StylePriority.UseFont = false;
            this.lblSelCategoryCode.StylePriority.UseForeColor = false;
            this.lblSelCategoryCode.Text = "CategoryCode";
            // 
            // lblLocParUID
            // 
            this.lblLocParUID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocParUID.ForeColor = System.Drawing.Color.Black;
            this.lblLocParUID.LocationFloat = new DevExpress.Utils.PointFloat(447.7917F, 30.70839F);
            this.lblLocParUID.Name = "lblLocParUID";
            this.lblLocParUID.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblLocParUID.SizeF = new System.Drawing.SizeF(265.1249F, 22.99998F);
            this.lblLocParUID.StylePriority.UseFont = false;
            this.lblLocParUID.StylePriority.UseForeColor = false;
            this.lblLocParUID.Text = "Fixed Location UID";
            // 
            // lblLocDesc
            // 
            this.lblLocDesc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocDesc.ForeColor = System.Drawing.Color.Black;
            this.lblLocDesc.LocationFloat = new DevExpress.Utils.PointFloat(6.875007F, 30.70839F);
            this.lblLocDesc.Name = "lblLocDesc";
            this.lblLocDesc.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblLocDesc.SizeF = new System.Drawing.SizeF(387.5F, 22.99998F);
            this.lblLocDesc.StylePriority.UseFont = false;
            this.lblLocDesc.StylePriority.UseForeColor = false;
            this.lblLocDesc.Text = "Fixed Location Description";
            // 
            // lblLocUID
            // 
            this.lblLocUID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocUID.ForeColor = System.Drawing.Color.Black;
            this.lblLocUID.LocationFloat = new DevExpress.Utils.PointFloat(6.875007F, 4.708374F);
            this.lblLocUID.Name = "lblLocUID";
            this.lblLocUID.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblLocUID.SizeF = new System.Drawing.SizeF(387.5F, 22.99998F);
            this.lblLocUID.StylePriority.UseFont = false;
            this.lblLocUID.StylePriority.UseForeColor = false;
            this.lblLocUID.Text = "Stock Search for Location :";
            // 
            // lblRptRecCount
            // 
            this.lblRptRecCount.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRptRecCount.LocationFloat = new DevExpress.Utils.PointFloat(527.0832F, 131.7917F);
            this.lblRptRecCount.Name = "lblRptRecCount";
            this.lblRptRecCount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblRptRecCount.SizeF = new System.Drawing.SizeF(198.9584F, 23.00002F);
            this.lblRptRecCount.StylePriority.UseFont = false;
            this.lblRptRecCount.StylePriority.UseTextAlignment = false;
            this.lblRptRecCount.Text = "lblRptRecCount";
            this.lblRptRecCount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // lblReportNameCap
            // 
            this.lblReportNameCap.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportNameCap.ForeColor = System.Drawing.Color.Black;
            this.lblReportNameCap.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 103.7917F);
            this.lblReportNameCap.Name = "lblReportNameCap";
            this.lblReportNameCap.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReportNameCap.SizeF = new System.Drawing.SizeF(120.8333F, 22.99999F);
            this.lblReportNameCap.StylePriority.UseFont = false;
            this.lblReportNameCap.StylePriority.UseForeColor = false;
            this.lblReportNameCap.Text = "Report run by :";
            // 
            // lblUserName
            // 
            this.lblUserName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUserName.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 130.7917F);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblUserName.SizeF = new System.Drawing.SizeF(120.8333F, 23F);
            this.lblUserName.StylePriority.UseFont = false;
            this.lblUserName.StylePriority.UseForeColor = false;
            this.lblUserName.Text = "Report Printed By";
            // 
            // lblHeader1
            // 
            this.lblHeader1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader1.ForeColor = System.Drawing.Color.Navy;
            this.lblHeader1.LocationFloat = new DevExpress.Utils.PointFloat(162.5F, 53F);
            this.lblHeader1.Name = "lblHeader1";
            this.lblHeader1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHeader1.SizeF = new System.Drawing.SizeF(398.9583F, 28.20832F);
            this.lblHeader1.StylePriority.UseFont = false;
            this.lblHeader1.StylePriority.UseForeColor = false;
            this.lblHeader1.StylePriority.UseTextAlignment = false;
            this.lblHeader1.Text = "Improved Stock Management";
            this.lblHeader1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblHeader2
            // 
            this.lblHeader2.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblHeader2.LocationFloat = new DevExpress.Utils.PointFloat(262.5F, 12.5F);
            this.lblHeader2.Name = "lblHeader2";
            this.lblHeader2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHeader2.SizeF = new System.Drawing.SizeF(189.5833F, 37.5F);
            this.lblHeader2.StylePriority.UseFont = false;
            this.lblHeader2.StylePriority.UseForeColor = false;
            this.lblHeader2.StylePriority.UseTextAlignment = false;
            this.lblHeader2.Text = "Stock Report";
            this.lblHeader2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
            this.PageHeader.HeightF = 45F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.StylePriority.UseBackColor = false;
            // 
            // xrPanel1
            // 
            this.xrPanel1.BackColor = System.Drawing.Color.DarkGray;
            this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel6,
            this.xrLabel7,
            this.xrLabel8});
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(2.083333F, 1.249995F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.SizeF = new System.Drawing.SizeF(721.8749F, 41.54167F);
            this.xrPanel1.StylePriority.UseBackColor = false;
            this.xrPanel1.StylePriority.UseBorders = false;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(347.7916F, 9.41667F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(41.08334F, 22.99998F);
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Cat";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(117.7916F, 9.41667F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(225F, 25F);
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Short Name";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(7.916674F, 9.41667F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(101.0416F, 22.99999F);
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Stock Code";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(394.875F, 9.41667F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(67.70831F, 23F);
            this.xrLabel6.StylePriority.UseBorders = false;
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "SOH";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(465.5001F, 9.41667F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(162.5F, 23F);
            this.xrLabel7.StylePriority.UseBorders = false;
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "Ser/Eqp No.";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(634.0417F, 9.41667F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(82.29169F, 23F);
            this.xrLabel8.StylePriority.UseBorders = false;
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "Batch/Lot";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrCrossBandBox1
            // 
            this.xrCrossBandBox1.EndBand = this.PageFooter;
            this.xrCrossBandBox1.EndPointFloat = new DevExpress.Utils.PointFloat(1.041667F, 3.125F);
            this.xrCrossBandBox1.LocationFloat = new DevExpress.Utils.PointFloat(1.041667F, 0F);
            this.xrCrossBandBox1.Name = "xrCrossBandBox1";
            this.xrCrossBandBox1.StartBand = this.PageHeader;
            this.xrCrossBandBox1.StartPointFloat = new DevExpress.Utils.PointFloat(1.041667F, 0F);
            this.xrCrossBandBox1.WidthF = 725F;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblPageInfoDate,
            this.lblPageInfoPageNo});
            this.PageFooter.Name = "PageFooter";
            // 
            // lblPageInfoDate
            // 
            this.lblPageInfoDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageInfoDate.Format = "Run Date {0:dd MMM yy}";
            this.lblPageInfoDate.LocationFloat = new DevExpress.Utils.PointFloat(0F, 6F);
            this.lblPageInfoDate.Name = "lblPageInfoDate";
            this.lblPageInfoDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblPageInfoDate.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.lblPageInfoDate.SizeF = new System.Drawing.SizeF(202.4583F, 23.00002F);
            this.lblPageInfoDate.StylePriority.UseFont = false;
            this.lblPageInfoDate.StylePriority.UseTextAlignment = false;
            this.lblPageInfoDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblPageInfoPageNo
            // 
            this.lblPageInfoPageNo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageInfoPageNo.Format = "Page {0} of {1}";
            this.lblPageInfoPageNo.LocationFloat = new DevExpress.Utils.PointFloat(569.7916F, 6F);
            this.lblPageInfoPageNo.Name = "lblPageInfoPageNo";
            this.lblPageInfoPageNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblPageInfoPageNo.SizeF = new System.Drawing.SizeF(154.1667F, 23F);
            this.lblPageInfoPageNo.StylePriority.UseFont = false;
            this.lblPageInfoPageNo.StylePriority.UseTextAlignment = false;
            this.lblPageInfoPageNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrCrossBandLine1
            // 
            this.xrCrossBandLine1.EndBand = this.PageHeader;
            this.xrCrossBandLine1.EndPointFloat = new DevExpress.Utils.PointFloat(632.3333F, 43.91664F);
            this.xrCrossBandLine1.LocationFloat = new DevExpress.Utils.PointFloat(632.3333F, 0F);
            this.xrCrossBandLine1.Name = "xrCrossBandLine1";
            this.xrCrossBandLine1.StartBand = this.PageHeader;
            this.xrCrossBandLine1.StartPointFloat = new DevExpress.Utils.PointFloat(632.3333F, 0F);
            this.xrCrossBandLine1.WidthF = 1.041687F;
            // 
            // xrCrossBandLine2
            // 
            this.xrCrossBandLine2.EndBand = this.PageHeader;
            this.xrCrossBandLine2.EndPointFloat = new DevExpress.Utils.PointFloat(392.9583F, 41.83331F);
            this.xrCrossBandLine2.LocationFloat = new DevExpress.Utils.PointFloat(392.9583F, 0F);
            this.xrCrossBandLine2.Name = "xrCrossBandLine2";
            this.xrCrossBandLine2.StartBand = this.PageHeader;
            this.xrCrossBandLine2.StartPointFloat = new DevExpress.Utils.PointFloat(392.9583F, 0F);
            this.xrCrossBandLine2.WidthF = 1.083313F;
            // 
            // xrCrossBandLine3
            // 
            this.xrCrossBandLine3.EndBand = this.PageHeader;
            this.xrCrossBandLine3.EndPointFloat = new DevExpress.Utils.PointFloat(346F, 43.875F);
            this.xrCrossBandLine3.LocationFloat = new DevExpress.Utils.PointFloat(346F, 1.041667F);
            this.xrCrossBandLine3.Name = "xrCrossBandLine3";
            this.xrCrossBandLine3.StartBand = this.PageHeader;
            this.xrCrossBandLine3.StartPointFloat = new DevExpress.Utils.PointFloat(346F, 1.041667F);
            this.xrCrossBandLine3.WidthF = 1.041656F;
            // 
            // xrCrossBandLine4
            // 
            this.xrCrossBandLine4.EndBand = this.PageHeader;
            this.xrCrossBandLine4.EndPointFloat = new DevExpress.Utils.PointFloat(116.6667F, 42.87497F);
            this.xrCrossBandLine4.LocationFloat = new DevExpress.Utils.PointFloat(116.6667F, 0F);
            this.xrCrossBandLine4.Name = "xrCrossBandLine4";
            this.xrCrossBandLine4.StartBand = this.PageHeader;
            this.xrCrossBandLine4.StartPointFloat = new DevExpress.Utils.PointFloat(116.6667F, 0F);
            this.xrCrossBandLine4.WidthF = 1.041664F;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Condition = "([DataSource.CurrentRowIndex] % [Parameters.parameter1] == 0) And ([DataSource.Cu" +
                "rrentRowIndex] != 0)\r\n";
            this.formattingRule1.Name = "formattingRule1";
            // 
            // xrCrossBandLine5
            // 
            this.xrCrossBandLine5.EndBand = this.PageHeader;
            this.xrCrossBandLine5.EndPointFloat = new DevExpress.Utils.PointFloat(466.5417F, 41.66667F);
            this.xrCrossBandLine5.LocationFloat = new DevExpress.Utils.PointFloat(466.5417F, 1.041667F);
            this.xrCrossBandLine5.Name = "xrCrossBandLine5";
            this.xrCrossBandLine5.StartBand = this.PageHeader;
            this.xrCrossBandLine5.StartPointFloat = new DevExpress.Utils.PointFloat(466.5417F, 1.041667F);
            this.xrCrossBandLine5.WidthF = 1.041687F;
            // 
            // RptStock
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.PageFooter});
            this.CrossBandControls.AddRange(new DevExpress.XtraReports.UI.XRCrossBandControl[] {
            this.xrCrossBandLine5,
            this.xrCrossBandLine4,
            this.xrCrossBandLine3,
            this.xrCrossBandLine2,
            this.xrCrossBandLine1,
            this.xrCrossBandBox1});
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 75, 75);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ShowPreviewMarginLines = false;
            this.Version = "10.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRLabel lblHeader1;
        private DevExpress.XtraReports.UI.XRLabel lblHeader2;
        private DevExpress.XtraReports.UI.XRLabel lblReportNameCap;
        public DevExpress.XtraReports.UI.XRLabel lblRptRecCount;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRCrossBandBox xrCrossBandBox1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRCrossBandLine xrCrossBandLine1;
        private DevExpress.XtraReports.UI.XRCrossBandLine xrCrossBandLine2;
        private DevExpress.XtraReports.UI.XRCrossBandLine xrCrossBandLine3;
        private DevExpress.XtraReports.UI.XRCrossBandLine xrCrossBandLine4;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private DevExpress.XtraReports.UI.XRPanel xrPanel2;
        private DevExpress.XtraReports.UI.XRPageInfo lblPageInfoDate;
        private DevExpress.XtraReports.UI.XRPageInfo lblPageInfoPageNo;
        public DevExpress.XtraReports.UI.XRLabel lblLocUID;
        public DevExpress.XtraReports.UI.XRLabel lblLocParUID;
        public DevExpress.XtraReports.UI.XRLabel lblLocDesc;
        public DevExpress.XtraReports.UI.XRLabel lblBatchNo;
        public DevExpress.XtraReports.UI.XRLabel lblSerNo;
        public DevExpress.XtraReports.UI.XRLabel lblSOH;
        public DevExpress.XtraReports.UI.XRLabel lblShortDesc;
        public DevExpress.XtraReports.UI.XRLabel lblStockCode;
        public DevExpress.XtraReports.UI.XRLabel lblUserName;
        private DevExpress.XtraReports.UI.FormattingRule formattingRule1;
        public DevExpress.XtraReports.UI.XRLabel lblCategory;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        public DevExpress.XtraReports.UI.XRLabel lblSelCategoryCode;
        private DevExpress.XtraReports.UI.XRCrossBandLine xrCrossBandLine5;
    }
}
