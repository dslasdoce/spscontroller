//****************************************************************************//
//       This source code belongs to Barcode Data Systems (BCDS)              //
//    Unit 24, 10 Yalgar Road, Kirrawee, Sydney, NSW Australia 2232           //
//                           www.bcds.com.au                                  //
//                                                                            //
// This file may not be copied in whole or in part without written permission //
//                         from the copyright owner.                          //
//                                                                            //
//                      © 2013, Barcode Data Systems (BCDS)                   //
//****************************************************************************//
//                                                                            //
// Project     : EnTrack GXX Project                                          //
//                                                                            //
// Client      : Classified                                                   //
//                                                                            //
// File        : GateCheckerReaderController.cs                               //
//                                                                            //
// Description :                                                              //
//                                                                            //
// Initial Author: Team BCDS - Jasper Liu                              .      //
// Date Written  : 17-SEP-2013                                                //
// Documentation : BCDS Visual Source Safe.                                   //
//                                                                            //
//****************************************************************************//
// Modification History:                                                      //
//                                                                            //
// Date..... Who.......... Modification Description.......................... //
// DD-MMM-YY xxxxxxxxxxxxx                                                    //
//
// 17-SEP-2013 Jasper Liu Initial Development Started                         //
//                                                                            //
//****************************************************************************//
using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using System.IO;
using System.Net.NetworkInformation;
using System.Drawing;
using System.Linq;

using Impinj.OctaneSdk;


using ARC.Tools;

namespace ARC.RFID.Direct
{

    public class ARCReadResult
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
  
  public class ARCReaderDirectController
  {

      public delegate void delegateReadingFinished(Dictionary<string, string> TagsRead);
      public event delegateReadingFinished OndelegateReadingFinished;

      public delegate void delegateReadingFinishedARCRets(List<ARCReadResult> ARCReadRetList);
      public event delegateReadingFinishedARCRets OndelegateReadingFinishedARCRets;

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


      public List<ARCReadResult> m_ARCReadResult = new List<ARCReadResult>();

      private bool mWriteUserMemoryFlag = false;
      private string mASCIIWritingData = "";
      private string mHexWritingData = "";

