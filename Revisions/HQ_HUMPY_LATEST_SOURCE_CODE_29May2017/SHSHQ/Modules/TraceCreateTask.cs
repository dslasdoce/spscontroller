 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

using System;
using ISMDAL.TableColumnName;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

namespace ISM.Modules
{
  public partial class TraceCreateTask : ISMBaseWorkSpace
  {
      #region "Private Variable Declaration"
      private byte m_TraceType = 0;  
      private string m_OperatorID;
      #endregion

      #region "Property"
      public byte TraceType
      {
          get { return m_TraceType; }
          set { m_TraceType = value; }
      }
      #endregion

      public TraceCreateTask(ISMLoginInfo AISMLoginInfo)
      : base(AISMLoginInfo)
    {
      InitializeComponent();
    }

      private void TraceCreateTask_Load(object sender, EventArgs e)
      {
           
          if (TraceType == (int)ISM.TraceType.ItemTrace)
          {
              lookUpEditItemUID.Enabled = true;
              lookUpEditItemUID.Properties.NullText = "Select a Item UID";  
              lookUpEditLocationUID.Enabled = false;
              lookUpEditLocationUID.BackColor = Color.White;
          }
          else
          {
              lookUpEditItemUID.Enabled = false;
              lookUpEditLocationUID.Enabled = true;
              lookUpEditLocationUID.Properties.NullText = "Select a Location UID";  
              lookUpEditItemUID.BackColor = Color.White;
          }
          
           
          txtStockCode.Enabled = false;
           
          SetLookUpEditCaption();
          btnClear.PerformClick();
          
      }
      #region "Lookup Edit Caption"
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
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMStockCatalogue.StockShortName, 180,"Stock Short Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMStock.StockCatalogueCode, "Stock Code",10,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LOCATION_UID", "Location UID",10,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODE", "CODE",10,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center)  });
            
              lookUpEditLocationUID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LOCATION_UID", 90, "Location UID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.LocationCode, 100,"Location Code"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPDIST, 80, "ERP DIST"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPWHS, 80, "ERP WHS"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPGRID, 80, "ERP GRID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLocation.ERPBIN, 80, "ERP BIN")});

             
             
             
             

               
              lookUpEditOperatorID.Properties.DisplayMember = ISMUser.UserLogonID;
              lookUpEditOperatorID.Properties.ValueMember = ISMUser.UserID;

              lookUpEditItemUID.Properties.DisplayMember = "ITEM_UID";  
              lookUpEditItemUID.Properties.ValueMember = ISMStock.StockItemUID;

               
               
               

              lookUpEditLocationUID.Properties.DisplayMember = "LOCATION_UID";  
              lookUpEditLocationUID.Properties.ValueMember = ISMLocation.LocationUID;
          }
          catch (Exception ex)
          {
              MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Trace", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
      }
      #endregion

