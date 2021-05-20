using DDari.Models;
using DDari.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class ContractRentController : Controller
    {
        // GET: ContractRent
        public ActionResult Index()
        {
            IEnumerable<Contract_rent> crents = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/");
                //HTTP GET
                var responseTask = client.GetAsync("/Contract_Rent/get");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Contract_rent>>();
                    readTask.Wait();

                    crents = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    crents = Enumerable.Empty<Contract_rent>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(crents);
        }

        public ActionResult PrintAll(int id_user, int id_property)
        {
            return new Rotativa.ActionAsPdf("Details", new { id_user = id_user, id_property = id_property });
        }
        // GET: ContractRent/Details/5
        public ActionResult Details(int id_user, int id_property)
        {

            Contract_rent contract_Rent = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/Contract_Rent/");
                //HTTP GET
                var responseTask = client.GetAsync("getOne/" + id_user + "/" + id_property);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Contract_rent>();
                    readTask.Wait();

                    contract_Rent = readTask.Result;
                }
            }
            return View(contract_Rent);
        }


        // GET: ContractRent/Create
        public ActionResult Create()
        {
            // For users 
            IEnumerable<long> users = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/");
                //HTTP GET
                var responseTask = client.GetAsync("/Contract_Rent/getIdUser");
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
                var responseTask = client.GetAsync("/Contract_Rent/getIdPropertyRent");
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
            var model = new CreateContractRentViewModel
            {
                Users = users,
                Properties = properties
            };


            return View(model);
        }

        // POST: ContractRent/Create
        [HttpPost]
        public ActionResult Create(Contract_rent cr)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/");
                //HTTP GET
                var responseTask = client.PostAsJsonAsync<Contract_rent>("/Contract_Rent/Add", cr).Result;
            }
            return RedirectToAction("Index");
        }

        // GET: ContractRent/Edit/5
        public ActionResult Edit(int id_user, int id_property)
        {
            Contract_rent contract_Rent = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/Contract_Rent/");
                //HTTP GET
                var responseTask = client.GetAsync("getOne/" + id_user + "/" + id_property);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Contract_rent>();
                    readTask.Wait();

                    contract_Rent = readTask.Result;
                }
            }
            return View(contract_Rent);
        }


        [HttpPost]
        public ActionResult Edit(Contract_rent contract_Rent)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/Contract_Rent/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Contract_rent>("put", contract_Rent);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(contract_Rent);
        }
        public async System.Threading.Tasks.Task<ActionResult> Delete(int id_user, int id_property)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081/Contract_Rent/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.DeleteAsync("Delete/" + id_user + "/" + id_property);
            return RedirectToAction("Index");
        }
    }
}
