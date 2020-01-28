 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ISMDAL.TableColumnName;

namespace ISM.Modules
{
  public partial class StockMoveCreateTask : ISMBaseWorkSpace
  {
    #region "Private Variable Declaration"

    private string m_StockTrackingInd = "";
     
     
     
     
     
     
     
     
    private byte m_nMoveType = 0;  
    private string m_OperatorID;
    #endregion

    #region "Property"
     
     
     
     
     
    
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
    public string StockTrackingIndicator
    {
      get { return m_StockTrackingInd; }
      set { m_StockTrackingInd = value; }
    }
    public Byte StockMoveType
    {
        get {return m_nMoveType;}
        set { m_nMoveType = value; }
    }
    #endregion

    public StockMoveCreateTask(ISMLoginInfo AISMLoginInfo)
      : base(AISMLoginInfo)
    {
      InitializeComponent();
    }

    private void StockMoveCreateTask_Load(object sender, EventArgs e)
    {
       
      SetLookUpEditCaption();
       
      gcStockData.LookAndFeel.SetSkinStyle(m_ISMLoginInfo.Params.AppSkinColour);  
      if (StockMoveType == (int)ISM.MoveType.LocationMove)
      {
          lookUpEditItemUID.Enabled = false;
          lookUpEditItemUID.BackColor = Color.White;  
          lookUpEditSourceLocUID.Properties.NullText = "Select a Location";  
      }
      else
      {
          lookUpEditSourceLocUID.Enabled = false;  
          lookUpEditItemUID.Properties.NullText = "Select a Item";  
      }
      btnClear.PerformClick();
    }

