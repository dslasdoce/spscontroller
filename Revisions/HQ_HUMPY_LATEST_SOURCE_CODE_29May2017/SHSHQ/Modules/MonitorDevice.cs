
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

using System;
using ISMDAL.TableColumnName;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using System.Data;

namespace ISM.Modules
{
  public partial class MonitorDevice : ISMBaseWorkSpace
  {
    #region "Private Variable Declaration"

    private string m_DeviceName = "";
    private int m_PowerStatus = 0;
    #endregion

    public MonitorDevice(ISMLoginInfo AISMLoginInfo)
      : base(AISMLoginInfo)
    {
      InitializeComponent();
    }

    private void MonitorDevice_Load(object sender, EventArgs e)
    {
      SetLookUpEditCaption();
      SetGridCaption();
      LoadDeviceMonitorMetaData();
      rdoDeviceStatus.SelectedIndex = 2;
    }

    #region "Lookup Edit And Data Grid Caption Settings"
    private void SetLookUpEditCaption()
    {
      try
      {
           
           
           
           

           
           

           
          lookUpEditDeviceName.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.ReaderName, 100, "Device Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.ReaderType, 100, "Device Type")});

          lookUpEditDeviceName.Properties.DisplayMember = ISMReaders.ReaderName;
          lookUpEditDeviceName.Properties.ValueMember = ISMReaders.ReaderName;


      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Device Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void SetGridCaption()
    {
      try
      {
          ColumnView ColView = gvDeviceMonitor.MainView as ColumnView;

          string[] fieldNames = new string[] { ISMReaders.ReaderName, ISMReaders.ReaderType, ISMReaders.PowerStatus, ISMReaders.IPAddress, ISMReaders.Description };
          DevExpress.XtraGrid.Columns.GridColumn column;
          ColView.Columns.Clear();
          for (int i = 0; i < fieldNames.Length; i++)
          {
              column = ColView.Columns.AddField(fieldNames[i]);
              column.VisibleIndex = i;
          }
          gridView.Columns[0].Caption = "Device Name";
          gridView.Columns[1].Caption = "Device Type";
           
           
          gridView.Columns[2].Caption = "Status";  
          gridView.Columns[3].Caption = "IP Address";  
          gridView.Columns[4].Caption = "Description";
           
           
           
           
           
           
           
           
           
           
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Device Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

    #region "Load Meta Data"
    private void LoadDeviceMonitorMetaData()
    {
      try
      {
         
         
        DataSet ds = m_ISMLoginInfo.ISMServer.GetLocationPortalMetaData();
        if (ds != null)
            lookUpEditDeviceName.Properties.DataSource = ds.Tables[ISMReaders.TableName].DefaultView;
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Device Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

    #region "Lookup Edit  / Radio Button Event"
    private void lookUpEditPortalName_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        if (lookUpEditDeviceName.EditValue != null)
          m_DeviceName = lookUpEditDeviceName.EditValue.ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Device Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void rdoDeviceStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        m_PowerStatus = int.Parse(rdoDeviceStatus.EditValue.ToString());
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Device Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         

         
         
         
         
         
         
        DataSet ds = m_ISMLoginInfo.ISMServer.GetDeviceMonitorReportData(m_PowerStatus, m_DeviceName);
        if (ds != null)
          gvDeviceMonitor.DataSource = ds.Tables[0].DefaultView;
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
        m_DeviceName = "";
        lookUpEditDeviceName.EditValue = null;
        rdoDeviceStatus.SelectedIndex = 2;
         
        gvDeviceMonitor.DataSource = null;
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Device Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
          string zReportLoc = String.Format("{0}DeviceRpt {1} {2}.xls", m_ISMLoginInfo.Params.ReportFolder, DateTime.Today.ToShortDateString().Replace('/', '-'), DateTime.Now.ToShortTimeString().Replace(':', '-'));
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
            zCommon.ExportExcelReport(zFileName, gvDeviceMonitor);
          }
        }
        else
          MessageBox.Show("Data does not exist for your selection criteria to export Excel Report", "Device Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Device Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

  }
}
