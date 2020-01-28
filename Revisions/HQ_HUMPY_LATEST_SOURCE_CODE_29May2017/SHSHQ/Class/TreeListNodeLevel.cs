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
// File        : TreeListNodeLevel.cs                                         //
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
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraTreeList.Nodes;

namespace ISM.Class
{
    public class TreeListNodeLevel : TreeListOperation
    {
        private int m_maxLevelNode = 0;
        public override void Execute(TreeListNode node)
        {
            if (node.Level > m_maxLevelNode)
            {
                m_maxLevelNode = node.Level;
            }
        }
        public int MaxLevel
        {
            get { return m_maxLevelNode; }
        }
    }
}
