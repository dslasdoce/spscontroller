

using System;
using System.Data;
using System.Windows.Forms;
using ISMComponents;   
using ISMDAL.TableColumnName;
using System.Text.RegularExpressions;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

using DevExpress.XtraGrid;

 
using Impinj.OctaneSdk;
using ImpinjWrite.Models;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Globalization;


using System.Configuration;


 
using MISCReaderTCPIP;

 
using SHSHQ.Tools;

using AMMO_BG_DLL.Background;

namespace ISM.Modules
{
  public partial class UserCreate : ISMBaseWorkSpace
  {
       
      private MISCReader mMISCReader;

      
      static ImpinjReader _impinjReader = new ImpinjReader();
      const ushort EPC_OP_ID = 123;
      static int USER_OP_ID;
      int USER_Write_ID;
      static int TID_OP_ID;
      const ushort PC_BITS_OP_ID = 321;
      private const ushort LenthPerBlock = 4;
      private ushort _currentPcBits;
      string _currentTag;
      int _maxLength = 24;
      int _prefixLength = 4;
      int _currentLength = 20;
      private readonly char _padding;
      private bool _isConnectedReader = false;
      private Color m_DefautltColor;
      bool m_TagVeify = false;
      private DataTable dt_gvHelmetTags = new DataTable();

      ISMLoginInfo mUserCreateLoginInfor;
      Logs applicationLogs = new Logs();
  


    public UserCreate(ISMLoginInfo AISMLoginInfo)
    : base(AISMLoginInfo)
    {
      InitializeComponent();

      mUserCreateLoginInfor = new ISMLoginInfo();
      mUserCreateLoginInfor.ISMServer = new ISMDAL.DAL(SHSHQ.Properties.Settings.Default.ConnectionString);
      mUserCreateLoginInfor.ISMServer.CurrentLoggedInUser = AISMLoginInfo.LogonID;
      mUserCreateLoginInfor.UserID = AISMLoginInfo.UserID;
      mUserCreateLoginInfor.LogonID = AISMLoginInfo.LogonID;
    }

