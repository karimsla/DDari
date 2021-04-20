using DDari.Models;
using DDari.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class ReclamationController : Controller
    {
        static ServiceReclamation serviceReclamation=null;
        public ReclamationController()
        {
            serviceReclamation = new ServiceReclamation();

        }
        // GET: Reclamation
        public  ActionResult Index()
        {
            //all reclams
            var task = Task.Run(async () => await serviceReclamation.FindAll());
          //  var reclamations = serviceReclamation.FindAll();
            var reclams = task.Result;

            return View(reclams);
        }

        // GET: Reclamation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reclamation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reclamation/Create
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

        // GET: Reclamation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reclamation/Edit/5
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

        // GET: Reclamation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reclamation/Delete/5
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
