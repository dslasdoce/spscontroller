
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ISMDAL.TableColumnName;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Collections;

namespace ISM.Modules
{
    public partial class LocationSearch : ISMBaseWorkSpace
    {
        #region "Private Variable Declaration"
        DataSet m_dataset = null;
        DataSet m_LocTreeDataSet = null;  
        DataSet m_SearchLocDataSet = null;   

        protected ArrayList selection;

        # endregion
        
        public LocationSearch(ISMLoginInfo AISMLoginInfo)
            : base(AISMLoginInfo)
        {
            InitializeComponent();
        }

        #region "Form Load Event"
        private void LocationSearch_Load(object sender, EventArgs e)
        {
            txtItemUID.Properties.Mask.EditMask = m_ISMLoginInfo.Params.ItemPrefix + "\\d{0,12}";
            txtItemUID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtLocationUID.Properties.Mask.EditMask = m_ISMLoginInfo.Params.LocPrefix + "\\d{0,12}";
            txtLocationUID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            SetGridCaption();
            LoadCategroyMetaData();
            LoadLocationTree();
             
            selection = new ArrayList();
        }

        # endregion

        #region "Data Initiation"
        private void LoadLocationTree()
        {
            try
            {
                long zRootLocUID = 0;
                 if(m_ISMLoginInfo.Params.RootLocationUID.Trim().Length > 0)
                     zRootLocUID = long.Parse(m_ISMLoginInfo.Params.RootLocationUID);
                 
                DataSet zDataSet = m_ISMLoginInfo.ISMServer.GetLocationTree(zRootLocUID);  
                m_LocTreeDataSet = zDataSet;  
                tvLocation.KeyFieldName = ISMLocationRelationship.ChildID;
                tvLocation.ParentFieldName = ISMLocationRelationship.ParentID;
                tvLocation.DataSource = zDataSet.Tables[0].DefaultView;
                tvLocation.ExpandAll();
                tvLocation.GetSelectImage += new GetSelectImageEventHandler(OnTreeListGetSelectImage);
                tvLocation.SelectImageList = imgSmallImageCollection;
                grdViewStock.OptionsView.ColumnAutoWidth = false;
                grdViewStock.BestFitColumns();
                SetGridCaption();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Loading Location Tree\n" + ex.Message + "\nPlease Contact System Administrator", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void SetGridCaption()
        {
            try
            {
                ColumnView ColView = grdStockData.MainView as ColumnView;

                
                string[] fieldNames = new string[] { "Stock Code", "Item UID", "Stock Name", "Category", "Qty On Hand", "Serial Equip No", "Batch Lot No" };

                DevExpress.XtraGrid.Columns.GridColumn column;
                ColView.Columns.Clear();
                for (int i = 0; i < fieldNames.Length; i++)
                {
                    column = ColView.Columns.AddField(fieldNames[i]);
                    column.VisibleIndex = i;
                }
                grdViewStock.Columns[0].Caption = "Stock Code";
                grdViewStock.Columns[0].Width = 110;
                grdViewStock.Columns[1].Caption = "Item UID"; 
                grdViewStock.Columns[1].Width = 120;
                grdViewStock.Columns[2].Caption = "Stock Name";
                grdViewStock.Columns[2].Width = 280;
                grdViewStock.Columns[3].Caption = "Category";  
                grdViewStock.Columns[3].Width = 90;

                grdViewStock.Columns[4].Caption = "Qty On Hand";
                grdViewStock.Columns[4].Width = 90;
                grdViewStock.Columns[5].Caption = "Serial Equip No";
                grdViewStock.Columns[5].Width = 140;
                grdViewStock.Columns[6].Caption = "Batch Lot No";
                grdViewStock.Columns[6].Width = 170;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stock Location Search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
         

        private void LoadCategroyMetaData()
        {
            try
            {
                lookUpEditCategory.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMCategory.CategoryCode, 100,"Code"),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ISMCategory.CategoryDesc, 350, "Description")});

                 
                lookUpEditCategory.Properties.DisplayMember = ISMCategory.CategoryCode;
                lookUpEditCategory.Properties.ValueMember = ISMCategory.CategoryCode;
                 
                 
                DataSet zPriorityCategoryCode = null;
                zPriorityCategoryCode = m_ISMLoginInfo.ISMServer.GetStockCategory();
                if (zPriorityCategoryCode.Tables[1].Rows.Count > 0)
                {
                    zPriorityCategoryCode.Tables[0].Rows.Add("-----", "------------------------------------------");
                    zPriorityCategoryCode.Tables[0].AcceptChanges();
                }
                foreach (DataRow dr in zPriorityCategoryCode.Tables[1].Rows)
                {
                    zPriorityCategoryCode.Tables[0].Rows.Add(dr.ItemArray);
                    zPriorityCategoryCode.Tables[0].AcceptChanges();
                }
                lookUpEditCategory.Properties.DataSource = zPriorityCategoryCode.Tables[0].DefaultView;


            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Stock Hierarchical Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        # endregion

        #region "Category Edit List"
        private void lookUpEditCategory_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dxErrorProvider.SetError(lookUpEditCategory, null);
                if (lookUpEditCategory.EditValue != null)  
                {
                    if (lookUpEditCategory.EditValue.ToString() == "-----")
                    {
                        dxErrorProvider.SetError(lookUpEditCategory, "Invalid Category");
                    }
                }
            }
            catch
            {
                MessageBox.Show("System Error. Contact System Administrator", "Stock Hierarchical Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        #endregion 

        #region "Tree Control Event"
        private void tvLocation_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                object zParentId = tvLocation.FocusedNode[tvLocation.KeyFieldName]; 
                DataSet zDataSet = m_ISMLoginInfo.ISMServer.LocationGetStock(Convert.ToUInt32(zParentId)); 
                grdStockData.DataSource = zDataSet.Tables[0].DefaultView;
                GetFocusOnSelectedValue();


            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stock Location Search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void OnTreeListGetSelectImage(object sender, GetSelectImageEventArgs e)
        {
            try
            {
                if (m_dataset == null)
                {
                    e.NodeImageIndex = 0;
                    return;
                }
                foreach (DataRow drow in m_dataset.Tables[0].Rows)
                {
                    
                    if ("8" + drow["FK_LOCATION_UID"].ToString().PadLeft(12, '0') == e.Node["LocationUID"].ToString())  
                        e.NodeImageIndex = 8;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Location Search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        # endregion

        #region "Button Click Event"

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                 
                if (txtStockCode.Text.Trim() == "" && txtShortName.Text.Trim() == "" 
                    && txtSerialNo.Text.Trim() == "" &&
                    txtItemUID.Text.Trim() == "" && txtLocationUID.Text.Trim() == "" &&
                    txtBatchLotNo.Text.Trim() == "" && lookUpEditCategory.EditValue == null)
                {
                    MessageBox.Show("Input at least any one of the field value", "Stock Location Search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtStockCode.Focus();
                    return;
                }
                else
                {
                    if (Validaion())
                    {
                        m_dataset = null;
                        string zLocationUID = "";
                        string zItemUID = "";
                        string zCategoryCode = "";
                        if (txtItemUID.Text.Trim() != "" && txtItemUID.Text.Trim().Length == 13)
                        {
                            zItemUID = long.Parse(txtItemUID.Text.Substring(1, 12)).ToString();  
                        }
                        if (txtLocationUID.Text.Trim() != "" && txtLocationUID.Text.Trim().Length == 13)
                        {
                            zLocationUID = long.Parse(txtLocationUID.Text.Substring(1, 12)).ToString(); 
                        }
                        if(lookUpEditCategory.EditValue != null)
                            zCategoryCode = lookUpEditCategory.EditValue.ToString().Trim();
                         
                        m_dataset = m_ISMLoginInfo.ISMServer.GetStockLocationSearch(txtStockCode.Text.Trim(), 
                                                                                    txtShortName.Text.Trim(),
                                                                                    txtSerialNo.Text.Trim(),
                                                                                    zItemUID,zLocationUID,
                                                                                    txtBatchLotNo.Text.Trim(),
                                                                                    zCategoryCode);

                        CreateSortedStockLocation();  

                        if (m_SearchLocDataSet.Tables[0].Rows.Count > 0)
                        {
                            DatabindingSource.DataSource = m_SearchLocDataSet.Tables[0].DefaultView;
                            txtDataBindLocUID.DataBindings.Clear();
                            txtDataBindLocUID.DataBindings.Add("EditValue", dataNavigator.BindingSource, ISMStock.StockLocationID);
                            bindingNavigatorMoveFirstItem.PerformClick();  
                            GetFocusOnSelectedValue();
                            SetFocusOnLocationTree();
                        }
                        else
                        {
                            txtDataBindLocUID.DataBindings.Clear();
                            DatabindingSource.DataSource = null;
                        }
                                                                                   
                        tvLocation.Refresh();  
                        if (m_dataset != null)
                        {
                            if (m_dataset.Tables[0].Rows.Count > 0)
                            {
                                if(m_dataset.Tables[0].Rows.Count == 1)
                                    txtStatusMsg.Text = string.Format("Stock Data found in {0} Location", m_dataset.Tables[0].Rows.Count);
                                else
                                    txtStatusMsg.Text = string.Format("Stock Data found in {0} Locations", m_dataset.Tables[0].Rows.Count);
                                foreach (DataRow drow in m_dataset.Tables[0].Rows)
                                {
                                     
                                    TreeListNode Trnode = tvLocation.FindNodeByFieldValue("LocationUID",  drow["FK_LOCATION_UID"].ToString());  
                                    tvLocation.SetFocusedNode(Trnode);
                                    break;
                                }
                            }
                            else
                            {
                                txtStatusMsg.Text = "Stock Data does not exist for the above selection criteria";
                                tvLocation.SetFocusedNode(tvLocation.Nodes.FirstNode);
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error: {0}\nContact System Administrator", ex.Message), "Stock Location Search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtStockCode.EditValue = null;
                txtShortName.EditValue = null;
                txtSerialNo.EditValue = null;
                txtItemUID.EditValue = null;
                txtLocationUID.EditValue = null;
                txtStatusMsg.EditValue = null;
                txtBatchLotNo.EditValue = null;  
                lookUpEditCategory.EditValue = null;  
                m_dataset = null;
                if(m_SearchLocDataSet != null)
                    m_SearchLocDataSet.Clear();  
                txtDataBindLocUID.DataBindings.Clear();  
                DatabindingSource.DataSource = null;
                tvLocation.SetFocusedNode(tvLocation.Nodes.FirstNode);
                tvLocation.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Location Search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void btnSearcher_Click(object sender, EventArgs e)
        {
            try
            {
                ISM.Forms.FrmStockCodeSearcher zStockCodeSearcher = new ISM.Forms.FrmStockCodeSearcher(m_ISMLoginInfo);
                zStockCodeSearcher.InputStockCode = txtStockCode.Text.Trim();
                zStockCodeSearcher.ShowDialog();
                txtStockCode.Text = zStockCodeSearcher.StockCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Location Search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        # endregion

        #region "Validation"
        private bool Validaion()
        {
            bool zResult = false;
            bool zValidationFail = true;
            try
            {
                if (txtLocationUID.Text.Trim() != "" && txtLocationUID.Text.Trim().Length != 13)
                {
                    dxErrorProvider.SetError(txtLocationUID, "Invalid Location UID");
                    txtLocationUID.SelectAll();
                    txtLocationUID.Focus();
                    zValidationFail = false;
                }
                if (txtItemUID.Text.Trim() != "" && txtItemUID.Text.Trim().Length != 13)
                {
                    dxErrorProvider.SetError(txtItemUID, "Invalid Item UID");
                    txtItemUID.SelectAll();
                    txtItemUID.Focus();
                    zValidationFail = false;
                }
                if (lookUpEditCategory.EditValue != null)  
                {
                    if (lookUpEditCategory.EditValue.ToString() == "-----")
                    {
                        dxErrorProvider.SetError(lookUpEditCategory, "Invalid Category");
                        lookUpEditCategory.Focus();
                        zValidationFail = false;
                    }
                }
                if (zValidationFail)
                    zResult = zValidationFail;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Location Search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return zResult;
        }
        #endregion 

        #region "TextBox Key Press Event"
        private void txtItemUID_EditValueChanged(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(txtItemUID, null);
        }

        private void txtLocationUID_EditValueChanged(object sender, EventArgs e)
        {
            dxErrorProvider.SetError(txtLocationUID, null);
        }
   
        private void txtStockCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearcher.PerformClick();
            }
        }
        #endregion

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            SetFocusOnLocationTree();
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            SetFocusOnLocationTree();
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            SetFocusOnLocationTree();
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            SetFocusOnLocationTree();
        }
        void SetFocusOnLocationTree()
        {
             
            TreeListNode Trnode = tvLocation.FindNodeByFieldValue("LocationUID", "8" + txtDataBindLocUID.Text.Trim().PadLeft(12, '0'));  
            tvLocation.SetFocusedNode(Trnode);

        }
        void GetFocusOnSelectedValue()
        {
            try
            {
                int rowHandle = 0;
                ColumnView View = grdStockData.MainView as ColumnView;
                GridColumn[] cols;
                if (txtItemUID.Text.Trim() != "")
                {
                    cols = new GridColumn[] { grdViewStock.Columns[1] };
                    object[] values = new object[] { txtItemUID.Text.Trim()};
                    rowHandle = LocateRowByValues(View, cols, values, 0);
                    if (rowHandle > 0)
                        grdViewStock.FocusedRowHandle = rowHandle;
                }
                else if(txtStockCode.Text.Trim() != "")
                {
                    cols = new GridColumn[] { grdViewStock.Columns[0] };
                     
                    object[] values = new object[] { txtStockCode.Text.Trim().ToUpper() };
                    rowHandle = LocateRowByValues(View, cols, values, 0);
                    if (rowHandle > 0)
                        grdViewStock.FocusedRowHandle = rowHandle;

                }
                else if (txtShortName.Text.Trim() != "")
                {
                    cols = new GridColumn[] { grdViewStock.Columns[2] };
                    object[] values = new object[] { txtShortName.Text.Trim() };
                    rowHandle = LocateRowByValues(View, cols, values, 0);
                    if (rowHandle > 0)
                        grdViewStock.FocusedRowHandle = rowHandle;

                }
                else if (txtSerialNo.Text.Trim() != "")
                {
                    cols = new GridColumn[] { grdViewStock.Columns[4] };
                    object[] values = new object[] { txtSerialNo.Text.Trim() };
                    rowHandle = LocateRowByValues(View, cols, values, 0);
                    if (rowHandle > 0)
                        grdViewStock.FocusedRowHandle = rowHandle;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Location Search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        public int LocateRowByValues(ColumnView view, GridColumn[] columns,
                                             object[] values, int startRowHandle)
        {
            try
            {
                 
                if (columns.Length != values.Length)
                    return DevExpress.XtraGrid.GridControl.InvalidRowHandle;
                 
                int dataRowCount;
                if (view is CardView)
                    dataRowCount = (view as CardView).RowCount;
                else
                    dataRowCount = (view as GridView).DataRowCount;
                 
                bool match;
                object currValue = null;
                for (int currentRowHandle = startRowHandle; currentRowHandle < dataRowCount;
                currentRowHandle++)
                {
                    match = true;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        currValue = view.GetRowCellValue(currentRowHandle, columns[i]);
                        if (!currValue.Equals(values[i]))
                            match = false;
                    }
                    if (match)
                        return currentRowHandle;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Location Search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

             
            return DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }

        private void CreateSortedStockLocation()
        {
            try
            {
                 
                System.Data.DataTable ztable = new DataTable("LOC");
                 
                DataColumn Tablecolumn;
                 
                Tablecolumn = new DataColumn();
                Tablecolumn.DataType = System.Type.GetType("System.Decimal");
                Tablecolumn.ColumnName = "FK_LOCATION_UID";
                Tablecolumn.AutoIncrement = false;
                Tablecolumn.Caption = "FK_LOCATION_UID";
                Tablecolumn.ReadOnly = false;
                Tablecolumn.Unique = false;

                 
                ztable.Columns.Add(Tablecolumn);

                 
                DataColumn[] zPrimaryKeyColumns = new DataColumn[1];
                zPrimaryKeyColumns[0] = ztable.Columns["FK_LOCATION_UID"];
                ztable.PrimaryKey = zPrimaryKeyColumns;

                 
                m_SearchLocDataSet = new DataSet();
                 
                m_SearchLocDataSet.Tables.Add(ztable);
                AddToNewDataSet();

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public void AddToNewDataSet()
        {
            try
            {
                foreach (DataRow Locrow in m_LocTreeDataSet.Tables[0].Rows)  
                {
                    string filterExp = "FK_LOCATION_UID = '" + Locrow["FK_LOCATION_UID"] + "'";
                    DataRow[]  zDataRow = m_dataset.Tables[0].Select(filterExp);
                    if (zDataRow != null)
                    {
                        foreach (DataRow dr in zDataRow)
                        {
                            CreateLocationTreeTable(dr);
                        }

                    }
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Stock Location Search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }
        private void CreateLocationTreeTable(DataRow ANewRow)
        {
            try
            {
                DataRow row;

                row = m_SearchLocDataSet.Tables[0].NewRow();
                row["FK_LOCATION_UID"] = ANewRow["FK_LOCATION_UID"];
                m_SearchLocDataSet.Tables[0].Rows.Add(row);

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("System Error:  {0}\nContact System Administrator", ex.Message), "Portal Location", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        


    }


}
