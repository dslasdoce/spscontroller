#region "History"

 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
#endregion

#region "Namespace"
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using ISM.Forms;
using ISMDAL.TableColumnName;
using DevExpress.XtraTreeList;
#endregion

namespace ISM.Forms
{

  public partial class FrmStockTakeLocTree : DevExpress.XtraEditors.XtraForm
  {
    #region "Private Variabl"
    private string m_LocationUID = string.Empty;
    private long m_LocationID = 0;
    private ISMLoginInfo m_ISMLoginInfo;
    private DataSet dsTreeList = null;
    private string m_LocFlag = "LOCFLAG";
     
    private enum StockTakeStatus : int { Default = 0, ParentLocOpen = 1, Complited = 2, Error = 3, ExpError = 4, SealExpError = 5, CompleteErr = 6};
    private string m_StockTakeResult;  
    private long m_TaskID = 0;
    private string m_MILISStock_TakeNo;  
    private string m_MILISPlanID;  
    private bool m_IsException = false;
#endregion


    #region "Property "
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
        get { return m_StockTakeResult; }
        set { m_StockTakeResult = value; }
    }

    public string MILISStockTakeNo  
    {
        get { return m_MILISStock_TakeNo; }
        set { m_MILISStock_TakeNo = value; }
    }
    public string MILISPlanID  
    {
        get { return m_MILISPlanID; }
        set { m_MILISPlanID = value; }
    }   
    #endregion

    public FrmStockTakeLocTree(ISMLoginInfo AISMLoginInfo)
    {
      InitializeComponent();
      m_ISMLoginInfo = AISMLoginInfo;
    }



