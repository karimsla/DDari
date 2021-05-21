using DDari.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class SubscribController : Controller
    {

        static ServiceSubscription serviceSub = null;

        public SubscribController()
        {
            serviceSub = new ServiceSubscription();

        }
        // GET: Subscrib
        public ActionResult Index()
        {
            var task = Task.Run(async () => await serviceSub.FindAll());

            var subscribes = task.Result;

            return View(subscribes);
        }

        // GET: Subscrib/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Subscrib/Create
        public ActionResult Creates()
        {
            return View();
        }

        // POST: Subscrib/Create
        
        public ActionResult Create(Models.Subscribe subscribe )
        {

            DateTime dateTime = new DateTime(2021, 12, 31);
            subscribe.DateF = dateTime;


            long idC = 1;
            if (ModelState.IsValid)
            {


                try
                {
                    var task = Task.Run(async () => await serviceSub.CreateSubscribe(subscribe , idC));
                    // TODO: Add insert logic here

                    var subsribe = task.Result;
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(subscribe);
                }
            }
            return View(subscribe);

        }

       


        // GET: Subscrib/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Subscrib/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Subscrib/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Subscrib/Delete/5
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
