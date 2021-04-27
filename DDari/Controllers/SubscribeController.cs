using DDari.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class SubscribeController : Controller
    {

        static ServiceSubscription serviceSub = null;

        public SubscribeController()
        {
            serviceSub = new ServiceSubscription();

        }
        // GET: Subscribe
        public ActionResult Index()
        {
            return View();
        }

        // GET: Subscribe/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Subscribe/Create
        public ActionResult Creates()
        {
            return View();
        }

        // POST: Subscribe/Create
        
        public ActionResult Create(Models.Subscribe subscribe)
        {
            if (ModelState.IsValid)
            {


                try
                {
                    await(_ = serviceSub.CreateSubscribe(subscribe));
                    // TODO: Add insert logic here

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(subscribe);
                }
            }
            return View(subscribe);

        }

        private void await(Task<Uri> task)
        {
            throw new NotImplementedException();
        }


        // GET: Subscribe/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Subscribe/Edit/5
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

        // GET: Subscribe/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Subscribe/Delete/5
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
