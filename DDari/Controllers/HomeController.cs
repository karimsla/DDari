using DDari.Models;
using DDari.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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