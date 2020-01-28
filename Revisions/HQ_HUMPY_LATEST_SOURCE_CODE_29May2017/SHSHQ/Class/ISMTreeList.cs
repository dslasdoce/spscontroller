using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.ViewInfo;
using DevExpress.XtraTreeList.Nodes;

namespace ISM
{
  public class ISMTreeList : DevExpress.XtraTreeList.TreeList
  {
    public ISMTreeList() : base() { }

    protected override TreeListViewInfo CreateViewInfo()
    {
      return new ISMTreeListViewInfo(this);
    }
  }

  public class ISMTreeListViewInfo : TreeListViewInfo
  {
    public ISMTreeListViewInfo(DevExpress.XtraTreeList.TreeList ATreeList) 
      : base(ATreeList) 
    { 
      // Nothing to do here but inherit
    }

    protected override Point GetDataBoundsLocation(TreeListNode node, int top)
    {
      Point zResult = base.GetDataBoundsLocation(node, top);

      if (Size.Empty != RC.SelectImageSize && -1 == node.SelectImageIndex)
        zResult.X -= RC.SelectImageSize.Width;

      if (Size.Empty != RC.StateImageSize && -1 == node.StateImageIndex)
        zResult.X -= RC.StateImageSize.Width;

      return zResult;
    }

    protected override void CalcStateImage(RowInfo ri)
    {
      base.CalcStateImage(ri);
      if (Size.Empty != RC.SelectImageSize && -1 == ri.Node.SelectImageIndex)
        ri.StateImageLocation.X -= RC.SelectImageSize.Width;
    }

    protected override void CalcSelectImage(RowInfo ri)
    {
      base.CalcSelectImage(ri);
      if (ri.Node.StateImageIndex == -1) 
          ri.SelectImageLocation = Point.Empty;
    }
  }
}