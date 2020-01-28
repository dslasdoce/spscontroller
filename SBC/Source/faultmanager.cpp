/*
 * main.cpp
 *
 *  Created on: 3 Aug 2017
 *      Author: nic
 */

#include<iostream>
#include<fstream>
#include<stdlib.h>
#include<unistd.h>
#include<stdio.h>
#include<vector>
#include<sstream>
#include<string>
#include<algorithm>
#include<signal.h>
#include<cstdlib>

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <netdb.h>

#include <ctime>
#include <sys/stat.h>

/*Third Party Modules*/
#include <mysql_connection.h>
#include <mysql_driver.h>
#include <cppconn/driver.h>
#include <cppconn/exception.h>
#include <cppconn/resultset.h>
#include <cppconn/statement.h>

#include <sbc.h>
#include<utility.h>
SBCUtility sbc_util;
using namespace std;

#define F_NAME_DBCON		((string)"Database Connection Error")
#define F_NAME_SPS			((string)"SPS Connection Error")
#define F_NAME_RFID			((string)"RFID Connection Error")
#define F_NAME_CREADER		((string)"Card Reader Connection Error")

#define F_ID_DBCON			((int) 1)
#define F_ID_SPS			((int) 2)
#define F_ID_RFID			((int) 3)
#define F_ID_CREADER		((int) 4)

typedef struct{
	int fault_id;
	string fault_name;
}FaultTypeStruct;

static DBStatusEnum checkDBStatus(void);
static SPSStatusEnum checkSPSStatus(void);
static RFIDStatusEnum checkRFID(void);
static CReaderStatusEnum checkCardReader(void);
static void logFault(FaultTypeStruct fault_type);
static void fmgetLocationID(void);
static void restartSPS(void);
static void restartDB(void);
static void restartRFID(void);
static bool setFMDBParameters(void);
volatile sig_atomic_t flag = 0;
static void keyIrqInit();
static void exitHandler();
bool is_locked;
string fname_logs;

string FMDB_HOST;
string FMDB_USERNAME;
string FMDB_PASSWORD;
string FMDB_SPS;


int main()
{
	atexit(exitHandler);
	keyIrqInit();
	sbc_util.dir_logs = "/faultmgrlogs";
	sbc_util.utilSysLogInit();
	SPSStatusEnum sps_stat = SPS_OK;
	FaultTypeStruct fault_type;
	while(setFMDBParameters() != true);
	sbc_util.db_con_param.db_name = FMDB_SPS;
	sbc_util.db_con_param.db_host = FMDB_HOST;
	sbc_util.db_con_param.db_username = FMDB_USERNAME;
	sbc_util.db_con_param.db_password = FMDB_PASSWORD;
	sbc_util.getWLANIP();
	fmgetLocationID();
	while(1)
	{
		/*Check Database Status*/
		if (checkDBStatus() != DB_OK){
			fault_type.fault_id = F_ID_DBCON;
			fault_type.fault_name = F_NAME_DBCON;
			logFault(fault_type);
			restartDB();
			sleep(2);
		}

		if (checkCardReader() != CREADER_OK){
			sbc_util.utilSysLog("Card Reader NOT Connected!!!\n");
			fault_type.fault_id = F_ID_CREADER;
			fault_type.fault_name = F_NAME_CREADER;
			logFault(fault_type);
		}
		else{
			sbc_util.utilSysLog("Card Reader  Connected...\n");
		}



		/*Check SPS Status*/
		for(uint8_t i = 0; i < 3; i++){
			sps_stat = checkSPSStatus();
			if(sps_stat == SPS_OK)
				break;
			sleep(1);
		}
		if ( sps_stat != SPS_OK){
			fault_type.fault_id = F_ID_SPS;
			fault_type.fault_name = F_NAME_SPS;
			logFault(fault_type);

			sbc_util.utilAppLog(
					LOG_COMPONENT_FAULTMGR
					, LOG_ID_FM_SPS
					, LOG_STATUS_SPS_ER
					, LOG_ACTION_SPS_RESTART
					, LOG_INPUT_SPS);

			restartSPS();
		}

		/*Check RFID Status*/
		if (checkRFID() != RFID_OK){
			fault_type.fault_id = F_ID_RFID;
			fault_type.fault_name = F_NAME_RFID;
			logFault(fault_type);
			restartRFID();
		}
		sleep(5);
	}
}

