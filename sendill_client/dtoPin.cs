using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sendill_client
{
    [Serializable]
    public class dtoPin
    {
        public int id { get; set; }
        public int idpin { get; set; }
        public int idcar { get; set; }
        public bool pbreak { get; set; }
        public string pcarcode { get; set; }
        public int pline { get; set; }
        public DateTime dtonpin { get; set; }
        public DateTime dtoffpinn { get; set; }
        public bool IsSelected { get; set; }
        public string phone { get; set; }
        public string owner { get; set; }
        public int carsize { get; set; }    
        public bool? car1 { get; set; }          //Lyfta     
        public bool? car2 { get; set; }          //Dregur     
        public bool? car3 { get; set; }          //Krókur
        public bool? car4 { get; set; }          //Þungaburður
        public bool? car5 { get; set; }          //Rafmagnstjakkur
        public bool? car6 { get; set; }
        public bool? car7 { get; set; }
    }
}
