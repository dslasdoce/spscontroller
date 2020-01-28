using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using SHSHQ_GLOBALS;
namespace SHSHQ_DAL
{
    class UserDAL
    {
        public DataTable GetUserList()
        {
            SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString());
            SqlDataAdapter dataAdapter = new SqlDataAdapter("ISMsp_ISMUserGetRecds", dataConnection);
            DataTable userData = new DataTable();
            try
                {
                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@ALoginID", SqlDbType.VarChar, 55)).Value = "";
                    dataAdapter.Fill(userData);
                }
            catch (Exception ex)
                { throw ex; }
            finally
            { }
            return userData;
        }
        public DataTable GetUserRole()
        {
            SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString());
            SqlDataAdapter dataAdapter = new SqlDataAdapter("ISMsp_GetISMUserRoleMetaData", dataConnection);
            DataTable userData = new DataTable();
            try
            {
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@ALoginID", SqlDbType.VarChar, 55)).Value = "";
                dataAdapter.Fill(userData);
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { }
            return userData;
        }
        public bool CheckUniqueFields(string newLogOnID,string oldLogOnID, string newAccessCardUID,string oldAccessCardUID,
                                           string newHelmetTag,string oldHelmetTag,bool insertMode,string validationKey)
        {
            SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString());
            SqlDataAdapter dataAdapter = new SqlDataAdapter("spISM_USER_CheckUniqueFields", dataConnection);
            DataTable userData = new DataTable();
            bool recordFound = false;

            try
            {
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@newLogOnID", SqlDbType.VarChar, 55)).Value = newLogOnID;
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@oldLogOnID", SqlDbType.VarChar, 55)).Value = oldLogOnID;
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@newAccessCardUID", SqlDbType.VarChar, 50)).Value = newAccessCardUID;
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@oldAccessCardUID", SqlDbType.VarChar, 50)).Value = oldAccessCardUID;
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@newHelmetTag", SqlDbType.VarChar, 50)).Value = newHelmetTag;
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@oldHelmetTag", SqlDbType.VarChar, 50)).Value = oldHelmetTag;
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@insertMode", SqlDbType.Bit)).Value = insertMode;
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@validationKey", SqlDbType.VarChar, 20)).Value = validationKey;
                dataAdapter.Fill(userData);

                recordFound = userData.Rows.Count == 0 ? false : true;
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { }
            return recordFound;
        }
        public DataTable GetUserDetailsByID(int userID)
        {
            SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString());
            SqlDataAdapter dataAdapter = new SqlDataAdapter("spISM_USER_GetUserDetailsByID  ", dataConnection);
            DataTable userData = new DataTable();
            try
            {
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@userID", SqlDbType.Int)).Value = userID;
                dataAdapter.Fill(userData);
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { }
            return userData;
        }
        public long InsertUser(string firstName,string lastName,string logOnID,string password,string accessCardUID, string helmetTag, string systemUser, string userRoleCode)
        {

            SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString());
            SqlDataAdapter dataAdapter = new SqlDataAdapter("spISM_USER_InsertUser  ", dataConnection);
            DataTable userIDDetail = new DataTable();
            long userID = 0;
           
