
using System;
using System.Data;
using System.Windows.Forms;
using ISMDAL.TableColumnName;
using ISMComponents;
using System.Runtime.InteropServices;
using AMMO_BG_DLL.Background;
using System.Configuration;
using SHSHQ_CONTROL_PROPERTIES;
using SHSHQ_GLOBALS;

namespace ISM.Modules
{


  public partial class FrmLogin : DevExpress.XtraEditors.XtraForm
  {
     
    public const int WM_NCLBUTTONDOWN = 0xA1;
    public const int HT_CAPTION = 0x2;

    [DllImportAttribute("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    [DllImportAttribute("user32.dll")]
    public static extern bool ReleaseCapture();


    private void FrmLogin_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
    }


    private int m_nNumberOfRetry = 0;
    public ISMLoginInfo m_ISMLoginInfo;
     
     
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern int SetLocalTime(ref SystemTime lpSystemTime);
     
    public struct SystemTime
    {
        public short wYear;
        public short wMonth;
        public short wDayOfWeek;
        public short wDay;
        public short wHour;
        public short wMinute;
        public short wSecond;
        public short wMilliseconds;
    } 
     
    bool m_AllowLogIn = false;

    Logs applicationLogs = new Logs();

    public FrmLogin(ISMLoginInfo AISMLoginInfo)
    {
      InitializeComponent();
      ResetControls();
      m_ISMLoginInfo = AISMLoginInfo;
    }

    private void FrmLogin_Load(object sender, EventArgs e)
    {

        DLLConstants createInstallDirectory = new DLLConstants();
      createInstallDirectory.CreateInstallationFolder();
      string zConnectionStr;
      if (SHSHQ.Properties.Settings.Default.ConnectionString.Contains("User ID=ismsupport;Password=p@ssw0rd"))
          zConnectionStr = SHSHQ.Properties.Settings.Default.ConnectionString;
      else
          zConnectionStr = SHSHQ.Properties.Settings.Default.ConnectionString
                           + "User ID=ismsupport;Password=p@ssw0rd";

      zConnectionStr = SHSHQ.Properties.Settings.Default.ConnectionString; 
       

       
      m_ISMLoginInfo.ISMServer = new ISMDAL.DAL(zConnectionStr); 


       
      m_ISMLoginInfo.UserEndDate = DateTime.MinValue;  
      m_ISMLoginInfo.UserLastLogon = DateTime.MinValue;  

      SynchronizeDateTimeFromDatabaseServer(m_ISMLoginInfo.ISMServer.PDTGetServerDateAndTime());  
      SetLookUpEditCaption();
      if (!ValidateDBAndApplicationVersion())
      {
          
          string[] zVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');  
           
          string zSysVersion = String.Format("Version {0}.{1}.{2}", zVersion[0], zVersion[1], zVersion[2]);  

           
          m_ISMLoginInfo.AddToJournal("A", "Discrepancy between HQ Application Version " + zSysVersion + " and Database version " + m_ISMLoginInfo.Params.DBVersion + " for the System - " + System.Environment.MachineName.ToString(), "SEC113");

          MessageBox.Show("Discrepancy between Application and Database version. \nPlease contact your System Administrator", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); 
          this.DialogResult = DialogResult.Cancel;  
          
          Close();
      }
      //txtUserID.Text = "sys.admin";
      //txtPassword.Text = "P@ssw0rd";
      //btnLogin_Click(this, EventArgs.Empty);
      //btnLogin_Click(this, EventArgs.Empty);

    }
    private bool ValidateDBAndApplicationVersion()  
    {
        bool zValidDB = false;
        try
        {
            m_ISMLoginInfo.ISMServer.CurrentLoggedInUser = "";
            DataSet ds = m_ISMLoginInfo.ISMServer.GetParametersSettings();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow zData = ds.Tables[0].Rows[0];
                    m_ISMLoginInfo.Params.TimeOut = Convert.ToInt16(zData[ISMParams.TimeOut]) * 60;
                    m_ISMLoginInfo.Params.District = zData[ISMParams.District].ToString();
                    m_ISMLoginInfo.Params.DateTimeFormat = zData[ISMParams.DateTimeFormat].ToString().Trim();
                    m_ISMLoginInfo.Params.AppSkinColour = zData[ISMParams.AppSkinColour].ToString().Trim();
                    m_ISMLoginInfo.Params.ReportFolder = zData[ISMParams.ReportFolder].ToString().Trim();
                    m_ISMLoginInfo.Params.LocPrefix = zData[ISMParams.LocPrefix].ToString().Trim();
                    m_ISMLoginInfo.Params.SealPrefix = zData[ISMParams.SealPrefix].ToString().Trim();
                    m_ISMLoginInfo.Params.ItemPrefix = zData[ISMParams.ItemPrefix].ToString().Trim();
                    m_ISMLoginInfo.Params.RootLocationUID = zData[ISMParams.RootLocationUID].ToString().Trim();
                    m_ISMLoginInfo.Params.ActiveReaderPollTime = zData[ISMParams.ActiveReaderPollTime].ToString().Trim();
                    m_ISMLoginInfo.Params.PassiveReaderPollTime = zData[ISMParams.PassiveReaderPollTime].ToString().Trim();

                    m_ISMLoginInfo.Params.ActiveTxPowermin = zData[ISMParams.ActiveTxPowermin].ToString().Trim();
                    m_ISMLoginInfo.Params.ActiveTxPowermax = zData[ISMParams.ActiveTxPowermax].ToString().Trim();
                    m_ISMLoginInfo.Params.PassiveTxPowermin = zData[ISMParams.PassiveTxPowermin].ToString().Trim();
                    m_ISMLoginInfo.Params.PassiveTxPowermax = zData[ISMParams.PassiveTxPowermax].ToString().Trim();
                    m_ISMLoginInfo.Params.ActiveThrPowermin = zData[ISMParams.ActiveThrPowermin].ToString().Trim();
                    m_ISMLoginInfo.Params.ActiveThrPowermax = zData[ISMParams.ActiveThrPowermax].ToString().Trim();
                    m_ISMLoginInfo.Params.PassiveThrPowermin = zData[ISMParams.PassiveThrPowermin].ToString().Trim();
                    m_ISMLoginInfo.Params.PassiveThrPowermax = zData[ISMParams.PassiveThrPowermax].ToString().Trim();
                    m_ISMLoginInfo.Params.DBVersion = zData[ISMParams.DBVersion].ToString().Trim();

                    if (!System.IO.Directory.Exists(m_ISMLoginInfo.Params.ReportFolder))
                        System.IO.Directory.CreateDirectory(m_ISMLoginInfo.Params.ReportFolder);

                    string[] zVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');  
                     
                     string zApplicationVersion = String.Format("{0}.{1}.{2}", zVersion[0], zVersion[1], zVersion[2]);  
                     //if (zApplicationVersion == m_ISMLoginInfo.Params.DBVersion)
                         zValidDB = true;
                }
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        return zValidDB;
    }

    private bool Validation()
    {
      bool zRetValue = false;  

      if (txtUserID.Text.Trim() == "")   
      {
        MessageBox.Show("Please input User Name", "User ID", MessageBoxButtons.OK, MessageBoxIcon.Information);
        txtUserID.Focus();
      }
      else if (txtPassword.Text.Trim() == "")  
      {
        MessageBox.Show("Please input Password", "Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
        txtPassword.Focus();
      }
      else
        zRetValue = true;   

      return zRetValue;
    }

    private void ResetControls()
    {
      txtUserID.Text = string.Empty;
      txtPassword.Text = string.Empty;
      txtUserID.Focus();
      btnLogin.Enabled = false;
    }

    private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
         
        if ((txtUserID.Text.Trim() != "") && (txtPassword.Text.Trim() != ""))
          btnLogin_Click(sender, e);  
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
       
      DialogResult zReply = MessageBox.Show("Are you sure you want to exit?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);      
      if (zReply == DialogResult.Yes)
      {
        this.DialogResult = DialogResult.Cancel;  
        Close();
      }
      else  
      {
        txtUserID.Focus();   
      }
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
      DateTime zEndDate = DateTime.Parse(DateTime.Now.ToString());
      m_ISMLoginInfo.UserEndDate = DateTime.MinValue;
       
      DataSet ds;  

       
       

      if (Validation())
      {
         
         
        if (txtUserID.Text.ToUpper() == "SUPER.ADMIN")   
        {
          ISM.SpecialEntry zSpecialEntry = new SpecialEntry();
          m_AllowLogIn = (txtPassword.Text == zSpecialEntry.EntryValue) ? true : false;
          if (m_AllowLogIn)                                   
          {
            m_ISMLoginInfo.LogonID = "Super.Admin";
            m_ISMLoginInfo.UserID = 0;
            m_ISMLoginInfo.UserProfileCode = "SYSADMIN";     
          }
        }
        else
        {
           
           
            m_ISMLoginInfo.ISMServer.CurrentLoggedInUser = txtUserID.Text;
            AppGlobals.globalCurrentSystemUser = txtUserID.Text.ToLower();  

            if (m_AllowLogIn == false && btnLogin.Text == "Validate")  
            {
                string zComparePWords = new ISMPassword().EncryptPassword(txtUserID.Text.ToUpper() + txtPassword.Text);
                m_AllowLogIn = m_ISMLoginInfo.ISMServer.ValidateUserLogin(txtUserID.Text, zComparePWords, ref m_ISMLoginInfo);
                 
                 
                if (m_ISMLoginInfo.UserEndDate != DateTime.MinValue)  
                {
                    TimeSpan zTimeSpan = m_ISMLoginInfo.UserEndDate - DateTime.Now;  
                    if (zTimeSpan.TotalDays <= 0)
                    {
                        m_ISMLoginInfo.AddToJournal("E", "Attempted Login after Account has expired", "SEC104");
                        MessageBox.Show("Your ISM account has expired. Please Contact System Administrator", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        m_ISMLoginInfo.UserEndDate = DateTime.MinValue;
                        ResetControls();
                        return;

                    }

                }

                 
                 
                if (m_AllowLogIn)
                {
                    if (m_ISMLoginInfo.ISMServer.ValidateUserProfile(m_ISMLoginInfo.UserID))
                    {
                        lblProfile.Visible = true;
                        luProfile.Visible = true;
                        LoadUserProfileCode();
                         
                        btnLogin.Text = "Login";
                        return;
                    }
                    else  
                    {
                        m_nNumberOfRetry += 1;
                        if (m_nNumberOfRetry >= SHSHQ.Properties.Settings.Default.NumberOfRetry)
                        {
                            m_ISMLoginInfo.AddToJournal("E", "Exceeded Login attempts", "SEC103");
                            MessageBox.Show("You have exceed retry option.\nIf you have forgotten your User ID and Password \nthen please contact your System Administrator", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);  
                            this.DialogResult = DialogResult.Cancel;  
                            Close();
                        }
                        else
                        {
                            m_ISMLoginInfo.AddToJournal("E", "PC - Attempted login by user with no PC Client access", "SEC102");
                            MessageBox.Show("User ID does not have HQController access", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);  
                            ResetControls();
                            return;
                        }
                    }
                }
                 

            }
        }
          
         
         
         
        if (m_AllowLogIn == true && btnLogin.Text == "Login" && luProfile.EditValue == null) 
        {
          MessageBox.Show("Select a Profile", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
          return;
        }

        m_nNumberOfRetry += 1;
         
         
         
         
         
         
         
         
         
        if (m_AllowLogIn == true && btnLogin.Text == "Login" && luProfile.EditValue != null
           && luProfile.EditValue.ToString() != "AREADER"
           && luProfile.EditValue.ToString() != "INTERFACE" && luProfile.EditValue.ToString() != "PREADER")   

        {
          if(m_ISMLoginInfo.ISMServer.IsUserHasValidProfileAndLoginAccess(m_ISMLoginInfo.UserID,luProfile.EditValue.ToString()))  
          {

                  m_ISMLoginInfo.UserProfileCode = luProfile.EditValue.ToString();  
                  m_ISMLoginInfo.AddToJournal("T", "PC-Successfull Login with User Profile " + luProfile.Text, "SEC101");  

                  m_ISMLoginInfo.ISMServer.AddLoginTask("3", 22, m_ISMLoginInfo.UserID);

                  ds = m_ISMLoginInfo.ISMServer.GetParametersSettings();
                  if (ds != null)
                  {
                      if (ds.Tables[0].Rows.Count > 0)
                      {
                          DataRow zData = ds.Tables[0].Rows[0];
                          m_ISMLoginInfo.Params.TimeOut = Convert.ToInt16(zData[ISMParams.TimeOut]) * 60; 
                          m_ISMLoginInfo.Params.District = zData[ISMParams.District].ToString(); 
                          m_ISMLoginInfo.Params.DateTimeFormat = zData[ISMParams.DateTimeFormat].ToString().Trim();
                          m_ISMLoginInfo.Params.AppSkinColour = zData[ISMParams.AppSkinColour].ToString().Trim();
                          m_ISMLoginInfo.Params.ReportFolder = zData[ISMParams.ReportFolder].ToString().Trim();
                          m_ISMLoginInfo.Params.LocPrefix = zData[ISMParams.LocPrefix].ToString().Trim();
                          m_ISMLoginInfo.Params.SealPrefix = zData[ISMParams.SealPrefix].ToString().Trim();
                          m_ISMLoginInfo.Params.ItemPrefix = zData[ISMParams.ItemPrefix].ToString().Trim();
                          m_ISMLoginInfo.Params.RootLocationUID = zData[ISMParams.RootLocationUID].ToString().Trim(); 
                          m_ISMLoginInfo.Params.ActiveReaderPollTime = zData[ISMParams.ActiveReaderPollTime].ToString().Trim(); 
                          m_ISMLoginInfo.Params.PassiveReaderPollTime = zData[ISMParams.PassiveReaderPollTime].ToString().Trim(); 

                          m_ISMLoginInfo.Params.ActiveTxPowermin = zData[ISMParams.ActiveTxPowermin].ToString().Trim(); 
                          m_ISMLoginInfo.Params.ActiveTxPowermax = zData[ISMParams.ActiveTxPowermax].ToString().Trim(); 
                          m_ISMLoginInfo.Params.PassiveTxPowermin = zData[ISMParams.PassiveTxPowermin].ToString().Trim(); 
                          m_ISMLoginInfo.Params.PassiveTxPowermax = zData[ISMParams.PassiveTxPowermax].ToString().Trim(); 
                          m_ISMLoginInfo.Params.ActiveThrPowermin = zData[ISMParams.ActiveThrPowermin].ToString().Trim(); 
                          m_ISMLoginInfo.Params.ActiveThrPowermax = zData[ISMParams.ActiveThrPowermax].ToString().Trim();
                          m_ISMLoginInfo.Params.PassiveThrPowermin = zData[ISMParams.PassiveThrPowermin].ToString().Trim(); 
                          m_ISMLoginInfo.Params.PassiveThrPowermax = zData[ISMParams.PassiveThrPowermax].ToString().Trim(); 

                          if (!System.IO.Directory.Exists(m_ISMLoginInfo.Params.ReportFolder))
                              System.IO.Directory.CreateDirectory(m_ISMLoginInfo.Params.ReportFolder);
                      }
                  }
                  this.DialogResult = DialogResult.OK;
                  applicationLogs.InsertApplicationLogs(
                      ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                      "Login",
                      txtUserID.Text,
                      "Success",
                      "",
                      "",
                      "HQ",
                      "");

          }
          else  
          {
              if (m_nNumberOfRetry >= SHSHQ.Properties.Settings.Default.NumberOfRetry)
              {
                   
                  m_ISMLoginInfo.AddToJournal("E", "Exceeded Login attempts", "SEC103");
                   
                   
                   
                  MessageBox.Show("You have exceed retry option.\nIf you have forgotten your User ID and Password \nthen please contact your System Administrator", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);  
                  this.DialogResult = DialogResult.Cancel;  
                  Close();
              }
              else
              {
                  m_ISMLoginInfo.AddToJournal("E", "PC - Attempted login by user with no PC Client access", "SEC102");
                  MessageBox.Show("User ID does not have PC Client access", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); 
                  ResetControls();
                  return;
              }
          }

        }
        else
        {
           
           
          if (m_nNumberOfRetry >= SHSHQ.Properties.Settings.Default.NumberOfRetry)
          {
             
            m_ISMLoginInfo.AddToJournal("E", "Exceeded Login attempts", "SEC103");
             
             
             
            MessageBox.Show("You have exceed retry option.\nIf you have forgotten your User ID and Password \nthen please contact your System Administrator", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);  
            this.DialogResult = DialogResult.Cancel;  
            Close();
          }
          else
          {
            
                 
               
               
               
               
               
               
               
              
             
            MessageBox.Show("Incorrect user name or password entered.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);  
            applicationLogs.InsertApplicationLogs(
                    ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                    "Login",
                    txtUserID.Text, 
                    "Failed",
                    "",
                    "",
                    "HQ",
                    "");
            ResetControls();

          }
        }
      }
    }

     
     
     
     
    private void EnableBtnLogin(object sender, EventArgs e)   
    {
        if ((txtUserID.Text.Trim().Length > 0) && (txtPassword.Text.Trim().Length > 0))
        {
            btnLogin.Enabled = true;
            if (btnLogin.Text == "login")
            {
                ResetProfileCode();
            }
        }
        else
        {
            btnLogin.Enabled = false;
            ResetProfileCode();
        }

    }
     
    private void SynchronizeDateTimeFromDatabaseServer(DateTime ADateTime)
    {
        SystemTime systNew = new SystemTime();
         
        systNew.wDay = (short)ADateTime.Day;;
        systNew.wMonth = (short)ADateTime.Month;
        systNew.wYear = (short)ADateTime.Year;
        systNew.wHour = (short)ADateTime.Hour;
        systNew.wMinute = (short)ADateTime.Minute;
        systNew.wSecond = (short)ADateTime.Second;
        systNew.wMilliseconds = (short)ADateTime.Millisecond; 
         
        SetLocalTime(ref systNew); 
    }
     

     
     
    private void SetLookUpEditCaption()
    {
        try
        {
            luProfile.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUserRole.Code, "ID",10,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUserRole.Desc, 250,"Description")});

             
            luProfile.Properties.DisplayMember = ISMUserRole.Desc;
            luProfile.Properties.ValueMember = ISMUserRole.Code;

        }
        catch
        {
            MessageBox.Show("System Error. Contact System Administrator", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    private void LoadUserProfileCode()
    {
        DataSet ds = null;
        try
        {
            ds = m_ISMLoginInfo.ISMServer.GetUserProfileCode(m_ISMLoginInfo.UserID);
            if (ds != null)
            {
                luProfile.Properties.DataSource = ds.Tables[0].DefaultView;
                if (ds.Tables[0].Rows.Count == 1)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    luProfile.EditValue = luProfile.Properties.GetKeyValueByDisplayText(dr["DESCRIPTION"].ToString());
                    luProfile.Focus();
                    btnLogin.Text = "login";
                }
                else
                    txtPassword.Focus();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    private void ResetProfileCode()
    {
        try
        {
            lblProfile.Visible = false;
            luProfile.Visible = false;
            btnLogin.Text = "Validate";
            m_AllowLogIn = false;
            luProfile.EditValue = null;
            luProfile.Properties.DataSource = null;
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }

    private void grControl_Paint(object sender, PaintEventArgs e)
    {

    }

     
  }
}