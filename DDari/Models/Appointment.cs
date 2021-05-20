using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDari.Models
{
    public class Appointment
    {
        public int appointmentId { get; set; }
        public string appointmentDate { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string appointmentType { get; set; }
        public int ownerId { get; set; }
        public int customerId { get; set; }
        public int? agentId { get; set; }

        public string custname { get; set; }
    }
}