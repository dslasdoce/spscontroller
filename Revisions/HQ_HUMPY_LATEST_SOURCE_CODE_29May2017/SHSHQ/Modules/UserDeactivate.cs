 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

using System;
using System.Data;
using System.Windows.Forms;
using ISMDAL.TableColumnName;
using AMMO_BG_DLL.Background;
using System.Configuration;

namespace ISM.Modules
{
  public partial class UserDeactivate : ISMBaseWorkSpace
  {
      Logs applicationLogs = new Logs();
    public UserDeactivate(ISMLoginInfo AISMLoginInfo)
      : base(AISMLoginInfo)
    {
      InitializeComponent();
    }

    private void RemoveOperator_Load(object sender, EventArgs e)
    {
       
      luLoginId.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLogonID, 240, "User ID"),  
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserFirstName, 180,"First Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLastName, 180,"Last Name")});

       
      luLoginId.Properties.DisplayMember = ISMUser.UserLogonID;
      luLoginId.Properties.ValueMember = ISMUser.UserID;

       
       
      luReactivateUserId.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLogonID, 240, "User ID"),  
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserFirstName, 180,"First Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLastName, 180,"Last Name")});

       
      luReactivateUserId.Properties.DisplayMember = ISMUser.UserLogonID;
      luReactivateUserId.Properties.ValueMember = ISMUser.UserID;
       

      LoadOperatorMetaData();

