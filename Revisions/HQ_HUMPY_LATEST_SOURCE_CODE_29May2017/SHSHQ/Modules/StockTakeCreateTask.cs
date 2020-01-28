 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

#region "Namespace"
using System;
using System.Data;
using System.Windows.Forms;
using ISMDAL.TableColumnName;
using ISM.Forms;
#endregion

namespace ISM.Modules
{
  public partial class StockTakeCreateTask : ISMBaseWorkSpace
  {
    private string m_OperatorID;

    #region "Form Load And Constructor"
    public StockTakeCreateTask(ISMLoginInfo AISMLoginInfo)
      : base(AISMLoginInfo)
    {
      InitializeComponent();
    }

    private void StockTakeCreateTask_Load(object sender, EventArgs e)
    {
      SetLookUpEditCaption();
       
      gcStockData.LookAndFeel.SetSkinStyle(m_ISMLoginInfo.Params.AppSkinColour);  
      btnClear.PerformClick();
    }
    #endregion

    #region "Initialization"
    private void LoadStockTakeMetaData()
    {
      DataSet ds = null;
      try
      {
        int zMod = 3;  
        ds = m_ISMLoginInfo.ISMServer.GetISMOperatorID(zMod, m_ISMLoginInfo.UserProfileCode, m_ISMLoginInfo.UserID);  
        if (ds != null)
        {
            lookUpEditOperatorID.Properties.DataSource = ds.Tables[0].DefaultView;
        }

        ds = m_ISMLoginInfo.ISMServer.GetStockTakeMetaData();
        if (ds != null)
        {
           
          lookUpEditLocationUID.Properties.DataSource = ds.Tables[ISMLocation.TableName].DefaultView;
           
          lookUpEditOperatorID.EditValue = lookUpEditOperatorID.Properties.GetKeyValueByDisplayText(m_ISMLoginInfo.LogonID);
          lblStockTakeBy.Text = m_ISMLoginInfo.LogonID;
        }
      }
      catch
      {
        MessageBox.Show("System Error. Contact System Administrator", "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.LocationCode, 100,"Location Code"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPDIST, 80, "ERP DIST"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPWHS, 80, "ERP WHS"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPGRID, 80, "ERP GRID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPBIN, 80, "ERP BIN")});

         
        lookUpEditOperatorID.Properties.DisplayMember = ISMUser.UserLogonID;
        lookUpEditOperatorID.Properties.ValueMember = ISMUser.UserID;

        lookUpEditLocationUID.Properties.DisplayMember = "LOCATION_UID";  
         
