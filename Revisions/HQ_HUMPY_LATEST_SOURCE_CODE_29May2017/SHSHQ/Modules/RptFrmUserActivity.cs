 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using ISMDAL.TableColumnName;
using ISM.Reports;
using DevExpress.XtraReports.UI;

namespace ISM.Modules
{
    public partial class RptFrmUserActivity : ISMBaseWorkSpace
    {
        public RptFrmUserActivity(ISMLoginInfo AISMLoginInfo)
            : base(AISMLoginInfo)
        {
            InitializeComponent();
        }
        #region "Form Load"
        private void RptFrmUserActivity_Load(object sender, EventArgs e)
        {
            SetLookUpEditCaption();
            LoadTaskMonitorMetaData();
            lookUpEditOperator.Focus();
            dateEditFrom.EditValue = DateTime.Today;  
            dateEditTo.EditValue = DateTime.Today;


        }
        #endregion 

        #region "Lookup Edit And Data Grid Caption Settings"
        private void SetLookUpEditCaption()
        {
            try
            {

                lookUpEditTaskType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMOperation.Desc, 100, "Task Type"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMOperation.ID, "Desc",10,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center)});

                lookUpEditTaskStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMTaskStatus.Desc, 100, "Task Status"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMTaskStatus.Code, "Code",10,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center)});

                lookUpEditOperator.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLogonID, 120, "User ID"),  
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserFirstName, 150,"First Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLastName, 150,"Last Name")});

                lookUpEditTaskType.Properties.DisplayMember = ISMOperation.Desc;
                lookUpEditTaskType.Properties.ValueMember = ISMOperation.Desc;  
                

                lookUpEditTaskStatus.Properties.DisplayMember = ISMTaskStatus.Desc;
                lookUpEditTaskStatus.Properties.ValueMember = ISMTaskStatus.Code;

                lookUpEditOperator.Properties.DisplayMember = ISMUser.UserLogonID;
                lookUpEditOperator.Properties.ValueMember = ISMUser.UserID;


            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "User Acticity Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void LoadTaskMonitorMetaData()
        {
            try
            {
                DataSet ds = m_ISMLoginInfo.ISMServer.GetTaskMonitorMetaData();
                if (ds != null)
                {
                    lookUpEditTaskType.Properties.DataSource = ds.Tables[1].DefaultView;
                    lookUpEditTaskStatus.Properties.DataSource = ds.Tables[2].DefaultView;
                    lookUpEditOperator.Properties.DataSource = ds.Tables[3].DefaultView;
                    lookUpEditOperator.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "User Acticity Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                if (lookUpEditOperator.Text.Trim() == "" && lookUpEditTaskType.Text.Trim() == "" && lookUpEditTaskStatus.Text == ""
                    && dateEditFrom.Text.Trim() == "" && dateEditTo.Text.Trim() == "")
                {
                    dxErrorProvider.SetError(lookUpEditOperator, "Select a User ID");
                    dxErrorProvider.SetError(lookUpEditTaskType, "Select a Task Type");
                    dxErrorProvider.SetError(lookUpEditTaskStatus, "Select a Task Status");
                    dxErrorProvider.SetError(dateEditFrom, "Select From Date");
                    dxErrorProvider.SetError(dateEditTo, "Select To Date");
                    lookUpEditOperator.Focus();
                    zValidationFail = false;

                }
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
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "User Acticity Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return zResult;
        }

        #endregion  
  
        #region "Generate Report

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
                    string zOperatorID = "";
                    string zTaskType = "";
                    string zTaskStatus = "";
                    string zSearchCriteria = "";
                    if (dateEditFrom.Text.Trim() != "" && dateEditTo.Text.Trim() != "")
                    {
                        zMode = 2;
                        zStartDate = dateEditFrom.Text;
                        zEndDate = dateEditTo.Text;
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
                    if (lookUpEditOperator.Text.Trim() != "")
                    {
                        zSearchCriteria = "User = " + lookUpEditOperator.Text.Trim();
                        zOperatorID = lookUpEditOperator.EditValue.ToString();
                    }

                    if (lookUpEditTaskType.Text.Trim() != "")
                    {
                        zTaskType = lookUpEditTaskType.EditValue.ToString();
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "Operation Type = " + lookUpEditTaskType.Text.Trim();
                        else
                            zSearchCriteria += "Operation Type  = " + lookUpEditTaskType.Text.Trim();
                    }
                    if (lookUpEditTaskStatus.Text.Trim() != "")
                    {
                        zTaskStatus = lookUpEditTaskStatus.EditValue.ToString();
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "Task Status = " + lookUpEditTaskStatus.Text.Trim();
                        else
                            zSearchCriteria += "Task Status = " + lookUpEditTaskStatus.Text.Trim();
                    }

                    if (dateEditFrom.Text.Trim() != "")
                    {
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "From Date = " + dateEditFrom.Text.Trim();
                        else
                            zSearchCriteria += "From Date  = " + dateEditFrom.Text.Trim();
                    }
                    if (dateEditTo.Text.Trim() != "")
                    {
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "To Date = " + dateEditTo.Text.Trim();
                        else
                            zSearchCriteria += "To Date  = " + dateEditTo.Text.Trim();
                    }

                    RptUserActivity zRptUserActivity = new RptUserActivity();
                    zRptUserActivity.lblUserName.Text = m_ISMLoginInfo.LogonID;
                    DataSet ds = m_ISMLoginInfo.ISMServer.GetRptUserActivity(zMode, zOperatorID, zTaskType, zTaskStatus,zStartDate, zEndDate);
                     
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        MessageBox.Show("Search criteria netted no results", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }

                     
                    zRptUserActivity.DataSource = ds;
                    zRptUserActivity.DataMember = ds.Tables[0].TableName;
                     
                    zRptUserActivity.lblRptRecCount.Text = "No Of Records : " + ds.Tables[0].Rows.Count;
                    if (zSearchCriteria.Trim() != "")
                        zRptUserActivity.lblSearch.Text = zSearchCriteria;
                    else
                        zRptUserActivity.lblSearch.Text = "";

                     
                    zRptUserActivity.GHTaskDate.Controls.Add(zRptUserActivity.lblGHTaskDate);
                    zRptUserActivity.GHTaskDate.KeepTogether = true;

                    zRptUserActivity.GHUserID.Controls.Add(zRptUserActivity.lblGHUserID);
                    zRptUserActivity.GHUserID.KeepTogether = true;

                    GroupField GFTaskDate = new GroupField("TASK_DATE", XRColumnSortOrder.Descending);
                    zRptUserActivity.GHTaskDate.GroupFields.Add(GFTaskDate);


                    GroupField GFUserName = new GroupField("LOGINID");
                    zRptUserActivity.GHUserID.GroupFields.Add(GFUserName);

                     
                    zRptUserActivity.lblGHUserID.DataBindings.Add("Text", ds, "TASK_DATE", "{0:dd/MM/yyyy}");
                    zRptUserActivity.lblTaskDate.DataBindings.Add("Text", ds, "TASK_DATE","{0:dd/MM/yyyy}");
                    zRptUserActivity.lblGHUserID.DataBindings.Add("Text", ds, "LOGINID");
                    zRptUserActivity.lblUserID.DataBindings.Add("Text", ds, "LOGINID");
                    zRptUserActivity.lblStatus.DataBindings.Add("Text", ds, "TASK_STATUS");
                    zRptUserActivity.lblTaskType.DataBindings.Add("Text", ds, "OPRA_STATUS");
                    zRptUserActivity.lblRefUID.DataBindings.Add("Text", ds, "REF_UID");
                    zRptUserActivity.lblGFTaskDate.DataBindings.Add("Text", ds, "TASK_DATE", "{0:dd/MM/yyyy}");
                    zRptUserActivity.lblGFUserID.DataBindings.Add("Text", ds, "LOGINID");

                     
                    zRptUserActivity.lblGFCount.Summary.Running = SummaryRunning.Group;
                    zRptUserActivity.lblGFCount.Summary.Func = SummaryFunc.Count;
                     
                    string zJnlDesc = "";
                    if (zSearchCriteria != "")
                        zJnlDesc = "User Activity Report Generated ( " + zSearchCriteria + " )";
                    else
                        zJnlDesc = "User Activity Report Generated";
                    m_ISMLoginInfo.AddToJournal("T", zJnlDesc, "RPT001", "", "0", "0", "0");

                     
                    Cursor.Current = Cursors.Default;
                    zRptUserActivity.ShowPreviewDialog();
                    
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "User Acticity Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                dxErrorProvider.SetError(lookUpEditOperator, null);
                dxErrorProvider.SetError(lookUpEditTaskType, null);
                dxErrorProvider.SetError(lookUpEditTaskStatus, null);

                lookUpEditOperator.EditValue = null;
                lookUpEditTaskType.EditValue = null;
                lookUpEditTaskStatus.EditValue = null;
                 
                 
                dateEditFrom.EditValue = DateTime.Today;  
                dateEditTo.EditValue = DateTime.Today;

                lookUpEditOperator.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "User Acticity Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void lookUpEditOperator_EditValueChanged(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(lookUpEditOperator, null);
        }

        private void lookUpEditTaskType_EditValueChanged(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(lookUpEditTaskType, null);
        }

        private void lookUpEditTaskStatus_EditValueChanged(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(lookUpEditTaskStatus, null);
        }

       
    }
}
