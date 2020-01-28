/*
 * main.cpp
 *
 *  Created on: 10 Jul 2017
 *      Author: nic
 */

#include <iostream>
#include <string>
#include <stdlib.h>
#include <unistd.h>
#include <stdio.h>
#include <signal.h>
#include <rfid.h>
#include <cstdlib>
#include <thread>
#include <sstream>

#include <sbc.h>
#include <sps.h>
#include <utility.h>
#include <cardreader.h>

using namespace std;
volatile sig_atomic_t flag = 0;
static void keyIrqInit();
static void exitHandler();
SBCUtility sbc_util;
RFID rfid_main;
CardReader card_reader;
vector<GroundWorker> gw_list;
vector<string> active_tid_list;
string LOCATION_ID = "";

string DB_HOST;
string DB_USERNAME;
string DB_PASSWORD;
string DB_SPS;

int main()
{
	DBStatusEnum db_stat = DB_ERROR;
	DBConnectionParam db_con_param;
	GroundWorker gworker;
	WorkerSafetyStatusEnum wss_stat = WSS_NOT_OK;
	WorkerSafetyStatusEnum wss_last_stat = WSS_NOT_OK;
	bool new_rfid_update, new_cardreader_update;

	gw_list.clear();

	/*Initialize Logger*/
	sbc_util.dir_logs = "/sbclogs";
	sbc_util.utilSysLogInit();

	/*Initialize IRQ Handlers*/
	atexit(exitHandler);
	keyIrqInit();


	/*Initialize database parameters*/
	setSPSDBParameters();
	getLocationID();
	db_con_param.db_name = DB_SPS;
	db_con_param.db_host = DB_HOST;
	db_con_param.db_username = DB_USERNAME;
	db_con_param.db_password = DB_PASSWORD;

	sbc_util.db_con_param.db_name = DB_SPS;
	sbc_util.db_con_param.db_host = DB_HOST;
	sbc_util.db_con_param.db_username = DB_USERNAME;
	sbc_util.db_con_param.db_password = DB_PASSWORD;

	sbc_util.getWLANIP();

	/*Start Card Reader*/
	if (card_reader.initialize() == CREADER_OK)
		card_reader.cardReaderStart();

	/*start RFID*/
	rfid_main.rfidStart();
	GetCheckedInWorkerStatus(db_con_param);


	/*************test code****************/
//	string tag_id;
//	gworker.gw_access_card = "04873362524880";
//	gworker.gw_tag_id = "0000-0000-0000-0000-0002-56";
//	clearWorkersInOutStatus();
//	UpdateCheckingStatus(gworker);
//	UpdateInOutStatus(gworker, true);
//	exit(0);
	/**************************************/


	while(1){
		clearWorkersInOutStatus();
		new_rfid_update = rfid_main.rfidRead();
		new_cardreader_update = card_reader.cardRead();

		wss_stat = GetCheckedInWorkerStatus(db_con_param);

		if( (new_rfid_update == true) || (new_cardreader_update == true)){
			sbc_util.utilSysLog("\n######Safety Operation Status !#####\n");
			if(wss_stat == WSS_OK)
				sbc_util.utilSysLog("Operation Safe\n\n");
			else
				sbc_util.utilSysLog("Operation Not Safe\n\n");
		}
	}
}

static void exitHandler() {
	//rfid_main.closeNetworkPort();
	rfid_main.rfidStop();
	card_reader.cardReaderStop();
	sbc_util.utilSysLog("Exiting\n");
}

static void keyIrqHandler(int s){
	string str_log;
	rfid_main.closeNetworkPort();
	rfid_main.rfidStop();
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




