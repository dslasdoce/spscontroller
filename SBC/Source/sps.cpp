/*
 * SPS.cpp
 *
 *  Created on: 12 Jul 2017
 *      Author: nic
 */
/*Native Modules*/
#include<iostream>
#include<stdlib.h>
#include<unistd.h>
#include<stdio.h>
#include<vector>
#include<sstream>
#include<string>
#include<algorithm>
#include<string.h>
/*Third Party Modules*/
#include <mysql_connection.h>
#include <mysql_driver.h>
#include <cppconn/driver.h>
#include <cppconn/exception.h>
#include <cppconn/resultset.h>
#include <cppconn/statement.h>

/*SPS Moduiles*/
#include <sbc.h>
#include <sps.h>
#include <utility.h>

using namespace std;

/***********************************************************************
* Function Name: workerPrintStatus
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 13, 2017
*
* Description: print ground worker values
*
* Arguments:
 		GroundWorker gworker: ground worker details
*
* Required Files/Databases:
*
*
* Non System Routines Called:
*
*
* Return Value:
*
* Error Codes/Exceptions:

*
* OS Specific Assumptions:
*
*
* Local Variables:

*
* Modification History:
* Date   Developer   Action
* 07/13/2017  dslasdoce    creation
***********************************************************************/
static void workerPrintStatus(GroundWorker gworker)
{
	stringstream str_s_log;
	str_s_log << "\n###############################" << endl;
	str_s_log << "ID: " << gworker.gw_id << endl;
	str_s_log << "Name: " << gworker.gw_name << endl;
	str_s_log << "Card: " << gworker.gw_access_card << endl;
	str_s_log << "Tag: " << gworker.gw_tag_id << endl;
	str_s_log << "Checked: " << gworker.gw_is_checked_in << endl;
	str_s_log << "In/Out: " << gworker.gw_is_in << endl;
	sbc_util.utilSysLog(str_s_log.str());
}

/***********************************************************************
* Function Name: workerCopyFromDBResult
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 13, 2017
*
* Description: copy sql result to ground worker object
*
* Arguments:
 		GroundWorker *gworker: pointer to save ground worker values
*		sql::ResultSet *res: sql result pointer
* Required Files/Databases:
*
*
* Non System Routines Called:
*
*
* Return Value:
*
* Error Codes/Exceptions:

*
* OS Specific Assumptions:
*
*
* Local Variables:

*
* Modification History:
* Date   Developer   Action
* 07/13/2017  dslasdoce    creation
***********************************************************************/
static inline void workerCopyFromDBResult(GroundWorker *gworker,
		sql::ResultSet *res)
{
	gworker->gw_id = res->getInt("ID");
	gworker->gw_name = res->getString("Name");
	gworker->gw_access_card = res->getString("AccessCardUID");
	gworker->gw_tag_id = res->getString("TIDP");
	gworker->gw_is_checked_in = res->getBoolean("CheckedIN");
	gworker->gw_is_in = res->getBoolean("InOut");
}


static inline void personnelCopyFromDBResult(Personnel *personnel,
		sql::ResultSet *res)
{
	personnel->id = res->getInt("ID");
	personnel->name = res->getString("Name");
	personnel->phone_num = res->getString("PhoneNum");
	personnel->address = res->getString("Address");
	personnel->card_id = res->getString("AccessCardUID");
	personnel->tag_id = res->getString("TIDP");
	personnel->gw_is_checked_in = res->getBoolean("CheckedIN");
	personnel->location_group  = res->getString("LocationGroup");
	personnel->gw_is_in = res->getBoolean("InOut");
	personnel->last_loc_id  = res->getString("LastLocId");
	personnel->msrepl_tran_version = res->getString("msrepl_tran_version");
	personnel->hq_user_id = res->getInt("HQUserID");
	personnel->crane_driver = res->getBoolean("CraneDriver");
}

static void personnelPrintStatus(Personnel personnel)
{
	stringstream str_s_log;
	str_s_log << "\nID: " << personnel.id << endl;
	str_s_log << "Name: " << personnel.name << endl;
	str_s_log << "PhoneNum: " << personnel.phone_num << endl;
	str_s_log << "Address: " << personnel.address << endl;
	str_s_log << "AccessCardUID: " << personnel.card_id << endl;
	str_s_log << "TIDP: " << personnel.tag_id << endl;
	str_s_log << "CheckedIN: " << personnel.gw_is_checked_in << endl;
	str_s_log << "LocationGroup: " << personnel.location_group  << endl;
	str_s_log << "InOut: " << personnel.gw_is_in << endl;
	str_s_log << "LastLocId: " << personnel.last_loc_id << endl;
	str_s_log << "msrepl_tran_version: " << personnel.msrepl_tran_version << endl;
	str_s_log << "HQUserID: " << personnel.hq_user_id << endl;
	str_s_log << "CraneDriver: " <<personnel.crane_driver << endl << endl;
	sbc_util.utilSysLog(str_s_log.str());
}

