
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ISMDAL.TableColumnName;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace ISM.Modules
{
    public partial class ISMDatabaseBackup : ISMBaseWorkSpace
    {
        private volatile bool m_EndThread;                  
        private static Server srvSql;
        private string m_DBFileName;
        private string m_DBFilePathAndName;
        private string m_DBBackupType;
        private string m_DBServerName;
        private string m_RemoteServerBackupFolder;
        private bool m_DBBackup;  

         
        #region "File Backup
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr handle);
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, out IntPtr phToken);
        #endregion

        public ISMDatabaseBackup(ISMLoginInfo AISMLoginInfo)
            : base(AISMLoginInfo)
        {
            InitializeComponent();
        }

        private void ISMDatabaseBackup_Load(object sender, EventArgs e)
        {
            try
            {
                m_EndThread = true;    
                BackupprogressBar.Visible = false;
                lblProgressLable.Visible = false;

                progressBar.Style = ProgressBarStyle.Blocks;
                progressBar.Visible = false;
                wtDoIt.WorkerReportsProgress = true;
                wtDoIt.WorkerSupportsCancellation = true;
                m_DBBackupType = SHSHQ.Properties.Settings.Default.DBBackupType;

                string zConString =  SHSHQ.Properties.Settings.Default.ConnectionString;
                string[] zdata  =  zConString.Split('=');
                string[] zDataSoruce = { "" };
                string[] zDatabase = { "" };
                
                string zUserID = "ismsupport";  
                string zPassword = "p@ssw0rd";  
                bool zDBFound = false;

                 
                if (zdata.Length == 4)
                {
                    zDataSoruce = zdata[1].Split(';');
                    zDatabase = zdata[2].Split(';');
                    
                }
                else
                {
                    txtStatusMsg.Text = "Database Connection String is not in Order. Please Contact System Administrator";
                    MessageBox.Show("Database Connection String is not in Order. Please Contact System Administrator", "Database Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                 
                if (zDataSoruce.Length != 2 || zDatabase.Length != 2)
                {
                    txtStatusMsg.Text = "Database Connection String is not in Order. Please Contact System Administrator";
                    MessageBox.Show("Database Connection String is not in Order. Please Contact System Administrator", "Database Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                lblDBServerName.Text = zDataSoruce[0];

                ServerConnection srvConn = new ServerConnection(lblDBServerName.Text);
                 
                m_DBServerName = zDataSoruce[0];
                 
                srvConn.LoginSecure = false;
                 
                 
                srvConn.Login = zUserID;  
                 
                 
                srvConn.Password = zPassword;  
                 
                srvSql = new Server(srvConn);
                 
                foreach (Database dbServer in srvSql.Databases)
                {
                    if (dbServer.Name.ToUpper() == zDatabase[0].ToUpper())
                    {
                        lblDbName.Text = dbServer.Name.ToUpper();
                        zDBFound = true;
                        break;

                    }
                }
                if (!zDBFound)
                {
                    lblDbName.Text = zDatabase[0].ToUpper() + " Does not exist";
                    txtStatusMsg.Text = zDatabase[0].ToUpper() + " Does not exist. Please Contact System Administrator";
                    MessageBox.Show(zDatabase[0].ToUpper() + " Does not exist. Please Contact System Administrator", "Database Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }

            }
            catch
            {
                txtStatusMsg.Text = "Database Connection String is not in Order. Please Contact System Administrator";
            }
        }

        private void btnStartDataTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnStartDataTransfer.Text == "Stop Database Backup")
                {
                    if (MessageBox.Show("Database Backup is in progress. Do you want cancel?", "Database Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        btnStartDataTransfer.Enabled = false;
                        EnableOrDisableProgressBar(false);
                        txtStatusMsg.Text = "Please wait. System cancelling Backup Process ";
                        m_EndThread = true;
                        wtDoIt.CancelAsync();
                        while (wtDoIt.IsBusy)
                            Application.DoEvents();
                         
                        if (m_DBBackupType.ToUpper() == "REMOTE")
                        {
                            if (File.Exists(m_DBFilePathAndName))
                                File.Delete(m_DBFilePathAndName);
                            if (File.Exists(m_DBFileName))
                                File.Delete(m_DBFileName);

                        }
                        else
                        {
                            if (File.Exists(m_DBFileName))
                                File.Delete(m_DBFileName);
                        }
                        btnStartDataTransfer.Enabled = true;
                        btnStartDataTransfer.Text = "Start Database Backup...";
                        txtStatusMsg.Text = "Database Backup cancelled by User ...";
                        m_ISMLoginInfo.ISMServer.AddInterfaceJournal("DBB003", m_ISMLoginInfo.UserID);
                    }
                }
                else
                {
                    m_DBBackup = false;  
                    if(m_ISMLoginInfo.Params.ReportFolder.Trim() != "")
                        BackupDlg.FileName = m_ISMLoginInfo.Params.ReportFolder +  String.Format("{0:yyMMdd}", DateTime.Now) + ".bak";  
                    else
                        BackupDlg.FileName = "C:\\" + String.Format("{0:yyMMdd}", DateTime.Now);

                    string[] zRemoteServerFolder = m_ISMLoginInfo.Params.ReportFolder.Split('\\');

                    if (zRemoteServerFolder.Length > 0)
                    {
                         
                        m_RemoteServerBackupFolder = zRemoteServerFolder[0].Replace(":", "$") +"\\"+zRemoteServerFolder[1];
                    }
                    else
                    {
                        MessageBox.Show("Database Server does not have Shard folder. Please Contact System Administrator", "Database Backup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (BackupDlg.ShowDialog() == DialogResult.OK)
                    {
                        m_DBFileName = BackupDlg.FileName;

                        progressBar.Visible = true;
                        progressBar.Style = ProgressBarStyle.Marquee;
                        m_EndThread = false;

                        btnStartDataTransfer.Text = "Stop Database Backup";
                        btnStartDataTransfer.ImageIndex = 5;
                        txtStatusMsg.Text = "";
                        wtDoIt.RunWorkerAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error. Contact System Administrator. Error Details : " + ex.Message, "Database Backup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

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
                    Backup bkpDatabase = new Backup();
                     
                    bkpDatabase.Action = BackupActionType.Database;
                     
                    bkpDatabase.Database = lblDbName.Text.Trim();
                    string[] zdata = m_DBFileName.Split('\\');
                    string zBackupFileName = "";
                    int zCount = zdata.Length;
                    if (zCount > 0)
                    {
                        for (int zIndex = 0; zIndex < zCount; zIndex++)
                        {
                            if (zdata[zIndex].Contains(".bak"))
                                zBackupFileName = zdata[zIndex];
                        }

                    }

                     
                     
                    BackupDeviceItem bkpDevice = null;
                    if (m_DBBackupType.ToUpper() == "REMOTE")
                         bkpDevice = new BackupDeviceItem(m_ISMLoginInfo.Params.ReportFolder + zBackupFileName, DeviceType.File);
                    else
                        bkpDevice = new BackupDeviceItem(m_DBFileName, DeviceType.File);

                     

                    bkpDatabase.Initialize = true;  
                    bkpDatabase.Checksum = true;  
                    bkpDatabase.Devices.Add(bkpDevice);
                    /* Specify whether you want to backup database, files or log */
                    bkpDatabase.Action = BackupActionType.Database;
                    /* You can specify Initialize = false (default) to create a new 
                     * backup set which will be appended as last backup set on the media.
                     * You can specify Initialize = true to make the backup as the first set
                     * on the medium and to overwrite any other existing backup sets if the
                     * backup sets have expired and specified backup set name matches
                     * with the name on the medium */

                     
                    /* Wiring up events for progress monitoring */
                    bkpDatabase.PercentComplete += CompletionStatusInPercent;
                    bkpDatabase.Complete += CompleteBackup;
                    EnableOrDisableProgressBar(true);
                    bkpDatabase.SqlBackup(srvSql);

                    m_ISMLoginInfo.ISMServer.AddInterfaceJournal("DBB001", m_ISMLoginInfo.UserID);
                     
                    
                    m_DBFilePathAndName = @"\\"+m_DBServerName + "\\" + m_RemoteServerBackupFolder + "\\" + zBackupFileName;
                    
                    if (m_DBBackupType.ToUpper() == "REMOTE")
                    {
                        BackupDatbaseFileToLocal();
                         
                         
                         
                         
                         
                         
                    }
                    m_DBBackup = true;  
                }
            }
            catch (Exception ex)
            {
                m_ISMLoginInfo.ISMServer.AddInterfaceJournal("DBB002", m_ISMLoginInfo.UserID);
                MessageBox.Show("System Error: " + ex.Message + " Contact System Administrator", "Database Backup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        void BackupDatbaseFileToLocal()
        {
            IntPtr token = IntPtr.Zero;
            WindowsImpersonationContext impersonateUser = null;
            try
            {
                bool result = LogonUser(@"ISM.INSTALL", m_DBServerName, "CasS95f^h3Ett99", 9, 0, out token);
                if (result)
                {

                    WindowsIdentity id = new WindowsIdentity(token);
                    impersonateUser = id.Impersonate();
                    string showtext = string.Format("Identity: {0}", WindowsIdentity.GetCurrent().Name);
                     
                    WindowsImpersonationContext context = WindowsIdentity.Impersonate(token);
                    if (File.Exists(m_DBFilePathAndName))
                    {
                         
                        CopyFile(m_DBFilePathAndName, m_DBFileName);
                        File.Delete(m_DBFilePathAndName);
                    }
                }

                else
                {
                    string showtext = string.Format("Identity: {0}", "Fail");
                    MessageBox.Show(showtext);
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
        void DeleteBackupDatbase()
        {
            IntPtr token = IntPtr.Zero;
            WindowsImpersonationContext impersonateUser = null;
            try
            {
                bool result = LogonUser(@"ISM.INSTALL", m_DBServerName, "CasS95f^h3Ett99", 9, 0, out token);
                if (result)
                {

                    WindowsIdentity id = new WindowsIdentity(token);
                    impersonateUser = id.Impersonate();
                    string showtext = string.Format("Identity: {0}", WindowsIdentity.GetCurrent().Name);
                     
                    WindowsImpersonationContext context = WindowsIdentity.Impersonate(token);
                    if (m_DBBackupType.ToUpper() == "REMOTE")
                    {
                        if (File.Exists(m_DBFilePathAndName))
                            File.Delete(m_DBFilePathAndName);
                        if (File.Exists(m_DBFileName))
                            File.Delete(m_DBFileName);

                    }
                    else
                    {
                        if (File.Exists(m_DBFileName))
                            File.Delete(m_DBFileName);

                    }
                }

                else
                {
                    string showtext = string.Format("Identity: {0}", "Fail");
                    MessageBox.Show(showtext);
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
        private void wtDoIt_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Cancelled) 
            {
                DeleteBackupDatbase();
                 
                 
                 
                 
                 
                 
                 

                 
                 
                 
                 
                 

                 
                progressBar.Style = ProgressBarStyle.Blocks;
                progressBar.Visible = false;
                btnStartDataTransfer.Text = "Database Backup cancelled by User ...";
                btnStartDataTransfer.ImageIndex = 6;
            }   
            else if (e.Error != null)   
            {
                progressBar.Style = ProgressBarStyle.Blocks;
                progressBar.Visible = false;
                btnStartDataTransfer.Text = "Error on Database Backup : " + (e.Error as Exception).ToString();   
                btnStartDataTransfer.ImageIndex = 6;
            }   
           else 
            {
                progressBar.Style = ProgressBarStyle.Blocks;
                progressBar.Visible = false;
                btnStartDataTransfer.Text = "Start Database Backup...";
                btnStartDataTransfer.ImageIndex = 6;
                if (m_DBBackup)  
                    txtStatusMsg.Text = "Database " + lblDbName.Text + " Backup File saved in Database Server " + m_DBFileName;
                else
                    txtStatusMsg.Text = "Database " + lblDbName.Text + " Backup is fail";  
                m_EndThread = true;
            }   
              

        }
    
         private void ISMDatabaseBackup_Leave(object sender, EventArgs e)
        {
            try
            {
                 
                 
                 
                 
                if (!m_EndThread)
                {
                    if (MessageBox.Show("Database Backup is in progress. Do you want cancel?", "Database Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        btnStartDataTransfer.Enabled = false;
                        EnableOrDisableProgressBar(false);
                        txtStatusMsg.Text = "Please wait. System cancelling Backup Process ";
                        m_EndThread = true;
                        wtDoIt.CancelAsync();
                        while (wtDoIt.IsBusy)
                            Application.DoEvents();

                        DeleteBackupDatbase();
                         
                         
                         
                         
                         
                         
                         

                         
                         
                         
                         
                         
                         
                        btnStartDataTransfer.Enabled = true;
                        btnStartDataTransfer.Text = "Start Database Backup...";
                        txtStatusMsg.Text = "Database Backup cancelled by User ...";
                        m_ISMLoginInfo.ISMServer.AddInterfaceJournal("DBB003", m_ISMLoginInfo.UserID);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error. Contact System Administrator. Error Details :" + ex.Message, "Database Backup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

         private void wtDoIt_ProgressChanged(object sender, ProgressChangedEventArgs e)
         {
             progressBar.Value = e.ProgressPercentage;
         }

         private delegate void CompletionStatusDelegate();
         private void CompletionStatus(int APercent)
         {
             Invoke(new CompletionStatusDelegate(delegate { BackupprogressBar.Value = APercent; }));
         }
         private delegate void FileCopyCompletionStatusDelegate();
         private void FileCopyCompletionStatus(long AMaxValue, int APercent)
         {
             Invoke(new FileCopyCompletionStatusDelegate(delegate {
                 BackupprogressBar.Maximum = (int)AMaxValue;
                 BackupprogressBar.Minimum = 0;

                 BackupprogressBar.Value = APercent; 
             }));
         }

         private delegate void EnableOrDisableProgressBarDelegate();
         private void EnableOrDisableProgressBar(bool AEnable)
         {
             Invoke(new EnableOrDisableProgressBarDelegate(delegate
             {
                 BackupprogressBar.Visible = AEnable;
                 lblProgressLable.Visible = AEnable;
             }));
         }

         private void CompletionStatusInPercent(object sender, PercentCompleteEventArgs args)
         {
             CompletionStatus((int)args.Percent);
         }

         private void CompleteBackup(object sender, ServerMessageEventArgs args)
         {
             EnableOrDisableProgressBar(false);
         }

         private void CopyFile(string zSourceFile, string zDestinationFile)
         {
             try
             {

                 byte[] m_aBuffer = new byte[1024];
                 Int32 m_iBytes = default(Int32);
                 Int32 m_ProgressValue = 0;
                 long m_FileLenth = 0;
                 if(!m_EndThread)
                    EnableOrDisableProgressBar(true);
                 else
                    EnableOrDisableProgressBar(false);
                  
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
                 MessageBox.Show("System Error. Contact System Administrator", "Database Backup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
             }


         }

    }
}
