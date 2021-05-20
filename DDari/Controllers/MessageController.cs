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
    public class MessageController : Controller
    {
        static ServiceMessages serviceMessages = null;
        public MessageController()
        {
            serviceMessages = new ServiceMessages();

        }


        public ActionResult chat_bot()
        {
            return View();
        }




        // GET: Message
        public ActionResult Index()
        {
            var task = Task.Run(async () => await serviceMessages.getUsers(1));
            var users=task.Result;
            ViewBag.contacts = users;
            return View("Messages");
        }

        public ActionResult Messages(int sentTo=0, string error = "")
        {
           

            var task = Task.Run(async () => await serviceMessages.getUsers(1));
            var users = task.Result;
            ViewBag.contacts = users;
            foreach(var item in users)
            {
                if (item.utilisateurId ==sentTo)
                {
                    ViewBag.fullname = item.firstName + " " + item.lastName;
                    break;
                }
            }

           
            var task1 = Task.Run(async () => await serviceMessages.GetMessage(1, sentTo));
            var messages = task1.Result;
            ViewBag.sentTo = sentTo;
          
            ViewBag.error = error;
            return View(messages);

        }

        public ActionResult sendMessage(string content, int to, int by)
        {
            if (by == 0) by= 1;
            if (to == 0) to= 2;

             
            var task = Task.Run(async () => await serviceMessages.AddMessageAsync(content, by, to));
            var result = task.Result;

            if (result)
            {
                return RedirectToAction("Messages", new { sentTo = to });
            }
            else
            {
                return RedirectToAction("Messages", new { sentTo = to, error = "couldn't send" });
            }

        }

        public ActionResult chatbot(string input)
        {
            var task = Task.Run(async () => await serviceMessages.chatBot(input));
            var response = task.Result;

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