 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraTreeList;         
using DevExpress.XtraTreeList.Nodes;   
using ISMDAL.TableColumnName;
using ISM.Class;

using System.Net;
using System.Net.NetworkInformation;

 
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;

using DevExpress.XtraGrid.Repository;

using SmartHumpyController; 
using System.Collections.Generic;

using DevExpress.XtraGrid.Menu;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraBars;



namespace ISM.Modules
{
  public partial class LocationTree : ISMBaseWorkSpace
  {
      private bool m_Refresh = false;
      private TreeListViewState m_TreeListViewState = null;
      private int m_NodeCount = 0;

      private Color m_Color_Green = Color.FromArgb(122, 201, 74);
      private Color m_Color_Red = Color.FromArgb(246, 104, 110);
      private Color m_Color_Yellow = Color.FromArgb(255, 157, 41);

      private HumpyDAL m_DAL_CheckedInWorker;
      private HumpyDAL m_DAL_Retriever;
      private HumpyDAL m_DAL_HQCheckOut;

      private DevExpress.XtraBars.BarManager barManager1;
      private PopupMenu menu;

       
      private string m_CurrSelectLocPortalID ="";
      private string m_CurrSelectLocPortalName ="";
      private string m_CurrSelectMasterHumpyIP ="";

      private DataRowView m_CurrSelectedWorkerRecord =null;

      #region "Property "
      public bool RefreshTreeControl
      {
          get { return m_Refresh; }
          set { m_Refresh = value; }
      }
      public TreeListViewState TreeListViewState
      {
          get { return m_TreeListViewState; }
          set { m_TreeListViewState = value; }
      }

      #endregion
      public LocationTree(ISMLoginInfo AISMLoginInfo)
      : base(AISMLoginInfo)
    {
      InitializeComponent();
    }

