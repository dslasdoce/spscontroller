 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

 
 
 
 
using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraTreeList.Nodes;

namespace ISM.Class
{
    public class TreeViewExpandOperation : TreeListOperation
    {
            private int m_level;
            public TreeViewExpandOperation(int expandLevel)
            {
                m_level = expandLevel;
            }

            public override void Execute(TreeListNode node)
            {
                if (node.Level <= m_level)
                {
                    node.Expanded = true;
                }
            }

            public override bool NeedsVisitChildren(TreeListNode node)
            {
                if (node.Level == m_level)
                {
                    return false;
                }
                return base.NeedsVisitChildren(node);
            }
    }
}
