using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDari.Models
{
    public class Contract_buy
    {
        public int id_user { get; set; }
        public int id_property { get; set; }
        public string details { get; set; }
        public DateTime date { get; set; }
    }
}