namespace ISM.Forms
{
  partial class FrmPWordChange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPWordChange));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblCaption = new DevExpress.XtraLayout.SimpleLabelItem();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.grControl = new DevExpress.XtraEditors.GroupControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblPassword = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grControl)).BeginInit();
            this.grControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(493, 56);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblCaption});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(493, 56);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblCaption
            // 
            this.lblCaption.AllowHotTrack = false;
            this.lblCaption.AppearanceItemCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblCaption.AppearanceItemCaption.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblCaption.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaption.AppearanceItemCaption.Options.UseBackColor = true;
            this.lblCaption.AppearanceItemCaption.Options.UseFont = true;
            this.lblCaption.CustomizationFormText = "LabellblCaption";
            this.lblCaption.Location = new System.Drawing.Point(0, 0);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(473, 36);
            this.lblCaption.Text = "ISM Password Update";
            this.lblCaption.TextSize = new System.Drawing.Size(210, 23);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(369, 196);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            // 
            // btnOk
            // 
            this.btnOk.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Appearance.Options.UseFont = true;
            this.btnOk.Location = new System.Drawing.Point(158, 196);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.EditValue = "";
            this.txtPassword.EnterMoveNextControl = true;
            this.txtPassword.Location = new System.Drawing.Point(158, 77);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Properties.MaxLength = 20;
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(286, 22);
            this.txtPassword.TabIndex = 2;
            // 
            // grControl
            // 
            this.grControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.grControl.Controls.Add(this.textEdit1);
            this.grControl.Controls.Add(this.labelControl1);
            this.grControl.Controls.Add(this.layoutControl1);
            this.grControl.Controls.Add(this.btnCancel);
            this.grControl.Controls.Add(this.btnOk);
            this.grControl.Controls.Add(this.txtPassword);
            this.grControl.Controls.Add(this.lblPassword);
            this.grControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grControl.Location = new System.Drawing.Point(0, 0);
            this.grControl.Name = "grControl";
            this.grControl.Size = new System.Drawing.Size(493, 249);
            this.grControl.TabIndex = 1;
            // 
            // textEdit1
            // 
            this.textEdit1.EditValue = "";
            this.textEdit1.EnterMoveNextControl = true;
            this.textEdit1.Location = new System.Drawing.Point(158, 117);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit1.Properties.Appearance.Options.UseFont = true;
            this.textEdit1.Properties.MaxLength = 20;
            this.textEdit1.Properties.PasswordChar = '*';
            this.textEdit1.Size = new System.Drawing.Size(286, 22);
            this.textEdit1.TabIndex = 4;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(27, 120);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(121, 16);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Confirm Password:";
            // 
            // lblPassword
            // 
            this.lblPassword.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(49, 80);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(99, 16);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "New Password:";
            // 
            // FrmPWordChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 249);
            this.ControlBox = false;
            this.Controls.Add(this.grControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPWordChange";
            this.Text = "Password Update";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grControl)).EndInit();
            this.grControl.ResumeLayout(false);
            this.grControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private DevExpress.XtraLayout.LayoutControl layoutControl1;
    private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    private DevExpress.XtraLayout.SimpleLabelItem lblCaption;
    private DevExpress.XtraEditors.SimpleButton btnCancel;
    private DevExpress.XtraEditors.SimpleButton btnOk;
    public DevExpress.XtraEditors.TextEdit txtPassword;
    private DevExpress.XtraEditors.GroupControl grControl;
    public DevExpress.XtraEditors.TextEdit textEdit1;
    private DevExpress.XtraEditors.LabelControl labelControl1;
    private DevExpress.XtraEditors.LabelControl lblPassword;
  }
}