    #region "Initialize Data "
    private void SetLookUpEditCaption()
    {
      try
      {
        lookUpEditOperatorID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLogonID, 120, "User ID"),  
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserFirstName, 150,"First Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMUser.UserLastName, 150,"Last Name")});

        lookUpEditItemUID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ITEM_UID", 100, "Item UID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMStockCatalogue.StockShortName, 180,"Stock Short Name")});

        lookUpEditSourceLocUID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LOCATION_UID", 90, "Location UID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.LocationCode, 100,"Location Code"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPDIST, 80, "ERP DIST"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPWHS, 80, "ERP WHS"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPGRID, 80, "ERP GRID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPBIN, 80, "ERP BIN"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocationRelationship.ParentID, "ParentID",10,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center)});

        lookUpEditDestLocUID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LOCATION_UID", 90, "Location UID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.LocationCode, 100,"Location Code"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPDIST, 80, "ERP DIST"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPWHS, 80, "ERP WHS"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPGRID, 80, "ERP GRID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPBIN, 80, "ERP BIN"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocationRelationship.ParentID, "ParentID",10,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center)});

         
        lookUpEditOperatorID.Properties.DisplayMember = ISMUser.UserLogonID;
        lookUpEditOperatorID.Properties.ValueMember = ISMUser.UserID;

        lookUpEditItemUID.Properties.DisplayMember = "ITEM_UID";  
        lookUpEditItemUID.Properties.ValueMember = ISMStock.StockItemUID;

        lookUpEditSourceLocUID.Properties.DisplayMember = "LOCATION_UID";  
        lookUpEditSourceLocUID.Properties.ValueMember = ISMLocation.LocationUID;

        lookUpEditDestLocUID.Properties.DisplayMember = "LOCATION_UID";  
        lookUpEditDestLocUID.Properties.ValueMember = ISMLocation.LocationUID;
      }
      catch (Exception ex)   
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stock Move", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }

    private void LoadStockMoveMetaData()
    {
      try
      {
           
          int zMod = 99;  
          DataSet zdsOperatorList = new DataSet();
          if (StockMoveType == (int)ISM.MoveType.LocationMove)
          {
              zMod = 5; 
          }
          else
          {
              zMod = 4; 
          }
          zdsOperatorList = m_ISMLoginInfo.ISMServer.GetISMOperatorID(zMod, m_ISMLoginInfo.UserProfileCode, m_ISMLoginInfo.UserID);
          if (zdsOperatorList != null)
          {
              lookUpEditOperatorID.Properties.DataSource = zdsOperatorList.Tables[0].DefaultView;
               
              lookUpEditOperatorID.EditValue = lookUpEditOperatorID.Properties.GetKeyValueByDisplayText(m_ISMLoginInfo.LogonID);
          }
           


        DataSet ds = m_ISMLoginInfo.ISMServer.GetStockMoveMetaData();
        if (ds != null)
        {
           
          lookUpEditItemUID.Properties.DataSource = ds.Tables[ISMStock.TableName].DefaultView;
          lookUpEditSourceLocUID.Properties.DataSource = ds.Tables["SOURCELOCATION"].DefaultView;
          lookUpEditDestLocUID.Properties.DataSource = ds.Tables["DESTLOCATION"].DefaultView;
           
          lookUpEditOperatorID.EditValue = lookUpEditOperatorID.Properties.GetKeyValueByDisplayText(m_ISMLoginInfo.LogonID);
          lblMovedBy.Text = m_ISMLoginInfo.LogonID;
        }
      }
      catch (Exception ex) 
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stock Move", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      
    }
    #endregion

    #region "Lookup Edit Control Event"
    private void lookUpEditOperatorID_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        dxErrorProvider.SetError(lookUpEditOperatorID, null);
        lblMovedBy.Text = lookUpEditOperatorID.Text;
        if (m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text)
        {
          gcCreateTask.Text = "Stock Move";
          btnSave.Text = "Move";
        }
        else
        {
          gcCreateTask.Text = "Create Task";
          btnSave.Text = "Create";
        }
        lookUpEditOperatorID.Properties.ForceInitialize();
        object oValue = lookUpEditOperatorID.Properties.GetDataSourceRowByKeyValue(lookUpEditOperatorID.EditValue);
        if (oValue != null)
        {
          DataRow dr = ((DataRowView)oValue).Row;
          if (dr != null)
            m_OperatorID = dr[ISMUser.UserID].ToString();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stock Move", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }

    private void lookUpEditItemUID_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
          if (lookUpEditItemUID.EditValue != null)
          {
              string zVolumetric = "";
              byte nSealCode = 0;
              txtStatusMsg.Text = "";  
              dxErrorProvider.SetError(lookUpEditItemUID, null);
              DataSet ds = m_ISMLoginInfo.ISMServer.GetLocationUIDForItemUID(lookUpEditItemUID.EditValue.ToString());
              lookUpEditSourceLocUID.EditValue = null;
              lookUpEditSourceLocUID.Properties.DataSource = ds.Tables[0].DefaultView;
              if (ds.Tables[0].Rows.Count > 0)
              {
                  DataRow dr = ds.Tables[0].Rows[0];
                  lookUpEditSourceLocUID.EditValue = lookUpEditSourceLocUID.Properties.GetKeyValueByDisplayText(dr["LOCATION_UID"].ToString());
                   
                  lblCurrLoc.Text = lookUpEditSourceLocUID.Text;
                  lblERPDistrict.Text = dr[ISMLocation.ERPDIST].ToString();
                  lblERPWHS.Text = dr[ISMLocation.ERPWHS].ToString();
                  lblERPGrid.Text = dr[ISMLocation.ERPGRID].ToString();
                  lblERPBin.Text = dr[ISMLocation.ERPBIN].ToString();
                   
                  object oValue = lookUpEditSourceLocUID.Properties.GetDataSourceRowByKeyValue(lookUpEditSourceLocUID.EditValue);
                  if (oValue != null)
                  {
                      dr = ((DataRowView)oValue).Row;
                      if (dr != null)
                      {
                           
                          lblSoruceLocParID.Text = m_ISMLoginInfo.Params.LocPrefix + dr[ISMLocationRelationship.ParentID].ToString().PadLeft(12, '0');
                          nSealCode = byte.Parse(dr[ISMLocation.SealCode].ToString());
                          switch (nSealCode)
                          {
                              case 0:
                                  lblSorSealCode.Text = nSealCode + " - No Seal";
                                  lblSorSealUID.Text = "";  
                                  break;
                              case 1:
                                  lblSorSealCode.Text = nSealCode + " - Tamper Evident";
                                  if (dr[ISMLocation.SealUID].ToString() != "0")
                                       
                                      lblSorSealUID.Text = m_ISMLoginInfo.Params.SealPrefix + dr[ISMLocation.SealUID].ToString().PadLeft(12, '0');  
                                  else
                                      lblSorSealUID.Text = "";
                                  break;
                              case 2:
                                  lblSorSealCode.Text = nSealCode + " - Electronic";
                                  if (dr[ISMLocation.SealUID].ToString() != "0")
                                       
                                      lblSorSealUID.Text = m_ISMLoginInfo.Params.SealPrefix + dr[ISMLocation.SealUID].ToString().PadLeft(12, '0');  
                                  else
                                      lblSorSealUID.Text = "";
                                  break;
                          }
                      }

                  }
                  oValue = lookUpEditItemUID.Properties.GetDataSourceRowByKeyValue(lookUpEditItemUID.EditValue);
                  if (oValue != null)
                  {
                      dr = ((DataRowView)oValue).Row;
                      if (dr != null)
                      {
                          lblStockCode.Text = dr[ISMStock.StockCatalogueCode].ToString();
                          lblShortName.Text = dr[ISMStockCatalogue.StockShortName].ToString();
                          lblUnitOfIssue.Text = dr[ISMStockCatalogue.StockUnitOfIssue].ToString();
                          lblSerialNo.Text = dr[ISMStock.StockSerialEquipNo].ToString();
                          lblBatchNo.Text = dr[ISMStock.StockBatchLotNo].ToString();
                          if (dr[ISMStockCatalogue.StockHeight].ToString() != "")
                              zVolumetric = "H: " + dr[ISMStockCatalogue.StockHeight].ToString() + " X ";
                          if (dr[ISMStockCatalogue.StockWeight].ToString() != "")
                              zVolumetric = zVolumetric + "W: " + dr[ISMStockCatalogue.StockWeight].ToString() + " X ";
                          if (dr[ISMStockCatalogue.StockLength].ToString() != "")
                              zVolumetric = zVolumetric + "L: " + dr[ISMStockCatalogue.StockLength].ToString();
                          lblVolumetric1.Text = zVolumetric;
                          if (dr[ISMStockCatalogue.StockWidth].ToString() != "")
                              zVolumetric = "X WD:" + dr[ISMStockCatalogue.StockWidth].ToString() + " X ";
                          if (dr[ISMStockCatalogue.StockVolume].ToString() != "")
                              zVolumetric = zVolumetric + "V: " + dr[ISMStockCatalogue.StockVolume].ToString();
                          lblVolumetric2.Text = zVolumetric;   
                          lblCategory.Text = dr["INVENTORYCATEGORY_CODE"].ToString();
                      }
                  }
                   
              }
              else
              {
                  MessageBox.Show(String.Format("Data doesn't Exist for Item UID " + lookUpEditItemUID.EditValue.ToString()), "Stock Move", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  lookUpEditItemUID.Focus();
             }
          }  
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stock Move", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void lookUpEditSourceLocUID_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
         
        if (StockMoveType == (int)ISM.MoveType.ItemMove && lookUpEditItemUID.EditValue == null)
        {
          dxErrorProvider.SetError(lookUpEditSourceLocUID, "Select Item UID");
          lookUpEditSourceLocUID.EditValue = null;
          lookUpEditItemUID.Focus();
          return;
        }
          
        if (lookUpEditSourceLocUID.EditValue != null)
        {
          byte nSealCode = 0;
          long zSourceLocUID = 0;
          txtStatusMsg.Text = "";  
          dxErrorProvider.SetError(lookUpEditSourceLocUID, null);
           
          if (StockMoveType == (int)ISM.MoveType.LocationMove)
          {
              zSourceLocUID = long.Parse(lookUpEditSourceLocUID.EditValue.ToString());
              DataSet ds = m_ISMLoginInfo.ISMServer.GetStockMoveDestLoc(zSourceLocUID);
              if (ds != null)
              {
                  lookUpEditDestLocUID.EditValue = null;
                  lookUpEditDestLocUID.Properties.DataSource = ds.Tables["DESTLOCATION"].DefaultView;
              }
          }
           
          lookUpEditSourceLocUID.Properties.ForceInitialize();
          object oValue = lookUpEditSourceLocUID.Properties.GetDataSourceRowByKeyValue(lookUpEditSourceLocUID.EditValue);
          if (oValue != null)
          {
            DataRow dr = ((DataRowView)oValue).Row;
            if (dr != null)
            {
              lblCurrLoc.Text = lookUpEditSourceLocUID.Text;
              lblERPDistrict.Text = dr[ISMLocation.ERPDIST].ToString();
              lblERPWHS.Text = dr[ISMLocation.ERPWHS].ToString();
              lblERPGrid.Text = dr[ISMLocation.ERPGRID].ToString();
              lblERPBin.Text = dr[ISMLocation.ERPBIN].ToString();
               
               
               
              lblSoruceLocParID.Text = m_ISMLoginInfo.Params.LocPrefix + dr[ISMLocationRelationship.ParentID].ToString().PadLeft(12, '0');
              nSealCode = byte.Parse(dr[ISMLocation.SealCode].ToString());
              switch (nSealCode)
              {
                case 0:
                  lblSorSealCode.Text = nSealCode + " - No Seal";
                  lblSorSealUID.Text = "";  
                  break;
                case 1:
                  lblSorSealCode.Text = nSealCode + " - Tamper Evident";
                  if (dr[ISMLocation.SealUID].ToString() != "0")
                     
                      lblSorSealUID.Text = m_ISMLoginInfo.Params.SealPrefix + dr[ISMLocation.SealUID].ToString().PadLeft(12, '0');  
                  else
                    lblSorSealUID.Text = "";
                  break;
                case 2:
                  lblSorSealCode.Text = nSealCode + " - Electronic";
                  if (dr[ISMLocation.SealUID].ToString() != "0")
                     
                      lblSorSealUID.Text = m_ISMLoginInfo.Params.SealPrefix + dr[ISMLocation.SealUID].ToString().PadLeft(12, '0');  
                  else
                    lblSorSealUID.Text = "";
                  break;
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stock Move", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void lookUpEditDestLocUID_EditValueChanged(object sender, EventArgs e)
    {
      try
      {
        if (lookUpEditDestLocUID.EditValue != null)
        {
          byte zSealCode = 0;
          txtStatusMsg.Text = "";  
          lblNewLoc.Text = lookUpEditDestLocUID.Text;
          dxErrorProvider.SetError(lookUpEditDestLocUID, null);
          lookUpEditDestLocUID.Properties.ForceInitialize();
          object oValue = lookUpEditDestLocUID.Properties.GetDataSourceRowByKeyValue(lookUpEditDestLocUID.EditValue);
          if (oValue != null)
          {
            DataRow dr = ((DataRowView)oValue).Row;
            if (dr != null)
            {
               
              lblDestDist.Text = dr[ISMLocation.ERPDIST].ToString();
              lblDestWHS.Text = dr[ISMLocation.ERPWHS].ToString();
              lblDestGrid.Text = dr[ISMLocation.ERPGRID].ToString();
              lblDestBin.Text = dr[ISMLocation.ERPBIN].ToString();
               
               
              lblDestLocParID.Text = m_ISMLoginInfo.Params.LocPrefix + dr[ISMLocationRelationship.ParentID].ToString().PadLeft(12, '0');
              if (dr[ISMLocation.SealUID].ToString() != "0")       
                 
                  lblDesSealUID.Text = m_ISMLoginInfo.Params.SealPrefix + dr[ISMLocation.SealUID].ToString().PadLeft(12, '0');
              else
                lblDesSealUID.Text = "";
              zSealCode = byte.Parse(dr[ISMLocation.SealCode].ToString());
               
              switch (zSealCode)
              {
                case 0:
                   lblDesSealCode.Text = zSealCode + " - No Seal";
                  break;
                case 1:
                  lblDesSealCode.Text = zSealCode + " - Tamper Evident";
                  break;
                case 2:
                  lblDesSealCode.Text = zSealCode + " - Electronic";
                  break;
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stock Move", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    private void dtMoveDate_EditValueChanged(object sender, EventArgs e)
    {
      dxErrorProvider.SetError(dtMoveDate, null);
      txtStatusMsg.Text = "";  
      lblMovedOn.Text = dtMoveDate.Text;
    }
    #endregion

    #region "Validataion"
    private void ClearErrorIconText()
    {
      try
      {
        dxErrorProvider.SetError(lookUpEditOperatorID, null);
        dxErrorProvider.SetError(lookUpEditItemUID, null);
        dxErrorProvider.SetError(lookUpEditSourceLocUID, null);
        dxErrorProvider.SetError(lookUpEditDestLocUID, null);
        dxErrorProvider.SetError(dtMoveDate, null);
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stock Move", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private bool Validation()
    {
      bool zResult = false;
      bool zValidationFail = true;
      ClearErrorIconText();
      try
      {
          if ((lookUpEditDestLocUID.EditValue != null && lookUpEditSourceLocUID.EditValue != null) && lookUpEditSourceLocUID.EditValue.ToString() == lookUpEditDestLocUID.EditValue.ToString())
        {
          dxErrorProvider.SetError(lookUpEditSourceLocUID, "Source and Destination Location UID can't be same");
          dxErrorProvider.SetError(lookUpEditDestLocUID, "Source and Destination Location UID can't be same");
          lookUpEditDestLocUID.Focus();
          zValidationFail = false;
        }
        if (lookUpEditDestLocUID.EditValue == null)
        {
          dxErrorProvider.SetError(lookUpEditDestLocUID, "Select a Destination Location UID");
          lookUpEditDestLocUID.Focus();
          zValidationFail = false;
        }
        if (lookUpEditSourceLocUID.EditValue == null)
        {
          dxErrorProvider.SetError(lookUpEditSourceLocUID, "Select a Source Location UID");
          lookUpEditSourceLocUID.Focus();
          zValidationFail = false;
        }
        if (StockMoveType == 0)
        {
          if (lookUpEditItemUID.Text.Trim() == "")
          {
            dxErrorProvider.SetError(lookUpEditItemUID, "Select a Item UID");
            lookUpEditItemUID.Focus();
            zValidationFail = false;
          }
        }
        if (dtMoveDate.EditValue == null)
        {
          dxErrorProvider.SetError(dtMoveDate, "Select a Move Date");
          dtMoveDate.Focus();
          zValidationFail = false;
        }
        else if (dtMoveDate.EditValue != null)
        {
          if (m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text.Trim())
          {
            if (DateTime.Parse(dtMoveDate.EditValue.ToString()) != DateTime.Today)
            {
              dxErrorProvider.SetError(dtMoveDate, "Enter Current Date");
              dtMoveDate.Focus();
              zValidationFail = false;
            }
          }
          else if (DateTime.Parse(dtMoveDate.EditValue.ToString()) < DateTime.Today)
          {
            dxErrorProvider.SetError(dtMoveDate, "Enter Current Date or Future Date");
            dtMoveDate.Focus();
            zValidationFail = false;
          }
        }
        if (lookUpEditOperatorID.Text.Trim() == "")
        {
          dxErrorProvider.SetError(lookUpEditOperatorID, "Select a Operator ID");
          lookUpEditOperatorID.Focus();
          zValidationFail = false;
        }
        if (zValidationFail)
          zResult = zValidationFail;
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stock Move", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      return zResult;
    }
    #endregion

    #region "Save Button"
    private void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        if (Validation())
        {
            string zMessage = "";
            string zConMessage = "";
            if (m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text)
            {
               if (StockMoveType == (int)ISM.MoveType.ItemMove)
              {
                zMessage = String.Format("Do you want to perform Stock Move for the Item {0} from Source Loc {1} to Dest Loc {2}?", lookUpEditItemUID.Text, lookUpEditSourceLocUID.Text, lookUpEditDestLocUID.Text);
                 
                zConMessage = "Stock Item " + lookUpEditItemUID.Text + " has been moved from Source Location " + lookUpEditSourceLocUID.Text + "\r\nto Destination Location  " + lookUpEditDestLocUID.Text;
              }
              else
              {
                zMessage = String.Format("Do you want to perform Stock Move from Source Loc {0} to Dest Loc {1} ?", lookUpEditSourceLocUID.Text, lookUpEditDestLocUID.Text);
                 
                zConMessage = "Stock Location has been moved from Source Location " + lookUpEditSourceLocUID.Text + " to Destination Location  " + lookUpEditDestLocUID.Text;
              }
            }
            else
            {
              if (StockMoveType == (int)ISM.MoveType.ItemMove)
              {
                zMessage = String.Format("Do you want to create Stock Move Task for the Item {0} Source Loc {1} and Dest Loc {2}?", lookUpEditItemUID.Text, lookUpEditSourceLocUID.Text, lookUpEditDestLocUID.Text);
                 
                zConMessage = "Stock Move Task has been created for the Item UID " + lookUpEditItemUID.Text + ", Source Location " + lookUpEditSourceLocUID.Text + "\r\nand Destination Location  " + lookUpEditDestLocUID.Text;
              }
              else
              {
                zMessage = String.Format("Do you want to create Stock Move Task for the Source Loc {0} and Dest Loc {1} ?", lookUpEditSourceLocUID.Text, lookUpEditDestLocUID.Text);
                 
                zConMessage = "Stock Move Task has been created for Source Location " + lookUpEditSourceLocUID.Text + " and Destination Location  " + lookUpEditDestLocUID.Text;
              }
            }
            DialogResult zReply = MessageBox.Show(zMessage, "Stock Move", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (zReply == DialogResult.Yes)
            {
              long zItemUID = 0;
              int nMode;
              ISMTask.StructTask zTask = new ISMTask.StructTask();

              if (m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text)
              {
                zTask.Type = "3";
                zTask.Status = "Completed";
                nMode = 0;
                if (StockMoveType == (int)ISM.MoveType.ItemMove)
                {
                    zTask.StockCode = lblStockCode.Text.Trim();  
                    zTask.OperationID = "32";  
                }
                else
                {
                    zTask.StockCode = "";  
                    zTask.OperationID = "26";  
                }
              }
              else
              {
                zTask.Type = "1";
                zTask.Status = "Assigned";
                 
                nMode = 1;
                if (StockMoveType == (int)ISM.MoveType.ItemMove)
                {
                    zTask.OperationID = "7";  
                    zTask.StockCode = lblStockCode.Text.Trim();  
                }
                else
                {
                    zTask.OperationID = "12";  
                    zTask.StockCode = "";  
                }
                   
              }
              zTask.StockQty = "0";
              zTask.SourceID = lookUpEditSourceLocUID.EditValue.ToString();
              zTask.DestinationID = lookUpEditDestLocUID.EditValue.ToString();

              if (lookUpEditItemUID.EditValue == null)
                zTask.ItemID = "0";
              else
              {
                zTask.ItemID = lookUpEditItemUID.EditValue.ToString();
                zItemUID = long.Parse(lookUpEditItemUID.EditValue.ToString());
              }
              zTask.StatusCode = "1"; 
              zTask.UserID = m_OperatorID;
              zTask.CreateUserID = m_ISMLoginInfo.LogonID;
               
               
              zTask.CreateDateTime = String.Format(dtMoveDate.EditValue.ToString().Substring(0, 10) + " " + DateTime.Now.ToLongTimeString(), m_ISMLoginInfo.Params.DateTimeFormat);  

               
              if (m_ISMLoginInfo.ISMServer.CreateStockMoveTask(nMode, zTask,lblCategory.Text.Trim()))  
              {
                 
                 
                 
                 
                 
                 
                 
                 
                 
                 
                btnClear.PerformClick();  
                txtStatusMsg.Text = zConMessage;  
               
              }
            }
          }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stock Move", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

    }
    #endregion

    #region "Clear Button"
    private void btnClear_Click(object sender, EventArgs e)
    {
      try
      {
        lookUpEditOperatorID.EditValue = null;
        lookUpEditItemUID.EditValue = null;
        lookUpEditSourceLocUID.EditValue = null;
        lookUpEditDestLocUID.EditValue = null;
        dtMoveDate.EditValue = null;
        lblSoruceLocParID.Text = "";
        lblSorSealCode.Text = "";
        lblSorSealUID.Text = "";
        lblDesSealCode.Text = "";
        lblDesSealUID.Text = "";
        lblDestLocParID.Text = "";
         
         
         
        lblMovedBy.Text = "";
        lblStockCode.Text = "";
        lblShortName.Text = "";
        lblUnitOfIssue.Text = "";
        lblSerialNo.Text = "";
        lblBatchNo.Text = "";
        lblERPDistrict.Text = "";
        lblERPWHS.Text = "";
        lblERPGrid.Text = "";
        lblERPBin.Text = "";
        lblNewLoc.Text = "";
        lblVolumetric1.Text = "";
        lblVolumetric2.Text = "";
        lblMovedOn.Text = "";
        lblCurrLoc.Text = "";
         
        lblDestDist.Text = "";
        lblDestWHS.Text = "";
        lblDestGrid.Text = "";
        lblDestBin.Text = "";
        btnSave.Enabled = true;
        lblCategory.Text = "";  
        LoadStockMoveMetaData();
        lookUpEditOperatorID.Focus();
        ClearErrorIconText();
        txtStatusMsg.Text = "";  
        lblCategory.Text = "";  

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stock Move", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
    #endregion

    private void btnException_Click(object sender, EventArgs e)
    {
      try
      {
        long zItemUID = 0;
        long zLocationUID = 0;
        long zSealUID = 0;
        string zStockCode = "";
        string zExpMessage = "Exception has been created for ";
         
        if (StockMoveType == (int)ISM.MoveType.ItemMove)
        {
           
          if (lookUpEditItemUID.EditValue == null && lookUpEditSourceLocUID.EditValue == null && lookUpEditDestLocUID.EditValue == null)
          {
            MessageBox.Show("Select at least any one of the key field e.g. Item UID or Source Location UID or Destination Location UID", "Stock Move", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }
          else
          {
              if (lookUpEditItemUID.EditValue != null)
              {
                  zItemUID = long.Parse(lookUpEditItemUID.EditValue.ToString());
                  zExpMessage = zExpMessage + "Item UID " + lookUpEditItemUID.Text + " ";
              }
              if (lookUpEditSourceLocUID.EditValue != null)
              {
                  zLocationUID = long.Parse(lookUpEditSourceLocUID.EditValue.ToString());
                  zExpMessage = zExpMessage + "Source Location " + lookUpEditSourceLocUID.Text + " ";
              }
              if (lookUpEditDestLocUID.EditValue != null)
              {
                  zLocationUID = long.Parse(lookUpEditDestLocUID.EditValue.ToString());
                  zExpMessage = zExpMessage + "Destination Location " + lookUpEditDestLocUID.Text + " ";
              }
          }
        }
        else
        {
          if (lookUpEditSourceLocUID.EditValue == null && lookUpEditDestLocUID.EditValue == null)
          {
            MessageBox.Show("Select at least any one of the key field e.g. Source Location UID or Destination Location UID", "Stock Move", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }
          else
          {
              if (lookUpEditSourceLocUID.EditValue != null)
              {
                  zLocationUID = long.Parse(lookUpEditSourceLocUID.EditValue.ToString());
                  zExpMessage = zExpMessage + "Source Location " + lookUpEditSourceLocUID.Text + " ";
              }
              if (lookUpEditDestLocUID.EditValue != null)
              {
                  zLocationUID = long.Parse(lookUpEditDestLocUID.EditValue.ToString());
                  zExpMessage = zExpMessage + "Destination Location " + lookUpEditDestLocUID.Text + " ";
              }
          }
        }

         
         
         
         
         
         
         
         

         
         

        ISM.Forms.FrmException frmSTException = new ISM.Forms.FrmException(m_ISMLoginInfo) { LocationUID = zLocationUID, ItemUID = zItemUID, 
                                                                                              StockCode = zStockCode, SealUID = zSealUID, 
                                                                                              JournalType = "SMV",
                                                                                              MsgBoxCaption = "Stock Move",
                                                                                              InvenCatCode = lblCategory.Text.Trim()  
                                                                                              };
        frmSTException.Text = "Stock Move Exception";
        frmSTException.ShowDialog();
        
         
         
        if (frmSTException.SaveResult)
            txtStatusMsg.Text = zExpMessage;
        else
            txtStatusMsg.EditValue = null;
           

      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stock Move", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

   
  }
}
