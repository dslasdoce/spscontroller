/*
 * utility.cpp
 *
 *  Created on: 25 Jul 2017
 *      Author: dslasdoce
 */

#include <iostream>
#include <sstream>
#include <fstream>
#include <utility.h>
#include <stdlib.h>
#include <ctime>
#include <sys/stat.h>
#include <sbc.h>
/*Third Party Modules*/
#include <mysql_connection.h>
#include <mysql_driver.h>
#include <cppconn/driver.h>
#include <cppconn/exception.h>
#include <cppconn/resultset.h>
#include <cppconn/statement.h>

#include <unistd.h>
#include <string.h> /* for strncpy */

#include <sys/types.h>
#include <sys/socket.h>
#include <sys/ioctl.h>
#include <netinet/in.h>
#include <net/if.h>
#include <arpa/inet.h>

#include <stropts.h>
#include <linux/netdevice.h>


using namespace std;

/***********************************************************************
* Function Name: utilSysLog
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 27, 2017
*
* Description: write a text to sbclog
*
* Arguments:
 		string buffer: text to be written and saved
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
* 07/27/2017  dslasdoce    creation
***********************************************************************/
void SBCUtility::utilSysLog(string buffer)
{
	while(is_locked);
	is_locked = true;

	ofstream myfile (fname_logs, ios_base::app);
	if (myfile.is_open()){
		myfile << buffer;
		myfile.close();
	}
	else cout << "\nUnable to open log file" << endl;
	cout << buffer;
	is_locked = false;
}

/***********************************************************************
* Function Name: utilSysLog
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 27, 2017
*
* Description: prepare the text file and directory to save logs
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
* 07/27/2017  dslasdoce    creation
***********************************************************************/
void SBCUtility::utilSysLogInit()
{

	struct stat st = {0};
	const char* homeDir = getenv("HOME");
	stringstream logs_dir_str;
	string logs_dir;
	stringstream fname;

	logs_dir_str << homeDir << dir_logs;
	logs_dir = logs_dir_str.str();
	if(stat(logs_dir.c_str(), &st) == -1)
		mkdir(logs_dir.c_str(), 0700);

	is_locked = true;


	time_t now = time(0);
	tm* ltm = localtime(&now);


	fname << homeDir << dir_logs << dir_logs <<"_"
			<< 1900 + ltm->tm_year << "_"
			<< 1 + ltm->tm_mon << "_"
			<< ltm->tm_mday << "_"
			<< ltm->tm_hour << "_"
			<< ltm->tm_min << "_"
			<< ltm->tm_sec << ".txt";
	fname_logs = fname.str();
	cout << fname_logs << endl;
	ofstream myfile (fname_logs);
	if (myfile.is_open()){
		cout << "Log File Creation Success\n";
		myfile.close();
	}
	else cout << "\nUnable to open file\n";

	is_locked = false;
}

void SBCUtility::utilAppLog(string component, string log_id, string log_status
		, string log_action, string log_input)
{
	string query_str_frmt = "INSERT INTO applicationlogs "
			"(Component, LogID, LogStatus, LogAction, LogInput, HumpyIPAddress, LocationID) "
			"VALUES ('%s', '%s', '%s', '%s', '%s', '%s', '%s')";

	char *insert_str = new char[query_str_frmt.length()
								+ component.length()
								+ log_id.length()
								+ log_status.length()
								+ log_action.length()
								+ log_input.length()
								+ wlan_ip.length()
								+ loc_id.length()];
	sprintf(insert_str, query_str_frmt.c_str()
					, component.c_str()
					, log_id.c_str()
					, log_status.c_str()
					, log_action.c_str()
					, log_input.c_str()
					, wlan_ip.c_str()
					, loc_id.c_str());
	sql::Connection *con = NULL;
	sql::Statement *stmt = NULL;
	sql::ResultSet *res = NULL;
	string query_statement;
	stringstream str_s_log;


	try{
		/*Prepare connection object*/
		sql::Driver *driver;
		driver = get_driver_instance();

		con = driver->connect(db_con_param.db_host, db_con_param.db_username,
				db_con_param.db_password);

		if ( con->isValid() ){
		con->setSchema(db_con_param.db_name);


			query_statement = insert_str;
			//cout << query_statement << endl;
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
		sbc_util.utilSysLog(str_s_log.str());;

	} catch(std::exception &e){
		str_s_log.str(string());
		str_s_log<< "**********\nERROR\n "<<e.what() << "\n**********\n" <<endl;
		sbc_util.utilSysLog(str_s_log.str());
	}

	delete stmt;
	delete con;
	delete[] insert_str;
}

bool SBCUtility::getWLANIP()
{
	utilSysLog("\n\n******Getting WLAN IP ADD*****\n");
	int s;
	struct ifconf ifconf;
	struct ifreq ifr[50];
	int ifs;
	int i;

	s = socket(AF_INET, SOCK_STREAM, 0);
	if (s < 0) {
		perror("socket");
		return false;
	}

	ifconf.ifc_buf = (char *) ifr;
	ifconf.ifc_len = sizeof ifr;

	if (ioctl(s, SIOCGIFCONF, &ifconf) == -1) {
		perror("ioctl");
		return false;
	}

	ifs = ifconf.ifc_len / sizeof(ifr[0]);

	for (i = 0; i < ifs; i++) {
		char ip[INET_ADDRSTRLEN];
		struct sockaddr_in *s_in = (struct sockaddr_in *) &ifr[i].ifr_addr;

		if (!inet_ntop(AF_INET, &s_in->sin_addr, ip, sizeof(ip))) {
			perror("inet_ntop");
			return false;
		}
		if( (strcmp(ifr[i].ifr_name, "lo") == 0)
				|| (strcmp(ip, db_con_param.db_host.c_str()) == 0) ){

		}
		else{
			wlan_ip = ip;
			utilSysLog(wlan_ip);
			utilSysLog("\n\n");
			break;
		}
	}

	close(s);
	return true;

}





