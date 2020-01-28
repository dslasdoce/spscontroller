using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AMMO_BG_DLL.Background;
using System.Net;
using System.Configuration;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ISMDAL.TableColumnName;
using ISM.Modules;
namespace SHSHQ.Modules
{
    public partial class PinningStation : UserControl
    {
        string currentStationID = "";
        string currentIPAddress = "";
        string currentCraneGroup = "";
        string currentWiFiIPAddress = "";
        bool currentMasterSlave = false;
        bool updateMode = false;
        int myHeight = 0;
        int myWidth = 0;
        Logs applicationLogs = new Logs();
        public ISMLoginInfo m_ISMLoginInfo = new ISMLoginInfo();
        string currentUser = "";

        public PinningStation(int frmHeight,int frmWidth,string currentLoggedInUser)
        {
            InitializeComponent();
            myHeight = frmHeight;
            myWidth = frmWidth;
            currentUser = currentLoggedInUser;
        }
        private void SetUpPinningStationGrid()
        {
            try
            {

                ColumnView ColView = gvPinningStation.MainView as ColumnView;

                string[] fieldNames = new string[] { "PinningStationName", "PinningStationIPAddress", "PinningStationWiFiIPAddress", "CraneGroup", "MasterSlave", "ConectionStatus" };

                DevExpress.XtraGrid.Columns.GridColumn gridColumns;
                ColView.Columns.Clear();

                for (int i = 0; i < fieldNames.Length; i++)
                {
                    gridColumns = ColView.Columns.AddField(fieldNames[i]);
                    gridColumns.VisibleIndex = i;
                }
                gridView.Columns[0].Caption = "STATION ID";
                gridView.Columns[0].Width = 80;
                gridView.Columns[0].OptionsColumn.AllowEdit = false;

                gridView.Columns[1].Caption = "LAN";
                gridView.Columns[1].Width = 80;
                gridView.Columns[1].OptionsColumn.AllowEdit = false;

                gridView.Columns[2].Caption = "Wi-Fi";
                gridView.Columns[2].Width = 80;
                gridView.Columns[2].OptionsColumn.AllowEdit = false;


                gridView.Columns[3].Caption = "CRANE GROUP";
                gridView.Columns[3].Width = 60;
                gridView.Columns[3].OptionsColumn.AllowEdit = false;

                gridView.Columns[4].Caption = "MASTER/SLAVE";
                gridView.Columns[4].Width = 60;
                gridView.Columns[4].OptionsColumn.AllowEdit = false;

                gridView.Columns[5].Caption = "STATUS";
                gridView.Columns[5].Width = 60;
                gridView.Columns[5].OptionsColumn.AllowEdit = false;

                gridView.OptionsCustomization.AllowColumnMoving = false;
                
              
            }

            catch (Exception ex)
            {
                ShowStatusMessage(string.Concat(ex.Message.ToString()," - ",ex.StackTrace.ToString()));
            }
        }
   
        private void PinningStation_Load(object sender, EventArgs e)
        {
            try
            {
                PopulateMasterSlaveList();
                SetUpPinningStationGrid();
                PopulatePinningStationList();
                this.Height = myHeight;
                pnlHeader.Width = myWidth;
                pnlHeader.Width = myWidth;

            }
            catch (Exception ex)
            {

                ShowStatusMessage(string.Concat(ex.Message.ToString()," - ",ex.StackTrace.ToString()));
            }

        }
        private void PopulatePinningStationList()
        {
            PinningStationManagement processPinningStationDetails = new PinningStationManagement();
            DataTable pinningStationDetails;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                pinningStationDetails =
                  processPinningStationDetails.ProcessPinningStationDetails(
                  processPinningStationDetails.opModeCommitSelect,"","","","","","","",false,
                  ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),"","");

                gvPinningStation.DataSource = pinningStationDetails;
                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void PopulateMasterSlaveList()
        {
            PinningStationManagement masterSlaveList = new PinningStationManagement();
            try
            {

                cmbMasterSlave.Properties.DataSource = null;
                cmbMasterSlave.Properties.Columns.Clear();

                cmbMasterSlave.Properties.DataSource = masterSlaveList.GetMasterSlaveItems();
                
                cmbMasterSlave.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(masterSlaveList.fieldNameMasterSlaveDescription, 250,"Description")});


