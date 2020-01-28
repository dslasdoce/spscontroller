#/bin/bash/ 

cd ~/octane_etk-5.12.0.240 

cd octane_etk-5.12.0.240
PATH=$PATH:$HOME/octane_etk-5.12.0.240:$HOME/octane_etk-5.12.0.240/arm-toolchain/bin:$HOME/octane_etk_sample-5.12.0.240

cd ~/sps-controller/RFID/

make clean
make arm

cp bin/speedwayr_arm cap/
make cap 

cp bin/speedwayr_arm speedwayr_arm
cp cap/sys/reader.conf reader.conf


 ~/sps-controller/Deployment/DeployToRFID/ftp_push.sh
 ~/sps-controller/Deployment/DeployToRFID/set_permission.sh

exit 0
