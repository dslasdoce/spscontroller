 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ISMDAL.TableColumnName;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Security.Permissions;
using Microsoft.Win32;
 


namespace ISM.Modules
{
    public partial class RptMILISStockTake : ISMBaseWorkSpace
    {
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
        string m_MILISStockNo;
        string m_MILISPlanID;
        private volatile bool m_EndThread;          
        private string m_ExcelTemplate;
        private string m_ExcelFileName;
        private string m_ExcelSavePathName;


        public RptMILISStockTake(ISMLoginInfo AISMLoginInfo)
            : base(AISMLoginInfo)
        {
            InitializeComponent();
        }
        private void RptMILISStockTake_Load(object sender, EventArgs e)
        {
            try
            {
                m_EndThread = true;    
                progressBar.Style = ProgressBarStyle.Blocks;
                progressBar.Visible = false;
                wtDoIt.WorkerReportsProgress = true;
                wtDoIt.WorkerSupportsCancellation = true;

                SetLookUpEditCaption();  
                LoadStockTakeMetaData();  
                lookUpEditStockTakeNo.Focus();
            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }

         
        private void SetLookUpEditCaption()
        {
            try
            {
                 
                lookUpEditStockTakeNo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FK_MILIS_STOCKTAKE_NO", 90, "MILIS Stocktake No"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FK_MILIS_PLAN_ID", 90, "Plan ID"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LOCATION_UID", 150, "ISM Location UID"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ERP_BIN", 150, "ERP BIN"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("STOCK_TAKEDATE", 150,"Completed Date")});

                lookUpEditStockTakeNo.Properties.DisplayMember = "FK_MILIS_STOCKTAKE_NO";
                lookUpEditStockTakeNo.Properties.ValueMember = "FK_MILIS_STOCKTAKE_NO";
                 
                lookUpEditMILISStockTakeNo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FK_MILIS_STOCKTAKE_NO", 90, "MILIS Stocktake No"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FK_MILIS_PLAN_ID", 90, "Plan ID"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LOCATION_UID", 150, "ISM Location UID"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ERP_BIN", 150, "ERP BIN"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("STOCK_TAKEDATE", 150,"Completed Date")});

