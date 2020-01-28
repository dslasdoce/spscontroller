 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ISMDAL.TableColumnName;

namespace ISM.Modules
{
    public partial class StockManage : ISMBaseWorkSpace
    {
        public StockManage(ISMLoginInfo AISMLoginInfo)
            : base(AISMLoginInfo)
        {
            InitializeComponent();
        }

        private void StockManage_Load(object sender, EventArgs e)
        {
            try
            {
                
                SetLookUpEditCaption();
                btnTaskClear.PerformClick();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void SetLookUpEditCaption()
        {
            try
            {

                lookUpEditItemUID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ITEM_UID", 90, "Item UID"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SOH", 80,"SOH"),  
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMCategory.CategoryCode, 140,"Category")  
                 
                });

                lookUpEditStockCode.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMStock.StockCatalogueCode, 90, "Stock Code"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMStockCatalogue.StockShortName, 100,"Stock Short Name")});

                lookUpEditCategory.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMCategory.CategoryCode, 100,"Code"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMCategory.CategoryDesc, 350, "Description")});

                lookUpEditLabelType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLabelType.LabelTypeID, "ID",10,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLabelType.LabelTypeCode, 90,"Label Type"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLabelType.LabelTypeDesc, 120, "Description")});

                lookUpEditItemUID.Properties.DisplayMember = "ITEM_UID";  
                lookUpEditItemUID.Properties.ValueMember = ISMStock.StockItemUID;

                lookUpEditCategory.Properties.DisplayMember = ISMCategory.CategoryCode;
                lookUpEditCategory.Properties.ValueMember = ISMCategory.CategoryCode;

                lookUpEditLabelType.Properties.DisplayMember = ISMLabelType.LabelTypeDesc;
                lookUpEditLabelType.Properties.ValueMember = ISMLabelType.LabelTypeID;

                lookUpEditStockCode.Properties.DisplayMember = ISMStock.StockCatalogueCode;
                lookUpEditStockCode.Properties.ValueMember = ISMStock.StockCatalogueCode;

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Manage Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void LoadStockIssueMetaData()
        {
            DataSet ds = null;
            try
            {

                ds = m_ISMLoginInfo.ISMServer.GetStockIssueMetaData();
                if (ds != null)
                {
                    lookUpEditItemUID.Properties.DataSource = ds.Tables[ISMStock.TableName].DefaultView;
                    lookUpEditStockCode.Properties.DataSource = ds.Tables[ISMStockCatalogue.TableName].DefaultView;
                }

                ds = m_ISMLoginInfo.ISMServer.GetReciveItemUIDAndLocationUID();
                if (ds != null)
                {
                    lookUpEditLabelType.Properties.DataSource = ds.Tables[0].DefaultView; 
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
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Manage Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
       

        private void lookUpEditItemUID_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtStatusMsg.Text = "";
                dxErrorProvider.SetError(lookUpEditItemUID, null);
                dxErrorProvider.SetError(lookUpEditStockCode, null);
                if (lookUpEditItemUID.EditValue != null)
                {
                    DataSet ds = null;
                    DataRow dr = null;
                    long zItemCode = long.Parse(lookUpEditItemUID.EditValue.ToString());
                    byte nMode = 0;
                    ds = m_ISMLoginInfo.ISMServer.GetStockCodeAndLocationUIDForItemUID(zItemCode);
                    if (ds != null)
                    {
                         
                        lookUpEditStockCode.EditValueChanged -= new System.EventHandler(lookUpEditStockCode_EditValueChanged);
                        lookUpEditStockCode.EditValue = null;
                        lookUpEditStockCode.Properties.ForceInitialize();
                        lookUpEditStockCode.Properties.DataSource = ds.Tables[0].DefaultView;
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dr = ds.Tables[0].Rows[0];
                            lookUpEditStockCode.EditValue = lookUpEditStockCode.Properties.GetKeyValueByDisplayText(dr[ISMStock.StockCatalogueCode].ToString());
                        }
                        lookUpEditStockCode.EditValueChanged += new System.EventHandler(lookUpEditStockCode_EditValueChanged);

                    }
                    DispalyData(lookUpEditItemUID.EditValue.ToString());
                   
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Manage Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lookUpEditStockCode_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtStatusMsg.Text = "";
                dxErrorProvider.SetError(lookUpEditStockCode, null);

                if (lookUpEditStockCode.EditValue == null)
                    return;
                byte nMode = 2;
                if (lookUpEditItemUID.EditValue == null && lookUpEditStockCode.EditValue != null)
                {
                    DataSet ds = null;
                    DataRow dr = null;
                    ds = m_ISMLoginInfo.ISMServer.GetIssueLocationAndItemUIDForStockCode(lookUpEditStockCode.EditValue.ToString().Trim());
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dr = ds.Tables[0].Rows[0];
                            lookUpEditItemUID.EditValue = null;
                            lookUpEditItemUID.Properties.DataSource = ds.Tables[0].DefaultView;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Manage Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void lookUpEditLabelType_EditValueChanged(object sender, EventArgs e)
        {
            txtStatusMsg.Text = "";
            dxErrorProvider.SetError(lookUpEditCategory, null);
            dxErrorProvider.SetError(lookUpEditLabelType, null);

        }
        private void DispalyData( string AItemUID)
        {
            try
            {
                DataSet ds = null;
                DataRow dr = null;
                string zVolumetric = "";
                txtStatusMsg.EditValue = "";
               
                string StockTrackingIndicator = "";
                ds = m_ISMLoginInfo.ISMServer.GetStockManageDisplayData(AItemUID);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dr = ds.Tables[0].Rows[0];
                        lblShortName.Text = dr[ISMStockCatalogue.StockShortName].ToString();
                        lblUnitOfIssue.Text = dr[ISMStockCatalogue.StockUnitOfIssue].ToString();
                        StockTrackingIndicator = dr[ISMStockCatalogue.StockTrakingInd].ToString();
                        if (StockTrackingIndicator == "E" || StockTrackingIndicator == "S")
                            lblSerialAndEquipNo.Text = dr[ISMStock.StockSerialEquipNo].ToString();
                        else
                            lblSerialAndEquipNo.Text = "";
                        if (dr[ISMStockCatalogue.StockBatchLotMgtInd].ToString().ToUpper() == "Y")
                            lblBatchAndLotNo.Text = dr[ISMStock.StockBatchLotNo].ToString();
                        else
                            lblBatchAndLotNo.Text = "";
                        lblSealCode.Text = dr[ISMLocation.SealCode].ToString();
                         
                        if (lblSealCode.Text.Trim() != "" && (lblSealCode.Text.Substring(0, 1) == "1" || lblSealCode.Text.Substring(0, 1) == "2"))
                            lblSealUID.Text = dr[ISMStock.SealUID].ToString();
                        else
                            lblSealUID.Text = "";
                        lblTrackableInd.Text = dr[ISMStockCatalogue.StockTrakingInd].ToString();
                        lblNewOnHandQty.Text = dr[ISMStock.StockQtyAtLoc].ToString();
                        lblRepairFlag.Text = dr[ISMStockCatalogue.StockRepairFlag].ToString();
                        if (dr[ISMStockCatalogue.StockHeight].ToString() != "")
                            zVolumetric = "H: " + dr[ISMStockCatalogue.StockHeight].ToString() + " X ";
                        if (dr[ISMStockCatalogue.StockWeight].ToString() != "")
                            zVolumetric = zVolumetric + "W: " + dr[ISMStockCatalogue.StockWeight].ToString() + " X ";
                        if (dr[ISMStockCatalogue.StockLength].ToString() != "")
                            zVolumetric = zVolumetric + "L: " + dr[ISMStockCatalogue.StockLength].ToString();
                        lblVolumetric1.Text = zVolumetric;
                        if (dr[ISMStockCatalogue.StockWidth].ToString() != "")
                            zVolumetric = "X WD:" + dr[ISMStockCatalogue.StockWidth].ToString() + " X ";
                        if (dr[ISMStockCatalogue.StockVolume].ToString() != "")
                            zVolumetric = zVolumetric + "V: " + dr[ISMStockCatalogue.StockVolume].ToString();
                        lblVolumetric2.Text = zVolumetric;

                        if (dr["INVENTORYCATEGORY_CODE"].ToString() != "")  
                        {
                            lookUpEditCategory.EditValueChanged -= new System.EventHandler(lookUpEditCategory_EditValueChanged);  
                            lookUpEditCategory.EditValue = lookUpEditCategory.Properties.GetKeyValueByDisplayText(dr["INVENTORYCATEGORY_CODE"].ToString());
                            lookUpEditCategory.EditValueChanged += new System.EventHandler(lookUpEditCategory_EditValueChanged); 
                            lblCategory.Text = dr["INVENTORYCATEGORY_CODE"].ToString();
                        }
                        lblLocationUID.Text = dr["FK_LOCATION_UID"].ToString();
                        lblSOH.Text = dr["QTY_OH_AT_LOC"].ToString();
                        lblLabelType.Text = dr["LabelTypeDesc"].ToString();
                        lookUpEditLabelType.EditValueChanged -= new System.EventHandler(lookUpEditLabelType_EditValueChanged);  
                        lookUpEditLabelType.EditValue = lookUpEditLabelType.Properties.GetKeyValueByDisplayText(dr["LabelTypeDesc"].ToString());
                        lookUpEditLabelType.EditValueChanged += new System.EventHandler(lookUpEditLabelType_EditValueChanged);  

                    }
                    else
                    {
                        lblShortName.Text = "";
                        lblUnitOfIssue.Text = "";
                        lblSerialAndEquipNo.Text = "";
                        lblBatchAndLotNo.Text = "";
                        lblSealCode.Text = "";
                        lblTrackableInd.Text = "";
                        lblNewOnHandQty.Text = "";
                        lblRepairFlag.Text = "";
                        lblVolumetric1.Text = "";
                        lblVolumetric2.Text = "";
                        lblSealUID.Text = "";
                        lblCategory.Text = "";
                        lblLabelType.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Manage Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void lookUpEditCategory_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dxErrorProvider.SetError(lookUpEditCategory, null);
                dxErrorProvider.SetError(lookUpEditLabelType, null);
                txtStatusMsg.Text = "";
                if (lookUpEditCategory.EditValue != null)
                {
                    if (lookUpEditCategory.EditValue.ToString() == "-----")
                        dxErrorProvider.SetError(lookUpEditCategory, "Invalid Category");
                    lookUpEditLabelType.Focus();  
                }
                 
                 
                 
                 
                 
                 
                 
                 
                 
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Manage Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void ClearControls()
        {
            try
            {
                lookUpEditStockCode.EditValue = null;
                lookUpEditItemUID.EditValue = null;
                lblLocationUID.Text = "";
                lblSOH.Text = "";
                lookUpEditCategory.EditValue = null;
                lookUpEditLabelType.EditValue = null;
                lblShortName.Text = "";
                lblUnitOfIssue.Text = "";
                lblSerialAndEquipNo.Text = "";
                lblBatchAndLotNo.Text = "";
                lblSealCode.Text = "";
                lblTrackableInd.Text = "";
                lblNewOnHandQty.Text = "";
                lblRepairFlag.Text = "";
                lblVolumetric1.Text = "";
                lblVolumetric2.Text = "";
                lblSealUID.Text = "";
                lblCategory.Text = "";
                lblLabelType.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Manage Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
        private void btnTaskClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearErrorIconText();
                ClearControls();
                LoadStockIssueMetaData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Manage Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void ClearErrorIconText()
        {
            try
            {
                dxErrorProvider.SetError(lookUpEditStockCode, null);
                dxErrorProvider.SetError(lookUpEditItemUID, null);
                dxErrorProvider.SetError(lookUpEditCategory, null);
                dxErrorProvider.SetError(lookUpEditLabelType, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Manage Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool Validation()
        {
            bool zResult = false;
            bool zValidationFail = true;

            try
            {
                ClearErrorIconText();
                if (lookUpEditLabelType.EditValue == null && lookUpEditCategory.EditValue == null)
                {
                    dxErrorProvider.SetError(lookUpEditLabelType, "Select a Label Type or Select a Category");
                    dxErrorProvider.SetError(lookUpEditCategory, "Select a Category or Select a Label Type");
                    lookUpEditLabelType.Focus();
                    zValidationFail = false;
                }
                
                if (lookUpEditItemUID.EditValue == null)
                {
                    dxErrorProvider.SetError(lookUpEditItemUID, "Select a Item UID");
                    lookUpEditLabelType.Focus();
                    zValidationFail = false;
                }
                if (lookUpEditStockCode.EditValue == null)
                {
                    dxErrorProvider.SetError(lookUpEditStockCode, "Select a Stock Code");
                    lookUpEditLabelType.Focus();
                    zValidationFail = false;
                }
                if (lookUpEditCategory.EditValue != null)
                {
                    if (lookUpEditLabelType.Text.Trim() == lblLabelType.Text.Trim() && lookUpEditCategory.EditValue.ToString().Trim() == lblCategory.Text.Trim())
                    {
                        dxErrorProvider.SetError(lookUpEditCategory, "No difference between existing Label Type and Category with New Label Type and Category");
                        dxErrorProvider.SetError(lookUpEditLabelType, "No difference between existing Label Type and Category with New Label Type and Category");
                        lookUpEditCategory.Focus();
                        zValidationFail = false;
                    }
                }
               
                if (lookUpEditCategory.EditValue == null)
                {
                    if (lookUpEditLabelType.EditValue != null)
                    {
                        if (lookUpEditLabelType.Text.Trim() == lblLabelType.Text.Trim())
                        {
                            dxErrorProvider.SetError(lookUpEditLabelType, "No difference between existing Label Type with New Label Type");
                            lookUpEditLabelType.Focus();
                            zValidationFail = false;

                        }
                    }
                }
                if (lookUpEditLabelType.EditValue == null)
                {
                    if (lookUpEditCategory.EditValue != null)
                    {
                        if (lookUpEditCategory.Text.Trim() == lblCategory.Text.Trim())
                        {
                            dxErrorProvider.SetError(lookUpEditCategory, "No difference between existing Category Type with New Category");
                            lblCategory.Focus();
                            zValidationFail = false;
                        }
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
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Manage Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return zResult;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    string zStockCode;
                    string zItemUID;
                    string zLabelID;
                    string zCategory;
                    string zLocUID;
                    string zLabelType = "";


                    int zMode = 0;
                    string zConMessage = "";
                    string zMessage = "";

                    zStockCode = lookUpEditStockCode.EditValue.ToString().Trim();
                    zItemUID = lookUpEditItemUID.EditValue.ToString().Trim();

                    if (lookUpEditLabelType.EditValue != null)
                    {
                        zLabelID = lookUpEditLabelType.EditValue.ToString().Trim();
                        zLabelType = lookUpEditLabelType.Text.Trim();
                    }
                    else
                    {
                        zLabelID = "0";
                        zLabelType = lblLabelType.Text.Trim();
                    }

                    if (lookUpEditCategory.EditValue != null)
                        zCategory = lookUpEditCategory.EditValue.ToString().Trim();
                    else
                        zCategory = lblCategory.Text.Trim();

                    if (lblLocationUID.Text.Trim() != "")
                        zLocUID = lblLocationUID.Text.Substring(1);
                    else
                        zLocUID = "0";  


                    if (zLabelType != lblLabelType.Text.Trim() && zCategory != lblCategory.Text.Trim())
                    {
                        zMode = 0;  
                        zConMessage = string.Format("Category - {0} and Label Type - {1} has been Updated for the Item UID {2} and Stock Code {3}",
                                                 zCategory,
                                                 zLabelType,
                                                 lookUpEditItemUID.Text.Trim(),
                                                 lookUpEditStockCode.EditValue.ToString());
                        zMessage = String.Format("Do you want to Update Category - {0} and Label Type - {1} for the Item UID {2} and Stock Code {3} ?",
                                                        zCategory,
                                                        zLabelType,
                                                        lookUpEditItemUID.Text.Trim(),
                                                        lookUpEditStockCode.EditValue.ToString());


                    }
                    else if (zLabelType == lblLabelType.Text.Trim() && zCategory != lblCategory.Text.Trim())
                    {
                        zMode = 1;  
                        zConMessage = string.Format("Category - {0} has been Updated for the Item UID {1} and Stock Code {2}",
                                                 zCategory,
                                                 lookUpEditItemUID.Text.Trim(),
                                                 lookUpEditStockCode.EditValue.ToString());
                        zMessage = String.Format("Do you want to Update Category - {0} for the Item UID {1} and Stock Code {2} ?",
                                zCategory,
                                lookUpEditItemUID.Text.Trim(),
                                lookUpEditStockCode.EditValue.ToString());


                    }
                    else if (zLabelType != lblLabelType.Text.Trim() && zCategory == lblCategory.Text.Trim())
                    {
                        zMode = 2;  
                        zConMessage = string.Format("Label Type - {0} has been Updated for the Item UID {1} and Stock Code {2}",
                                                 zLabelType,
                                                 lookUpEditItemUID.Text.Trim(),
                                                 lookUpEditStockCode.EditValue.ToString());
                        zMessage = String.Format("Do you want to Update Label Type - {0} for the Item UID {1} and Stock Code {2} ?",
                                zLabelType,
                                lookUpEditItemUID.Text.Trim(),
                                lookUpEditStockCode.EditValue.ToString());

                    }


                    DialogResult zReply = MessageBox.Show(zMessage, "Manage Stock", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                   if (zReply == DialogResult.Yes)
                   {
                      

                      

                       if (m_ISMLoginInfo.ISMServer.UpdateStockManageData(zMode, zItemUID, zLocUID, zStockCode,zLabelID,zCategory))
                       {

                           btnTaskClear.PerformClick();
                           txtStatusMsg.Text = zConMessage;
                       }

                   }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

       


    }
}
