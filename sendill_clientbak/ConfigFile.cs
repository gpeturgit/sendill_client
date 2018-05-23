using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace sendill_client
{
    public class ConfigFile
    {
        internal XPathDocument self;
        string file = @"C:\sendill\startconfdev.xml";
        string xml_apprun;
        string s_node;
        public string s_height;
        public string s_width;
        string s_appfolder =@"C:\sendill\";

        public List<dtoListConfig> GetAreaList()
        {
            XElement xmlDoc = XElement.Load(file);
            var listconf =from lconf in xmlDoc.Descendants("List")
    		select new dtoListConfig()
    		{
    			id = lconf.Element("id").Value,
    			header = lconf.Element("Header").Value,
    			name = lconf.Element("Name").Value,
                tmpldata = lconf.Element("DataTemplate").Value
    		};
            return listconf.ToList();
        }

        public List<dtoListConfig> GetViewAreaList()
        {
            XElement xmlDoc = XElement.Load(file);
            var listconf = from lconf in xmlDoc.Descendants("ViewList")
                           select new dtoListConfig()
                           {
                               id = lconf.Element("id").Value,
                               header = lconf.Element("Header").Value,
                               name = lconf.Element("Name").Value,
                               tmpldata = lconf.Element("DataTemplate").Value
                           };
            return listconf.ToList();
        }

        public string GetListTemlate(string s_id)
        {
            ConfigFile file_config = new ConfigFile();
            var _rec = file_config.GetAreaList().FirstOrDefault(c => c.id == s_id);
            return _rec.tmpldata;
        }

        public string GetListViewtTemlate(string s_id)
        {
            ConfigFile file_config = new ConfigFile();
            var _rec = file_config.GetViewAreaList().FirstOrDefault(c => c.id == s_id);
            return _rec.tmpldata;
        }
        
        
        public void GetMainScreenRes()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(file);
            //xml.LoadXml(file); //myXmlString is the xml file in string //copying xml to string: string myXmlString = xmldoc.OuterXml.ToString();
            XmlNodeList xnList = xml.SelectNodes("/Config[@*]/ScreenRes");
            foreach (XmlNode xn in xnList)
            {
                XmlNode example = xn.SelectSingleNode("MainScreen");
                if (example != null)
                {
                    s_height = example["Height"].InnerText;
                    s_width = example["Width"].InnerText;
                }
            }
        }

        public double GetViewScreenWidth()
        {
            XPathDocument document = new XPathDocument(file);
            XPathNavigator navigator = document.CreateNavigator();

            XPathExpression query = navigator.Compile("//ViewScreenWidth");

            XPathNavigator node = navigator.SelectSingleNode(query);

            return Convert.ToDouble(node.InnerXml);
        }

        public double GetViewScreenHeight()
        {
            XPathDocument document = new XPathDocument(file);
            XPathNavigator navigator = document.CreateNavigator();

            XPathExpression query = navigator.Compile("//ViewScreenHeight");

            XPathNavigator node = navigator.SelectSingleNode(query);

            return Convert.ToDouble(node.InnerXml);
        }


        public string GetAppPath()
        {
                self =new XPathDocument(file);
                string s_xpath;
                foreach (XPathNavigator child in self.CreateNavigator().Select("Config/*"))
                {
                    xml_apprun = child.InnerXml.ToString();
                }

                switch (xml_apprun)
                {
                    case "Dev":
                        s_xpath = "Config/Dev/";
                        break;
                    case "Global":
                        s_xpath = "Config/Global/";
                        break;
                    case "Test":
                        s_xpath = "Config/Test/";
                        break;
                    default:
                        s_xpath = "Config/Global/";
                        break;

                }
                return s_xpath;
        }

        public string GetAppConfigSetting()
        {
            //ConfigFile conf = new ConfigFile();
            string ssetting ="C:\\Sendill\\DataFiles\\";
            return ssetting;

        }

        public string GetLocalBinFolder()
        {
            string s_xpath;
            self = new XPathDocument(file);
            s_xpath = GetAppPath();

            foreach (XPathNavigator child in self.CreateNavigator().Select(s_xpath+"LocalBinFolder"))
            {
                s_node = child.InnerXml.ToString();
            }
            return s_node;
        }



        public string GetDataConnection(string sdbtype)
        {
            string xml_node, sdatafile, sdbconn;


            switch (sdbtype)
            {
                case "Jet":
                    xml_node = "LocalPartialJetConnectionString";
                    sdatafile = GetAppConfigSetting()+"dbsendill.mdb";
                    break;
                case "sqlserver":
                    xml_node = "connsql";
                    sdatafile = "";
                    break;
                case "azure":
                    xml_node = "connazure";
                    sdatafile = "";
                    break;
                default:
                    xml_node = "conndb";
                    sdatafile = "";
                    break;
            }

            string s_xpath;
            self = new XPathDocument(file);
            s_xpath = GetAppPath();


            foreach (XPathNavigator child in self.CreateNavigator().Select(s_xpath + xml_node))
            {
                s_node = child.InnerXml.ToString();
            }
            sdbconn = s_node + sdatafile;
            return sdbconn;
        }
    }
}