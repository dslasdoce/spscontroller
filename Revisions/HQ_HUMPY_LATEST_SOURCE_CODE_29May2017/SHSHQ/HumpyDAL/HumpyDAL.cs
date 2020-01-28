
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using Impinj;
using SHSHQ;//JL: it needs to be taken out in the SmartHumpyController and HQtoHumpyMon

namespace SmartHumpyController
{
    public class HumpyDAL
    {
        #region "Constructor and Common Function "
        
        private SqlConnection m_Connection = null;
        public SqlConnection CurrentSession       
        {
            get { return m_Connection; }
        }

        public HumpyDAL(string AConnectionString)
        {
            m_Connection = new SqlConnection(AConnectionString);
        }
        private SqlCommand OpenStoredProcedure(string ASPName)
        {
            SqlCommand zSQLCmd;
            try
            {
                if (m_Connection.State == ConnectionState.Closed)
                    m_Connection.Open();
                zSQLCmd = new SqlCommand(ASPName, m_Connection);
                zSQLCmd.CommandType = System.Data.CommandType.StoredProcedure;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // In just to keep the system operating when at fault
            }
            return zSQLCmd;
        }
        /// <summary>
        /// Allows the program to execute an SQl statement directly      
        /// without relying on a Stored Procedure        
        /// </summary>
        /// <param name="ACommandText"></param>
        /// <returns></returns>
        private SqlCommand OpenSqlQuery(string ACommandText)
        {
            SqlCommand zSqlCmd;
            try
            {
                if (m_Connection.State == ConnectionState.Closed)
                    m_Connection.Open();
                zSqlCmd = new SqlCommand(ACommandText, m_Connection);
                zSqlCmd.CommandType = System.Data.CommandType.Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
            return zSqlCmd;
        }

        #endregion

        #region "Stored Procedures - Dispense Tag List, Collect Tag List"

        /// <summary>
        /// Dispense Mode (Pickup)
        /// Get valid/registered tag information in last X seconds
        /// </summary>
        /// <param name="AMode"></param>
        /// <returns></returns>
        public DataSet GetPickupTagList(int ATimeFrame_s)
        {
            DataSet zDataSet = null;
            try
            {

                SqlCommand zSQLCmd = OpenStoredProcedure("BDSsp_PickupTagList");
                if (zSQLCmd != null)
                {
                    zSQLCmd.Parameters.Add("@ATimeFrame", SqlDbType.Int).Value = ATimeFrame_s;
                    SqlDataAdapter zReader = new SqlDataAdapter(zSQLCmd);
                    zDataSet = new DataSet();
                    zReader.Fill(zDataSet);
                    m_Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_Connection.Close();
            }
            return zDataSet;
        }


        /// <summary>
        /// Get all workers tag information in last X seconds
        /// </summary>
        /// <param name="AMode"></param>
        /// <returns></returns>
        public DataSet SHS_GetWorkersAll(string aGroupName)
        {
            DataSet zDataSet = null;
            try
            {

                SqlCommand zSQLCmd = OpenStoredProcedure("BDSsp_CheckedInWorkerList");
                if (zSQLCmd != null)
                {

                    zSQLCmd.Parameters.Add("@AGroupName", SqlDbType.VarChar,50).Value = aGroupName;
                    SqlDataAdapter zReader = new SqlDataAdapter(zSQLCmd);
                    zDataSet = new DataSet();
                    zReader.Fill(zDataSet);
                    m_Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_Connection.Close();
            }
            return zDataSet;
        }

        /// <summary>
        /// Get all workers tag information in last X seconds
        /// </summary>
        /// <param name="AMode"></param>
        /// <returns></returns>
        public DataSet SHS_GetWorkersAll(float ATimeFrameX_s,float ATimeFrameY_s)
        {
            DataSet zDataSet = null;
            try
            {

                SqlCommand zSQLCmd = OpenStoredProcedure("BDSsp_WorkerTagList");
                if (zSQLCmd != null)
                {

                    zSQLCmd.Parameters.Add("@ATimeFrameX", SqlDbType.Float).Value = ATimeFrameX_s;//Modified JL 19-01-16
                    zSQLCmd.Parameters.Add("@ATimeFrameY", SqlDbType.Float).Value = ATimeFrameY_s;//IN JL 19-01-16
                    SqlDataAdapter zReader = new SqlDataAdapter(zSQLCmd);
                    zDataSet = new DataSet();
                    zReader.Fill(zDataSet);
                    m_Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_Connection.Close();
            }
            return zDataSet;
        }


        public DataSet SHS_HQGetWorkersAll(float ATimeFrameX_s, float ATimeFrameY_s)
        {
            DataSet zDataSet = null;
            try
            {

                SqlCommand zSQLCmd = OpenStoredProcedure("BDSsp_HQWorkerTagList");
                if (zSQLCmd != null)
                {

                    zSQLCmd.Parameters.Add("@ATimeFrameX", SqlDbType.Float).Value = ATimeFrameX_s;//Modified JL 19-01-16
                    zSQLCmd.Parameters.Add("@ATimeFrameY", SqlDbType.Float).Value = ATimeFrameY_s;//IN JL 19-01-16

                    //zSQLCmd.CommandTimeout = aConnectionTimeout;//IN JL 27-MAY-2016

                    SqlDataAdapter zReader = new SqlDataAdapter(zSQLCmd);
                    zDataSet = new DataSet();
                    zReader.Fill(zDataSet);
                    m_Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_Connection.Close();
            }
            return zDataSet;
        }

        #endregion


        #region Inputs Table Actions
        public int SHS_InsertInputsRecord(string TIDP,
                                          string PeakRSSI,
                                          string AntennaID,
                                          string LocId,
                                          float ATimeFrameY_s)
        {
            int zRet = 0;
            try
            {
                //m_Connection.Close();
                SqlCommand zSQLCmd = OpenStoredProcedure("BDSsp_InsertInputs");

                if (zSQLCmd != null)
                {
                    zSQLCmd.Parameters.Add("@TIDP", SqlDbType.VarChar, 50).Value = TIDP;
                    zSQLCmd.Parameters.Add("@PeakRSSI", SqlDbType.VarChar, 50).Value = PeakRSSI;
                    zSQLCmd.Parameters.Add("@AntennaID", SqlDbType.VarChar, 50).Value = AntennaID;
                    zSQLCmd.Parameters.Add("@LocId", SqlDbType.VarChar, 50).Value = LocId;
                    zSQLCmd.Parameters.Add("@ATimeFrameY", SqlDbType.Float).Value = ATimeFrameY_s;//IN JL 19-01-16
                    int zRetInt = Convert.ToInt32(zSQLCmd.ExecuteScalar());
                    m_Connection.Close();

                    if (zRetInt == 0)
                    {
                        zRet = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_Connection.Close();
            }
            return zRet;
        }


        /// <summary>
        /// Delete everything from Inputs Table
        /// </summary>
        /// <param name="AMedBarcode"></param>
        /// <param name="AMedInstance"></param>
        /// <returns></returns>
        public bool SHS_Clear_InputsTable()
        {
            bool zRet = false;
            try
            {
                string zSqlStr = " DELETE FROM Inputs ";

                SqlCommand zSqlCmd = OpenSqlQuery(zSqlStr);
                int zRetInt = Convert.ToInt32(zSqlCmd.ExecuteScalar());
                m_Connection.Close();
                if (zRetInt == 0)
                {
                    zRet = true;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return zRet;
        }
        #endregion

        #region Log Table Actions
        public bool SHS_Insert_Log(SHS_LogType AJt, string ADescription, string ACreatedBy,string HumpyID)
        {
            bool zRet = false;
            try
            {
                //string zSqlStr = "  INSERT INTO History (JournalType,Description,CreatedDT) " +
                //                 "  VALUES ('" + AJt.ToString() + "','" + ADescription + "', GETDATE()) ";

                string zSqlStr = "  INSERT INTO Log (JournalType,Description,CreatedBy,CreatedDT,HumpyID) " +
                           "  VALUES ('" + AJt.ToString() + "','" + ADescription + "','" + ACreatedBy + "', GETDATE() ,'" + HumpyID + "')";

                SqlCommand zSqlCmd = OpenSqlQuery(zSqlStr);
                int zRetInt = Convert.ToInt32(zSqlCmd.ExecuteScalar());
                m_Connection.Close();
                if (zRetInt == 0)
                {
                    zRet = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //ex.ToString();
            }
            return zRet;
        }
        #endregion

        #region History Table Actions
        /// <summary>
        /// Insert record into History table
        /// </summary>
        /// <param name="AMedBarcode"></param>
        /// <param name="AMedInstance"></param>
        /// <returns></returns>
        public bool SHS_Insert_History(SHS_JournalType AJt,string ADescription,string ACreatedBy)
        {
            bool zRet = false;
            try
            {
                //string zSqlStr = "  INSERT INTO History (JournalType,Description,CreatedDT) " +
                //                 "  VALUES ('" + AJt.ToString() + "','" + ADescription + "', GETDATE()) ";

                string zSqlStr = "  INSERT INTO History (JournalType,Description,CreatedBy,CreatedDT) " +
                           "  VALUES ('" + AJt.ToString() + "','" + ADescription + "','" + ACreatedBy + "', GETDATE()) ";

                SqlCommand zSqlCmd = OpenSqlQuery(zSqlStr);
                int zRetInt = Convert.ToInt32(zSqlCmd.ExecuteScalar());
                m_Connection.Close();
                if (zRetInt == 0)
                {
                    zRet = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //ex.ToString();
            }
            return zRet;
        }

        /// <summary>
        /// Delete everything from History Table
        /// </summary>
        /// <param name="AMedBarcode"></param>
        /// <param name="AMedInstance"></param>
        /// <returns></returns>
        public bool SHS_Clear_History()
        {
            bool zRet = false;
            try
            {
                string zSqlStr = "  DELETE FROM History ";

                SqlCommand zSqlCmd = OpenSqlQuery(zSqlStr);
                int zRetInt = Convert.ToInt32(zSqlCmd.ExecuteScalar());
                m_Connection.Close();
                if (zRetInt == 0)
                {
                    zRet = true;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return zRet;
        }

        /// <summary>
        /// Return all history DESC
        /// </summary>
        /// <param name="APrescriptionNum"></param>
        /// <returns></returns>
        public DataSet SHS_GetHistoryAll()
        {
            DataSet zDataSet = new DataSet();
            try
            {
                string zSqlStr = "  SELECT * FROM History ORDER BY CreatedDT DESC ";

                SqlCommand zSqlCmd = OpenSqlQuery(zSqlStr);
                SqlDataAdapter zSqlDataAdapter = new SqlDataAdapter(zSqlCmd);
                zSqlDataAdapter.Fill(zDataSet);
                m_Connection.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return zDataSet;
        }


        /// <summary>
        /// Return all history DESC
        /// </summary>
        /// <param name="APrescriptionNum"></param>
        /// <returns></returns>
        public DataSet SHS_GetHistory_PendingSync()
        {
            DataSet zDataSet = new DataSet();
            try
            {
                string zSqlStr = "   SELECT * FROM History  WHERE SyncDT is null ORDER BY CreatedDT DESC";

                SqlCommand zSqlCmd = OpenSqlQuery(zSqlStr);
                SqlDataAdapter zSqlDataAdapter = new SqlDataAdapter(zSqlCmd);
                zSqlDataAdapter.Fill(zDataSet);
                m_Connection.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return zDataSet;
        }


        /// <summary>
        /// Delete pending sync record
        /// </summary>
        /// <param name="APrescriptionNum"></param>
        /// <returns></returns>
        public bool SHS_PendingSync_Del(string aTransId)
        {
            bool zRet = false;
            try
            {
                string zSqlStr = "   DELETE FROM HISTORY WHERE ID = '" + aTransId + "'";

                SqlCommand zSqlCmd = OpenSqlQuery(zSqlStr);
                int zRetInt = Convert.ToInt32(zSqlCmd.ExecuteScalar());
                m_Connection.Close();
                if (zRetInt == 0)
                {
                    zRet = true;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return zRet;
        }

        /// <summary>
        /// Delete pending sync record
        /// </summary>
        /// <param name="APrescriptionNum"></param>
        /// <returns></returns>
        public bool SHS_PendingSync_EndDate(string aTransId)
        {
            bool zRet = false;
            try
            {
                string zSqlStr = "   UPDATE HISTORY SET SyncDT = GETDATE() WHERE ID = '" + aTransId + "'";

                SqlCommand zSqlCmd = OpenSqlQuery(zSqlStr);
                int zRetInt = Convert.ToInt32(zSqlCmd.ExecuteScalar());
                m_Connection.Close();
                if (zRetInt == 0)
                {
                    zRet = true;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return zRet;
        }
        #endregion


       



        /// <summary>
        /// </summary>
        /// <param name="APrescriptionNum"></param>
        /// <returns></returns>
        public DataSet SHS_AllWorkers()
        {
            DataSet zDataSet = new DataSet();
            try
            {
                //string zSqlStr = " SELECT TIDP,TIDM,MedBarcode,MediCareNum,RecBatch,  ISNULL(Status,'NULL') AS MediStatus" +
                //                 " FROM PersonnelMedRelationship " +
                //                 " WHERE PersonnelMedRelationship.RecBatch = '" + APrescriptionNum.ToString() + "' ";
             

                //Return Medicine name as well
                string zSqlStr = " SELECT * FROM Personnel";


                SqlCommand zSqlCmd = OpenSqlQuery(zSqlStr);
                SqlDataAdapter zSqlDataAdapter = new SqlDataAdapter(zSqlCmd);
                zSqlDataAdapter.Fill(zDataSet);
                m_Connection.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return zDataSet;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="aAccessCardUID"></param>
       /// <param name="aInOutStatus"></param>
       /// <returns></returns>
        public bool SHS_Update_Personnel_CheckInOut(string aAccessCardUID, SHS_CheckInOutStatus aInOutStatus)
        {
            bool zRet = false;
            try
            {
                int zCheckInOut = 0;
                if (aInOutStatus == SHS_CheckInOutStatus.CheckIn)
                    zCheckInOut = 1;
                else if (aInOutStatus == SHS_CheckInOutStatus.CheckOut)
                {
                    zCheckInOut = 0;
                }

                string zSqlStr = " UPDATE Personnel SET CheckedIn = " + zCheckInOut + " WHERE AccessCardUID = '" + aAccessCardUID + "'";

                SqlCommand zSqlCmd = OpenSqlQuery(zSqlStr);
                int zRetInt = Convert.ToInt32(zSqlCmd.ExecuteScalar());
                m_Connection.Close();
                if (zRetInt == 0)
                {
                    zRet = true;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return zRet;
        }

        public bool SHS_Update_Personnel_AllCheckIn()
        {
            bool zRet = false;
            try
            {
                string zSqlStr = " UPDATE Personnel SET CheckedIn = 1 ";

                SqlCommand zSqlCmd = OpenSqlQuery(zSqlStr);
                int zRetInt = Convert.ToInt32(zSqlCmd.ExecuteScalar());
                m_Connection.Close();
                if (zRetInt == 0)
                {
                    zRet = true;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return zRet;
        }

        public bool SHS_Update_Personnel_AllCheckOut()
        {
            bool zRet = false;
            try
            {
                string zSqlStr = " UPDATE Personnel SET CheckedIn = 0 ";

                SqlCommand zSqlCmd = OpenSqlQuery(zSqlStr);
                int zRetInt = Convert.ToInt32(zSqlCmd.ExecuteScalar());
                m_Connection.Close();
                if (zRetInt == 0)
                {
                    zRet = true;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return zRet;
        }
 
        #region Personnel Table Actions

        /// <summary>
        ///  Mode = 0 : check user existence based on AccessCard number to create 
        ///             If does exist, update, name, phone number, address, access card, TIDP, 
        ///  Mode = 1: return Personnel's informaiton based on accesscard number
        ///  Mode = 2: Based on HQUserID to see if the user existence.
        ///            If it doesn't exist, create new user informaiton.
        ///            Else,update Personnel's AccessCardUID and TIDP
        /// </summary>
        /// <param name="AMode"></param>
        /// <param name="AccessCard"></param>
        /// <param name="Name"></param>
        /// <param name="PhoneNum"></param>
        /// <param name="Address"></param>
        /// <param name="TIDP"></param>
        /// <param name="HQUserID"></param>
        /// <returns></returns>
        /// 
 
        public bool SHS_CreateUpdatePatientDetail(int AMode, string Name, string PhoneNum, string Address, string AccessCard,
                                                        string TIDP, long HQUserID)
        {
            bool zRet = false;
            try
            {
                SqlCommand zSQLCmd = OpenStoredProcedure("BDSsp_InsertPersonnel");

                if (zSQLCmd != null)
                {
                    zSQLCmd.Parameters.Add("@AMode", SqlDbType.Int).Value = AMode;//Create or update
                    zSQLCmd.Parameters.Add("@Name", SqlDbType.VarChar, 250).Value = Name;
                    zSQLCmd.Parameters.Add("@PhoneNum", SqlDbType.VarChar, 50).Value = PhoneNum;
                    zSQLCmd.Parameters.Add("@Address", SqlDbType.VarChar, 250).Value = Address;
                    zSQLCmd.Parameters.Add("@AccessCard", SqlDbType.VarChar, 50).Value = AccessCard;
                    zSQLCmd.Parameters.Add("@TIDP", SqlDbType.VarChar, 50).Value = TIDP;
                    zSQLCmd.Parameters.Add("@HQUserID", SqlDbType.BigInt).Value = HQUserID;

                    int zRetInt = Convert.ToInt32(zSQLCmd.ExecuteScalar());
                    m_Connection.Close();

                    if (zRetInt == 0)
                    {
                        zRet = true;
                    }
                }
                
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return zRet;
        }



        /// <summary>
        ///  Mode = 0 : Only update the CHeckedIn bit value when the record existed.
        /// </summary>
        /// <param name="AMode"></param>
        /// <param name="AccessCard"></param>
        /// <param name="Name"></param>
        /// <param name="PhoneNum"></param>
        /// <param name="Address"></param>
        /// <param name="TIDP"></param>
        /// <param name="HQUserID"></param>
        /// <returns></returns>
        public bool SHS_UpdateUserCheckedInOut(string Name, bool CheckedIn, long HQUserID)
        {
            bool zRet = false;
            try
            {
                SqlCommand zSQLCmd = OpenStoredProcedure("BDSsp_UpdatePersonnelCheckInOut");

                if (zSQLCmd != null)
                {
                    zSQLCmd.Parameters.Add("@AMode", SqlDbType.Int).Value = 0;//Create or update
                    zSQLCmd.Parameters.Add("@Name", SqlDbType.VarChar, 250).Value = Name;
                    zSQLCmd.Parameters.Add("@CheckedIn", SqlDbType.Bit).Value = CheckedIn;
                    zSQLCmd.Parameters.Add("@HQUserID", SqlDbType.BigInt).Value = HQUserID;

                    int zRetInt = Convert.ToInt32(zSQLCmd.ExecuteScalar());
                    m_Connection.Close();

                    if (zRetInt == 0)
                    {
                        zRet = true;
                    }
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return zRet;
        }
        #endregion


        #region

        public HumpyDetail SHS_GetHumpySettings()
        {
            HumpyDetail zHD = new HumpyDetail();
            DataSet zDataSet = null;
            try
            {
                string zQuery = "SELECT * FROM HumpySetting";
                SqlCommand zSQLCmd = OpenSqlQuery(zQuery);
                if (zSQLCmd != null)
                {
                    SqlDataAdapter zReader = new SqlDataAdapter(zSQLCmd);
                    zDataSet = new DataSet();
                    zReader.Fill(zDataSet);

                    if (zDataSet != null)
                    {
                        if (zDataSet.Tables.Count > 0)
                        {
                            if (zDataSet.Tables[0].Rows.Count > 0)
                            {
                                #region HumpyGate
                                zHD.HumpyGate.GateID = "";
                                zHD.HumpyGate.gateDescription = "";
                                zHD.HumpyGate.FK_privilegeID = "";
                                zHD.HumpyGate.embeddedDNS = "";
                                zHD.HumpyGate.embeddedIP = "";

                                zHD.HumpyGate.ant1Power = zDataSet.Tables[0].Rows[0]["Rd_Ant1Power"].ToString();
                                zHD.HumpyGate.ant1Sensitivity = zDataSet.Tables[0].Rows[0]["Rd_ant1Sensitivity"].ToString();
                                zHD.HumpyGate.ant2Power = zDataSet.Tables[0].Rows[0]["Rd_Ant2Power"].ToString();
                                zHD.HumpyGate.ant2Sensitivity = zDataSet.Tables[0].Rows[0]["Rd_ant2Sensitivity"].ToString();
                                zHD.HumpyGate.ant3Power = zDataSet.Tables[0].Rows[0]["Rd_Ant3Power"].ToString();
                                zHD.HumpyGate.ant3Sensitivity = zDataSet.Tables[0].Rows[0]["Rd_ant3Sensitivity"].ToString();
                                zHD.HumpyGate.ant4Power = zDataSet.Tables[0].Rows[0]["Rd_Ant4Power"].ToString();
                                zHD.HumpyGate.ant4Sensitivity = zDataSet.Tables[0].Rows[0]["Rd_ant4Sensitivity"].ToString();
                                zHD.HumpyGate.stateOnOff = zDataSet.Tables[0].Rows[0]["stateOnOff"].ToString();

                                zHD.HumpyGate.Rd_Filter1 = zDataSet.Tables[0].Rows[0]["Rd_Filter1"].ToString();
                                zHD.HumpyGate.Rd_Filter2 = zDataSet.Tables[0].Rows[0]["Rd_Filter2"].ToString();

                                zHD.HumpyGate.readerType = zDataSet.Tables[0].Rows[0]["Rd_Type"].ToString();
                                
                                //zHD.HumpyGate.GateReader.ReaderName = zHD.HumpyGate.readerName = zDataSet.Tables[0].Rows[0]["Rd_UHFReader_IP"].ToString();
                                zHD.HumpyGate.GateReader.ReaderName = zHD.HumpyGate.readerName = zDataSet.Tables[0].Rows[0]["Sys_HumpyId"].ToString();//IN JL 31-MAY-2016
                                zHD.HumpyGate.readerDNS = "";
                                zHD.HumpyGate.GateReader.ReaderIPAddress = zHD.HumpyGate.readerIP = zDataSet.Tables[0].Rows[0]["Rd_UHFReader_IP"].ToString();

                                zHD.HumpyGate.stateOnOff = zDataSet.Tables[0].Rows[0]["stateOnOff"].ToString();
                                zHD.HumpyGate.AccessPwd = "";
                                
                                if (zHD.HumpyGate.readerType == "R220")
                                    zHD.HumpyGate.GateReader.ReaderType = ReaderType.R220;
                                else if (zHD.HumpyGate.readerType == "R420")
                                    zHD.HumpyGate.GateReader.ReaderType = ReaderType.R420;
                                else if (zHD.HumpyGate.readerType == "XPortal")
                                    zHD.HumpyGate.GateReader.ReaderType = ReaderType.XPortal;

                                zHD.HumpyGate.GateReader.Antenna1.TxPower = double.Parse(zHD.HumpyGate.ant1Power);
                                zHD.HumpyGate.GateReader.Antenna1.RssiThreshold = double.Parse(zHD.HumpyGate.ant1Sensitivity);
                                zHD.HumpyGate.GateReader.Antenna2.TxPower = double.Parse(zHD.HumpyGate.ant2Power);
                                zHD.HumpyGate.GateReader.Antenna2.RssiThreshold = double.Parse(zHD.HumpyGate.ant2Sensitivity);
                                zHD.HumpyGate.GateReader.Antenna3.TxPower = double.Parse(zHD.HumpyGate.ant3Power);
                                zHD.HumpyGate.GateReader.Antenna3.RssiThreshold = double.Parse(zHD.HumpyGate.ant3Sensitivity);
                                zHD.HumpyGate.GateReader.Antenna4.TxPower = double.Parse(zHD.HumpyGate.ant4Power);
                                zHD.HumpyGate.GateReader.Antenna4.RssiThreshold = double.Parse(zHD.HumpyGate.ant4Sensitivity);
                                #endregion

                                #region System Settings
                                zHD.Sys_DBConnection = zDataSet.Tables[0].Rows[0]["Sys_DBConnection"].ToString();
                                zHD.Sys_DB_Datasource = zDataSet.Tables[0].Rows[0]["Sys_DB_Datasource"].ToString();
                                zHD.Sys_DB_InitialCatalog = zDataSet.Tables[0].Rows[0]["Sys_DB_InitialCatalog"].ToString();
                                zHD.Sys_DB_UserID = zDataSet.Tables[0].Rows[0]["Sys_DB_UserID"].ToString();
                                zHD.Sys_DB_Password = zDataSet.Tables[0].Rows[0]["Sys_DB_Password"].ToString();
                                zHD.Sys_DB_MaxPoolSize = zDataSet.Tables[0].Rows[0]["Sys_DB_MaxPoolSize"].ToString();

                                #endregion

                                zHD.Rd_HFReader_COM = zDataSet.Tables[0].Rows[0]["Rd_HFReader_COM"].ToString();
                                zHD.Rd_HFReader_Baudrate = zDataSet.Tables[0].Rows[0]["Rd_HFReader_Baudrate"].ToString();
                                zHD.Sys_HumpyId = zDataSet.Tables[0].Rows[0]["Sys_HumpyId"].ToString();
                                zHD.Sys_RetrieveTimer_ms = int.Parse(zDataSet.Tables[0].Rows[0]["Sys_RetrieveTimer_ms"].ToString());
                                zHD.Sys_TagReadSensitivity_ms = int.Parse(zDataSet.Tables[0].Rows[0]["Sys_TagReadSensitivity_ms"].ToString());
                                zHD.CheckInOutWaitingTime_ms = int.Parse(zDataSet.Tables[0].Rows[0]["CheckInOutWaitingTime_ms"].ToString());
                                zHD.Sys_HumpyGroupName = zDataSet.Tables[0].Rows[0]["Sys_HumpyGroupName"].ToString();
                                zHD.FullStatusReportTimer_ms = int.Parse(zDataSet.Tables[0].Rows[0]["FullStatusReportTimer_ms"].ToString());
                                zHD.Sys_TagReadSensitivity_ms = int.Parse(zDataSet.Tables[0].Rows[0]["Sys_TagReadSensitivity_ms"].ToString());
                                zHD.Sys_DBorLocalLogging = bool.Parse(zDataSet.Tables[0].Rows[0]["Sys_DBorLocalLogging"].ToString());

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_Connection.Close();
            }
            return zHD;
        }

        public bool SHS_IsHumpyConfigueChanged(HumpyDetail ACurrHumpy, ref HumpyDetail ANewHumpy)
        {
            bool zRet = false;
            HumpyDetail zTempNewHumpy = new HumpyDetail();

            try
            {
                zTempNewHumpy = SHS_GetHumpySettings();

                if (ACurrHumpy.Sys_DBConnection != zTempNewHumpy.Sys_DBConnection)
                    zRet = true;

                if (ACurrHumpy.Sys_HumpyId != zTempNewHumpy.Sys_HumpyId)
                    zRet = true;

                if (ACurrHumpy.HumpyGate.readerIP != zTempNewHumpy.HumpyGate.readerIP)
                    zRet = true;


                //if (ACurrHumpy.HumpyGate..stateOnOff != zTempNewHumpy.HumpyGate.stateOnOff)
                //    zRet = true;

                ANewHumpy = zTempNewHumpy;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //m_Connection.Close();
            }
            return zRet;
        }
        #endregion

    }
}