    private void LocationTree_Load(object sender, EventArgs e)
    {
      try
      {


           
          DataSet zDataSet = m_ISMLoginInfo.ISMServer.LocationGetRelationshipTree();   

           
           
          tvLocation.KeyFieldName = ISMLocationRelationship.ChildID;
          tvLocation.ParentFieldName = ISMLocationRelationship.ParentID;
          tvLocation.DataSource = zDataSet.Tables[0].DefaultView;   

          tvLocation.ExpandAll(); 

           
           

          gridView_CheckedIn.OptionsView.ColumnAutoWidth = false;
          gridView_CheckedIn.BestFitColumns(); 

           
          TreeListNodeLevel zTreeListNodeLevel = new TreeListNodeLevel();
          tvLocation.NodesIterator.DoOperation(zTreeListNodeLevel);
          m_NodeCount = zTreeListNodeLevel.MaxLevel;
          SetLookUpEditCaption();
          CreateMetaDataForLocationTreeNode();
          LoadFindLocationLookUpEdit();  
           
           
           
           
           
           
           

           
           
           
           
          ConfigCheckedInListGrid();
          LoadLocationMetaData_PortalData(); 

      }
      catch (Exception ex)
      {
        MessageBox.Show("Error: Loading Location Tree\n" + ex.Message + "\nPlease Contact System Administrator", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void LoadLocationMetaData_PortalData()
    {
        try
        {
            DataSet ds = m_ISMLoginInfo.ISMServer.GetLocationMetaData();
            if (ds != null)
            {
                 
                 
                 
                luPortalName.Properties.DataSource = ds.Tables[ISMPortal.TableName].DefaultView;  
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Load Location Meta Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }


    #region Retreive the Humpy Worker status Section
    private void UpdateTagList_Humpy(string zDestinationHumpyIP)
    {
        bool zFirstHumpyAccessAttempt = false;
        try
        {
            int zConnectionTimeout = SHSHQ.Properties.Settings.Default.HumpyViewerTimeout_s; 

            string zInstanceName = "";
            if (SHSHQ.Properties.Settings.Default.HumpyDBSQLInstanceName.Trim() == "")
            {
                zInstanceName = "";
            }
            else
                zInstanceName = "\\" + SHSHQ.Properties.Settings.Default.HumpyDBSQLInstanceName;

             

            string zConnectStr = "Data Source=" + zDestinationHumpyIP + zInstanceName
                    + ";Initial Catalog=" + SHSHQ.Properties.Settings.Default.HumpyDBName
                    + ";User ID=" + SHSHQ.Properties.Settings.Default.HumpyDBUserName
                    + ";Password=" + SHSHQ.Properties.Settings.Default.HumpyDBPassword + ""
                    +";Connection Timeout=" + SHSHQ.Properties.Settings.Default.HumpyViewerTimeout_s.ToString()+ ";";

            m_DAL_CheckedInWorker = new HumpyDAL(zConnectStr);

            #region GridControl2->Checked In List Data Retrieving
            try
            {
                DataSet ds = m_DAL_CheckedInWorker.SHS_HQGetWorkersAll(SHSHQ.Properties.Settings.Default.Humpy_TagReadSensitivity_ms,
                                                                       SHSHQ.Properties.Settings.Default.Humpy_TagINsensitivity_ms);

                zFirstHumpyAccessAttempt = true;

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        UpdateGridControl2_CheckedInWorkers(ds.Tables[0]);
                    }
                }
                else
                {
                    MessageBox.Show("HUMPY:"+zDestinationHumpyIP+" doesn't have worker information", "HUMPY VIEWER");
                }
            }

            catch (Exception ex)
            {

                ex.ToString();
                MessageBox.Show("HUMPY:" + zDestinationHumpyIP + " is not accessable.\r\nPlease check the humpy db connection and db login credential", "HUMPY VIEWER");
                 
            }

            #endregion

            #region GridControl1->Detail List Data Retrieving

             
             
             
             
             
             
             
             
             
             
             
             
             
             
             
             

             
             
             
            #endregion

        }
        catch (Exception ex)
        {
            ex.ToString();
             
        }
        finally
        {
        }
    }

     
     
     
     

     
     
     

     

     
     
     
     

     
     
     

     
     

     
     
     
     

     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     

     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     

     
     
     
     
     
     

     
     
     
     
     
     

    public void ConfigCheckedInListGrid()
    {
        try
        {
             
             
             

             
             
             
             
             

             
             
            gridView_CheckedIn.OptionsView.ShowIndicator = false;

             
            gridView_CheckedIn.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;

             
            gridView_CheckedIn.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView_CheckedIn.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridView_CheckedIn.OptionsSelection.EnableAppearanceHideSelection = false;

             
            ColumnView ColView = gridControl2.MainView as ColumnView;
            string[] zFieldNames = new string[] {SHS_DbReturnColumnNames.Column1,
                                                     SHS_DbReturnColumnNames.Column2,
                                                     SHS_DbReturnColumnNames.Column3,
                                                     SHS_DbReturnColumnNames.Column4,
                                                     SHS_DbReturnColumnNames.Column5,
                                                     SHS_DbReturnColumnNames.Column6,
                                                     SHS_DbReturnColumnNames.Column7,
                                                     SHS_DbReturnColumnNames.Column8};
            DevExpress.XtraGrid.Columns.GridColumn zColumn;
            ColView.Columns.Clear();
            for (int i = 0; i < zFieldNames.Length; i++)
            {
                zColumn = ColView.Columns.AddField(zFieldNames[i]);
                zColumn.VisibleIndex = i;
            }

            gridView_CheckedIn.Columns[0].Caption = "NAME"; 
             
            gridView_CheckedIn.Columns[0].Width = 500;
            gridView_CheckedIn.Columns[0].Visible = true;
            this.gridView_CheckedIn.Columns[0].SummaryItem.DisplayFormat = "(Total Checked In Worker= {0})";
            this.gridView_CheckedIn.Columns[0].SummaryItem.FieldName = "NAME";
            this.gridView_CheckedIn.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;

            gridView_CheckedIn.Columns[1].Caption = SHS_DbReturnColumnNames.Column2; 
            gridView_CheckedIn.Columns[1].Visible = false;
             
            gridView_CheckedIn.Columns[2].Caption = SHS_DbReturnColumnNames.Column3; 
            gridView_CheckedIn.Columns[2].Visible = false;

             
            gridView_CheckedIn.Columns[3].Caption = SHS_DbReturnColumnNames.Column4; 
            gridView_CheckedIn.Columns[3].Visible = false;

             
            gridView_CheckedIn.Columns[4].Caption = SHS_DbReturnColumnNames.Column5; 
            gridView_CheckedIn.Columns[4].Visible = false;

             
            gridView_CheckedIn.Columns[5].Caption = SHS_DbReturnColumnNames.Column6; 
            gridView_CheckedIn.Columns[5].Visible = false;

             
            gridView_CheckedIn.Columns[6].Caption = "Humpy ID"; 
            gridView_CheckedIn.Columns[6].Visible = true;
            gridView_CheckedIn.Columns[6].Width = 350;

            gridView_CheckedIn.Columns[7].Caption = "Status"; 
            gridView_CheckedIn.Columns[7].Visible = true;
            gridView_CheckedIn.Columns[7].Width = 350;


            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("IN", "IN", 7),  
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("OUT", "OUT", 6), 
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Not Checked In", "Not Checked In", 8)});
            this.repositoryItemImageComboBox1.LargeImages = this.imgSmallImageCollection;
            repositoryItemImageComboBox1.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;

            gridView_CheckedIn.Columns[7].ColumnEdit = repositoryItemImageComboBox1; 
        }
        catch (Exception Ex)
        {
            MessageBox.Show(String.Format("List Configuration: {0}", Ex.Message), "Checked-In List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    #region DATA GRID (gridView_CheckIn) controls
    private void UpdateGridControl2_CheckedInWorkers(DataTable dt)
    {
        this.gridControl2.BeginInvoke
        (
            new MethodInvoker
            (
                delegate
                {

                    gridControl2.BeginUpdate();
                    gridControl2.DataSource = null;
                    gridControl2.DataSource = dt.DefaultView;
                    gridControl2.EndUpdate();
                }
            )
        );
    }

    public void UpdateGridControl2_CheckedInWorkers_RESET() 
    {
        this.gridControl2.BeginInvoke
        (
            new MethodInvoker
            (
                delegate
                {

                    gridControl2.BeginUpdate();
                    gridControl2.DataSource = null;
                     
                    gridControl2.EndUpdate();
                }
            )
        );
    }

    #endregion

    #region DATA GRID (gridView1) controls
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     

    private void GetCurrenGridViewRowData(string ColumnFiledName, string Value)
    {
        int rowHandle = GetRowHandleByColumnValue(gridView1, ColumnFiledName, Value);
        if (rowHandle != GridControl.InvalidRowHandle)
        {
            object value = gridView1.GetRow(rowHandle);

             
             
             
        }
    }

    private bool SearchCurrentGridView(string ColumnFiledName, string Value)
    {
        bool zRet = false;
        int rowHandle = GetRowHandleByColumnValue(gridView1, ColumnFiledName, Value);
        if (rowHandle != GridControl.InvalidRowHandle)
        {
             
             
             
            zRet = true;
        }

        return zRet;
    }

    private int GetRowHandleByColumnValue(GridView view, string ColumnFieldName, object value)
    {
        int result = GridControl.InvalidRowHandle;
        for (int i = 0; i < view.RowCount; i++)
            if (view.GetDataRow(i)[ColumnFieldName].Equals(value))
                return i;
        return result;
    }

    private object GetRowColumnDataByRowHandle(GridView view, int aRowHandle, string ColumnFieldName)
    {
        object retValue = "";
        int result = GridControl.InvalidRowHandle;

        if (aRowHandle != result)
        {
            retValue = view.GetDataRow(aRowHandle)[ColumnFieldName];
        }

        return retValue;
    }

    private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
    {
        try
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string zINOUTStatus = View.GetRowCellDisplayText(e.RowHandle, View.Columns[SHS_DbReturnColumnNames.Column8]); 
                object zWorkerName = GetRowColumnDataByRowHandle(gridView1, e.RowHandle, SHS_DbReturnColumnNames.Column1); 

                if (zINOUTStatus == SHS_INOUT_Values.INValue)
                {
                    e.Appearance.BackColor = m_Color_Green; 
                     
                     
                }
                else if (zINOUTStatus == SHS_INOUT_Values.OUTValue)
                {
                    e.Appearance.BackColor = m_Color_Red; 
                     
                     
                }
                else if (zINOUTStatus == SHS_INOUT_Values.NotCheckedInValue)
                {
                     
                    e.Appearance.BackColor = m_Color_Yellow; 
                     
                     
                }

            }
             
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

     
    private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
    {
        e.Info.ImageIndex = -1;
    }

    private void gridView1_CustomColumnGroup(object sender, CustomColumnSortEventArgs e)
    {

    }

    private void gridView1_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
    {

    }
    #endregion

    private void HQ_CheckOutWorkerFromHumpy(string zDestinationHumpyIP,string HumpyWorkerMISCCard, string HumpyWorkerName, string PortalName)
    {
        try
        {
            long zOperatorID = m_ISMLoginInfo.UserID;

             

             
             
             
             

            int zConnectionTimeout = SHSHQ.Properties.Settings.Default.HumpyViewerTimeout_s; 

            string zInstanceName = "";
            if (SHSHQ.Properties.Settings.Default.HumpyDBSQLInstanceName.Trim() == "")
            {
                zInstanceName = "";
            }
            else
                zInstanceName = "\\" + SHSHQ.Properties.Settings.Default.HumpyDBSQLInstanceName;

            string zConnectStr = "Data Source=" + zDestinationHumpyIP + zInstanceName
                    + ";Initial Catalog=" + SHSHQ.Properties.Settings.Default.HumpyDBName
                    + ";User ID=" + SHSHQ.Properties.Settings.Default.HumpyDBUserName
                    + ";Password=" + SHSHQ.Properties.Settings.Default.HumpyDBPassword + ""
                    + ";Connection Timeout=" + SHSHQ.Properties.Settings.Default.HumpyViewerTimeout_s.ToString() + ";";

            m_DAL_HQCheckOut = new HumpyDAL(zConnectStr);
             
            if (m_DAL_HQCheckOut.SHS_Update_Personnel_CheckInOut(HumpyWorkerMISCCard, SHS_CheckInOutStatus.CheckOut))
            {
                 
                HQ_CheckOut_AddToJournal("T", 
                                        "HQ Operator Check Out Worker-" + HumpyWorkerName + " At:" + PortalName, 
                                        "HUM10",
                                        zOperatorID.ToString(),
                                        PortalName);
            }
            else
            { 
                
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show("Check Out User:" + ex.ToString(), "HQ_CheckOutWorkerFromHumpy");
        }

    }

     
    public bool HQ_CheckOut_AddToJournal(string AExpFlg, string AExcDesc, string AJnlCode, string AUserID, string APortalName)
    {
        bool zRet = false;
        try
        {
            ISMJournal.StructJouurnal zStructJournal = new ISMJournal.StructJouurnal();
            zStructJournal.ExceptionFlag = AExpFlg;
            zStructJournal.ExceptionDesc = AExcDesc;
            zStructJournal.JournalCode = AJnlCode;
            zStructJournal.LocationID = "0";
            zStructJournal.UserID = AUserID;
            zStructJournal.TaskID = "0";
            zStructJournal.SealID = "0";
            zStructJournal.StockCode = "";
            zStructJournal.PortalName = APortalName; 
            zStructJournal.ItemUID = "0";

            zRet = m_ISMLoginInfo.ISMServer.AddToJournalTable(zStructJournal);
        }
        catch (Exception ex)
        {
            ex.ToString();
            zRet = false;
        }
        return zRet;
    }

    private int HQ_GetHumpyIPs(int aPortalID, ref List<HumpyTablet> aHumpyTabletList)
    {
        aHumpyTabletList = new List<HumpyTablet>();
        int zHumpyCount = 0;
         
         
         
        try
        {
            DataSet ds = m_ISMLoginInfo.ISMServer.GetPortalORReaderData(1, aPortalID, "");
            if (ds != null)
            {
                 
                string zHumpy1IP = "";
                string zHumpy2IP = "";

                zHumpyCount = ds.Tables[0].Rows.Count;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        DataRow dr1 = ds.Tables[0].Rows[0];  
                        bool zMasterSlave = false;
                       
                        if (dr1[ISMReaders.PowerStatus].ToString() == "1")
                            zMasterSlave = true;
                        else
                            zMasterSlave = false;

                        aHumpyTabletList.Add(new HumpyTablet( dr1[ISMReaders.IPAddress].ToString(), zMasterSlave));            
                    }
                    else if (ds.Tables[0].Rows.Count == 2)
                    {


                        DataRow dr1 = ds.Tables[0].Rows[0];  
                        zHumpy1IP = dr1[ISMReaders.IPAddress].ToString();
                          bool zMasterSlave1 = false;
                        if (dr1[ISMReaders.PowerStatus].ToString() == "1")
                            zMasterSlave1 = true;
                        else
                            zMasterSlave1 = false;
                        aHumpyTabletList.Add(new HumpyTablet( dr1[ISMReaders.IPAddress].ToString(),  zMasterSlave1));

                        DataRow dr2 = ds.Tables[0].Rows[1];  
                        zHumpy2IP = dr2[ISMReaders.IPAddress].ToString();
                           bool zMasterSlave2= false;
                        if (dr2[ISMReaders.PowerStatus].ToString() == "1")
                            zMasterSlave2 = true;
                        else
                            zMasterSlave2 = false;
                        aHumpyTabletList.Add(new HumpyTablet( dr1[ISMReaders.IPAddress].ToString(),  zMasterSlave2));
                    }
                }
                else
                {


                }

            }
            else
            {
                 
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show("HQ_GetHumpyIPs Error:"+ex.ToString());
        }
        finally { }
        return zHumpyCount;
        
    }

    #endregion

    public void RefreshTreeControlView()  
    {
        try
        {
            DataSet zDataSet = m_ISMLoginInfo.ISMServer.LocationGetRelationshipTree();  
            tvLocation.KeyFieldName = ISMLocationRelationship.ChildID;
            tvLocation.ParentFieldName = ISMLocationRelationship.ParentID;
            tvLocation.DataSource = zDataSet.Tables[0].DefaultView;  

            object zParentId = tvLocation.FocusedNode[tvLocation.KeyFieldName];  
             
             
             

             
             

            gridView_CheckedIn.OptionsView.ColumnAutoWidth = false;
            gridView_CheckedIn.BestFitColumns();

            TreeListViewState.LoadState();
             
            tvLocation.Columns[0].Width += tvLocation.ViewInfo.RC.LevelWidth;  
            tvLocation.Columns[1].Width += tvLocation.ViewInfo.RC.LevelWidth;  
            tvLocation.Columns[2].Width += tvLocation.ViewInfo.RC.LevelWidth;  


        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: Refresh Tree Control View\n" + ex.Message + "\nPlease Contact System Administrator", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }

     
     
     
     
     
    private void tvLocation_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
    {
      if ((e.Column.FieldName == "SealUID")  && (e.CellText == "5000000000000"))  
        e.CellText = "";   
      /* Out DM 16-SEP-10
      TreeList zTreeList = sender as TreeList;

      if (e.Node == zTreeList.FocusedNode)
      {
        e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
        Rectangle zRect = new Rectangle(e.EditViewInfo.ContentRect.Left,
                                        e.EditViewInfo.ContentRect.Top,
                                        Convert.ToInt32(e.Graphics.MeasureString(e.CellText, tvLocation.Font).Width + 1),
                                        Convert.ToInt32(e.Graphics.MeasureString(e.CellText, tvLocation.Font).Height));
        e.Graphics.FillRectangle(SystemBrushes.Highlight, zRect);

        if (e.Column.FieldName == "SealUID")   
        {
          if (e.CellText != "5000000000000")
            e.Graphics.DrawString(e.CellText, tvLocation.Font, SystemBrushes.HighlightText, zRect);
          
        }
        else
        {
          e.Graphics.DrawString(e.CellText, tvLocation.Font, SystemBrushes.HighlightText, zRect);
        }
        e.Handled = true;
      }
      else if (e.Column.FieldName == "SealUID")   
      {
        if (e.CellText == "5000000000000")
          e.Handled = true;
      }
       */
    }

     
     
    private void tvLocation_DragDrop(object sender, DragEventArgs e)
    {
      TreeList zTList = sender as TreeList;
      TreeListNode zDragNode;
      TreeListNode zTargetNode;
      Point zPoint = zTList.PointToClient(new Point(e.X, e.Y));

      object zOldParentId = tvLocation.FocusedNode[tvLocation.KeyFieldName];   
      zTargetNode = zTList.CalcHitInfo(zPoint).Node;   
       
      object zNewParentId = zTargetNode.FirstNode[tvLocation.KeyFieldName];   
      zNewParentId = zTargetNode.FirstNode[tvLocation.ParentFieldName];       

      if (m_ISMLoginInfo.ISMServer.UpdateLocationRelationshipTree(Convert.ToUInt32(zOldParentId), Convert.ToUInt32(zNewParentId)) == true)
      {
        zDragNode = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
        zTList.SetNodeIndex(zDragNode, zTList.GetNodeIndex(zTargetNode));
      }
    }
     

    private void tvLocation_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)  
    {
      try
      {
          #region Return Stock --Disabled
           
           
           
           
           
          #endregion

        m_CurrSelectLocPortalID ="-1";
        m_CurrSelectLocPortalName ="";
        m_CurrSelectMasterHumpyIP ="";

        object zFocusedLocID = tvLocation.FocusedNode[tvLocation.KeyFieldName]; ;

        if (zFocusedLocID == null)
            return;

        DataSet zLocationDS = m_ISMLoginInfo.ISMServer.LocationGetRecd_SHS(long.Parse(zFocusedLocID.ToString()));
        int zPortalID = -1;
        string zLocName ="";
        string zLocUID = "";
        List<HumpyTablet> zHumpyTabletList = new List<HumpyTablet>();

        try
        {
            if (zLocationDS.Tables[ISMLocation.TableName].Rows.Count > 0)
            {
                DataRow zCurrentDR = zLocationDS.Tables[ISMLocation.TableName].Rows[0];  
                zPortalID = Convert.ToInt32(zCurrentDR[ISMLocation.PortalID]);
                zLocName = zCurrentDR[ISMLocation.UserDesc].ToString();
                zLocUID = zCurrentDR[ISMLocation.LocationUID].ToString();

                m_CurrSelectLocPortalID =  zPortalID.ToString();

                try
                {
                    luPortalName.EditValue = Convert.ToInt32(zCurrentDR[ISMLocation.PortalID]);
                }
                catch (Exception)
                {
                     
                }
            }
        }
        catch (Exception)  
        {
             
        }

        if (zPortalID != -1)
        {
             
             
             
            string zDestinationHumpyIP = "";
             

            if (HQ_GetHumpyIPs(zPortalID, ref zHumpyTabletList) > 0)
            {
                 
                foreach (HumpyTablet zht in zHumpyTabletList)
                {
                    if (zht.mHumpyMasterFlag)
                        zDestinationHumpyIP = zht.mHumpyIP;
                }

                m_CurrSelectMasterHumpyIP = zDestinationHumpyIP;

                if (zDestinationHumpyIP != "")
                {
                    Ping pingSender = new Ping();
                    PingReply reply = pingSender.Send(zDestinationHumpyIP);

                    if (reply.Status == IPStatus.Success)
                    {
                         
                         
                         
                        UpdateTagList_Humpy(zDestinationHumpyIP);
                    }
                    else
                    {
                        MessageBox.Show("HUMPY:" + zDestinationHumpyIP + " is not accessable.\r\n Cannot ping the master humpy", "HUMPY VIEWER");
                    }
                }
            }
            else
            {
               
            }
        }
        else
        { 
           
        }

      }
      catch (Exception ex)
      {
          MessageBox.Show("Retrieve Humpy Group Worker Status:" + ex.ToString(), "Location Focused");
         
      }
    }

    private void luPortalName_EditValueChanged(object sender, EventArgs e)
    {
        if (luPortalName.EditValue != null)
        {
            DataRowView zDataRowView = luPortalName.Properties.GetDataSourceRowByKeyValue(luPortalName.EditValue) as DataRowView;
            if (zDataRowView != null)   
            {
                 
                txtPortalDescription.Text = zDataRowView[ISMPortal.Description].ToString();
                 
                m_CurrSelectLocPortalName = zDataRowView[ISMPortal.Description].ToString();
            }
             
        }
        else
        {
             
            txtPortalDescription.Text = "";
            m_CurrSelectLocPortalName = "";
             
        }
         

    }

    private void tvLocation_GetStateImage(object sender, GetStateImageEventArgs e)
    {
       
      if (e.Node["FK_SEAL_CODE"].ToString() == "0")   
        e.NodeImageIndex = -1;
      else
      {
         
         
         

        if (e.Node["SEAL_BROKEN"].ToString() == "0")  
          e.NodeImageIndex = 1;
        else
          e.NodeImageIndex = 5;
         
      }
    }

    private void tvLocation_AfterExpand(object sender, NodeEventArgs e)
    {
      tvLocation.Columns[0].Width += tvLocation.ViewInfo.RC.LevelWidth;  
      tvLocation.Columns[1].Width += tvLocation.ViewInfo.RC.LevelWidth;  
      tvLocation.Columns[2].Width += tvLocation.ViewInfo.RC.LevelWidth;  
    }

    private void tvLocation_AfterCollapse(object sender, NodeEventArgs e)
    {
      tvLocation.Columns[0].Width -= tvLocation.ViewInfo.RC.LevelWidth;  
      tvLocation.Columns[1].Width -= tvLocation.ViewInfo.RC.LevelWidth;  
      tvLocation.Columns[2].Width -= tvLocation.ViewInfo.RC.LevelWidth;  

    }
     
    private void LocationTree_Leave(object sender, EventArgs e)
    {
        TreeListViewState.SaveState();
    }
   
     
    public void CreateMetaDataForLocationTreeNode()
    {
        try
        {
             
            System.Data.DataTable table = new DataTable("LOCNODE");
             
            DataColumn column;
            DataRow row;

             
             
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "ID";
            column.ReadOnly = true;
            column.Unique = true;
             
            table.Columns.Add(column);

             
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "LEVEL";
            column.AutoIncrement = false;
            column.Caption = "LEVEL";
            column.ReadOnly = false;
            column.Unique = false;
             
            table.Columns.Add(column);

             
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = table.Columns["ID"];
            table.PrimaryKey = PrimaryKeyColumns;

             
            DataSet ds = new DataSet();
             
            ds.Tables.Add(table);
             
            tvLocation.Refresh();
            TreeListNodeLevel zTreeListNodeLevel = new TreeListNodeLevel();
            tvLocation.NodesIterator.DoOperation(zTreeListNodeLevel);
            m_NodeCount = zTreeListNodeLevel.MaxLevel;
             


             
             
            if (m_NodeCount > 0)  
            {
                for (int i = 0; i <= m_NodeCount - 1; i++)
                {
                    row = table.NewRow();
                    row["ID"] = i;
                    row["LEVEL"] = "Level - " + (i + 1);
                    table.Rows.Add(row);
                }

                lookUpEditNode.Properties.DataSource = ds.Tables[0].DefaultView;
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    private void SetLookUpEditCaption()
    {
        try
        {

            lookUpEditLocationUID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LOCATION_UID", 90, "Location UID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.LocationCode, 140,"Location Code"),
             
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPDIST, 90, "ERP DIST"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPWHS, 90, "ERP WHS"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPGRID, 90, "ERP GRID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPBIN, 90, "ERP BIN")});

            lookUpEditNode.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID",10,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LEVEL", 90,"Level")});

             
            lookUpEditNode.Properties.DisplayMember = "LEVEL";
            lookUpEditNode.Properties.ValueMember = "ID";

            lookUpEditLocationUID.Properties.DisplayMember = "LOCATION_UID";  
            lookUpEditLocationUID.Properties.ValueMember = ISMLocation.LocationUID;


             
            luPortalName.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalID, "ID",70,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center),  
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalName, 100,"Portal Name"), 
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.Description, 180,"Description ")});

            luPortalName.Properties.DisplayMember = ISMPortal.PortalName;
            luPortalName.Properties.ValueMember = ISMPortal.PortalID;

        }
        catch
        {
            MessageBox.Show("System Error. Contact System Administrator", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    private void lookUpEditNode_EditValueChanged(object sender, EventArgs e)
    {
        try
        {
            if (lookUpEditNode.EditValue != null)
            {
                lookUpEditLocationUID.EditValue = null;
                tvLocation.CollapseAll();
                int zExpandLevel = int.Parse(lookUpEditNode.EditValue.ToString());
                TreeViewExpandOperation expandOperation = new TreeViewExpandOperation(zExpandLevel);
                tvLocation.NodesIterator.DoOperation(expandOperation);
            }
        }
        catch
        {
            MessageBox.Show("System Error. Contact System Administrator", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }

    private void lookUpEditLocationUID_EditValueChanged(object sender, EventArgs e)
    {
        try
        {
            if (lookUpEditLocationUID.EditValue != null)
            {
                lookUpEditNode.EditValue = null;
                TreeListNode Trnode = tvLocation.FindNodeByFieldValue("UIDStr", "8" + lookUpEditLocationUID.EditValue.ToString().PadLeft(12,'0'));
                if (Trnode != null)
                {
                    tvLocation.SetFocusedNode(Trnode);
                }
            }
        }
        catch
        {
            MessageBox.Show("System Error. Contact System Administrator", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
     
    public void LoadFindLocationLookUpEdit()  
    {
        try
        {
            DataSet ds = m_ISMLoginInfo.ISMServer.GetReciveItemUIDAndLocationUID();
            if (ds != null)
            {
                lookUpEditLocationUID.Properties.DataSource = ds.Tables[1].DefaultView;
            }

        }
        catch
        {
            MessageBox.Show("System Error. Contact System Administrator", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }

      void item_Click(object sender, EventArgs e)
      {
           

          DialogResult zdr = MessageBox.Show("!!!!!!-Are you sure to check out worker?-!!!!!!!","HQ CHECKOUT",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
          if(zdr == DialogResult.Yes)
          {
              if (m_CurrSelectedWorkerRecord != null)
              {

                   
                   
                   
                   
                   
                   
                   
                   
                   
                   
                   
                  HQ_CheckOutWorkerFromHumpy(m_CurrSelectMasterHumpyIP,
                                             m_CurrSelectedWorkerRecord[SHS_DbReturnColumnNames.Column4].ToString(),
                                             m_CurrSelectedWorkerRecord[SHS_DbReturnColumnNames.Column1].ToString(),
                                             m_CurrSelectLocPortalName);


                   
                 tvLocation_FocusedNodeChanged(sender,null);



              }
          }
      }

      private void gridView_CheckedIn_MouseDown_1(object sender, MouseEventArgs e)
      {
          if (e.Button == System.Windows.Forms.MouseButtons.Right)
          {

              if (m_ISMLoginInfo.UserProfileCode == "MANAGER" || m_ISMLoginInfo.UserProfileCode == "SYSADMIN") 
              {

                  try
                  {

                      GridHitInfo hi = gridView_CheckedIn.CalcHitInfo(e.Location);
                      object value = "null";

                      if (hi.InRowCell)
                      {
                          value = gridView_CheckedIn.GetRowCellValue(hi.RowHandle, hi.Column);
                          m_CurrSelectedWorkerRecord = (DataRowView)gridView_CheckedIn.GetRow(hi.RowHandle);

                          if (barManager1 == null)
                          {
                              barManager1 = new BarManager();
                              barManager1.Form = this;
                          }
                          if (menu == null)
                              menu = new PopupMenu(barManager1);
                           
                          menu.ItemLinks.Clear();
                          BarButtonItem item1 = new BarButtonItem(barManager1, "Check Out Worker:" + m_CurrSelectedWorkerRecord[SHS_DbReturnColumnNames.Column1].ToString() + "?");
                          BarButtonItem item2 = new BarButtonItem(barManager1, "Cancel");

                          item1.ItemClick += new ItemClickEventHandler(item_Click);
                          menu.AddItems(new BarItem[] { item1 });
                          menu.AddItems(new BarItem[] { item2 });
                          menu.ShowPopup(Cursor.Position);
                      }
                      else
                          m_CurrSelectedWorkerRecord = null;
                  }
                  catch (Exception ex)
                  {
                      ex.ToString();
                      MessageBox.Show("Empty Grid View Data, Please select something, dear user!");
                  }
              }
          }
      }

      private void gridView_CheckedIn_RowCellDefaultAlignment(object sender, RowCellAlignmentEventArgs e)
      {
          e.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
      }

      private void gridView_CheckedIn_RowStyle(object sender, RowStyleEventArgs e)
      {
          try
          {
              GridView View = sender as GridView;
              if (e.RowHandle >= 0)
              {
                  string zINOUTStatus = View.GetRowCellDisplayText(e.RowHandle, View.Columns[SHS_DbReturnColumnNames.Column8]); 
                  object zWorkerName = GetRowColumnDataByRowHandle(gridView_CheckedIn, e.RowHandle, SHS_DbReturnColumnNames.Column1); 

                  if (zINOUTStatus == SHS_INOUT_Values.INValue)
                  {
                      e.Appearance.BackColor = m_Color_Green; 
                       
                       
                  }
                  else if (zINOUTStatus == SHS_INOUT_Values.OUTValue)
                  {
                      e.Appearance.BackColor = m_Color_Red; 
                       
                       
                  }
                  else if (zINOUTStatus == SHS_INOUT_Values.NotCheckedInValue)
                  {
                       
                      e.Appearance.BackColor = m_Color_Yellow; 
                       
                       
                  }
              }
               
          }
          catch (Exception ex)
          {
              ex.ToString();
          }
      }

      private void tvLocation_Click(object sender, EventArgs e) 
      {
          UpdateGridControl2_CheckedInWorkers_RESET();
          tvLocation_FocusedNodeChanged(sender, null);
      }

  }
}
