/*
 * rfid.cpp
 *
 *  Created on: 14 Jul 2017
 *      Author: nic
 */
#include <iostream>
#include <vector>
#include <algorithm>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <rfid.h>
#include <thread>
#include <sstream>
#include <utility.h>

#include <sbc.h>
#include <sps.h>
using namespace std;

/***********************************************************************
* Function Name: rfidStart
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 18, 2017
*
* Description: start socket listening in another thread
*
* Arguments:

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
* 07/18/2017  dslasdoce    creation
***********************************************************************/
void RFID::rfidStart()
{
	sbc_util.utilSysLog("\n*******RFID Reader Initialization******\n");
	new_tag_detected = false;
    thread t1(&RFID::rfidConfigureNetworkPort, this);
    rfid_thread = &t1;
    rfid_thread->detach();
}

/***********************************************************************
* Function Name: rfidStop
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 18, 2017
*
* Description: stop socket listening, close socket, end thread
*
* Arguments:

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
* 07/18/2017  dslasdoce    creation
***********************************************************************/
void RFID::rfidStop()
{
	run_flag = false;
	sbc_util.utilSysLog("ending RFID\n");
	RFID::closeNetworkPort();
}

/***********************************************************************
* Function Name: rfidConfigureNetworkPort
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 18, 2017
*
* Description: stop socket listening, close socket, end thread
*
* Arguments:

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
* 07/18/2017  dslasdoce    creation
***********************************************************************/
void RFID::rfidConfigureNetworkPort()
{
	int portnumber = 8888;
    socklen_t clilen;
    char buffer[256];
    struct sockaddr_in serv_addr, cli_addr;
    int n;
    int enable = 1;
    string str_log;
    string hcheck_ack = "SPS Network Running OK";
    stringstream str_s_log;
    sockfd = socket(AF_INET, SOCK_STREAM, 0);
    if (sockfd < 0){
    	sbc_util.utilAppLog(
    			LOG_COMPONENT_SPS
    			, LOG_ID_SPS_RFID
    			, LOG_STATUS_SPS_ER
    			, LOG_ACTION_SPS_RESTART
    			, LOG_INPUT_SPS);
    	str_log = "ERROR opening socket";
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
    	sbc_util.utilAppLog(
    			LOG_COMPONENT_SPS
    			, LOG_ID_SPS_RFID
    			, LOG_STATUS_SPS_ER
    			, LOG_ACTION_SPS_RESTART
    			, LOG_INPUT_SPS);
    	throw runtime_error(str_log);
    }

	sbc_util.utilAppLog(
			LOG_COMPONENT_SPS
			, LOG_ID_SPS_RFID
			, LOG_STATUS_SPS_INIT
			, LOG_ACTION_SPS_NONE
			, LOG_INPUT_SPS);
    sbc_util.utilSysLog("RFID Socket Now Open...\n");
    while(run_flag)
    {
		listen(sockfd,5);
		clilen = sizeof(cli_addr);
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
		if(strstr(buffer, RQ_SBC_HCHK) ){
			n = write(newsockfd, hcheck_ack.c_str(),strlen(hcheck_ack.c_str()));
			str_s_log.str(string());
			str_s_log << "\nFMgr: Network Check Acknowledged" << endl;
			sbc_util.utilSysLog(str_s_log.str());

		}
		else{
			tag_id = buffer;

			sprintf(buffer, "Tag ID: %s", tag_id.c_str());
			n = write(newsockfd,buffer,strlen(buffer));

			tag_id.pop_back();
			tag_id.pop_back();

			if(find(active_tid_list.begin(), active_tid_list.end(), tag_id ) != active_tid_list.end() ){
				/*Tag Already in List*/
				new_tag_detected = false;
			}
			else{
				str_s_log.str(string());
				str_s_log << "\nServer--> TagID Received:" << tag_id << endl;
				sbc_util.utilSysLog(str_s_log.str());

				str_s_log.str(string());
				str_s_log << "TID Group:" << tag_id <<endl;
				sbc_util.utilSysLog(str_s_log.str());

				while(is_list_locked);
				is_list_locked = true;
				//sbc_util.utilSysLog("New Tag ID\n");
				active_tid_list.push_back(tag_id);
				new_tag_detected = true;
				is_list_locked = false;
			}
		}
		if (n < 0){
			str_log = "ERROR writing to socket\n";
			sbc_util.utilSysLog(str_log);
			throw runtime_error(str_log);
		}
		close(newsockfd);
    }

    sbc_util.utilSysLog("Socket Closing\n");
    this->closeNetworkPort();

}

/***********************************************************************
* Function Name: rfidRead
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 18, 2017
*
* Description: copy the newly detected tag id to the argument
*
* Arguments:
	string &new_tag_id: container of the new tag id
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
* 07/18/2017  dslasdoce    creation
***********************************************************************/
bool RFID::rfidRead(void)
{
	bool new_update = false;
	GroundWorker gworker;
	while(is_list_locked);
	is_list_locked = true;
	active_tid_list.clear();
	is_list_locked = false;
	sleep(1);

	while(is_list_locked);
	is_list_locked = true;

	new_update = ResetInOutStatus();
	for(uint8_t i = 0; i < active_tid_list.size(); i++)
	{

		if (CheckTagStatus(active_tid_list[i]) == TAG_UPDATE_USER_INOUT){
			gworker.gw_tag_id = active_tid_list[i];
			UpdateInOutStatus(gworker, true);
			new_update = true;
		}
	}

	is_list_locked = false;
	return new_update;
}

void RFID::closeNetworkPort()
{
	close(newsockfd);
	close(sockfd);
}

