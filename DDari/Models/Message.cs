using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDari.Models
{
    public class Message
    {
        public int id { get; set; }
        public string content { get; set; }
        public DateTime dateTime { get; set; }
        public int sender { get; set; }
        public ChatRoom chatRoom { get; set; }

    }
}