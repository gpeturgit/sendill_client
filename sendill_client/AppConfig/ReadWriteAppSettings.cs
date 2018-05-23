using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace sendill_client.AppConfig
{
    public class ReadWriteAppSettings
    {
        public string ReadOfficeDatabaseConnection()
        {
            string path = System.Windows.Forms.Application.StartupPath;
            path = path + "/AppConfig/AppSettings.json";
            using (StreamReader _stream = new StreamReader("C:/sendilloffice/AppConfig/AppSettings.json"))
            {
                var _res = _stream.ReadToEnd();
                List<AppSettings> _settings = JsonConvert.DeserializeObject<List<AppSettings>>(_res);
                var _rec = from setting in _settings
                            where setting.key== "OFFICE_DATABASE_CONNECTION"
                           select setting;

                var _colval = _settings[0];
                return _colval.value;
            }
        }
        public string ReadDefaultDatabaseConnection()
        {
            using (StreamReader _stream = new StreamReader("AppConfig/AppSettings.json"))
            {
                var _res = _stream.ReadToEnd();
                List<AppSettings> _settings = JsonConvert.DeserializeObject<List<AppSettings>>(_res);
                var _rec = from setting in _settings
                           where setting.key == "DEFAULT_DATABASE_CONNECTION"
                           select setting;

                var _colval = _settings[0];
                return _colval.value;
            }
        }
        public string ReadAzureDatabaseConnection()
        {
            using (StreamReader _stream = new StreamReader("AppConfig/AppSettings.json"))
            {
                var _res = _stream.ReadToEnd();
                List<AppSettings> _settings = JsonConvert.DeserializeObject<List<AppSettings>>(_res);
                var _rec = from setting in _settings
                           where setting.key == "AZURE_DATABASE_CONNECTION"
                           select setting;

                var _colval = _settings[0];
                return _colval.value;
            }
        }
        public string ReadNotifyServerUri()
        {
            using (StreamReader _stream = new StreamReader("AppConfig/AppSettings.json"))
            {
                var _res = _stream.ReadToEnd();
                List<AppSettings> _settings = JsonConvert.DeserializeObject<List<AppSettings>>(_res);
                var _rec = from setting in _settings
                           where setting.key == "AZURE_NOTIFY_SIGNALR_URI_DEV"
                           select setting;

                var _colval = _settings[0];
                return _colval.value;
            }
        }

        public string ReadNotifyServerUri()
        {
            using (StreamReader _stream = new StreamReader("AppConfig/AppSettings.json"))
            {
                var _res = _stream.ReadToEnd();
                List<AppSettings> _settings = JsonConvert.DeserializeObject<List<AppSettings>>(_res);
                var _rec = from setting in _settings
                           where setting.key == "AZURE_NOTIFY_SIGNALR_URI_DEV"
                           select setting;

                var _colval = _settings[0];
                return _colval.value;
            }
        }
    }
}
