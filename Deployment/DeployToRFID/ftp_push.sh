#!/bin/sh
HOST='10.0.10.5'
USER='root'
PASSWD='impinj'

cd ~/sps-controller/RFID/

ftp -n $HOST <<END_SCRIPT
quote USER $USER
quote PASS $PASSWD
cd /cust
bin
put speedwayr_arm
put rfidCfg.txt
mkdir sys
cd sys
put reader.conf
END_SCRIPT
exit 0
