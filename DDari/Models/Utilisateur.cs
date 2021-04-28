using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace DDari.Models
{
    public class Utilisateur
    {
        public long utilisateurId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [StringLength(21, ErrorMessage = "It must be between 2 and 21 characters", MinimumLength = 2)]
        public string username { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "This field is required")]
        [StringLength(21, ErrorMessage = "It must be between 2 and 21 characters", MinimumLength = 2)]
        public string firstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Last Name")]
        [StringLength(21, ErrorMessage = "It must be between 2 and 21 characters", MinimumLength = 2)]
        public string lastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "It must be between 5 and 50 characters", MinimumLength = 5)]
        public string password { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address")]
        [Required(ErrorMessage = "This field is required")]
        public string email { get; set; }
        public DateTime? created { get; set; }
        public bool enabled { get; set; }
        public string picture { get; set; }
        public string phone { get; set; }
        public string resetPasswordToken { get; set; }
        public List<Role> roles { get; set; }

        public override string ToString()
        {
            return "Utilisateur{" +
                "utilisateurId=" + utilisateurId +
                ", lastName='" + lastName + '\'' +
                ", firstName='" + firstName + '\'' +
                ", password='" + password + '\'' +
                ", picture='" + picture + '\'' +
                ", phone='" + phone + '\'' +

                ", username='" + username + '\'' +
                ", email='" + email + '\'' +
                ", created=" + created +
                                ", role=" + roles;

        }
    }
}