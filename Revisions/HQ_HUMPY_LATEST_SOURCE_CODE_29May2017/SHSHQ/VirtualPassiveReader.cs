using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.NetworkInformation;

using Impinj.OctaneSdk;


namespace SHSHQ
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

        public PassivePortalClass(List<PassiveReaderClass> APassiveReaderList, int APortalID)
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
        private ReaderType m_ReaderType = ReaderType.R420;

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

        public ReaderType ReaderType
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

    //public enum LightStackColor : ushort { RedOrange = 3, OrangeOnly = 2, GreenOnly = 4, RedOnly = 1, Off = 0, All = 5};
    public enum ReaderType : ushort { XPortal = 3, R420 = 2, R220 = 1 };


    public class GateDetail
    {

        public PassiveReaderClass GateReader = new PassiveReaderClass();

        public string AccessPwd = "";
        public string GateID = "";
        public string gateDescription = "";
        public string FK_privilegeID = "";
        public string embeddedDNS = "";//??
        public string embeddedIP = "";//??

        public string readerType = "";
        public string readerName = "";
        public string readerDNS = "";
        public string readerIP = "";
        public string ant1Power = "";
        public string ant1Sensitivity = "";
        public string ant2Power = "";
        public string ant2Sensitivity = "";
        public string ant3Power = "";
        public string ant3Sensitivity = "";
        public string ant4Power = "";
        public string ant4Sensitivity = "";

        public string stateOnOff = "";

        public string Rd_Filter1 = "";
        public string Rd_Filter2 = "";
        public ReaderMode rdReaderMode = ReaderMode.AutoSetDenseReader;//IN JL 25-APR-2016
        public SearchMode rdSearchMode = SearchMode.DualTarget;//IN JL 25-APR-2016
        public ushort rdSessionNum = 0;//IN JL 25-APR-2016
    }

    public class HumpyDetail
    {
        public GateDetail HumpyGate = new GateDetail();//UHF Reader

        public string Sys_DBConnection = "";
        public string Sys_DB_Datasource = "";
        public string Sys_DB_InitialCatalog = "";
        public string Sys_DB_UserID = "";
        public string Sys_DB_Password = "";
        public string Sys_DB_MaxPoolSize = "";
        public string Sys_HumpyId = "";

        //HF Reader
        public string Rd_HFReader_COM = "";
        public string Rd_HFReader_Baudrate = "";

        //GUI Retrieving 
        public int Sys_RetrieveTimer_ms = 200;
        public int Sys_TagReadSensitivity_ms = 800;
        public int CheckInOutWaitingTime_ms = 3000;
        public string Sys_HumpyGroupName = "";
        public int FullStatusReportTimer_ms = 600000;
        public float Sys_TagINsensitivity_ms = 2000;

        public bool Sys_DBorLocalLogging = true;
        public string Sys_password = "";
    }

    public class SysParams
    {
        public string ID = "";
        public string paramName = "";
        public string paramValue = "";
        public string AcsDNS = "";//??
        public string AcsIP = "";//??
        public string photoPath = "";//?
        public string TagPwd = "";
    }

    public class GateTransaction
    {
        public string ID = "";
        public string FK_RFID_TagID = "";
        public string FK_GateID = "";
        public string transDateTime = "";
    }

    public class ErrorLog
    {
        public string ID = "";
        public string errMsg = "";//DT-RIFDTagID-GateID-DetailMessage
        public string errCode = "1";
        public string errCat = "";//ERR-PORTAL
        public string errSrc = "";
        public string occurredAt = "";//DT?
    }




}