/***********************************************************************
* Function Name: dbConnect
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 12, 2017
*
* Description: Collect all the statuses of checkedin workers
*
* Arguments:
 		DBConnectionParam *db_param: pointer to database connection parameters
 		sql::Connection **con: address of the connection pointer object
*
* Required Files/Databases: SPSControllerDB
*
*
* Non System Routines Called:
*
*TBL_WORKERSTATUS
* Return Value: DBStatus status: enumeration of database statuses
*
* Error Codes/Exceptions:

*
* OS Specific Assumptions:
*
*
* Local Variables:
 		sql::Driver *driver;
*
* Modification History:
* Date   Developer   Action
* 07/12/2017  dslasdoce    creation
***********************************************************************/
DBStatusEnum dbConnect(DBConnectionParam *db_param, sql::Connection **con)
{
	DBStatusEnum db_stat = DB_ERROR;
	stringstream str_s_log;
	try{
		/*Prepare connection object*/
		sql::Driver *driver;
		driver = get_driver_instance();

		*con = driver->connect(db_param->db_host, db_param->db_username,
				db_param->db_password);

		(*con)->setSchema(db_param->db_name);

		if ( (*con)->isValid() )
			db_stat = DB_CONNECTED;
	} catch(sql::SQLException &e) {
		str_s_log.str(string());
		str_s_log << "# ERR: SQLException in " << __FILE__;
		str_s_log << "(" << __FUNCTION__ << ") on line " << __LINE__ << endl;
		str_s_log << "# ERR: " << e.what();
		str_s_log << " (MySQL error code: " << e.getErrorCode();
		str_s_log << ", SQLState: " << e.getSQLState() << " )" << endl;
		sbc_util.utilSysLog(str_s_log.str());
	} catch(std::exception &e){
		str_s_log.str(string());
		str_s_log<< "**********\nERROR\n "<<e.what() << "\n**********\n" <<endl;
		sbc_util.utilSysLog(str_s_log.str());
	}
	return db_stat;
}

/***********************************************************************
* Function Name: GetCheckedInWorkerStatus
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 12, 2017
*
* Description: Collect all the statuses of checkedin workers
*
* Arguments:
 		DBConnectionParam db_con_param
*
* Required Files/Databases: SPSControllerDB
*
*
* Non System Routines Called:
*
*
* Return Value: WorkerSafety status: enumeration of safety statuses(e.g Go/Stop)
*
* Error Codes/Exceptions:

*
* OS Specific Assumptions:
*
*
* Local Variables:
 		DBStatusEnum db_stat = DB_ERROR;
		WorkerSafetyStatusEnum wss_stat = WSS_NOT_OK;
		string query_statement;
*
* Modification History:
* Date   Developer   Action
* 07/12/2017  dslasdoce    creation
***********************************************************************/
WorkerSafetyStatusEnum GetCheckedInWorkerStatus(DBConnectionParam db_con_param)
{
	DBStatusEnum db_stat = DB_ERROR;
	WorkerSafetyStatusEnum wss_stat = WSS_NOT_OK;
	string query_statement;

	sql::Connection *con = NULL;
	sql::Statement *stmt = NULL;
	sql::ResultSet *res = NULL;
	vector<GroundWorker> gw_list;
	/*Create Container*/
	gw_list.clear();
	GroundWorker gw_current;

	db_stat = dbConnect(&db_con_param, &con);

	if(db_stat != DB_CONNECTED){
		sbc_util.utilSysLog("DB Connection Failed\n");
	} else{
		wss_stat = WSS_OK;
		query_statement = "SELECT * FROM " + TBL_WORKERSTATUS
			+ CNDTION_CHECKED_IN + ";";
		/* Connect to the MySQL test database */
		stmt = con->createStatement();
		res = stmt->executeQuery(query_statement);

		/*Add all the checked in groung worker to gw_list*/
		while (res->next()) {
			workerCopyFromDBResult(&gw_current, res);
			gw_list.push_back(gw_current);
		}

		/*Check if all checked in are IN*/
		for(uint8_t i = 0; i < gw_list.size(); i++){
			//workerPrintStatus(gw_list[i]);
			if(gw_list[i].gw_is_in != true)
				wss_stat = WSS_NOT_OK;
		}
		db_stat = DB_OK;
	}

	gw_list.clear();

	delete stmt;
	delete res;
	delete con;
	return wss_stat;
}

