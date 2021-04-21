using DDari.Models;
using DDari.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class DeliveriesController : Controller
    {
        static ServiceDelivery deliveriesService = null;
        // GET: Deliveries
        public DeliveriesController()
        {
            deliveriesService = new ServiceDelivery();
        }
            public ActionResult Index()
        {
            DeliveryMan dm = new DeliveryMan();
     
            var task = Task.Run(async () => await deliveriesService.FindAllDm());
            
            return View(task.Result);
        }
    }
}