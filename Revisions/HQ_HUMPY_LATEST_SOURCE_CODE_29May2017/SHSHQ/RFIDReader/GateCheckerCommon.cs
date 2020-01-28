using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;
using System.IO;
using System.Drawing;

namespace ARC.RFID.Direct
{
    public enum DirectReaderControMode_Function {Palletising,Boxing}

    public enum LasterSensorState : ushort {Backwalking = 3,Tailgating =2, Triggered = 1, Idle = 0}; 
   
    public class LaserSensor
    {
        public LasterSensorState m_SensorState = LasterSensorState.Idle;
        public int m_SensorCounter = 0;

        public void ResetSensorState()
        {
            m_SensorState = LasterSensorState.Idle;
            m_SensorCounter = 0;
        }
    }

    /// <summary>
    /// V1.0.0.2
    /// 30-SEP-2013 JL: Added another sensor
    /// Sensor1 is to trigger the start reading
    /// Sensor3 is to trigger the start reading as well, and also, it is used to test the tailgating
    /// Sensor2 is to trigger the stop reading
    /// </summary>
    public class GateCheckerSensors
    {
        LaserSensor m_Sensor1 = new LaserSensor();//trigger start
        LaserSensor m_Sensor3 = new LaserSensor();//trigger start
        LaserSensor m_Sensor2 = new LaserSensor();//trigger stop
      

        List<LaserSensor> m_SensorSequenceList = new List<LaserSensor>();

        public LasterSensorState GetCurrntSensorState(string ASensorNum)
        {
            if (ASensorNum == "1")
                return m_Sensor1.m_SensorState;
            else if (ASensorNum == "2")
                return m_Sensor2.m_SensorState;
            else if (ASensorNum == "3")
                return m_Sensor3.m_SensorState;
            else
                return LasterSensorState.Idle;
        }


        public bool TriggerSensor(string ASensorNum)
        {
            bool zRet = true;
            if (ASensorNum == "1")
            {
                m_Sensor1.m_SensorCounter++;
                m_SensorSequenceList.Add(m_Sensor1);

                //IN JL 01-OCT-2013
                //Exit sensor been triggered before first 2 sensors triggered, reset it
                if (m_Sensor2.m_SensorState == LasterSensorState.Triggered)
                    m_Sensor2.m_SensorState = LasterSensorState.Idle;

                if(m_Sensor1.m_SensorState == LasterSensorState.Idle)
                   m_Sensor1.m_SensorState = LasterSensorState.Triggered;

                if (m_Sensor1.m_SensorCounter >= 4
                    && m_Sensor3.m_SensorCounter >= 4)
                {
                    m_Sensor1.m_SensorState = LasterSensorState.Tailgating;
                    m_Sensor3.m_SensorState = LasterSensorState.Tailgating;
                    zRet = false;
                }
            }
            else if (ASensorNum == "3")
            {
                m_Sensor3.m_SensorCounter++;
                m_SensorSequenceList.Add(m_Sensor3);

                //IN JL 01-OCT-2013
                //Exit sensor been triggered before first 2 sensors triggered, reset it
                if (m_Sensor2.m_SensorState == LasterSensorState.Triggered)
                    m_Sensor2.m_SensorState = LasterSensorState.Idle;

                if (m_Sensor3.m_SensorState == LasterSensorState.Idle)
                   m_Sensor3.m_SensorState = LasterSensorState.Triggered;

                if (m_Sensor1.m_SensorCounter >= 4
               && m_Sensor3.m_SensorCounter >= 4)
                {
                    m_Sensor1.m_SensorState = LasterSensorState.Tailgating;
                    m_Sensor3.m_SensorState = LasterSensorState.Tailgating;
                    zRet = false;
                }
            }
            else if (ASensorNum == "2")
            {
                m_Sensor2.m_SensorCounter++;
                m_SensorSequenceList.Add(m_Sensor2);

                if (m_Sensor2.m_SensorState == LasterSensorState.Idle)
                  m_Sensor2.m_SensorState = LasterSensorState.Triggered;

                //if ((m_Sensor1.m_SensorState == LasterSensorState.Idle ||
                //    m_Sensor3.m_SensorState == LasterSensorState.Idle)
                //    && m_Sensor2.m_SensorState == LasterSensorState.Triggered)
                //{
                //    m_Sensor1.m_SensorState = LasterSensorState.Backwalking;
                //    m_Sensor2.m_SensorState = LasterSensorState.Backwalking;
                //    m_Sensor3.m_SensorState = LasterSensorState.Backwalking;
                //    zRet = false;
                //}
            }
            return zRet;
        }

