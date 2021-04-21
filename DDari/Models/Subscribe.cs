using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDari.Models
{
    public class Subscribe
    {
        public int id { get; set; }
        public DateTime DateD { get; set; }
        public DateTime DateF { get; set; }

        public Subscription subscriptions { get; set; }

        //public Customer customers { get; set; }
    }
}