//****************************************************************************//
//       This source code belongs to Barcode Data Systems (BCDS)              //
//    Unit 24, 10 Yalgar Road, Kirrawee, Sydney, NSW Australia 2232           //
//                           www.bcds.com.au                                  //
//                                                                            //
// This file may not be copied in whole or in part without written permission //
//                         from the copyright owner.                          //
//                                                                            //
//                      © 2010, Barcode Data Systems (BCDS)                   //
//****************************************************************************//
//                                                                            //
// Project     : Improved Stock Management (ISM)                              //
//                                                                            //
// Client      : Australian Department of Defense                             //
//                                                                            //
// File        : FrmAboutISM.cs                                               //
//                                                                            //
// Description : About form for the ISM app                                   //
//                                                                            //
// Initial Author: Team BCDS                                           .      //
// Date Written  : 17-AUG-2010                                                //
// Documentation : BCDS Visual Source Safe.                                   //
//                 ISM\Document\Functional Spec\Functional Spec V1.doc        //
//                                                                            //
//****************************************************************************//
// Modification History:                                                      //
//                                                                            //
// Date..... Who..........  Modification Description......................... //
// DD-MMM-YY xxxxxxxxxxxxx                                                    //
//                                                                            //
// 18-AUG-11 MR             Updated Version X.Y.Z Format                      //
//                                                                            //
// 27-AUG-10 Damian Murray  Added a timer to close this Form if the user has  //
//                          not closed it themselves within 30 seconds a set  //
//                          static property value.                            //
//                                                                            //
// 17-AUG-10 Damian Murray  Added this About form to the ISM project and      //
//                          allowed it to be called from FrmMain.cs           //
//                                                                            //
// dd-MMM-YY MR             ver 1.0                                           //
//                          Initial Release.                                  //
//                                                                            //
//****************************************************************************//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices; //MR In 15-DEC-10
using System.Windows.Forms;

namespace ISM.Forms
{
  partial class FrmAboutSHS : Form
  {
      //Boardless form movement IN J: 12-MAY-16
      public const int WM_NCLBUTTONDOWN = 0xA1;
      public const int HT_CAPTION = 0x2;

      [DllImportAttribute("user32.dll")]
      public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
      [DllImportAttribute("user32.dll")]
      public static extern bool ReleaseCapture();


      private void FrmAboutISM_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
          if (e.Button == MouseButtons.Left)
          {
              ReleaseCapture();
              SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
          }
      }

    public FrmAboutSHS()
    {
      InitializeComponent();
      this.Text = String.Format("About {0}", AssemblyTitle);
      this.labelProductName.Text = AssemblyProduct;
      string[] zVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.'); // MR IN 18-AUG-11
      //Show Ver display X.Y.Z
      // this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion); MR OUT 18-AUG-11
      this.labelVersion.Text = String.Format(" {0}.{1}.{2}", zVersion[0], zVersion[1], zVersion[2]); // MR IN 18-AUG-11
      this.labelCopyright.Text = AssemblyCopyright;
      this.labelCompanyName.Text = AssemblyCompany;
      this.textBoxDescription.Text = AssemblyDescription;
    }

    #region Assembly Attribute Accessors

    public string AssemblyTitle
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
        if (attributes.Length > 0)
        {
          AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
          if (titleAttribute.Title != "")
          {
            return titleAttribute.Title;
          }
        }
        return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
      }
    }

    public string AssemblyVersion
    {
      get
      {
        return Assembly.GetExecutingAssembly().GetName().Version.ToString();
      }
    }

    public string AssemblyDescription
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
        if (attributes.Length == 0)
        {
          return "";
        }
        return ((AssemblyDescriptionAttribute)attributes[0]).Description;
      }
    }

    public string AssemblyProduct
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
        if (attributes.Length == 0)
        {
          return "";
        }
        return ((AssemblyProductAttribute)attributes[0]).Product;
      }
    }

    public string AssemblyCopyright
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
        if (attributes.Length == 0)
        {
          return "";
        }
        return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
      }
    }

    public string AssemblyCompany
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
        if (attributes.Length == 0)
        {
          return "";
        }
        return ((AssemblyCompanyAttribute)attributes[0]).Company;
      }
    }
    #endregion

    private void tmrFrmClose_Tick(object sender, EventArgs e) // DM In 27-AUG-10
    {
      this.Close();
    }

    private void FrmAboutISM_Load(object sender, EventArgs e)
    {
      tmrFrmClose.Enabled = true; // DM 27-AUG-10 Start the count down to Close this Form
    }
  }
}
