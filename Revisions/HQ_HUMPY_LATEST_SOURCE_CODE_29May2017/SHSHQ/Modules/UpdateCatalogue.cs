
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ISMDAL.TableColumnName;
using System.IO;
using System.Collections;

namespace ISM.Modules
{
  public partial class UpdateCatalogue : ISMBaseWorkSpace
  {

    private bool m_ImportExceptionGeneratedFlag = false; 
                                                         
                                                         

    private volatile bool m_EndThread;                  
    private List<ISMStockCatalogue> m_StockCatalogue;   
    public bool m_UpdateInProgress; 
    private UInt64 m_RecordsRead;
    private UInt64 m_Inserted;
    private UInt64 m_Ignored;
    private UInt64 m_Updated; 
    private string m_FileRead;
    private bool m_RecInserted;
    private bool m_RecUpdated; 
    public bool m_FirstTimeView; 
    private int m_ViewCounter; 
    private bool m_RecRead;
    private UInt64 m_CSVErrors;  

    private ISMLoginInfo m_ISMLoginInfo_StockExist;
    private ISMLoginInfo m_ISMLoginInfo_AddToStockCatalogue;
    private ISMLoginInfo m_ISMLoginInfo_UpdateStockCatalogue;
    long m_TaskID = 0;  

    public UpdateCatalogue(ISMLoginInfo AISMLoginInfo)
      : base(AISMLoginInfo)
    {
      InitializeComponent();

      m_ViewCounter = 0; 

      m_ISMLoginInfo_StockExist = new ISMLoginInfo();
      m_ISMLoginInfo_AddToStockCatalogue = new ISMLoginInfo();
      m_ISMLoginInfo_UpdateStockCatalogue = new ISMLoginInfo();

      string zCurrentLogginUser = AISMLoginInfo.ISMServer.CurrentLoggedInUser;

      string zConnectionStr;
      if (SHSHQ.Properties.Settings.Default.ConnectionString.Contains("User ID=ismsupport;Password=p@ssw0rd"))
        zConnectionStr = SHSHQ.Properties.Settings.Default.ConnectionString;
      else
        zConnectionStr = SHSHQ.Properties.Settings.Default.ConnectionString
                         + "User ID=ismsupport;Password=p@ssw0rd";

      m_ISMLoginInfo_StockExist.ISMServer = new ISMDAL.DAL(zConnectionStr);
      m_ISMLoginInfo_StockExist.ISMServer.CurrentLoggedInUser = zCurrentLogginUser;
      m_ISMLoginInfo_AddToStockCatalogue.ISMServer = new ISMDAL.DAL(zConnectionStr);
      m_ISMLoginInfo_AddToStockCatalogue.ISMServer.CurrentLoggedInUser = zCurrentLogginUser;
      m_ISMLoginInfo_UpdateStockCatalogue.ISMServer = new ISMDAL.DAL(zConnectionStr);
      m_ISMLoginInfo_UpdateStockCatalogue.ISMServer.CurrentLoggedInUser = zCurrentLogginUser;
    }

    private void UpdateCatalogue_Load(object sender, EventArgs e)
    {
      m_EndThread = true;    
      progressBar.Style = ProgressBarStyle.Blocks;
      progressBar.Visible = false;
      m_StockCatalogue = new List<ISMStockCatalogue>();
      m_RecRead = true;
      m_RecInserted = false;
      m_RecUpdated = false; 
      m_UpdateInProgress = false; 
      m_FirstTimeView = false; 

       
      m_ViewCounter++;
      if (m_ViewCounter > 1)
      {
        m_FirstTimeView = true;
      }
    }

