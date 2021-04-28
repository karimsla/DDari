using DDari.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class ContractBuyController : Controller
    {
        // GET: ContractBuy
        public ActionResult Index()
        {
            IEnumerable<Contract_buy> cbuys = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/");
                //HTTP GET
                var responseTask = client.GetAsync("/Contract_Buy/get");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Contract_buy>>();
                    readTask.Wait();

                    cbuys = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    cbuys = Enumerable.Empty<Contract_buy>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(cbuys);
        }

        // GET: ContractBuy/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContractBuy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContractBuy/Create
        [HttpPost]
        public ActionResult Create(Contract_buy cb)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.PostAsJsonAsync<Contract_buy>("/Contract_Buy/Add", cb).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");
        }

        // GET: ContractBuy/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContractBuy/Edit/5
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
        public async System.Threading.Tasks.Task<ActionResult> Delete(int id_user,int id_property)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081/Contract_Buy/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.DeleteAsync("Delete/"+ id_user + "/" + id_property);
            return RedirectToAction("Index");
        }
    }
}
