﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace sendill_client
{
    [Serializable]
    public class dtoTour
    {
        public dtoTour()
        {
            tdatetime = DateTime.Now;
            time = tdatetime.ToShortTimeString();
        }
        public int id { get; set; }
        public int idcustomer { get; set; }
        public int idcar { get; set; }
        public int idpin { get; set; }
        public DateTime tdatetime { get; set; }
        public string time { get; set; }
        public string tcustomer { get; set; }
        public string taddress { get; set; }
        public string tcontact { get; set; }
        public string tphone { get; set; }
        public string tnote { get; set; }
        public bool isdel { get; set; }
        public int carsize { get; set; }
        public bool? car1 { get; set; }          //Lyfta     
        public bool? car2 { get; set; }          //Dregur     
        public bool? car3 { get; set; }          //Krókur
        public bool? car4 { get; set; }          //Þungaburður
        public bool? car5 { get; set; }          //Rafmagnstjakkur

    }
}
