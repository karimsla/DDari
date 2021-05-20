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
            Subscription sub = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/Subscription/");
                //HTTP GET
                var responseTask = client.GetAsync("getOne/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Subscription>();
                    readTask.Wait();

                    sub = readTask.Result;
                }
            }
            return View(sub);
        }

        // POST: Subscription/Edit/5
      
        [HttpPost]
        public ActionResult Edit(Subscription subscription)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/Subscription/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Subscription>("Modify", subscription);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(subscription);


        }



        // POST: Subscription/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            /* var task = Task.Run(async () => await serviceSub.Delete(id));
             var result = task.Result;
             return RedirectToAction("Index");*/

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081/Subscription/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("delete/" + id);
            return RedirectToAction("Index");


        }
    }


}
