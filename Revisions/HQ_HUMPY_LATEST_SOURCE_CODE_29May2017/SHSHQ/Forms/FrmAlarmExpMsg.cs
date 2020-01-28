 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ISMDAL.TableColumnName;

namespace ISM.Forms
{
    public partial class FrmAlarmExpMsg : DevExpress.XtraEditors.XtraForm
    {
        private string m_ExpType = "";
        private string m_ExpDescription = "";
        private string m_ExpFlag = "";
        private string m_UserAction= "";
        byte[] m_TimeStamp;
        private ISMLoginInfo m_ISMLoginInfo;

        public string ExpType
        {
            get { return m_ExpType; }
            set { m_ExpType = value; }
        }

        public string ExpDescription
        {
            get { return m_ExpDescription; }
            set { m_ExpDescription = value; }
        }

        public string ExpFlag
        {
            get { return m_ExpFlag; }
            set { m_ExpFlag = value; }
        }
        public byte[] JnlTimeStamp
        {
            get { return m_TimeStamp; }
            set { m_TimeStamp = value; }
        }
        public string UserAction
        {
            get { return m_UserAction; }
            set { m_UserAction = value; }
        }
   
        public FrmAlarmExpMsg(ISMLoginInfo AISMLoginInfo)
        {
            InitializeComponent();
            m_ISMLoginInfo = AISMLoginInfo;
        }

        private void FrmAlarmExpMsg_Load(object sender, EventArgs e)
        {
            try
            {
                lblExpType.Text = ExpType;
                lblExpDescription.Text = ExpDescription;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                MessageBox.Show("System Error. Contact System Administrator", "Alarm/Excep Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        private void ClearErrorIconText()
        {
            dxErrorProvider.SetError(txtComment, null);
        }
        private bool Validation()
        {
            bool zResult = false;
            bool zValidationFail = true;
            ClearErrorIconText();

            if (m_ExpFlag == "A" && txtComment.Text.Trim() == "")
            {
                dxErrorProvider.SetError(txtComment, "Enter User Comment");
                txtComment.Focus();
                zValidationFail = false;
            }
            if (zValidationFail)
                zResult = zValidationFail;
            return zResult;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                dxErrorProvider.SetError(txtComment, null);
                if (Validation())
                {
                    string zMessage = "";
                    if (m_ExpFlag == "A")
                        zMessage = "Do you want close this Alarm?";
                    else
                        zMessage = "Do you want close this Exception?";

                    DialogResult zReply = MessageBox.Show(zMessage, "Alarm/Excep Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (zReply == DialogResult.Yes)
                    {
                        int zMode = 0;  
                        string zComments = txtComment.Text.Trim();
                      
                        UserAction = "CLOSED";
                        this.Close();  
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                MessageBox.Show("System Error. Contact System Administrator", "Alarm/Excep Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                string zMessage = "Do you want to Cancel?";
                DialogResult zReply = MessageBox.Show(zMessage, "Alarm/Excep Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (zReply == DialogResult.Yes)
                {
                    int zMode = 5;  
                    string zComments = "";
                    
                    UserAction = "CANCELLED";
                    this.Close();  
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                MessageBox.Show("System Error. Contact System Administrator", "Alarm/Excep Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(txtComment, null);
            txtComment.Text = "";
            txtComment.Focus();
        }

    }
}