

using System;
using System.Data;
using System.Windows.Forms;
using ISMDAL.TableColumnName;
using System.Text.RegularExpressions;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;  

 
using Impinj.OctaneSdk;
using ImpinjWrite.Models;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Globalization;

 
using MISCReaderTCPIP;
using System.Configuration;
using AMMO_BG_DLL.Background;

 
using SHSHQ.Tools;

namespace ISM.Modules
{

  public partial class UserUpdate : ISMBaseWorkSpace
  {
      UserUpdateMode m_UserUpdateMode = UserUpdateMode.IndividualMode; 

       
      long mCurrentSelectedUserId = -1;

       
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

      ISMLoginInfo mUserUpdateLoginInfor;
      Logs applicationLogs = new Logs();


      private DataTable dt_gvHelmetTags = new DataTable();

     bool m_CellValueChanged = false;
    public UserUpdate(ISMLoginInfo AISMLoginInfo)
      : base(AISMLoginInfo)
    {
      InitializeComponent();

      mUserUpdateLoginInfor = new ISMLoginInfo();
      mUserUpdateLoginInfor.ISMServer = new ISMDAL.DAL(SHSHQ.Properties.Settings.Default.ConnectionString);
      mUserUpdateLoginInfor.ISMServer.CurrentLoggedInUser = AISMLoginInfo.LogonID;
      mUserUpdateLoginInfor.UserID = AISMLoginInfo.UserID;
      mUserUpdateLoginInfor.LogonID = AISMLoginInfo.LogonID;

      rdbIndividual.Checked = false; 
      rdbBulkUpdateMode.Checked = true;
    }

    private void OperatorUpdate_Load(object sender, EventArgs e)
    {

       
      luLoginId.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLogonID, 240, "User ID"),  
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserFirstName, 180,"First Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLastName, 180,"Last Name")});

       
      luLoginId.Properties.DisplayMember = ISMUser.UserLogonID;
      luLoginId.Properties.ValueMember = ISMUser.UserID;

       
       
       
       
       

       
       
       

      LoadOperatorMetaData();
       
      SetGridCaption();

      UHFReaderPanelSetup(); 

      Initilization_MISCReader_TCPIP(); 

    }

