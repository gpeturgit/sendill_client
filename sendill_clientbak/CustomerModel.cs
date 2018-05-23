using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sendill_client
{
    [Serializable]
    public class CustomerModel
    {
        public string id { get; set; }
        public string customer { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
    }
}
