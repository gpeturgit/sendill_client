using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sendill_client
{
    [Serializable]
    public class dtooCars
    {
        public int id { get; set; }
        public Int16? stationid { get; set; }
        public string carnumber { get; set; }
        public string code { get; set; }
        public bool listed { get; set; }
        public string carname { get; set; }
        public bool? car1 { get; set; }
        public bool? car2 { get; set; }
        public bool? car3 { get; set; }
        public bool? car4 { get; set; }
        public bool? car5 { get; set; }
        public bool? car6 { get; set; }
        public bool? car7 { get; set; }
        public bool? car8 { get; set; }
        public bool? car9 { get; set; }
        public bool? car10 { get; set; }
        public bool? car11 { get; set; }
        public bool? car12 { get; set; }
        public double? length { get; set; }
        public double? backdoorlength { get; set; }
        public double? backdoorheight { get; set; }
        public double? sidedoorlength { get; set; }
        public double? sidedoorheight { get; set; }
        public double? weightlimit { get; set; }
        public double? liftsize { get; set; }
        public double? volume { get; set; }
        public double? width { get; set; }
        public string model { get; set; }
        public double? maxcarry { get; set; }
        public string owner { get; set; }
        public string kt { get; set; }
        public string address { get; set; }
        public string town { get; set; }
        public string postcode { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string driver { get; set; }
        public string dkt { get; set; }
        public string daddress { get; set; }
        public string dtown { get; set; }
        public string dpostcode { get; set; }
        public string dphone { get; set; }
        public string dmobile { get; set; }
        public double? heightofbox { get; set; }
        public bool isdel { get; set; }
        public bool isChanged { get; set; }
        public int size { get; set; }

    }
}
