using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AMMO_BG_DLL.Background;
using System.Configuration;
using System.Diagnostics;
namespace SmartHumpyController
{
    public partial class ConnectionReminder : Form
    {
        public HumpyDetail m_HumpyDetail = new HumpyDetail();
        string connectionReminder = "";
        string issue = "";
        int timer = 0;

        int disposeTimer = 0;
        public ConnectionReminder(string connectionIssue,string Message)
        {
            issue = connectionIssue;
            connectionReminder = Message;
            InitializeComponent();
        }

        private void ConnectionReminder_Load(object sender, EventArgs e)
        {
            lblMessage.Text = connectionReminder;
        }

        private void timConnection_Tick(object sender, EventArgs e)
        {

            timer += 1;
            textBox1.Text = timer.ToString();
            if (timer > 8)
            {
                if (issue == "FDB" || issue == "FMDB" || issue == "FSANIP" || issue == "FUHF" || issue == "FHF")
                {
                //if (issue == "FDB" || issue == "FMDB")
                //    this.Close();
                //else
                //{
                    Application.Exit();
                    System.Diagnostics.Process.Start(Application.ExecutablePath);
                //}


                }
            }
            if (timer >= 600)
            {
                if (issue == "DB")
                    lblMessage.Text = "Can not establish connection with Local database.\n Please check the local database server.";

                if (issue == "MDB")
                    lblMessage.Text = "Can not establish connection with Master database.\n Please check the Master database server and ethernet connection.";

                if (issue == "SANIP")
                    lblMessage.Text = "Can not establish connection with Share Drive.\n Please contact IT Support before restarting the application.";

                if (issue == "UHF")
                    lblMessage.Text = "Can not establish connection with RFID reader.\n Please contact an electrician.";

                if (issue == "HF")
                    lblMessage.Text = "Can not establish connection with Card reader.\n Please re-attach the Tablet from the mount.";

                timConnection.Enabled = false;
                cmdRestart.Visible = true;
            }

        }

        private void cmdRestart_Click(object sender, EventArgs e)
        {
         
        }

        private void cmdRestart_Click_1(object sender, EventArgs e)
        {

            if (issue == "DB")
                MessageBox.Show("Please make sure that the local databse is now accessible before restarting the application.", "Restart Application", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (issue == "MDB")
                MessageBox.Show("Please make sure that the master databse is now accessible before restarting the application.", "Restart Application", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (issue == "SANIP")
                MessageBox.Show("Please make sure that the shared folder for the Pinning Station controller is now accessible before restarting the application.", "Restart Application", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (issue == "UHF")
                MessageBox.Show("Please make sure that the UHF reader is properly connected.", "Restart Application", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (issue == "HF")
                MessageBox.Show("Please make sure that the HF reader is properly connected.", "Restart Application", MessageBoxButtons.OK, MessageBoxIcon.Information);

            FrmAdmin zFrmAdmin = new FrmAdmin(m_HumpyDetail);
            zFrmAdmin.ShowDialog();

            if (zFrmAdmin.zdr == System.Windows.Forms.DialogResult.OK)
                Application.Restart();
         
        }

        private void lblMessage_Click(object sender, EventArgs e)
        {

        }
    }
}
