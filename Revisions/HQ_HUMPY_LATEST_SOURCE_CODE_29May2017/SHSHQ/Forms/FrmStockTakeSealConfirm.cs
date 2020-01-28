 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

using System;
using System.Windows.Forms;
using ISMDAL.TableColumnName;   

namespace ISM.Forms
{
  public partial class FrmStockTakeSealConfirm : DevExpress.XtraEditors.XtraForm
  {
    #region "Priave Variable "
    private string m_LocationUID = string.Empty;
    private long m_LocationID = 0;
    private int m_SealBroken = 0;  
    private ISMLoginInfo m_ISMLoginInfo;   
    public DialogResult m_DialogResult;
    private string m_ResultMsg = "";  
    private string m_TaskID = "";  

    #endregion

    #region "Property"
    public long LocationID
    {
      get { return m_LocationID; }
      set { m_LocationID = value; }
    }

    public string LocationUID
    {
      get { return m_LocationUID; }
      set { m_LocationUID = value; }
    }
    public string StockTakeResult  
    {
        get { return m_ResultMsg; }
        set { m_ResultMsg = value; }
    }
    public string TaskID  
    {
        get { return m_TaskID; }
        set { m_TaskID = value; }
    }
    #endregion

    public FrmStockTakeSealConfirm(ISMLoginInfo AISMLoginInfo)
    {
      InitializeComponent();
      m_ISMLoginInfo = AISMLoginInfo; 
    }
    private void FrmStockTakeSealConfirm_Load(object sender, EventArgs e)
    {
      gcSealCodeConfirm.Enabled = false;
      gcSealUIDConfirm.Enabled = false;
      txtLocation.Properties.Mask.EditMask = m_ISMLoginInfo.Params.LocPrefix + "\\d{0,12}";
      txtLocation.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
      txtSealUID.Properties.Mask.EditMask = m_ISMLoginInfo.Params.SealPrefix + "\\d{0,12}";
      txtSealUID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
      btnException.Enabled = false; 
    }
    private void checkEditYes_CheckedChanged(object sender, EventArgs e)
    {
      checkEditNo.CheckedChanged -= new System.EventHandler(this.checkEditNo_CheckedChanged);

        if (checkEditYes.Checked == true)
          checkEditNo.Checked = false;
        else 
          checkEditNo.Checked = true;

        if (!btnConfirm.Enabled)
          btnConfirm.Enabled = true;

      checkEditNo.CheckedChanged += new System.EventHandler(this.checkEditNo_CheckedChanged);
    }

    private void checkEditNo_CheckedChanged(object sender, EventArgs e)
    {
      this.checkEditYes.CheckedChanged -= new System.EventHandler(this.checkEditYes_CheckedChanged);

      if (checkEditNo.Checked == true)
        checkEditYes.Checked = false;
      else
        checkEditYes.Checked = true;

      if (!btnConfirm.Enabled)
        btnConfirm.Enabled = true;
      checkEditYes.CheckedChanged += new System.EventHandler(this.checkEditYes_CheckedChanged);
    }
   
