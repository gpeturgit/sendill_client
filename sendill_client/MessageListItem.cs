using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sendill_client
{
    public class MessageListItem:dtoMessage
    {
        public MessageListItem()
        {
            loadfinished = false;
            messagebutton = true;
            tablebutton = true;
            loadingindicatoron = false;
        }
        public bool loadfinished { get; set; }
        public bool messagebutton { get; set; }
        public bool tablebutton { get; set; }
        public bool loadingindicatoron { get; set; }
    }
}
