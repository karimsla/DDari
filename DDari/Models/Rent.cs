using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDari.Models
{
    public class Rent : Property
    {
        // public Utilisateur user{get;set;}
        public float pricePerMonth { get; set; }
        public float pricePerDay { get; set; }
    }
}