using DDari.Models;
using DDari.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class SubscriptionController : Controller
    {
        static ServiceSubscription serviceSub = null;

        public SubscriptionController()
        {
            serviceSub = new ServiceSubscription();

        }
        // GET: Subscription
        public ActionResult Index()
        {

            //all subs
            var task = Task.Run(async () => await serviceSub.FindAll());

            var subs = task.Result;

            return View(subs);
        }

        // GET: Subscription/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Subscription/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subscription/Create
        [HttpPost]
        public async Task<ActionResult> Create(Subscription sub)
        {
            if (ModelState.IsValid)
            {


                try
                {
                    await (_ = serviceSub.Create(sub));
                    // TODO: Add insert logic here

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(sub);
                }
            }
            return View(sub);

        }

        // GET: Subscription/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Subscription/Edit/5
       
        public ActionResult Edit(int id, Subscription subscription)
        {

         /*   if (ModelState.IsValid)
            {


                try
                {
                   var p  = serviceSub.Update(id);
                    // TODO: Add insert logic here

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(sub);
                }
            }
            return View(sub);
         */
        }



        // POST: Subscription/Delete/5
        public ActionResult Delete(int id)
        {

            var task = Task.Run(async () => await serviceSub.Delete(id));
            var result = task.Result;
            return RedirectToAction("Index");


        }
    }
}