      public ARCReaderDirectController(GateDetail AGate,
                                       ProjectConfiguration AConfiguration,
                                       bool WriteUserMemoryFlag,
                                       string DataString)
    {

        //m_GateSensors = new GateCheckerSensors();


        mLogger_ARC = LogString.GetLogString("ARC Move Portal" + AGate.readerIP);
        //OUT JL 03-DEC-2013 
        //m_G20DefaultImage = GateChecker.Properties.Resources.G20Australia;
        //m_G20AlarmImage = GateChecker.Properties.Resources.G20Australia;
       
        m_Gate = AGate;
 
        m_Configuration = AConfiguration;
        m_Reader = new OctaneReader(m_Gate.GateReader);

        //Initialize the Ping Reader Timer
        m_PingReaderTimer = new System.Threading.Timer(new System.Threading.TimerCallback(OnPingReaderTimer), null,
                                                          System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        //Initialize the Check Reader Connection Timer
        m_CheckReaderConnection = new System.Threading.Timer(new System.Threading.TimerCallback(OnCheckReaderConnection), null,
                                                             System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

        ////Initialize the G20 default image on the DelegatesInfor form
        //m_DefaultTimer = new System.Threading.Timer(new System.Threading.TimerCallback(OnDefaultTimer), null,
        //                                                     System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);


        mWriteUserMemoryFlag = WriteUserMemoryFlag;
        mASCIIWritingData = DataString;
        if (DataString != "")
        {
            mHexWritingData = ToHex(mASCIIWritingData).PadLeft(16,'2');
        }

        MainInitialization(mHexWritingData);
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


    private void MainInitialization(string aHexWritingData)
    {   
        try
        {
            if (ReaderInitialization(aHexWritingData))
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

            //if (m_DefaultTimer != null)
            //    m_DefaultTimer.Change(m_Configuration.Sys_DefaultImageTimer_ms, System.Threading.Timeout.Infinite);

        }
        catch (Exception ex)
        {
           m_Logger.Add("MainInitialization: " + ex.Message.ToString());
        }
    }

    public void OnReadingChangedDetected(bool AOnOff)
    {

        m_InProcess = true;//IN JL 15-OCT-2013

        GC.Collect();GC.WaitForPendingFinalizers();//IN JL 13-OCT-201

        m_InProcess = false;//IN JL 15-OCT-2013
    }

      /// <summary>
      /// IN JL 16-OCT-2013
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
        if (!m_AlarmedFlag && !m_InProcess)
        {
            #region Triggered
            if (AStatus == "False" && AGPIPort == "1")//Reading Stopped - Stopped pased the tag list to the front end to process movement.
            {
                //if (m_DefaultTimer!= null)
                //    m_DefaultTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

                m_InProcess = true;//IN JL 14-OCT-2013

                m_ReadingStartedStopped = "ReadingStopped";//ReadingStopped 
                m_Logger.Add("BCDS - SensorTriggeredStopped by port" + AGPIPort);
                mLogger_ARC.Add("BCDS - Reader Stopped by port" + AGPIPort);

                m_Logger.Add("SensorTrigger PortNum:" + AGPIPort.ToString());//IN JL 14-OCT-2013 Test only
                m_Reader.SetReaderGPO(LightStackColor.Off);

                m_InProcess = false;//IN JL 14-OCT-2013

                /////////////Pass the taglist
                try
                {
                    mLogger_ARC.Add(String.Format("Total: {0} tags read", m_TagsRead.Count));
                    OndelegateReadingFinished(m_TagsRead);

                    OndelegateReadingFinishedARCRets(m_ARCReadResult);
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

                m_Reader.SetReaderGPO(LightStackColor.OrangeOnly);
            }
            #endregion
        }
    }


    //The Reader op result event handler can do some kind of filtering job
    //and this handler do another level database control job + image display job.
    void OnDelegateTagDetected(string ARevData, string ADelegateTagData, string AEPCTagData, double APhaseInRadians, double APeakRSSI, int AAntPort)
    {
        // Add this tag to the list of tags we've read.
        lock (m_TagsRead)
        {
            if (AEPCTagData != "")
            {
                //string zStrCount = "1";

                //if (APeakRSSI > )
                {
                   ARCReadResult zRdRet = new ARCReadResult();
                   zRdRet.AntennaId = AAntPort;
                   zRdRet.PeakRSSI = APeakRSSI;
                   zRdRet.Count = 1;
                   zRdRet.EPC = AEPCTagData;

                   ARCReadResult zSearchRet = m_ARCReadResult.Find(x => x.EPC == AEPCTagData && x.AntennaId == AAntPort);
                   if (zSearchRet != null)
                   {
                       //Update
                       m_ARCReadResult.Where(x => x.EPC == AEPCTagData && x.AntennaId == AAntPort).Select(x => { x.Count++; x.PeakRSSI = APeakRSSI; return x;});
                   }
                   else //add
                   {
                       m_ARCReadResult.Add(zRdRet);
                   }


                    if (!m_TagsRead.ContainsKey(AEPCTagData.ToString()))
                    {
                        m_TagsRead.Add(AEPCTagData.ToString(), AAntPort.ToString());
                    }

                }
            }
            //else//otherwise, alarm the inactivated tag
            //{
            //    if (!m_InactivatedTagsRead.ContainsKey(AEPCTagData))
            //    {
            //        m_InactivatedTagsRead.Add(AEPCTagData, AEPCTagData);
            //    }
            //}
        }
    }


    /// <summary>
    /// IN JL 08-OCT-2013
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
            GC.Collect();// GC.WaitForPendingFinalizers();//IN JL 13-OCT-2013
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


    private bool ReaderInitialization(string aHexWritingData)
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
                                           aHexWritingData);
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
          m_Logger.Add("OctaneRd" + ex.Message.ToString());
      } 
      return zRet;
    }

    private bool ReaderReconnect(string aHexWritingData)
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
                //OUT JL 26-FEB-2014
                //m_Reader.OneTimeConfiguration(null,
                //                             m_Reader.m_PassiveReader.ReaderType,
                //                             m_Reader.m_PassiveReader.Antenna1,
                //                             m_Reader.m_PassiveReader.Antenna2,
                //                             m_Reader.m_PassiveReader.Antenna3,
                //                             m_Reader.m_PassiveReader.Antenna4);

                //IN JL 26-FEB-2014
                m_Reader.OneTimeConfiguration(zAccessPwd,
                                  m_Reader.m_PassiveReader.ReaderType,
                                  m_Reader.m_PassiveReader.Antenna1,
                                  m_Reader.m_PassiveReader.Antenna2,
                                  m_Reader.m_PassiveReader.Antenna3,
                                  m_Reader.m_PassiveReader.Antenna4,
                                  m_Configuration.TagFilter1,
                                  m_Configuration.TagFilter2,
                                  aHexWritingData);

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

