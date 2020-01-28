//****************************************************************************//
//       This source code belongs to Barcode Data Systems (BCDS)              //
//    Unit 24, 10 Yalgar Road, Kirrawee, Sydney, NSW Australia 2232           //
//                           www.bcds.com.au                                  //
//                                                                            //
// This file may not be copied in whole or in part without written permission //
//                         from the copyright owner.                          //
//                                                                            //
//                      © 2010, Barcode Data Systems (BCDS)                   //
//****************************************************************************//
//                                                                            //
// Project     : Improved Stock Management (ISM)                              //
//                                                                            //
// Client      : Australian Department of Defense                             //
//                                                                            //
// File        : ISMCommon.cs                                                 //
//                                                                            //
// Description :                                                              //
//                                                                            //
// Initial Author: Team BCDS                                           .      //
// Date Written  : 21-AUG-2010                                                //
// Documentation : BCDS Visual Source Safe.                                   //
//                 ISM\Document\Functional Spec\Functional Spec V1.doc        //
//                                                                            //
//****************************************************************************//
// Modification History:                                                      //
//                                                                            //
// Date..... Who.......... Modification Description.......................... //
// DD-MMM-YY xxxxxxxxxxxxx                                                    //
//                                                                            //
// 04-NOV-11 MR             Added String_To_Bytes, String_To_Bytes method     //
// 06-SEP-10 JL             Changed Validate Location UID range               //
//                          Min = 8000000000000, Max = 8999999999999          //
//                                                                            //
// 31-AUG-10 MR             Added Enum ExceptionType Type                     //
// 27-AUG-10 MR             Added Enum Trace Type                             //
// 24-AUG-10 Damian Murray  Added a Conditional Directive to control whether  //
//                          certain pieces of code should be excluded from the//
//                          PDET environment but included in the Win32 world! //
//                                                                            //
//                          Move the two class ISMPassword and SpecialEntry   //
//                          into this ISMCommon.cs file and then removed them //
//                          from the project ISMComponents althoughter.       //
//                                                                            //
//                                                                            //
// 23-AUG-10 MR              Moved Classes ValidItemNumber, ValidSealNumber   //
//                           ValidLocationNumber from ValidateISLNumbers.cs   //
//                           to here                                          //
//                                                                            //
// 21-AUG-10 MR              ver 1.0                                          //
//                           Initial Release.                                 //
//                                                                            //
//****************************************************************************//

using System;
using System.Security.Cryptography; // DM In 24-AUG-10
using System.Diagnostics;           // DM in 24-AUG-10 

namespace ISM
{
  public enum SealType : int { None = 0, TamperEvident, Electronic }; // 21-AUG-10
  public enum MoveType : int { ItemMove = 0, LocationMove }; // MR in 21-AUG-10
  public enum TraceType : int {ItemTrace =0,LocationTrace }; // MR 27-AUG-10
  public enum ExceptionType : int { Other = 0, StockTake }; // MR 31-AUG-10
  public enum ReportType : int {Location = 0, Item }; // MR IN 11-OCT-11

  //MR 23-AUG-10 Changed Class Name Common to ISMCommon
  public class ISMCommon
  {
    #if !PocketPC  // DM in 24-AUG-10  Only allow this method to be included in the Win32 client
    // [Conditional("PocketPC")]
    public void ExportExcelReport(string AFileName, DevExpress.XtraGrid.GridControl AGridControl)
    {
      DevExpress.XtraGrid.Export.GridViewExportLink zLink;
      DevExpress.XtraExport.ExportXlsProvider zProvider;
      zProvider = new DevExpress.XtraExport.ExportXlsProvider(AFileName);
      zLink = AGridControl.MainView.CreateExportLink(zProvider) as DevExpress.XtraGrid.Export.GridViewExportLink;
      zLink.ExportCellsAsDisplayText = false;
      zLink.ExportTo(true);
      System.Diagnostics.Process.Start(AFileName);
    }
 
    #endif   
  }

  #region "Validate Item/Seal/Location Numbers"
  //MR Start 23-AUG-10
  /// <summary>
  /// Helper Class to Validate if a Item UID number is between the upper and lower limits
  /// </summary>
  #region "Validate ITEM No"
  class ValidItemNumber
  {
    // DM Note* = ulong	0 to 18,446,744,073,709,551,615	Unsigned 64-bit integer	
    enum Range : ulong { Min = 1000000000001, Max = 1999999999999 }; // DM In 18-AUG-10
    private UInt64 m_ItemNo;

    public ValidItemNumber(string AItemNo)
    {
      try
      {
        m_ItemNo = Convert.ToUInt64(AItemNo);
      }
      catch (FormatException)
      {
        m_ItemNo = 0;
      }
      finally
      {
        // Just to keep the system alive if I've overlooked something
      }
    }

