using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDari.Models
{
    public class Delivery
    {
        public int deliveryId { get; set; }
        public string destination { get; set; }
        public string date { get; set; }
        public string deliveryState { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public long? deliverymanId { get; set; }
        public string dateJson { get; set; }
        public int orderId { get; set; }
        public DeliveryMan deliveryMan { get; set; }
    }
}