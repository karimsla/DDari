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
    public class ForgotPasswordController : Controller
    {
        // GET: ForgotPassword/ForgotPage
        public ActionResult ForgotPage()
        {
            return View();
        }

        // POST: ForgotPassword/ForgotPage
        [HttpPost]
        public ActionResult ForgotPage(ForgotPasswordRequest forgotPassword)
        {
            HttpClient httpClient = HttpClientBuilder.Get();
            HttpResponseMessage response = httpClient.PostAsJsonAsync<ForgotPasswordRequest>("forgotpssd/forgot_passwordp",forgotPassword).Result;
            string message = response.Content.ReadAsStringAsync().Result;
           // response.EnsureSuccessStatusCode();
            if (message.Equals("reset password email has been sent successfully")){
                ModelState.AddModelError("email", "reset password email has been sent successfully");
            }  
            else if(message.Equals("we can't find an account with this email address"))
            {
                ModelState.AddModelError("email", "we can't find an account with this email address");

            }
            return View();

        }
        //GET: ForgotPassword/resetPassword
        public ActionResult resetPassword(string token)
        {
            HttpClient httpClient = HttpClientBuilder.Get();
            HttpResponseMessage response = httpClient.GetAsync("forgotpssd/reset_password").Result;
            response.EnsureSuccessStatusCode();
            return View();
        }
        //POST: ForgotPassword/resetPassword
        [HttpPost]
        public ActionResult resetPassword(ResetPassword resetPassword)
        {
            HttpClient httpClient = HttpClientBuilder.Get();
            
            HttpResponseMessage response = httpClient.PostAsJsonAsync<ResetPassword>("forgotpssd/reset_password",resetPassword).Result;
            return View();

        }



    }
}
