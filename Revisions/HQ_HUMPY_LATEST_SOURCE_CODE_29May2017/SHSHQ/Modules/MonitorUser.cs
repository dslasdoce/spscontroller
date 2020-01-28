 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
using System;
using ISMDAL.TableColumnName;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using System.Data;

namespace ISM.Modules
{
  public partial class MonitorUser : ISMBaseWorkSpace
  {
    #region "Private Variable Declaration"

    private string m_TaskStatus = "";
    private string m_UserProfileCode = "";
    private string m_UserID = "";
    private int m_DataViewType = 0;
    private bool m_SelectionChanged = false;
    #endregion
    public MonitorUser(ISMLoginInfo AISMLoginInfo)
      :base(AISMLoginInfo)
    {
      InitializeComponent();
    }


    private void MonitorOperator_Load(object sender, EventArgs e)
    {
      rdoDataView.SelectedIndex = 0;
      SetLookUpEditCaption();
      SetGridCaption();
      LoadOperatorMonitorMetaData();
      dateEditFrom.EditValue = DateTime.Today;  
      dateEditTo.EditValue = DateTime.Today;

    }
    #region "Lookup Edit And Data Grid Caption Settings"
    private void SetLookUpEditCaption()
    {
      try
      {
        lookUpEditOperatorID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLogonID, 120, "User ID"),  
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserFirstName, 150,"First Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLastName, 150,"Last Name")});

         
         
         

        lookUpEditTaskStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMTaskStatus.Desc, 100, "Task Status"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMTaskStatus.Code, "Code",10,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center)});

        lookUpEditOperatorID.Properties.DisplayMember = ISMUser.UserLogonID;
        lookUpEditOperatorID.Properties.ValueMember = ISMUser.UserID;

         
         

        lookUpEditTaskStatus.Properties.DisplayMember = ISMTaskStatus.Desc;
        lookUpEditTaskStatus.Properties.ValueMember = ISMTaskStatus.Code;

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Operator Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void SetGridCaption()
    {
      try
      {
        ColumnView ColView = gCOperatorMonitor.MainView as ColumnView;

        if (m_DataViewType == 0)
        {
          string[] fieldNames = new string[] { ISMUser.UserLogonID,  "TASK_TYPE", "TASK_STATUS", "DATE_ASSIGN", "TIME_DIFFVAL" };
           
          DevExpress.XtraGrid.Columns.GridColumn column;
          ColView.Columns.Clear();
          for (int i = 0; i < fieldNames.Length; i++)
          {
            column = ColView.Columns.AddField(fieldNames[i]);
            column.VisibleIndex = i;
          }
          gridView.Columns[0].Caption = "User ID";  
           
          gridView.Columns[1].Caption = "Task Type";
          gridView.Columns[2].Caption = "Status";
          gridView.Columns[3].Caption = "Assigned Date";
           
          gridView.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
           
          gridView.Columns[3].DisplayFormat.FormatString = m_ISMLoginInfo.Params.DateTimeFormat;  
          gridView.Columns[4].Caption = "Time Ref";
        }
        else
        {
           
          string[] fieldNames = new string[] { ISMUser.UserLogonID,  "TASK_TYPE", "TASK_STATUS", "STARTDATE","EXPTYPE", "EXPDESC" };
          DevExpress.XtraGrid.Columns.GridColumn column;
          ColView.Columns.Clear();
          for (int i = 0; i < fieldNames.Length; i++)
          {
            column = ColView.Columns.AddField(fieldNames[i]);
            column.VisibleIndex = i;
          }
          gridView.Columns[0].Caption = "User";
           
          gridView.Columns[1].Caption = "Task Type";
          gridView.Columns[2].Caption = "Status";
          gridView.Columns[3].Caption = "Exception Date";
           
          gridView.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
           
          gridView.Columns[3].DisplayFormat.FormatString = m_ISMLoginInfo.Params.DateTimeFormat;

          gridView.Columns[4].Caption = "Exception Type";
          gridView.Columns[5].Caption = "Exception Description";
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Operator Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

    #region "Load Meta Data"
    private void LoadOperatorMonitorMetaData()
    {
      try
      {
        DataSet ds = m_ISMLoginInfo.ISMServer.GetOperatorMonitorMetaData();
        if (ds != null)
        {
          lookUpEditOperatorID.Properties.DataSource = ds.Tables[0].DefaultView;
          
          lookUpEditTaskStatus.Properties.DataSource = ds.Tables[1].DefaultView;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Operator Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

    #region "Lookup Edit Event"

    private void lookUpEditOperatorID_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        if (lookUpEditOperatorID.Text.Trim() != "")
          m_UserID = lookUpEditOperatorID.EditValue.ToString();
        m_SelectionChanged = true;  
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Operator Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    

    private void lookUpEditTaskStatus_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        if (lookUpEditTaskStatus.Text.Trim() != "")
          m_TaskStatus = lookUpEditTaskStatus.EditValue.ToString();
        m_SelectionChanged = true;  
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Operator Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }
    private void dateEditFrom_EditValueChanged(object sender, EventArgs e)
    {
        m_SelectionChanged = true;  
    }

    private void dateEditTo_EditValueChanged(object sender, EventArgs e)
    {
        m_SelectionChanged = true;  
    }
    #endregion

    #region "Radio Button Event"
    private void rdoDataView_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        m_SelectionChanged = true;  
        if (rdoDataView.EditValue.ToString() == "0")
        {
          m_DataViewType = 0;
          lookUpEditTaskStatus.Enabled = true;
          
        }
        else
        {
          m_DataViewType = 1;
          lookUpEditTaskStatus.Enabled = false;
          
          lookUpEditTaskStatus.EditValue = null;
          m_TaskStatus = "";
          m_UserProfileCode = "";
          m_TaskStatus = "";
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Operator Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }
    #endregion

    #region "Validation"
    private bool Validation()
    {
      bool zResult = false;
      bool zValidationFail = true;
      try
      {
        dxErrorProvider.SetError(dateEditFrom, null);
        dxErrorProvider.SetError(dateEditTo, null);
        if (m_DataViewType == 1)
        {
          if (dateEditFrom.Text.Trim() != "")
          {
            if (DateTime.Parse(dateEditFrom.Text) > DateTime.Today)
            {
              dxErrorProvider.SetError(dateEditFrom, "From Date Can't be grater than Today’s Date");
              dateEditFrom.Focus();
              zValidationFail = false;
            }
          }
          if (dateEditTo.Text.Trim() != "")
          {
            if (DateTime.Parse(dateEditTo.Text) > DateTime.Today)
            {
              dxErrorProvider.SetError(dateEditTo, "To Date Can't be grater than Today’s Date");
              dateEditTo.Focus();
              zValidationFail = false;
            }
          }
        }
        if (dateEditTo.Text != "")
        {
          if (dateEditFrom.Text == "")
          {
            dxErrorProvider.SetError(dateEditFrom, "Select From Date");
            dateEditFrom.Focus();
            zValidationFail = false;
          }
          else if (dateEditFrom.Text != "")
          {
            if (DateTime.Parse(dateEditFrom.Text) > DateTime.Parse(dateEditTo.Text))
            {
              dxErrorProvider.SetError(dateEditTo, "To Date should be grater than From Date");
              dateEditTo.Focus();
              zValidationFail = false;
            }

          }
        }
        if (zValidationFail)
          zResult = zValidationFail;

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Operator Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      return zResult;
    }
    #endregion

    #region "Report Button"
    private void btnReport_Click(object sender, EventArgs e)
    {
      try
      {
        if (Validation() && m_SelectionChanged)
        {
          int zMode = 0;
          string zStartDate = "";
          string zEndDate = "";
          if (dateEditFrom.Text.Trim() != "" && dateEditTo.Text.Trim() != "")
          {
            zMode = 2;
            zStartDate = dateEditFrom.Text.Trim();
            zEndDate = dateEditTo.Text.Trim();
          }
          else if (dateEditFrom.Text.Trim() != "" && dateEditTo.Text.Trim() == "")
          {
            zMode = 2;
             
             
             
             

            zEndDate = DateTime.Now.ToString();
            zStartDate = dateEditFrom.Text.Trim();
          }
          else if (dateEditFrom.Text.Trim() == "" && dateEditTo.Text.Trim() == "")
          {
            zMode = 0;
            zStartDate = DateTime.Now.ToString();
            zEndDate = DateTime.Now.ToString();

          }
           
          DataSet ds = m_ISMLoginInfo.ISMServer.GetOperatorMonitorReportData(m_DataViewType, zMode, m_UserID, m_TaskStatus,  zStartDate, zEndDate);
          if (m_DataViewType == 0)
          {
              string zTimeDiff = "";
              TimeSpan zTimspan;
              int zCount = 0;
              foreach (DataRow drow in ds.Tables[0].Rows)
              {
                   
                  if (drow["ENDDATETIME"] != System.DBNull.Value && drow["STARTDATETIME"] != System.DBNull.Value)
                  {
                       
                      if (drow["TASK_STATUS"].ToString() == "Completed")
                      {
                          DateTime zTaskEndDate = DateTime.Parse(drow["ENDDATETIME"].ToString());  
                          DateTime zTaskCreateDate = DateTime.Parse(drow["STARTDATETIME"].ToString());  
                          zTimspan = zTaskEndDate.Subtract(zTaskCreateDate);

                          zTimeDiff = string.Format("{0:D2} Day(s) : {1:D2} Hr : {2:D2} Min : {3:D2} Sec",      
                                        zTimspan.Days,
                                        zTimspan.Hours,
                                        zTimspan.Minutes,
                                       zTimspan.Seconds);
                      }
                      else
                          zTimeDiff = "";  
                  }
                  ds.Tables[0].Rows[zCount].BeginEdit();
                  drow["TIME_DIFFVAL"] = zTimeDiff;
                  ds.Tables[0].Rows[zCount].EndEdit();

                  zCount += 1;
              }
              ds.Tables[0].AcceptChanges();
          }

           
          m_SelectionChanged = false;
          SetGridCaption();
          if (ds != null)
          {
            gCOperatorMonitor.DataSource = ds.Tables[0].DefaultView;
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Operator Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

    #region "Clear Button"
    private void btnClear_Click(object sender, EventArgs e)
    {
      try
      {
        lookUpEditOperatorID.EditValue = null;
        lookUpEditTaskStatus.EditValue = null;
         
         
        dateEditFrom.EditValue = DateTime.Today;  
        dateEditTo.EditValue = DateTime.Today;

        dxErrorProvider.SetError(dateEditFrom, null);
        dxErrorProvider.SetError(dateEditTo, null);
        m_TaskStatus = "";
        m_UserProfileCode = "";
        m_UserID = "";
        if (rdoDataView.EditValue.ToString() == "0")
          m_DataViewType = 0;
        else
          m_DataViewType = 1;

        lookUpEditOperatorID.Focus();
         
        gCOperatorMonitor.DataSource = null;
         
        m_SelectionChanged = true;
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Operator Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

    #region "Excel Report"
    private void btnExcelReport_Click(object sender, EventArgs e)
    {
      try
      {
        if (gridView.RowCount > 0)
        {
          string zReportLoc = String.Format("{0}OperatorRpt {1} {2}.xls", m_ISMLoginInfo.Params.ReportFolder, DateTime.Today.ToShortDateString().Replace('/', '-'), DateTime.Now.ToShortTimeString().Replace(':', '-'));
          SaveFileDialog dlgFile = new SaveFileDialog();
          dlgFile.InitialDirectory = m_ISMLoginInfo.Params.ReportFolder;
          dlgFile.FileName = zReportLoc;
          dlgFile.Filter = "Excel Files (*.xls)|*.xls";
          dlgFile.FilterIndex = 1;
          dlgFile.RestoreDirectory = true;
          if (dlgFile.ShowDialog() == DialogResult.OK)
          {
            string zFileName = dlgFile.FileName;
            ISMCommon zCommon = new ISMCommon();
            zCommon.ExportExcelReport(zFileName, gCOperatorMonitor);
          }
        }
        else
          MessageBox.Show("Data does not exist for your selection criteria to export Excel Report", "Operator Report", MessageBoxButtons.OK, MessageBoxIcon.Information);

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Operator Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

  
  }
}
