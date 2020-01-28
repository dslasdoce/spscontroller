 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

using System;
using System.Data;
using System.Windows.Forms;

using ISMDAL.TableColumnName;

namespace ISM.Modules
{
  public partial class LocationAdmin : ISMBaseWorkSpace
  {
    private EnumFrmMode m_FrmMode;
    bool m_FixedLocation = false;

     
    private struct UpdateRelationshipKeys
    {
      public ulong m_LocationUID;
      public ulong m_ChildID;
      public ulong m_PrevParentID;
      public ulong m_NewParentID;
    };
    string m_PortalID; 

    UpdateRelationshipKeys m_UpdateRelationshipKeys;
     

    public LocationAdmin(ISMLoginInfo AISMLoginInfo, EnumFrmMode AMode)
      : base(AISMLoginInfo)
    {
      InitializeComponent();
      m_FrmMode = AMode;

      m_UpdateRelationshipKeys = new UpdateRelationshipKeys();
    }

    private void CreateLocation_Load(object sender, EventArgs e)
    {
        try
        {
             
            if ((m_FrmMode == EnumFrmMode.EDIT) || (m_FrmMode == EnumFrmMode.REMOVE))
            {
                btnAdd.Text = "Save";   
                lblNewLocationUID.Visible = false;
                txtNewLocationUID.Visible = false;
                luParentLocUID.Enabled = false;
                luParentLocUID.Properties.NullText = "";
                luLocationType.Properties.NullText = "";
                luLabelType.Properties.NullText = "";

                lblExistingLocationUID.Top = lblNewLocationUID.Top;
                lblExistingLocationUID.Left = lblNewLocationUID.Left;

                luExistingLocnUID.Top = txtNewLocationUID.Top;
                luExistingLocnUID.Left = txtNewLocationUID.Left;
                 
                luExistingLocnUID.Properties.Mask.EditMask = m_ISMLoginInfo.Params.LocPrefix + "\\d{0,12}";
                luExistingLocnUID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                 

                 
                lblSealType.Visible = false;
                lblSealUID.Visible = false;
                lbltxtSealType.Visible = true;
                lbltxtSealUID.Visible = true;

                int zlblSealTypeTop = lblSealType.Top;
                int zlblSealTypeLeft = lblSealType.Left;
                int ztxtSealTypeTop = lbltxtSealType.Top;
                int ztxtSealTypeLeft = lbltxtSealType.Left;

                int zlblSealUIDTop = lblSealUID.Top;
                int zlblSealUIDLeft = lblSealUID.Left;
                int ztxtSealUIDTop = lbltxtSealUID.Top;
                int ztxtSealUIDLeft = lbltxtSealUID.Left;

                lblSealType.Top = lblERPDist.Top;
                lblSealType.Left = lblERPDist.Left;
                lbltxtSealType.Top = txtERPDistrict.Top;
                lbltxtSealType.Left = txtERPDistrict.Left;

                lblSealUID.Top = lblERPWHS.Top;
                lblSealUID.Left = lblERPWHS.Left;
                lbltxtSealUID.Top = txtERPWHS.Top;
                lbltxtSealUID.Left = txtERPWHS.Left;

                lblERPDist.Top = lblERPGrd.Top;
                lblERPDist.Left = lblERPGrd.Left;
                txtERPDistrict.Top = txtERPGrid.Top;
                txtERPDistrict.Left = txtERPGrid.Left;

                lblERPWHS.Top = lblERPBin.Top;
                lblERPWHS.Left = lblERPBin.Left;
                txtERPWHS.Top = txtERPBin.Top;
                txtERPWHS.Left = txtERPBin.Left;

                lblERPGrd.Top = zlblSealTypeTop;
                lblERPGrd.Left = zlblSealTypeLeft;
                txtERPGrid.Top = ztxtSealTypeTop;
                txtERPGrid.Left = ztxtSealTypeLeft;

                lblERPBin.Top = zlblSealUIDTop;
                lblERPBin.Left = zlblSealUIDLeft;
                txtERPBin.Top = ztxtSealUIDTop;
                txtERPBin.Left = ztxtSealUIDLeft;
                 

                if (m_FrmMode == EnumFrmMode.REMOVE)
                {
                    btnRemove.Visible = true;
                    btnRemove.Top = btnAdd.Top;
                    btnRemove.Left = btnAdd.Left;
                    btnAdd.Visible = false;
                     
                    txtNewLocationUID.Enabled = false;
                    txtUserDescription.Enabled = false;
                    luParentLocUID.Enabled = false;
                    luLocationType.Enabled = false;
                    luLabelType.Enabled = false;
                    cbLocnFixedFlag.Enabled = false;
                    luPortalName.Enabled = false;
                    luPortalName.Properties.NullText = "";
                    btnClearPortal.Enabled = false;
                     
                }
                else
                {
                    btnRemove.Visible = false;
                }
            }
            else  
            {
                lblExistingLocationUID.Visible = false;
                luExistingLocnUID.Visible = false;
                btnRemove.Visible = false;
                 
                txtNewLocationUID.Properties.Mask.EditMask = m_ISMLoginInfo.Params.LocPrefix + "\\d{0,12}";
                txtNewLocationUID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                 

                 
                lblSealType.Visible = false;
                lblSealUID.Visible = false;
                lbltxtSealType.Visible = false;
                lbltxtSealUID.Visible = false;
                 

            }
            SetLookUpEditCaption();    
            LoadLocationMetaData();    
            ClearControls();           
        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Crane Location Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    private void SetLookUpEditCaption()
    {
      try
      {
        luParentLocUID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LOCATIONUID", 100, "UID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.LocationCode, 70,"Locn Code"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.UserDesc, 180,"Description")});

        luLocationType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocationType.LocTypeCode, 70, "Loc Type Code"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocationType.LocTypeDesc, 180,"Description") });

        luLabelType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLabelType.LabelTypeCode, 100, "Label Type Code"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLabelType.LabelTypeDesc, 180,"Description") });

         
        luPortalName.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalID, "ID",70,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center),  
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.PortalName, 100,"Portal Name"),  
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMPortal.Description, 180,"Description ")});
         

         

        luParentLocUID.Properties.DisplayMember = "LOCATIONUID";
        luParentLocUID.Properties.ValueMember = ISMLocation.LocationUID;

        luLocationType.Properties.DisplayMember = ISMLocationType.LocTypeDesc;

        luLabelType.Properties.DisplayMember = ISMLabelType.LabelTypeDesc;
        luLabelType.Properties.ValueMember = ISMLabelType.LabelTypeCode;

        luPortalName.Properties.DisplayMember = ISMPortal.PortalName;
        luPortalName.Properties.ValueMember = ISMPortal.PortalID;


         
        if ((m_FrmMode == EnumFrmMode.EDIT) || (m_FrmMode == EnumFrmMode.REMOVE))  
        {
          luExistingLocnUID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LOCATIONUID", 190, "UID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.LocationCode, 110,"Locn Code"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.UserDesc, 180,"Description")});

          luExistingLocnUID.Properties.DisplayMember = "LOCATIONUID";
          luExistingLocnUID.Properties.ValueMember = ISMLocation.LocationUID;
        }
      }
      catch (Exception ex)
      {
          MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Set Lookup Edit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void LoadLocationMetaData()
    {
      try
      {
        DataSet ds = m_ISMLoginInfo.ISMServer.GetLocationMetaData(); 
        if (ds != null)
        {
          luParentLocUID.Properties.DataSource = ds.Tables[ISMLocation.TableName].DefaultView;
          luLocationType.Properties.DataSource = ds.Tables[ISMLocationType.TableName].DefaultView;
          luLabelType.Properties.DataSource = ds.Tables[ISMLabelType.TableName].DefaultView;
          luPortalName.Properties.DataSource = ds.Tables[ISMPortal.TableName].DefaultView;  
           
           
           
           
           
           
           

          luLabelType.EditValue = "pRFID"; 

           
          if ((m_FrmMode == EnumFrmMode.EDIT) || (m_FrmMode == EnumFrmMode.REMOVE))  
          {
             
            luExistingLocnUID.Properties.DataSource = ds.Tables[4].DefaultView;  
            luExistingLocnUID.Properties.BestFit();
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Load Location Meta Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }


     
    private ISMLocation.Data PackageLocationDetails()  
    {
      DataRowView zDataRow = null;
       
      ISMLocation.Data zLocationData = new ISMLocation.Data();  
      zLocationData.Fixed_Flag = (cbLocnFixedFlag.Checked) ? '1' : '0';

       
       
       
        DataRowView zDataRowView = luPortalName.Properties.GetDataSourceRowByKeyValue(luPortalName.EditValue) as DataRowView;
        if (zDataRowView != null)   
          zLocationData.PortalID = Convert.ToInt32(luPortalName.EditValue);
      else
        zLocationData.PortalID = 0;
       
       


      if (m_FrmMode == EnumFrmMode.CREATE)  
        zLocationData.FK_Location_UID = Convert.ToUInt64(txtNewLocationUID.Text.Remove(0, 1));

      zLocationData.FK_Parent_UID = Convert.ToUInt64(luParentLocUID.Text.Remove(0, 1));

      zDataRow = luLocationType.Properties.GetDataSourceRowByKeyValue(luLocationType.EditValue) as DataRowView;
      if (zDataRow != null)   
        zLocationData.FK_Location_Code = Convert.ToString(zDataRow[0]);

      if (txtUserDescription.Text.Trim() != "")  
          zLocationData.User_Description = txtUserDescription.Text;
      else
          zLocationData.User_Description = zLocationData.FK_Location_Code;

      zLocationData.ERP_DISTRICT = txtERPDistrict.Text;
      zLocationData.ERP_WHOUSE = txtERPWHS.Text;
      zLocationData.ERP_GRID = txtERPGrid.Text;
      zLocationData.ERP_BIN = txtERPBin.Text;
       
       

      
     /* if (new ValidSealNumber(txtSealUID.Text).IsValidSealNo())
        zLocationData.FK_Seal_UID = Convert.ToUInt64(txtSealUID.Text.Remove(0, 1));

      zDataRow = luSealType.Properties.GetDataSourceRowByKeyValue(luSealType.EditValue) as DataRowView;
      if (zDataRow != null)
        zLocationData.FK_Seal_Code = Convert.ToInt16(zDataRow[0]);

      if (zLocationData.FK_Seal_Code == (int)ISM.SealType.Electronic || zLocationData.FK_Seal_Code == (int)ISM.SealType.TamperEvident)  
        zLocationData.Seal_Broken = '1';
      else
        zLocationData.Seal_Broken = '0';*/
       
       
      zLocationData.FK_Seal_UID = 0;  
      zLocationData.FK_Seal_Code = 0;  
      zLocationData.Seal_Broken = '0';  
       

      luLabelType.EditValue = "pRFID"; 

       
      zDataRow = luLabelType.Properties.GetDataSourceRowByKeyValue(luLabelType.EditValue) as DataRowView;
      if (zDataRow != null)
        zLocationData.FK_Type_ID = Convert.ToInt16(zDataRow[0]);
       

      zLocationData.FK_Type_ID = 3; 

      return zLocationData;
    }

    private void SaveLocationRecord()
    {
       
      ISMLocation.Data zLocationData = PackageLocationDetails();  
      m_ISMLoginInfo.ISMServer.LocationInsertRecd(zLocationData);
    }

     
    private void UpdateLocationRecord()
    {
       
       
       

      DataRowView zDataRowView = luExistingLocnUID.Properties.GetDataSourceRowByKeyValue(luExistingLocnUID.EditValue) as DataRowView;

      if (zDataRowView != null)   
      {
        ISMLocation.Data zLocationData = PackageLocationDetails();   

        zLocationData.FK_Location_UID = m_UpdateRelationshipKeys.m_LocationUID;
        
          zLocationData.FK_Type_ID = 3; 

         
        DataSet zLocationDS = m_ISMLoginInfo.ISMServer.LocationGetRecd(Convert.ToUInt64(zDataRowView[0]));

        if (zLocationDS != null)  
        {
          if (zLocationDS.Tables[ISMLocation.TableName].Rows.Count > 0)  
          {
          DataRow zCurrentDR = zLocationDS.Tables[ISMLocation.TableName].Rows[0];  
          if (zCurrentDR != null)  
          {
            m_UpdateRelationshipKeys.m_NewParentID = Convert.ToUInt64(zCurrentDR[ISMLocation.ID]);
             
             
             
             
             
            
            m_ISMLoginInfo.ISMServer.LocationUpdateRecd(zLocationData);  

          }
        }
        }
      }
    }

    private void RemoveLocationRecord()
    {
      m_ISMLoginInfo.ISMServer.LocationDeleteRecord(m_UpdateRelationshipKeys.m_ChildID,
                                                    m_UpdateRelationshipKeys.m_PrevParentID,
                                                    m_UpdateRelationshipKeys.m_NewParentID);
    }
     

    private bool IsChildLocationFixed()  
    {
        bool zResult = false;
        try
        {
            if (luExistingLocnUID.EditValue != null)
            {
                long zLocUID = long.Parse(luExistingLocnUID.EditValue.ToString());
                DataSet ds = m_ISMLoginInfo.ISMServer.GetLocationTree(zLocUID);
                if (ds != null)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr[ISMLocation.FixedFlag].ToString() == "1" && luExistingLocnUID.EditValue.ToString() != dr[ISMLocation.LocationUID].ToString())
                        {
                            zResult = true;
                            break;
                        }
                    }

               }
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "IsChildLocationFixed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        return zResult;
    }
                            

    private bool Validation()
    {
      bool zResult = false;
      bool zValidationFail = true;  
      ClearErrorIconText();  
      if (cbLocnFixedFlag.Checked)  
      {
          if (!m_FixedLocation)  
          {
              if (m_FrmMode == EnumFrmMode.CREATE)
                  dxErrorProvider.SetError(cbLocnFixedFlag, "Cannot create a fixed location whose parent location is non-fixed");
              else if (m_FrmMode == EnumFrmMode.EDIT)
                  dxErrorProvider.SetError(cbLocnFixedFlag, "Cannot update a non-fixed location to a fixed location if its parent location is unfixed");
              zValidationFail = false;
          }

          if (m_FixedLocation)
          {
              if (txtERPBin.Text.Trim() == "")
              {
                  dxErrorProvider.SetError(txtERPBin, "Enter a ERP BIN");
                  txtERPBin.Focus();
                  zValidationFail = false;
              }
              if (txtERPGrid.Text.Trim() == "")
              {
                  dxErrorProvider.SetError(txtERPGrid, "Enter a ERP GRID");
                  txtERPGrid.Focus();
                  zValidationFail = false;
              }
              if (txtERPWHS.Text.Trim() == "")
              {
                  dxErrorProvider.SetError(txtERPWHS, "Enter a ERP WHS");
                  txtERPWHS.Focus();
                  zValidationFail = false;

              }
              if (txtERPDistrict.Text.Trim() == "")
              {
                  dxErrorProvider.SetError(txtERPDistrict, "Enter a ERP District");
                  txtERPDistrict.Focus();
                  zValidationFail = false;
              }
          }
      }
      else  
      {
          if (m_FrmMode == EnumFrmMode.EDIT)
          {
              if (IsChildLocationFixed())
              {
                  dxErrorProvider.SetError(cbLocnFixedFlag, "It contains fixed child Location");
                  zValidationFail = false;
              }

          }

      }
      if (luPortalName.EditValue != null)
      {
          int zPortalID = int.Parse(luPortalName.EditValue.ToString());
          if (m_FrmMode == EnumFrmMode.CREATE)
          {
              if (m_ISMLoginInfo.ISMServer.IsPortalIDUsed(zPortalID))   
              {
                  dxErrorProvider.SetError(luPortalName, "Portal " + luPortalName.Text.Trim() + " is already used for other location ");
                  luPortalName.Focus();
                  zValidationFail = false;
              }
          }
          else if (m_FrmMode == EnumFrmMode.EDIT)
          {
              if (m_PortalID != luPortalName.EditValue.ToString())
              {
                  if (m_ISMLoginInfo.ISMServer.IsPortalIDUsed(zPortalID))   
                  {
                      dxErrorProvider.SetError(luPortalName, "Portal " + luPortalName.Text.Trim() + " is already used for other location ");
                      luPortalName.Focus();
                      zValidationFail = false;
                  }

              }

          }
      }
      luLabelType.EditValue = "pRFID";

      if (luLabelType.EditValue == null)
      {
          dxErrorProvider.SetError(luLabelType, "Select a Label Type");
          luLabelType.Focus();
          zValidationFail = false;
      }
      if (luLocationType.EditValue == null)
      {
          dxErrorProvider.SetError(luLocationType, "Select a Location Type");
          luLocationType.Focus();
          zValidationFail = false;
      }
      if (luParentLocUID.EditValue == null)
      {
          dxErrorProvider.SetError(luParentLocUID, "Select a Parent Location UID");
          luParentLocUID.Focus();
          zValidationFail = false;
      }
      if ((m_FrmMode == EnumFrmMode.CREATE) && (txtNewLocationUID.Text.Trim().Length != 13))
      {
          dxErrorProvider.SetError(txtNewLocationUID, "Location UID is not valid");
          txtNewLocationUID.Focus();
          zValidationFail = false;
      }
      if ((m_FrmMode == EnumFrmMode.CREATE) && ((txtNewLocationUID.Text.Trim() == "") || !(new ValidLocationNumber(txtNewLocationUID.Text).IsValidLocationNo())))
      {
          dxErrorProvider.SetError(txtNewLocationUID, "Enter a Valid Location UID [Range 8000000000001 to 8999999999999]");
          txtNewLocationUID.Focus();
          zValidationFail = false;
      }
      if ((m_FrmMode == EnumFrmMode.EDIT) && (luExistingLocnUID.Text.Trim().Length != 13))  
      {
          dxErrorProvider.SetError(luExistingLocnUID, "Location UID is not valid");
          luExistingLocnUID.Focus();
          zValidationFail = false;
      }  

      if ((m_FrmMode == EnumFrmMode.EDIT) && (luExistingLocnUID.Text.Trim() == ""))
      {
        dxErrorProvider.SetError(luExistingLocnUID, "Please select an existing Location UID");
        luExistingLocnUID.Focus();
        zValidationFail = false;
      }
      if (zValidationFail)
          zResult = zValidationFail;
      return zResult;
    }
    private void ClearErrorIconText()  
    {
        dxErrorProvider.SetError(txtERPDistrict, null);
        dxErrorProvider.SetError(txtERPWHS, null);
        dxErrorProvider.SetError(txtERPGrid, null);
        dxErrorProvider.SetError(txtERPBin, null);
        dxErrorProvider.SetError(luLabelType, null);
        dxErrorProvider.SetError(luLocationType, null);
        dxErrorProvider.SetError(luParentLocUID, null);
        dxErrorProvider.SetError(txtNewLocationUID, null);
        dxErrorProvider.SetError(luExistingLocnUID, null);
    }

    private void ClearControls()
    {
      luExistingLocnUID.EditValue = null;
      txtNewLocationUID.Text = "";
      txtUserDescription.Text = "";  
      luParentLocUID.EditValue = null;
      luLocationType.EditValue = null;
      luLabelType.EditValue = null;
      lbltxtSealType.Text = "";
      lbltxtSealUID.Text = "";
      cbLocnFixedFlag.Checked = false;
      cbLocnFixedFlag_CheckStateChanged(null, null); 

       
       

       

      /* DM out 25-AUG-10
      txtERPDistrict.Text = "";
      txtERPWHS.Text = "";
      txtERPGrid.Text = "";
      txtERPBin.Text = "";
       */

       
      m_UpdateRelationshipKeys.m_LocationUID = 0;
      m_UpdateRelationshipKeys.m_ChildID = 0;
      m_UpdateRelationshipKeys.m_PrevParentID = 0;
      m_UpdateRelationshipKeys.m_NewParentID = 0;
       
      
      luPortalName.EditValue = null;  
      txtPortalDescription.Text = "";  
      txtStatusMsg.EditValue = "";  
      ClearErrorIconText();  
      m_FixedLocation = false;  
      m_PortalID = "";  

      
    }

     
     
     
     
     
     
     
     
     
    /* Out DM 04-SEP-10   
    private void cbLocnFixedFlag_CheckedChanged(object sender, EventArgs e)
    {
      if (!cbLocnFixedFlag.Checked) 
      {
        txtERPDistrict.Text = "";
        txtERPWHS.Text = "";
        txtERPGrid.Text = "";
        txtERPBin.Text = "";
      }
      
      txtERPDistrict.Enabled = cbLocnFixedFlag.Checked;
      txtERPWHS.Enabled = cbLocnFixedFlag.Checked;
      txtERPGrid.Enabled = cbLocnFixedFlag.Checked;
      txtERPBin.Enabled = cbLocnFixedFlag.Checked;
    }
     
    */

    private void luExistingLocnUID_EditValueChanged(object sender, EventArgs e)
    {
       
      try
      {
          if (luExistingLocnUID.EditValue != null)  
          {
              txtStatusMsg.EditValue = "";  
              btnClearPortal.PerformClick();  
               
              m_UpdateRelationshipKeys.m_LocationUID = 0;
              m_UpdateRelationshipKeys.m_ChildID = 0;
              m_UpdateRelationshipKeys.m_PrevParentID = 0;
              m_UpdateRelationshipKeys.m_NewParentID = 0;
               
               
              if (m_ISMLoginInfo.Params.RootLocationUID == luExistingLocnUID.EditValue.ToString())
                  gcPortalDetails.Visible = false;
              else
                  gcPortalDetails.Visible = true;
               

              DataRowView zDataRowView = luExistingLocnUID.Properties.GetDataSourceRowByKeyValue(luExistingLocnUID.EditValue) as DataRowView;
              if (zDataRowView != null)   
              {
                   
                  ulong zValue = Convert.ToUInt64(zDataRowView[0]);
                  m_UpdateRelationshipKeys.m_LocationUID = zValue;
                  DataSet zLocationDS = m_ISMLoginInfo.ISMServer.LocationGetRecd(zValue);
                  if (zLocationDS != null)  
                  {
                      if (zLocationDS.Tables[ISMLocation.TableName].Rows.Count > 0)  
                      {
                          DataRow zCurrentDR = zLocationDS.Tables[ISMLocation.TableName].Rows[0];  
                          DataRow zParentData = zLocationDS.Tables["PARENT_RECD"].Rows[0];         
                          if (zCurrentDR != null)  
                          {
                              if (Convert.ToInt64(zCurrentDR[ISMLocation.ID]) > 0)  
                              {
                                  m_UpdateRelationshipKeys.m_ChildID = Convert.ToUInt64(zCurrentDR[ISMLocation.ID]);    
                                  m_UpdateRelationshipKeys.m_PrevParentID = Convert.ToUInt64(zParentData[ISMLocation.ID]);   
                              }
                              txtUserDescription.Text = zCurrentDR[ISMLocation.UserDesc].ToString();

                               
                              if (zCurrentDR[ISMLocation.SealUID] != DBNull.Value)  
                              {
                                  if (Convert.ToInt64(zCurrentDR[ISMLocation.SealUID]) > 0)
                                      lbltxtSealUID.Text = String.Format("5{0:000000000000}", zCurrentDR[ISMLocation.SealUID]);
                                  else
                                      lbltxtSealUID.Text = "";  
                              }
                              else
                                  lbltxtSealUID.Text = "";  


                              lbltxtSealType.Visible = false; 

                               
                              switch (zCurrentDR[ISMLocation.SealCode].ToString())
                              {
                                  case "0":
                                      lbltxtSealType.Text = "Non Sealed Location";
                                      break;
                                  case "1":
                                      lbltxtSealType.Text = "Non Electronic Tamper Evident";
                                      break;
                                  case "2":
                                      lbltxtSealType.Text = "Electronic Seal";
                                      break;
                              }

                              cbLocnFixedFlag.Checked = (zCurrentDR[ISMLocation.FixedFlag].ToString() == "1") ? true : false;
                              txtERPDistrict.Text = zCurrentDR[ISMLocation.ERPDIST].ToString();
                              txtERPWHS.Text = zCurrentDR[ISMLocation.ERPWHS].ToString();
                              txtERPGrid.Text = zCurrentDR[ISMLocation.ERPGRID].ToString();
                              txtERPBin.Text = zCurrentDR[ISMLocation.ERPBIN].ToString();

                            
                               
                               
                              int zRow = -1;
                              zRow = luLocationType.Properties.GetDataSourceRowIndex(ISMLocationType.LocTypeCode, zCurrentDR[ISMLocation.LocationCode].ToString());
                              luLocationType.EditValue = luLocationType.Properties.GetDataSourceValue(luLocationType.Properties.ValueMember, zRow);

                               
                               
                              string zTmpStr = String.Format("8{0:000000000000}", zParentData[ISMLocation.LocationUID]);
                              zRow = luParentLocUID.Properties.GetDataSourceRowIndex("LOCATIONUID", zTmpStr);
                              luParentLocUID.EditValue = luParentLocUID.Properties.GetDataSourceValue(luParentLocUID.Properties.ValueMember, zRow);
                               

                               
                               
                              zRow = luLabelType.Properties.GetDataSourceRowIndex(ISMLabelType.LabelTypeCode, zCurrentDR[ISMLabelType.LabelTypeCode].ToString());
                              luLabelType.EditValue = luLabelType.Properties.GetDataSourceValue(luLabelType.Properties.ValueMember, zRow);
                               
                              try
                              {
                                  m_PortalID = zCurrentDR[ISMLocation.PortalID].ToString();  
                                  luPortalName.EditValue = Convert.ToInt32(zCurrentDR[ISMLocation.PortalID]);  
                                 
                              }
                              catch (Exception)  
                              {
                                   
                              }
                          }
                      }
                  }
              }
          }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Location Lookup Edit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private bool Remove_Validation()  
    {
        bool zResult = false;
        bool zValidationFail = true; 

        try
        {
            if (luExistingLocnUID.EditValue == null)
            {
                dxErrorProvider.SetError(luExistingLocnUID, "Select a Location UID to be Remove");
                luExistingLocnUID.Focus();
                zValidationFail = false;
            }
            if (luExistingLocnUID.EditValue != null)  
            {
                string zLocationUID;
                zLocationUID = luExistingLocnUID.Text.Trim();
                long zLocUID = long.Parse(luExistingLocnUID.EditValue.ToString());

                if (m_ISMLoginInfo.Params.RootLocationUID == luExistingLocnUID.EditValue.ToString())
                {
                    dxErrorProvider.SetError(luExistingLocnUID, "You can’t Remove Root Location");
                    luExistingLocnUID.Focus();
                    zValidationFail = false;

                }

                 
                     

                DataSet ds = m_ISMLoginInfo.ISMServer.GetLocationTree(zLocUID);
                if (ds.Tables[0].Rows.Count > 1)
                {
                    dxErrorProvider.SetError(luExistingLocnUID, "You can’t Remove Location " + luExistingLocnUID.Text.Trim() + ". It contains child location(s)");
                    luExistingLocnUID.Focus();
                    zValidationFail = false;
                }

                 
                 
                      
            }
            if (cbLocnFixedFlag.Checked)
            {
                dxErrorProvider.SetError(luExistingLocnUID, "You can’t Remove Location " + luExistingLocnUID.Text.Trim() + " It is a fixed Location");
                luExistingLocnUID.Focus();
                zValidationFail = false;

            }

            if (zValidationFail)
                zResult = zValidationFail;

        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Remove Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        return zResult;
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
        if (m_FrmMode == EnumFrmMode.REMOVE)   
        {
            /* MR OUT Start 04-FEB-11
             
            if(luExistingLocnUID.EditValue == null) 
            {
                MessageBox.Show("Select a Location UID to be Remove", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string zLocationUID; 
            zLocationUID = luExistingLocnUID.Text.Trim(); 
            long zLocUID = long.Parse(luExistingLocnUID.EditValue.ToString());
            if (m_ISMLoginInfo.Params.RootLocationUID == luExistingLocnUID.EditValue.ToString())
            {
                MessageBox.Show("You can’t Remove Root Location " + luExistingLocnUID.Text.Trim(), lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            DataSet ds = m_ISMLoginInfo.ISMServer.pRFIDPortalTools(15, 0, "", zLocUID, 0,"");
            if (ds.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("You can’t Remove Location " + luExistingLocnUID.Text.Trim() + ". It has Stock Item", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
             
            ds = m_ISMLoginInfo.ISMServer.GetLocationTree(zLocUID); 
            if (ds.Tables[0].Rows.Count > 1)
            {
                MessageBox.Show("You can’t Remove Location " + luExistingLocnUID.Text.Trim() + ". It has Child Location", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (m_ISMLoginInfo.ISMServer.IsRemoveLocationHasTask(zLocUID))
            {
                MessageBox.Show("You can’t Remove Location " + luExistingLocnUID.Text.Trim() + ". It has some Task", lblHeader.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }MR OUT End 04-FEB-11*/
             
            if (Remove_Validation())  
            {
                string zMsgStr = string.Format("Are you sure? Do you want to Remove Crane(Humpy)\r\nCrane(Humpy) UID: {0:s}", luExistingLocnUID.Text.Trim());
                DialogResult zReply = MessageBox.Show(zMsgStr, "Crane/Humpy Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (zReply == DialogResult.Yes)
                {
                    string zLocationUID;
                    zLocationUID = luExistingLocnUID.Text.Trim(); 
                    RemoveLocationRecord();
                     
                     
                    ClearControls();
                    txtStatusMsg.Text = "Crane/Humpy " + zLocationUID + " has been removed";  
                     
                    LoadLocationMetaData();  
                }
            }
             
        }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      ClearControls();
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      if (Validation())  
      {
        string zMessage = "";  
        string zLocationUID;   
        zLocationUID = txtNewLocationUID.Text.Trim();  

        if (m_FrmMode == EnumFrmMode.CREATE)   
        {
            /* MR OUT Start 04-MAR-11
            if (luPortalName.EditValue != null)
            {
                int zPortalID = int.Parse(luPortalName.EditValue.ToString());
                if (m_ISMLoginInfo.ISMServer.IsPortalIDUsed(zPortalID))   
                {
                    luPortalName.ErrorText = String.Format("Portal {0} is already used for other location ", luPortalName.Text.Trim());
                    luPortalName.Focus();
                    return;
                }
            }MR OUT End 04-MAR-11 */
            if (!m_ISMLoginInfo.ISMServer.LocationExists(Convert.ToUInt64(txtNewLocationUID.Text.Remove(0, 1))))
            {
                zMessage = String.Format("Do you want create Crane/Humpy {0} under the Parent Crane/Humpy {1}?", txtNewLocationUID.Text, luParentLocUID.Text);  
                DialogResult zReply = MessageBox.Show(zMessage, "Crane/Humpy Create", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);  
                if (zReply == DialogResult.Yes)  
                {

                    SaveLocationRecord();
                     
                    ClearControls();
                    txtStatusMsg.Text = "Crane/Humpy " + zLocationUID + " has been created";
                    LoadLocationMetaData();  
                     
                     
                     
                     
                }
            }
            else
            {
                dxErrorProvider.SetError(txtNewLocationUID, "This Crane/Humpy already exists in the system");
                txtNewLocationUID.Focus();
            }

        }
        else if (m_FrmMode == EnumFrmMode.EDIT)   
        {
             /*  
             
            if (m_SealUID != txtSealUID.Text.Trim())
            {
                if (int.Parse(luSealType.EditValue.ToString()) == (int)ISM.SealType.Electronic)
                {
                    if (!m_ISMLoginInfo.ISMServer.IsStockReceiveSealUIDValid(long.Parse(txtSealUID.Text.Substring(1, 12))))
                    {
                        txtSealUID.ErrorText = "This Seal UID already exists in the system";
                        txtSealUID.Focus();
                        return;
                    }
                }
            }*/
             
            /* MR OUT Start 04-FEB-11
            if (luPortalName.EditValue != null)
            {
                if (m_PortalID != luPortalName.EditValue.ToString())
                {
                    int zPortalID = int.Parse(luPortalName.EditValue.ToString());
                    DataRowView zDataRowView = luPortalName.Properties.GetDataSourceRowByKeyValue(luPortalName.EditValue) as DataRowView;
                  if (m_ISMLoginInfo.ISMServer.IsPortalIDUsed(zPortalID))  
                    {
                        MessageBox.Show(String.Format("Portal {0} is already used for other location ", luPortalName.Text.Trim()), "Location", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
            } MR OUT End 04-FEB-11*/
            zMessage = String.Format("Do you want update Crane/Humpy {0} under the Parent Crane/Humpy {1}?", luExistingLocnUID.Text.Trim(), luParentLocUID.Text);  
            DialogResult zReply = MessageBox.Show(zMessage, "Crane/Humpy Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);  
            if (zReply == DialogResult.Yes)  
            {
                UpdateLocationRecord();
                zLocationUID = luExistingLocnUID.Text.Trim();  
                ClearControls();
                txtStatusMsg.Text = "Crane/Humpy " + zLocationUID + " has been updated";
                LoadLocationMetaData();  
                 
                 
                 
            }
        }
      }
    }


     
     
     
     
     
     
     
     
     
    private void cbLocnFixedFlag_CheckStateChanged(object sender, EventArgs e)
    {
      dxErrorProvider.SetError(cbLocnFixedFlag, null);   
      if (!cbLocnFixedFlag.Checked)
      {
        txtERPDistrict.Text = "";
        txtERPWHS.Text = "";
        txtERPGrid.Text = "";
        txtERPBin.Text = "";

          /* MR OUT Start 04-FEB-11
             
          txtPortalDescription.Text = "";
           
          luPortalName.EditValue = "";
           
          MR OUT End 04-FEB-11
          */
      }
      if (m_FrmMode == EnumFrmMode.CREATE || m_FrmMode == EnumFrmMode.EDIT)
      {
          txtERPDistrict.Enabled = cbLocnFixedFlag.Checked;
          txtERPWHS.Enabled = cbLocnFixedFlag.Checked;
          txtERPGrid.Enabled = cbLocnFixedFlag.Checked;
          txtERPBin.Enabled = cbLocnFixedFlag.Checked;

      }
      else
      {
          txtERPDistrict.Enabled = false;
          txtERPWHS.Enabled = false;
          txtERPGrid.Enabled = false;
          txtERPBin.Enabled = false;
      }

       
    }

    private void luPortalName_EditValueChanged(object sender, EventArgs e)
    {
      dxErrorProvider.SetError(luPortalName, null);  
      if(luPortalName.EditValue != null)
      {
          DataRowView zDataRowView = luPortalName.Properties.GetDataSourceRowByKeyValue(luPortalName.EditValue) as DataRowView;
          if (zDataRowView != null)   
          {
               
              txtPortalDescription.Text = zDataRowView[ISMPortal.Description].ToString();
              int zPortalID = int.Parse(luPortalName.EditValue.ToString());
              if (m_FrmMode == EnumFrmMode.CREATE)
              {
                  if (m_ISMLoginInfo.ISMServer.IsPortalIDUsed(zPortalID))   
                  {
                      dxErrorProvider.SetError(luPortalName, "Portal " + luPortalName.Text.Trim() + " is already used for other location");
                      luPortalName.Focus();
                  }
              }
              else if (m_FrmMode == EnumFrmMode.EDIT)
              {
                  if (m_PortalID != luPortalName.EditValue.ToString())
                  {
                      if (m_ISMLoginInfo.ISMServer.IsPortalIDUsed(zPortalID))   
                      {
                          dxErrorProvider.SetError(luPortalName, "Portal " + luPortalName.Text.Trim() + " is already used for other location");
                          luPortalName.Focus();
                      }
                  }

              }
          }
           
      }
      else
      {
           
        txtPortalDescription.Text = "";
           
      }
       

    }
     
     
    private void txtNewLocationUID_EditValueChanged(object sender, EventArgs e)
    {
        txtStatusMsg.Text = "";
    }
     
    private void btnClearPortal_Click(object sender, EventArgs e)
    {
        luPortalName.EditValue = null;
        txtPortalDescription.Text = "";
        if (m_FrmMode == EnumFrmMode.REMOVE)
            luPortalName.Properties.NullText = "";
         
         
    }
     
    
    private void txtNewLocationUID_Leave(object sender, EventArgs e)  
    {
        try
        {
            dxErrorProvider.SetError(txtNewLocationUID, null);
            if (txtNewLocationUID.Text.Trim() != "")
            {
                if ( ((txtNewLocationUID.Text.Trim() == "") ||(txtNewLocationUID.Text.Trim().Length < 13)|| !(new ValidLocationNumber(txtNewLocationUID.Text).IsValidLocationNo())))
                {
                    dxErrorProvider.SetError(txtNewLocationUID, "Enter a Valid Crane/Humpy UID [Range 8000000000001 to 8999999999999]");
                    return;
                }

                if (m_ISMLoginInfo.ISMServer.LocationExists(Convert.ToUInt64(txtNewLocationUID.Text.Remove(0, 1))))
                    dxErrorProvider.SetError(txtNewLocationUID, "This Crane/Humpy already exists in the system");
            }

        }
        catch(Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "New Crane/Humpy UID Leave", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }

    private void luParentLocUID_EditValueChanged(object sender, EventArgs e)  
    {
        try
        {
            dxErrorProvider.SetError(luParentLocUID, null);
            if (luParentLocUID.EditValue != null)
            {
              DataRowView zDataRowView = luParentLocUID.Properties.GetDataSourceRowByKeyValue(luParentLocUID.EditValue) as DataRowView;
              if (zDataRowView != null)
              {
                  m_FixedLocation = (zDataRowView[ISMLocation.FixedFlag].ToString() == "1") ? true : false;
              }
            }
           

        }
        catch (Exception ex)
        {
            MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Parent Loc Edit Value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    private void luLocationType_EditValueChanged(object sender, EventArgs e)  
    {
        dxErrorProvider.SetError(luLocationType, null);

    }

    private void luLabelType_EditValueChanged(object sender, EventArgs e)  
    {
        dxErrorProvider.SetError(luLabelType, null);
    }

    private void LocationAdmin_Load(object sender, EventArgs e)
    {
         
         
    }
  }
}