            try
            {
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@firstName", SqlDbType.VarChar, 25)).Value = firstName;
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 25)).Value = lastName;
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@logOnID", SqlDbType.VarChar, 55)).Value = logOnID;
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 128)).Value = password;
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@accessCardUID", SqlDbType.VarChar, 50)).Value = accessCardUID;
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@helmetTag", SqlDbType.VarChar, 50)).Value = helmetTag;
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@systemUser", SqlDbType.VarChar, 50)).Value = systemUser;
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@userRoleCode", SqlDbType.VarChar, 10)).Value = userRoleCode;

               dataAdapter.Fill(userIDDetail);
               userID = Convert.ToInt64(userIDDetail.Rows[0]["NewUserID"]);

            }
            catch (Exception ex)
            { throw ex; }
            finally
            { dataConnection = null;}

            return userID;
        }
        public void UpdatetUser(long userID,string firstName, string lastName, string logOnID, string password, string accessCardUID, string helmetTag, string systemUser, string userRoleCode)
        {

            SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString());
            SqlCommand dataCommand = new SqlCommand("spISM_USER_UpdateUser  ", dataConnection);

            try
            {
                dataCommand.CommandType = CommandType.StoredProcedure;
                dataCommand.Parameters.Add(new SqlParameter("@userID", SqlDbType.Int)).Value = userID;
                dataCommand.Parameters.Add(new SqlParameter("@firstName", SqlDbType.VarChar, 25)).Value = firstName;
                dataCommand.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 25)).Value = lastName;
                dataCommand.Parameters.Add(new SqlParameter("@logOnID", SqlDbType.VarChar, 55)).Value = logOnID;
                dataCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 128)).Value = password;
                dataCommand.Parameters.Add(new SqlParameter("@accessCardUID", SqlDbType.VarChar, 50)).Value = accessCardUID;
                dataCommand.Parameters.Add(new SqlParameter("@helmetTag", SqlDbType.VarChar, 50)).Value = helmetTag;
                dataCommand.Parameters.Add(new SqlParameter("@systemUser", SqlDbType.VarChar, 50)).Value = systemUser;
                dataCommand.Parameters.Add(new SqlParameter("@userRoleCode", SqlDbType.VarChar, 10)).Value = userRoleCode;

                if (dataConnection.State == ConnectionState.Closed)
                    dataConnection.Open();

                dataCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                if (dataConnection.State == ConnectionState.Open)
                    dataConnection.Close();

                dataConnection = null;

            }
        }
        public void DeleteUserHelmetTags(long userID)
        {

            SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString());
            SqlCommand dataCommand = new SqlCommand("spUSER_HELMET_DeleteUserHelmentTags  ", dataConnection);

            try
            {
                dataCommand.CommandType = CommandType.StoredProcedure;
                dataCommand.Parameters.Add(new SqlParameter("@userID", SqlDbType.Int)).Value = userID;

                if (dataConnection.State == ConnectionState.Closed)
                    dataConnection.Open();

                dataCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                if (dataConnection.State == ConnectionState.Open)
                    dataConnection.Close();

                dataConnection = null;

            }
        }
        public void InsertUserHelmetTags(long userID,string tidp, string tidpHelmet)
        {

            SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString());
            SqlCommand dataCommand = new SqlCommand("spUSER_HELMET_InsertUserHelmentTags  ", dataConnection);

            try
            {
                dataCommand.CommandType = CommandType.StoredProcedure;
                dataCommand.Parameters.Add(new SqlParameter("@userID", SqlDbType.Int)).Value = userID;
                dataCommand.Parameters.Add(new SqlParameter("@tidp", SqlDbType.VarChar, 50)).Value = tidp;
                dataCommand.Parameters.Add(new SqlParameter("@tidpHelmet", SqlDbType.VarChar, 50)).Value = tidpHelmet;

                if (dataConnection.State == ConnectionState.Closed)
                    dataConnection.Open();

                dataCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                if (dataConnection.State == ConnectionState.Open)
                    dataConnection.Close();

                dataConnection = null;

            }
        }
        public void DeleteUser(string logOnID)
        {

            SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString());
            SqlCommand dataCommand = new SqlCommand("ISMsp_OperatorDeactivate", dataConnection);

            try
            {
                dataCommand.CommandType = CommandType.StoredProcedure;
                dataCommand.Parameters.Add(new SqlParameter("@ADeactivatedBy", SqlDbType.VarChar, 55)).Value = AppGlobals.globalCurrentSystemUser;
                dataCommand.Parameters.Add(new SqlParameter("@AOperatorId", SqlDbType.VarChar, 55)).Value = logOnID;
                dataCommand.Parameters.Add(new SqlParameter("@ADeactivatedDate", SqlDbType.VarChar, 30)).Value = string.Format("{0:MM/dd/yyyy hh:mm:ss tt}", DateTime.Now);

                if (dataConnection.State == ConnectionState.Closed)
                    dataConnection.Open();

                dataCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                if (dataConnection.State == ConnectionState.Open)
                    dataConnection.Close();

                dataConnection = null;

            }
        }
        public DataTable GetUserSycDetails(int userID)
        {
            SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString());
            SqlDataAdapter dataAdapter = new SqlDataAdapter("spUserSyncDetails_GetUserSyncDetails  ", dataConnection);
            DataTable userSyncData = new DataTable();
            try
            {
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;
                dataAdapter.Fill(userSyncData);
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { }
            return userSyncData;
        }
    }
}
