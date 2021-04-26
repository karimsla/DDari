using DDari.Models;
using DDari.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class MessageController : Controller
    {
        static ServiceMessages serviceMessages=null;
        public MessageController()
        {
            serviceMessages = new ServiceMessages();

        }


      


        // GET: Message
        public ActionResult Index()
        {
            //var users=serviceMessages.getUsers();
            return View();
        }

        public ActionResult Messages(int sentTo,string error="")
        {
            //var users=serviceMessages.getUsers();
            int sentby = 1; sentTo = 2;
            var messages = serviceMessages.GetMessage(sentby, sentTo);
            return View(messages);

        }

        public ActionResult sendMessage(string content,int to)
        {
            int by = 1;to = 2;
            if (serviceMessages.AddMessageAsync(content, by, to).Result)
            {
                return RedirectToAction("Messages", new { to = to });
            }
            else
            {
                return RedirectToAction("Messages", new { to = to,error="couldn't send" });
            }
             
        }

        public ActionResult chatbot( string input)
        {
            string response = serviceMessages.chatBot(input).Result;
            if (String.IsNullOrEmpty(response))
            {
                return Json("something wrong happened! please try again in few moments");
            }
            else
            {
                return Json(response);
            }

        }
    }
}