                cmbMasterSlave.Properties.DisplayMember = masterSlaveList.fieldNameMasterSlaveDescription;
                cmbMasterSlave.Properties.ValueMember = masterSlaveList.fieldNameMasterSlaveID; 
                
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        private void ShowStatusMessage(string statusMessage)
        {
            try
            {
                txtStatusMsg.Text = statusMessage;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!updateMode)
                    ValidateandSaveDetails("INSERT");
                else
                    ValidateandSaveDetails("UPDATE");

            }
            catch (Exception ex)
            {
                
                ShowStatusMessage(string.Concat(ex.Message.ToString()," - ",ex.StackTrace.ToString()));
            }
        }
        private void ValidateandSaveDetails(string mode)
        {

            PinningStationManagement processPinningStationDetails =  new PinningStationManagement();

            bool validEntries = true;
            string stationID = "";
            IPAddress address;
            string ipAddress = "";
            string wifiIPAddress = "";
            string craneGroup ="";
            string masterSlave = "";

            DataTable stationIDExist;
            DataTable ipAddressExist;
            DataTable masterStationExist;
            DataTable modeOutPut;
            DataTable wifiIPAddressExist;


            try
            {
                ResetErrorMessage();
                stationID = txtStationID.Text.Trim();
                craneGroup = txtCraneGroup.Text.Trim();

                if (cmbMasterSlave.EditValue != null)  
                    masterSlave = cmbMasterSlave.EditValue.ToString().Trim();


                if (stationID.Length == 0)
                {
                    dxErrorProvider.SetError(txtStationID, "Please enter a station ID.");
                    validEntries = false;
                }
              
                if (IPAddress.TryParse(txtIPAddress.Text.Trim(), out address) == false)
                {
                    dxErrorProvider.SetError(txtIPAddress, "Please enter a valid LAN IP Address.");
                    validEntries = false;
                }
                else
                   ipAddress = txtIPAddress.Text.Trim();

                if (IPAddress.TryParse(txtWiFiIPAddress.Text.Trim(), out address) == false)
                {
                    dxErrorProvider.SetError(txtWiFiIPAddress, "Please enter a valid Wi-Fi IP Address.");
                    validEntries = false;
                }
                else
                    wifiIPAddress = txtWiFiIPAddress.Text.Trim();


                if (craneGroup.Length == 0)
                {
                    dxErrorProvider.SetError(txtCraneGroup, "Please enter a crane group.");
                    validEntries = false;
                }

                if (masterSlave.Length == 0)
                {
                    dxErrorProvider.SetError(cmbMasterSlave, "Please select humpy type.");
                    validEntries = false;
                }

                if (masterSlave == "0")
                    masterSlave = "true";
                else
                    masterSlave = "false";

                stationIDExist = 
                processPinningStationDetails.ProcessPinningStationDetails(
                mode == "INSERT" ? processPinningStationDetails.opModeValidateInsert : processPinningStationDetails.opModeValidateUpdate,
                processPinningStationDetails.keyWordCheckStationName,stationID, currentStationID,ipAddress, currentIPAddress, craneGroup,currentCraneGroup, Convert.ToBoolean(masterSlave), 
                ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),wifiIPAddress,currentWiFiIPAddress);

                if (stationIDExist.Rows[0][0].ToString() == "1")
                {
                    dxErrorProvider.SetError(txtStationID, "Station ID already exists.");
                    validEntries = false;
                }

                ipAddressExist = 
                processPinningStationDetails.ProcessPinningStationDetails(
                mode == "INSERT" ? processPinningStationDetails.opModeValidateInsert : processPinningStationDetails.opModeValidateUpdate,
                processPinningStationDetails.keyWordCheckIPAddress, stationID, currentStationID, ipAddress, currentIPAddress, craneGroup, currentCraneGroup, Convert.ToBoolean(masterSlave),
                ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(), wifiIPAddress, currentWiFiIPAddress);

                if (ipAddressExist.Rows[0][0].ToString() == "1")
                {
                    dxErrorProvider.SetError(txtIPAddress, "LAN IP Address already exists.");
                    validEntries = false;
                }


                wifiIPAddressExist =
                processPinningStationDetails.ProcessPinningStationDetails(
                mode == "INSERT" ? processPinningStationDetails.opModeValidateInsert : processPinningStationDetails.opModeValidateUpdate,
                processPinningStationDetails.keyWordCheckWiFiIPAddress, stationID, currentStationID, ipAddress, currentIPAddress, craneGroup, currentCraneGroup, Convert.ToBoolean(masterSlave),
                ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(), wifiIPAddress, currentWiFiIPAddress);

                if (wifiIPAddressExist.Rows[0][0].ToString() == "1")
                {
                    dxErrorProvider.SetError(txtWiFiIPAddress, "Wi-Fi IP Address already exists.");
                    validEntries = false;
                }

                masterStationExist =
                processPinningStationDetails.ProcessPinningStationDetails(
                mode == "INSERT" ? processPinningStationDetails.opModeValidateInsert : processPinningStationDetails.opModeValidateUpdate,
                processPinningStationDetails.keyWordCheckIfMasterStationExists, stationID, currentStationID, ipAddress, currentIPAddress, craneGroup, currentCraneGroup, Convert.ToBoolean(masterSlave),
                ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(), wifiIPAddress, currentWiFiIPAddress);

                if (masterStationExist.Rows[0][0].ToString() == "1")
                {
                    dxErrorProvider.SetError(cmbMasterSlave, string.Concat("There is currently no master humpy defined in crane group : ", craneGroup, ". please assign this humpy as master humpy."));
                    cmbMasterSlave.ItemIndex = 0;
                    validEntries = false;
                }

                masterStationExist =
                processPinningStationDetails.ProcessPinningStationDetails(
                mode == "INSERT" ? processPinningStationDetails.opModeValidateInsert : processPinningStationDetails.opModeValidateUpdate,
                processPinningStationDetails.keyWordCheckExistingMasterStation, stationID, currentStationID, ipAddress, currentIPAddress, craneGroup, currentCraneGroup, Convert.ToBoolean(masterSlave),
                ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(), wifiIPAddress, currentWiFiIPAddress);

                if (masterStationExist.Rows[0][0].ToString() == "1")
                {
                    dxErrorProvider.SetError(cmbMasterSlave, string.Concat("There is already a master humpy defined in crane group : ", craneGroup, ". please assign this humpy as slave humpy."));
                    cmbMasterSlave.ItemIndex = 1;
                    validEntries = false;
                }

                if (validEntries == true)
                {
                    if (MessageBox.Show("Save the following to Pinning Station Details?","Confirm Action",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        modeOutPut = 
                        processPinningStationDetails.ProcessPinningStationDetails(
                        mode == "INSERT" ? processPinningStationDetails.opModeCommitInsert : processPinningStationDetails.opModeCommitUpdate,
                        "", stationID, currentStationID, ipAddress, currentIPAddress, craneGroup, currentCraneGroup, Convert.ToBoolean(masterSlave),
                        ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(), wifiIPAddress, currentWiFiIPAddress);

                        if (modeOutPut.Rows[0][0].ToString() == "1")
                        {
                           applicationLogs.InsertApplicationLogs(
                            ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                            "Station",
                            currentUser,
                            "Success",
                            mode == "INSERT" ? "Inserted" : "Updated",
                            stationID,
                            "HQ",
                            ""); 

                            MessageBox.Show("Pinning Station details successfully updated.", "Update Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            PopulatePinningStationList();
                            ResetForm();
                            ResetErrorMessage();
                            currentStationID = "";
                            currentCraneGroup = "";
                            currentIPAddress = "";
                            currentWiFiIPAddress = "";
                            currentMasterSlave = false;
                            updateMode = false;
                            EnableControls(true);
                            ShowStatusMessage("");
                            
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            } 
            finally
            { }

          

        }
        private void ResetForm()
        {
            try
            {
                txtStationID.Text = "";
                txtIPAddress.Text = "";
                txtCraneGroup.Text = "";
                txtWiFiIPAddress.Text = "";
                PopulateMasterSlaveList();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void ResetErrorMessage()
        {
            try
            {
                dxErrorProvider.SetError(txtStationID, null);
                dxErrorProvider.SetError(txtIPAddress, null);
                dxErrorProvider.SetError(txtWiFiIPAddress, null);
                dxErrorProvider.SetError(txtCraneGroup, null);
                dxErrorProvider.SetError(cmbMasterSlave, null);


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        private void cmdCancelClear_Click(object sender, EventArgs e)
        {
            ResetForm();
            ResetErrorMessage();
            currentStationID = "";
            currentCraneGroup = "";
            currentIPAddress = "";
            currentMasterSlave = false;
            updateMode = false;
            EnableControls(true);
            ShowStatusMessage("");

        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {

            PinningStationManagement processPinningStation = new PinningStationManagement();
            DataRowView selRow;
            int rowIndex = -1;

            string stationID = "";
            string craneGroup = "";
            string ipAddress  = "";
            string masterSlave = "";

            DataTable commitDeleteResult;
            try
            {
                rowIndex = GetSelectedRowIndex();
                if (rowIndex == -1)
                {
                    MessageBox.Show("No pinning station details selected.", "Unable to Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                 selRow = (DataRowView)(((GridView)gvPinningStation.MainView).GetRow(rowIndex));
                 stationID = selRow[processPinningStation.fieldColumnPinningStationName].ToString();
                 craneGroup = selRow[processPinningStation.fieldColumnCraneGroup].ToString();
                 ipAddress  = selRow[processPinningStation.fieldColumnPinningStationIPAddress].ToString();
                 masterSlave =selRow[processPinningStation.fieldColumnMasterSlave].ToString();

                if (MessageBox.Show(
                    string.Concat("Are you sure you want to delete this pinning station details?","\n",
                                  "Station ID: ", stationID.ToUpper(), "\n",
                                  "Crane Group: ", craneGroup.ToUpper(), "\n",
                                  "IP Address: ", ipAddress.ToUpper(), "\n",
                                  "Master/Slave: ", masterSlave.ToUpper(), "\n"),
                                  "Confirm Delete",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
                    return;

                commitDeleteResult = processPinningStation.ProcessPinningStationDetails
                        (
                        processPinningStation.opModeCommitDelete, "", "", stationID, "", "", "", "", false,
                        ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),"",""
                        );

                if (commitDeleteResult.Rows[0][0].ToString() == "1")
                { 
                    MessageBox.Show("Pinning Station details successfully deleted.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopulatePinningStationList();
                    ResetErrorMessage();
                    ShowStatusMessage("");
                }

            }
            catch (Exception ex)
            {

                ShowStatusMessage(string.Concat(ex.Message.ToString(), " - ", ex.StackTrace.ToString()));
            }

        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            PinningStationManagement processPinningStation = new PinningStationManagement();
            DataRowView selRow;
            int rowIndex = -1;

            try
            {
                rowIndex = GetSelectedRowIndex();
                if (rowIndex == -1)
                {
                    MessageBox.Show("No pinning station details selected.", "Unable to Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                selRow = (DataRowView)(((GridView)gvPinningStation.MainView).GetRow(rowIndex));
                currentStationID = selRow[processPinningStation.fieldColumnPinningStationName].ToString();
                currentCraneGroup = selRow[processPinningStation.fieldColumnCraneGroup].ToString();
                currentIPAddress = selRow[processPinningStation.fieldColumnPinningStationIPAddress].ToString();
                currentWiFiIPAddress = selRow[processPinningStation.fieldColumnPinningStationWiFiIPAddress].ToString();
                currentMasterSlave = selRow[processPinningStation.fieldColumnMasterSlave].ToString() == "MASTER" ? true:false;

                txtStationID.Text = currentStationID;
                txtCraneGroup.Text = currentCraneGroup;
                txtIPAddress.Text = currentIPAddress;
                cmbMasterSlave.ItemIndex = currentMasterSlave == true ? 0 : 1;
                txtWiFiIPAddress.Text = currentWiFiIPAddress;

                ResetErrorMessage();
                ShowStatusMessage("Update Details Mode.");
                EnableControls(false);
                updateMode = true;


            }
            catch (Exception ex)
            {

                ShowStatusMessage(string.Concat(ex.Message.ToString(), " - ", ex.StackTrace.ToString()));
            }
        }
        private void EnableControls(bool enabledControl)
        {
            cmdUpdate.Enabled = enabledControl;
            cmdDelete.Enabled = enabledControl;
        }
        private int GetSelectedRowIndex()
        {
            int rowIndex = -1;
            int[] selRows; 
         
            try
            {
                
                selRows = ((GridView)gvPinningStation.MainView).GetSelectedRows();

                if (selRows.Length > 0)
                    rowIndex = selRows[0];
              
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return rowIndex;
        }
    }
}
