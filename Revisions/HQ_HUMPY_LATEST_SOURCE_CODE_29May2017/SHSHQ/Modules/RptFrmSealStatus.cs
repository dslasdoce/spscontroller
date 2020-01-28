 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using ISMDAL.TableColumnName;
using ISM.Reports;
using DevExpress.XtraReports.UI;
namespace ISM.Modules
{
    public partial class RptFrmSealStatus : ISMBaseWorkSpace
    {
        public RptFrmSealStatus(ISMLoginInfo AISMLoginInfo)
            : base(AISMLoginInfo)
        {
            InitializeComponent();
        }
        #region "Form Load"
        private void RptFrmSealStatus_Load(object sender, EventArgs e)
        {
            SetLookUpEditCaption();
            LoadMetaData();
            lookUpEditLocationUID.Focus();

        }
        #endregion

        #region "Initialization"
        private void SetLookUpEditCaption()
        {
            try
            {

                lookUpEditLocationUID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LOCATIONUID", 90, "Location UID"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.LocationCode, 120,"Location Code"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPDIST, 60, "ERP DIST"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPWHS, 60, "ERP WHS"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPGRID, 60, "ERP GRID"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPBIN, 60, "ERP BIN")});

                lookUpEditSealStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Status", 90, "Status")});

                lookUpEditLocationUID.Properties.DisplayMember = "LOCATIONUID";
                lookUpEditLocationUID.Properties.ValueMember = ISMLocation.LocationUID;

                lookUpEditSealStatus.Properties.DisplayMember = "Status";
                lookUpEditSealStatus.Properties.ValueMember = "Status";



            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Seal Status Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void LoadMetaData()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable tblSeal = ds.Tables.Add();

                 
                tblSeal.Columns.Add("ID", typeof(int));
                tblSeal.Columns.Add("Status", typeof(string));

                 
                tblSeal.Rows.Add(0, "Broken");
                tblSeal.Rows.Add(1, "Sealed");
                tblSeal.Rows.Add(2, "None");

                lookUpEditSealStatus.Properties.DataSource = ds.Tables[0].DefaultView;

                ds = m_ISMLoginInfo.ISMServer.GetSealStatusRptMetaData();
                if (ds != null)
                {
                    lookUpEditLocationUID.Properties.DataSource = ds.Tables[0].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Seal Status Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region "Edit Control Event
        private void lookUpEditLocationUID_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lookUpEditLocationUID.EditValue != null)
                {
                    dxErrorProvider.SetError(lookUpEditLocationUID, null);
                    dxErrorProvider.SetError(lookUpEditSealStatus, null);

                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Seal Status Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        private void lookUpEditSealStatus_EditValueChanged(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(lookUpEditLocationUID, null);
            dxErrorProvider.SetError(lookUpEditSealStatus, null);
        }
        
        #endregion

        


        #region "Validation "
        private bool Validation()
        {
            bool zResult = false;
            bool zValidationFail = true;

            try
            {
                dxErrorProvider.SetError(lookUpEditLocationUID, null);
                dxErrorProvider.SetError(lookUpEditSealStatus, null);

                if (lookUpEditLocationUID.EditValue == null && lookUpEditSealStatus.EditValue == null)
                {
                    dxErrorProvider.SetError(lookUpEditLocationUID, "Select a Location UID");
                    dxErrorProvider.SetError(lookUpEditSealStatus, "Select a Seal Type");
                    lookUpEditLocationUID.Focus();
                    zValidationFail = false;
                }
                if (zValidationFail)
                    zResult = zValidationFail;

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Seal Status Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    string zLocationUID = "";
                    string zSealStatus = "";
                    string zSearchCriteria = "";
                    if (lookUpEditLocationUID.Text.Trim() != "")
                    {
                        zSearchCriteria = "Location UID = " + lookUpEditLocationUID.Text.Trim();
                        zLocationUID = lookUpEditLocationUID.EditValue.ToString();
                    }

                    if (lookUpEditSealStatus.Text.Trim() != "")
                    {
                        zSealStatus = lookUpEditSealStatus.EditValue.ToString();
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "Seal Status = " + zSealStatus;
                        else
                            zSearchCriteria += "Seal Status = " + zSealStatus;
                    }

                    RptSealStatus zRptSealStatus = new RptSealStatus();
                    zRptSealStatus.lblUserName.Text = m_ISMLoginInfo.LogonID;
                    if (zSearchCriteria.Trim() != "")
                        zRptSealStatus.lblSearch.Text = zSearchCriteria;
                    else
                        zRptSealStatus.lblSearch.Text = "";

                    DataSet ds = m_ISMLoginInfo.ISMServer.GetRptSealStatus(zLocationUID, zSealStatus);
                     
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        MessageBox.Show("Search criteria netted no results", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }

                     
                    zRptSealStatus.DataSource = ds;
                    zRptSealStatus.DataMember = ds.Tables[0].TableName;
                     
                    zRptSealStatus.lblRptRecCount.Text = "No Of Locations : " + ds.Tables[0].Rows.Count;

                     

                    zRptSealStatus.lblLocUID.DataBindings.Add("Text", ds, "LocationUID");
                    zRptSealStatus.lblStorageType.DataBindings.Add("Text", ds, "FK_LOCATION_CODE");
                    zRptSealStatus.lblSealUID.DataBindings.Add("Text", ds, "SealUID");
                    zRptSealStatus.lblSealDate.DataBindings.Add("Text", ds, "LastUpdatedDT", "{0:dd/MM/yyyy hh:mm:ss}");
                    zRptSealStatus.lblUpdatedby.DataBindings.Add("Text", ds, "LastUpdatedBy");
                    zRptSealStatus.lblSealStatus.DataBindings.Add("Text", ds, "SealStatus");
                     
                    string zJnlDesc = "";
                    if(zSearchCriteria != "")
                        zJnlDesc = "Seal Status Report Generated ( " + zSearchCriteria + " )";
                    else
                        zJnlDesc = "Seal Status Report Generated" ;
                    if(zLocationUID != "")
                        m_ISMLoginInfo.AddToJournal("T", zJnlDesc, "RPT001", "", zLocationUID, "0", "0");
                    else
                        m_ISMLoginInfo.AddToJournal("T", zJnlDesc, "RPT001", "", "0", "0", "0");

                     
                    Cursor.Current = Cursors.Default;
                    zRptSealStatus.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Seal Status Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        #endregion

        #region "Clear Button"
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                dxErrorProvider.SetError(lookUpEditLocationUID, null);
                dxErrorProvider.SetError(lookUpEditSealStatus, null);  
                lookUpEditLocationUID.EditValue = null;
                lookUpEditSealStatus.EditValue = null;
                LoadMetaData();
                lookUpEditLocationUID.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Seal Status Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        #endregion

       
        



    }
}
