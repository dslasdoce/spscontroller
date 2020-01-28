
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

using System;
using ISMDAL.TableColumnName;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;

namespace ISM.Modules
{
  public partial class MonitorTask : ISMBaseWorkSpace
  {
    #region "Private Variable Declaration"

    private string m_TaskID = "";
    private string m_TaskType = "";
    private string m_TaskStatus = "";
    private string m_OperatorID = "";
    #endregion

    public MonitorTask(ISMLoginInfo AISMLoginInfo)
      : base(AISMLoginInfo)
    {
      InitializeComponent();
    }

    private void MonitorTask_Load(object sender, EventArgs e)
    {
      SetLookUpEditCaption();
      LoadTaskMonitorMetaData();
      SetGridCaption();
      dateEditFrom.EditValue = DateTime.Today;  
      dateEditTo.EditValue = DateTime.Today;

    }

    #region "Lookup Edit And Data Grid Caption Settings"
    private void SetLookUpEditCaption()
    {
      try
      {
        lookUpEditTaskID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TASK_ID", 120, "Task ID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMTask.ID, "ID",10,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center)});

        lookUpEditTaskType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMOperation.Desc, 100, "Task Type"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMOperation.ID, "Desc",10,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center)});

        lookUpEditTaskStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMTaskStatus.Desc, 100, "Task Status"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMTaskStatus.Code, "Code",10,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center)});

        lookUpEditAssignee.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLogonID, 120, "User ID"),  
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserFirstName, 150,"First Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLastName, 150,"Last Name")});


         
        lookUpEditTaskID.Properties.DisplayMember = "TASK_ID";
        lookUpEditTaskID.Properties.ValueMember = ISMTask.ID;

        lookUpEditTaskType.Properties.DisplayMember = ISMOperation.Desc;
        lookUpEditTaskType.Properties.ValueMember = ISMOperation.Desc;  
         

        lookUpEditTaskStatus.Properties.DisplayMember = ISMTaskStatus.Desc;
        lookUpEditTaskStatus.Properties.ValueMember = ISMTaskStatus.Code;

        lookUpEditAssignee.Properties.DisplayMember = ISMUser.UserLogonID;
        lookUpEditAssignee.Properties.ValueMember = ISMUser.UserID;


      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Task Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void SetGridCaption()
    {
      try
      {
        ColumnView ColView = gvTaskMonitor.MainView as ColumnView;

          
         
         
         
         
        string[] zFieldNames = new string[] { "TASK_ID", "TYPE","TASK_TYPE", ISMTask.CreateDateTime, ISMTask.StartDateTime, ISMTask.EndDateTime, "TASK_STATUS", "ASSIGNEE", ISMTask.ItemID, "SOURCELOC_UID", "DESTLOC_UID", ISMTask.StockCode, ISMTask.StockQty };
        
        DevExpress.XtraGrid.Columns.GridColumn zColumn;
        ColView.Columns.Clear();
        for (int i = 0; i < zFieldNames.Length; i++)
        {
          zColumn = ColView.Columns.AddField(zFieldNames[i]);
          zColumn.VisibleIndex = i;
        }
         
        gridView.Columns[0].Caption = "Task ID";
        gridView.Columns[0].Width = 100;

         
        gridView.Columns[1].Caption = "Task Type";
        gridView.Columns[1].Width = 80;
   
         
        gridView.Columns[2].Caption = "Operation Type";
        gridView.Columns[2].Width = 100;

        gridView.Columns[3].Caption = "Assigned Date";
        gridView.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
         
        gridView.Columns[3].DisplayFormat.FormatString = m_ISMLoginInfo.Params.DateTimeFormat;  
        gridView.Columns[3].Width = 125;
        
         
        gridView.Columns[4].Caption = "Task Start Date";  
        gridView.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
         
        gridView.Columns[4].DisplayFormat.FormatString = m_ISMLoginInfo.Params.DateTimeFormat;  
        gridView.Columns[4].Width = 125;
         
        gridView.Columns[5].Caption = "Task Completed Date";
        gridView.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
         
        gridView.Columns[5].DisplayFormat.FormatString = m_ISMLoginInfo.Params.DateTimeFormat;  
        gridView.Columns[5].Width = 125;
        
        /* Out DM 17-SEP-10
        gridView.Columns[4].Caption = "Assigned Date";
        gridView.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
        gridView.Columns[4].DisplayFormat.FormatString = "g";
        gridView.Columns[4].Width = 100;
        */
        gridView.Columns[6].Caption = "Status";
        gridView.Columns[6].Width = 80;
        
        gridView.Columns[7].Caption = "User ID";  
        gridView.Columns[7].Width = 110;
        gridView.Columns[8].Caption = "Item UID";
        gridView.Columns[8].Width = 90;
        gridView.Columns[9].Caption = "Source Loc UID";
        gridView.Columns[9].Width = 90;
        gridView.Columns[10].Caption = "Dest Loc UID";
        gridView.Columns[10].Width = 90;
        gridView.Columns[11].Caption = "Stock Code";  
        gridView.Columns[11].Width = 110;
        gridView.Columns[12].Caption = "Quantity"; 
        gridView.Columns[12].Width = 90;
        
         
      }
      catch
      {
        MessageBox.Show("System Error. Contact System Administrator", "Task Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
#endregion
      
    #region "Load Meta Data"

    private void LoadTaskMonitorMetaData()
    {
      try
      {
        DataSet ds = m_ISMLoginInfo.ISMServer.GetTaskMonitorMetaData();
        if (ds != null)
        {
          lookUpEditTaskID.Properties.DataSource = ds.Tables[0].DefaultView;
          lookUpEditTaskType.Properties.DataSource = ds.Tables[1].DefaultView;
          lookUpEditTaskStatus.Properties.DataSource = ds.Tables[2].DefaultView;
          lookUpEditAssignee.Properties.DataSource = ds.Tables[3].DefaultView;
          lookUpEditTaskID.Focus();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Task Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

    #region "LookupEdit Event"

    private void lookUpEditTaskID_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
          if (lookUpEditTaskID.Text.Trim() != "")
          {
              m_TaskID = lookUpEditTaskID.EditValue.ToString();
              EnableDisableControl(false);  
              btnReport.PerformClick();  
          }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Task Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }

    private void lookUpEditTaskType_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        if (lookUpEditTaskType.Text.Trim() != "")
          m_TaskType = lookUpEditTaskType.EditValue.ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Task Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void lookUpEditTaskStatus_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        if (lookUpEditTaskStatus.Text.Trim() != "")
          m_TaskStatus = lookUpEditTaskStatus.EditValue.ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Task Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void lookUpEditAssignee_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        if (lookUpEditAssignee.Text.Trim() != "")
          m_OperatorID = lookUpEditAssignee.EditValue.ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Task Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }
    #endregion

    #region "Validation "
    private bool Validation()
    {
      bool zResult = false;
      bool zValidationFail = true;

      try
      {
        dxErrorProvider.SetError(dateEditFrom, null);
        dxErrorProvider.SetError(dateEditTo, null);

        if (dateEditTo.Text != "")
        {
          if (dateEditFrom.Text == "")
          {
            dxErrorProvider.SetError(dateEditFrom, "Select a From Date");
            dateEditFrom.Focus();
            zValidationFail = false;
          }
          else if (dateEditFrom.Text != "")
          {
            if (DateTime.Parse(dateEditFrom.Text) > DateTime.Parse(dateEditTo.Text))
            {
              dxErrorProvider.SetError(dateEditTo, "To Date should be greater than From Date");
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
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Task Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      return zResult;
    }

    #endregion

    #region "Report Button Event"

    private void btnReport_Click(object sender, EventArgs e)
    {
      try
      {
        if (Validation())
        {
          int zMode = 0;
          string zStartDate = "";
          string zEndDate = "";
          SetGridCaption();  
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
          
          DataSet ds = m_ISMLoginInfo.ISMServer.GetTaskMonitorReportData(zMode, m_TaskID, m_TaskType, m_TaskStatus, m_OperatorID, zStartDate, zEndDate);
          if (ds != null)
          {
            gvTaskMonitor.DataSource = ds.Tables[0].DefaultView;
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Task Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }
    #endregion

    #region "Clear Button Event"
    private void EnableDisableControl(bool AEnableDisable)
    {
        try
        {
            lookUpEditTaskType.EditValue = null;
            lookUpEditTaskStatus.EditValue = null;
            lookUpEditAssignee.EditValue = null;
            dateEditFrom.EditValue = null;
            dateEditTo.EditValue = null;
            lookUpEditTaskType.Enabled = AEnableDisable;
            lookUpEditTaskStatus.Enabled = AEnableDisable;
            lookUpEditAssignee.Enabled = AEnableDisable;
            dateEditFrom.Enabled = AEnableDisable;
            dateEditTo.Enabled = AEnableDisable;
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Task Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    private void btnClear_Click(object sender, EventArgs e)
    {
      try
      {
        m_TaskID = "";
        m_TaskType = "";
        m_TaskStatus = "";
        m_OperatorID = "";
        lookUpEditTaskID.EditValue = null;
        lookUpEditTaskType.EditValue = null;
        lookUpEditTaskStatus.EditValue = null;
        lookUpEditAssignee.EditValue = null;
         
         
        dateEditFrom.EditValue = DateTime.Today;  
        dateEditTo.EditValue = DateTime.Today;

        dxErrorProvider.SetError(dateEditFrom, null);
        dxErrorProvider.SetError(dateEditTo, null);
         
        gvTaskMonitor.DataSource = null;
        lookUpEditTaskID.Focus();
        EnableDisableControl(true);  
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Task Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
          string zReportLoc = String.Format("{0}TaskRpt {1} {2}.xls", m_ISMLoginInfo.Params.ReportFolder, DateTime.Today.ToShortDateString().Replace('/', '-'), DateTime.Now.ToShortTimeString().Replace(':', '-'));
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
            zCommon.ExportExcelReport(zFileName, gvTaskMonitor);
          }
        }
        else
          MessageBox.Show("Data does not exist for your selection criteria to export Excel Report", "Task Report", MessageBoxButtons.OK, MessageBoxIcon.Information);

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Task Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion
  }

}
