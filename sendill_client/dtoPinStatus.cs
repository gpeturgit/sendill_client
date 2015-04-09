using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sendill_client
{
    [Serializable]
    public class dtoPinStatus
    {
        public dtoPinStatus()
        {
            logtime = DateTime.Now;
        }
        public int carid { get; set; }
        public int pinid { get; set; }
        public int pinpos { get; set; }
        public byte onbreak { get; set; }
        public DateTime logtime { get; set; }
    }
}
