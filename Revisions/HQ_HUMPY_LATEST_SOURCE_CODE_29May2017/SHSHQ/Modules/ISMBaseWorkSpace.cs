 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

using System;
using System.ComponentModel;
using System.Windows.Forms;

using ISMDAL.TableColumnName;

namespace ISM.Modules
{
  [ToolboxItem(false)]  
  public partial class ISMBaseWorkSpace : DevExpress.XtraEditors.XtraUserControl
  {
    protected ISMLoginInfo m_ISMLoginInfo;
    public enum EnumFrmMode { CREATE, EDIT, REMOVE };    
    
    public ISMBaseWorkSpace()
    {
      InitializeComponent();
    }
    
    public ISMBaseWorkSpace(ISMLoginInfo AISMLoginInfo)
    {
      InitializeComponent();
      m_ISMLoginInfo = AISMLoginInfo;
    }

    private void ISMBaseWorkSpace_Load(object sender, EventArgs e)
    {
      this.Dock = DockStyle.Fill;
       
       
       
       
       
    }

    private void ISMBaseWorkSpace_Leave(object sender, EventArgs e)
    {

    }
  }
}
