 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
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
    public partial class RptFrmIssue : ISMBaseWorkSpace
    {
        public RptFrmIssue(ISMLoginInfo AISMLoginInfo)
            : base(AISMLoginInfo)
        {
            InitializeComponent();
        }

        private void RptFrmIssue_Load(object sender, EventArgs e)
        {
            try
            {
                SetLookUpEditCaption();
                LoadIssuesReportMetaData();
                dateEditFrom.EditValue = DateTime.Today;  
                dateEditTo.EditValue = DateTime.Today;


            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Issues Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }

        private void SetLookUpEditCaption()
        {
            try
            {
                lookUpEditIssuedBy.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLogonID, 120, "User ID"), 
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserFirstName, 150,"First Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLastName, 150,"Last Name")});

                lookUpEditIssuedBy.Properties.DisplayMember = ISMUser.UserLogonID;
                lookUpEditIssuedBy.Properties.ValueMember = ISMUser.UserID;

                 
                lookUpEditCategory.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMCategory.CategoryCode, 100,"Code"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMCategory.CategoryDesc, 350, "Description")});

                 
                lookUpEditCategory.Properties.DisplayMember = ISMCategory.CategoryCode;
                lookUpEditCategory.Properties.ValueMember = ISMCategory.CategoryCode;


            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Issues Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void LoadIssuesReportMetaData()
        {
            try
            {
                DataSet ds = m_ISMLoginInfo.ISMServer.GetTaskMonitorMetaData();
                if (ds != null)
                {
                    lookUpEditIssuedBy.Properties.DataSource = ds.Tables[3].DefaultView;
                    lookUpEditIssuedBy.Focus();
                }
                 
                DataSet zPriorityCategoryCode = null;
                zPriorityCategoryCode = m_ISMLoginInfo.ISMServer.GetStockCategory();
                if (zPriorityCategoryCode.Tables[1].Rows.Count > 0)
                {
                    zPriorityCategoryCode.Tables[0].Rows.Add("-----", "------------------------------------------");
                    zPriorityCategoryCode.Tables[0].AcceptChanges();
                }
                foreach (DataRow dr in zPriorityCategoryCode.Tables[1].Rows)
                {
                    zPriorityCategoryCode.Tables[0].Rows.Add(dr.ItemArray);
                    zPriorityCategoryCode.Tables[0].AcceptChanges();
                }
                lookUpEditCategory.Properties.DataSource = zPriorityCategoryCode.Tables[0].DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "User Acticity Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private bool Validation()
        {
            bool zResult = false;
            bool zValidationFail = true;

            try
            {
                dxErrorProvider.SetError(dateEditFrom, null);
                dxErrorProvider.SetError(dateEditTo, null);
                dxErrorProvider.SetError(lookUpEditIssuedBy, null);
                dxErrorProvider.SetError(lookUpEditCategory, null);  

                 
                if (lookUpEditIssuedBy.EditValue == null && dateEditFrom.Text.Trim() == "" && dateEditTo.Text.Trim() == "" && lookUpEditCategory.EditValue == null)
                {
                    dxErrorProvider.SetError(lookUpEditIssuedBy, "Select a Issued By User ID");
                    dxErrorProvider.SetError(dateEditFrom, "Select a Issued From Date");
                    dxErrorProvider.SetError(dateEditTo, "Select a Issued To Date");
                    dxErrorProvider.SetError(lookUpEditCategory, "Select a Category");  
                    lookUpEditIssuedBy.Focus();
                    zValidationFail = false;
                }
                if (dateEditTo.Text.Trim() != "")
                {
                    if (dateEditFrom.Text.Trim() == "")
                    {
                        dxErrorProvider.SetError(dateEditFrom, "Select a Issued From Date");
                        dateEditFrom.Focus();
                        zValidationFail = false;
                    }
                    else if (dateEditFrom.Text.Trim() != "")
                    {
                        if (DateTime.Parse(dateEditFrom.Text) > DateTime.Parse(dateEditTo.Text))
                        {
                            dxErrorProvider.SetError(dateEditTo, "To Date should be greater than From Date");
                            dateEditTo.Focus();
                            zValidationFail = false;
                        }
                    }
                }
                if (dateEditFrom.Text.Trim() != "")
                {
                    if (dateEditTo.Text.Trim() == "")
                    {
                        dxErrorProvider.SetError(dateEditTo, "Select a Issued To Date");
                        dateEditTo.Focus();
                        zValidationFail = false;
                    }
                }
                if (lookUpEditCategory.EditValue != null)  
                {
                    if (lookUpEditCategory.EditValue.ToString() == "-----")
                    {
                        dxErrorProvider.SetError(lookUpEditCategory, "Invalid Category");
                        lookUpEditCategory.Focus();
                        zValidationFail = false;
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
        private void lookUpEditIssuedBy_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dxErrorProvider.SetError(lookUpEditIssuedBy, null);
                dxErrorProvider.SetError(lookUpEditCategory, null);  
                dxErrorProvider.SetError(dateEditFrom, null);  
                dxErrorProvider.SetError(dateEditTo, null);  

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "User Acticity Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
         
        private void lookUpEditCategory_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dxErrorProvider.SetError(lookUpEditCategory, null);
                 
                dxErrorProvider.SetError(lookUpEditIssuedBy, null);
                dxErrorProvider.SetError(dateEditFrom, null);
                dxErrorProvider.SetError(dateEditTo, null);

                if (lookUpEditCategory.EditValue != null)
                {
                    if (lookUpEditCategory.EditValue.ToString() == "-----")
                        dxErrorProvider.SetError(lookUpEditCategory, "Invalid Category");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Receive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void dateEditFrom_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dxErrorProvider.SetError(lookUpEditIssuedBy, null);
                dxErrorProvider.SetError(dateEditFrom, null);
                dxErrorProvider.SetError(dateEditTo, null);
                dxErrorProvider.SetError(lookUpEditCategory, null);

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "User Acticity Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dateEditTo_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dxErrorProvider.SetError(lookUpEditIssuedBy, null);
                dxErrorProvider.SetError(dateEditFrom, null);
                dxErrorProvider.SetError(dateEditTo, null);
                dxErrorProvider.SetError(lookUpEditCategory, null);  

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "User Acticity Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                dxErrorProvider.SetError(dateEditFrom, null);
                dxErrorProvider.SetError(dateEditTo, null);
                dxErrorProvider.SetError(lookUpEditIssuedBy, null);
                dxErrorProvider.SetError(lookUpEditCategory, null);  
                lookUpEditCategory.EditValue = null;
                lookUpEditIssuedBy.EditValue = null;
                 
                 
                dateEditFrom.EditValue = DateTime.Today;  
                dateEditTo.EditValue = DateTime.Today;

                lookUpEditIssuedBy.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "User Acticity Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    Cursor.Current = Cursors.WaitCursor;
                    string zStartDate = "";
                    string zEndDate = "";
                    string zIssuedBy = "";
                    string zSearchCriteria = "";
                    string zCatCode = "";
                    int zMode = 0;
                    if (dateEditFrom.Text.Trim() != "" && dateEditTo.Text.Trim() != "" && lookUpEditIssuedBy.Text.Trim() == "")
                        zMode = 0;
                    else if (dateEditFrom.Text.Trim() == "" && dateEditTo.Text.Trim() == "" && lookUpEditIssuedBy.Text.Trim() != "")
                    {
                        zMode = 1;
                    }
                    else if (dateEditFrom.Text.Trim() != "" && dateEditTo.Text.Trim() != "" && lookUpEditIssuedBy.Text.Trim() != "")
                        zMode = 2;
                    if (lookUpEditIssuedBy.Text.Trim() != "")
                    {
                        zIssuedBy = lookUpEditIssuedBy.Text.ToString();
                        zSearchCriteria = "Issue By = " + zIssuedBy;
                    }
                    if (lookUpEditCategory.EditValue != null)  
                    {
                        zCatCode = lookUpEditCategory.EditValue.ToString();
                        if (zSearchCriteria.Trim() != "")
                        {
                            zSearchCriteria += ", " + "Category = " + zCatCode;
                        }
                        else
                        {
                             
                            zSearchCriteria += "Category = " + zCatCode;
                        }
                    }
                    if (dateEditFrom.Text.Trim() != "")
                    {
                        zStartDate = dateEditFrom.Text.Trim();
                        if (zSearchCriteria.Trim() != "")
                        {
                            zSearchCriteria += ", " + "Issued From Date = " + zStartDate;
                        }
                        else
                        {
                            zSearchCriteria += "Issued From Date  = " + zStartDate;
                        }
                    }
                    if (dateEditTo.Text.Trim() != "")
                    {
                        zEndDate = dateEditTo.Text.Trim();
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "Issued To Date = " + zEndDate;
                        else
                            zSearchCriteria += "Issued To Date  = " + zEndDate;
                    }
                    if (dateEditFrom.Text.Trim() != "" && dateEditTo.Text.Trim() == "")
                    {
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "To Date = " + dateEditTo.Text.Trim();
                        else
                            zSearchCriteria += "To Date  = " + dateEditTo.Text.Trim();
                    }
                    

                    if (zStartDate == "")
                        zStartDate = DateTime.Now.ToString();
                    if (zEndDate == "")
                        zEndDate = DateTime.Now.ToString();

                    RptIssue zRptIssue = new RptIssue();
                    zRptIssue.lblUserName.Text = m_ISMLoginInfo.LogonID;
                    DataSet ds = m_ISMLoginInfo.ISMServer.GetRptIssuesData(zMode, zStartDate, zEndDate, zIssuedBy, zCatCode);

                     
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        MessageBox.Show("Search criteria netted no results", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }

                     
                    zRptIssue.DataSource = ds;
                    zRptIssue.DataMember = ds.Tables[0].TableName;
                     
                    zRptIssue.lblRptRecCount.Text = "No Of Issues : " + ds.Tables[0].Rows.Count;
                    if (zSearchCriteria.Trim() != "")
                        zRptIssue.lblSearch.Text = zSearchCriteria;
                    else
                        zRptIssue.lblSearch.Text = "";

                     
                    GroupField GFIssueDate = new GroupField();
                    GFIssueDate.FieldName = "IssueDT";
                    GFIssueDate.SortOrder = XRColumnSortOrder.Ascending;
                     
                    zRptIssue.Detail.SortFields.Add(GFIssueDate);


                     
                    zRptIssue.lblStockCode.DataBindings.Add("Text", ds, "StockCode");
                    zRptIssue.lblShortName.DataBindings.Add("Text", ds, "ShortName");
                    zRptIssue.lblIssueDate.DataBindings.Add("Text", ds, "IssueDT", "{0:dd/MM/yyyy hh:mm:ss}");
                    zRptIssue.lblItemUID.DataBindings.Add("Text", ds, "ItemUID");
                    zRptIssue.lblParentLocUID.DataBindings.Add("Text", ds, "LocationUID");
                    zRptIssue.lblLocDesc.DataBindings.Add("Text", ds, "FixedLocDescrip");
                    zRptIssue.lblIssueQty.DataBindings.Add("Text", ds, "IssueQty", "{0:#,#}");
                    zRptIssue.lblSerialNo.DataBindings.Add("Text", ds, "SerialNo");
                    zRptIssue.lblBatchLotNo.DataBindings.Add("Text", ds, "BatchLotNo");
                    zRptIssue.lblIssueBy.DataBindings.Add("Text", ds, "IssuedBy");
                    zRptIssue.lblCategory.DataBindings.Add("Text", ds, "CATCODE");  
                    
                     
                    string zJnlDesc = "";
                    if (zSearchCriteria != "")
                        zJnlDesc = "Issue (Picks) Report Generated ( " + zSearchCriteria + " )";
                    else
                        zJnlDesc = "Issue (Picks) Report Generated";
                    m_ISMLoginInfo.AddToJournal("T", zJnlDesc, "RPT001", "", "0", "0", "0");

                     
                    Cursor.Current = Cursors.Default;
                    zRptIssue.ShowPreviewDialog();

                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "User Acticity Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
       

        

    }
}
