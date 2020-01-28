using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ISMDAL.TableColumnName;
using ISM.Reports;

namespace ISM.Modules
{
    public partial class RptFrmStock : ISMBaseWorkSpace
    {
        public RptFrmStock(ISMLoginInfo AISMLoginInfo)
            : base(AISMLoginInfo)
        {
            InitializeComponent();
        }

        private void RptFrmStock_Load(object sender, EventArgs e)
        {
            try
            {
                SetLookUpEditCaption();
                LoadLoacationMetaData();
                lookUpEditLocationUID.Focus();
            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Stock Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }

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

                 
                lookUpEditCategory.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMCategory.CategoryCode, 100,"Code"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMCategory.CategoryDesc, 350, "Description")});

                 
                lookUpEditCategory.Properties.DisplayMember = ISMCategory.CategoryCode;
                lookUpEditCategory.Properties.ValueMember = ISMCategory.CategoryCode;


            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Stock Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void LoadLoacationMetaData()
        {
            try
            {
                DataSet ds = m_ISMLoginInfo.ISMServer.GetReciveItemUIDAndLocationUID();
                if (ds != null)
                {
                    lookUpEditLocationUID.Properties.DataSource = ds.Tables[1].DefaultView;
                }
                 
                DataSet zPriorityCategoryCode = null;
                zPriorityCategoryCode = m_ISMLoginInfo.ISMServer.GetStockCategory();
                if (zPriorityCategoryCode.Tables[1].Rows.Count > 0)
                {
                    zPriorityCategoryCode.Tables[0].Rows.Add("-----", "------------------------------------------");
                    zPriorityCategoryCode.Tables[0].AcceptChanges();
                }
                foreach (DataRow dr in zPriorityCategoryCode.Tables[1].Rows)
                {
                    zPriorityCategoryCode.Tables[0].Rows.Add(dr.ItemArray);
                    zPriorityCategoryCode.Tables[0].AcceptChanges();
                }
                lookUpEditCategory.Properties.DataSource = zPriorityCategoryCode.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void lookUpEditLocationUID_EditValueChanged(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(lookUpEditLocationUID, null);
            dxErrorProvider.SetError(lookUpEditCategory, null);
        }
        private bool Validation()
        {
            bool zResult = false;
            bool zValidationFail = true;

            try
            {
                dxErrorProvider.SetError(lookUpEditLocationUID, null);
                dxErrorProvider.SetError(lookUpEditCategory, null);
                
                 
                if (lookUpEditLocationUID.EditValue == null && lookUpEditCategory.EditValue == null)
                {
                    dxErrorProvider.SetError(lookUpEditLocationUID, "Select a Location UID or Category");
                    dxErrorProvider.SetError(lookUpEditCategory, "Select a Category or Location UID");
                    lookUpEditLocationUID.Focus();
                    zValidationFail = false;
                }
                
                if (lookUpEditCategory.EditValue != null)  
                {
                    if (lookUpEditCategory.EditValue.ToString() == "-----")
                    {
                        dxErrorProvider.SetError(lookUpEditCategory, "Invalid Category");
                        lookUpEditCategory.Focus();
                        zValidationFail = false;
                    }
                }

                if (zValidationFail)
                    zResult = zValidationFail;

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stock Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return zResult;
        }


        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    Cursor.Current=  Cursors.WaitCursor;
                    string zLocUID = "";
                    string zCatCode = "";
                    DataSet zStockDS = null;
                    DataRow zStockDR = null;
                    if (lookUpEditLocationUID.EditValue != null)
                    {
                        zLocUID = lookUpEditLocationUID.EditValue.ToString();
                    }
                    if (lookUpEditCategory.EditValue != null)  
                    {
                        zCatCode = lookUpEditCategory.EditValue.ToString();
                    }
                    zStockDS = m_ISMLoginInfo.ISMServer.GetRptStockData(zLocUID, zCatCode);
                     
                    if (zStockDS.Tables[2].Rows.Count <= 0)
                    {
                        MessageBox.Show("Search criteria netted no results", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    RptStock zRptStock = new RptStock();

                    zRptStock.DataSource = zStockDS;
                    zRptStock.DataMember = zStockDS.Tables[0].TableName;
                    if(zStockDS.Tables[1].Rows.Count > 0)
                        zStockDR = zStockDS.Tables[1].Rows[0];  

                    zRptStock.lblUserName.Text = m_ISMLoginInfo.LogonID;
                    if (zStockDS.Tables[1].Rows.Count > 0)
                    {
                        zRptStock.lblRptRecCount.Text = "No of Stock Codes : " + zStockDS.Tables[2].Rows.Count;
                        zRptStock.lblLocDesc.Text = "Fixed Location Description :" + zStockDR["USER_DESCRIPTION"].ToString();
                        zRptStock.lblLocParUID.Text = "Fixed Location UID : " + zStockDR["UIDStr"].ToString();
                         
                        if (zCatCode.Trim() != "") 
                            zRptStock.lblSelCategoryCode.Text = "Category : " + zCatCode;
                        else
                            zRptStock.lblSelCategoryCode.Text = "";
                    }
                    else
                    {
                        zRptStock.lblRptRecCount.Text = "No of Stock Codes : 0";
                        zRptStock.lblLocDesc.Text = "Fixed Location Description :";
                        zRptStock.lblLocParUID.Text = "Fixed Location UID : ";
                        zRptStock.lblSelCategoryCode.Text = ""; 

                    }
                    zRptStock.lblLocUID.Text = "Stock Search for Location : <" + lookUpEditLocationUID.Text.Trim() + ">";

                     
                    zRptStock.lblStockCode.DataBindings.Add("Text", zStockDS, "FK_CATALOGUED_STOCK_CODE");
                    zRptStock.lblShortDesc.DataBindings.Add("Text", zStockDS, "SHORT_NAME");
                    zRptStock.lblSOH.DataBindings.Add("Text", zStockDS, "SOH", "{0:#,#}");
                    zRptStock.lblSerNo.DataBindings.Add("Text", zStockDS, "SERIAL_EQUIP_NO");
                    zRptStock.lblBatchNo.DataBindings.Add("Text", zStockDS, "BATCH_LOT_NO");
                    zRptStock.lblCategory.DataBindings.Add("Text", zStockDS, "CATCODE");  
                     
                    string zJnlDesc = "Stock Report Generated";
                    
                    m_ISMLoginInfo.AddToJournal("T", zJnlDesc, "RPT001", "", lookUpEditLocationUID.Text.Trim(), "0", "0","",zCatCode);  
                     
                    Cursor.Current = Cursors.Default;

                    //zRptStock.ShowPreviewDialog();


                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stock Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Cursor.Current = Cursors.Default;
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                dxErrorProvider.SetError(lookUpEditLocationUID, null);
                dxErrorProvider.SetError(lookUpEditCategory, null);
                lookUpEditLocationUID.EditValue = null;
                lookUpEditCategory.EditValue = null;
                lookUpEditLocationUID.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stock Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void lookUpEditCategory_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dxErrorProvider.SetError(lookUpEditCategory, null);
                dxErrorProvider.SetError(lookUpEditLocationUID, null);
                if (lookUpEditCategory.EditValue != null)
                {
                    if (lookUpEditCategory.EditValue.ToString() == "-----")
                        dxErrorProvider.SetError(lookUpEditCategory, "Invalid Category");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

       
    }
}