/***********************************************************************
* Function Name: clearWorkersInOutStatus
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: Aug 14, 2017
*
* Description: remove all checked out workers from workersinoutstatus table
*
* Arguments:
 		s
* Required Files/Databases:
*
*
* Non System Routines Called:
*
*
* Return Value:
*
* Error Codes/Exceptions:

*
* OS Specific Assumptions:
*
*
* Local Variables:
	 	DBStatusEnum db_stat: return value
		Personnel personnel: container of the personnel details fetched from DB
		string query_statement: container of sql request strings
		stringstream str_holder: temp holder for converting int to string
		bool is_tag_exist: checker if a tag is in database
		sql::Connection *con: sql connection object
		sql::Statement *stmt: sql statement object
		sql::ResultSet *res: sql result object
*
* Modification History:
* Date   Developer   Action
* 08/14/2017  dslasdoce    creation
***********************************************************************/
void clearWorkersInOutStatus()
{
	DBStatusEnum db_stat = DB_ERROR;
	GroundWorker gw_db;
	string query_statement;
	stringstream str_holder;
	stringstream str_s_log;
	sql::Connection *con = NULL;
	sql::Statement *stmt = NULL;
	DBConnectionParam db_con_param;

	db_con_param.db_name = DB_SPS;
	db_con_param.db_host = DB_HOST;
	db_con_param.db_username = DB_USERNAME;
	db_con_param.db_password = DB_PASSWORD;

	try{
	db_stat = dbConnect(&db_con_param, &con);

		if(db_stat != DB_CONNECTED){
			sbc_util.utilSysLog("\n*DB: Update InOutStatus Failed\n");
		} else{
			query_statement = "DELETE from " + TBL_WORKERSTATUS +
					"WHERE `CheckedIN`= 0 AND `InOut`=0";
			stmt = con->createStatement();
			stmt->execute(query_statement);


		}

	} catch(sql::SQLException &e) {
		str_s_log.str(string());
		str_s_log << "# ERR: SQLException in " << __FILE__;
		str_s_log << "(" << __FUNCTION__ << ") on line " << __LINE__ << endl;
		str_s_log << "# ERR: " << e.what();
		str_s_log << " (MySQL error code: " << e.getErrorCode();
		str_s_log << ", SQLState: " << e.getSQLState() << " )" << endl;
		sbc_util.utilSysLog(str_s_log.str());
		exit(0);
	} catch(std::exception &e){
		str_s_log.str(string());
		str_s_log<< e.what() <<endl;
		sbc_util.utilSysLog(str_s_log.str());
	}

	delete stmt;
	delete con;
}
/***********************************************************************
* Function Name: UpdateCheckingStatus
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 13, 2017
*
* Description: update a workers checking status
*
* Arguments:
 		DBConnectionParam *db_param: pointer to database connection parameters
 		GroundWorker gworker: object containing details of the worker to be updated
*
* Required Files/Databases: SPSControllerDB
*
*
* Non System Routines Called:
*
*
* Return Value: DBStatus status: enumeration of database statuses
*
* Error Codes/Exceptions:

*
* OS Specific Assumptions:
*
*
* Local Variables:
 		DBStatusEnum db_stat: return value
		GroundWorker gw_db: container of the worker details fetched from DB
		string query_statement: container of sql request strings
		stringstream str_holder: temp holder for converting int to string
		bool is_user_exist: checker if a user is in database
		sql::Connection *con: sql connection object
		sql::Statement *stmt: sql statement object
		sql::ResultSet *res: sql result object
*
* Modification History:
* Date   Developer   Action
* 07/13/2017  dslasdoce    creation
***********************************************************************/
DBStatusEnum UpdateCheckingStatus(GroundWorker gworker)
{
	DBStatusEnum db_stat = DB_ERROR;
	Personnel personnel;
	DBConnectionParam db_con_param;
	string query_statement;
	stringstream str_holder;
	stringstream str_s_log;
	bool is_user_exist = false;
	char insert_str[200];
	sql::Connection *con = NULL;
	sql::Statement *stmt = NULL;
	sql::ResultSet *res = NULL;
	string log_id;
	string log_status;
	string log_action;
	string log_input;


	db_con_param.db_name = DB_SPS;
	db_con_param.db_host = DB_HOST;
	db_con_param.db_username = DB_USERNAME;
	db_con_param.db_password = DB_PASSWORD;

	try{
	db_stat = dbConnect(&db_con_param, &con);

		if(db_stat != DB_CONNECTED){
			sbc_util.utilSysLog("Update Checking Status Failed\n");
		} else{
			/*Get DB Details of Worker to be updated*/
			sbc_util.utilSysLog("\n*******CURRENT USER STATUS*******");
			query_statement = "SELECT * from" + TBL_PERSONNEL + "WHERE AccessCardUID = "
				+ '\'' +gworker.gw_access_card + '\'' + "";
			stmt = con->createStatement();
			res = stmt->executeQuery(query_statement);


			while (res->next()) {
				personnelCopyFromDBResult(&personnel, res);
				personnelPrintStatus(personnel);
				is_user_exist = true;
			}
			delete res;

			if(is_user_exist){
				str_holder << !personnel.gw_is_checked_in;

				/*Update Personnel Table*/
				query_statement = "UPDATE " + TBL_PERSONNEL + "SET `CheckedIN`="
						+ str_holder.str() + " WHERE `Name`=\'" + personnel.name + "\'";
				stmt->execute(query_statement);

				sbc_util.utilSysLog("\n\n*******NEW USER STATUS*******");
				query_statement = "SELECT * from" + TBL_PERSONNEL + "WHERE Name="
					+ '\'' +personnel.name + '\'' + "";
				res = stmt->executeQuery(query_statement);

				while (res->next()) {
					personnelCopyFromDBResult(&personnel, res);
					personnelPrintStatus(personnel);
				}
				delete res;

				/*Update workersinoutstatus table*/
				sprintf(insert_str,
						"INSERT INTO %s VALUES "
						"(%d,'%s','%s','%s',%d,%d,'%s') "
						"ON DUPLICATE KEY UPDATE `CheckedIN` = %d",
						TBL_WORKERSTATUS.c_str(), personnel.id, personnel.name.c_str()
						,personnel.card_id.c_str(), personnel.tag_id.c_str()
						,personnel.gw_is_checked_in, personnel.gw_is_in
						,LOCATION_ID.c_str()
						,personnel.gw_is_checked_in);
				query_statement = insert_str;
				stmt->execute(query_statement);
				db_stat = DB_OK;

				log_id = personnel.name;
				if(personnel.gw_is_checked_in){
					log_status = LOG_STATUS_WORKER_CIN;
					log_action = LOG_ACTION_WORKER_CIN;
				}
				else{
					log_status = LOG_STATUS_WORKER_COUT;
					log_action = LOG_ACTION_WORKER_COUT;
				}

				log_input = personnel.card_id;

				sbc_util.utilAppLog(
						LOG_COMPONENT_WORKER
						, personnel.name
						, log_status
						, log_action
						, log_input);

			} else{
				str_s_log.str(string());
				str_s_log<< "\nUSER WITH AccessCardUID \'" << gworker.gw_access_card << "\'"
						<< " DOES NOT EXIST" << endl;
				sbc_util.utilSysLog(str_s_log.str());
			}

		}

	} catch(sql::SQLException &e) {
		str_s_log.str(string());
		str_s_log << "# ERR: SQLException in " << __FILE__;
		str_s_log << "(" << __FUNCTION__ << ") on line " << __LINE__ << endl;
		str_s_log << "# ERR: " << e.what();
		str_s_log << " (MySQL error code: " << e.getErrorCode();
		str_s_log << ", SQLState: " << e.getSQLState() << " )" << endl;
		sbc_util.utilSysLog(str_s_log.str());
	} catch(std::exception &e){
		str_s_log.str(string());
		str_s_log<< e.what() <<endl;
		sbc_util.utilSysLog(str_s_log.str());
	}
	delete stmt;
	delete con;
	return db_stat;
}

