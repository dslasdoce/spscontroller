//****************************************************************************//
//       This source code belongs to Barcode Data Systems (BCDS)              //
//    Unit 24, 10 Yalgar Road, Kirrawee, Sydney, NSW Australia 2232           //
//                           www.bcds.com.au                                  //


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace SmartHumpyController
{
  partial class FrmAboutISM : Form
  {
    public FrmAboutISM()
    {
      InitializeComponent();
      this.Text = String.Format("About {0}", AssemblyTitle);
      this.labelProductName.Text = AssemblyProduct;
      string[] zVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.'); // MR IN 18-AUG-11
      //Show Ver display X.Y.Z
      // this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion); MR OUT 18-AUG-11
      this.labelVersion.Text = String.Format("Version {0}.{1}.{2}", zVersion[0], zVersion[1], zVersion[2]); // MR IN 18-AUG-11
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
