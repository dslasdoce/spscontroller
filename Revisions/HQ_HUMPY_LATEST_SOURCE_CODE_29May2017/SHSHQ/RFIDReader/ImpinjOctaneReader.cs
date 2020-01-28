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
// File        : ImpinjOctaneLLRP.cs                                          //
//                                                                            //
// Description : Impinj Octane 7 Reader Wrapper                               //
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
using System.IO;
using System.Collections.Generic;
using System.Net.NetworkInformation;

using Impinj.OctaneSdk;

using ARC.Tools;

 namespace ARC.RFID.Direct
{

  #region VirtualPassivePortal Region
    /// <summary>
  /// Passive Portal Class
  /// Added On 20-MAR-2011 
  /// For Dynamic Updating
  /// </summary>
  public class PassivePortalClass
  {
    public int mPortalID = 0;
    public string mReaderNumbers = string.Empty;
    public List<PassiveReaderClass> mPassiveReaderList;
    
    public PassivePortalClass(List<PassiveReaderClass> APassiveReaderList,int APortalID)
    {
      mPassiveReaderList = APassiveReaderList;
      mPortalID = APortalID;
    }

    ~PassivePortalClass()
    {
      // Do nothing
      //GC.SuppressFinalize(this);
    }

    public void ConstructReaders()
    {
      // Do nothing
    }

    public int PortalIDValue
    {
      get { return mPortalID; }
      set { mPortalID = value; }
    }

    public string PortalReaderNumbers
    {
      get { return mReaderNumbers; }
      set { mReaderNumbers = value; }
    }
  }

  /// <summary>
  /// Passive Reader Reader Class
  /// Added on 28-FEB-2011
  /// </summary>
  public class PassiveReaderClass
  {
    public const string RevolutionXPortal = "R640";
    public const string RevolutionR420 = "R420 ";
    public const string RevolutionR220 = "R220";
    public const int AntTXMax = 80;
    public const int AntTXMin = 10;

    public string m_AccessPwd = "";
    private OctaneReader.ReaderType m_ReaderType = OctaneReader.ReaderType.R420;

    private double m_ReaderTxPower = 0;
    private string m_ReaderID = string.Empty;
    private string m_PortalID = string.Empty;
    private string m_ReaderName = string.Empty;
    private string m_ReaderIPAddress = string.Empty;
    private string m_ReaderPowerONOFF = string.Empty;
    private string m_ReaderPollTime = string.Empty; //Seconds
    private string m_ReaderDescription = string.Empty;

    bool m_ReaderIsConnected_Ping = true;//IN JL 30-MAY-11
    private System.Threading.Timer m_PingReaderTimer; //IN JL 30-MAY-11

    public bool m_ReaderDisconntedAlarmSentFlag = false;//IN JL 17-SEP-12

    public PassiveReaderAntClass Antenna1 = new PassiveReaderAntClass();
    public PassiveReaderAntClass Antenna2 = new PassiveReaderAntClass();
    public PassiveReaderAntClass Antenna3 = new PassiveReaderAntClass();
    public PassiveReaderAntClass Antenna4 = new PassiveReaderAntClass();

    //JL 13-MAR-13 Comment: If the pinging is in the process, class cannot shut itself down.
    private bool m_Terminated = false;//IN JL 13-MAR-13

    //public ISMPassiveLogTool.ISMPassiveConfig m_ISMPassiveConfig = new ISMPassiveConfig();//IN JL 26-FEB-2013

    public PassiveReaderClass()
    {
        //OUT 13-OCT-2013 m_PingReaderTimer = new System.Threading.Timer(new System.Threading.TimerCallback(OnPingReaderTimer), null, 2000, System.Threading.Timeout.Infinite);//IN JL 13-MAR-13
    }

    /// <summary>
    /// IN JL 13-MAR-13
    /// Stop the pinging timer
    /// </summary>
    public void Close()
    {

        if (m_PingReaderTimer != null) //IN JL 13-MAR-13
        {
            m_Terminated = true;
            m_PingReaderTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            m_PingReaderTimer = null;
        }
    }

    ~PassiveReaderClass()
    {
      // Do nothing
    }

    public bool ReaderIsConnected_Ping
    {
        get { return m_ReaderIsConnected_Ping; }
        set { m_ReaderIsConnected_Ping = value; }
    }

    //OUT JL 26-FEB-2013
    private void OnPingReaderTimer(object AObject)
    {
      try
      {
        if (m_ReaderIPAddress != string.Empty)
        {
          if (!PingReader(m_ReaderIPAddress))
          {
            //Comment:As per the log history, reader is not actually dead, 
            //        our application send false alarm due to the ping delay response. 
            //        To avoid this false alarm We write another function to check really this reader is dead or alive.
            //if(!SecondTimePingReader(m_ReaderIPAddress))//IN JL 13-MAR-13
              m_ReaderIsConnected_Ping = false;
          }
        }
      }
      catch (Exception ex)
      {
        ex.ToString();
      }
      finally
      {
        GC.Collect();// GC.WaitForPendingFinalizers();//IN JL 13-OCT-2013

        if (m_PingReaderTimer != null) //IN JL 13-MAR-13
          m_PingReaderTimer.Change(2000, System.Threading.Timeout.Infinite);
      }

      if (m_Terminated == true)
      {
          Close();
          return;
      }
    }

