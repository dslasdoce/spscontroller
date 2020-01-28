/*
 * Generic.h
 *
 *  Created on: 12 Jul 2017
 *      Author: nic
 */

#ifndef SOURCE_SPS_CONTROLLER_SBC_INCLUDE_SBC_H_
#define SOURCE_SPS_CONTROLLER_SBC_INCLUDE_SBC_H_

#include<iostream>
#include <vector>

using namespace std;

#define TBL_WORKERSTATUS		((string) " workersinoutstatus ")
#define TBL_PERSONNEL			((string) " personnel ")
#define TBL_HUMPYSETTING		((string) " humpysetting ")
#define TBL_FAULTFLAGS			((string) " faultflags ")
#define CNDTION_CHECKED_IN		((string) " WHERE CheckedIN = 1 ")
#define DBCON_COLUM				((string) "Sys_DBConnection")

#define DB_LOCAL_HOST			((string) "localhost")
#define DB_LOCAL_USERNAME		((string) "root")
#define DB_LOCAL_PASSWORD		((string) "Amm02o16!")
#define DB_LOCAL_SPS			((string) "spscontrollerdb")

#define DBPARAM_KEY_HOST				((string) "server")
#define DBPARAM_KEY_UID					((string) "uid")
#define DBPARAM_KEY_PWD					((string) "pwd")
#define DBPARAM_KEY_DBNAME				((string) "database")

extern string DB_HOST;
extern string DB_USERNAME;
extern string DB_PASSWORD;
extern string DB_SPS;

#define CREADER_PORT			((const char*)"/dev/ttyACM0")
#define CREADER_BAUD			(B115200)

#define RQ_SBC_HCHK				((const char*) "SBC Health Check Request")

typedef enum
{
	DB_ERROR,
	DB_OK,
	DB_CONNECTED
}DBStatusEnum;

typedef enum
{
	RFID_ERROR,
	RFID_OK,
	RFID_CONNECTED
}RFIDStatusEnum;

typedef enum
{
	SPS_ERROR,
	SPS_OK,
	SPS_NETWORK_OK,
	SPS_NETWORK_ERROR,
	SPS_SOCK_ERROR,
	SPS_WR_ERROR,
	SPS_RD_ERROR,
	SPS_HOST_ERROR
}SPSStatusEnum;

typedef enum
{
	TAG_ERROR,
	TAG_OK,
	TAG_UPDATE_USER_INOUT,
	TAG_USER_ALREADY_IN,
	TAG_NOT_RECOGNIZED
}TagStatusEnum;

typedef enum
{
	CARD_ERROR,
	CARD_OK,
	CARD_USER_TAG_NOTDETECTED,
	CARD_NOT_RECOGNIZED
}CardStatusEnum;

typedef enum
{
	WSS_NOT_OK,
	WSS_OK,
}WorkerSafetyStatusEnum;

typedef enum
{
	CREADER_ERROR,
	CREADER_OK,
	CREADER_DISCONNECTED,
	CREADER_CONNECTION_FAILED
}CReaderStatusEnum;

class GroundWorker{
public:
	int gw_id;
	string gw_name;
	string gw_access_card;
	string gw_tag_id;
	bool gw_is_checked_in;
	bool gw_is_in;
};

class Personnel{
public:
	int id;
	int hq_user_id;
	string name;
	string phone_num;
	string address;
	string card_id;
	string tag_id;
	string last_loc_id;
	string msrepl_tran_version;
	string location_group;
	bool gw_is_checked_in;
	bool gw_is_in;
	bool crane_driver;
};

class DBConnectionParam
{
public:
	string db_name;
	string db_host;
	string db_username;
	string db_password;
};

extern vector<string> active_tid_list;
extern string LOCATION_ID;

#endif /* SOURCE_SPS_CONTROLLER_SBC_INCLUDE_SBC_H_ */
