using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartHumpyController
{
    public partial class FrmStatus : Form
    {
        public FrmStatus(string Caption, int ShutDownTime)
        {
            InitializeComponent();
            this.Text = Caption;
            this.lblStatus.Text = Caption + " - " + "Please present your card......";
            this.timrShutDown.Interval = ShutDownTime;
        }

        private void FrmStatus_Load(object sender, EventArgs e)
        {
            timrShutDown.Enabled = true;
        }

        private void timrShutDown_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