    private void UpdateCatalogue_ControlRemoved(object sender, ControlEventArgs e)
    {
      try
      {
         
         
         
        if (wtDoIt.IsBusy)
        {
          m_EndThread = true;
          wtDoIt.CancelAsync();
          while (wtDoIt.IsBusy)
            Application.DoEvents();

          m_StockCatalogue.Clear();  
          GC.Collect();  
          GC.WaitForPendingFinalizers();
        }
      }
      catch
      {
        MessageBox.Show("System Error. Contact System Administrator", "Update Catalogue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }

    private delegate void UpdateTotalScanDelegate();
    private void UpdateRecordsRead(UInt64 ARecordsRead)
    {
      Invoke(new UpdateTotalScanDelegate(delegate { lblTotalRecordsRead.Text = ARecordsRead.ToString(); }));
    }

    private delegate void UpdateRecordsInsertedDelegate();
    private void UpdateRecordsInserted(UInt64 ARecordsInserted)
    {
      Invoke(new UpdateRecordsInsertedDelegate(delegate { lblInserted.Text = ARecordsInserted.ToString(); }));
    }

    private delegate void UpdateRecordsIgnoredDelegate();
    private void UpdateRecordsIgnored(UInt64 ARecordsIgnored)
    {
      Invoke(new UpdateRecordsIgnoredDelegate(delegate { lblIgnored.Text = ARecordsIgnored.ToString(); }));
    }

     
    private delegate void UpdateRecordsUpdatedDelegate();
    private void UpdateRecordsUpdated(UInt64 ARecordsUpdated)
    {
      Invoke(new UpdateRecordsUpdatedDelegate(delegate { lblUpdated.Text = ARecordsUpdated.ToString(); }));
    }


    private delegate void UpdateCSVErrorsDelegate();
    private void UpdateCSVErrors(UInt64 ACSVErrorCnt)
    {
      Invoke(new UpdateCSVErrorsDelegate(delegate { lblCSVErrors.Text = ACSVErrorCnt.ToString(); }));
    }

    private delegate void ProgressUpdateStockCatalogueDelegate();
    private void ProgressUpdateStockCatalogue(int AMode, long ATaskID, long ATempTaskID)
    {
        Invoke(new ProgressUpdateStockCatalogueDelegate(delegate { m_ISMLoginInfo.ISMServer.UpdateStockCatalogueProgress(AMode, ATaskID, ref ATempTaskID); }));
    }

    private void btnStartDataTransfer_Click(object sender, EventArgs e)
    {
      try
      {
        if (wtDoIt.IsBusy)
        {
          m_EndThread = true;
          m_UpdateInProgress = false; 
          wtDoIt.CancelAsync();
          while (wtDoIt.IsBusy)
            Application.DoEvents();

          wtDoIt_RunWorkerCompleted(sender, null);  
        }
        else
        {
          OpenFileDialog zDlgOpenCSV = new OpenFileDialog();
          zDlgOpenCSV.Filter = "CSV Files|*.csv";
          zDlgOpenCSV.InitialDirectory = Application.StartupPath;
          if (zDlgOpenCSV.ShowDialog() == DialogResult.OK)
          {
            m_FileRead = zDlgOpenCSV.FileName;

            m_StockCatalogue.Clear();  
            lblTotalRecordsRead.Text = "";
            lblInserted.Text = "";
             
            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee;
            m_EndThread = false;
            m_RecordsRead = 0;
            m_Inserted = 0;
            m_Ignored = 0;
            m_Updated = 0; 
            m_UpdateInProgress = true; 
            m_CSVErrors = 0;   
            UpdateRecordsRead(m_RecordsRead);
            UpdateRecordsInserted(m_Inserted);
            UpdateRecordsIgnored(m_Ignored);
            UpdateRecordsUpdated(m_Updated); 
            UpdateCSVErrors(m_CSVErrors);
            btnStartDataTransfer.Text = "Stop Data Transfer";
            btnStartDataTransfer.ImageIndex = 5;
            txtStatusMsg.Text = "";
             
            int zMode = 0;  
            m_ISMLoginInfo.ISMServer.UpdateStockCatalogueProgress(zMode, 0, ref m_TaskID);
             

            wtDoIt.RunWorkerAsync();
          }
        }
      }
      catch
      {
        MessageBox.Show("System Error. Contact System Administrator", "Update Catalogue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }

    private void wtDoIt_DoWork(object sender, DoWorkEventArgs e)
    {
      ImportStockCatalogue();

      if (!m_EndThread)
      {
         
        try
        {
           
          InsertStockCatalogue_JL();
          m_StockCatalogue.Clear();  
          GC.Collect();  
          GC.WaitForPendingFinalizers();
        }
        catch (Exception ex)
        {
          MessageBox.Show("System Error:" + ex.ToString(), "Update Catalogue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
      }
    }

    private void wtDoIt_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      progressBar.Style = ProgressBarStyle.Blocks;
      progressBar.Visible = false;
      btnStartDataTransfer.Text = "Start Data Transfer...";
      btnStartDataTransfer.ImageIndex = 4;
      if (m_CSVErrors > 0)
      {
          txtStatusMsg.Text = string.Format("Stock Catalogue Data Updated with error(s).  Please see ISMCatalogueErrors.txt in {0} folder", m_ISMLoginInfo.Params.ReportFolder);
           
          int zMode = 2;  
          long zTempTaskID = 0;
          ProgressUpdateStockCatalogue(zMode, m_TaskID, zTempTaskID);
           

           
      }
      else  
      {
           
          if (e == null)
          {
              txtStatusMsg.Text = "Stock Catalogue Data Updated aborted by user";

               
               
              int zMode = 1;  
              long zTempTaskID = 0;
              ProgressUpdateStockCatalogue(zMode, m_TaskID,  zTempTaskID);
               

          }
          else
          {
              txtStatusMsg.Text = "Stock Catalogue Data Updated Completed";
              
               
              int zMode = 3;  
              long zTempTaskID = 0;
              ProgressUpdateStockCatalogue(zMode, m_TaskID, zTempTaskID);
               
               
          }
      }

      m_UpdateInProgress = false; 
      m_StockCatalogue.Clear();  
      GC.Collect();  
      GC.WaitForPendingFinalizers();

    }



     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     


     
     
     
     
     
     
     
    private void InsertStockCatalogue_JL()
    {
        foreach (ISMDAL.TableColumnName.ISMStockCatalogue zStockCatalogue in m_StockCatalogue)
        {
            try
            {
                if (!m_EndThread)
                {
                     
                    if (zStockCatalogue.m_StockData.StockCodeOld != null && zStockCatalogue.m_StockData.StockCodeOld == "")
                    {
                         
                         
                        if (zStockCatalogue.m_StockData.StockCode != null && zStockCatalogue.m_StockData.StockCode != "") 
                        {
                             
                             
                             

                             
                            if (m_ISMLoginInfo_StockExist.ISMServer.IsStockCodeExistInStockCatalogue(zStockCatalogue.m_StockData.StockCode))
                            {
                                 

                                 
                                 

                                 
                                DataSet zDs = m_ISMLoginInfo_UpdateStockCatalogue.ISMServer.UpdateStockCatalogueRecord(zStockCatalogue, 0);
                                bool zIgnoreFlag = false;
                                if (zDs != null)
                                {
                                    if (zDs.Tables.Count > 0)
                                    {
                                        if (zDs.Tables[0].Rows.Count > 0) 
                                        {
                                            zIgnoreFlag = true;
                                        }
                                    }
                                }
                                 
                                if (zIgnoreFlag == false)
                                {
                                    if (m_ISMLoginInfo_UpdateStockCatalogue.ISMServer.UpdateStockCatalogueRecord(zStockCatalogue, 1) == null)
                                    {
                                        ++m_Updated;
                                        UpdateRecordsUpdated(m_Updated);
                                        m_RecUpdated = true;
                                         
                                    }
                                }
                                else
                                {
                                    ++m_Ignored;
                                    UpdateRecordsIgnored(m_Ignored);
                                }
                            }
                             
                            else
                            {
                                 

                                 
                                if (m_ISMLoginInfo_AddToStockCatalogue.ISMServer.AddStockCatalogueRecord(zStockCatalogue) == true)
                                {
                                    ++m_Inserted;
                                    UpdateRecordsInserted(m_Inserted);
                                    m_RecInserted = true;
                                     
                                }
                            }
                        }
                         
                        else
                        {
                            ++m_Ignored;
                            UpdateRecordsIgnored(m_Ignored);
                        }
                    }
                     
                     
                     
                    else if (zStockCatalogue.m_StockData.StockCodeOld != null && zStockCatalogue.m_StockData.StockCodeOld != "")  
                    {
                         
                        if (!m_ISMLoginInfo_StockExist.ISMServer.IsStockCodeExistInStockCatalogue(zStockCatalogue.m_StockData.StockCodeOld))
                        {
                            if (zStockCatalogue.m_StockData.StockCode != null && zStockCatalogue.m_StockData.StockCode != "") 
                            {
                                 
                                if (!m_ISMLoginInfo_StockExist.ISMServer.IsStockCodeExistInStockCatalogue(zStockCatalogue.m_StockData.StockCode))
                                {
                                     

                                     
                                    if (m_ISMLoginInfo_AddToStockCatalogue.ISMServer.AddStockCatalogueRecord(zStockCatalogue) == true)
                                    {
                                        ++m_Inserted;
                                        UpdateRecordsInserted(m_Inserted);
                                        m_RecInserted = true;
                                         
                                    }
                                }
                                else
                                {
                                     
                                     

                                     
                                     
                                     
                                     
                                     
                                     
                                     
                                     
                                     
                                     

                                     

                                     
                                    DataSet zDs = m_ISMLoginInfo_UpdateStockCatalogue.ISMServer.UpdateStockCatalogueRecord(zStockCatalogue, 0);
                                    bool zIgnoreFlag = false;
                                    if (zDs != null)
                                    {
                                        if (zDs.Tables.Count > 0)
                                        {
                                            if (zDs.Tables[0].Rows.Count > 0) 
                                            {
                                                zIgnoreFlag = true;
                                            }
                                        }
                                    }
                                     
                                    if (zIgnoreFlag == false)
                                    {
                                        if (m_ISMLoginInfo_UpdateStockCatalogue.ISMServer.UpdateStockCatalogueRecord(zStockCatalogue, 1) == null)
                                        {
                                            ++m_Updated;
                                            UpdateRecordsUpdated(m_Updated);
                                            m_RecUpdated = true;
                                             
                                        }
                                    }
                                    else
                                    {
                                        ++m_Ignored;
                                        UpdateRecordsIgnored(m_Ignored);
                                    }

                                }
                            }
                             
                            else
                            {
                                ++m_Ignored;
                                UpdateRecordsIgnored(m_Ignored);
                            }
                        }
                         
                        else
                        {
                             

                             
                             
                             
                             
                             

                            if (zStockCatalogue.m_StockData.StockCode != null && zStockCatalogue.m_StockData.StockCode != "") 
                            {

                                 
                                if (!m_ISMLoginInfo_StockExist.ISMServer.IsStockCodeExistInStockCatalogue(zStockCatalogue.m_StockData.StockCode))
                                {
                                     
                                    if (m_ISMLoginInfo_AddToStockCatalogue.ISMServer.AddStockCatalogueRecord(zStockCatalogue) == true)
                                    {
                                         
                                        int AUpdateRecord = 0;
                                        m_ISMLoginInfo_AddToStockCatalogue.ISMServer.UpdateStockCatalogueNewStockCode(zStockCatalogue, ref AUpdateRecord);

                                        if (AUpdateRecord == 1)
                                        {
                                            ++m_Updated;
                                            UpdateRecordsUpdated(m_Updated);
                                            m_RecUpdated = true;

                                        }
                                        else
                                        {
                                            ++m_Ignored;
                                            UpdateRecordsIgnored(m_Ignored);
                                        }
                                    }
                                }
                                 
                                else
                                {
                                     

                                     
                                    m_ISMLoginInfo_UpdateStockCatalogue.ISMServer.UpdateStockCatalogueRecord(zStockCatalogue, 1); 

                                     
                                     
                                     

                                    int AUpdateRecord = 0;
                                    m_ISMLoginInfo_AddToStockCatalogue.ISMServer.UpdateStockCatalogueNewStockCode(zStockCatalogue, ref AUpdateRecord);

                                    if (AUpdateRecord == 1)
                                    {
                                        ++m_Updated;
                                        UpdateRecordsUpdated(m_Updated);
                                        m_RecUpdated = true;

                                    }
                                    else
                                    {
                                        ++m_Ignored;
                                        UpdateRecordsIgnored(m_Ignored);
                                    }
                                }
                            }
                            else   
                            {
                                ++m_Ignored;
                                UpdateRecordsIgnored(m_Ignored);
                            }
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             
                             

                        }
                        
                    }
                }
                else
                    break;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

     
     
     
     
     
     
    private void InsertStockCatalogue()
    {
      foreach (ISMDAL.TableColumnName.ISMStockCatalogue zStockCatalogue in m_StockCatalogue)
      {
        if (!m_EndThread)
        {
            if (zStockCatalogue.m_StockData.StockCodeOld != null && zStockCatalogue.m_StockData.StockCodeOld == "")
            {
                if (zStockCatalogue.m_StockData.StockCode != null)
                {
                     
                     
                     

                    if (m_ISMLoginInfo_StockExist.ISMServer.IsStockCodeExistInStockCatalogue(zStockCatalogue.m_StockData.StockCode))
                    {

                         
                         

                         
                        DataSet zDs = m_ISMLoginInfo_UpdateStockCatalogue.ISMServer.UpdateStockCatalogueRecord(zStockCatalogue, 0);
                        bool zIgnoreFlag = false;
                        if (zDs != null)
                        {
                            if (zDs.Tables.Count > 0)
                            {
                                if (zDs.Tables[0].Rows.Count > 0) 
                                {
                                    zIgnoreFlag = true;
                                }
                            }
                        }
                         
                        if (zIgnoreFlag == false)
                        {
                            if (m_ISMLoginInfo_UpdateStockCatalogue.ISMServer.UpdateStockCatalogueRecord(zStockCatalogue, 1) == null)
                            {
                                ++m_Updated;
                                UpdateRecordsUpdated(m_Updated);
                                m_RecUpdated = true;
                                 
                            }
                        }
                        else
                        {
                            ++m_Ignored;
                            UpdateRecordsIgnored(m_Ignored);
                        }
                    }
                    else
                    {
                         
                        if (m_ISMLoginInfo_AddToStockCatalogue.ISMServer.AddStockCatalogueRecord(zStockCatalogue) == true)
                        {
                            ++m_Inserted;
                            UpdateRecordsInserted(m_Inserted);
                            m_RecInserted = true;
                             
                        }
                    }
                }
            }
            else if (zStockCatalogue.m_StockData.StockCode != null && zStockCatalogue.m_StockData.StockCodeOld != null &&
                    zStockCatalogue.m_StockData.StockCode != "" && zStockCatalogue.m_StockData.StockCodeOld != "")  
            {
                if (!m_ISMLoginInfo_StockExist.ISMServer.IsStockCodeExistInStockCatalogue(zStockCatalogue.m_StockData.StockCodeOld))
                {
                    if (!m_ISMLoginInfo_StockExist.ISMServer.IsStockCodeExistInStockCatalogue(zStockCatalogue.m_StockData.StockCode))
                    {
                         
                        if (m_ISMLoginInfo_AddToStockCatalogue.ISMServer.AddStockCatalogueRecord(zStockCatalogue) == true)
                        {
                            ++m_Inserted;
                            UpdateRecordsInserted(m_Inserted);
                            m_RecInserted = true;
                             
                        }
                    }
                    else
                    {
                         
                         
                         
                         
                         
                         
                         
                         
                         
                         

                         

                         
                        DataSet zDs = m_ISMLoginInfo_UpdateStockCatalogue.ISMServer.UpdateStockCatalogueRecord(zStockCatalogue, 0);
                        bool zIgnoreFlag = false;
                        if (zDs != null)
                        {
                            if (zDs.Tables.Count > 0)
                            {
                                if (zDs.Tables[0].Rows.Count > 0) 
                                {
                                    zIgnoreFlag = true;
                                }
                            }
                        }
                         
                        if (zIgnoreFlag == false)
                        {
                            if (m_ISMLoginInfo_UpdateStockCatalogue.ISMServer.UpdateStockCatalogueRecord(zStockCatalogue, 1) == null)
                            {
                                ++m_Updated;
                                UpdateRecordsUpdated(m_Updated);
                                m_RecUpdated = true;
                                 
                            }
                        }
                        else
                        {
                            ++m_Ignored;
                            UpdateRecordsIgnored(m_Ignored);
                        }

                    }
                }
                else
                {
                     
                     
                     
                     
                     
                    if (!m_ISMLoginInfo_StockExist.ISMServer.IsStockCodeExistInStockCatalogue(zStockCatalogue.m_StockData.StockCode))
                    {
                         
                        if (m_ISMLoginInfo_AddToStockCatalogue.ISMServer.AddStockCatalogueRecord(zStockCatalogue) == true)
                        {
                             
                            int AUpdateRecord = 0;
                            m_ISMLoginInfo_AddToStockCatalogue.ISMServer.UpdateStockCatalogueNewStockCode(zStockCatalogue, ref AUpdateRecord);

                            if (AUpdateRecord == 1)
                            {
                                ++m_Updated;
                                UpdateRecordsUpdated(m_Updated);
                                m_RecUpdated = true;

                            }
                            else
                            {
                                ++m_Ignored;
                                UpdateRecordsIgnored(m_Ignored);
                            }
                        }
                    }
                    else
                    {
                        int AUpdateRecord = 0;
                        m_ISMLoginInfo_AddToStockCatalogue.ISMServer.UpdateStockCatalogueNewStockCode(zStockCatalogue, ref AUpdateRecord);

                        if (AUpdateRecord == 1)
                        {
                            ++m_Updated;
                            UpdateRecordsUpdated(m_Updated);
                            m_RecUpdated = true;

                        }
                        else
                        {
                            ++m_Ignored;
                            UpdateRecordsIgnored(m_Ignored);
                        }
                    }
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     

                }
            }
        }
        else
          break;
      }
    }

     
     
     
     
     
     
    public String ReadUntil(StreamReader sr, String delim)
    {
        StringBuilder sb = new StringBuilder();
        bool found = false;

        while (!found && !sr.EndOfStream)
        {
            for (int i = 0; i < delim.Length; i++)
            {
                Char c = (char)sr.Read();
                sb.Append(c);

                if (c != delim[i])
                    break;

                if (i == delim.Length - 1)
                {
                    sb.Remove(sb.Length - delim.Length, delim.Length);
                    found = true;
                }
            }
        }

        return sb.ToString();
    }

     
     
     
     
     
     
    private void ImportStockCatalogue()
    {
      m_ImportExceptionGeneratedFlag = false; 

      try
      {
        using (StreamReader zStreamReader = new StreamReader(m_FileRead))
        {
          bool zReadRec = false;
          bool zHeaderFlag = false;  
          bool zEOFBlankFlag = false; 
          long zCurrentLineIndex = 1; 
          while (!zStreamReader.EndOfStream && !m_EndThread)
          {
            zReadRec = true;
            zHeaderFlag = false;  
            zEOFBlankFlag = false; 

             
             
             

             
             
             
            ISMDAL.TableColumnName.ISMStockCatalogue zISMStockCatalogue = new ISMDAL.TableColumnName.ISMStockCatalogue(ReadUntil(zStreamReader,"\r\n"),
                                                                                                                m_ISMLoginInfo.Params.ReportFolder,
                                                                                                                m_ISMLoginInfo,
                                                                                                                zCurrentLineIndex,
                                                                                                                ref zReadRec,
                                                                                                                ref zHeaderFlag,
                                                                                                                ref zEOFBlankFlag);
             
             
             
             
             
             
             
             
             
             
             
             

            zCurrentLineIndex++; 

            if (zReadRec)
            {
                m_RecRead = zReadRec;

                 
                 
                if (!zHeaderFlag && !zEOFBlankFlag) 
                {
                    m_StockCatalogue.Add(zISMStockCatalogue); 
                }
                else
                { 
                    
                }
            }
            else
            {
                 
                if (!m_ImportExceptionGeneratedFlag)
                {
                    m_ImportExceptionGeneratedFlag = true;
                     
                     
                }
                 

                m_CSVErrors++;
                UpdateCSVErrors(m_CSVErrors);
            }

             
            if (!zHeaderFlag && !zEOFBlankFlag)
            {
                ++m_RecordsRead;
                UpdateRecordsRead(m_RecordsRead);
            }
          }


          zStreamReader.Close();
        }
      }
      catch (Exception ex)
      {
         
        MessageBox.Show("Error reading file: " + m_FileRead + ". \rError received: " + ex.Message.ToString() +"\r\r\rStackTrace:"+ex.StackTrace,
                        "ImportStockCatalogue");
      }
    }
  }

}
