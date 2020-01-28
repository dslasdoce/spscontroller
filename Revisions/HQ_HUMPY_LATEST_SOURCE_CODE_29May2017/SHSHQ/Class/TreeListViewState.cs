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
// File        : TreeListViewState.cs                                         //
//                                                                            //
// Description :                                                              //
//                                                                            //
// Initial Author: R.Murugan                                                  //
// Date Written  : 20-MAY-2011                                                //
// Documentation : BCDS Visual Source Safe.                                   //
//                 ISM\Document\Functional Spec\Functional Spec V1.doc        //
//                                                                            //
//****************************************************************************//
// Modification History:                                                      //
//                                                                            //
// Date..... Who.......... Modification Description.......................... //
// DD-MMM-YY xxxxxxxxxxxxx                                                    //
//                                                                            //

// 20-MAY-11 MR              ver 1.0                                          //
//                           Initial Release.                                 //
//                                                                            //
//****************************************************************************//
using System;
using System.Collections;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Nodes.Operations;
using ISMDAL.TableColumnName;
using System.Data;

namespace ISM.Class
{
    public class TreeListViewState
    {
        private ArrayList expanded;
        private ArrayList selected;
        private object focused;
        private int topIndex;
        private DataSet m_DataSet = null;
        private ISMLoginInfo m_ISMLoginInfo = null;

        #region "Property "
        public ISMLoginInfo PropISMLoginInfo
        {
            get { return m_ISMLoginInfo; }
            set { m_ISMLoginInfo = value; }
        }
        #endregion

        public TreeListViewState() : this(null) { }
        public TreeListViewState(TreeList treeList)
        {
            this.treeList = treeList;
            expanded = new ArrayList();
            selected = new ArrayList();
        }

        public void Clear()
        {
            expanded.Clear();
            selected.Clear();
            focused = null;
            topIndex = 0;
        }
        private ArrayList GetExpanded()
        {
            OperationSaveExpanded op = new OperationSaveExpanded();
            TreeList.NodesIterator.DoOperation(op);
            return op.Nodes;
        }
        private ArrayList GetSelected()
        {
            ArrayList al = new ArrayList();
            foreach (TreeListNode node in TreeList.Selection)
                al.Add(node.GetValue(TreeList.KeyFieldName));
            return al;
        }

        public void LoadState()
        {
            TreeList.BeginUpdate();
            try
            {
                TreeList.CollapseAll();
                TreeListNode node;
                foreach (object key in expanded)
                {
                    node = TreeList.FindNodeByKeyID(key);
                    if (node != null)
                        node.Expanded = true;
                   
                }
                foreach (object key in selected)
                {
                    node = TreeList.FindNodeByKeyID(key);
                    if (node != null)
                        TreeList.Selection.Add(node);
                }
                if (m_DataSet != null)
                {
                    foreach (DataRow drERPData in m_DataSet.Tables[0].Rows)
                    {
                        TreeListNode Trnode = TreeList.FindNodeByFieldValue("UIDStr", "8" + drERPData["LocationUID"].ToString().PadLeft(12, '0'));
                        if (Trnode != null)
                        {
                            TreeList.SetFocusedNode(Trnode);
                            break;
                        }
                    }
                }
                else
                    TreeList.FocusedNode = TreeList.FindNodeByKeyID(focused);
            }
            finally
            {
                TreeList.EndUpdate();
                TreeList.TopVisibleNodeIndex = TreeList.GetVisibleIndexByNode(TreeList.FocusedNode) - topIndex;
            }
        }
        public void SaveState()
        {
            if (TreeList.FocusedNode != null)
            {
                long zFocusedLocUID = 0;
                expanded = GetExpanded();
                selected = GetSelected();
                focused = TreeList.FocusedNode[TreeList.KeyFieldName];
                object oValue = TreeList.GetDataRecordByNode(TreeList.FocusedNode);
                if (oValue != null)
                {
                    DataRow dr = ((DataRowView)oValue).Row;
                    if (dr != null)
                    {
                        if(dr["UIDStr"].ToString().Length == 13)
                        zFocusedLocUID = long.Parse(dr["UIDStr"].ToString().Substring(1,12));
                        m_DataSet = m_ISMLoginInfo.ISMServer.GetLocationParentUIDList(m_ISMLoginInfo.LogonID, zFocusedLocUID);
                    }
                }
               // m_FocusedNodeID = focusedTemp.ToString();
                topIndex = TreeList.GetVisibleIndexByNode(TreeList.FocusedNode) - TreeList.TopVisibleNodeIndex;
            }
            else
                Clear();
        }

        private TreeList treeList;
        public TreeList TreeList
        {
            get
            {
                return treeList;
            }
            set
            {
                treeList = value;
                Clear();
            }
        }

        class OperationSaveExpanded : TreeListOperation
        {
            private ArrayList al = new ArrayList();
            public override void Execute(TreeListNode node)
            {
                if (node.HasChildren && node.Expanded)
                    al.Add(node.GetValue(node.TreeList.KeyFieldName));
            }
            public ArrayList Nodes { get { return al; } }
        }

    }
}