    private void btnVerifyConfirm_Click(object sender, EventArgs e)
    {
      try
      {
        int zSealCode = 0;
        
        if (txtLocation.Text.Trim().Length != 13)
        {
          dxErrorProvider.SetError(txtLocation, "Invalid Location UID");
          txtLocation.Focus();
          return;
        }
        else if (txtLocation.Text.Trim() == LocationUID)
        {
          if (m_ISMLoginInfo.ISMServer.GetLocationSealStatus(LocationID, ref zSealCode, ref m_SealBroken))
          {
            radioGroupSealType.SelectedIndex = zSealCode;
             
             
            if ((zSealCode == (int)ISM.SealType.None || m_SealBroken == 0))
            {
               
               
              m_DialogResult = DialogResult.No; 
              MessageBox.Show("Seal is broken. Complete Stocktake Task", "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
              using (FrmStockTake zFrmStockTake = new FrmStockTake(m_ISMLoginInfo) { LocationID = LocationID, TaskID = TaskID })
              {
                zFrmStockTake.ShowDialog();
                 
                if (zFrmStockTake.StockDlgResult == DialogResult.Cancel || zFrmStockTake.StockDlgResult == DialogResult.Retry)  
                    m_DialogResult = zFrmStockTake.StockDlgResult;
              }
              Close();
            }
            else if (zSealCode == (int)ISM.SealType.TamperEvident && m_SealBroken != 0)
            {
              gcSealCodeConfirm.Enabled = true;
              btnConfirm.Enabled = false;
            }
             
            else if (zSealCode == (int)ISM.SealType.Electronic && m_SealBroken != 0)
            {
              btnException.Enabled = true;   
              gcSealUIDConfirm.Enabled = true;
              txtSealUID.Focus();  
            }
          }
        }
        else
        {
          dxErrorProvider.SetError(txtLocation, "Invalid Location UID");
          txtLocation.Focus();
        }
      }
      catch (Exception ex)
      {
          MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void btnConfirm_Click(object sender, EventArgs e)
    {
      try
      {
          if (checkEditYes.Checked == false && checkEditNo.Checked == false)
          {
             MessageBox.Show("Select any one of the Seal Status","Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Warning);
             return;
          }
          if (checkEditYes.Checked == true)
          {
               
               
               
               
               
               
              m_ISMLoginInfo.ISMServer.UpdateStockTakeSealedLocation(LocationID, m_ISMLoginInfo.UserID, TaskID);  
              string zJnlDesc = String.Format("Stocktake Task has been completed for the Location UID {0}", LocationUID);  
              m_ISMLoginInfo.AddToJournal("T", zJnlDesc, "STK350", LocationID.ToString(), "0", "0", "", 0);  
              m_DialogResult = DialogResult.Yes;  
          }
          else if (checkEditNo.Checked == true)
          {
              if (m_SealBroken == 0)
                  m_ISMLoginInfo.AddToJournal("E", "Seal is Broken", "SEL404", LocationID.ToString(), "0", "0", "", 0);  
              else
              {
                  m_ISMLoginInfo.AddToJournal("E", "Seal is Broken. But Location Table shows that is Sealed", "SEL404", LocationID.ToString(), "0", "0", "", 0);  
                  UpdateSealStatus();  
              }
               
              using (FrmStockTake zFrmStockTake = new FrmStockTake(m_ISMLoginInfo) { LocationID = LocationID, TaskID = TaskID })
              {
                  zFrmStockTake.ShowDialog();
                  StockTakeResult = zFrmStockTake.StockTakeResult;  
                   
                   
                  m_DialogResult = zFrmStockTake.StockDlgResult;  
              }
          }
        Close();
      }
      catch (Exception ex)
      {
          MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void UpdateSealStatus()  
    {
        try
        {
            int zSealCode = 0;
            int zSealBroken = 0;
            long zSealUID = 0;
            long zTemp = 0;

             
            m_ISMLoginInfo.ISMServer.GetLocationSealStatus(LocationID, ref zSealCode, ref zSealBroken);
            if (zSealCode == 1) 
            {
                 
                m_ISMLoginInfo.ISMServer.UpdateSealStatus(1, ref zTemp, zSealUID, LocationID, 1, 0);
            }
            else if (zSealCode == 2) 
            {
                 
                m_ISMLoginInfo.ISMServer.UpdateSealStatus(0, ref zSealUID, 0, LocationID, 0, 0);
                 
                m_ISMLoginInfo.ISMServer.UpdateSealStatus(1, ref zTemp, zSealUID, LocationID, 2, 0);
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }

    private void btnSealUIDConfirm_Click(object sender, EventArgs e)
    {
      try
      {
        
        if (txtSealUID.Text.Trim().Length != 13)
        {
            if (txtSealUID.Text.Trim() == "")
                dxErrorProvider.SetError(txtSealUID, "Enter Seal UID");
            else
            {
                dxErrorProvider.SetError(txtSealUID, "Invalid Seal UID");
                txtSealUID.Focus();
                return;
            }
        }
        else
        {
          long SealCode = long.Parse(txtSealUID.Text.Substring(1, 12));
          if (m_ISMLoginInfo.ISMServer.ValidLocationSealUID(LocationID, SealCode))
          {
            gcSealCodeConfirm.Enabled = true;
             
            gcSealCodeConfirm.LookAndFeel.SetSkinStyle(m_ISMLoginInfo.Params.AppSkinColour);  
            checkEditNo.Focus();
          }
          else
          {
             
             
             
             
            dxErrorProvider.SetError(txtSealUID, "Invalid Seal UID");
             
          }
        }
      }
      catch (Exception ex)
      {
          MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void btnClose_Click(object sender, EventArgs e)
    {
      DialogResult zReply = MessageBox.Show("Seal Verification is not complete. Do you want to close?", "Stocktake", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
      if (zReply == DialogResult.Yes)
      {
        StockTakeResult = "Seal Verification is not completed for the Location UID " + LocationUID;  
        m_DialogResult = DialogResult.Cancel;
        Close();
      }
    }
     
    private void btnException_Click(object sender, EventArgs e)
    {
        try
        {        
             
            long zSealUID = 0;
             
            if (txtSealUID.Text.Trim() != "")
            {
                if (txtSealUID.Text.Trim().Length != 13)
                {
                    dxErrorProvider.SetError(txtSealUID, "Enter 13 digit Seal UID");
                    txtSealUID.Focus();
                    return;
                }
                else
                    zSealUID = long.Parse(txtSealUID.Text.Trim().Substring(1,12));

            }
            FrmException frmSTException = new FrmException(m_ISMLoginInfo) { LocationUID = LocationID, SealUID = zSealUID, JournalType = "STK", MsgBoxCaption = "Stocktake", ExceptionType = (int)ISM.ExceptionType.StockTake, ExceptionLevel = 1 };
            frmSTException.Text = "Stocktake Exception";
            frmSTException.ShowDialog();
            if (frmSTException.SaveResult)  
            {
                 
                using (FrmStockTake zFrmStockTake = new FrmStockTake(m_ISMLoginInfo) { LocationID = LocationID, TaskID = TaskID })
                {

                    zFrmStockTake.StockDlgResult = DialogResult.Ignore;  
                    zFrmStockTake.ShowDialog();
                    if (zFrmStockTake.StockDlgResult == DialogResult.Cancel)
                    {
                        m_DialogResult = zFrmStockTake.StockDlgResult;
                        StockTakeResult = "Seal Verification is not completed for the Location UID " + LocationUID;  
                    }
                    else
                        m_DialogResult = zFrmStockTake.StockDlgResult;  
                }
            }
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    private void txtSealUID_EditValueChanged(object sender, EventArgs e)
    {
        dxErrorProvider.SetError(txtSealUID, null);
         
         
         
         
         
         
         
         
         
         
    }

    private void txtLocation_EditValueChanged(object sender, EventArgs e)
    {
        dxErrorProvider.SetError(txtLocation, null);
    }
     
  }
}