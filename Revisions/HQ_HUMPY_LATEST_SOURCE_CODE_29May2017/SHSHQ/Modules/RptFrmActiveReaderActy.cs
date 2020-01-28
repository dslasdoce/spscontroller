 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using ISMDAL.TableColumnName;
using ISM.Reports;
using DevExpress.XtraReports.UI;

namespace ISM.Modules
{
    public partial class RptFrmActiveReaderActy : ISMBaseWorkSpace
    {
        public RptFrmActiveReaderActy(ISMLoginInfo AISMLoginInfo)
            : base(AISMLoginInfo)
        {
            InitializeComponent();
        }

        #region "Form Load"

        private void RptFrmActiveReaderActy_Load(object sender, EventArgs e)
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
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LOCATION_UID", 90, "Location UID"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.LocationCode, 120,"Location Code"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPDIST, 60, "ERP DIST"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPWHS, 60, "ERP WHS"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPGRID, 60, "ERP GRID"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPBIN, 60, "ERP BIN")});

                lookUpEditLocationUID.Properties.DisplayMember = "LOCATION_UID";
                lookUpEditLocationUID.Properties.ValueMember = ISMLocation.LocationUID;

                lookUpEditPortal.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalName, 90, "Portal Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.Description, 120,"Description")});

                lookUpEditPortal.Properties.DisplayMember = ISMPortal.PortalName;
                lookUpEditPortal.Properties.ValueMember = ISMPortal.PortalName;

                lookUpEditReader.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.ReaderName, 90, "Reader Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.Description, 120,"Description")});

                lookUpEditReader.Properties.DisplayMember = ISMReaders.ReaderName;
                lookUpEditReader.Properties.ValueMember = ISMReaders.ReaderName;
            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Active Reader Activity Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void LoadMetaData()
        {
            try
            {
                 
                DataSet ds = m_ISMLoginInfo.ISMServer.GetRptActiveReaderMetaData(0,"","");
                if (ds != null)
                {
                    lookUpEditLocationUID.Properties.DataSource = ds.Tables[0].DefaultView;
                    lookUpEditPortal.Properties.DataSource = ds.Tables[1].DefaultView;
                    lookUpEditReader.Properties.DataSource = ds.Tables[2].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Active Reader Activity Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lookUpEditPortal_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lookUpEditPortal != null)
                {
                    if (lookUpEditReader.Text.Trim() == "")
                    {
                        lookUpEditReader.EditValueChanged -= new System.EventHandler(lookUpEditReader_EditValueChanged);
                         
                        DataSet ds = m_ISMLoginInfo.ISMServer.GetRptActiveReaderMetaData(1, lookUpEditPortal.Text.Trim(), "");
                        if (ds != null)
                        {
                            lookUpEditReader.Properties.DataSource = ds.Tables[0].DefaultView;
                        }
                        lookUpEditReader.EditValueChanged += new System.EventHandler(lookUpEditReader_EditValueChanged);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Active Reader Activity Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lookUpEditReader_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lookUpEditReader != null)
                {
                    if (lookUpEditPortal.Text.Trim() == "")
                    {
                        lookUpEditPortal.EditValueChanged -= new System.EventHandler(lookUpEditPortal_EditValueChanged);
                         
                        DataSet ds = m_ISMLoginInfo.ISMServer.GetRptActiveReaderMetaData(2, "", lookUpEditReader.Text.Trim());
                        if (ds != null)
                        {
                            lookUpEditPortal.Properties.DataSource = ds.Tables[0].DefaultView;
                        }
                        lookUpEditPortal.EditValueChanged += new System.EventHandler(lookUpEditPortal_EditValueChanged);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Active Reader Activity Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Active Reader Activity Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    int zMode = 0;
                    string zStartDate = "";
                    string zEndDate = "";
                    string zLocationUID = "";
                    string zPortalName = "";
                    string zReaderName = "";
                    string zSearchCriteria = "";
                    if (dateEditFrom.Text.Trim() != "" && dateEditTo.Text.Trim() != "")
                    {
                        zMode = 2;
                        zStartDate = dateEditFrom.Text;
                        zEndDate = dateEditTo.Text;
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
                    if (lookUpEditLocationUID.Text.Trim() != "")
                    {
                        zSearchCriteria = "Location UID = " + lookUpEditLocationUID.Text.Trim();
                        zLocationUID = lookUpEditLocationUID.EditValue.ToString();
                    }
                    if (lookUpEditPortal.Text.Trim() != "")
                    {
                        zPortalName = lookUpEditPortal.Text.Trim();
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "Portal Name = " + lookUpEditPortal.Text.Trim();
                        else
                            zSearchCriteria += "Portal Name  = " + lookUpEditPortal.Text.Trim();
                    }
                    if (lookUpEditReader.Text.Trim() != "")
                    {
                        zReaderName = lookUpEditReader.Text.Trim();
                        if (zSearchCriteria.Trim() != "")
                            zSearchCriteria += ", " + "Reader Name = " + lookUpEditReader.Text.Trim();
                        else
                            zSearchCriteria += "Reader Name  = " + lookUpEditReader.Text.Trim();
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

                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     

                     
                     
                     
                     
                     


                     
                     

                     
                     

                     
                     
                     
                     
                     
                     
                     

                     
                     
                     
                     

                     
                     
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Active Reader Activity Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                dateEditFrom.EditValue = null;
                dateEditTo.EditValue = null;
                lookUpEditReader.EditValue = null;
                lookUpEditPortal.EditValue = null;
                LoadMetaData();
                lookUpEditLocationUID.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Active Reader Activity Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
