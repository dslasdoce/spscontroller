#!/bin/sh

echo "killing all previous apps"
killall SafetyAmmo


echo "Building RFID App...."

cd ~/sps-controller/Deployment/DeployToRFID/
./build_deploy.sh

echo "Rebooting RFID.."

sshpass -p "impinj" ssh -o StrictHostKeyChecking=no root@10.0.10.5 <<HERE
osshell developer
reboot
HERE

sleep 60

echo "Buidling SBC App"

cd ~/sps-controller/Deployment/DeployToSBC/BuildFiles/
./build_deploy.sh


echo "Testing Package Now" 

cd ~/sps-controller/

./Deployment/DeployToSBC/BuildFiles/SafetyAmmo & ./TestScripts/RunRFID.sh & 
sleep 60

killall SafetyAmmo

echo "Done Test, now sending report"

cd
mv sbclogs* ~/sps-controller/TestResults/
rm sbclogs*

rm ~/sps-controller/reports.zip

zip -r ~/sps-controller/reports.zip /home/ghost/sps-controller/TestResults/
mpack -s TestReports ~/sps-controller/reports.zip taras.woronjanski@ammo.co andrew.hayek@ammo.co dominique.lasdoce@ammo.co

echo "All done check email"

exit 0
