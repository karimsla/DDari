using DDari.Models;
using DDari.Services;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class RentController : Controller
    {

        static ServiceRent serviceRent = null;
        public RentController()
        {
            serviceRent = new ServiceRent();

        }


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

        public ActionResult Tri()
        {
            IEnumerable<Rent> rents = null;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");
                //HTTP GET
                var responseTask = client.GetAsync("/rent/Tri");
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

            return View("Index", rents);
        }

        // GET: Rent/Details/5
        public ActionResult Details(int id)
        {

            Rent rent = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/rent/");
                //HTTP GET
                var responseTask = client.GetAsync("getone/"+ id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Rent>();
                    readTask.Wait();

                    rent = readTask.Result;
                }
            }
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator oQRCodeGenerator = new QRCodeGenerator();
                QRCodeData oQRCodeData = oQRCodeGenerator.CreateQrCode(rent.ToString(), QRCodeGenerator.ECCLevel.Q);
                QRCode oQRCode = new QRCode(oQRCodeData);
                using (Bitmap oBitmap = oQRCode.GetGraphic(20))
                {
                    oBitmap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCode = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }

            }
            return View(rent);

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
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/");
                //HTTP GET
                var responseTask = client.PostAsJsonAsync<Rent>("/rent/AddRent", rent).Result;
            }
            return RedirectToAction("Index");

        }


        public ActionResult PrintAll(int id)
        {
            return new Rotativa.ActionAsPdf("Details", new { id = id });
        }



        // GET: Rent/Edit/5

        public ActionResult Edit(int id)
        {
            Rent rent = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/rent/");
                //HTTP GET
                var responseTask = client.GetAsync("getone/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Rent>();
                    readTask.Wait();

                    rent = readTask.Result;
                }
            }
            return View(rent);
        }


        [HttpPost]
        public ActionResult Edit(Rent rent)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/rent/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Rent>("put", rent);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(rent);
        }


        public async System.Threading.Tasks.Task<ActionResult> Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081/rent/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.DeleteAsync("delete/" + id);
            return RedirectToAction("Index");
        }
        public ActionResult searchMulti(string filter, string adress, string state, string city)
        {
            List<Rent> rents = new List<Rent>();
            var task = Task.Run(async () => await serviceRent.searchMultiCriteria(filter, adress, state, city, 0));
            rents = task.Result;
            return View("Index", rents);
        }
    }
}
