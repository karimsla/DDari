using DDari.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using DDari.ViewModels;

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

        public ActionResult Details(int id_user, int id_property)
        {

            Contract_buy contract_Buy = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/Contract_Buy/");
                //HTTP GET
                var responseTask = client.GetAsync("getOne/" + id_user + "/" + id_property);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Contract_buy>();
                    readTask.Wait();

                    contract_Buy = readTask.Result;
                }
            }
            return View(contract_Buy);
        }


        public ActionResult PrintAll(int id_user,int id_property)
        {
            return new Rotativa.ActionAsPdf("Details", new { id_user = id_user, id_property= id_property });
        }


        // GET: ContractBuy/Create
        public ActionResult Create()
        {
            // For users 
            IEnumerable<long> users = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/");
                //HTTP GET
                var responseTask = client.GetAsync("/Contract_Buy/getIdUser");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<long>>();
                    readTask.Wait();

                    users = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    users = Enumerable.Empty<long>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            // FOR properties 

            IEnumerable<int> properties = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/");
                //HTTP GET
                var responseTask = client.GetAsync("/Contract_Buy/getIdPropertyBuy");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<int>>();
                    readTask.Wait();

                    properties = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    properties = Enumerable.Empty<int>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            var model = new CreateContractViewModel
            {
                Users = users,
                Properties = properties
            };


            return View(model);
        }

        // POST: ContractBuy/Create
        [HttpPost]
        public ActionResult Create(Contract_buy cb)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/");
                //HTTP GET
                var responseTask = client.PostAsJsonAsync<Contract_buy>("/Contract_Buy/Add", cb).Result;
            }
            return RedirectToAction("Index");

        }


        public ActionResult Edit(int id_user, int id_property)
        {
            Contract_buy contract_Buy = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/Contract_Buy/");
                //HTTP GET
                var responseTask = client.GetAsync("getOne/" + id_user + "/" + id_property);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Contract_buy>();
                    readTask.Wait();

                    contract_Buy = readTask.Result;
                }
            }
            return View(contract_Buy);
        }


        [HttpPost]
        public ActionResult Edit(Contract_buy contract_Buy)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/Contract_Buy/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Contract_buy>("put", contract_Buy);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(contract_Buy);
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
