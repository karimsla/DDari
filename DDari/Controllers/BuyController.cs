using DDari.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class BuyController : Controller
    {
        // GET: Buy
        public ActionResult Index()
        {
            IEnumerable<Buy> buys = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");
                //HTTP GET
                var responseTask = client.GetAsync("/buy/get");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Buy>>();
                    readTask.Wait();
                    buys = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    buys = Enumerable.Empty<Buy>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(buys);
        }

        // GET: Buy/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Buy/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Buy/Create
        [HttpPost]
        public ActionResult Create(Buy buy)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.PostAsJsonAsync<Buy>("/buy/AddBuy", buy).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

            return RedirectToAction("Index");

        }

       public ActionResult Edit(int id)
        {
            Buy buy = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/buy/");
                //HTTP GET
                var responseTask = client.GetAsync("getOne/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Buy>();
                    readTask.Wait();

                    buy = readTask.Result;
                }
            }
            return View(buy);
        }


        [HttpPost]
        public ActionResult Edit(Buy buy)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/buy/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Buy>("put", buy);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(buy);
        }
       
        public async System.Threading.Tasks.Task<ActionResult> Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081/buy/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.DeleteAsync("Delete/" + id);
            return RedirectToAction("Index");
        }

    }
}

