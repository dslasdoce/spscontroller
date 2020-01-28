/*
 * SPS.h
 *
 *  Created on: 12 Jul 2017
 *      Author: nic
 */

#ifndef SOURCE_SPS_CONTROLLER_SBC_INCLUDE_SPS_H_
#define SOURCE_SPS_CONTROLLER_SBC_INCLUDE_SPS_H_

/*Include Third Party Functions*/
#include<mysql_connection.h>
#include<mysql_driver.h>

/*Include SBC Functions*/
#include <sbc.h>

/*Function Prototypes**********************************************************/
DBStatusEnum dbConnect(DBConnectionParam *db_param, sql::Connection **con);
WorkerSafetyStatusEnum GetCheckedInWorkerStatus(DBConnectionParam db_con_param);
DBStatusEnum UpdateCheckingStatus(GroundWorker gworker);
TagStatusEnum CheckTagStatus(string tag_id);
CardStatusEnum VerifyCardStatus(string card_id);
DBStatusEnum UpdateInOutStatus(GroundWorker gworker, bool is_in);
bool ResetInOutStatus();
void clearWorkersInOutStatus();
void getLocationID(void);
void setSPSDBParameters();


#endif /* SOURCE_SPS_CONTROLLER_SBC_INCLUDE_SPS_H_ */