        public void ResetSensors()
        {
            m_SensorSequenceList.Clear();
            m_Sensor1.ResetSensorState();
            m_Sensor2.ResetSensorState();
            m_Sensor3.ResetSensorState();//V1.0.0.2
        }
    }


    ///// <summary>
    ///// V1.0.0.1
    ///// Sensor1 is to trigger the start reading
    ///// Sensor2 is to trigger the stop reading
    ///// </summary>
    //public class GateCheckerSensors
    //{
    //     LaserSensor m_Sensor1 = new LaserSensor();
    //     LaserSensor m_Sensor2 = new LaserSensor();

    //     List<LaserSensor> m_SensorSequenceList = new List<LaserSensor>();

    //     public LasterSensorState GetCurrntSensorState(string ASensorNum)
    //     {
    //         if (ASensorNum == "1")
    //             return m_Sensor1.m_SensorState;
    //         else if (ASensorNum == "2")
    //             return m_Sensor2.m_SensorState;
    //         else
    //             return LasterSensorState.Idle;
    //     }


    //    public bool TriggerSensor(string ASensorNum)
    //    {
    //        bool zRet = true;
    //        if (ASensorNum == "1")
    //        {
    //            m_Sensor1.m_SensorCounter++;
    //            m_SensorSequenceList.Add(m_Sensor1);


    //            //Normal
    //            if (m_Sensor1.m_SensorState == LasterSensorState.Idle
    //                && m_Sensor2.m_SensorState == LasterSensorState.Idle)
    //            {
    //                m_Sensor1.m_SensorState = LasterSensorState.Triggered;
    //            }
    //            //Tailgating: before reset, sensor 1 triggered again
    //            else if (m_Sensor1.m_SensorState == LasterSensorState.Triggered)
    //            {
    //                m_Sensor1.m_SensorState = LasterSensorState.Tailgating;
    //                m_Sensor2.m_SensorState = LasterSensorState.Tailgating;
    //                zRet = false;
    //            }
    //        }
    //        else if (ASensorNum == "2")
    //        {
    //            m_Sensor2.m_SensorCounter++;
    //            m_SensorSequenceList.Add(m_Sensor2);


    //            //Normal Scenario
    //            if (m_Sensor1.m_SensorState == LasterSensorState.Triggered
    //                && m_Sensor2.m_SensorState == LasterSensorState.Idle)
    //            {
    //                m_Sensor2.m_SensorState = LasterSensorState.Triggered;
    //            }
    //            ////Backwalking: trigger1 not triggerd before, but trigger2 triggerd.
    //            //else if (m_Sensor1.m_SensorState == LasterSensorState.Idle
    //            //    && m_Sensor2.m_SensorState == LasterSensorState.Idle)
    //            //{
    //            //    m_Sensor1.m_SensorState = LasterSensorState.Backwalking;
    //            //    m_Sensor2.m_SensorState = LasterSensorState.Backwalking;
    //            //    zRet = false;
    //            //}
    //        }
    //        return zRet;
    //    }

    //    public void ResetSensors()
    //    {
    //        m_SensorSequenceList.Clear();
    //        m_Sensor1.ResetSensorState();
    //        m_Sensor2.ResetSensorState();
    //    }
    //}


    public class DelegateDetail
    {
        public string ID = "";
        public string passType = "";
        public string accreditationID = "";
        public string RFID_TagID = "";
        public string firstName = "";
        public string lastName = "";
        public string orgName = "";
        public string category = "";
        public string photoFile = "";
        public string photoFileHash = "";
        public string photoPath = "";
        public Image photoImage = null;
        public string passStatus = "";
        public string activeFrom = "";
        public string activeTo = "";
        public string FK_StagingID = "";

        public List<int> PrivilegeIDs = new List<int>();
    }

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
