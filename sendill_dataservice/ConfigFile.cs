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


    //    DirectoryInfo outputFolder = ConfigFile.Read.Global.OutputFolder;
    //ConfigFile.Write(file => file.Global.OutputFolder = outputFolder);


    public class ConfigFile
    {
        internal XPathDocument self;
        string file = @"C:\sendill\startconf.xml";
        string xml_apprun;
        string s_node;
        string s_appfolder = Application.StartupPath;

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
            string ssetting = Application.StartupPath + "\\DataFiles\\";
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