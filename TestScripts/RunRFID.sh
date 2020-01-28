
#!/bin/sh

sshpass -p "impinj" ssh -o StrictHostKeyChecking=no root@10.0.10.5 <<HERE
osshell developer
cd /cust
./speedwayr_arm
HERE