    private void FrmStockTakeLocTree_Load(object sender, EventArgs e)
    {
      try
      {
        dsTreeList = m_ISMLoginInfo.ISMServer.GetLocationTree(LocationID);
        tvLocation.KeyFieldName = ISMLocationRelationship.ChildID;
        tvLocation.ParentFieldName = ISMLocationRelationship.ParentID;
        tvLocation.DataSource = dsTreeList.Tables[0].DefaultView;
        TreeListNode Trnode = tvLocation.FindNodeByFieldValue("LocationUID", LocationUID);
        tvLocation.SetFocusedNode(Trnode);
        tvLocation.ExpandAll();
        tvLocation.SelectImageList = imgSmallImageCollection;
        tvLocation.GetSelectImage += new GetSelectImageEventHandler(OnTreeListGetSelectImage);
        CreateTaskTableRec();
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
         
        string zLocationUID = tvLocation.FocusedNode["LocationUID"].ToString();
        long zLocationId = 0;
        bool zLocationUIDFound = false;

        if (dsTreeList == null)
          return;
        foreach (DataRow carDataRow in dsTreeList.Tables[0].Rows)
        {
           
            
           if (zLocationUID == carDataRow["LocationUID"].ToString() && (carDataRow[m_LocFlag].ToString() != Convert.ToString((int)StockTakeStatus.Default)&& 
               carDataRow[m_LocFlag].ToString() != Convert.ToString((int)StockTakeStatus.Complited) &&
               carDataRow[m_LocFlag].ToString() != Convert.ToString((int)StockTakeStatus.ExpError)&&
               carDataRow[m_LocFlag].ToString() != Convert.ToString((int)StockTakeStatus.SealExpError)))
          {
            zLocationId = long.Parse(tvLocation.FocusedNode[ISMLocation.LocationUID].ToString());
            zLocationUIDFound = true;
            break;
          }
        }
        if (zLocationUIDFound)
          StockTakeSealConfirm(zLocationUID, zLocationId);
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    void StockTakeSealConfirm(string ALocationUID, long ALocationID)
    {
      try
      {
         
        using (FrmStockTakeSealConfirm m_zFrmStockTakeSealConfirm = new FrmStockTakeSealConfirm(m_ISMLoginInfo) { LocationID = ALocationID, LocationUID = ALocationUID, TaskID = m_TaskID.ToString() })
        {
          int zResult = 0;
          m_zFrmStockTakeSealConfirm.ShowDialog();
          if (m_zFrmStockTakeSealConfirm.m_DialogResult == DialogResult.Yes)
          {
            zResult = 0;
            UpdateChildParentSealStatus(ALocationUID, zResult);  
          }
          else if (m_zFrmStockTakeSealConfirm.m_DialogResult == DialogResult.No)
          {
            zResult = 1;
            UpdateChildParentSealStatus(ALocationUID, zResult);  
          }
          else if (m_zFrmStockTakeSealConfirm.m_DialogResult == DialogResult.Cancel)
          {
            zResult = 2;
            UpdateChildParentSealStatus(ALocationUID, zResult);  
          }
          else if (m_zFrmStockTakeSealConfirm.m_DialogResult == DialogResult.Ignore)  
          {
              zResult = 3;
              UpdateChildParentSealStatus(ALocationUID, zResult);  
          }
          else if (m_zFrmStockTakeSealConfirm.m_DialogResult == DialogResult.Retry)  
          {
              zResult = 4;
              UpdateChildParentSealStatus(ALocationUID, zResult);  
          }

          Refresh();  
          txtStatusMsg.Text = m_zFrmStockTakeSealConfirm.StockTakeResult;  
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    void UpdateChildParentSealStatus(string zLocationUID, int AStatus)
    {
      try
      {
        bool zFound = false;
        int nCount = 0;
         
        string zChildID;
        string zExpr;
        DataRow[] foundRows;
        DataTable ParentNodeTable;
        string zParentNodeID = tvLocation.FocusedNode[ISMLocationRelationship.ParentID].ToString();
        string zParentNodeUID = tvLocation.FocusedNode["LocationUID"].ToString();
        ParentNodeTable = dsTreeList.Tables[0];
         
        zExpr = String.Format("LocationUID = '{0}'", zLocationUID);

         
        foundRows = ParentNodeTable.Select(zExpr);
         
         
        zChildID = null;
        for (int nIndex = 0; nIndex <= foundRows.GetUpperBound(0); nIndex++)
        {
            zChildID = foundRows[nIndex][0].ToString();
            break;
        }
         
        if (AStatus == 0 || AStatus == 1 )  
        { 
           
           
           
           
           
           
           
           
           
           

           
           
           
           
           
           
           
           
           
           
           
          foreach (DataRow carDataRow in dsTreeList.Tables[0].Rows)
          {
            
             
            if (carDataRow[ISMLocationRelationship.ParentID].ToString() == zParentNodeID && carDataRow["LocationUID"].ToString() == zParentNodeUID)
            {
                 
                dsTreeList.Tables[0].Rows[nCount].BeginEdit();
                carDataRow[m_LocFlag] = (int)StockTakeStatus.Complited;
                dsTreeList.Tables[0].Rows[nCount].EndEdit();
            }
            if (AStatus == 0)
            {
                if (carDataRow[ISMLocationRelationship.ParentID].ToString() == zChildID)
                {
                    string zLocUID;  
                    dsTreeList.Tables[0].Rows[nCount].BeginEdit();
                    carDataRow[m_LocFlag] = (int)StockTakeStatus.Complited;
                    dsTreeList.Tables[0].Rows[nCount].EndEdit();
                    zLocUID = carDataRow["LocationUID"].ToString();  
                     
                    DisableChildSubNode(1, zLocUID);
                }
            }
            else if (AStatus == 1)
            {
                if (carDataRow[ISMLocationRelationship.ParentID].ToString() == zChildID)
                {
                  dsTreeList.Tables[0].Rows[nCount].BeginEdit();
                  carDataRow[m_LocFlag] = (int)StockTakeStatus.ParentLocOpen;
                  dsTreeList.Tables[0].Rows[nCount].EndEdit();
                }
            }
            nCount += 1;
          }
          dsTreeList.Tables[0].AcceptChanges();
        }
        else if ( AStatus == 2)
        {
            foreach (DataRow carDataRow in dsTreeList.Tables[0].Rows)
            {
                if (carDataRow["LocationUID"].ToString() == zLocationUID)
                {
                    dsTreeList.Tables[0].Rows[nCount].BeginEdit();
                    carDataRow[m_LocFlag] = (int)StockTakeStatus.Error;
                    dsTreeList.Tables[0].Rows[nCount].EndEdit();
                }
            }
            nCount += 1;
        }
        else if (AStatus == 3)  
        {
            foreach (DataRow carDataRow in dsTreeList.Tables[0].Rows)
            {
                if (carDataRow["LocationUID"].ToString() == zLocationUID)
                {
                    dsTreeList.Tables[0].Rows[nCount].BeginEdit();
                    carDataRow[m_LocFlag] = (int)StockTakeStatus.SealExpError;
                    dsTreeList.Tables[0].Rows[nCount].EndEdit();
                }
                if (carDataRow[ISMLocationRelationship.ParentID].ToString() == zChildID)
                {
                    dsTreeList.Tables[0].Rows[nCount].BeginEdit();
                    carDataRow[m_LocFlag] = (int)StockTakeStatus.ParentLocOpen;
                    dsTreeList.Tables[0].Rows[nCount].EndEdit();
                }
            }
            nCount += 1;
        }
        else if (AStatus == 4)  
        {
            foreach (DataRow carDataRow in dsTreeList.Tables[0].Rows)
            {
                if (carDataRow["LocationUID"].ToString() == zLocationUID)
                {
                    dsTreeList.Tables[0].Rows[nCount].BeginEdit();
                    carDataRow[m_LocFlag] = (int)StockTakeStatus.CompleteErr;
                    dsTreeList.Tables[0].Rows[nCount].EndEdit();
                }
                if (carDataRow[ISMLocationRelationship.ParentID].ToString() == zChildID)
                {
                    dsTreeList.Tables[0].Rows[nCount].BeginEdit();
                    carDataRow[m_LocFlag] = (int)StockTakeStatus.ParentLocOpen;
                    dsTreeList.Tables[0].Rows[nCount].EndEdit();
                }
            }
            nCount += 1;
        }

         
         
         
         
           
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
        CheckVerificationCompleted();  

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void btnException_Click(object sender, EventArgs e)
    {
      try
      {
          long zLocationId = 0;
          if (IsLocationVerified(ref zLocationId))
          {
               
              LocationID = long.Parse(tvLocation.FocusedNode[ISMLocation.LocationUID].ToString());
               
              FrmException frmSTException = new FrmException(m_ISMLoginInfo) { LocationUID = LocationID, JournalType = "STK", MsgBoxCaption = "Stocktake", ExceptionType = (int)ISM.ExceptionType.StockTake, ExceptionLevel = 0 };
              frmSTException.Text = "Stocktake Exception";
              frmSTException.ShowDialog();
              if (frmSTException.DlgResult == DialogResult.No)  
              {
                   
                  ExceptionChildNode();
                  StockTakeResult = "Location verification has been completed with error for the Location UID " + LocationUID;  
                  UpdateTaskTableRec("Completed Error", "3");
              }
              if(frmSTException.SaveResult)  
                txtStatusMsg.Text = "Exception raised for Location UID " + tvLocation.FocusedNode["LocationUID"].ToString();

               
              if (frmSTException.DlgResult == DialogResult.No || frmSTException.DlgResult == DialogResult.Retry)
              {
                  m_IsException = true;
              }
          }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
     
    private void ExceptionChildNode()
    {
        try
        {
            int nCount = 0;
            string zChildID;
            string zExpr;
            DataRow[] foundRows;
            DataTable ParentNodeTable;
            string zParentNodeID = tvLocation.FocusedNode[ISMLocationRelationship.ParentID].ToString();
            string zParentNodeUID = tvLocation.FocusedNode["LocationUID"].ToString();
            string zLocationUID;
            ParentNodeTable = dsTreeList.Tables[0];
             
            zExpr = String.Format("LocationUID = '{0}'", zParentNodeUID);

             
            foundRows = ParentNodeTable.Select(zExpr);
             
             
            zChildID = null;
            for (int nIndex = 0; nIndex <= foundRows.GetUpperBound(0); nIndex++)
            {
                zChildID = foundRows[nIndex][0].ToString();
                break;
            }
            foreach (DataRow carDataRow in dsTreeList.Tables[0].Rows)
            {

                if (carDataRow[ISMLocationRelationship.ParentID].ToString() == zParentNodeID && carDataRow["LocationUID"].ToString() == zParentNodeUID)
                {
                    dsTreeList.Tables[0].Rows[nCount].BeginEdit();
                    carDataRow[m_LocFlag] = (int)StockTakeStatus.ExpError;
                    dsTreeList.Tables[0].Rows[nCount].EndEdit();
                }
                if (carDataRow[ISMLocationRelationship.ParentID].ToString() == zChildID)
                {
                    dsTreeList.Tables[0].Rows[nCount].BeginEdit();
                    carDataRow[m_LocFlag] = (int)StockTakeStatus.ExpError;
                    dsTreeList.Tables[0].Rows[nCount].EndEdit();
                    zLocationUID = carDataRow["LocationUID"].ToString();
                     
                    DisableChildSubNode(2, zLocationUID);
                }
                nCount += 1;
            }
            dsTreeList.Tables[0].AcceptChanges();
             
            CheckVerificationCompleted(); 
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
     
    private void DisableChildSubNode(int AMode, string ALocationUID)
    {
        try
        {
            int nCount = 0;
            int mCount = 0;  
            string zChildID;
            string zExpr;
            DataRow[] foundRows;
            DataTable ParentNodeTable;
            string zParentNodeID = Convert.ToString(long.Parse(ALocationUID.Substring(1,12)));
            string zParentNodeUID = ALocationUID;
            string zLocationUID = "";
            ParentNodeTable = dsTreeList.Tables[0];
             
            zExpr = String.Format("LocationUID = '{0}'", zParentNodeUID);

             
            foundRows = ParentNodeTable.Select(zExpr);
             
             
            zChildID = null;
            for (int nIndex = 0; nIndex <= foundRows.GetUpperBound(0); nIndex++)
            {
                zChildID = foundRows[nIndex][0].ToString();
                break;
            }
            foreach (DataRow carDataRow in dsTreeList.Tables[0].Rows)
            {
                if (carDataRow[ISMLocationRelationship.ParentID].ToString() == zChildID)
                {
                    dsTreeList.Tables[0].Rows[mCount].BeginEdit();
                    if (AMode == 1)
                        carDataRow[m_LocFlag] = (int)StockTakeStatus.Complited;
                    if (AMode == 2)
                        carDataRow[m_LocFlag] = (int)StockTakeStatus.ExpError;
                    dsTreeList.Tables[0].Rows[mCount].EndEdit();
                    dsTreeList.Tables[0].AcceptChanges();
                    zLocationUID = carDataRow["LocationUID"].ToString();  
                    DisableChildSubNode(AMode, zLocationUID);
                }
                nCount += 1;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
     
    private void OnTreeListGetSelectImage(object sender, GetSelectImageEventArgs e)
    {
      if (dsTreeList == null)
        return;
       
      foreach (DataRow carDataRow in dsTreeList.Tables[0].Rows)
      {
        if (e.Node.GetValue("LocationUID").ToString() == carDataRow["LocationUID"].ToString())
        {
            switch (carDataRow[m_LocFlag].ToString())
            {
                case "0":  
                    e.NodeImageIndex = 4;  
                    break;
                case "1":
                    e.NodeImageIndex = 5;  
                    break;
                case "2":
                    e.NodeImageIndex = 6;  
                    break;
                case "3":
                    e.NodeImageIndex = 0;  
                    break;
                case "4":
                    e.NodeImageIndex = 7;  
                    break;
                case "5":
                    e.NodeImageIndex = 8;  
                    break;
                case "6":
                    e.NodeImageIndex = 9;  
                    break;
            }
           
           
           
           
           
           
           
           
           
           
        }
      }
    }
    private void btnCancel_Click(object sender, EventArgs e)
    {
      try
      {
        bool zFound = false;
        foreach (DataRow carDataRow in dsTreeList.Tables[0].Rows)
        {
          if (carDataRow[m_LocFlag].ToString() != Convert.ToString((int)StockTakeStatus.Complited)||
              carDataRow[m_LocFlag].ToString() != Convert.ToString((int)StockTakeStatus.ExpError))
          {
            zFound = true;
            break;
          }
        }
        if (zFound)
        {
          DialogResult zReply = MessageBox.Show("Location Verification is not complete. Do you want to close? ", "Stocktake", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
          if (zReply == DialogResult.No)
              return;
          else
          {
              StockTakeResult = "Location verification is not complete for the Location UID " + LocationUID;  
              UpdateTaskTableRec("Cancelled", "5");  
          }
        }
        Close();

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private bool IsLocationVerified(ref long zLocationId)
    {
        bool zLocationUIDFound = false;
        try
        {
            if (dsTreeList == null)
                return zLocationUIDFound;

            string zLocationUID = tvLocation.FocusedNode["LocationUID"].ToString();
            foreach (DataRow carDataRow in dsTreeList.Tables[0].Rows)
            {
                if (zLocationUID == carDataRow["LocationUID"].ToString() && (carDataRow[m_LocFlag].ToString() == Convert.ToString((int)StockTakeStatus.ParentLocOpen) || 
                    carDataRow[m_LocFlag].ToString() == Convert.ToString((int)StockTakeStatus.Error)||
                    carDataRow[m_LocFlag].ToString() == Convert.ToString((int)StockTakeStatus.ExpError)))
                {
                    zLocationId = long.Parse(tvLocation.FocusedNode[ISMLocation.LocationUID].ToString());
                    zLocationUIDFound = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        return zLocationUIDFound;
    }

    private void tvLocation_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
    {
        try
        {
            if (!tvLocation.Focused)
                return;
            txtStatusMsg.Text = "";  
             
            string zLocationUID = tvLocation.FocusedNode["LocationUID"].ToString();
            bool zLocationUIDFound = false;

            if (dsTreeList == null)
                return;
            foreach (DataRow carDataRow in dsTreeList.Tables[0].Rows)
            {
                if (zLocationUID == carDataRow["LocationUID"].ToString() &&
                    (carDataRow[m_LocFlag].ToString() != Convert.ToString((int)StockTakeStatus.Default) ||
                     carDataRow[m_LocFlag].ToString() != Convert.ToString((int)StockTakeStatus.ParentLocOpen) ||
                     carDataRow[m_LocFlag].ToString() != Convert.ToString((int)StockTakeStatus.ExpError) ||
                     carDataRow[m_LocFlag].ToString() != Convert.ToString((int)StockTakeStatus.Error))||
                     carDataRow[m_LocFlag].ToString() != Convert.ToString((int)StockTakeStatus.CompleteErr))  
                {
                    zLocationUIDFound = true;
                    break;
                }
            }
            if (zLocationUIDFound)
            {
                btnConfirm.Enabled = true;
                btnException.Enabled = true;
            }
            else
            {
                btnConfirm.Enabled = false;
                btnException.Enabled = false;
            }


        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
     
    private void CreateTaskTableRec()
    {
        try
        {
            int zMode = 1;
            ISMTask.StructTask zTask = new ISMTask.StructTask();
            zTask.Type = "3";
            zTask.Status = "In Progress";
            zTask.StockQty = "0";
            zTask.UserID = m_ISMLoginInfo.UserID.ToString();
            zTask.ItemID = "0";
            zTask.SourceID = m_LocationID.ToString();
            zTask.DestinationID = "0";
            zTask.OperationID = "25";  
            zTask.StatusCode = "2"; 
            zTask.CreateUserID = m_ISMLoginInfo.LogonID;
            zTask.CreateDateTime = DateTime.Now.ToString();
            zTask.StockCode = "";
            zTask.MILISStockTakeNo = MILISStockTakeNo;  
            m_ISMLoginInfo.ISMServer.CreateStockTakeTask(zMode, zTask, ref m_TaskID, MILISPlanID);

        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Take", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    private void UpdateTaskTableRec(string AStatus, string AStatusCode)
    {
        try
        {
            m_ISMLoginInfo.ISMServer.UpdateStockTakeStatus(m_TaskID, AStatus, AStatusCode);
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    private void CheckVerificationCompleted()
    {
        try
        {
             
            bool zFound = true;
            bool zCompleteError = false;
            foreach (DataRow carDataRow in dsTreeList.Tables[0].Rows)
            {

                if (carDataRow[m_LocFlag].ToString() == Convert.ToString((int)StockTakeStatus.Default) ||
                    carDataRow[m_LocFlag].ToString() == Convert.ToString((int)StockTakeStatus.Error) ||
                    carDataRow[m_LocFlag].ToString() == Convert.ToString((int)StockTakeStatus.ParentLocOpen))
                {
                    zFound = false;
                    break;
                }
                if (carDataRow[m_LocFlag].ToString() == Convert.ToString((int)StockTakeStatus.ExpError) ||
                     
                     
                    carDataRow[m_LocFlag].ToString() == Convert.ToString((int)StockTakeStatus.CompleteErr))  
                    zCompleteError = true;
            }
            if (zFound)
            {
                if (m_IsException)  
                {
                    StockTakeResult = "Location verification has been completed with error for the Location UID " + LocationUID;  
                    UpdateTaskTableRec("Completed Error", "3");

                }
                else if (zCompleteError)
                {
                    StockTakeResult = "Location verification has been completed with error for the Location UID " + LocationUID;  
                    UpdateTaskTableRec("Completed Error", "3");
                }
                else
                {
                    StockTakeResult = "Location verification has been completed for the Location UID " + LocationUID;
                    UpdateTaskTableRec("Completed", "4");
                }
                Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

     
  }

}