                //OUT JL 08-OCT-2013 m_Reader.SetReaderGPO(LightStackColor.GreenOnly);
                //m_Reader.SetReaderGPO(LightStackColor.RedOnly);//IN JL 08-OCT-2013
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
                //m_Reader.TagsReported -= new EventHandler<TagsReportedEventArgs>(m_pReader_TagsReportedHandler);
                m_Reader.OnDelegateTagDetected -= new OctaneReader.delegateTagDetected(OnDelegateTagDetected);
                m_Reader.OnDelegateGPIChangeDetected -= new OctaneReader.delegateGPIChangedDetected(OnDelegateGPIChangeDetected);
                m_Reader.OnReadingChangedDetected -= new OctaneReader.delegateReadingStatus(OnReadingChangedDetected);

                if (m_Reader.m_ReadingFlag)
                {
                    //m_Reader.StopReading();
                    m_Reader.m_ReadingFlag = false;
                }

                m_Reader.SetReaderGPO(LightStackColor.Off);
                m_Logger.AddActions("ImpinjReaderStopReading", "Reader Reading Stopped");
            }
        }
        catch (Exception ex)
        {
            m_Logger.AddErrors("ImpinjReaderStopReading", ex.StackTrace.ToString());
        }
    }


    //public void OnDefaultTimer(object AObject)
    //{
    //    try
    //    {
    //        if (!m_ReaderIsConnected)//IN JL 08-OCT-2013
    //        {
    //            if (m_DefaultTimer != null)
    //                m_DefaultTimer.Change(m_Configuration.Sys_DefaultImageTimer_ms, System.Threading.Timeout.Infinite);
    //        }
    //        else
    //        {
    //            if (true)
    //            {

    //                if (!m_InProcess)
    //                {
    //                    //IN JL 19-SEP-2014: If the screen is green/red, the timer started in 3 seconds currently.
    //                    //                   While it is reseting, if the 
    //                    m_Reader.SetReaderGPO(LightStackColor.RedOnly);


    //                    m_Logger.Add("!! on default timer, ignore this reset!!!!Sensor Reset after reader stopped");
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        m_Logger.AddErrors("OnDefaultG20Image", ex.StackTrace.ToString());
    //    }
    //    finally
    //    {
    //        m_InProcess = false;
    //        m_ResetTimerTriggered = false;//IN JL 17-APR-2014
    //        GC.Collect(); GC.WaitForPendingFinalizers();//IN JL 13-OCT-2013
    //    }
    //}

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
                    //START IN JL 08-OCT-2013 When it disconnected, display the the new message
                    //if (m_DefaultTimer != null)
                    //    m_DefaultTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);


                    m_Logger.AddActions("ReconnectReader", "Start reconnecting reader");

                    Stop();

                    m_DisconnectAlarmed = true;
                }

                if (m_Reader.m_PassiveReader.ReaderIPAddress != string.Empty)//IN JL 13-OCT-2013
                {
                    if (PingReader(m_Reader.m_PassiveReader.ReaderIPAddress))//IN JL 13-OCT-2013
                    {
                        //Initialize the reader
                        if (ReaderReconnect(mHexWritingData))
                        {
                            //START IN JL 08-OCT-2013 WHen it reconnect, Reset the screen back to default
                            //if (m_DefaultTimer != null)
                            //    m_DefaultTimer.Change(m_Configuration.Sys_DefaultImageTimer_ms,                                                                         System.Threading.Timeout.Infinite);
                            //END IN JL 08-OCT-2013 WHen it reconnect, Reset the screen back to default

                            m_Logger.AddActions("ReconnectReader", "Reader reconnected");
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
                    else //IN JL 13-OCT-2013
                    {
                        m_Logger.AddActions("ReconnectReader: Cannot Ping Reader IP Address", "Reader reconnected");
                    }
                }
                else //IN JL 13-OCT-2013
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
            GC.Collect();// GC.WaitForPendingFinalizers();//IN JL 13-OCT-2013

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
                        m_ReaderIsConnected = false;

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
            GC.Collect();// GC.WaitForPendingFinalizers();//IN JL 13-OCT-2013

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
