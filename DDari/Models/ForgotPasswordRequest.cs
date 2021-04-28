using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDari.Models
{
    public class ForgotPasswordRequest
    {

        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address")]
        [Required(ErrorMessage = "This field is required")]
        public string email { get; set; }
    }
}