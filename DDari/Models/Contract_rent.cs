using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDari.Models
{
    public class Contract_rent
    {
        public int id_user { get; set; }
        public int id_property { get; set; }
        
        public DateTime dateDebut { get; set; }
        public DateTime dateFin { get; set; }
        public string details { get; set; }
        public bool rented { get; set; }

    }
}