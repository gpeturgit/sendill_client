using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sendill_client
{

    [Serializable()]
    public class DataStateLocal
    {
        public int id { get; set; }
        public string tablename { get; set; } 
        public bool haschanged { get; set; } 
        public DateTime modTime { get; set; }
    }
}