        lookUpEditLocationUID.Properties.ValueMember = ISMLocation.LocationUID;  
      }
      catch
      {
        MessageBox.Show("System Error. Contact System Administrator", "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

    #region "Lookup Edit & Date Control Event"

    private void lookUpEditOperatorID_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        lblStockTakeBy.Text = lookUpEditOperatorID.Text;
        if (m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text)
        {
          gcCreateTask.Text = "Stocktake";
          btnSave.Text = "Verify";
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
          {
            m_OperatorID = dr[ISMUser.UserID].ToString();
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }

    private void lookUpEditLocationUID_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        if (lookUpEditLocationUID.EditValue != null)
        {
           
          dxErrorProvider.SetError(lookUpEditLocationUID, null);
          txtStatusMsg.Text = "";  
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
              lblLocationUID.Text = lookUpEditLocationUID.Text;
               
               
               
               
               
               
               
               
               
               
               
               
               
               
               
               
               
            }
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void dtStockTakeDate_EditValueChanged(object sender, EventArgs e)
    {
      lblStockDate.Text = dtStockTakeDate.Text;
      txtStatusMsg.Text = "";  
      dxErrorProvider.SetError(dtStockTakeDate, null);
    }

    #endregion

    #region "Validation"
    private bool Validation()
    {
      bool bResult = false;
      try
      {
        bool zValidationFail = true;
        ClearErrorIcon();
        if (lookUpEditOperatorID.Text.Trim() == "")
        {
          dxErrorProvider.SetError(lookUpEditOperatorID, "Select a Operator ID");
          lookUpEditOperatorID.Focus();
          zValidationFail = false;
        }
        if (lookUpEditLocationUID.EditValue == null)  
        {
            dxErrorProvider.SetError(lookUpEditLocationUID, "Select a Location UID");
            lookUpEditLocationUID.Focus();
            zValidationFail = false;
        }
        if (dtStockTakeDate.EditValue == null)  
        {
          dxErrorProvider.SetError(dtStockTakeDate, "Select Stocktake Date");
          dtStockTakeDate.Focus();
          zValidationFail = false;
        }
        else if (dtStockTakeDate.EditValue != null)
        {
          if (m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text)
          {
            if (DateTime.Parse(dtStockTakeDate.EditValue.ToString()) != DateTime.Today)
            {
              dxErrorProvider.SetError(dtStockTakeDate, "Enter Current Date");
              dtStockTakeDate.Focus();
              zValidationFail = false;
            }
          }
          else if (DateTime.Parse(dtStockTakeDate.EditValue.ToString()) < DateTime.Today)
          {
            dxErrorProvider.SetError(dtStockTakeDate, "Enter Current Date or Future Date");
            dtStockTakeDate.Focus();
            zValidationFail = false;
          }
        }
        if (txtMILISStocTakeNo.Text.Trim() != "")
        {
            if(m_ISMLoginInfo.ISMServer.IsMILISStockTakeNoExists(txtMILISStocTakeNo.Text.Trim()))  
            {
                dxErrorProvider.SetError(txtMILISStocTakeNo, "MILIS Stock Take No already exist");
                dtStockTakeDate.Focus();
                zValidationFail = false;
            }
            if (txtMILISPlanID.Text.Trim() == "")  
            {
                dxErrorProvider.SetError(txtMILISPlanID, "Enter MILIS Plan ID");
                txtMILISPlanID.Focus();
                zValidationFail = false;
            }
        }
        if (txtMILISPlanID.Text.Trim() != "")
        {
            if (txtMILISStocTakeNo.Text.Trim() == "")  
            {
                dxErrorProvider.SetError(txtMILISStocTakeNo, "Enter MILIS Stock Take No");
                txtMILISStocTakeNo.Focus();
                zValidationFail = false;
            }

        }
        
        if (zValidationFail)
          bResult = zValidationFail;
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      return bResult;
    }
    #endregion

    #region " Save Button"
    private void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        if (Validation())
        {
          bool zResult = false;
          string zConMessage = "";
          int zMode = 0;  
          long zTaskID = 0;  
          if (m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text)
          {
             
            using (FrmStockTakeLocTree zfrmStockTakeLocTree = new FrmStockTakeLocTree(m_ISMLoginInfo) { LocationID = long.Parse(lookUpEditLocationUID.EditValue.ToString()), 
                                                                                      LocationUID = lookUpEditLocationUID.Text.Trim(), 
                                                                                      MILISStockTakeNo = txtMILISStocTakeNo.Text.Trim(),
                                                                                      MILISPlanID = txtMILISPlanID.Text.Trim() })
            {
              zfrmStockTakeLocTree.ShowDialog();
              zConMessage = zfrmStockTakeLocTree.StockTakeResult;
              zResult = true;
            }
          }
          else
          {
            DialogResult zReply = MessageBox.Show(String.Format("Do you want to create a Stocktake Task for Location UID {0} ?", lookUpEditLocationUID.Text), "Stocktake", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (zReply == DialogResult.Yes)
            {
                ISMTask.StructTask zTask = new ISMTask.StructTask();
                zTask.Type = "1";
                zTask.Status = "Assigned"; 
                zTask.StockQty = "0";
                zTask.UserID = m_OperatorID;
                zTask.ItemID = "0";
                zTask.SourceID = lookUpEditLocationUID.EditValue.ToString();
                zTask.DestinationID = "0";
                zTask.OperationID = "6";  
                zTask.StatusCode = "1"; 
                zTask.CreateUserID = m_ISMLoginInfo.LogonID;
                 
                 
                zTask.CreateDateTime = String.Format(dtStockTakeDate.EditValue.ToString().Substring(0, 10) + " " + DateTime.Now.ToLongTimeString(), m_ISMLoginInfo.Params.DateTimeFormat);  
                zTask.StockCode = "";
                zTask.MILISStockTakeNo = txtMILISStocTakeNo.Text.Trim();  
                if (m_ISMLoginInfo.ISMServer.CreateStockTakeTask(zMode, zTask, ref zTaskID, txtMILISPlanID.Text.Trim()))
                {
                    zResult = true;  
                    zConMessage = "Stocktake Task has been created for Location UID " + lookUpEditLocationUID.Text;  
                     

                     
                     
                }
            }
            else
                return;
          }
          btnClear.PerformClick(); 
          if (zResult)
            txtStatusMsg.Text = zConMessage;   
        }
      }
      catch (Exception ex)
      {
          MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

    #region " Clear Button"
    private void ClearErrorIcon()
    {
      dxErrorProvider.SetError(lookUpEditOperatorID, null);
      dxErrorProvider.SetError(dtStockTakeDate, null);
      dxErrorProvider.SetError(lookUpEditLocationUID, null);
      dxErrorProvider.SetError(txtMILISStocTakeNo, null);
      dxErrorProvider.SetError(txtMILISPlanID, null);
    }
    private void btnClear_Click(object sender, EventArgs e)
    {
      try
      {
        lookUpEditOperatorID.EditValue = null;
        lookUpEditLocationUID.EditValue = null;
        dtStockTakeDate.EditValue = null;
        lblERPDistrict.Text = "";
        lblERPWHS.Text = "";
        lblERPGrid.Text = "";
        lblERPBin.Text = "";
        lblStockTakeBy.Text = "";
        lblLocationUID.Text = "";
        lblSealCode.Text = "";
         
        lblStockDate.Text = "";
        LoadStockTakeMetaData();
        lookUpEditOperatorID.Focus();
        btnSave.Enabled = true;
        ClearErrorIcon();
        txtStatusMsg.Text = "";  
        txtMILISStocTakeNo.Text = "";  
        txtMILISPlanID.Text = "";  
      }
      catch (Exception ex)
      {
          MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

    #region "TextChanged"
    private void txtMILISStocTakeNo_TextChanged(object sender, EventArgs e)
    {
        dxErrorProvider.SetError(txtMILISStocTakeNo, null);  
    }

    private void txtMILISPlanID_TextChanged(object sender, EventArgs e)
    {
        dxErrorProvider.SetError(txtMILISPlanID, null);  
    }
    #endregion


  }
}
