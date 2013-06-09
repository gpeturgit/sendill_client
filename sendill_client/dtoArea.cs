using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sendill_client
{
    [Serializable]
    public class dtoArea
    {
        public int id { get; set; }
        public string name { get; set; }
        public string note { get; set; }
    }
}
