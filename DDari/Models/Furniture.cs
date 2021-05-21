using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDari.Models
{
        public class Furniture
        {
            public int id_fur { get; set; }
        [Required]
        public int price { get; set; }
        [Required]
        public string description { get; set; }
      
        public string picture { get; set; }
        [Required]
        public string title { get; set; }

            public FurnitureType type { get; set; }

        }
    }