/***********************************************************************
* Function Name: UpdateInOutStatus
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 13, 2017
*
* Description: update a workers in/out status
*
* Arguments:
 		GroundWorker gworker: object containing details of the worker to be updated
 		bool is_in: status to be written
*
* Required Files/Databases: SPSControllerDB
*
*
* Non System Routines Called:
*
*
* Return Value: DBStatus status: enumeration of database statuses
*
* Error Codes/Exceptions:

*
* OS Specific Assumptions:
*
*
* Local Variables:
 		DBStatusEnum db_stat: return value
		GroundWorker gw_db: container of the worker details fetched from DB
		string query_statement: container of sql request strings
		stringstream str_holder: temp holder for converting int to string
		bool is_user_exist: checker if a user is in database
		sql::Connection *con: sql connection object
		sql::Statement *stmt: sql statement object
		sql::ResultSet *res: sql result object
*
* Modification History:
* Date   Developer   Action
* 07/13/2017  dslasdoce    creation
***********************************************************************/
DBStatusEnum UpdateInOutStatus(GroundWorker gworker, bool is_in)
{
	DBStatusEnum db_stat = DB_ERROR;
	Personnel personnel;
	string query_statement;
	stringstream str_holder;
	stringstream str_s_log;
	bool is_user_exist = false;
	sql::Connection *con = NULL;
	sql::Statement *stmt = NULL;
	sql::ResultSet *res = NULL;
	DBConnectionParam db_con_param;
	char insert_str[200];
	db_con_param.db_name = DB_SPS;
	db_con_param.db_host = DB_HOST;
	db_con_param.db_username = DB_USERNAME;
	db_con_param.db_password = DB_PASSWORD;
	string log_id;
	string log_status;
	string log_action;
	string log_input;

	try{
	db_stat = dbConnect(&db_con_param, &con);

		if(db_stat != DB_CONNECTED){
			sbc_util.utilSysLog("\n*DB: Update InOutStatus Failed\n");
		} else{
			/*Get DB Details of Worker to be updated*/
			sbc_util.utilSysLog("\n*******CURRENT USER STATUS*******");
			query_statement = "SELECT * from" + TBL_PERSONNEL + "WHERE TIDP = "
				+ '\'' +gworker.gw_tag_id + '\'' + "";
			stmt = con->createStatement();
			res = stmt->executeQuery(query_statement);


			while (res->next()) {
				personnelCopyFromDBResult(&personnel, res);
				personnelPrintStatus(personnel);
				is_user_exist = true;
			}
			delete res;

			if(is_user_exist){
				str_holder << is_in;

				/*update personnel table*/
				query_statement = "UPDATE " + TBL_PERSONNEL + "SET `InOut`="
						+ str_holder.str() + " WHERE `Name`=\'" + personnel.name + "\'";
				stmt->execute(query_statement);

				/*get new status from personnel table*/
				sbc_util.utilSysLog("\n*******NEW USER STATUS*******");
				query_statement = "SELECT * from" + TBL_PERSONNEL + "WHERE Name="
					+ '\'' +personnel.name + '\'' + "";
				res = stmt->executeQuery(query_statement);
				while (res->next()) {
					personnelCopyFromDBResult(&personnel, res);
					personnelPrintStatus(personnel);
					is_user_exist = true;
				}
				delete res;
				sprintf(insert_str,
						"INSERT INTO %s VALUES "
						"(%d,'%s','%s','%s',%d,%d,'%s') "
						"ON DUPLICATE KEY UPDATE `InOut` = %d",
						TBL_WORKERSTATUS.c_str(), personnel.id, personnel.name.c_str()
						,personnel.card_id.c_str(), personnel.tag_id.c_str()
						,personnel.gw_is_checked_in, personnel.gw_is_in
						,LOCATION_ID.c_str()
						,personnel.gw_is_in);
				query_statement = insert_str;
				stmt->execute(query_statement);

				log_id = personnel.name;
				if(personnel.gw_is_checked_in)
					log_status = LOG_STATUS_WORKER_CIN;
				else
					log_status = LOG_STATUS_WORKER_COUT;

				if(personnel.gw_is_in)
					log_action = LOG_ACTION_WORKER_IN;
				else
					log_action = LOG_ACTION_WORKER_OUT;

				log_input = personnel.tag_id;

				sbc_util.utilAppLog(
						LOG_COMPONENT_WORKER
						, personnel.name
						, log_status
						, log_action
						, log_input);
			} else{
				str_s_log.str(string());
				str_s_log<< "\nUSER WITH TID \'" << gworker.gw_tag_id << "\'"
						<< " DOES NOT EXIST" << endl;
				sbc_util.utilSysLog(str_s_log.str());
			}

		}

	} catch(sql::SQLException &e) {
		str_s_log.str(string());
		str_s_log << "# ERR: SQLException in " << __FILE__;
		str_s_log << "(" << __FUNCTION__ << ") on line " << __LINE__ << endl;
		str_s_log << "# ERR: " << e.what();
		str_s_log << " (MySQL error code: " << e.getErrorCode();
		str_s_log << ", SQLState: " << e.getSQLState() << " )" << endl;
		sbc_util.utilSysLog(str_s_log.str());
	} catch(std::exception &e){
		str_s_log.str(string());
		str_s_log<< e.what() <<endl;
		sbc_util.utilSysLog(str_s_log.str());
	}
	delete stmt;
	delete con;
	return db_stat;
}

