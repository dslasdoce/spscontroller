using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;//IN JL 12-MAY-2016

using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;

using System.IO;

namespace SmartHumpyController
{
    public partial class FrmAdmin : Form
    {
        private KeyboardFunction mVirtualKb = new KeyboardFunction();
        private System.Diagnostics.Process _p = null;
        string mAdminPassword = "";
     
        public DialogResult zdr = DialogResult.Cancel;

        public FrmAdmin(HumpyDetail aHD)
        {
            InitializeComponent();
            mAdminPassword = "p@ssw0rd";//aHD.Sys_password.ToString();

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtAdminPassword.Text.ToUpper() != mAdminPassword.ToUpper())
            {
                MessageBox.Show("Invalid Password", "WARNING!!!!");
                txtAdminPassword.Text = "";
                return;
            }
            else
            {
                zdr = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAdminPassword_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                mVirtualKb.openKeyboardOnTablet();

                ////IN JL 13-MAY-16: Touch Keyboard
                //string progFiles = @"C:\Program Files\Common Files\Microsoft Shared\ink";
                //string TouchPad_keyboardPath = Path.Combine(progFiles, "TabTip.exe");
                ////this.keyboardProc = Process.Start(keyboardPath);

                ////IN JL 13-MAY-16: On Screen keyboard (32bit;64bit;)
                //var Keyboard_path64 = @"C:\Windows\winsxs\amd64_microsoft-windows-osk_31bf3856ad364e35_6.1.7600.16385_none_06b1c513739fb828\osk.exe";
                //var Keyboard_path32 = @"C:\windows\system32\osk.exe";
                //var Keyboard_path = (Environment.Is64BitOperatingSystem) ? Keyboard_path64 : Keyboard_path32;

                ////IN JL 12-MAY-2016
                //if (_p == null)
                //    _p = System.Diagnostics.Process.Start(Keyboard_path);
                //else
                //{
                //    if (_p != null)
                //    {
                //        _p.Kill();
                //        _p.Dispose();
                //        _p = null;
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Close Keyboard Error:"+ex.ToString());
            }
        }
    }


    public class KeyboardFunction //IN JL 28-MAY-2016
    {
        //Used for close keyboard method
        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;
        //End close keyboard inputs


        /*
       *Opens the tablet keyboard on device
       *
       */
        public void openKeyboardOnTablet()
        {
            System.Diagnostics.Process.Start(@"C:\Program Files\Common Files\Microsoft Shared\ink\TabTip.exe");
        }

        /*
      *Close on screen keyboard
      *
      */
        public void closeOnscreenKeyboard()
        {
            // retrieve the handler of the window  
            int iHandle = FindWindow("IPTIP_Main_Window", "");
            if (iHandle > 0)
            {
                // close the window using API        
                SendMessage(iHandle, WM_SYSCOMMAND, SC_CLOSE, 0);
            }
        }

    }
}
