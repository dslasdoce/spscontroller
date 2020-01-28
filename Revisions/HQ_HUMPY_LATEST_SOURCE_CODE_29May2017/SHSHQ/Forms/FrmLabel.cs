 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

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
  public partial class FrmLabel : DevExpress.XtraEditors.XtraForm
  {
    #region "Priave Variable "
    private ISMLoginInfo m_ISMLoginInfo;
    private string m_LabelUID = "";
    private bool m_ValidLabelUID = false;
    private long m_UserID = 0;
    #endregion

    #region "Property"
    public string LabelUID
    {
      get { return m_LabelUID; }
      set { m_LabelUID = value; }
    }
    public long UserID
    {
      get { return m_UserID; }
      set { m_UserID = value; }
    }
    #endregion

    public FrmLabel(ISMLoginInfo AISMLoginInfo)
    {
      InitializeComponent();
      m_ISMLoginInfo = AISMLoginInfo;
    }

    private void FrmLabel_Load(object sender, EventArgs e)
    {
      SetLookUpEditCaption();
      LoadLabelMetaData();
      lblCategory.Text = "Item Label";
      txtItemUID.Properties.Mask.EditMask = m_ISMLoginInfo.Params.ItemPrefix + "\\d{0,12}";
      txtItemUID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
    }

    private void SetLookUpEditCaption()
    {
      try
      {
        lookUpEditType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLabelType.LabelTypeID, "ID",10,DevExpress.Utils.FormatType.None,"",false,DevExpress.Utils.HorzAlignment.Center),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLabelType.LabelTypeCode, 90,"Label Type"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMLabelType.LabelTypeDesc, 120, "Description")});

        lookUpEditType.Properties.DisplayMember = ISMLabelType.LabelTypeDesc;
        lookUpEditType.Properties.ValueMember = ISMLabelType.LabelTypeID;

      }
      catch
      {
        MessageBox.Show("System Error. Contact System Administrator", "Label", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void LoadLabelMetaData()
    {
      try
      {
        long zItemUID = 0;
        DataSet ds = m_ISMLoginInfo.ISMServer.GetLabelMetaData(ref zItemUID);
        if (ds != null)
        {
          lblLastUID.Text = m_ISMLoginInfo.Params.ItemPrefix + zItemUID.ToString().PadLeft(12, '0');
          lookUpEditType.Properties.DataSource = ds.Tables[ISMLabelType.TableName].DefaultView;
        }
      }
      catch
      {
        MessageBox.Show("System Error. Contact System Administrator", "Label", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void txtItemUID_Leave(object sender, EventArgs e)
    {
      try
      {
        dxErrorProvider.SetError(txtItemUID, null);
        if (txtItemUID.Text.Trim() == "")
        {
          dxErrorProvider.SetError(txtItemUID, "Enter Label UID");
          txtItemUID.Focus();
           
        }
        else if (txtItemUID.Text.Length != 13)
        {
          dxErrorProvider.SetError(txtItemUID, "Enter 13 digit Label UID");
          txtItemUID.Focus();
           
        }
        else if (txtItemUID.Text.Trim() == "1000000000000")
        {
          dxErrorProvider.SetError(txtItemUID, "Invalid Label UID");
          txtItemUID.Focus();
           
        }
        else
        {
          int zMode = 2; 
           
          long zItemUID = long.Parse(txtItemUID.Text.Substring(0, 13));
          long zLocUID = 0;
          if (m_ISMLoginInfo.ISMServer.PDTStockReceiveValidation(zMode, zItemUID, zLocUID))
          {
            dxErrorProvider.SetError(txtItemUID, "Item Label UID is used already or Invalid");
            txtItemUID.Focus();
          }
          else
            m_ValidLabelUID = true;
        }
      }
      catch
      {
        MessageBox.Show("System Error. Contact System Administrator", "Label", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void ClearErrorIconText()
    {
      dxErrorProvider.SetError(txtItemUID, null);
      dxErrorProvider.SetError(lookUpEditType, null);
    }

    private bool Validation()
    {
      bool zResult = false;
      bool zValidationFail = true;
      ClearErrorIconText();

      if (txtItemUID.Text == "")
      {
        dxErrorProvider.SetError(txtItemUID, "Enter Label UID");
        txtItemUID.Focus();
        zValidationFail = false;
      }

      if (txtItemUID.Text.Trim().Length != 13)
      {
        dxErrorProvider.SetError(txtItemUID, "Enter 13 digit Label UID");
        txtItemUID.Focus();
        zValidationFail = false;
      }

      if (!m_ValidLabelUID)
      {
        dxErrorProvider.SetError(txtItemUID, "Item Label UID is used already or Invalid");
        txtItemUID.Focus();
        zValidationFail = false;
      }

      if (lookUpEditType.Text == "")
      {
        dxErrorProvider.SetError(lookUpEditType, "Select a Label Type");
        lookUpEditType.Focus();
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
        if (Validation())
        {
          long zItemUID = long.Parse(txtItemUID.Text.Substring(1, 12));
          int zLabelType = int.Parse(lookUpEditType.EditValue.ToString());
          int zMode = 0;
          if (m_ISMLoginInfo.ISMServer.CreateLabel(zMode, zItemUID, zLabelType, m_ISMLoginInfo.LogonID, UserID))
           
          {
            MessageBox.Show("Label Created for the Item UID " + txtItemUID.Text.Trim(), "Label", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LabelUID = txtItemUID.Text.Trim();
            Close();
          }
        }
      }
      catch
      {
        MessageBox.Show("System Error. Contact System Administrator", "Label", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      ClearErrorIconText();
      txtItemUID.EditValue = null;
      lookUpEditType.EditValue = null;
      txtItemUID.Focus();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Label is not created. Do you want to close?", "Label", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
        Close();
    }
  }
}