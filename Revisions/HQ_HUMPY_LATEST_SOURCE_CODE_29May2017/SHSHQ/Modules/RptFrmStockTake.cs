 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using ISMDAL.TableColumnName;
using ISM.Reports;
using DevExpress.XtraReports.UI;

namespace ISM.Modules
{
    public partial class RptFrmStockTake : ISMBaseWorkSpace
    {
        public RptFrmStockTake(ISMLoginInfo AISMLoginInfo)
            : base(AISMLoginInfo)
        {
            InitializeComponent();
        }
        #region "Form Load"
        private void RptFrmStockTake_Load(object sender, EventArgs e)
        {
            SetLookUpEditCaption();
            LoadStockTakeMetaData();
            lookUpEditMILISStockTakeNo.Focus(); 
        }
        #endregion

        #region "Initialization"

        private void SetLookUpEditCaption()
        {
            try
            {
                 
                 
                 
                 
                 

                 
                 

                lookUpEditStockTakeNo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ISM_STOCKTAKE_NO", 90, "ISM Stocktake No"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LOCATION_UID", 150, "ISM Location UID"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ERP_BIN", 150, "ERP BIN"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("STOCK_TAKE_DATE", 150,"Completed Date")});

                lookUpEditStockTakeNo.Properties.DisplayMember = "ISM_STOCKTAKE_NO";
                lookUpEditStockTakeNo.Properties.ValueMember = "ISM_STOCKTAKE_NO";


                 
                lookUpEditMILISStockTakeNo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FK_MILIS_STOCKTAKE_NO", 90, "MILIS Stocktake No"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FK_MILIS_PLAN_ID", 90, "Plan ID"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LOCATION_UID", 150, "ISM Location UID"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ERP_BIN", 150, "ERP BIN"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("STOCK_TAKEDATE", 150,"Completed Date")});

