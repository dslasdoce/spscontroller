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
using DevExpress.XtraReports.UI;

namespace ISM.Modules
{
    public partial class RptFrmStockHierarchical : ISMBaseWorkSpace
    {
        private bool m_SelectStockCode = false;
        public byte m_ReportType = 0;  

        public RptFrmStockHierarchical(ISMLoginInfo AISMLoginInfo)
            : base(AISMLoginInfo)
        {
            InitializeComponent();
        }

        private void RptFrmStockHierarchical_Load(object sender, EventArgs e)
        {
            try
            {
                if (m_ReportType == (int)ISM.ReportType.Location)
                {
                    lblHeader.Text = "ISM 8000 - Stock Location Report (Hierarchical)";
                    txtStockCode.Enabled = false;
                    btnSearcher.Enabled = false;
                    SetLookUpEditCaption();
                    LoadStockSearchMetaData();
                    lookUpEditLocationUID.Focus();

                }
                else
                {
                    lblHeader.Text = "ISM 8000 - Stock Item Report (Hierarchical)";
                    lookUpEditLocationUID.Enabled = false;
                    lookUpEditLocationUID.Properties.NullText = "";
                    txtStockCode.Focus();
                }
                LoadCategroyMetaData();
                
            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Stock Hierarchical Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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



            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Stock Hierarchical Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void LoadCategroyMetaData()
        {
            try
            {
                 
                lookUpEditCategory.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMCategory.CategoryCode, 100,"Code"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMCategory.CategoryDesc, 350, "Description")});

                 
                lookUpEditCategory.Properties.DisplayMember = ISMCategory.CategoryCode;
                lookUpEditCategory.Properties.ValueMember = ISMCategory.CategoryCode;
                 
                 
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
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Stock Hierarchical Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            catch 
            {
                MessageBox.Show("System Error. Contact System Administrator", "Stock Hierarchical Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSearcher_Click(object sender, EventArgs e)
        {
            try
            {
                ISM.Forms.FrmStockCodeSearcher zStockCodeSearcher = new ISM.Forms.FrmStockCodeSearcher(m_ISMLoginInfo);
                zStockCodeSearcher.InputStockCode = txtStockCode.Text.Trim();
                zStockCodeSearcher.ShowDialog();
                txtStockCode.Text = zStockCodeSearcher.StockCode;
            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Stock Hierarchical Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private bool Validation()
        {
            bool zResult = false;
            bool zValidationFail = true;

            try
            {
                dxErrorProvider.SetError(lookUpEditLocationUID, null);
                dxErrorProvider.SetError(lookUpEditCategory, null); 
                dxErrorProvider.SetError(txtStockCode, null);
                if (m_ReportType == (int)ISM.ReportType.Location)
                {
                     
                    if (lookUpEditLocationUID.EditValue == null && lookUpEditCategory.EditValue == null)  
                    {
                        dxErrorProvider.SetError(lookUpEditLocationUID, "Select a Location UID or Category");
                        dxErrorProvider.SetError(lookUpEditCategory, "Select a Category or Location UID");
                        lookUpEditLocationUID.Focus();
                        zValidationFail = false;
                    }
                }
                else
                {
                    if (txtStockCode.Text.Trim() == "" && lookUpEditCategory.EditValue == null)
                    {
                        dxErrorProvider.SetError(txtStockCode, "Enter Stock Code or Select a Category");
                        dxErrorProvider.SetError(lookUpEditCategory, "Select a Category or Enter Stock Code");  
                        txtStockCode.Focus();
                        zValidationFail = false;
                    }
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
                    string zLocUID = "";
                    int zMode = 1;
                    DataSet zStockDS = null;
                    DataRow zStockDR = null;
                    Cursor.Current = Cursors.WaitCursor;
                    if (lookUpEditLocationUID.EditValue != null)
                    {
                        zLocUID = lookUpEditLocationUID.EditValue.ToString();
                    }
                    string zCatCode = "";
                    if (lookUpEditCategory.EditValue != null)  
                    {
                        zCatCode = lookUpEditCategory.EditValue.ToString();
                    }
 
                    if (m_ReportType == (int)ISM.ReportType.Item)
                    {
                        zStockDS = m_ISMLoginInfo.ISMServer.GetRptStockItemHierarchicalData(txtStockCode.Text.Trim(), zCatCode);

                         
                        if (zStockDS.Tables[0].Rows.Count <= 0)
                        {
                            MessageBox.Show("Search criteria netted no results", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            return;
                        }


                        RptItemHierarchical zRptItemHierarchical = new RptItemHierarchical();

                        zRptItemHierarchical.DataSource = zStockDS;
                        zRptItemHierarchical.DataMember = zStockDS.Tables[0].TableName;

                        zRptItemHierarchical.lblUserName.Text = m_ISMLoginInfo.LogonID;
                        zRptItemHierarchical.lblStockCodeHeader.Text = "Stock Search for Stock Code : " + txtStockCode.Text.Trim();
                         
                        if (zCatCode.Trim() != "")
                            zRptItemHierarchical.lblSelCategoryCode.Text = "Category : " + zCatCode;
                        else
                            zRptItemHierarchical.lblSelCategoryCode.Text = "";

                         
                        zRptItemHierarchical.GroupHeader1.Controls.Add(zRptItemHierarchical.lblLocDesc);
                        zRptItemHierarchical.GroupHeader1.KeepTogether = true;

                        GroupField GFLabelLoc = new GroupField("Label_Descrip");
                        zRptItemHierarchical.GroupHeader1.GroupFields.Add(GFLabelLoc);

                         
                        zRptItemHierarchical.lblLocDesc.DataBindings.Add("Text", zStockDS, "Label_Descrip");
                        zRptItemHierarchical.lblStockCode.DataBindings.Add("Text", zStockDS, "StockCode");
                        zRptItemHierarchical.lblShortDesc.DataBindings.Add("Text", zStockDS, "ShortName");
                        zRptItemHierarchical.lblSOH.DataBindings.Add("Text", zStockDS, "SOH", "{0:#,#}");
                        zRptItemHierarchical.lblSerNo.DataBindings.Add("Text", zStockDS, "SerialNo");
                        zRptItemHierarchical.lblBatchNo.DataBindings.Add("Text", zStockDS, "BatchLotNo");
                        zRptItemHierarchical.lblCategory.DataBindings.Add("Text", zStockDS, "CATCODE");
                         
                        string zJnlDesc = "Stock Item Report (Hierarchical) Generated";
                        
                        m_ISMLoginInfo.AddToJournal("T", zJnlDesc, "RPT001", "0", "0", "0", "0", txtStockCode.Text.Trim(), zCatCode);  


                         
                        Cursor.Current = Cursors.Default;
                        zRptItemHierarchical.ShowPreviewDialog();
                    }
                    else if (m_ReportType == (int)ISM.ReportType.Location)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        zStockDS = m_ISMLoginInfo.ISMServer.GetRptStockLocationHierarchicalData(zMode, zLocUID, zCatCode);

                        RptLocationHierarchical zRptLocationHierarchical = new RptLocationHierarchical();

                        zRptLocationHierarchical.DataSource = zStockDS;
                        zRptLocationHierarchical.DataMember = zStockDS.Tables[0].TableName;

                         
                        if (zStockDS.Tables[1].Rows.Count <= 0)
                        {
                            MessageBox.Show("Search criteria netted no results", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        if(zStockDS.Tables[1].Rows.Count > 0)
                            zStockDR = zStockDS.Tables[1].Rows[0];  

                        zRptLocationHierarchical.lblUserName.Text = m_ISMLoginInfo.LogonID;
                        if (zStockDS.Tables[1].Rows.Count > 0)
                            zRptLocationHierarchical.lblRptRecCount.Text = "No of Stock Code : " + zStockDS.Tables[1].Rows.Count;
                        else
                            zRptLocationHierarchical.lblRptRecCount.Text = "No of Stock Code : 0";

                        if (zStockDS.Tables[2].Rows.Count > 0)
                            zStockDR = zStockDS.Tables[2].Rows[0];  

                        zRptLocationHierarchical.lblLocUID.Text = "Stock Search Location " + zLocUID;

                        if (zStockDS.Tables[2].Rows.Count > 0)
                        {

                            zRptLocationHierarchical.lblFixedLocDesc.Text = "Fixed Location Description :" + zStockDR["USER_DESCRIPTION"].ToString();
                            zRptLocationHierarchical.lblLocParUID.Text = "Fixed Location UID : " + zStockDR["UIDStr"].ToString();
                             
                            if (zCatCode.Trim() != "")
                                zRptLocationHierarchical.lblSelCategoryCode.Text = "Category : " + zCatCode;
                            else
                                zRptLocationHierarchical.lblSelCategoryCode.Text = "";

                        }
                        else
                        {
                            zRptLocationHierarchical.lblLocDesc.Text = "Fixed Location Description :";
                            zRptLocationHierarchical.lblLocParUID.Text = "Fixed Location UID : ";
                            zRptLocationHierarchical.lblSelCategoryCode.Text = ""; 

                        }
                        zRptLocationHierarchical.lblLocUID.Text = "Stock Search for Location : <" + lookUpEditLocationUID.Text.Trim() + ">";


                         
                        zRptLocationHierarchical.GroupHeader1.Controls.Add(zRptLocationHierarchical.lblLocDesc);
                        zRptLocationHierarchical.GroupHeader1.KeepTogether = true;

                        GroupField GFLabelLoc = new GroupField("Label_Loc");
                        zRptLocationHierarchical.GroupHeader1.GroupFields.Add(GFLabelLoc);

                         
                        GroupField GFLabelStockCode = new GroupField();
                        GFLabelStockCode.FieldName = "FK_CATALOGUED_STOCK_CODE";
                        GFLabelStockCode.SortOrder = XRColumnSortOrder.Ascending;
                         
                        zRptLocationHierarchical.Detail.SortFields.Add(GFLabelStockCode);

                         
                        zRptLocationHierarchical.lblLocDesc.DataBindings.Add("Text", zStockDS, "Label_Loc");
                        zRptLocationHierarchical.lblStockCode.DataBindings.Add("Text", zStockDS, "FK_CATALOGUED_STOCK_CODE");
                        zRptLocationHierarchical.lblShortDesc.DataBindings.Add("Text", zStockDS, "SHORT_NAME");
                        zRptLocationHierarchical.lblSOH.DataBindings.Add("Text", zStockDS, "SOH", "{0:#,#}");
                        zRptLocationHierarchical.lblSerNo.DataBindings.Add("Text", zStockDS, "SERIAL_EQUIP_NO");
                        zRptLocationHierarchical.lblBatchNo.DataBindings.Add("Text", zStockDS, "BATCH_LOT_NO");
                        zRptLocationHierarchical.lblCategory.DataBindings.Add("Text", zStockDS, "CATCODE"); 
                         
                        string zJnlDesc = "Stock Location Report (Hierarchical) Generated";
                         
                        m_ISMLoginInfo.AddToJournal("T", zJnlDesc, "RPT001", "", lookUpEditLocationUID.Text.Trim(), "0", "0", "", zCatCode);  
                         
                        Cursor.Current = Cursors.Default;
                        zRptLocationHierarchical.ShowPreviewDialog();

                    }


                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Cursor.Current = Cursors.Default;
                MessageBox.Show("System Error. Contact System Administrator", "Stock Hierarchical Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                dxErrorProvider.SetError(lookUpEditLocationUID, null);
                dxErrorProvider.SetError(txtStockCode, null);
                txtStockCode.Text = "";
                lookUpEditLocationUID.EditValue = null;
                if (m_ReportType == (int)ISM.ReportType.Location)
                    lookUpEditLocationUID.Focus();
                else
                    txtStockCode.Focus();
                lookUpEditCategory.EditValue = null;  

            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Stock Hierarchical Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void lookUpEditLocationUID_EditValueChanged(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(lookUpEditLocationUID, null);
            dxErrorProvider.SetError(lookUpEditCategory, null);  
        }

        private void txtStockCode_TextChanged(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(txtStockCode, null);
            dxErrorProvider.SetError(lookUpEditCategory, null);  
        }

        private void lookUpEditCategory_EditValueChanged(object sender, EventArgs e)  
        {
            dxErrorProvider.SetError(lookUpEditLocationUID, null);
            dxErrorProvider.SetError(txtStockCode, null);
            dxErrorProvider.SetError(lookUpEditCategory, null); 
           
        }
    }
}
