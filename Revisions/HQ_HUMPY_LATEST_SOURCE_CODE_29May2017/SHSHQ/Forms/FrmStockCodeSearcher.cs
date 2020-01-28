#region "History"
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
#endregion

#region "Namespace"
using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ISMDAL.TableColumnName;
#endregion

namespace ISM.Forms
{
  public partial class FrmStockCodeSearcher : DevExpress.XtraEditors.XtraForm
  {
    private int m_SearchType = 0;
    private int m_nPageNumber = 1;
    private int m_TablePageCount = 0;
    private int m_RecCount = 50;  
    private string m_StockCode = "";
    private string m_StockCodeDesc = "";
    private string m_TrackingInd = "";
    private bool m_SelStockCode = false;
    private string m_InputStockCode = "";  
    private ISMLoginInfo m_ISMLoginInfo;

     
    private int PageNumber  
    {
      get { return m_nPageNumber; }
      set { m_nPageNumber = value; }
    }

    public int TablePageCount
    {
      get { return m_TablePageCount; }
      set { m_TablePageCount = value; }
    }

    public int RecordCount
    {
      get { return m_RecCount; }
      set { m_RecCount = value; }
    }

    public int SearchType
    {
      get { return m_SearchType; }
      set { m_SearchType = value; }
    }

    public string StockCode
    {
      get { return m_StockCode; }
      set { m_StockCode = value; }
    }

    public string StockCodeDesc
    {
      get { return m_StockCodeDesc; }
      set { m_StockCodeDesc = value; }
    }

    public string TrackingIndicator
    {
      get { return m_TrackingInd; }
      set { m_TrackingInd = value; }
    }

    public bool SelStockCode
    {
      get { return m_SelStockCode; }
      set { m_SelStockCode = value; }
    }
     
    public string InputStockCode
    {
      get { return m_InputStockCode;}
      set { m_InputStockCode = value;}
    }

    public FrmStockCodeSearcher(ISMLoginInfo AISMLoginInfo)
    {
      InitializeComponent();
      m_ISMLoginInfo = AISMLoginInfo;
    }

    private void FrmStockCodeSearcher_Load(object sender, EventArgs e)
    {
       
      LookAndFeel.SetStyle(DevExpress.LookAndFeel.LookAndFeelStyle.Style3D, true, false);
       
      LookAndFeel.SetSkinStyle(m_ISMLoginInfo.Params.AppSkinColour);  
       
      txtSearch.Text = InputStockCode;

       
      btnSelect.Enabled = false; 
      btnNext.Enabled = false;
      btnPrevious.Enabled = false;
       
      try
      {
        PageNumber = 1;
        rbStock.SelectedIndex = 0;
        ColumnView ColView = gridStockCode.MainView as ColumnView;

        string[] fieldNames = new string[] { ISMStockCatalogue.StockCode, ISMStockCatalogue.StockShortName };
        DevExpress.XtraGrid.Columns.GridColumn column;
        ColView.Columns.Clear();
        for (int i = 0; i < fieldNames.Length; i++)
        {
          column = ColView.Columns.AddField(fieldNames[i]);
          column.VisibleIndex = i;
        }
        gridView.Columns[0].Caption = "Stock Code";
        gridView.Columns[1].Caption = "Stock Description";  
        gridView.BestFitColumns();  
         

         
        GetStockCode();
         
        txtSearch.Focus();
      }

      catch (Exception ex)  
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

      /* Out DM 11-AUG-10
      catch
      {
        MessageBox.Show("System Error. Contact System Administrator", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
       */
    }

    private void radioGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
      SearchType = (rbStock.EditValue.ToString() == "CODE") ? 0 : 1;  

      /*  Out DM 11-AUG-10
      if (radioGroup.EditValue.ToString() == "CODE")
        SearchType = 0;
      else
        SearchType = 1;
      */
       
      PageNumber = 1;
    }

    private void btnPrevious_Click(object sender, EventArgs e)
    {
       
      PageNumber = (PageNumber > 1) ? PageNumber -= 1 : 1;  
      GetStockCode();  
      /* DM out 11-AUG-10
      if (PageNumber > 1)
        PageNumber = PageNumber - 1;
      else
        PageNumber = 1;
      GetStockCode(PageNumber, RecordCount, SearchType, txtSearch.Text.Trim(), m_ISMLoginInfo.LogonID);
       */
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
       
      PageNumber = (PageNumber >= TablePageCount) ? TablePageCount : PageNumber += 1;  
      /* DM Out 11-AUG-10
      if (PageNumber > TablePageCount)
        PageNumber = TablePageCount;
      else
        PageNumber = PageNumber + 1;
       */
       
      GetStockCode();  
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
      PageNumber = 1;  
       
      GetStockCode();  
    }


