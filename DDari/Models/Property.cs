using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDari.Models
{
    public class Property
    {
        public int id_prop { get; set; }
        [Required]
        [StringLength(20)]
        public string title { get; set; }
        public PropertyType type { get; set; }
        public int nbrooms { get; set; }
        [Required]
        public float surface { get; set; }
        [Required]
        public float superficie { get; set; }
        [Required]
        public float loyer { get; set; }
     
        public string image { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public float prix { get; set; }
        [Required]
        public string adress { get; set; }
        [Required]
        public string state { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public int zipCode { get; set; }
    }
}