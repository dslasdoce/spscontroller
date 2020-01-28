#region "History"

 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
#endregion

#region "Namespace"

using System;
using System.Data;
using ISMDAL.TableColumnName;
using System.Windows.Forms;
#endregion

namespace ISM.Forms
{
   
  public partial class FrmException : DevExpress.XtraEditors.XtraForm
  {
    private ISMLoginInfo m_ISMLoginInfo;
    private long m_LocationID = 0;
    private long m_ItemID = 0;
    private long m_SealID = 0;
    private string m_StockCode = "";
    private string m_InvenCatCode = "";
    private string m_JournalType = "";
    private string m_MsgString = "";
    private string m_MsgBoxCaption = "";  
    private int m_ExpType = 0;  
    private byte m_ExpLevel = 0;  
    private DialogResult m_DlgResult = DialogResult.Yes;
    private bool m_SaveResult = false;  
   
    public long LocationUID
    {
      get { return m_LocationID; }
      set { m_LocationID = value;}
    }
    public long ItemUID
    {
      get { return m_ItemID; }
      set { m_ItemID = value; }
    }
    public long SealUID
    {
      get { return m_SealID; }
      set { m_SealID = value; }
    }
    public string StockCode
    {
      get { return m_StockCode; }
      set { m_StockCode = value; }
    }
    public string InvenCatCode  
    {
        get { return m_InvenCatCode; }
        set { m_InvenCatCode = value; }
    }
    public string JournalType
    {
      get { return m_JournalType; }
      set { m_JournalType = value; }
    }
    public string MsgBoxCaption
    {
      get { return m_MsgBoxCaption; }
      set {m_MsgBoxCaption = value;}
    }
    public int ExceptionType
    {
        get { return m_ExpType; }
        set { m_ExpType = value; }
    }
    public byte ExceptionLevel
    {
        get { return m_ExpLevel; }
        set { m_ExpLevel = value; }
    }
    public DialogResult DlgResult
    {
        get { return m_DlgResult; }
        set { m_DlgResult = value; }
    }

