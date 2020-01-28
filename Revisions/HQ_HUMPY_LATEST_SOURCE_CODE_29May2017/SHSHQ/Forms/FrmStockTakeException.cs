#region "History"

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
// File        : FrmStockTakeException.cs                                     //
//                                                                            //
// Description : Stock Take Exception                                         //
//                                                                            //
// Initial Author: R. Murugan                                          .      //
// Date Written  : 03-AUG-2010                                                //
// Documentation : BCDS Visual Source Safe.                                   //
//                 ISM\Document\Functional Spec\Functional Spec V1.doc        //
//                                                                            //
//****************************************************************************//
// Modification History:                                                      //
//                                                                            //
// Date..... Who..........  Modification Description......................... //
// DD-MMM-YY xxxxxxxxxxxxx                                                    //
//                                                                            //
// 03-AUG-10 MR             ver 1.0                                           //
//                          Initial Release.                                  //
//                                                                            //
//****************************************************************************//
#endregion
#region "Namespace"

using System;
using System.Data;
using ISMDAL.TableColumnName;
using System.Windows.Forms;
#endregion

namespace ISM.Forms
{
  public partial class FrmStockTakeException : DevExpress.XtraEditors.XtraForm
  {
    private ISMLoginInfo m_ISMLoginInfo;
    private long m_LocationID = 0;
    private long m_ItemID = 0;
    private long m_SealID = 0;
    private string m_StockCode = "";
    private string m_JournalType = "";
    private string m_MsgString = "";

    public long LocationID
    {
      get { return m_LocationID; }
      set { m_LocationID = value;}
    }
    public long ItemID
    {
      get { return m_ItemID; }
      set { m_ItemID = value; }
    }
    public long SealID
    {
      get { return m_SealID; }
      set { m_SealID = value; }
    }
    public string StockCode
    {
      get { return m_StockCode; }
      set { m_StockCode = value; }
    }
    public string JournalType
    {
      get { return m_JournalType; }
      set { m_JournalType = value; }
    }
    
    public FrmStockTakeException(ISMLoginInfo AISMLoginInfo)
    {
      InitializeComponent();
      m_ISMLoginInfo = AISMLoginInfo;

    }
    private void FrmStockTakeException_Load(object sender, EventArgs e)
    {
      if (LocationID > 0)
        m_MsgString = String.Format("Location UID 8{0} ", LocationID.ToString().PadLeft(12, '0'));
      if (ItemID > 0)
        m_MsgString = String.Format("{0}Item UID 1{1} ", m_MsgString, ItemID.ToString().PadLeft(12, '0'));
      if (SealID > 0)
        m_MsgString = String.Format("{0}Seal UID 5{1} ", m_MsgString, SealID.ToString().PadLeft(12, '0'));
      if (StockCode.Trim() != "")
        m_MsgString = m_MsgString + "Stock Code " + StockCode;

     // m_MsgString = "Exception raised for the " + m_MsgString;
      LoadJournalType();
      btnSave.Enabled = false;
      /////////////////////////////////////////////////////////////

    }
    private void LoadJournalType()
    {
      try
      {
       // string zJournalType = "STK";
        DataSet ds = m_ISMLoginInfo.ISMServer.GetJournalType(JournalType);
        if (ds != null)
        {
          LBExcpCode.DataSource = ds.Tables[0].DefaultView;
          LBExcpCode.ValueMember = ISMJournalType.Code;
          LBExcpCode.DisplayMember = ISMJournalType.Description;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    #region Save Button"
    private void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        if (LBExcpCode.CheckedItems.Count > 1)
        {
          MessageBox.Show("Multiple Selections are not Permitted. Select a Exception at a time", "Stock Take Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
        ////////////////////////////////////////////////////////////////////////////////////////
        DialogResult zReply = MessageBox.Show("Do you want raise Exception for the " + m_MsgString, "Exception", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        if (zReply == DialogResult.Yes)
        {
          ISMJournal.StructJouurnal zStructJournal = new ISMJournal.StructJouurnal();
          foreach (object item in LBExcpCode.CheckedItems)
          {
            DataRowView row = item as DataRowView;
            zStructJournal.ExceptionFlag = "E";
            zStructJournal.ExceptionDesc = row[ISMJournalType.Description].ToString();
            zStructJournal.JournalCode = row[ISMJournalType.Code].ToString();
            zStructJournal.LocationID = LocationID.ToString();
            zStructJournal.UserID = m_ISMLoginInfo.UserID.ToString();
            zStructJournal.TaskID = "0";
            zStructJournal.SealID = SealID.ToString();
            zStructJournal.StockCode = StockCode;
            m_ISMLoginInfo.ISMServer.AddToJournalTable(zStructJournal);
          }
          MessageBox.Show("Exception raised for the " + m_MsgString,  "Exception", MessageBoxButtons.OK, MessageBoxIcon.Information);
          btnSave.Enabled = false;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

    #region "Close Button"

    private void btnClose_Click(object sender, EventArgs e)
    {
      Close();
    }
    #endregion
    #region "List Box Event "
    private void LBExcpCode_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
    {
      try
      {
        bool zItemCheck = false;
        foreach (object item in LBExcpCode.CheckedItems)
        {
          zItemCheck = true;
          break;
        }
        if (zItemCheck)
          btnSave.Enabled = true;
        else
          btnSave.Enabled = false;
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }
    #endregion
  }
}