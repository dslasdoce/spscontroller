using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;

namespace MISCReaderTCPIP
{
    /// <summary>
    /// The command client command class.
    /// </summary>
    public class CMDClient
    {
        public Socket clientSocket;
        public NetworkStream networkStream;
        public StreamWriter SWstream;
        public BackgroundWorker bwReceiver;
        public IPEndPoint serverEP;
        public string networkName;

        public string rec_data;


        public string Rdata
        {
            get { return rec_data; }
        
        }
        /// <summary>
        /// [Gets] The value that specifies the current client is connected or not.
        /// </summary>
        public bool Connected
        {
            get
            {
                if ( this.clientSocket != null )
                    return this.clientSocket.Connected;
                else
                    return false;
            }
        }
        /// <summary>
        /// [Gets] The IP address of the remote server.If this client is disconnected,this property returns IPAddress.None.
        /// </summary>
        public IPAddress ServerIP
        {
            get
            {
                if ( this.Connected )
                    return this.serverEP.Address;
                else
                    return IPAddress.None;
            }
        }

        /// <summary>
        /// [Gets] The comunication port of the remote server.If this client is disconnected,this property returns -1.
        /// </summary>
        public int ServerPort
        {
            get
            {
                if ( this.Connected )
                    return this.serverEP.Port;
                else
                    return -1;
            }
        }
        /// <summary>
        /// [Gets] The IP address of this client.If this client is disconnected,this property returns IPAddress.None.
        /// </summary>
        public IPAddress IP
        {
            get
            {
                if ( this.Connected )
                    return ( (IPEndPoint)this.clientSocket.LocalEndPoint ).Address;
                else
                    return IPAddress.None;
            }
        }
        /// <summary>
        /// [Gets] The comunication port of this client.If this client is disconnected,this property returns -1.
        /// </summary>
        public int Port
        {
            get
            {
                if ( this.Connected )
                    return ( (IPEndPoint)this.clientSocket.LocalEndPoint ).Port;
                else
                    return -1;
            }
        }
        /// <summary>
        /// [Gets/Sets] The string that will sent to the server and then to other clients, to identify this client to them.
        /// </summary>
        public string NetworkName
        {
            get { return networkName; }
            set { networkName = value; }
        }

        #region Contsructors
        /// <summary>
        /// Cretaes a command client instance.
        /// </summary>
        /// <param name="server">The remote server to connect.</param>
        /// <param name="netName">The string that will send to the server and then to other clients, to identify this client to all clients.</param>
        public CMDClient(IPEndPoint server,string netName)
        {
            this.serverEP = server;
            this.networkName = netName;
            System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += new System.Net.NetworkInformation.NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);
        }

