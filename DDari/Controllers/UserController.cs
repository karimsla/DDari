using DDari.Models;
using DDari.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {

            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
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
        public ActionResult Edit(Utilisateur user)
        {
            Utilisateur u = (Utilisateur)Session["user"];
            if (u == null)
                return Redirect("Login");
            HttpClient httpClient = HttpClientBuilder.Get(Session["api-cookie"]);
            user.utilisateurId = u.utilisateurId;
            HttpResponseMessage response = httpClient.PostAsJsonAsync<Utilisateur>("Customer/updateProfile", user).Result;
            response.EnsureSuccessStatusCode();
            string message = response.Content.ReadAsStringAsync().Result;
           
                Session["user"] = user;
                
           
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Utilisateur user)
        {
           // Session["user"] = user;
            HttpClient httpClient = HttpClientBuilder.Get();
            string regtype = Request.Form["registerType"];
            if (ModelState.IsValid)
            {
                if (regtype.Equals("customer")) { 
                HttpResponseMessage response = httpClient.PostAsJsonAsync<Utilisateur>("Auth/signupCustomer", user).Result;
                response.EnsureSuccessStatusCode();
                string message = response.Content.ReadAsStringAsync().Result;
                }
                if (regtype.Equals("agent"))
                {
                    HttpResponseMessage response = httpClient.PostAsJsonAsync<Utilisateur>("Auth/signupAgent", user).Result;
                    response.EnsureSuccessStatusCode();
                    string message = response.Content.ReadAsStringAsync().Result;
                }
                return RedirectToAction("Index", "Home");

            }
            return View(user);
        }
        [HttpPost]
        public ActionResult Login(Utilisateur user)
        {
            // Session["user"] = user;
            HttpClient httpClient = HttpClientBuilder.Get();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");
           

            var loginForm = new MultipartFormDataContent
            {
                { new StringContent(user.username), "username" },
                { new StringContent(user.password), "password" }
            };
            HttpResponseMessage response = httpClient.PostAsync("/login", loginForm).Result;
            string message = response.Content.ReadAsStringAsync().Result;
            string f = message.Substring(5,5);
            Utilisateur u = null;
            if (message.Contains("TYPE ")) {
                ModelState.AddModelError("passowrd", "Bad Credentials");
                
            }

            else
            {

                 u = response.Content.ReadAsAsync<Utilisateur>().Result;
                Role r = response.Content.ReadAsAsync<Role>().Result;
            }
            // string message = response.Content.ReadAsStringAsync().Result;

            if (u != null)
            {
                string role = httpClient.GetStringAsync("UserCrud/getLoggedInRole").Result;
                Session["user"] = u;
                Session["role"] = role;
                if (role.Equals("USER"))
                {
                   return RedirectToAction("Edit", "Home", new { id = u.utilisateurId });
                }
                if (role.Equals("AGENT"))
                {
                    return RedirectToAction("Index", "Agent");
                }
            }
             ModelState.AddModelError("password", "Wrong username or password");
            return RedirectToAction("Index", "Home");
        }
        
    
          
    }

    }

