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
using AMMO_BG_DLL.Background;
using System.Configuration;

namespace SHSHQ.Modules
{
    public partial class ViewLive : UserControl
    {
        int frmHeight;
        int frmWidth = 0;
        public ViewLive(int formHeight, int formWidth)
        {
            InitializeComponent();
            frmHeight = formHeight;
            frmWidth = formWidth;
        }

        private void ViewLive_Load(object sender, EventArgs e)
        {
            try
            {
               SetUpViewLiveGrid();
               PopulateLiveViewDetails();
               this.Height = frmHeight;
               pnlHeader.Width = frmWidth;
               panelFooter.Width = frmWidth;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void PopulateLiveViewDetails()
        {
            LiveView liveViewDetails = new LiveView();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                gvViewLive.DataSource = liveViewDetails.GetAllHumpyDataForLiveView(ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString());

                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void SetUpViewLiveGrid()
        {
            try
            {
                
                ColumnView ColView = gvViewLive.MainView as ColumnView;

                string[] fieldNames = new string[] 
                { 
                  "Name", "LastLocID", "CraneGroup","HQUserID", "AccessCardUID", "TIDP", "Color", 
                  "Displayed", "DateTimeExtracted", "CheckedIn", "InOut", "ConnectionString" ,"CraneDriver"
                };

                DevExpress.XtraGrid.Columns.GridColumn gridColumns;
                ColView.Columns.Clear();

                for (int i = 0; i < fieldNames.Length; i++)
                {
                    gridColumns = ColView.Columns.AddField(fieldNames[i]);
                    gridColumns.VisibleIndex = i;
                }
                gridView.Columns[0].Caption = "User";
                gridView.Columns[0].Width = Convert.ToInt32(gvViewLive.Width * .33); ;
                gridView.Columns[0].OptionsColumn.AllowEdit = false;

                gridView.Columns[1].Caption = "Station ID";
                gridView.Columns[1].Width = Convert.ToInt32(gvViewLive.Width * .33);
                gridView.Columns[1].OptionsColumn.AllowEdit = false;

                gridView.Columns[2].Caption = "Crane Group";
                gridView.Columns[2].Width = Convert.ToInt32(gvViewLive.Width * .33);
                gridView.Columns[2].OptionsColumn.AllowEdit = false;

                gridView.Columns[3].Visible = false;
                gridView.Columns[4].Visible = false;
                gridView.Columns[5].Visible = false;
                gridView.Columns[6].Visible = false;
                gridView.Columns[7].Visible = false;
                gridView.Columns[8].Visible = false;
                gridView.Columns[9].Visible = false;
                gridView.Columns[10].Visible = false;
                gridView.Columns[11].Visible = false;
                gridView.Columns[12].Visible = false;
                gridView.OptionsCustomization.AllowColumnMoving = false;


            }

            catch (Exception ex)
            {
                ShowStatusMessage(string.Concat(ex.Message.ToString(), " - ", ex.StackTrace.ToString()));
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

        private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            Color m_Color_Green = Color.FromArgb(85, 180, 26);
            Color m_Color_Red = Color.FromArgb(199, 32, 52);
            Color m_Color_Yellow = Color.FromArgb(231, 219, 19);

            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Color"]);
                if (category == "GREEN")
                    e.Appearance.BackColor = m_Color_Green;
                if (category == "RED")
                    e.Appearance.BackColor = m_Color_Red;
                if (category == "YELLOW")
                    e.Appearance.BackColor = m_Color_Yellow;

            }
        }

        private void cmdReferesh_Click(object sender, EventArgs e)
        {
            try
            {
                PopulateLiveViewDetails();
            }
            catch (Exception ex)
            {
                
                ShowStatusMessage(string.Concat(ex.Message.ToString(), " - ", ex.StackTrace.ToString()));
            }
        }
        private int GetSelectedRowIndex()
        {
            int rowIndex = -1;
            int[] selRows;

            try
            {

                selRows = ((GridView)gvViewLive.MainView).GetSelectedRows();

                if (selRows.Length > 0)
                    rowIndex = selRows[0];

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rowIndex;
        }

  

        private void cmdCheckIn_Click(object sender, EventArgs e)
        {
            LiveView manageLiveView = new LiveView();
            DataRowView selRow;
            int rowIndex = -1;

            string checkInStatus = "";
            string inOutStatus = "";
            string userName = "";
            string stationID = "";
            string craneGroup = "";
            string connectionString = "";
            int hqUserID = 0;
            bool craneDriver = false;


            try
            {
                rowIndex = GetSelectedRowIndex();
                if (rowIndex == -1)
                {
                    MessageBox.Show("No details selected.", "Unable to Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                selRow = (DataRowView)(((GridView)gvViewLive.MainView).GetRow(rowIndex));
                hqUserID = (int)selRow[manageLiveView.fieldColumnHQUserID];
                checkInStatus = selRow[manageLiveView.fieldColumnCheckedIn].ToString();
                inOutStatus = selRow[manageLiveView.fieldColumnInOut].ToString();
                userName = selRow[manageLiveView.fieldColumnName].ToString();
                stationID = selRow[manageLiveView.fieldColumnLastLocID].ToString();
                craneGroup = selRow[manageLiveView.fieldColumnCraneGroup].ToString();
                connectionString = selRow[manageLiveView.fieldColumnConnectionString].ToString();
                craneDriver = (bool)selRow[manageLiveView.fieldColumnCraneDriver];

                if (craneDriver)
                {
                    MessageBox.Show(
                                       String.Concat("Selected worker is crane driver can not be checked in. \n ",
                                       "User Name:", userName, "\n",
                                       "Station ID:", stationID, "\n",
                                       "Crane Group:", craneGroup), "Crane Driver", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                return;
                }

                if (inOutStatus.ToLower() == "true" && checkInStatus.ToLower() == "false")
                {
                    if (MessageBox.Show(
                                       String.Concat("Are you sure you want to set the status to Check-In for this user?.\n",
                                       "User Name:", userName, "\n",
                                       "Station ID:", stationID, "\n",
                                       "Crane Group:", craneGroup), "Confirm CheckIn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;

                    manageLiveView.ManageUserCheckInOutStatus(connectionString, hqUserID, checkInStatus.ToLower() == "true" ? true : false, inOutStatus.ToLower() == "true" ? true : false, "CHECK-IN");

                    MessageBox.Show("User status successfully updated.", "Check-In", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    PopulateLiveViewDetails();
                    

                }
                else
                    MessageBox.Show("User status can not be set to check-in.", "Check-In", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {

                ShowStatusMessage(string.Concat(ex.Message.ToString(), " - ", ex.StackTrace.ToString()));
            }
        }

        private void cmdCheckOut_Click(object sender, EventArgs e)
        {
            LiveView manageLiveView = new LiveView();
            DataRowView selRow;
            int rowIndex = -1;

            string checkInStatus = "";
            string inOutStatus = "";
            string userName = "";
            string stationID = "";
            string craneGroup = "";
            string connectionString = "";
            bool craneDriver = false;
            int hqUserID = 0;


            try
            {
                rowIndex = GetSelectedRowIndex();
                if (rowIndex == -1)
                {
                    MessageBox.Show("No details selected.", "Unable to Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                selRow = (DataRowView)(((GridView)gvViewLive.MainView).GetRow(rowIndex));
                hqUserID = (int)selRow[manageLiveView.fieldColumnHQUserID];
                checkInStatus = selRow[manageLiveView.fieldColumnCheckedIn].ToString();
                inOutStatus = selRow[manageLiveView.fieldColumnInOut].ToString();
                userName = selRow[manageLiveView.fieldColumnName].ToString();
                stationID = selRow[manageLiveView.fieldColumnLastLocID].ToString();
                craneGroup = selRow[manageLiveView.fieldColumnCraneGroup].ToString();
                connectionString = selRow[manageLiveView.fieldColumnConnectionString].ToString(); 
                 craneDriver = (bool)selRow[manageLiveView.fieldColumnCraneDriver];

                if (craneDriver)
                {
                    MessageBox.Show(
                                       String.Concat("Selected worker is crane driver and can not be checked out. \n ",
                                       "User Name:", userName, "\n",
                                       "Station ID:", stationID, "\n",
                                       "Crane Group:", craneGroup), "Crane Driver", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                return;
                }


                if (checkInStatus.ToLower() == "true")
                {
                    if (MessageBox.Show(
                                       String.Concat("Are you sure you want to set the status to Check-Out for this user?.\n",
                                       "User Name:", userName, "\n",
                                       "Station ID:", stationID, "\n",
                                       "Crane Group:", craneGroup), "Confirm CheckOut", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;

                    manageLiveView.ManageUserCheckInOutStatus(connectionString, hqUserID, checkInStatus.ToLower() == "true" ? true : false, inOutStatus.ToLower() == "true" ? true : false, "CHECK-OUT");

                    MessageBox.Show("User status successfully updated.", "Check-Out", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    PopulateLiveViewDetails();
                    

                }
                else
                    MessageBox.Show("User status can not be set to check-out.", "Check-Out", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {

                ShowStatusMessage(string.Concat(ex.Message.ToString(), " - ", ex.StackTrace.ToString()));
            }
        }
       
    }
}
