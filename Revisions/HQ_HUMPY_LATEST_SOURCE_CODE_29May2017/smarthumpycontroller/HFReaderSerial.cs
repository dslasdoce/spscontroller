using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WindowsInput;

namespace SmartHumpyController
{
    public enum ANDScaleParameter
    { 
        PC,//Count
        WT,//Weight
        UW,//Unit Weight in use
        TR,//Tare Weight in user
        TL,//Total unit
        AN,//Accumulation numbers
        CD,//IN number in use
        CP,//Comparator Result
        CM,//Comma
        SP,//Space
        CR,//Carriage return
        LF//Line feed
    }

  public class HFReaderSerial
  {
    [DllImport("user32.dll")]
    static extern short VkKeyScan(char ch);

    private SerialPort mSp;

    private string mOutput ="";
    public bool m_PortOpen = false;

    private bool mCarrageReturnFlag = false;
    //private string mSerialPortName;
   // WindowsInput.InputSimulator mInputSimulator;

    public delegate void DelegateMessageRecieved(string Message);
    public event DelegateMessageRecieved OnMessageReceived;
    private LogString m_Logger = LogString.GetLogString("HFReaderSerial");

    public HFReaderSerial(string APortName, int ABaudRate, bool CarriageReturnFlag)
    {
        mCarrageReturnFlag = CarriageReturnFlag;
      //mSerialPortName = APortName;
        InitialSerialPort(APortName, ABaudRate);
    }

    private bool InitialSerialPort(string APortName,int ABaudRate)
    {
        bool zRet = false;
        try
        {
            mSp = new SerialPort(APortName);
            mSp.BaudRate = ABaudRate;
            mSp.PortName = APortName;
            mSp.DataBits = 8;
            mSp.StopBits = StopBits.One;
            mSp.Parity = Parity.None;
            mSp.ReadBufferSize = 1024;
            mSp.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(mSp_DataReceived);
            mSp.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(mSp_ErrorReceived);
            zRet = true;
        }
        catch (Exception ex)
        {
            ex.ToString();
            m_Logger.AddErrors("Initial", ex.ToString());
            zRet = false;
        }
        return zRet;
    }

    public void OpenPort()
    {
      try
      {
        mSp.Open();
        m_PortOpen = true;
      }
      catch (Exception ex)
      {
          m_PortOpen = false;
        throw ex;
      }
    }
    public void ClosePort()
    {
      try
      {
          m_PortOpen = false;
         mSp.Close();
      }
      catch (Exception ex)
      {
          m_PortOpen = false;
          throw ex;
      }
    }

    private string BCDSFormateEditor(int AMode, string AInput)
    {
      string zOutput = string.Empty;
      char[] zSeparator = new char[] { '*' };

      if (AMode == 1) //FDX-B ISO Compliant transponde
      {
        string[] zTempArray = AInput.Replace(" ","").Split(zSeparator);
        foreach (string zSt in zTempArray)
        {
          if (zSt.Length > 5)
          {
            if (zSt.Substring(0, 4) == "FDXB")
            {
              zOutput = zSt.Substring(4, zSt.Length-4);
            }
            else if (zSt.Substring(0, 5) == "UFDXB")
            {
              zOutput = zSt.Substring(5, zSt.Length - 5);
            }
          }
        }
      }
      else if (AMode == 2)
      { 
      
      }

      return zOutput;
    }


    /// <summary>
    /// IN JL 12-DEC-2013 
    /// </summary>
    /// <param name="hexString"></param>
    /// <returns></returns>
    private string HexString2Ascii(string hexString)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i <= hexString.Length - 2; i += 2)
        {
            sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.HexNumber))));
        }
        return sb.ToString();
    }


    private void mSp_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      string zInput = mSp.ReadExisting().Replace("\n", "").Replace(" ","");

      #region Data Handeling
      {
          mOutput = mOutput + zInput;

          //string[] zStrArr = mOutput.Split('\n');
          //if(zStrArr.Length>0)

          string zOutputStr = "";
          int zCRIndex = 0;

          zCRIndex = mOutput.IndexOf('\r');
          //zCRIndex = mOutput.IndexOf("kg");
          while (zCRIndex != -1)
          {
              zOutputStr = mOutput.Substring(0, zCRIndex + 1);
              //zOutputStr = zOutputStr.Replace("kg", "").Replace("ST,","").Replace("US,","").Replace("N","").Replace("\r","");
              zOutputStr = zOutputStr.Replace("\r", "");

              try
              {
                  //double weight = double.Parse(zOutputStr);

                  if (mCarrageReturnFlag)
                  {
                      OnMessageReceived(zOutputStr+Environment.NewLine);
                  }
                  else
                      OnMessageReceived(zOutputStr);
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
      #endregion
    }
    private void mSp_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
    {

    }
  }
}
