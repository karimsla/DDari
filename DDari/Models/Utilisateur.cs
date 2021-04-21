using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDari.Models
{
    public class Utilisateur
    {
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public DateTime created { get; set; }
        private bool enabled { get; set; }
        private String picture { get; set; }
        private String phone { get; set; }
        private String resetPasswordToken { get; set; }

    }
}