using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDari.Models
{
    public class Panier
    {

        public int idPanier { get; set; }
        public DateTime dateMajPanier { get; set; }
      
        public double sommeTotale { get; set; }
     
        public string etatPanier { get; set; }
     

    }

}