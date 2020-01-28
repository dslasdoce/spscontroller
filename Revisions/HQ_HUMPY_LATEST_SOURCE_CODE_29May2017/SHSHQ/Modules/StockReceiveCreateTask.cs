 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using ISMDAL.TableColumnName;
using ISM.Forms;

namespace ISM.Modules
{
  public partial class StockReceiveCreateTask : ISMBaseWorkSpace
  {
    #region "Private Variable Declaration"
    private string m_StockTrackingInd = "";
    private string m_BatchLotInd = "";
    private bool m_StockCodeSelectedFromList = false;
    private string m_OperatorID = "";
    private int m_SealCode = 0;
    private bool m_ValidLabelUID = false;  
    private bool m_ValidSerialNo = false;  
     

     
     
     
    private enum CoALoginStauts : int { ValidLogin = 0, Exception = 1, Cancel = 2, ThreeTimesAttempt = 3 };

    # endregion

    public string StockTrackingIndicator
    {
      get { return m_StockTrackingInd; }
      set { m_StockTrackingInd = value; }
    }
    private bool SelectStockCode
    {
      get { return m_StockCodeSelectedFromList; }
      set { m_StockCodeSelectedFromList = value; }
    }

    public StockReceiveCreateTask(ISMLoginInfo AISMLoginInfo)
      : base(AISMLoginInfo)
    {
      InitializeComponent();
    }

    private void StockReceiveCreateTask_Load(object sender, EventArgs e)
    {
      try
      {
         
        SetLookUpEditCaption();
         
         
        gcStockData.LookAndFeel.SetSkinStyle(m_ISMLoginInfo.Params.AppSkinColour);  
         
         

        
        txtQuantity.Properties.Mask.EditMask = "##########;";  
        txtQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
        txtQuantity.Properties.Mask.UseMaskAsDisplayFormat = true;

        txtItemUID.Properties.Mask.EditMask = m_ISMLoginInfo.Params.ItemPrefix + "\\d{0,12}";
        txtItemUID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
        btnClear.PerformClick();
        
        
      }
      catch (Exception ex)
      {
          MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void SetLookUpEditCaption()
    {
      try
      {
        lookUpEditOperatorID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLogonID, 120, "User ID"),  
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserFirstName, 150,"First Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLastName, 150,"Last Name")});

         
         
         
         
         
         

        lookUpEditLocationUID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LOCATION_UID", 90, "Location UID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.LocationCode, 120,"Location Code"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPDIST, 60, "ERP DIST"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPWHS, 60, "ERP WHS"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPGRID, 60, "ERP GRID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPBIN, 60, "ERP BIN")});
        
        lookUpEditLabelType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLabelType.LabelTypeID, "ID",10,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLabelType.LabelTypeCode, 90,"Label Type"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLabelType.LabelTypeDesc, 120, "Description")});
         
