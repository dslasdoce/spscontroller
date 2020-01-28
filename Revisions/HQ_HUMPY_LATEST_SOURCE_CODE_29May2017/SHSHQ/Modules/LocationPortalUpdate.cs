
using System;
using System.Data;
using System.Windows.Forms;

using System.Text;
using System.Text.RegularExpressions;   
using System.Net.NetworkInformation;    

using ISMDAL.TableColumnName;
using DevExpress.XtraGrid.Views.Base;

namespace ISM.Modules
{
  public partial class LocationPortalUpdate : ISMBaseWorkSpace
  {
    private EnumFrmMode m_FrmMode;
    int m_PortalID = 0;
    int m_ReaderPortalID = 0;
    int m_AntennaReaderID = 0;
    int m_ReaderID = 0; 
    private enum ControlType : int { Portal = 0, Reader = 1, Antenna = 2 };

    private string mPrevName_1A = "";
    private string mPrevName_1B = "";
    private string mPrevName_2A = "";
    private string mPrevName_2B = "";
    private string mPrevName_3A = "";
    private string mPrevName_3B = "";
    private string mPrevName_4A = "";
    private string mPrevName_4B = "";
    private string mPrevName_5A = "";
    private string mPrevName_5B = "";

    public LocationPortalUpdate(ISMLoginInfo AISMLoginInfo, EnumFrmMode AMode)
      : base(AISMLoginInfo)
    {
      InitializeComponent();
      m_FrmMode = AMode;
      m_FrmMode = EnumFrmMode.EDIT; 
    }

