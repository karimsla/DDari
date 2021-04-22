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
        // GET: Subscript
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
            var taskt = Task.Run(async () => await serviceReclamation.getOne(id));
            var reclam = taskt.Result;
            return View(reclam);
        }

        // GET: Reclamation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reclamation/Create
        [HttpPost]
        public ActionResult Create(Reclamation reclamation)
        {
            ModelState.Remove("dateTime");
            ModelState.Remove("treatement");
            ModelState.Remove("state");
            ModelState.Remove("priority");
            if (ModelState.IsValid)
            {

            
            try
            {
                    _ = serviceReclamation.Create(reclamation, 1);
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View(reclamation);
            }}
            return View(reclamation);
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
            var task = Task.Run(async () => await serviceReclamation.Delete(id));
            var result = task.Result;
            return RedirectToAction("Index");
        }

        // POST: Reclamation/Delete/5
       


        public ActionResult TreatClaim(int id, string treatement)
        {
            var task = Task.Run(async () => await serviceReclamation.treat(id, treatement));
            var res = task.Result;
            return RedirectToAction("Details", new { id = id });
        }


    }
}