      #region "Load Meta Data"
      private void LoadTraceMetaData()
      {
          DataSet ds = null;
          try
          {
               
               

              int zMod = 0;
              if (TraceType == (int)ISM.TraceType.ItemTrace)
                  zMod = 6;
              else
                  zMod = 7;
              ds = m_ISMLoginInfo.ISMServer.GetISMOperatorID(zMod, m_ISMLoginInfo.UserProfileCode, m_ISMLoginInfo.UserID);  
              if(ds != null)
                lookUpEditOperatorID.Properties.DataSource = ds.Tables[0].DefaultView;


              ds = m_ISMLoginInfo.ISMServer.GetTraceMetaData();
              if (ds != null)
              {
                   
                  lookUpEditItemUID.Properties.DataSource = ds.Tables[ISMStock.TableName].DefaultView;
                  lookUpEditLocationUID.Properties.DataSource = ds.Tables[ISMLocation.TableName].DefaultView;
                   
                   
                   
                  lookUpEditOperatorID.EditValue = lookUpEditOperatorID.Properties.GetKeyValueByDisplayText(m_ISMLoginInfo.LogonID);
              }

          }
          catch (Exception ex)
          {
              MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Trace", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
      }
      #endregion

      #region "Looup Edit Event"
      private void lookUpEditOperatorID_EditValueChanged(object sender, EventArgs e)
      {
          lblTraceBy.Text = lookUpEditOperatorID.Text;
          if (m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text)
          {
              if (TraceType == (int)ISM.TraceType.ItemTrace)
                 gcCreateTask.Text = "Item Trace";
              else
                  gcCreateTask.Text = "Location Trace";   
              btnSave.Text = "Trace";
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
      private void lookUpEditItemUID_EditValueChanged(object sender, EventArgs e)
      {
          try
          {
              if (lookUpEditItemUID.EditValue != null)
              {
                  DataSet ds = null;
                  DataRow dr = null;
                  txtStatusMsg.Text = "";  
                  long zItemCode = long.Parse(lookUpEditItemUID.EditValue.ToString());
                  byte nMode = 0;

                  object oValue = lookUpEditItemUID.Properties.GetDataSourceRowByKeyValue(lookUpEditItemUID.EditValue);
                  if (oValue != null)
                  {
                      dr = ((DataRowView)oValue).Row;
                      if (dr != null)
                      {
                          txtStockCode.Text = dr[ISMStock.StockCatalogueCode].ToString();
                          lblCategory.Text = dr["CODE"].ToString();  
                      }
                      else  
                      {
                          txtStockCode.Text = "";
                          lblCategory.Text = "";
                      }
                  }

                  ds = m_ISMLoginInfo.ISMServer.GetStockCodeAndLocationUIDForItemUID(zItemCode);
                  if (ds != null)
                  {
                       
                       
                       
                       
                       
                       
                       
                       
                       
                      lookUpEditLocationUID.EditValue = null;
                      lookUpEditLocationUID.Properties.ForceInitialize();
                      lookUpEditLocationUID.Properties.DataSource = ds.Tables[1].DefaultView;
                      dr = ds.Tables[1].Rows[0];
                      lookUpEditLocationUID.EditValue = lookUpEditLocationUID.Properties.GetKeyValueByDisplayText(dr["LOCATION_UID"].ToString());
                  }
                  DispalyData(nMode, lookUpEditItemUID.EditValue.ToString());
              }
          }

          catch (Exception ex)
          {
              MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Trace", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
      }

       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       

       
       
       
       
       

       

      private void lookUpEditLocationUID_EditValueChanged(object sender, EventArgs e)
      {
          try
          {
              if (lookUpEditLocationUID.EditValue == null)
                  return;

              txtStatusMsg.Text = "";  
              object oValue = lookUpEditLocationUID.Properties.GetDataSourceRowByKeyValue(lookUpEditLocationUID.EditValue);
              if (oValue != null)
              {
                  DataRow dr = ((DataRowView)oValue).Row;
                  if (dr != null)
                  {
                      lblERPDistrict.Text = dr[ISMLocation.ERPDIST].ToString();
                      lblERPWHS.Text = dr[ISMLocation.ERPWHS].ToString();
                      lblERPGrid.Text = dr[ISMLocation.ERPGRID].ToString();
                      lblERPBin.Text = dr[ISMLocation.ERPBIN].ToString();
                  }
              }
              byte nMode = 1;
              DispalyData(nMode, lookUpEditLocationUID.EditValue.ToString());
          }
          catch (Exception ex)
          {
              MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Trace", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
      }
      #endregion

      #region "Stock Data"
      private void DispalyData(byte AMode, string ADataWhere)
      {
          try
          {
               
              DataSet ds = null;
              DataRow dr = null;
              string zVolumetric = "";
              string zStockTracInd = "";
              ClearErrorIconText();
              ds = m_ISMLoginInfo.ISMServer.GetStockIssueDisplayData(AMode, ADataWhere);
              if (ds != null)
              {
                  if (ds.Tables[0].Rows.Count > 0)
                  {
                      dr = ds.Tables[0].Rows[0];
                      lblShortName.Text = dr[ISMStockCatalogue.StockShortName].ToString();
                      zStockTracInd = dr[ISMStockCatalogue.StockTrakingInd].ToString();
                      if (zStockTracInd == "E" || zStockTracInd == "S")
                          lblSerialAndEquipNo.Text = dr[ISMStock.StockSerialEquipNo].ToString();
                      else
                          lblSerialAndEquipNo.Text = "";
                      if (dr[ISMStockCatalogue.StockBatchLotMgtInd].ToString().ToUpper() == "Y")
                          lblBatchAndLotNo.Text = dr[ISMStock.StockBatchLotNo].ToString();
                      else
                          lblBatchAndLotNo.Text = "";
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
                  }
                  else
                  {
                      lblShortName.Text = "";
                      lblSerialAndEquipNo.Text = "";
                      lblBatchAndLotNo.Text = "";
                      lblVolumetric1.Text = "";
                      lblVolumetric2.Text = "";
                  }
              }
          }
          catch (Exception ex)
          {
              MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Trace", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

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
              lookUpEditLocationUID.EditValue = null;
               
              txtStockCode.Text = "";  
              dtTraceDate.EditValue = null;
              lblTraceBy.Text = "";
              lblShortName.Text = "";
              lblVolumetric1.Text = "";
              lblVolumetric2.Text = "";
              lblSerialAndEquipNo.Text = "";
              lblBatchAndLotNo.Text = "";
              lblERPDistrict.Text = "";
              lblERPWHS.Text = "";
              lblERPGrid.Text = "";
              lblERPBin.Text = "";
              lblShortName.Text = "";
              lblCategory.Text = "";  
              lookUpEditOperatorID.Focus();
              ClearErrorIconText();
              LoadTraceMetaData();
              txtStatusMsg.Text = "";  
              lblCategory.Text = "";  
      }
          catch (Exception ex)
          {
              MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Trace", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
      }
      #endregion

      #region "Validation"
      private void ClearErrorIconText()
      {
          try
          {
              dxErrorProvider.SetError(lookUpEditOperatorID, null);
              dxErrorProvider.SetError(lookUpEditItemUID, null);
              dxErrorProvider.SetError(lookUpEditLocationUID, null);
              dxErrorProvider.SetError(txtStockCode, null);
              
              dxErrorProvider.SetError(dtTraceDate, null);
          }
          catch (Exception ex)
          {
              MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Trace", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
      }
      private bool Validation()
      {
          bool zResult = false;
          bool zValidationFail = true;

          try
          {
              ClearErrorIconText();
              if (TraceType == (int)ISM.TraceType.ItemTrace)
              {
                  if (lookUpEditItemUID.EditValue == null)
                  {
                      dxErrorProvider.SetError(lookUpEditItemUID, "Select a Item UID");
                      lookUpEditItemUID.Focus();
                      zValidationFail = false;
                  }
                   
                   
                   
                   
                   
                   
                   
              }
              if (lookUpEditLocationUID.EditValue == null)
              {
                  dxErrorProvider.SetError(lookUpEditLocationUID, "Select a Location UID");
                  lookUpEditLocationUID.Focus();
                  zValidationFail = false;
              }
              if (dtTraceDate.EditValue == null)
              {
                  dxErrorProvider.SetError(dtTraceDate, "Select Trace Date");
                  dtTraceDate.Focus();
                  zValidationFail = false;
              }
              else if (dtTraceDate.Text != "")
              {
                  if (m_ISMLoginInfo.LogonID == lookUpEditOperatorID.Text)
                  {
                      if (DateTime.Parse(dtTraceDate.EditValue.ToString()) != DateTime.Today)
                      {
                          dxErrorProvider.SetError(dtTraceDate, "Enter Current Date");
                          dtTraceDate.Focus();
                          zValidationFail = false;
                      }
                  }
                  else if (DateTime.Parse(dtTraceDate.EditValue.ToString()) < DateTime.Today)
                  {
                      dxErrorProvider.SetError(dtTraceDate, "Enter Current Date or Future Date");
                      dtTraceDate.Focus();
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
              MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              return false;
          }
          return zResult;
      }
      #endregion

      #region "Save Button"
      private void btnSave_Click(object sender, EventArgs e)
      {
        if (Validation())
        {
            try
            {
                if (m_ISMLoginInfo.LogonID != lookUpEditOperatorID.Text)
                {
                    ISMTask.StructTask zTask = new ISMTask.StructTask();
                    string zMessage = "";
                    string zResultMsg = "";
                    string zOperationID = "";
                    if (TraceType == (int)ISM.TraceType.ItemTrace)
                    {
                        zMessage = String.Format("Do you want to create a Trace Task for Item UID {0} and Location UID {1}?", lookUpEditItemUID.Text, lookUpEditLocationUID.Text);
                        zResultMsg = String.Format("Trace Task has been created for Item UID {0} and Location UID", lookUpEditItemUID.Text, lookUpEditLocationUID.Text);
                         
                        zOperationID = "13";  
                        zTask.ItemID = lookUpEditItemUID.EditValue.ToString();
                        zTask.StockCode = txtStockCode.Text;  
                        
                    }
                    else
                    {
                        zMessage = String.Format("Do you want to create a Trace Task for Location UID {0}?", lookUpEditLocationUID.Text);
                        zResultMsg = String.Format("Trace Task has been created for Location UID {0}", lookUpEditLocationUID.Text);
                         
                        zOperationID = "14";  
                        zTask.ItemID = "0";
                        zTask.StockCode = "";
                    }
                    DialogResult zReply = MessageBox.Show(zMessage, "Trace", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (zReply == DialogResult.Yes)
                    {
                       
                        zTask.Type = "1";
                        zTask.Status = "Assigned"; 
                        zTask.StockQty = "0";
                        zTask.UserID = m_OperatorID;
                        
                        zTask.SourceID = lookUpEditLocationUID.EditValue.ToString();
                        zTask.DestinationID = "0";
                        zTask.OperationID = zOperationID;
                        zTask.StatusCode = "1"; 
                        zTask.CreateUserID = m_ISMLoginInfo.LogonID;
                        zTask.CreateDateTime = String.Format(dtTraceDate.EditValue.ToString().Substring(0, 10) + " " + DateTime.Now.ToLongTimeString(), m_ISMLoginInfo.Params.DateTimeFormat);  
                        
                        if (m_ISMLoginInfo.ISMServer.CreateTraceTakeTask(zTask,lblCategory.Text.Trim()))
                        {
                            
                            btnClear.PerformClick();
                            txtStatusMsg.Text = zResultMsg;  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stocktake", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }      

        }
      }
      #endregion

      #region "Exception "
      private void btnException_Click(object sender, EventArgs e)
      {
          try
          {
              long zItemUID = 0;
              long zLocationUID = 0;
              long zSealUID = 0;
              string zStockCode = "";
               
               
               
              if (lookUpEditItemUID.EditValue == null && lookUpEditLocationUID.EditValue == null && txtStockCode.Text.Trim() == "")
              {
                  MessageBox.Show("Select at least any one of the key field e.g. Item UID or Location UID or Stock Code", "Trace", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  return;
              }
              else
              {
                  if (lookUpEditItemUID.EditValue != null)
                      zItemUID = long.Parse(lookUpEditItemUID.EditValue.ToString());
                  if (lookUpEditLocationUID.EditValue != null)
                      zLocationUID = long.Parse(lookUpEditLocationUID.EditValue.ToString());
                   
                   
                  if (txtStockCode.Text.Trim() != "")
                      zStockCode = txtStockCode.Text.Trim();

              }
              ISM.Forms.FrmException frmSTException = new ISM.Forms.FrmException(m_ISMLoginInfo)
              {
                  LocationUID = zLocationUID,
                  ItemUID = zItemUID,
                  StockCode = zStockCode,
                  SealUID = zSealUID,
                  JournalType = "STR",
                  MsgBoxCaption = "Trace",
                  InvenCatCode = lblCategory.Text.Trim()  
              }; 
              frmSTException.Text = "Trace Exception";
              frmSTException.ShowDialog();
          }
          catch (Exception ex)
          {
              MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
      }
      #endregion

  }
}
