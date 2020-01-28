 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using ISMDAL.TableColumnName;
using ISM.Reports;
using DevExpress.XtraReports.UI;

namespace ISM.Modules
{
    public partial class RptFrmStockSearch : ISMBaseWorkSpace
    {
        public RptFrmStockSearch(ISMLoginInfo AISMLoginInfo)
            : base(AISMLoginInfo)
        {
            InitializeComponent();
        }

        #region "Form Load"
        private void RptFrmStockSearch_Load(object sender, EventArgs e)
        {

            SetLookUpEditCaption();
            LoadStockSearchMetaData();
            lookUpEditLocationUID.Focus();
        }
        #endregion

        #region "Initialization"
        private void SetLookUpEditCaption()
        {
            try
            {

                lookUpEditLocationUID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LOCATION_UID", 90, "Location UID"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.LocationCode, 120,"Location Code"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPDIST, 60, "ERP DIST"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPWHS, 60, "ERP WHS"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPGRID, 60, "ERP GRID"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPBIN, 60, "ERP BIN")});

                lookUpEditLocationUID.Properties.DisplayMember = "LOCATION_UID"; 
                lookUpEditLocationUID.Properties.ValueMember = ISMLocation.LocationUID;


            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Stock Search Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void LoadStockSearchMetaData()
        {
            try
            {
                DataSet ds = m_ISMLoginInfo.ISMServer.GetReciveItemUIDAndLocationUID();
                if (ds != null)
                {
                    lookUpEditLocationUID.Properties.DataSource = ds.Tables[1].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Search Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region "Stock Code Searcher"

        private void btnSearcher_Click(object sender, EventArgs e)
        {
            try
            {
                ISM.Forms.FrmStockCodeSearcher zStockCodeSearcher = new ISM.Forms.FrmStockCodeSearcher(m_ISMLoginInfo);
                zStockCodeSearcher.InputStockCode = txtStockCode.Text.Trim(); 
                zStockCodeSearcher.ShowDialog();
                txtStockCode.Text = zStockCodeSearcher.StockCode;
                if (lookUpEditLocationUID.Text.Trim() == "" && txtStockCode.Text.Trim() != "")
                {
                    LoadLocationUIDForStockCode();
                }
            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Stock Search Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region "General Function"
        private void LoadLocationUIDForStockCode()
        {
            try
            {
                DataSet ds = m_ISMLoginInfo.ISMServer.GetReceiveLocationUIDForStockCode(txtStockCode.Text.Trim());
                if (ds != null)
                    lookUpEditLocationUID.Properties.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Search Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                dxErrorProvider.SetError(dateEditFrom, null);
                dxErrorProvider.SetError(dateEditTo, null);

                if (dateEditTo.Text != "")
                {
                    if (dateEditFrom.Text == "")
                    {
                        dxErrorProvider.SetError(dateEditFrom, "Select a From Date");
                        dateEditFrom.Focus();
                        zValidationFail = false;
                    }
                    else if (dateEditFrom.Text != "")
                    {
                        if (DateTime.Parse(dateEditFrom.Text) > DateTime.Parse(dateEditTo.Text))
                        {
                            dxErrorProvider.SetError(dateEditTo, "To Date should be greater than From Date");
                            dateEditTo.Focus();
                            zValidationFail = false;
                        }
                    }
                }
                if (zValidationFail)
                    zResult = zValidationFail;

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stock Search Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return zResult;
        }

        #endregion

        #region "Report"
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {

                if (Validation())
                {
                    int zMode = 0;
                    string zStartDate = "";
                    string zEndDate = "";
                    string zLocUID = "";
                    string zSearchCriteria = "";
                    long zLocationUID = 0;
                    DataSet zLocERPDS = null;
                    int zRecCount = 0;
                    if (dateEditFrom.Text.Trim() != "" && dateEditTo.Text.Trim() != "")
                    {
                        zMode = 2;
                        zStartDate = dateEditFrom.Text.Trim();
                        zEndDate = dateEditTo.Text.Trim();
                    }
                    else if (dateEditFrom.Text.Trim() != "" && dateEditTo.Text.Trim() == "")
                    {
                        zMode = 1;
                        zEndDate = DateTime.Now.ToString();
                        zStartDate = dateEditFrom.Text.Trim();
                    }
                    else if (dateEditFrom.Text.Trim() == "" && dateEditTo.Text.Trim() == "")
                    {
                        zMode = 0;
                        zStartDate = DateTime.Now.ToString();
                        zEndDate = DateTime.Now.ToString();
                    }
                    if(lookUpEditLocationUID.Text != "")
                    {
                        zLocUID = lookUpEditLocationUID.EditValue.ToString();
                    }
                    if (lookUpEditLocationUID.Text.Trim() != "")
                        zSearchCriteria = "LOC UID = " + lookUpEditLocationUID.Text.Trim();
                    
                    if (txtStockCode.Text.Trim() != "")
                    {
                        if(zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "Stock Code = " + txtStockCode.Text.Trim();
                        else
                             zSearchCriteria += "Stock Code = " + txtStockCode.Text.Trim();
                    }
                    if (txtShortName.Text.Trim() != "")
                    {
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "Short Name = " + txtShortName.Text.Trim();
                        else
                            zSearchCriteria += "Short Name = " + txtShortName.Text.Trim();
                    }

                    if (txtSerialNo.Text.Trim() != "")
                    {
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "Serial No = " + txtSerialNo.Text.Trim();
                        else
                            zSearchCriteria += "Serial No = " + txtSerialNo.Text.Trim();
                    }
                    if (txtBatchLotNo.Text.Trim() != "")
                    {
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "Batch No = " + txtBatchLotNo.Text.Trim();
                        else
                            zSearchCriteria += "Batch No = " + txtBatchLotNo.Text.Trim();
                    }
                    if (txtGrid.Text.Trim() != "")
                    {
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "Grid = " + txtGrid.Text.Trim();
                        else
                            zSearchCriteria += "Grid = " + txtGrid.Text.Trim();
                    }

                    if (txtBin.Text.Trim() != "")
                    {
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "Bin = " + txtBin.Text.Trim();
                        else
                            zSearchCriteria += "Bin = " + txtBin.Text.Trim();
                    }

                    if (dateEditFrom.Text.Trim() != "")
                    {
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "From Date = " + dateEditFrom.Text.Trim();
                        else
                            zSearchCriteria += "From Date  = " + dateEditFrom.Text.Trim();
                    }
                    if (dateEditTo.Text.Trim() != "")
                    {
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "To Date = " + dateEditTo.Text.Trim();
                        else
                            zSearchCriteria += "To Date  = " + dateEditTo.Text.Trim();
                    }


                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     

                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     

                     
                     
                     
                     
                     
                     
                     
                     

                     
                     
                     
                     
                     
                     
                     
                     
                     
                     

                     
                     
                     
                     
                     
                     
                     
                     
                     

                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                    
                     
                     
                     
                     
                     
                   
                     
                     
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Search Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }
        #endregion

        #region "Clear Button"
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                dxErrorProvider.SetError(dateEditFrom, null);  
                dxErrorProvider.SetError(dateEditTo, null);  
                lookUpEditLocationUID.EditValue = null;
                txtStockCode.EditValue = null;
                txtShortName.EditValue = null;
                txtSerialNo.EditValue = null;
                txtBatchLotNo.EditValue = null;
                txtGrid.EditValue = null;
                txtBin.EditValue = null;
                dateEditFrom.EditValue = null;
                dateEditTo.EditValue = null;
                lookUpEditLocationUID.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Search Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        #endregion

        #region "Edit Value Changed"

        private void dateEditFrom_EditValueChanged(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(dateEditFrom, null);  

        }
        private void dateEditTo_EditValueChanged(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(dateEditTo, null);  

        }
        #endregion


    }
}
