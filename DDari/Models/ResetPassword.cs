using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDari.Models
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "It must be between 5 and 50 characters", MinimumLength = 5)]
        public string password { get; set; }
        
        public string token { get; set; }
    }
}