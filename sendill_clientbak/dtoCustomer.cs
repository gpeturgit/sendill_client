using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace sendill_client
{
    [Serializable]
    public class dtoCustomer
    {
        public int id { get; set; }
        public string address { get; set; }
        public string name { get; set; }
        public string streetnr { get; set; }
        public double areacode { get; set; }
        public string phone1 { get; set; }
        public string fax { get; set; }
        public string phone2 { get; set; }
        public string umbaf { get; set; }
        public string mobile { get; set; }
        public string kt { get; set; }
        public string number { get; set; }
        public string area { get; set; }
        public string email { get; set; }
        public string cumbaf { get; set; }
        public double svaedi { get; set; }
        public string url { get; set; }
        public bool isdel { get; set; }
    }
}
