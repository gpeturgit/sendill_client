using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sendill_client
{
    [Serializable]
    public class appSysSettings
    {
        public string CODE { get; set; }
        public string TYPE { get; set; }
        public string VALUE { get; set; }
    }
}