/***********************************************************************
* Function Name: ResetInOutStatus
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: August 2, 2017
*
* Description: update a workers in/out status
*
* Arguments:
*
* Required Files/Databases: SPSControllerDB
*
*
* Non System Routines Called:
*
*
* Return Value: bool new_update: indicates whether a user's inout status was updated
*
* Error Codes/Exceptions:

*
* OS Specific Assumptions:
*
*
* Local Variables:
 		DBStatusEnum db_stat: return value
		GroundWorker gw_db: container of the worker details fetched from DB
		string query_statement: container of sql request strings
		stringstream str_holder: temp holder for converting int to string
		bool is_user_exist: checker if a user is in database
		sql::Connection *con: sql connection object
		sql::Statement *stmt: sql statement object
		sql::ResultSet *res: sql result object
*
* Modification History:
* Date   Developer   Action
* 08/02/2017  dslasdoce    creation
***********************************************************************/
bool ResetInOutStatus()
{
	bool new_update = false;
	DBStatusEnum db_stat = DB_ERROR;
	Personnel personnel;
	GroundWorker gworker;
	string query_statement;
	stringstream str_holder;
	stringstream str_s_log;
	bool is_user_exist = false;
	sql::Connection *con = NULL;
	sql::Statement *stmt = NULL;
	sql::ResultSet *res = NULL;
	DBConnectionParam db_con_param;

	db_con_param.db_name = DB_SPS;
	db_con_param.db_host = DB_HOST;
	db_con_param.db_username = DB_USERNAME;
	db_con_param.db_password = DB_PASSWORD;

	try{
	db_stat = dbConnect(&db_con_param, &con);

		if(db_stat != DB_CONNECTED){
			sbc_util.utilSysLog("\n*DB: Update InOutStatus Failed\n");
		} else{
			/*Get all IN workers on db*/
			query_statement = "SELECT * from" + TBL_PERSONNEL
					+ "WHERE `InOut` = 1";

			stmt = con->createStatement();
			res = stmt->executeQuery(query_statement);


			while (res->next()) {
				personnelCopyFromDBResult(&personnel, res);
				/*check if they are still on the detected tag list*/
				if(find(active_tid_list.begin(), active_tid_list.end(),
						personnel.tag_id ) == active_tid_list.end() ){

					str_s_log.str(string());
					str_s_log<< "\n### TAG ID " <<  personnel.tag_id << " is no longer detected: ";
					str_s_log<< "WORKER will be marked as OUT"<< endl;
					sbc_util.utilSysLog(str_s_log.str());

					gworker.gw_access_card = personnel.card_id;
					gworker.gw_tag_id = personnel.tag_id;
					gworker.gw_name = personnel.name;
					UpdateInOutStatus(gworker, false);

					new_update = true;

				}
			}
			delete res;
		}

	} catch(sql::SQLException &e) {
		str_s_log.str(string());
		str_s_log << "# ERR: SQLException in " << __FILE__;
		str_s_log << "(" << __FUNCTION__ << ") on line " << __LINE__ << endl;
		str_s_log << "# ERR: " << e.what();
		str_s_log << " (MySQL error code: " << e.getErrorCode();
		str_s_log << ", SQLState: " << e.getSQLState() << " )" << endl;
		sbc_util.utilSysLog(str_s_log.str());
	} catch(std::exception &e){
		str_s_log.str(string());
		str_s_log<< e.what() <<endl;
		sbc_util.utilSysLog(str_s_log.str());
	}

	delete stmt;
	delete con;
	return new_update;
}

