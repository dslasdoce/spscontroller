
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ISMDAL.TableColumnName;
using ISM.Reports;
using DevExpress.XtraReports.UI;

namespace ISM.Modules
{
    public partial class RptFrmAlarmStatus : ISMBaseWorkSpace
    {
        public byte m_ReportType = 0;  

        public RptFrmAlarmStatus(ISMLoginInfo AISMLoginInfo)
            : base(AISMLoginInfo)
        {
            InitializeComponent();
        }
        #region "Form Load"

        private void RptFrmAlarmStatus_Load(object sender, EventArgs e)
        {
            SetLookUpEditCaption();
            LoadMetaData();
            CreateMetaDataForAlarmStatus();
            dateEditFrom.EditValue = DateTime.Today;  
            dateEditTo.EditValue = DateTime.Today;

        }
        #endregion

        #region "Initialization"
        private void SetLookUpEditCaption()
        {
            try
            {

                lookUpEditLocationUID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LOCATION_UID", 90, "Location UID"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.LocationCode, 120,"Location Code"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPDIST, 60, "ERP DIST"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPWHS, 60, "ERP WHS"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPGRID, 60, "ERP GRID"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPBIN, 60, "ERP BIN")});

                lookUpEditLocationUID.Properties.DisplayMember = "LOCATION_UID";
                lookUpEditLocationUID.Properties.ValueMember = ISMLocation.LocationUID;

                lookUpEditDescription.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODE", "CODE",1,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DESCRIPTION", 900, "Alarm Description")});

