using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace sendill_client
{
    [Table("tbl_queue")]
    public class dtoMessage
    {
        public int id { get; set; }
        public int message_type { get; set; }
        public int from_id { get; set; }
        public int to_id { get; set; }
        public DateTime send_from_date { get; set; }
        public DateTime send_to_date { get; set; }
        public bool delivered { get; set; }
        public string message_text { get; set; }

    }
}
