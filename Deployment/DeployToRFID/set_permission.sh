#!/bin/sh

sshpass -p "impinj" ssh -o StrictHostKeyChecking=no root@10.0.10.5 << HERE
osshell developer
chmod a+x /cust/speedwayr_arm
cd /cust
rm -rf start
rm -rf rfidlogs
cp speedwayr_arm ./start
HERE
exit 0
