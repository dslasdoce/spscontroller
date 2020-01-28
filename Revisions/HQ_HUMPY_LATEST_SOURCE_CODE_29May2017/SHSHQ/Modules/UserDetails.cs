using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISMDAL.TableColumnName;
using SHSHQ_DAL;
using ISM.Modules;
using System.Text.RegularExpressions;
using SHSHQ_CONTROL_PROPERTIES;
using ISM;
using AMMO_BG_DLL.Background;
using System.Configuration;
using Impinj.OctaneSdk;
using System.Net;
using System.Net.NetworkInformation;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using MISCReaderTCPIP;
using SHSHQ_GLOBALS;


namespace SHSHQ.Modules
{
    public partial class UserDetails : UserControl 
    {
        static ImpinjReader uhfReader;
        private MISCReader msicReader;

        DataTable uhfTagDetails = new DataTable();
        ControlProperties setControlProperty = new ControlProperties();
        Logs applicationLogs = new Logs();
        
        bool insertMode = true;
        bool uhfReaderConnected = false;
        bool uhfReaderStarted = false;
        bool msicReaderConnected = true;

        string msicIP = "";
        string uhfIP = "";

        string currentAccessCardUID = "";
        string currentHelmentTag = "";
        string currenLogOnID = "";
        string currentPassword = "";
        int currentUserID = -1;

        public System.Threading.Timer uhfTagMonitoringThreadTimer;
        public System.Threading.Timer msicTagMonitoringThreadTimer;

        int uhfTagMonitoringCounter = 0;
        int msicTagMonitoringCounter = 0;
        int oneSecondThreadDueTime = 1000;
        bool uhfTagRemoved = true;
        bool msicTagRemoved = true;


