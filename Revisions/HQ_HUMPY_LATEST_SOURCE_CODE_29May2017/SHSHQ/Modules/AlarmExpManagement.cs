 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ISMDAL.TableColumnName;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ISM.Forms;

namespace ISM.Modules
{
    public partial class AlarmExpManagement : ISMBaseWorkSpace
    {
         
         
         
         
         
        private bool m_SelectedRecord = false;

        public AlarmExpManagement(ISMLoginInfo AISMLoginInfo)
            : base(AISMLoginInfo)
        {
            InitializeComponent();
        }

        private void AlarmExpManagement_Load(object sender, EventArgs e)
        {
            try
            {
                btnTaskClear.PerformClick();
                SetGridCaption();
                GetAlarmExpData();
            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Alarm/Excep Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }
        #region "Grid Caption"
        private void SetGridCaption()
        {
            try
            {
                ColumnView ColView = gvTaskManagement.MainView as ColumnView;
                 
                 
                 
                string[] zFieldNames = new string[] { "AlarmExp", "FK_PORTAL_NAME", "FK_ITME_UID", "FK_LOCATION_UID", "STARTDATE", "EXCEPTION_DESCRIPTION", "JNL_ID", "EXCEPTION_FLAG", "AL_CLOSURE_COMMENT" };  


                DevExpress.XtraGrid.Columns.GridColumn zColumn;
                ColView.Columns.Clear();
                for (int i = 0; i < zFieldNames.Length; i++)
                {
                    zColumn = ColView.Columns.AddField(zFieldNames[i]);
                    zColumn.VisibleIndex = i;
                }
                gridView.Columns[0].Caption = "Type";
                gridView.Columns[0].Width = 110;

                gridView.Columns[1].Caption = "Device Name";
                gridView.Columns[1].Width = 130;

                gridView.Columns[2].Caption = "Item UID";  
                gridView.Columns[2].Width = 75;

                gridView.Columns[3].Caption = "Location UID";
                gridView.Columns[3].Width = 75;

                gridView.Columns[4].Caption = "Open Date";
                gridView.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView.Columns[4].DisplayFormat.FormatString = m_ISMLoginInfo.Params.DateTimeFormat;
                gridView.Columns[4].Width = 100;

                gridView.Columns[5].Caption = "Exception / Alarm Description";
                gridView.Columns[5].Width = 325;
                 
                 
                gridView.Columns[6].Caption = "Journal ID";  
                gridView.Columns[6].Width = 10;
                gridView.Columns[6].Visible = false;
               
                gridView.Columns[7].Caption = "Exp Flag";  
                gridView.Columns[7].Width = 10;
                gridView.Columns[7].Visible = false;

                gridView.Columns[8].Caption = "Closure Comment";  
                gridView.Columns[8].Width = 10;
                gridView.Columns[8].Visible = false;

                
            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Alarm/Excep Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region "Functions"

         
         
         
         
        public byte[] String_To_Bytes(string strInput)  
        {
             
            int i = 0;
             
            int x = 0;
             
            byte[] bytes = new byte[(strInput.Length) / 2];
             
             
            while (strInput.Length > i + 1)
            {
                long lngDecimal = Convert.ToInt32(strInput.Substring(i, 2), 16);
                bytes[x] = Convert.ToByte(lngDecimal);
                i = i + 2;
                ++x;
            }
             
            return bytes;
        }

         
         
         
         
        public string Bytes_To_String(byte[] bytes_Input)  
        {
             
            string strTemp = "";
            for (int x = 0; x <= bytes_Input.GetUpperBound(0); x++)
            {
                int number = int.Parse(bytes_Input[x].ToString());
                strTemp += number.ToString("X").PadLeft(2, '0');
            }
             
            return strTemp;
        }  
        private void GetAlarmExpData()
        {
            try
            {
                int zMode = 1;  
                string zComments = "";
                 
                
                 
                double zJnlID = 0;

                DataSet ds = m_ISMLoginInfo.ISMServer.GetAlarmExpceptionData(zMode, zComments, zJnlID);
                if (ds != null)
                {
                    gvTaskManagement.DataSource = ds.Tables[0].DefaultView;
                }

            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Alarm/Excep Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void GetSelectedRow()
        {
            try
            {
                GridView zView = gridView;
                 
                 
                 
                txtStatusMsg.Text = "";
                if (zView.GetSelectedRows().Length > 0)
                {
                    DataRow zDataRow = zView.GetDataRow(zView.GetSelectedRows()[0]);
                   
                    if ((zDataRow["JNL_ID"].ToString().Trim() != ""))
                    {
                        m_ISMLoginInfo.m_JnlID = Convert.ToDouble((zDataRow["JNL_ID"].ToString()));  
                         
                    }
                    lblType.Text = (string)(zDataRow["AlarmExp"]);
                    lblDeviceName.Text = (string)(zDataRow["FK_PORTAL_NAME"]);
                    lblLocationUID.Text = (string)(zDataRow["FK_LOCATION_UID"]);
                    lblItemUID.Text = (string)(zDataRow["FK_ITME_UID"]); ;  
                    lblOpenDate.Text = ((DateTime)(zDataRow["STARTDATE"])).ToString();
                    memoEditDescription.Text = (string)(zDataRow["EXCEPTION_DESCRIPTION"]);
                    txtComment.Text = (string)(zDataRow["AL_CLOSURE_COMMENT"]);
                    m_ISMLoginInfo.m_ExpFlag = (string)(zDataRow["EXCEPTION_FLAG"]);  
                    
                    checkEditClose.Checked = false;  
                    
                    m_SelectedRecord = true;
                     
                    m_ISMLoginInfo.m_RecLockedForEdit = true;  
                    txtComment.Focus();
                     
                    
                    int zMode = 4;  
                    string zComments = "";
                   
                    DataSet ds = m_ISMLoginInfo.ISMServer.GetAlarmExpceptionData(zMode, zComments, m_ISMLoginInfo.m_JnlID);  
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("This data has already being updated by another User", "Alarm/Excep Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            GetAlarmExpData();
                            return;
                        }
                         
                         
                         
                         
                         
                         
                         
                         
                         
                         
                         
                         
                         
                         
                         
                    }

                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     

                     
                     
                     
                     
                     
                     
                     

                     
                     
                    
                }

            }
            catch (Exception ex) 
            {
                ex.Message.ToString();
                MessageBox.Show("System Error. Contact System Administrator", "Alarm/Excep Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region "Grid Function
        private void gvTaskManagement_DoubleClick(object sender, EventArgs e)
        {
             
             
             
             
             
             
             

             
             
             
            if (m_ISMLoginInfo.m_RecLockedForEdit)
            {
                int zMode = 5;  
                string zComments = "";
                DataSet ds = m_ISMLoginInfo.ISMServer.GetAlarmExpceptionData(zMode, zComments, m_ISMLoginInfo.m_JnlID);
                m_ISMLoginInfo.m_RecLockedForEdit = false;

                 
            }
            
            GetSelectedRow();

        }
        #endregion 
        #region "Update / Clear Button"

        private bool Validation()
        {
            bool zResult = false;
            bool zValidationFail = true;
            dxErrorProvider.SetError(txtComment, null);
            
             
            if (m_ISMLoginInfo.m_ExpFlag == "A" && txtComment.Text.Trim() == "")  
            {
                dxErrorProvider.SetError(txtComment, "Enter User Comment");
                txtComment.Focus();
                zValidationFail = false;
            }
            if (!m_SelectedRecord)
            {
                MessageBox.Show("Select a Grid Record to update", "Alarm/Excep Management", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); 
                zValidationFail = false;
            }

            if (zValidationFail)
                zResult = zValidationFail;
            return zResult;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    int zMode = 7;  
                    string zMessage = "";
                    if (m_ISMLoginInfo.m_ExpFlag == "A" && checkEditClose.EditValue.ToString() == "True")
                        zMessage = "Do you want to close the Alarm?";
                    else if ((m_ISMLoginInfo.m_ExpFlag == "E" || m_ISMLoginInfo.m_ExpFlag == "B") && checkEditClose.EditValue.ToString() == "True")
                        zMessage = "Do you want to close the Exception?";
                    else if (m_ISMLoginInfo.m_ExpFlag == "A" && checkEditClose.EditValue.ToString() == "False")
                        zMessage = "Do you want to update Alarm’s User Comment? ";
                    else if ((m_ISMLoginInfo.m_ExpFlag == "E" || m_ISMLoginInfo.m_ExpFlag == "B") && checkEditClose.EditValue.ToString() == "False")
                        zMessage = "Do you want to update Exception’s User Comment? ";
                    if (checkEditClose.EditValue.ToString() == "True")
                        zMode = 0;  
                    else if (checkEditClose.EditValue.ToString() == "False")
                        zMode = 6;  
                    DialogResult zReply = MessageBox.Show(zMessage, "Alarm/Excep Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (zReply == DialogResult.Yes)
                    {
                        string zComments = txtComment.Text.Trim();
                        m_ISMLoginInfo.ISMServer.GetAlarmExpceptionData(zMode, zComments, m_ISMLoginInfo.m_JnlID);
                        GetAlarmExpData();
                        m_ISMLoginInfo.m_RecLockedForEdit = false;
                        btnTaskClear.PerformClick();
                    }

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                MessageBox.Show("System Error. Contact System Administrator", "Alarm/Excep Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void CancelUpdate()
        {
            try
            {

                 
                 
                 
                 
                 

                 
                 
                 
                 
                 
                 
                 
                 
                 
                int zMode = 5;  
                string zComments = "";
                DataSet ds = m_ISMLoginInfo.ISMServer.GetAlarmExpceptionData(zMode, zComments, m_ISMLoginInfo.m_JnlID);
                GetAlarmExpData();
                m_ISMLoginInfo.m_RecLockedForEdit = false;

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                MessageBox.Show("System Error. Contact System Administrator", "Alarm/Excep Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        private void btnTaskClear_Click(object sender, EventArgs e)
        {
            try
            {
                bool zClearData = false;
                if (m_ISMLoginInfo.m_RecLockedForEdit)
                {
                    CancelUpdate();
                    if (!m_ISMLoginInfo.m_RecLockedForEdit)
                        zClearData = true;
                }
                else
                    zClearData = true;
                if (zClearData)
                {
                    lblType.Text = "";
                    lblDeviceName.Text = "";
                    lblLocationUID.Text = "";
                    lblItemUID.Text = "";  
                    lblOpenDate.Text = "";
                    txtComment.Text = "";
                    memoEditDescription.Text = "";
                     
                    checkEditClose.Checked = false;  
                    m_ISMLoginInfo.m_ExpFlag = "";
                    m_ISMLoginInfo.m_JnlID = 0;
                    m_SelectedRecord = false;
                    m_ISMLoginInfo.m_RecLockedForEdit = false;
                    dxErrorProvider.SetError(txtComment, null);

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                MessageBox.Show("System Error. Contact System Administrator", "Alarm/Excep Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }
        #endregion

        private void AlarmExpManagement_Leave(object sender, EventArgs e)
        {
            try
            {
                if (m_ISMLoginInfo.m_RecLockedForEdit)
                {
                    int zMode = 5;  
                    string zComments = "";
                    DataSet ds = m_ISMLoginInfo.ISMServer.GetAlarmExpceptionData(zMode, zComments, m_ISMLoginInfo.m_JnlID);
                    m_ISMLoginInfo.m_RecLockedForEdit = false;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                MessageBox.Show("System Error. Contact System Administrator", "Alarm/Excep Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        


    }
}
