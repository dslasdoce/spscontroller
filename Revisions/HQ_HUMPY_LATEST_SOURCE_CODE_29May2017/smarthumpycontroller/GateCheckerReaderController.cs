using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using System.IO;
using System.Net.NetworkInformation;
using System.Drawing;
using System.Linq;
using System.Configuration;

using Impinj.OctaneSdk;

using AMMO_BG_DLL.Background;

namespace SmartHumpyController
{

    public class SHSReadResult
    {
        private string mEPC = "";
        public string EPC
        {
            get
            {
                return mEPC;
            }
            set
            {
                if (mEPC == value)
                    return;
                mEPC = value;
            }
        }
        private double mPeakRSSI = -70;
        public double PeakRSSI
        {
            get
            {
                return mPeakRSSI;
            }
            set
            {
                if (mPeakRSSI == value)
                    return;

                if(value>mPeakRSSI)
                   mPeakRSSI = value;
            }
        }
        private string mLocId = "NA";
        public string LocId
        {
            get
            {
                return mLocId;
            }
            set
            {
                if (mLocId == value)
                    return;
                mLocId = value;
            }
        }
        private int mCount = 0;
        public int Count
        {
            get
            {
                return mCount;
            }
            set
            {
                if (mCount == value)
                    return;
                mCount = value;
            }
        }
        private int mAntennaId = 0;
        public int AntennaId
        {
            get
            {
                return mAntennaId;
            }
            set
            {
                if (mAntennaId == value)
                    return;
                mAntennaId = value;
            }
        }

    }
  //public enum LightStackColor : ushort { RedOrange = 3, OrangeOnly = 2, GreenOnly = 4, RedOnly = 1 ,Off = 0};
  
  public class ReaderDirectController
  {

      private HumpyDAL m_DAL;

      public delegate void delegateGPIChanged(string aPortNum, string aStatus);
      public event delegateGPIChanged OndelegateGPIChanged;

      public delegate void delegateReadingFinished(Dictionary<string, string> TagsRead);
      public event delegateReadingFinished OndelegateReadingFinished;

      //public delegate void delegateReadingFinishedARCRets(List<ARCReadResult> ARCReadRetList);
      //public event delegateReadingFinishedARCRets OndelegateReadingFinishedARCRets;

      public delegate void delegateTagReported(SHSReadResult ReadRet, bool InsertRet);
      public event delegateTagReported OndelegateTagReported;

      public delegate void delegateReaderConnectionChanged(bool zReaderConnected);
      public event delegateReaderConnectionChanged OndelegateReaderConnectionChanged;

      
      //bool m_ResetTimerTriggered = false;//IN JL 17-APR-2014s

      //LightStackColor m_CurrStackLightStatus = LightStackColor.Off;//IN JL 14-OCT-2013
      string m_ReadingStartedStopped = "ReadingStarted";//ReadingStopped IN JL 14-OCT-2013
      bool m_InProcess = false;//IN JL 14-OCT-2013

      //GateCheckerSensors m_GateSensors;

      private LogString m_Logger = LogString.GetLogString("GateChecker");
      private LogString mLogger_ARC;
      public bool m_ReaderIsConnected = true;

      public bool m_DisconnectAlarmed = false;//IN JL 13-OCT-2013

      private System.Threading.Timer m_PingReaderTimer;
      private System.Threading.Timer m_CheckReaderConnection;
      //public System.Threading.Timer m_DefaultTimer;
      //private System.Threading.Timer m_CheckGateConfigurationChanges;//IN JL 08-OCT-2013s

      private int m_PingCounter = 0;
      private int m_CanPingCounter = 0;
      public OctaneReader m_Reader;
      private ProjectConfiguration m_Configuration;
      public GateDetail m_Gate;

      public string m_TagIdPreviousRead = "";//IN JL 13-OCT-2013

      public bool m_AlarmedFlag = false;

