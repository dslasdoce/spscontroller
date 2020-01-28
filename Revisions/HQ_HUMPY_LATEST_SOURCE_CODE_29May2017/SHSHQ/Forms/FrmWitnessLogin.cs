 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ISMDAL.TableColumnName;

namespace ISM.Forms
{
    public partial class FrmWitnessLogin : Form
    {
        private ISMLoginInfo m_ISMLoginInfo;
         
         
        private enum LoginStauts : int { ValidLogin = 0, Exception = 1, Cancel = 2,ThreeTimesAttempt =3 };
        private int m_LoginResult; 
        private int m_nNumberOfRetry = 0;
        int m_AllowLogIn = 0;
        int m_CoAUserID;

        public int CoALoginResult
        {
            set { m_LoginResult = value; }
            get { return m_LoginResult; }
        }
        public int CoALoginUserID
        {
            set { m_CoAUserID = value; }
            get { return m_CoAUserID; }
        }

        public FrmWitnessLogin(ISMLoginInfo AISMLoginInfo)
        {
            InitializeComponent();
            m_ISMLoginInfo = AISMLoginInfo;
        }
        private void FrmWitnessLogin_Load(object sender, EventArgs e)
        {
            ResetControls();

        }
        
        private void ResetControls()
        {
            try
            {
                txtUserID.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtUserID.Focus();
                btnLogin.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        private void EnableBtnLogin(object sender, EventArgs e) 
        {
            if ((txtUserID.Text.Trim().Length > 0) && (txtPassword.Text.Trim().Length > 0))
            {
                btnLogin.Enabled = true;
            }
            else
            {
                btnLogin.Enabled = false;
            }

        }
        private bool Validation()
        {
            bool zRetValue = false;  
            try
            {
                if (txtUserID.Text.Trim() == "")   
                {
                    MessageBox.Show("Please input User Name", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUserID.Focus();
                }
                else if (txtPassword.Text.Trim() == "")  
                {
                    MessageBox.Show("Please input Password", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPassword.Focus();
                }
                else if (txtUserID.Text.Trim().ToUpper() == m_ISMLoginInfo.LogonID.Trim().ToUpper())  
                {
                    MessageBox.Show("The CoA Verifier User ID can’t be the same as the logged in user", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUserID.Focus();
                }
                else
                    zRetValue = true;  
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


            return zRetValue;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    string zComparePWords = new ISMPassword().EncryptPassword(txtUserID.Text.ToUpper() + txtPassword.Text);
                    int zLoginUserID = 0;
                    DateTime zEndDate = DateTime.Parse(DateTime.Now.ToString());
                    DateTime zUserEndDate = DateTime.MinValue;
                    m_AllowLogIn = m_ISMLoginInfo.ISMServer.ValidateCoAUserLogin(txtUserID.Text, zComparePWords, ref zLoginUserID, ref zUserEndDate);
                    CoALoginUserID = zLoginUserID;
                     
                     
                     
                    m_nNumberOfRetry += 1;
                    
                    if (m_AllowLogIn == 3 && m_nNumberOfRetry >= SHSHQ.Properties.Settings.Default.NumberOfRetry)
                    {
                        
                        MessageBox.Show("You have reached the maximum number of retry options.\nIf you have forgotten your User ID or password\nthen please contact your System Administrator", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                         
                        CoALoginResult = (int)LoginStauts.ThreeTimesAttempt; 
                        Close();
                    }
                    else if (m_AllowLogIn == 3 && m_nNumberOfRetry < SHSHQ.Properties.Settings.Default.NumberOfRetry)
                    {
                        MessageBox.Show("Incorrect User ID or password entered", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ResetControls();
                        return;
                    }
                    else if (m_AllowLogIn == 2)
                    {
                        MessageBox.Show("This User ID is not a CoA Verifier", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (m_nNumberOfRetry >= SHSHQ.Properties.Settings.Default.NumberOfRetry)
                        {
                             
                            CoALoginResult = (int)LoginStauts.ThreeTimesAttempt; 
                            this.Close();
                        }
                        else
                            txtUserID.Focus();
                    }
                    else if (m_AllowLogIn == 1)
                    {
                        if (zEndDate.Date >= DateTime.Now.Date && zUserEndDate == DateTime.MinValue)   
                        {
                            CoALoginResult = (int)LoginStauts.ValidLogin;
                            this.Close(); 
                        }
                        else if (m_nNumberOfRetry >= SHSHQ.Properties.Settings.Default.NumberOfRetry)  
                        {
                             
                            CoALoginResult = (int)LoginStauts.ThreeTimesAttempt; 
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Your ISM account has expired. Please Contact System Administrator", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetControls();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                 
                 

                 
                 
                 
                 
                 
                 
                 
                 
                 
                CoALoginResult = (int)LoginStauts.Cancel;
                Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void btnException_Click(object sender, EventArgs e)
        {
            try
            {
                
                 
                 

                 
                 
                 
                 
                 
                 
                 
                 
                 

                CoALoginResult = (int)LoginStauts.Exception;
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }  
    }
}
