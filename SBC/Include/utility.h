/*
 * utility.h
 *
 *  Created on: 25 Jul 2017
 *      Author: nic
 */

#ifndef SOURCE_SPS_CONTROLLER_SBC_INCLUDE_UTILITY_H_
#define SOURCE_SPS_CONTROLLER_SBC_INCLUDE_UTILITY_H_
#include <iostream>
#include <sbc.h>

#define	LOG_COMPONENT_SPS			((string)"SPS")
#define	LOG_COMPONENT_WORKER		((string)"Worker")
#define	LOG_COMPONENT_FAULTMGR		((string)"FaultManager")

#define LOG_ID_SPS_RFID				((string)"RFID")
#define LOG_ID_SPS_CREADER			((string)"CardReader")
#define	LOG_ID_SPS_SOFTWARE			((string)"Software")
#define LOG_ID_SPS_DB				((string)"Database")
#define	LOG_ID_FM_SPS				((string)"SPS")

#define	LOG_STATUS_SPS_INIT			((string)"Initialized")
#define	LOG_STATUS_SPS_ER			((string)"Error")
#define	LOG_STATUS_WORKER_CIN		((string)"Checked-In")
#define	LOG_STATUS_WORKER_COUT		((string)"Not Checked-in")

#define	LOG_ACTION_SPS_NONE			((string)"")
#define	LOG_ACTION_SPS_RESTART		((string)"Restarting")
#define	LOG_ACTION_WORKER_IN		((string)"IN")
#define	LOG_ACTION_WORKER_OUT		((string)"OUT")
#define	LOG_ACTION_WORKER_CIN		((string)"Check-in")
#define	LOG_ACTION_WORKER_COUT		((string)"Check-out")

#define LOG_INPUT_SPS				((string)"")

class SBCUtility{
private:
	bool is_locked = false;
	string wlan_ip = "0.0.0.0";
	std::string fname_logs;

public:
	DBConnectionParam db_con_param;
	string loc_id = "";
	std::string dir_logs;
	void utilSysLogInit();
	void utilSysLog(std::string buffer);
	void utilAppLog(std::string component, std::string log_id
			, std::string log_status, std::string log_action
			, std::string log_input);
	bool getWLANIP();
};

extern SBCUtility sbc_util;

#endif /* SOURCE_SPS_CONTROLLER_SBC_INCLUDE_UTILITY_H_ */
