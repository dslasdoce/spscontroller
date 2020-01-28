using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections.Specialized;
using System.Diagnostics;

namespace SmartHumpyController
{
    public partial class FrmStartupcs : Form
    {
        private LogString m_Logger = LogString.GetLogString("SHS_StartUP");
        public bool m_MainFlag = true;
        public System.Threading.Timer mStartupTimer;
        private System.Threading.Timer m_CheckGateConfigurationChanges;
        private HumpyDAL m_DAL_Local;
        private HumpyDetail m_HumpyDetail;

        public FrmStartupcs()
        {
            InitializeComponent();
        }

        private void FrmStartupcs_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            m_MainFlag = true;
            //IN JL: Service needs to starup quickly, otherwise it will times out.
            int zDelayInterval = 5000;//5Seconds
            mStartupTimer = new System.Threading.Timer(new System.Threading.TimerCallback(OnStartupTimer), null,
                                                                     zDelayInterval, System.Threading.Timeout.Infinite);

            //Initialize the gate configuration changes //IN JL 08-OCT-2013
            m_CheckGateConfigurationChanges = new System.Threading.Timer(new System.Threading.TimerCallback(OnCheckGateConfigurationChanges), null,
                                                                 System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        private void OnCheckGateConfigurationChanges(object AObject)//IN JL 27-MAY-2016
        {
            try
            {
                if (m_CheckGateConfigurationChanges != null)
                    m_CheckGateConfigurationChanges.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

                m_Logger.AddActions("CurrApp Memory:", GC.GetTotalMemory(false).ToString());

                HumpyDetail mTempNewHD = new HumpyDetail();
                
                //JL IN If there is any configuration changed, restart the gate.
                if (m_DAL_Local.SHS_IsHumpyConfigueChanged(m_HumpyDetail, ref mTempNewHD))
                {
                    //Restart it???
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                m_Logger.AddErrors("OnCheckGateConfigurationChanges", ex.StackTrace.ToString());
            }
            finally
            {
                //GC.Collect();// GC.WaitForPendingFinalizers();//IN JL 13-OCT-2013
                GC.Collect(); GC.WaitForPendingFinalizers();//IN JL 13-OCT-2013

                if (m_CheckGateConfigurationChanges != null)
                    m_CheckGateConfigurationChanges.Change(SmartHumpyController.Properties.Settings.Default.Sys_CheckHumpyConfigurationChanges_ms, System.Threading.Timeout.Infinite);
            }
        }


        private void OnStartupTimer(object AObject)
        {
            if (mStartupTimer != null)
            {
                mStartupTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            }

            if (!CheckDBConnection() && m_MainFlag)
            {
                int zDelayInterval = 5000;//5Seconds
                mStartupTimer.Change(zDelayInterval, System.Threading.Timeout.Infinite);
            }
            else
            {
                mStartupTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                //mStartupTimer = null;

                if (m_MainFlag)
                    MainStartUp(AObject, null);
            }

            GC.Collect();// GC.WaitForPendingFinalizers();//IN JL 13-OCT-2013
        }

        /// <summary>
        /// IN 24-SEP-2013: Check the data integraty
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CheckDBConnection()
        {
            bool zRet = true;
            try
            {
                m_DAL_Local = new HumpyDAL(SmartHumpyController.Properties.Settings.Default.Sys_DBConnectionLocal);
                m_HumpyDetail = m_DAL_Local.SHS_GetHumpySettings();
            }
            catch (Exception ex)
            {
                m_Logger.Add("CheckDBConnection-Waiting for DB to Start up:" + ex.ToString());
                ex.ToString();
                zRet = false;
            }

            return zRet;
        }

        private void MainStartUp(object sender, EventArgs e)
        {
            if (mStartupTimer != null)
            {
                mStartupTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                //mStartupTimer = null;
            }

            m_DAL_Local = null;
            m_DAL_Local = new HumpyDAL(SmartHumpyController.Properties.Settings.Default.Sys_DBConnectionLocal);

            //start the thread
            
            //Initialize the gate configuration changes //IN JL 08-OCT-2013
            if (m_CheckGateConfigurationChanges != null)
                m_CheckGateConfigurationChanges.Change(SmartHumpyController.Properties.Settings.Default.Sys_CheckHumpyConfigurationChanges_ms, System.Threading.Timeout.Infinite);


        }



    }
}
