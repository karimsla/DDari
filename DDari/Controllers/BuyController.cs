using DDari.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using System.IO;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using DDari.Services;

namespace DDari.Controllers
{
    public class BuyController : Controller
    {
        static ServiceBuy serviceBuy = null;
        public BuyController()
        {
            serviceBuy = new ServiceBuy();

        }

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


        public ActionResult Tri()
        {
            IEnumerable<Buy> buys = null;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");
                //HTTP GET
                var responseTask = client.GetAsync("/buy/Tri");
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

            return View("Index",buys);
        }

        // GET: Buy/Details/5

        public ActionResult Details(int id)
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
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator oQRCodeGenerator = new QRCodeGenerator();
                QRCodeData oQRCodeData = oQRCodeGenerator.CreateQrCode(buy.ToString(), QRCodeGenerator.ECCLevel.Q);
                QRCode oQRCode = new QRCode(oQRCodeData);
                using (Bitmap oBitmap = oQRCode.GetGraphic(20))
                {
                    oBitmap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCode = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }

            }
            return View(buy);

        }
            public ActionResult PrintAll(int id)
        {
            
            return new Rotativa.ActionAsPdf("Details", new { id=id });
        }


        [HttpPost]
        public ActionResult Estim(string text)
        {
            string res  = "";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081/buy/");
            var responseTask = client.GetAsync("Estimate/"+text);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                res = readTask.Result;
            }

            ViewBag.res = res.ToString();
           return Json(new { txt = res.ToString() });
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
          
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/");
                //HTTP GET
                var responseTask = client.PostAsJsonAsync<Buy>("/buy/AddBuy", buy).Result;
            }
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

        public ActionResult searchMulti(string filter, string adress, string state, string city)
        {
            List<Buy> buys = new List<Buy>();
            var task = Task.Run(async () => await serviceBuy.searchMultiCriteria(filter, adress,state,city, 0));
            buys = task.Result;
            return View("Index", buys);
        }

    }

    
}

