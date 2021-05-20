using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDari.ViewModels
{
    public class CreateContractViewModel
    {
        public IEnumerable<long> Users { get; set; }
        public IEnumerable<int> Properties { get; set; }
        public int id_user { get; set; }
        public int id_property { get; set; }
        public string details { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime date { get; set; }
    }
}