        /// <summary>
        /// Cretaes a command client instance.
        /// </summary>
        ///<param name="serverIP">The IP of remote server.</param>
        ///<param name="port">The port of remote server.</param>
        /// <param name="netName">The string that will send to the server and then to other clients, to identify this client to all clients.</param>
        public CMDClient(IPAddress serverIP , int port,string netName)
        {
            this.serverEP = new IPEndPoint(serverIP , port);
            this.networkName = netName;
            System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += new System.Net.NetworkInformation.NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);
        }

        #endregion

        #region public Methods

        public void NetworkChange_NetworkAvailabilityChanged(object sender , System.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        {
            if ( !e.IsAvailable )
            {
                this.OnNetworkDead(new EventArgs());
                this.OnDisconnectedFromServer(new EventArgs());
            }
            else
                this.OnNetworkAlived(new EventArgs());
        }


        public void StartReceive(object sender, DoWorkEventArgs e)
        {
          try
          {
              //while (this.clientSocket.Connected)

              while(SocketExtensions.IsConnected(this.clientSocket))
              {
                  CommandType cmdType = CommandType.Message;
                  //IPAddress senderIP = IPAddress.Parse("172.16.0.1");
                  //IPAddress senderIP = IPAddress.Parse("10.248.252.79");
                  //IPAddress senderIP = IPAddress.Parse("169.254.110.79");
                  //IPAddress senderIP = IPAddress.Parse("10.248.252.240");
                  IPAddress senderIP = IPAddress.Parse(this.ServerIP.ToString());
                  string senderName = "BDSReader";
                  byte[] buffer = new byte[512];
                  string cmdMetaData;
                  //Read the command's Meta data.
                  buffer = new byte[512];
                  // Received data string.
                  StringBuilder sb = new StringBuilder();

                  bool zCanReadFlag;
                  bool zGoodRead = false;
                  zCanReadFlag = this.networkStream.CanRead;

                  if (this.networkStream.DataAvailable == true)
                  {
                      int readBytes = this.networkStream.Read(buffer, 0, 512);
                      //Command Execution Result Status
                      //cmdMetaData = ByteArrayToString(buffer).Substring(0, 27);
                      //cmdMetaData = System.Text.Encoding.Unicode.GetString(buffer);
                      cmdMetaData = Encoding.ASCII.GetString(buffer, 0, readBytes).ToString();
                      //cmdMetaData = sb.Append(Encoding.ASCII.GetString(buffer, 0, readBytes)).ToString();
                      zGoodRead = true;
                      if (zGoodRead == true)
                      {
                          cmdMetaData = Encoding.ASCII.GetString(buffer, 0, readBytes);
                          Command cmd = new Command(cmdType, senderIP, cmdMetaData);
                          cmd.SenderIP = senderIP;
                          cmd.SenderName = senderName;
                          this.OnCommandReceived(new CommandEventArgs(cmd));
                          cmdMetaData = "";
                      }
                      else
                      {
                          cmdMetaData = "";
                          Command cmd = new Command(cmdType, senderIP, cmdMetaData);
                          cmd.SenderIP = senderIP;
                          cmd.SenderName = senderName;
                          this.OnCommandReceived(new CommandEventArgs(cmd));
                      }
                  }
              }
            //this.OnServerDisconnected(new ServerEventArgs(this.clientSocket));
            //this.Disconnect();
          }
          catch (Exception ex)
          {
            ex.ToString();//IN JL 14-SEP-11 
          }
        }

        public void bwSender_RunWorkerCompleted(object sender , RunWorkerCompletedEventArgs e)
        {
           
        }

        public static string ByteArrayToString(byte[] ba)
        {
          StringBuilder hex = new StringBuilder(ba.Length * 2);

          for (int i = 0; i < ba.Length; i++)       // <-- use for loop is faster than foreach   
            hex.Append(ba[i].ToString("X2"));   // <-- ToString is faster than AppendFormat   

          return hex.ToString();
        } 


        public void bwSender_DoWork(object sender , DoWorkEventArgs e)
        {
            Command cmd = (Command)e.Argument;
            e.Result = this.SendCommandToServer(cmd);
        }

        //This Semaphor is to protect the critical section from concurrent access of sender threads.
        System.Threading.Semaphore semaphor = new System.Threading.Semaphore(1 , 2);
        public bool SendCommandToServer(Command cmd)
        {
            try
            {
              
                semaphor.WaitOne();
                if ( cmd.MetaData == null || cmd.MetaData == "" )
                    this.SetMetaDataIfIsNull(cmd);

                //Command MetaData
                byte [] metaBuffer = Encoding.Unicode.GetBytes(cmd.MetaData);
                //CommandType

                if (cmd.CommandType == CommandType.InventoryWithoutAFI)
                {
                  metaBuffer.Initialize();
                  metaBuffer = new byte[4];
                  metaBuffer[0] = 0x05;//Len
                  metaBuffer[1] = 0x00;//default  Com_adr
                  metaBuffer[2] = 0x01;//Cmd
                  metaBuffer[3] = 0x06;//State

                  metaBuffer = CRCCalc(metaBuffer);

                  //metaBuffer[4] = 0x5D;//LSB-CRC
                  //metaBuffer[5] = 0xB2;//MSB-CRC
                }
                else if (cmd.CommandType == CommandType.GetReaderInfor)
                {
                  metaBuffer.Initialize();
                  metaBuffer = new byte[4];
                  metaBuffer[0] = 0x05;//Len
                  metaBuffer[1] = 0x00;//default  Com_adr
                  metaBuffer[2] = 0x00;//Cmd
                  metaBuffer[3] = 0x00;//State

                  metaBuffer = CRCCalc(metaBuffer);
                }
                else if (cmd.CommandType == CommandType.ChangeToISO15693)
                {
                  metaBuffer.Initialize();
                  metaBuffer = new byte[4];
                  metaBuffer[0] = 0x05;//Len
                  metaBuffer[1] = 0x00;//default  Com_adr
                  metaBuffer[2] = 0x00;//Cmd
                  metaBuffer[3] = 0x06;//State

                  metaBuffer = CRCCalc(metaBuffer);
                }
                else if (cmd.CommandType == CommandType.SetBeep)
                {
                  metaBuffer.Initialize();
                  metaBuffer = new byte[7];
                  metaBuffer[0] = 0x08;//Len
                  metaBuffer[1] = 0x00;//default  Com_adr
                  metaBuffer[2] = 0x00;//Cmd
                  metaBuffer[3] = 0x08;//State
                  metaBuffer[4] = 0x03;//On_Duration
                  metaBuffer[5] = 0x02;//Off_Duration
                  metaBuffer[6] = 0x01;//Beeping_times

                  metaBuffer = CRCCalc(metaBuffer);
                }

                else if (cmd.CommandType == CommandType.SetLED)
                {
                  metaBuffer.Initialize();
                  metaBuffer = new byte[7];
                  metaBuffer[0] = 0x08;//Len
                  metaBuffer[1] = 0x00;//default  Com_adr
                  metaBuffer[2] = 0x00;//Cmd
                  metaBuffer[3] = 0x07;//State
                  metaBuffer[4] = 0x03;//On_Duration
                  metaBuffer[5] = 0x02;//Off_Duration
                  metaBuffer[6] = 0x01;//Beeping_times

                  metaBuffer = CRCCalc(metaBuffer);
                }



                //this.SWstream.Write(cmd.MetaData);
                this.networkStream.Write(metaBuffer , 0 , metaBuffer.Length);
                this.networkStream.Flush();

                

                //this.SWstream.Flush();

                semaphor.Release();
                return true;
            }
            catch
            {
                semaphor.Release();
                return false;
            }

        }

        public byte[] CRCCalc(byte[] AInput)
        {
          int zByteLength = AInput.Length+2;
          byte[] zRet = new byte[zByteLength];
          //byte[] metaBuffer = new byte[4];
          //metaBuffer[0] = 0x05;//Len
          //metaBuffer[1] = 0xFF;//default  Com_adr
          //metaBuffer[2] = 0x01;//Cmd
          //metaBuffer[3] = 0x00;//State

         const ushort POLYNOMIAL16 = 0x8408;

         int len = AInput.Length;
         const int PRESET_VALUE = 0xffff ;
         int i,j;
         uint current_crc_value = PRESET_VALUE;

          try
          {
            for (i = 0; i < len; i++) /*len=number of protocol bytes without CRC*/
            {
              zRet[i] = AInput[i];
              //current_crc_value = current_crc_value ^ ((uint)metaBuffer[i]);
              current_crc_value = current_crc_value ^ ((uint)AInput[i]);
              for (j = 0; j < 8; j++)
              {
                if ((current_crc_value & 0x0001) == 1)
                {
                  current_crc_value = (current_crc_value >> 1) ^ POLYNOMIAL16;
                }
                else
                {
                  current_crc_value = (current_crc_value >> 1);
                }
              }
            }
            //// LSB-CRC16          
            //metaBuffer[i++] = Convert.ToByte(current_crc_value & 0x00ff);
            //// MSB-CRC16
            //metaBuffer[i] = Convert.ToByte((current_crc_value >> 8) & 0x00ff);


            // MSB-CRC16
            zRet[zRet.Length-1] = Convert.ToByte((current_crc_value >> 8) & 0x00ff);
            // LSB-CRC16          
            zRet[zRet.Length-2] = Convert.ToByte(current_crc_value & 0x00ff);


          }
          catch (Exception ex)
          {
            ex.ToString();
            //throw ex;
          }

          return zRet;
        }

        public void SetMetaDataIfIsNull(Command cmd)
        {
            switch ( cmd.CommandType )
            {
                case ( CommandType.ClientLoginInform ):
                    cmd.MetaData = this.IP.ToString() + ":" + this.networkName;
                    break;
                case ( CommandType.ChangeToISO14443A ):
                case ( CommandType.ChangeToISO14443B ):
                case ( CommandType.ChangeToISO15693 ):
                case ( CommandType.CloseRF ):
                case ( CommandType.OpenRF ):
                    cmd.MetaData = "60000";
                    break;
                default:
                    cmd.MetaData = "\n";
                    break;
            }
        }
 
        #endregion

        #region Public Methods
        /// <summary>
        /// Connect the current instance of command client to the server.This method throws ServerNotFoundException on failur.Run this method and handle the 'ConnectingSuccessed' and 'ConnectingFailed' to get the connection state.
        /// </summary>
        public void ConnectToServer()
        {
            BackgroundWorker bwConnector = new BackgroundWorker();
            bwConnector.DoWork += new DoWorkEventHandler(bwConnector_DoWork);
            bwConnector.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwConnector_RunWorkerCompleted);
            bwConnector.RunWorkerAsync();
        }

        public void bwConnector_RunWorkerCompleted(object sender , RunWorkerCompletedEventArgs e)
        {
            if(!((bool)e.Result))
                this.OnConnectingFailed(new EventArgs());
            else
                this.OnConnectingSuccessed(new EventArgs());

            ( (BackgroundWorker)sender ).Dispose();
        }

        public void bwConnector_DoWork(object sender , DoWorkEventArgs e)
        {
            try
            {
                this.clientSocket = new Socket(AddressFamily.InterNetwork , SocketType.Stream , ProtocolType.Tcp);
                this.clientSocket.Connect(this.serverEP);

                //Command cmd = new Command(CommandType.Message, IPAddress.Parse("172.16.0.1"), "Actions(Command(readAddress=20,readLength=1))\n\r");
                //this.SendCommand(cmd);

                e.Result = true;
                this.networkStream = new NetworkStream(this.clientSocket);
                this.bwReceiver = new BackgroundWorker();
                this.bwReceiver.WorkerSupportsCancellation = true;
                this.bwReceiver.DoWork += new DoWorkEventHandler(StartReceive);
                this.bwReceiver.RunWorkerAsync();
                
            
            }
            catch
            {
                e.Result = false;
            }
        }
        /// <summary>
        /// Sends a command to the server if the connection is alive.
        /// </summary>
        /// <param name="cmd">The command to send.</param>
        public void SendCommand(Command cmd)
        {
            if ( this.clientSocket != null && this.clientSocket.Connected )
            {
                BackgroundWorker bwSender = new BackgroundWorker();
                bwSender.DoWork += new DoWorkEventHandler(bwSender_DoWork);
                bwSender.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwSender_RunWorkerCompleted);
                bwSender.WorkerSupportsCancellation = true;
                bwSender.RunWorkerAsync(cmd);
            }
            else
                this.OnCommandFailed(new EventArgs());
        }
        
        /// <summary>
        /// Disconnect the client from the server and returns true if the client had been disconnected from the server.
        /// </summary>
        /// <returns>True if the client had been disconnected from the server,otherwise false.</returns>
        public bool Disconnect()
        {
            if (this.clientSocket != null && this.clientSocket.Connected )
            {
                try
                {
                    this.clientSocket.Shutdown(SocketShutdown.Both);
                    this.clientSocket.Close();
                    this.bwReceiver.CancelAsync();
                    this.OnDisconnectedFromServer(new EventArgs());
                    return true;
                }
                catch
                {
                    return false;
                }

            }
            else
                return true;
        } 
        #endregion

        #region Events
        /// <summary>
        /// Occurs when a command received from a remote client.
        /// </summary>
        public event CommandReceivedEventHandler CommandReceived;
        /// <summary>
        /// Occurs when a command received from a remote client.
        /// </summary>
        /// <param name="e">The received command.</param>
        protected virtual void OnCommandReceived(CommandEventArgs e)
        {
            try
            {
                if (CommandReceived != null)
                {
                    Control target = CommandReceived.Target as Control;
                    if (target != null && target.InvokeRequired)
                        target.Invoke(CommandReceived, new object[] { this, e });
                    else
                        CommandReceived(this, e);
                }
            }
            catch (Exception exc1)
            {
                exc1.ToString();
            }


        }

        public void receivedMessage(string data)
        {
            rec_data = data;
            
        
        }

        /// <summary>
        /// Occurs when a command had been sent to the the remote server Successfully.
        /// </summary>
        public event CommandSentEventHandler CommandSent;
        /// <summary>
        /// Occurs when a command had been sent to the the remote server Successfully.
        /// </summary>
        /// <param name="e">The sent command.</param>
        protected virtual void OnCommandSent(EventArgs e)
        {
            if ( CommandSent != null )
            {
                Control target = CommandSent.Target as Control;
                if ( target != null && target.InvokeRequired )
                    target.Invoke(CommandSent , new object [] { this , e });
                else
                    CommandSent(this , e);
            }
        }

        /// <summary>
        /// Occurs when a command sending action had been failed.This is because disconnection or sending exception.
        /// </summary>
        public event CommandSendingFailedEventHandler CommandFailed;
        /// <summary>
        /// Occurs when a command sending action had been failed.This is because disconnection or sending exception.
        /// </summary>
        /// <param name="e">The sent command.</param>
        protected virtual void OnCommandFailed(EventArgs e)
        {
            if ( CommandFailed != null )
            {
                Control target = CommandFailed.Target as Control;
                if ( target != null && target.InvokeRequired )
                    target.Invoke(CommandFailed , new object [] { this , e });
                else
                    CommandFailed(this , e);
            }
        }

        /// <summary>
        /// Occurs when the client disconnected.
        /// </summary>
        public event ServerDisconnectedEventHandler ServerDisconnected;
        /// <summary>
        /// Occurs when the server disconnected.
        /// </summary>
        /// <param name="e">Server information.</param>
        protected virtual void OnServerDisconnected(ServerEventArgs e)
        {
            if ( ServerDisconnected != null )
            {
                Control target = ServerDisconnected.Target as Control;
                if ( target != null && target.InvokeRequired )
                    target.Invoke(ServerDisconnected , new object [] { this , e });
                else
                    ServerDisconnected(this , e);
            }
        }

        /// <summary>
        /// Occurs when this client disconnected from the remote server.
        /// </summary>
        public event DisconnectedEventHandler DisconnectedFromServer;
        /// <summary>
        /// Occurs when this client disconnected from the remote server.
        /// </summary>
        /// <param name="e">EventArgs.</param>
        protected virtual void OnDisconnectedFromServer(EventArgs e)
        {
            if ( DisconnectedFromServer != null )
            {
                Control target = DisconnectedFromServer.Target as Control;
                if ( target != null && target.InvokeRequired )
                    target.Invoke(DisconnectedFromServer , new object [] { this , e });
                else
                    DisconnectedFromServer(this , e);
            }
        }

        /// <summary>
        /// Occurs when this client connected to the remote server Successfully.
        /// </summary>
        public event ConnectingSuccessedEventHandler ConnectingSuccessed;
        /// <summary>
        /// Occurs when this client connected to the remote server Successfully.
        /// </summary>
        /// <param name="e">EventArgs.</param>
        protected virtual void OnConnectingSuccessed(EventArgs e)
        {
            if ( ConnectingSuccessed != null )
            {
                Control target = ConnectingSuccessed.Target as Control;
                if ( target != null && target.InvokeRequired )
                    target.Invoke(ConnectingSuccessed , new object [] { this , e });
                else
                    ConnectingSuccessed(this , e);
            }
        }

        /// <summary>
        /// Occurs when this client failed on connecting to server.
        /// </summary>
        public event ConnectingFailedEventHandler ConnectingFailed;
        /// <summary>
        /// Occurs when this client failed on connecting to server.
        /// </summary>
        /// <param name="e">EventArgs.</param>
        protected virtual void OnConnectingFailed(EventArgs e)
        {
            if ( ConnectingFailed != null )
            {
                Control target = ConnectingFailed.Target as Control;
                if ( target != null && target.InvokeRequired )
                    target.Invoke(ConnectingFailed , new object [] { this , e });
                else
                    ConnectingFailed(this , e);
            }
        }

        /// <summary>
        /// Occurs when the network had been failed.
        /// </summary>
        public event NetworkDeadEventHandler NetworkDead;
        /// <summary>
        /// Occurs when the network had been failed.
        /// </summary>
        /// <param name="e">EventArgs.</param>
        protected virtual void OnNetworkDead(EventArgs e)
        {
            if ( NetworkDead != null )
            {
                Control target = NetworkDead.Target as Control;
                if ( target != null && target.InvokeRequired )
                    target.Invoke(NetworkDead , new object [] { this , e });
                else
                    NetworkDead(this , e);
            }
        }

        /// <summary>
        /// Occurs when the network had been started to work.
        /// </summary>
        public event NetworkAlivedEventHandler NetworkAlived;
        /// <summary>
        /// Occurs when the network had been started to work.
        /// </summary>
        /// <param name="e">EventArgs.</param>
        protected virtual void OnNetworkAlived(EventArgs e)
        {
            if ( NetworkAlived != null )
            {
                Control target = NetworkAlived.Target as Control;
                if ( target != null && target.InvokeRequired )
                    target.Invoke(NetworkAlived , new object [] { this , e });
                else
                    NetworkAlived(this , e);
            }
        }

        #endregion
    }

    static class SocketExtensions
    {
        public static bool IsConnected(this Socket socket)
        {
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException) { return false; }
        }
    }

}