/***********************************************************************
* Function Name: restartRFID
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: August 08, 2017
*
* Description: reboot the rfid
*
* Arguments:
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
* 08/08/2017  dslasdoce    creation
***********************************************************************/
static void restartRFID(void)
{
	sbc_util.utilSysLog("Restarting RFID...\n");
	system("sshpass -p \"impinj\" ssh -o StrictHostKeyChecking=no root@10.0.10.5 <<HERE\n"
	                "osshell developer\n"
	                "reboot\n"
	                "HERE\n");
	sbc_util.utilSysLog("Waiting for RFID to reboot...\n");
	sleep(60);


}

/***********************************************************************
* Function Name: restartDB
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: August 08, 2017
*
* Description: restart database service
*
* Arguments:
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
* 08/08/2017  dslasdoce    creation
***********************************************************************/
static void restartDB(void)
{
	sbc_util.utilSysLog("Restarting DB...\n");
	system("sudo /etc/init.d/mysql restart");
}

/***********************************************************************
* Function Name: restartSPS
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: August 08, 2017
*
* Description: restart SPS app
*
* Arguments:
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
* 08/08/2017  dslasdoce    creation
***********************************************************************/
static void restartSPS(void)
{
	FILE *sys_file;
	char sys_buff[1000];
	string sys_user;

	/*Get Logged In User name*/
	sbc_util.utilSysLog("Getting current user...\n");
	bzero(sys_buff, 1000);
	sys_file = (FILE*)popen("who | awk \'{print $1}\'", "r");
	if(sys_file == 0)
		sbc_util.utilSysLog("ERROR Issuing Sys CMD\n");
	fgets(sys_buff, 1000, sys_file);
	sys_user = sys_buff;
	sys_user.pop_back();
	fclose(sys_file);

	/*Start SPS*/
	sbc_util.utilSysLog("Starting SPS...\n");
	bzero(sys_buff, sizeof(sys_buff));
	sprintf(sys_buff
			,"sudo -u %s ./SafetyAmmo &"
			, sys_user.c_str());
	sbc_util.utilSysLog(sys_buff);
	sbc_util.utilSysLog("\n");
	sys_file = (FILE*)popen(sys_buff, "r");
	bzero(sys_buff, sizeof(sys_buff));
	if(sys_file == 0)
		sbc_util.utilSysLog("ERROR Issuing Sys CMD\n");
	fclose(sys_file);

	sbc_util.utilSysLog("Resuming System Verification...\n");
}

