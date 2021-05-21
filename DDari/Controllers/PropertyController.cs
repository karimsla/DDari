using DDari.Models;
using DDari.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DDari.Controllers
{
    public class PropertyController : Controller
    {
        static ServiceProperty serviceProperty = null;

        public PropertyController()
        {
            serviceProperty = new ServiceProperty();

        }


        // GET: Property
        public ActionResult Index(string filter)
        {
            List<Property> properties = null;
            if (!string.IsNullOrEmpty(filter))
            {
                var task = Task.Run(async () => await serviceProperty.search(filter));
                 properties = task.Result;
            }
            else
            {
            var task = Task.Run(async () => await serviceProperty.FindAll());
                 properties = task.Result;
            }
            //all properties


        


            return View(properties);
        }


        // GET: Property/Details/5
        public ActionResult Details(int id)
        {
            var taskt = Task.Run(async () => await serviceProperty.getOne(id));

            var property = taskt.Result;


            return View(property);
        }


        protected bool verifyFiles(HttpPostedFileBase item)
        {
            bool flag = true;

            if (item != null)
            {
                //check if the size and the extension are ok
                if (item.ContentLength > 0 && item.ContentLength < 5000000)
                {
                    if (!(Path.GetExtension(item.FileName).ToLower() == ".jpg" ||
                        Path.GetExtension(item.FileName).ToLower() == ".png" ||
                        Path.GetExtension(item.FileName).ToLower() == ".jpeg"))
                    {
                        flag = false;
                    }
                }
                else { flag = false; }
            }
            else { flag = false; }

            return flag;
        }

        // GET: Property/Create
        public ActionResult Create()
        {
            return View();
        }

          // POST: Property/Create
           [HttpPost]
           public ActionResult Create(Property property, HttpPostedFileBase postedFile)
           {

               if (ModelState.IsValid)
               {


                   try
                   {

                   
                    if (postedFile != null && verifyFiles(postedFile))
                    {
                        string path = Server.MapPath("/Content/Upload/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        Guid obj = Guid.NewGuid();
                        //if the image exist then remove it
                        if (System.IO.File.Exists(Path.GetFullPath(path + "property_" +obj + Path.GetExtension(postedFile.FileName))))

                            System.IO.File.Delete(path + "property_" + obj+ Path.GetExtension(postedFile.FileName));

                        //save the new file
                        postedFile.SaveAs(path + "property_" + obj  + Path.GetExtension(postedFile.FileName));
                        property.image = Path.GetFileName( "property_" + obj + Path.GetExtension(postedFile.FileName));
                    }
                    var task = Task.Run(async () => await serviceProperty.Create(property, 8));

                    // TODO: Add insert logic here

                    return RedirectToAction("Index");
                   }
                   catch
                   {
                       return View(property);
                   }
               }

            var errors = ModelState.Select(x => x.Value.Errors)
                                   .Where(y => y.Count > 0)
                                   .ToList();
            return View(property);
           }


           

        // GET: Property/Edit/5
        public ActionResult Edit(int id)
        {
            var task = Task.Run(async () => await serviceProperty.getOne(id));
            Property prop=task.Result;
            return View(prop);
        }

        // POST: Property/Edit/5
        [HttpPost]
        public ActionResult Edit(Property prop, HttpPostedFileBase postedFile)
        {
            try
            {

                if (postedFile != null && verifyFiles(postedFile))
                {
                    string path = Server.MapPath("/Content/Upload/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    Guid obj = Guid.NewGuid();
                    //if the image exist then remove it
                     
                        System.IO.File.Delete(path +prop.image);

                    //save the new file
                    postedFile.SaveAs(path + "property_" + obj + Path.GetExtension(postedFile.FileName));
                    prop.image = Path.GetFileName("property_" + obj + Path.GetExtension(postedFile.FileName));
                }

                var task = Task.Run(async () => await serviceProperty.Update(prop));
                task.Wait();
                  // TODO: Add update logic here

                return RedirectToAction("MyProperties");
            }
            catch
            {
                return View(prop);
            }
        }



        // GET: Property/Delete/5
        public ActionResult Delete(int id)
        {
            var task = Task.Run(async () => await serviceProperty.Delete(id));
            task.Wait();
            return RedirectToAction("MyProperties");
        }

     


        public ActionResult MyProperties()
        {
            int id = 1;
            var task=Task.Run(async()=>await serviceProperty.getPropsUser(id));
            var list = task.Result;
            return View(list);
        }
    }


    


}
