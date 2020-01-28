using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Localization;

using System.Drawing.Drawing2D;

using System.Threading;
using System.Diagnostics;
using System.IO;
using AMMO_BG_DLL.Background;
using System.Configuration;
using System.IO.Ports;
using Impinj.OctaneSdk;

namespace SmartHumpyController
{

    public partial class FrmMain : Form
    {
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox_IconStatus;//IN JL 16-MAY-16

        //OUT JL 16-MAY-16 private SHS_DetailMode m_DetailMode = SHS_DetailMode.CheckedInList;
        private SHS_DetailMode m_DetailMode = SHS_DetailMode.DetailList;//IN JL 16-MAY-2016: Modified the detail list layout
        private System.Threading.Timer m_UpdateTagListTimer;
        private int m_RetrieveTimeInterval_ms = 0;

        private HFReaderSerial m_HFReader;
        private ReaderDirectController m_UHFReader;

        private HumpyDAL m_DAL_LOCAL;//IN JL 28-05-2016

        private HumpyDAL m_DAL;
        private HumpyDAL m_DAL_Retriever;
        private HumpyDAL m_DAL_CheckedInWorker;
        private HumpyDAL m_DAL_Journaler;

        private HumpyDAL m_DAL_DBLogger;

        private LogString m_Logger = LogString.GetLogString("HumpyController");
        private string m_HumpyId;

        private Thread m_FullStatusReportThread;
        private System.Threading.Timer m_Timer_FullStatusReport;
        private int m_FullStatusReportTimeInterval_ms = 0;

        private Thread m_CheckInOutThread;
        private SHS_CheckInOutStatus m_CurrentCheckInOutStatus = SHS_CheckInOutStatus.NA;

        private bool m_IgnoreNoneCheckedInWorker = false;
        private string m_CurrentHumpyOwner = "";

        private bool m_SystemFailure_ReadersWIFIDBConn = false;

        private Color m_Color_Green = Color.FromArgb(85, 180, 26);
        private Color m_Color_Red = Color.FromArgb(199, 32, 52);
        private Color m_Color_Yellow = Color.FromArgb(231, 219, 19);

        private System.Threading.Timer m_CheckGateConfigurationChanges;

        public GateDetail m_GD = new GateDetail();
        public HumpyDetail m_HumpyDetail = new HumpyDetail();
        ConnectionReminder conRemind;

        private System.Threading.Timer m_SoftwareHeartBeat;//HeartBeat Software
        private bool m_SoftwareHeartBeatOnOff = true;//HeartBeat 

        int windowTimer = 0;
        string masterHumpyConnectionString = "";
        string sharedFolderIP = "";
        bool uhfReaderConnected = true;
        bool hfReaderConnected = true;
        string hfComPort = "";
        bool localDBDisconnected = false;
        bool masterDBDisconnected = false;
        bool sharedFolderDisconnected = false;
        bool uhfReaderDisconnected = false;
        bool hfReaderDisconnected = false;

        int reconnectTimer = 0;
        int connectionMonitorTimer = 0;

        string uhfReaderIPAddress = "";
        string wifiIPAddress = "";
        string humpyLocationID = "";

        Logs insertApplicationLogs = new Logs();

        string sanIPAddress = "";
        string sanShareFolder = "";

        bool gpiTwoHasSignal = false;
        bool disableGpiMonitoring = false;
        HumpyMode humpyMode;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            timConnection.Enabled = false;
            DLLConstants createInstallDirectory = new DLLConstants();
            DLLConstants getProgramValue = new DLLConstants();
            createInstallDirectory.CreateInstallationFolder();
            

            HQDataExtract getConfigValues = new HQDataExtract();
            //MessageBox.Show("Test");
            //Initialize the software heart beat thread// in jl 28-MAY-2016
            m_SoftwareHeartBeat = new System.Threading.Timer(new System.Threading.TimerCallback(OnSoftwareHeartBeat), null,
                                                                 SmartHumpyController.Properties.Settings.Default.Sys_SoftwareHeartBeat_ms, System.Threading.Timeout.Infinite);

            //Initialize the gate configuration changes //IN JL 08-OCT-2013
            m_CheckGateConfigurationChanges = new System.Threading.Timer(new System.Threading.TimerCallback(OnCheckGateConfigurationChanges), null,
                                                                 System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            //Q: when do we start the difference checking?????



            //IN JL 28-MAY-2016: load the confgiratuion via the local db.
            m_DAL_LOCAL = new HumpyDAL(SmartHumpyController.Properties.Settings.Default.Sys_DBConnectionLocal);
            m_HumpyDetail = m_DAL_LOCAL.SHS_GetHumpySettings();

            if (m_HumpyDetail.Sys_DBConnection != "")
            {

                masterHumpyConnectionString = m_HumpyDetail.Sys_DBConnection;
                hfComPort = m_HumpyDetail.Rd_HFReader_COM;
                uhfReaderIPAddress = m_HumpyDetail.Rd_UHFReader_IP;
                sharedFolderIP = getConfigValues.GetConfigValue(ConfigurationManager.ConnectionStrings["SmartHumpyController.Properties.Settings.Sys_DBConnectionLocal"].ToString(), getProgramValue.dbConfigProgramValue_SANIPAddress);
                sanShareFolder = getConfigValues.GetConfigValue(ConfigurationManager.ConnectionStrings["SmartHumpyController.Properties.Settings.Sys_DBConnectionLocal"].ToString(), getProgramValue.dbConfigProgramValue_SANFolderName);
                wifiIPAddress = getConfigValues.GetConfigValue(ConfigurationManager.ConnectionStrings["SmartHumpyController.Properties.Settings.Sys_DBConnectionLocal"].ToString(), getProgramValue.dbConfigProgramValue_WirelessIPAddress);
                humpyLocationID = m_HumpyDetail.Sys_HumpyId;
                               
                System_Initialization_GUI();

                System_Initialization_Data();

                this.WindowState = FormWindowState.Maximized;
                insertApplicationLogs.InsertApplicationLogs(masterHumpyConnectionString,
                                                            insertApplicationLogs.AppLogs_ComponentHumpyValue,
                                                            "Software",
                                                            insertApplicationLogs.AppLogs_StatusNormalValue,
                                                            insertApplicationLogs.AppLogs_ActionInitializedValue,
                                                            "",
                                                            humpyLocationID,
                                                            wifiIPAddress);
            }
            else
            {
                GUI_StackLightStatus_Humpy(m_Color_Yellow);
                //2. Actual Stack Light GPIO Port.
                GPIOStackLight(LightStackColor.OrangeOnly);
                //
                UpdateStatusBar("Local DB Connection Failure");
            }

        
            if (m_CheckGateConfigurationChanges != null)
                m_CheckGateConfigurationChanges.Change(SmartHumpyController.Properties.Settings.Default.Sys_CheckHumpyConfigurationChanges_ms, System.Threading.Timeout.Infinite);

            timConnection.Enabled = true;
           
        }

