using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ISMDAL.TableColumnName;
using ISM.Modules;
using ISM.Class;
using AMMO_BG_DLL.Background;
using System.Configuration;

namespace SHSHQ.Modules
{
    public partial class StationLogs : UserControl
    {
        int myHeight = 0;
        int myWidth = 0;
        DataSet ds;
        public StationLogs(int frmHeight, int frmWidth,DataSet dtWorker)
        {
            InitializeComponent();
            myHeight = frmHeight;
            myWidth = frmWidth;
            ds = dtWorker;
        }

        private void StationLogs_Load(object sender, EventArgs e)
        {
            try
            {
               
                this.Height = myHeight;
                pnlHeader.Width = myWidth;
                SetUpStationLogGrid();
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
                comboBox1.Items.Add ("HQ");
                comboBox1.Items.Add ("Crane");
                comboBox1.Items.Add ("Worker");

                comboBox1.SelectedIndex = comboBox1.FindString("Worker");


               
               

            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }
        private void SetUpStationLogGrid()
        {
            try
            {

                ColumnView ColView = gvStationLogs.MainView as ColumnView;

                string[] fieldNames = new string[] { "Component", "LogID", "LogStatus", "LogAction", "LogInput", "LogTime", "LocationID" };

                DevExpress.XtraGrid.Columns.GridColumn gridColumns;
                ColView.Columns.Clear();

                for (int i = 0; i < fieldNames.Length; i++)
                {
                    gridColumns = ColView.Columns.AddField(fieldNames[i]);
                    gridColumns.VisibleIndex = i;
                }
                gridView.Columns[0].Caption = "Component";
                gridView.Columns[0].Width = Convert.ToInt32(gvStationLogs.Width * .14);
                gridView.Columns[0].OptionsColumn.AllowEdit = false;

                gridView.Columns[1].Caption = "ID";
                gridView.Columns[1].Width = Convert.ToInt32(gvStationLogs.Width * .14);
                gridView.Columns[1].OptionsColumn.AllowEdit = false;
                

                gridView.Columns[2].Caption = "Status";
                gridView.Columns[2].Width = Convert.ToInt32(gvStationLogs.Width * .14);
                gridView.Columns[2].OptionsColumn.AllowEdit = false;

                gridView.Columns[3].Caption = "Action";
                gridView.Columns[3].Width = Convert.ToInt32(gvStationLogs.Width * .14);
                gridView.Columns[3].OptionsColumn.AllowEdit = false;

                gridView.Columns[4].Caption = "Input";
                gridView.Columns[4].Width = Convert.ToInt32(gvStationLogs.Width * .14);
                gridView.Columns[4].OptionsColumn.AllowEdit = false;

                gridView.Columns[5].Caption = "Date & Time";
                gridView.Columns[5].Width = Convert.ToInt32(gvStationLogs.Width * .14);
                gridView.Columns[5].OptionsColumn.AllowEdit = false;
                gridView.Columns[5].DisplayFormat.FormatString = "MMMM dd, yyyy hh:mm tt";

                gridView.Columns[6].Caption = "Location";
                gridView.Columns[6].Width = Convert.ToInt32(gvStationLogs.Width * .14);
                gridView.Columns[6].OptionsColumn.AllowEdit = false;


                gridView.OptionsCustomization.AllowColumnMoving = false;


            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "HQ")
                luLoginId.Enabled = false;
            else
            { 
                luLoginId.Enabled = true;
                luLoginId.Properties.Columns.Clear();
                luLoginId.Properties.DataSource = null;

                if (comboBox1.Text == "Worker")
                {
                    luLoginId.Properties.NullText = "Select a worker";
                    luLoginId.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLogonID, 240, "User ID"),  
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserFirstName, 180,"First Name"),
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLastName, 180,"Last Name")});

                    luLoginId.Properties.DisplayMember = ISMUser.UserLogonID;
                    luLoginId.Properties.ValueMember = ISMUser.UserID;

                    if (ds != null)
                        luLoginId.Properties.DataSource = ds.Tables[0].DefaultView;
                }
                else if (comboBox1.Text == "Crane")
                {
                    luLoginId.Properties.NullText = "Select a crane";
                    Logs getCranes = new Logs();
                    DataTable dt = new DataTable();

                    dt = getCranes.GetAllMasterDBOfCraneGroup(ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString());

                    luLoginId.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CraneGroup", 240, "Crane"),  
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PinningStationIPAddress", 180,"IP"),
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PinningStationName", 180,"Station Name")});

                    luLoginId.Properties.DisplayMember = "CraneGroup";
                    luLoginId.Properties.ValueMember = "PinningStationIPAddress";

                    if (dt != null)
                        luLoginId.Properties.DataSource = dt.DefaultView;


                }

            }

        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            Logs appLog = new Logs();
            DataTable appDetails = new DataTable();
            string ipAddress = "";

            try
            {

                if (comboBox1.Text == "Crane")
                { 
                    if (luLoginId.ItemIndex > -1)
                        ipAddress = luLoginId.EditValue.ToString();
                    else
                    {
                        MessageBox.Show("Please select a crane", "App log", MessageBoxButtons.OK);
                        return;
                    }
                }

                this.Cursor = Cursors.WaitCursor;
                appDetails = appLog.ConsolidateApplicationLogs(string.Format("{0:MM/dd/yyyy HH:mm}", dateTimePicker1.Value), string.Format("{0:MM/dd/yyyy HH:mm}", dateTimePicker2.Value), ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(), comboBox1.Text, luLoginId.Text, ipAddress);
                if (appDetails.Rows.Count == 0)
                    MessageBox.Show("No records found on the given search criteria.", "App Logs", MessageBoxButtons.OK, MessageBoxIcon.Information);

                gvStationLogs.DataSource = appDetails;
                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
           
            }

        }
    }
}