    public bool IsValidItemNo()
    {
      // DM out 18-AUG-10 return ((m_ItemNo >= m_LowerRange) && (m_ItemNo <= m_UpperRange)) ? true : false;
      return ((m_ItemNo >= (ulong)Range.Min) && (m_ItemNo <= (ulong)Range.Max)) ? true : false; // DM In 18-AUG-10
    }
  }
  #endregion 

  /// <summary>
  /// Helper Class to Validate if a Seal UID number is between the upper and lower limits
  /// </summary>
  #region "Validate SEAL No"
  class ValidSealNumber
  {
    enum Range : ulong { Min = 5000000000001, Max = 5999999999999 }; // DM In 18-AUG-10
    private UInt64 m_SealNo;

    public ValidSealNumber(string ASealNo)
    {
      m_SealNo = 0;
      try
      {
        m_SealNo = Convert.ToUInt64(ASealNo);
      }
      catch (FormatException)
      {
        m_SealNo = 0;
      }
      finally
      {
        // Just to keep the system alive if I've overlooked something
      }
    }
    public bool IsValidSealNo()
    {
      // DM Out 18-AUG-10 return ((m_SealNo >= m_LowerRange) && (m_SealNo <= m_UpperRange)) ? true : false;
      return ((m_SealNo >= (ulong)Range.Min) && (m_SealNo <= (ulong)Range.Max)) ? true : false; // DM In 18-AUG-10
    }
  }
  #endregion

  /// <summary>
  /// Helper Class to Validate if a LOCATION UID number is between the upper and lower limits
  /// </summary>
  #region "Validate LOCATION No"
  class ValidLocationNumber
  {
    //OUT JL 06-SEP -10 enum Range : ulong { Min = 8000000000001, Max = 8999999999999 }; // DM In 18-AUG-10

    enum Range : ulong { Min = 8000000000000, Max = 8999999999999 }; // JL In 06-SEP-10
    private UInt64 m_LocationNo;

    public ValidLocationNumber(string ALocationUID)
    {
      try
      {
        m_LocationNo = Convert.ToUInt64(ALocationUID);
      }
      catch (FormatException)
      {
        m_LocationNo = 0;
      }
      finally
      {
        // Just to keep the system alive if I've overlooked something
      }
    }

    public bool IsValidLocationNo()
    {
      // DM Out 18-AUG-10 return ((m_LocationNo >= m_LowerRange) && (m_LocationNo <= m_UpperRange)) ? true : false;
      return ((m_LocationNo >= (ulong)Range.Min) && (m_LocationNo <= (ulong)Range.Max)) ? true : false; // DM In 18-AUG-10
    }
  }
  #endregion
  //MR End 23-AUG-10
  #endregion

  #region "Special Entry Object"
  public class SpecialEntry
  {
    private DateTime m_EntryDate;
    private string m_ExpiryStr;

    public string EntryValue
    {
      get { return m_ExpiryStr; }
    }

    public SpecialEntry()
    {
      m_EntryDate = DateTime.Now;
      Init();
    }

    public SpecialEntry(DateTime ADateTimeIn)
    {
      m_EntryDate = ADateTimeIn;
      Init();
    }

    private void Init()
    {
      long zExpiry;
      string zTmpStr;

      zExpiry = ((m_EntryDate.Year - 2000) * m_EntryDate.Month) * m_EntryDate.Day;
      zTmpStr = string.Format("{0:d2}{1:d2}{2:d2}", (m_EntryDate.Year - 2000), m_EntryDate.Month, m_EntryDate.Day);

      m_ExpiryStr = string.Format("{0:d6}", Convert.ToUInt32(zTmpStr) - zExpiry);
      m_ExpiryStr = m_ExpiryStr.Remove(0, 1); // Remove the First Char
    }
  }
  #endregion 

  #region "ISM Validate Password"
  public class ISMPassword
  {
    // C# to convert a string to a byte array.
    private static byte[] StrToByteArray(string AStrIn)
    {
      System.Text.ASCIIEncoding zEncoding = new System.Text.ASCIIEncoding();
      return zEncoding.GetBytes(AStrIn);
    }

    private static string ByteArrayToString(byte[] AByteArr)
    {
      string zRetVal = "";
      foreach (byte zByte in AByteArr)
        zRetVal = zRetVal + string.Format("{0:X2}", zByte);
      return zRetVal.ToString();
    }

    public string EncryptPassword(string APassword)
    {
      string zRetValue;
      byte[] zPasswordBytes = StrToByteArray(APassword);
      // This is one implementation of the abstract class MD5.
      // DM Note: For Future Use if MD5 is not acceptable >> byte[] zEncryptedPWord2 = new SHA1Managed().ComputeHash(zPasswordBytes);
      byte[] zEncryptedPWord = new MD5CryptoServiceProvider().ComputeHash(zPasswordBytes);  // Get the Hash
      zRetValue = ByteArrayToString(zEncryptedPWord);
      return zRetValue;
    }
  }
  #endregion


}
