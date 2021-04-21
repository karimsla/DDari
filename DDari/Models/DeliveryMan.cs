using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDari.Models
{
    public class DeliveryMan
    {
        public int utilisateurId { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string password { get; set; }
        public string picture { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public bool enabled { get; set; }
        public string region { get; set; }
 
    }
}