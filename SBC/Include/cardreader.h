/*
 * cardreader.h
 *
 *  Created on: 28 Jul 2017
 *      Author: nic
 */

#ifndef SOURCE_SPS_CONTROLLER_SBC_INCLUDE_CARDREADER_H_
#define SOURCE_SPS_CONTROLLER_SBC_INCLUDE_CARDREADER_H_

#include <sbc.h>


class CardReader{
private:
	bool run_flag = true;
	bool reader_state;
	bool new_card_detected;
	bool is_card_list_locked;
	int serial_fd;
	int openPort(const char *portname);
	int configurePort();
	thread *cardreader_thread;
	vector<string> active_card_list;
public:
	CReaderStatusEnum initialize();
	CardStatusEnum cardVerify(string card_id);
	void cardReaderStart();
	void cardReaderStop();
	void cardDetect();
	bool cardRead();
};



#endif /* SOURCE_SPS_CONTROLLER_SBC_INCLUDE_CARDREADER_H_ */
