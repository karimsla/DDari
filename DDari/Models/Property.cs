using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDari.Models
{
    public class Property
    {
        public int id_prop { get; set; }
        public PropertyType type { get; set; }
        public int nbrooms { get; set; }
        public float surface { get; set; }
        public float superficie { get; set; }
        public float loyer { get; set; }
        public float prix { get; set; }
        public string adress { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public int zipCode { get; set; }
    }
}