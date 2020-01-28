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
    public partial class UserSyncHistory : UserControl
    {
        int myHeight = 0;
        int myWidth = 0;
        public UserSyncHistory(int frmHeight, int frmWidth)
        {
            InitializeComponent();
            myHeight = frmHeight;
            myWidth = frmWidth;

        }

        private void UserSyncHistory_Load(object sender, EventArgs e)
        {
            try
            {
                dtFilter.Value = DateTime.Now;
                SetUpPinningStationGrid();
                this.Height = myHeight;
                pnlHeader.Width = myWidth;
                

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
        private void SetUpPinningStationGrid()
        {
            try
            {

                ColumnView ColView = gvSyncHistory.MainView as ColumnView;

                string[] fieldNames = new string[] { "IPAddress", "SyncDateTime", "HumpyID", "HumpyGroup" };

                DevExpress.XtraGrid.Columns.GridColumn gridColumns;
                ColView.Columns.Clear();

                for (int i = 0; i < fieldNames.Length; i++)
                {
                    gridColumns = ColView.Columns.AddField(fieldNames[i]);
                    gridColumns.VisibleIndex = i;
                }
                gridView.Columns[0].Caption = "HUMPY IP ADDRESS";
                gridView.Columns[0].Width = Convert.ToInt32(gvSyncHistory.Width * .33);
                gridView.Columns[0].OptionsColumn.AllowEdit = false;

                gridView.Columns[1].Caption = "SYNCED DATE";
                gridView.Columns[1].Width = Convert.ToInt32(gvSyncHistory.Width * .33);
                gridView.Columns[1].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView.Columns[1].DisplayFormat.FormatString = "MMMM dd, yyyy hh:mm tt";
                gridView.Columns[1].OptionsColumn.AllowEdit = false;

                gridView.Columns[2].Caption = "HUMPY ID";
                gridView.Columns[2].Width = Convert.ToInt32(gvSyncHistory.Width * .33);
                gridView.Columns[2].OptionsColumn.AllowEdit = false;

                gridView.Columns[3].Caption = "HUMPY GROUP";
                gridView.Columns[3].Width = Convert.ToInt32(gvSyncHistory.Width * .25);
                gridView.Columns[3].OptionsColumn.AllowEdit = false;
                gridView.Columns[3].Visible = false;


                gridView.OptionsCustomization.AllowColumnMoving = false;


            }

            catch (Exception ex)
            {
                ShowStatusMessage(string.Concat(ex.Message.ToString(), " - ", ex.StackTrace.ToString()));
            }
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {

            HQDataExtract syncHistory = new HQDataExtract();

            try
            {
              
                    Cursor.Current = Cursors.WaitCursor;

                    gvSyncHistory.DataSource = syncHistory.GetHQtoHumpySyncHistory
                                (ConfigurationManager.ConnectionStrings["SHSHQ.Properties.Settings.ConnectionString"].ToString(),
                                String.Format("{0:MM/dd/yyyy}", dtFilter.Value), String.Format("{0:MM/dd/yyyy}", dtFilter.Value));

                    Cursor.Current = Cursors.Default;

             
            }
            catch (Exception ex)
            {
                ShowStatusMessage(string.Concat(ex.Message.ToString(), " - ", ex.StackTrace.ToString()));
            }
        }
    }
}