/***********************************************************************
* Function Name: CheckTagStatus
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 14, 2017
*
* Description: ccheck the tastatus of a tag in the database
*
* Arguments:
 		string tag_id: tag identification to be verified
* Required Files/Databases:
*
*
* Non System Routines Called:
*
*
* Return Value:
*		TagStatusEnum tag_stat: status of the verified tag
* Error Codes/Exceptions:

*
* OS Specific Assumptions:
*
*
* Local Variables:
	 	DBStatusEnum db_stat: return value
		Personnel personnel: container of the personnel details fetched from DB
		string query_statement: container of sql request strings
		stringstream str_holder: temp holder for converting int to string
		bool is_tag_exist: checker if a tag is in database
		sql::Connection *con: sql connection object
		sql::Statement *stmt: sql statement object
		sql::ResultSet *res: sql result object
*
* Modification History:
* Date   Developer   Action
* 07/14/2017  dslasdoce    creation
***********************************************************************/
TagStatusEnum CheckTagStatus(string tag_id)
{
	TagStatusEnum tag_stat = TAG_ERROR;
	stringstream str_s_log;
	sql::Connection *con = NULL;
	sql::Statement *stmt = NULL;
	sql::ResultSet *res = NULL;

	try{
		DBStatusEnum db_stat = DB_ERROR;
		string query_statement;

		DBConnectionParam db_con_param;
		Personnel personnel;
		bool is_tag_exist = false;

		db_con_param.db_name = DB_SPS;
		db_con_param.db_host = DB_HOST;
		db_con_param.db_username = DB_USERNAME;
		db_con_param.db_password = DB_PASSWORD;

		db_stat = dbConnect(&db_con_param, &con);
		if(db_stat != DB_CONNECTED){
			sbc_util.utilSysLog("DB Connection Failed\n");
		} else{
			/*Check if tag is tied to a personnel*/
			query_statement = "SELECT * from" + TBL_PERSONNEL + "WHERE TIDP = "
				+ "\'" + tag_id + "\'" + "";
			stmt = con->createStatement();
			res = stmt->executeQuery(query_statement);

			if (res->next()) {
				//sbc_util.utilSysLog("*******Personnel Info*******\n");
				personnelCopyFromDBResult(&personnel, res);
				if( personnel.gw_is_in == true)
					tag_stat = TAG_USER_ALREADY_IN;
				else
					tag_stat = TAG_UPDATE_USER_INOUT;
			} else {
				str_s_log.str(string());
				str_s_log << "TAG \'" << tag_id << "\' DOES NOT EXIST" <<endl;
				sbc_util.utilSysLog(str_s_log.str());
				tag_stat = TAG_NOT_RECOGNIZED;
			}
			delete res;


		}


		} catch(sql::SQLException &e) {
			str_s_log.str(string());
			str_s_log << "# ERR: SQLException in " << __FILE__;
			str_s_log << "(" << __FUNCTION__ << ") on line " << __LINE__ << endl;
			str_s_log << "# ERR: " << e.what();
			str_s_log << " (MySQL error code: " << e.getErrorCode();
			str_s_log << ", SQLState: " << e.getSQLState() << " )" << endl;
			sbc_util.utilSysLog(str_s_log.str());
		} catch(std::exception &e){
			str_s_log.str(string());
			str_s_log<< e.what() <<endl;
			sbc_util.utilSysLog(str_s_log.str());
		}
	delete stmt;
	delete con;
	return tag_stat;
}

