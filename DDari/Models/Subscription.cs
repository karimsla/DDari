using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDari.Models
{
    public class Subscription
    {
        public int id { get; set; }
        public String title { get; set; }

        public float price { get; set; }
        public String description { get; set; }


    }
}