
using DDari.Services;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Web.Mvc;

namespace DDari.Controllers
{
    public class OrdersController : Controller
    {




        private ServiceOrders serviceOrders;
        public OrdersController()
        {
            serviceOrders = new ServiceOrders();
        }
        // GET: Orders
        public ActionResult Index()
        {
            //all orders

            var task = Task.Run(async () => await serviceOrders.FindAll());

            var orders = task.Result;


            return View(orders);
        }




        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            var taskt = Task.Run(async () => await serviceOrders.getOne(id));

            var order = taskt.Result;


            return View(order);
        }

        public ActionResult passOrder()
        {
            ViewBag.sum = TempData["sum"];
            ViewBag.count = TempData["count"];
            return View();
        }




        // GET: Orders/Create
        public ActionResult Create(string price,string count)
        {
           
            StripeConfiguration.ApiKey = "sk_test_51IncNxGyQHQcbQ8NDormZm0pm33IAE6DrSXpJIewUngomj00pUyP5q6dWOhUg26YbRynK2f49e11MxIfSVAYIXnD00jwqzvSEx";

            
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                      UnitAmount = int.Parse(price)*100 ,
                      Currency = "usd",
                      ProductData = new SessionLineItemPriceDataProductDataOptions
                      {
                        Name = "Dari Furnitures",
                      },
                    },
                    Quantity = 1,
                  },
                },
                Mode = "payment",
                
                SuccessUrl = "https://localhost:44307"+Url.Action("Success" ),

                CancelUrl = "https://localhost:44307"+Url.Action("Error"),
            };
            var service = new SessionService();
            Session session = service.Create(options);
            return Json(new { id = session.Id },JsonRequestBehavior.AllowGet);
           
        }
        
        public ActionResult Success()
        {
            //confirm the order and save it in the db
         var task=Task.Run(async()=>await   serviceOrders.addOrder());
            task.Wait();
            return View();

        }

        public ActionResult Error()
        {
            return View();
        }


        public ActionResult myOrders()
        {
            var task = Task.Run(async () => await serviceOrders.FindAll());
            var res = task.Result;
            return View(res);
        }

        public ActionResult orderDetail(int id)
        {
            var task = Task.Run(async () => await serviceOrders.FurnOrd(id));
            var result = task.Result;
            return View(result);
        }




        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Orders/Edit/5
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

        // GET: Orders/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Orders/Delete/5
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