/***********************************************************************
* Function Name: CheckTagStatus
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 14, 2017
*
* Description: ccheck the tastatus of a tag in the database
*
* Arguments:
 		string tag_id: tag identification to be verified
* Required Files/Databases:
*
*
* Non System Routines Called:
*
*
* Return Value:
*		TagStatusEnum tag_stat: status of the verified tag
* Error Codes/Exceptions:

*
* OS Specific Assumptions:
*
*
* Local Variables:
	 	DBStatusEnum db_stat: return value
		Personnel personnel: container of the personnel details fetched from DB
		string query_statement: container of sql request strings
		stringstream str_holder: temp holder for converting int to string
		bool is_tag_exist: checker if a tag is in database
		sql::Connection *con: sql connection object
		sql::Statement *stmt: sql statement object
		sql::ResultSet *res: sql result object
*
* Modification History:
* Date   Developer   Action
* 07/14/2017  dslasdoce    creation
***********************************************************************/
CardStatusEnum VerifyCardStatus(string card_id)
{
	CardStatusEnum card_stat = CARD_ERROR;
	sql::Connection *con = NULL;
	sql::Statement *stmt = NULL;
	sql::ResultSet *res = NULL;
	stringstream str_s_log;
	try{
		DBStatusEnum db_stat = DB_ERROR;
		string query_statement;

		DBConnectionParam db_con_param;
		Personnel personnel;
		GroundWorker gw_local;
		bool is_tag_exist = false;

		db_con_param.db_name = DB_SPS;
		db_con_param.db_host = DB_HOST;
		db_con_param.db_username = DB_USERNAME;
		db_con_param.db_password = DB_PASSWORD;

		db_stat = dbConnect(&db_con_param, &con);
		if(db_stat != DB_CONNECTED){
			sbc_util.utilSysLog("DB Connection Failed\n");
		} else{
			/*Check if tag is tied to a personnel*/
			query_statement = "SELECT * from" + TBL_PERSONNEL + "WHERE AccessCardUID = "
				+ "\'" + card_id + "\'" + "";
			stmt = con->createStatement();
			res = stmt->executeQuery(query_statement);

			if (res->next()) {
				sbc_util.utilSysLog("\n*******Personnel Info*******");
				personnelCopyFromDBResult(&personnel, res);
				personnelPrintStatus(personnel);
				if(personnel.gw_is_in){
					card_stat = CARD_OK;
				}
				else {
					str_s_log.str(string());
					str_s_log << "!!! TAG of USER with AccessCardUID \'"
							<< card_id << "\' IS NOT DETECTED : Checking Status Will not Be Updated !!!" <<endl;
					sbc_util.utilSysLog(str_s_log.str());
					card_stat = CARD_USER_TAG_NOTDETECTED;
				}

			} else {
				str_s_log.str(string());
				str_s_log << "AccessCardUID \'" << card_id << "\' DOES NOT EXIST" <<endl;
				sbc_util.utilSysLog(str_s_log.str());
				card_stat = CARD_NOT_RECOGNIZED;
			}
			delete res;

			}



		} catch(sql::SQLException &e) {
			str_s_log.str(string());
			str_s_log << "# ERR: SQLException in " << __FILE__;
			str_s_log << "(" << __FUNCTION__ << ") on line " << __LINE__ << endl;
			str_s_log << "# ERR: " << e.what();
			str_s_log << " (MySQL error code: " << e.getErrorCode();
			str_s_log << ", SQLState: " << e.getSQLState() << " )" << endl;
			sbc_util.utilSysLog(str_s_log.str());
		} catch(std::exception &e){
			str_s_log.str(string());
			str_s_log<< e.what() <<endl;
			sbc_util.utilSysLog(str_s_log.str());
		}
	delete stmt;
	delete con;
	return card_stat;
}

