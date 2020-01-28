
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
  public partial class LocationPortal : ISMBaseWorkSpace
  {
    private EnumFrmMode m_FrmMode;
    int m_PortalID = 0;
    int m_ReaderPortalID = 0;
    int m_AntennaReaderID = 0;
    int m_ReaderID = 0; 
    private enum ControlType : int { Portal = 0, Reader = 1, Antenna = 2 };


    public LocationPortal(ISMLoginInfo AISMLoginInfo, EnumFrmMode AMode)
      : base(AISMLoginInfo)
    {
      InitializeComponent();
      m_FrmMode = AMode;
    }

    private void LocationPortal_Load(object sender, EventArgs e)
    {
       
      try
      {
        rdoPortalType.EditValue = "PASSIVE"; 

        if ((m_FrmMode == EnumFrmMode.EDIT) || (m_FrmMode == EnumFrmMode.REMOVE))
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

          if (m_FrmMode == EnumFrmMode.REMOVE)
          {
            btnRemove.Visible = true;
            btnRemove.Top = btnPortalAdd.Top;
            btnRemove.Left = btnPortalAdd.Left;
            btnPortalAdd.Visible = false;
             
            btnReaderRemove.Top = btnReaderAdd.Top;
            btnReaderRemove.Left = btnReaderAdd.Left;
            btnReaderAdd.Visible = false;
             
            btnAntennaRemove.Top = btnAntennaAdd.Top;
            btnAntennaRemove.Left = btnAntennaAdd.Left;
            btnAntennaAdd.Visible = false;
             
            txtDescription.Enabled = false;
            rdoPortalType.Enabled = false;
            txtReaderName.Enabled = false;
            rdoReaderType.Enabled = false;
            txtReaderDesc.Enabled = false;
            txtIPAddress.Enabled = false;
            ChkReaderPowerState.Enabled = false;
            txtAntTXPower.Enabled = false;
            txtThershold.Enabled = false;
            chkAntennaPowerState.Enabled = false;
            txtPoll.Enabled = false;
             
             
            txtAntTXPower.Enabled = false;
            txtThershold.Enabled = false;
            chkAntennaPowerState.Enabled = false;

          }
          else
          {
            btnRemove.Visible = false;
            btnReaderRemove.Visible = false;
            btnAntennaRemove.Visible = false;

          }
        }
        else  
        {
          lblExistingPortal.Visible = false;
          luExistingPortalName.Visible = false;
          btnRemove.Visible = false;
           
          lblReaderCaption.Visible = false;
          luRFIDReader.Visible = false;
          btnReaderRemove.Visible = false;
           
          lblExistingAntennaID.Visible = false;
          luAntenna.Visible = false;
          btnAntennaRemove.Visible = false;

          luAntenna.Enabled = true;
          txtAntTXPower.Enabled = true;
          txtThershold.Enabled = true;
          chkAntennaPowerState.Enabled = true;
        }

        SetLookUpEditCaption();    
        LoadPortalMetaData();    
        ClearControls();
        SetGridCaption();
        rdoReaderType.Enabled = false;  
        txtPoll.Properties.Mask.EditMask = "\\d{0,4}";  
        txtPoll.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;  

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

        luAntennaReader.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.ReaderName, 140, "Humpy Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.IPAddress, 200, "IP Address")});

        luAntennaReader.Properties.DisplayMember = ISMReaders.ReaderName;
        luAntennaReader.Properties.ValueMember = ISMReaders.ReaderName;


        if ((m_FrmMode == EnumFrmMode.EDIT) || (m_FrmMode == EnumFrmMode.REMOVE))  
        {
          luExistingPortalName.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalName,  140, "Portal Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.Description, 200, ISMPortal.Description),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalType, 140, ISMPortal.PortalType)});

          luExistingPortalName.Properties.DisplayMember = ISMPortal.PortalName;
          luExistingPortalName.Properties.ValueMember = ISMPortal.PortalName;

          luRFIDReader.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.ReaderName, 140, "Humpy Name"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMReaders.IPAddress, 200, "IP Address")});

          luRFIDReader.Properties.DisplayMember = ISMReaders.ReaderName;
          luRFIDReader.Properties.ValueMember = ISMReaders.ReaderName;
           
          luAntenna.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMAntenna.AnntennaID, 200, "Antenna ID")});

          luAntenna.Properties.DisplayMember = ISMAntenna.AnntennaID;
          luAntenna.Properties.ValueMember = ISMAntenna.AnntennaID;
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

                  if (m_FrmMode == EnumFrmMode.CREATE)  
                  {
                      txtPoll.Enabled = true;
                      ChkReaderPowerState.Enabled = true;
                      ChkReaderPowerState.Checked = false;
                  }

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

    private void btnReaderAdd_Click(object sender, EventArgs e)
    {
      try
      {
          txtReaderDesc.Text = "Humpy"; 

        if (Validation((int)ControlType.Reader))
        {
          string zReaderName;
          string zMessage;
          zReaderName = txtReaderName.Text;
          if (m_FrmMode == EnumFrmMode.CREATE)
          {
            zMessage = String.Format("Do you want create Controller\n\nController Name : {0} ?", zReaderName);  
            DialogResult zReply = MessageBox.Show(zMessage, "Create Controller", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);  
            if (zReply == DialogResult.Yes)  
            {
              SaveReaderRecord();
              LoadPortalMetaData();    
              ClearReaderControls();
              txtStatusMsg.Text = "Humpy Controller " + zReaderName + " has been created";
            }
          }
          else
          {
            zMessage = String.Format("Do you want update Controller\n\nController Name : {0} ?", zReaderName);  
            DialogResult zReply = MessageBox.Show(zMessage, "Update Controller", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);  
            if (zReply == DialogResult.Yes)  
            {
              zReaderName = luRFIDReader.Text.Trim();
              SaveReaderRecord();
              ClearControls();
              LoadPortalMetaData();
              ClearReaderControls();
              txtStatusMsg.Text = "Controller " + zReaderName + " has been updated";
            }
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void SaveReaderRecord()
    {
      try
      {
        ISMReaders.Data zReaderData = new ISMReaders.Data();
        zReaderData.PortalID = m_PortalID.ToString();
        zReaderData.ReaderType = rdoReaderType.EditValue.ToString();
        zReaderData.Description = txtReaderDesc.Text.Trim();
        if (ChkReaderPowerState.EditValue != null)
        {
          if (ChkReaderPowerState.Checked)
            zReaderData.PowerStatus = "1";
          else
            zReaderData.PowerStatus = "0";
        }
        else
          zReaderData.PowerStatus = "0";
        zReaderData.IPAddress = txtIPAddress.Text;
        zReaderData.PollTime = txtPoll.Text;
        if (m_FrmMode == EnumFrmMode.CREATE)
        {
          zReaderData.ReaderName = txtReaderName.Text.Trim();
          m_ISMLoginInfo.ISMServer.LocationPortalReaderIUD(zReaderData, 0, luReaderPortal.Text.Trim(), txtPriority.Text);

        }
        else if (m_FrmMode == EnumFrmMode.EDIT)
        {
          zReaderData.ReaderName = luRFIDReader.Text.Trim();
          m_ISMLoginInfo.ISMServer.LocationPortalReaderIUD(zReaderData, 1, luReaderPortal.Text.Trim(), txtPriority.Text);
        }
        else if (m_FrmMode == EnumFrmMode.REMOVE)
        {
          zReaderData.ReaderName = luRFIDReader.Text.Trim();
          m_ISMLoginInfo.ISMServer.LocationPortalReaderIUD(zReaderData, 2, luReaderPortal.Text.Trim(), txtPriority.Text);
        }

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }

    private void btnReaderRemove_Click(object sender, EventArgs e)
    {
      try
      {
        if (m_FrmMode == EnumFrmMode.REMOVE)
        {
          string zPortalName;
          dxErrorProvider.SetError(luReaderPortal, null);
          dxErrorProvider.SetError(luRFIDReader, null);
          if (luReaderPortal.EditValue == null)
          {
            dxErrorProvider.SetError(luReaderPortal, "Select a Portal");
             
            return;
          }
          if (luRFIDReader.EditValue == null)
          {
              dxErrorProvider.SetError(luRFIDReader, "Select a Humpy to be removed");
            return;
          }
          string zMsgStr = string.Format("Are you sure? Do you want to deactivate Reader\n\nReader Name : {0:s}", luRFIDReader.Text.Trim());
          DialogResult zReply = MessageBox.Show(zMsgStr, "Remove Reader", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
          if (zReply == DialogResult.Yes)
          {
            SaveReaderRecord();
            zPortalName = luExistingPortalName.Text;
            txtStatusMsg.Text = "Humpy : " + zPortalName + " has been removed";
            ClearReaderControls();
            LoadPortalMetaData();
          }
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

    private void CreateMetaDataForNewAntenna(string AReaderType)
    {
      try
      {
         
        System.Data.DataTable table = new DataTable("ANTENNA");
         
        DataColumn column;
        DataRow row;

         
         
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "ID";
        column.ReadOnly = true;
        column.Unique = true;
         
        table.Columns.Add(column);

         
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "ANTENNA_ID";
        column.AutoIncrement = false;
        column.Caption = "ANTENNA_ID";
        column.ReadOnly = false;
        column.Unique = false;
         
        table.Columns.Add(column);

         
        DataColumn[] PrimaryKeyColumns = new DataColumn[1];
        PrimaryKeyColumns[0] = table.Columns["ID"];
        table.PrimaryKey = PrimaryKeyColumns;

         
        DataSet ds = new DataSet();
         
        ds.Tables.Add(table);

         
         
        if (AReaderType == "PASSIVE")
        {
          for (int i = 0; i <= 3; i++)
          {
            row = table.NewRow();
            row["ID"] = i + 1;

            switch (i)
            {
              case 0:
                row["ANTENNA_ID"] = "1";
                break;
              case 1:
                row["ANTENNA_ID"] = "2";
                break;
              case 2:
                row["ANTENNA_ID"] = "3";
                break;
              case 3:
                row["ANTENNA_ID"] = "4";
                break;
            }
            table.Rows.Add(row);
          }
        }
        else if (AReaderType == "ACTIVE")
        {
          row = table.NewRow();
          row["ID"] = 1;
          row["ANTENNA_ID"] = "1";
          table.Rows.Add(row);
        }
        luNewAntenna.Properties.DataSource = ds.Tables[0].DefaultView;

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void luAntennaReader_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        if (luAntennaReader.EditValue != null)  
          luAntenna.Enabled = true;
        else
          luAntenna.Enabled = false;
        string zReaderType = "";


        if (luAntennaReader.EditValue != null)
        {
          DataSet ds = null;
          DataRowView zDataRowView = luAntennaReader.Properties.GetDataSourceRowByKeyValue(luAntennaReader.EditValue) as DataRowView;
          if (zDataRowView != null)
            m_AntennaReaderID = int.Parse(zDataRowView[ISMReaders.ReaderID].ToString());
          if (m_FrmMode == EnumFrmMode.CREATE || m_FrmMode == EnumFrmMode.EDIT)  
          {
            ds = m_ISMLoginInfo.ISMServer.GetPortalORReaderData(4, 0, luAntennaReader.EditValue.ToString());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
              DataRow dr = ds.Tables[0].Rows[0];
              CreateMetaDataForNewAntenna(dr[ISMReaders.ReaderType].ToString());
              zReaderType = dr[ISMReaders.ReaderType].ToString();  
              if (dr[ISMReaders.ReaderType].ToString() == "ACTIVE")
              {
                 
                 
                 
                 
                 
                 
                 
                 
                 
                 
                 
                
                 
                  txtAntTXPower.Properties.MinValue = decimal.Parse(m_ISMLoginInfo.Params.ActiveTxPowermin);
                  txtAntTXPower.Properties.MaxValue = decimal.Parse(m_ISMLoginInfo.Params.ActiveTxPowermax);
                  txtThershold.Properties.MinValue = decimal.Parse(m_ISMLoginInfo.Params.ActiveThrPowermin);
                  txtThershold.Properties.MaxValue = decimal.Parse(m_ISMLoginInfo.Params.ActiveThrPowermax);
                  txtThershold.EditValue = decimal.Parse(m_ISMLoginInfo.Params.ActiveThrPowermin);
                  txtThershold.Enabled = true;
                 

              }
              else
              {
                 
                 
                 
                 
                 
                 
                 
                 

                 
                  txtAntTXPower.Properties.MinValue = decimal.Parse(m_ISMLoginInfo.Params.PassiveTxPowermin);
                  txtAntTXPower.Properties.MaxValue = decimal.Parse(m_ISMLoginInfo.Params.PassiveTxPowermax);
                  txtThershold.Properties.MinValue = decimal.Parse(m_ISMLoginInfo.Params.PassiveThrPowermin);
                  txtThershold.Properties.MaxValue = decimal.Parse(m_ISMLoginInfo.Params.PassiveThrPowermax);
                  txtThershold.EditValue = decimal.Parse(m_ISMLoginInfo.Params.PassiveThrPowermax); 
                  txtThershold.Enabled = false;  
                   
                chkAntennaPowerState.Enabled = true;
              }
            }
          }
          else
          {
            txtAntTXPower.Enabled = false;  
            txtThershold.Enabled = false;
            chkAntennaPowerState.Enabled = false;

          }
          if (m_FrmMode == EnumFrmMode.EDIT || m_FrmMode == EnumFrmMode.REMOVE)
          {
            luAntenna.EditValueChanged -= new System.EventHandler(luAntenna_EditValueChanged);

            ds = m_ISMLoginInfo.ISMServer.GetPortalORReaderData(3, 0, luAntennaReader.EditValue.ToString());
            if (ds != null)
            {
              luAntenna.Properties.DataSource = ds.Tables[0].DefaultView;
              if (ds.Tables[0].Rows.Count > 0)
              {
                luAntenna.EditValue = luAntenna.Properties.GetKeyValueByDisplayText("1");  
                 
                zDataRowView = luAntenna.Properties.GetDataSourceRowByKeyValue(luAntenna.EditValue) as DataRowView;
                if (zDataRowView != null)
                {
                  txtAntTXPower.Text = zDataRowView[ISMAntenna.TxPower].ToString();
                  txtThershold.Text = zDataRowView[ISMAntenna.Rssi_Threshold].ToString();
                  if (zDataRowView[ISMAntenna.PowerStatus].ToString() == "1")
                    chkAntennaPowerState.Checked = true;
                  else
                    chkAntennaPowerState.Checked = false;
                }

                if (m_FrmMode == EnumFrmMode.CREATE || m_FrmMode == EnumFrmMode.EDIT)
                {
                  luAntenna.Enabled = true;
                  txtAntTXPower.Enabled = true;
                  chkAntennaPowerState.Enabled = true;

                  if (zReaderType == "ACTIVE")
                    txtThershold.Enabled = true;
                  else
                    txtThershold.Enabled = false;
                }
                 

              }
              else
              {
                 
                txtAntTXPower.EditValue = null;  
                txtThershold.EditValue = null;  
                chkAntennaPowerState.Checked = false;  
                txtAntTXPower.Enabled = false;
                txtThershold.Enabled = false;
                chkAntennaPowerState.Enabled = false;
              }

            }
            luAntenna.EditValueChanged += new System.EventHandler(luAntenna_EditValueChanged);
          }
          txtStatusMsg.Text = "";
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void luAntenna_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        if (luAntenna.EditValue != null)
        {
          DataRowView zDataRowView = luAntenna.Properties.GetDataSourceRowByKeyValue(luAntenna.EditValue) as DataRowView;
          if (zDataRowView != null)
          {
            txtAntTXPower.Text = zDataRowView[ISMAntenna.TxPower].ToString();
            txtThershold.Text = zDataRowView[ISMAntenna.Rssi_Threshold].ToString();
            if (zDataRowView[ISMAntenna.PowerStatus].ToString() == "1")
              chkAntennaPowerState.Checked = true;
            else
              chkAntennaPowerState.Checked = false;
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void btnAntennaAdd_Click(object sender, EventArgs e)
    {
      try
      {
        if (Validation((int)ControlType.Antenna))
        {
          string zAntenna;
          string zMessage;

          if (m_FrmMode == EnumFrmMode.CREATE)
          {
            zAntenna = luNewAntenna.EditValue.ToString();
            zMessage = String.Format("Do you want create Antenna\n\nAntenna : {0} ?", zAntenna);  
            DialogResult zReply = MessageBox.Show(zMessage, "Create Antenna", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);  
            if (zReply == DialogResult.Yes)  
            {

              SaveAntennaRecord();
              txtStatusMsg.Text = "Antenna " + zAntenna + " has been created";
              ClearAntennaControls();
              LoadPortalMetaData();    
            }
          }
          else
          {
            zAntenna = luAntenna.EditValue.ToString();
            zMessage = String.Format("Do you want update Antenna\n\nAntenna : {0} ?", zAntenna);  
            DialogResult zReply = MessageBox.Show(zMessage, "Update Antenna", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);  
            if (zReply == DialogResult.Yes)  
            {
              SaveAntennaRecord();
              txtStatusMsg.Text = "Antenna " + zAntenna + " has been updated";
              ClearAntennaControls();
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

    private void SaveAntennaRecord()
    {
      try
      {
        ISMAntenna.Data zAntennaData = new ISMAntenna.Data();
        zAntennaData.ReaderID = m_AntennaReaderID.ToString();
        zAntennaData.TxPower = txtAntTXPower.EditValue.ToString();
        zAntennaData.Rssi_Threshold = txtThershold.EditValue.ToString();

        if (chkAntennaPowerState.EditValue != null)
        {
          if (chkAntennaPowerState.Checked)
            zAntennaData.PowerStatus = "1";
          else
            zAntennaData.PowerStatus = "0";
        }
        else
          zAntennaData.PowerStatus = "0";

        if (m_FrmMode == EnumFrmMode.CREATE)
        {
          zAntennaData.AnntennaID = luNewAntenna.EditValue.ToString();
          m_ISMLoginInfo.ISMServer.LocationPortalAntennaIUD(zAntennaData, 0);
        }
        else if (m_FrmMode == EnumFrmMode.EDIT)
        {
          zAntennaData.AnntennaID = luAntenna.EditValue.ToString();
          m_ISMLoginInfo.ISMServer.LocationPortalAntennaIUD(zAntennaData, 1);
        }
        else if (m_FrmMode == EnumFrmMode.REMOVE)
        {
          zAntennaData.AnntennaID = luAntenna.EditValue.ToString();
          m_ISMLoginInfo.ISMServer.LocationPortalAntennaIUD(zAntennaData, 2);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void btnAntennaRemove_Click(object sender, EventArgs e)
    {
      try
      {
        if (m_FrmMode == EnumFrmMode.REMOVE)
        {
          string zPortalName;
          dxErrorProvider.SetError(luAntennaReader, null);
          dxErrorProvider.SetError(luAntenna, null);
          if (luAntennaReader.EditValue == null)
          {
              dxErrorProvider.SetError(luAntennaReader, "Select a Humpy");
             
            return;
          }
          if (luAntenna.EditValue == null)
          {
            dxErrorProvider.SetError(luAntenna, "Select a Antenna ID to be removed");
            return;
          }

          string zMsgStr = string.Format("Are you sure? Do you want to deactivate Antenna\n\nAntenna ID : {0:s}", luAntenna.Text.Trim());
          DialogResult zReply = MessageBox.Show(zMsgStr, "Remove Antenna", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
          if (zReply == DialogResult.Yes)
          {
            SaveAntennaRecord();
            zPortalName = luAntenna.Text;
            txtStatusMsg.Text = "Antenna : " + zPortalName + " has been removed";
            ClearAntennaControls();
            LoadPortalMetaData();
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
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
  }
}
