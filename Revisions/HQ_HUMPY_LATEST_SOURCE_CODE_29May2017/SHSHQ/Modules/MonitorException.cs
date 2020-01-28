 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
using System;
using ISMDAL.TableColumnName;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using System.Data;

namespace ISM.Modules
{
  public partial class MonitorException : ISMBaseWorkSpace
  {
    #region "Private Variable Declaration"

    private string m_JournalCode= "";
    private string m_OperatorID = "";
    private int m_JournalType = 0;
    #endregion

    public MonitorException(ISMLoginInfo AISMLoginInfo)
      : base(AISMLoginInfo)
    {
      InitializeComponent();
    }

    private void MonitorException_Load(object sender, EventArgs e)
    {
      SetLookUpEditCaption();
      SetGridCaption();
      LoadExceptionMonitorMetaData();
      rdoDataView.SelectedIndex = 3;
      dateEditFrom.EditValue = DateTime.Today;  
      dateEditTo.EditValue = DateTime.Today;

    }

    #region "Lookup Edit And Data Grid Caption Settings"
    private void SetLookUpEditCaption()
    {
      try
      {
        lookUpEditExcepType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMJournalType.Description, 200, "Type"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMJournalType.Code, "Code",10,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center)});
        
        lookUpEditOperatorID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLogonID, 120, "User ID"),  
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserFirstName, 150,"First Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLastName, 150,"Last Name")});

        lookUpEditOperatorID.Properties.DisplayMember = ISMUser.UserLogonID;
        lookUpEditOperatorID.Properties.ValueMember = ISMUser.UserID;

        lookUpEditExcepType.Properties.DisplayMember = ISMJournalType.Description;
        lookUpEditExcepType.Properties.ValueMember = ISMJournalType.Code;
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Exception Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void SetGridCaption()
    {
      try
      {

        ColumnView ColView = gcExceptionMonitor.MainView as ColumnView;

          
        string[] fieldNames = new string[] { ISMJournal.StartDate, ISMJournalType.Description, ISMJournal.ItemUID,ISMJournal.LocationID, ISMJournal.SealID, ISMJournal.TaskID, ISMJournal.StockCode, ISMUser.UserLogonID, "CoA_UserID" };  

          DevExpress.XtraGrid.Columns.GridColumn column;
          ColView.Columns.Clear();
          for (int i = 0; i < fieldNames.Length; i++)
          {
            column = ColView.Columns.AddField(fieldNames[i]);
            column.VisibleIndex = i;
          }
           
          gridView.Columns[0].Caption = "Date";  
           
          gridView.Columns[0].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
           
          gridView.Columns[0].DisplayFormat.FormatString = m_ISMLoginInfo.Params.DateTimeFormat;  
          gridView.Columns[0].Width = 100; 
           
          gridView.Columns[1].Caption = "Description";   
          gridView.Columns[1].Width = 370;

          gridView.Columns[2].Caption = "Item UID";  
          gridView.Columns[2].Width = 80;
          gridView.Columns[2].Visible = false; 

          gridView.Columns[3].Caption = "Location UID";
          gridView.Columns[3].Width = 80;
          gridView.Columns[3].Visible = false; 

          gridView.Columns[4].Caption = "Seal UID";
          gridView.Columns[4].Width = 80;
          gridView.Columns[4].Visible = false; 

          gridView.Columns[5].Caption = "Task ID";  
          gridView.Columns[5].Width = 110;
          gridView.Columns[6].Caption = "Stock Code";
          gridView.Columns[6].Width = 90;
          gridView.Columns[6].Visible = false; 

          gridView.Columns[7].Caption = "User ID";  
          gridView.Columns[7].Width = 140;

          gridView.Columns[8].Caption = "CoA Verifier User ID";  
          gridView.Columns[8].Width = 140;
          gridView.Columns[8].Visible = false; 

          gridView.HorzScrollVisibility = ScrollVisibility.Always;

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Exception Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

    #region "Load Meta Data"
    private void LoadExceptionMonitorMetaData()
    {
      try
      {
        DataSet ds = m_ISMLoginInfo.ISMServer.GetExceptionMonitorMetaData();
        if (ds != null)
        {
          lookUpEditOperatorID.Properties.DataSource = ds.Tables[0].DefaultView;
          lookUpEditExcepType.Properties.DataSource = ds.Tables[1].DefaultView;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Exception Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

    #region Lookup Edit Event"
    private void lookUpEditExcepType_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        if (lookUpEditExcepType.EditValue != null)
          m_JournalCode = lookUpEditExcepType.EditValue.ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Exception Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }

    private void lookUpEditOperatorID_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        if (lookUpEditOperatorID.EditValue != null)
          m_OperatorID = lookUpEditOperatorID.EditValue.ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Exception Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      return zResult;
    }

    #endregion

    #region "Clear Button Event"
    private void btnClear_Click(object sender, EventArgs e)
    {
      try
      {
        m_OperatorID = "";
        m_JournalCode = "";
        lookUpEditOperatorID.EditValue = null;
        lookUpEditExcepType.EditValue = null;
         
         
        dateEditFrom.EditValue = DateTime.Today;  
        dateEditTo.EditValue = DateTime.Today;
        dxErrorProvider.SetError(dateEditFrom, null);
        dxErrorProvider.SetError(dateEditTo, null);
        gcExceptionMonitor.DataSource = null;
        rdoDataView.SelectedIndex = 3;  
        lookUpEditExcepType.Focus();
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Exception Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
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
          if (rdoDataView.EditValue != null)
          {
              m_JournalType = int.Parse(rdoDataView.EditValue.ToString());
          }
         

          DataSet ds = m_ISMLoginInfo.ISMServer.GetExceptionMonitorReportData(zMode,m_JournalType, m_JournalCode, m_OperatorID,  zStartDate, zEndDate);
          if (ds != null)
          {
            gcExceptionMonitor.DataSource = ds.Tables[0].DefaultView;
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Exception Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

    #region "Excel Report Button Event"
    private void btnExcelReport_Click(object sender, EventArgs e)
    {
      try
      {
        if (gridView.RowCount > 0)
        {
          string zReportLoc = String.Format("{0}JournalRpt {1} {2}.xls", m_ISMLoginInfo.Params.ReportFolder, DateTime.Today.ToShortDateString().Replace('/', '-'), DateTime.Now.ToShortTimeString().Replace(':', '-'));
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
            zCommon.ExportExcelReport(zFileName, gcExceptionMonitor);
          }
        }
        else
          MessageBox.Show("Data does not exist for your selection criteria to export Excel Report", "Exception Report", MessageBoxButtons.OK, MessageBoxIcon.Information);

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Exception Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }
    #endregion

    #region "Radio Button Event"
    private void rdoDataView_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            m_JournalType = int.Parse(rdoDataView.EditValue.ToString());
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Exception Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


    }
    #endregion
  }
}
