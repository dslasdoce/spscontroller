 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

using System;
using System.Data;
using System.Windows.Forms;
using ISMDAL.TableColumnName;
using System.Drawing;
using ISM.Forms;

namespace ISM.Modules
{
  public partial class StockIssueCreateTask : ISMBaseWorkSpace
  {
    #region "Private Variable Declaration"

    private string m_StockTrackingInd = "";
    private string m_OperatorID;
    private bool m_ClearItemUIDErrIcon = true;

     
     
     
    private enum CoALoginStauts : int { ValidLogin = 0, Exception = 1, Cancel = 2, ThreeTimesAttempt = 3 };

    # endregion

    #region "Property"
    public string StockTrackingIndicator
    {
      get { return m_StockTrackingInd; }
      set { m_StockTrackingInd = value; }
    }
    #endregion

    public StockIssueCreateTask(ISMLoginInfo AISMLoginInfo)
    {
      InitializeComponent();
      m_ISMLoginInfo = AISMLoginInfo;
    }

    private void StockIssueCreateTask_Load(object sender, EventArgs e)
    {
      SetLookUpEditCaption();     
    
       
       
      gcStockData.LookAndFeel.SetSkinStyle(m_ISMLoginInfo.Params.AppSkinColour);  
      
       
       
      txtQuantity.Properties.Mask.EditMask = "##########;"; 
      txtQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
      txtQuantity.Properties.Mask.UseMaskAsDisplayFormat = true;
       
      btnClear.PerformClick();   
      lookUpEditOperatorID.Focus();
    }