        private void AdminMenuStatusLightChange_UHFReaderStatus(bool onoff)//IN JL 29-MAY-2016
        {
            try
            {
                if (onoff)
                    uHFReaderStatusToolStripMenuItem.BackColor = Color.Green;
                else
                    uHFReaderStatusToolStripMenuItem.BackColor = Color.Red;

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        private void AdminMenuStatusLightChange_HFReaderStatus(bool onoff)//IN JL 29-MAY-2016
        {
            try
            {
                if (onoff)
                    hFReaderStatusToolStripMenuItem.BackColor = Color.Green;
                else
                    hFReaderStatusToolStripMenuItem.BackColor = Color.Red;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        private void OnSoftwareHeartBeat(object AObject)//IN JL 28-MAY-2016
        {
            try
            {
                if (m_SoftwareHeartBeat != null)
                    m_SoftwareHeartBeat.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

                if (m_UHFReader != null)
                {
                    if (m_UHFReader.m_Reader != null)
                    {
                        if (m_UHFReader.m_ReaderIsConnected)
                        {
                            m_UHFReader.m_Reader.SetReaderGPO_SinglePort(4, m_SoftwareHeartBeatOnOff);
                            if (m_SoftwareHeartBeatOnOff)
                                m_SoftwareHeartBeatOnOff = false;
                            else
                                m_SoftwareHeartBeatOnOff = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                GC.Collect(); GC.WaitForPendingFinalizers();//IN JL 13-OCT-2013

                if (m_SoftwareHeartBeat != null)
                    m_SoftwareHeartBeat.Change(SmartHumpyController.Properties.Settings.Default.Sys_SoftwareHeartBeat_ms, System.Threading.Timeout.Infinite);
            }
        }

        private void OnCheckGateConfigurationChanges(object AObject)//IN JL 27-MAY-2016
        {
            try
            {
                if (m_CheckGateConfigurationChanges != null)
                    m_CheckGateConfigurationChanges.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

                m_Logger.AddActions("CurrApp Memory:", GC.GetTotalMemory(false).ToString());

                HumpyDetail mTempNewHD = new HumpyDetail();
                //JL IN If there is any configuration changed, restart the gate.
                if (m_DAL_LOCAL.SHS_IsHumpyConfigueChanged(m_HumpyDetail, ref mTempNewHD))
                {

                    UpdateStatusBar("Config change detected, system restarting....");
                    System.Threading.Thread.Sleep(1000);


                    m_HumpyDetail = mTempNewHD;
                    //Restart it???
                    DisposeForReStart();
                    System.Threading.Thread.Sleep(2000);

                    //System Rebooting....
                    //GUI clear upuuu
                    ResetGridControl();

                    System_Initialization_Data();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                m_Logger.AddErrors("OnCheckGateConfigurationChanges", ex.StackTrace.ToString());
            }
            finally
            {
                //GC.Collect();// GC.WaitForPendingFinalizers();//IN JL 13-OCT-2013
                GC.Collect(); GC.WaitForPendingFinalizers();//IN JL 13-OCT-2013

                if (m_CheckGateConfigurationChanges != null)
                    m_CheckGateConfigurationChanges.Change(SmartHumpyController.Properties.Settings.Default.Sys_CheckHumpyConfigurationChanges_ms, System.Threading.Timeout.Infinite);
            }
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            //Please Present the card 
            StartINOUTMessageBox(SHS_CheckInOutStatus.CheckIn);
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            //Please Present the card 
            StartINOUTMessageBox(SHS_CheckInOutStatus.CheckOut);
        }



        #region General Initialization

        private void System_Initialization_GUI()
        {
            //Hide Menu Bar && Form Border
            clsCrossThreadCalls.SetAnyProperty(this, "FormBorderStyle", FormBorderStyle.None);
            clsCrossThreadCalls.SetAnyProperty(menuStrip1, "Visible", false);
            //make split border invisiable.
            SetSplitBoarder(BorderStyle.None);
            //Set Grid View
            GridVisiableCOnfigurationBasedOnDetailMode();
            //Intialize the worker list 
            ConfigWorkListGrid();
            //Intialize the not checked-in list
            ConfigNotCheckedInWorkListGrid();//IN JL 25-MAY-2016
            //Initalize the CHecked In List
            //OUT JL 16-MAY-16 ConfigCheckedInListGrid();
        }

        private void System_Initialization_Data()
        {
            //Data access layer :JL 28-MAY-2016: most important one now.. needs to be able to grab the settings to start the app
            Intialization_DAL(m_HumpyDetail);
            //Header Initliazation
            Initilization_Header();
            //HF MISC Card Reader Initliazation
            Initialization_HFReader(m_HumpyDetail);
            //UHF Reader Intiliazation
            Initilaization_RFIDReader_DIRECT("", m_HumpyDetail);
            //Intilaize the UpdateTagListTimer
            Initialization_RetrieveTimer();
            //Disabled the full status button: default as the detail status report now JL 16-MAY-16
            btnCheckedInListOrDetailList.Visible = false;
            //Log it
            db_LoggerIt(SHS_LogType.ACTION, "DAL,HF,UHF,TIMER,GRID Initilaization Finished", m_CurrentHumpyOwner, m_HumpyDetail.Sys_HumpyId);
            //IN JL 27-MAY-2016: when it is all ok, set it to green as default start up color
            GUI_StackLightStatus_Humpy(m_Color_Green);
            //2. Actual Stack Light GPIO Port.
            GPIOStackLight(LightStackColor.GreenOnly);
        }

        private void Initilization_Header()
        {
            //Header Initliazation
            //this.Text = "Smart Humpy Controller -" + "Humpy:"
            //           + m_HumpyDetail.Sys_HumpyId
            //           + " Rd:" + m_HumpyDetail.HumpyGate.readerIP;

            m_CurrentHumpyOwner = "Smart Humpy Controller -" + "Humpy:"
                       + m_HumpyDetail.Sys_HumpyId
                       + " Rd:" + m_HumpyDetail.HumpyGate.readerIP;

            clsCrossThreadCalls.SetTextProperty(this, m_CurrentHumpyOwner);

        }

        private void Initialization_RetrieveTimer()
        {

            m_RetrieveTimeInterval_ms = m_HumpyDetail.Sys_RetrieveTimer_ms;

            m_UpdateTagListTimer = new System.Threading.Timer(new System.Threading.TimerCallback(OnUpdateTagListTimer), null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

            if (m_UpdateTagListTimer != null)
                m_UpdateTagListTimer.Change(m_RetrieveTimeInterval_ms, System.Threading.Timeout.Infinite);

            //Full Status report timer
            m_FullStatusReportTimeInterval_ms = m_HumpyDetail.FullStatusReportTimer_ms;

            m_Timer_FullStatusReport = new System.Threading.Timer(new System.Threading.TimerCallback(OnFullStatusReportTimer), null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

            //if (m_Timer_FullStatusReport != null)
            //    m_Timer_FullStatusReport.Change(m_FullStatusReportTimeInterval_ms, System.Threading.Timeout.Infinite);

        }

        private void GridVisiableCOnfigurationBasedOnDetailMode()
        {
            if (m_DetailMode == SHS_DetailMode.CheckedInList)
            {
                //GridControl1 ->Detail List
                //clsCrossThreadCalls.SetAnyProperty(gridControl1, "Visible", false);
                //clsCrossThreadCalls.SetAnyProperty(gridControl1, "Dock", DockStyle.None);

                //OUT JL 25-MAY-2016 clsCrossThreadCalls.SetAnyProperty(pnl_Detail, "Visible", false);
                //OUT JL 25-MAY-2016 clsCrossThreadCalls.SetAnyProperty(pnl_Detail, "Dock", DockStyle.None);


                //GridControl2 ->Checked In List
                //clsCrossThreadCalls.SetAnyProperty(gridControl2, "Visible", true);
                //clsCrossThreadCalls.SetAnyProperty(gridControl2, "Dock", DockStyle.Fill);

                clsCrossThreadCalls.SetAnyProperty(pnl_CheckedIn, "Visible", true);
                clsCrossThreadCalls.SetAnyProperty(pnl_CheckedIn, "Dock", DockStyle.Fill);

            }
            else if (m_DetailMode == SHS_DetailMode.DetailList)
            {
                ////GridControl1 ->Detail List
                //clsCrossThreadCalls.SetAnyProperty(gridControl1, "Visible", true);
                //clsCrossThreadCalls.SetAnyProperty(gridControl1, "Dock", DockStyle.Fill);

                //OUT JL 25-MAY-2016 clsCrossThreadCalls.SetAnyProperty(pnl_Detail, "Visible", true);
                //OUT JL 25-MAY-2016 clsCrossThreadCalls.SetAnyProperty(pnl_Detail, "Dock", DockStyle.Fill);

                //GridControl2 ->Checked In List
                //clsCrossThreadCalls.SetAnyProperty(gridControl2, "Visible", false);
                //clsCrossThreadCalls.SetAnyProperty(gridControl2, "Dock", DockStyle.None);

                clsCrossThreadCalls.SetAnyProperty(pnl_CheckedIn, "Visible", false);
                clsCrossThreadCalls.SetAnyProperty(pnl_CheckedIn, "Dock", DockStyle.None);
            }
        }

        private void ConfigWorkListGrid()
        {
            try
            {

                //gridView1.Appearance.Row.BackColor = Color.Black;
                //gridView1.Appearance.Row.BorderColor = Color.Black;
                //gridView1.Appearance.Row.ForeColor = Color.White;

                //gridControl1.Visible = true;

                //gridControl1.ForceInitialize();
                //gridView1.FocusedColumn = this.gridView1.VisibleColumns[0];
                //gridView1.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                //gridView1.ShowEditor();

                //1. Hide the indicator column or hide the focused row's indicator icon
                //1a. To hide the indicator column you can use the code below:
                gridView1.OptionsView.ShowIndicator = false;

                //2. Disable the cell focus rectangle:
                gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;

                //. Deactivate the EnableAppearanceFocusedCell, EnableAppearanceFocusedRow, and EnableAppearanceHideSelection options of the GridView.OptionsSelection property:
                gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
                gridView1.OptionsSelection.EnableAppearanceHideSelection = false;

                //Column name/size handling.
                ColumnView ColView = gridControl1.MainView as ColumnView;
                string[] zFieldNames = new string[] {SHS_DbReturnColumnNames.Column1,
                                                     SHS_DbReturnColumnNames.Column2,
                                                     SHS_DbReturnColumnNames.Column3,
                                                     SHS_DbReturnColumnNames.Column4,
                                                     SHS_DbReturnColumnNames.Column5,
                                                     SHS_DbReturnColumnNames.Column6,
                                                     SHS_DbReturnColumnNames.Column7,
                                                     SHS_DbReturnColumnNames.Column8,
                                                     SHS_DbReturnColumnNames.Column9};//IN JL 16-MAY-16
                DevExpress.XtraGrid.Columns.GridColumn zColumn;
                ColView.Columns.Clear();
                for (int i = 0; i < zFieldNames.Length; i++)
                {
                    zColumn = ColView.Columns.AddField(zFieldNames[i]);
                    zColumn.VisibleIndex = i;

                }


                gridView1.Columns[0].Caption = "NAME";// "Name";
                //gridView1.Columns[0].Caption = SHS_DbReturnColumnNames.Column1;// "Name";
                gridView1.Columns[0].Width = 200;
                gridView1.Columns[0].Visible = true;
                gridView1.Columns[0].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;//IN JL 25-MAY-2016
                gridView1.Columns[0].OptionsColumn.AllowEdit = false;
                gridView1.Columns[0].OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;


                gridView1.Columns[1].Caption = SHS_DbReturnColumnNames.Column2;//"CheckedIn";
                gridView1.Columns[1].Visible = false;
                //gridView1.Columns[1].Width = 150;
                gridView1.Columns[2].Caption = SHS_DbReturnColumnNames.Column3;//"PersonUID";
                gridView1.Columns[2].Visible = false;
                //gridView1.Columns[2].Width = 150;
                gridView1.Columns[3].Caption = SHS_DbReturnColumnNames.Column4;//"MISCCard";
                gridView1.Columns[3].Visible = false;
                //gridView1.Columns[3].Width = 150;
                gridView1.Columns[4].Caption = SHS_DbReturnColumnNames.Column5;//"FirstSeenDT";
                gridView1.Columns[4].Visible = false;
                //gridView1.Columns[4].Width = 150;
                gridView1.Columns[5].Caption = SHS_DbReturnColumnNames.Column6;//"LastSeenDT";
                gridView1.Columns[5].Visible = false;
                //gridView1.Columns[5].Width = 150;
                gridView1.Columns[6].Caption = "HUMPY";// SHS_DbReturnColumnNames.Column7;//"LocId";
                gridView1.Columns[6].Visible = true;
                gridView1.Columns[6].Width = 150;
                gridView1.Columns[6].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;//IN JL 25-MAY-2016
                gridView1.Columns[6].OptionsColumn.AllowEdit = false;
                gridView1.Columns[6].OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;

                gridView1.Columns[7].Caption = "STATUS";//SHS_DbReturnColumnNames.Column8;//"INOUT";
                gridView1.Columns[7].Visible = true;
                gridView1.Columns[7].Width = 200;
                gridView1.Columns[7].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;//IN JL 25-MAY-2016
                gridView1.Columns[7].OptionsColumn.AllowEdit = false;
                gridView1.Columns[7].OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;


                //IN JL 16-MAY-16: Icon Cross/Tick/Questioning Mark ////SHS_DbReturnColumnNames.Column9;//"INOUTIcon"; //SP:[BDSsp_WorkerTagList]
                gridView1.Columns[8].Caption = "  ";
                gridView1.Columns[8].Visible = true;
                gridView1.Columns[8].Width = 50;

                this.repositoryItemImageComboBox_IconStatus = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
                this.repositoryItemImageComboBox_IconStatus.AutoHeight = false;
                this.repositoryItemImageComboBox_IconStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
                this.repositoryItemImageComboBox_IconStatus.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("B", "B", 1), //Display Value, Field Value, Pic Index //IN
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("A", "A", 2),//Display Value, Field Value, Pic Index //OUT
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("C", "C", 0)});//Not Checked In
                this.repositoryItemImageComboBox_IconStatus.LargeImages = this.imgSmallImageCollection;
                repositoryItemImageComboBox_IconStatus.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;

                repositoryItemImageComboBox_IconStatus.Buttons[0].Visible = false;//IN JL 25-MAY-2016
                repositoryItemImageComboBox_IconStatus.Buttons[1].Visible = false;//IN JL 25-MAY-2016
                repositoryItemImageComboBox_IconStatus.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;//IN JL 25-MAY-2016

                gridView1.Columns[8].ColumnEdit = repositoryItemImageComboBox_IconStatus;//Status.

                gridView1.ClearSorting();
                //gridView1.Columns[SHS_DbReturnColumnNames.Column9].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                gridView1.Columns[8].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                gridView1.Columns[8].OptionsColumn.AllowEdit = false;
                gridView1.Columns[8].OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;

                gridView1.OptionsCustomization.AllowFilter = false;
                gridView1.OptionsCustomization.AllowColumnMoving = false;
                //string mSQLQuery = "SELECT EPC,AssetDesc FROM EPCData";
                //SqlCeDataReader dataReader = null;
                //dataReader = m_dbConn.ExecuteReader(mSQLQuery);
                //DataTable dt = new DataTable();
                //dt.Load(dataReader);
                //gridReport.DataSource = dt.DefaultView;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(String.Format("List Configuration: {0}", Ex.Message), "Work List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ConfigNotCheckedInWorkListGrid()
        {
            try
            {

                //gridView1.Appearance.Row.BackColor = Color.Black;
                //gridView1.Appearance.Row.BorderColor = Color.Black;
                //gridView1.Appearance.Row.ForeColor = Color.White;
                //gridControl1.Visible = true;
                //gridControl1.ForceInitialize();
                //gridView1.FocusedColumn = this.gridView1.VisibleColumns[0];
                //gridView1.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                //gridView1.ShowEditor();

                gridView3.OptionsView.ShowColumnHeaders = false;//IN JL 25-MAY-16 : hide the header

                //1. Hide the indicator column or hide the focused row's indicator icon
                //1a. To hide the indicator column you can use the code below:
                gridView3.OptionsView.ShowIndicator = false;

                //2. Disable the cell focus rectangle:
                gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;

                //. Deactivate the EnableAppearanceFocusedCell, EnableAppearanceFocusedRow, and EnableAppearanceHideSelection options of the GridView.OptionsSelection property:
                gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridView3.OptionsSelection.EnableAppearanceFocusedRow = false;
                gridView3.OptionsSelection.EnableAppearanceHideSelection = false;

                //Column name/size handling.
                ColumnView ColView = gridControl3.MainView as ColumnView;
                string[] zFieldNames = new string[] {SHS_DbReturnColumnNames.Column1,
                                                     SHS_DbReturnColumnNames.Column2,
                                                     SHS_DbReturnColumnNames.Column3,
                                                     SHS_DbReturnColumnNames.Column4,
                                                     SHS_DbReturnColumnNames.Column5,
                                                     SHS_DbReturnColumnNames.Column6,
                                                     SHS_DbReturnColumnNames.Column7,
                                                     SHS_DbReturnColumnNames.Column8,
                                                     SHS_DbReturnColumnNames.Column9};//IN JL 16-MAY-16
                DevExpress.XtraGrid.Columns.GridColumn zColumn;
                ColView.Columns.Clear();
                for (int i = 0; i < zFieldNames.Length; i++)
                {
                    zColumn = ColView.Columns.AddField(zFieldNames[i]);
                    zColumn.VisibleIndex = i;

                }

                gridView3.Columns[0].Caption = "NAME";// "Name";
                //gridView1.Columns[0].Caption = SHS_DbReturnColumnNames.Column1;// "Name";
                gridView3.Columns[0].Width = 200;
                gridView3.Columns[0].Visible = true;
                gridView3.Columns[0].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;//IN JL 25-MAY-2016

                gridView3.Columns[1].Caption = SHS_DbReturnColumnNames.Column2;//"CheckedIn";
                gridView3.Columns[1].Visible = false;
                //gridView1.Columns[1].Width = 150;
                gridView3.Columns[1].OptionsColumn.AllowEdit = false;
                gridView3.Columns[1].OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;

                gridView3.Columns[2].Caption = SHS_DbReturnColumnNames.Column3;//"PersonUID";
                gridView3.Columns[2].Visible = false;
                //gridView1.Columns[2].Width = 150;
                gridView3.Columns[2].OptionsColumn.AllowEdit = false;
                gridView3.Columns[2].OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;

                gridView3.Columns[3].Caption = SHS_DbReturnColumnNames.Column4;//"MISCCard";
                gridView3.Columns[3].Visible = false;
                //gridView1.Columns[3].Width = 150;
                gridView3.Columns[3].OptionsColumn.AllowEdit = false;
                gridView3.Columns[3].OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;

                gridView3.Columns[4].Caption = SHS_DbReturnColumnNames.Column5;//"FirstSeenDT";
                gridView3.Columns[4].Visible = false;
                //gridView1.Columns[4].Width = 150;
                gridView3.Columns[4].OptionsColumn.AllowEdit = false;
                gridView3.Columns[4].OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;

                gridView3.Columns[5].Caption = SHS_DbReturnColumnNames.Column6;//"LastSeenDT";
                gridView3.Columns[5].Visible = false;
                //gridView1.Columns[5].Width = 150;
                gridView3.Columns[5].OptionsColumn.AllowEdit = false;
                gridView3.Columns[5].OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;

                gridView3.Columns[6].Caption = "HUMPY";// SHS_DbReturnColumnNames.Column7;//"LocId";
                gridView3.Columns[6].Visible = true;
                gridView3.Columns[6].Width = 150;
                gridView3.Columns[6].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;//IN JL 25-MAY-2016

                gridView3.Columns[6].OptionsColumn.AllowEdit = false;
                gridView3.Columns[6].OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;

                gridView3.Columns[7].Caption = "STATUS";//SHS_DbReturnColumnNames.Column8;//"INOUT";
                gridView3.Columns[7].Visible = true;
                gridView3.Columns[7].Width = 200;
                gridView3.Columns[7].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;//IN JL 25-MAY-2016

                gridView3.Columns[7].OptionsColumn.AllowEdit = false;
                gridView3.Columns[7].OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;

                //IN JL 16-MAY-16: Icon Cross/Tick/Questioning Mark ////SHS_DbReturnColumnNames.Column9;//"INOUTIcon"; //SP:[BDSsp_WorkerTagList]
                gridView3.Columns[8].Caption = "  ";
                gridView3.Columns[8].Visible = true;
                gridView3.Columns[8].Width = 50;

                gridView3.Columns[8].OptionsColumn.AllowEdit = false;
                gridView3.Columns[8].OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;

                this.repositoryItemImageComboBox_IconStatus = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
                this.repositoryItemImageComboBox_IconStatus.AutoHeight = false;
                this.repositoryItemImageComboBox_IconStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
                this.repositoryItemImageComboBox_IconStatus.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("B", "B", 1), //Display Value, Field Value, Pic Index //IN
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("A", "A", 2),//Display Value, Field Value, Pic Index //OUT
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("C", "C", 0)});//Not Checked In
                this.repositoryItemImageComboBox_IconStatus.LargeImages = this.imgSmallImageCollection;
                repositoryItemImageComboBox_IconStatus.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;

                repositoryItemImageComboBox_IconStatus.Buttons[0].Visible = false;//IN JL 25-MAY-2016
                repositoryItemImageComboBox_IconStatus.Buttons[1].Visible = false;//IN JL 25-MAY-2016
                repositoryItemImageComboBox_IconStatus.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;//IN JL 25-MAY-2016

                gridView3.Columns[8].ColumnEdit = repositoryItemImageComboBox_IconStatus;//Status.

                gridView3.ClearSorting();
                //gridView1.Columns[SHS_DbReturnColumnNames.Column9].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                gridView3.Columns[8].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                gridView3.Columns[8].OptionsColumn.AllowEdit = false;
                gridView3.Columns[8].OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;

                gridView3.OptionsCustomization.AllowFilter = false;
                gridView3.OptionsCustomization.AllowColumnMoving = false;

                //string mSQLQuery = "SELECT EPC,AssetDesc FROM EPCData";
                //SqlCeDataReader dataReader = null;
                //dataReader = m_dbConn.ExecuteReader(mSQLQuery);
                //DataTable dt = new DataTable();
                //dt.Load(dataReader);
                //gridReport.DataSource = dt.DefaultView;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(String.Format("List Configuration: {0}", Ex.Message), "Work List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ConfigCheckedInListGrid()
        {
            try
            {
                //gridView1.Appearance.Row.BackColor = Color.Black;
                //gridView1.Appearance.Row.BorderColor = Color.Black;
                //gridView1.Appearance.Row.ForeColor = Color.White;

                //gridControl2.Visible = true;
                //gridControl1.ForceInitialize();
                //gridView1.FocusedColumn = this.gridView1.VisibleColumns[0];
                //gridView1.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                //gridView1.ShowEditor();

                //1. Hide the indicator column or hide the focused row's indicator icon
                //1a. To hide the indicator column you can use the code below:
                gridView_CheckedIn.OptionsView.ShowIndicator = false;

                //2. Disable the cell focus rectangle:
                gridView_CheckedIn.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;

                //. Deactivate the EnableAppearanceFocusedCell, EnableAppearanceFocusedRow, and EnableAppearanceHideSelection options of the GridView.OptionsSelection property:
                gridView_CheckedIn.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridView_CheckedIn.OptionsSelection.EnableAppearanceFocusedRow = false;
                gridView_CheckedIn.OptionsSelection.EnableAppearanceHideSelection = false;

                //Column name/size handling.
                ColumnView ColView = gridControl2.MainView as ColumnView;
                string[] zFieldNames = new string[] {SHS_DbReturnColumnNames.Column1,
                                                     SHS_DbReturnColumnNames.Column2,
                                                     SHS_DbReturnColumnNames.Column3,
                                                     SHS_DbReturnColumnNames.Column4,
                                                     SHS_DbReturnColumnNames.Column5,
                                                     SHS_DbReturnColumnNames.Column6,
                                                     SHS_DbReturnColumnNames.Column7,
                                                     SHS_DbReturnColumnNames.Column8};
                DevExpress.XtraGrid.Columns.GridColumn zColumn;
                ColView.Columns.Clear();
                for (int i = 0; i < zFieldNames.Length; i++)
                {
                    zColumn = ColView.Columns.AddField(zFieldNames[i]);
                    zColumn.VisibleIndex = i;
                }

                gridView_CheckedIn.Columns[0].Caption = "NAME";// "Name";
                //gridView_CheckedIn.Columns[0].Caption = SHS_DbReturnColumnNames.Column1;// "Name";
                gridView_CheckedIn.Columns[0].Width = 370;
                gridView_CheckedIn.Columns[0].Visible = true;
                gridView_CheckedIn.Columns[0].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                gridView_CheckedIn.Columns[1].Caption = SHS_DbReturnColumnNames.Column2;//"CheckedIn";
                gridView_CheckedIn.Columns[1].Visible = false;
                gridView_CheckedIn.Columns[1].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                //gridView_CheckedIn.Columns[1].Width = 150;
                gridView_CheckedIn.Columns[2].Caption = SHS_DbReturnColumnNames.Column3;//"PersonUID";
                gridView_CheckedIn.Columns[2].Visible = false;
                gridView_CheckedIn.Columns[2].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                //gridView_CheckedIn.Columns[2].Width = 150;
                gridView_CheckedIn.Columns[3].Caption = SHS_DbReturnColumnNames.Column4;//"MISCCard";
                gridView_CheckedIn.Columns[3].Visible = false;
                gridView_CheckedIn.Columns[3].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                //gridView_CheckedIn.Columns[3].Width = 150;
                gridView_CheckedIn.Columns[4].Caption = SHS_DbReturnColumnNames.Column5;//"FirstSeenDT";
                gridView_CheckedIn.Columns[4].Visible = false;
                gridView_CheckedIn.Columns[4].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                //gridView_CheckedIn.Columns[4].Width = 150;
                gridView_CheckedIn.Columns[5].Caption = SHS_DbReturnColumnNames.Column6;//"LastSeenDT";
                gridView_CheckedIn.Columns[5].Visible = false;
                gridView_CheckedIn.Columns[5].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                //gridView_CheckedIn.Columns[5].Width = 150;
                gridView_CheckedIn.Columns[6].Caption = "Humpy ID";// SHS_DbReturnColumnNames.Column7;//"LocId";
                gridView_CheckedIn.Columns[6].Visible = false;
                gridView_CheckedIn.Columns[6].Width = 150;
                gridView_CheckedIn.Columns[6].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                gridView_CheckedIn.Columns[7].Caption = "Status";//SHS_DbReturnColumnNames.Column8;//"INOUT";
                gridView_CheckedIn.Columns[7].Visible = false;
                gridView_CheckedIn.Columns[7].Width = 150;
                gridView_CheckedIn.Columns[7].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                //gridView_CheckedIn.Columns[7].OptionsColumn.AllowEdit = false;
                //gridView_CheckedIn.Columns[7].OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
                gridView_CheckedIn.OptionsCustomization.AllowFilter = false;


                //string mSQLQuery = "SELECT EPC,AssetDesc FROM EPCData";
                //SqlCeDataReader dataReader = null;
                //dataReader = m_dbConn.ExecuteReader(mSQLQuery);
                //DataTable dt = new DataTable();
                //dt.Load(dataReader);
                //gridReport.DataSource = dt.DefaultView;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(String.Format("List Configuration: {0}", Ex.Message), "Checked-In List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool Intialization_DAL(HumpyDetail aHD)
        {
            bool zRet = false;

            m_HumpyId = aHD.Sys_HumpyId;
            m_DAL = new HumpyDAL(aHD.Sys_DBConnection);
            m_DAL_Retriever = new HumpyDAL(aHD.Sys_DBConnection);
            m_DAL_Journaler = new HumpyDAL(aHD.Sys_DBConnection);
            m_DAL_CheckedInWorker = new HumpyDAL(aHD.Sys_DBConnection);
            m_DAL_DBLogger = new HumpyDAL(aHD.Sys_DBConnection);//IN JL 12-MAY-2016 

            zRet = true;
            return zRet;
        }

        private bool Disposal_DAL()//IN JL 12-MAY-2016 
        {
            bool zRet = false;

            m_DAL = null;
            m_DAL_Retriever = null;
            m_DAL_Journaler = null;
            m_DAL_CheckedInWorker = null;
            m_DAL_DBLogger = null;

            zRet = true;
            return zRet;
        }

        #endregion

        #region Check In Out Present message box
        private void StartINOUTMessageBox(SHS_CheckInOutStatus aStatus)
        {
            try
            {
                //If it is already running, stop it.
                if (m_CheckInOutThread != null)
                {
                    if (m_CheckInOutThread.IsAlive)
                        m_CheckInOutThread.Abort();

                    m_CheckInOutThread = null;
                }

               if (aStatus == SHS_CheckInOutStatus.CheckIn)
                {
                    m_CurrentCheckInOutStatus = SHS_CheckInOutStatus.CheckIn;

                    m_CheckInOutThread = new Thread(Thread_CheckInMessageBox);
                    m_CheckInOutThread.IsBackground = true;
                    m_CheckInOutThread.Start();
                    //Thread.Sleep(SmartHumpyController.Properties.Settings.Default.CheckInOutWaitingTime_ms);
                    //if (m_CheckInOutThread.IsAlive)
                    //{
                    //    m_CheckInOutThread.Abort();
                    //}
                }
                else if (aStatus == SHS_CheckInOutStatus.CheckOut)
                {
                    m_CurrentCheckInOutStatus = SHS_CheckInOutStatus.CheckOut;

                    m_CheckInOutThread = new Thread(Thread_CheckOutMessageBox);
                    m_CheckInOutThread.IsBackground = true;
                    m_CheckInOutThread.Start();
                    //Thread.Sleep(SmartHumpyController.Properties.Settings.Default.CheckInOutWaitingTime_ms);
                    //if (m_CheckInOutThread.IsAlive)
                    //    m_CheckInOutThread.Abort();
                }
                hfReaderConnected = true;
            }
            catch (Exception ex)
            {
                hfReaderConnected = false;
                throw ex;
            }
            finally
            {
            }
        }

        private void StopINOUTMessageBox()
        {
            try
            {
                //If it is already running, stop it.
                if (m_CheckInOutThread != null)
                {
                    if (m_CheckInOutThread.IsAlive)
                        m_CheckInOutThread.Abort();

                    //m_CheckInOutThread = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_CurrentCheckInOutStatus = SHS_CheckInOutStatus.NA;
            }
        }

        void Thread_CheckInMessageBox()
        {
            FrmStatus zFrm = new FrmStatus("CHECK IN", m_HumpyDetail.CheckInOutWaitingTime_ms);
            zFrm.ShowDialog();
            m_CurrentCheckInOutStatus = SHS_CheckInOutStatus.NA;
        }
        void Thread_CheckOutMessageBox()
        {
            FrmStatus zFrm = new FrmStatus("CHECK OUT", m_HumpyDetail.CheckInOutWaitingTime_ms);
            zFrm.ShowDialog();
            m_CurrentCheckInOutStatus = SHS_CheckInOutStatus.NA;
        }
        #endregion

        private void db_LoggerIt(SHS_LogType aJt, string aMessage, string aCreatedBy, string aHumpyID)
        {

            if (m_HumpyDetail.Sys_DBorLocalLogging)
            {
                //DB logger
                try
                {
                    m_DAL_DBLogger.SHS_Insert_Log(aJt, aMessage, aCreatedBy, aHumpyID);
                }
                catch (Exception ex)
                {
                    ex.ToString();

                    //SYSTEM FAILURE
                    m_SystemFailure_ReadersWIFIDBConn = true;
                    UpdateStatusBar("DB Connection Failure");
                }
            }
            else
            {

                //txt file logger
                try
                {
                    m_Logger.AddActions(aCreatedBy, aJt.ToString() + "-" + aMessage);
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
        }

        #region HF Reader Initial/Stop/Tag Event Handler
        private bool Initialization_HFReader(HumpyDetail aHD)
        {
            bool zRet = false;

            m_HFReader = new HFReaderSerial(aHD.Rd_HFReader_COM,
                                int.Parse(aHD.Rd_HFReader_Baudrate),
                                true);
            m_HFReader.OnMessageReceived += new HFReaderSerial.DelegateMessageRecieved(m_HFReader_OnMessageReceived);

            try
            {
                insertApplicationLogs.InsertApplicationLogs(masterHumpyConnectionString,
                                              insertApplicationLogs.AppLogs_ComponentHumpyValue,
                                              "HF",
                                              insertApplicationLogs.AppLogs_StatusNormalValue,
                                              insertApplicationLogs.AppLogs_ActionInitializedValue,
                                              "",
                                              humpyLocationID,
                                              wifiIPAddress);
                if (m_HFReader.m_PortOpen == true)
                    m_HFReader.ClosePort();

                m_HFReader.OpenPort();
                connectToolStripMenuItem.Text = "Disconnect";
                UpdateStatusBar("HF Reader Connected");

                zRet = true;
                hfReaderConnected = true;

            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("denied") > -1)
                {
                    while (m_HFReader.m_PortOpen == true)
                    {
                        m_HFReader.ClosePort();
                    }
                    if (m_HFReader.m_PortOpen == false)
                        Initialization_HFReader(m_HumpyDetail);

                    connectToolStripMenuItem.Text = "Disconnect";
                    UpdateStatusBar("HF Reader Connected");

                    zRet = true;
                    hfReaderConnected = true;

                }
                else
                { 
                connectToolStripMenuItem.Text = "Connect";
                UpdateStatusBar("HF Reader Connect Error");
                m_Logger.AddErrors("HF-Connect", ex.ToString());
                //MessageBox.Show(ex.Message);

                //SYSTEM FAILURE
                m_SystemFailure_ReadersWIFIDBConn = true;
                UpdateStatusBar("HF Reader Failure");

                zRet = false;
                hfReaderConnected = false;
                }
            }

            AdminMenuStatusLightChange_HFReaderStatus(zRet);//IN JL 29-MAY-2016

            return zRet;
        }

        private bool Stop_HFReader()
        {
            bool zRet = false;

            try
            {
                if (m_HFReader != null)
                {
                    if (m_HFReader.m_PortOpen)
                    {
                        m_HFReader.OnMessageReceived -= new HFReaderSerial.DelegateMessageRecieved(m_HFReader_OnMessageReceived);
                        m_HFReader.ClosePort();
                        m_HFReader = null;
                    }
                    else
                    {
                        m_HFReader = null;
                    }

                    connectToolStripMenuItem.Text = "Connect";
                    hfReaderConnected = false;
                    UpdateStatusBar("HF Reader Disconnected");

                }

            }
            catch (Exception ex)
            {
                connectToolStripMenuItem.Text = "Connect";
                UpdateStatusBar("HF Reader Disconnect Error");
                m_Logger.AddErrors("HF-Disconnect", ex.ToString());
                hfReaderConnected = false;
            }

            zRet = true;

            return zRet;
        }


        void m_HFReader_OnMessageReceived(string Message)
        {
           

            Workers getWorkersName = new Workers();
            Workers checkWorkerStatus = new Workers();
            Boolean workerStatus = false;
            try
            {
                ////throw new NotImplementedException();
                ////Log it
                db_LoggerIt(SHS_LogType.NA,
                          "MISCCard Original Input" + Message,
                           m_CurrentHumpyOwner,
                           m_HumpyDetail.Sys_HumpyId);//IN JL 12-MAY-201
                uhfReaderConnected = true;
            }
            catch (Exception ex)
            {
                uhfReaderConnected = false;
                ex.ToString();
            }

            //string zMISCCard = Message.Replace("\r", "").Replace("\n", "").ToUpper();//IN JL 31-MAY-2016
            string zMISCCard = Message.Replace("\n", "").Trim();//IN JL 31-MAY-2016
            //MessageBox.Show(zMISCCard);
            //
            if (gpiTwoHasSignal)
                zMISCCard = "";

            workerStatus = checkWorkerStatus.GetWorkerCurrentCheckInStatus(masterHumpyConnectionString, zMISCCard.Trim());


            if (workerStatus)
                m_CurrentCheckInOutStatus = SHS_CheckInOutStatus.CheckOut;
            else
                m_CurrentCheckInOutStatus = SHS_CheckInOutStatus.CheckIn;
            
      

             //

                if (m_CurrentCheckInOutStatus == SHS_CheckInOutStatus.CheckIn)
                {
                    if (SearchCurrentGridView_NotCheckInList(SHS_DbReturnColumnNames.Column4, zMISCCard.Trim()))//MISCCard
                    {
                        //If found it checked worker in.
                        m_DAL.SHS_Update_Personnel_CheckInOut(zMISCCard.Trim(), SHS_CheckInOutStatus.CheckIn);

                        //insertApplicationLogs.InsertApplicationLogs(masterHumpyConnectionString,
                        //              insertApplicationLogs.AppLogs_ComponentWorkerValue,
                        //              getWorkersName.GetWorkersName(masterHumpyConnectionString,zMISCCard.Trim()),
                        //              insertApplicationLogs.AppLogs_StatusUncheckedValue ,
                        //              insertApplicationLogs.AppLogs_ActionCheckingInValue,
                        //              zMISCCard.Trim(),
                        //              humpyLocationID,
                        //              wifiIPAddress);

                        //Log it
                        db_LoggerIt(SHS_LogType.ACTION,
                                  "MSIC Card Checked In:" + zMISCCard.Trim(),
                                   m_CurrentHumpyOwner,
                                   m_HumpyDetail.Sys_HumpyId);//IN JL 12-MAY-2015

                        //OUT JL 12-MAY-2015 
                        //db_LoggerIt(SHS_JournalType.CHECKIN, "MSIC Card Checked In:"+zMISCCard, m_CurrentHumpyOwner);

                        UpdateStatusBar("Checked in successfully");
                    }
                    else //Cannot find this work ground helmet tag, or not detected.
                    {
                        //! it could be that access card not registered..... Future...
                        if (!gpiTwoHasSignal)
                         UpdateStatusBar("Err: Worker's helmet tag is not detected");
                    }
                }
                else if (m_CurrentCheckInOutStatus == SHS_CheckInOutStatus.CheckOut)
                {
                    if (SearchCurrentGridView_CheckInList(SHS_DbReturnColumnNames.Column4, zMISCCard.Trim()))//MISCCard
                    {
                        //If found it checked worker in.
                        m_DAL.SHS_Update_Personnel_CheckInOut(zMISCCard.Trim(), SHS_CheckInOutStatus.CheckOut);

                        //Log it
                        db_LoggerIt(SHS_LogType.ACTION,
                                  "MSIC Card Checked Out:" + zMISCCard.Trim(),
                                   m_CurrentHumpyOwner,
                                   m_HumpyDetail.Sys_HumpyId);//IN JL 12-MAY-2015

                        //db_LoggerIt(SHS_JournalType.CHECKOUT, "MSIC Card Checked Out:" + zMISCCard, m_CurrentHumpyOwner);

                        //insertApplicationLogs.InsertApplicationLogs(masterHumpyConnectionString,
                        //          insertApplicationLogs.AppLogs_ComponentWorkerValue,
                        //          getWorkersName.GetWorkersName(masterHumpyConnectionString, zMISCCard.Trim()),
                        //          insertApplicationLogs.AppLogs_StatusCheckedValue,
                        //          insertApplicationLogs.AppLogs_ActionCheckingOutValue,
                        //          zMISCCard.Trim(),
                        //          humpyLocationID,
                        //          wifiIPAddress);

                        UpdateStatusBar("Checked Out successfully");
                    }
                    else //Cannot find this work ground helmet tag, or not detected.
                    {
                        //! it could be that access card not registered..... Future...
                        if (!gpiTwoHasSignal)
                            UpdateStatusBar("Worker hasn't been checked in yet");
                    }

                }
                else if (m_CurrentCheckInOutStatus == SHS_CheckInOutStatus.NA)
                {
                    //Dont do anything
                    UpdateStatusBar("Please press the check in or check out button first");
                }


            StopINOUTMessageBox();
            //UpdateStatusBar("HF Card Detected:"+Message);

        }

        #endregion


        private delegate void UpdateSystemErrorDelegate();
        private void UpdateSystemError()
        {
            Invoke(new UpdateSystemErrorDelegate(delegate
            {
                //If system error flag set, constant Yellow
                if (m_SystemFailure_ReadersWIFIDBConn)
                {

                    GUI_StackLightStatus_Humpy(m_Color_Yellow);
                    GPIOStackLight(LightStackColor.OrangeOnly);
                }
            }
            ));
        }

        private delegate void UpdateStackLightDelegate();
        private void UpdateWarningLights_GPOGUI()
        {
            Invoke(new UpdateStackLightDelegate(delegate
            {

                //Field INOUT Column
                if (SearchCurrentGridView_CheckInList(SHS_DbReturnColumnNames.Column8, SHS_INOUT_Values.OUTValue))//INOUT IN/OUT/Not Checked In
                {
                    //If found any one OUT, RED WARNING!!!!!
                    //1. Local GUI stack light
                    GUI_StackLightStatus_Humpy(m_Color_Red);
                    //2. Actual Stack Light GPIO Port.
                    GPIOStackLight(LightStackColor.RedOnly);
                }

                else //all IN or someone 
                {
                    if (m_IgnoreNoneCheckedInWorker)
                    {
                        //If anyone is in the humpy but not checked in.
                        if (SearchCurrentGridView_NotCheckInList(SHS_DbReturnColumnNames.Column8, SHS_INOUT_Values.NotCheckedInValue))//INOUT IN/OUT/Not Checked In
                        {
                            //If found any one OUT, RED WARNING!!!!!
                            //1. Local GUI stack light
                            GUI_StackLightStatus_Humpy(m_Color_Yellow);
                            //2. Actual Stack Light GPIO Port.
                            GPIOStackLight(LightStackColor.OrangeOnly);
                        }
                        else
                        {
                            //1. Local GUI stack light
                            GUI_StackLightStatus_Humpy(m_Color_Green);
                            //2. Actual Stack Light GPIO Port.
                            GPIOStackLight(LightStackColor.GreenOnly);
                        }
                    }
                    else
                    {
                        //1. Local GUI stack light
                        GUI_StackLightStatus_Humpy(m_Color_Green);
                        //2. Actual Stack Light GPIO Port.
                        GPIOStackLight(LightStackColor.GreenOnly);
                    }
                }
            }
            ));
        }

        #region UHF Reader Initlial/Stop/Tag Event Handler/GPO control(stack light)
        //OUT JL 28-MAY-2016 private bool Initilaization_RFIDReader_DIRECT(string UserMemoryData)

        private bool Initilaization_RFIDReader_DIRECT(string UserMemoryData, HumpyDetail aHD)
        {
            bool zRet = false;

            #region Direct Control Mode

            #region disabled the config file loading...28-MAY-2016
            //GateDetail zGD = new GateDetail
            //    {
            //        ant1Power = SmartHumpyController.Properties.Settings.Default.Rd_Ant1Power,
            //        ant1Sensitivity = SmartHumpyController.Properties.Settings.Default.Rd_ant1Sensitivity,
            //        ant2Power = SmartHumpyController.Properties.Settings.Default.Rd_Ant2Power,
            //        ant2Sensitivity = SmartHumpyController.Properties.Settings.Default.Rd_ant2Sensitivity,
            //        ant3Power = SmartHumpyController.Properties.Settings.Default.Rd_Ant3Power,
            //        ant3Sensitivity = SmartHumpyController.Properties.Settings.Default.Rd_ant3Sensitivity,
            //        ant4Power = SmartHumpyController.Properties.Settings.Default.Rd_Ant4Power,
            //        ant4Sensitivity = SmartHumpyController.Properties.Settings.Default.Rd_ant4Sensitivity,
            //        readerType = SmartHumpyController.Properties.Settings.Default.Rd_Type.ToUpper()
            //    };

            //    zGD.GateReader.ReaderName = m_HumpyId;
            //    zGD.GateReader.ReaderIPAddress = SmartHumpyController.Properties.Settings.Default.Rd_UHFReader_IP;

            //    if (zGD.readerType.ToUpper() == "R220".ToUpper())
            //        zGD.GateReader.ReaderType = OctaneReader.ReaderType.R220;
            //    else if (zGD.readerType.ToUpper() == "R420".ToUpper())
            //        zGD.GateReader.ReaderType = OctaneReader.ReaderType.R420;
            //    else if (zGD.readerType.ToUpper() == "XPortal".ToUpper())
            //        zGD.GateReader.ReaderType = OctaneReader.ReaderType.XPortal;

            //    zGD.GateReader.Antenna1.TxPower = double.Parse(zGD.ant1Power);
            //    zGD.GateReader.Antenna1.RssiThreshold = double.Parse(zGD.ant1Sensitivity);
            //    zGD.GateReader.Antenna2.TxPower = double.Parse(zGD.ant2Power);
            //    zGD.GateReader.Antenna2.RssiThreshold = double.Parse(zGD.ant2Sensitivity);
            //    zGD.GateReader.Antenna3.TxPower = double.Parse(zGD.ant3Power);
            //    zGD.GateReader.Antenna3.RssiThreshold = double.Parse(zGD.ant3Sensitivity);
            //    zGD.GateReader.Antenna4.TxPower = double.Parse(zGD.ant4Power);
            //    zGD.GateReader.Antenna4.RssiThreshold = double.Parse(zGD.ant4Sensitivity);

            //    ProjectConfiguration zPc = new ProjectConfiguration(SmartHumpyController.Properties.Settings.Default.Rd_Filter1,
            //                                                        SmartHumpyController.Properties.Settings.Default.Rd_Filter2);

            //    zGD.rdReaderMode = SmartHumpyController.Properties.Settings.Default.Rd_ReaderMode;//IN JL 25-APR-2016
            //    zGD.rdSearchMode = SmartHumpyController.Properties.Settings.Default.Rd_SearchMode;//IN JL 25-APR-2016
            //    zGD.rdSessionNum = SmartHumpyController.Properties.Settings.Default.Rd_SessionNum;//IN JL 25-APR-2016
            #endregion

            ProjectConfiguration zPc = new ProjectConfiguration(aHD.HumpyGate.Rd_Filter1,
                                                                aHD.HumpyGate.Rd_Filter2);

            m_UHFReader = new ReaderDirectController(aHD, zPc, false, "", aHD.Sys_DBConnection);

            insertApplicationLogs.InsertApplicationLogs(masterHumpyConnectionString,
                                              insertApplicationLogs.AppLogs_ComponentHumpyValue,
                                              "UHF",
                                              insertApplicationLogs.AppLogs_StatusNormalValue,
                                              insertApplicationLogs.AppLogs_ActionInitializedValue,
                                              "",
                                              humpyLocationID,
                                              wifiIPAddress);

            Thread.Sleep(1000);
            if (m_UHFReader.m_ReaderIsConnected)
            {
                //m_UHFReader.OndelegateReadingFinished += mReaderController_OndelegateReadingFinished;
                //m_UHFReader.OndelegateReadingFinishedARCRets += mReaderController_OndelegateReadingFinishedARCRets;
                //m_UHFReader.m_Reader.OnDelegateTagDetected += new OctaneReader.delegateTagDetected(m_Reader_OnDelegateTagDetected);
                m_UHFReader.OndelegateTagReported += new ReaderDirectController.delegateTagReported(m_UHFReader_OndelegateTagReported);

                //IN JL 27-MAY-2016
                m_UHFReader.OndelegateReaderConnectionChanged += new ReaderDirectController.delegateReaderConnectionChanged(m_UHFReader_OndelegateReaderConnectionChanged);

                //IN JL 16-MAY-16
                m_UHFReader.OndelegateGPIChanged += new ReaderDirectController.delegateGPIChanged(m_UHFReader_OndelegateGPIChanged);

                zRet = true;
                uhfReaderConnected = true;
                connectToolStripMenuItem1.Text = "Disconnect";
                //clsCrossThreadCalls.SetTextProperty(stripRdStartStop, "Disconnect");
           

                UpdateStatusBar("UHF Reader Connected");
            }
            else
            {

                //SYSTEM FAILURE
                m_SystemFailure_ReadersWIFIDBConn = true;
                UpdateStatusBar("UHF Reader Failure");

                connectToolStripMenuItem1.Text = "Connect";
                uhfReaderConnected = false;
                //clsCrossThreadCalls.SetTextProperty(stripRdStartStop, "Connect");
                UpdateStatusBar("UHF Reader Not Connected");
            }

            #endregion

            //mReaderController.
            AdminMenuStatusLightChange_UHFReaderStatus(zRet);//IN JL 29-MAY-2016

            return zRet;
        }

        //IN JL 27-MAY-2016
        void m_UHFReader_OndelegateReaderConnectionChanged(bool zReaderConnected)
        {
            try
            {
                if (!zReaderConnected)//Connected ->disconnected
                {
                    //stack light and message
                    m_SystemFailure_ReadersWIFIDBConn = true;

                    //update message
                    uhfReaderConnected = false;
                    UpdateStatusBar("UHF Reader Lost Connection");
                }
                else   //Disconnected -> Connected
                {
                    m_SystemFailure_ReadersWIFIDBConn = false;
                    uhfReaderConnected = true;
                    UpdateStatusBar("UHF Reader Connected");
                }
            }
            catch (Exception ex)
            {
                //Log it
                db_LoggerIt(SHS_LogType.ERROR,
                          ex.ToString(),
                           "OndelegateReaderConnectionChanged",
                           m_HumpyDetail.Sys_HumpyId);//IN JL 27-MAY-2016
            }
        }

        void m_UHFReader_OndelegateGPIChanged(string aPortNum, string aStatus)
        {
            //IN JL 25-APR-2015: Link GPI4 directly to GPO4
            if (aPortNum.ToString() == "4")
            {
                if (aStatus.ToUpper() == "TRUE")
                {
                    GUI_StackLightStatus_Crane(m_Color_Red);
                }
                else if (aStatus.ToUpper() == "FALSE")
                {
                    GUI_StackLightStatus_Crane(m_Color_Green);
                }
            }
            if (aPortNum.ToString() == "2")
            {
                if (aStatus.ToUpper() == "TRUE")
                {
                    if (disableGpiMonitoring == false)
                    {gpiTwoHasSignal = true;}
                    else
                    { gpiTwoHasSignal = false; }
                    textBox1.Text = "GPI 2 : ON";
                    
                }
                else if (aStatus.ToUpper() == "FALSE")
                {
                    gpiTwoHasSignal=false;
                    textBox1.Text = "GPI 2 : OFF";
                }
            }
        }
        private bool Stop_RFIDReader_DIRECT()
        {
            bool zRet = false;

            try
            {
                if (m_UHFReader.m_ReaderIsConnected)
                {
                    m_UHFReader.Dispose();
                    uhfReaderConnected = true;
                    UpdateStatusBar("UHF Reader Disconnected");
                    zRet = true;
                }
                else
                {
                    m_UHFReader = null;
                    m_Logger.AddErrors("UHF Reader Disconnected", "Reader is not Connected");
                    UpdateStatusBar("UHF Reader Disconnected (not connected)");
                    zRet = true;
                    uhfReaderConnected = false;
                }
            }
            catch (Exception ex)
            {
                m_Logger.AddErrors("UHF Reader Disconnected", "Error:" + ex.ToString());
                zRet = false;
                uhfReaderConnected = false;
            }

            return zRet;
        }

        private void GPIOStackLight(LightStackColor aLightColor)
        {
            StacklightInfo sli_gpo_port1 = new StacklightInfo(aLightColor);
            // Queue the task and data.
            ThreadPool.QueueUserWorkItem(new WaitCallback(SetGPO), sli_gpo_port1);
        }

        private void SetGPO(object stateInfo)
        {
            //bool zRet = false;
            try
            {

                StacklightInfo sli = (StacklightInfo)stateInfo;
                LightStackColor zColor = sli.LightColor;

                if (m_UHFReader != null)
                {
                    if (m_UHFReader.m_ReaderIsConnected)
                    {
                        if (m_UHFReader.m_Reader != null)
                        {
                            m_UHFReader.m_Reader.SetReaderGPO(zColor);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateStatusBar("GPO Control Error");
                //MessageBox.Show("Error:" + ex.ToString(), "GPO");
                m_Logger.AddErrors("Reader GPO", ex.ToString());
            }
            finally
            {

            }
            //return zRet;
        }

        void m_UHFReader_OndelegateTagReported(SHSReadResult ReadRet, bool InsertRet)
        {
            if (!InsertRet)//IN JL 28-MAY-2016
            {
                //SYSTEM FAILURE - JL: tag inserting error -- network lagging or any other issue
                m_SystemFailure_ReadersWIFIDBConn = true;
                hfReaderConnected = false;
                UpdateStatusBar("DB Connection Failure");

            }
            //else
            //    hfReaderConnected = true;

            //JL: this portion has been moved into the Reader Class
            //throw new NotImplementedException();

            //try
            //{
            //    m_DAL.SHS_InsertInputsRecord(ReadRet.EPC,
            //                                 ReadRet.PeakRSSI.ToString(),
            //                                 ReadRet.AntennaId.ToString(),
            //                                 ReadRet.LocId);
            //}
            //catch (Exception ex)
            //{
            //    m_Logger.AddErrors("UHF Read Result Insert Fail", ex.ToString());
            //    //Control the Reader GPIO port

            //    //Control the local light
            //    StackLightStatus(Color.Yellow);
            //}
        }
        #endregion

        #region GUI Tools: Status bar update, etc
        private void UpdateStatusBar(string message)
        {
            //...
            //string[] lines = Regex.Split(message, "\r\n");
            //int lineCount = lines.Count();

            if (statusStrip.InvokeRequired)
            {
                statusStrip.Invoke(
                       new MethodInvoker(() => lblStatus.Text = "Status:" + message));
            }
            else
            {
                lblStatus.Text = "Status:" + message;
                statusStrip.Refresh();
            }
        }


        private void GUI_StackLightStatus_Humpy(Color aColor)
        {
            try
            {
                clsCrossThreadCalls.SetAnyProperty(btnRound_StackLight, "BackColor", aColor);

                //picLight.BackgroundImage = SmartHumpyController.Properties.Resources.green_light;
                //picLight.BackgroundImage = SmartHumpyController.Properties.Resources.red_light;
                //picLight.BackgroundImage = SmartHumpyController.Properties.Resources.yellow_light;

                //OUT JL 16-MAY-16
                //if(aColor == m_Color_Green)
                //    clsCrossThreadCalls.SetAnyProperty(picHumpyLight, "BackgroundImage", SmartHumpyController.Properties.Resources.green_light);
                //else if (aColor == m_Color_Yellow)
                //    clsCrossThreadCalls.SetAnyProperty(picHumpyLight, "BackgroundImage", SmartHumpyController.Properties.Resources.yellow_light);
                //else if (aColor == m_Color_Red)
                //    clsCrossThreadCalls.SetAnyProperty(picHumpyLight, "BackgroundImage", SmartHumpyController.Properties.Resources.red_light);

                //IN JL 16-MAY-16
                if (aColor == m_Color_Green)
                    clsCrossThreadCalls.SetAnyProperty(picHumpyLight, "BackgroundImage", SmartHumpyController.Properties.Resources.tick);
                else if (aColor == m_Color_Yellow)
                    clsCrossThreadCalls.SetAnyProperty(picHumpyLight, "BackgroundImage", SmartHumpyController.Properties.Resources.exclamation);
                else if (aColor == m_Color_Red)
                    clsCrossThreadCalls.SetAnyProperty(picHumpyLight, "BackgroundImage", SmartHumpyController.Properties.Resources.x);

                //clsCrossThreadCalls.SetAnyProperty(splitContainer2.Panel1, "BackColor", aColor);
                //splitContainer2.Panel2.BackColor = aColor;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void GUI_StackLightStatus_Crane(Color aColor)
        {
            try
            {

                //IN JL 16-MAY-16
                if (aColor == m_Color_Green)
                    clsCrossThreadCalls.SetAnyProperty(picCraneLight, "BackgroundImage", SmartHumpyController.Properties.Resources.tick);
                else if (aColor == m_Color_Yellow)
                    clsCrossThreadCalls.SetAnyProperty(picCraneLight, "BackgroundImage", SmartHumpyController.Properties.Resources.exclamation);
                else if (aColor == m_Color_Red)
                    clsCrossThreadCalls.SetAnyProperty(picCraneLight, "BackgroundImage", SmartHumpyController.Properties.Resources.x);

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        #endregion

        private void OnUpdateTagListTimer(object AObject)
        {
            try
            {
                //Make relative grid view visible....
                GridVisiableCOnfigurationBasedOnDetailMode();

                #region GridControl2->Checked In List Data Retrieving -- JL 16-MAY-16:Disabled
                //OUT JL 16-MAY-16
                //try
                //{
                //    DataSet ds = m_DAL_CheckedInWorker.SHS_GetWorkersAll(SmartHumpyController.Properties.Settings.Default.Sys_HumpyGroupName);
                //    if (ds != null)
                //    {
                //        if (ds.Tables.Count > 0)
                //        {
                //            UpdateGridControl2_CheckedInWorkers(ds.Tables[0]);
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    db_LoggerIt(SHS_LogType.EXCEPTION,
                //                ex.ToString(),
                //                "OnUpdateTagListTimer-Checked In List",
                //                SmartHumpyController.Properties.Settings.Default.Sys_HumpyId);//IN JL 12-MAY-2015

                //    //OUT JL 12-MAY-2015 
                //    //db_LoggerIt(SHS_LogType.EXCEPTION, "DAL,HF,UHF,TIMER,GRID Initilaization Finished", m_CurrentHumpyOwner, SmartHumpyController.Properties.Settings.Default.Sys_HumpyId);

                //    ex.ToString();
                //}
                #endregion

                #region GridControl1->Detail List Data Retrieving
                try
                {
                    //DataSet ds = m_DAL_Retriever.SHS_GetWorkersAll(SmartHumpyController.Properties.Settings.Default.Sys_TagReadSensitivity_ms,
                    //                                                SmartHumpyController.Properties.Settings.Default.Sys_TagINsensitivity_ms);


                    DataSet ds = m_DAL_Retriever.SHS_GetWorkersAll(m_HumpyDetail.Sys_TagReadSensitivity_ms,
                                                                   m_HumpyDetail.Sys_TagINsensitivity_ms);

                    //Workers workersDetails = new Workers();
                    //workersDetails.GetAllWorkers(ConfigurationManager.ConnectionStrings["SmartHumpyController.Properties.Settings.Sys_DBConnectionLocal"].ToString(),
                    //                           m_HumpyDetail.Sys_TagReadSensitivity_ms, m_HumpyDetail.Sys_TagINsensitivity_ms);

                    //DataTable checkInWokers = new DataTable();
                    //DataTable notCheckInWorkers = new DataTable();


                    //checkInWokers = workersDetails.GetWorkersInDetails("CheckInWorkers.csv", ConfigurationManager.ConnectionStrings["SmartHumpyController.Properties.Settings.Sys_DBConnectionLocal"].ToString());
                    //notCheckInWorkers = workersDetails.GetWorkersInDetails("NotCheckInWorkers.csv", ConfigurationManager.ConnectionStrings["SmartHumpyController.Properties.Settings.Sys_DBConnectionLocal"].ToString());

                    //UpdateGridControl(checkInWokers, notCheckInWorkers);

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            UpdateGridControl(ds.Tables[2], ds.Tables[3]);//Updated 25-MAY-2016 JL
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Log it
                    db_LoggerIt(SHS_LogType.ERROR,
                              ex.ToString(),
                               "OnUpdateTagListTimer-Detail List",
                               m_HumpyDetail.Sys_HumpyId);//IN JL 12-MAY-2015

                    //OUT JL 12-MAY-2015 
                    //db_LoggerIt(SHS_JournalType.GETDBDATA, ex.ToString(), "OnUpdateTagListTimer-Detail List");
                    ex.ToString();
                }

                UpdateWarningLights_GPOGUI();
                //Check system error flag constantly.. any rd failure/db connection.
                UpdateSystemError();
                #endregion


            }
            catch (Exception ex)
            {
                ex.ToString();
                //throw ex;
            }
            finally
            {
                if (m_UpdateTagListTimer != null)
                    m_UpdateTagListTimer.Change(m_RetrieveTimeInterval_ms, System.Threading.Timeout.Infinite);
            }
        }

        #region DATA GRID (gridView_CheckIn) controls
        private void UpdateGridControl2_CheckedInWorkers(DataTable dt)
        {
            this.gridControl2.BeginInvoke
            (
                new MethodInvoker
                (
                    delegate
                    {

                        gridControl2.BeginUpdate();
                        gridControl2.DataSource = null;
                        gridControl2.DataSource = dt.DefaultView;
                        gridControl2.EndUpdate();
                    }
                )
            );
        }
        #endregion

        #region DATA GRID (gridView1) controls
        //updated 25-MAY-2016
        private void UpdateGridControl(DataTable dtCheckIn, DataTable dtNotCheckedIn)//Detailed Worker list with IN/OUT/NOT CHECKED IN STATUS
        {
            this.gridControl1.BeginInvoke
            (
                new MethodInvoker
                (
                    delegate
                    {
                        gridControl1.BeginUpdate();
                        gridControl1.DataSource = null;
                        gridControl1.DataSource = dtCheckIn.DefaultView;
                        gridControl1.EndUpdate();
                    }
                )
            );

            this.gridControl3.BeginInvoke
            (
                 new MethodInvoker
                 (
                     delegate
                     {
                         gridControl3.BeginUpdate();
                         gridControl3.DataSource = null;
                         gridControl3.DataSource = dtNotCheckedIn.DefaultView;
                         gridControl3.EndUpdate();
                     }
                 )
             );
        }


        //updated 28-MAY-2016
        private void ResetGridControl()//Detailed Worker list with IN/OUT/NOT CHECKED IN STATUS
        {
            this.gridControl1.BeginInvoke
            (
                new MethodInvoker
                (
                    delegate
                    {
                        gridControl1.BeginUpdate();
                        gridControl1.DataSource = null;
                        gridControl1.EndUpdate();
                    }
                )
            );

            this.gridControl3.BeginInvoke
            (
                 new MethodInvoker
                 (
                     delegate
                     {
                         gridControl3.BeginUpdate();
                         gridControl3.DataSource = null;
                         gridControl3.EndUpdate();
                     }
                 )
             );
        }

        private void GetCurrenGridViewRowData(string ColumnFiledName, string Value)
        {
            int rowHandle = GetRowHandleByColumnValue(gridView1, ColumnFiledName, Value);
            if (rowHandle != GridControl.InvalidRowHandle)
            {
                object value = gridView1.GetRow(rowHandle);

                //gridView1.FocusedColumn = gridView1.Columns.ColumnByFieldName(ColumnFiledName);
                //gridView1.FocusedRowHandle = rowHandle;
                //gridView1.ShowEditor();
            }
        }

        private bool SearchCurrentGridView_CheckInList(string ColumnFiledName, string Value)//IN JL 25-MAY-16 
        {
            bool zRet = false;
            int rowHandle = GetRowHandleByColumnValue(gridView1, ColumnFiledName, Value);
            if (rowHandle != GridControl.InvalidRowHandle)
            {
                //gridView1.FocusedColumn = gridView1.Columns.ColumnByFieldName(ColumnFiledName);
                //gridView1.FocusedRowHandle = rowHandle;
                //gridView1.ShowEditor();
                zRet = true;
            }

            return zRet;
        }

        private bool SearchCurrentGridView_NotCheckInList(string ColumnFiledName, string Value)
        {
            bool zRet = false;
            int rowHandle = GetRowHandleByColumnValue(gridView3, ColumnFiledName, Value.Trim()); //IN JL 25-MAY-16 
            if (rowHandle != GridControl.InvalidRowHandle)
            {
                //gridView1.FocusedColumn = gridView1.Columns.ColumnByFieldName(ColumnFiledName);
                //gridView1.FocusedRowHandle = rowHandle;
                //gridView1.ShowEditor();
                zRet = true;
            }

            return zRet;
        }


        private int GetRowHandleByColumnValue(GridView view, string ColumnFieldName, object value)
        {
            int result = GridControl.InvalidRowHandle;
            for (int i = 0; i < view.RowCount; i++)
                if (view.GetDataRow(i)[ColumnFieldName].ToString().Trim().Equals(value.ToString().Trim()))
                    return i;
            return result;
        }

        private object GetRowColumnDataByRowHandle(GridView view, int aRowHandle, string ColumnFieldName)
        {
            object retValue = "";
            int result = GridControl.InvalidRowHandle;

            if (aRowHandle != result)
            {
                retValue = view.GetDataRow(aRowHandle)[ColumnFieldName];
            }

            return retValue;
        }

        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string zINOUTStatus = View.GetRowCellDisplayText(e.RowHandle, View.Columns[SHS_DbReturnColumnNames.Column8]);//IN OUT
                //OUT JL 25-MAY-16 object zWorkerName = GetRowColumnDataByRowHandle(gridView1, e.RowHandle, SHS_DbReturnColumnNames.Column1);//Name

                if (zINOUTStatus == SHS_INOUT_Values.INValue)
                {
                    e.Appearance.BackColor = m_Color_Green;//Color.Green;
                    //e.Appearance.BackColor2 = Color.ForestGreen;
                    //db_LoggerIt(SHS_JournalType.IN, "CheckedIn Worker:" + zWorkerName + " is in", m_CurrentHumpyOwner);
                }
                else if (zINOUTStatus == SHS_INOUT_Values.OUTValue)
                {
                    e.Appearance.BackColor = m_Color_Red;//Color.Salmon;
                    //e.Appearance.BackColor2 = Color.SeaShell;
                    //db_LoggerIt(SHS_JournalType.OUT, "CheckedIn Worker:" + zWorkerName + " is out", m_CurrentHumpyOwner);
                }
                else if (zINOUTStatus == SHS_INOUT_Values.NotCheckedInValue)
                {
                    //e.Appearance.ForeColor = Color.Yellow;
                    e.Appearance.BackColor = m_Color_Yellow;//Color.Yellow;
                    //e.Appearance.BackColor2 = Color.SeaShell;
                    //db_LoggerIt(SHS_JournalType.IN, "UnCheckedIn Worker:" + zWorkerName + " is in", m_CurrentHumpyOwner);
                }

            }
            //View.RefreshRow(e.RowHandle);
        }

        //1b. To hide the focused row indicator icon you can handle the gridView's CustomDrawRowIndicator event as demonstrated below:
        private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.ImageIndex = -1;
        }

        private void gridView1_CustomColumnGroup(object sender, CustomColumnSortEventArgs e)
        {

        }

        private void gridView1_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {

        }
        #endregion

        #region Admin buttons

        #region Tool Menu Bar Controls

        private void resetSystemErrorFlagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_SystemFailure_ReadersWIFIDBConn = false;
        }

        private void stripRdStartStop_Click(object sender, EventArgs e)
        {
            if (connectToolStripMenuItem1.Text == "Disconnect")
            {
                if (Stop_RFIDReader_DIRECT())
                {
                    connectToolStripMenuItem1.Text = "Connect";
                    startToolStripMenuItem.Enabled = false;
                    stopToolStripMenuItem.Enabled = false;
                    //clsCrossThreadCalls.SetTextProperty(btnBoxing_Connect, "Connect");
                    UpdateStatusBar("UHF Reader Disconnected");
                }
            }
            else if (connectToolStripMenuItem1.Text == "Connect")
            {
                //Connect Reader
                if (Initilaization_RFIDReader_DIRECT("", m_HumpyDetail))
                {
                    connectToolStripMenuItem1.Text = "Disconnect";
                    startToolStripMenuItem.Enabled = true;
                    stopToolStripMenuItem.Enabled = false;
                    //clsCrossThreadCalls.SetTextProperty(btnBoxing_Connect, "Disconnect");
                    UpdateStatusBar("UHF Reader Connected");
                }
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (connectToolStripMenuItem.Text == "Disconnect")
            {
                if (Stop_HFReader())
                {
                    connectToolStripMenuItem.Text = "Connect";
                    UpdateStatusBar("HF Reader Disconnected");
                }
            }
            else if (connectToolStripMenuItem.Text == "Connect")
            {
                //Connect Reader
                if (Initialization_HFReader(m_HumpyDetail))
                {
                    connectToolStripMenuItem.Text = "Disconnect";
                    UpdateStatusBar("HF Reader Connected");
                }
            }
        }
        #endregion

        private void sTARTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_UHFReader.m_ReaderIsConnected)
            {
                m_UHFReader.Start();
                startToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = true;
            }
        }

        private void sTOPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_UHFReader.m_ReaderIsConnected)
            {
                m_UHFReader.Stop();
                startToolStripMenuItem.Enabled = true;
                stopToolStripMenuItem.Enabled = false;
            }
        }

        private void btnIgnoreNonCheckedIn_Click(object sender, EventArgs e)
        {
            if (btnIgnoreNonCheckedIn.Text == "Ignore None Checked In Worker (off) -TURN ON")
            {
                m_IgnoreNoneCheckedInWorker = false;
                btnIgnoreNonCheckedIn.Text = "Ignore None Checked In Worker (on) -TURN OFF";
            }
            else if (btnIgnoreNonCheckedIn.Text == "Ignore None Checked In Worker (on) -TURN OFF")
            {
                m_IgnoreNoneCheckedInWorker = true;
                btnIgnoreNonCheckedIn.Text = "Ignore None Checked In Worker (off) -TURN ON";
            }
        }

        private void clearInputsTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_DAL.SHS_Clear_InputsTable();
        }

        private void clearHistoryTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_DAL.SHS_Clear_History();
        }

        private void setEveryoneCheckedIn_Click(object sender, EventArgs e)
        {
            m_DAL.SHS_Update_Personnel_AllCheckIn();
        }

        private void sETPMRAllToCOLLECTEDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_DAL.SHS_Update_Personnel_AllCheckOut();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //IN JL 27-MAY-2016: Shut down the application.
            GPIOStackLight(LightStackColor.Off);
            //IN JL 16-MAY-16
            //Dispose the HF reader properly
            Stop_RFIDReader_DIRECT();
            //Dispose the UHF reader properly.
            Stop_HFReader();
            //Dispose all the DALs.
            Disposal_DAL();



            this.Close();
        }

        private void DisposeForReStart()//IN JL 28-MAY-2016
        {
            //IN JL 27-MAY-2016: Shut down the application.
            GPIOStackLight(LightStackColor.Off);
            //IN JL 16-MAY-16
            //Dispose the HF reader properly
            Stop_RFIDReader_DIRECT();
            //Dispose the UHF reader properly.
            Stop_HFReader();
            //Dispose all the retrievers
            Disposal_TimerRetrievers();
            //Dispose all the DALs.
            Disposal_DAL();
        }

        private void Disposal_TimerRetrievers()
        {
            if (m_UpdateTagListTimer != null)
                m_UpdateTagListTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

            m_UpdateTagListTimer = null;
            m_Timer_FullStatusReport = null;
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateStatusBar("");
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAboutISM zFrmAbout = new FrmAboutISM();
            zFrmAbout.ShowDialog();
        }
        #endregion

        private void btnCheckedInListOrDetailList_Click(object sender, EventArgs e)
        {
            if (m_DetailMode == SHS_DetailMode.CheckedInList)
            {
                m_DetailMode = SHS_DetailMode.DetailList;
                //btnCheckedInListOrDetailList.Text = "Press this button to go to detail List";
                clsCrossThreadCalls.SetTextProperty(btnCheckedInListOrDetailList, "BACK TO CHECKED-IN REPORT");

                //Trigger the timer
                DetailReportThread();//IN JL 17-DEC-15 v1.0.1.1
            }
            else if (m_DetailMode == SHS_DetailMode.DetailList)
            {
                m_DetailMode = SHS_DetailMode.CheckedInList;
                //btnCheckedInListOrDetailList.Text = "Press this button to go to checked-in List";
                clsCrossThreadCalls.SetTextProperty(btnCheckedInListOrDetailList, "SEE FULL STATUS REPORT");
            }

            GridVisiableCOnfigurationBasedOnDetailMode();
        }

        void Thread_FullStatusReport()
        {
            if (m_Timer_FullStatusReport != null)
                m_Timer_FullStatusReport.Change(m_FullStatusReportTimeInterval_ms, System.Threading.Timeout.Infinite);
        }

        private void OnFullStatusReportTimer(object AObject)
        {
            try
            {
                m_DetailMode = SHS_DetailMode.CheckedInList;
                //btnCheckedInListOrDetailList.Text = "Press this button to go to checked-in List";
                clsCrossThreadCalls.SetTextProperty(btnCheckedInListOrDetailList, "SEE FULL STATUS REPORT");
            }
            catch (Exception ex)
            {
                ex.ToString();
                //throw ex;
            }
            finally
            {

            }
        }

        private void DetailReportThread()
        {
            try
            {
                //If it is already running, stop it.
                if (m_FullStatusReportThread != null)
                {
                    if (m_FullStatusReportThread.IsAlive)
                        m_FullStatusReportThread.Abort();

                    m_FullStatusReportThread = null;
                }

                m_FullStatusReportThread = new Thread(Thread_FullStatusReport);
                m_FullStatusReportThread.IsBackground = true;
                m_FullStatusReportThread.Start();
                //Thread.Sleep(SmartHumpyController.Properties.Settings.Default.CheckInOutWaitingTime_ms);
                //if (m_CheckInOutThread.IsAlive)
                //{
                //    m_CheckInOutThread.Abort();
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        //Modify the border thickness
        private void gridView1_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            //var cellBounds = ((DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo)e.Cell).Bounds;
            //DrawCellBorder(e.Graphics, Brushes.Red, cellBounds, 1);

            //OUT JL 25-MAY-2016 doesn't work the following code
            //if (e.Column.FieldName == SHS_DbReturnColumnNames.Column8)
            //{
            //    GridCellInfo gci = e.Cell as GridCellInfo;
            //    (gci.ViewInfo as TextEditViewInfo).ContextImage = imgSmallImageCollection.Images[6];
            //}
        }

        //Modify the border thickness
        private void gridView_CheckedIn_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            //var cellBounds = ((DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo)e.Cell).Bounds;
            //DrawCellBorder(e.Graphics, Brushes.Red, cellBounds, 1);
        }

        void DrawCellBorder(Graphics g, Brush borderBrush, Rectangle cellBounds, int borderThickness)
        {
            Rectangle innerRect = Rectangle.Inflate(cellBounds, -borderThickness, -borderThickness);
            g.ExcludeClip(innerRect);
            g.FillRectangle(borderBrush, cellBounds);
        }

        private void spliterBorderToggleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (spliterDetailList.BorderStyle != BorderStyle.None)
            {
                SetSplitBoarder(BorderStyle.None);
            }
            else
            {
                SetSplitBoarder(BorderStyle.Fixed3D);
            }
        }

        private void SetSplitBoarder(BorderStyle zbs)
        {
            int zWidth = 1;
            if (zbs == BorderStyle.None)
                zWidth = 1;
            else
                zWidth = 5;

            clsCrossThreadCalls.SetAnyProperty(splitContainer_CheckInOut, "BorderStyle", zbs);
            clsCrossThreadCalls.SetAnyProperty(splitContainer_CheckInOut, "SplitterWidth", zWidth);

            clsCrossThreadCalls.SetAnyProperty(splitContainer1, "BorderStyle", zbs);
            clsCrossThreadCalls.SetAnyProperty(splitContainer1, "SplitterWidth", zWidth);

            clsCrossThreadCalls.SetAnyProperty(splitContainer2, "BorderStyle", zbs);
            clsCrossThreadCalls.SetAnyProperty(splitContainer2, "SplitterWidth", zWidth);

            clsCrossThreadCalls.SetAnyProperty(splitContainer3_Display, "BorderStyle", zbs);
            clsCrossThreadCalls.SetAnyProperty(splitContainer3_Display, "SplitterWidth", zWidth);

            clsCrossThreadCalls.SetAnyProperty(spliterDetailList, "BorderStyle", zbs);
            clsCrossThreadCalls.SetAnyProperty(spliterDetailList, "SplitterWidth", zWidth);


            clsCrossThreadCalls.SetAnyProperty(splitContainer_Lights, "BorderStyle", zbs);//IN JL 16-MAY-16
            clsCrossThreadCalls.SetAnyProperty(splitContainer_Lights, "SplitterWidth", zWidth);

            clsCrossThreadCalls.SetAnyProperty(splitContainer_CheckedInListNotCheckedInList, "BorderStyle", zbs);//IN JL 25-MAY-16
            clsCrossThreadCalls.SetAnyProperty(splitContainer_CheckedInListNotCheckedInList, "SplitterWidth", zWidth);
        }

        private void gridView_CheckedIn_RowCellDefaultAlignment(object sender, RowCellAlignmentEventArgs e)
        {
            e.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        private void tmrCurrDt_Tick(object sender, EventArgs e)
        {
            clsCrossThreadCalls.SetTextProperty(lblCurrDt, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private void picLight_Click(object sender, EventArgs e)
        {


            if (menuStrip1.Visible)
            {
                clsCrossThreadCalls.SetAnyProperty(this, "FormBorderStyle", FormBorderStyle.None);
                clsCrossThreadCalls.SetAnyProperty(menuStrip1, "Visible", false);
            }
            else
            {
                FrmAdmin zFrmAdmin = new FrmAdmin(m_HumpyDetail);
                zFrmAdmin.ShowDialog();

                if (zFrmAdmin.zdr == System.Windows.Forms.DialogResult.OK)
                {
                    clsCrossThreadCalls.SetAnyProperty(this, "FormBorderStyle", FormBorderStyle.Sizable);
                    clsCrossThreadCalls.SetAnyProperty(menuStrip1, "Visible", true);
                }
            }

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblCurrDt_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer_CheckInOut_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //IN JL 16-MAY-16
        private void craneLightTriggerToggleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_UHFReader.TriggerGPIManually("4", "TRUE");//Raising edge
            
        }
        //IN JL 16-MAY-16
        private void craneLightTriggerToggleRaisingEdgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_UHFReader.TriggerGPIManually("4", "FALSE");//Raising edge
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        //IN JL 28-MAY-2016: Hide the grid menu contains the filter, etc.
        private void gridView1_ShowGridMenu(object sender, GridMenuEventArgs e)
        {
            if (e.MenuType == GridMenuType.Column)
            {
                e.Allow = false;
                //// Customize
                //DXMenuItem miCustomize = GetItemByStringId(e.Menu, GridStringId.MenuColumnColumnCustomization);
                //if (miCustomize != null)
                //    miCustomize.Visible = false;

                //e.Menu.Visible = false;

                //// Group By This Column
                //DXMenuItem miGroup = GetItemByStringId(e.Menu, GridStringId.MenuColumnGroup);
                //if (miGroup != null)
                //{
                //    miGroup.Visible = false;
                //    //miGroup.Enabled = false;
                //}

            }
        }
        //IN JL 28-MAY-2016: Hide the grid menu contains the filter, etc.
        private void gridView3_ShowGridMenu(object sender, GridMenuEventArgs e)
        {
            if (e.MenuType == GridMenuType.Column)
            {
                e.Allow = false;
                //// Customize
                //DXMenuItem miCustomize = GetItemByStringId(e.Menu, GridStringId.MenuColumnColumnCustomization);
                //if (miCustomize != null)
                //    miCustomize.Visible = false;

                //// Group By This Column
                //DXMenuItem miGroup = GetItemByStringId(e.Menu, GridStringId.MenuColumnGroup);
                //if (miGroup != null)
                //    miGroup.Enabled = false;

            }
        }
        private DXMenuItem GetItemByStringId(DXPopupMenu menu, GridStringId id)
        {
            foreach (DXMenuItem item in menu.Items)
                if (item.Caption == GridLocalizer.Active.GetLocalizedString(id))
                    return item;
            return null;
        }

        private void timExtract_Tick(object sender, EventArgs e)
        {
            HQDataExtract dataExtract = new HQDataExtract();
            string systemHumpyID = "";
            string systemHumpyGroupName = "";

            systemHumpyID = m_HumpyDetail.Sys_HumpyId;
            systemHumpyGroupName = m_HumpyDetail.Sys_HumpyGroupName;
            dataExtract.UploadPersonnelSyncDataToHummpyDB(ConfigurationManager.ConnectionStrings["SmartHumpyController.Properties.Settings.Sys_DBConnectionLocal"].ToString(), systemHumpyID, systemHumpyGroupName);
        }

        private void button1_Click(object sender, EventArgs e)
        {



        }

        private void timConnection_Tick(object sender, EventArgs e)
        {
            connectionMonitorTimer += 1;

            if (connectionMonitorTimer % 10 == 0)
            {
                ConfigurationManagement configManager = new ConfigurationManagement();
                ConfigurationManagement ipChecker = new ConfigurationManagement();
                SerialPort hfPort = new SerialPort(hfComPort);

                string connectionString = "";
                string testConnectionStringLocal = "";
                string testConnectionStringMaster = "";
                bool ipPingable = false;
                bool hasError = false;

                if (uhfReaderConnected == false)
                {
                    insertApplicationLogs.InsertApplicationLogs(masterHumpyConnectionString,
                                            insertApplicationLogs.AppLogs_ComponentHumpyValue,
                                            "UHF",
                                            insertApplicationLogs.AppLogs_StatusErrorValue,
                                            "",
                                            "",
                                            humpyLocationID,
                                            wifiIPAddress);

                    m_UHFReader.m_Reader.SetReaderGPO_SinglePort(4, false);
                    m_SoftwareHeartBeat = null;

                    timConnection.Enabled = false;
                    timReconnect.Enabled = true;
                    timExtract.Enabled = false;
                    uhfReaderDisconnected = true;
                    disableGpiMonitoring = true;
                    if (humpyMode != null)
                        humpyMode.Dispose();
                    conRemind = new ConnectionReminder("UHF", "RFID Reader disconnected.\n Checking RFID connection...");
                    conRemind.ShowDialog(this);
                    return;
                }

                try
                {
                    hfPort.Open();
                    hfReaderConnected = true;
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("denied") > -1)
                        hfReaderConnected = true;
                    if (ex.Message.IndexOf("exist") > -1)
                        hfReaderConnected = false;
                }

                hfPort = null;
                if (hfReaderConnected == false)
                {
                    insertApplicationLogs.InsertApplicationLogs(masterHumpyConnectionString,
                                            insertApplicationLogs.AppLogs_ComponentHumpyValue,
                                            "HF",
                                            insertApplicationLogs.AppLogs_StatusErrorValue,
                                            "",
                                            "",
                                            humpyLocationID,
                                            wifiIPAddress);

                    m_UHFReader.m_Reader.SetReaderGPO_SinglePort(4, false);
                    m_SoftwareHeartBeat = null;

                    timConnection.Enabled = false;
                    timReconnect.Enabled = true;
                    timExtract.Enabled = false;
                    hfReaderDisconnected = true;
                    disableGpiMonitoring = true;
                    if (humpyMode != null)
                        humpyMode.Dispose();

                    conRemind = new ConnectionReminder("HF", "Card Reader disconnected.\n Checking Card Reader connection...");
                    conRemind.ShowDialog(this);
                    return;

                }

                connectionString = masterHumpyConnectionString;
                testConnectionStringMaster = configManager.CheckDatabaseConnectionString("", "", "", "", "", connectionString);

                if (testConnectionStringMaster.Trim().Length == 0)
                {
                    m_UHFReader.m_Reader.SetReaderGPO_SinglePort(4, false);
                    m_SoftwareHeartBeat = null;
                    hasError = true;

                    insertApplicationLogs.CreateApplicationLogsNew(wifiIPAddress, sharedFolderIP, sanShareFolder,
                         insertApplicationLogs.AppLogs_ComponentHumpyValue,
                         "Master DB",
                         insertApplicationLogs.AppLogs_StatusErrorValue,
                         "",
                         "",
                         humpyLocationID,
                         wifiIPAddress);

                    timConnection.Enabled = false;
                    timReconnect.Enabled = true;
                    masterDBDisconnected = true;
                    timExtract.Enabled = false;
                    disableGpiMonitoring = true;
                    if (humpyMode != null)
                        humpyMode.Dispose();

                    conRemind = new ConnectionReminder("MDB", "Connection lost to Master database.\n Reconnecting to the database...");
                    conRemind.ShowDialog(this);
                    return;
                }

                connectionString = ConfigurationManager.ConnectionStrings["SmartHumpyController.Properties.Settings.Sys_DBConnectionLocal"].ToString();
                testConnectionStringLocal = configManager.CheckDatabaseConnectionString("", "", "", "", "", connectionString);

                if (testConnectionStringLocal.Trim().Length == 0)
                {
                    m_UHFReader.m_Reader.SetReaderGPO_SinglePort(4, false);
                    m_SoftwareHeartBeat = null;
                    hasError = true;
                    insertApplicationLogs.CreateApplicationLogsNew(wifiIPAddress, sharedFolderIP, sanShareFolder,
                    insertApplicationLogs.AppLogs_ComponentHumpyValue,
                    "Local DB",
                    insertApplicationLogs.AppLogs_StatusErrorValue,
                    "",
                    "",
                    humpyLocationID,
                    wifiIPAddress);

                    timConnection.Enabled = false;
                    timReconnect.Enabled = true;
                    localDBDisconnected = true;
                    timExtract.Enabled = false;
                    disableGpiMonitoring = true;
                    if (humpyMode != null)
                        humpyMode.Dispose();

                    conRemind = new ConnectionReminder("DB", "Connection lost to Local database.\n Reconnecting to the database...");
                    conRemind.ShowDialog(this);
                    return;
                }

                //ipPingable = ipChecker.CheckIPIfPingable(sharedFolderIP);
                //if (!ipPingable)
                //{
                //    insertApplicationLogs.InsertApplicationLogs(masterHumpyConnectionString,
                //           insertApplicationLogs.AppLogs_ComponentHumpyValue,
                //           "Shared Folder",
                //           insertApplicationLogs.AppLogs_StatusErrorValue,
                //           "",
                //           "",
                //           humpyLocationID,
                //           wifiIPAddress);
                //    m_UHFReader.m_Reader.SetReaderGPO_SinglePort(4, false);
                //    m_SoftwareHeartBeat = null;

                //    timConnection.Enabled = false;
                //    timReconnect.Enabled = true;
                //    sharedFolderDisconnected = true;
                //    timExtract.Enabled = false;
                //    conRemind = new ConnectionReminder("SANIP", "Connection lost to Shared Drive.\n Reconnecting to the shared drive...");
                //    conRemind.ShowDialog(this);
                //    return;
                //}
                if (gpiTwoHasSignal && !disableGpiMonitoring && uhfReaderConnected && hfReaderConnected && hasError == false)
                {

                    //timConnection.Enabled = false;
                    if (humpyMode != null)
                        return;
                    humpyMode = new HumpyMode("Crane is not in RFID Mode",true);
                    timReconnect.Enabled = true;
                    humpyMode.ShowDialog();
                    if (humpyMode.DialogResult == DialogResult.OK)
                    {
                        disableGpiMonitoring = true;
                        timConnection.Enabled = true;
                        timReconnect.Enabled = false;
                        label7.Visible = true;
                        gpiTwoHasSignal = false;
                    }
                        
                    return;

                }

            }
        }

        private void uHFReaderStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (conRemind != null)
                conRemind.Dispose();

        }

        private void timReconnect_Tick(object sender, EventArgs e)
        {
            reconnectTimer += 1;

            if (reconnectTimer % 10 == 0)
            {
                if (localDBDisconnected)
                {
                    ConfigurationManagement configManager = new ConfigurationManagement();
                    string connectionString = "";
                    string testConnectionStringLocal = "";
                    connectionString = ConfigurationManager.ConnectionStrings["SmartHumpyController.Properties.Settings.Sys_DBConnectionLocal"].ToString();
                    testConnectionStringLocal = configManager.CheckDatabaseConnectionString("", "", "", "", "", connectionString);

                    if (testConnectionStringLocal.ToString().Length != 0)
                    {
                        timReconnect.Enabled = false;
                        timConnection.Enabled = true;
                        localDBDisconnected = false;
                        disableGpiMonitoring = false;
                        if (conRemind != null)
                        {
                            conRemind.Dispose();
                            conRemind = null;
                            conRemind = new ConnectionReminder("FMDB", "Master database connection established. \n Restarting the application.");
                            conRemind.ShowDialog();
                            return;
                        }
                    }

                }

                if (masterDBDisconnected)
                {
                    ConfigurationManagement configManager = new ConfigurationManagement();


                    string connectionString = "";
                    string testConnectionStringMaster = "";

                    connectionString = masterHumpyConnectionString;
                    testConnectionStringMaster = configManager.CheckDatabaseConnectionString("", "", "", "", "", connectionString);

                    if (testConnectionStringMaster.Trim().Length != 0)
                    {
                        timReconnect.Enabled = false;
                        timConnection.Enabled = true;
                        masterDBDisconnected = false;
                        disableGpiMonitoring = false;
                        if (conRemind != null)
                        {
                            conRemind.Dispose();
                            conRemind = null;
                            conRemind = new ConnectionReminder("FMDB", "Master database connection established. \n Restarting the application.");
                            conRemind.ShowDialog();
                            return;
                         }
                    }

                }
                if (sharedFolderDisconnected)
                {
                    ConfigurationManagement ipChecker = new ConfigurationManagement();
                    bool ipPingable = false;
                    ipPingable = ipChecker.CheckIPIfPingable(sharedFolderIP);

                    if (ipPingable)
                    {
                        timReconnect.Enabled = false;
                        timConnection.Enabled = true;
                        sharedFolderDisconnected = false;
                        disableGpiMonitoring = false;
                        if (conRemind != null)
                        {
                            conRemind.Dispose();
                            conRemind = null;
                            conRemind = new ConnectionReminder("FSANIP", "Shared drive connection established. \n Restarting the application."); 
                            conRemind.ShowDialog();
                            return;
                        }
                    }
                }
                if (uhfReaderDisconnected)
                {
                    ConfigurationManagement ipChecker = new ConfigurationManagement();
                    bool ipPingable = false;
                    ipPingable = ipChecker.CheckIPIfPingable(uhfReaderIPAddress);

                    if (uhfReaderConnected == true && ipPingable)
                    {
                        timReconnect.Enabled = false;
                        timConnection.Enabled = true;
                        uhfReaderDisconnected = false;
                        disableGpiMonitoring = false;
                        if (conRemind != null)
                        {
                            conRemind.Dispose();
                            conRemind = null;
                            conRemind = new ConnectionReminder("FUHF", "RFID Reader found. \n Restarting the application.");
                            conRemind.ShowDialog();
                            return;
                        }
                    }
                }
               
                if (hfReaderDisconnected)
                {
                    SerialPort hfPort = new SerialPort(hfComPort);
                    try
                     {
                        hfPort.Open();
                        hfReaderConnected = true;
                     }
                   catch (Exception ex)
                    {
                        if (ex.Message.IndexOf("denied") > -1)
                            hfReaderConnected = true;
                        if (ex.Message.IndexOf("exist") > -1)
                            hfReaderConnected = false;
                    }

                    if (hfReaderConnected == true)
                    {
                        timReconnect.Enabled = false;
                        timConnection.Enabled = true;
                        hfReaderDisconnected = false;
                        disableGpiMonitoring = false;
                        if (conRemind != null)
                        {
                            conRemind.Dispose();
                            conRemind = null;
                            conRemind = new ConnectionReminder("FHF", "Card Reader found. \n Restarting the application.");
                            conRemind.ShowDialog();
                            return;
                        }
                    }
                }
                if (!gpiTwoHasSignal && !disableGpiMonitoring )
                {

                    timConnection.Enabled = true;

                    if (humpyMode != null)
                    {
                        humpyMode.Dispose();
                        humpyMode = new HumpyMode("Starting RFID Operations", false);
                        timReconnect.Enabled = false;
                        timConnection.Enabled = true;
                        humpyMode.ShowDialog(); 
                        return;
                    }
             
                }



            }


        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //IN JL 27-MAY-2016: Shut down the application.
            GPIOStackLight(LightStackColor.Off);
            //IN JL 16-MAY-16
            //Dispose the HF reader properly
            Stop_RFIDReader_DIRECT();
            //Dispose the UHF reader properly.
            Stop_HFReader();
            //Dispose all the DALs.
            Disposal_DAL();

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
         

        }

        private void button1_Click_3(object sender, EventArgs e)
        {
          

    //        Reader.Disconnect();



            //Impinj.OctaneSdk.SpeedwayReader Reader = new Impinj.OctaneSdk.SpeedwayReader();
            //Reader.Co
        }

       
          

        }
    }  