    private void GetStockCode(int APage, int ARecordCount, int ASearchType, string ASearchCriteria, string AUserID)
    {
      try
      {
        if (ASearchCriteria.Trim() != "")
        {
           
          int zTablePageCount = 0;
          gridStockCode.DataSource = m_ISMLoginInfo.ISMServer.GetSearcherStockCode(APage, ARecordCount, ref zTablePageCount, ASearchType, ASearchCriteria).Tables[0].DefaultView;
          TablePageCount = zTablePageCount;
           

          /* Out DM 11-AUG-10
          int nTablePageCount = 0;
          DataSet ds = null;
          ds = m_ISMLoginInfo.ISMServer.GetSearcherStockCode(APage, ARecordCount, ref nTablePageCount, ASearchType, ASearchCriteria);
          gridStockCode.DataSource = ds.Tables[0].DefaultView;
          TablePageCount = nTablePageCount;
           */
        }
      }
      catch (Exception ex)  
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

      /* Out DM 11-AUG-10
      catch
      {
        MessageBox.Show("System Error. Contact System Administrator", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
       */
    }

    private void GetStockCode()  
    {
      try
      {
        if (txtSearch.Text.Trim() != "")
        {
          Cursor = Cursors.WaitCursor;
          int zTablePageCount = 0;
          gridStockCode.DataSource = m_ISMLoginInfo.ISMServer.GetSearcherStockCode(PageNumber, RecordCount, ref zTablePageCount, SearchType, txtSearch.Text).Tables[0].DefaultView;
          gridView.BestFitColumns();
          TablePageCount = zTablePageCount;
           
          if (gridView.DataRowCount > 0)
          {
             
            if (PageNumber == TablePageCount)
              btnNext.Enabled = false;
            else
              btnNext.Enabled = true;
            if ((PageNumber == 1))
              btnPrevious.Enabled = false;
            else
              btnPrevious.Enabled = true;
             
            btnSelect.Enabled = true;
          }
          else
          {
            btnSelect.Enabled = false;
            btnPrevious.Enabled = false;
            btnSelect.Enabled = false;
          }
           
        }
      }
      catch (Exception ex)  
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      finally
      {
        Cursor = Cursors.Default;
      }
    }

    private void GetSelectedRow()
    {
      try
      {
        GridView zView = gridView;
        if (zView.GetSelectedRows().Length > 0)
        {
          DataRow zDataRow = zView.GetDataRow(zView.GetSelectedRows()[0]);   
          StockCode = zDataRow[ISMStockCatalogue.StockCode].ToString();
          StockCodeDesc = zDataRow[ISMStockCatalogue.StockShortName].ToString();
          TrackingIndicator = zDataRow[ISMStockCatalogue.StockTrakingInd].ToString();
          SelStockCode = true;
        }

        /* DM Out 11-AUG-10
         
        GridView view = gridView;
        int[] selected = view.GetSelectedRows();
        DataRow rows = null;
        if (selected.Length > 0)
        {
          rows = view.GetDataRow(selected[0]);   
          StockCode = rows[ISMStockCatalogue.StockCode].ToString();
          StockCodeDesc = rows[ISMStockCatalogue.StockShortName].ToString();
          TrackingIndicator = rows[ISMStockCatalogue.StockTrakingInd].ToString();
          SelStockCode = true;
        }
        */
        this.Close();
      }
      catch (Exception ex)  
      {
        MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      /* Out DM 11-AUG-10
      catch
      {
        MessageBox.Show("System Error. Contact System Administrator", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
       */
    }

    private void gridView_DoubleClick(object sender, EventArgs e)
    {
      GetSelectedRow();
    }

    private void btnSelect_Click(object sender, EventArgs e)
    {
      GetSelectedRow();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void txtSearch_EditValueChanged(object sender, EventArgs e)
    {
      btnSearch.Enabled = (txtSearch.Text.Trim() != "") ? true : false;
    }

    private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == (char)13)
      {
        btnSearch.PerformClick();
      }
    }

    private void FrmStockCodeSearcher_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (!SelStockCode)
      {
         
         
        StockCode = InputStockCode;
      }
    }
  }
}