    private void LocationPortal_Load(object sender, EventArgs e)
    {
       
      try
      {
        rdoPortalType.EditValue = "PASSIVE"; 

        if (m_FrmMode == EnumFrmMode.EDIT)
        {
          btnPortalAdd.Text = "Save";
          btnReaderAdd.Text = "Save";
          btnAntennaAdd.Text = "Save";

          luExistingPortalName.Top = txtNewPortalName.Top;
          luExistingPortalName.Left = txtNewPortalName.Left;

          lblExistingPortal.Top = lblNewPortal.Top;
          lblExistingPortal.Left = lblNewPortal.Left;

          lblNewPortal.Top = lblPortalDesc.Top;
          txtNewPortalName.Top = txtDescription.Top;

          lblNewPortal.Visible = false;
          txtNewPortalName.Visible = false;
           
          lblReaderCaption.Top = lblReaderName.Top;
          lblReaderCaption.Left = lblReaderName.Left;
          luRFIDReader.Top = txtReaderName.Top;
          luRFIDReader.Left = txtReaderName.Left;
          lblReaderName.Visible = false;
          txtReaderName.Visible = false;

          lblExistingAntennaID.Top = lblNewAntenna.Top;
          lblExistingAntennaID.Left = lblNewAntenna.Left;

          luAntenna.Top = luNewAntenna.Top;
          luAntenna.Left = luNewAntenna.Left;

          lblNewAntenna.Visible = false;
          luNewAntenna.Visible = false;

          luAntenna.Enabled = false; 
          luRFIDReader.Enabled = false; 
          rdoPortalType.Enabled = false;  
        }

        SetLookUpEditCaption();    
        LoadPortalMetaData();    
        ClearControls();
        SetGridCaption();
        rdoReaderType.Enabled = false;  
        txtPoll.Properties.Mask.EditMask = "\\d{0,4}";  
        txtPoll.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;  

        luReaderPortalsLoading(); 
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Pinning Station Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }


    }
    private void SetLookUpEditCaption()
    {
       
      try
      {
        luReaderPortal.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalName, 140, "Portal Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.Description, 200, ISMPortal.Description),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalType, 140, ISMPortal.PortalType)});
        luReaderPortal.Properties.DisplayMember = ISMPortal.PortalName;
        luReaderPortal.Properties.ValueMember = ISMPortal.PortalName;

         
        #region A/B swapping Section Crane Group
        luReaderPortal1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalName, 140, "Portal Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.Description, 200, ISMPortal.Description),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalType, 140, ISMPortal.PortalType)});
        luReaderPortal1.Properties.DisplayMember = ISMPortal.PortalName;
        luReaderPortal1.Properties.ValueMember = ISMPortal.PortalName;

        luReaderPortal2.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalName, 140, "Portal Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.Description, 200, ISMPortal.Description),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalType, 140, ISMPortal.PortalType)});
        luReaderPortal2.Properties.DisplayMember = ISMPortal.PortalName;
        luReaderPortal2.Properties.ValueMember = ISMPortal.PortalName;

        luReaderPortal3.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalName, 140, "Portal Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.Description, 200, ISMPortal.Description),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalType, 140, ISMPortal.PortalType)});
        luReaderPortal3.Properties.DisplayMember = ISMPortal.PortalName;
        luReaderPortal3.Properties.ValueMember = ISMPortal.PortalName;

        luReaderPortal4.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalName, 140, "Portal Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.Description, 200, ISMPortal.Description),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalType, 140, ISMPortal.PortalType)});
        luReaderPortal4.Properties.DisplayMember = ISMPortal.PortalName;
        luReaderPortal4.Properties.ValueMember = ISMPortal.PortalName;

        luReaderPortal5.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalName, 140, "Portal Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.Description, 200, ISMPortal.Description),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalType, 140, ISMPortal.PortalType)});
        luReaderPortal5.Properties.DisplayMember = ISMPortal.PortalName;
        luReaderPortal5.Properties.ValueMember = ISMPortal.PortalName;

         
        luRFIDReader_1_A.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.ReaderName, 140, "Humpy Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.IPAddress, 200, "IP Address")});

        luRFIDReader_1_A.Properties.DisplayMember = ISMReaders.ReaderName;
        luRFIDReader_1_A.Properties.ValueMember = ISMReaders.ReaderName;

        luRFIDReader_2_A.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.ReaderName, 140, "Humpy Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.IPAddress, 200, "IP Address")});

        luRFIDReader_2_A.Properties.DisplayMember = ISMReaders.ReaderName;
        luRFIDReader_2_A.Properties.ValueMember = ISMReaders.ReaderName;

        luRFIDReader_3_A.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.ReaderName, 140, "Humpy Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.IPAddress, 200, "IP Address")});

        luRFIDReader_3_A.Properties.DisplayMember = ISMReaders.ReaderName;
        luRFIDReader_3_A.Properties.ValueMember = ISMReaders.ReaderName;

        luRFIDReader_4_A.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.ReaderName, 140, "Humpy Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.IPAddress, 200, "IP Address")});

        luRFIDReader_4_A.Properties.DisplayMember = ISMReaders.ReaderName;
        luRFIDReader_4_A.Properties.ValueMember = ISMReaders.ReaderName;

        luRFIDReader_5_A.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.ReaderName, 140, "Humpy Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.IPAddress, 200, "IP Address")});

        luRFIDReader_5_A.Properties.DisplayMember = ISMReaders.ReaderName;
        luRFIDReader_5_A.Properties.ValueMember = ISMReaders.ReaderName;

         
        luRFIDReader_1_B.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.ReaderName, 140, "Humpy Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.IPAddress, 200, "IP Address")});

        luRFIDReader_1_B.Properties.DisplayMember = ISMReaders.ReaderName;
        luRFIDReader_1_B.Properties.ValueMember = ISMReaders.ReaderName;

        luRFIDReader_2_B.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.ReaderName, 140, "Humpy Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.IPAddress, 200, "IP Address")});

        luRFIDReader_2_B.Properties.DisplayMember = ISMReaders.ReaderName;
        luRFIDReader_2_B.Properties.ValueMember = ISMReaders.ReaderName;

        luRFIDReader_3_B.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.ReaderName, 140, "Humpy Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.IPAddress, 200, "IP Address")});

        luRFIDReader_3_B.Properties.DisplayMember = ISMReaders.ReaderName;
        luRFIDReader_3_B.Properties.ValueMember = ISMReaders.ReaderName;

        luRFIDReader_4_B.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.ReaderName, 140, "Humpy Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.IPAddress, 200, "IP Address")});

        luRFIDReader_4_B.Properties.DisplayMember = ISMReaders.ReaderName;
        luRFIDReader_4_B.Properties.ValueMember = ISMReaders.ReaderName;

        luRFIDReader_5_B.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.ReaderName, 140, "Humpy Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.IPAddress, 200, "IP Address")});

        luRFIDReader_5_B.Properties.DisplayMember = ISMReaders.ReaderName;
        luRFIDReader_5_B.Properties.ValueMember = ISMReaders.ReaderName;

        #endregion


        if ((m_FrmMode == EnumFrmMode.EDIT) || (m_FrmMode == EnumFrmMode.REMOVE))  
        {
          luExistingPortalName.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalName, 140, "Portal Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.Description, 200, ISMPortal.Description),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalType, 140, ISMPortal.PortalType)});

          luExistingPortalName.Properties.DisplayMember = ISMPortal.PortalName;
          luExistingPortalName.Properties.ValueMember = ISMPortal.PortalName;

          luRFIDReader.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.ReaderName, 140, "Humpy Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.IPAddress, 200, "IP Address")});

          luRFIDReader.Properties.DisplayMember = ISMReaders.ReaderName;
          luRFIDReader.Properties.ValueMember = ISMReaders.ReaderName;
        }
        else
        {
          luNewAntenna.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ANTENNA_ID", 100, "Antenna ID")});

          luNewAntenna.Properties.DisplayMember = "ANTENNA_ID";
          luNewAntenna.Properties.ValueMember = "ANTENNA_ID";
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void SetGridCaption()
    {
      try
      {
        ColumnView ColView = gridPortal.MainView as ColumnView;
        string[] zFieldNames = new string[] { "PORTAL_NAME", "TYPE", "DESCRIPTION" };
        DevExpress.XtraGrid.Columns.GridColumn zColumn;
        ColView.Columns.Clear();
        for (int i = 0; i < zFieldNames.Length; i++)
        {
          zColumn = ColView.Columns.AddField(zFieldNames[i]);
          zColumn.VisibleIndex = i;
        }
        gridView2.Columns[0].Caption = "Portal Name";
        gridView2.Columns[0].Width = 100;

        gridView2.Columns[1].Caption = "Type";
        gridView2.Columns[1].Width = 80;
        gridView2.Columns[1].Visible = false; 

        gridView2.Columns[2].Caption = "Description";
        gridView2.Columns[2].Width = 150;


        ColView = gridReader.MainView as ColumnView;
         
         
        zFieldNames = new string[] { "Priority","PORTAL_NAME", "READER_NAME", "DESCRIPTION", "TYPE", "IP_ADDRESS", "POLL_TIME", "POWER_ON_OFF" };

        ColView.Columns.Clear();
        for (int i = 0; i < zFieldNames.Length; i++)
        {
          zColumn = ColView.Columns.AddField(zFieldNames[i]);
          zColumn.VisibleIndex = i;
        }
        gridView3.Columns[0].Caption = "PID"; 
        gridView3.Columns[0].Width = 50;
        gridView3.Columns[0].Visible = false; 

        gridView3.Columns[1].Caption = "Group Name";
        gridView3.Columns[1].Width = 120;

        gridView3.Columns[2].Caption = "Humpy Name";
        gridView3.Columns[2].Width = 120;

        gridView3.Columns[3].Caption = "Description";
        gridView3.Columns[3].Width = 250;
        gridView3.Columns[3].Visible = false; 

        gridView3.Columns[4].Caption = "Type";
        gridView3.Columns[4].Width = 100;
        gridView3.Columns[4].Visible = false; 

        gridView3.Columns[5].Caption = "IP Address";
        gridView3.Columns[5].Width = 150;

        gridView3.Columns[6].Caption = "Poll Time";
        gridView3.Columns[6].Width = 80;
        gridView3.Columns[6].Visible = false; 

        gridView3.Columns[7].Caption = "Master";
        gridView3.Columns[7].Width = 100;

        ColView = gridAntenna.MainView as ColumnView;
        zFieldNames = new string[] { "READER_NAME", "ANTENNA_ID", "TX_POWER", "RSSI_THRESHOLD", "POWER_ON_OFF" };

        ColView.Columns.Clear();
        for (int i = 0; i < zFieldNames.Length; i++)
        {
          zColumn = ColView.Columns.AddField(zFieldNames[i]);
          zColumn.VisibleIndex = i;
        }
        gridView1.Columns[0].Caption = "Humpy Name";
        gridView1.Columns[0].Width = 180;

        gridView1.Columns[1].Caption = "Antenna ID";
        gridView1.Columns[1].Width = 100;

        gridView1.Columns[2].Caption = "Tx Power";
        gridView1.Columns[2].Width = 100;

        gridView1.Columns[3].Caption = "Threshold";
        gridView1.Columns[3].Width = 100;

        gridView1.Columns[4].Caption = "Status";
        gridView1.Columns[4].Width = 100;
      }
      catch
      {
        MessageBox.Show("System Error. Contact System Administrator", "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void LoadPortalMetaData()
    {
      try
      {
        DataSet ds = m_ISMLoginInfo.ISMServer.GetLocationPortalMetaData();
        if (ds != null)
        {
          luReaderPortal.Properties.DataSource = ds.Tables[ISMPortal.TableName].DefaultView;
          luAntennaReader.Properties.DataSource = ds.Tables[ISMReaders.TableName].DefaultView;

          if ((m_FrmMode == EnumFrmMode.EDIT) || (m_FrmMode == EnumFrmMode.REMOVE))
          {
            luExistingPortalName.Properties.DataSource = ds.Tables[ISMPortal.TableName].DefaultView;
            luExistingPortalName.Properties.BestFit();
            luRFIDReader.Properties.DataSource = ds.Tables[ISMReaders.TableName].DefaultView;

             
             
            luReaderPortal1.Properties.DataSource = ds.Tables[ISMPortal.TableName].DefaultView;
            luReaderPortal1.Properties.BestFit();
            luRFIDReader_1_A.Properties.DataSource = ds.Tables[ISMReaders.TableName].DefaultView;
            luRFIDReader_1_B.Properties.DataSource = ds.Tables[ISMReaders.TableName].DefaultView;
             
            luReaderPortal2.Properties.DataSource = ds.Tables[ISMPortal.TableName].DefaultView;
            luReaderPortal2.Properties.BestFit();
            luRFIDReader_2_A.Properties.DataSource = ds.Tables[ISMReaders.TableName].DefaultView;
            luRFIDReader_2_B.Properties.DataSource = ds.Tables[ISMReaders.TableName].DefaultView;
             
            luReaderPortal3.Properties.DataSource = ds.Tables[ISMPortal.TableName].DefaultView;
            luReaderPortal3.Properties.BestFit();
            luRFIDReader_3_A.Properties.DataSource = ds.Tables[ISMReaders.TableName].DefaultView;
            luRFIDReader_3_B.Properties.DataSource = ds.Tables[ISMReaders.TableName].DefaultView;
             
            luReaderPortal4.Properties.DataSource = ds.Tables[ISMPortal.TableName].DefaultView;
            luReaderPortal4.Properties.BestFit();
            luRFIDReader_4_A.Properties.DataSource = ds.Tables[ISMReaders.TableName].DefaultView;
            luRFIDReader_4_B.Properties.DataSource = ds.Tables[ISMReaders.TableName].DefaultView;
             
            luReaderPortal5.Properties.DataSource = ds.Tables[ISMPortal.TableName].DefaultView;
            luReaderPortal5.Properties.BestFit();
            luRFIDReader_5_A.Properties.DataSource = ds.Tables[ISMReaders.TableName].DefaultView;
            luRFIDReader_5_B.Properties.DataSource = ds.Tables[ISMReaders.TableName].DefaultView;

          }
          gridPortal.DataSource = ds.Tables[ISMPortal.TableName].DefaultView;
          gridReader.DataSource = ds.Tables[ISMReaders.TableName].DefaultView;
          gridAntenna.DataSource = ds.Tables[ISMAntenna.TableName].DefaultView;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    #region Portal with less use in this function
    private void ClearControls()
    {
      luExistingPortalName.EditValue = null;
      txtNewPortalName.Text = "";
      txtDescription.Text = "";
      m_PortalID = 0;  
      txtPriority.Text = "1";
      rdoPortalType.EditValue = "PASSIVE"; 
       
      ClearPortalErrorIconText();
    }
    private void luExistingPortalName_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        txtStatusMsg.Text = "";  
        DataRowView zDataRowView = luExistingPortalName.Properties.GetDataSourceRowByKeyValue(luExistingPortalName.EditValue) as DataRowView;
        if (zDataRowView != null)   
        {
          m_PortalID = int.Parse(zDataRowView[ISMPortal.PortalID].ToString());
          txtDescription.Text = zDataRowView[ISMPortal.Description].ToString();
          txtNewPortalName.Text = zDataRowView[ISMPortal.PortalName].ToString();
          rdoPortalType.EditValue = zDataRowView[ISMPortal.PortalType].ToString();  
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void ClearPortalErrorIconText()
    {
      try
      {
        dxErrorProvider.SetError(txtNewPortalName, null);
        dxErrorProvider.SetError(txtDescription, null);
        dxErrorProvider.SetError(luExistingPortalName, null);
        dxErrorProvider.SetError(rdoPortalType, null);      
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void ClearReaderErrorIconText()
    {
      try
      {
        dxErrorProvider.SetError(luReaderPortal, null);
        dxErrorProvider.SetError(txtReaderName, null);
        dxErrorProvider.SetError(rdoReaderType, null);
        dxErrorProvider.SetError(txtReaderDesc, null);
        dxErrorProvider.SetError(txtIPAddress, null);
        dxErrorProvider.SetError(ChkReaderPowerState, null);
        dxErrorProvider.SetError(luRFIDReader, null);
        dxErrorProvider.SetError(txtPoll, null);  
        dxErrorProvider.SetError(txtPriority, null); 
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void ClearAntennaErrorIconText()
    {
      try
      {
        dxErrorProvider.SetError(luAntennaReader, null);
        dxErrorProvider.SetError(luNewAntenna, null);
        dxErrorProvider.SetError(luAntenna, null);
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private bool Validation(int AType)
    {
      bool zResult = false;
      bool zValidationFail = true;  
      try
      {
        if ((int)ControlType.Portal == AType)
        {
          ClearPortalErrorIconText();
          if (m_FrmMode == EnumFrmMode.CREATE || m_FrmMode == EnumFrmMode.EDIT)
          {
            if (rdoPortalType.EditValue == null)
            {
              dxErrorProvider.SetError(rdoPortalType, "Select a Portal Type");
              zValidationFail = false;
              rdoPortalType.Focus();
            }

            if (txtDescription.Text.Trim() == "")
            {
              dxErrorProvider.SetError(txtDescription, "Enter a description for this Portal");
              zValidationFail = false;
              txtDescription.Focus();
            }

          }
          if (m_FrmMode == EnumFrmMode.CREATE)
          {
            if (txtNewPortalName.Text.Trim() == "")
            {
              dxErrorProvider.SetError(txtNewPortalName, "Enter a name for this Portal Name");
              zValidationFail = false;
              txtNewPortalName.Focus();
            }
             
             
            else if (m_ISMLoginInfo.ISMServer.IsPortalExist(0, txtNewPortalName.Text.Trim(), "", "", ""))
            {
              dxErrorProvider.SetError(txtNewPortalName, "Portal Name already exists");
              zValidationFail = false;
              txtNewPortalName.Focus();
            }
          }
          else
          {

            if (luExistingPortalName.EditValue == null)
            {
              dxErrorProvider.SetError(luExistingPortalName, "Please select a Portal Type");
              zValidationFail = false;
              luExistingPortalName.Focus();

            }

          }
        }
        else if ((int)ControlType.Reader == AType)
        {
          ClearReaderErrorIconText();
        
          if (m_FrmMode == EnumFrmMode.EDIT)
          {
               
              if (rdoReaderType.EditValue.ToString() == "ACTIVE")
              {
                  if (int.Parse(txtPriority.Text) <= 0 || int.Parse(txtPriority.Text) > 10)
                  {
                      dxErrorProvider.SetError(txtPriority, "Priority ID number range is (0 - 10)");
                      zValidationFail = false;
                      txtPriority.Focus();
                  }
                  else
                  {
                      int zCurrPriorityID = m_ISMLoginInfo.ISMServer.aRFIDReaderPriorityTool_GetCurrentReaderPID(m_ReaderID, "ACTIVE");
                      if (zCurrPriorityID != int.Parse(txtPriority.Text))
                      {
                           
                           
                           
                           
                           
                          if (!m_ISMLoginInfo.ISMServer.aRFIDReaderPriorityTool_CheckReaderPriority(Convert.ToInt16(txtPriority.Text),
                                                                                                  m_PortalID,
                                                                                                  "ACTIVE"))
                          {
                               
                               

                               
                              dxErrorProvider.SetError(txtPriority, "Priority ID:" + txtPriority.Text + " has been used");
                              zValidationFail = false;
                              txtPriority.Focus();
                          }
                      }
                  }
              }
               

            if (txtPoll.EditValue == null)  
            {
              dxErrorProvider.SetError(txtPoll, "Enter a Reader Poll Time");
              zValidationFail = false;
              txtPoll.Focus();
            }
             
            if (txtPoll.EditValue != null)
            {
              if (rdoReaderType.EditValue.ToString() == "ACTIVE")
              {
                if (int.Parse(txtPoll.EditValue.ToString()) < int.Parse(m_ISMLoginInfo.Params.ActiveReaderPollTime))  
                {
                  dxErrorProvider.SetError(txtPoll, "Min Reader Poll Time is " + m_ISMLoginInfo.Params.ActiveReaderPollTime);
                  zValidationFail = false;
                  txtPoll.Focus();
                }
              }
              else
              {
                if (int.Parse(txtPoll.EditValue.ToString()) < int.Parse(m_ISMLoginInfo.Params.PassiveReaderPollTime))  
                {
                  dxErrorProvider.SetError(txtPoll, "Min Reader Poll Time is " + m_ISMLoginInfo.Params.PassiveReaderPollTime);
                  zValidationFail = false;
                  txtPoll.Focus();
                }
              }
            }

            if (txtIPAddress.EditValue == null)
            {
              dxErrorProvider.SetError(txtIPAddress, "Enter a valid IP Address");
              zValidationFail = false;
              txtIPAddress.Focus();

            }
            if (!IsValidIP(txtIPAddress.Text))
            {
              dxErrorProvider.SetError(txtIPAddress, "Enter a valid IP Address");
              zValidationFail = false;
              txtIPAddress.Focus();

            }
            if (txtReaderDesc.EditValue == null)
            {
                dxErrorProvider.SetError(txtReaderDesc, "Enter Humpy Description");
              zValidationFail = false;
              txtReaderDesc.Focus();

            }
            if (rdoReaderType.EditValue == null)
            {
              dxErrorProvider.SetError(rdoReaderType, "Select a Reader Type");
              zValidationFail = false;
              rdoReaderType.Focus();

            }
            if (luRFIDReader.EditValue == null)
            {
                dxErrorProvider.SetError(luRFIDReader, "Select a Humpy");
              zValidationFail = false;
              luRFIDReader.Focus();

            }
            if (luReaderPortal.EditValue == null)
            {
              dxErrorProvider.SetError(luReaderPortal, "Select a Portal");
              zValidationFail = false;
              luReaderPortal.Focus();
            }

          }
          if (m_FrmMode == EnumFrmMode.CREATE)
          {
               
              if (rdoReaderType.EditValue.ToString() == "ACTIVE")
              {
                  if (int.Parse(txtPriority.Text) <= 0 || int.Parse(txtPriority.Text) > 10)
                  {
                      dxErrorProvider.SetError(txtPriority, "Priority ID number range is (0 - 10)");
                      zValidationFail = false;
                      txtPriority.Focus();
                  }
                  else
                  {
                       
                       
                       
                       
                       
                      if (!m_ISMLoginInfo.ISMServer.aRFIDReaderPriorityTool_CheckReaderPriority(Convert.ToInt16(txtPriority.Text),
                                                                                              m_PortalID,
                                                                                              "ACTIVE"))
                      {
                           
                           

                           
                          dxErrorProvider.SetError(txtPriority, "Priority ID:" + txtPriority.Text + " has been used");
                          zValidationFail = false;
                          txtPriority.Focus();
                      }
                  }
              }
               

            if (txtPoll.EditValue == null)  
            {
                dxErrorProvider.SetError(txtPoll, "Enter a Humpy Poll Time");
              zValidationFail = false;
              txtPoll.Focus();
            }
             
            if (txtPoll.EditValue != null)
            {
              if (rdoReaderType.EditValue.ToString() == "ACTIVE")
              {
                if (int.Parse(txtPoll.EditValue.ToString()) < int.Parse(m_ISMLoginInfo.Params.ActiveReaderPollTime))  
                {
                  dxErrorProvider.SetError(txtPoll, "Min Reader Poll Time is " + m_ISMLoginInfo.Params.ActiveReaderPollTime);
                  zValidationFail = false;
                  txtPoll.Focus();
                }
              }
              else
              {
                if (int.Parse(txtPoll.EditValue.ToString()) < int.Parse(m_ISMLoginInfo.Params.PassiveReaderPollTime))  
                {
                  dxErrorProvider.SetError(txtPoll, "Min Reader Poll Time is " + m_ISMLoginInfo.Params.PassiveReaderPollTime);
                  zValidationFail = false;
                  txtPoll.Focus();
                }

              }
            }

            if (txtIPAddress.EditValue == null)
            {
              dxErrorProvider.SetError(txtIPAddress, "Enter valid IP Address");
              zValidationFail = false;
              txtIPAddress.Focus();

            }
             
            if (m_ISMLoginInfo.ISMServer.IsPortalExist(2, "", "", txtIPAddress.Text.Trim(), ""))
            {
              dxErrorProvider.SetError(txtIPAddress, "IP Address already exists");
              zValidationFail = false;
              txtIPAddress.Focus();
            }

            if (txtReaderDesc.EditValue == null)
            {
                dxErrorProvider.SetError(txtReaderDesc, "Enter Humpy Description");
              zValidationFail = false;
              txtReaderDesc.Focus();

            }
            if (rdoReaderType.EditValue == null)
            {
                dxErrorProvider.SetError(rdoReaderType, "Select a Humpy Type");
              zValidationFail = false;
              rdoReaderType.Focus();

            }
            if (txtReaderName.Text.Trim() == "")
            {
                dxErrorProvider.SetError(txtReaderName, "Enter Humpy Name");
              zValidationFail = false;
              txtReaderName.Focus();

            }
             
            if (m_ISMLoginInfo.ISMServer.IsPortalExist(1, "", txtReaderName.Text.Trim(), "", ""))
            {
              dxErrorProvider.SetError(txtReaderName, "Reader Name already exists");
              zValidationFail = false;
              txtReaderName.Focus();
            }

            if (luReaderPortal.EditValue == null)
            {
              dxErrorProvider.SetError(luReaderPortal, "Select a Portal");
              zValidationFail = false;
              luReaderPortal.Focus();
            }
          }

          if (m_FrmMode == EnumFrmMode.REMOVE)
          {
            if (luRFIDReader.EditValue == null)
            {
              dxErrorProvider.SetError(luRFIDReader, "Select a Reader");
              zValidationFail = false;
              luRFIDReader.Focus();

            }
            if (luReaderPortal.EditValue == null)
            {
              dxErrorProvider.SetError(luReaderPortal, "Select a Portal");
              zValidationFail = false;
              luReaderPortal.Focus();
            }
          }
        }
        else if ((int)ControlType.Antenna == AType)
        {
          ClearAntennaErrorIconText();

          if (m_FrmMode == EnumFrmMode.CREATE)
          {
            if (luNewAntenna.EditValue == null)
            {
              dxErrorProvider.SetError(luNewAntenna, "Select a Antenna");
              zValidationFail = false;
              luNewAntenna.Focus();
            }
            if (m_ISMLoginInfo.ISMServer.IsPortalExist(3, "", luAntennaReader.Text.Trim(), "", luNewAntenna.Text.Trim()))
            {
              dxErrorProvider.SetError(luNewAntenna, "Antenna ID already exists");
              zValidationFail = false;
              luNewAntenna.Focus();
            }
            if (luAntennaReader.EditValue == null)
            {
                dxErrorProvider.SetError(luAntennaReader, "Select a Humpy");
              zValidationFail = false;
              luNewAntenna.Focus();
            }
          }
          else
          {
            if (luAntenna.EditValue == null)
            {
              dxErrorProvider.SetError(luAntenna, "Select a Antenna");
              zValidationFail = false;
              luAntenna.Focus();
            }
            if (luAntennaReader.EditValue == null)
            {
                dxErrorProvider.SetError(luAntennaReader, "Select a Humpy");
              zValidationFail = false;
              luNewAntenna.Focus();
            }
          }
        }
        if (zValidationFail)
          zResult = zValidationFail;

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      return zResult;
    }

    private void SavePortalRecord()
    {
       
      try
      {
        ISMPortal.Data zPortalData = PackagePortalData();
        m_ISMLoginInfo.ISMServer.LocationPortalInsertRecd(zPortalData);
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void RemovePortalRecord()
    {
       
      try
      {
        ISMPortal.Data zPortalData = PackagePortalData();
        m_ISMLoginInfo.ISMServer.LocationPortalRemoveRecd(zPortalData);
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      ClearControls();
    }

    private bool DeviceCanBePinged()
    {
      bool zNPortReplied = false;  
      Ping zPingNPort = new Ping();
      PingOptions zOptions = new PingOptions();
      zOptions.DontFragment = true;
       
      string zData = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
      byte[] zBuffer = Encoding.ASCII.GetBytes(zData);
      int zTimeOut = 120;
      try
      {
        PingReply zReply = zPingNPort.Send(txtIPAddress.Text, zTimeOut, zBuffer, zOptions);
        if (zReply.Status == IPStatus.Success)
          zNPortReplied = true;
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         
      }
      return zNPortReplied;
    }

    public bool IsValidIP(string AIPAddress)
    {
      bool zValid = false;
       
      try
      {
         
        const string zIPRegExp = "(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)";
         
        Regex zRegEx = new Regex(zIPRegExp);

        if (AIPAddress != "")  
          zValid = zRegEx.IsMatch(AIPAddress, 0);  
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

      return zValid;
    }

    private void txtIPAddress_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
    {

      string zTmpStr;
      if (IsValidIP(txtIPAddress.Text))  
      {
        if (DeviceCanBePinged())
        {
          zTmpStr = string.Format("The device located at '{0}' returned a valid response.", txtIPAddress.Text);
          MessageBox.Show(zTmpStr, lblHeader.Text, MessageBoxButtons.OK);
        }
        else
        {
          zTmpStr = string.Format("The device located at '{0}' cannot be be reached.", txtIPAddress.Text);
          MessageBox.Show(zTmpStr, lblHeader.Text, MessageBoxButtons.OK);
        }
      }
      else  
      {
        txtIPAddress.SelectAll();
        txtIPAddress.Focus();
        zTmpStr = "Please enter a valid IP Address";
        MessageBox.Show(zTmpStr, lblHeader.Text);
      }

    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
      try  
      {
        if (m_FrmMode == EnumFrmMode.REMOVE)
        {
           
          string zPortalName;
          dxErrorProvider.SetError(luExistingPortalName, null);
          if (luExistingPortalName.EditValue == null)
          {
            dxErrorProvider.SetError(luExistingPortalName, "Select a Portal to be removed");
             
            return;
          }
           
          if (m_ISMLoginInfo.ISMServer.IsPortalExist(4, luExistingPortalName.Text.Trim(), "", "", ""))
          {
            MessageBox.Show("Unable to Remove Portal :" + luExistingPortalName.Text + "\n\nSome Readers are referring Portal " + luExistingPortalName.Text + "", "Remove Portal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
          }

          string zMsgStr = string.Format("Are you sure? Do you want to deactivate Portal\n\nPortal Name : {0:s}", luExistingPortalName.Text.Trim());
          DialogResult zReply = MessageBox.Show(zMsgStr, "Remove Portal", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
          if (zReply == DialogResult.Yes)
          {
            RemovePortalRecord();
            zPortalName = luExistingPortalName.Text;
            ClearControls();
            txtStatusMsg.Text = "Portal : " + zPortalName + " has been removed";
            LoadPortalMetaData();
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }


    }

    private void txtNewPortalName_EditValueChanged(object sender, EventArgs e)
    {
      txtStatusMsg.Text = "";
    }

    private void btnPortalAdd_Click(object sender, EventArgs e)
    {
      try
      {
        if (Validation((int)ControlType.Portal))
        {
          string zPortalName;
          string zMessage;
          zPortalName = txtNewPortalName.Text;
          if (m_FrmMode == EnumFrmMode.CREATE)
          {
            zMessage = String.Format("Do you want create Portal\n\nPortal Name : {0} ?", zPortalName);  
            DialogResult zReply = MessageBox.Show(zMessage, "Create Portal", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);  
            if (zReply == DialogResult.Yes)  
            {

              SavePortalRecord();
              ClearControls();
              txtStatusMsg.Text = "Portal " + zPortalName + " has been created";
              LoadPortalMetaData();
            }
          }
          else
          {
            zMessage = String.Format("Do you want update Portal\n\nPortal Name : {0} ?", zPortalName);  
            DialogResult zReply = MessageBox.Show(zMessage, "Create Portal", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);  
            if (zReply == DialogResult.Yes)  
            {
              UpdatePortalRecord();
              ClearControls();
              txtStatusMsg.Text = "Portal " + zPortalName + " has been updated";
              LoadPortalMetaData();
            }
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }


    private void luReaderPortal_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        if (luReaderPortal.EditValue != null)  
          luRFIDReader.Enabled = true;
        else
          luRFIDReader.Enabled = false;

        if (luReaderPortal.EditValue != null && luRFIDReader.EditValue == null)
        {

          luRFIDReader.EditValueChanged -= new System.EventHandler(luRFIDReader_EditValueChanged);

          DataRowView zDataRowView = luReaderPortal.Properties.GetDataSourceRowByKeyValue(luReaderPortal.EditValue) as DataRowView;
          if (zDataRowView != null)
          {
            int zPortalID = int.Parse(zDataRowView[ISMPortal.PortalID].ToString());
            rdoReaderType.EditValue = zDataRowView[ISMPortal.PortalType].ToString();
             
            if (zDataRowView[ISMPortal.PortalType].ToString() == "ACTIVE")
              txtPoll.EditValue = m_ISMLoginInfo.Params.ActiveReaderPollTime;
            else
              txtPoll.EditValue = m_ISMLoginInfo.Params.PassiveReaderPollTime;

             
            if (zDataRowView[ISMPortal.PortalType].ToString() == "ACTIVE")
            {
                txtPriority.Enabled = true;
            }
            else if (zDataRowView[ISMPortal.PortalType].ToString() == "PASSIVE")
            {
                txtPriority.Enabled = false;
                txtPriority.Text = "1";
            }
             

            m_PortalID = zPortalID;
            DataSet ds = m_ISMLoginInfo.ISMServer.GetPortalORReaderData(1, zPortalID, "");
            if (ds != null)
            {
              luRFIDReader.Properties.DataSource = ds.Tables[0].DefaultView;
               
              if (m_FrmMode == EnumFrmMode.CREATE && zDataRowView[ISMPortal.PortalType].ToString() == "PASSIVE")
              {
                if (ds.Tables[0].Rows.Count > 0)
                {
                  DataRow dr = ds.Tables[0].Rows[0];  
                  txtPoll.EditValue = dr[ISMReaders.PollTime].ToString();
                  if (dr[ISMReaders.PowerStatus].ToString() == "1") 
                    ChkReaderPowerState.Checked = false;
                  else
                    ChkReaderPowerState.Checked = true;

                  ChkReaderPowerState.Enabled = false;
                  txtPoll.Enabled = false;
                }
                else
                {
                  txtPoll.EditValue = m_ISMLoginInfo.Params.PassiveReaderPollTime;
                  txtPoll.Enabled = true;
                  ChkReaderPowerState.Enabled = true;
                  ChkReaderPowerState.Checked = false;
                }
              }
              else if (m_FrmMode == EnumFrmMode.CREATE && zDataRowView[ISMPortal.PortalType].ToString() == "ACTIVE")
              {
                  if (ds.Tables[0].Rows.Count > 0)
                  {
                      DataRow dr = ds.Tables[0].Rows[0];  
                      txtPoll.EditValue = dr[ISMReaders.PollTime].ToString();
                      if (dr[ISMReaders.PowerStatus].ToString() == "1")
                          ChkReaderPowerState.Checked = true;
                      else
                          ChkReaderPowerState.Checked = false;
                      ChkReaderPowerState.Enabled = false;
                      txtPoll.Enabled = false;
                  }
                  else
                  {
                      txtPoll.EditValue = m_ISMLoginInfo.Params.ActiveReaderPollTime;
                      txtPoll.Enabled = true;
                      ChkReaderPowerState.Enabled = true;
                      ChkReaderPowerState.Checked = false;
                  }
              }
              else
              {
                  if (m_FrmMode == EnumFrmMode.EDIT)  
                  {
                      txtPoll.Enabled = true;
                      ChkReaderPowerState.Enabled = true;
                      ChkReaderPowerState.Checked = false;
                  }
              }
            }
          }
          luRFIDReader.EditValueChanged += new System.EventHandler(luRFIDReader_EditValueChanged);
        }
        txtStatusMsg.Text = "";
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void luRFIDReader_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        if (luRFIDReader.EditValue != null)
        {
          luReaderPortal.EditValueChanged -= new System.EventHandler(luReaderPortal_EditValueChanged);

          DataRowView zDataRowView = luRFIDReader.Properties.GetDataSourceRowByKeyValue(luRFIDReader.EditValue) as DataRowView;
          if (zDataRowView != null)
          {
            m_ReaderPortalID = int.Parse(zDataRowView[ISMReaders.PortalID].ToString());
            txtReaderName.Text = zDataRowView[ISMReaders.ReaderName].ToString();
            rdoReaderType.EditValue = zDataRowView[ISMReaders.ReaderType].ToString();
            txtReaderDesc.Text = zDataRowView[ISMReaders.Description].ToString();
            txtIPAddress.Text = zDataRowView[ISMReaders.IPAddress].ToString();
            txtPoll.Text = zDataRowView[ISMReaders.PollTime].ToString();  
            if (zDataRowView[ISMReaders.PowerStatus].ToString() == "1")
              ChkReaderPowerState.Checked = true;
            else
              ChkReaderPowerState.Checked = false;

            DataSet ds = m_ISMLoginInfo.ISMServer.GetPortalORReaderData(0, m_ReaderPortalID, luRFIDReader.EditValue.ToString());
            if (ds != null)
            {
              luReaderPortal.Properties.DataSource = ds.Tables[0].DefaultView;
            }

             
            m_ReaderID = int.Parse(zDataRowView[ISMReaders.ReaderID].ToString());
            txtPriority.Text = m_ISMLoginInfo.ISMServer.aRFIDReaderPriorityTool_GetCurrentReaderPID(m_ReaderID, "ACTIVE").ToString();
          }
          luReaderPortal.EditValueChanged += new System.EventHandler(luReaderPortal_EditValueChanged);
          txtStatusMsg.Text = "";
        }

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }

    private void ClearReaderControls()
    {
      luReaderPortal.EditValue = null;
      luRFIDReader.EditValue = null;
      txtReaderName.EditValue = null;
      rdoReaderType.EditValue = null;
      txtReaderDesc.EditValue = null;
      txtIPAddress.EditValue = null;
      ChkReaderPowerState.Checked = false;
      luReaderPortal.Focus();
      m_PortalID = 0;
      m_ReaderPortalID = 0;
      m_ReaderID = 0; 
      txtPoll.EditValue = null;  
      ClearReaderErrorIconText();
      LoadPortalMetaData();
    }

    private void btnReaderClear_Click(object sender, EventArgs e)
    {
      ClearReaderControls();
    }

    private void ClearAntennaControls()
    {
      luAntennaReader.EditValue = null;
      luAntenna.EditValue = null;
      luNewAntenna.EditValue = null;
      txtAntTXPower.EditValue = "10";
      txtThershold.EditValue = "-128";
      chkAntennaPowerState.Checked = false;
      luAntennaReader.Focus();
      m_PortalID = 0;
      m_ReaderPortalID = 0;
      m_ReaderID = 0; 
      m_AntennaReaderID = 0;
      ClearAntennaErrorIconText();
      LoadPortalMetaData();
    }

    private void btnAntennaClear_Click(object sender, EventArgs e)
    {
      ClearAntennaControls();
    }
    #endregion

    #region 5 Crane Group Auto Selection + Selection Changes Events (luReaderPortal1-5)
    private void UpdateHumpyRecord(string aPortalNameCraneGroupName,string aHumpyReaderName,string aMasterSlaveFlag)
    {
        try
        {
           m_ISMLoginInfo.ISMServer.SHS_PortalHumpyTools(1, aHumpyReaderName, aPortalNameCraneGroupName, aMasterSlaveFlag);
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    private ISMPortal.Data PackagePortalData()
    {
        ISMPortal.Data zPortalData = new ISMPortal.Data();
        try
        {

            if (m_FrmMode == EnumFrmMode.CREATE)
            {
                zPortalData.PortalName = txtNewPortalName.EditValue.ToString();
            }
            else
            {
                zPortalData.PortalID = m_PortalID;
                zPortalData.PortalName = luExistingPortalName.EditValue.ToString();
            }

            zPortalData.Description = txtDescription.Text;
            zPortalData.PortalType = rdoPortalType.EditValue.ToString();  
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        return zPortalData;
    }
    private void UpdatePortalRecord()
    {
         
        try
        {
            ISMPortal.Data zPortalData = PackagePortalData();
            m_ISMLoginInfo.ISMServer.LocationPortalUpdateRecd(zPortalData);
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    private void luReaderPortalsLoading() 
    {
        try
        {
            luRFIDReader_1_A.Enabled = false;
            luRFIDReader_1_B.Enabled = false;
            luRFIDReader_2_A.Enabled = false;
            luRFIDReader_2_B.Enabled = false;
            luRFIDReader_3_A.Enabled = false;
            luRFIDReader_3_B.Enabled = false;
            luRFIDReader_4_A.Enabled = false;
            luRFIDReader_4_B.Enabled = false;
            luRFIDReader_5_A.Enabled = false;
            luRFIDReader_5_B.Enabled = false;

            DataSet ds = m_ISMLoginInfo.ISMServer.GetLocationPortalMetaData();
            if (ds != null)
            {
                if (ds.Tables[ISMPortal.TableName].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[ISMPortal.TableName].Rows)
                    {
                        if (dr["PORTAL_NAME"].ToString() == SHSHQ.Properties.Settings.Default.PortalUpdate_CraneGroupName1)
                        {
                            luReaderPortal1.EditValue = luReaderPortal1.Properties.GetKeyValueByDisplayText(SHSHQ.Properties.Settings.Default.PortalUpdate_CraneGroupName1);
                        }
                        if (dr["PORTAL_NAME"].ToString() == SHSHQ.Properties.Settings.Default.PortalUpdate_CraneGroupName2)
                        {
                            luReaderPortal2.EditValue = luReaderPortal2.Properties.GetKeyValueByDisplayText(SHSHQ.Properties.Settings.Default.PortalUpdate_CraneGroupName2);
                        }
                        if (dr["PORTAL_NAME"].ToString() == SHSHQ.Properties.Settings.Default.PortalUpdate_CraneGroupName3)
                        {
                            luReaderPortal3.EditValue = luReaderPortal3.Properties.GetKeyValueByDisplayText(SHSHQ.Properties.Settings.Default.PortalUpdate_CraneGroupName3);
                        }
                        if (dr["PORTAL_NAME"].ToString() == SHSHQ.Properties.Settings.Default.PortalUpdate_CraneGroupName4)
                        {
                            luReaderPortal4.EditValue = luReaderPortal4.Properties.GetKeyValueByDisplayText(SHSHQ.Properties.Settings.Default.PortalUpdate_CraneGroupName4);
                        }
                        if (dr["PORTAL_NAME"].ToString() == SHSHQ.Properties.Settings.Default.PortalUpdate_CraneGroupName5)
                        {
                            luReaderPortal5.EditValue = luReaderPortal5.Properties.GetKeyValueByDisplayText(SHSHQ.Properties.Settings.Default.PortalUpdate_CraneGroupName5);
                        }
                    }
                }
            }
            txtStatusMsg.Text = "";
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Load Crane Group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    private void luReaderPortal1_EditValueChanged(object sender, EventArgs e)
    {
        try
        {
            if (luReaderPortal1.EditValue != null)
            {
                luRFIDReader_1_A.Enabled = true;
                luRFIDReader_1_B.Enabled = true;
            }
            else
            {
                luRFIDReader_1_A.Enabled = false;
                luRFIDReader_1_B.Enabled = false;
            }

            luRFIDReader_1_A.EditValue = null;
            luRFIDReader_1_B.EditValue = null;

            if (luReaderPortal1.EditValue != null && luRFIDReader_1_A.EditValue == null && luRFIDReader_1_B.EditValue == null)
            {

                 
                DataRowView zDataRowView = luReaderPortal1.Properties.GetDataSourceRowByKeyValue(luReaderPortal1.EditValue) as DataRowView;
                if (zDataRowView != null)
                {
                    int zPortalID = int.Parse(zDataRowView[ISMPortal.PortalID].ToString());
                    rdoReaderType.EditValue = zDataRowView[ISMPortal.PortalType].ToString();

                     
                    m_PortalID = zPortalID;

                    DataSet ds = m_ISMLoginInfo.ISMServer.GetPortalORReaderData(1, zPortalID, "");
                    if (ds != null)
                    {
                         
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                if (dr["POWER_ON_OFF"].ToString() == "1")  
                                {
                                    luRFIDReader_1_A.EditValue = luRFIDReader_1_A.Properties.GetKeyValueByDisplayText(dr["READER_NAME"].ToString());
                                    mPrevName_1A = dr["READER_NAME"].ToString();
                                }
                                else
                                {
                                    luRFIDReader_1_B.EditValue = luRFIDReader_1_B.Properties.GetKeyValueByDisplayText(dr["READER_NAME"].ToString());
                                    mPrevName_1B = dr["READER_NAME"].ToString();
                                }
                            }                           
                        }
                    }
                }
                 
            }
            txtStatusMsg.Text = "";
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    private void luReaderPortal2_EditValueChanged(object sender, EventArgs e)
    {
        try
        {
            if (luReaderPortal2.EditValue != null)
            {
                luRFIDReader_2_A.Enabled = true;
                luRFIDReader_2_B.Enabled = true;
            }
            else
            {
                luRFIDReader_2_A.Enabled = false;
                luRFIDReader_2_B.Enabled = false;
            }

            luRFIDReader_2_A.EditValue = null;
            luRFIDReader_2_B.EditValue = null;

            if (luReaderPortal2.EditValue != null && luRFIDReader_2_A.EditValue == null && luRFIDReader_2_B.EditValue == null)
            {

                 
                DataRowView zDataRowView = luReaderPortal2.Properties.GetDataSourceRowByKeyValue(luReaderPortal2.EditValue) as DataRowView;
                if (zDataRowView != null)
                {
                    int zPortalID = int.Parse(zDataRowView[ISMPortal.PortalID].ToString());
                    rdoReaderType.EditValue = zDataRowView[ISMPortal.PortalType].ToString();

                     
                    m_PortalID = zPortalID;

                    DataSet ds = m_ISMLoginInfo.ISMServer.GetPortalORReaderData(1, zPortalID, "");
                    if (ds != null)
                    {
                         
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                if (dr["POWER_ON_OFF"].ToString() == "1")  
                                {
                                    luRFIDReader_2_A.EditValue = luRFIDReader_2_A.Properties.GetKeyValueByDisplayText(dr["READER_NAME"].ToString());
                                    mPrevName_2A = dr["READER_NAME"].ToString();
                                }
                                else
                                {
                                    luRFIDReader_2_B.EditValue = luRFIDReader_2_B.Properties.GetKeyValueByDisplayText(dr["READER_NAME"].ToString());
                                    mPrevName_2B = dr["READER_NAME"].ToString();
                                }
                            }
                        }
                    }
                }
                 
            }
            txtStatusMsg.Text = "";
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    private void luReaderPortal3_EditValueChanged(object sender, EventArgs e)
    {
        try
        {
            if (luReaderPortal3.EditValue != null)
            {
                luRFIDReader_3_A.Enabled = true;
                luRFIDReader_3_B.Enabled = true;
            }
            else
            {
                luRFIDReader_3_A.Enabled = false;
                luRFIDReader_3_B.Enabled = false;
            }

            luRFIDReader_3_A.EditValue = null;
            luRFIDReader_3_B.EditValue = null;

            if (luReaderPortal3.EditValue != null && luRFIDReader_3_A.EditValue == null && luRFIDReader_3_B.EditValue == null)
            {

                 
                DataRowView zDataRowView = luReaderPortal3.Properties.GetDataSourceRowByKeyValue(luReaderPortal3.EditValue) as DataRowView;
                if (zDataRowView != null)
                {
                    int zPortalID = int.Parse(zDataRowView[ISMPortal.PortalID].ToString());
                    rdoReaderType.EditValue = zDataRowView[ISMPortal.PortalType].ToString();

                     
                    m_PortalID = zPortalID;

                    DataSet ds = m_ISMLoginInfo.ISMServer.GetPortalORReaderData(1, zPortalID, "");
                    if (ds != null)
                    {
                         
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                if (dr["POWER_ON_OFF"].ToString() == "1")  
                                {
                                    luRFIDReader_3_A.EditValue = luRFIDReader_3_A.Properties.GetKeyValueByDisplayText(dr["READER_NAME"].ToString());
                                    mPrevName_3A = dr["READER_NAME"].ToString();
                                }
                                else
                                {
                                    luRFIDReader_3_B.EditValue = luRFIDReader_3_B.Properties.GetKeyValueByDisplayText(dr["READER_NAME"].ToString());
                                    mPrevName_3B = dr["READER_NAME"].ToString();
                                }
                            }
                        }
                    }
                }
                 
            }
            txtStatusMsg.Text = "";
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    private void luReaderPortal4_EditValueChanged(object sender, EventArgs e)
    {
        try
        {
            if (luReaderPortal4.EditValue != null)
            {
                luRFIDReader_4_A.Enabled = true;
                luRFIDReader_4_B.Enabled = true;
            }
            else
            {
                luRFIDReader_4_A.Enabled = false;
                luRFIDReader_4_B.Enabled = false;
            }

            luRFIDReader_4_A.EditValue = null;
            luRFIDReader_4_B.EditValue = null;

            if (luReaderPortal4.EditValue != null && luRFIDReader_4_A.EditValue == null && luRFIDReader_4_B.EditValue == null)
            {

                 
                DataRowView zDataRowView = luReaderPortal4.Properties.GetDataSourceRowByKeyValue(luReaderPortal4.EditValue) as DataRowView;
                if (zDataRowView != null)
                {
                    int zPortalID = int.Parse(zDataRowView[ISMPortal.PortalID].ToString());
                    rdoReaderType.EditValue = zDataRowView[ISMPortal.PortalType].ToString();

                     
                    m_PortalID = zPortalID;

                    DataSet ds = m_ISMLoginInfo.ISMServer.GetPortalORReaderData(1, zPortalID, "");
                    if (ds != null)
                    {
                         
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                if (dr["POWER_ON_OFF"].ToString() == "1")  
                                {
                                    luRFIDReader_4_A.EditValue = luRFIDReader_4_A.Properties.GetKeyValueByDisplayText(dr["READER_NAME"].ToString());
                                    mPrevName_4A = dr["READER_NAME"].ToString();
                                }
                                else
                                {
                                    luRFIDReader_4_B.EditValue = luRFIDReader_4_B.Properties.GetKeyValueByDisplayText(dr["READER_NAME"].ToString());
                                    mPrevName_4B = dr["READER_NAME"].ToString();
                                }
                            }
                        }
                    }
                }
                 
            }
            txtStatusMsg.Text = "";
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    private void luReaderPortal5_EditValueChanged(object sender, EventArgs e)
    {
        try
        {
            if (luReaderPortal5.EditValue != null)
            {
                luRFIDReader_5_A.Enabled = true;
                luRFIDReader_5_B.Enabled = true;
            }
            else
            {
                luRFIDReader_5_A.Enabled = false;
                luRFIDReader_5_B.Enabled = false;
            }

            luRFIDReader_5_A.EditValue = null;
            luRFIDReader_5_B.EditValue = null;

            if (luReaderPortal5.EditValue != null && luRFIDReader_5_A.EditValue == null && luRFIDReader_5_B.EditValue == null)
            {

                 
                DataRowView zDataRowView = luReaderPortal5.Properties.GetDataSourceRowByKeyValue(luReaderPortal5.EditValue) as DataRowView;
                if (zDataRowView != null)
                {
                    int zPortalID = int.Parse(zDataRowView[ISMPortal.PortalID].ToString());
                    rdoReaderType.EditValue = zDataRowView[ISMPortal.PortalType].ToString();

                     
                    m_PortalID = zPortalID;

                    DataSet ds = m_ISMLoginInfo.ISMServer.GetPortalORReaderData(1, zPortalID, "");
                    if (ds != null)
                    {
                         
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                if (dr["POWER_ON_OFF"].ToString() == "1")  
                                {
                                    luRFIDReader_5_A.EditValue = luRFIDReader_5_A.Properties.GetKeyValueByDisplayText(dr["READER_NAME"].ToString());
                                    mPrevName_5A = dr["READER_NAME"].ToString();
                                }
                                else
                                {
                                    luRFIDReader_5_B.EditValue = luRFIDReader_5_B.Properties.GetKeyValueByDisplayText(dr["READER_NAME"].ToString());
                                    mPrevName_5B = dr["READER_NAME"].ToString();
                                }
                            }
                        }
                    }
                }
                 
            }
            txtStatusMsg.Text = "";
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    #endregion

    #region Data Saving Section

    private void DataReload()
    {
         
        luReaderPortal1.EditValue = null;
        luReaderPortal2.EditValue = null;
        luReaderPortal3.EditValue = null;
        luReaderPortal4.EditValue = null;
        luReaderPortal5.EditValue = null;


        LoadPortalMetaData();
        luReaderPortalsLoading();
    }

    private void btnSaveCraneGroup1_Click(object sender, EventArgs e)
    {
        try
        {
             
            if (luReaderPortal1.EditValue == null || luRFIDReader_1_A.EditValue == null || luRFIDReader_1_B.EditValue == null)
            {
                txtStatusMsg.Text = "CraneGroup/Master/Slave Humpy doesn't exist"; 
                MessageBox.Show("CraneGroup/Master/Slave Humpy doesn't exist","Validation Failure");
            }
            else
            {
                if (luRFIDReader_1_A.EditValue == luRFIDReader_1_B.EditValue)
                {
                    txtStatusMsg.Text = "Master and Slave Humpy cannot be the same"; 
                    MessageBox.Show("Master and Slave Humpy cannot be the same", "Validation Failure");
                }
                else
                {
                     
                    string CraneGroupName = luReaderPortal1.Properties.GetDisplayText(luReaderPortal1.EditValue);
                    string MasterHumpyName_1 = luRFIDReader_1_A.Properties.GetDisplayText(luRFIDReader_1_A.EditValue);
                    string SlaveHumpyName_1 = luRFIDReader_1_B.Properties.GetDisplayText(luRFIDReader_1_B.EditValue);

                     
                     
                    if(mPrevName_1A!="")
                        UpdateHumpyRecord("NA", mPrevName_1A, "1");
                    if(mPrevName_1B!="")
                        UpdateHumpyRecord("NA", mPrevName_1B, "0");
                     
                    UpdateHumpyRecord(CraneGroupName, MasterHumpyName_1, "1");
                    UpdateHumpyRecord(CraneGroupName, SlaveHumpyName_1, "0");
                     
                    m_ISMLoginInfo.ISMServer.SHS_HumpyConfigGenerater(MasterHumpyName_1, SlaveHumpyName_1, CraneGroupName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBSQLInstanceName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBUserName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBPassword);

                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "SaveCraneGroup1", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        finally
        {
            DataReload();
        }
    }

    private void btnSaveCraneGroup2_Click(object sender, EventArgs e)
    {
        try
        {
             
            if (luReaderPortal2.EditValue == null || luRFIDReader_2_A.EditValue == null || luRFIDReader_2_B.EditValue == null)
            {
                txtStatusMsg.Text = "CraneGroup/Master/Slave Humpy doesn't exist"; 
                MessageBox.Show("CraneGroup/Master/Slave Humpy doesn't exist", "Validation Failure");
            }
            else
            {
                if (luRFIDReader_2_A.EditValue == luRFIDReader_2_B.EditValue)
                {
                    txtStatusMsg.Text = "Master and Slave Humpy cannot be the same"; 
                    MessageBox.Show("Master and Slave Humpy cannot be the same", "Validation Failure");
                }
                else
                {
                     
                    string CraneGroupName = luReaderPortal2.Properties.GetDisplayText(luReaderPortal2.EditValue);
                    string MasterHumpyName_2 = luRFIDReader_2_A.Properties.GetDisplayText(luRFIDReader_2_A.EditValue);
                    string SlaveHumpyName_2 = luRFIDReader_2_B.Properties.GetDisplayText(luRFIDReader_2_B.EditValue);

                     
                     
                    if (mPrevName_2A != "")
                        UpdateHumpyRecord("NA", mPrevName_2A, "1");
                    if (mPrevName_2B != "")
                        UpdateHumpyRecord("NA", mPrevName_2B, "0");
                     
                    UpdateHumpyRecord(CraneGroupName, MasterHumpyName_2, "1");
                    UpdateHumpyRecord(CraneGroupName, SlaveHumpyName_2, "0");
                     
                    m_ISMLoginInfo.ISMServer.SHS_HumpyConfigGenerater(MasterHumpyName_2, SlaveHumpyName_2, CraneGroupName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBSQLInstanceName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBUserName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBPassword);

                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "SaveCraneGroup2", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        finally
        {
            DataReload();
        }
    }

    private void btnSaveCraneGroup3_Click(object sender, EventArgs e)
    {
        try
        {
             
            if (luReaderPortal3.EditValue == null || luRFIDReader_3_A.EditValue == null || luRFIDReader_3_B.EditValue == null)
            {
                txtStatusMsg.Text = "CraneGroup/Master/Slave Humpy doesn't exist"; 
                MessageBox.Show("CraneGroup/Master/Slave Humpy doesn't exist", "Validation Failure");
            }
            else
            {
                if (luRFIDReader_3_A.EditValue == luRFIDReader_3_B.EditValue)
                {
                    txtStatusMsg.Text = "Master and Slave Humpy cannot be the same"; 
                    MessageBox.Show("Master and Slave Humpy cannot be the same", "Validation Failure");
                }
                else
                {
                     
                    string CraneGroupName = luReaderPortal3.Properties.GetDisplayText(luReaderPortal3.EditValue);
                    string MasterHumpyName_3 = luRFIDReader_3_A.Properties.GetDisplayText(luRFIDReader_3_A.EditValue);
                    string SlaveHumpyName_3 = luRFIDReader_3_B.Properties.GetDisplayText(luRFIDReader_3_B.EditValue);
                     
                     
                    if (mPrevName_3A != "")
                        UpdateHumpyRecord("NA", mPrevName_3A, "1");
                    if (mPrevName_3B != "")
                        UpdateHumpyRecord("NA", mPrevName_3B, "0");
                     
                    UpdateHumpyRecord(CraneGroupName, MasterHumpyName_3, "1");
                    UpdateHumpyRecord(CraneGroupName, SlaveHumpyName_3, "0");
                     
                    m_ISMLoginInfo.ISMServer.SHS_HumpyConfigGenerater(MasterHumpyName_3, SlaveHumpyName_3, CraneGroupName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBSQLInstanceName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBUserName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBPassword);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "SaveCraneGroup3", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        finally
        {
            DataReload();
        }
    }

    private void btnSaveCraneGroup4_Click(object sender, EventArgs e)
    {
        try
        {
             
            if (luReaderPortal4.EditValue == null || luRFIDReader_4_A.EditValue == null || luRFIDReader_4_B.EditValue == null)
            {
                txtStatusMsg.Text = "CraneGroup/Master/Slave Humpy doesn't exist"; 
                MessageBox.Show("CraneGroup/Master/Slave Humpy doesn't exist", "Validation Failure");
            }
            else
            {
                if (luRFIDReader_4_A.EditValue == luRFIDReader_4_B.EditValue)
                {
                    txtStatusMsg.Text = "Master and Slave Humpy cannot be the same"; 
                    MessageBox.Show("Master and Slave Humpy cannot be the same", "Validation Failure");
                }
                else
                {
                     
                    string CraneGroupName = luReaderPortal4.Properties.GetDisplayText(luReaderPortal4.EditValue);
                    string MasterHumpyName_4 = luRFIDReader_4_A.Properties.GetDisplayText(luRFIDReader_4_A.EditValue);
                    string SlaveHumpyName_4 = luRFIDReader_4_B.Properties.GetDisplayText(luRFIDReader_4_B.EditValue);
                     
                    if (mPrevName_4A != "")
                        UpdateHumpyRecord("NA", mPrevName_4A, "1");
                    if (mPrevName_4B != "")
                        UpdateHumpyRecord("NA", mPrevName_4B, "0");
                    UpdateHumpyRecord(CraneGroupName, MasterHumpyName_4, "1");
                    UpdateHumpyRecord(CraneGroupName, SlaveHumpyName_4, "0");
                     
                    m_ISMLoginInfo.ISMServer.SHS_HumpyConfigGenerater(MasterHumpyName_4, SlaveHumpyName_4, CraneGroupName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBSQLInstanceName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBUserName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBPassword);

                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "SaveCraneGroup4", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        finally
        {
            DataReload();
        }
    }


    private void btnSaveCraneGroup5_Click(object sender, EventArgs e)
    {
        try
        {
             
            if (luReaderPortal5.EditValue == null || luRFIDReader_5_A.EditValue == null || luRFIDReader_5_B.EditValue == null)
            {
                txtStatusMsg.Text = "CraneGroup/Master/Slave Humpy doesn't exist"; 
                MessageBox.Show("CraneGroup/Master/Slave Humpy doesn't exist", "Validation Failure");
            }
            else
            {
                if (luRFIDReader_5_A.EditValue == luRFIDReader_5_B.EditValue)
                {
                    txtStatusMsg.Text = "Master and Slave Humpy cannot be the same"; 
                    MessageBox.Show("Master and Slave Humpy cannot be the same", "Validation Failure");
                }
                else
                {
                     
                    string CraneGroupName = luReaderPortal5.Properties.GetDisplayText(luReaderPortal5.EditValue);
                    string MasterHumpyName_5 = luRFIDReader_5_A.Properties.GetDisplayText(luRFIDReader_5_A.EditValue);
                    string SlaveHumpyName_5 = luRFIDReader_5_B.Properties.GetDisplayText(luRFIDReader_5_B.EditValue);
                     
                    if (mPrevName_5A != "")
                        UpdateHumpyRecord("NA", mPrevName_5A, "1");
                    if (mPrevName_5B != "")
                        UpdateHumpyRecord("NA", mPrevName_5B, "0");

                    UpdateHumpyRecord(CraneGroupName, MasterHumpyName_5, "1");
                    UpdateHumpyRecord(CraneGroupName, SlaveHumpyName_5, "0");
                     
                    m_ISMLoginInfo.ISMServer.SHS_HumpyConfigGenerater(MasterHumpyName_5, SlaveHumpyName_5, CraneGroupName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBSQLInstanceName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBUserName,
                                            SHSHQ.Properties.Settings.Default.HumpyDBPassword);

                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "SaveCraneGroup5", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        finally
        {
            DataReload();
        }
    }
    #endregion

    private void luRFIDReader_1_A_EditValueChanged(object sender, EventArgs e)
    {

    }

    private void luRFIDReader_1_B_EditValueChanged(object sender, EventArgs e)
    {

    }

    private void luRFIDReader_2_A_EditValueChanged(object sender, EventArgs e)
    {

    }

    private void luRFIDReader_2_B_EditValueChanged(object sender, EventArgs e)
    {

    }

    private void luRFIDReader_3_A_EditValueChanged(object sender, EventArgs e)
    {

    }

    private void luRFIDReader_3_B_EditValueChanged(object sender, EventArgs e)
    {

    }

    private void luRFIDReader_4_A_EditValueChanged(object sender, EventArgs e)
    {

    }

    private void luRFIDReader_4_B_EditValueChanged(object sender, EventArgs e)
    {

    }

    private void luRFIDReader_5_A_EditValueChanged(object sender, EventArgs e)
    {

    }

    private void luRFIDReader_5_B_EditValueChanged(object sender, EventArgs e)
    {

    }

  }
}