    private void Operator_Load(object sender, EventArgs e)
    {
        try
        {
            SetGridCaption();
             
             
            LoadMetaData();        

            UHFReaderPanelSetup();

            Initilization_MISCReader_TCPIP();

        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    private void ConfigureControls()
    {
       
       
       
       
       
       
       
       

       
       
       
    }

    private void SetGridCaption()
    {
        try
        {
             
            ColumnView ColView = gvUserProfile.MainView as ColumnView;

            string[] zFieldNames = new string[] { "SELECT", "DESCRIPTION", "CODE" };

            DevExpress.XtraGrid.Columns.GridColumn zColumn;
            ColView.Columns.Clear();
            for (int i = 0; i < zFieldNames.Length; i++)
            {
                zColumn = ColView.Columns.AddField(zFieldNames[i]);
                zColumn.VisibleIndex = i;
            }
            gridView.Columns[0].Caption = "SELECT";
            gridView.Columns[0].Width = 60;

            gridView.Columns[1].Caption = "DESCRIPTION";
            gridView.Columns[1].Width = 450;

            gridView.Columns[2].Caption = "CODE";
            gridView.Columns[2].Width = 0;
            gridView.Columns[2].Visible = false;

             
            ColumnView ColView_Helmet = gvHelmetTags.MainView as ColumnView;

            string[] zFieldNames_Helmet = new string[] { "Used", "TIDP", "TIDHelmet" };

            DevExpress.XtraGrid.Columns.GridColumn zColum_Helmet;
            ColView_Helmet.Columns.Clear();
            for (int i = 0; i < zFieldNames_Helmet.Length; i++)
            {
                zColum_Helmet = ColView_Helmet.Columns.AddField(zFieldNames_Helmet[i]);
                zColum_Helmet.VisibleIndex = i;
            }
            gridViewUIDList.Columns[0].Caption = "Used";
            gridViewUIDList.Columns[0].Width = 60;
            gridViewUIDList.Columns[0].Visible = false;


            gridViewUIDList.Columns[1].Caption = "Group Number";
            gridViewUIDList.Columns[1].Width = 230;

            gridViewUIDList.Columns[2].Caption = "Individual Number";
            gridViewUIDList.Columns[2].Width = 230;
             

            dt_gvHelmetTags.Columns.Add("Used", typeof(bool));
            dt_gvHelmetTags.Columns.Add("TIDP", typeof(string));
            dt_gvHelmetTags.Columns.Add("TIDHelmet", typeof(string));
        }
        catch
        {
            MessageBox.Show("System Error. Contact System Administrator", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }


    private void LoadMetaData()
    {
      try
      {
        DataSet ds = m_ISMLoginInfo.ISMServer.GetISMUserRoleTable();
        if (ds != null)
        {
            gvUserProfile.DataSource = ds.Tables[0].DefaultView;
             
        }

        gvHelmetTags.DataSource = dt_gvHelmetTags.DefaultView;
      }
      catch (Exception ex)
      {
        MessageBox.Show(string.Format("System Error: LoadMetaData\n{0:s}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

     
     
     
     
     
    private void ClearControls()
    {
        try
        {
            try
            {
                 
                DataTable clone = dt_gvHelmetTags.Copy();
                clone.Rows.Clear();
                gvHelmetTags.BeginInvoke(new MethodInvoker(delegate { gvHelmetTags.DataSource = clone.DefaultView; }));
                dt_gvHelmetTags = clone;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

             
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtLoginId.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtTIDP.Text = "";  
            txtAccessCard.Text = ""; 
             
            ClearErrorIconText();  
            LoadMetaData();  
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }

     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     

     
     
     
     
     
     
    public class PasswordPolicy
    {
      private static int Minimum_Length = 2; 
       
      private static int Upper_Case_length = 1;
      private static int Lower_Case_length = 1;
      private static int SpecialPunctuation_length = 1;
      private static int Numeric_length = 1;

       
       
      public static bool IsValid(string Password)
      {
        if (Password.Length < Minimum_Length)
          return false;

        if (!IsNonPrintable(Password)) 
          return false;

         
         
         
         
         
         
         

         
         
         
         
         
         
         

         
         
         
         
         
         
         

         
         
         
         
         
         
         

        return true;
      }

      private static int UpperCaseCount(string Password)
      {
        return Regex.Matches(Password, "[A-Z]").Count;
      }
      private static int LowerCaseCount(string Password)
      {
        return Regex.Matches(Password, "[a-z]").Count;
      }
      private static int NumericCount(string Password)
      {
        return Regex.Matches(Password, "[0-9]").Count;
      }
       
       
       
       
       
       
       
       
       
      private static int SpecialPunctuation(string Password)
      {
         
         
         
        return Regex.Matches(Password, "[!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~]").Count;
         
      }
       
       
       
       
       
       
       
      private static bool IsNonPrintable(string Password)
      {

        if (Regex.Matches(Password, "[\u0000-\u0020]").Count > 0)
          return false;
        else return true;
      }
    }





    private void ClearErrorIconText()  
    {
        
        dxErrorProvider.SetError(txtConfirmPassword, null);
        dxErrorProvider.SetError(txtPassword, null);
        dxErrorProvider.SetError(txtLastName, null);
        dxErrorProvider.SetError(txtFirstName, null);
        dxErrorProvider.SetError(txtLoginId, null);  

        dxErrorProvider.SetError(txtAccessCard, null); 
        dxErrorProvider.SetError(txtTIDP, null);
    }
    private bool Validation()
    {
        bool zResult = false;
        bool zValidationFail = true;
        try
        {
            ClearErrorIconText();  
             
             
             
             
             
             
             
             
             

             
            if (!ValidateProfileSelection())
            {
                MessageBox.Show("Please select a profile to assign", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gvUserProfile.Focus();
                zValidationFail = false;
            }

            if (txtConfirmPassword.Text.Trim() == "")
            {
                 
                 
                 
                dxErrorProvider.SetError(txtConfirmPassword, "Enter a confirm your password");
                txtConfirmPassword.Focus();
                zValidationFail = false;
            }

            if (txtPassword.Text.Trim() == "")
            {
                 
                 
                 
                dxErrorProvider.SetError(txtPassword, "Enter a password to use");
                txtPassword.Focus();
                zValidationFail = false;

            }


             
            if (PasswordPolicy.IsValid(txtPassword.Text) == false) 
            {
                zValidationFail = false;
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
                 
                dxErrorProvider.SetError(txtPassword, "Your password does not match the rules required(2 character min), please try a again");
                dxErrorProvider.SetError(txtConfirmPassword, "Your password does not match the rules required, please try a again");
                txtPassword.Focus();
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                 
                 
                 
                zValidationFail = false;
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
                 
                dxErrorProvider.SetError(txtPassword, "The Password and Confirm Password do not match.  Re-enter the passwords again");
                dxErrorProvider.SetError(txtConfirmPassword, "The Password and Confirm Password do not match.  Re-enter the passwords again");
                txtPassword.Focus();
            }

            if (txtLoginId.Text.Trim() == "")
            {
                dxErrorProvider.SetError(txtLoginId, "Enter the user's unique login ID ");
                txtLastName.Focus();
                zValidationFail = false;
            }

            if (txtLastName.Text.Trim() == "")
            {
                 
                 
                 
                dxErrorProvider.SetError(txtLastName, "Enter the user's last name");
                txtLastName.Focus();
                zValidationFail = false;

            }
            if (txtFirstName.Text.Trim() == "")
            {
                 
                 
                 
                dxErrorProvider.SetError(txtFirstName, "Enter the user's first name");
                txtFirstName.Focus();
                zValidationFail = false;
            }
            
             
            if (txtAccessCard.Text.Trim() == "")
            {
                 
                 
                 
                dxErrorProvider.SetError(txtAccessCard, "Enter the Access Card UID");
                txtAccessCard.Focus();
                zValidationFail = false;
            }

              
            DataTable zUniqueCols = dt_gvHelmetTags.DefaultView.ToTable(true, "TIDP");
            if (zUniqueCols.Rows.Count>1)
            {
                dxErrorProvider.SetError(txtTIDP, "More than 1 TIDP detected!");
                txtTIDP.Focus();
                zValidationFail = false;
            }

            if (dt_gvHelmetTags.Rows.Count == 0)
            {
                dxErrorProvider.SetError(txtTIDP , "Empty Helmet Tag List Error");
                txtTIDP.Focus();
                zValidationFail = false;
            }

            if (dt_gvHelmetTags.Rows.Count > 6)
            {
                dxErrorProvider.SetError(txtTIDP, "Helmet tag should be maximum of 6 tags only.");
                txtTIDP.Focus();
                zValidationFail = false;
            }

             
            if (txtTIDP.Text.Trim() == "")
            {
                 
                 
                 
                dxErrorProvider.SetError(txtTIDP, "Enter the valid helmet UID");
                txtTIDP.Focus();
                zValidationFail = false;
            }


            //if ((txtTIDP.Text.Length % 4 != 0))
            //{
            //    dxErrorProvider.SetError(txtTIDP, "TIDP must be a multiple of 16 bits (4 hex chars)");
            //    txtTIDP.Focus();
            //    zValidationFail = false;
            //}

             
             


            if (m_ISMLoginInfo.ISMServer.ISMAccountExists(txtLoginId.Text.Trim()))
            {
                 
                dxErrorProvider.SetError(txtLoginId, "An account using this User ID already exists ");
                txtLoginId.Focus();
                zValidationFail = false;
            }

             
            if (m_ISMLoginInfo.ISMServer.HQAccessCardExists(0, txtAccessCard.Text.Trim(),""))
            {

                 
                dxErrorProvider.SetError(txtAccessCard, "Access Card already used ");
                txtAccessCard.Focus();
                zValidationFail = false;
            }

             
            if (m_ISMLoginInfo.ISMServer.HQTIDPExists(0, txtTIDP.Text.Trim(),""))
            {
                 
                dxErrorProvider.SetError(txtTIDP, "User Helmet Tag UID already exists ");
                txtTIDP.Focus();
                zValidationFail = false;
            }


            if (zValidationFail)
                zResult = zValidationFail;
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        return zResult;

    }

     
     
     
     
     
     
    private void UpdateISMLoginId(object sender, EventArgs e)
    {
        try
        {
            txtLoginId.Text = txtFirstName.Text + "." + txtLastName.Text;
            txtStatusMsg.Text = "";  
             
            dxErrorProvider.SetError(txtLoginId, null);  
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }

     
     
     
     
     
    private void btnSave_Click(object sender, EventArgs e)
    {
        HQDataExtract extractData = new HQDataExtract();
        try
        {
            

            if (Validation())
            {
                ISMUser.StructUser zUser = new ISMUser.StructUser();
                string zLoginID;  
                zUser.UserLogonID = txtLoginId.Text;
                zUser.UserLastName = txtLastName.Text;
                zUser.UserFirstName = txtFirstName.Text;

                 
                 
                 
                zUser.UserPassword = new ISMPassword().EncryptPassword(zUser.UserLogonID.ToUpper() + txtPassword.Text);   

                zUser.UserStartDate = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
                zUser.UserCreateUserID = m_ISMLoginInfo.LogonID;
                zUser.UserCreateDate = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
                zUser.UserUpdateUserID = m_ISMLoginInfo.LogonID;
                zUser.UserUpdateDateTime = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");

                 
                zUser.UserAccessCardUID = txtAccessCard.Text;
                zUser.UserTIDP = txtTIDP.Text;

                 
                zUser.UserProfileCode = "";
                 
                 
                 
                 
                 

                 


                 
                /* MR OUT 04-MAR-11
                if (m_ISMLoginInfo.ISMServer.ISMAccountExists(zUser.UserLogonID) == false)  
                {
                   
                  if (m_ISMLoginInfo.ISMServer.AddNewISMUser(zUser) == true)   
                  {
                      zLoginID = txtLoginId.Text.Trim();
                    
                    
                    ClearControls();
                    txtStatusMsg.Text = "Login ID " + zLoginID + " has been created";  
                  }
                }
                else
                {
                  MessageBox.Show(String.Format("An account for {0} already exists.\n", ISMUser.UserLogonID), lblHeader.Text, MessageBoxButtons.OK);
                }*/

                 
                string zMessage = String.Format("Do you want create User ID {0} ?", txtLoginId.Text);  
                 
                 
                DialogResult zReply = MessageBox.Show(zMessage, lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);  

                if (zReply == DialogResult.Yes)  
                {
                    long AUserID = 0;
                    if (m_ISMLoginInfo.ISMServer.AddNewISMUser(zUser, ref AUserID) == true)   
                    {
                        zLoginID = txtLoginId.Text.Trim();
                         
                         
                        if (AUserID > 0)
                        {
                             
                            CreateProfile(AUserID,zUser.UserLogonID); 
                        }

                         
                        if (AUserID > 0)
                        {
                             
                            CreateHelmetTagList(AUserID, zUser.UserLogonID);
                             
                            btnDisconnect_Click(sender, e);                       
                        }

                       
                        txtStatusMsg.Text = "Login ID " + zLoginID + " has been created";
                       // extractData.PerformPesonelDataExtract(ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),false);
                        applicationLogs.InsertApplicationLogs(
                        ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                        "User",
                        m_ISMLoginInfo.ISMServer.CurrentLoggedInUser.ToString(), 
                        "Success",
                        "Adding",
                        txtLoginId.Text,
                        "HQ",
                        "");
                        ClearControls();

                    }
                    else
                    {
                        applicationLogs.InsertApplicationLogs(
                       ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                       "User",
                       m_ISMLoginInfo.ISMServer.CurrentLoggedInUser.ToString(),
                       "Failed",
                       "Adding",
                       txtLoginId.Text,
                       "HQ",
                       "");
                    }
                }
            }
            else
            {
                applicationLogs.InsertApplicationLogs(
                       ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                       "User",
                       m_ISMLoginInfo.ISMServer.CurrentLoggedInUser.ToString(),
                       "Failed",
                       "Adding",
                       txtLoginId.Text,
                       "HQ",
                       "");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }

     
     
     
     
     
    private void btnClear_Click(object sender, EventArgs e)
    {
      ClearControls();
    }

    private void txtLastName_Leave(object sender, EventArgs e)  
    {
        try
        {
            if (txtLoginId.Text.Trim() != "")
            {

                if (m_ISMLoginInfo.ISMServer.ISMAccountExists(txtLoginId.Text.Trim()))
                {  
                    
                    dxErrorProvider.SetError(txtLoginId, "An account using this User ID already exists ");  
                    txtLoginId.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }
     
    private bool ValidateProfileSelection()
    {
        bool zResult = false;
        try
        {
            int zCount = 0;
            while (zCount < gridView.RowCount)
            {
                DataRow row = gridView.GetDataRow(zCount);
                if ((row != null))
                {
                    if (row["SELECT"].ToString() == "True")
                    {
                        zResult = true;
                        return zResult;
                    }
                }
                zCount += 1;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        return zResult;

    }

    private void CreateHelmetTagList(long AUserID, string AUserLogonID) 
    {
        try
        {
            int zCount = 0;
            bool zResult = false;
            string zJournlaDesc = "Helmet Tag(s): ";
            DateTime zDeactivateTime = DateTime.Now;
            while (zCount < gridViewUIDList.RowCount)
            {
                DataRow row = gridViewUIDList.GetDataRow(zCount);
                if ((row != null))
                {
                    if (row[0].ToString().ToUpper() == "FALSE")
                    {
                         
                        string zProfileDescription = row[2].ToString();
                        zJournlaDesc += zProfileDescription + ", ";

                        DataTable zdt=new DataTable();
                        m_ISMLoginInfo.ISMServer.HQTIDHelmetTools(1, row[1].ToString(), row[2].ToString(), AUserID, ref zdt);
                        zResult = true;
                    }
                }
                zCount += 1;
            }
            if (zResult)
            {
                int zLength = zJournlaDesc.Trim().Length;
                if (zLength > 0)
                    zJournlaDesc = zJournlaDesc.Remove(zLength - 1);  

                 
                zJournlaDesc += " has been assigned for User:" + AUserLogonID; 
                m_ISMLoginInfo.AddToJournal("T", zJournlaDesc, "SEC111");
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }



     
    private void CreateProfile(long AUserID,string AUserLogonID) 
    {
        try
        {
            int zCount = 0;
            bool zResult = false;
            string zJournlaDesc = "User Profile(s): ";
            DateTime zDeactivateTime = DateTime.Now;
            while (zCount < gridView.RowCount)
            {
                DataRow row = gridView.GetDataRow(zCount);
                if ((row != null))
                {
                    if (row["SELECT"].ToString() == "True")
                    {
                        int zMod = 0;  
                        string zProfileCode = row["CODE"].ToString();
                        string zProfileDescription = row["DESCRIPTION"].ToString();
                        zJournlaDesc += zProfileDescription + ", ";
                        m_ISMLoginInfo.ISMServer.AddOrDeleteUserProfile(zMod, zProfileCode, AUserID, zDeactivateTime);
                        zResult = true;
                    }
                }
                zCount += 1;
            }
            if (zResult)
            {
                int zLength = zJournlaDesc.Trim().Length;
                if (zLength > 0)
                    zJournlaDesc = zJournlaDesc.Remove(zLength - 1);  

                 
                zJournlaDesc += " has been assigned for User:" + AUserLogonID; 
                m_ISMLoginInfo.AddToJournal("T", zJournlaDesc, "SEC111"); 
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    private void txtFirstName_TextChanged(object sender, EventArgs e)
    {
        dxErrorProvider.SetError(txtFirstName, null);
    }

    private void txtLastName_TextChanged(object sender, EventArgs e)
    {
        dxErrorProvider.SetError(txtLastName, null);
    }

    private void txtPassword_TextChanged(object sender, EventArgs e)
    {
        dxErrorProvider.SetError(txtPassword, null);
        dxErrorProvider.SetError(txtConfirmPassword, null);
    }

    private void gridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
    {
        GridView view = sender as GridView;
        if (view.FocusedColumn.FieldName == "DESCRIPTION")

            e.Cancel = true;
    }

    private void txtLoginId_EditValueChanged(object sender, EventArgs e)  
    {
        dxErrorProvider.SetError(txtLoginId, null);
    }
     




     
    #region
    private void UHFReaderPanelSetup()
    {
        clsCrossThreadCalls.SetAnyProperty(lblUHFReaderIP, "BackColor", Color.Red);
        lblVerification.Text = "";
        m_DefautltColor = lblVerification.BackColor;
         

        trackBar.Value = 10;
        txtTxPower.Text = trackBar.Value.ToString();
         
        lblUHFReaderIP.Text = SHSHQ.Properties.Settings.Default.UHFReaderIP;
         
    }

    private string GetFormattedTag(string tag)
    {
        try
        {
            var newGenerated = "";

            for (int i = 0; i < tag.Length; )
            {
                newGenerated += tag.Slice(ref i, 4).PadRight(4, '0');
                if (i < tag.Length)
                    newGenerated += "-";
            }

            return newGenerated;
        }
        catch (Exception)
        {
            return tag;
        }
    }

     
    #region Set Message
     
     
     
     
    private void SetCurrentTag(string text)
    {
        if (InvokeRequired)
            BeginInvoke(new Action<string>(SetCurrentTag), text);
        else
             
            txtTIDP.Text = text;
    }

     
     
     
     
    private void SetStatusMessage(string text)
    {
        if (InvokeRequired)
            BeginInvoke(new Action<string>(SetStatusMessage), text);
        else
            txtStatusMsg.Text = text;
    }

     
     
     
     
     
     
     
     
     
     
     
    #endregion Set Message
    private int GetCmbMemorySelectedIndex()
    {
        return 0;
    }
    private bool AdjustUHFReaderAntPower()
    {
        try
        {
            if (_isConnectedReader)
            {
                Settings settings = _impinjReader.QueryDefaultSettings();
                StopReader();
                settings.Report.IncludeFastId = true;
                settings.Report.IncludePcBits = true;
                settings.Report.Mode = ReportMode.Individual;

                 
                 
                settings.Antennas.GetAntenna(1).IsEnabled = true;
                settings.Antennas.GetAntenna(1).PortNumber = 1;
                settings.Antennas.GetAntenna(1).RxSensitivityInDbm = -70;
                settings.Antennas.GetAntenna(1).TxPowerInDbm = double.Parse(trackBar.Value.ToString());
                _impinjReader.ApplySettings(settings);
                StartReader();
            }
            else
            {
                SetStatusMessage("Helmet Reader Adjust Power-Reader is not connected!");
            }
            return true;
        }
        catch (Exception ex)
        {
            ex.ToString();
            SetStatusMessage("Helmet Reader Adjust Power-"+ex.ToString());
             
            Cursor.Current = Cursors.Default;
            return false;
        }
    }

    private bool InitializeUHFReader()
    {
        try
        { 
            IPAddress ipAddress = System.Net.IPAddress.Parse(lblUHFReaderIP.Text.ToString());
            var pingreply = new Ping().Send(ipAddress);

             

            if (pingreply.Status == IPStatus.Success)
            {
                 
                 
                 
                _impinjReader = new ImpinjReader();
                SetStatusMessage("Please wait... reader is connecting!");
                _impinjReader.Connect(ipAddress.ToString());
                Settings settings = _impinjReader.QueryDefaultSettings();

                if (GetCmbMemorySelectedIndex() == (int)Memory.EPC)
                {
                    _impinjReader.TagsReported += OnTagsReported;
                    settings.Report.IncludeFastId = true;
                    settings.Report.IncludePcBits = true;
                     
                    settings.Report.Mode = ReportMode.Individual;

                     
                     
                    settings.Antennas.GetAntenna(1).IsEnabled = true;
                    settings.Antennas.GetAntenna(1).PortNumber = 1;
                    settings.Antennas.GetAntenna(1).RxSensitivityInDbm = -70;
                    settings.Antennas.GetAntenna(1).TxPowerInDbm = double.Parse(trackBar.Value.ToString());
                    _impinjReader.ApplySettings(settings);
                }
                StartReader();
                SetStatusMessage("Please present RFID Tag...");
                return true;
            }

            clsCrossThreadCalls.SetAnyProperty(lblUHFReaderIP, "BackColor", Color.Red);
            SetStatusMessage("Could not connect to the reader!");
            _isConnectedReader = false;
            return false;
        }
        catch (Exception ex)
        {
            clsCrossThreadCalls.SetAnyProperty(lblUHFReaderIP, "BackColor", Color.Red);
            ex.ToString();
            SetStatusMessage("Could not connect to the reader!");
            _isConnectedReader = false;
            Cursor.Current = Cursors.Default;
            return false;
        }
    }
    private void StartReader()
    {
        try
        {
            if (_impinjReader != null)
            {
                _impinjReader.Start();
                _isConnectedReader = true;
                clsCrossThreadCalls.SetAnyProperty(lblUHFReaderIP, "BackColor", Color.LightGreen);
            }
        }
        catch (Exception)
        {
            SetStatusMessage("Could not start the reader!");
        }
    }
    private void StopReader()
    {
        try
        {
            if (_impinjReader != null && _isConnectedReader)
                _impinjReader.Stop();
        }
        catch (Exception)
        {
            SetStatusMessage("Could not stop the reader!");
        }
    }



    private void CreatNewRow_HelmetUIDList(bool Used, string TIDP, string TIDHelmet)
    {
        try
        {
            DataTable clone = dt_gvHelmetTags.Copy();
            clone.Rows.Add(Used, TIDP, TIDHelmet);
            gvHelmetTags.BeginInvoke(new MethodInvoker(delegate { gvHelmetTags.DataSource = clone.DefaultView; }));
            dt_gvHelmetTags = clone;
        }
        catch (Exception ex)
        {
            MessageBox.Show("New Helmet Tags Error:"+ex.ToString());
        }
    }

    private int GetRowHandleByColumnValue_HelmetUIDlist(GridView view, string ColumnFieldName, object value)
    {
        
        int result = GridControl.InvalidRowHandle;
        for (int i = 0; i < view.RowCount; i++)
            if (view.GetDataRow(i)[ColumnFieldName].Equals(value))
                return i;
        return result;
    }

   
    void OnTagsReported(ImpinjReader sender, TagReport report)
    {

        TagHandling processTag = new TagHandling();
        try
        {
             
            Tag tag = report.Tags[0];
            string zEPCData = processTag.GetTagGroup (tag.Epc.ToHexWordString().Replace(" ", "").ToString(),  false);//tag.Epc.ToHexWordString().Replace(" ", "").ToString()
            string zTIDData = processTag.GetTagInvidualNumber(tag.Epc.ToHexWordString().Replace(" ", "").ToString()); //tag.Tid.ToHexWordString().Replace(" ", "").ToString(); 
            _currentPcBits = tag.PcBits;

             

             
            {
                _currentTag = zEPCData;
                _currentPcBits = tag.PcBits;
                SetCurrentTag(_currentTag);

                clsCrossThreadCalls.SetTextProperty(txtTIDP, zEPCData);

                 
                if(zEPCData !="" && zTIDData!="")
                {
                    DataTable zdt =new DataTable();
                    bool zUsed = mUserCreateLoginInfor.ISMServer.HQTIDHelmetTools(0, zEPCData, zTIDData, 0, ref zdt);
                     
                     

                    if(dt_gvHelmetTags.Select("TIDHelmet='"+zTIDData+"'").Length<1)
                    {


                        CreatNewRow_HelmetUIDList(zUsed, zEPCData, zTIDData);
                    }
                    else
                    {
                        
                    }
                }
            }

        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.ToString());
        }
    }
    void OnKeepaliveReceived(ImpinjReader reader)
    {
        try
        {
            SetStatusMessage("Keepalive received from " + reader.Name + " - " + reader.Address);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.ToString());
        }

    }
    void OnConnectionLost(ImpinjReader reader)
    {
        try
        {
            SetStatusMessage("Keepalive received from " + reader.Name + " - " + reader.Address);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.ToString());
        }
    }

    private void btnConnect_Click(object sender, EventArgs e)
    {
        try
        {
            Cursor.Current = Cursors.WaitCursor;
            _currentTag = string.Empty;
            if (InitializeUHFReader())
            {
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
            }

            Cursor.Current = Cursors.Default;
             
             
        }
        catch (Exception)
        {
            clsCrossThreadCalls.SetAnyProperty(lblUHFReaderIP, "BackColor", Color.Red);
            Cursor.Current = Cursors.Default;
            SetStatusMessage("Unable to Connect Reader");
        }
    }
    private bool DisposeUHFReader()
    {
        try
        {
             
            if (GetCmbMemorySelectedIndex() == (int)Memory.EPC)
            {
                _impinjReader.TagsReported -= OnTagsReported;
            }

            if (_impinjReader != null && _isConnectedReader)
            {
                
                _impinjReader.Stop();
                _impinjReader.Disconnect();
                clsCrossThreadCalls.SetAnyProperty(lblUHFReaderIP, "BackColor", Color.Red);
            }
            return true;
        }
        catch (Exception)
        {
            SetStatusMessage("Could not disconnect to the reader!");
            return false;
        }
    }

    private void btnDisconnect_Click(object sender, EventArgs e)
    {
        try
        {
            if (DisposeUHFReader())
            {
                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
            }
            SetStatusMessage("Reader is disconnected");
            _currentTag = string.Empty;
        }
        catch (Exception)
        {
            SetStatusMessage("Unable to Connect Reader");
        }
    }

    private void trackBar_Scroll(object sender, EventArgs e)
    {
        txtTxPower.Text = trackBar.Value.ToString();

        AdjustUHFReaderAntPower(); 
    }

    private void timerVerification_Tick(object sender, EventArgs e)
    {
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
    }

    #endregion


       
      #region
    private void Initilization_MISCReader_TCPIP()
    {
        try
        {
             
            lblMISCReaderIP.Text = SHSHQ.Properties.Settings.Default.MISCReaderIP;
             
            int TCPIPPort = int.Parse(SHSHQ.Properties.Settings.Default.MISCReaderPort);
            string IP = SHSHQ.Properties.Settings.Default.MISCReaderIP;
            mMISCReader = new MISCReader(IP, TCPIPPort);
            mMISCReader.OnReaderConnected += new MISCReader.DelegateConnected(mMISCReader_OnReaderConnected);
            mMISCReader.OnTagDetected += mMISCReader_OnTagDetected;
            mMISCReader.Start(null);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error:" + ex.ToString(), "MISC Reader");
        }
    }

    void mMISCReader_OnReaderConnected(bool IsConnected)
    {
        if(IsConnected)
          clsCrossThreadCalls.SetAnyProperty(lblMISCReaderIP, "BackColor", Color.LightGreen);
        else
          clsCrossThreadCalls.SetAnyProperty(lblMISCReaderIP, "BackColor", Color.Red);
    }

    private void Disposal_MISCReader()
    {
        try
        {
            if (mMISCReader != null)
            {
                mMISCReader.OnReaderConnected -= mMISCReader_OnReaderConnected;
                mMISCReader.OnTagDetected -= mMISCReader_OnTagDetected;
                mMISCReader.Stop(null);
                mMISCReader.Dispose(null);
                mMISCReader = null;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error:" + ex.ToString(), "MISC Reader Disposal");
        }
    }

    void mMISCReader_OnTagDetected(MISCTag Tag)
    {
        clsCrossThreadCalls.SetTextProperty(txtAccessCard, Tag.TagNumber);
         
         
         
    }
    #endregion

    private void txtAccessCard_EditValueChanged(object sender, EventArgs e)
    {
        dxErrorProvider.SetError(txtAccessCard, null);
    }

    private void btnHelmetTagListClear_Click(object sender, EventArgs e)
    {
        
        

       DataTable clone = dt_gvHelmetTags.Copy();
       clone.Rows.Clear();
       gvHelmetTags.BeginInvoke(new MethodInvoker(delegate { gvHelmetTags.DataSource = clone.DefaultView; }));
       dt_gvHelmetTags = clone;

       clsCrossThreadCalls.SetTextProperty(txtTIDP, "");
    }

    private void gcOperatorCreate_EnabledChanged(object sender, EventArgs e)
    {
         

        DisposeUHFReader();
        Disposal_MISCReader();

        mUserCreateLoginInfor = null;
         
    }

    private void gvHelmetTags_DataSourceChanged(object sender, EventArgs e)
    {
        try
        {
            clsCrossThreadCalls.SetTextProperty(lblHelmetTagCount, gridViewUIDList.DataRowCount.ToString());
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    private void picStatus_DoubleClick(object sender, EventArgs e)
    {
        if (txtTIDP.Properties.ReadOnly)
            txtTIDP.Properties.ReadOnly = false;
        else
            txtTIDP.Properties.ReadOnly = true;
    }

  }
}