                lookUpEditDescription.Properties.DisplayMember = "DESCRIPTION";
                lookUpEditDescription.Properties.ValueMember = "CODE";
            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Alarm Status Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void LoadMetaData()
        {
            try
            {
                DataSet ds = null;
                 
                ds = m_ISMLoginInfo.ISMServer.GetAlarmsReportMetaData(m_ReportType);
                if (ds != null)
                {
                    lookUpEditDescription.Properties.DataSource = ds.Tables[0].DefaultView;
                    lookUpEditLocationUID.Properties.DataSource = ds.Tables[1].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Alarm Status Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CreateMetaDataForAlarmStatus()
        {
            try
            {
                 
                System.Data.DataTable table = new DataTable("Status");
                 
                DataColumn column;
                DataRow row;

                 
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Status";
                column.AutoIncrement = false;
                column.Caption = "Status";
                column.ReadOnly = false;
                column.Unique = false;
                 
                table.Columns.Add(column);

                 
                DataColumn[] PrimaryKeyColumns = new DataColumn[1];
                PrimaryKeyColumns[0] = table.Columns["Status"];
                table.PrimaryKey = PrimaryKeyColumns;

                 
                DataSet ds = new DataSet();
                 
                ds.Tables.Add(table);

                 
                 

                row = table.NewRow();
                row["Status"] = "All";
                table.Rows.Add(row);

                row = table.NewRow();
                row["Status"] = "Opened";
                table.Rows.Add(row);
                 
                row = table.NewRow();
                row["Status"] = "Closed";
                table.Rows.Add(row);
                

                lookUpEditAlarm.Properties.DisplayMember = "Status";
                lookUpEditAlarm.Properties.ValueMember = "Status";

                lookUpEditAlarm.Properties.DataSource = ds.Tables[0].DefaultView;



            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Alarm Status Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Alarm Status Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return zResult;
        }

        #endregion

        #region "Generate Report"
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    Cursor.Current = Cursors.WaitCursor;
                    int zMode = 0;
                    string zStartDate = "";
                    string zEndDate = "";
                    string zLocationUID = "";
                    string zAlarmStatus = "2";  
                    string zDescription = "";
                    string zSearchCriteria = "";
                    string zDescCode = "";  
                    if (dateEditFrom.Text.Trim() != "" && dateEditTo.Text.Trim() != "")
                    {   if(m_ReportType == 0)
                            zMode = 2;
                        else
                            zMode = 5;
                        zStartDate = dateEditFrom.Text;
                        zEndDate = dateEditTo.Text;
                    }
                    else if (dateEditFrom.Text.Trim() != "" && dateEditTo.Text.Trim() == "")
                    {
                         
                         
                         
                         
                         
                         
                         

                         
                         
                         
                        if (m_ReportType == 0)
                            zMode = 2;
                        else
                            zMode = 5;
                        zEndDate = DateTime.Now.ToString();
                        zStartDate = dateEditFrom.Text.Trim();

                    }
                    else if (dateEditFrom.Text.Trim() == "" && dateEditTo.Text.Trim() == "")
                    {
                        if (m_ReportType == 0)
                            zMode = 0;
                        else
                            zMode = 3;
                        zStartDate = DateTime.Now.ToString();
                        zEndDate = DateTime.Now.ToString();
                    }
                    if (lookUpEditLocationUID.Text.Trim() != "")
                    {
                        zLocationUID = lookUpEditLocationUID.EditValue.ToString();
                    }
                    if (lookUpEditAlarm.Text.Trim() != "")
                    {
                        if (lookUpEditAlarm.EditValue.ToString() == "Opened")
                            zAlarmStatus = "0";
                        else if(lookUpEditAlarm.EditValue.ToString() == "Closed")
                            zAlarmStatus = "1";
                        else if (lookUpEditAlarm.EditValue.ToString() == "All")
                            zAlarmStatus = "2";

                    }
                    if(lookUpEditLocationUID.Text.Trim() != "")
                        zDescription = lookUpEditDescription.Text.Trim();


                    if (lookUpEditLocationUID.Text.Trim() != "")
                    {
                        zSearchCriteria = "Location UID = " + lookUpEditLocationUID.Text.Trim();
                    }
                    if (lookUpEditDescription.Text.Trim() != "")
                    {
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "Description = " + lookUpEditDescription.Text.Trim();
                        else
                            zSearchCriteria += "Description = " + lookUpEditDescription.Text.Trim();
                        zDescCode = lookUpEditDescription.EditValue.ToString();  
                    }

                    if (lookUpEditAlarm.Text.Trim() != "")
                    {
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "Status = " + lookUpEditAlarm.Text.Trim();
                        else
                            zSearchCriteria += "Status  = " + lookUpEditAlarm.Text.Trim();
                    }
                    if (dateEditFrom.Text.Trim() != "")
                    {
                        if (zSearchCriteria.Trim() != "")
                        {
                            zSearchCriteria += ", " + "From Date = " + dateEditFrom.Text.Trim();
                        }
                        else
                        {
                            zSearchCriteria += "From Date  = " + dateEditFrom.Text.Trim();
                        }
                    }
                    if (dateEditTo.Text.Trim() != "")
                    {
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "To Date = " + dateEditTo.Text.Trim();
                        else
                            zSearchCriteria += "To Date  = " + dateEditTo.Text.Trim();
                    }

                    if (dateEditFrom.Text.Trim() != "" && dateEditTo.Text.Trim() == "")
                    {
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "To Date = " + DateTime.Now.Date.ToShortDateString();
                        else
                            zSearchCriteria += "To Date  = " + DateTime.Now.Date.ToShortDateString();
                    }


                    RptAlarmStatus zRptAlarmStatus = new RptAlarmStatus();
                    zRptAlarmStatus.lblUserName.Text = m_ISMLoginInfo.LogonID;
                     
                    DataSet ds = m_ISMLoginInfo.ISMServer.GetRptAlarmStatus(zMode, zLocationUID, zAlarmStatus, zDescCode, zStartDate, zEndDate);
                     
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        MessageBox.Show("Search criteria netted no results", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }

                     
                    zRptAlarmStatus.DataSource = ds;
                    zRptAlarmStatus.DataMember = ds.Tables[0].TableName;
                     
                    if (m_ReportType == 0)
                    {
                        zRptAlarmStatus.lblHeader2.Text = "Alarms Status Report";
                        zRptAlarmStatus.lblRptRecCount.Text = "No Of Alarms : " + ds.Tables[0].Rows.Count;
                    }
                    else
                    {
                        zRptAlarmStatus.lblHeader2.Text = "Exceptions Status Report";
                        zRptAlarmStatus.lblRptRecCount.Text = "No Of Exceptions : " + ds.Tables[0].Rows.Count;
                    }
                    zRptAlarmStatus.lblSearchCretria.Text = zSearchCriteria;
                    
                     
                    zRptAlarmStatus.lblLocationUID.DataBindings.Add("Text", ds, "FK_LOCATION_UID");
                    zRptAlarmStatus.lblStatus.DataBindings.Add("Text", ds, "Status");
                    zRptAlarmStatus.lblDescription.DataBindings.Add("Text",ds, "JNLCodeDescription");
                    zRptAlarmStatus.lblDateOpen.DataBindings.Add("Text", ds, "DateOpened");
                    zRptAlarmStatus.lblDateClosed.DataBindings.Add("Text", ds, "DateClosed");
                    zRptAlarmStatus.lblComment.DataBindings.Add("Text", ds, "Comment");
                    zRptAlarmStatus.lblClosedUserID.DataBindings.Add("Text", ds, "LOGON_ID");  
                     
                    string zJnlDesc = "";
                    if (m_ReportType == 0)  
                    {
                        if (zSearchCriteria != "")
                            zJnlDesc = "Alarms Report Generated ( " + zSearchCriteria + " )";
                        else
                            zJnlDesc = "Alarms Report Generated";
                    }
                    else if (m_ReportType == 1)
                    {
                        if (zSearchCriteria != "")
                            zJnlDesc = "Exceptions Report Generated ( " + zSearchCriteria + " )";
                        else
                            zJnlDesc = "Exceptions Report Generated";

                    }
                    m_ISMLoginInfo.AddToJournal("T", zJnlDesc, "RPT001", "", "0", "0", "0");

                     
                    Cursor.Current = Cursors.Default;
                    zRptAlarmStatus.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Alarm Status Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }
        #endregion

        #region "Clear Button"
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                dxErrorProvider.SetError(dateEditFrom, null);  
                dxErrorProvider.SetError(dateEditTo, null);   
                lookUpEditLocationUID.EditValue = null;
                 
                 
                dateEditFrom.EditValue = DateTime.Today;  
                dateEditTo.EditValue = DateTime.Today;
                lookUpEditAlarm.EditValue = null;
                lookUpEditDescription.EditValue = null;
                lookUpEditLocationUID.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Alarm Status Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion 

        #region "Edit Value Changed"
        private void dateEditFrom_EditValueChanged(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(dateEditFrom, null);  

        }

        private void dateEditTo_EditValueChanged(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(dateEditTo, null);   
        }
        #endregion

    }
}
