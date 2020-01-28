 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ISMDAL.TableColumnName;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace ISM.Modules
{
    public partial class TaskMgnt : ISMBaseWorkSpace
    {
        #region "Private Variable Declaration"

        private string m_TaskType = "";
        private string m_TaskStatus = "";
        private string m_OperatorID = "";
        private bool m_IsEditProgress = false;
        private bool m_RecSelected = false;
        private string m_TaskStatusCode = "";
        private string m_AssignDate = "";
        
        #endregion

        public TaskMgnt(ISMLoginInfo AISMLoginInfo)
            : base(AISMLoginInfo)
        {
            InitializeComponent();
        }

        private void TaskMgnt_Load(object sender, EventArgs e)
        {
            try
            {
                SetLookUpEditCaption();
                SetGridCaption();
                LoadTaskMonitorMetaData();
                btnClear.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

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

                lookUpEditAssignee.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLogonID, 120, "User ID"), 
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserFirstName, 150,"First Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLastName, 150,"Last Name")});


                lookUpEditNewAssignee.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLogonID, 120, "User ID"), 
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserFirstName, 150,"First Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLastName, 150,"Last Name")});



                lookUpEditTaskType.Properties.DisplayMember = ISMOperation.Desc;
                lookUpEditTaskType.Properties.ValueMember = ISMOperation.ID;

                lookUpEditTaskStatus.Properties.DisplayMember = ISMTaskStatus.Desc;
                lookUpEditTaskStatus.Properties.ValueMember = ISMTaskStatus.Code;

                lookUpEditAssignee.Properties.DisplayMember = ISMUser.UserLogonID;
                lookUpEditAssignee.Properties.ValueMember = ISMUser.UserID;

                lookUpEditNewAssignee.Properties.DisplayMember = ISMUser.UserLogonID;
                lookUpEditNewAssignee.Properties.ValueMember = ISMUser.UserID;



            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void SetGridCaption()
        {
            try
            {
                ColumnView ColView = gvTaskManagement.MainView as ColumnView;

                
                string[] zFieldNames = new string[] { "TASK_ID", "TYPE", "TASK_TYPE", ISMTask.CreateDateTime, ISMTask.StartDateTime,  "TASK_STATUS", "ASSIGNEE", ISMTask.ItemID, "SOURCELOC_UID", "DESTLOC_UID", ISMTask.StockCode, ISMTask.StockQty, "TASK_STATUS_CODE" };


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
                 
                 
                 
                 
                 

                gridView.Columns[5].Caption = "Status";
                gridView.Columns[5].Width = 110;

                gridView.Columns[6].Caption = "User ID"; 
                gridView.Columns[6].Width = 110;

                gridView.Columns[7].Caption = "Item UID";
                gridView.Columns[7].Width = 90;

                gridView.Columns[8].Caption = "Source Loc UID";
                gridView.Columns[8].Width = 90;

                gridView.Columns[9].Caption = "Dest Loc UID";
                gridView.Columns[9].Width = 90;

                gridView.Columns[10].Caption = "Stock Code"; 
                gridView.Columns[10].Width = 110;

                gridView.Columns[11].Caption = "Quantity";
                gridView.Columns[11].Width = 90;

                gridView.Columns[12].Caption = "Quantity";
                gridView.Columns[12].Width = 0;
                gridView.Columns[12].Visible = false;

            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion
        #region "Load Meta Data"

        private void LoadTaskMonitorMetaData()
        {
            try
            {
                DataSet ds = m_ISMLoginInfo.ISMServer.GetTaskManagementMetaData();
                if (ds != null)
                {
                    lookUpEditTaskType.Properties.DataSource = ds.Tables[0].DefaultView;
                    lookUpEditTaskStatus.Properties.DataSource = ds.Tables[1].DefaultView;
                    lookUpEditAssignee.Properties.DataSource = ds.Tables[2].DefaultView;
                    lookUpEditNewAssignee.Properties.DataSource = ds.Tables[3].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region "Edit Value Change Event"
        private void lookUpEditTaskType_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lookUpEditTaskType.Text.Trim() != "")
                    m_TaskType = lookUpEditTaskType.EditValue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return zResult;
        }

        #endregion

        #region "Report Button Event"

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateEditStatus();
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
                        zMode = 1;
                        zEndDate = DateTime.Now.ToString();
                        zStartDate = dateEditFrom.Text.Trim();
                    }
                    else if (dateEditFrom.Text.Trim() == "" && dateEditTo.Text.Trim() == "")
                    {
                        zMode = 0;
                        zStartDate = DateTime.Now.ToString();
                        zEndDate = DateTime.Now.ToString();

                    }

                    DataSet ds = m_ISMLoginInfo.ISMServer.GetTaskManagementReportData(zMode, m_TaskType, m_TaskStatus, m_OperatorID, zStartDate, zEndDate);
                    if (ds != null)
                    {
                        gvTaskManagement.DataSource = ds.Tables[0].DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        #endregion

        #region "Report Clear Button"
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateEditStatus();
                m_IsEditProgress = false;
                 
                m_ISMLoginInfo.TaskEditProgress = false;
                m_ISMLoginInfo.TaskEditID = 0;
                m_ISMLoginInfo.TaskStatusCode = "";
                m_ISMLoginInfo.TaskStatus = "";
                 

                m_TaskType = "";
                m_TaskStatus = "";
                m_OperatorID = "";
                lookUpEditTaskType.EditValue = null;
                lookUpEditTaskStatus.EditValue = null;
                lookUpEditAssignee.EditValue = null;
                dateEditFrom.EditValue = null;
                dateEditTo.EditValue = null;
                dxErrorProvider.SetError(dateEditFrom, null);
                dxErrorProvider.SetError(dateEditTo, null);
                gvTaskManagement.DataSource = null;

                lblTaskID.Text = "";
                lblOperationType.Text = "";
                lblAssignedDate.Text = "";
                lblStatus.Text = "";
                lblUserID.Text = "";
                lblItemUID.Text = "";
                lblSourceLocUID.Text = "";
                lblDestLocUID.Text = "";
                lblStockCode.Text = "";
                lblQuantity.Text = "";
                lookUpEditTaskType.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion 

        #region "Grid Click Event"

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                UpdateEditStatus();  
                btnTaskClear.PerformClick(); 
                int zMode = 0;  
                long zTaskID = 0;
                GridView zView = gridView;
                if (gridView.RowCount > 0)  
                {
                    if (zView.GetSelectedRows().Length > 0)
                    {
                        DataRow zDataRow = zView.GetDataRow(zView.GetSelectedRows()[0]);
                        lblTaskID.Text = zDataRow["TASK_ID"].ToString();
                        if (lblTaskID.Text.Trim() != "")
                            zTaskID = long.Parse(lblTaskID.Text);
                        if (m_ISMLoginInfo.ISMServer.TaskIsValid(zTaskID))
                        {
                            MessageBox.Show(String.Format("Task status changed by other user. You can't modify this record"), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            btnReport.PerformClick();
                            return;
                        }

                        lblOperationType.Text = zDataRow["TASK_TYPE"].ToString();
                        lblAssignedDate.Text = DateTime.Parse(zDataRow[ISMTask.CreateDateTime].ToString()).ToString(m_ISMLoginInfo.Params.DateTimeFormat);
                         
                        m_AssignDate = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(zDataRow[ISMTask.CreateDateTime].ToString()));
                        lblStatus.Text = zDataRow["TASK_STATUS"].ToString();
                        lblUserID.Text = zDataRow["ASSIGNEE"].ToString();
                        lblItemUID.Text = zDataRow[ISMTask.ItemID].ToString();
                        lblSourceLocUID.Text = zDataRow["SOURCELOC_UID"].ToString();
                        lblDestLocUID.Text = zDataRow["DESTLOC_UID"].ToString();
                        lblStockCode.Text = zDataRow[ISMTask.StockCode].ToString();
                        lblQuantity.Text = zDataRow[ISMTask.StockQty].ToString();
                        m_TaskStatusCode = zDataRow["TASK_STATUS_CODE"].ToString();
                        m_ISMLoginInfo.ISMServer.UpdateTaskManagementStatus(zTaskID, m_TaskStatusCode, "Task Edit", m_ISMLoginInfo.UserID, DateTime.Now, zMode);
                        m_IsEditProgress = true;
                        m_RecSelected = true;
                         
                         
                        m_ISMLoginInfo.TaskEditProgress = true;
                        m_ISMLoginInfo.TaskEditID = zTaskID;
                        m_ISMLoginInfo.TaskStatusCode = m_TaskStatusCode;
                        m_ISMLoginInfo.TaskStatus = zDataRow["TASK_STATUS"].ToString();
                         
                         
                         
                        int zMod = 0;
                        DataSet ds = null;
                        if (lblOperationType.Text == "Mobile Receive")
                        {
                            zMod = 8;  
                        }
                        else if (lblOperationType.Text == "Mobile Issue")
                        {
                            zMod = 9;  
                        }
                        else if (lblOperationType.Text == "Mobile Item Move")
                        {
                            zMod = 10;  
                        }
                        else if (lblOperationType.Text == "Mobile Loc Move")
                        {
                            zMod = 10;  
                        }
                        else if (lblOperationType.Text == "Mobile Stocktake")
                        {
                            zMod = 11;  
                        }
                        else if (lblOperationType.Text == "Mobile Item Trace")
                        {
                            zMod = 12;  
                        }

                        ds = m_ISMLoginInfo.ISMServer.GetISMOperatorID(zMod, m_ISMLoginInfo.UserProfileCode, m_ISMLoginInfo.UserID);
                        {
                            lookUpEditNewAssignee.Properties.DataSource = ds.Tables[0].DefaultView;
                        }

                         
                         
                         
                         
                         
                         
                         
                         
                         
                         
                         
                         
                         

                         
                         
                         
                         
                         
                         
                         
                         
                         
                         
                         
                    }
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region "UpdateEditStatus
        private void UpdateEditStatus()
        {
            try
            {
                if (m_IsEditProgress)
                {
                    long zTaskID = 0;
                    if (lblTaskID.Text.Trim() != "")
                        zTaskID = long.Parse(lblTaskID.Text);

                    m_ISMLoginInfo.ISMServer.UpdateTaskManagementStatus(zTaskID, m_TaskStatusCode, lblStatus.Text.Trim(), m_ISMLoginInfo.UserID, DateTime.Now, 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        #endregion

        #region "Form Unload"
        private void TaskMgnt_Leave(object sender, EventArgs e)
        {
            try
            {
                UpdateEditStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        #endregion

        #region "Task Validataion
        private void ClearErrorIconText()
        {
            dxErrorProvider.SetError(lookUpEditNewAssignee, null);
            dxErrorProvider.SetError(dateAssigned, null);
            dxErrorProvider.SetError(rdoTaskStatus, null);

        }
        private bool TaskValidation()
        {
            bool zResult = false;
            bool zValidationFail = true;

            try
            {
                ClearErrorIconText();
                if (!m_RecSelected)
                {
                     
                     
                    MessageBox.Show(String.Format("Please select a record from the grid for editing"), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    zValidationFail = false;

                }
                if (rdoTaskStatus.EditValue == null)
                {
                    dxErrorProvider.SetError(rdoTaskStatus, "Select a Task Status");
                    zValidationFail = false;
                }
                else
                {
                    if(rdoTaskStatus.EditValue.ToString() == "ASSIGN")
                    {
                        if (dateAssigned.Text == "")
                        {
                            dxErrorProvider.SetError(dateAssigned, "Select a Task Assign Date");
                            dateAssigned.Focus();
                            zValidationFail = false;
                        }
                        else
                        {
                            if (dateAssigned.Text != m_AssignDate)
                            {
                              if (DateTime.Parse(dateAssigned.EditValue.ToString()) < DateTime.Today)
                              {
                                  dxErrorProvider.SetError(dateAssigned, "Enter current date or future Date");
                                  dateAssigned.Focus();
                                  zValidationFail = false;
                              }
                            }

                        }
                        if (lookUpEditNewAssignee.EditValue == null)
                        {
                            dxErrorProvider.SetError(lookUpEditNewAssignee, "Select a User ID for reassign Task");
                            lookUpEditNewAssignee.Focus();
                            zValidationFail = false;
                        }
                        if (lblStatus.Text == "Assigned")
                        {
                            if (lookUpEditNewAssignee.Text == lblUserID.Text && dateAssigned.Text == m_AssignDate)
                            {
                                dxErrorProvider.SetError(lookUpEditNewAssignee, "Existing User ID and new assigned User ID can’t be same");
                                dxErrorProvider.SetError(dateAssigned, "Existing assigned Date and new assigned Date can’t be same");
                                lookUpEditNewAssignee.Focus();
                                zValidationFail = false;
                            }
                        }
                    }

                }
                if (zValidationFail)
                    zResult = zValidationFail;

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return zResult;
        }

        #endregion

        #region "Task Assign or Cancel Button"
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (TaskValidation())
                {
                    long zTaskID = 0;
                    string zStatusCode = "";
                    string zStatus = "";
                    string zMessage = "";
                    string zConMessage = "";

                    if (lblTaskID.Text.Trim() != "")
                        zTaskID = long.Parse(lblTaskID.Text);

                    if (rdoTaskStatus.EditValue.ToString() == "ASSIGN")
                    {
                        long zOperatorID = long.Parse(lookUpEditNewAssignee.EditValue.ToString());
                        zMessage = String.Format("Do you want reassign Task ID : {0} and Task : {1} to User ID : {2}?", lblTaskID.Text,lblOperationType.Text, lookUpEditNewAssignee.Text);
                        if(dateAssigned.Text == m_AssignDate)
                            zConMessage = string.Format("Task ID : {0} and Task : {1} has been reassigned to User ID : {2}", lblTaskID.Text, lblOperationType.Text, lookUpEditNewAssignee.Text);
                        else
                            zConMessage = string.Format("Task ID : {0} and Task : {1} has been reassigned to User ID : {2} with new assigned Date {3}", lblTaskID.Text, 
                                                                            lblOperationType.Text, lookUpEditNewAssignee.Text,dateAssigned.Text);
                        DialogResult zReply = MessageBox.Show(zMessage, "Task Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (zReply == DialogResult.Yes)
                        {
                            zStatusCode = "1";
                            zStatus = "Assigned";
                            DateTime zAssingedDate = DateTime.Parse(dateAssigned.EditValue.ToString());
                            DateTime zAssignDateTime = new DateTime(zAssingedDate.Year, zAssingedDate.Month, zAssingedDate.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                             
                            
                            m_ISMLoginInfo.ISMServer.UpdateTaskManagementStatus(zTaskID, zStatusCode, zStatus, zOperatorID, zAssignDateTime, 2);
                            m_IsEditProgress = false;
                             
                            m_ISMLoginInfo.TaskEditProgress = false;
                            m_ISMLoginInfo.TaskEditID = 0;
                            m_ISMLoginInfo.TaskStatusCode = "";
                            m_ISMLoginInfo.TaskStatus = "";

                             
                            btnTaskClear.PerformClick();
                            btnReport.PerformClick();
                            txtStatusMsg.Text = zConMessage;
                        }
                    }
                    else
                    {
                        zMessage = String.Format("Do you want cancel Task ID : {0} for the Task : {1}?", lblTaskID.Text, lblOperationType.Text);
                        zConMessage = string.Format("Task ID : {0} and Task : {1} has been Cancelled", lblTaskID.Text, lblOperationType.Text);
                        DialogResult zReply = MessageBox.Show(zMessage, "Task Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (zReply == DialogResult.Yes)
                        {
                            zStatusCode = "5";
                            zStatus = "Cancelled";
                            m_ISMLoginInfo.ISMServer.UpdateTaskManagementStatus(zTaskID, zStatusCode, zStatus, m_ISMLoginInfo.UserID, DateTime.Now, 3);
                            m_IsEditProgress = false;
                             
                            m_ISMLoginInfo.TaskEditProgress = false;
                            m_ISMLoginInfo.TaskEditID = 0;
                            m_ISMLoginInfo.TaskStatusCode = "";
                            m_ISMLoginInfo.TaskStatus = "";
                             
                            btnTaskClear.PerformClick();
                            btnReport.PerformClick();
                            txtStatusMsg.Text = zConMessage;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        #endregion

        #region "Task Clear Button"
        private void btnTaskClear_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateEditStatus();
                m_IsEditProgress = false;
                 
                m_ISMLoginInfo.TaskEditProgress = false;
                m_ISMLoginInfo.TaskEditID = 0;
                m_ISMLoginInfo.TaskStatusCode = "";
                m_ISMLoginInfo.TaskStatus = "";
                 

                rdoTaskStatus.EditValue = null;
                rdoTaskStatus.SelectedIndex = -1;  
                lookUpEditNewAssignee.EditValue = null;
                dateAssigned.EditValue = null;
                txtStatusMsg.Text = "";
                m_AssignDate = "";
                rdoTaskStatus.Focus();
                ClearErrorIconText();
                lblTaskID.Text = "";
                lblOperationType.Text = "";
                lblAssignedDate.Text =  "";
                lblStatus.Text = "";
                lblUserID.Text = "";
                lblItemUID.Text = "";
                lblSourceLocUID.Text = "";
                lblDestLocUID.Text = "";
                lblStockCode.Text = "";
                lblQuantity.Text = "";
                m_TaskStatusCode = "";
                m_RecSelected = false;
                lookUpEditNewAssignee.Enabled = true;
                dateAssigned.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        #endregion

        #region "Edit Value Change Event"
        private void rdoTaskStatus_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dxErrorProvider.SetError(rdoTaskStatus, null);
                if (rdoTaskStatus.EditValue != null)
                {
                    if (rdoTaskStatus.EditValue.ToString() == "ASSIGN")
                    {
                        if (lblOperationType.Text.Trim() != "")
                        {
                            lookUpEditNewAssignee.Enabled = true;
                            dateAssigned.Enabled = true;
                             
                             
                             
                             
                             
                             
                             
                             
                        }
                        else  
                        {
                            MessageBox.Show(String.Format("Please select a record from the grid for editing"), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            rdoTaskStatus.EditValueChanged -= new System.EventHandler(rdoTaskStatus_EditValueChanged);
                            rdoTaskStatus.SelectedIndex = -1;
                            rdoTaskStatus.EditValueChanged += new System.EventHandler(rdoTaskStatus_EditValueChanged);
                        }
                    }
                    else
                    {
                        if (rdoTaskStatus.EditValue.ToString() == "CANCEL")  
                        {
                            if (lblOperationType.Text.Trim() != "")
                            {
                                lookUpEditNewAssignee.Enabled = false;
                                dateAssigned.Enabled = false;
                                lookUpEditNewAssignee.EditValue = null;
                                dateAssigned.EditValue = null;
                            }
                            else
                            {
                                MessageBox.Show(String.Format("Please select a record from the grid to cancel"), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                rdoTaskStatus.EditValueChanged -= new System.EventHandler(rdoTaskStatus_EditValueChanged);
                                rdoTaskStatus.SelectedIndex = -1;
                                rdoTaskStatus.EditValueChanged += new System.EventHandler(rdoTaskStatus_EditValueChanged);

                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Task Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void lookUpEditNewAssignee_EditValueChanged(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(lookUpEditNewAssignee, null);
            dxErrorProvider.SetError(dateAssigned, null);  
        }

        private void dateAssigned_EditValueChanged(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(dateAssigned, null);
            dxErrorProvider.SetError(dateAssigned, null); 
        }
        #endregion

    }
}