      // Create a Dictionary to store the tags we've read.
      //public Dictionary<string, Tag> m_TagsRead = new Dictionary<string, Tag>();
      public Dictionary<string,string> m_TagsRead = new Dictionary<string,string>();
      //public Dictionary<string, string> m_InactivatedTagsRead = new Dictionary<string, string>();


      //public List<ARCReadResult> m_ARCReadResult = new List<ARCReadResult>();

      private bool mWriteUserMemoryFlag = false;
      private string mASCIIWritingData = "";
      private string mHexWritingData = "";

      private HumpyDetail m_HumpyDetail = new HumpyDetail();

      public ReaderDirectController(   HumpyDetail AHD,//IN JL 28-MAY-2016
                                      //OUT JL 28-MAY-2016 GateDetail AGate,
                                       ProjectConfiguration AConfiguration,
                                       bool WriteUserMemoryFlag,
                                       string DataString,string ADBConn)
    {

        m_HumpyDetail = AHD;

        m_DAL = new HumpyDAL(ADBConn);
        //m_GateSensors = new GateCheckerSensors();

        mLogger_ARC = LogString.GetLogString("Humpy_" +AHD.HumpyGate.readerIP);

        m_Gate = AHD.HumpyGate;//INOUT 28-MAY-2016 AGate;
 
        m_Configuration = AConfiguration;
        m_Reader = new OctaneReader(m_Gate.GateReader);

        //Initialize the Ping Reader Timer
        m_PingReaderTimer = new System.Threading.Timer(new System.Threading.TimerCallback(OnPingReaderTimer), null,
                                                          System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        //Initialize the Check Reader Connection Timer
        m_CheckReaderConnection = new System.Threading.Timer(new System.Threading.TimerCallback(OnCheckReaderConnection), null,
                                                             System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

        mWriteUserMemoryFlag = WriteUserMemoryFlag;
        mASCIIWritingData = DataString;
        if (DataString != "")
        {
            mHexWritingData = ToHex(mASCIIWritingData).PadLeft(16,'2');
        }

        MainInitialization(mHexWritingData, m_Gate.rdReaderMode, m_Gate.rdSearchMode, m_Gate.rdSessionNum);
    }

      public static String ToHex(String data)
      {
          String output = String.Empty;
          foreach (Char c in data)
          {
              output += ((int)c).ToString("X");
          }
          return output;
      }


    private void MainInitialization(string aHexWritingData, ReaderMode aReaderMode, SearchMode aSearchMode, ushort aSession)
    {   
        try
        {
            if (ReaderInitialization(aHexWritingData, aReaderMode,aSearchMode,aSession))
            {
                m_ReaderIsConnected = true;
                m_DisconnectAlarmed = false;//IN JL 13-OCT-2013
                Start();
            }
            else
            {
                m_ReaderIsConnected = false;
            }

            //Enable the pinging and check reader connection timers
            if (m_PingReaderTimer != null)
                m_PingReaderTimer.Change(m_Configuration.Rd_PingReaderTimer_ms, System.Threading.Timeout.Infinite);

            if (m_CheckReaderConnection != null)
                m_CheckReaderConnection.Change(m_Configuration.Rd_CheckReaderConnection_ms, System.Threading.Timeout.Infinite);

        }
        catch (Exception ex)
        {
           m_Logger.Add("MainInitialization: " + ex.Message.ToString());
        }
    }

    public void OnReadingChangedDetected(bool AOnOff)
    {

        m_InProcess = true;

        GC.Collect();GC.WaitForPendingFinalizers();

        m_InProcess = false;
    }

      /// <summary>
      /// For Manully trigger the sensor purpose
      /// </summary>
      /// <param name="AGPIPort"></param>
      /// <param name="AStatus">False or True (False for triggering)</param>
    public void TriggerGPIManually(string AGPIPort, string AStatus)
    { 
       OnDelegateGPIChangeDetected( AGPIPort,  AStatus);
    }

    void OnDelegateGPIChangeDetected(string AGPIPort, string AStatus)
    {

        //IN JL 16-MAY-16: GPI 4 from Crane  direct to GPO 4
        //IN JL 25-APR-2015: Link GPI4 directly to GPO4
        try
        {
            OndelegateGPIChanged(AGPIPort, AStatus);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }


        if (!m_AlarmedFlag && !m_InProcess)
        {
            #region Triggered
            if (AStatus == "False" && AGPIPort == "1")//Reading Stopped - Stopped pased the tag list to the front end to process movement.
            {
                m_InProcess = true;

                m_ReadingStartedStopped = "ReadingStopped";//ReadingStopped 
                m_Logger.Add("BCDS - SensorTriggeredStopped by port" + AGPIPort);
                mLogger_ARC.Add("BCDS - Reader Stopped by port" + AGPIPort);

                m_Logger.Add("SensorTrigger PortNum:" + AGPIPort.ToString());//IN JL 14-OCT-2013 Test only
                //m_Reader.SetReaderGPO(LightStackColor.Off);

                m_InProcess = false;

                /////////////Pass the taglist
                try
                {
                    mLogger_ARC.Add(String.Format("Total: {0} tags read", m_TagsRead.Count));
                    OndelegateReadingFinished(m_TagsRead);

                    m_TagsRead.Clear();
                }
                catch (Exception ex)
                {
                    ex.ToString();//not associated witht front end...ignore it.
                }
            }
            else if (AStatus == "True" && AGPIPort == "1")
            {
                if (m_ReadingStartedStopped != "ReadingStarted")
                {
                    m_ReadingStartedStopped = "ReadingStarted";//ReadingStopped 
                    m_Logger.Add("BCDS - SensorTriggeredStarted by port" + AGPIPort);
                    mLogger_ARC.Add("BCDS - Reader Started by port" + AGPIPort);
                }

               // m_Reader.SetReaderGPO(LightStackColor.OrangeOnly);
            }
            #endregion
        }
    }


    //The Reader op result event handler can do some kind of filtering job
    //and this handler do another level database control job + image display job.
    void OnDelegateTagDetected(string ARevData, string ADelegateTagData, string AEPCTagData, double APhaseInRadians, double APeakRSSI, int AAntPort)
    {


        TagHandling processTag = new TagHandling();
        // Add this tag to the list of tags we've read.
        string wifiIPAddress = "";
        string humpyLocationID = "";
        string masterHumpyConnectionString = "";
        HQDataExtract getConfigValues = new HQDataExtract();

        DLLConstants getProgramValue = new DLLConstants();
        Workers getWorkersName = new Workers();
        Logs insertApplicationLogs = new Logs();
        masterHumpyConnectionString = m_HumpyDetail.Sys_DBConnection;

        wifiIPAddress = getConfigValues.GetConfigValue(ConfigurationManager.ConnectionStrings["SmartHumpyController.Properties.Settings.Sys_DBConnectionLocal"].ToString(), getProgramValue.dbConfigProgramValue_WirelessIPAddress);
        humpyLocationID = m_HumpyDetail.Sys_HumpyId;
        lock (m_TagsRead)
        {
            if (processTag.GetTagGroup(AEPCTagData, false) != "")
            {
                //string zStrCount = "1";
                //if (APeakRSSI > )
                {
                   SHSReadResult zRdRet = new SHSReadResult();
                   zRdRet.AntennaId = AAntPort;
                   zRdRet.PeakRSSI = APeakRSSI;
                   zRdRet.Count = 1;
                   zRdRet.EPC = processTag.GetTagGroup(AEPCTagData, false);
                   zRdRet.LocId = m_Gate.GateReader.ReaderName;


                   //1. Write into the database
                   bool zRet_Inserting =true;
                   try
                   {
                       m_DAL.SHS_InsertInputsRecord(zRdRet.EPC,
                                                    zRdRet.PeakRSSI.ToString(),
                                                    zRdRet.AntennaId.ToString(),
                                                    zRdRet.LocId,
                                                    m_HumpyDetail.Sys_TagINsensitivity_ms);//Modified JL 19-01-16 //Modified 28-MAY-16
                                                    //SmartHumpyController.Properties.Settings.Default.Sys_TagINsensitivity_ms);//Modified JL 19-01-16 //Modified 28-MAY-16
                       //insertApplicationLogs.InsertApplicationLogs(masterHumpyConnectionString,
                       //           insertApplicationLogs.AppLogs_ComponentWorkerValue,
                       //           getWorkersName.GetWorkersNameByEPC(masterHumpyConnectionString, zRdRet.EPC),
                       //           insertApplicationLogs.AppLogs_StatusUncheckedValue,
                       //           insertApplicationLogs.AppLogs_ActionInValue,
                       //           zRdRet.EPC,
                       //           humpyLocationID,
                       //           wifiIPAddress);

                   }
                   catch (Exception ex)
                   {
                       zRet_Inserting =false;
                       m_Logger.AddErrors("UHF Read Result Insert Fail", ex.ToString());
                       //Control the Reader GPIO port

                       //Control the local light
                       //StackLightStatus(Color.Yellow);
                   }

                   //2. Report the read and insert result -failed or success.

                   OndelegateTagReported(zRdRet, zRet_Inserting);

                   //2.1 If failed, cannot connect to the db, 
                   //             a. Log the error
                   //             b. Change the stack light to YELLOW!!!







                   //ARCReadResult zSearchRet = m_ARCReadResult.Find(x => x.EPC == AEPCTagData && x.AntennaId == AAntPort);
                   //if (zSearchRet != null)
                   //{
                   //    //Update
                   //    m_ARCReadResult.Where(x => x.EPC == AEPCTagData && x.AntennaId == AAntPort).Select(x => { x.Count++; x.PeakRSSI = APeakRSSI; return x;});
                   //}
                   //else //add
                   //{
                   //    m_ARCReadResult.Add(zRdRet);
                   //}

                   //if (!m_TagsRead.ContainsKey(AEPCTagData.ToString()))
                   //{
                   //     m_TagsRead.Add(AEPCTagData.ToString(), AAntPort.ToString());
                   //}
                }
            }
        }
    }


    /// <summary>
    /// </summary>
    public void DisplayMsg(Color AColr, Image AImage, string ADisplayMessage)
    {
        try
        {
            //display form update
            if (AColr == Color.Red)
            {

            }
            else if (AColr == Color.Green)
            {
             
            }
            else
            {
             
            }

            //m_zFrmDelegate.picDelegate.Invoke(new EventHandler(delegate { m_zFrmDelegate.picDelegate.Image = AImage; }));
            //m_zFrmDelegate.txtInformation.Invoke(new EventHandler(delegate { m_zFrmDelegate.txtInformation.Text = ADisplayMessage; }));
        }
        catch (Exception ex)
        {
            m_Logger.AddErrors("DisplayMsg", ex.Message);
        }
        finally
        {
            GC.Collect();// GC.WaitForPendingFinalizers();
        }
    }



    private void InsertErrorLog(DelegateDetail Add,string AErroMsg,string AErrorCode)
    {
        try
        {
            //error log insert into tht ErrorLog Table
            ErrorLog zEL = new ErrorLog();
            zEL.errMsg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-" + Add.RFID_TagID + "-" + m_Gate.GateID + "-" + AErroMsg;
            zEL.errCode = AErrorCode;
            zEL.errCat = "ERR-PORTAL";
            zEL.occurredAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //m_DAL.GC_AddErrorLog(zEL);
        }
        catch (Exception ex)
        {
            m_Logger.AddErrors("InsertErrorLog", ex.Message);
        }
    }


    private bool ReaderInitialization(string aHexWritingData, ReaderMode aReaderMode,SearchMode aSearchmode, ushort aSession)
    {
      bool zRet = false;
      try
      {
          if (m_Reader.Connect())
          {
              //Access Password
              TagData zAccessPwd = null;
              if (m_Gate.AccessPwd != "")
              {
                  zAccessPwd = new TagData();
                  zAccessPwd = TagData.FromHexString(m_Gate.AccessPwd);
              }

              //Configurations
              m_Reader.OneTimeConfiguration(zAccessPwd,
                                           m_Reader.m_PassiveReader.ReaderType,
                                           m_Reader.m_PassiveReader.Antenna1,
                                           m_Reader.m_PassiveReader.Antenna2,
                                           m_Reader.m_PassiveReader.Antenna3,
                                           m_Reader.m_PassiveReader.Antenna4,
                                           m_Configuration.TagFilter1,
                                           m_Configuration.TagFilter2,
                                           aHexWritingData,
                                           aReaderMode,
                                           aSearchmode,
                                           aSession);
              //Add tag handler
              m_Reader.EventHandlersAdded(aHexWritingData);
              //m_Reader.getReaderCapabilities();

              zRet = true;
          }
          else
          {
              //OnReaderCheckiConnection timer is working on it...
          }
      }
      catch (Exception ex)
      {
          m_Logger.Add("OctaneRd" + ex.Message.ToString());
      } 
      return zRet;
    }
    //public Settings GetMReaderSettings()
    //{
    //    if (m_ReaderIsConnected == true)
           
    //    else
    //        return null;
    //}
    private bool ReaderReconnect(string aHexWritingData, ReaderMode aReaderMode, SearchMode aSearchMode, ushort aSession)
    {
        bool zRet = false;
        try
        {
            m_Reader.StopAndDisconnect(aHexWritingData);

            m_Reader = new OctaneReader(m_Gate.GateReader);//IN JL 13-OCT-2013

            if (m_Reader.Connect())
            {
                //Access Password
                TagData zAccessPwd = null;
                if (m_Gate.AccessPwd != "")
                {
                    zAccessPwd = new TagData();
                    zAccessPwd = TagData.FromHexString(m_Gate.AccessPwd);
                }

                //Configurations
                //m_Reader.OneTimeConfiguration(null,
                //                             m_Reader.m_PassiveReader.ReaderType,
                //                             m_Reader.m_PassiveReader.Antenna1,
                //                             m_Reader.m_PassiveReader.Antenna2,
                //                             m_Reader.m_PassiveReader.Antenna3,
                //                             m_Reader.m_PassiveReader.Antenna4);

                m_Reader.OneTimeConfiguration(zAccessPwd,
                                  m_Reader.m_PassiveReader.ReaderType,
                                  m_Reader.m_PassiveReader.Antenna1,
                                  m_Reader.m_PassiveReader.Antenna2,
                                  m_Reader.m_PassiveReader.Antenna3,
                                  m_Reader.m_PassiveReader.Antenna4,
                                  m_Configuration.TagFilter1,
                                  m_Configuration.TagFilter2,
                                  aHexWritingData,
                                  aReaderMode,
                                  aSearchMode,
                                  aSession);

                //Add tag handler
                m_Reader.EventHandlersAdded(aHexWritingData);
                zRet = true;
            }
            else
            {
                //OnReaderCheckiConnection timer is working on it...
            }
        }
        catch (Exception ex)
        {
            m_Logger.Add("GateChecker:" + ex.Message.ToString());
        }
        finally
        {
            GC.Collect(); GC.WaitForPendingFinalizers();
        }
        return zRet;
    }
  
    public void Dispose()
    {
        try
        {
            DisposeAllThreadTimers();



            m_Reader.StopAndDisconnect(mHexWritingData);
            m_Reader = null;//IN JL 13-OCT-2013
        }
        catch (Exception ex)
        {
            m_Logger.Add("Octane:" + ex.Message.ToString());
            //m_Logger.Add("ImpinjMultiReaders-Disconnect: " + ex.Message.ToString());
        }
        finally
        {
            GC.Collect(); GC.WaitForPendingFinalizers();
        }
    }

    /// <summary>
    /// Start the Impinj Reader(s)
    /// </summary>
    public void Start()
    {
        try
        {
            if (m_Reader != null)
            {
                m_Reader.OnDelegateTagDetected += new OctaneReader.delegateTagDetected(OnDelegateTagDetected);
                m_Reader.OnDelegateGPIChangeDetected += new OctaneReader.delegateGPIChangedDetected(OnDelegateGPIChangeDetected);
                m_Reader.OnReadingChangedDetected += new OctaneReader.delegateReadingStatus(OnReadingChangedDetected);
                m_Reader.StartReading();

                m_Logger.AddActions("ImpinjReadersStartReading", "Reader Reading Started");
            }
        }
        catch (Exception ex)
        {
            m_Logger.AddErrors("ImpinjReadersStartReading", ex.StackTrace.ToString());
        }
    }

    /// <summary>
    /// Start the Impinj Reader(s)
    /// </summary>
    public void Stop()
    {
        try
        {
            if (m_Reader != null)
            {

                m_Reader.StopReading();

                //m_Reader.TagsReported -= new EventHandler<TagsReportedEventArgs>(m_pReader_TagsReportedHandler);
                m_Reader.OnDelegateTagDetected -= new OctaneReader.delegateTagDetected(OnDelegateTagDetected);
                m_Reader.OnDelegateGPIChangeDetected -= new OctaneReader.delegateGPIChangedDetected(OnDelegateGPIChangeDetected);
                m_Reader.OnReadingChangedDetected -= new OctaneReader.delegateReadingStatus(OnReadingChangedDetected);

                if (m_Reader.m_ReadingFlag)
                {
                    //m_Reader.StopReading();
                    m_Reader.m_ReadingFlag = false;
                }

                //m_Reader.SetReaderGPO(LightStackColor.Off);
                m_Logger.AddActions("ImpinjReaderStopReading", "Reader Reading Stopped");
            }
        }
        catch (Exception ex)
        {
            m_Logger.AddErrors("ImpinjReaderStopReading", ex.StackTrace.ToString());
        }
    }

    private void DisposeAllThreadTimers()
    {
        //Enable the pinging and check reader connection timers
        if (m_PingReaderTimer != null)
        {
            m_PingReaderTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            m_PingReaderTimer = null;
        }

        if (m_CheckReaderConnection != null)
        {
            m_CheckReaderConnection.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            m_CheckReaderConnection = null;
        }

        //if (m_DefaultTimer != null)
        //{
        //    m_DefaultTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        //    m_DefaultTimer = null;
        //}
    }



    #region CheckReaderConnection & PingReaderTimer
    private void OnCheckReaderConnection(object AObject)
    {
        try
        {
            if (m_ReaderIsConnected == false)
            {
                if (m_DisconnectAlarmed == false)
                {

                    m_Logger.AddActions("ReconnectReader", "Start reconnecting reader");

                    Stop();

                    m_DisconnectAlarmed = true;
                }

                if (m_Reader.m_PassiveReader.ReaderIPAddress != string.Empty)
                {
                    if (PingReader(m_Reader.m_PassiveReader.ReaderIPAddress))
                    {
                        //Initialize the reader
                        if (ReaderReconnect(mHexWritingData,m_Gate.rdReaderMode, m_Gate.rdSearchMode, m_Gate.rdSessionNum))
                        {
                            m_Logger.AddActions("ReconnectReader", "Reader reconnected");

                            if (!m_ReaderIsConnected)//IN JL 27-MAY-16: if reader wasn't connected, but now it is connected, report it.
                            {
                                m_ReaderIsConnected = true;

                                try
                                {
                                    OndelegateReaderConnectionChanged(m_ReaderIsConnected);
                                }
                                catch (Exception ex)
                                {
                                    ex.ToString();
                                }
                            }

                            m_ReaderIsConnected = true;
                            m_DisconnectAlarmed = false;//IN JL 13-OCT-2013
                            Start();
                        }
                        else
                        {
                            m_Logger.AddActions("ReconnectReader", "Reader reconnect failed");
                            m_ReaderIsConnected = false;
                        }
                    }
                    else
                    {
                        m_Logger.AddActions("ReconnectReader: Cannot Ping Reader IP Address", "Reader reconnected");
                    }
                }
                else
                {
                    m_Logger.AddActions("ReconnectReader: Empty Reader IP Address", "Reader reconnected");
                }
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
            m_Logger.AddErrors("OnCheckReaderConnection", ex.StackTrace.ToString());
        }
        finally
        {
            GC.Collect();

            if (m_CheckReaderConnection != null)
                m_CheckReaderConnection.Change(m_Configuration.Rd_CheckReaderConnection_ms, System.Threading.Timeout.Infinite);
        }
    }

    /// <summary>
    /// Ping reader every second, if it can ping, increase the ping counter.
    /// </summary>
    /// <param name="AObject"></param>
    private void OnPingReaderTimer(object AObject)
    {
        try
        {
            if (m_Reader.m_PassiveReader.ReaderIPAddress != string.Empty)
            {
                m_PingCounter++;
                if (PingReader(m_Reader.m_PassiveReader.ReaderIPAddress))
                {
                    m_CanPingCounter++;
                }

                if (m_PingCounter >= m_Configuration.Rd_PingCounter)
                {
                    //if (m_CanPingCounter < (m_PingCounter / 2))
                    if (m_CanPingCounter < m_PingCounter)
                    {
                        if (m_ReaderIsConnected)//IN JL 27-MAY-16: if reader was connected, but now it is disconnected, report it.
                        {
                            m_ReaderIsConnected = false;

                            try
                            {
                                OndelegateReaderConnectionChanged(m_ReaderIsConnected);
                            }
                            catch (Exception ex)
                            {
                                ex.ToString();
                            }
                        }
                    }

                    m_PingCounter = 0;
                    m_CanPingCounter = 0;
                }
            }
            else
            {
                m_PingCounter = 0;
                m_CanPingCounter = 0;
                m_ReaderIsConnected = false;//IN JL 13-OCT-2013
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        finally
        {
            GC.Collect();// GC.WaitForPendingFinalizers();

            if (m_PingReaderTimer != null)
                m_PingReaderTimer.Change(m_Configuration.Rd_PingReaderTimer_ms, System.Threading.Timeout.Infinite);
        }
    }
    /// <summary>
    /// Pings the devices
    /// </summary>
    /// <param name="AIPAddress">IP Addess of the Device to Ping</param>
    /// <returns>TRUE is device could be reached otherwise FALSE</returns>
    private bool PingReader(string AIPAddress)
    {
        bool zResult = false;
        try
        {
            PingReply zPingReply;//IN JL 29-MAY-2013  v1.0.3.3
            Ping zPingObject = new Ping();//29-MAY-2013  v1.0.3.3

            //OUT 29-MAY-2013  v1.0.3.3 PingReply zPingReply = new Ping().Send(AIPAddress);
            zPingReply = zPingObject.Send(AIPAddress);//IN JL 29-MAY-2013  v1.0.3.3

            GC.KeepAlive(zPingObject);//IN JL 29-MAY-2013  v1.0.3.3

            if (zPingReply.Status == IPStatus.Success)
                zResult = true;
        }
        catch (Exception ex)
        {
            ex.ToString();
            zResult = false;
        }
        finally
        {
            GC.Collect();// GC.WaitForPendingFinalizers();//IN JL 13-OCT-2013
        }

        return zResult;
    }
    #endregion
  }
}
