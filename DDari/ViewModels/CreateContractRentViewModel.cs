using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDari.ViewModels
{
    public class CreateContractRentViewModel
    {
        public IEnumerable<long> Users { get; set; }
        public IEnumerable<int> Properties { get; set; }
        public DateTime dateDebut { get; set; }
        public DateTime dateFin { get; set; }
        public string details { get; set; }
        public bool rented { get; set; }
    }
}