/***********************************************************************
* Function Name: checkSPSStatus
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: August 08, 2017
*
* Description: check if SPS app is still working properly via network
*
* Arguments:
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
* 08/08/2017  dslasdoce    creation
***********************************************************************/
SPSStatusEnum checkSPSStatus(void)
{
	SPSStatusEnum sps_stat = SPS_OK;
	int sockfd, portno, n;
	struct sockaddr_in serv_addr;
	struct hostent *server;
	char buffer[256];
	const char* sbc_host = "localhost";
	const char* sbc_port = "8888";

	sbc_util.utilSysLog("\n\n### Checking SPS\n");

	portno = atoi(sbc_port);

	sockfd = socket(AF_INET, SOCK_STREAM, 0);

	if (sockfd < 0){
		sbc_util.utilSysLog("ERROR opening socket\n");
		sps_stat = SPS_SOCK_ERROR;
	}

	server = gethostbyname(sbc_host);
	if (server == NULL) {
		sbc_util.utilSysLog("ERROR, no such host\n");
		sps_stat = SPS_HOST_ERROR;
	}

	bzero((char *) &serv_addr, sizeof(serv_addr));
	serv_addr.sin_family = AF_INET;
	bcopy((char *)server->h_addr, (char *)&serv_addr.sin_addr.s_addr, server->h_length);
	serv_addr.sin_port = htons(portno);

	if (connect(sockfd,(struct sockaddr *) &serv_addr,sizeof(serv_addr)) < 0){
		sbc_util.utilSysLog("ERROR connecting to network\n");
		sps_stat = SPS_NETWORK_ERROR;
	}

	bzero(buffer,256);
	strcpy(buffer, RQ_SBC_HCHK);

	n = write(sockfd,buffer,strlen(buffer));
	if (n < 0){
		sbc_util.utilSysLog("ERROR writing to socket\n");
		sps_stat = SPS_WR_ERROR;
	}

	bzero(buffer,256);
	n = read(sockfd,buffer,255);
	if (n < 0){
		sbc_util.utilSysLog("ERROR reading from socket\n");
		sps_stat = SPS_RD_ERROR;
	}
	sbc_util.utilSysLog(buffer);
	sbc_util.utilSysLog("\n");

	close(sockfd);
	return sps_stat;
}

