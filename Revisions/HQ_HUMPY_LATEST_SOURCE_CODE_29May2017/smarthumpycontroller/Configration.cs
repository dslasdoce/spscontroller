
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections.Specialized;

namespace SmartHumpyController
{
  class Configration
  {
    private static NameValueCollection m_settings;
    private static string m_settingsPath;

    static Configration()
    {
      //OUT JL 13-DEC-2013: this is for handheld(WM) 
      //m_settingsPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

      m_settingsPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
      m_settingsPath += @"\systemBCDS.xml";

       if (File.Exists(m_settingsPath))
      {
        System.Xml.XmlDocument xdoc = new XmlDocument();
        xdoc.Load(m_settingsPath);
        XmlElement root = xdoc.DocumentElement;

        System.Xml.XmlNodeList nodeList = root.ChildNodes.Item(0).ChildNodes;
        // Add settings to the NameValueCollection.
        m_settings = new NameValueCollection();
        m_settings.Add("bluetoothConfig", nodeList.Item(0).Attributes["value"].Value);
        m_settings.Add("COMPort", nodeList.Item(1).Attributes["value"].Value);

      }
      else
      {
        throw new FileNotFoundException(m_settingsPath + " could not be found.");
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

    public static string bluetoothConfig
    {
        get { return m_settings.Get("bluetoothConfig"); }
        set { m_settings.Set("bluetoothConfig", value); }
    }

    public static string COMPort
    {
        get { return m_settings.Get("COMPort"); }
        set { m_settings.Set("COMPort", value); }
    }
  }
}
