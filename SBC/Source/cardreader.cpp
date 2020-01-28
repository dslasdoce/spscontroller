/*
 * cardreader.cpp
 *
 *  Created on: 28 Jul 2017
 *      Author: nic
 */

#include <iostream>
#include <sstream>
#include <algorithm>
#include <stdlib.h>
#include <stdio.h> // standard input / output functions
#include <string.h> // string function definitions
#include <unistd.h> // UNIX standard function definitions
#include <fcntl.h> // File control definitions
#include <errno.h> // Error number definitions
#include <termios.h> // POSIX terminal control definitionss
#include <time.h>   // time calls
#include <thread>

#include <cardreader.h>
#include <sbc.h>
#include <sps.h>
#include <utility.h>

using namespace std;

/***********************************************************************
* Function Name: initialize
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 28, 2017
*
* Description: prepare serial port to accept card reader details
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
* 			CReaderStatusEnum stat: enumeration of card reader status
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
* 07/28/2017  dslasdoce    creation
***********************************************************************/
CReaderStatusEnum CardReader::initialize(){
	CReaderStatusEnum stat = CREADER_ERROR;

	sbc_util.utilSysLog("\n\n*******Card Reader Initialization******\n");
	serial_fd = this->openPort(CREADER_PORT);

	if(serial_fd != -1){
		this->configurePort();
		stat = CREADER_OK;
		sbc_util.utilAppLog(
				LOG_COMPONENT_SPS
				, LOG_ID_SPS_CREADER
				, LOG_STATUS_SPS_INIT
				, LOG_ACTION_SPS_NONE
				, LOG_INPUT_SPS);
	}
	else{
		sbc_util.utilAppLog(
				LOG_COMPONENT_SPS
				, LOG_ID_SPS_CREADER
				, LOG_STATUS_SPS_ER
				, LOG_ACTION_SPS_NONE
				, LOG_INPUT_SPS);
	}
	return stat;
}

/***********************************************************************
* Function Name: cardReaderStart
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 28, 2017
*
* Description: start receiving of data from serial port on a different thread
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
* 07/28/2017  dslasdoce    creation
***********************************************************************/
void CardReader::cardReaderStart()
{
	run_flag = true;
	new_card_detected = false;
    thread t1(&CardReader::cardDetect, this);
    cardreader_thread = &t1;
    cardreader_thread->detach();
}

/***********************************************************************
* Function Name: cardReaderStop
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 28, 2017
*
* Description: stop receiving of data from serial port on a different thread
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
* 07/28/2017  dslasdoce    creation
***********************************************************************/
void CardReader::cardReaderStop()
{
	run_flag = false;
	sbc_util.utilSysLog("ending Card Reader\n");
	close(serial_fd);
}

/***********************************************************************
* Function Name: openPort
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 28, 2017
*
* Description: open a serial port for card reader
*
* Arguments:
* 			const char *portname: pointer to name of the serial port to be opened
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
* 07/28/2017  dslasdoce    creation
***********************************************************************/
int CardReader::openPort(const char *portname)
{
	int fd; // file description for the serial port
	stringstream str_s_log;
	str_s_log.str(string());

	fd = open(portname, O_RDWR | O_NOCTTY | O_NDELAY);

	if(fd == -1) // if open is unsucessful
	{

		str_s_log << "Unable to open Serial Port: " << CREADER_PORT << endl;
		sbc_util.utilSysLog(str_s_log.str());
	}
	else
	{
		fcntl(serial_fd, F_SETFL, 0);
		str_s_log << "Successfully Opened Serial Port: " << CREADER_PORT << endl;
		sbc_util.utilSysLog(str_s_log.str());
	}

	return(fd);
}

/***********************************************************************
* Function Name: configurePort
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 28, 2017
*
* Description: configure tx/rx parameters and settings of the serial port
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
* 07/28/2017  dslasdoce    creation
***********************************************************************/
int CardReader::configurePort()      // configure the port
{
	struct termios port_settings;      // structure to store the port settings in

	cfsetispeed(&port_settings, CREADER_BAUD);    // set baud rates
	cfsetospeed(&port_settings, CREADER_BAUD);

	port_settings.c_cflag &= ~PARENB;    // set no parity, stop bits, data bits
	port_settings.c_cflag &= ~CSTOPB;
	port_settings.c_cflag &= ~CSIZE;
	port_settings.c_cflag |= CS8;

	tcsetattr(serial_fd, TCSANOW, &port_settings);    // apply the settings to the port

	return(serial_fd);
}

/***********************************************************************
* Function Name: readCard
*
* Original Author: Dominique Lasdoce
*
* Function Creation Date: July 28, 2017
*
* Description: continuously check for incoming packets from card reader
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
* 07/28/2017  dslasdoce    creation
***********************************************************************/
void CardReader::cardDetect(){
	char buff [100];
	string card_id;
	stringstream str_s_log;
	fd_set read_fds, write_fds, except_fds;
	struct timeval timeout;


    while(run_flag)
    {
    	/*reset file descriptor sets*/
    	FD_ZERO(&read_fds);
    	FD_ZERO(&write_fds);
    	FD_ZERO(&except_fds);
    	FD_SET(serial_fd, &read_fds);

    	// Set timeout to 5 seconds
    	timeout.tv_sec = 60;
    	timeout.tv_usec = 0;

    	/*clear local buffer and serial buffer*/
		bzero(buff,sizeof buff);
		tcflush(serial_fd, TCIOFLUSH);

		/*read data from serial port*/
		if (select(serial_fd + 1, &read_fds, &write_fds, &except_fds, &timeout) == 1){

			read (serial_fd, buff, sizeof buff);

			card_id = buff;
			while(is_card_list_locked);
			is_card_list_locked = true;
			if(find(active_card_list.begin(), active_card_list.end(), card_id ) != active_card_list.end() ){
				/*Card ID Already in List*/
			}
			else{
				new_card_detected = true;
				active_card_list.push_back(card_id);
				str_s_log.str(string());
				str_s_log << "\n\n+++++Access Card:  " << buff <<endl;
				sbc_util.utilSysLog(str_s_log.str());

			}
			is_card_list_locked = false;

		}

    }
}

bool CardReader:: cardRead()
{
	GroundWorker gworker;
	bool new_card_update = false;
	if(new_card_detected){
		while(is_card_list_locked);
		is_card_list_locked = true;
		for(uint8_t i = 0; i < active_card_list.size(); i++){
			gworker.gw_access_card = active_card_list[i];
			if (VerifyCardStatus(gworker.gw_access_card) == CARD_OK){
				new_card_update = true;
				UpdateCheckingStatus(gworker);
			}
		}

		active_card_list.clear();
		is_card_list_locked = false;
		new_card_detected = false;
	}
	return new_card_update;

}

