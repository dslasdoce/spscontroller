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
    public partial class HumpyMode : Form
    {
        int timer = 0;
        public HumpyMode(string caption, bool buttonVisible)
        {
            InitializeComponent();
            label1.Text = caption;
            button1.Visible = buttonVisible;
            timer1.Enabled = !buttonVisible;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HumpyDAL m_DAL_LOCAL;
            HumpyDetail m_HumpyDetail = new HumpyDetail();
            m_DAL_LOCAL = new HumpyDAL(SmartHumpyController.Properties.Settings.Default.Sys_DBConnectionLocal);
            m_HumpyDetail = m_DAL_LOCAL.SHS_GetHumpySettings();

            FrmAdmin adminForm = new FrmAdmin(m_HumpyDetail);
            adminForm.ShowDialog();

            if (adminForm.zdr == DialogResult.OK)
                this.DialogResult = DialogResult.OK;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer += 1;
            if (timer == 10 )
            {
                timer1.Enabled = false;
                Application.Restart();
            }
        }
    }
}
