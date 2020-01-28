

 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ISMDAL.TableColumnName;
using DevExpress.XtraGrid.Views.Base;


namespace ISM.Modules
{
    public partial class MonitorPortal : ISMBaseWorkSpace    
    {

        public MonitorPortal(ISMLoginInfo AISMLoginInfo)
            : base (AISMLoginInfo)
        {
            InitializeComponent();
        }
        #region "Form Load"
        private void MonitorPortal_Load(object sender, EventArgs e)
        {
            try
            {
                SetLookUpEditCaption();
                SetGridCaption();
                LoadExceptionMonitorMetaData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Portal Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        #endregion

        #region "Lookup Edit And Data Grid Caption Settings"
        private void SetLookUpEditCaption()
        {
            try
            {
                lookUpEditPortalName.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalName, 300, "Portal Name")});

                 
                lookUpEditReaderlName.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.ReaderName, 300, "Reader Name"),
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.ReaderType, 180, "Type")});

                lookUpEditPortalName.Properties.DisplayMember = ISMPortal.PortalName;
                lookUpEditPortalName.Properties.ValueMember = ISMPortal.PortalName;

                lookUpEditReaderlName.Properties.DisplayMember = ISMReaders.ReaderName;
                lookUpEditReaderlName.Properties.ValueMember = ISMReaders.ReaderName;


            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Portal Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void SetGridCaption()
        {
            try
            {
                ColumnView ColView = gvPortalMonitor.MainView as ColumnView;

                string[] fieldNames = new string[] { ISMPortal.PortalName, ISMReaders.ReaderName, ISMReaders.ReaderType, ISMLocation.LocationUID, ISMLocation.UserDesc };
                DevExpress.XtraGrid.Columns.GridColumn column;
                ColView.Columns.Clear();
                for (int i = 0; i < fieldNames.Length; i++)
                {
                    column = ColView.Columns.AddField(fieldNames[i]);
                    column.VisibleIndex = i;
                }
                gridView.Columns[0].Caption = "Portal Name";
                gridView.Columns[0].Width = 200;
                gridView.Columns[1].Caption = "Reader Name";  
                gridView.Columns[1].Width = 200;
                gridView.Columns[2].Caption = "Reader Type";
                gridView.Columns[2].Width = 150;
                gridView.Columns[3].Caption = "Location UID";
                gridView.Columns[3].Width = 150;
                gridView.Columns[4].Caption = "Location Desc";
                gridView.Columns[4].Width = 250;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Portal Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region "Load Meta Data"
        private void LoadExceptionMonitorMetaData()
        {
            try
            {
                 
                DataSet ds = m_ISMLoginInfo.ISMServer.GetPortalMonitorMetaData(0,"","");
                if (ds != null)
                {
                    lookUpEditPortalName.Properties.DataSource = ds.Tables[0].DefaultView;
                    lookUpEditReaderlName.Properties.DataSource = ds.Tables[1].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Portal Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region "Lookup Edit Event" MR IN 08-FEB-11
        private void lookUpEditPortalName_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lookUpEditPortalName.EditValue != null)
                {
                    if (lookUpEditReaderlName.EditValue == null)
                    {
                        lookUpEditReaderlName.EditValueChanged -= new System.EventHandler(lookUpEditReaderlName_EditValueChanged);
                         
                        DataSet ds = m_ISMLoginInfo.ISMServer.GetPortalMonitorMetaData(1, lookUpEditPortalName.Text.Trim(), "");
                        if (ds != null)
                        {
                            lookUpEditReaderlName.Properties.DataSource = ds.Tables[0].DefaultView;
                        }

                        lookUpEditReaderlName.EditValueChanged += new System.EventHandler(lookUpEditReaderlName_EditValueChanged);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Portal Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lookUpEditReaderlName_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lookUpEditReaderlName.EditValue != null)
                {
                    if (lookUpEditPortalName.EditValue == null)
                    {
                        lookUpEditPortalName.EditValueChanged -= new System.EventHandler(lookUpEditPortalName_EditValueChanged);
                         
                        DataSet ds = m_ISMLoginInfo.ISMServer.GetPortalMonitorMetaData(2, "", lookUpEditReaderlName.Text.Trim());
                        if (ds != null)
                        {
                            lookUpEditPortalName.Properties.DataSource = ds.Tables[0].DefaultView;
                        }

                        lookUpEditPortalName.EditValueChanged += new System.EventHandler(lookUpEditPortalName_EditValueChanged);

                    }
                }
 
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Portal Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion
        
        #region "Button Event"
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                string zReaderType = "";
                string zPortalName = "";
                string zReaderName = "";
                if(rbPortalType.EditValue != null)
                    zReaderType = rbPortalType.EditValue.ToString();
                if(lookUpEditPortalName.EditValue !=null)
                    zPortalName = lookUpEditPortalName.EditValue.ToString();
                if (lookUpEditReaderlName.EditValue != null)
                    zReaderName = lookUpEditReaderlName.EditValue.ToString();

                DataSet ds = m_ISMLoginInfo.ISMServer.GetPortalMonitorReportData(zPortalName, zReaderName, zReaderType);
                if (ds != null)
                {
                    gvPortalMonitor.DataSource = ds.Tables[0].DefaultView;
                    gridView.Columns[ISMPortal.PortalName].GroupIndex = 0;
                    gridView.ExpandAllGroups();
                }
                
 
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Portal Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                rbPortalType.EditValue = null;
                lookUpEditPortalName.EditValue = null;
                lookUpEditReaderlName.EditValue = null;
                lookUpEditPortalName.Focus();
                LoadExceptionMonitorMetaData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Portal Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }

        private void btnExcelReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView.RowCount > 0)
                {
                    string zReportLoc = String.Format("{0}Portal {1} {2}.xls", m_ISMLoginInfo.Params.ReportFolder, DateTime.Today.ToShortDateString().Replace('/', '-'), DateTime.Now.ToShortTimeString().Replace(':', '-'));
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
                        zCommon.ExportExcelReport(zFileName, gvPortalMonitor);
                    }
                }
                else
                    MessageBox.Show("Data does not exist for your selection criteria to export Excel Report", "Portal Report", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Portal Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

      

      

       
    }
}