        public UserDetails(int myWidth, int myHeight)
        {
            InitializeComponent();
            this.Width = myWidth;
            this.Height = myHeight;
        }
        private void PopulateUserSyncDetails(int userID)
        {
            UserDAL getUserSyncDetails = new UserDAL();
            try
            {
                
                cmbSyncStatus.Properties.DataSource = null;
                cmbSyncStatus.Properties.Columns.Clear();
                //grdSyncHistory.BeginInvoke(new MethodInvoker(delegate { grdSyncHistory.DataSource = null; }));

                cmbSyncStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                     new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HumpyID", 240, "Humpy ID"),  
                     new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DateTimeLastSynced", 480,"Date"),
                     new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SyncStatus", 240,"Status")});


                cmbSyncStatus.Properties.DisplayMember = "NoSelection";
                cmbSyncStatus.Properties.ValueMember = "NoSelection";

                cmbSyncStatus.Properties.DataSource = getUserSyncDetails.GetUserSycDetails(userID);

                //grdSyncHistory.BeginInvoke(new MethodInvoker(delegate { grdSyncHistory.DataSource = getUserSyncDetails.GetUserSycDetails(userID); }));

            }
            catch (Exception ex)
            { throw ex; }

        }
        private void PopulateUserList()
        {
            UserDAL getUserList = new UserDAL();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //cmbUserList.Properties.DataSource = null;
                //cmbUserList.Properties.Columns.Clear();

                //cmbUserList.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                //     new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLogonID, 240, "User ID"),  
                //     new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserFirstName, 180,"First Name"),
                //     new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLastName, 180,"Last Name")});


                //cmbUserList.Properties.DisplayMember = ISMUser.UserLogonID;
                //cmbUserList.Properties.ValueMember = ISMUser.UserID;



                //cmbUserList.Properties.DataSource = getUserList.GetUserList();

                grdUsers.BeginInvoke(new MethodInvoker(delegate { grdUsers.DataSource = getUserList.GetUserList(); }));
                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {throw ex;}

        }
        private void PopulateUserRole()
        {
            UserDAL getUserRole = new UserDAL();
            try
            {
                cmbRole.Properties.DataSource = null;
                cmbRole.Properties.Columns.Clear();

                cmbRole.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                     new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DESCRIPTION", 240, "ROLE")});


                cmbRole.Properties.DisplayMember = "DESCRIPTION";
                cmbRole.Properties.ValueMember = "CODE";

                cmbRole.Properties.DataSource = getUserRole.GetUserRole();

            }
            catch (Exception ex)
            { throw ex; }

        }
        private void ClearErrorNotification()
        {
            dxErrorProvider.SetError(txtFirstName, null);
            dxErrorProvider.SetError(txtLastName, null);
            dxErrorProvider.SetError(txtPassword, null);
            dxErrorProvider.SetError(txtConfirmPassword, null);
            dxErrorProvider.SetError(txtAccessCard, null);
            dxErrorProvider.SetError(txtHelmetTags, null);
            dxErrorProvider.SetError(cmbRole, null);
            dxErrorProvider.SetError(txtUserID, null);
            
        }
        private void UserDetails_Load(object sender, EventArgs e)
        {
            try
            {
                SetUpUHFTagGrid();
                SetUpUserGrid();
                PopulateUserList();
                PopulateUserRole();
                PopulateUserSyncDetails(-1);
                cmbUserList.Focus();

                uhfTagDetails.Columns.Add("EPCData", typeof(string));
                uhfTagDetails.Columns.Add("TIDPData", typeof(string));
                uhfTagDetails.Columns.Add("TagGroup", typeof(string));
                uhfTagDetails.Columns.Add("EPCDataFormatted", typeof(string));
                uhfTagDetails.Columns.Add("TIDPDataFormatted", typeof(string));

                //uhfTagDetails.Rows.Add("EPCData1", "TIDPData1", "TagGroup1", "EPCDataFormatted1", "TIDPDataFromatted1");
                //uhfTagDetails.Rows.Add("EPCData2", "TIDPData2", "TagGroup2", "EPCDataFormatted2", "TIDPDataFromatted2");
                //uhfTagDetails.Rows.Add("EPCData3", "TIDPData3", "TagGroup3", "EPCDataFormatted3", "TIDPDataFromatted3");
                //uhfTagDetails.Rows.Add("EPCData4", "TIDPData4", "TagGroup4", "EPCDataFormatted4", "TIDPDataFromatted4");
                //uhfTagDetails.Rows.Add("EPCData5", "TIDPData5", "TagGroup5", "EPCDataFormatted5", "TIDPDataFromatted5");

                //grdUHFTags.BeginInvoke(new MethodInvoker(delegate { grdUHFTags.DataSource = uhfTagDetails; }));

                msicIP = SHSHQ.Properties.Settings.Default.MISCReaderIP; ;
                uhfIP = SHSHQ.Properties.Settings.Default.UHFReaderIP;

                setControlProperty.SetControlPoperties(lblMSICIP,"Text" ,msicIP);
                setControlProperty.SetControlTextProperty(lblUHFIP,"Text" , uhfIP);
                setControlProperty.SetControlTextProperty(lblUHFIP, "Text", uhfIP);
                setControlProperty.SetControlTextProperty(cmdUHF, "Text", string.Concat(uhfIP,"\n DISCONNECTED"));
                setControlProperty.SetControlTextProperty(txtStatusMsg, "Text", "User Insert Mode");

                uhfTagMonitoringThreadTimer = new System.Threading.Timer(
                                        new System.Threading.TimerCallback(UHFTagMonitoringTimer),
                                        null,
                                        System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

                msicTagMonitoringThreadTimer = new System.Threading.Timer(
                                        new System.Threading.TimerCallback(MSICTagMonitoringTimer),
                                        null,
                                        System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

                ConnectToUHFReader(true);
               
                if (uhfReaderConnected)
                    StartUHFReader(true);

                ConnectToMSICReader(true);


            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Concat("Application Error!\n Please contact the administrator.\n",
                                              ex.Message.ToString(), ex.StackTrace.ToString()), "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void GenerateUserID(object sender, EventArgs e)
        {
            string firstName = "";
            string lastName = "";
            string userID = "";

            try
            {
                firstName = txtFirstName.Text.Trim();
                lastName = txtLastName.Text.Trim();
                userID = string.Concat(firstName, ".", lastName);

                if (firstName.Length == 0 && lastName.Length == 0)
                    userID = "";

                txtUserID.Text = userID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Concat("Application Error!\n Please contact the administrator.\n",
                                              ex.Message.ToString(), ex.StackTrace.ToString()), "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            UserDAL checkUniqueFields = new UserDAL();
            UserDAL updateUserDetails = new UserDAL();
            DataTable distictCountTagDetails = new DataTable();

            string firstName = "";
            string lastName = "";
            string userID = "";
            string password = "";
            string confirmPassword = "";
            string accessCard = "";
            string helmetTags = "";
            string userRole = "";

            int uhfTagCount = 0;
            int uhfTagDistinctCount = 0;

            long newUserID = 0;
           

            bool validEntries = true;

            try
            {
                ClearErrorNotification();

                distictCountTagDetails = uhfTagDetails.DefaultView.ToTable(true, "TagGroup");
                uhfTagCount = uhfTagDetails.Rows.Count;
                uhfTagDistinctCount = distictCountTagDetails.Rows.Count;

                if (txtFirstName.EditValue != null)
                    firstName = txtFirstName.EditValue.ToString().Trim();

                lastName = txtLastName.Text.Trim();
                userID = txtUserID.Text.Trim();
                password = txtPassword.Text.Trim();
                confirmPassword = txtConfirmPassword.Text.Trim();
                accessCard = txtAccessCard.Text.Trim();
                helmetTags = txtHelmetTags.Text.Trim();
                if (cmbRole.EditValue != null)
                    userRole = cmbRole.EditValue.ToString().Trim();

                #region Data Entry Validation
              

                if (firstName.Length == 0)
                {
                    validEntries = false;
                    dxErrorProvider.SetError(txtFirstName, "Enter the user's first name.");
                }
                if (lastName.Length == 0)
                {
                    validEntries = false;
                    dxErrorProvider.SetError(txtLastName, "Enter the user's last name.");
                }
                if (userID.Length == 0)
                {
                    validEntries = false;
                    dxErrorProvider.SetError(txtUserID, "Enter the user's unique login ID.");
                }
                if (checkUniqueFields.CheckUniqueFields(userID, currenLogOnID, "", "", "", "", insertMode, "LOGONID"))
                {
                    validEntries = false;
                    dxErrorProvider.SetError(txtUserID, "An account using this User ID already exists ");
                }
                if (password.Length == 0 && insertMode)
                {
                    validEntries = false;
                    dxErrorProvider.SetError(txtPassword, "Enter a password to use.");
                }
                if (confirmPassword.Length == 0 && insertMode)
                {
                    validEntries = false;
                    dxErrorProvider.SetError(txtConfirmPassword, "Confirm your password");
                }
                if (password != confirmPassword)
                {
                    validEntries = false;
                    txtPassword.Text = "";
                    txtConfirmPassword.Text = "";

                    dxErrorProvider.SetError(txtPassword, "The Password and Confirm Password do not match.  Re-enter the passwords again");
                    dxErrorProvider.SetError(txtConfirmPassword, "The Password and Confirm Password do not match.  Re-enter the passwords again");
                }

                if (PasswordPolicy.IsValid(password) == false && password.Length > 0)
                {
                    validEntries = false;
                    txtPassword.Text = "";
                    txtConfirmPassword.Text = "";
                    dxErrorProvider.SetError(txtPassword, "Your password does not match the rules required(2 character min), please try a again");
                    dxErrorProvider.SetError(txtConfirmPassword, "Your password does not match the rules required, please try a again");
            
                }
                if (accessCard.Length == 0 && insertMode)
                {
                    validEntries = false;
                    dxErrorProvider.SetError(txtAccessCard, "Enter the Access Card UID");
                }
                if (checkUniqueFields.CheckUniqueFields("", "", accessCard, currentAccessCardUID, "", "", insertMode, "ACUID"))
                {
                    validEntries = false;
                    dxErrorProvider.SetError(txtAccessCard, "Access Card already used.");
                }
                if (helmetTags.Length == 0 && insertMode)
                {
                    validEntries = false;
                    dxErrorProvider.SetError(txtHelmetTags, "Enter the valid helmet UID");
                }
                if (uhfTagDistinctCount > 1 && uhfTagCount > 0)
                {
                    validEntries = false;
                    dxErrorProvider.SetError(txtHelmetTags,"More than 1 TIDP detected!");
                }
                if (uhfTagCount > 6)
                {
                    validEntries = false;
                    dxErrorProvider.SetError(txtHelmetTags, "Helmet tag should be maximum of 6 tags only.");
                }
                if (uhfTagCount < 6 && uhfTagCount > 0)
                {
                    validEntries = false;
                    dxErrorProvider.SetError(txtHelmetTags, "Less than 6 helmet tags detected.");
                }

                if (checkUniqueFields.CheckUniqueFields("", "", "", "", helmetTags, currentHelmentTag, insertMode, "HELTAG"))
                {
                    validEntries = false;
                    dxErrorProvider.SetError(txtHelmetTags, "User Helmet Tag UID already exists.");
                }
                if (userRole.Length == 0)
                {
                    validEntries = false;
                    dxErrorProvider.SetError(cmbRole, "Select a user role.");
                }
                #endregion
                if (validEntries)
                {
             
                    if (insertMode)
                    {
                        if (MessageBox.Show(String.Format("Do you want create User ID {0} ?", userID), lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            password = new ISMPassword().EncryptPassword(userID.ToUpper() + password);
                            newUserID = updateUserDetails.InsertUser(firstName, lastName, userID, password, accessCard, helmetTags, AppGlobals.globalCurrentSystemUser, userRole);

                            if (uhfTagDetails.Rows.Count == 6)
                            {
                                for (int loopCounter = 0; loopCounter < uhfTagDetails.Rows.Count; loopCounter++)
                                {
                                    updateUserDetails.InsertUserHelmetTags(newUserID, uhfTagDetails.Rows[loopCounter]["EPCDataFormatted"].ToString(), uhfTagDetails.Rows[loopCounter]["TIDPDataFormatted"].ToString());
                                }
                            }

                            applicationLogs.InsertApplicationLogs(
                                            ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                                             "User", AppGlobals.globalCurrentSystemUser, "Success", "Adding", userID, "HQ", "");
                            ClearFields();
                            PopulateUserList();

                            Cursor.Current = Cursors.Default;

                            MessageBox.Show(string.Concat("Login ID " + userID + " has been created"), "Insert User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            applicationLogs.InsertApplicationLogs(
                                         ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                                         "User", AppGlobals.globalCurrentSystemUser, "Failed", "Adding", userID, "HQ", "");

                        }

                    }
                    else
                    {
                        if (MessageBox.Show(String.Format("Do you want update User ID {0} ?", userID), lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            if (password.Length == 0)
                                password = currentPassword;
                            else
                                password = new ISMPassword().EncryptPassword(userID.ToUpper() + password);

                            updateUserDetails.UpdatetUser(currentUserID, firstName, lastName, userID, password, accessCard, helmetTags, AppGlobals.globalCurrentSystemUser, userRole);

                            updateUserDetails.DeleteUserHelmetTags(currentUserID);

                            if (uhfTagDetails.Rows.Count == 6)
                            {
                                for (int loopCounter = 0; loopCounter < uhfTagDetails.Rows.Count; loopCounter++)
                                {
                                    updateUserDetails.InsertUserHelmetTags(currentUserID, uhfTagDetails.Rows[loopCounter]["EPCDataFormatted"].ToString(), uhfTagDetails.Rows[loopCounter]["TIDPDataFormatted"].ToString());
                                }
                            }

                            applicationLogs.InsertApplicationLogs(
                                            ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                                             "User", AppGlobals.globalCurrentSystemUser, "Success", "Update", userID, "HQ", "");
                            ClearFields();
                            PopulateUserList();

                            Cursor.Current = Cursors.Default;

                            MessageBox.Show(string.Concat("Login ID " + userID + " has been Updated"), "Update User", MessageBoxButtons.OK, MessageBoxIcon.Information);

                         
                        }
                        else
                        {
                            applicationLogs.InsertApplicationLogs(
                                         ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                                         "User", AppGlobals.globalCurrentSystemUser, "Failed", "Update", userID, "HQ", "");

                        }
                    }
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Concat("Application Error!\n Please contact the administrator.\n",
                                              ex.Message.ToString(), ex.StackTrace.ToString()), "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (insertMode)
                    applicationLogs.InsertApplicationLogs(
                                           ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                                            "User", AppGlobals.globalCurrentSystemUser, "Failed", "Adding", userID, "HQ", "");
                else
                    applicationLogs.InsertApplicationLogs(
                                           ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                                            "User", AppGlobals.globalCurrentSystemUser, "Failed", "Update", userID, "HQ", "");

            }
            finally { }
        }
        # region Password Policy
        public class PasswordPolicy
        {
            private static int Minimum_Length = 2;

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
        #endregion

        private void cmbUserList_EditValueChanged(object sender, EventArgs e)
        {
           
        }
        private void ClearFields()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                setControlProperty.SetControlPoperties(txtFirstName, "EditValue", null);
                setControlProperty.SetControlPoperties(txtLastName, "Text", "");
                setControlProperty.SetControlPoperties(txtUserID, "Text", "");
                setControlProperty.SetControlPoperties(txtPassword, "Text", "");
                setControlProperty.SetControlPoperties(txtConfirmPassword, "Text", "");
                setControlProperty.SetControlPoperties(txtAccessCard, "Text", "");
                setControlProperty.SetControlPoperties(txtHelmetTags, "Text", "");

                setControlProperty.SetControlPoperties(cmbRole, "EditValue", null);
                setControlProperty.SetControlPoperties(cmbUserList, "EditValue", null);

                cmbSyncStatus.Properties.DataSource = null;
                cmbSyncStatus.Properties.Columns.Clear();

                insertMode = true;
                currentAccessCardUID = "";
                currentHelmentTag = "";
                currenLogOnID = "";
                currentUserID = -1;

                setControlProperty.SetControlPoperties(txtStatusMsg, "Text", "User Insert Mode");
                ClearErrorNotification();
                uhfTagDetails.Rows.Clear();
                grdUHFTags.BeginInvoke(new MethodInvoker(delegate { grdUHFTags.DataSource = null; }));
                setControlProperty.SetControlPoperties(cmdSave, "Text", "Add User");
                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            { throw ex;}
        }
        private void cmdResetForm_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to reset the form?", "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                ClearFields();


            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Concat("Application Error!\n Please contact the administrator.\n",
                                                ex.Message.ToString(), ex.StackTrace.ToString()), "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region UHF Reader Intialization and Connection

        private void ConnectToUHFReader(bool connectToReader)
        {
    
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (connectToReader)
                {
                    IPAddress ipAddress = System.Net.IPAddress.Parse(uhfIP);
                    var pingreply = new Ping().Send(ipAddress);

                    if (pingreply.Status == IPStatus.Success)
                    {
                        uhfReader = new ImpinjReader();
                        setControlProperty.SetControlPoperties(lblUHStatusDetail, "Text", "");
                        uhfReader.Connect(ipAddress.ToString());
                        Settings settings = uhfReader.QueryDefaultSettings();

                        uhfReader.ReaderStarted += UHFReaderStarted;
                        uhfReader.ReaderStopped += UHFReaderStopped;
                        uhfReader.TagsReported += UHFTagsReported;

                        #region Reader Settings

                        settings.Report.IncludeFastId = true;
                        settings.Report.IncludePcBits = true;

                        settings.Antennas.GetAntenna(1).IsEnabled = true;
                        settings.Antennas.GetAntenna(1).PortNumber = 1;
                        settings.Antennas.GetAntenna(1).RxSensitivityInDbm = -70;
                        settings.Antennas.GetAntenna(1).TxPowerInDbm = double.Parse("10");
                     
                        #endregion

                        uhfReader.ApplySettings(settings);

                        uhfReaderConnected = true;
                        setControlProperty.SetControlPoperties(lblUHFStatus, "Text", "CONNECTED");
                        setControlProperty.SetControlPoperties(lblUHFStatus, "BackColor", Color.Green);
                        setControlProperty.SetControlPoperties(lblUHStatusDetail, "Text", "");
                        setControlProperty.SetControlPoperties(lblUHFIP, "BackColor", Color.Green);

                        setControlProperty.SetControlPoperties(cmdUHF, "BackColor", Color.Green);
                        setControlProperty.SetControlTextProperty(cmdUHF, "Text", string.Concat(uhfIP, "\n CONNECTED"));


                    }
                    else
                    {
                        uhfReaderConnected = false;
                        setControlProperty.SetControlPoperties(lblUHFStatus, "Text", "DISCONNECTED");
                        setControlProperty.SetControlPoperties(lblUHFStatus, "BackColor", Color.Red);
                        setControlProperty.SetControlPoperties(lblUHStatusDetail, "Text", "Could not connect to the reader.");
                        setControlProperty.SetControlPoperties(lblUHFIP, "BackColor", Color.Red);

                        setControlProperty.SetControlPoperties(cmdUHF, "BackColor", Color.Red);
                        setControlProperty.SetControlTextProperty(cmdUHF, "Text", string.Concat(uhfIP, "\n DISCONNECTED"));


                    }
                }
                else
                {

                    if (uhfReader != null && uhfReaderConnected)
                    {

                        uhfReader.TagsReported -= UHFTagsReported;
                        uhfReader.ReaderStarted -= UHFReaderStarted;
                        uhfReader.ReaderStopped -= UHFReaderStopped;

                        uhfReader.Disconnect();

                        uhfReaderConnected = false;
                        setControlProperty.SetControlPoperties(lblUHFStatus, "Text", "DISCONNECTED");
                        setControlProperty.SetControlPoperties(lblUHFStatus, "BackColor", Color.Red);
                        setControlProperty.SetControlPoperties(lblUHStatusDetail, "Text", "");
                        setControlProperty.SetControlPoperties(lblUHFIP, "BackColor", Color.Red);

                        setControlProperty.SetControlPoperties(cmdUHF, "BackColor", Color.Red);
                        setControlProperty.SetControlTextProperty(cmdUHF, "Text", string.Concat(uhfIP, "\n DISCONNECTED"));

                        uhfReader = null;

                    }

                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                if (uhfReaderConnected)
                    setControlProperty.SetControlPoperties(lblUHStatusDetail, "Text", "Could not disconnect from the reader");
                else
                    setControlProperty.SetControlPoperties(lblUHStatusDetail, "Text", "Could not connect to the reader");
            }
        }
        void UHFReaderStarted(ImpinjReader reader, ReaderStartedEvent e)
        {
            try
            { uhfReaderStarted = true; }
            catch (Exception ex)
            { MessageBox.Show(string.Concat(ex.Message.ToString(), "\n", ex.StackTrace.ToString())); }

        }
        void UHFReaderStopped(ImpinjReader reader, ReaderStoppedEvent e)
        {
            try
            { uhfReaderStarted = false; }
            catch (Exception ex)
            { MessageBox.Show(string.Concat(ex.Message.ToString(), "\n", ex.StackTrace.ToString())); }
        }
        void UHFTagsReported(ImpinjReader sender, TagReport report)
        {
            string epcData = "";
            string tidpData = "";

            Tag entryTag = report.Tags[0];
            
            try
            {

                uhfTagMonitoringCounter = 0;

                epcData = entryTag.Epc.ToHexWordString().Replace(" ", "").ToString();
                tidpData = entryTag.Tid.ToHexWordString().Replace(" ", "").ToString();

                UpdateUHFTagDataTable(epcData, tidpData);

            }
            catch (Exception ex)
            {
                uhfTagDetails.Rows.Clear();
                grdUHFTags.BeginInvoke(new MethodInvoker(delegate { grdUHFTags.DataSource = null; }));

            }


        }
        private void UpdateUHFTagDataTable(string epcData, string tidpData)
        {
            DataTable newTagDetails = new DataTable();
            TagHandling generateTag = new TagHandling();

            try
            {
                if (epcData.Length > 0 && tidpData.Length > 0)
                {
                    if (uhfTagDetails.Select("TIDPData='" + tidpData + "'").Length < 1)
                        uhfTagDetails.Rows.Add(
                                                epcData, 
                                                tidpData, 
                                                generateTag.GetTagGroup(epcData, true),
                                                generateTag.GetTagGroup(epcData, false),
                                                generateTag.GetTagInvidualNumber(epcData)
                                               );

                    uhfTagDetails.AcceptChanges();
                    newTagDetails = uhfTagDetails.Copy();

                    grdUHFTags.BeginInvoke(new MethodInvoker(delegate { grdUHFTags.DataSource = newTagDetails; }));
                    setControlProperty.SetControlPoperties(lblUHStatusDetail, "Text", "");
                    setControlProperty.SetControlPoperties(txtHelmetTags, "Text", generateTag.GetTagGroup(epcData, false));
                   
                }
                else
                {
                    newTagDetails = uhfTagDetails.Copy();
                    grdUHFTags.BeginInvoke(new MethodInvoker(delegate { grdUHFTags.DataSource = newTagDetails; }));
                    setControlProperty.SetControlPoperties(lblUHStatusDetail, "Text", "Please present RFID Tag");
                    if (insertMode)
                        setControlProperty.SetControlPoperties(txtHelmetTags, "Text", "");
                    else
                        setControlProperty.SetControlPoperties(txtHelmetTags, "Text", currentHelmentTag);
                    
                }



            }
            catch (Exception ex)
            {
                uhfTagDetails.Rows.Clear();
                grdUHFTags.BeginInvoke(new MethodInvoker(delegate { grdUHFTags.DataSource = null; }));
            }
        }
        private void StartUHFReader(bool startReader)
        {
            
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (startReader)
                {
                    uhfReader.Start();
                    uhfTagMonitoringThreadTimer.Change(oneSecondThreadDueTime, System.Threading.Timeout.Infinite);
                }
                if (!startReader)
                {
                    uhfReader.Stop();
                    uhfTagDetails.Rows.Clear();
                    uhfTagMonitoringThreadTimer = null;
                    UpdateUHFTagDataTable("", "");
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                uhfTagDetails.Rows.Clear();
                grdUHFTags.BeginInvoke(new MethodInvoker(delegate { grdUHFTags.DataSource = null; }));

            }
        }
        private void UHFTagMonitoringTimer(object requestingObject)
        {
            try
            {
                if (uhfTagMonitoringThreadTimer != null)
                {
                    uhfTagMonitoringCounter += 1;
                    uhfTagRemoved = false;

                    uhfTagMonitoringThreadTimer.Change(oneSecondThreadDueTime, System.Threading.Timeout.Infinite);

                    if (uhfTagMonitoringCounter % 2 == 0)
                    {
                        uhfTagMonitoringCounter = 0;
                        uhfTagDetails.Rows.Clear();
                        UpdateUHFTagDataTable("", "");
                        uhfTagRemoved = true;

                    }

                }

            }
            catch (Exception)
            {
                uhfTagDetails.Rows.Clear();
                grdUHFTags.BeginInvoke(new MethodInvoker(delegate { grdUHFTags.DataSource = null; }));
            }
        }
        #endregion

        #region MSIC Reader Initialization and Connection
        private void ConnectToMSICReader(bool connectToReader)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (connectToReader)
                {
                    int tcpipPort = int.Parse(SHSHQ.Properties.Settings.Default.MISCReaderPort);
                    string msicIP = SHSHQ.Properties.Settings.Default.MISCReaderIP;

                    msicReader = new MISCReader(msicIP, tcpipPort);
                    msicReader.OnTagDetected += MSICTagDetected;
                    msicReader.Start(null);

                    setControlProperty.SetControlPoperties(lblMSICStatus, "Text", "CONNECTED");
                    setControlProperty.SetControlPoperties(lblMSICStatus, "BackColor", Color.Green);
                    setControlProperty.SetControlPoperties(lblMSICIP,"BackColor",Color.Green);
                    setControlProperty.SetControlPoperties(lblMSICStatusDetails, "Text", "");
                    msicTagMonitoringThreadTimer.Change(oneSecondThreadDueTime, System.Threading.Timeout.Infinite);
                    msicReaderConnected = true;
                }
                else
                {
                    if (msicReader != null)
                    {
                        msicReader.OnTagDetected -= MSICTagDetected;
                        msicReader.Stop(null);
                        msicReader.Dispose(null);
                        msicReader = null;

                       
                        setControlProperty.SetControlPoperties(lblMSICStatus, "Text", "DISCONNECTED");
                        setControlProperty.SetControlPoperties(lblMSICStatus, "BackColor", Color.Red);
                        setControlProperty.SetControlPoperties(lblMSICIP, "BackColor", Color.Red);
                        msicReaderConnected = false;
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                setControlProperty.SetControlPoperties(lblMSICStatus, "Text", "DISCONNECTED");
                setControlProperty.SetControlPoperties(lblMSICStatus, "BackColor", Color.Red);
                setControlProperty.SetControlPoperties(lblMSICIP, "BackColor", Color.Red);
             

                if (connectToReader)
                    setControlProperty.SetControlPoperties(lblMSICStatusDetails, "Text", "Could not connect to the reader.");
                else
                    setControlProperty.SetControlPoperties(lblMSICStatusDetails, "Text", "Could not disconnect from the reader.");

                msicReaderConnected = false;
                Cursor.Current = Cursors.Default;

            }
        }
        void MSICTagDetected(MISCTag Tag)
        {
            msicTagMonitoringCounter = 0;
            setControlProperty.SetControlPoperties(txtAccessCard,"Text", Tag.TagNumber);
        }
        private void MSICTagMonitoringTimer(object requestingObject)
        {

            if (msicTagMonitoringThreadTimer != null)
            {
                msicTagMonitoringCounter += 1;
                msicTagRemoved = false;

                msicTagMonitoringThreadTimer.Change(oneSecondThreadDueTime, System.Threading.Timeout.Infinite);

                //if (uhfTagMonitoringCounter % 2 == 0)
                //{
                //    uhfTagMonitoringCounter = 0;
                //    msicTagRemoved = true;
                //    if (insertMode)
                //        setControlProperty.SetControlPoperties(txtAccessCard, "Text", "");
                //    else
                //        setControlProperty.SetControlPoperties(txtAccessCard, "Text", currentAccessCardUID);
                //}

            }
        }
        #endregion
        private void SetUpUHFTagGrid()
        {
            
            try
            {
                ColumnView ColView_Helmet = grdUHFTags.MainView as ColumnView;

                string[] zFieldNames_Helmet = new string[] { "EPCData", "TIDPData", "TagGroup", "EPCDataFormatted", "TIDPDataFormatted" };

                DevExpress.XtraGrid.Columns.GridColumn zColum_Helmet;
                ColView_Helmet.Columns.Clear();
                for (int i = 0; i < zFieldNames_Helmet.Length; i++)
                {
                    zColum_Helmet = ColView_Helmet.Columns.AddField(zFieldNames_Helmet[i]);
                    zColum_Helmet.VisibleIndex = i;
                }
                gridView1.Columns[0].Caption = "EPC";
                gridView1.Columns[1].Caption = "TIDP";
                gridView1.Columns[2].Caption = "Group";
                gridView1.Columns[3].Caption = "Group #";
                gridView1.Columns[4].Caption = "Individual #";


                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Visible = false;
                gridView1.Columns[2].Visible = false;


                gridView1.Columns[3].Width = (int)(grdUHFTags.Width * .5);
                gridView1.Columns[3].OptionsColumn.AllowEdit = false;

                gridView1.Columns[4].Width = (int)(grdUHFTags.Width * .5);
                gridView1.Columns[4].OptionsColumn.AllowEdit = false;


                gridView1.OptionsCustomization.AllowColumnMoving = false;
                gridView1.OptionsCustomization.AllowColumnResizing = false;
                gridView1.OptionsCustomization.AllowRowSizing = false;
                gridView1.OptionsCustomization.AllowSort = false;
                gridView1.OptionsCustomization.AllowFilter = false;

                gridView1.OptionsView.ShowIndicator = false;
                gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
                gridView1.OptionsView.ShowColumnHeaders = true;
                gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
                gridView1.OptionsSelection.EnableAppearanceHideSelection = false;



            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "CEHCKER", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void SetUpUserGrid()
        {

            try
            {
                ColumnView ColView_Helmet = grdUsers.MainView as ColumnView;

                string[] zFieldNames_Helmet = new string[] { "USER_ID", "LOGON_ID", "TIDP" };

                DevExpress.XtraGrid.Columns.GridColumn zColum_Helmet;
                ColView_Helmet.Columns.Clear();
                for (int i = 0; i < zFieldNames_Helmet.Length; i++)
                {
                    zColum_Helmet = ColView_Helmet.Columns.AddField(zFieldNames_Helmet[i]);
                    zColum_Helmet.VisibleIndex = i;
                }
                gridView2.Columns[0].Caption = "ID";
                gridView2.Columns[1].Caption = "Name(Click name to edit)";
                gridView2.Columns[2].Caption = "Group #";


                gridView2.Columns[0].Visible = false;
                gridView2.Columns[1].Visible = true;
                gridView2.Columns[2].Visible = true;


                gridView2.Columns[1].Width = (int)(grdUsers.Width * .7);
                gridView2.Columns[1].OptionsColumn.AllowEdit = false;

                gridView2.Columns[2].Width = (int)(grdUsers.Width * .3);
                gridView2.Columns[2].OptionsColumn.AllowEdit = false;
                gridView2.Columns[2].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;


                gridView2.OptionsCustomization.AllowColumnMoving = false;
                gridView2.OptionsCustomization.AllowColumnResizing = false;
                gridView2.OptionsCustomization.AllowRowSizing = false;

            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "CEHCKER", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
       
        private void txtFirstName_EnabledChanged(object sender, EventArgs e)
        {
            if (uhfReaderStarted)
                StartUHFReader(false);

            if (uhfReaderConnected)
                ConnectToUHFReader(false);

            ConnectToMSICReader(false);
           
            uhfTagMonitoringThreadTimer = null;
            msicTagMonitoringThreadTimer = null;
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            UserDAL executeDeleteUser = new UserDAL();
            try
            {
                if (insertMode)
                {
                    MessageBox.Show("Select a Login ID to be Remove.", "Delete User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show(string.Format("Are you sure? Do you want to delete\n\nOperator: {0:s}", currenLogOnID), "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    applicationLogs.InsertApplicationLogs(
                                                 ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                                                  "User",
                                                  AppGlobals.globalCurrentSystemUser,
                                                 "Failed", "Deleted", currenLogOnID,"HQ","");
                    return;
                }
                executeDeleteUser.DeleteUser(currenLogOnID);

                MessageBox.Show(string.Concat("Login ID ", currenLogOnID, " has been deleted."), "Delete User", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PopulateUserList();
                ClearFields();

                applicationLogs.InsertApplicationLogs(
                             ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                              "User",
                              AppGlobals.globalCurrentSystemUser,
                             "Sucess", "Deleted", currenLogOnID, "HQ", "");

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Concat("Application Error!\n Please contact the administrator.\n",
                                              ex.Message.ToString(), ex.StackTrace.ToString()), "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                applicationLogs.InsertApplicationLogs(
                                      ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                                       "User",
                                       AppGlobals.globalCurrentSystemUser,
                                      "Failed", "Deleted", currenLogOnID, "HQ", "");

            }
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!uhfReaderConnected)
                {
                    ConnectToUHFReader(true);
                    if (uhfReaderConnected)
                        StartUHFReader(true);
                }
                if (!msicReaderConnected)
                    ConnectToMSICReader(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Concat("Application Error!\n Please contact the administrator.\n",
                                              ex.Message.ToString(), ex.StackTrace.ToString()), "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdRefreshTags_Click(object sender, EventArgs e)
        {
            try
            {
                uhfTagDetails.Rows.Clear();
                grdUHFTags.BeginInvoke(new MethodInvoker(delegate { grdUHFTags.DataSource = null; }));
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Concat("Application Error!\n Please contact the administrator.\n",
                                                ex.Message.ToString(), ex.StackTrace.ToString()), "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridView2_RowClick(object sender, RowClickEventArgs e)
        {
            System.Data.DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
          

            UserDAL getUserDetails = new UserDAL();
            DataTable userDetails = new DataTable();
            string userID = row[0].ToString(); 

            try
            {
                if (Convert.ToInt32(userID) > -1)
                {
                    ClearErrorNotification();
                    userDetails = getUserDetails.GetUserDetailsByID(Convert.ToInt32(userID));

                    if (userDetails.Rows.Count > 0)
                    {
                        currentUserID = Convert.ToInt32(userDetails.Rows[0]["USER_ID"]);
                        currentAccessCardUID = Convert.ToString(userDetails.Rows[0]["AccessCardUID"]);
                        currentHelmentTag = Convert.ToString(userDetails.Rows[0]["TIDP"]);
                        currenLogOnID = Convert.ToString(userDetails.Rows[0]["LOGON_ID"]);
                        currentPassword = Convert.ToString(userDetails.Rows[0]["ISM_PASSWORD"]);

                        setControlProperty.SetControlPoperties(txtFirstName, "Text", Convert.ToString(userDetails.Rows[0]["USER_FIRST_NAME"]));
                        setControlProperty.SetControlPoperties(txtLastName, "Text", Convert.ToString(userDetails.Rows[0]["USER_LAST_NAME"]));
                        setControlProperty.SetControlPoperties(txtUserID, "Text", Convert.ToString(userDetails.Rows[0]["LOGON_ID"]));
                        setControlProperty.SetControlPoperties(txtPassword, "Text", "");
                        setControlProperty.SetControlPoperties(txtConfirmPassword, "Text", "");
                        setControlProperty.SetControlPoperties(txtAccessCard, "Text", Convert.ToString(userDetails.Rows[0]["AccessCardUID"]));
                        setControlProperty.SetControlPoperties(txtHelmetTags, "Text", Convert.ToString(userDetails.Rows[0]["TIDP"]));
                        setControlProperty.SetControlPoperties(cmbRole, "EditValue", Convert.ToString(userDetails.Rows[0]["ProfileCode"]));

                        insertMode = false;
                        setControlProperty.SetControlPoperties(txtStatusMsg, "Text", "User Update Mode");
                        setControlProperty.SetControlPoperties(cmdSave, "Text", "Update User");
                        PopulateUserSyncDetails(Convert.ToInt32(userID));
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Concat("Application Error!\n Please contact the administrator.\n",
                                                ex.Message.ToString(), ex.StackTrace.ToString()), "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                getUserDetails = null;
            }
        }

        private void cmdUHF_Click(object sender, EventArgs e)
        {
            try
            {
                if (!uhfReaderConnected)
                {
                    ConnectToUHFReader(true);
                    if (uhfReaderConnected)
                        StartUHFReader(true);
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Concat("Application Error!\n Please contact the administrator.\n",
                                              ex.Message.ToString(), ex.StackTrace.ToString()), "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
