using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class StaForPropertyController : Controller
    {
        // GET: StaForProperty
        public ActionResult Index()
        {
            return View();
        }

        // GET: StaForProperty/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StaForProperty/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StaForProperty/Create
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

        // GET: StaForProperty/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StaForProperty/Edit/5
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

        // GET: StaForProperty/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StaForProperty/Delete/5
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