    public bool SaveResult
    {
        get { return m_SaveResult; }
        set { m_SaveResult = value; }
    }
    public FrmException(ISMLoginInfo AISMLoginInfo)
    {
      InitializeComponent();
      m_ISMLoginInfo = AISMLoginInfo;

    }
    private void FrmStockTakeException_Load(object sender, EventArgs e)
    {
      if (LocationUID > 0)
        m_MsgString = String.Format("Location UID {0}{1} ", m_ISMLoginInfo.Params.LocPrefix,LocationUID.ToString().PadLeft(12, '0'));
      if (ItemUID > 0)
        m_MsgString = String.Format("{0}Item UID {1}{2} ", m_MsgString, m_ISMLoginInfo.Params.ItemPrefix,ItemUID.ToString().PadLeft(12, '0'));
      if (SealUID > 0)
        m_MsgString = String.Format("{0}Seal UID {1}{2} ", m_MsgString, m_ISMLoginInfo.Params.SealPrefix,SealUID.ToString().PadLeft(12, '0'));
      if (StockCode.Trim() != "")
        m_MsgString = String.Format("{0}Stock Code {1}", m_MsgString, StockCode);
    
      
      LoadJournalType();
      btnSave.Enabled = false;
      txtOtherException.Enabled = false;
    }
    private void LoadJournalType()
    {
      try
      {
        
        DataSet ds = null;
        if(ExceptionType == (int)ISM.ExceptionType.Other)
            ds = m_ISMLoginInfo.ISMServer.GetJournalType(JournalType);
        else
            ds = m_ISMLoginInfo.ISMServer.GetStockTakeJournalType(ExceptionLevel);
        if (ds != null)
        {
          LBExcpCode.DataSource = ds.Tables[0].DefaultView;
          LBExcpCode.ValueMember = ISMJournalType.Code;
          LBExcpCode.DisplayMember = ISMJournalType.Description;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), MsgBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    #region Save Button"
    private void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        DialogResult zReply = MessageBox.Show(String.Format("Do you want to raise an Exception for the {0}?", m_MsgString), MsgBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        if (zReply == DialogResult.Yes)
        {
          ISMJournal.StructJouurnal zStructJournal = new ISMJournal.StructJouurnal();
          foreach (object item in LBExcpCode.CheckedItems)
          {
            DataRowView row = item as DataRowView;
             
            if (row[ISMJournalType.Description].ToString().Contains("Other") && txtOtherException.Text.Trim() == "")
            {
              MessageBox.Show("Provide Exception Text", MsgBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
              return;
            }
            zStructJournal.ExceptionFlag = "E";
            if (row[ISMJournalType.Description].ToString().Contains("Other"))
            {
                 
                DlgResult = DialogResult.Yes;  
                zStructJournal.ExceptionDesc = row[ISMJournalType.Description].ToString() + " - " + txtOtherException.Text.Trim();
            }
            else
                zStructJournal.ExceptionDesc = row[ISMJournalType.Description].ToString();
             
            if (ExceptionType == (int)ISM.ExceptionType.StockTake && ExceptionLevel == 0)  
            {
                if (row[ISMJournalType.Code].ToString().Contains("STK353"))
                {
                    DlgResult = DialogResult.No;
                     
                    if (txtOtherException.Text.Trim() != "")
                        zStructJournal.ExceptionDesc = row[ISMJournalType.Description].ToString() + " for the Location UID " + m_ISMLoginInfo.Params.LocPrefix + LocationUID.ToString().PadLeft(12, '0') + " - " + txtOtherException.Text.Trim();
                    else
                        zStructJournal.ExceptionDesc = row[ISMJournalType.Description].ToString() + " for the Location UID " + m_ISMLoginInfo.Params.LocPrefix + LocationUID.ToString().PadLeft(12, '0');

                }
                 
                if (row[ISMJournalType.Code].ToString().Contains("STK361"))  
                {
                     
                    
                    DlgResult = DialogResult.No;
                    if (txtOtherException.Text.Trim() != "")
                        zStructJournal.ExceptionDesc = row[ISMJournalType.Description].ToString() + " for the Location UID " + m_ISMLoginInfo.Params.LocPrefix + LocationUID.ToString().PadLeft(12, '0') + " - " + txtOtherException.Text.Trim();
                    else
                        zStructJournal.ExceptionDesc = row[ISMJournalType.Description].ToString() + " for the Location UID " + m_ISMLoginInfo.Params.LocPrefix + LocationUID.ToString().PadLeft(12, '0');

                }
            }
            else if (ExceptionType == (int)ISM.ExceptionType.StockTake && ExceptionLevel == 2)  
            {
                if (row[ISMJournalType.Code].ToString().Contains("STK357"))  
                {
                    
                    DlgResult = DialogResult.Retry;  
                    if (ItemUID > 0)
                    {
                        if (txtOtherException.Text.Trim() != "")
                            zStructJournal.ExceptionDesc = row[ISMJournalType.Description].ToString() + " for the Item UID " + m_ISMLoginInfo.Params.ItemPrefix + ItemUID.ToString().PadLeft(12, '0') + " - " + txtOtherException.Text.Trim();
                        else
                            zStructJournal.ExceptionDesc = row[ISMJournalType.Description].ToString() + " for the Item UID " + m_ISMLoginInfo.Params.ItemPrefix + ItemUID.ToString().PadLeft(12, '0');

                    }
                    else
                    {
                        if (txtOtherException.Text.Trim() != "")
                            zStructJournal.ExceptionDesc = row[ISMJournalType.Description].ToString() + " - " + txtOtherException.Text.Trim();
                        else
                            zStructJournal.ExceptionDesc = row[ISMJournalType.Description].ToString();
                    }

                     
                }
                if (row[ISMJournalType.Code].ToString().Contains("STK362"))  
                {
                     
                    
                    
                     
                     
                    DlgResult = DialogResult.No;   
                    if (ItemUID > 0)
                    {
                        if (txtOtherException.Text.Trim() != "")
                            zStructJournal.ExceptionDesc = row[ISMJournalType.Description].ToString() + " for the Item UID " + m_ISMLoginInfo.Params.ItemPrefix + ItemUID.ToString().PadLeft(12, '0') + " - " + txtOtherException.Text.Trim();
                        else
                            zStructJournal.ExceptionDesc = row[ISMJournalType.Description].ToString() + " for the Item UID " + m_ISMLoginInfo.Params.ItemPrefix + ItemUID.ToString().PadLeft(12, '0');

                    }
                    else
                    {
                        if (txtOtherException.Text.Trim() != "")
                            zStructJournal.ExceptionDesc = row[ISMJournalType.Description].ToString() + " - " + txtOtherException.Text.Trim();
                        else
                            zStructJournal.ExceptionDesc = row[ISMJournalType.Description].ToString();
                    }

                }
            }
             
            else if (ExceptionType == (int)ISM.ExceptionType.StockTake && ExceptionLevel == 1)  
            {
                if (row[ISMJournalType.Code].ToString().Contains("SEL401"))
                    DlgResult = DialogResult.No;
                if (row[ISMJournalType.Code].ToString().Contains("SEL401"))
                {
                    if (txtOtherException.Text.Trim() != "")  
                        zStructJournal.ExceptionDesc = row[ISMJournalType.Description].ToString() + " for the Location UID " + m_ISMLoginInfo.Params.LocPrefix + LocationUID.ToString().PadLeft(12, '0') + " - " + txtOtherException.Text.Trim();
                    else
                        zStructJournal.ExceptionDesc = row[ISMJournalType.Description].ToString() + " for the Location UID " + m_ISMLoginInfo.Params.LocPrefix + LocationUID.ToString().PadLeft(12, '0');
                }
            }
              
             
            zStructJournal.JournalCode = row[ISMJournalType.Code].ToString();
            zStructJournal.LocationID = LocationUID.ToString();
            zStructJournal.UserID = m_ISMLoginInfo.UserID.ToString();
            zStructJournal.TaskID = "0";
            zStructJournal.SealID = SealUID.ToString();
            zStructJournal.StockCode = StockCode;
            zStructJournal.ItemUID = ItemUID.ToString();
            zStructJournal.PortalName = "";                  
            zStructJournal.StockCategoryCode = InvenCatCode;  
            m_ISMLoginInfo.ISMServer.AddToJournalTable(zStructJournal);
            SaveResult = true;  
             
             
             
             
            if (row[ISMJournalType.Code].ToString().Contains("SEL401") || row[ISMJournalType.Code].ToString().Contains("SEL402") || row[ISMJournalType.Code].ToString().Contains("STK359"))
                UpdateSealStatus();

          }
          
          btnSave.Enabled = false;
           
          LBExcpCode.UnCheckAll();
          txtOtherException.Text = "";  
          if (ExceptionType == (int)ISM.ExceptionType.StockTake)  
              Close();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), MsgBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
        if (LBExcpCode.CheckedItems.Count > 0)
          zItemCheck = true;
        else
          txtOtherException.Enabled = false;
         
        if (LBExcpCode.CheckedItems.Count > 1)
        {
          MessageBox.Show("Select one Exception at a time", MsgBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
          LBExcpCode.UnCheckAll();
          return;
        }
        else
        {
          foreach (object item in LBExcpCode.CheckedItems)
          {
            DataRowView row = item as DataRowView;
            if (row[ISMJournalType.Description].ToString().Contains("Other"))
              txtOtherException.Enabled = true;
            if (ExceptionType == (int)ISM.ExceptionType.StockTake && ExceptionLevel == 2 && row[ISMJournalType.Code].ToString().Contains("STK362"))
                txtOtherException.Enabled = true;
             
            if (ExceptionType == (int)ISM.ExceptionType.StockTake && ExceptionLevel == 0 && row[ISMJournalType.Code].ToString().Contains("STK361"))
                txtOtherException.Enabled = true;
             
            if (ExceptionType == (int)ISM.ExceptionType.StockTake && ExceptionLevel == 1 && row[ISMJournalType.Code].ToString().Contains("SEL401"))
                txtOtherException.Enabled = true;  
          }
        }
         
         
         
         
         
         
         
        if (zItemCheck)
          btnSave.Enabled = true;
        else
          btnSave.Enabled = false;
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), MsgBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }
    #endregion

      #region "Update Seal Status"

    private void UpdateSealStatus()  
    {
        try
        {
            int zSealCode = 0;
            int zSealBroken = 0;
            long zSealUID = 0;
            long zTemp = 0;

             
            m_ISMLoginInfo.ISMServer.GetLocationSealStatus(LocationUID, ref zSealCode, ref zSealBroken);
            if (zSealCode == 1) 
            {
                 
                m_ISMLoginInfo.ISMServer.UpdateSealStatus(1, ref zTemp, zSealUID, LocationUID, 1, 0);
            }
            else if (zSealCode == 2) 
            {
                 
                m_ISMLoginInfo.ISMServer.UpdateSealStatus(0, ref zSealUID, 0, LocationUID, 0, 0);
                 
                m_ISMLoginInfo.ISMServer.UpdateSealStatus(1, ref zTemp, zSealUID, LocationUID, 2, 0);
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }
      #endregion

  }
}