using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sendill_client
{
    [Serializable]
    public class dtoTour
    {
        public dtoTour()
        {
            tdatetime = DateTime.Now;
        }
        public int id { get; set; }
        public int idcustomer { get; set; }
        public int idcar { get; set; }
        public DateTime tdatetime { get; set; }
        public string tcustomer { get; set; }
        public string taddress { get; set; }
        public string tcontact { get; set; }
        public string tphone { get; set; }
        public string tnote { get; set; }
        public bool isdel { get; set; }
    }
}
