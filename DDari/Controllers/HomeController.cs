using DDari.Models;
using DDari.Services;
using DDari.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class HomeController : Controller
    {
        ServiceReclamation serviceReclamation = new ServiceReclamation();

        public ActionResult Index()
        {
          
            return View();
        }


        public ActionResult Dashboard()
        {
            //so i m gonna add a fashboard having some stats
            //nb of claims
            //nb of high priority claims
            //nb of treated claims

            List<Reclamation> reclams = null;
            var task = Task.Run(async () => await serviceReclamation.FindAll());

            reclams = task.Result;
            ViewBag.nbclaims = reclams.Count();
            ViewBag.nbhigh = reclams.Where(x => x.priority == Priority.high).Count();
            ViewBag.treated = reclams.Where(w => w.state == true).Count();


            return View();
        }

        public PartialViewResult chart()
        {

            return PartialView();
        }



        public JsonResult json()
        {

            List<Reclamation> reclams = null;
            var task = Task.Run(async () => await serviceReclamation.FindAll());

            reclams = task.Result;

            //claim by priority
            var chart1 =reclams.GroupBy(x => x.priority).Select(x => new { name = x.Key.ToString(), y = x.Count() }).ToList();
            
            //réclamations par mois
            var chart2 = reclams.GroupBy(x => x.dateTime.ToString("MMMM", CultureInfo.CreateSpecificCulture("en"))).OrderBy(s=>s.Key).Select(x => new  { name = x.Key.ToString(), y = x.Count() }).OrderBy(s=>s.name).ToList();



            //liste à envoyer
            List<dynamic> list = new List<dynamic>();
            list.Add(chart1);
            list.Add(chart2);

            var chart3 = reclams.GroupBy(x => x.state).Select(s => new { name = s.Key==true?"Treated":"Still Pending", y = s.Count() }).ToList();
            list.Add(chart3);
            DateTime testDate = DateTime.Now.AddYears(-1);
            DateTime testDate1 = DateTime.Now.AddYears(1);
        
            return Json(list, JsonRequestBehavior.AllowGet);
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            Utilisateur user = (Utilisateur)Session["user"];
            if (user == null)
                return Redirect("Login");
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(Customer user)
        {
            Utilisateur u = (Utilisateur)Session["user"];
            if (u == null)
                return Redirect("Login");
            HttpClient httpClient = HttpClientBuilder.Get();
            user.utilisateurId = u.utilisateurId;
            Customer um = new Customer();
            um.lastName = user.lastName;
            um.firstName = user.firstName;
            um.username = user.username;
            um.email = user.email;
            um.utilisateurId = user.utilisateurId;
            DateTime? nullDateTime = null;
            um.created = nullDateTime;
            HttpResponseMessage response = httpClient.PutAsJsonAsync<Customer>("Customer/updateProfile", um).Result;
            response.EnsureSuccessStatusCode();
            string message = response.Content.ReadAsStringAsync().Result;

            Session["user"] = user;


            return View(user);
        }
    }
}