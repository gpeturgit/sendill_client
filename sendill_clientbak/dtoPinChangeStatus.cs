using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sendill_client
{
    public class dtoPinChangeStatus
    {

        public int idpin { get; set; }
        public bool update { get; set; }
        public DateTime lastchange { get; set; }
        public DateTime lastupdate { get; set; }

    } 
}
