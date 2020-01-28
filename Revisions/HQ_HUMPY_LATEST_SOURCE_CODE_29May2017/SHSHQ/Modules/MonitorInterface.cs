 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ISMDAL;
using ISMDAL.TableColumnName;

namespace ISM.Modules
{
  public partial class MonitorInterface : ISMBaseWorkSpace
  {
    public MonitorInterface(ISMLoginInfo AISMLoginInfo)
      : base(AISMLoginInfo)
    {
      InitializeComponent();
    }

    private void btnReport_Click(object sender, EventArgs e)
    {

    }
  }
}