void getLocationID(void)
{
	sql::Connection *con = NULL;
	sql::Statement *stmt = NULL;
	sql::ResultSet *res = NULL;
	stringstream str_s_log;
	try{
		DBStatusEnum db_stat = DB_ERROR;
		string query_statement;

		DBConnectionParam db_con_param;
		Personnel personnel;
		GroundWorker gw_local;
		bool is_tag_exist = false;

		db_con_param.db_name = DB_SPS;
		db_con_param.db_host = DB_HOST;
		db_con_param.db_username = DB_USERNAME;
		db_con_param.db_password = DB_PASSWORD;

		db_stat = dbConnect(&db_con_param, &con);
		if(db_stat != DB_CONNECTED){
			sbc_util.utilSysLog("DB Connection Failed\n");
		} else{
			/*Check if tag is tied to a personnel*/
			query_statement = "SELECT Sys_HumpyID from " + TBL_HUMPYSETTING;
			stmt = con->createStatement();
			res = stmt->executeQuery(query_statement);

			if (res->next()) {
				sbc_util.utilSysLog("\n*******SPS Location ID Obtained*******\n");
				LOCATION_ID = res->getString("Sys_HumpyID");
				sbc_util.loc_id = LOCATION_ID;
				sbc_util.utilSysLog(LOCATION_ID);

			} else {
				str_s_log.str(string());
				str_s_log << "NO LocationID in humpysetting" <<endl;
				sbc_util.utilSysLog(str_s_log.str());
			}
		}
	}catch(sql::SQLException &e) {
		str_s_log.str(string());
		str_s_log << "# ERR: SQLException in " << __FILE__;
		str_s_log << "(" << __FUNCTION__ << ") on line " << __LINE__ << endl;
		str_s_log << "# ERR: " << e.what();
		str_s_log << " (MySQL error code: " << e.getErrorCode();
		str_s_log << ", SQLState: " << e.getSQLState() << " )" << endl;
		sbc_util.utilSysLog(str_s_log.str());
	} catch(std::exception &e){
		str_s_log.str(string());
		str_s_log<< e.what() <<endl;
		sbc_util.utilSysLog(str_s_log.str());
	}
	delete res;
	delete stmt;
	delete con;
}

void setSPSDBParameters()
{
	sql::Connection *con = NULL;
	sql::Statement *stmt = NULL;
	sql::ResultSet *res = NULL;
	stringstream str_s_log;
	stringstream dbparam_all_stream;
	string str_dbparameter;
	stringstream dbfield_stream;
	string *db_global_param;

	try{
		DBStatusEnum db_stat = DB_ERROR;
		string query_statement;

		DBConnectionParam db_con_param;
		Personnel personnel;
		GroundWorker gw_local;
		bool is_tag_exist = false;

		db_con_param.db_name = DB_LOCAL_SPS;
		db_con_param.db_host = DB_LOCAL_HOST;
		db_con_param.db_username = DB_LOCAL_USERNAME;
		db_con_param.db_password = DB_LOCAL_PASSWORD;

		db_stat = dbConnect(&db_con_param, &con);
		if(db_stat != DB_CONNECTED){
			sbc_util.utilSysLog("DB Connection Failed\n");
		} else{
			/*Check if tag is tied to a personnel*/
			query_statement = "SELECT " + DBCON_COLUM  + " from " + TBL_HUMPYSETTING;
			stmt = con->createStatement();
			res = stmt->executeQuery(query_statement);

			if (res->next()) {
				sbc_util.utilSysLog("\n*******SPS DBParam ID Obtained*******\n");
				dbparam_all_stream << res->getString(DBCON_COLUM) << endl;
				while(getline(dbparam_all_stream, str_dbparameter, ';'))
				{
					dbfield_stream.str(string());
					dbfield_stream.clear();
					dbfield_stream << str_dbparameter << endl;
					sbc_util.utilSysLog(dbfield_stream.str());

					getline(dbfield_stream, str_dbparameter, '=');
					if(str_dbparameter == DBPARAM_KEY_HOST){
						db_global_param = &DB_HOST;
					}
					else if(str_dbparameter == DBPARAM_KEY_UID){
						db_global_param = &DB_USERNAME;
					}
					else if(str_dbparameter == DBPARAM_KEY_PWD){
						db_global_param = &DB_PASSWORD;
					}
					else if(str_dbparameter == DBPARAM_KEY_DBNAME){
						db_global_param = &DB_SPS;
					}
					else{
						sbc_util.utilSysLog("\nWRONG DB PAREMETER!\n");
						exit(0);
					}

					/*copy parameter to global var*/
					getline(dbfield_stream, str_dbparameter, '=');
					str_dbparameter.erase(
						remove(str_dbparameter.begin(),str_dbparameter.end(),'\n')
						,str_dbparameter.end());
					*db_global_param = str_dbparameter;
				}

			} else {
				str_s_log.str(string());
				str_s_log << "NO database details in humpysetting" <<endl;
				sbc_util.utilSysLog(str_s_log.str());
			}
		}
	}catch(sql::SQLException &e) {
		str_s_log.str(string());
		str_s_log << "# ERR: SQLException in " << __FILE__;
		str_s_log << "(" << __FUNCTION__ << ") on line " << __LINE__ << endl;
		str_s_log << "# ERR: " << e.what();
		str_s_log << " (MySQL error code: " << e.getErrorCode();
		str_s_log << ", SQLState: " << e.getSQLState() << " )" << endl;
		sbc_util.utilSysLog(str_s_log.str());
	} catch(std::exception &e){
		str_s_log.str(string());
		str_s_log<< e.what() <<endl;
		sbc_util.utilSysLog(str_s_log.str());
	}
	delete res;
	delete stmt;
	delete con;
}
