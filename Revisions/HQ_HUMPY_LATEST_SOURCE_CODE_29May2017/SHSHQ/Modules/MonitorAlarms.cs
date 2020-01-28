 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

using System;
using ISMDAL.TableColumnName;
using DevExpress.XtraGrid.Views.Base;
using System.Windows.Forms;
using System.Data;

namespace ISM.Modules
{
  public partial class MonitorAlarms : ISMBaseWorkSpace
  {
    #region "Private Variable Declaration"

    private string m_DeviceName= "";
    private string m_LocationUID = "";
    #endregion

    public MonitorAlarms(ISMLoginInfo AISMLoginInfo)
      : base(AISMLoginInfo)
    {
      InitializeComponent();
    }

    private void MonitorAlarms_Load(object sender, EventArgs e)
    {
      SetLookUpEditCaption();
      SetGridCaption();
      LoadTaskMonitorMetaData();
      dateEditFrom.EditValue = DateTime.Today;  
      dateEditTo.EditValue = DateTime.Today;

    }

    #region "Lookup Edit And Data Grid Caption Settings"
    private void SetLookUpEditCaption()
    {
      try
      {
        lookUpEditDevice.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalName, 120, "Device Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalID, "ID",10,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center)});
        
        lookUpEditLoc.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LOCATION_UID", 90, "Location UID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.LocationUID, "Location Code",10,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.SealUID, 100,"Seal UID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPDIST, 80, "ERP DIST"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPWHS, 80, "ERP WHS"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPGRID, 80, "ERP GRID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPBIN, 80, "ERP BIN")});

        lookUpEditDevice.Properties.DisplayMember = ISMPortal.PortalName;
        lookUpEditDevice.Properties.ValueMember = ISMPortal.PortalName;
         

        lookUpEditLoc.Properties.DisplayMember = "LOCATION_UID";
        lookUpEditLoc.Properties.ValueMember = ISMLocation.LocationUID;
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Alarms Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void SetGridCaption()
    {
      try
      {
        ColumnView ColView = gvAlarmsMonitor.MainView as ColumnView;

        string[] fieldNames = new string[] { ISMJournal.PortalName, ISMLocation.LocationUID, ISMLocation.SealUID, ISMJournal.ItemUID,ISMJournal.StartDate, ISMJournal.ExceptionDesc };
        DevExpress.XtraGrid.Columns.GridColumn column;
        ColView.Columns.Clear();
        for (int i = 0; i < fieldNames.Length; i++)
        {
          column = ColView.Columns.AddField(fieldNames[i]);
          column.VisibleIndex = i;
        }
        gridView.Columns[0].Caption = "Device Name";
        gridView.Columns[0].Width = 90;
        gridView.Columns[1].Caption = "Item UID";
        gridView.Columns[1].Width = 60;
        gridView.Columns[2].Caption = "Location UID";
        gridView.Columns[2].Width = 60;
        gridView.Columns[3].Caption = "Seal UID";
        gridView.Columns[3].Width = 60;
        gridView.Columns[4].Caption = "Status Date";
         
        gridView.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
         
        gridView.Columns[4].DisplayFormat.FormatString = m_ISMLoginInfo.Params.DateTimeFormat; 
        gridView.Columns[4].Width = 80;
        gridView.Columns[5].Caption = "Exception Description";
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Alarms Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

    #region "Load Meta Data"
    private void LoadTaskMonitorMetaData()
    {
      try
      {
        DataSet ds = m_ISMLoginInfo.ISMServer.GetAlarmsMonitorMetaData();
        if (ds != null)
        {
          lookUpEditDevice.Properties.DataSource = ds.Tables[0].DefaultView;
          lookUpEditLoc.Properties.DataSource = ds.Tables[1].DefaultView;
          lookUpEditDevice.Focus();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Alarms Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

    private void lookUpEditDevice_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        if (lookUpEditDevice.Text.Trim() != "")
            m_DeviceName = lookUpEditDevice.EditValue.ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Alarms Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void lookUpEditLoc_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        if (lookUpEditLoc.Text.Trim() != "")
          m_LocationUID = lookUpEditLoc.EditValue.ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Alarms Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

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
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Alarms Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      return zResult;
    }

    #endregion


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

          DataSet ds = m_ISMLoginInfo.ISMServer.GetAlarmsMonitorReportData(zMode, m_DeviceName, m_LocationUID, zStartDate, zEndDate);
          if (ds != null)
          {
            gvAlarmsMonitor.DataSource = ds.Tables[0].DefaultView;
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Alarms Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      try
      {
        m_DeviceName = "";
        m_LocationUID = "";
        lookUpEditDevice.EditValue = null;
        lookUpEditLoc.EditValue = null;
         
         
        dateEditFrom.EditValue = DateTime.Today;  
        dateEditTo.EditValue = DateTime.Today;
        dxErrorProvider.SetError(dateEditFrom, null);
        dxErrorProvider.SetError(dateEditTo, null);
        lookUpEditDevice.Focus();
         
        gvAlarmsMonitor.DataSource = null;

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Alarms Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void btnExcelReport_Click(object sender, EventArgs e)
    {
      try
      {
        if (gridView.RowCount > 0)
        {
          string zReportLoc = String.Format("{0}AlarmRpt {1} {2}.xls", m_ISMLoginInfo.Params.ReportFolder, DateTime.Today.ToShortDateString().Replace('/', '-'), DateTime.Now.ToShortTimeString().Replace(':', '-'));
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
            zCommon.ExportExcelReport(zFileName, gvAlarmsMonitor);
          }

        }
        else
          MessageBox.Show("Data does not exist for your selection criteria to export Excel Report", "Alarms Report", MessageBoxButtons.OK, MessageBoxIcon.Information);

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Alarms Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }
  }
}
