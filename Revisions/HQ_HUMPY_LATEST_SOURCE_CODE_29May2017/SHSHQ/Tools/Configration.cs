
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections.Specialized;

using System.Windows.Forms;

namespace SHSHQ.Tools
{
  public class Configration
  {
    private static NameValueCollection m_settings;
    private static string m_settingsPath;

    static Configration()
    {
      //OUT JL 13-DEC-2013: this is for handheld(WM) 
      //m_settingsPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

      m_settingsPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
      m_settingsPath += @"\Tools\systemBCDS.xml";

      try
      {
          //MessageBox.Show(m_settingsPath.ToString());

          if (File.Exists(m_settingsPath))
          {
              System.Xml.XmlDocument xdoc = new XmlDocument();
              xdoc.Load(m_settingsPath);
              XmlElement root = xdoc.DocumentElement;

              System.Xml.XmlNodeList nodeList = root.ChildNodes.Item(0).ChildNodes;
              // Add settings to the NameValueCollection.
              m_settings = new NameValueCollection();
              m_settings.Add("RFID_ReaderIP", nodeList.Item(0).Attributes["value"].Value);
              m_settings.Add("RFID_ReaderTCPIPPort", nodeList.Item(1).Attributes["value"].Value);
              m_settings.Add("RFID_IgnoreAntPort", nodeList.Item(2).Attributes["value"].Value);
              m_settings.Add("Printer_PrinterName", nodeList.Item(3).Attributes["value"].Value);
              m_settings.Add("Scale_COMPortName", nodeList.Item(4).Attributes["value"].Value);
              m_settings.Add("Scale_Baudrate", nodeList.Item(5).Attributes["value"].Value);
              m_settings.Add("Scale_CarriageReturnFlag", nodeList.Item(6).Attributes["value"].Value);
              m_settings.Add("KPI_CheckInterval", nodeList.Item(7).Attributes["value"].Value);
              m_settings.Add("KPI_FilePath", nodeList.Item(8).Attributes["value"].Value);
              m_settings.Add("KPI_KPITargetPrefix", nodeList.Item(9).Attributes["value"].Value);
              m_settings.Add("Cloud_StaticURL", nodeList.Item(10).Attributes["value"].Value);
              m_settings.Add("Cloud_LicenceKey", nodeList.Item(11).Attributes["value"].Value);
              m_settings.Add("TagList_HaventSeenTagX", nodeList.Item(12).Attributes["value"].Value);
              m_settings.Add("TagList_CheckInterval", nodeList.Item(13).Attributes["value"].Value);
              m_settings.Add("Cloud_CompanyId", nodeList.Item(14).Attributes["value"].Value);
              m_settings.Add("Cloud_DeviceId", nodeList.Item(15).Attributes["value"].Value);
              m_settings.Add("SSD_LoadItemTypeId", nodeList.Item(16).Attributes["value"].Value);
              m_settings.Add("Cloud_CompanyName", nodeList.Item(17).Attributes["value"].Value);
              m_settings.Add("Cloud_Token", nodeList.Item(18).Attributes["value"].Value);
              m_settings.Add("Cloud_Username", nodeList.Item(19).Attributes["value"].Value);
              m_settings.Add("Category_FFPA_Id", nodeList.Item(20).Attributes["value"].Value);
              m_settings.Add("Category_FFPB_Id", nodeList.Item(21).Attributes["value"].Value);
              m_settings.Add("Category_FFPAB_Id", nodeList.Item(22).Attributes["value"].Value);
              m_settings.Add("Category_FFPO_Id", nodeList.Item(23).Attributes["value"].Value);
              m_settings.Add("Category_CRYOA_Id", nodeList.Item(24).Attributes["value"].Value);
              m_settings.Add("Category_CRYOB_Id", nodeList.Item(25).Attributes["value"].Value);
              m_settings.Add("Category_CRYOC_Id", nodeList.Item(26).Attributes["value"].Value);
              m_settings.Add("Category_CRYOD_Id", nodeList.Item(27).Attributes["value"].Value);
              m_settings.Add("AssetType_BloodBag_Id", nodeList.Item(28).Attributes["value"].Value);
              m_settings.Add("AssetType_MetalBasket_Id", nodeList.Item(29).Attributes["value"].Value);
              m_settings.Add("AssetType_PlasticTote_Id", nodeList.Item(30).Attributes["value"].Value);
              m_settings.Add("AssetType_Pallet_Id", nodeList.Item(31).Attributes["value"].Value);
              m_settings.Add("AssetType_Trolley_Id", nodeList.Item(32).Attributes["value"].Value);
              m_settings.Add("AssetType_Vehicle_Id", nodeList.Item(33).Attributes["value"].Value);
              m_settings.Add("UserStatusId_Received", nodeList.Item(34).Attributes["value"].Value);
              m_settings.Add("UserStatusId_Approved", nodeList.Item(35).Attributes["value"].Value);
              m_settings.Add("UserStatusId_AwaitingResults", nodeList.Item(36).Attributes["value"].Value);
              m_settings.Add("UserStatusId_QAHold", nodeList.Item(37).Attributes["value"].Value);
              m_settings.Add("UserStatusId_Disposal", nodeList.Item(38).Attributes["value"].Value);
              m_settings.Add("GUI_DataGridViewUpdateInterval", nodeList.Item(39).Attributes["value"].Value);
              m_settings.Add("Weight_PlasticToteWeight_kg", nodeList.Item(40).Attributes["value"].Value);
              m_settings.Add("Weight_MetalBasketWeight_kg", nodeList.Item(41).Attributes["value"].Value);
              m_settings.Add("Weight_OffsetPercent", nodeList.Item(42).Attributes["value"].Value);
              m_settings.Add("Category_NA_Id", nodeList.Item(43).Attributes["value"].Value);
              m_settings.Add("MoveLocList", nodeList.Item(44).Attributes["value"].Value);
              m_settings.Add("UserStatusId_RecallPending", nodeList.Item(45).Attributes["value"].Value);
              m_settings.Add("SSD_IncludeChildLocation", nodeList.Item(46).Attributes["value"].Value);
          }
          else
          {
              throw new FileNotFoundException(m_settingsPath + " could not be found.");
          }
      }
      catch (Exception ex)
      {
          MessageBox.Show("Error:" + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
      }
    }

    public static void Update()
    {
      XmlTextWriter tw = new XmlTextWriter(m_settingsPath, System.Text.UTF8Encoding.UTF8);
      tw.WriteStartDocument();
      tw.WriteStartElement("configuration");
      tw.WriteStartElement("appSettings");
      tw.Formatting = Formatting.Indented;

      for (int i = 0; i < m_settings.Count; ++i)
      {
          if (m_settings.GetKey(i) != "RFIDVersion")//IN JL 03-JUN-13
          {
              tw.WriteStartElement("add");
              tw.WriteStartAttribute("key", string.Empty);
              tw.WriteRaw(m_settings.GetKey(i));
              tw.WriteEndAttribute();

              tw.WriteStartAttribute("value", string.Empty);
              tw.WriteRaw(m_settings.Get(i));
              tw.WriteEndAttribute();
              tw.WriteEndElement();
          }
      }

      tw.WriteEndElement();
      tw.WriteEndElement();
      tw.Close();
    }

    public static string SSD_IncludeChildLocation
    {
        get { return m_settings.Get("SSD_IncludeChildLocation"); }
        set { m_settings.Set("SSD_IncludeChildLocation", value); }
    }

    public static string UserStatusId_RecallPending
    {
        get { return m_settings.Get("UserStatusId_RecallPending"); }
        set { m_settings.Set("UserStatusId_RecallPending", value); }
    }

    public static string UserStatusId_Received
    {
        get { return m_settings.Get("UserStatusId_Received"); }
        set { m_settings.Set("UserStatusId_Received", value); }
    }
    public static string UserStatusId_Approved
    {
        get { return m_settings.Get("UserStatusId_Approved"); }
        set { m_settings.Set("UserStatusId_Approved", value); }
    }
    public static string UserStatusId_AwaitingResults
    {
        get { return m_settings.Get("UserStatusId_AwaitingResults"); }
        set { m_settings.Set("UserStatusId_AwaitingResults", value); }
    }
    public static string UserStatusId_QAHold
    {
        get { return m_settings.Get("UserStatusId_QAHold"); }
        set { m_settings.Set("UserStatusId_QAHold", value); }
    }
    public static string UserStatusId_Disposal
    {
        get { return m_settings.Get("UserStatusId_Disposal"); }
        set { m_settings.Set("UserStatusId_Disposal", value); }
    }

    public static string Cloud_CompanyName
    {
        get { return m_settings.Get("Cloud_CompanyName"); }
        set { m_settings.Set("Cloud_CompanyName", value); }
    }

    public static string RFID_ReaderIP
    {
        get { return m_settings.Get("RFID_ReaderIP"); }
        set { m_settings.Set("RFID_ReaderIP", value); }
    }

    public static string RFID_ReaderTCPIPPort
    {
        get { return m_settings.Get("RFID_ReaderTCPIPPort"); }
        set { m_settings.Set("RFID_ReaderTCPIPPort", value); }
    }
    public static string RFID_IgnoreAntPort
    {
        get { return m_settings.Get("RFID_IgnoreAntPort"); }
        set { m_settings.Set("RFID_IgnoreAntPort", value); }
    }

    public static string Printer_PrinterName
    {
        get { return m_settings.Get("Printer_PrinterName"); }
        set { m_settings.Set("Printer_PrinterName", value); }
    }

    public static string Scale_COMPortName
    {
        get { return m_settings.Get("Scale_COMPortName"); }
        set { m_settings.Set("Scale_COMPortName", value); }
    }
    public static string Scale_Baudrate
    {
        get { return m_settings.Get("Scale_Baudrate"); }
        set { m_settings.Set("Scale_Baudrate", value); }
    }

    public static string Scale_CarriageReturnFlag
    {
        get { return m_settings.Get("Scale_CarriageReturnFlag"); }
        set { m_settings.Set("Scale_CarriageReturnFlag", value); }
    }
    public static string KPI_CheckInterval
    {
        get { return m_settings.Get("KPI_CheckInterval"); }
        set { m_settings.Set("KPI_CheckInterval", value); }
    }

    public static string KPI_FilePath
    {
        get { return m_settings.Get("KPI_FilePath"); }
        set { m_settings.Set("KPI_FilePath", value); }
    }
    public static string KPI_KPITargetPrefix
    {
        get { return m_settings.Get("KPI_KPITargetPrefix"); }
        set { m_settings.Set("KPI_KPITargetPrefix", value); }
    }
    public static string Cloud_StaticURL
    {
        get { return m_settings.Get("Cloud_StaticURL"); }
        set { m_settings.Set("Cloud_StaticURL", value); }
    }
    public static string Cloud_LicenceKey
    {
        get { return m_settings.Get("Cloud_LicenceKey"); }
        set { m_settings.Set("Cloud_LicenceKey", value); }
    }
    public static string TagList_HaventSeenTagX
    {
        get { return m_settings.Get("TagList_HaventSeenTagX"); }
        set { m_settings.Set("TagList_HaventSeenTagX", value); }
    }
    public static string TagList_CheckInterval
    {
        get { return m_settings.Get("TagList_CheckInterval"); }
        set { m_settings.Set("TagList_CheckInterval", value); }
    }
    public static string Cloud_CompanyId
    {
        get { return m_settings.Get("Cloud_CompanyId"); }
        set { m_settings.Set("Cloud_CompanyId", value); }
    }
    public static string Cloud_DeviceId
    {
        get { return m_settings.Get("Cloud_DeviceId"); }
        set { m_settings.Set("Cloud_DeviceId", value); }
    }

    public static string SSD_LoadItemTypeId
    {
        get { return m_settings.Get("SSD_LoadItemTypeId"); }
        set { m_settings.Set("SSD_LoadItemTypeId", value); }
    }

    public static string Cloud_Token
    {
        get { return m_settings.Get("Cloud_Token"); }
        set { m_settings.Set("Cloud_Token", value); }
    }
      
    public static string Cloud_Username
    {
        get { return m_settings.Get("Cloud_Username"); }
        set { m_settings.Set("Cloud_Username", value); }
    }

    public static string Category_FFPA_Id
    {
        get { return m_settings.Get("Category_FFPA_Id"); }
        set { m_settings.Set("Category_FFPA_Id", value); }
    }
        public static string Category_FFPB_Id
    {
        get { return m_settings.Get("Category_FFPB_Id"); }
        set { m_settings.Set("Category_FFPB_Id", value); }
    }
        public static string Category_FFPAB_Id
    {
        get { return m_settings.Get("Category_FFPAB_Id"); }
        set { m_settings.Set("Category_FFPAB_Id", value); }
    }
        public static string Category_FFPO_Id
    {
        get { return m_settings.Get("Category_FFPO_Id"); }
        set { m_settings.Set("Category_FFPO_Id", value); }
    }
      public static string Category_CRYOA_Id
    {
        get { return m_settings.Get("Category_CRYOA_Id"); }
        set { m_settings.Set("Category_CRYOA_Id", value); }
    }
      public static string Category_CRYOB_Id
    {
        get { return m_settings.Get("Category_CRYOB_Id"); }
        set { m_settings.Set("Category_CRYOB_Id", value); }
    }
     public static string Category_CRYOAB_Id
    {
        get { return m_settings.Get("Category_CRYOAB_Id"); }
        set { m_settings.Set("Category_CRYOAB_Id", value); }
    } 
    public static string Category_CRYOO_Id
    {
        get { return m_settings.Get("Category_CRYOO_Id"); }
        set { m_settings.Set("Category_CRYOO_Id", value); }
    }

    public static string Category_NA_Id
    {
        get { return m_settings.Get("Category_NA_Id"); }
        set { m_settings.Set("Category_NA_Id", value); }
    }

    public static string AssetType_BloodBag_Id
    {
        get { return m_settings.Get("AssetType_BloodBag_Id"); }
        set { m_settings.Set("AssetType_BloodBag_Id", value); }
    }

      public static string AssetType_MetalBasket_Id
    {
        get { return m_settings.Get("AssetType_MetalBasket_Id"); }
        set { m_settings.Set("AssetType_MetalBasket_Id", value); }
    }

            public static string AssetType_PlasticTote_Id
    {
        get { return m_settings.Get("AssetType_PlasticTote_Id"); }
        set { m_settings.Set("AssetType_PlasticTote_Id", value); }
    }
            public static string AssetType_Pallet_Id
    {
        get { return m_settings.Get("AssetType_Pallet_Id"); }
        set { m_settings.Set("AssetType_Pallet_Id", value); }
    }
            public static string AssetType_Trolley_Id
    {
        get { return m_settings.Get("AssetType_Trolley_Id"); }
        set { m_settings.Set("AssetType_Trolley_Id", value); }
    }
            public static string AssetType_Vehicle_Id
    {
        get { return m_settings.Get("AssetType_Vehicle_Id"); }
        set { m_settings.Set("AssetType_Vehicle_Id", value); }
    }

     public static string GUI_DataGridViewUpdateInterval
    {
        get { return m_settings.Get("GUI_DataGridViewUpdateInterval"); }
        set { m_settings.Set("GUI_DataGridViewUpdateInterval", value); }
    }

    public static string Weight_PlasticToteWeight_kg
    {
        get { return m_settings.Get("Weight_PlasticToteWeight_kg"); }
        set { m_settings.Set("Weight_PlasticToteWeight_kg", value); }
    }

    public static string Weight_MetalBasketWeight_kg
    {
        get { return m_settings.Get("Weight_MetalBasketWeight_kg"); }
        set { m_settings.Set("Weight_MetalBasketWeight_kg", value); }
    }

     public static string Weight_OffsetPercent
    {
        get { return m_settings.Get("Weight_OffsetPercent"); }
        set { m_settings.Set("Weight_OffsetPercent", value); }
    }

     public static string MoveLocList
    {
        get { return m_settings.Get("MoveLocList"); }
        set { m_settings.Set("MoveLocList", value); }
    }

  }
}