                lookUpEditMILISStockTakeNo.Properties.DisplayMember = "FK_MILIS_STOCKTAKE_NO";
                lookUpEditMILISStockTakeNo.Properties.ValueMember = "FK_MILIS_STOCKTAKE_NO";


            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void LoadStockTakeMetaData()
        {
            DataSet ds = null;
            try
            {
                int zMode = 0;  
                ds = m_ISMLoginInfo.ISMServer.GetRptStockTakeMetaData(zMode);
                if (ds != null)
                {
                    lookUpEditStockTakeNo.Properties.DataSource = ds.Tables[0].DefaultView;
                }

                zMode = 1;  
                ds = m_ISMLoginInfo.ISMServer.GetRptStockTakeMetaData(zMode);
                if (ds != null)
                {
                    lookUpEditMILISStockTakeNo.Properties.DataSource = ds.Tables[0].DefaultView;
                }
            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region "Validation "
        private bool Validation()
        {
            bool zResult = false;
            bool zValidationFail = true;

            try
            {
                ClearErroIcon();
                if (lookUpEditMILISStockTakeNo.EditValue == null && lookUpEditStockTakeNo.EditValue == null)
                {
                    dxErrorProvider.SetError(lookUpEditMILISStockTakeNo, "Select a MILIS Stocktake No");
                    dxErrorProvider.SetError(lookUpEditStockTakeNo, "Select a ISM Stocktake No");
                    lookUpEditMILISStockTakeNo.Focus();  
                    zValidationFail = false;
                }
                 
                 
                 
                 
                 
                 
                 
                 

                 
                if (zValidationFail)
                    zResult = zValidationFail;

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stocktake Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return zResult;
        }

        #endregion

        #region "Generate Report"

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    Cursor.Current = Cursors.WaitCursor;
                    DataSet zStockDS = null;
                    DataRow zStockDR = null;
                    DataRow zStockDRDetail = null;  
                    string zStockTakeNo = "";
                    int zMode = 0;

                    if (lookUpEditMILISStockTakeNo.EditValue != null)
                    {
                        zStockTakeNo = lookUpEditMILISStockTakeNo.EditValue.ToString();
                        zMode = 1;
                    }
                    else if (lookUpEditStockTakeNo.EditValue != null)
                    {
                        zStockTakeNo = lookUpEditStockTakeNo.EditValue.ToString();
                        zMode = 0;
                    }


                    RptStockTake zRptStockTake = new RptStockTake();
                    zRptStockTake.lblReportGeneratedBy.Text = m_ISMLoginInfo.LogonID;
                    zStockDS = m_ISMLoginInfo.ISMServer.GetRptStocktakeData(zMode, zStockTakeNo);
                     
                    if (zStockDS.Tables[1].Rows.Count <= 0)
                    {
                        MessageBox.Show("Search criteria netted no results", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }

                     
                     

                    zRptStockTake.DataSource = zStockDS;
                    zRptStockTake.DataMember = zStockDS.Tables[0].TableName;
                    if(zStockDS.Tables[2].Rows.Count >0)
                        zStockDR = zStockDS.Tables[2].Rows[0];  

                     


                    
                    zRptStockTake.lblTotalStockCode.Text = zStockDS.Tables[1].Rows.Count.ToString();
                     
                    if (zStockDS.Tables[1].Rows.Count > 0)
                    {
                        zStockDRDetail = zStockDS.Tables[1].Rows[0];
                        if (lookUpEditMILISStockTakeNo.EditValue != null)
                        {
                            zRptStockTake.lblPlanID.Text = "Plan ID : " + zStockDRDetail["FK_MILIS_PLAN_ID"].ToString();
                            zRptStockTake.lblMILISStockNo.Text = "MILIS Stocktake No : " + zStockDRDetail["FK_MILIS_STOCKTAKE_NO"].ToString();
                        }
                        else
                        {
                            zRptStockTake.lblPlanID.Text = "";
                            zRptStockTake.lblMILISStockNo.Text = "ISM Stocktake No : " + zStockDRDetail["ISM_STOCKTAKE_NO"].ToString();

                        }
                         
                        zRptStockTake.lblDistrict.Text = zStockDRDetail["ERP_DISTRICT"].ToString();
                        zRptStockTake.lblGrid.Text = zStockDRDetail["ERP_GRID"].ToString();
                        zRptStockTake.lblWHS.Text = zStockDRDetail["ERP_WHOUSE"].ToString();
                        zRptStockTake.lblBin.Text = zStockDRDetail["ERP_BIN"].ToString();
                        zRptStockTake.lblLocationUID.Text = zStockDRDetail["LocationUID"].ToString();  
                         
                        zRptStockTake.lblStocktakeConductedBy.Text = zStockDRDetail["USERID"].ToString();
                        zRptStockTake.lblStocktakeConductedDate.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Parse(zStockDRDetail["STOCKTAKE_DATE"].ToString()));
                        zRptStockTake.lblReportGeneratedBy.Text = m_ISMLoginInfo.LogonID;
                        zRptStockTake.lblReportGeneratedOn.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);

                         

                         
                         
                         
                         
                         
                         
                         
                         
                         
                         
                    }
                     

                     
                     
                     
                     
                     
                     
                     
                     
                     
                   
                    
                    if (zMode == 0)
                    {
                        zRptStockTake.lblPlanID.Visible = false;
                        zRptStockTake.lblExportCommand.Visible = false;
                        zRptStockTake.lblPrintCommand.Visible = false;
                        zRptStockTake.lblPrintLine.Visible = false;
                        
                    }
                    else
                    {
                        zRptStockTake.lblPlanID.Visible = true;
                        zRptStockTake.lblExportCommand.Visible = true;
                        zRptStockTake.lblPrintCommand.Visible = true;
                        zRptStockTake.lblPrintLine.Visible = true;
                        
                         
                    }


                    zRptStockTake.lblStockCode.DataBindings.Add("Text", zStockDS, "STOCK_CODE");
                    zRptStockTake.lblShortDesc.DataBindings.Add("Text", zStockDS, "SHORT_NAME");
                    zRptStockTake.lblSOH.DataBindings.Add("Text", zStockDS, "SOH", "{0:#,#}");
                    zRptStockTake.lblSerNo.DataBindings.Add("Text", zStockDS, "SERIAL_EQUIP_NO");
                    zRptStockTake.lblBatchNo.DataBindings.Add("Text", zStockDS, "BATCH_LOT_NO");
                    zRptStockTake.lblCategory.DataBindings.Add("Text", zStockDS, "INVCODE");

                     
                    string zJnlDesc = "";
                    if (lookUpEditMILISStockTakeNo.EditValue != null)
                    {
                        zJnlDesc = "Stocktake Report Generated (MILIS Stocktake No " + lookUpEditMILISStockTakeNo.EditValue.ToString() + " )";
                    }
                    else if (lookUpEditStockTakeNo.EditValue != null)
                    {
                        zJnlDesc = "Stocktake Report Generated (ISM Stocktake No " + lookUpEditStockTakeNo.Text.Trim() + " )";

                    }
                    m_ISMLoginInfo.AddToJournal("T", zJnlDesc, "RPT001", "", "0", "0", "0");
                     
                    Cursor.Current = Cursors.Default;
                    zRptStockTake.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stocktake Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        #endregion

        #region "Clear Button"
        private void ClearErroIcon()
        {
            dxErrorProvider.SetError(lookUpEditMILISStockTakeNo, null);
            dxErrorProvider.SetError(lookUpEditStockTakeNo, null);

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearErroIcon();
                lookUpEditMILISStockTakeNo.EditValue = null;  
                lookUpEditStockTakeNo.EditValue = null;
                lookUpEditMILISStockTakeNo.Focus();
                lookUpEditStockTakeNo.Enabled = true;
                lookUpEditMILISStockTakeNo.Enabled = true;
                lookUpEditMILISStockTakeNo.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stocktake Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region "Edit Value Changed"
         
        private void lookUpEditMILISStockTakeNo_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                ClearErroIcon();
                lookUpEditStockTakeNo.EditValueChanged -= new System.EventHandler(lookUpEditStockTakeNo_EditValueChanged);
                lookUpEditStockTakeNo.EditValue = null;
                lookUpEditStockTakeNo.Enabled = false;
                lookUpEditStockTakeNo.EditValueChanged += new System.EventHandler(lookUpEditStockTakeNo_EditValueChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stocktake Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        

        private void lookUpEditStockTakeNo_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                 
                 
                 
                ClearErroIcon();
                lookUpEditMILISStockTakeNo.EditValueChanged -= new System.EventHandler(lookUpEditMILISStockTakeNo_EditValueChanged);
                lookUpEditMILISStockTakeNo.EditValue = null;
                lookUpEditMILISStockTakeNo.Enabled = false;
                lookUpEditMILISStockTakeNo.EditValueChanged += new System.EventHandler(lookUpEditMILISStockTakeNo_EditValueChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stocktake Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        

         
        #endregion

    }
}
