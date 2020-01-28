
using System;
using System.Windows.Forms;

using ISMDAL.TableColumnName;
using ISM.Modules;
using ISM.Class;
using System.Data;
using System.Net.NetworkInformation;
using System.Text;
using System.Runtime.InteropServices;
using AMMO_BG_DLL.Background;
using System.Configuration;
using SHSHQ.Modules;
using SHSHQ.Forms;
using System.Diagnostics;
namespace SHSHQ
{

  public partial class FrmMain : DevExpress.XtraEditors.XtraForm
  {

       
      public const int WM_NCLBUTTONDOWN = 0xA1;
      public const int HT_CAPTION = 0x2;

      [DllImportAttribute("user32.dll")]
      public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
      [DllImportAttribute("user32.dll")]
      public static extern bool ReleaseCapture();


      private void FrmMain_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
           
           
           
           
           
           
      }


    private ISMLoginInfo m_ISMLoginInfo;
    private bool m_AutoLoggedOut;   
    LocationTree m_LocationTree = null;   
    TreeListViewState m_TreeListViewState = null;  
    UpdateCatalogue m_UpdateCatalogue; 
    private string m_DBServerName = "";
    Logs applicationLogs = new Logs();

    private const int CP_NOCLOSE_BUTTON = 0x200;
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams myCp = base.CreateParams;
            myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
            return myCp;
        }
    } 
    public FrmMain(ISMLoginInfo AISMLoginInfo)
    {
      InitializeComponent();
      DisableMenuAccess();  
      m_ISMLoginInfo = AISMLoginInfo;
      m_AutoLoggedOut = false;

    }

    private void FrmMain_Load(object sender, EventArgs e)
    {
        foreach (var process in Process.GetProcessesByName("HQ User Sync Process"))
        {
            process.Kill();
        }
        Process.Start(string.Concat(Application.StartupPath, @"\HQ User Sync Process.exe"));


      LookAndFeel.SetStyle(DevExpress.LookAndFeel.LookAndFeelStyle.Style3D, true, false);
       
      LookAndFeel.SetSkinStyle(m_ISMLoginInfo.Params.AppSkinColour);  
      AdjustMenuAccessToProfile();  

       
       

      string[] zVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');  
       
      lblVersionNo.Text = String.Format("Version {0}.{1}.{2}", zVersion[0], zVersion[1], zVersion[2]);  

       

      sbUserName.Caption = m_ISMLoginInfo.LogonID;  
      sbAssignedProfile.Caption = m_ISMLoginInfo.UserProfileCode;  

       
       
      sbLastLoggedIn.Caption = m_ISMLoginInfo.UserLoggedOnAt.ToString(m_ISMLoginInfo.Params.DateTimeFormat);  

      tmrUpdateCurTime_Tick(sender, e);  
      tmrUpdateCurTime.Enabled = true;

       

       
       
      tmrApplicationIdle.IdleTime = TimeSpan.FromSeconds(m_ISMLoginInfo.Params.TimeOut);   
      tmrApplicationIdle.IdleAsync += new EventHandler(ApplicationIdle_IdleAsync);

       
      if (!tmrApplicationIdle.IsRunning)
        tmrApplicationIdle.Start();

      barStatusInfo.Caption = "Ready";  

      btnHomePage.PerformClick();  
      
       
      sbOpenAlarm.Caption = "";
      sbOpenException.Caption = "";
      GetDatabaseServerName();
       
      tmrUpdateAlarmExp.Enabled = true;
       
      menuGrpSystem.Expanded = true;  
      menuGrpAdmin.Expanded = false;
      menuGroupCraneStations.Expanded = false;
      menuGrpPortal.Expanded = false;
      menuGrpStockControl.Expanded = false;
      menuGrpUsers.Expanded = false;
      menuGrpMonitor.Expanded = false;
      menuGrpReports.Expanded = false;

      //navUserDetails_LinkClicked(this, null);
    }

    private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (m_AutoLoggedOut == false)   
        {
            m_ISMLoginInfo.AddToJournal("T", "PC-Logout Successfully", "SEC106");  
            m_ISMLoginInfo.ISMServer.AddLoginTask("3", 30, m_ISMLoginInfo.UserID);  
       
        }
    }

    private void AdjustMenuAccessToProfile()
    {
        try
        {

            DataSet ds = m_ISMLoginInfo.ISMServer.GetFunctionCode(m_ISMLoginInfo.UserProfileCode);
            if (ds != null)
            {
                int zFunCode = 0;
                foreach (DataRow zRow in ds.Tables[0].Rows)
                {
                    zFunCode = int.Parse(zRow["FUNCTION_CODE"].ToString());

                    switch (zFunCode)
                    {
                        case 1001:
                            menuGrpSystem.Visible = true;
                            break;
                        case 1002:
                            subMenuApplicationExit.Visible = true;
                            break;
                        case 1003:
                            btnHomePage.Visible = true;
                            break;
                        case 1004:
                             
                            break;
                        case 1005:
                            subMenuAbout.Visible = true;
                            break;
                        case 1100:
                            menuGrpAdmin.Visible = false;
                            menuGroupCraneStations.Visible = true;
                            break;
                        case 1101:
                            subMenuAdminNewLoc.Visible = true;
                            break;
                        case 1102:
                            subMenuAdminUpdateLoc.Visible = true;
                            break;
                        case 1103:
                            subMenuRemoveLocation.Visible = true;
                            break;
                        case 1104:
                            menuGrpUsers.Visible = true;
                            //subMenuAdminUserCreate.Visible = true;
                            navUserDetails.Visible = true;
                            
                            break;
                        case 1105:
                            //subMenuAdminUserEdit.Visible = true;
                            break;
                        case 1106:
                            //subMenuAdminUserDeactivate.Visible = true;
                            break;
                        case 1200:
                            menuGrpPortal.Visible = false;
                            break;
                        case 1201:
                            subMenuCreatePortal.Visible = true;
                            break;
                        case 1202:
                            subMenuUpdatePortal.Visible = true;
                            break;
                        case 1203:
                            subMenuRemovePortal.Visible = true;
                            break;
                        case 1204:
                             
                            break;
                        case 1205:
                            subMenuSyncAllUser.Visible = true;
                            //subMenuPersonnelSyncHistory.Visible = true;
                            subMenuMonitorException.Visible = true;
                            break;
                        case 1300:
                             
                            break;
                        case 1301:
                             
                            break;
                        case 1302:
                             
                            break;
                        case 1303:
                             
                            break;
                        case 1304:
                             
                            break;
                        case 1305:
                             
                            break;
                        case 1306:
                             
                            break;
                        case 1307:
                             
                            break;
                        case 1308:
                             
                            break;
                        case 1400:
                             
                            break;
                        case 1401:
                             
                            break;
                        case 1402:
                             
                            break;
                        case 1500:
                            menuGrpMonitor.Visible = true;
                            break;
                        case 1501:
                             
                            break;
                        case 1502:
                             
                            break;
                        case 1503:
                             
                            break;
                        case 1504:
                             
                            break;
                        case 1505:
                            subMenuMonitorException.Visible = true;
                            break;
                        case 1506:
                             
                            break;
                        case 1600:
                             
                            break;
                        case 1601:
                             
                            break;
                        case 1602:
                             
                            break;
                        case 1603:
                             
                            break;
                        case 1604:
                             
                            break;
                        case 1605:
                             
                            break;
                        case 1606:
                             
                            break;
                        case 1607:
                             
                            break;
                        case 1608:
                             
                            break;
                        case 1609:
                             
                            break;
                        case 1700:
                             
                            break;
                        case 1701:
                             
                            break;
                        case 1702:
                             
                            break;
                        case 1703:
                             
                            break;
                    }
                }
            }
             

             
             

             
             
             
             
             
             
             
             
             

             
             
             
             
             
             

             
             
             
             
             
             
             
             
             
             
             
             
             
             

             
             
             
             

             
             
             
             
             
             
             
             
             
             
             
             
             
             

             
             

             
             

             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "ISM Main", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }

    private void navBarItemAdminNewLoc_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
      if (pnlWorkSpace.Controls.Count > 0)
        pnlWorkSpace.Controls.RemoveAt(0);

      LocationAdmin zCreateNewLoc = new LocationAdmin(m_ISMLoginInfo, ISMBaseWorkSpace.EnumFrmMode.CREATE);

      zCreateNewLoc.lblHeader.Text = "Add Crane";
      zCreateNewLoc.Parent = this;
      pnlWorkSpace.Controls.Add(zCreateNewLoc);
    }

    private void navBarItemAdminEditLoc_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
      if (pnlWorkSpace.Controls.Count > 0)
        pnlWorkSpace.Controls.RemoveAt(0);

      LocationAdmin zCreateNewLoc = new LocationAdmin(m_ISMLoginInfo, ISMBaseWorkSpace.EnumFrmMode.EDIT);
      zCreateNewLoc.lblHeader.Text = "Update Crane";
      zCreateNewLoc.Parent = this;
      pnlWorkSpace.Controls.Add(zCreateNewLoc);
    }

    private void navBarItemRemoveLocation_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
      if (pnlWorkSpace.Controls.Count > 0)
        pnlWorkSpace.Controls.RemoveAt(0);
      LocationAdmin zRemoveLoaction = new LocationAdmin(m_ISMLoginInfo, ISMBaseWorkSpace.EnumFrmMode.REMOVE);
      zRemoveLoaction.lblHeader.Text = "Remove Crane";
      zRemoveLoaction.Parent = this;
      pnlWorkSpace.Controls.Add(zRemoveLoaction);
    }

    private void navBarItemAdminNewOperator_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
      if (pnlWorkSpace.Controls.Count > 0)
        pnlWorkSpace.Controls.RemoveAt(0);

      UserCreate zOperator = new UserCreate(m_ISMLoginInfo);   
      zOperator.Parent = this;
      pnlWorkSpace.Controls.Add(zOperator);
    }

    private void navBarItemAdminEditOperator_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
      if (pnlWorkSpace.Controls.Count > 0)
        pnlWorkSpace.Controls.RemoveAt(0);

      UserUpdate zOperator = new UserUpdate(m_ISMLoginInfo);  
      pnlWorkSpace.Controls.Add(zOperator);
    }

    private void navBarItemAdminRemoveOperator_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
      if (pnlWorkSpace.Controls.Count > 0)
        pnlWorkSpace.Controls.RemoveAt(0);
      UserDeactivate zRemoveOperator = new UserDeactivate(m_ISMLoginInfo);
      pnlWorkSpace.Controls.Add(zRemoveOperator);
    }

    private void navBarItemStockReceiveCreate_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
      if (pnlWorkSpace.Controls.Count > 0)
        pnlWorkSpace.Controls.RemoveAt(0);
      StockReceiveCreateTask zStockCreateReceiveTask = new StockReceiveCreateTask(m_ISMLoginInfo);
      pnlWorkSpace.Controls.Add(zStockCreateReceiveTask);
    }

    private void navBarItemCreateStockTake_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
      if (pnlWorkSpace.Controls.Count > 0)
        pnlWorkSpace.Controls.RemoveAt(0);
      StockTakeCreateTask zStockTakeCreateTask = new StockTakeCreateTask(m_ISMLoginInfo);
      pnlWorkSpace.Controls.Add(zStockTakeCreateTask);
    }

    private void navBarItemStockMoveCreate_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
       
      if (pnlWorkSpace.Controls.Count > 0)
        pnlWorkSpace.Controls.RemoveAt(0);
      StockMoveCreateTask zStockMoveCreateTask = new StockMoveCreateTask(m_ISMLoginInfo);
      zStockMoveCreateTask.lblHeader.Text = " 4000 - Move Item";  
      zStockMoveCreateTask.StockMoveType = 0;  
      pnlWorkSpace.Controls.Add(zStockMoveCreateTask);
       
    }
     
    private void navBarItemLocationMove_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
      if (pnlWorkSpace.Controls.Count > 0)
        pnlWorkSpace.Controls.RemoveAt(0);
      StockMoveCreateTask zStockMoveCreateTask = new StockMoveCreateTask(m_ISMLoginInfo);
      zStockMoveCreateTask.lblHeader.Text = " 4000 - Move Location"; 
      zStockMoveCreateTask.StockMoveType = 1;  
      pnlWorkSpace.Controls.Add(zStockMoveCreateTask);
    }
     
    private void navBarItemCreateIssueStock_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
      if (pnlWorkSpace.Controls.Count > 0)
        pnlWorkSpace.Controls.RemoveAt(0);
      StockIssueCreateTask zStockIssueCreateTask = new StockIssueCreateTask(m_ISMLoginInfo);
      zStockIssueCreateTask.Parent = this;
      pnlWorkSpace.Controls.Add(zStockIssueCreateTask);
    }

    private void navBarItemTraceItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
      if (pnlWorkSpace.Controls.Count > 0)
        pnlWorkSpace.Controls.RemoveAt(0);
      TraceCreateTask zTrace = new TraceCreateTask(m_ISMLoginInfo);
       
       
      zTrace.lblHeader.Text = " 6000 - Trace Item"; 
      zTrace.TraceType = 0;  
       
      pnlWorkSpace.Controls.Add(zTrace);
    }

     
    private void subMenuTraceLocation_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
      if (pnlWorkSpace.Controls.Count > 0)
        pnlWorkSpace.Controls.RemoveAt(0);
      TraceCreateTask zTrace = new TraceCreateTask(m_ISMLoginInfo);
      
      zTrace.lblHeader.Text = " 6000 - Trace Location";  
      zTrace.TraceType = 1;  
      pnlWorkSpace.Controls.Add(zTrace);

    }
     
     
     
     
     
     
     
     
     
     
    private void navBarIteTaskMonitor_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
      if (pnlWorkSpace.Controls.Count > 0)
        pnlWorkSpace.Controls.RemoveAt(0);
      MonitorTask zTaskMonitor = new MonitorTask(m_ISMLoginInfo);
      pnlWorkSpace.Controls.Add(zTaskMonitor);
    }

    private void navBarItemOperatorMonitor_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
      if (pnlWorkSpace.Controls.Count > 0)
        pnlWorkSpace.Controls.RemoveAt(0);
      MonitorUser zOperatorMonitor = new MonitorUser(m_ISMLoginInfo);
      pnlWorkSpace.Controls.Add(zOperatorMonitor);
    }

    private void navBarItemAlarmsMonitor_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
       
       
       
       
       
       
        if (pnlWorkSpace.Controls.Count > 0)
            pnlWorkSpace.Controls.RemoveAt(0);
        AlarmExpManagement zAlarmExpManagement = new AlarmExpManagement(m_ISMLoginInfo);
        pnlWorkSpace.Controls.Add(zAlarmExpManagement);

    }

    private void navBarItemDeviceMonitor_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
      if (pnlWorkSpace.Controls.Count > 0)
        pnlWorkSpace.Controls.RemoveAt(0);
      MonitorDevice zDeviceMonitor = new MonitorDevice(m_ISMLoginInfo);
      pnlWorkSpace.Controls.Add(zDeviceMonitor);
    }

    private void navBarItemInterfaceMonitor_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
      if (pnlWorkSpace.Controls.Count > 0)
        pnlWorkSpace.Controls.RemoveAt(0);
      MonitorInterface zInterfaceMonitor = new MonitorInterface(m_ISMLoginInfo);
      pnlWorkSpace.Controls.Add(zInterfaceMonitor);
    }

    private void navBarItemExceptionMonitor_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
        //if (pnlWorkSpace.Controls.Count > 0)
        //    pnlWorkSpace.Controls.RemoveAt(0);
        //MonitorException zExceptionMonitor = new MonitorException(m_ISMLoginInfo);
        //pnlWorkSpace.Controls.Add(zExceptionMonitor);

        DataSet ds = m_ISMLoginInfo.ISMServer.ISMUserGetRecds();
        if (pnlWorkSpace.Controls.Count > 0)
            pnlWorkSpace.Controls.RemoveAt(0);
        StationLogs zExceptionMonitor = new StationLogs(pnlWorkSpace.Height, pnlWorkSpace.Width, ds);
        pnlWorkSpace.Controls.Add(zExceptionMonitor);
    }

    private void navBarItemExit_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
       
       
      if (MessageBox.Show("Are you sure you want to exit?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)  
      {
         
        tmrApplicationIdle.IdleAsync -= new EventHandler(ApplicationIdle_IdleAsync);  
        tmrApplicationIdle.Dispose();  
        Application.Exit();
      }
    }

    private void tmrUpdateCurTime_Tick(object sender, EventArgs e)
    {
       
       
       
      sbCurrentTime.Caption = DateTime.Now.ToString(m_ISMLoginInfo.Params.DateTimeFormat);  
    }

    void ApplicationIdle_IdleAsync(object sender, EventArgs e)
    {
      BeginInvoke(new MethodInvoker(delegate() { ApplicationIdle_Idle(sender, e); }));
    }

    void ApplicationIdle_Idle(object sender, EventArgs e)
    {
      tmrUpdateAlarmExp.Tick -= new System.EventHandler(this.tmrUpdateAlarmExp_Tick);  
      this.Hide();    
      m_AutoLoggedOut = true;  
       
       
       
       
       
       
      if (m_ISMLoginInfo.m_RecLockedForEdit)
      {
          int zMode = 5;  
          string zComments = "";
          DataSet ds = m_ISMLoginInfo.ISMServer.GetAlarmExpceptionData(zMode, zComments, m_ISMLoginInfo.m_JnlID);
          m_ISMLoginInfo.m_RecLockedForEdit = false;
      }
       
       
       
       
      m_ISMLoginInfo.AddToJournal("T", String.Format("PC User has been auto logged out of the application", m_ISMLoginInfo.LogonID), "SEC116");  
      m_ISMLoginInfo.ISMServer.AddLoginTask("3", 30, m_ISMLoginInfo.UserID);  
      m_ISMLoginInfo.ISMServer = null;  
      MessageBox.Show(this, "You have been automatically logged out.", /* DM Out 13-JUL-10 "Application Idle", */ this.Text, /* DM in 13-JUL-10 */ MessageBoxButtons.OK, MessageBoxIcon.Information);
      tmrApplicationIdle.IdleAsync -= new EventHandler(ApplicationIdle_IdleAsync);  
      tmrApplicationIdle.Dispose();  
       
      this.Close();   
    }

    private void btnHomePage_Click(object sender, EventArgs e)
    {
      //if (pnlWorkSpace.Controls.Count > 0)
      //  pnlWorkSpace.Controls.RemoveAt(0);
      //m_LocationTree = new LocationTree(m_ISMLoginInfo);
      //m_TreeListViewState = new TreeListViewState(m_LocationTree.tvLocation);  

      //m_TreeListViewState.PropISMLoginInfo = m_ISMLoginInfo;
      //m_LocationTree.TreeListViewState = m_TreeListViewState;  
      //pnlWorkSpace.Controls.Add(m_LocationTree);
        if (pnlWorkSpace.Controls.Count > 0)
            pnlWorkSpace.Controls.RemoveAt(0);

        ViewLive zLocationPortal = new ViewLive(pnlWorkSpace.Height,pnlWorkSpace.Width);
        zLocationPortal.Parent = this;
        pnlWorkSpace.Controls.Add(zLocationPortal);

    }

     
    private void btnLocRefresh_Click(object sender, EventArgs e)
    {
        if (pnlWorkSpace.Controls.Count > 0)
            pnlWorkSpace.Controls.RemoveAt(0);

        m_LocationTree.RefreshTreeControl = false;
        try
        {
            m_LocationTree.UpdateGridControl2_CheckedInWorkers_RESET();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
         
         
         
        m_LocationTree.Visible = true;
        m_LocationTree.Enabled = true;
         
        pnlWorkSpace.Controls.Add(m_LocationTree);
    }

     
    private void subMenuAbout_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
       
        new ISM.Forms.FrmAboutSHS().ShowDialog();  
    }

    private void subMenuCreatePortal_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
      if (pnlWorkSpace.Controls.Count > 0)
        pnlWorkSpace.Controls.RemoveAt(0);

      LocationPortal zLocationPortal = new LocationPortal(m_ISMLoginInfo, ISMBaseWorkSpace.EnumFrmMode.CREATE);
      zLocationPortal.Parent = this;
      zLocationPortal.lblHeader.Text = " Add Pinning Station";
      pnlWorkSpace.Controls.Add(zLocationPortal);
    }

    private void subMenuUpdatePortal_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
      if (pnlWorkSpace.Controls.Count > 0)
        pnlWorkSpace.Controls.RemoveAt(0);

      LocationPortal zLocationPortal = new LocationPortal(m_ISMLoginInfo, ISMBaseWorkSpace.EnumFrmMode.EDIT);
      zLocationPortal.Parent = this;
      zLocationPortal.lblHeader.Text = " Update Pinning Station";
      pnlWorkSpace.Controls.Add(zLocationPortal);
    }
     
    private void subMenuRemovePortal_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
        if (pnlWorkSpace.Controls.Count > 0)
            pnlWorkSpace.Controls.RemoveAt(0);

        LocationPortal zLocationPortal = new LocationPortal(m_ISMLoginInfo, ISMBaseWorkSpace.EnumFrmMode.REMOVE);
        zLocationPortal.Parent = this;
        zLocationPortal.lblHeader.Text = " Remove Pinning Station";
        pnlWorkSpace.Controls.Add(zLocationPortal);
    }
     
     
    private void subMenuStockLocSearch_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
        if (pnlWorkSpace.Controls.Count > 0)
            pnlWorkSpace.Controls.RemoveAt(0);
        LocationSearch zLocationSearch = new LocationSearch(m_ISMLoginInfo);
        pnlWorkSpace.Controls.Add(zLocationSearch);
    }
    private void subMenuPortal_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
        if (pnlWorkSpace.Controls.Count > 0)
            pnlWorkSpace.Controls.RemoveAt(0);
        MonitorPortal zMonitorPortal = new MonitorPortal(m_ISMLoginInfo);
        pnlWorkSpace.Controls.Add(zMonitorPortal);

    }
     
     
    private void subMenuRptStockSearch_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
        if (pnlWorkSpace.Controls.Count > 0)
            pnlWorkSpace.Controls.RemoveAt(0);
         
        RptFrmStock zRptFrmStock = new RptFrmStock(m_ISMLoginInfo);  
        pnlWorkSpace.Controls.Add(zRptFrmStock);
    }

    private void subMenuRptStockTake_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
        if (pnlWorkSpace.Controls.Count > 0)
            pnlWorkSpace.Controls.RemoveAt(0);
        RptFrmStockTake zRptFrmStockTake = new RptFrmStockTake(m_ISMLoginInfo);
        pnlWorkSpace.Controls.Add(zRptFrmStockTake);
    }

    private void subMenuRptUserActivity_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
        if (pnlWorkSpace.Controls.Count > 0)
            pnlWorkSpace.Controls.RemoveAt(0);
        RptFrmUserActivity zRptFrmUserActivity = new RptFrmUserActivity(m_ISMLoginInfo);
        pnlWorkSpace.Controls.Add(zRptFrmUserActivity);

    }

    private void subMenuRptaRfidActivity_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
        if (pnlWorkSpace.Controls.Count > 0)
            pnlWorkSpace.Controls.RemoveAt(0);
        RptFrmActiveReaderActy zRptFrmActiveReaderActy = new RptFrmActiveReaderActy(m_ISMLoginInfo);
        pnlWorkSpace.Controls.Add(zRptFrmActiveReaderActy);
    }

    private void subMenuRptStorageSealStatus_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
        if (pnlWorkSpace.Controls.Count > 0)
            pnlWorkSpace.Controls.RemoveAt(0);
        RptFrmSealStatus zRptFrmSealStatus = new RptFrmSealStatus(m_ISMLoginInfo);
        pnlWorkSpace.Controls.Add(zRptFrmSealStatus);

    }

    private void subMenuRptAlarms_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
        if (pnlWorkSpace.Controls.Count > 0)
            pnlWorkSpace.Controls.RemoveAt(0);
        RptFrmAlarmStatus zRptFrmAlarmStatus = new RptFrmAlarmStatus(m_ISMLoginInfo);
        zRptFrmAlarmStatus.m_ReportType = 0;  
        zRptFrmAlarmStatus.lblHeader.Text = " 8000 - Alarms Status Report";  
        pnlWorkSpace.Controls.Add(zRptFrmAlarmStatus);

    }
     
     
    private void subMenuUpdateCatalogue_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
        if (pnlWorkSpace.Controls.Count > 0)
            pnlWorkSpace.Controls.RemoveAt(0);

         
        if (m_UpdateCatalogue == null)
        {
          UpdateCatalogue zUpdateCatalogue = new UpdateCatalogue(m_ISMLoginInfo);
          m_UpdateCatalogue = zUpdateCatalogue;
          pnlWorkSpace.Controls.Add(m_UpdateCatalogue);
        }
        else
        {
           

           
           
          if (m_UpdateCatalogue.m_UpdateInProgress == true && m_UpdateCatalogue.m_FirstTimeView == false)
          {
            pnlWorkSpace.Controls.Add(m_UpdateCatalogue);
          }
          else
          {
            m_UpdateCatalogue = null;
            UpdateCatalogue zUpdateCatalogue = new UpdateCatalogue(m_ISMLoginInfo);
            m_UpdateCatalogue = zUpdateCatalogue;
            pnlWorkSpace.Controls.Add(zUpdateCatalogue);
          }
        }
         

         
         
         

    }

    private void subMenuMILISReport_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
        if (pnlWorkSpace.Controls.Count > 0)
            pnlWorkSpace.Controls.RemoveAt(0);

        RptMILISStockTake zRptMILISStockTake = new RptMILISStockTake(m_ISMLoginInfo);
        pnlWorkSpace.Controls.Add(zRptMILISStockTake);

    }
     
     
    private void subMenuTaskManagement_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
        if (pnlWorkSpace.Controls.Count > 0)
            pnlWorkSpace.Controls.RemoveAt(0);
        TaskMgnt zTaskMgnt = new TaskMgnt(m_ISMLoginInfo);
        pnlWorkSpace.Controls.Add(zTaskMgnt);

    }
    
      private void subMenuDbBackup_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
    {
        if (pnlWorkSpace.Controls.Count > 0)
            pnlWorkSpace.Controls.RemoveAt(0);

        ISMDatabaseBackup zISMDatabaseBackup = new ISMDatabaseBackup(m_ISMLoginInfo);
        pnlWorkSpace.Controls.Add(zISMDatabaseBackup);

    }
     
       
      private void subMenuRptStockLocation_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
      {
          if (pnlWorkSpace.Controls.Count > 0)
              pnlWorkSpace.Controls.RemoveAt(0);

          RptFrmStockHierarchical zRptFrmStockHierarchical = new RptFrmStockHierarchical(m_ISMLoginInfo);
          zRptFrmStockHierarchical.m_ReportType = 0;  
          pnlWorkSpace.Controls.Add(zRptFrmStockHierarchical);

      }

      private void subMenuRptStockItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
      {
          if (pnlWorkSpace.Controls.Count > 0)
              pnlWorkSpace.Controls.RemoveAt(0);

          RptFrmStockHierarchical zRptFrmStockHierarchical = new RptFrmStockHierarchical(m_ISMLoginInfo);
          zRptFrmStockHierarchical.m_ReportType = 1;  
          pnlWorkSpace.Controls.Add(zRptFrmStockHierarchical);
      }

      private void subMenuRptIssues_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
      {
          if (pnlWorkSpace.Controls.Count > 0)
              pnlWorkSpace.Controls.RemoveAt(0);
          RptFrmIssue zRptFrmIssue = new RptFrmIssue(m_ISMLoginInfo);
          pnlWorkSpace.Controls.Add(zRptFrmIssue);

      }
       
       
      private void subMenuExpceptionMgt_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
      {
          if (pnlWorkSpace.Controls.Count > 0)
              pnlWorkSpace.Controls.RemoveAt(0);
          AlarmExpManagement zAlarmExpManagement = new AlarmExpManagement(m_ISMLoginInfo);
          pnlWorkSpace.Controls.Add(zAlarmExpManagement);
      }
      private void subMenuRptException_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
      {
          if (pnlWorkSpace.Controls.Count > 0)
              pnlWorkSpace.Controls.RemoveAt(0);
          RptFrmAlarmStatus zRptFrmAlarmStatus = new RptFrmAlarmStatus(m_ISMLoginInfo);
          zRptFrmAlarmStatus.m_ReportType = 1;  
          zRptFrmAlarmStatus.lblHeader.Text = " 8000 - Exceptions Status Report";
          pnlWorkSpace.Controls.Add(zRptFrmAlarmStatus);

      }

      private void tmrUpdateAlarmExp_Tick(object sender, EventArgs e)
      {
        try
        {
            tmrUpdateAlarmExp.Tick -= new System.EventHandler(this.tmrUpdateAlarmExp_Tick);
            GetOpenAlarmExceptionData();
            tmrUpdateAlarmExp.Tick += new System.EventHandler(this.tmrUpdateAlarmExp_Tick);
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), " Main", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

      }

      void GetOpenAlarmExceptionData()
      {
          try
          {
              if (DBServerCanBePinged())
              {
                  int zAlarmCount = 0;
                  int zExpceptionCount = 0;

                  m_ISMLoginInfo.ISMServer.GetAlarmExceptionRecordCount(ref zAlarmCount, ref zExpceptionCount);
                  sbOpenAlarm.Caption = zAlarmCount.ToString();
                  sbOpenException.Caption = zExpceptionCount.ToString();
              }
          }
          catch (Exception ex)
          {
              MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), " Main", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
      }
      private void GetDatabaseServerName()
      {
          try
          {
              string zConString = SHSHQ.Properties.Settings.Default.ConnectionString;
              string[] zdata = zConString.Split('=');
              string[] zDataSoruce = { "" };
              string[] zDatabase = { "" };

              if (zdata.Length == 6)  
               
              {
                  zDataSoruce = zdata[1].Split(';');
                  zDatabase = zdata[2].Split(';');
                  m_DBServerName = zDataSoruce[0];
                   
              }
              else
              {
                  MessageBox.Show("Database Connection String is not in Order. Please Contact System Administrator", " Main", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  return;
              }

          }
          catch (Exception ex)
          {
              MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), " Main", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }

      }
      public bool DBServerCanBePinged()
      {
          bool zNPortReplied = false;  
          Ping zPingNPort = new Ping();
          PingOptions zOptions = new PingOptions();
          zOptions.DontFragment = true;
           
          string zData = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
          byte[] zBuffer = Encoding.ASCII.GetBytes(zData);
          int zTimeOut = 120;
          try
          {
              if (m_DBServerName != "")
              {
                  PingReply zReply = zPingNPort.Send(m_DBServerName, zTimeOut, zBuffer, zOptions);
                  if (zReply.Status == IPStatus.Success)
                      zNPortReplied = true;
              }
          }
          catch (Exception ex)
          {
              ex.ToString();
               
          }
          return zNPortReplied;
      }
       
       
      private void DisableMenuAccess()
      {
          try
          {
              menuGrpAdmin.Visible = false;
              menuGroupCraneStations.Visible = false;
              subMenuAdminNewLoc.Visible = false;
              subMenuAdminUpdateLoc.Visible = false;
              subMenuRemoveLocation.Visible = false;
              subMenuAdminUserCreate.Visible = false;
              subMenuAdminUserEdit.Visible = false;
              subMenuAdminUserDeactivate.Visible = false;
              subMenuSyncAllUser.Visible = false;
              subMenuPersonnelSyncHistory.Visible = false;
              navUserDetails.Visible = false;

              menuGrpPortal.Visible = false;
              subMenuCreatePortal.Visible = false;
              subMenuUpdatePortal.Visible = false;
              subMenuRemovePortal.Visible = false;
              subMenuAdminService.Visible = false;

              menuGrpStockControl.Visible = false;
               
               
               
               
              subMentStockLocationMove.Visible = false;
               
              subMenuTaskManagement.Visible = false;
               

              menuGrpUsers.Visible = false; 
               
               

              menuGrpMonitor.Visible = false;
              subMenuMonitorTask.Visible = false;
              subMenuMonitorOperator.Visible = false;
              subMenuMonitorAlarms.Visible = false;
              subMenuMonitorDevice.Visible = false;
              subMenuMonitorException.Visible = false;
              subMenuPortal.Visible = false;

              menuGrpReports.Visible = false;
              subMenuRptStockSearch.Visible = false;
              subMenuRptStockLocation.Visible = false;
              subMenuRptStockItem.Visible = false;
              subMenuRptStockTake.Visible = false;
              subMenuRptUserActivity.Visible = false;
              subMenuRptStorageSealStatus.Visible = false;
              subMenuRptIssues.Visible = false;
              subMenuRptAlarms.Visible = false;
              subMenuRptException.Visible = false;

               
              subMenuUpdateCatalogue.Visible = false;
              subMenuMILISReport.Visible = false;
              subMenuDbBackup.Visible = false;

              menuGrpSystem.Visible = false;
              subMenuAbout.Visible = false;
              subMenuApplicationExit.Visible = false;
          }
          catch (Exception ex)
          {
              MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), " Main", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }

      }

      private void subMenuAdminService_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
      {
          if (pnlWorkSpace.Controls.Count > 0)
              pnlWorkSpace.Controls.RemoveAt(0);

          ISMServiceMonitor zISMServiceMonitor = new ISMServiceMonitor(m_ISMLoginInfo);
          pnlWorkSpace.Controls.Add(zISMServiceMonitor);

      }

       
       
      private void subMenuManageStock_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
      {
          if (pnlWorkSpace.Controls.Count > 0)
              pnlWorkSpace.Controls.RemoveAt(0);
          StockManage zStockManage = new StockManage(m_ISMLoginInfo);
          pnlWorkSpace.Controls.Add(zStockManage);

      }

      private void pnlWorkSpace_ControlRemoved(object sender, ControlEventArgs e)
      {
          e.Control.Controls.Owner.Visible = false;
          e.Control.Controls.Owner.Enabled = false;
      }

      private void splitContainerControl_Paint(object sender, PaintEventArgs e)
      {

      }

       
      private void subMenuUpdateHumpyRelationship_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
      {
          if (pnlWorkSpace.Controls.Count > 0)
              pnlWorkSpace.Controls.RemoveAt(0);


          LocationPortalUpdate zLocationPortalUpdate = new LocationPortalUpdate(m_ISMLoginInfo, ISMBaseWorkSpace.EnumFrmMode.EDIT);
          zLocationPortalUpdate.Parent = this;
          zLocationPortalUpdate.lblHeader.Text = " Update Pinning Station Relationship";
          pnlWorkSpace.Controls.Add(zLocationPortalUpdate);
      }



      private void subMenuSyncAllUser_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
      {

          frmSecurity sec = new frmSecurity();

          sec.ShowDialog();
          if (sec.DialogResult != DialogResult.OK)
              return;

          if (MessageBox.Show("Sync all users?","Confirm Sync",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
          {
              MessageBox.Show("Sync all users cancelled.", "Sync Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
              return;
          }

          HQDataExtract syncAllData = new HQDataExtract();
          syncAllData.PerformUserSync(ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString());
          applicationLogs.InsertApplicationLogs(
                  ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                  "User",
                   m_ISMLoginInfo.ISMServer.CurrentLoggedInUser.ToString(),
                  "Success",
                  "Synced",
                  "",
                  "HQ",
                  "");
          MessageBox.Show("User sync done.", "Sync Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

      }

      private void subMenuEditStation_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
      {
          frmSecurity sec = new frmSecurity();

          sec.ShowDialog();
          if (sec.DialogResult != DialogResult.OK)
              return;

          
          if (pnlWorkSpace.Controls.Count > 0)
              pnlWorkSpace.Controls.RemoveAt(0);

          PinningStation managePinningStation = new PinningStation(pnlWorkSpace.Height, pnlWorkSpace.Width, m_ISMLoginInfo.ISMServer.CurrentLoggedInUser.ToString());


          managePinningStation.Parent = this;
          managePinningStation.lblHeader.Text = "Pinning Station Management";
          pnlWorkSpace.Controls.Add(managePinningStation);
      }

      private void subMenuSyncStation_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
      {
          frmSecurity sec = new frmSecurity();

          sec.ShowDialog();
          if (sec.DialogResult != DialogResult.OK)
              return;

          PinningStationManagement processPinningStation = new PinningStationManagement();

          processPinningStation.UpdatePinningStationDataSource(ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString());

          applicationLogs.InsertApplicationLogs(
                            ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                            "Station",
                             m_ISMLoginInfo.ISMServer.CurrentLoggedInUser.ToString(),
                            "Success",
                            "Synced",
                            "",
                            "HQ",
                            ""); 
      }

      private void subMenuPersonnelSyncHistory_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
      {
          if (pnlWorkSpace.Controls.Count > 0)
              pnlWorkSpace.Controls.RemoveAt(0);

          UserSyncHistory syncHistory = new UserSyncHistory(pnlWorkSpace.Height,pnlWorkSpace.Width);


          syncHistory.Parent = this;
          syncHistory.lblHeader.Text = "User Sync History";
          pnlWorkSpace.Controls.Add(syncHistory);
      }

      private void navUserDetails_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
      {
          if (pnlWorkSpace.Controls.Count > 0)
              pnlWorkSpace.Controls.RemoveAt(0);

          UserDetails manageUser = new UserDetails(pnlWorkSpace.Width,pnlWorkSpace.Height);
          manageUser.Parent = this;
          pnlWorkSpace.Controls.Add(manageUser);
      }
       
       
       
       
       
       
       
       

  }
}
