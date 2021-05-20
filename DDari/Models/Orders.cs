using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDari.Models
{
    public class Orders
    {
        public int id_o { get; set; }
        public DateTime datecommande { get; set; }
        public int? idc { get; set; }
        public int idf { get; set; }
        public int? fur { get; set; }
    }
}