    /// <summary>
    /// Pings the devices
    /// 10-JUN-11 Modification: Instead of pinging the ip once.
    ///                         Put the sampling here. If the number is more than 50% pingable,
    ///                         return the True, otherwise return the False;
    /// </summary>
    /// <param name="AIPAddress">IP Addess of the Device to Ping</param>
    /// <returns>TRUE is device could be reached otherwise FALSE</returns>
    private bool PingReader(string AIPAddress)
    {
      int zPassCounter = 0;
      int zFailCounter = 0;
      bool zResult = false;
      try
      {
          PingReply zPingReply;//IN JL 19-MAY-2013  v1.4.2.12
          Ping zPingObject = new Ping();//IN JL 19-MAY-2013  v1.4.2.12

          for (int zInd = 0; zInd < 10; zInd++)
          {
              zPingReply = zPingObject.Send(AIPAddress, 1000);//IN JL 19-MAY-2013  v1.4.2.12
              GC.KeepAlive(zPingObject);//IN JL 19-MAY-2013 v1.0.4.12


              if (zPingReply.Status == IPStatus.Success)
                  zPassCounter++;
              else
                  zFailCounter++;
          }

          if (zPassCounter >= zFailCounter)
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

    public string ReaderID
    {
      get { return m_ReaderID; }
      set { m_ReaderID = value; }
    }

    public OctaneReader.ReaderType ReaderType
    {
        get { return m_ReaderType; }
        set { m_ReaderType = value; }
    }

    public string PortalID
    {
      get { return m_PortalID; }
      set { m_PortalID = value; }
    }

    public string ReaderName
    {
      get { return m_ReaderName; }
      set { m_ReaderName = value; }
    }

    public string ReaderIPAddress
    {
      get { return m_ReaderIPAddress; }
      set { m_ReaderIPAddress = value; }
    }

    public string ReaderPowerONOFF
    {
      get { return m_ReaderPowerONOFF; }
      set { m_ReaderPowerONOFF = value; }
    }

    public string ReaderPollTime
    {
      get { return m_ReaderPollTime; }
      set { m_ReaderPollTime = value; }
    }

    public string ReaderDescription
    {
      get { return m_ReaderDescription; }
      set { m_ReaderDescription = value; }
    }

    public double ReaderTxPower
    {
      get { return m_ReaderTxPower; }
      set { m_ReaderTxPower = value; }
    }
  }

  /// <summary>
  /// Passive Reader Antenna Class
  /// Added on 28-FEB-2011
  /// </summary>
  public class PassiveReaderAntClass
  {
    private string m_ReaderId = string.Empty;
    private string m_AntennaId = string.Empty;
    private double m_TxPower = 0;
    private double m_RssiThreshold = 0;
    private string m_PowerONOFF = string.Empty;

    public PassiveReaderAntClass()
    {
      // Do nothing
    }

    ~PassiveReaderAntClass()
    {
      // Do nothing
    }

    public string ReaderId
    {
      get { return m_ReaderId; }
      set { m_ReaderId = value; }
    }

    public string AntennaId
    {
      get { return m_AntennaId; }
      set { m_AntennaId = value; }
    }

    public double TxPower
    {
      get { return m_TxPower; }
      set { m_TxPower = value; }
    }

    public double RssiThreshold
    {
      get { return m_RssiThreshold; }
      set { m_RssiThreshold = value; }
    }

    public string PowerONOFF
    {
      get { return m_PowerONOFF; }
      set { m_PowerONOFF = value; }
    }
  }

  #endregion 

  public enum LightStackColor : ushort { RedOrange = 3, OrangeOnly = 2, GreenOnly = 4, RedOnly = 1, Off = 0, All = 5};
  public enum ReaderType : ushort { XPortal = 3, R420 = 2, R220 = 1 };
  public class OctaneReader
  {

    public delegate void delegateTagDetected(string ARevData,string ADelegateTagData,string AEPCTagData,double APhaseInRadians,double APeakRSSI,int AAntPort);
    public event delegateTagDetected OnDelegateTagDetected;

    public delegate void delegateGPIChangedDetected(string AGPIPort,string AStatus);
    public event delegateGPIChangedDetected OnDelegateGPIChangeDetected;

    public delegate void delegateReadingStatus(bool AOnOff);//True->ON(Reading); False->OFF(Stopped)
    public event delegateReadingStatus OnReadingChangedDetected;
    public bool m_ReadingFlag = false;
    public enum ReaderType : ushort { XPortal = 3, R420 = 2, R220 = 1};

    //private long m_keepalivePackageCount = 0;
    //public System.Threading.Timer m_CheckReaderConnection;
    public bool m_ReaderIsConnected = true;
    private LogString m_Logger = LogString.GetLogString("GateChecker");
    public PassiveReaderClass m_PassiveReader;
    private ImpinjReader m_RfidReader;
    private string m_ReaderName;
    //static int m_opIdReserved;
    //static int m_opIdTid;

    /// <summary>
    /// Returns the RFID Reader LLRP Client used in this Instance
    /// </summary>
    public ImpinjReader RFIDReader  // DM in 24-JAN-11
    {
      get { return m_RfidReader; }
    }

    public string ReaderName
    {
      get { return m_ReaderName; }
      set { m_ReaderName = value; }
    }

    public OctaneReader(PassiveReaderClass APassiveReader)
    {
        m_PassiveReader = APassiveReader;
        Initializing(m_PassiveReader.ReaderIPAddress); 
    }
    //IN JL 10-APR-12 End

    private void Initializing()
    {
      if (m_RfidReader == null)
        m_RfidReader = new ImpinjReader(); // Create an instance of LLRP reader client.

      if (m_ReaderName == "" || m_ReaderName == string.Empty)
      {
         //m_Logger.Add("ImpinjLLRPReader-Initialization:  Please Setup the Reader Name or IP");
      }
    }

    private void Initializing(string AReaderNameOrIP)
    {
      if (m_RfidReader == null)
      {
        m_ReaderName = AReaderNameOrIP;
        m_RfidReader = new ImpinjReader(); // Create an instance of LLRP reader client.
      }
    }

    /// <summary>
    /// Signals the value of the GPO Port state
    /// Port 1->Red; Port 2->Orange; Port 3->Green
    /// 0000->0  Off
    /// 1000->0001->1 RedOnly
    /// 0010->0100->4 GreenOnly    0100->4
    /// 0100->0010->2 OrangeOnly   0010->2
    /// 1100->0011->3 Red&Orange   0011->3
    /// 
    /// LightStackColor : ushort { RedOrange = 3, OrangeOnly = 2, GreenOnly = 4, RedOnly = 1 ,Off = 0}
    /// </summary>
    /// <param name="AOutputWord"></param>
    public delegate void SetReaderGPOValue(LightStackColor AOutputWord);
    public void SetReaderGPO_CallBack(LightStackColor AOutputWord)
    {

        SetReaderGPOValue d = new SetReaderGPOValue(SetReaderGPO);
        d.BeginInvoke(AOutputWord, null, null);
    }

    public void SetReaderGPO(LightStackColor AOutputWord)  // IN JL 13-MAR-13
    {
        try
        {
            if (m_RfidReader.IsConnected)
            {
                if (AOutputWord == LightStackColor.GreenOnly)
                {
                    m_RfidReader.SetGpo(1, false);
                    m_RfidReader.SetGpo(2, false);
                    m_RfidReader.SetGpo(3, true);
                    m_RfidReader.SetGpo(4, false);
                }
                else if (AOutputWord == LightStackColor.OrangeOnly)
                {
                    m_RfidReader.SetGpo(1, false);
                    m_RfidReader.SetGpo(2, true);
                    m_RfidReader.SetGpo(3, false);
                    m_RfidReader.SetGpo(4, false);
                }
                else if (AOutputWord == LightStackColor.RedOnly)
                {
                    m_RfidReader.SetGpo(1, true);
                    m_RfidReader.SetGpo(2, false);
                    m_RfidReader.SetGpo(3, false);
                    m_RfidReader.SetGpo(4, false);
                
                }
                else if (AOutputWord == LightStackColor.Off)
                {
                    m_RfidReader.SetGpo(1, false);
                    m_RfidReader.SetGpo(2, false);
                    m_RfidReader.SetGpo(3, false);
                    m_RfidReader.SetGpo(4, false);
                }
                else if (AOutputWord == LightStackColor.All)
                {
                    m_RfidReader.SetGpo(1, true);
                    m_RfidReader.SetGpo(2, true);
                    m_RfidReader.SetGpo(3, true);
                    m_RfidReader.SetGpo(4, true);
                }
            }
        }
        catch
        {
            //Do nothing...
        }
        //GC.SuppressFinalize(this);
        GC.Collect(); GC.WaitForPendingFinalizers();//IN JL 26-SEP-2012
    }

    public void StopAndDisconnect(string aHexWritingData)
    {
        EventHandlersRemoved(aHexWritingData);//IN JL 08-OCT-2013

        if (m_RfidReader.IsConnected)
            StopReading();

        SetReaderGPO(LightStackColor.Off);
        m_RfidReader.Disconnect();

       GC.Collect(); GC.WaitForPendingFinalizers();
    }

    /// <summary>
    /// Connect to the Impinj Speedway Reader
    /// </summary>
    /// <returns></returns>
    public bool Connect()
    {
      bool zRetValue = false;
      try
      {
         m_RfidReader.ConnectTimeout = 1500;//Half second connectiong time out
         m_RfidReader.Connect(m_ReaderName);
         m_ReaderIsConnected = true;
         zRetValue = true;
         //m_Logger.Add(m_PassiveReader.ReaderIPAddress.ToString()+" Connecting to reader failed: " + zTmpStr);   
      }
      catch (Exception ex)
      {
          m_ReaderIsConnected = false;
          zRetValue = false;

          m_Logger.Add("OctaneRd" + ex.Message.ToString());
      }
      return zRetValue;
    }

    /// <summary>
    /// Adds an Event Handlers associated with an Impinj Reader
    /// </summary>
    /// <param name="AReader"></param>
    public void EventHandlersAdded(string aWritingMemoryData)
    {
        m_RfidReader.AntennaChanged += new ImpinjReader.AntennaEventHandler(m_RfidReader_AntennaChanged);
        m_RfidReader.ConnectionLost += new ImpinjReader.ConnectionLostHandler(m_RfidReader_ConnectionLost);
        m_RfidReader.GpiChanged += new ImpinjReader.GpiChangedHandler(m_RfidReader_GpiChanged);
        m_RfidReader.KeepaliveReceived += new ImpinjReader.KeepaliveHandler(m_RfidReader_KeepaliveReceived);

        m_RfidReader.ReaderStarted += new ImpinjReader.ReaderStartedEventHandler(m_RfidReader_ReaderStarted);
        m_RfidReader.ReaderStopped += new ImpinjReader.ReaderStoppedEventHandler(m_RfidReader_ReaderStopped);
        m_RfidReader.ReportBufferOverflow += new ImpinjReader.ReportBufferOverflowEventHandler(m_RfidReader_ReportBufferOverflow);
        m_RfidReader.ReportBufferWarning += new ImpinjReader.ReportBufferWarningEventHandler(m_RfidReader_ReportBufferWarning);
        //m_RfidReader.TagOpComplete += new ImpinjReader.TagOpCompleteHandler(m_RfidReader_TagOpComplete);

        if(aWritingMemoryData!="")
           m_RfidReader.TagOpComplete += OnTagOpComplete;
        //m_RfidReader.TagOpComplete += m_RfidReader_TagOpComplete;//IN JL 02-MAY-2014

        m_RfidReader.TagsReported += m_RfidReader_TagsReported;
    }

    /// <summary>
    /// Removes the Event Handlers associated with an Impinj Reader
    /// </summary>
    /// <param name="AReader"></param>
    public void EventHandlersRemoved(string aWritingMemoryData)
    {
        m_RfidReader.AntennaChanged -= new ImpinjReader.AntennaEventHandler(m_RfidReader_AntennaChanged);
        m_RfidReader.ConnectionLost -= new ImpinjReader.ConnectionLostHandler(m_RfidReader_ConnectionLost);
        m_RfidReader.GpiChanged -= new ImpinjReader.GpiChangedHandler(m_RfidReader_GpiChanged);
        m_RfidReader.KeepaliveReceived -= new ImpinjReader.KeepaliveHandler(m_RfidReader_KeepaliveReceived);

        m_RfidReader.ReaderStarted -= new ImpinjReader.ReaderStartedEventHandler(m_RfidReader_ReaderStarted);
        m_RfidReader.ReaderStopped -= new ImpinjReader.ReaderStoppedEventHandler(m_RfidReader_ReaderStopped);
        m_RfidReader.ReportBufferOverflow -= new ImpinjReader.ReportBufferOverflowEventHandler(m_RfidReader_ReportBufferOverflow);
        m_RfidReader.ReportBufferWarning -= new ImpinjReader.ReportBufferWarningEventHandler(m_RfidReader_ReportBufferWarning);
        //m_RfidReader.TagOpComplete -= new ImpinjReader.TagOpCompleteHandler(m_RfidReader_TagOpComplete);

        if (aWritingMemoryData != "")
            m_RfidReader.TagOpComplete -= OnTagOpComplete;

        m_RfidReader.TagsReported -= m_RfidReader_TagsReported;
    }

    void m_RfidReader_TagsReported(ImpinjReader reader, TagReport report)
    {
        string tidData, epcData;
        double zRfDopplerFrequncy = 0;
        double zTagSeenCount = 0;
        double zPeakRSSIinDBM = 0;
        double zPhaseAngleInRadians = 0;
        int zAntPort = 0;
        // This event handler is called asynchronously 
        // when tag reports are available.
        // Loop through each tag in the report 
        // and print the data.
        foreach (Tag tag in report)
        {

            zRfDopplerFrequncy = tag.RfDopplerFrequency;
            zTagSeenCount = tag.TagSeenCount;
            zPeakRSSIinDBM = tag.PeakRssiInDbm;
            zPhaseAngleInRadians = tag.PhaseAngleInRadians;
            zAntPort = tag.AntennaPortNumber;

            // Save the EPC
            epcData = tag.Epc.ToHexString();
            tidData = tag.Tid.ToHexString();

            {
                //Console.WriteLine("EPC : {0}, TID : {1}", tag.Epc, tag.Tid);
                // Add this tag to the list of tags we've read.
                //tagsRead.Add(key, tag);
                m_Logger.Add(string.Format("EPC:{0} DopFreq (Hz):{1} PhasAng:(degree):{2} AntPort:{3} Rssi:{4} Tid: {5} RevData: {6}(Hidden)",
                           tag.Epc,
                           tag.RfDopplerFrequency.ToString("0.00"),
                           tag.PhaseAngleInRadians.ToString("0.00"),
                           tag.AntennaPortNumber.ToString(),
                           tag.PeakRssiInDbm.ToString(),
                           tag.Tid,
                           "N"));

                OnDelegateTagDetected("N", tidData, epcData, zPhaseAngleInRadians, zPeakRSSIinDBM, zAntPort);     
            }
        }
    }

    // This event handler will be called when tag 
    // operations have been executed by the reader.
    void OnTagOpComplete(ImpinjReader reader, TagOpReport report)
    {
        // Loop through all the completed tag operations
        foreach (TagOpResult result in report)
        {
            // Was this completed operation a tag write operation?
            if (result is TagWriteOpResult)
            {
                // Cast it to the correct type.
                TagWriteOpResult writeResult = result as TagWriteOpResult;
                // Print out the results.
                if (writeResult != null)
                {
                    m_Logger.AddActions("UserMemoryEncoding", "Write complete. EPC:" + writeResult.Tag.Epc + "Status:" + writeResult.Result +
                                        "Number of words written :" + writeResult.NumWordsWritten);
                }
            }
        }
    }

    //void m_RfidReader_TagOpComplete(ImpinjReader reader, TagOpReport results)
    //{
    //    string ResearvedData, tidData, epcData;
    //    double zRfDopplerFrequncy = 0;
    //    double zTagSeenCount = 0;
    //    double zPeakRSSIinDBM = 0;
    //    double zPhaseAngleInRadians = 0;
    //    int zAntPort = 0;

    //    ResearvedData = tidData = epcData = "";

    //    // Loop through all the completed tag operations
    //    foreach (TagOpResult result in results)
    //    {
    //        // Was this completed operation a tag read operation?
    //        if (result is TagReadOpResult)
    //        {
    //            // Cast it to the correct type.
    //            TagReadOpResult readResult = result as TagReadOpResult;

    //            zRfDopplerFrequncy = readResult.Tag.RfDopplerFrequency;
    //            zTagSeenCount = readResult.Tag.TagSeenCount;
    //            zPeakRSSIinDBM = readResult.Tag.PeakRssiInDbm;
    //            zPhaseAngleInRadians = readResult.Tag.PhaseAngleInRadians;
    //            zAntPort = readResult.Tag.AntennaPortNumber;

    //            // Save the EPC
    //            epcData = readResult.Tag.Epc.ToHexString();

    //            // Are these the results for User memory or TID?
    //            if (readResult.OpId == m_opIdReserved)
    //                ResearvedData = readResult.Data.ToHexString();
    //            else if (readResult.OpId == m_opIdTid)
    //                tidData = readResult.Data.ToHexString();
    //        }
    //    }

    //    string zResvedYN = "N";
    //    if (ResearvedData != "")
    //        zResvedYN = "Y";

    //    m_Logger.Add(string.Format("EPC:{0} DopFreq (Hz):{1} PhasAng:(degree):{2} AntPort:{3} Rssi:{4} Tid: {5} RevData: {6}(Hidden)",
    //                               epcData,
    //                               zRfDopplerFrequncy.ToString("0.00"),
    //                               zPhaseAngleInRadians.ToString("0.00"),
    //                               zAntPort.ToString(),
    //                               zPeakRSSIinDBM.ToString(),
    //                               tidData,
    //                               zResvedYN));

    //    OnDelegateTagDetected(ResearvedData, tidData, epcData, zPhaseAngleInRadians, zPeakRSSIinDBM, zAntPort);          
    //}

    void m_RfidReader_ReportBufferOverflow(ImpinjReader reader, ReportBufferOverflowEvent e)
    {
        m_Logger.Add("ReaderBufferOverflow:" + e.ToString()); 
    }

    void m_RfidReader_ReaderStarted(ImpinjReader reader, ReaderStartedEvent e)
    {
       //Reader Started
        m_Logger.Add("ImpinjReply -ReaderReadingStarted");

        OnReadingChangedDetected(true);
    }

    void m_RfidReader_KeepaliveReceived(ImpinjReader reader)
    {
        m_Logger.Add("KeepAlive Message Received");
        //throw new NotImplementedException();
    }

    public void m_RfidReader_GpiChanged(ImpinjReader reader, GpiEvent e)
    {
        //OUT JL 14-OCT-2013 m_Logger.Add("GpiChanged"+e.PortNumber.ToString()+e.State.ToString());

        //JL: 30-MAY-14: Move it to the front ofe. start/stop. It was after the star/stop reading.
        OnDelegateGPIChangeDetected(e.PortNumber.ToString(), e.State.ToString());

        //IN JL 30-SEP-2013:Stop using trigger AutoStart AutoSop feature to control the inventory.s
        //if (e.PortNumber == 1 || e.PortNumber == 3)
        //{
        //    if (m_ReadingFlag == false)
        //    {
        //        StartReading();
        //        //StartReading_TagEventOnly();//IN JL 22-MAY-2014
        //        m_ReadingFlag = true;
        //        m_Logger.Add("ImpinjStartingReader");
        //    }
        //}
        //else if (e.PortNumber == 2)
        //{
        //    if (m_ReadingFlag)
        //    {
        //        StopReading();
        //        //StopReading_TagEventOnly();//IN JL 22-MAY-2014 
        //        m_ReadingFlag = false;
        //        m_Logger.Add("ImpinjStoppingReader");
        //    }
        //}

        //OUT JL 29-MAY-2014: This is for 100.0.0.1 experimental version
        ////IN JL 22-MAY-2014: If Exit sensor trigger
        //if (e.PortNumber == 2)
        //{
        //    OnReadingChangedDetected(false);//IN JL 22-MAY-2014
        //}
    }

    void m_RfidReader_ConnectionLost(ImpinjReader reader)
    {
        m_Logger.Add("ConnectionLost");
        //throw new NotImplementedException();
    }

    void m_RfidReader_AntennaChanged(ImpinjReader reader, AntennaEvent e)
    {
        m_Logger.Add("ReaderAntennaChanged");
        //throw new NotImplementedException();
    }

    void m_RfidReader_ReportBufferWarning(ImpinjReader reader, ReportBufferWarningEvent e)
    {
        m_Logger.Add("ReaderReportBufferWarning");
        //throw new NotImplementedException();
    }

    void m_RfidReader_ReaderStopped(ImpinjReader reader, ReaderStoppedEvent e)
    {
        m_Logger.Add("ImpinjReply - ReaderReadingStopped");
        //JL 20SEP13: Move this one to GateCheckerReaderController, when there is no alarm occured, give it to green
        //SetReaderGPO(LightStackColor.GreenOnly);
        
        OnReadingChangedDetected(false);
    }

    void m_RfidReader_Logging(ImpinjReader reader)
    {
        m_Logger.Add("ReaderLoggingEvent");
        //throw new NotImplementedException();
    }

    public bool StartReading()
    {
      bool zRetValue = false;

      try
      {
          if (m_ReadingFlag == false)
          {
              m_RfidReader.Start();
              m_ReadingFlag = true;
          }
      }
      catch (Exception ex)//IN JL 20-MAY-2014
      {
          //logging
          m_Logger.Add("OctaneRd-StartReading" + ex.Message.ToString());
          zRetValue = false;
      }
      return zRetValue;
    }

    ///// <summary>
    ///// IN JL 22-MAY-2014
    ///// </summary>
    ///// <returns></returns>
    //public bool StartReading_TagEventOnly()
    //{
    //    bool zRetValue = false;

    //    try
    //    {
    //        if (m_ReadingFlag == false)
    //        {
    //            m_RfidReader.TagOpComplete += m_RfidReader_TagOpComplete;//IN JL 02-MAY-2014
    //            m_ReadingFlag = true;
    //        }
    //    }
    //    catch (Exception ex)//IN JL 20-MAY-2014
    //    {
    //        //logging
    //        m_Logger.Add("OctaneRd-StartReading" + ex.Message.ToString());
    //        zRetValue = false;
    //    }
    //    return zRetValue;
    //}



    public bool StopReading()
    {
      bool zRetValue = false;

      try
      {
          if (m_ReadingFlag)
          {
              m_RfidReader.Stop();
              m_ReadingFlag = false;
          }
      }
      catch (Exception ex)//IN JL 20-MAY-2014
      {
          //logging
          m_Logger.Add("OctaneRd-StopReading" + ex.Message.ToString());
          zRetValue = false;
      }

      return zRetValue;
    }

    
    ///// <summary>
    ///// IN JL 22-MAY-2014
    ///// </summary>
    ///// <returns></returns>
    //public bool StopReading_TagEventOnly()
    //{
    //    bool zRetValue = false;

    //    try
    //    {
    //        if (m_ReadingFlag)
    //        {
    //            m_RfidReader.TagOpComplete -= m_RfidReader_TagOpComplete;//IN JL 02-MAY-2014
    //            m_ReadingFlag = false;
    //        }
    //    }
    //    catch (Exception ex)//IN JL 20-MAY-2014
    //    {
    //        //logging
    //        m_Logger.Add("OctaneRd-StopReading" + ex.Message.ToString());
    //        zRetValue = false;
    //    }

    //    return zRetValue;
    //}

    /// <summary>
    /// IN JL 30-MAY-11
    /// NEW 
    /// </summary>
    /// <returns></returns>
    public bool CloseReader()
    {
      bool zRet = false;
      try
      {
         m_RfidReader.Disconnect();
         m_ReaderIsConnected = false;
      }
      catch (Exception ex)
      {
        ex.ToString();
        zRet = false;
      }
      return zRet;
    }

    /// <summary>
    /// Put the Impinj Reader back to Factory Default values
    /// </summary>
    /// <returns></returns>
    public bool FactoryDefault()
    {
      bool zRetValue = false;
      try
      {
          m_RfidReader.ApplyDefaultSettings();
          zRetValue = true;
      }
      catch (Exception ex)
      { 
        //logging
          m_Logger.Add("OctaneRd" + ex.Message.ToString());
          zRetValue = false;
      }

      return zRetValue;
    }

    //public bool SetReaderAntPower(ReaderType AReaderType, 
    //                            PassiveReaderAntClass AAnt1,
    //                            PassiveReaderAntClass AAnt2,
    //                            PassiveReaderAntClass AAnt3,
    //                            PassiveReaderAntClass AAnt4)
    //{
    //  bool zRetValue = true;
    //  try
    //  {
          
    //      // Get the default settings
    //      // We'll use these as a starting point
    //      // and then modify the settings we're 
    //      // interested in.
    //      Settings settings = m_RfidReader.QuerySettings();

    //      // Tell the reader to include the Peak RSSI
    //      // in all tag reports. Other fields can be added
    //      // to the reports in the same way by setting the 
    //      // appropriate Report.IncludeXXXXXXX property.
    //      settings.Report.IncludePeakRssi = true;
    //      settings.Report.Mode = ReportMode.Individual;
    //      settings.Report.IncludeAntennaPortNumber = true;


    //      if (AReaderType == ReaderType.R220)
    //      {
    //          settings.Antennas.GetAntenna(1).TxPowerInDbm = AAnt1.TxPower;
    //          settings.Antennas.GetAntenna(2).TxPowerInDbm = AAnt2.TxPower;

    //          settings.Antennas.GetAntenna(1).RxSensitivityInDbm = AAnt1.RssiThreshold;
    //          settings.Antennas.GetAntenna(2).RxSensitivityInDbm = AAnt2.RssiThreshold;
    //      }
    //      else if (AReaderType == ReaderType.R420 || AReaderType == ReaderType.XPortal)
    //      {
    //          settings.Antennas.GetAntenna(1).TxPowerInDbm = AAnt1.TxPower;
    //          settings.Antennas.GetAntenna(2).TxPowerInDbm = AAnt2.TxPower;
    //          settings.Antennas.GetAntenna(3).TxPowerInDbm = AAnt3.TxPower;
    //          settings.Antennas.GetAntenna(4).TxPowerInDbm = AAnt4.TxPower;

    //          settings.Antennas.GetAntenna(1).RxSensitivityInDbm = AAnt1.RssiThreshold;
    //          settings.Antennas.GetAntenna(2).RxSensitivityInDbm = AAnt2.RssiThreshold;
    //          settings.Antennas.GetAntenna(3).RxSensitivityInDbm = AAnt3.RssiThreshold;
    //          settings.Antennas.GetAntenna(4).RxSensitivityInDbm = AAnt4.RssiThreshold;
    //      }
    //          // Apply the new transmit power settings.
    //      m_RfidReader.ApplySettings(settings);
    //  }
    //  catch (Exception ex)
    //  { 
    //     //Loggging
    //      m_Logger.Add("Octane:" + ex.Message.ToString());
    //      zRetValue = false;
    //  }
    //  return zRetValue;
    //}

    /// <summary>
    /// "Gets the Reader Capabilities
    /// </summary>
    /// <returns></returns>
    public bool getReaderCapabilities(ref Impinj.OctaneSdk.Settings ASettings)
    {
      bool zRetValue = true;

      if (m_RfidReader.IsConnected)
      {
          ASettings = m_RfidReader.QueryDefaultSettings();
      }
      else
      {
          zRetValue = false;
          ASettings = null;
      }

      return zRetValue;
    }

    public bool AddGPOControl()
    {
        bool zRetValue = false;

        try
        {
            // Get the CURRENT settings
            // We'll use these as a starting point
            // and then modify the settings we're 
            // interested in.
            Settings settings = m_RfidReader.QuerySettings();
            // GPO 1 will go high when tags when tags are read.
            //settings.Gpos.GetGpo(1).Mode = GpoMode.ReaderInventoryTagsStatus;
            
            //// GPO 2 will go high when a client application connects to the reader.
            //settings.Gpos.GetGpo(2).Mode = GpoMode.LLRPConnectionStatus;
            //settings.Gpos.GetGpo(2).Mode = GpoMode.Normal;

            //// GPO 3 will pulse high for the specified period of time.
            //settings.Gpos.GetGpo(3).Mode = GpoMode.Pulsed;
            //settings.Gpos.GetGpo(3).GpoPulseDurationMsec = 1000;

            settings.Gpos.GetGpo(1).Mode = GpoMode.Normal;
            settings.Gpos.GetGpo(2).Mode = GpoMode.Normal;
            settings.Gpos.GetGpo(3).Mode = GpoMode.Normal;
            settings.Gpos.GetGpo(4).Mode = GpoMode.Normal;

            // Apply the newly modified settings.
            m_RfidReader.ApplySettings(settings);

            zRetValue = true;
        }
        catch (Exception ex)
        {
            //logging
            m_Logger.Add("Octane:" + ex.Message.ToString());
            zRetValue = false;
        }
        return zRetValue;
    }

    public bool OneTimeConfiguration(TagData AAccessPassword,
                                     ReaderType AReaderType, 
                                PassiveReaderAntClass AAnt1,
                                PassiveReaderAntClass AAnt2,
                                PassiveReaderAntClass AAnt3,
                                PassiveReaderAntClass AAnt4,
                                string AFilter1,
                                string AFilter2,
                                string aHexWritingData)
    {
        bool zRetValue = false;

        try
        {
            m_RfidReader.ApplyDefaultSettings();
            // Get the CURRENT settings
            // We'll use these as a starting point
            // and then modify the settings we're 
            // interested in.
            Settings settings = m_RfidReader.QuerySettings();
            // Tell the reader to include the antenna number
            // in all tag reports. Other fields can be added
            // to the reports in the same way by setting the 
            // appropriate Report.IncludeXXXXXXX property.
            // Tell the reader to include the Peak RSSI
            // in all tag reports. Other fields can be added
            // to the reports in the same way by setting the 
            // appropriate Report.IncludeXXXXXXX property.


            settings.Report.IncludePeakRssi = true;
            settings.Report.Mode = ReportMode.Individual;
            settings.Report.IncludeAntennaPortNumber = true;
            settings.Report.IncludeDopplerFrequency = true;
            settings.Report.IncludeSeenCount = true;
            settings.Report.IncludePeakRssi = true;
            settings.Report.IncludePhaseAngle = true;
            settings.Report.IncludeAntennaPortNumber = true;

            settings.ReaderMode = ReaderMode.AutoSetDenseReader;
            //settings.ReaderMode = ReaderMode.DenseReaderM4;
            //settings.ReaderMode = ReaderMode.MaxMiller;

            //settings.ReaderMode = ReaderMode.AutoSetDenseReader;
            settings.SearchMode = SearchMode.DualTarget;
            settings.Session = 0;
            //settings.Report.

            #region Tag Filter Section
            if (AFilter1 != "")
            {
                // Setup a tag filter.
                // Only the tags that match this filter will respond.
                // First, setup tag filter #1.
                // We want to apply the filter to the EPC memory bank.
                settings.Filters.TagFilter1.MemoryBank = MemoryBank.Epc;
                // Start matching at the third word (bit 32), since the 
                // first two words of the EPC memory bank are the
                // CRC and control bits. BitPointers.Epc is a helper
                // enumeration you can use, so you don't have to remember this.
                settings.Filters.TagFilter1.BitPointer = BitPointers.Epc;
                // Only match tags with EPCs that start with "3008"
                settings.Filters.TagFilter1.TagMask = AFilter1;
                // This filter is 16 bits long (one word).
                settings.Filters.TagFilter1.BitCount = AFilter1.Length*4;
            }
            if (AFilter2 != "")
            {
                // Setup a tag filter.
                // Only the tags that match this filter will respond.
                // First, setup tag filter #1.
                // We want to apply the filter to the EPC memory bank.
                settings.Filters.TagFilter2.MemoryBank = MemoryBank.Epc;
                // Start matching at the third word (bit 32), since the 
                // first two words of the EPC memory bank are the
                // CRC and control bits. BitPointers.Epc is a helper
                // enumeration you can use, so you don't have to remember this.
                settings.Filters.TagFilter2.BitPointer = BitPointers.Epc;
                // Only match tags with EPCs that start with "3008"
                settings.Filters.TagFilter2.TagMask = AFilter2;
                // This filter is 16 bits long (one word).
                settings.Filters.TagFilter2.BitCount = AFilter2.Length*4;
            }

            if (AFilter1 != "" && AFilter2 != "")
            {
                settings.Filters.Mode = TagFilterMode.Filter1OrFilter2;
            }
            if (AFilter1 != "" && AFilter2 == "")
            {
                settings.Filters.Mode = TagFilterMode.OnlyFilter1;
            }
            if (AFilter1 == "" && AFilter2 != "")
            {
                settings.Filters.Mode = TagFilterMode.OnlyFilter2;
            }
            if (AFilter1 == "" && AFilter2 == "")
            {
                settings.Filters.Mode = TagFilterMode.None;
            }



            #endregion


            //Set FastId Feature
            #region FastId
            // Tell the reader to include the TID
            // in all tag reports. We will use FastID
            // to do this. FastID is supported
            // by Impinj Monza 4 and later tags.
            //settings.Report.IncludeFastId = true;
            settings.Report.IncludeFastId = true;//Changed 02-FEB-14
            #endregion 

            //SetAntenna power
            #region AntPower
            if (AReaderType == ReaderType.R220)
            {
                settings.Antennas.GetAntenna(1).TxPowerInDbm = AAnt1.TxPower;
                settings.Antennas.GetAntenna(2).TxPowerInDbm = AAnt2.TxPower;

                settings.Antennas.GetAntenna(1).RxSensitivityInDbm = AAnt1.RssiThreshold;
                settings.Antennas.GetAntenna(2).RxSensitivityInDbm = AAnt2.RssiThreshold;
            }
            else if (AReaderType == ReaderType.R420 || AReaderType == ReaderType.XPortal)
            {
                settings.Antennas.GetAntenna(1).TxPowerInDbm = AAnt1.TxPower;
                settings.Antennas.GetAntenna(2).TxPowerInDbm = AAnt2.TxPower;
                settings.Antennas.GetAntenna(3).TxPowerInDbm = AAnt3.TxPower;
                settings.Antennas.GetAntenna(4).TxPowerInDbm = AAnt4.TxPower;

                settings.Antennas.GetAntenna(1).RxSensitivityInDbm = AAnt1.RssiThreshold;
                settings.Antennas.GetAntenna(2).RxSensitivityInDbm = AAnt2.RssiThreshold;
                settings.Antennas.GetAntenna(3).RxSensitivityInDbm = AAnt3.RssiThreshold;
                settings.Antennas.GetAntenna(4).RxSensitivityInDbm = AAnt4.RssiThreshold;
            }
            #endregion

            //AddGPITrigger
            #region AddGPITrigger
            // Start reading tags when GPI #1 goes low.
            settings.Gpis.GetGpi(1).IsEnabled = true;
            settings.Gpis.GetGpi(1).DebounceInMs = 20;
            settings.AutoStart.Mode = AutoStartMode.GpiTrigger;
            settings.AutoStart.GpiPortNumber = 1;
            settings.AutoStart.GpiLevel = true;
            settings.AutoStop.Mode = AutoStopMode.GpiTrigger;
            settings.AutoStop.GpiPortNumber = 1;
            settings.AutoStop.GpiLevel = false;

            ////V1.0.0.2
            ////IN JL 30-SEP-2013: Second trigger sensor added
            //// Start reading tags when GPI #3 goes low.
            //settings.Gpis.GetGpi(3).IsEnabled = true;
            //settings.Gpis.GetGpi(3).DebounceInMs = 50;
            //settings.AutoStart.Mode = AutoStartMode.None;
            //settings.AutoStart.GpiPortNumber = 3;
            //settings.AutoStart.GpiLevel = false;

            //// Stop reading tags when GPI #1 goes high.
            //settings.AutoStop.Mode = AutoStopMode.None;
            ////settings.AutoStop.GpiPortNumber = 1;
            ////settings.AutoStop.GpiLevel = true;
            //settings.Gpis.GetGpi(2).IsEnabled = true;
            //settings.Gpis.GetGpi(2).DebounceInMs = 50;
            //settings.AutoStop.GpiPortNumber = 2;
            //settings.AutoStop.GpiLevel = false;

            #endregion

            //AddGPOControll
            #region GPOControl
            // Get the CURRENT settings
            // We'll use these as a starting point
            // and then modify the settings we're 
            // interested in.
            // GPO 1 will go high when tags when tags are read.
            //settings.Gpos.GetGpo(1).Mode = GpoMode.ReaderInventoryTagsStatus;

            //// GPO 2 will go high when a client application connects to the reader.
            //settings.Gpos.GetGpo(2).Mode = GpoMode.LLRPConnectionStatus;
            //settings.Gpos.GetGpo(2).Mode = GpoMode.Normal;

            //// GPO 3 will pulse high for the specified period of time.
            //settings.Gpos.GetGpo(3).Mode = GpoMode.Pulsed;
            //settings.Gpos.GetGpo(3).GpoPulseDurationMsec = 1000;

            settings.Gpos.GetGpo(1).Mode = GpoMode.Normal;
            settings.Gpos.GetGpo(2).Mode = GpoMode.Normal;
            settings.Gpos.GetGpo(3).Mode = GpoMode.Normal;
            settings.Gpos.GetGpo(4).Mode = GpoMode.Normal;
            #endregion

            // Apply the newly modified settings.
            m_RfidReader.ApplySettings(settings);

            //WriteOperationSequence
            #region ReadOperationSequence
            //Writng the memory
            if (aHexWritingData != "")
            {
                // Create a tag operation sequence.
                // You can add multiple read, write, lock, kill and QT
                // operations to this sequence.
                TagOpSequence seq = new TagOpSequence();

                // Specify a target tag based on the EPC.
                seq.TargetTag.MemoryBank = MemoryBank.Epc;
                seq.TargetTag.BitPointer = BitPointers.Epc;
                // Setting this to null will specify any tag.
                // Replace this line with the one below it to target a particular tag.
                seq.TargetTag.Data = null;
                //seq.TargetTag.Data = "11112222333344445555666677778888";

                // If you are using Monza 4, Monza 5 or Monza X tag chips,
                // uncomment these two lines. This enables 32-bit block writes
                // which significantly improves write performance.
                seq.BlockWriteEnabled = true;
                seq.BlockWriteWordCount = 2;

                // Create a tag write operation.
                TagWriteOp writeOp = new TagWriteOp();
                // Write to user memory
                writeOp.MemoryBank = MemoryBank.User;
                // Write two (16-bit) words
                writeOp.Data = TagData.FromHexString(aHexWritingData);
                // Starting at word 0
                writeOp.WordPointer = 0;

                // Add this tag write op to the tag operation sequence.
                seq.Ops.Add(writeOp);

                // Add the tag operation sequence to the reader.
                // The reader supports multiple sequences.
                m_RfidReader.AddOpSequence(seq);
            }

            //// Create a tag read operation for User memory.
            //TagReadOp readReserved= new TagReadOp();
            //if (AAccessPassword != null)
            //    readReserved.AccessPassword = AAccessPassword;
            //// Read from user memory
            //readReserved.MemoryBank = MemoryBank.Reserved;
            //// Read two (16-bit) words
            //readReserved.WordCount = 4;
            //// Starting at word 0
            //readReserved.WordPointer = 0;

            //// Create a tag read operation for TID memory.
            //TagReadOp readTid = new TagReadOp();

            //if (AAccessPassword != null)
            //    readTid.AccessPassword = AAccessPassword;

            //// Read from TID memory
            //readTid.MemoryBank = MemoryBank.Tid;
            //// Read two (16-bit) words
            //readTid.WordCount = 6;
            //// Starting at word 0
            //readTid.WordPointer = 0;

            //// Add these operations to the reader as Optimized Read ops.
            //// Optimized Read ops apply to all tags, unlike 
            //// Tag Operation Sequences, which can be applied to specific tags.
            //// Speedway Revolution supports up to two Optimized Read operations.
            //settings.Report.OptimizedReadOps.Add(readReserved);
            //settings.Report.OptimizedReadOps.Add(readTid);

            //// Store the operation IDs for later.
            //m_opIdReserved = readReserved.Id;
            //m_opIdTid = readTid.Id;
            #endregion

            zRetValue = true;
        }
        catch (Exception ex)
        {
            //logging
            m_Logger.Add("Octane:" + ex.Message.ToString());
            zRetValue = false;
        }
        return zRetValue;
    }

    //public bool AddReadOperation(TagData AAccessPassword)
    //{
    //  bool zRetValue = false;

    //  try
    //  {
    //      // Get the default settings
    //      // We'll use these as a starting point
    //      // and then modify the settings we're 
    //      // interested in.

    //      Settings settings = m_RfidReader.QuerySettings();

    //      // Create a tag read operation for User memory.
    //      TagReadOp readUser = new TagReadOp();

    //      if (AAccessPassword != null)
    //          readUser.AccessPassword = AAccessPassword;

    //      // Read from user memory
    //      readUser.MemoryBank = MemoryBank.User;
    //      // Read two (16-bit) words
    //      readUser.WordCount = 2;
    //      // Starting at word 0
    //      readUser.WordPointer = 0;

    //      // Create a tag read operation for TID memory.
    //      TagReadOp readTid = new TagReadOp();

    //      if (AAccessPassword != null)
    //          readTid.AccessPassword = AAccessPassword;

    //      // Read from TID memory
    //      readTid.MemoryBank = MemoryBank.Tid;
    //      // Read two (16-bit) words
    //      readTid.WordCount = 2;
    //      // Starting at word 0
    //      readTid.WordPointer = 0;

    //      // Add these operations to the reader as Optimized Read ops.
    //      // Optimized Read ops apply to all tags, unlike 
    //      // Tag Operation Sequences, which can be applied to specific tags.
    //      // Speedway Revolution supports up to two Optimized Read operations.
    //      //settings.Report.OptimizedReadOps.Add(readUser);
    //      settings.Report.OptimizedReadOps.Add(readTid);

    //      // Store the operation IDs for later.
    //      //m_opIdUser = readUser.Id;
    //      m_opIdTid= readTid.Id;

    //      // Apply the newly modified settings.
    //      m_RfidReader.ApplySettings(settings);
    //      zRetValue = true;
    //  }
    //  catch (Exception ex)
    //  { 
    //      //logging
    //      m_Logger.Add("Octane:" + ex.Message.ToString());
    //      zRetValue = false;
    //  }

    //  return zRetValue;
    //}

    //public bool AddFastIdFeature()
    //{
    //    bool zRetValue = false;

    //    try
    //    {
    //        // Get the default settings
    //        // We'll use these as a starting point
    //        // and then modify the settings we're 
    //        // interested in.

    //        Settings settings = m_RfidReader.QuerySettings();

    //        // Tell the reader to include the TID
    //        // in all tag reports. We will use FastID
    //        // to do this. FastID is supported
    //        // by Impinj Monza 4 and later tags.
    //        settings.Report.IncludeFastId = true;

    //        // Apply the newly modified settings.
    //        m_RfidReader.ApplySettings(settings);
    //        zRetValue = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        //logging
    //        m_Logger.Add("OctaneRd" + ex.Message.ToString());
    //        zRetValue = false;
    //    }

    //    return zRetValue;
    //}

    //public bool AddTxFrequencyFeature()
    //{
    //    bool zRetValue = false;

    //    try
    //    {
          
    //        // Get the reader features to determine if the 
    //        // reader supports a fixed-frequency table.
    //        FeatureSet features = m_RfidReader.QueryFeatureSet();

    //        if (!features.IsHoppingRegion)
    //        {

    //            // Get the default settings
    //            // We'll use these as a starting point
    //            // and then modify the settings we're 
    //            // interested in.
    //            Settings settings = m_RfidReader.QuerySettings();

    //            //// Tell the reader to include the antenna number
    //            //// in all tag reports. Other fields can be added
    //            //// to the reports in the same way by setting the 
    //            //// appropriate Report.IncludeXXXXXXX property.
    //            //settings.Report.IncludeAntennaPortNumber = true;

    //            //// Send a tag report for every tag read.
    //            //settings.Report.Mode = ReportMode.Individual;

    //            // Specify the transmit frequencies to use.
    //            // Make sure your reader supports this and
    //            // that the frequencies are valid for your region.
    //            // The following example is for ETSI (Europe) 
    //            // readers.
    //            List<double> freqList = new List<double>();
    //            freqList.Add(865.7);
    //            freqList.Add(866.3);
    //            freqList.Add(866.9);
    //            freqList.Add(867.5);
    //            settings.TxFrequenciesInMhz = freqList;

    //            // Apply the newly modified settings.
    //            m_RfidReader.ApplySettings(settings);

    //        }
    //        else
    //        {
    //            Console.WriteLine("This reader does not allow the transmit frequencies to be specified.");
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        //logging
    //        m_Logger.Add("OctaneRd" + ex.Message.ToString());
    //        zRetValue = false;
    //    }

    //    return zRetValue;
    //}
  }

  public class ProjectConfiguration
  {
      public int Rd_CheckReaderConnection_ms = 5000;
      public int Rd_PingCounter = 5;
      public int Rd_PingReaderTimer_ms = 1000;
      public bool Rd_ReconnectFlag = true;
      public string Pcs_ImageFilePath = @"C:\PCSFileshare\ResizedImages";
      public string Sys_DBConnectionStr = "";
      public int Sys_CheckGateConfigurationChanges_ms = 10000;//Modified JL 29-MAY-14 20000 to 10000
      public bool Sys_AllowRawTagsFlag = true;
      public bool Sys_AutoStart = true;
      public string Sys_InvalidTagDefaultValue = "999999999999999999999999";

      public int PicDelegate_Width = 235;//IN JL 11-DEC-2013 
      public int PicDelegate_Height = 294;//IN JL 11-DEC-2013 
      public int PicDefault_Width = 404;//IN JL 11-DEC-2013 
      public int PicDefault_Height = 300;//IN JL 11-DEC-2013 

      public int PicDefault_Loc_X = 255;//IN JL 11-DEC-2013
      public int PicDefault_Loc_Y = 31;//IN JL 11-DEC-2013
      public int PicDelegate_Loc_X = 354;//IN JL 11-DEC-2013
      public int PicDelegate_Loc_Y = 40;//IN JL 11-DEC-2013

      public int Sys_DefaultImageTimer_ms = 3000;//Modified JL 29-MAY-14 5000 to 3000

      public string TagFilter1 = "";
      public string TagFilter2 = "";

      public ProjectConfiguration(string AEPCFilter1, string AEPCFilter2)
      {
          TagFilter1 = AEPCFilter1;
          TagFilter2 = AEPCFilter2;

      }
  }
}
