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
        public  ActionResult Index(string search)
        {
            List<Reclamation> reclams=null;
            if (string.IsNullOrEmpty(search))
            {

            //all reclams
            var task = Task.Run(async () => await serviceReclamation.FindAll());
    
              reclams = task.Result;
            }
            else
            {
                var task = Task.Run(async () => await serviceReclamation.filter(search));
                 reclams = task.Result;
            }



            return View(reclams);
        }
        
        public ActionResult treated(bool treated)
        {
            List<Reclamation> reclams = null;
            if (treated == false)
            {
                 var task = Task.Run(async () => await serviceReclamation.findNotTreated());
               
                 reclams = task.Result;
                ViewBag.treated = false;
            }
            else
            {
                //all reclams
                var task = Task.Run(async () => await serviceReclamation.FindAll());
          
               reclams = task.Result;
                reclams = reclams.Where(x => x.state == true).ToList();
                ViewBag.treated = true;
            }
          

            return View("Index",reclams);
        }



        public ActionResult betweendate(string date1,string date2)
        { 
            if(string.IsNullOrEmpty(date1) && string.IsNullOrEmpty(date2))
            {
                return RedirectToAction("Index");
            }
            DateTime d1=DateTime.Now, d2=DateTime.Now;
            if (string.IsNullOrEmpty(date1))
            {
                d1 = DateTime.Now.AddYears(-20);
                d2 = DateTime.Parse(date2);
            }else if (string.IsNullOrEmpty(date2))
            {
                d1 = DateTime.Parse(date1);
                d2 = DateTime.Now.AddDays(30);
            }
            else if(!string.IsNullOrEmpty(date1) && !string.IsNullOrEmpty(date2))
            {
                d1 = DateTime.Parse(date1);
                d2 = DateTime.Parse(date2);
            }
            
            var task = Task.Run(async () => await serviceReclamation.findBetweenDate(d1.ToString("yyyy-MM-dd HH:mm"),d2.ToString("yyyy-MM-dd HH:mm")));

            List<Reclamation> reclams = task.Result;
            return View("Index",reclams);

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



        public ActionResult MyReclam(string search)
        {
          
        
        
            List<Reclamation> reclams = null;
            if (string.IsNullOrEmpty(search))
            {

                //all reclams
                var task = Task.Run(async () => await serviceReclamation.findMyReclam(1));

                reclams = task.Result;
            }
            else
            {
                var task = Task.Run(async () => await serviceReclamation.searchMultiCriteria(search,null,true,null,null,false,1));
                reclams = task.Result;
            }

            return View(reclams);


        }


        public ActionResult findbytype(string type)
        {
            List<Reclamation> reclams=null;
            if (!string.IsNullOrEmpty(type))
            {
                var task = Task.Run(async () => await serviceReclamation.findByType(type));
                reclams = task.Result;
            
            }
            else
            {
                return RedirectToAction("index");
            }
            return View("Index", reclams);
        }
         
        
       

        public ActionResult searchMulti(string filter,string type,string date1,string date2, bool treat)
        {
            DateTime d1 = DateTime.Now, d2 = DateTime.Now;
            if(string.IsNullOrEmpty(date1) && string.IsNullOrEmpty(date2))
            {

            }else if (string.IsNullOrEmpty(date1) && !string.IsNullOrEmpty(date2))
            {
                d1 = DateTime.Now.AddYears(-20);
                d2 = DateTime.Parse(date2);
            }
            else if (string.IsNullOrEmpty(date2) && string.IsNullOrEmpty(date1))
            {
                d1 = DateTime.Parse(date1);
                d2 = DateTime.Now.AddDays(30);
            }
            else if (!string.IsNullOrEmpty(date1) && !string.IsNullOrEmpty(date2))
            {
                d1 = DateTime.Parse(date1);
                d2 = DateTime.Parse(date2);
            }
            List<Reclamation> reclams = new List<Reclamation>() ;
            var task = Task.Run(async () => await serviceReclamation.searchMultiCriteria(filter, type, false, d1.ToString("yyyy-MM-dd HH:mm"), d2.ToString("yyyy-MM-dd HH:mm"), treat, 0));
            reclams = task.Result;
            return View("Index", reclams);
        }

   
        
        
        
      public ActionResult DeleteMy(int id)
        {
            var task = Task.Run(async () => await serviceReclamation.Delete(id));
            var result = task.Result;
            return RedirectToAction("Index");
        }


        public ActionResult searchMultimine(string filter, string type, string date1, string date2, bool treat)
        {
            DateTime d1 = DateTime.Now, d2 = DateTime.Now;
            if (string.IsNullOrEmpty(date1) && string.IsNullOrEmpty(date2))
            {

            }
            else if (string.IsNullOrEmpty(date1) && !string.IsNullOrEmpty(date2))
            {
                d1 = DateTime.Now.AddYears(-20);
                d2 = DateTime.Parse(date2);
            }
            else if (string.IsNullOrEmpty(date2) && string.IsNullOrEmpty(date1))
            {
                d1 = DateTime.Parse(date1);
                d2 = DateTime.Now.AddDays(30);
            }
            else if (!string.IsNullOrEmpty(date1) && !string.IsNullOrEmpty(date2))
            {
                d1 = DateTime.Parse(date1);
                d2 = DateTime.Parse(date2);
            }
            List<Reclamation> reclams = new List<Reclamation>();
            var task = Task.Run(async () => await serviceReclamation.searchMultiCriteria(filter, type, true,
                d1.ToString("yyyy-MM-dd HH:mm"), d2.ToString("yyyy-MM-dd HH:mm"), treat, 1));
            reclams = task.Result;
            return View("MyReclam", reclams);
        }

      



    }
}
