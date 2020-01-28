namespace ISM.Forms
{
  partial class FrmStockCodeSearcher
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStockCodeSearcher));
        this.gcStockCode = new DevExpress.XtraEditors.GroupControl();
        this.gridStockCode = new DevExpress.XtraGrid.GridControl();
        this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
        this.btnPrevious = new DevExpress.XtraEditors.SimpleButton();
        this.imageCollection = new DevExpress.Utils.ImageCollection(this.components);
        this.btnNext = new DevExpress.XtraEditors.SimpleButton();
        this.txtSearch = new DevExpress.XtraEditors.TextEdit();
        this.rbStock = new DevExpress.XtraEditors.RadioGroup();
        this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
        this.txtSearchText = new DevExpress.XtraEditors.LabelControl();
        this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
        this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
        ((System.ComponentModel.ISupportInitialize)(this.gcStockCode)).BeginInit();
        this.gcStockCode.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.gridStockCode)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.rbStock.Properties)).BeginInit();
        this.SuspendLayout();
        // 
        // gcStockCode
        // 
        this.gcStockCode.Controls.Add(this.gridStockCode);
        this.gcStockCode.Location = new System.Drawing.Point(12, 12);
        this.gcStockCode.Name = "gcStockCode";
        this.gcStockCode.ShowCaption = false;
        this.gcStockCode.Size = new System.Drawing.Size(564, 290);
        this.gcStockCode.TabIndex = 7;
        // 
        // gridStockCode
        // 
        this.gridStockCode.EmbeddedNavigator.Buttons.Append.Enabled = false;
        this.gridStockCode.EmbeddedNavigator.Buttons.Edit.Enabled = false;
        this.gridStockCode.EmbeddedNavigator.Buttons.Remove.Enabled = false;
        this.gridStockCode.Location = new System.Drawing.Point(5, 5);
        this.gridStockCode.MainView = this.gridView;
        this.gridStockCode.Name = "gridStockCode";
        this.gridStockCode.Size = new System.Drawing.Size(554, 280);
        this.gridStockCode.TabIndex = 0;
        this.gridStockCode.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
        // 
        // gridView
        // 
        this.gridView.ActiveFilterEnabled = false;
        this.gridView.AppearancePrint.OddRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.gridView.GridControl = this.gridStockCode;
        this.gridView.Name = "gridView";
        this.gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
        this.gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
        this.gridView.OptionsBehavior.AutoSelectAllInEditor = false;
        this.gridView.OptionsBehavior.AutoUpdateTotalSummary = false;
        this.gridView.OptionsBehavior.Editable = false;
        this.gridView.OptionsBehavior.ReadOnly = true;
        this.gridView.OptionsCustomization.AllowColumnMoving = false;
        this.gridView.OptionsCustomization.AllowFilter = false;
        this.gridView.OptionsCustomization.AllowGroup = false;
        this.gridView.OptionsCustomization.AllowQuickHideColumns = false;
        this.gridView.OptionsFilter.AllowColumnMRUFilterList = false;
        this.gridView.OptionsFilter.AllowFilterEditor = false;
        this.gridView.OptionsFilter.AllowMRUFilterList = false;
        this.gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
        this.gridView.OptionsSelection.InvertSelection = true;
        this.gridView.OptionsSelection.UseIndicatorForSelection = false;
        this.gridView.OptionsView.EnableAppearanceEvenRow = true;
        this.gridView.OptionsView.ShowGroupPanel = false;
        this.gridView.OptionsView.ShowIndicator = false;
        this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
        // 
        // btnPrevious
        // 
        this.btnPrevious.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnPrevious.Appearance.Options.UseFont = true;
        this.btnPrevious.ImageIndex = 4;
        this.btnPrevious.ImageList = this.imageCollection;
        this.btnPrevious.Location = new System.Drawing.Point(12, 310);
        this.btnPrevious.Name = "btnPrevious";
        this.btnPrevious.Size = new System.Drawing.Size(91, 35);
        this.btnPrevious.TabIndex = 4;
        this.btnPrevious.Text = "Previous";
        this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
        // 
        // imageCollection
        // 
        this.imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection.ImageStream")));
        this.imageCollection.Images.SetKeyName(0, "arrow_next.gif");
        this.imageCollection.Images.SetKeyName(1, "arrow_prev.gif");
        this.imageCollection.Images.SetKeyName(2, "play.jpg");
        this.imageCollection.Images.SetKeyName(3, "accept22.gif");
        this.imageCollection.Images.SetKeyName(4, "back22.png");
        this.imageCollection.Images.SetKeyName(5, "next22.png");
        this.imageCollection.Images.SetKeyName(6, "crit_32.gif");
        this.imageCollection.Images.SetKeyName(7, "but_search.gif");
        // 
        // btnNext
        // 
        this.btnNext.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnNext.Appearance.Options.UseFont = true;
        this.btnNext.ImageIndex = 5;
        this.btnNext.ImageList = this.imageCollection;
        this.btnNext.Location = new System.Drawing.Point(123, 310);
        this.btnNext.Name = "btnNext";
        this.btnNext.Size = new System.Drawing.Size(91, 35);
        this.btnNext.TabIndex = 3;
        this.btnNext.Text = "Next";
        this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
        // 
        // txtSearch
        // 
        this.txtSearch.Location = new System.Drawing.Point(123, 355);
        this.txtSearch.Name = "txtSearch";
        this.txtSearch.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtSearch.Properties.Appearance.Options.UseFont = true;
        this.txtSearch.Size = new System.Drawing.Size(453, 22);
        this.txtSearch.TabIndex = 0;
        this.txtSearch.EditValueChanged += new System.EventHandler(this.txtSearch_EditValueChanged);
        this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
        // 
        // rbStock
        // 
        this.rbStock.Location = new System.Drawing.Point(259, 308);
        this.rbStock.Name = "rbStock";
        this.rbStock.Properties.Appearance.BackColor = System.Drawing.Color.White;
        this.rbStock.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.rbStock.Properties.Appearance.Options.UseBackColor = true;
        this.rbStock.Properties.Appearance.Options.UseFont = true;
        this.rbStock.Properties.Columns = 2;
        this.rbStock.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("CODE", "Stock Code"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("DESC", "Short Code")});
        this.rbStock.Size = new System.Drawing.Size(317, 41);
        this.rbStock.TabIndex = 1;
        this.rbStock.SelectedIndexChanged += new System.EventHandler(this.radioGroup_SelectedIndexChanged);
        // 
        // btnSearch
        // 
        this.btnSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnSearch.Appearance.Options.UseFont = true;
        this.btnSearch.ImageIndex = 7;
        this.btnSearch.ImageList = this.imageCollection;
        this.btnSearch.Location = new System.Drawing.Point(123, 387);
        this.btnSearch.Name = "btnSearch";
        this.btnSearch.Size = new System.Drawing.Size(81, 35);
        this.btnSearch.TabIndex = 2;
        this.btnSearch.Text = "Search";
        this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
        // 
        // txtSearchText
        // 
        this.txtSearchText.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.txtSearchText.Appearance.Options.UseFont = true;
        this.txtSearchText.Location = new System.Drawing.Point(17, 361);
        this.txtSearchText.Name = "txtSearchText";
        this.txtSearchText.Size = new System.Drawing.Size(86, 16);
        this.txtSearchText.TabIndex = 6;
        this.txtSearchText.Text = "Search Text :";
        // 
        // btnCancel
        // 
        this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnCancel.Appearance.Options.UseFont = true;
        this.btnCancel.ImageIndex = 6;
        this.btnCancel.ImageList = this.imageCollection;
        this.btnCancel.Location = new System.Drawing.Point(495, 387);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new System.Drawing.Size(81, 35);
        this.btnCancel.TabIndex = 6;
        this.btnCancel.Text = "Cancel";
        this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
        // 
        // btnSelect
        // 
        this.btnSelect.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnSelect.Appearance.Options.UseFont = true;
        this.btnSelect.ImageIndex = 3;
        this.btnSelect.ImageList = this.imageCollection;
        this.btnSelect.Location = new System.Drawing.Point(290, 387);
        this.btnSelect.Name = "btnSelect";
        this.btnSelect.Size = new System.Drawing.Size(81, 35);
        this.btnSelect.TabIndex = 5;
        this.btnSelect.Text = "Select";
        this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
        // 
        // FrmStockCodeSearcher
        // 
        this.Appearance.BackColor = System.Drawing.Color.White;
        this.Appearance.Options.UseBackColor = true;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(588, 439);
        this.Controls.Add(this.btnSelect);
        this.Controls.Add(this.btnCancel);
        this.Controls.Add(this.txtSearchText);
        this.Controls.Add(this.btnSearch);
        this.Controls.Add(this.rbStock);
        this.Controls.Add(this.txtSearch);
        this.Controls.Add(this.btnNext);
        this.Controls.Add(this.btnPrevious);
        this.Controls.Add(this.gcStockCode);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "FrmStockCodeSearcher";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Stock Code Searcher";
        this.Load += new System.EventHandler(this.FrmStockCodeSearcher_Load);
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmStockCodeSearcher_FormClosing);
        ((System.ComponentModel.ISupportInitialize)(this.gcStockCode)).EndInit();
        this.gcStockCode.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.gridStockCode)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.rbStock.Properties)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private DevExpress.XtraEditors.GroupControl gcStockCode;
    private DevExpress.XtraEditors.SimpleButton btnPrevious;
    private DevExpress.XtraEditors.SimpleButton btnNext;
    private DevExpress.Utils.ImageCollection imageCollection;
    private DevExpress.XtraEditors.RadioGroup rbStock;
    private DevExpress.XtraEditors.SimpleButton btnSearch;
    private DevExpress.XtraEditors.LabelControl txtSearchText;
    private DevExpress.XtraEditors.SimpleButton btnCancel;
    private DevExpress.XtraGrid.GridControl gridStockCode;
    private DevExpress.XtraGrid.Views.Grid.GridView gridView;
    private DevExpress.XtraEditors.SimpleButton btnSelect;
    public DevExpress.XtraEditors.TextEdit txtSearch;
  }
}