using DDari.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class RentController : Controller
    {
        // GET: Rent
        public ActionResult Index()
        {
            IEnumerable<Rent> rents = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");
                //HTTP GET
                var responseTask = client.GetAsync("/rent/get");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Rent>>();
                    readTask.Wait();

                    rents = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    rents = Enumerable.Empty<Rent>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(rents);
        }

        // GET: Rent/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rent/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rent/Create
        [HttpPost]
        public ActionResult Create(Rent rent)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.PostAsJsonAsync<Rent>("/rent/AddRent", rent).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

            return RedirectToAction("Index");

        }

        // GET: Rent/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Rent/Edit/5
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

        public async System.Threading.Tasks.Task<ActionResult> Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081/rent/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.DeleteAsync("delete/" + id);
            return RedirectToAction("Index");
        }
    }
}
