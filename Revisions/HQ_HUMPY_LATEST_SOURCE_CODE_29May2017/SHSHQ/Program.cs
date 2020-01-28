using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ISM.Modules;
using ISMDAL.TableColumnName;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace SHSHQ
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
   
    //[DllImport("user32.dll")]
    //public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    //[DllImport("user32.dll")]
    //public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    static void Main()
    {


        Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");//MR IN 26-JUL-12
        if (!mutex.WaitOne(TimeSpan.Zero, true)) 
        {
            MessageBox.Show("The PC Client is already running", "HQ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        else
        {
            //IntPtr hWnd = FindWindow(null, Console.Title);
            GC.Collect();
            ISMLoginInfo zLogInInfo = new ISMLoginInfo();         // DM in 23-JUL-10
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FrmLogin zFrmLogin = new FrmLogin(zLogInInfo);        // DM in 23-JUL-10
            zFrmLogin.ShowDialog();                               // DM in 23-JUL-10
            if (zFrmLogin.DialogResult.Equals(DialogResult.OK))   // DM in 23-JUL-10
                Application.Run(new FrmMain(zLogInInfo));
            // Out DM 23-JUL-10 Application.Run(new FrmLogin());
            mutex.ReleaseMutex();

        }
    }
  }
}
