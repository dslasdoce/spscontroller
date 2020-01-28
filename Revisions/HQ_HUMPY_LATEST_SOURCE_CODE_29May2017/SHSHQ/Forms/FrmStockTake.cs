#region "History"
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
#endregion

#region "Namespace"
using System;
using System.Data;
using System.Windows.Forms;
using ISMDAL.TableColumnName;
#endregion

namespace ISM.Forms
{
  public partial class FrmStockTake : DevExpress.XtraEditors.XtraForm
  {
    #region "Private Variable Declaration"
    private ISMLoginInfo m_ISMLoginInfo;
    private long m_LocationID = 0;
    private long m_ItemID = 0;  
    private int m_zRetryQuantity;
    private int m_zRetrySerialNo;
    private int m_nRecordCount;
    private bool m_bStockComplate;
    private int m_zMaxRetry = 3;  
    public DialogResult m_DialogResult;
    private string m_ResultMsg = "";  
    private string m_TaskID = "";  
   
    # endregion

    #region "Property
    public long LocationID
    {
      get { return m_LocationID; }
      set { m_LocationID = value; }
    }

    public DialogResult StockDlgResult
    {
      get { return m_DialogResult; }
      set { m_DialogResult = value; }
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
    # endregion
      public FrmStockTake(ISMLoginInfo AISMLoginInfo)
    {
      InitializeComponent();
      m_ISMLoginInfo = AISMLoginInfo;
    }

    private void FrmStockTake_Load(object sender, EventArgs e)
    {
      InitData();

       
      txtQuanity.Properties.Mask.EditMask = "##########;";  
      txtQuanity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
      txtQuanity.Properties.Mask.UseMaskAsDisplayFormat = true;

    }
    private void InitData()
    {
      try
      {
        DataSet ds = null;
        ds = m_ISMLoginInfo.ISMServer.GetStockTakeData(LocationID);
        dataNavigator.DataSource = ds.Tables[0].DefaultView;
         
        txtLocationUID.DataBindings.Add("EditValue", dataNavigator.DataSource, ISMStock.StockLocationID);
        txtItemUID.DataBindings.Add("EditValue", dataNavigator.DataSource, ISMStock.StockItemUID);
        txtStockCode.DataBindings.Add("EditValue", dataNavigator.DataSource, ISMStock.StockCatalogueCode);
        txtStockName.DataBindings.Add("EditValue", dataNavigator.DataSource, ISMStockCatalogue.StockShortName);
        txtDBQuanity.DataBindings.Add("EditValue", dataNavigator.DataSource, ISMStock.StockQtyAtLoc);
        txtDBTrackInd.DataBindings.Add("EditValue", dataNavigator.DataSource, ISMStockCatalogue.StockTrakingInd);
        txtDBtxtSerialNo.DataBindings.Add("EditValue", dataNavigator.DataSource, ISMStock.StockSerialEquipNo);
        txtDBQuanity.Hide();
        txtDBTrackInd.Hide();
        txtDBtxtSerialNo.Hide();
        m_nRecordCount = ds.Tables[0].Rows.Count;
        txtSerialNo.Focus();
        if (m_nRecordCount > 0)
        {
          txtQuanity.Enabled = true;
          btnValidate.Enabled = true;
          if (txtItemUID.Text.Trim() != "" && txtItemUID.Text.Length == 13)
              m_ItemID = long.Parse(txtItemUID.Text.Substring(1, 12));
           
           
          m_ISMLoginInfo.AddToJournal("T", "Stocktake Task In Progress for Location UID " + txtLocationUID.Text, "STK352", LocationID.ToString(), "0", "0", "", 0); 
        }
        else
        {
          txtQuanity.Enabled = false;
          btnValidate.Enabled = false;
          m_bStockComplate = true; 
           
           
           
           
          txtStatusMsg.Text = String.Format("Location UID {0} does not contain any stock Item", m_ISMLoginInfo.Params.LocPrefix + LocationID.ToString().PadLeft(12, '0'));  
        }
        if (txtDBTrackInd.Text.Trim() == "S" || txtDBTrackInd.Text.Trim() == "E")
        {
          txtSerialNo.Enabled = true;
          txtSerialNo.Focus();
        }
        else
        {
          txtSerialNo.Enabled = false;
          txtQuanity.Focus();
        }

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Count", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void btnValidate_Click(object sender, EventArgs e)
    {
      try
      {
        float zDbQty = 0;
        float zStockQty = 0;
        string zJnlDesc;
        bool zValidStockData = true;
        bool zRecMoved = false;

        long zItemUID =0;   
        string zStockCode = "";  

        if (ValidateData())
        {
          zStockQty = float.Parse(txtQuanity.Text);
           
          if (txtItemUID.Text.Trim() != "" && txtItemUID.Text.Length == 13)
              m_ItemID = long.Parse(txtItemUID.Text.Substring(1, 12));
          
          if (txtDBQuanity.Text.Trim() != "")
            zDbQty = float.Parse(txtDBQuanity.Text);

          if (txtDBTrackInd.Text.Trim() == "S" || txtDBTrackInd.Text.Trim() == "E")
          {
            if (txtSerialNo.Text.Trim().ToUpper() != txtDBtxtSerialNo.Text.Trim().ToUpper())
            {
              MessageBox.Show("Serial/Equipment No does not match", "Stock Count", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              dxErrorProvider.SetError(txtSerialNo, "Serial/Equipment No does not match");
              m_zRetrySerialNo += 1;
              zValidStockData = false;
               
              if(m_zRetrySerialNo >= m_zMaxRetry)
              {
                m_zRetrySerialNo = 0;
                MessageBox.Show("Serial/Equipment No does not match. You have exceeded the maximum retry option", "Stock Count", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StockDlgResult = DialogResult.Retry;  
                 
                 
                zJnlDesc = String.Format("Stocktake Serial/Equipment No does not match for Loc UID {0} Item UID {1} Stock Code {2}", txtLocationUID.Text, txtItemUID.Text, txtStockCode.Text);
                 
                m_ISMLoginInfo.AddToJournal("E", zJnlDesc, "STK360", LocationID.ToString(), "0", "0", txtStockCode.Text, m_ItemID);
                 
                if ((dataNavigator.Position + 1) == m_nRecordCount)
                  m_bStockComplate = true;
                MoveNextRecord();
                zRecMoved = true;
                 
                 
                 
                 
                 
                 
              }
            }
          }
          if (zDbQty != zStockQty)
          {
            MessageBox.Show("Stock Quantity does not match. Please recount", "Stock Count", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            dxErrorProvider.SetError(txtQuanity, "Stock Quantity does not match");
            txtQuanity.Focus();
            txtQuanity.SelectAll();
            m_zRetryQuantity += 1;
            zValidStockData = false;
             
            if (m_zRetryQuantity >= m_zMaxRetry)
            {
              m_zRetryQuantity = 0;
              MessageBox.Show("Stock Quantity does not match. You have exceeded the maximum retry option", "Stock Count", MessageBoxButtons.OK, MessageBoxIcon.Error);
              StockDlgResult = DialogResult.Retry;  
               
               
              zJnlDesc = String.Format("Stocktake Quantity Mismatched for Loc UID {0} Item UID {1} Stock Code {2}", txtLocationUID.Text, txtItemUID.Text, txtStockCode.Text);
               
              m_ISMLoginInfo.AddToJournal("E", zJnlDesc, "STK355", LocationID.ToString(), "0", "0", txtStockCode.Text, m_ItemID);
               
              txtQuanity.EditValue = null;
              dxErrorProvider.SetError(txtQuanity, null);
              if ((dataNavigator.Position + 1) == m_nRecordCount && !zRecMoved)
                m_bStockComplate = true;
              if (!zRecMoved)  
                MoveNextRecord();
               
               
               
               
               
               
            }
          }
          if (zValidStockData)
          {
             
            txtQuanity.EditValue = null;
            txtSerialNo.EditValue = null;
             
            m_zRetryQuantity = 0;
            m_zRetrySerialNo = 0;
            if ((dataNavigator.Position + 1) == m_nRecordCount)
              m_bStockComplate = true;

             
            zItemUID = long.Parse(txtItemUID.Text.Trim().Substring(1,txtItemUID.Text.Length - 1));
            zStockCode = txtStockCode.Text.Trim();
             
            m_ISMLoginInfo.ISMServer.UpdateStockTakeData(zItemUID, LocationID, zStockCode, m_ISMLoginInfo.UserID, TaskID); 
             
            MoveNextRecord();
            txtStatusMsg.Text = "Stock Quantity verified for the Item UID " + txtItemUID.Text + " and Stock Code " + txtStockCode.Text;   

          }
          if (m_bStockComplate)
          {
            zJnlDesc = String.Format("Stocktake Task has been completed for the Location UID {0}", txtLocationUID.Text);
            
            m_ISMLoginInfo.AddToJournal("T", zJnlDesc, "STK350", LocationID.ToString(), "0", "0", "", 0);
             
            if (StockDlgResult != DialogResult.Ignore && StockDlgResult != DialogResult.Retry)  
                StockDlgResult = DialogResult.Yes;  
            StockTakeResult = zJnlDesc;  
             
            Close();
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Count", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        if (!m_bStockComplate)
        {
             
            DialogResult zReply = MessageBox.Show(String.Format("Stocktake Task is not completed for Location UID {0}. Do you want to close?", txtLocationUID.Text), "Stock Count", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (zReply == DialogResult.Yes)
            {
                 
                string zJnlDesc;
                StockDlgResult = DialogResult.Cancel;  
                StockTakeResult = "Stocktake Task is not completed for Location UID " + txtLocationUID.Text;
                zJnlDesc = String.Format("Stocktake is not completed for Location UID {0} ", txtLocationUID.Text);
                 
                m_ISMLoginInfo.AddToJournal("E", zJnlDesc, "STK358", LocationID.ToString(), "0", "0", "", 0);
                Close();
            }
        }
        else
        {
             
            if (m_nRecordCount > 0)
                StockTakeResult = "Stocktake Task has been completed for the Location UID " + txtLocationUID.Text;
            else
                StockTakeResult = "Location UID " + m_ISMLoginInfo.Params.LocPrefix + LocationID.ToString().PadLeft(12, '0') + " does not contain any stock Item";  
             
             
            if (StockDlgResult != DialogResult.Ignore && StockDlgResult != DialogResult.Retry)  
                StockDlgResult = DialogResult.Yes;  
            
            Close();  
        }
    }
    private bool ValidateData()
    {
      bool zValidStockData = true;
      try
      {
        dxErrorProvider.SetError(txtSerialNo, null);
        dxErrorProvider.SetError(txtQuanity, null);

        if (txtDBTrackInd.Text == "S" || txtDBTrackInd.Text == "E")
        {
          if (txtSerialNo.Text.Trim() == "")
          {
            dxErrorProvider.SetError(txtSerialNo, "Enter Serial No");
            txtSerialNo.Focus();
            zValidStockData = false;
          }
        }
        if (txtQuanity.Text.Trim() == "")
        {
          dxErrorProvider.SetError(txtQuanity, "Enter Stock Quantity");
          txtQuanity.SelectAll();
          txtQuanity.Focus();
          zValidStockData = false;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Count", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      return zValidStockData;

    }
    void MoveNextRecord()
    {
      try
      {
        txtStatusMsg.Text = "";   
        txtQuanity.EditValue = null;
        txtSerialNo.EditValue = null;
        dxErrorProvider.SetError(txtSerialNo, null);
        dxErrorProvider.SetError(txtQuanity, null);
        dataNavigator.Buttons.DoClick(dataNavigator.Buttons.Next);
        if (txtDBTrackInd.Text.Trim() == "S" || txtDBTrackInd.Text.Trim() == "E")
        {
          txtSerialNo.Enabled = true;
          txtSerialNo.Focus();
        }
        else
        {
          txtSerialNo.Enabled = false;
          txtQuanity.Focus();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Count", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
     
    private void btnException_Click(object sender, EventArgs e)
    {
        try
        {
             
            string zExpMsg;
            long zItemUID = 0;
            if(txtItemUID.Text.Trim() != "")
                zItemUID = long.Parse(txtItemUID.Text.Substring(1));
            
             
            FrmException frmSTException = new FrmException(m_ISMLoginInfo) { LocationUID = LocationID, JournalType = "STK", MsgBoxCaption = "Stocktake", ExceptionType = (int)ISM.ExceptionType.StockTake, ExceptionLevel = 2, ItemUID = zItemUID };

            frmSTException.Text = "Stocktake Exception";
            frmSTException.ShowDialog();
            bool zItemFoundException = false;
            if (frmSTException.DlgResult == DialogResult.No)
            {
                if ((dataNavigator.Position + 1) == m_nRecordCount || m_nRecordCount == 0)
                {
                    string zJnlDesc = String.Format("Stocktake Task has been completed for the Location UID {0}", m_ISMLoginInfo.Params.LocPrefix + LocationID.ToString().PadLeft(12, '0'));
                     
                    if (m_nRecordCount > 0)
                        StockTakeResult = "Stocktake Task has been completed for the Location UID " + txtLocationUID.Text;
                    else
                        StockTakeResult = "Location UID " + LocationID.ToString().PadLeft(12, '0') + " does not contain any stock Item";
                     
                    m_ISMLoginInfo.AddToJournal("T", zJnlDesc, "STK350", LocationID.ToString(), "0", "0", "", 0);
                     
                    if (StockDlgResult != DialogResult.Ignore && StockDlgResult != DialogResult.Retry)  
                        StockDlgResult = DialogResult.Yes;  

                    if (frmSTException.DlgResult == DialogResult.No)  
                    {
                        zItemFoundException = true;
                        StockDlgResult = DialogResult.Retry; ;
                        StockTakeResult = "Stocktake Exception Error";
                    }

                    if (m_nRecordCount != 0)
                        txtStatusMsg.Text = zJnlDesc;  
                     
                     
                     
                    if(!zItemFoundException)  
                        Close();

                }
                else  
                {
                    if (frmSTException.DlgResult == DialogResult.No)  
                    {
                        StockDlgResult = DialogResult.Retry; ;
                        StockTakeResult = "Stocktake Exception Error";
                    }
                }
                zExpMsg = "Exception raised for the Item UID " + txtItemUID.Text + " and Stock Code " + txtStockCode.Text;   
                 

                 
                 
                 
                 
                if (frmSTException.DlgResult == DialogResult.Retry)  
                    MoveNextRecord();
                 
                 
                 
                 
                txtStatusMsg.Text = zExpMsg;
            }
            else if (frmSTException.DlgResult == DialogResult.Cancel || frmSTException.DlgResult == DialogResult.Retry)
            {
                if ((dataNavigator.Position + 1) == m_nRecordCount || m_nRecordCount == 0)
                {
                    if (frmSTException.DlgResult == DialogResult.Cancel)
                    {
                        string zJnlDesc = String.Format("Stocktake Task has been completed for the Location UID {0}", m_ISMLoginInfo.Params.LocPrefix + LocationID.ToString().PadLeft(12, '0'));
                         
                        if (m_nRecordCount > 0)
                            StockTakeResult = "Stocktake Task has been completed for the Location UID " + txtLocationUID.Text;
                        else
                            StockTakeResult = "Location UID " + LocationID.ToString().PadLeft(12, '0') + " does not contain any stock Item";
                         
                        m_ISMLoginInfo.AddToJournal("T", zJnlDesc, "STK350", LocationID.ToString(), "0", "0", "", 0);
                        StockDlgResult = DialogResult.Retry; ;
                        StockTakeResult = "Stocktake Exception Error";

                        if (m_nRecordCount != 0)
                            txtStatusMsg.Text = zJnlDesc;  
                        Close();
                    }
                    else
                    {
                        StockDlgResult = DialogResult.Retry; ;
                        StockTakeResult = "Stocktake Exception Error";
                    }
                }
                else
                {
                    StockDlgResult = DialogResult.Retry; ;
                    StockTakeResult = "Stocktake Exception Error";
                }
                if (frmSTException.DlgResult == DialogResult.Cancel)
                {
                    MoveNextRecord();
                }
                if (frmSTException.DlgResult == DialogResult.Retry)  
                {
                    if ((dataNavigator.Position + 1) == m_nRecordCount)
                        Close();  
                    else
                        MoveNextRecord();
                }

            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Count", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
     
  }
}