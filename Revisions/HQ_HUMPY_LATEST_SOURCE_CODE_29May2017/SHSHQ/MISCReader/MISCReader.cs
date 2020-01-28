using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

using SHSHQ.Tools;

namespace MISCReaderTCPIP
{

  public class MISCReader
  {
    public CMDClient mClient;
    public string mReaderIP;

    private string mOutput = "";
    
    private System.Threading.Timer mReconnectTimer;
    private bool mFirstTimeDetectd = true;

    private string mCompleteData = "";

    //public delegate void DelegateTagDetectedd(RFIDTag Tag);
    public delegate void DelegateTagDetectedd(MISCTag Tag);
    public event DelegateTagDetectedd OnTagDetected;

    public delegate void DelegateConnected(bool IsConnected);
    public event DelegateConnected OnReaderConnected;

    public MISCReader(string AReaderIP, int AReaderPort)
    {
      mReaderIP = AReaderIP;

      //Connect and assign the event handler.
      this.mClient = new CMDClient(IPAddress.Parse(mReaderIP), AReaderPort, "None");
      this.mClient.CommandReceived += new CommandReceivedEventHandler(CommandReceived);
      this.mClient.ServerDisconnected += new ServerDisconnectedEventHandler(ServerDisconnected);
      this.mClient.DisconnectedFromServer += new DisconnectedEventHandler(mClient_DisconnectedFromServer);
      this.mClient.ConnectingSuccessed += new ConnectingSuccessedEventHandler(mClient_ConnectingSuccessed);
      //try
      //{
      //  this.mClient.ConnectToServer();
      //}
      //catch (Exception ex)
      //{
      //  ex.ToString();
      //}

      System.Threading.Thread.Sleep(1000);
      mReconnectTimer = new System.Threading.Timer(new System.Threading.TimerCallback(OnReconnectTimer), null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
    }

    void mClient_ConnectingSuccessed(object sender, EventArgs e)
    {
        try
        {
            OnReaderConnected(true);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    ~MISCReader()
    { }


     private void CommandReceived(object sender, CommandEventArgs e)
     {

       switch (e.Command.CommandType)
       {
         case (CommandType.Message):
           if (e.Command.MetaData != null)
           {
             if (e.Command.MetaData.ToString() != "" && mFirstTimeDetectd == true)
             {
                 //string cmdMetaData =e.Command.MetaData.ToString();
                 string zInput= mCompleteData = e.Command.MetaData.ToString();
                 //string zInput = mCompleteData.Substring(0, mCompleteData.LastIndexOf('\r'));

                  mOutput = mOutput + zInput;

                  //string[] zStrArr = mOutput.Split('\n');
                  //if(zStrArr.Length>0)

                  string zOutputStr = "";
                  int zCRIndex = 0;
                  zCRIndex = mOutput.IndexOf('\r');
                  while (zCRIndex != -1)
                  {
                      zOutputStr = mOutput.Substring(0, zCRIndex + 1);

                      MISCTag zTag = new MISCTag();
                      zTag.AntPort = "0";
                      zTag.TagNumber = zOutputStr;
                      zTag.DateTimeStamp = DateTime.Now;

                      try
                      {
                          OnTagDetected(zTag);
                      }
                      catch (Exception ex)
                      {
                          ex.ToString();
                      }

                      zOutputStr = "";

                      if (mOutput.Length > zCRIndex)
                          mOutput = mOutput.Substring(zCRIndex + 1, mOutput.Length - zCRIndex - 1);
                      else
                          mOutput = "";

                      zCRIndex = mOutput.IndexOf('\r');
                  }
             }
             else if (e.Command.MetaData.ToString() == "")
             {
               mFirstTimeDetectd = true;
             }
           }
           break;
       }
     }


     void mClient_DisconnectedFromServer(object sender, EventArgs e)
     {
         try
         {
             OnReaderConnected(false);
         }
         catch (Exception ex)
         {
             ex.ToString();
         }

         if (e.ToString() == "")
         {
         }
     }


     private void ServerDisconnected(object sender, ServerEventArgs e)
     {
       if (e.ToString() == "")
       {
       }
     }


     private void OnReconnectTimer(object AObject)
     {
       try
       {
         if (mClient.Connected == true)
         {
           //this.mClient.SendCommand(cmdSingleInventory);
         }
         else
         {
           if (this.mClient.Connected == false)
           {
             System.Net.NetworkInformation.Ping zPing = new Ping();
             PingReply zpr = zPing.Send(mClient.serverEP.Address);
             if (zpr.Status == IPStatus.Success)
             {
               this.mClient.ConnectToServer();
             }
           }
         }
       }
       catch (Exception ex)
       {
         ex.ToString();
         throw ex;
       }
       finally
       {
           if (mReconnectTimer != null)
               mReconnectTimer.Change(3000, System.Threading.Timeout.Infinite);
       }
     }

     public void Start(object state)
     {
         try
         {
             this.mClient.ConnectToServer();
         }
         catch (Exception ex)
         {
             ex.ToString();
         }

         System.Threading.Thread.Sleep(1000);

         if (mReconnectTimer != null)
           mReconnectTimer.Change(3000, System.Threading.Timeout.Infinite);
     }

     public void Dispose(object state)
     {
       if (mClient.Connected)
         mClient.Disconnect();
     }

     public void Stop(object state)
     {
         if (mReconnectTimer != null)
         {
             mReconnectTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
             mReconnectTimer = null;
         }

         if (mClient.Connected)
             mClient.Disconnect();
     }
  }
}
