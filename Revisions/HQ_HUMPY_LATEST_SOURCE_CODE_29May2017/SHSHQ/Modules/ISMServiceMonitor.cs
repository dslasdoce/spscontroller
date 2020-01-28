using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ISMDAL.TableColumnName;
using System.Management;
using System.IO;
using System.Diagnostics;
using System.Security.Principal;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ISM.Modules
{
    public partial class ISMServiceMonitor : ISMBaseWorkSpace
    {
        string m_ServiceServerIP;
        ManagementScope m_Mgmscope = null;
        private volatile bool m_EndThread;                  
        private volatile bool m_BackUpEndThread;                  
        string m_ServiceAction;
         
         
         
        string m_ServiceType; 
         
         
         
        string m_LogFilePathAndName = "";
        string m_LogDistFileName = "";
        bool m_LogFileExist = false;

        #region "File Backup
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr handle);
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, out IntPtr phToken);
        #endregion


        public ISMServiceMonitor(ISMLoginInfo AISMLoginInfo)
            : base(AISMLoginInfo)
        {
            InitializeComponent();
        }

        private void ISMServiceMonitor_Load(object sender, EventArgs e)
        {
            try
            {
                m_EndThread = true;
                m_BackUpEndThread = true;

                BackupprogressBar.Style = ProgressBarStyle.Blocks;
                BackupprogressBar.Visible = false;
                lblProgressLable.Visible = false;
                wtBackup.WorkerReportsProgress = true;
                wtBackup.WorkerSupportsCancellation = true;

                progressBar.Style = ProgressBarStyle.Blocks;
                progressBar.Visible = false;
                wtDoIt.WorkerReportsProgress = true;
                wtDoIt.WorkerSupportsCancellation = true;

                m_ServiceServerIP = SHSHQ.Properties.Settings.Default.ServiceServerIP;
                m_Mgmscope = new ManagementScope(@"\\" + m_ServiceServerIP + @"\root\cimv2");
                m_ServiceAction = "FormLoad";
                m_ServiceType = "Status";
               
                progressBar.Visible = true;
                progressBar.Style = ProgressBarStyle.Marquee;
                m_EndThread = false;
                wtDoIt.RunWorkerAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void ServiceActivity()
        {
            try
            {
                 
                 
                 
                EnableOrDisableButton(false);
                if (!SHSHQ.Properties.Settings.Default.ServiceLocal)  
                {

                    ConnectionOptions zconnectoptions = new ConnectionOptions();
                    zconnectoptions.Impersonation = ImpersonationLevel.Impersonate;
                    zconnectoptions.Authentication = AuthenticationLevel.PacketPrivacy;
                    
                    zconnectoptions.EnablePrivileges = true;
                    zconnectoptions.Authority = null;

                     
                     
                     
                     
                     
                     
                    
                    
                    
                    
                    
                     
                     

                    zconnectoptions.Username = @"SHSHQ.INSTALL";
                    zconnectoptions.Password = "CasS95f^h3Ett99";
                    m_Mgmscope.Options = zconnectoptions;
                }
                if(!m_Mgmscope.IsConnected)
                    m_Mgmscope.Connect();

                
                ObjectGetOptions Objoptions = new ObjectGetOptions(null, TimeSpan.MaxValue, true);
                ManagementPath MgmspoolerPath = new ManagementPath("Win32_Service");
                ManagementClass servicesManager = new ManagementClass(m_Mgmscope, MgmspoolerPath, Objoptions);
                
                try
                {
                     
                    using (ManagementObjectCollection services = servicesManager.GetInstances())
                    {
                        bool zActiveService = false;
                        bool zPassiveService = false;

                        bool zActiveServiceExist = false;  
                        bool zPassiveServiceExist = false;  


                        if (m_ServiceAction == "FormLoad")
                        {
                            foreach (ManagementObject service in services)
                            {
                                if (service["Name"].ToString() == "ISMActiveMon" || service["Name"].ToString() == "ISMPassiveMon")
                                {
                                     
                                    if (service["Name"].ToString() == "ISMActiveMon")
                                        zActiveServiceExist = true;

                                    if (service["Name"].ToString() == "ISMPassiveMon")
                                        zPassiveServiceExist = true;
                                     

                                    if (service["Started"].Equals(true))
                                    {
                                        if (service["Name"].ToString() == "ISMActiveMon")
                                        {
                                            ServiceStatus("ISMActiveMon", "aRFID Service is running", "Start");
                                            zActiveService = true;
                                        }
                                        else if (service["Name"].ToString() == "ISMPassiveMon")
                                        {
                                            ServiceStatus("ISMPassiveMon", "pRFID Service is running", "Start");
                                            zPassiveService = true;
                                        }
                                    }
                                    else
                                    {
                                        if (service["Name"].ToString() == "ISMActiveMon")
                                        {
                                            ServiceStatus("ISMActiveMon", "aRFID Service is stopped", "Stop");
                                            zActiveService = true;
                                        }
                                        else if (service["Name"].ToString() == "ISMPassiveMon")
                                        {
                                            ServiceStatus("ISMPassiveMon", "pRFID Service is stopped", "Stop");
                                            zPassiveService = true;
                                        }
                                    }
                                    if (zActiveService && zPassiveService)
                                        break;
                                }
                           }
                             
                            if (zActiveServiceExist)
                            {
                                EnableOrDisableButtoActive(true);
                            }
                            else
                            {
                                EnableOrDisableButtoActive(false);
                            }
                            if (zPassiveServiceExist)
                            {
                                EnableOrDisableButtonPassive(true);
                            }
                            else
                            {
                                EnableOrDisableButtonPassive(false);
                            }
                             
 
                        }
                        else
                        {
                            foreach (ManagementObject service in services)
                            {
                                if (service["Name"].ToString() == m_ServiceAction)
                                {

                                    if (m_ServiceType == "StopService")
                                    {
                                        if (service["Started"].Equals(true))
                                        {
                                             
                                            service.InvokeMethod("StopService", null);
                                            if (m_ServiceAction == "ISMActiveMon")
                                            {
                                                ServiceStatus("ISMActiveMon", "aRFID Service is stopped", "Stop");
                                                 
                                            }
                                            else if (m_ServiceAction == "ISMPassiveMon")
                                            {
                                                ServiceStatus("ISMPassiveMon", "pRFID Service is stopped", "Stop");
                                                 
                                            }
                                            break;
                                        }
                                    }
                                    else if (m_ServiceType == "StartService")
                                    {
                                         
                                        service.InvokeMethod("StartService", null);
                                        if (m_ServiceAction == "ISMActiveMon")
                                        {
                                            ServiceStatus("ISMActiveMon", "aRFID Service is started", "Start");
                                             
                                        }
                                        else if (m_ServiceAction == "ISMPassiveMon")
                                        {
                                            ServiceStatus("ISMPassiveMon", "pRFID Service is started", "Start");
                                             
                                        }
                                        break;
                                    }

                                }
                            }
                            GetCurrentStatus(servicesManager);

                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    throw;
                } 

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        void GetCurrentStatus(ManagementClass AservicesManager)
        {
            try
            {
                PauseForMilliSeconds(1000);
                 
                using (ManagementObjectCollection services = AservicesManager.GetInstances())
                {
                    bool zActiveService = false;
                    bool zPassiveService = false;
                    bool zActiveServiceExist = false;  
                    bool ZPassiveServiceExist = false;  

                     
                    EnableOrDisableButtoActive(false);
                    EnableOrDisableButtonPassive(false);
                     

                    foreach (ManagementObject service in services)
                    {
                        if (service["Name"].ToString() == "ISMActiveMon" || service["Name"].ToString() == "ISMPassiveMon")
                        {
                             
                            if (service["Name"].ToString() == "ISMActiveMon")
                            {
                                EnableOrDisableButtoActive(true);
                            }

                            if (service["Name"].ToString() == "ISMPassiveMon")
                            {
                                EnableOrDisableButtonPassive(true);
                            }
                             

                            if (service["Started"].Equals(true))
                            {
                                if (service["Name"].ToString() == "ISMActiveMon")
                                {
                                    ServiceStatus("ISMActiveMon", "aRFID Service is started", "Start");
                                    {
                                        zActiveService = true;
                                    }
                                }
                                else if (service["Name"].ToString() == "ISMPassiveMon")
                                {
                                    ServiceStatus("ISMPassiveMon", "pRFID Service is started", "Start");
                                    zPassiveService = true;
                                }
                            }
                            else
                            {
                                if (service["Name"].ToString() == "ISMActiveMon")
                                {
                                    ServiceStatus("ISMActiveMon", "aRFID Service is stopped", "Stop");
                                    zActiveService = true;
                                }
                                else if (service["Name"].ToString() == "ISMPassiveMon")
                                {
                                    ServiceStatus("ISMPassiveMon", "pRFID Service is stopped", "Stop");
                                    zPassiveService = true;
                                }
                            }
                            if (zActiveService && zPassiveService)
                                break;
                        }
                    }
                     
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        private delegate void ServiceStatusDelegate();
        private void ServiceStatus(string AServiceType, string AMessage, string AStatus)
        {
            Invoke(new ServiceStatusDelegate(delegate
            {
                if (AServiceType == "ISMActiveMon")
                {
                    lblActiveServiceStatus.Text = AMessage;
                    if (AStatus == "Start")
                    {
                        btnActiveStart.Enabled = false;
                        btnActiveStop.Enabled = true;
                    }
                    else if (AStatus == "Stop")
                    {
                        btnActiveStart.Enabled = true;
                        btnActiveStop.Enabled = false;
                    }
                }
                else if (AServiceType == "ISMPassiveMon")
                {
                    lblPassiveServiceStatus.Text = AMessage;
                    if (AStatus == "Start")
                    {
                        btnPassiveStart.Enabled = false;
                        btnPassiveStop.Enabled = true;
                    }
                    else if (AStatus == "Stop")
                    {
                        btnPassiveStart.Enabled = true;
                        btnPassiveStop.Enabled = false;
                    }

                }
            }));
        }
        private delegate void CompletionStatusDelegate();
        private void CompletionStatus(int APercent)
        {
            Invoke(new CompletionStatusDelegate(delegate { BackupprogressBar.Value = APercent; }));
        }
        private delegate void FileCopyCompletionStatusDelegate();
        private void FileCopyCompletionStatus(long AMaxValue, int APercent)
        {
            Invoke(new FileCopyCompletionStatusDelegate(delegate
            {
                BackupprogressBar.Maximum = (int)AMaxValue;
                BackupprogressBar.Minimum = 0;

                BackupprogressBar.Value = APercent;
            }));
        }

        private delegate void EnableOrDisableButtonDelegate();
        private void EnableOrDisableButton(bool AEnable)
        {
            Invoke(new EnableOrDisableButtonDelegate(delegate
            {
                btnActiveStart.Enabled = AEnable;
                btnActiveStop.Enabled = AEnable;
                btnPassiveStart.Enabled = AEnable;
                btnPassiveStop.Enabled = AEnable;
                bntActiveBackup.Enabled = AEnable;
                bntPassiveBackup.Enabled = AEnable;

            }));
        }

        private delegate void EnableOrDisableButtoActiveDelegate();
        private void EnableOrDisableButtoActive(bool AEnable)
        {
            Invoke(new EnableOrDisableButtoActiveDelegate(delegate
            {
                 
                
                bntActiveBackup.Enabled = AEnable;

            }));
        }
        private delegate void EnableOrDisableButtonPassiveDelegate();
        private void EnableOrDisableButtonPassive(bool AEnable)
        {
            Invoke(new EnableOrDisableButtonPassiveDelegate(delegate
            {
                
                
                bntPassiveBackup.Enabled = AEnable;

            }));
        }
        private void wtDoIt_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (wtDoIt.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                else if (!m_EndThread)
                {
                    ServiceActivity();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void wtDoIt_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                progressBar.Value = e.ProgressPercentage;
            }

            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void wtDoIt_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled)
                {
                    progressBar.Style = ProgressBarStyle.Blocks;
                    progressBar.Visible = false;
                }
                else if (e.Error != null)
                {
                    progressBar.Style = ProgressBarStyle.Blocks;
                    progressBar.Visible = false;
                }
                else
                {
                    progressBar.Style = ProgressBarStyle.Blocks;
                    progressBar.Visible = false;
                    m_EndThread = true;
                }   

            }

            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

     
        #region "Active Service Action"
        private void btnActiveStart_Click(object sender, EventArgs e)
        {
            try
            {
                txtStatusMsg.Text = "";
                if (wtDoIt.IsBusy)
                {
                    if (MessageBox.Show("System is verifying service status. Do you want cancel?", lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        CancelServiceStatusProcess();
                        txtStatusMsg.Text = "Checking service status cancelled by User...";
                    }
                }
                else if (wtBackup.IsBusy)
                {
                    if (MessageBox.Show("Service Log file backup is in progress. Do you want cancel?", lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        CancelBackupStatusProcess();
                        txtStatusMsg.Text = "Service Log file backup task is cancelled by User ...";
                    }
                }
                else
                {

                    txtStatusMsg.Text = "";
                    m_ServiceAction = "ISMActiveMon";
                    m_ServiceType = "StartService";
                    progressBar.Visible = true;
                    progressBar.Style = ProgressBarStyle.Marquee;
                    m_EndThread = false;
                    wtDoIt.RunWorkerAsync();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        private void btnActiveStop_Click(object sender, EventArgs e)
        {
            try
            {
                txtStatusMsg.Text = "";
                if (wtDoIt.IsBusy)
                {
                    if (MessageBox.Show("System is verifying service status. Do you want cancel?", lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        CancelServiceStatusProcess();
                        txtStatusMsg.Text = "Checking service status cancelled by User...";
                    }
                }
                else if (wtBackup.IsBusy)
                {
                    if (MessageBox.Show("Service Log file backup is in progress. Do you want cancel?", lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        CancelBackupStatusProcess();
                        txtStatusMsg.Text = "Service Log file backup task is cancelled by User ...";
                    }
                }
                else
                {
                    txtStatusMsg.Text = "";
                    m_ServiceAction = "ISMActiveMon";
                    m_ServiceType = "StopService";
                    progressBar.Visible = true;
                    progressBar.Style = ProgressBarStyle.Marquee;
                    m_EndThread = false;
                    wtDoIt.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        private void btnPassiveStart_Click(object sender, EventArgs e)
        {
            try
            {
                txtStatusMsg.Text = "";
                if (wtDoIt.IsBusy)
                {
                    if (MessageBox.Show("System is verifying service status. Do you want cancel?", lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        CancelServiceStatusProcess();
                        txtStatusMsg.Text = "Checking service status cancelled by User...";
                    }
                }
                else if (wtBackup.IsBusy)
                {
                    if (MessageBox.Show("Service Log file backup is in progress. Do you want cancel?", lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        CancelBackupStatusProcess();
                        txtStatusMsg.Text = "Service Log file backup task is cancelled by User ...";
                    }
                }
                else
                {
                    txtStatusMsg.Text = "";
                    m_ServiceAction = "ISMPassiveMon";
                    m_ServiceType = "StartService";
                    progressBar.Visible = true;
                    progressBar.Style = ProgressBarStyle.Marquee;
                    m_EndThread = false;
                    wtDoIt.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void btnPassiveStop_Click(object sender, EventArgs e)
        {
            try
            {
                txtStatusMsg.Text = "";
                if (wtDoIt.IsBusy)
                {
                    if (MessageBox.Show("System is verifying service status. Do you want cancel?", lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        CancelServiceStatusProcess();
                        txtStatusMsg.Text = "Checking service status cancelled by User...";
                    }
                }
                else if (wtBackup.IsBusy)
                {
                    if (MessageBox.Show("Service Log file backup is in progress. Do you want cancel?", lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        CancelBackupStatusProcess();
                        txtStatusMsg.Text = "Service Log file backup task is cancelled by User ...";
                    }
                }
                else
                {
                    txtStatusMsg.Text = "";
                    m_ServiceAction = "ISMPassiveMon";
                    m_ServiceType = "StopService";
                    progressBar.Visible = true;
                    progressBar.Style = ProgressBarStyle.Marquee;
                    m_EndThread = false;
                    wtDoIt.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        public void PauseForMilliSeconds(int MilliSecondsToPauseFor)
        {
            System.DateTime ThisMoment = System.DateTime.Now;
            System.TimeSpan duration = new System.TimeSpan(0, 0, 0, 0, MilliSecondsToPauseFor);
            System.DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = System.DateTime.Now;
            }

        }
        #endregion

        #region "Backup"
        private void bntActiveBackup_Click(object sender, EventArgs e)
        {
            try
            {
                txtStatusMsg.Text = "";
                if (wtDoIt.IsBusy)
                {
                    if (MessageBox.Show("System is verifying service status. Do you want cancel?", lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        CancelServiceStatusProcess();
                        txtStatusMsg.Text = "Checking service status cancelled by User...";
                    }
                }
                else if (wtBackup.IsBusy)
                {
                    if (MessageBox.Show("Service Log file backup is in progress. Do you want cancel?", lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        CancelBackupStatusProcess();
                        txtStatusMsg.Text = "Service Log file backup task is cancelled by User ...";
                    }
                }
                else
                {
                    if (!SHSHQ.Properties.Settings.Default.ServiceLocal)  
                        m_LogFilePathAndName = Path.Combine(@"\\" + m_ServiceServerIP + "\\C$\\", "Windows\\System32\\ISMActiveMon.log");
                    else
                        m_LogFilePathAndName = "C:\\Windows\\System32\\ISMActiveMon.log";  
                     
                    m_LogDistFileName = m_ISMLoginInfo.Params.ReportFolder + "\\aRFIDservice.log";  
                    BackupprogressBar.Visible = true;
                    lblProgressLable.Visible = true;
                    BackupprogressBar.Style = ProgressBarStyle.Marquee;
                    m_BackUpEndThread = false;
                    wtBackup.RunWorkerAsync();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }

        private void bntPassiveBackup_Click(object sender, EventArgs e)
        {
            try
            {
                txtStatusMsg.Text = "";
                if (wtDoIt.IsBusy)
                {
                    if (MessageBox.Show("System is verifying service status. Do you want cancel?", lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        CancelServiceStatusProcess();
                        txtStatusMsg.Text = "Checking service status cancelled by User...";
                    }
                }
                else if (wtBackup.IsBusy)
                {
                    if (MessageBox.Show("Service Log file backup is in progress. Do you want cancel?", lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        CancelBackupStatusProcess();
                        txtStatusMsg.Text = "Service Log file backup task is cancelled by User ...";
                    }
                }
                else
                {
                    if (!SHSHQ.Properties.Settings.Default.ServiceLocal)  
                        m_LogFilePathAndName = Path.Combine(@"\\" + m_ServiceServerIP + "\\C$\\", "Windows\\System32\\ISMPassiveMon.log");
                    else
                        m_LogFilePathAndName = "C:\\Windows\\System32\\ISMPassiveMon.log";  

                    
                     
                    m_LogDistFileName = m_ISMLoginInfo.Params.ReportFolder + "\\pRFIDService.log";  
                    BackupprogressBar.Visible = true;
                    lblProgressLable.Visible = true;
                    BackupprogressBar.Style = ProgressBarStyle.Marquee;
                    m_BackUpEndThread = false;
                    wtBackup.RunWorkerAsync();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
     
        private void CopyFile(string zSourceFile, string zDestinationFile)
        {
            try
            {

                byte[] m_aBuffer = new byte[1024];
                Int32 m_iBytes = default(Int32);
                Int32 m_ProgressValue = 0;
                long m_FileLenth = 0;
                 
                CompletionStatus(0);
                FileStream FS = new FileStream(zSourceFile, FileMode.Open);
                FileStream FW = new FileStream(zDestinationFile, FileMode.Create);
                m_FileLenth = FS.Length;  
                do
                {
                    m_aBuffer.Initialize();

                    m_iBytes = FS.Read(m_aBuffer, 0, m_aBuffer.Length);
                    FW.Write(m_aBuffer, 0, m_iBytes);
                    m_ProgressValue += m_iBytes;
                    FileCopyCompletionStatus(m_FileLenth, m_ProgressValue);
                    if ((m_iBytes <= 0))
                    {
                        break;
                    }
                } while (!(m_iBytes <= 0));
                 
                FS.Close();
                FW.Close();
                FS.Dispose();
                FW.Dispose();
                FS = null;
                FW = null;
            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        void CancelServiceStatusProcess()
        {
            try
            {
                wtDoIt.CancelAsync();
                while (wtDoIt.IsBusy)
                    Application.DoEvents();
                m_EndThread = true;
                progressBar.Visible = false;
            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        void CancelBackupStatusProcess()
        {
            try
            {

                wtBackup.CancelAsync();
                while (wtBackup.IsBusy)
                    Application.DoEvents();
                m_BackUpEndThread = true;
                BackupprogressBar.Visible = false;
                lblProgressLable.Visible = false;

            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
       

        private void wtBackup_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (wtDoIt.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                else if (!m_BackUpEndThread)
                {
                    m_LogFileExist = false;

                    IntPtr token = IntPtr.Zero;
                    WindowsImpersonationContext impersonateUser = null;
                    try
                    {
                        if (!SHSHQ.Properties.Settings.Default.ServiceLocal)  
                        {
                            bool result = LogonUser(@"SHSHQ.INSTALL", m_ServiceServerIP, "CasS95f^h3Ett99", 9, 0, out token);
                            if (result)
                            {

                                WindowsIdentity id = new WindowsIdentity(token);
                                impersonateUser = id.Impersonate();
                                string showtext = string.Format("Identity: {0}", WindowsIdentity.GetCurrent().Name);
                                 
                                WindowsImpersonationContext context = WindowsIdentity.Impersonate(token);
                                if (File.Exists(m_LogFilePathAndName))
                                {
                                     
                                    CopyFile(m_LogFilePathAndName, m_LogDistFileName);
                                    m_LogFileExist = true;
                                }
                            }

                            else
                            {
                                string showtext = string.Format("Identity: {0}", "Fail");
                                MessageBox.Show(showtext);
                            }
                        }
                        else
                        {
                            if (File.Exists(m_LogFilePathAndName))
                            {
                                 
                                CopyFile(m_LogFilePathAndName, m_LogDistFileName);
                                m_LogFileExist = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (impersonateUser != null)
                            impersonateUser.Undo();
                        if (token != IntPtr.Zero)
                            CloseHandle(token);
                    } 


                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void wtBackup_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                BackupprogressBar.Value = e.ProgressPercentage;
            }

            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void wtBackup_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled)
                {
                    BackupprogressBar.Style = ProgressBarStyle.Blocks;
                    BackupprogressBar.Visible = false;
                    lblProgressLable.Visible = false;
                }
                else if (e.Error != null)
                {
                    BackupprogressBar.Style = ProgressBarStyle.Blocks;
                    BackupprogressBar.Visible = false;
                    lblProgressLable.Visible = false;
                }
                else
                {
                    BackupprogressBar.Style = ProgressBarStyle.Blocks;
                    BackupprogressBar.Visible = false;
                    lblProgressLable.Visible = false;
                    if (m_LogFileExist)
                    {
                        m_LogDistFileName = m_LogDistFileName.Replace("\\\\","\\"); 
                        txtStatusMsg.Text = "Service log file has been saved in " + m_LogDistFileName;
                    }
                    else
                        txtStatusMsg.Text = "Service log file does not exist";
                    m_BackUpEndThread = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        private void ISMServiceMonitor_Leave(object sender, EventArgs e)
        {
            try
            {
                if (wtDoIt.IsBusy)
                {
                   CancelServiceStatusProcess();
                }
                else if (wtBackup.IsBusy)
                {
                   CancelBackupStatusProcess();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void GetRemoteServerServiceVersion(ref string AaRFIDVersionNo, ref string AaRFIDProdIdentifier, ref string ApRFIDVersionNo, ref string ApRFIDProdIdentifier)
        {
            try
            {
                string Host = m_ServiceServerIP;
                string Path = (@"\\" + Host + @"\root\CIMV2");
                var options = new ConnectionOptions();
                options.Username = @"SHSHQ.INSTALL";
                options.Password = "CasS95f^h3Ett99";

                var scope = new ManagementScope(Path, options);
                var queryString = string.Format("SELECT * FROM Win32_Product WHERE Name = '{0}'", "ISM RFID Passive Monitor");
                var query = new SelectQuery(queryString);
                var searcher = new ManagementObjectSearcher(scope, query);

                using (ManagementObjectCollection products = searcher.Get())
                    foreach (ManagementObject product in products)
                    {
                        ApRFIDVersionNo = (string)product["Version"];
                        ApRFIDProdIdentifier = (string)product["IdentifyingNumber"];
                    }

                queryString = string.Format("SELECT * FROM Win32_Product WHERE Name = '{0}'", "ISM RFID Active Monitor");
                query = new SelectQuery(queryString);
                searcher = new ManagementObjectSearcher(scope, query);

                using (ManagementObjectCollection products = searcher.Get())
                    foreach (ManagementObject product in products)
                    {
                        AaRFIDVersionNo = (string)product["Version"];
                        AaRFIDProdIdentifier = (string)product["IdentifyingNumber"];
                    }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool GetLocalMachineServerServiceVersion(string AMachineName, string AServiceName, ref string AVersionNo, ref string AprodIdentifier)
        {
            string keyName;

             
            keyName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            if (ExistsInRemoteSubKey(AMachineName, RegistryHive.CurrentUser, keyName, "DisplayName", AServiceName, ref AVersionNo, ref AprodIdentifier) == true)
            {
                return true;
            }

             
            keyName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            if (ExistsInRemoteSubKey(AMachineName, RegistryHive.LocalMachine, keyName, "DisplayName", AServiceName, ref AVersionNo, ref AprodIdentifier) == true)
            {
                return true;
            }

             
            keyName = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
            if (ExistsInRemoteSubKey(AMachineName, RegistryHive.LocalMachine, keyName, "DisplayName", AServiceName, ref AVersionNo, ref AprodIdentifier) == true)
            {
                return true;
            }

            return false;
        }
        private static bool ExistsInRemoteSubKey(string AMachineName, RegistryHive p_hive, string p_subKeyName, string p_attributeName, string p_name, ref string AVersionNo, ref string AprodIdentifier)
        {
            RegistryKey subkey;
            string zDisplayName;

            using (RegistryKey regHive = RegistryKey.OpenRemoteBaseKey(p_hive, AMachineName))
            {
                using (RegistryKey regKey = regHive.OpenSubKey(p_subKeyName))
                {
                    if (regKey != null)
                    {
                        foreach (string kn in regKey.GetSubKeyNames())
                        {
                            using (subkey = regKey.OpenSubKey(kn))
                            {
                                zDisplayName = subkey.GetValue(p_attributeName) as string;
                                if (p_name.Equals(zDisplayName, StringComparison.OrdinalIgnoreCase) == true)  
                                {
                                    AVersionNo = subkey.GetValue("DisplayVersion") as string;
                                    AprodIdentifier = subkey.GetValue("UninstallString").ToString().Remove(0, 14);
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
