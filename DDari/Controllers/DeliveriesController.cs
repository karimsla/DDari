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
  
            var task = Task.Run(async () => await deliveriesService.ListDeliveries());
            
            return View(task.Result);
        }

        public ActionResult Create()
        {

            return View();

        }
        [HttpPost]
            public ActionResult Create(DeliveryMan dm)
        {
            if (ModelState.IsValid)
            {
                var task = Task.Run(async () => await deliveriesService.AddDmAsync(dm));
                return RedirectToAction("index");

            }

            return View(dm);
        }
        public ActionResult listdm()
        {
            var task = Task.Run(async () => await deliveriesService.FindAllDm());

            return View(task.Result);

        }
        [HttpPost]
        public ActionResult listdm(Delivery dm)
        {
            if (ModelState.IsValid)
            {
             

            }

            return View();
        }

        public ActionResult assign()
        {
          

            return View();
        }

        [HttpPost]
        public ActionResult assign(Delivery delivery)
        {
            if (ModelState.IsValid)
            {


            }

            return View();
        }
    }
}