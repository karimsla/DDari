using DDari.Models;
using DDari.Services;
using DDari.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class DeliveriesController : Controller
    {
        static ServiceDelivery deliveriesService = null;
        static int id ;
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
                var t = task.Result;
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
            var task = Task.Run(async () => await deliveriesService.FindAllDm());
            List<DeliveryMan> dms = task.Result;

            var selectListdm = dms.Select(x =>
          new SelectListItem
          {
              Text = x.firstName + " " + x.lastName,
              Value = x.utilisateurId + "",
          });
            ViewBag.selectdm = selectListdm;
            var task2 = Task.Run(async () => await deliveriesService.FindAllOrds());
            List<Orders> ords = task2.Result;

            var selectList = ords.Select(x =>
    new SelectListItem
    {
        Text = x.id_o + "", Value = x.id_o + "",
    });
            ViewBag.select = selectList;

            return PartialView();
        }

        [HttpPost]
        public ActionResult assign(Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                var tas = Task.Run(async () => await deliveriesService.AddDeliveryAsync(delivery));
                var res = tas.Result;
            }
            var task = Task.Run(async () => await deliveriesService.FindAllDm());
            List<DeliveryMan> dms = task.Result;

            var selectListdm = dms.Select(x =>
          new SelectListItem
          {
              Text = x.firstName + " " + x.lastName,
              Value = x.utilisateurId + "",
          });
            ViewBag.selectdm = selectListdm;

            var task2 = Task.Run(async () => await deliveriesService.FindAllOrds());
            List<Orders> ords = task2.Result;

            var selectList = ords.Select(x =>
          new SelectListItem
          {
              Text = x.id_o + "",
              Value = x.id_o + "",
          });
            ViewBag.select = selectList;
            return PartialView();
        }
        [AllowCrossSiteJson]
        public JsonResult trackjson()
        {
            List<dynamic> ds=new List<dynamic>();
            var res = Task.Run(async () => await deliveriesService.ListDeliveryPerCust(1));
            List<Delivery> lsd = res.Result;
            foreach (Delivery deliv in lsd.Where(x => DateTime.Parse(x.date) == DateTime.Now.Date))
            {
                var response = Task.Run(async () => await deliveriesService.getRoute(deliv.destination));
                double Latitude = response.Result.Response.View[0].Result[0].Location.DisplayPosition.Latitude;
                double Lonitude = response.Result.Response.View[0].Result[0].Location.DisplayPosition.Longitude;
                dynamic d = new { Latitude = Latitude, Longitude = Lonitude,dmLat = deliv.latitude,dmLon =deliv.longitude,Address=deliv.destination };
                ds.Add(d);
            }
                return new JsonResult { Data = ds, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; 


            }
        public ActionResult updateDel(FormCollection collection)
        {
            int id = Int32.Parse(collection["id"]);
            int state = Int32.Parse(collection["state"]);
            var task = Task.Run(async () => await deliveriesService.UpdateDSAsync(id,state));
            task.Wait();
  
            return RedirectToAction("deliveriesDM");
        }
        
        [AllowCrossSiteJson]
        public JsonResult trackdm()
        {
            List<dynamic> ds = new List<dynamic>();
            var res = Task.Run(async () => await deliveriesService.ListDeliveryPerDm(3));
            List<Delivery> lsd = res.Result;
            foreach (Delivery deliv in lsd.Where(x=>DateTime.Parse(x.date)==DateTime.Now.Date))
            {
                var response = Task.Run(async () => await deliveriesService.getRoute(deliv.destination));
                double Latitude = response.Result.Response.View[0].Result[0].Location.DisplayPosition.Latitude;
                double Lonitude = response.Result.Response.View[0].Result[0].Location.DisplayPosition.Longitude;
                dynamic d = new { Latitude = Latitude, Longitude = Lonitude, dmLat = deliv.latitude, dmLon = deliv.longitude, Address = deliv.destination };
                ds.Add(d);
            }
            return new JsonResult { Data = ds, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

        [AllowCrossSiteJson]
        public void locate(double latitude,double longitude)
        {
            
            var res = Task.Run(async () => await deliveriesService.LocateAsync(id,latitude,longitude));
            res.Wait(); 
        }
        [AllowCrossSiteJson]
        public JsonResult routeTodest()
        {

            List<dynamic> ds = new List<dynamic>();
            var res = Task.Run(async () => await deliveriesService.ListDeliveryPerDm(3));

            List<Delivery> lsd = res.Result;
            Delivery deliv = lsd.Where(x => x.deliveryId == id).First();
                var response = Task.Run(async () => await deliveriesService.getRoute(deliv.destination));
                double Latitude = response.Result.Response.View[0].Result[0].Location.DisplayPosition.Latitude;
                double Lonitude = response.Result.Response.View[0].Result[0].Location.DisplayPosition.Longitude;
                dynamic d = new { Latitude = Latitude, Longitude = Lonitude, dmLat = deliv.latitude, dmLon = deliv.longitude, Address = deliv.destination };
                ds.Add(d);
            
            return new JsonResult { Data = ds, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }


        public ActionResult deliveriesDM()
        {
            if(Session["currentd"]!=null)
             id = (int)Session["currentd"];

            var task = Task.Run(async () => await deliveriesService.ListDeliveryPerDm(3));


            return View(task.Result);
        }
            public ActionResult trackDelivery()
        {
            
            var task = Task.Run(async () => await deliveriesService.ListDeliveryPerCust(1));
          
            return View(task.Result);
        }


        }

}