    private void SetLookUpEditCaption()
    {
      try
      {
        lookUpEditOperatorID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLogonID, 120, "User ID"),  
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserFirstName, 150,"First Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLastName, 150,"Last Name")});

        lookUpEditItemUID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ITEM_UID", 90, "Item UID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SOH", 80,"SOH"),  
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMCategory.CategoryCode, 140,"Category")  
            
            });
         
        lookUpEditLocationUID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LOCATION_UID", 90, "Location UID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.LocationCode, 120,"Location Code"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPDIST, 80, "ERP DIST"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPWHS, 80, "ERP WHS"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPGRID, 80, "ERP GRID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPBIN, 80, "ERP BIN")});
         
        lookUpEditStockCode.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMStock.StockCatalogueCode, 90, "Stock Code"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMStockCatalogue.StockShortName, 100,"Stock Short Name")});

         
        lookUpEditOperatorID.Properties.DisplayMember = ISMUser.UserLogonID;
        lookUpEditOperatorID.Properties.ValueMember = ISMUser.UserID;

        lookUpEditItemUID.Properties.DisplayMember = "ITEM_UID";  
        lookUpEditItemUID.Properties.ValueMember = ISMStock.StockItemUID;

        lookUpEditStockCode.Properties.DisplayMember = ISMStock.StockCatalogueCode;
        lookUpEditStockCode.Properties.ValueMember = ISMStock.StockCatalogueCode;

        lookUpEditLocationUID.Properties.DisplayMember = "LOCATION_UID";  
        lookUpEditLocationUID.Properties.ValueMember = ISMLocation.LocationUID;
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void LoadStockIssueMetaData()
    {
      DataSet ds = null;
      try
      {
          int zMod = 2;  
          ds = m_ISMLoginInfo.ISMServer.GetISMOperatorID(zMod, m_ISMLoginInfo.UserProfileCode, m_ISMLoginInfo.UserID);  
          if (ds != null)
          {
              lookUpEditOperatorID.Properties.DataSource = ds.Tables[0].DefaultView;
               
              lookUpEditOperatorID.EditValue = lookUpEditOperatorID.Properties.GetKeyValueByDisplayText(m_ISMLoginInfo.LogonID);
          }

        ds = m_ISMLoginInfo.ISMServer.GetStockIssueMetaData();
        if (ds != null)
        {
           
          lookUpEditItemUID.Properties.DataSource = ds.Tables[ISMStock.TableName].DefaultView;
          lookUpEditLocationUID.Properties.DataSource = ds.Tables[ISMLocation.TableName].DefaultView;
          lookUpEditStockCode.Properties.DataSource = ds.Tables[ISMStockCatalogue.TableName].DefaultView;
           
             
        }

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void lookUpEditOperatorID_EditValueChanged(object sender, EventArgs e)
    {
      lblIssueBy.Text = lookUpEditOperatorID.Text;
      if (m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text)
      {
        gcCreateTask.Text = "Stock Issue";
        btnSave.Text = "Issue";
      }
      else
      {
        gcCreateTask.Text = "Create Task";
        btnSave.Text = "Create";
      }
      lookUpEditOperatorID.Properties.ForceInitialize();
      object oValue = lookUpEditOperatorID.Properties.GetDataSourceRowByKeyValue(lookUpEditOperatorID.EditValue);
      if (oValue != null)
      {
        DataRow dr = ((DataRowView)oValue).Row;
        if (dr != null)
          m_OperatorID = dr[ISMUser.UserID].ToString();
      }

    }
    private void lookUpEditItemUID_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
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

               
               
               
               
               
               
               
               
               
               
               
               
               

               
               
               
               
               
               
               
               
               
              lookUpEditLocationUID.EditValueChanged -= new System.EventHandler(lookUpEditLocationUID_EditValueChanged);
              lookUpEditLocationUID.EditValue = null;
              lookUpEditLocationUID.Properties.ForceInitialize();
              lookUpEditLocationUID.Properties.DataSource = ds.Tables[1].DefaultView;
              if (ds.Tables[1].Rows.Count > 0)
              {
                  dr = ds.Tables[1].Rows[0];
                  lookUpEditLocationUID.EditValue = lookUpEditLocationUID.Properties.GetKeyValueByDisplayText(dr["LOCATION_UID"].ToString());
              }
               
              object oValue = lookUpEditLocationUID.Properties.GetDataSourceRowByKeyValue(lookUpEditLocationUID.EditValue);
              if (oValue != null)
              {
                  dr = ((DataRowView)oValue).Row;
                  if (dr != null)
                  {
                      lblERPDistrict.Text = dr[ISMLocation.ERPDIST].ToString();
                      lblERPWHS.Text = dr[ISMLocation.ERPWHS].ToString();
                      lblERPGrid.Text = dr[ISMLocation.ERPGRID].ToString();
                      lblERPBin.Text = dr[ISMLocation.ERPBIN].ToString();
                  }
              }
               

              lookUpEditLocationUID.EditValueChanged -= new System.EventHandler(lookUpEditLocationUID_EditValueChanged);
          }
          DispalyData(nMode, lookUpEditItemUID.EditValue.ToString());
          }
        }

      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void lookUpEditLocationUID_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
           
         
         
         

         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
      }
      catch(Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }
    private void lookUpEditStockCode_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        if (lookUpEditStockCode.EditValue == null)
          return;
        byte nMode = 2;
        lookUpEditLocationUID.EditValue = null;  
        if (lookUpEditItemUID.EditValue == null &&  lookUpEditStockCode.EditValue != null)
        {
          DataSet ds = null;
          DataRow dr = null;
          ds = m_ISMLoginInfo.ISMServer.GetIssueLocationAndItemUIDForStockCode(lookUpEditStockCode.EditValue.ToString().Trim());
          if (ds != null)
          {
            lookUpEditLocationUID.EditValue = null;
            lookUpEditLocationUID.Properties.DataSource = ds.Tables[1].DefaultView;
            dr = ds.Tables[0].Rows[0];
            lookUpEditItemUID.EditValue = null;
             
            lookUpEditItemUID.Properties.DataSource = ds.Tables[0].DefaultView;
             
             
          }
        }
         
        

      }
      catch(Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void DispalyData(byte AMode, string ADataWhere)
    {
      try
      {
         
        DataSet ds = null;
        DataRow dr = null;
        string zVolumetric = "";
        txtStatusMsg.EditValue = "";  
        ClearErrorIconText();
        ds = m_ISMLoginInfo.ISMServer.GetStockIssueDisplayData(AMode, ADataWhere);
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
                lblCategory.Text = dr["INVENTORYCATEGORY_CODE"].ToString();

            
             
             
             
             
             
             
             
          }
          else
          {
            lblShortName.Text ="";
            lblUnitOfIssue.Text ="";
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
             
             
             
             
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

      }
    }
    private void dtIssueDate_EditValueChanged(object sender, EventArgs e)
    {
      dxErrorProvider.SetError(dtIssueDate, null);
      txtStatusMsg.EditValue = "";  
    }

     
     
     
     

    private void txtQuantity_EditValueChanged(object sender, EventArgs e)
    {
      dxErrorProvider.SetError(txtQuantity, null);
      txtStatusMsg.EditValue = "";  
    }
    #region "Validataion"
    private void ClearErrorIconText()
    {
      try
      {
        dxErrorProvider.SetError(lookUpEditOperatorID, null);
        if(m_ClearItemUIDErrIcon)  
            dxErrorProvider.SetError(lookUpEditItemUID, null);
        dxErrorProvider.SetError(lookUpEditLocationUID, null);
        dxErrorProvider.SetError(lookUpEditStockCode, null);
         
        dxErrorProvider.SetError(dtIssueDate, null);
        dxErrorProvider.SetError(txtQuantity, null);
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private bool Validation()
    {
      bool zResult = false;
      bool zValidationFail = true;

      try
      {
        ClearErrorIconText();
        if (txtQuantity.Text.Trim() == "" )
        {
          dxErrorProvider.SetError(txtQuantity, "Enter Quantity");
          txtQuantity.Focus();
          zValidationFail = false;
        }
        else if(float.Parse(txtQuantity.Text) <= 0)
        {
          dxErrorProvider.SetError(txtQuantity, "Can’t Issue Zero Qunaity");
          txtQuantity.Focus();
          zValidationFail = false;

        }
        if (lblNewOnHandQty.Text.Trim() != "")
        {
          if (txtQuantity.Text.Trim() != "" && (float.Parse(txtQuantity.Text) > float.Parse(lblNewOnHandQty.Text)))
          {
            dxErrorProvider.SetError(txtQuantity, "Issue Quantity can’t be more than Stock Quantity");
            txtQuantity.Focus();
            zValidationFail = false;
          }
        }
         
         
         
         
         
         
         
         
         
         
         
         
         
         

         
         
         
        if (lookUpEditStockCode.EditValue == null)   
        {
          dxErrorProvider.SetError(lookUpEditStockCode, "Select a Stock Code");
          lookUpEditStockCode.Focus();
          zValidationFail = false;
        }
        if (lookUpEditLocationUID.EditValue == null)  
        {
          dxErrorProvider.SetError(lookUpEditLocationUID, "Select a Location UID");
          lookUpEditLocationUID.Focus();
          zValidationFail = false;
        }
        if (lookUpEditItemUID.EditValue == null)  
        {
          dxErrorProvider.SetError(lookUpEditItemUID, "Select a Item UID");
          lookUpEditItemUID.Focus();
          zValidationFail = false;
        }
        if (dtIssueDate.EditValue == null)
        {
          dxErrorProvider.SetError(dtIssueDate, "Select Issue Date");
          dtIssueDate.Focus();
          zValidationFail = false;
        }
        else if (dtIssueDate.EditValue != null)
        {
          if (m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text)
          {
            if (DateTime.Parse(dtIssueDate.EditValue.ToString()) != DateTime.Today)
            {
              dxErrorProvider.SetError(dtIssueDate, "Enter Current Date");
              dtIssueDate.Focus();
              zValidationFail = false;
            }
          }
          else if (DateTime.Parse(dtIssueDate.EditValue.ToString()) < DateTime.Today)
          {
            dxErrorProvider.SetError(dtIssueDate, "Enter current date or future Date");
            dtIssueDate.Focus();
            zValidationFail = false;
          }
        }
        if (lookUpEditOperatorID.Text.Trim() == "")
        {
          dxErrorProvider.SetError(lookUpEditOperatorID, "Select a Operator ID");
          lookUpEditOperatorID.Focus();
          zValidationFail = false;
        }
        if (zValidationFail)
          zResult = zValidationFail;
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return false;
      }
      return zResult;
    }
    # endregion

    #region "Save Button"
    private void btnSave_Click(object sender, EventArgs e)
    {
      try
      {

        if (Validation())
        {
          string zMessage = "";
          string zConMessage = "";
          if (m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text)
          {
            zMessage = String.Format("Do you want to perform Stock Issue for the Item UID {0} and Quantity {1} ?", lookUpEditItemUID.Text, txtQuantity.Text);
             
            zConMessage = "Stock Item Issued for the Item UID " + lookUpEditItemUID.Text + " and Quantity " + txtQuantity.Text;  
          }
          else
          {
            zMessage = String.Format("Do you want to create Stock Issue Task for the Item UID {0} and Quantity {1} ?", lookUpEditItemUID.Text, txtQuantity.Text);
            
            zConMessage = "Stock Issue Task has been created for the Item UID " + lookUpEditItemUID.Text + " and Quantity " + txtQuantity.Text;  
          }

          DialogResult zReply = MessageBox.Show(zMessage, "Stock Issue", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
          if (zReply == DialogResult.Yes)
          {
            ISMTask.StructTask zTask = new ISMTask.StructTask();
            int nMode;
            int zCoALoginID = 0;  
            long zSealUID = 0;
            string zSealCode = "";
            string zStockItemCode = "";
            string zInvenCatCode = lblCategory.Text.Trim();  
            if (lblSealCode.Text.Trim() == "")
              zSealCode = "0";
            else
              zSealCode = lblSealCode.Text.Trim().Substring(0, 1);
            if (lookUpEditStockCode.EditValue.ToString() == "")
              zStockItemCode = "0";
            else
              zStockItemCode = lookUpEditStockCode.EditValue.ToString();

            if (m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text)
            {
                 
                FrmWitnessLogin zFrmWitnessLogin = new FrmWitnessLogin(m_ISMLoginInfo);
                zFrmWitnessLogin.ShowDialog();
                if (zFrmWitnessLogin.CoALoginResult == (int)CoALoginStauts.ValidLogin || zFrmWitnessLogin.CoALoginResult == (int)CoALoginStauts.Exception)  
                {


                    zTask.Type = "3";
                    zTask.Status = "Completed";
                     
                     
                     
                    nMode = 0;
                    zTask.OperationID = "31";  
                    zCoALoginID = zFrmWitnessLogin.CoALoginUserID;  
                }
                 
                else if (zFrmWitnessLogin.CoALoginResult == (int)CoALoginStauts.ThreeTimesAttempt)
                {
                    m_ISMLoginInfo.AddToJournal("E", "Exceeded Login attempts", "SEC103");
                    return;
                }
                else
                    return;
                 

            }
            else
            {
              zTask.Type = "1";
              zTask.Status = "Assigned";
               
              nMode = 1;
              zTask.OperationID = "5";  
              zCoALoginID = 0;  
            }
            zTask.StockQty = txtQuantity.Text;
            zTask.ItemID = lookUpEditItemUID.EditValue.ToString();
            zTask.SourceID = lookUpEditLocationUID.EditValue.ToString();
            zTask.DestinationID = "0";
            zTask.UserID = m_OperatorID;
            zTask.StatusCode = "1"; 
            zTask.CreateUserID = m_ISMLoginInfo.LogonID;
            if ((lblSealUID.Text.Trim() != "") && (lblSealUID.Text.Trim().Length == 13))
                zSealUID = long.Parse(lblSealUID.Text.Trim().Substring(1, 12));
             
             
            zTask.CreateDateTime = String.Format(dtIssueDate.EditValue.ToString().Substring(0, 10) + " " + DateTime.Now.ToLongTimeString(), m_ISMLoginInfo.Params.DateTimeFormat);  
            if (m_ISMLoginInfo.ISMServer.CreateIssueTask(nMode, zSealCode, zStockItemCode, zTask, zSealUID, zCoALoginID, zInvenCatCode))  
            {
               
               
               
               
               
               
               
               
               
               
               
              btnClear.PerformClick();
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
    # endregion

    #region "Clear Button"
    private void btnClear_Click(object sender, EventArgs e)
    {
      try
      {
        lookUpEditItemUID.EditValue = null;
        lookUpEditLocationUID.EditValue = null;
        lookUpEditStockCode.EditValue = null;
        dtIssueDate.EditValue = null;
        txtQuantity.EditValue = null;
        lblERPDistrict.Text = "";
        lblERPWHS.Text = "";
        lblERPGrid.Text = "";
        lblERPBin.Text = "";
        lblShortName.Text = "";
        lblUnitOfIssue.Text = "";
        lblSerialAndEquipNo.Text = "";
        lblBatchAndLotNo.Text = "";
        lblSealCode.Text = "";
        lblNewOnHandQty.Text = "";
        lblRepairFlag.Text = "";
        lblVolumetric1.Text = "";
        lblVolumetric2.Text = "";  
        lblTrackableInd.Text = "";  
        lblCategory.Text = "";  
        LoadStockIssueMetaData();
        ClearErrorIconText();
         
        btnSave.Enabled = true;
        lblSealUID.Text = ""; 
         
         
         
         
         
        txtStatusMsg.EditValue = "";  
        btnSave.Enabled = true;  
        m_ClearItemUIDErrIcon = true;
        lookUpEditOperatorID.Focus();
        lblCategory.Text = "";  

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    # endregion
    #region "Exception"
    private void btnException_Click(object sender, EventArgs e)
    {
      try
      {
        long zItemUID = 0;
        long zLocationUID = 0;
        long zSealUID = 0;
        string zStockCode = "";
        string zExpMessage = "Exception has been created for ";
         
         
        if (lookUpEditItemUID.EditValue == null && lookUpEditLocationUID.EditValue == null && lookUpEditStockCode.EditValue == null)
        {
          MessageBox.Show("Select at least any one of the key field e.g. Item UID or Location UID or Stock Code", "Stock Issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
        else
        {
            if (lookUpEditItemUID.EditValue != null)
            {
                zItemUID = long.Parse(lookUpEditItemUID.EditValue.ToString());
                zExpMessage = zExpMessage + "Item UID " + lookUpEditItemUID.Text + " ";
            }
            if (lookUpEditLocationUID.EditValue != null)
            {
                zLocationUID = long.Parse(lookUpEditLocationUID.EditValue.ToString());
                zExpMessage = zExpMessage + "Location UID " + lookUpEditLocationUID.Text + " ";
            }
            if (lookUpEditStockCode.EditValue != null)
            {
                zStockCode = lookUpEditStockCode.EditValue.ToString();
                zExpMessage = zExpMessage + "Stock Code " + lookUpEditStockCode.Text;
            }
          if (lblSealUID.Text.Trim() != "")
           zSealUID = long.Parse(lblSealUID.Text.Substring(1,(lblSealUID.Text.Length - 1)));

        }
        ISM.Forms.FrmException frmSTException = new ISM.Forms.FrmException(m_ISMLoginInfo) { LocationUID = zLocationUID,
                                                                                              ItemUID = zItemUID, StockCode = zStockCode, 
                                                                                              SealUID = zSealUID, JournalType = "SIS",
                                                                                              MsgBoxCaption = "Stock Issue",
                                                                                             InvenCatCode = lblCategory.Text
                                                                                            };  
        frmSTException.Text = "Stock Issue Exception";
        frmSTException.ShowDialog();
        
         
        if (frmSTException.SaveResult)
            txtStatusMsg.Text = zExpMessage;
        else
            txtStatusMsg.EditValue = null;
           

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

  }
}
