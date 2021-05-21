using DDari.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class PanierController : Controller
    {
        ServiceDetailPanier panierService = null; 
        public PanierController()
        {
            panierService = new ServiceDetailPanier();

        }


        public ActionResult AddToBasket(int id)
        {
            var task=Task.Run(async()=> await panierService.addToBasket(id));
            task.Wait();
            return RedirectToAction("Index");
        }
        // GET: Panier
        public ActionResult Index()
        {
            //get my basket

            var task = Task.Run(async () => await panierService.getBasket());
            var furns = task.Result;

            ViewBag.sum = furns.Sum(s => s.price);


            return View(furns);
        }

        public ActionResult removeFurn(int id)
        {
            var task = Task.Run(async () => await panierService.Delete(id));
            task.Wait();
            return RedirectToAction("Index");
        }



        // GET: Panier/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Panier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Panier/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Panier/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Panier/Edit/5
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

        // GET: Panier/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Panier/Delete/5
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
