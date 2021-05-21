using DDari.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class SimulationController : Controller
    {

        static ServiceSimulation serviceSimulations= null;
        public SimulationController()
        {
            serviceSimulations = new ServiceSimulation();

        }
        // GET: Simulation
        public ActionResult Index()
        {
            return View();
        }

 
        public ActionResult Simul()
        {

            return View();
        }

        public ActionResult mensualite(double montant , float taux , long duree)
        {

            var task = Task.Run(async () => await serviceSimulations.mensualite(montant,taux,duree));

            var sim = task.Result;

            if (string.IsNullOrEmpty(sim))
            {
                return Json("something wrong happened! please enter data needed");
            }
            else
            {
                return Json(sim,JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult total(double montantCredit, long duree, float interet, double assurance, double frais)
        {

            var task = Task.Run(async () => await serviceSimulations.total(montantCredit, duree,interet,assurance,frais ));

            var total = task.Result;

            if (string.IsNullOrEmpty(total))
            {
                return Json("something wrong happened! please enter data needed");
            }
            else
            {
                return Json(total, JsonRequestBehavior.AllowGet);
            }

        }
        // GET: Simulation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Simulation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Simulation/Create
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

        // GET: Simulation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Simulation/Edit/5
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

        // GET: Simulation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Simulation/Delete/5
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