    private void LoadOperatorMetaData()
    {
      try
      {
         
        DataSet ds = m_ISMLoginInfo.ISMServer.ISMUserGetRecds();  
        if (ds != null)
          luLoginId.Properties.DataSource = ds.Tables[0].DefaultView;
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void LoadOperatorTIDHelmetList(long AUserID)
    {
        try
        {            
            DataTable dt = new DataTable();
            if(m_ISMLoginInfo.ISMServer.HQTIDHelmetTools(2,"","",AUserID,ref dt))
            {
                gvHelmetTags.DataSource = dt.DefaultView;  
            }
        }
        catch (Exception ex)
        { 
            MessageBox.Show(string.Format("System Error: LoadMetaData\n{0:s}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }


    private void LoadOperatorProfileMetaData(long AUserID)
    {
      try
      {
         
          DataSet ds = m_ISMLoginInfo.ISMServer.GetUpdateUserMetaData(AUserID);  
        if (ds != null)
        {
            gvUserProfile.DataSource = ds.Tables[0].DefaultView;  
           
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(string.Format("System Error: LoadMetaData\n{0:s}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void luLoginId_EditValueChanged(object sender, EventArgs e)
    {
        mCurrentSelectedUserId = -1; 
      dxErrorProvider.SetError(luLoginId, null);  
      if (luLoginId.EditValue != null)
      {
          DevExpress.XtraEditors.LookUpEdit zEditor = (sender as DevExpress.XtraEditors.LookUpEdit);
          DataRowView zDataRow = zEditor.Properties.GetDataSourceRowByKeyValue(zEditor.EditValue) as DataRowView;
          txtStatusMsg.Text = "";  
          long zUserID = long.Parse(luLoginId.EditValue.ToString());  
          mCurrentSelectedUserId = zUserID; 

          if (zDataRow != null)   
          {
              txtFirstName.Text = Convert.ToString(zDataRow[2]);
              txtLastName.Text = Convert.ToString(zDataRow[3]);

              txtAccessCard.Text = Convert.ToString(zDataRow[5]); 
              txtEncoding.Text = txtTIDP.Text = Convert.ToString(zDataRow[6]); 
              
              
              LoadOperatorProfileMetaData(zUserID);
               
              LoadOperatorTIDHelmetList(zUserID);
          }
      }
      else
          gvUserProfile.DataSource = null;
    }

    private void btnClear_Click(object sender, EventArgs e)
    {

        mCurrentSelectedUserId = -1;
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

        try
        {
             
            gvHelmetNew.DataSource = null;
        }
        catch (Exception ex)
        {
            ex.ToString();
        }

        rdbIndividual.Checked = false;
        rdbBulkUpdateMode.Checked = true;

        txtEncoding.Text = ""; 
        txtCurrTag.Text = ""; 
      luLoginId.EditValue = null;
      txtFirstName.Text = "";
      txtLastName.Text = "";
      txtPassword.Text = "";  
      txtConfirmPassword.Text = "";  
      txtTIDP.Text = "";  
      txtAccessCard.Text = ""; 
      ClearErrorIconText();  
      gvUserProfile.DataSource = null;  
      m_CellValueChanged = false;
    }
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     

     
     
     
     
     
     
    public class PasswordPolicy
    {
        private static int Minimum_Length = 2; 
         
        //private static int Upper_Case_length = 1;
        //private static int Lower_Case_length = 1;
        //private static int SpecialPunctuation_length = 1;
        //private static int Numeric_length = 1;

         
         
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
        dxErrorProvider.SetError(luLoginId, null);
        dxErrorProvider.SetError(txtPassword, null);
        dxErrorProvider.SetError(txtConfirmPassword, null);

        dxErrorProvider.SetError(txtAccessCard, null); 
        dxErrorProvider.SetError(txtTIDP, null);

        dxErrorProvider.SetError(txtEncoding, null);
        dxErrorProvider.SetError(txtCurrTag, null);


    }
    private bool Validation()
    {
        SetStatusMessage("");

        bool zResult = false;
        bool zValidationFail = true;
        ClearErrorIconText();  

         
        if (!ValidateProfileSelection())
        {
            MessageBox.Show("Please select at lease one profile", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            gvUserProfile.Focus();
            zValidationFail = false;

        }
         
        
         
         
         
         
         
         
         
         

         
         
         
         
         
         
         

         
         
        if (txtConfirmPassword.Text.Trim() != "" && txtPassword.Text.Trim() != "")  
        {
            if (txtConfirmPassword.Text.Trim() == "")
            {
                 
                dxErrorProvider.SetError(txtConfirmPassword, "Confirm your password");
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
                 
                dxErrorProvider.SetError(txtPassword, "Your password does not match the rules required(2 character min), please try again");  
                dxErrorProvider.SetError(txtConfirmPassword, "Your password does not match the rules required, please try again");  
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
        }


        if (luLoginId.EditValue == null)  
        {
            dxErrorProvider.SetError(luLoginId, "Select the Login ID");
            luLoginId.Focus();
            zValidationFail = false;
        }

        ISMUser.StructUser zUser = new ISMUser.StructUser();
        DataRowView zDataRow = luLoginId.Properties.GetDataSourceRowByKeyValue(luLoginId.EditValue) as DataRowView;
        if (zDataRow != null)   
        {
            zUser.UserID = Convert.ToInt64(zDataRow[0]);
            zUser.UserLogonID = Convert.ToString(zDataRow[1]);

             
            if (m_ISMLoginInfo.ISMServer.HQAccessCardExists(1, txtAccessCard.Text.Trim(),zUser.UserLogonID))
            {
                 
                dxErrorProvider.SetError(txtAccessCard, "Access Card already used ");
                txtAccessCard.Focus();
                zValidationFail = false;
            }

             
            if (m_ISMLoginInfo.ISMServer.HQTIDPExists(1, txtTIDP.Text.Trim(), zUser.UserLogonID))
            {
                 
                dxErrorProvider.SetError(txtTIDP, "User Helmet Tag UID already exists ");
                txtTIDP.Focus();
                zValidationFail = false;
            }
        }

  
        if (m_CellValueChanged)
        {

        }
        if (zValidationFail)
            zResult = zValidationFail;
        return zResult;
    }

     
    private void btnSave_Click(object sender, EventArgs e)
    {
        HQDataExtract extractData = new HQDataExtract();
        if (Validation())  
        {
            string zLoginID;  
            int zMode;  
            ISMUser.StructUser zUser = new ISMUser.StructUser();
            DataRowView zDataRow = luLoginId.Properties.GetDataSourceRowByKeyValue(luLoginId.EditValue) as DataRowView;
            if (zDataRow != null)   
            {
                zUser.UserID = Convert.ToInt64(zDataRow[0]);
                zUser.UserLogonID = Convert.ToString(zDataRow[1]);
            }

            zUser.UserUpdateUserID = m_ISMLoginInfo.LogonID;
            zUser.UserUpdateDateTime = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");

            zUser.UserTIDP = txtTIDP.Text; 
            zUser.UserAccessCardUID = txtAccessCard.Text; 


             
             
             
             
             
            zUser.UserProfileCode = "";  
            if (txtConfirmPassword.Text.Trim() != "" && txtPassword.Text.Trim() != "")  
            {
                zUser.UserPassword = new ISMPassword().EncryptPassword(zUser.UserLogonID.ToUpper() + txtPassword.Text);   
                zMode = 0;
            }
            else
            {
                zMode = 1;
                zUser.UserPassword = "";
            }

             
            string zMessage = String.Format("Do you want update User ID {0} ?", zUser.UserLogonID);  
            DialogResult zReply = MessageBox.Show(zMessage, " User ID Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);  
            if (zReply == DialogResult.Yes)  
            {
                if (m_ISMLoginInfo.ISMServer.UpdateISMUser(zUser, zMode) == true)   
                {

                     
                     
                    zLoginID = luLoginId.Text.Trim();
                    if (m_CellValueChanged)
                        CreateProfile(zUser.UserID,zUser.UserLogonID); 
                          

                    if (m_UserUpdateMode == UserUpdateMode.BulkUpdateMode)
                    {
                         
                        if (zUser.UserID > 0)
                        {
                             
                            DataTable zdt = new DataTable();
                            m_ISMLoginInfo.ISMServer.HQTIDHelmetTools(5, "", "", zUser.UserID, ref zdt);

                             
                            CreateHelmetTagList(zUser.UserID, zUser.UserLogonID);
                             
                            btnDisconnect_Click(sender, e);
                        }
                    }

                    applicationLogs.InsertApplicationLogs(
                       ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                       "User",
                     m_ISMLoginInfo.ISMServer.CurrentLoggedInUser.ToString(),
                       "Success",
                       "Update",
                      luLoginId.Text.ToString(),
                       "HQ",
                       "");
                    btnClear.PerformClick();
                    LoadOperatorMetaData(); 
                    txtStatusMsg.Text = "Login ID " + zLoginID + " has been updated";
                    //extractData.PerformPesonelDataExtract(ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),false);


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
                      "Update",
                     luLoginId.Text.ToString(),
                      "HQ",
                      "");
        }
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
            string[] zFieldNames_Helmet = new string[] {"TIDP", "TIDHelmet" };

            DevExpress.XtraGrid.Columns.GridColumn zColum_Helmet;
            ColView_Helmet.Columns.Clear();
            for (int i = 0; i < zFieldNames_Helmet.Length; i++)
            {
                zColum_Helmet = ColView_Helmet.Columns.AddField(zFieldNames_Helmet[i]);
                zColum_Helmet.VisibleIndex = i;
            }

            gridViewUIDList.Columns[0].Caption = "Group Number";
            gridViewUIDList.Columns[0].Width = 200;

            gridViewUIDList.Columns[1].Caption = "Individual Number";
            gridViewUIDList.Columns[1].Width = 200;
             

             
            ColumnView ColView_HelmetNEW = gvHelmetNew.MainView as ColumnView;
            string[] zFieldNames_HelmetNEW = new string[] { "Used", "TIDP", "TIDHelmet" };

            DevExpress.XtraGrid.Columns.GridColumn zColum_HelmetNEW;
            ColView_HelmetNEW.Columns.Clear();
            for (int i = 0; i < zFieldNames_HelmetNEW.Length; i++)
            {
                zColum_HelmetNEW = ColView_HelmetNEW.Columns.AddField(zFieldNames_HelmetNEW[i]);
                zColum_HelmetNEW.VisibleIndex = i;
            }
            gridViewNewUIDs.Columns[0].Caption = "Used";
            gridViewNewUIDs.Columns[0].Width = 60;
            gridViewNewUIDs.Columns[0].Visible = false;


            gridViewNewUIDs.Columns[1].Caption = "Group Number";
            gridViewNewUIDs.Columns[1].Width = 230;

            gridViewNewUIDs.Columns[2].Caption = "Individual Number";
            gridViewNewUIDs.Columns[2].Width = 230;

            dt_gvHelmetTags.Columns.Add("Used", typeof(bool));
            dt_gvHelmetTags.Columns.Add("TIDP", typeof(string));
            dt_gvHelmetTags.Columns.Add("TIDHelmet", typeof(string));
        }
        catch
        {
            MessageBox.Show("System Error. Contact System Administrator", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    private void gridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
    {
        GridView view = sender as GridView;
        if (view.FocusedColumn.FieldName == "DESCRIPTION")

            e.Cancel = true;
    }

    private void gridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
    {
        m_CellValueChanged = true;
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
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        return zResult;

    }

     
    private void CreateProfile(long AUserID,string AUserLogonID) 
    {
        try
        {
            int zCount = 0;
            bool zResult = false;
            string zJournlaDesc = "User Profile(s): ";
            int zMod = 1;  
            DateTime zDeactivateTime = DateTime.Now;
            m_ISMLoginInfo.ISMServer.AddOrDeleteUserProfile(zMod, "", AUserID, zDeactivateTime);
            while (zCount < gridView.RowCount)
            {
                DataRow row = gridView.GetDataRow(zCount);
                if ((row != null))
                {
                    if (row["SELECT"].ToString() == "True")
                    {
                        zMod = 0;  
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
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    private void txtPassword_EditValueChanged(object sender, EventArgs e)
    {
        dxErrorProvider.SetError(txtPassword, null);
        dxErrorProvider.SetError(txtConfirmPassword, null); 
    }
     

     
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
        if (IsConnected)
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

     

     
    #region

    private void UHFReaderPanelSetup()
    {
        clsCrossThreadCalls.SetAnyProperty(lblUHFReaderIP, "BackColor", Color.Red);

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
     
     
     
     
     
     
     
     
     
     
     
     

     
     
     
     
    private void SetStatusMessage(string text)
    {
        if (InvokeRequired)
            BeginInvoke(new Action<string>(SetStatusMessage), text);
        else
            txtStatusMsg.Text = text;
    }

    #endregion Set Message
    #region Get Message
    private int GetCmbMemorySelectedIndex()
    {
         
         

         

        return 0;
    }
    #endregion Get Message     

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
            SetStatusMessage("Helmet Reader Adjust Power-" + ex.ToString());
             
            Cursor.Current = Cursors.Default;
            return false;
        }
    }

    private bool InitializeReader()
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
                    _impinjReader.TagOpComplete += OnTagOpComplete;
                    settings.Report.IncludeFastId = true;
                    settings.Report.IncludePcBits = true;

                     
                     

                     
                     
                    settings.Antennas.GetAntenna(1).IsEnabled = true;
                    settings.Antennas.GetAntenna(1).PortNumber = 1;
                    settings.Antennas.GetAntenna(1).RxSensitivityInDbm = -70;
                    settings.Antennas.GetAntenna(1).TxPowerInDbm = double.Parse(trackBar.Value.ToString());
                    _impinjReader.ApplySettings(settings);
                }


                StartReader();
                SetStatusMessage("Please present new helmet tag...");
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


    void OnTagsReported(ImpinjReader sender, TagReport report)
    {
        TagHandling processTag = new TagHandling();
        try
        {
             
            Tag tag = report.Tags[0];
            //string zEPCData = tag.Epc.ToHexWordString().Replace(" ", "").ToString();
            //string zTIDData = tag.Tid.ToHexWordString().Replace(" ", "").ToString(); 
            string zEPCData = processTag.GetTagGroup(tag.Epc.ToHexWordString().Replace(" ", "").ToString(), false);//tag.Epc.ToHexWordString().Replace(" ", "").ToString()
            string zTIDData = processTag.GetTagInvidualNumber(tag.Epc.ToHexWordString().Replace(" ", "").ToString()); //tag.Tid.ToHexWordString().Replace(" ", "").ToString(); 
 
            _currentPcBits = tag.PcBits;

             

             
            {
                _currentTag = zEPCData;
                _currentPcBits = tag.PcBits;

                clsCrossThreadCalls.SetTextProperty(txtCurrTag, zEPCData);

                if (m_UserUpdateMode == UserUpdateMode.BulkUpdateMode)
                {
                    clsCrossThreadCalls.SetTextProperty(txtTIDP, zEPCData);
                }
                else
                {
                    clsCrossThreadCalls.SetTextProperty(txtCurrTag, zEPCData);
                }

                 
                if (zEPCData != "" && zTIDData != "")
                {
                    DataTable zdt = new DataTable();
                    bool zUsed = mUserUpdateLoginInfor.ISMServer.HQTIDHelmetTools(0, zEPCData, zTIDData, 0, ref zdt);
                     
                     

                    if (dt_gvHelmetTags.Select("TIDHelmet='" + zTIDData + "'").Length < 1)
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


    private void CreateHelmetTagList(long AUserID, string AUserLogonID) 
    {
        try
        {
            int zCount = 0;
            bool zResult = false;
            string zJournlaDesc = "Helmet Tag(s): ";
            DateTime zDeactivateTime = DateTime.Now;
            while (zCount < gridViewNewUIDs.RowCount)
            {
                DataRow row = gridViewNewUIDs.GetDataRow(zCount);
                if ((row != null))
                {
                    if (row[0].ToString().ToUpper() == "FALSE")
                    {
                         
                        string zProfileDescription = row[2].ToString();
                        zJournlaDesc += zProfileDescription + ", ";

                        DataTable zdt = new DataTable();
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

    private void CreatNewRow_HelmetUIDList(bool Used, string TIDP, string TIDHelmet)
    {
        try
        {
            DataTable clone = dt_gvHelmetTags.Copy();
            clone.Rows.Add(Used, TIDP, TIDHelmet);
            gvHelmetNew.BeginInvoke(new MethodInvoker(delegate { gvHelmetNew.DataSource = clone.DefaultView; }));
            dt_gvHelmetTags = clone;
        }
        catch (Exception ex)
        {
            MessageBox.Show("New Tag In the Helmet Tag List Error:"+ex.ToString());
            ex.ToString();
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
    void OnTagOpComplete(ImpinjReader reader, TagOpReport report)
    {
         
        foreach (TagOpResult result in report)
        {
             
            if (result is TagWriteOpResult)
            {
                 
                TagWriteOpResult writeResult = result as TagWriteOpResult;
                if (writeResult.OpId == EPC_OP_ID)
                {
                    if (writeResult.Result == WriteResultStatus.NoResponseFromTag)
                    {
                        SetStatusMessage("Write to EPC Fail.  Please increase Antenna Power Or Adjust Tag Position ");
                    }
                    else if (writeResult.Result == WriteResultStatus.InsufficientPower)
                    {
                        SetStatusMessage("Write to EPC Fail.  Please increase Antenna Power");
                    }
                    else if (writeResult.Result == WriteResultStatus.NonspecificTagError)
                    {
                        SetStatusMessage("Write to EPC Fail.  Non specific Tag Error");
                    }
                    else if (writeResult.Result == WriteResultStatus.Success)
                    {
                        SetStatusMessage("Write to EPC complete - " + writeResult.Result);
                         
                         
                         
                        string zTIDData =result.Tag.Tid.ToHexWordString().Replace(" ", "").ToString(); 
                        DataTable zdt = new DataTable();
                        if (m_ISMLoginInfo.ISMServer.HQTIDHelmetTools(1, txtEncoding.Text, zTIDData, mCurrentSelectedUserId, ref zdt))
                        {
                             
                            SetStatusMessage("New Helmet Tag Encoded Successfully!");
                             
                            btnHelmetTagListClear_Click_1(null, null);
                        }
                        else
                        {
                           SetStatusMessage("New Helmet Tag Association failed");
                        }
                    }
                }
                else if (writeResult.OpId == PC_BITS_OP_ID)
                {
                    if (writeResult.Result == WriteResultStatus.Success)
                    {
                        SetStatusMessage("Write to EPC complete  " + writeResult.Result);
                         
                    }
                }
                else if (writeResult.OpId == USER_Write_ID)
                {
                    if (writeResult.Result == WriteResultStatus.NoResponseFromTag)
                    {
                        SetStatusMessage("Write to USER Fail.  Please increase Antenna Power  Or Adjust Tag Position ");
                    }
                    else if (writeResult.Result == WriteResultStatus.InsufficientPower)
                    {
                        SetStatusMessage("Write to USER Fail.  Please increase Antenna Power");
                    }
                    else if (writeResult.Result == WriteResultStatus.NonspecificTagError)
                    {
                        SetStatusMessage("Write to USER Fail.  Non specific Tag Error");
                    }
                    else if (writeResult.Result == WriteResultStatus.Success)
                    {
                        SetStatusMessage("Write to USER complete - " + writeResult.Result);
                        m_TagVeify = true;
                         
                    }
                }
                break;
            }
            else if (result is TagReadOpResult)
            {
                if (GetCmbMemorySelectedIndex() == (int)Memory.UserMemory)
                {
                     
                    TagReadOpResult readResult = result as TagReadOpResult;

                    string zEPCData = readResult.Data.ToHexWordString().Replace(" ", "").ToString();
                    _currentPcBits = readResult.Tag.PcBits;

                    if (_currentTag != zEPCData)
                    {
                        _currentTag = zEPCData;
                        _currentPcBits = readResult.Tag.PcBits;
                         
                        if (readResult.OpId == USER_OP_ID)
                        {
                            zEPCData = readResult.Data.ToHexWordString().Replace(" ", "").ToString();
                            if ("24".ToString().Trim().Length > 0)
                            {
                                int zLength = int.Parse("24".ToString().Trim());
                                zEPCData = zEPCData.Substring(0, zLength);
                            }
                        }
                    }
                }
            }
        }
    }
     
    private void WriteEPCMemeory()
    {
        try
        {

            if ((txtCurrTag.Text.Length % 4 != 0) || (txtEncoding.Text.Length % 4 != 0))
            {
                SetStatusMessage("EPCs must be a multiple of 16 bits (4 hex chars)");
                return;
            }
             
            txtTIDP.Text = txtEncoding.Text.Trim();
             
            TagOpSequence seq = new TagOpSequence();

             
            seq.TargetTag.MemoryBank = MemoryBank.Epc;
            seq.TargetTag.BitPointer = BitPointers.Epc;
            seq.TargetTag.Data = txtCurrTag.Text.Trim();

            TagWriteOp writeEpc = new TagWriteOp();
             
            writeEpc.Id = EPC_OP_ID;
             
            writeEpc.MemoryBank = MemoryBank.Epc;
             
            writeEpc.Data = TagData.FromHexString(txtEncoding.Text.Trim());
             
            writeEpc.WordPointer = WordPointers.Epc;

             
            seq.Ops.Add(writeEpc);
            if (txtCurrTag.Text.Trim().Length != txtEncoding.Text.Trim().Length)
            {
                 
                 

                 
                ushort newEpcLenWords = (ushort)(txtEncoding.Text.Length / 4);
                ushort newPcBits = PcBits.AdjustPcBits(_currentPcBits, newEpcLenWords);

                TagWriteOp writePc = new TagWriteOp();
                writePc.Id = PC_BITS_OP_ID;
                 
                writePc.MemoryBank = MemoryBank.Epc;
                 
                writePc.Data = TagData.FromWord(newPcBits);
                 
                writePc.WordPointer = WordPointers.PcBits;

                 
                seq.Ops.Add(writePc);
            }

             
             
            _impinjReader.AddOpSequence(seq);
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
            InitializeReader();
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
                _impinjReader.TagOpComplete -= OnTagOpComplete;
            }
            else
            {
                _impinjReader.TagOpComplete -= OnTagOpComplete;
            }

            if (_impinjReader != null && _isConnectedReader)
            {
                _impinjReader.QueryStatus();
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
            DisposeUHFReader();
            SetStatusMessage("Reader is disconnected");
            _currentTag = string.Empty;
        }
        catch (Exception)
        {
            SetStatusMessage("Unable to Connect Reader");
        }
    }

    private bool WriteValidation()
    {
        bool zRet = true;
        try
        {
            if (txtEncoding.Text.Trim() == "" || txtTIDP.Text.Trim() == "")
            {
                dxErrorProvider.SetError(txtTIDP, "Please select user account");
                dxErrorProvider.SetError(txtEncoding, "Please select user account");
                zRet =false;
            }

            if (dt_gvHelmetTags.Rows.Count > 1)
            {
                MessageBox.Show("Please encode the helmet tag one at a time!", "Multiple Target Tags Error");
                dxErrorProvider.SetError(txtCurrTag, "Please encode the helmet tag one at a time!");
                zRet =false;
            }

            if (dt_gvHelmetTags.Rows.Count == 1)
            {
                if (dt_gvHelmetTags.Rows[0].ItemArray[0].ToString().ToUpper() == "TRUE") 
                {
                    MessageBox.Show("The helmet tag has been used before", "Tag Used Error");
                     
                    zRet = false;
                }
            }

            if (dt_gvHelmetTags.Rows.Count == 0)
            {
                MessageBox.Show("Please present the new tag", "New Tag Error");
                dxErrorProvider.SetError(txtCurrTag, "Please present the new tag");
                zRet = false;
            }

            //if ((txtCurrTag.Text.Length % 4 != 0) || (txtTIDP.Text.Length % 4 != 0))
            //{
            //    dxErrorProvider.SetError(txtTIDP, "Encoding Data must be a multiple of 16 bits (4 hex chars)");
            //    SetStatusMessage("Encoding Data must be a multiple of 16 bits (4 hex chars)");
            //    zRet =false;
            //}
        }
        catch (Exception ex)
        {
            MessageBox.Show("Tag Encoding Validation Error:"+ex.ToString());
        }
        return zRet;
    }

    private void btnWrite_Click(object sender, EventArgs e)
    {
        try
        {
            if (WriteValidation())
            {
                DialogResult zdr = MessageBox.Show("Do you want to encode the new Helmet tag and Add it to the User Helmet Tag List?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (zdr == DialogResult.Yes)
                {
                     
                    StopReader();

                    Debug.WriteLine("EPC");
                    WriteEPCMemeory();
                    _currentTag = string.Empty;
                    StartReader();
                }
            }
        }
        catch (Exception)
        {
            SetStatusMessage("Tag could not be written");
            StartReader();
        }
    }

    private void trackBar_Scroll(object sender, EventArgs e)
    {
        txtTxPower.Text = trackBar.Value.ToString();

        AdjustUHFReaderAntPower(); 
    }

    #endregion

    private void gcOperatorCreate_EnabledChanged(object sender, EventArgs e)
    {
        DisposeUHFReader();
        Disposal_MISCReader();

         
        mUserUpdateLoginInfor = null;
    }

    private void txtAccessCard_EditValueChanged(object sender, EventArgs e)
    {
        dxErrorProvider.SetError(txtAccessCard, null);
    }

    private void groupControl_EnabledChanged(object sender, EventArgs e)
    {
        Disposal_MISCReader();
        DisposeUHFReader();

         
        mUserUpdateLoginInfor = null;
    }

    private void txtAccessCard_EditValueChanged_1(object sender, EventArgs e)
    {
        dxErrorProvider.SetError(txtAccessCard, null);
    }

    private void txtTIDP_EditValueChanged(object sender, EventArgs e)
    {
        dxErrorProvider.SetError(txtTIDP, null);
        txtEncoding.Text = txtTIDP.Text;
    }

    private void btnHelmetTagListClear_Click_1(object sender, EventArgs e)
    {
        DataTable clone = dt_gvHelmetTags.Copy();
        clone.Rows.Clear();
        gvHelmetNew.BeginInvoke(new MethodInvoker(delegate { gvHelmetNew.DataSource = clone.DefaultView; }));
        dt_gvHelmetTags = clone;

        clsCrossThreadCalls.SetTextProperty(txtCurrTag, "");
    }

    private void gridViewUIDList_DataSourceChanged(object sender, EventArgs e)
    {
        try
        {
           clsCrossThreadCalls.SetTextProperty(lblHelmetTagCount,gridViewUIDList.DataRowCount.ToString());
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

     
     
    private void rdbIndividual_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbIndividual.Checked)
        {
            rdbBulkUpdateMode.Checked = false;
            m_UserUpdateMode = UserUpdateMode.IndividualMode; 
            btnWrite.Enabled = true;
        }
    }

    private void rdbBulkUpdateMode_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbBulkUpdateMode.Checked)
        {
            rdbIndividual.Checked = false;
            m_UserUpdateMode = UserUpdateMode.BulkUpdateMode; 
            btnWrite.Enabled = false;
        }
    }

    private void btnDeleteAllHelmetTags_Click(object sender, EventArgs e)
    {
        if (Validation())  
        {
            string zLoginID;  
            int zMode;  
            ISMUser.StructUser zUser = new ISMUser.StructUser();
            DataRowView zDataRow = luLoginId.Properties.GetDataSourceRowByKeyValue(luLoginId.EditValue) as DataRowView;
            if (zDataRow != null)   
            {
                zUser.UserID = Convert.ToInt64(zDataRow[0]);
                zUser.UserLogonID = Convert.ToString(zDataRow[1]);
            }

            zUser.UserUpdateUserID = m_ISMLoginInfo.LogonID;
            zUser.UserUpdateDateTime = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");

            zUser.UserTIDP = ""; 
            zUser.UserAccessCardUID = txtAccessCard.Text; 


             
             
             
             
             
            zUser.UserProfileCode = "";  
            if (txtConfirmPassword.Text.Trim() != "" && txtPassword.Text.Trim() != "")  
            {
                zUser.UserPassword = new ISMPassword().EncryptPassword(zUser.UserLogonID.ToUpper() + txtPassword.Text);   
                zMode = 0;
            }
            else
            {
                zMode = 1;
                zUser.UserPassword = "";  
            }

             
            string zMessage = String.Format("Do you want clear helmet tag list of User ID {0} ?", zUser.UserLogonID);  
            DialogResult zReply = MessageBox.Show(zMessage, " User ID Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);  
            if (zReply == DialogResult.Yes)  
            {
                if (m_ISMLoginInfo.ISMServer.UpdateISMUser(zUser, zMode) == true)   
                {
                     
                     
                    zLoginID = luLoginId.Text.Trim();
                    if (m_CellValueChanged)
                        CreateProfile(zUser.UserID, zUser.UserLogonID); 
                     

                    if (m_UserUpdateMode == UserUpdateMode.BulkUpdateMode)
                    {
                         
                        if (zUser.UserID > 0)
                        {
                             
                            DataTable zdt = new DataTable();
                            m_ISMLoginInfo.ISMServer.HQTIDHelmetTools(5, "", "", zUser.UserID, ref zdt);
                        }
                    }

                    btnClear.PerformClick();
                    LoadOperatorMetaData(); 
                    txtStatusMsg.Text = "Login ID " + zLoginID + " helmet tag list cleared";  
                }
            }
        }


    }


    
  }

  public enum UserUpdateMode {IndividualMode,BulkUpdateMode };

}