                lookUpEditMILISStockTakeNo.Properties.DisplayMember = "FK_MILIS_STOCKTAKE_NO";
                lookUpEditMILISStockTakeNo.Properties.ValueMember = "FK_MILIS_STOCKTAKE_NO";
            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void LoadStockTakeMetaData()
        {
            DataSet ds = null;
            try
            {
                int zMode = 1;  
                ds = m_ISMLoginInfo.ISMServer.GetRptStockTakeMetaData(zMode);
                if (ds != null)
                {
                    lookUpEditStockTakeNo.Properties.DataSource = ds.Tables[0].DefaultView;
                    lookUpEditMILISStockTakeNo.Properties.DataSource = ds.Tables[0].DefaultView;
                }
            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
         

        private bool ValidateReportData()  
        {
            bool zValidStockData = true;
            try
            {
                dxErrorProvider.SetError(txtMILISUserID, null);
                dxErrorProvider.SetError(lookUpEditStockTakeNo, null);

                if (txtMILISUserID.Text.Trim() == "")  
                {
                    dxErrorProvider.SetError(txtMILISUserID, "Enter MILIS User ID");
                    txtMILISUserID.Focus();
                    zValidStockData = false;
                }
                if (lookUpEditStockTakeNo.EditValue == null)
                {
                    dxErrorProvider.SetError(lookUpEditStockTakeNo, "Select a MILIS Stocktake No");
                    lookUpEditStockTakeNo.Focus();
                    zValidStockData = false;
                }
                if (!IsOfficeInstalled())  
                {
                    MessageBox.Show("System doesn’t have Microsoft Office. Can’t process MILIS Stocktake File", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    zValidStockData = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return zValidStockData;

        }

        private bool IsOfficeInstalled()  
        {
            bool zExist = false;
            try
            {

                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\excel.exe");
                if (key != null)
                {
                    key.Close();
                    zExist = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return zExist;
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {

                txtStatusMsg.Text = "";
                if(ValidateReportData())
                {
                     
                     

                    m_ExcelTemplate = System.Windows.Forms.Application.StartupPath + "\\MILISStockNoTemplate.xls";

                     
                     
                     
                     
                     
                     
                    if (!File.Exists(m_ExcelTemplate))
                    {
                        MessageBox.Show("MILIS Stocktake Excel template does not exit at " + m_ExcelTemplate + ". \nContact System Administrator ", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    FolderBrowserDialog zDlgOpenCSV = new FolderBrowserDialog();
                    zDlgOpenCSV.RootFolder = Environment.SpecialFolder.MyDocuments;  

                    if (zDlgOpenCSV.ShowDialog() == DialogResult.OK)
                    {
                        m_ExcelSavePathName = zDlgOpenCSV.SelectedPath;

                        progressBar.Visible = true;
                        progressBar.Style = ProgressBarStyle.Marquee;
                        m_EndThread = false;
                        wtDoIt.RunWorkerAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void ReleaseExcelObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                
            }
            finally
            {
                GC.Collect();

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearIcon();
            lookUpEditStockTakeNo.EditValue = null;
            lookUpEditStockTakeNo.Enabled = true;
            txtMILISUserID.Text = "";
            txtMILISUserID.Enabled = true;
            btnReport.Enabled = true;
            lookUpEditStockTakeNo.Focus();
             
            ClearEditDataIcon();
            lookUpEditMILISStockTakeNo.EditValue = null;
            lookUpEditMILISStockTakeNo.Enabled = true;
            txtMILISStockTakeNo.Text = "";
            txtMILISStockTakeNo.Enabled = true;
            txtMILISPlanID.Text = "";
            txtMILISPlanID.Enabled = true;
            btnEditUpdate.Enabled = true;
            txtStatusMsg.Text = "";

        }

        private void GenerateCSVFile(System.IO.StreamWriter zfile, string zWriteData)
        {
            try
            {
                if(zfile != null)
                {
                    zfile.WriteLine(zWriteData);
                }  

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void txtMILISUserID_EditValueChanged(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(txtMILISUserID, null);  
        }

        private void lookUpEditStockTakeNo_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lookUpEditStockTakeNo.EditValue != null)
                {
                     
                    lookUpEditMILISStockTakeNo.EditValue = null;
                    lookUpEditMILISStockTakeNo.Enabled = false;
                    txtMILISStockTakeNo.Text = "";
                    txtMILISStockTakeNo.Enabled = false;
                    txtMILISPlanID.Text = "";
                    txtMILISPlanID.Enabled = false;
                    btnEditUpdate.Enabled = false;
                    ClearEditDataIcon();
                    txtStatusMsg.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lookUpEditMILISStockTakeNo_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lookUpEditMILISStockTakeNo.EditValue != null)
                {
                    txtStatusMsg.Text = "";
                    lookUpEditStockTakeNo.EditValue = null;
                    lookUpEditStockTakeNo.Enabled = false;
                    txtMILISUserID.Text = "";
                    txtMILISUserID.Enabled = false;
                    btnReport.Enabled = false;
                     
                    object oValue = lookUpEditMILISStockTakeNo.Properties.GetDataSourceRowByKeyValue(lookUpEditMILISStockTakeNo.EditValue);
                    if (oValue != null)
                    {
                        DataRow dr = ((DataRowView)oValue).Row;
                        if (dr != null)
                        {
                            txtMILISStockTakeNo.Text = dr["FK_MILIS_STOCKTAKE_NO"].ToString().Trim();
                            m_MILISStockNo = dr["FK_MILIS_STOCKTAKE_NO"].ToString().Trim();
                            txtMILISPlanID.Text = dr["FK_MILIS_PLAN_ID"].ToString().Trim();
                            m_MILISPlanID = dr["FK_MILIS_PLAN_ID"].ToString().Trim();
                            btnEditUpdate.Enabled = false;  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        void ClearEditDataIcon()  
        {
            try
            {
                dxErrorProvider.SetError(lookUpEditMILISStockTakeNo, null);  
                dxErrorProvider.SetError(txtMILISStockTakeNo, null);  
                dxErrorProvider.SetError(txtMILISPlanID, null);  

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void ClearIcon()  
        {
            try
            {
                dxErrorProvider.SetError(lookUpEditStockTakeNo, null);  
                dxErrorProvider.SetError(txtMILISUserID, null);  

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEditClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearEditDataIcon();
                lookUpEditMILISStockTakeNo.EditValue = null;
                lookUpEditMILISStockTakeNo.Enabled = true;
                txtMILISStockTakeNo.Text = "";
                txtMILISStockTakeNo.Enabled = true;
                txtMILISPlanID.Text = "";
                txtMILISPlanID.Enabled = true;
                btnEditUpdate.Enabled = true;
                 
                ClearIcon();
                lookUpEditStockTakeNo.EditValue = null;
                lookUpEditStockTakeNo.Enabled = true;
                txtMILISUserID.Text = "";
                txtMILISUserID.Enabled = true;
                btnReport.Enabled = true;
                txtStatusMsg.Text = "";
                lookUpEditStockTakeNo.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        private bool ValidateEditData()
        {
            bool zValidStockData = true;
            try
            {
                dxErrorProvider.SetError(txtMILISStockTakeNo, null);
                dxErrorProvider.SetError(txtMILISPlanID, null);

                if (txtMILISStockTakeNo.Text.Trim() == "")
                {
                    dxErrorProvider.SetError(txtMILISStockTakeNo, "Enter MILIS Stocktake No");
                    txtMILISStockTakeNo.Focus();
                    zValidStockData = false;
                }
                if (txtMILISPlanID.Text.Trim() == "")
                {
                    dxErrorProvider.SetError(txtMILISPlanID, "Enter MILIS Plan ID");
                    txtMILISPlanID.Focus();
                    zValidStockData = false;
                }

                if (m_MILISStockNo == txtMILISStockTakeNo.Text.Trim() && m_MILISPlanID == txtMILISPlanID.Text.Trim())
                {
                    dxErrorProvider.SetError(txtMILISStockTakeNo, "There has been no change to the MILIS Stocktake No and Plan ID");
                    dxErrorProvider.SetError(txtMILISPlanID, "There has been no change to the MILIS Stocktake No and Plan ID");
                    zValidStockData = false;
                }
                if (m_MILISStockNo != txtMILISStockTakeNo.Text.Trim() && txtMILISStockTakeNo.Text.Trim() != "")
                {
                    if (m_ISMLoginInfo.ISMServer.IsMILISStockTakeNoExists(txtMILISStockTakeNo.Text.Trim()))
                    {
                        dxErrorProvider.SetError(txtMILISStockTakeNo, "MILIS Stock Take No already exist");
                        txtMILISStockTakeNo.Focus();
                        zValidStockData = false;
                    }
                }
               
                if (lookUpEditMILISStockTakeNo.EditValue == null)
                {
                    dxErrorProvider.SetError(lookUpEditMILISStockTakeNo, "MILIS Stock Take No already exist");
                    lookUpEditMILISStockTakeNo.Focus();
                    zValidStockData = false;
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return zValidStockData;

        }

        private void btnEditUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateEditData())
                {
                    int zMode = 3;  
                    if (m_MILISStockNo != txtMILISStockTakeNo.Text.Trim() && m_MILISPlanID != txtMILISPlanID.Text.Trim())
                        zMode = 0;
                    else if (m_MILISStockNo != txtMILISStockTakeNo.Text.Trim() && m_MILISPlanID == txtMILISPlanID.Text.Trim())
                        zMode = 1;
                    else if (m_MILISStockNo == txtMILISStockTakeNo.Text.Trim() && m_MILISPlanID != txtMILISPlanID.Text.Trim())
                        zMode = 2;
                    string zMessage  = "Do you want to Update MILIS Stocktake No: " + m_MILISStockNo + ", Plan ID : " + @m_MILISPlanID + " to Stocktake No : " + txtMILISStockTakeNo.Text.Trim() + ", Plan ID : " + @txtMILISPlanID.Text.Trim() + " ?";
                    DialogResult zReply = MessageBox.Show(zMessage, lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (zReply == DialogResult.Yes)
                    {

                        if (m_ISMLoginInfo.ISMServer.UpdateMILISStockTakeNo(m_MILISStockNo,txtMILISStockTakeNo.Text.Trim(), txtMILISPlanID.Text.Trim(), zMode))
                        {
                            zMessage = "MILIS Stocktake Stocktake No: " + m_MILISStockNo + ", Plan ID: " + @m_MILISPlanID + " has been updated to Stocktake No: " + txtMILISStockTakeNo.Text.Trim() + ", Plan ID: " + @txtMILISPlanID.Text.Trim();
                            LoadStockTakeMetaData();
                            btnClear.PerformClick();
                            btnEditClear.PerformClick();
                            txtStatusMsg.Text = zMessage;
                        }
                    }
                    else
                        txtStatusMsg.Text = "Edit MILIS Stocktake has been cancelled by user" ;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void txtMILISStockTakeNo_TextChanged(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(txtMILISStockTakeNo, null); 
        }

        private void txtMILISPlanID_TextChanged(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(txtMILISPlanID, null); 
        }

        private void txtMILISUserID_TextChanged(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(txtMILISUserID, null); 
        }
        private void EnableBtnUpdate(object sender, EventArgs e)   
        {
            if (m_MILISStockNo == txtMILISStockTakeNo.Text.Trim() && m_MILISPlanID == txtMILISPlanID.Text.Trim())
            {
                btnEditUpdate.Enabled = false;
            }
            else
            {
                if (txtMILISStockTakeNo.Text.Trim() == "" && txtMILISPlanID.Text.Trim() == "")
                    btnEditUpdate.Enabled = false;
                else
                    btnEditUpdate.Enabled = true;
            }

        }

        private void wtDoIt_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int zRecCount = 15;
                int zMode = 1;  
                string zMILISSTOCKNO = lookUpEditStockTakeNo.EditValue.ToString();
                string zMILISStocktakeNo = "";
                DataSet ds = m_ISMLoginInfo.ISMServer.GetRptMILISStocktakeNo(zMode, zMILISSTOCKNO);
                if (ds != null)
                {
                    Excel.ApplicationClass zExcelApp = new Excel.ApplicationClass();
                    Excel.Workbook zworkbook = (Excel.Workbook)zExcelApp.Workbooks.Add(Missing.Value);
                    Excel.Worksheet zworksheet = null;
                     
                    System.Diagnostics.Process[] zExcelProcess = Process.GetProcessesByName("excel");
                    try  
                    {
                        zworkbook = zExcelApp.Workbooks.Open(m_ExcelTemplate, 0, false, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                         
                        zworksheet = (Excel.Worksheet)zworkbook.Sheets.get_Item(1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        if(zworkbook != null)
                            ReleaseExcelObject(zworkbook);
                        zExcelApp.Quit();
                        ReleaseExcelObject(zExcelApp);
                         
                         
                        int zCount = zExcelProcess.Length;  
                        for (int nIndex = 0; nIndex < zCount; nIndex++)  
                        {
                            zExcelProcess[nIndex].Kill();
                            zExcelProcess[nIndex].WaitForExit();
                        }
                        return;

                    }
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        zMILISStocktakeNo = ds.Tables[0].Rows[0]["FK_MILIS_STOCKTAKE_NO"].ToString().Trim();
                        m_ExcelFileName = m_ExcelSavePathName + "\\ISM_Stocktake_" + ds.Tables[0].Rows[0]["STOCKTAKEDATE"].ToString() + "_" + zMILISStocktakeNo + ".xls";

                    }
                    else
                    {
                        UpdateStatus("MILIS Stocktake File data does not exist for MILIS Stocktake No : " + zMILISSTOCKNO);
                    }

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        ((Excel.Range)zworksheet.Cells[zRecCount, "C"]).Value2 = "B";
                        ((Excel.Range)zworksheet.Cells[zRecCount, "D"]).Value2 = dr["FK_MILIS_STOCKTAKE_NO"].ToString().Trim();  
                        ((Excel.Range)zworksheet.Cells[zRecCount, "E"]).Value2 = "0001";  
                        ((Excel.Range)zworksheet.Cells[zRecCount, "F"]).Value2 = dr["FK_MILIS_PLAN_ID"].ToString().Trim();   
                        ((Excel.Range)zworksheet.Cells[zRecCount, "G"]).Value2 = txtMILISUserID.Text.Trim();  
                        ((Excel.Range)zworksheet.Cells[zRecCount, "H"]).Value2 = dr["ERP_DISTRICT"].ToString().Trim();  
                        ((Excel.Range)zworksheet.Cells[zRecCount, "I"]).Value2 = dr["ERP_WHOUSE"].ToString().Trim();  
                        ((Excel.Range)zworksheet.Cells[zRecCount, "J"]).Value2 = dr["ERP_BIN"].ToString().Trim();  
                        ((Excel.Range)zworksheet.Cells[zRecCount, "K"]).Value2 = "";  
                        ((Excel.Range)zworksheet.Cells[zRecCount, "L"]).NumberFormat = "@";
                        ((Excel.Range)zworksheet.Cells[zRecCount, "L"]).Value2 = dr["STOCK_CODE"].ToString().Trim();  
                        ((Excel.Range)zworksheet.Cells[zRecCount, "M"]).Value2 = System.Convert.ToInt32(dr["SOH"]);  
                        ((Excel.Range)zworksheet.Cells[zRecCount, "N"]).Value2 = dr["SERIAL_EQUIP_NO"].ToString().Trim();  
                        ((Excel.Range)zworksheet.Cells[zRecCount, "O"]).Value2 = dr["EQUIPMENT_REFERENCE"].ToString().Trim();  
                        ((Excel.Range)zworksheet.Cells[zRecCount, "P"]).Value2 = "";  
                        ((Excel.Range)zworksheet.Cells[zRecCount, "Q"]).Value2 = dr["BATCH_LOT_NO"].ToString().Trim();  
                        zRecCount = zRecCount + 1;


                    }
                    zExcelApp.DisplayAlerts = false;
                    if (File.Exists(m_ExcelFileName))
                    {
                        DialogResult zReply = MessageBox.Show("MILIS Stocktake file " + m_ExcelFileName + " already exist. Do you want to overwrite?", lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (zReply == DialogResult.Yes)
                        {
                            File.Delete(m_ExcelFileName);
                            zworkbook.Saved = true;  
                            zworkbook.SaveAs(m_ExcelFileName, Excel.XlFileFormat.xlWorkbookNormal, null, null, false, false, Excel.XlSaveAsAccessMode.xlShared, false, false, null, null, null);
                            m_ISMLoginInfo.ISMServer.AddInterfaceJournal("INT003", m_ISMLoginInfo.UserID);
                            UpdateStatus("MILIS Stocktake File saved at " + m_ExcelFileName);
                            

                        }

                    }
                    else
                    {
                        zworkbook.Saved = true;  
                        zworkbook.SaveAs(m_ExcelFileName, Excel.XlFileFormat.xlWorkbookNormal, null, null, false, false, Excel.XlSaveAsAccessMode.xlShared, false, false, null, null, null);
                        m_ISMLoginInfo.ISMServer.AddInterfaceJournal("INT003", m_ISMLoginInfo.UserID);
                        UpdateStatus("MILIS Stocktake File saved at " + m_ExcelFileName);
                    }

                    zworkbook.Close(0, 0, 0);
                    ReleaseExcelObject(zworksheet);
                    ReleaseExcelObject(zworkbook);
                    zExcelApp.Quit();
                    ReleaseExcelObject(zExcelApp);
                     
                     
                    int ZCount = zExcelProcess.Length;  
                    for (int nIndex = 0; nIndex < ZCount; nIndex++)  
                    {
                        if (zExcelProcess[nIndex].MainWindowTitle == "")  
                        {
                            zExcelProcess[nIndex].Kill();
                            zExcelProcess[nIndex].WaitForExit();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void wtDoIt_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void wtDoIt_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled)
                {
                    progressBar.Style = ProgressBarStyle.Blocks;
                    progressBar.Visible = false;
                }
                else if (e.Error != null)
                {
                    progressBar.Style = ProgressBarStyle.Blocks;
                    progressBar.Visible = false;
                }
                else
                {
                    progressBar.Style = ProgressBarStyle.Blocks;
                    progressBar.Visible = false;
                    m_EndThread = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

    
        }
        private delegate void UpdateStatusDelegate();
        private void UpdateStatus(string AStatus)
        {
            Invoke(new UpdateStatusDelegate(delegate { txtStatusMsg.Text = AStatus; }));
        }

        private void RptMILISStockTake_Leave(object sender, EventArgs e)
        {
            try
            {
                 
                 
                 
                 
                if (!m_EndThread)
                {
                    if (MessageBox.Show("System creating MILIS Stocktake file. Do you want cancel?", lblHeader.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        UpdateStatus("Please wait. System cancelling  Create MILIS Stocktake file");
                        m_EndThread = true;
                        wtDoIt.CancelAsync();
                        while (wtDoIt.IsBusy)
                            Application.DoEvents();
                        if (File.Exists(m_ExcelFileName))
                            File.Delete(m_ExcelFileName);
                       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error. Contact System Administrator. Error Details :" + ex.Message, lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
       
    }
}
