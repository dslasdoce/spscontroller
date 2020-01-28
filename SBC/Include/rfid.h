/*
 * rfid.h
 *
 *  Created on: 17 Jul 2017
 *      Author: nic
 */

#ifndef SOURCE_SPS_CONTROLLER_SBC_INCLUDE_RFID_H_
#define SOURCE_SPS_CONTROLLER_SBC_INCLUDE_RFID_H_
#include <iostream>
#include <thread>
#include<vector>

using namespace std;

class RFID{
private:
	int sockfd, newsockfd;
	pthread_attr_t attr;
	thread *rfid_thread;
	string tag_id;
	bool new_tag_detected;
	bool is_list_locked = false;
public:
	bool run_flag = true;
	void rfidConfigureNetworkPort();
	void closeNetworkPort();
	void rfidStart();
	void rfidStop();
	bool rfidRead(void);
};

#endif /* SOURCE_SPS_CONTROLLER_SBC_INCLUDE_RFID_H_ */
