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
    public class AppointmentController : Controller
    {

        static ServiceAppointment appointmentService = null;

        public AppointmentController()
        {
         appointmentService = new ServiceAppointment();
        }
        // GET: Appointment
        public ActionResult Index()
        {
  
            var task = Task.Run(async () => await appointmentService.ownerAppAsync(8));
            List<Appointment> ls= task.Result;
            foreach (Appointment app in ls)
            {
                var t1 = Task.Run(async () => await appointmentService.getCustAsync(app.customerId));
                Customer c = t1.Result;
                app.custname = c.firstName + " " + c.lastName;
            }
            return View(ls);
        }
        public ActionResult request()
        {
            Appointment app = new Appointment();
            app.customerId = 8;
            app.ownerId = 9;
            var task = Task.Run(async () => await appointmentService.RequestAppAsync(app));
            task.Wait();
            return RedirectToAction("Index");
        }
            // GET: Appointment
            public ActionResult requested()
        {

            var task = Task.Run(async () => await appointmentService.CustAppAsync(8));
            List<Appointment> ls = task.Result;
            foreach (Appointment app in ls)
            {
                var t1 = Task.Run(async () => await appointmentService.getCustAsync(app.ownerId));
                Customer c = t1.Result;
                app.custname = c.firstName + " " + c.lastName;
            }
            return PartialView(task.Result);
        }
        // GET: Appointment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Appointment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Appointment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Appointment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                 var task = Task.Run(async () => await appointmentService.AcceptAppAsync(Int32.Parse(collection["id"]),collection["date"], collection["address"], Int32.Parse(collection["at"])));
                var t = task.Result;
                return RedirectToAction("index");

            }
            catch
            {
                return View();
            }
        }

        // GET: Appointment/Delete/5
        public ActionResult Delete(int id)
        {
            var task = Task.Run(async () => await appointmentService.cancelApp(id));
            var t=task.Result;
            return RedirectToAction("Index");
        }

        // POST: Appointment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
