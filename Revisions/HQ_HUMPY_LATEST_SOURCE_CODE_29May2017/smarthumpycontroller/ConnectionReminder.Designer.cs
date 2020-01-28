namespace SmartHumpyController
{
    partial class ConnectionReminder
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
            this.lblMessage = new System.Windows.Forms.Label();
            this.timConnection = new System.Windows.Forms.Timer(this.components);
            this.cmdRestart = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(1, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(663, 197);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = " sdfgksdfgs\'dfg sdfgsdlfgk;sldfgkfsdg gsdflgsldfgksdlfg ";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMessage.Click += new System.EventHandler(this.lblMessage_Click);
            // 
            // timConnection
            // 
            this.timConnection.Enabled = true;
            this.timConnection.Interval = 1000;
            this.timConnection.Tick += new System.EventHandler(this.timConnection_Tick);
            // 
            // cmdRestart
            // 
            this.cmdRestart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cmdRestart.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRestart.ForeColor = System.Drawing.Color.White;
            this.cmdRestart.Location = new System.Drawing.Point(160, 200);
            this.cmdRestart.Name = "cmdRestart";
            this.cmdRestart.Size = new System.Drawing.Size(346, 49);
            this.cmdRestart.TabIndex = 22;
            this.cmdRestart.Text = "Manually restart  Pinning Station Contoller";
            this.cmdRestart.UseVisualStyleBackColor = false;
            this.cmdRestart.Visible = false;
            this.cmdRestart.Click += new System.EventHandler(this.cmdRestart_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(42, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 23;
            this.textBox1.Visible = false;
            // 
            // ConnectionReminder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(666, 261);
            this.ControlBox = false;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cmdRestart);
            this.Controls.Add(this.lblMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ConnectionReminder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection Failure";
            this.Load += new System.EventHandler(this.ConnectionReminder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Timer timConnection;
        private System.Windows.Forms.Button cmdRestart;
        private System.Windows.Forms.TextBox textBox1;
    }
}