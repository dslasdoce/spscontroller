namespace ISM.Modules
{
  partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.grControl = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.luProfile = new DevExpress.XtraEditors.LookUpEdit();
            this.lblProfile = new DevExpress.XtraEditors.LabelControl();
            this.lblWarningMsg3 = new DevExpress.XtraEditors.LabelControl();
            this.lblWarningMsg2 = new DevExpress.XtraEditors.LabelControl();
            this.lblWarningMsg1 = new DevExpress.XtraEditors.LabelControl();
            this.lblWarning = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtUserID = new DevExpress.XtraEditors.TextEdit();
            this.lblPassword = new DevExpress.XtraEditors.LabelControl();
            this.lblUserName = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblCaption = new DevExpress.XtraLayout.SimpleLabelItem();
            this.imgSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grControl)).BeginInit();
            this.grControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.luProfile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).BeginInit();
            this.SuspendLayout();
            // 
            // grControl
            // 
            this.grControl.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grControl.Appearance.Options.UseBackColor = true;
            this.grControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.grControl.Controls.Add(this.labelControl1);
            this.grControl.Controls.Add(this.luProfile);
            this.grControl.Controls.Add(this.lblProfile);
            this.grControl.Controls.Add(this.lblWarningMsg3);
            this.grControl.Controls.Add(this.lblWarningMsg2);
            this.grControl.Controls.Add(this.lblWarningMsg1);
            this.grControl.Controls.Add(this.lblWarning);
            this.grControl.Controls.Add(this.btnCancel);
            this.grControl.Controls.Add(this.btnLogin);
            this.grControl.Controls.Add(this.txtPassword);
            this.grControl.Controls.Add(this.txtUserID);
            this.grControl.Controls.Add(this.lblPassword);
            this.grControl.Controls.Add(this.lblUserName);
            this.grControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grControl.Location = new System.Drawing.Point(0, 0);
            this.grControl.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.grControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.grControl.Name = "grControl";
            this.grControl.Size = new System.Drawing.Size(639, 388);
            this.grControl.TabIndex = 0;
            this.grControl.Paint += new System.Windows.Forms.PaintEventHandler(this.grControl_Paint);
            this.grControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmLogin_MouseDown);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(62)))), ((int)(((byte)(115)))));
            this.labelControl1.Location = new System.Drawing.Point(250, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(107, 39);
            this.labelControl1.TabIndex = 15;
            this.labelControl1.Text = "LOGIN";
            // 
            // luProfile
            // 
            this.luProfile.Location = new System.Drawing.Point(177, 165);
            this.luProfile.Name = "luProfile";
            this.luProfile.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.luProfile.Properties.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            this.luProfile.Properties.Appearance.Options.UseFont = true;
            this.luProfile.Properties.Appearance.Options.UseForeColor = true;
            this.luProfile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luProfile.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.luProfile.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.luProfile.Properties.NullText = "Select a profile";
            this.luProfile.Size = new System.Drawing.Size(323, 30);
            this.luProfile.TabIndex = 2;
            this.luProfile.Visible = false;
            // 
            // lblProfile
            // 
            this.lblProfile.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProfile.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            this.lblProfile.Location = new System.Drawing.Point(97, 169);
            this.lblProfile.Name = "lblProfile";
            this.lblProfile.Size = new System.Drawing.Size(68, 25);
            this.lblProfile.TabIndex = 14;
            this.lblProfile.Text = "Profile";
            this.lblProfile.Visible = false;
            // 
            // lblWarningMsg3
            // 
            this.lblWarningMsg3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarningMsg3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblWarningMsg3.Location = new System.Drawing.Point(10, 263);
            this.lblWarningMsg3.Name = "lblWarningMsg3";
            this.lblWarningMsg3.Size = new System.Drawing.Size(0, 0);
            this.lblWarningMsg3.TabIndex = 11;
            // 
            // lblWarningMsg2
            // 
            this.lblWarningMsg2.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarningMsg2.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            this.lblWarningMsg2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblWarningMsg2.Location = new System.Drawing.Point(91, 334);
            this.lblWarningMsg2.Name = "lblWarningMsg2";
            this.lblWarningMsg2.Size = new System.Drawing.Size(449, 23);
            this.lblWarningMsg2.TabIndex = 10;
            this.lblWarningMsg2.Text = "Authorised personnel and contracted staff only";
            // 
            // lblWarningMsg1
            // 
            this.lblWarningMsg1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarningMsg1.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            this.lblWarningMsg1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblWarningMsg1.Location = new System.Drawing.Point(10, 314);
            this.lblWarningMsg1.Name = "lblWarningMsg1";
            this.lblWarningMsg1.Size = new System.Drawing.Size(611, 23);
            this.lblWarningMsg1.TabIndex = 9;
            this.lblWarningMsg1.Text = "Your use of this system is being monitored and may be audited. ";
            // 
            // lblWarning
            // 
            this.lblWarning.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.Appearance.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblWarning.Location = new System.Drawing.Point(232, 289);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(166, 23);
            this.lblWarning.TabIndex = 8;
            this.lblWarning.Text = "Important Notice";
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(62)))), ((int)(((byte)(115)))));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Appearance.Options.UseBackColor = true;
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Appearance.Options.UseForeColor = true;
            this.btnCancel.Location = new System.Drawing.Point(343, 215);
            this.btnCancel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.btnCancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(157, 42);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Exit";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(62)))), ((int)(((byte)(115)))));
            this.btnLogin.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Appearance.Options.UseBackColor = true;
            this.btnLogin.Appearance.Options.UseFont = true;
            this.btnLogin.Appearance.Options.UseForeColor = true;
            this.btnLogin.Location = new System.Drawing.Point(176, 215);
            this.btnLogin.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnLogin.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(157, 42);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Validate";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.EditValue = "";
            this.txtPassword.EnterMoveNextControl = true;
            this.txtPassword.Location = new System.Drawing.Point(177, 118);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Properties.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Properties.Appearance.Options.UseForeColor = true;
            this.txtPassword.Properties.MaxLength = 20;
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(323, 30);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.EditValueChanged += new System.EventHandler(this.EnableBtnLogin);
            // 
            // txtUserID
            // 
            this.txtUserID.EnterMoveNextControl = true;
            this.txtUserID.Location = new System.Drawing.Point(177, 69);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserID.Properties.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            this.txtUserID.Properties.Appearance.Options.UseFont = true;
            this.txtUserID.Properties.Appearance.Options.UseForeColor = true;
            this.txtUserID.Properties.MaxLength = 55;
            this.txtUserID.Size = new System.Drawing.Size(323, 30);
            this.txtUserID.TabIndex = 0;
            this.txtUserID.EditValueChanged += new System.EventHandler(this.EnableBtnLogin);
            // 
            // lblPassword
            // 
            this.lblPassword.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            this.lblPassword.Location = new System.Drawing.Point(62, 122);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(103, 25);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password";
            // 
            // lblUserName
            // 
            this.lblUserName.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Appearance.ForeColor = System.Drawing.Color.DarkGray;
            this.lblUserName.Location = new System.Drawing.Point(85, 73);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(80, 25);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "User ID";
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
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 5;
            this.layoutControlGroup1.Size = new System.Drawing.Size(497, 69);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
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
            this.lblCaption.Size = new System.Drawing.Size(477, 49);
            this.lblCaption.Text = "ISM User Login";
            this.lblCaption.TextSize = new System.Drawing.Size(182, 29);
            // 
            // imgSmallImageCollection
            // 
            this.imgSmallImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgSmallImageCollection.ImageStream")));
            this.imgSmallImageCollection.Images.SetKeyName(0, "add22.png");
            this.imgSmallImageCollection.Images.SetKeyName(1, "delete22.png");
            this.imgSmallImageCollection.Images.SetKeyName(2, "refresh22.png");
            this.imgSmallImageCollection.Images.SetKeyName(3, "saveas22.png");
            this.imgSmallImageCollection.Images.SetKeyName(4, "dropdown arrow.png");
            this.imgSmallImageCollection.Images.SetKeyName(5, "exit.png");
            this.imgSmallImageCollection.Images.SetKeyName(6, "login box.png");
            this.imgSmallImageCollection.Images.SetKeyName(7, "text box.png");
            this.imgSmallImageCollection.Images.SetKeyName(8, "validate.png");
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 388);
            this.Controls.Add(this.grControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HQ Controller";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmLogin_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grControl)).EndInit();
            this.grControl.ResumeLayout(false);
            this.grControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.luProfile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmallImageCollection)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private DevExpress.XtraEditors.GroupControl grControl;
    private DevExpress.XtraEditors.SimpleButton btnCancel;
    private DevExpress.XtraEditors.SimpleButton btnLogin;
    public DevExpress.XtraEditors.TextEdit txtPassword;
    public DevExpress.XtraEditors.TextEdit txtUserID;
    private DevExpress.XtraEditors.LabelControl lblPassword;
    private DevExpress.XtraEditors.LabelControl lblUserName;
    private DevExpress.XtraLayout.LayoutControl layoutControl1;
    private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    private DevExpress.XtraLayout.SimpleLabelItem lblCaption;
    private DevExpress.XtraEditors.LabelControl lblWarningMsg1;
    private DevExpress.XtraEditors.LabelControl lblWarning;
    private DevExpress.XtraEditors.LabelControl lblWarningMsg2;
    private DevExpress.XtraEditors.LabelControl lblWarningMsg3;
    private DevExpress.XtraEditors.LookUpEdit luProfile;
    private DevExpress.XtraEditors.LabelControl lblProfile;
    private DevExpress.XtraEditors.LabelControl labelControl1;
    private DevExpress.Utils.ImageCollection imgSmallImageCollection;


  }
}