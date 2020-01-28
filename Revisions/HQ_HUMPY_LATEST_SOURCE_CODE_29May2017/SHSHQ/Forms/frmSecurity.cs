using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SHSHQ.Forms
{
    public partial class frmSecurity : Form
    {
        public frmSecurity()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "Amm02o16!")
            {
                MessageBox.Show("Invalid password!", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                textBox1.SelectAll();
                textBox1.Text = "";
                return;
            }

            this.DialogResult = DialogResult.OK;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
