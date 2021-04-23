using DDari.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class Contract_buyController : Controller
    {
        // GET: Contract_buy
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("Contract_buy").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Contract_buy>>().Result;
            }
            else
            {
                ViewBag.result = "error";
            }
            return View();
        }

        // GET: Contract_buy/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Contract_buy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contract_buy/Create
        [HttpPost]
        public ActionResult Create(Contract_buy contract_Buy)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.PostAsJsonAsync<Contract_buy>("Contract_buy/Add", contract_Buy).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

            return RedirectToAction("Index");
        }

        // GET: Contract_buy/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Contract_buy/Edit/5
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

        // GET: Contract_buy/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Contract_buy/Delete/5
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