      btnDeactivate.Enabled = false;
      btnReactivate.Enabled = false;  
    }

    private void LoadOperatorMetaData()
    {
      try
      {
         
        DataSet ds = m_ISMLoginInfo.ISMServer.ISMUserGetRecds();  
        if (ds != null)
        {
             
             
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow zRowUserRecord in ds.Tables[0].Rows)
                {
                    if (zRowUserRecord["LOGON_ID"].ToString() == m_ISMLoginInfo.LogonID.ToString())
                        zRowUserRecord.Delete();
                }
            }
             
            luLoginId.Properties.DataSource = ds.Tables[0].DefaultView;
        }

         
        ds = m_ISMLoginInfo.ISMServer.GetISMDeactivateUser();
        if (ds != null)
        {
            luReactivateUserId.Properties.DataSource = ds.Tables[0].DefaultView;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("System Error: " + ex.Message + "\nContact System Administrator", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void luLoginId_EditValueChanged(object sender, EventArgs e)
    {
        if (luLoginId.EditValue != null)  
        {
            DevExpress.XtraEditors.LookUpEdit zEditor = (sender as DevExpress.XtraEditors.LookUpEdit);
            DataRowView zDataRow = zEditor.Properties.GetDataSourceRowByKeyValue(zEditor.EditValue) as DataRowView;
            txtStatusMsg.Text = "";  
            if (zDataRow != null)   
            {
                txtFirstName.Text = Convert.ToString(zDataRow[2]);
                txtLastName.Text = Convert.ToString(zDataRow[3]);
                dtDeactivateDate.DateTime = DateTime.Now;
            }
            btnDeactivate.Enabled = true;
        }
    }
    private void luReactivateUserId_EditValueChanged(object sender, EventArgs e)
    {
        if (luReactivateUserId.EditValue != null)
        {
            DevExpress.XtraEditors.LookUpEdit zEditor = (sender as DevExpress.XtraEditors.LookUpEdit);
            DataRowView zDataRow = zEditor.Properties.GetDataSourceRowByKeyValue(zEditor.EditValue) as DataRowView;
            txtStatusMsg.Text = "";
            if (zDataRow != null)
            {
                txtReActFirstName.Text = Convert.ToString(zDataRow[2]);
                txtReActLastName.Text = Convert.ToString(zDataRow[3]);
            }
            btnReactivate.Enabled = true;
        }

    }

    private void ClearFields()
    {
      luLoginId.EditValue = null;
      txtFirstName.Text = "";
      txtLastName.Text = "";
      dtDeactivateDate.Text = "";
      btnDeactivate.Enabled = false;
      luLoginId.Focus();    
    }
    private void ClearReactivateFields()
    {
        luReactivateUserId.EditValue = null;
        txtReActFirstName.Text = "";
        txtReActLastName.Text = "";
        dxErrorProvider.SetError(luReactivateUserId, null);
        btnReactivate.Enabled = false;
    }

    private void btnDeactivate_Click(object sender, EventArgs e)
    {
        HQDataExtract dataExtract = new HQDataExtract();
        dxErrorProvider.SetError(luLoginId, null);  
        if (luLoginId.EditValue == null)  
        {
             
            dxErrorProvider.SetError(luLoginId, "Select a Login ID to be Remove");  
            return;

        }
        string zLoginID;  
        int zMod = 1;  
        long zUserID = long.Parse(luLoginId.EditValue.ToString());
        string personnelName = "";

        personnelName = luLoginId.Text.ToString();

        string zMsgStr = string.Format("Are you sure? Do you want to delete\n\nOperator: {0:s}", luLoginId.Text);
        DialogResult zReply = MessageBox.Show(zMsgStr, "ISM Delete User ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
       if (zReply == DialogResult.Yes)
       {
            zLoginID = luLoginId.Text.Trim();  
            if (m_ISMLoginInfo.ISMServer.DeactivateISMAccount(luLoginId.Text, dtDeactivateDate.DateTime) == false)
            {
                applicationLogs.InsertApplicationLogs(
                     ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                     "User",
                   m_ISMLoginInfo.ISMServer.CurrentLoggedInUser.ToString(),
                     "Failed",
                     "Delete",
                    luLoginId.Text.ToString(),
                     "HQ",
                     "");
              zMsgStr = string.Format("Error deleting Operator: {0:s}", luLoginId.Text);
              MessageBox.Show(zMsgStr, lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            }
            else
            {
                applicationLogs.InsertApplicationLogs(
                  ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                  "User",
                m_ISMLoginInfo.ISMServer.CurrentLoggedInUser.ToString(),
                  "Success",
                  "Deleted",
                 luLoginId.Text.ToString(),
                  "HQ",
                  ""); 
              
              ClearFields();
              LoadOperatorMetaData();  
              txtStatusMsg.Text = "Login ID " + zLoginID + " has been deleted.";
             // dataExtract.PerformPesonelDataExtract(ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(), false);
            }
       }     
    }

    private void btnClear_Click(object sender, EventArgs e)  
    {
        luLoginId.EditValue = null;
        txtFirstName.Text = "";
        txtLastName.Text = "";
        dtDeactivateDate.EditValue = null;
        dxErrorProvider.SetError(luLoginId, null);
        btnDeactivate.Enabled = false;
        luLoginId.Focus();
    }

    private void btnReactivateClear_Click(object sender, EventArgs e)  
    {
        ClearReactivateFields();
    }

    private void btnReactivate_Click(object sender, EventArgs e)  
    {
        HQDataExtract extractData = new HQDataExtract();
        try
        {
            dxErrorProvider.SetError(luReactivateUserId, null);
            if (luReactivateUserId.EditValue == null) 
            {
                dxErrorProvider.SetError(luReactivateUserId, "Select a Login ID to be Reactivate");  
                return;

            }
            string zLoginID; 

            string zMsgStr = string.Format("Are you sure? Do you want to reactivate\n\nOperator: {0:s}", luReactivateUserId.Text);
            DialogResult zReply = MessageBox.Show(zMsgStr, "ISM Reactivate User ID ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (zReply == DialogResult.Yes)
            {
                zLoginID = luReactivateUserId.Text.Trim();
                if (m_ISMLoginInfo.ISMServer.ReactivateISMAccount(luReactivateUserId.Text) == false)
                {
                    zMsgStr = string.Format("Error reactivate Operator: {0:s}", luLoginId.Text);
                    MessageBox.Show(zMsgStr, lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    ClearReactivateFields();
                    LoadOperatorMetaData();  
                    txtStatusMsg.Text = "Login ID " + zLoginID + " has been reactivated";
                    extractData.PerformPesonelDataExtract(ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(), false);
                }
            }     

        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }

   
  }
}