/***********************************************************************
* Function Name: checkDBStatus
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: August 08, 2017
*
* Description: check if database service is still working properly via query
*
* Arguments:
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
* 08/08/2017  dslasdoce    creation
***********************************************************************/
static DBStatusEnum checkDBStatus(void)
{
	DBStatusEnum db_stat = DB_ERROR;
	string query_statement;
	sql::Connection *con = NULL;
	sql::Statement *stmt = NULL;
	sql::ResultSet *res = NULL;
	vector<GroundWorker> gw_list;
	/*Create Container*/
	gw_list.clear();
	GroundWorker gw_current;
	DBConnectionParam db_con_param;
	Personnel personnel;

	db_con_param.db_name = FMDB_SPS;
	db_con_param.db_host = FMDB_HOST;
	db_con_param.db_username = FMDB_USERNAME;
	db_con_param.db_password = FMDB_PASSWORD;

	sbc_util.utilSysLog("\n\n### Checking DB\n");

	stringstream str_s_log;
	try{
		/*Prepare connection object*/
		sql::Driver *driver;
		driver = get_driver_instance();

		con = driver->connect(db_con_param.db_host, db_con_param.db_username,
				db_con_param.db_password);

		if ( con->isValid() ){
		con->setSchema(db_con_param.db_name);


			query_statement = "SELECT * FROM personnel";
			/* Connect to the MySQL test database */
			stmt = con->createStatement();
			res = stmt->executeQuery(query_statement);

			/*Add all the checked in groung worker to gw_list*/
			if (res->next()) {
				sbc_util.utilSysLog("DB Running\n");
				db_stat = DB_OK;
			}

		}
		else{
			cout << "DB FAILED" << endl;
		}
		delete stmt;
		delete res;
		delete con;

	} catch(sql::SQLException &e) {
		cout << "DB FAILED" << endl;
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

	if(db_stat != DB_OK){
		sbc_util.utilAppLog(
				LOG_COMPONENT_FAULTMGR
				, LOG_ID_SPS_DB
				, LOG_STATUS_SPS_ER
				, LOG_ACTION_SPS_RESTART
				, LOG_INPUT_SPS);
	}
	return db_stat;
}

/***********************************************************************
* Function Name: checkRFID
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: August 08, 2017
*
* Description: check if RFID is still working properly via network
*
* Arguments:
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
* 08/08/2017  dslasdoce    creation
***********************************************************************/
static RFIDStatusEnum checkRFID(void)
{
	RFIDStatusEnum stat = RFID_ERROR;
	int portnumber = 8889;
    socklen_t clilen;
    char buffer[256];
    struct sockaddr_in serv_addr, cli_addr;
    int n;
    int enable = 1;
    string str_log;
    string hcheck_ack = "SPS Network OK\n";
    stringstream str_s_log;
    int sockfd, newsockfd;
	fd_set read_fds, write_fds, except_fds;
	struct timeval timeout;

	sbc_util.utilSysLog("\n\n### Checking RFID\n");

	timeout.tv_sec = 10;
	timeout.tv_usec = 0;

    sockfd = socket(AF_INET, SOCK_STREAM, 0);
    if (sockfd < 0){
    	str_log = "ERROR opening socket\n";
    	sbc_util.utilSysLog(str_log);
       throw runtime_error(str_log);
    }

    bzero((char *) &serv_addr, sizeof(serv_addr));
    setsockopt(sockfd, SOL_SOCKET, SO_REUSEPORT, &enable, sizeof(int));

    serv_addr.sin_family = AF_INET;
    serv_addr.sin_addr.s_addr = INADDR_ANY;
    serv_addr.sin_port = htons(portnumber);

    if (bind(sockfd, (struct sockaddr *) &serv_addr,
             sizeof(serv_addr)) < 0){
    	str_log = "ERROR on binding\n";
    	sbc_util.utilSysLog(str_log);
    	throw runtime_error(str_log);
    }

    sbc_util.utilSysLog("Checking RFID Response...\n");

	listen(sockfd,5);
	clilen = sizeof(cli_addr);

	FD_ZERO(&read_fds);
	FD_ZERO(&write_fds);
	FD_ZERO(&except_fds);
	FD_SET(sockfd, &read_fds);

	if (select(sockfd + 1, &read_fds, &write_fds, &except_fds, &timeout) == 1){
		newsockfd = accept(sockfd,
					(struct sockaddr *) &cli_addr,
					&clilen);

		if (newsockfd < 0){
			throw runtime_error("ERROR on accept\n");
		}

		bzero(buffer,256);
		n = read(newsockfd,buffer,255);

		if (n < 0){
			str_log = "ERROR reading from socket\n";
			sbc_util.utilSysLog(str_log);
			throw runtime_error(str_log);
		}

		bzero(buffer, sizeof(buffer));
		sprintf(buffer, "RFID HCHECK OK...\n");
		n = write(newsockfd, buffer, strlen(buffer));

		sbc_util.utilSysLog(buffer);
		stat = RFID_OK;
		close(newsockfd);
	}
	else{
		sbc_util.utilSysLog("RFID is not responding\n");
	}

	if(stat != RFID_OK){
		sbc_util.utilAppLog(
				LOG_COMPONENT_FAULTMGR
				, LOG_ID_SPS_RFID
				, LOG_STATUS_SPS_ER
				, LOG_ACTION_SPS_RESTART
				, LOG_INPUT_SPS);
	}
    close(sockfd);
    return stat;
}

static CReaderStatusEnum checkCardReader(void)
{
	FILE *sys_file;
	char sys_buff[1000];
	string sys_user;
	CReaderStatusEnum stat = CREADER_DISCONNECTED;

	sbc_util.utilSysLog("\n\n### Checking Card Reader\n");
	/*Start SPS*/
	bzero(sys_buff, sizeof(sys_buff));
	sprintf(sys_buff
			,"ls /dev | grep  ttyACM0"
			, sys_user.c_str());
	sbc_util.utilSysLog(sys_buff);
	sbc_util.utilSysLog("\n");
	sys_file = (FILE*)popen(sys_buff, "r");
	bzero(sys_buff, sizeof(sys_buff));

	if(sys_file == 0)
		sbc_util.utilSysLog("ERROR Issuing Sys CMD\n");
	else{

		while (fgets(sys_buff, 1000, sys_file) != 0){
			sbc_util.utilSysLog(sys_buff);
			if(strstr(sys_buff, "ACM0")){
				stat = CREADER_OK;
				break;
			}
		}
		fclose(sys_file);

	}

	if(stat != CREADER_OK){
		sbc_util.utilAppLog(
				LOG_COMPONENT_FAULTMGR
				, LOG_ID_SPS_CREADER
				, LOG_STATUS_SPS_ER
				, LOG_ACTION_SPS_NONE
				, LOG_INPUT_SPS);
	}
	return stat;
}

bool setFMDBParameters(void)
{
	sql::Connection *con = NULL;
	sql::Statement *stmt = NULL;
	sql::ResultSet *res = NULL;
	stringstream str_s_log;
	stringstream dbparam_all_stream;
	string str_dbparameter;
	stringstream dbfield_stream;
	string *db_global_param;
	bool stat = false;
	sbc_util.utilSysLog("\n### Obtaining FMDB Parameters...\n");
	try{
		string query_statement;

		DBConnectionParam db_con_param;
		Personnel personnel;
		GroundWorker gw_local;

		db_con_param.db_name = DB_LOCAL_SPS;
		db_con_param.db_host = DB_LOCAL_HOST;
		db_con_param.db_username = DB_LOCAL_USERNAME;
		db_con_param.db_password = DB_LOCAL_PASSWORD;

		/*Prepare connection object*/
		sql::Driver *driver;
		driver = get_driver_instance();

		con = driver->connect(db_con_param.db_host, db_con_param.db_username,
				db_con_param.db_password);

		if ( !con->isValid() ){
			sbc_util.utilSysLog("DB Connection Failed\n");
		} else{
			con->setSchema(db_con_param.db_name);
			/*Check if tag is tied to a personnel*/
			query_statement = "SELECT " + DBCON_COLUM  + " from " + TBL_HUMPYSETTING;
			stmt = con->createStatement();
			res = stmt->executeQuery(query_statement);

			if (res->next()) {
				sbc_util.utilSysLog("SPS Fault Manager DBParam ID Obtained\n");
				dbparam_all_stream << res->getString(DBCON_COLUM) << endl;
				while(getline(dbparam_all_stream, str_dbparameter, ';'))
				{
					dbfield_stream.str(string());
					dbfield_stream.clear();
					dbfield_stream << str_dbparameter << endl;
					sbc_util.utilSysLog(dbfield_stream.str());

					getline(dbfield_stream, str_dbparameter, '=');
					if(str_dbparameter == DBPARAM_KEY_HOST){
						db_global_param = &FMDB_HOST;
					}
					else if(str_dbparameter == DBPARAM_KEY_UID){
						db_global_param = &FMDB_USERNAME;
					}
					else if(str_dbparameter == DBPARAM_KEY_PWD){
						db_global_param = &FMDB_PASSWORD;
					}
					else if(str_dbparameter == DBPARAM_KEY_DBNAME){
						db_global_param = &FMDB_SPS;
					}
					else{
						sbc_util.utilSysLog("WRONG DB PAREMETER!\n");
						exit(0);
					}
					/*copy parameter to global var*/
					getline(dbfield_stream, str_dbparameter, '=');
					str_dbparameter.erase(
						remove(str_dbparameter.begin(),str_dbparameter.end(),'\n')
						,str_dbparameter.end());
					*db_global_param = str_dbparameter;
					cout << *db_global_param << endl;
				}

				stat = true;
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
		restartDB();
	} catch(std::exception &e){
		str_s_log.str(string());
		str_s_log<< e.what() <<endl;
		sbc_util.utilSysLog(str_s_log.str());
		restartDB();
	}
	delete res;
	delete stmt;
	delete con;
	return stat;
}

static void logFault(FaultTypeStruct fault_type)
{
	string query_statement;
	sql::Connection *con = NULL;
	sql::Statement *stmt = NULL;
	char insert_str[200];
	/*Create Container*/

	DBConnectionParam db_con_param;
	db_con_param.db_name = FMDB_SPS;
	db_con_param.db_host = FMDB_HOST;
	db_con_param.db_username = FMDB_USERNAME;
	db_con_param.db_password = FMDB_PASSWORD;

	sbc_util.utilSysLog("\n\n### Logging  Fault: ");
	sbc_util.utilSysLog(fault_type.fault_name);

	stringstream str_s_log;
	try{
		/*Prepare connection object*/
		sql::Driver *driver;
		driver = get_driver_instance();

		con = driver->connect(db_con_param.db_host, db_con_param.db_username,
				db_con_param.db_password);

		if ( con->isValid() ){
		con->setSchema(db_con_param.db_name);


		sprintf(insert_str,
				"INSERT INTO %s (ID, fault_type, fault_exist) VALUES "
				"(%d,'%s',1) "
				"ON DUPLICATE KEY UPDATE `fault_exist` = 1",
				TBL_FAULTFLAGS.c_str()
				, fault_type.fault_id
				, fault_type.fault_name.c_str());

			query_statement = insert_str;
			/* Connect to the MySQL test database */
			stmt = con->createStatement();
			stmt->execute(query_statement);

		}
		else{
			cout << "DB FAILED" << endl;
		}

	} catch(sql::SQLException &e) {
		cout << "DB FAILED" << endl;
		str_s_log.str(string());
		str_s_log << "# ERR: SQLException in " << __FILE__;
		str_s_log << "(" << __FUNCTION__ << ") on line " << __LINE__ << endl;
		str_s_log << "# ERR: " << e.what();
		str_s_log << " (MySQL error code: " << e.getErrorCode();
		str_s_log << ", SQLState: " << e.getSQLState() << " )" << endl;
		sbc_util.utilSysLog(str_s_log.str());
		restartDB();

	} catch(std::exception &e){
		str_s_log.str(string());
		str_s_log<< "**********\nERROR\n "<<e.what() << "\n**********\n" <<endl;
		sbc_util.utilSysLog(str_s_log.str());
		restartDB();
	}

	delete stmt;
	delete con;
}

static void fmgetLocationID(void)
{
	sql::Connection *con = NULL;
	sql::Statement *stmt = NULL;
	sql::ResultSet *res = NULL;
	sql::Driver *driver = NULL;
	stringstream str_s_log;
	try{
		DBStatusEnum db_stat = DB_ERROR;
		string query_statement;

		DBConnectionParam db_con_param;
		Personnel personnel;
		GroundWorker gw_local;
		bool is_tag_exist = false;

		db_con_param.db_name = FMDB_SPS;
		db_con_param.db_host = FMDB_HOST;
		db_con_param.db_username = FMDB_USERNAME;
		db_con_param.db_password = FMDB_PASSWORD;

		driver = get_driver_instance();
		con = driver->connect(db_con_param.db_host, db_con_param.db_username,
						db_con_param.db_password);

		if ( con->isValid() ){
				con->setSchema(db_con_param.db_name);
				db_stat = DB_CONNECTED;
			}

		if(db_stat != DB_CONNECTED){
			sbc_util.utilSysLog("DB Connection Failed\n");
		} else{
			/*Check if tag is tied to a personnel*/
			query_statement = "SELECT Sys_HumpyID from " + TBL_HUMPYSETTING;
			stmt = con->createStatement();
			res = stmt->executeQuery(query_statement);

			if (res->next()) {
				sbc_util.utilSysLog("\n*******FM: SPS Location ID Obtained*******\n");
				sbc_util.loc_id  = res->getString("Sys_HumpyID");
				sbc_util.utilSysLog(sbc_util.loc_id);

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

static void exitHandler() {
	//rfid_main.closeNetworkPort();
	sbc_util.utilSysLog("Exiting\n");
}

static void keyIrqHandler(int s){
	string str_log;

	str_log = "Caught signal " + to_string(s) + "\n";
	sbc_util.utilSysLog(str_log);
	exit(1);
}

static void keyIrqInit()
{
	/*Initialize signal IRQ handler*/
	struct sigaction sigIntHandler;
	sigIntHandler.sa_handler = keyIrqHandler;
	sigemptyset(&sigIntHandler.sa_mask);
	sigIntHandler.sa_flags = 0;
	sigaction(SIGINT, &sigIntHandler, NULL);
}