        lookUpEditCategory.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMCategory.CategoryCode, 100,"Code"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMCategory.CategoryDesc, 350, "Description")});


         
        lookUpEditOperatorID.Properties.DisplayMember = ISMUser.UserLogonID;
        lookUpEditOperatorID.Properties.ValueMember = ISMUser.UserID;

        lookUpEditLocationUID.Properties.DisplayMember = "LOCATION_UID";  
        lookUpEditLocationUID.Properties.ValueMember = ISMLocation.LocationUID;

        lookUpEditLabelType.Properties.DisplayMember = ISMLabelType.LabelTypeDesc;
        lookUpEditLabelType.Properties.ValueMember = ISMLabelType.LabelTypeID;
         
        lookUpEditCategory.Properties.DisplayMember = ISMCategory.CategoryCode;
        lookUpEditCategory.Properties.ValueMember = ISMCategory.CategoryCode;

      }
      catch
      {
          MessageBox.Show("System Error. Contact System Administrator", "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void LoadStockReceiveMetaData()
    {
      DataSet ds = null;
      try
      {

             
            int zMod = 1;  
            ds = m_ISMLoginInfo.ISMServer.GetISMOperatorID(zMod, m_ISMLoginInfo.UserProfileCode, m_ISMLoginInfo.UserID);  
            if (ds != null)
            {
              lookUpEditOperatorID.Properties.DataSource = ds.Tables[0].DefaultView;
               
              lookUpEditOperatorID.EditValue = lookUpEditOperatorID.Properties.GetKeyValueByDisplayText(m_ISMLoginInfo.LogonID);
            }
            ds = m_ISMLoginInfo.ISMServer.GetReciveItemUIDAndLocationUID();
            if (ds != null)
            {
               
               
               
               
               
               
               
               
               
              lookUpEditLabelType.Properties.DataSource = ds.Tables[0].DefaultView;  
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
           
            lookUpEditCategory.EditValue = "SV";

         
      }
      catch(Exception ex)
      {
          MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void lookUpEditOperatorID_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        lblReceivedBy.Text = lookUpEditOperatorID.Text.Trim();
        dxErrorProvider.SetError(lookUpEditOperatorID, null);
        if (m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text)
        {
          gcCreateTask.Text = "Stock Receive";
          btnSave.Text = "Receive";
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
        EnableDisableControl();
      }
      catch (Exception ex)
      {
          MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
     
    private void txtItemUID_EditValueChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtItemUID.Text.Trim() != "")
            {
                lookUpEditLabelType.Properties.NullText = "Select a Label Type";  
                 
            }
            else
            {
                lookUpEditLabelType.Properties.NullText = "";  
                 
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


    }
    private void txtItemUID_Leave(object sender, EventArgs e)
    {
        try
        {
            if (txtItemUID.Text.Trim() != "")
            {
                dxErrorProvider.SetError(txtItemUID, null);
                m_ValidLabelUID = false;
                if (txtItemUID.Text.Length != 13)
                {
                    dxErrorProvider.SetError(txtItemUID, "Enter 13 digit Label UID");
                    return;
                }
                if (txtItemUID.Text.Trim() == m_ISMLoginInfo.Params.ItemPrefix.PadRight(13, '0'))
                {
                    dxErrorProvider.SetError(txtItemUID, "Invalid Label UID");
                    return;
                }
                int zMode = 2; 
                long zItemUID = long.Parse(txtItemUID.Text.Substring(1, 12));
                long zLocUID = 0;
                if (m_ISMLoginInfo.ISMServer.PDTStockReceiveValidation(zMode, zItemUID, zLocUID))
                    dxErrorProvider.SetError(txtItemUID, "This Item UID is used  or Invalid");   
                    
                else
                    m_ValidLabelUID = true;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


    }
    private void lookUpEditType_EditValueChanged(object sender, EventArgs e)
    {
        try
        {
            dxErrorProvider.SetError(lookUpEditLabelType, null);
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }
     

     
     
     
     
     
     
     
     
     
     
     
    private void txtStockCode_KeyDown(object sender, KeyEventArgs e)
    {
      dxErrorProvider.SetError(txtStockCode, null);
      SelectStockCode = false;  
      if (e.KeyCode == Keys.Enter)
      {
        dxErrorProvider.SetError(txtStockCode, null);
        btnSearcher.PerformClick();
      }
    }
     
    private void txtStockCode_Leave(object sender, EventArgs e)
    {
     
      if (txtStockCode.Text.Trim() != "")
      {
        DispalyData();
        if (SelectStockCode)
        {
          dxErrorProvider.SetError(txtStockCode, null);
           
           
          if(lookUpEditLocationUID.Text == "")    
            LoadLocationUIDForStockCode();
        }
      }
      else
        ClearStockData();
    }
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
         MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
     
    private void lookUpEditLocationUID_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        if (lookUpEditLocationUID.EditValue == null)
          return;

        txtStatusMsg.EditValue = "";  
        dxErrorProvider.SetError(lookUpEditLocationUID, null);
        lblReceiveLoc.Text = lookUpEditLocationUID.Text.Trim();
        lookUpEditLocationUID.Properties.ForceInitialize();
        object oValue = lookUpEditLocationUID.Properties.GetDataSourceRowByKeyValue(lookUpEditLocationUID.EditValue);
        if (oValue != null)
        {
          DataRow dr = ((DataRowView)oValue).Row;
          if (dr != null)
          {
            lblERPDistrict.Text = dr[ISMLocation.ERPDIST].ToString();
            lblERPWHS.Text = dr[ISMLocation.ERPWHS].ToString();
            lblERPGrid.Text = dr[ISMLocation.ERPGRID].ToString();
            lblERPBin.Text = dr[ISMLocation.ERPBIN].ToString();
             
            lblSealCode.Text = dr[ISMLocation.SealCode].ToString();
             
             
            if (lblSealCode.Text.Trim() != "")
            {
                m_SealCode = int.Parse(lblSealCode.Text.Substring(0, 1));
                if (m_SealCode != (int)ISM.SealType.None)
                    lblSealUID.Text = dr[ISMLocation.SealUID].ToString();
                else
                    lblSealUID.Text = "";
            }
             
   
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
    private void txtSerialNo_EditValueChanged(object sender, EventArgs e)
    {
      txtStatusMsg.EditValue = "";  
      dxErrorProvider.SetError(txtSerialNo, null);
    }
     
    private void txtSerialNo_Leave(object sender, EventArgs e)
    {
        try
        {
            if (txtSerialNo.Text.Trim() != "" && txtStockCode.Text.Trim() != "")
            {
                dxErrorProvider.SetError(txtSerialNo, null);
                m_ValidSerialNo = false;
                int zMode = 0; 
                if (!m_ISMLoginInfo.ISMServer.PDTSerialNumberChecking(zMode, txtStockCode.Text.Trim(), txtSerialNo.Text.Trim()))
                    dxErrorProvider.SetError(txtSerialNo, "Stock Code and Serial No Exists already. Enter unique Serial No");
                else
                    m_ValidSerialNo = true;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
        StockTrackingIndicator = zStockCodeSearcher.TrackingIndicator;
        if (zStockCodeSearcher.SelStockCode)
          DispalyData();
        
         if (lookUpEditLocationUID.Text.Trim() == "" && txtStockCode.Text.Trim() != "")
        {
          LoadLocationUIDForStockCode();
           
           
           
           
           
           
           
        }
      }
      catch
      {
        MessageBox.Show("System Error. Contact System Administrator", "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void txtQuantity_EditValueChanged(object sender, EventArgs e)
    {
      txtStatusMsg.EditValue = "";  
      dxErrorProvider.SetError(txtQuantity, null);
      lblReceiveQty.Text = txtQuantity.Text;
    }
    private void dtReceiveDate_EditValueChanged(object sender, EventArgs e)
    {
      txtStatusMsg.EditValue = "";  
      dxErrorProvider.SetError(dtReceiveDate, null);
      lblReceiveDate.Text = dtReceiveDate.Text;

    }
    private void txtBatchLotNo_EditValueChanged(object sender, EventArgs e)
    {
      txtStatusMsg.EditValue = "";  
      dxErrorProvider.SetError(txtBatchLotNo, null);
    }
    private void DispalyData()
    {
      try
      {
        if (txtStockCode.Text.Trim() != "")
        {
          DataSet ds = null;
          DataRow dr = null;
          string zVolumetric = "";
          dxErrorProvider.SetError(txtStockCode, null);
          ds = m_ISMLoginInfo.ISMServer.GetStockReceiveDisplayData(txtStockCode.Text.Trim());
          if (ds != null)
          {
            if (ds.Tables[0].Rows.Count > 0)
            {
              dr = ds.Tables[0].Rows[0];
              lblShortName.Text = dr[ISMStockCatalogue.StockShortName].ToString();
              lblUnitOfIssue.Text = dr[ISMStockCatalogue.StockUnitOfIssue].ToString();
              lblTrackableInd.Text = dr[ISMStockCatalogue.StockTrakingInd].ToString();
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
              StockTrackingIndicator = dr[ISMStockCatalogue.StockTrakingInd].ToString();
              m_BatchLotInd = dr[ISMStockCatalogue.StockBatchLotMgtInd].ToString();
              lblBatchLotInd.Text = m_BatchLotInd;
               
              EnableDisableControl();
               
               
              SelectStockCode = true;  
            }
            else
            {
               
              SelectStockCode = false;
              ClearStockData();
              dxErrorProvider.SetError(txtStockCode, "Invalid Stock Code");
            }
          }
        }
        else
        {
          SelectStockCode = false;
          dxErrorProvider.SetError(txtStockCode, "Select a Stock Code");
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

      }
    }

    #region "Clear Button"
    private void btnClear_Click(object sender, EventArgs e)
    {
      try
      {
        lookUpEditOperatorID.EditValue = lookUpEditOperatorID.Properties.GetKeyValueByDisplayText(m_ISMLoginInfo.LogonID);
        lblReceivedBy.Text = m_ISMLoginInfo.LogonID;
        dtReceiveDate.EditValue = null;
         
        lookUpEditLocationUID.EditValue = null;
        lblERPDistrict.Text = "";
        lblERPWHS.Text = "";
        lblERPGrid.Text = "";
        lblERPBin.Text = "";
        txtStockCode.EditValue = null;
        txtSerialNo.EditValue = null;
        txtBatchLotNo.EditValue = null;
        txtPartNo.EditValue = null;
        txtQuantity.EditValue = null; 
        lblSealCode.Text = "";
         
        lblShortName.Text = "";
        lblUnitOfIssue.Text = "";
        lblTrackableInd.Text = "";
        lblRepairFlag.Text = "";
        lblVolumetric1.Text = "";
        lblReceiveLoc.Text = "";
        lblReceivetemUID.Text = "";
        lblReceiveQty.Text = "";
        lblVolumetric2.Text = "";
        lblReceiveDate.Text = "";
        btnSave.Enabled = true;
        lookUpEditOperatorID.Focus();
        LoadStockReceiveMetaData();
         
         
        lblSealUID.Text = "";
         
        m_BatchLotInd = "";      
        lblBatchLotInd.Text = ""; 
         
        SelectStockCode = false;
         
         
        ClearErrorIconText();
        txtStatusMsg.EditValue = "";  
        m_ValidLabelUID = false;  
        txtItemUID.EditValue = null;  
        lookUpEditLabelType.EditValue = null;  
         
        m_ValidSerialNo = false;  
        lblCategory.Text = "SV";  
        lookUpEditCategory.EditValue = "SV";  
       
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }
    void ClearStockData()
    {
      try
      {
        lblShortName.Text = "";
        lblUnitOfIssue.Text = "";
        lblTrackableInd.Text = "";
        lblRepairFlag.Text = "";
        lblBatchLotInd.Text = "";
        lblVolumetric1.Text = "";
        lblVolumetric2.Text = "";
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    void EnableDisableControl()
    {
      try
      {
        if (m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text.Trim())
        {
          txtPartNo.Enabled = true;
           
           
           
           
           
          if ((StockTrackingIndicator == "S" || StockTrackingIndicator == "E"))
            txtSerialNo.Enabled = true;
          else
          {
            txtSerialNo.EditValue = null;
            txtSerialNo.Enabled = false;
          }

          if (m_BatchLotInd == "Y")
            txtBatchLotNo.Enabled = true;
          else
          {
            txtBatchLotNo.EditValue = null;
            txtBatchLotNo.Enabled = false;
          }
        }
        else
        {
          
          txtSerialNo.Enabled = false;
          txtBatchLotNo.Enabled = false;
          txtPartNo.Enabled = false;
          txtSerialNo.EditValue = null;
          txtBatchLotNo.EditValue = null;
          txtPartNo.EditValue = null;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }
    #endregion

    #region "Validation"
    private void ClearErrorIconText()
    {
      try
      {
        dxErrorProvider.SetError(lookUpEditOperatorID, null);
        dxErrorProvider.SetError(dtReceiveDate, null);
        
        dxErrorProvider.SetError(lookUpEditLocationUID, null);
        dxErrorProvider.SetError(txtStockCode, null);
         
        dxErrorProvider.SetError(txtSerialNo, null);
        dxErrorProvider.SetError(txtQuantity, null);
        dxErrorProvider.SetError(txtBatchLotNo, null);
        dxErrorProvider.SetError(txtItemUID, null);  
        dxErrorProvider.SetError(lookUpEditLabelType, null);  
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    
    private bool Validaion()
    {
      bool zResult = false;
      bool zValidationFail = true;
      ClearErrorIconText();
      try
      {
        if (txtQuantity.Text.Trim() == "" || float.Parse(txtQuantity.Text) <= 0)
        {
          dxErrorProvider.SetError(txtQuantity, "Enter Quantity");
          txtQuantity.Focus();
          zValidationFail = false;
        }
        if ((StockTrackingIndicator == "S" || StockTrackingIndicator == "E"))
        {
          if (txtQuantity.Text.Trim() != "" && float.Parse(txtQuantity.Text) > 1)
          {
            dxErrorProvider.SetError(txtQuantity, "Stock Code is Serialised. Receive Quantity should be 1");
            txtQuantity.Focus();
            zValidationFail = false;
          }
        }
        if (m_BatchLotInd == "Y" && m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text.Trim())
        {
          if (txtBatchLotNo.Text.Trim() == "")
          {
             
            dxErrorProvider.SetError(txtBatchLotNo, "Enter Batch/Lot No");
            txtSerialNo.Focus();
            zValidationFail = false;
          }
        }
        if ((StockTrackingIndicator == "S" || StockTrackingIndicator == "E") && m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text.Trim())
        {
          if (txtSerialNo.Text.Trim() == "")
          {
            dxErrorProvider.SetError(txtSerialNo, "Enter Serial No");
            txtSerialNo.Focus();
            zValidationFail = false;
          }
           
          if (!m_ValidSerialNo)
          {
              dxErrorProvider.SetError(txtSerialNo, "Stock Code and Serial No Exists already. Enter unique Serial No");
              txtSerialNo.Focus();
              zValidationFail = false;
          }
           
        }

        if (txtStockCode.Text.Trim() == "")
        {
          dxErrorProvider.SetError(txtStockCode, "Select a Stock Code");
          txtStockCode.Focus();
          zValidationFail = false;
        }
        else if (!SelectStockCode)
        {
          dxErrorProvider.SetError(txtStockCode, "Select a Stock Code From Searcher List");
          txtStockCode.Focus();
          zValidationFail = false;
        }
         
         
         
         
         
         
         
         
         
         
         
        if (m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text.Trim())
        {
             
            if (lookUpEditLocationUID.EditValue == null)
            {
                dxErrorProvider.SetError(lookUpEditLocationUID, "Select a Location UID");
                lookUpEditLocationUID.Focus();
                zValidationFail = false;
            }
             
             
             
             
             
             
             
            if (txtItemUID.Text.Trim() == "")
            {
                dxErrorProvider.SetError(txtItemUID, "Enter Item UID");
                txtItemUID.Focus();
                zValidationFail = false;
            }
            if (txtItemUID.Text.Trim() != "")
            {
                if (txtItemUID.Text.Trim().Length != 13)
                {
                    dxErrorProvider.SetError(txtItemUID, "Enter 13 digit Label UID");
                    txtItemUID.Focus();
                    zValidationFail = false;

                }
                if (!m_ValidLabelUID)
                {
                    dxErrorProvider.SetError(txtItemUID, "Item Label UID is used already or Invalid");
                    txtItemUID.Focus();
                    zValidationFail = false;
                }
            }
            if (lookUpEditLabelType.EditValue == null)
            {
                dxErrorProvider.SetError(lookUpEditLabelType, "Select a Label Type");
                lookUpEditLabelType.Focus();
                zValidationFail = false;
            }
        }
        else
        {
             
            if (txtItemUID.Text.Trim() != "")
            {
                if (txtItemUID.Text.Trim().Length != 13)
                {
                    dxErrorProvider.SetError(txtItemUID, "Enter 13 digit Label UID");
                    txtItemUID.Focus();
                    zValidationFail = false;

                }
                if (!m_ValidLabelUID)
                {
                    dxErrorProvider.SetError(txtItemUID, "Item Label UID is used already or Invalid");
                    txtItemUID.Focus();
                    zValidationFail = false;
                }

                if (lookUpEditLabelType.EditValue == null)
                {
                    dxErrorProvider.SetError(lookUpEditLabelType, "Select a Label Type");
                    lookUpEditLabelType.Focus();
                    zValidationFail = false;
                }
            }
        }
         
        if (dtReceiveDate.EditValue != null)
        {
          if (m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text.Trim())
          {
            if (DateTime.Parse(dtReceiveDate.EditValue.ToString()) != DateTime.Today)
            {
              dxErrorProvider.SetError(dtReceiveDate, "Enter Current Date");
              dtReceiveDate.Focus();
              zValidationFail = false;
            }
          }
          else if (DateTime.Parse(dtReceiveDate.EditValue.ToString()) < DateTime.Today)
          {
            dxErrorProvider.SetError(dtReceiveDate, "Enter Current Date");
            dtReceiveDate.Focus();
            zValidationFail = false;
          }
        }
        else
        {
          dxErrorProvider.SetError(dtReceiveDate, "Enter Current Date or Future Date");
          dtReceiveDate.Focus();
          zValidationFail = false;
        } 
        if (lookUpEditOperatorID.Text.Trim() == "")
        {
          dxErrorProvider.SetError(lookUpEditOperatorID, "Select a Operator");
          lookUpEditOperatorID.Focus();
          zValidationFail = false;
        }
       if (lookUpEditCategory.EditValue == null)  
        {
            dxErrorProvider.SetError(lookUpEditCategory, "Select a Category");
            lookUpEditCategory.Focus();
            zValidationFail = false;
        }
       if (lookUpEditCategory.EditValue.ToString() == "-----")  
       {
           dxErrorProvider.SetError(lookUpEditCategory, "Invalid Category");
           lookUpEditCategory.Focus();
           zValidationFail = false;
       }
         
        if (zValidationFail)
          zResult = zValidationFail;
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      return zResult;
    }
    #endregion

    #region " Save Button"
    private void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        if (Validaion())
        {
          string zMessage = "";
          string zConMessage = "";
          long zSealUID = 0;
           
          int zLabelType = 0;
          if (m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text)
          {
            zMessage = String.Format("Do you want to perform Stock Receive for the Item UID {0}, Location UID {1} and Stock Code {2}?",  txtItemUID.Text,lookUpEditLocationUID.Text,txtStockCode.Text);
             
            zConMessage = "Stock Received for the Item UID" + txtItemUID.Text + ", Location " + lookUpEditLocationUID.Text + "\r\nand Stock Code " + txtStockCode.Text;  
          }
          else
          {
             
              if (txtItemUID.Text.Trim() != "")
            {
              if (lookUpEditLocationUID.Text.Trim() != "")
              {
                  zMessage = String.Format("Do you want to create Stock Receive Task for the Item UID {0}, Location UID {1} and Stock Code {2}?", txtItemUID.Text, lookUpEditLocationUID.Text, txtStockCode.Text);
                 
                zConMessage = "Stock Receive Task has been created the Item UID" + txtItemUID.Text + ", Location " + lookUpEditLocationUID.Text + "\r\nand Stock Code " + txtStockCode.Text;
              }
              else
              {
                  zMessage = String.Format("Do you want to create Stock Receive Task for the Item UID {0} and Stock Code {1}?", txtItemUID.Text, txtStockCode.Text);
                 
                zConMessage = "Stock Receive Task has been created for the Item UID " + txtItemUID.Text + "\r\nand Stock Code " + txtStockCode.Text;  
              }
            }
            else
            {
              zMessage = String.Format("Do you want to create Stock Receive Task for the Stock Code {0}?", txtStockCode.Text);
              
              zConMessage = "Stock Receive Task has been created for the Stock Code " + txtStockCode.Text;  
            }
             
          }
          DialogResult zReply = MessageBox.Show(zMessage, "Stock Receive", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
          if (zReply == DialogResult.Yes)
          {

            ISMTask.StructTask zTask = new ISMTask.StructTask();
            ISMStock.StructStock zStock = new ISMStock.StructStock();
            int nMode;
            if (m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text)
            {
                 
                FrmWitnessLogin zFrmWitnessLogin = new FrmWitnessLogin(m_ISMLoginInfo);
                zFrmWitnessLogin.ShowDialog();
                if (zFrmWitnessLogin.CoALoginResult == (int)CoALoginStauts.ValidLogin || zFrmWitnessLogin.CoALoginResult == (int)CoALoginStauts.Exception)  
                {
                    zTask.Type = "3";
                    zTask.Status = "Completed";
                       
                       
                       
                      nMode = 0;
                      zTask.OperationID = "24";  
                      zStock.CoALoginID = zFrmWitnessLogin.CoALoginUserID.ToString();
                      
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
                  zTask.OperationID = "4";  
                   
                  nMode = 1;
                  zStock.CoALoginID = "0";  
            }
             
            if (lblSealUID.Text.Trim() != "" && lblSealUID.Text.Length == 13)
                zSealUID = long.Parse(lblSealUID.Text.Trim().Substring(1, 12));
            zTask.StockQty = txtQuantity.Text;
             
             
             
             
             
            if (txtItemUID.Text.Trim() != "")
                zTask.ItemID = txtItemUID.Text.Substring(1, 12);
            else
                zTask.ItemID = "0";
            if (lookUpEditLabelType.EditValue != null)  
              zLabelType = int.Parse(lookUpEditLabelType.EditValue.ToString());
             
            zTask.SourceID = "0";
            if (lookUpEditLocationUID.EditValue == null)  
              zTask.DestinationID = "0";
            else
              zTask.DestinationID = lookUpEditLocationUID.EditValue.ToString();

            zTask.UserID = m_OperatorID;
            zTask.StatusCode = "1"; 
            zTask.CreateUserID = m_ISMLoginInfo.LogonID;
             
             
            zTask.CreateDateTime = String.Format(dtReceiveDate.EditValue.ToString().Substring(0, 10) + " " + DateTime.Now.ToLongTimeString(), m_ISMLoginInfo.Params.DateTimeFormat);  

            zStock.StockCatalogueCode = txtStockCode.Text;
            zStock.StockSerialEquipNo = txtSerialNo.Text.Trim();
            zStock.StockBatchLotNo = txtBatchLotNo.Text.Trim();
            zStock.StockPartNo = txtPartNo.Text.Trim();
            zStock.StockErpDistrict = lblERPDistrict.Text;
            zStock.StockErpWHS = lblERPWHS.Text;
            zStock.StockErpGrid = lblERPGrid.Text;
            zStock.StockErpBin = lblERPBin.Text;
            zStock.StockCatalogueCode = txtStockCode.Text.Trim();
            zStock.SealUID = zSealUID.ToString();
            zStock.StockCategoryCode = lookUpEditCategory.EditValue.ToString();  
            

             
            if (m_ISMLoginInfo.ISMServer.CreateReceiveTask(nMode, zStock, zTask, zLabelType))
            {
              
              
               
               
               
               
               
              
               
               
              btnClear.PerformClick();
               
              txtStatusMsg.Text = zConMessage;
            }
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

    private void btnException_Click(object sender, EventArgs e)
    {
      try
      {
        long zItemUID = 0;
        long zLocationUID = 0;
        long zSealUID = 0;
        string zStockCode = "";
        string zExpMessage = "Exception has been created for ";
        if (txtItemUID.EditValue != null)  
        {
            if (!m_ValidLabelUID)
            {
                dxErrorProvider.SetError(txtItemUID, "Item Label UID is used already or Invalid");
                return;
            }
        }
        if (lookUpEditLocationUID.EditValue == null && !SelectStockCode)
        {
           
           
           
          MessageBox.Show("Select at least any one of the key field e.g. Location UID or Stock Code", "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
        else
        {
           
            if (txtItemUID.EditValue != null)
            {
                zItemUID = long.Parse(txtItemUID.Text.Substring(1, 12));
                zExpMessage = zExpMessage + "Item UID " + txtItemUID.Text + " ";
            }
            if (lookUpEditLocationUID.EditValue != null)
            {
                zLocationUID = long.Parse(lookUpEditLocationUID.EditValue.ToString());
                zExpMessage = zExpMessage + "Location UID " + lookUpEditLocationUID.Text + " ";
            }
            if (txtStockCode.Text.Trim() != "")
            {
                zStockCode = txtStockCode.Text;
                zExpMessage = zExpMessage + "Stock Code " + txtStockCode.Text;
            }
           
        }
        ISM.Forms.FrmException frmSTException = new ISM.Forms.FrmException(m_ISMLoginInfo) {    LocationUID = zLocationUID, 
                                                                                                ItemUID = zItemUID, 
                                                                                                StockCode = zStockCode, SealUID = zSealUID, 
                                                                                                JournalType = "SRC" ,
                                                                                                MsgBoxCaption = "Stock Receive",
                                                                                                InvenCatCode = lblCategory.Text  
                                                                                            };
        frmSTException.Text = "Stock Receive Exception";
        frmSTException.ShowDialog();
         
        if (frmSTException.SaveResult)
            txtStatusMsg.Text = zExpMessage;
        else
            txtStatusMsg.EditValue = null;
        

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void lookUpEditCategory_EditValueChanged(object sender, EventArgs e)
    {
        try
        {
            dxErrorProvider.SetError(lookUpEditCategory, null);
            if (lookUpEditCategory.EditValue == null)
                lblCategory.Text = "";
            else
            {
                if (lookUpEditCategory.EditValue.ToString() == "-----")
                {
                    dxErrorProvider.SetError(lookUpEditCategory, "Invalid Category");
                    lblCategory.Text = "";
                }
                else
                    lblCategory.Text = lookUpEditCategory.Text;
            }
        }
        catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void groupControl_Paint(object sender, PaintEventArgs e)
    {

    }
    
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     


     
  }
}
