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
    public class FurnitureController : Controller

    {
        static ServiceFurniture serviceFurniture = null;

        public FurnitureController()
        {
            serviceFurniture = new ServiceFurniture();

        }




        // GET: Furniture
        public ActionResult Index(string filter)
        {
            List<Furniture> furnitures = new List<Furniture>();
            if (!string.IsNullOrEmpty(filter))
            {
                var task = Task.Run(async () => await serviceFurniture.search(filter));
                furnitures = task.Result;
            }
            else
            {
                var task = Task.Run(async () => await serviceFurniture.FindAll());
                furnitures = task.Result;
            }
            //all properties





            return View(furnitures);
        }





        // GET: Furniture/Details/5
        public ActionResult Details(int id)
        {
            var taskt = Task.Run(async () => await serviceFurniture.getOne(id));

            var furniture = taskt.Result;


            return View(furniture);
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

        // GET: Furniture/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Furniture/Create
        [HttpPost]
        public ActionResult Create(Furniture furniture, HttpPostedFileBase postedFile)
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
                        if (System.IO.File.Exists(Path.GetFullPath(path + "furniture_" + obj + Path.GetExtension(postedFile.FileName))))

                            System.IO.File.Delete(path + "furniture_" + obj + Path.GetExtension(postedFile.FileName));

                        //save the new file
                        postedFile.SaveAs(path + "furniture_" + obj + Path.GetExtension(postedFile.FileName));
                        furniture.picture = Path.GetFileName("furniture_" + obj + Path.GetExtension(postedFile.FileName));
                    }
                    var task = Task.Run(async () => await serviceFurniture.Create(furniture, 8));

                    // TODO: Add insert logic here

                    return RedirectToAction("MyFurnitures");
                }
                catch
                {
                    return View(furniture);
                }



            }
            return View(furniture);
        }






        public ActionResult MyFurnitures()
        {
            int id = 1;
            var task = Task.Run(async () => await serviceFurniture.getMine(8));
            var result = task.Result;
            return View(result);
        }






        // GET: Furniture/Edit/5
        public ActionResult Edit(int id)
        {
            var task = Task.Run(async () => await serviceFurniture.getOne(id));
            Furniture furniture = task.Result;
            return View(furniture);
        }

        // POST: Furniture/Edit/5
        [HttpPost]
        public ActionResult Edit(Furniture furniture, HttpPostedFileBase postedFile)
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
                    if (furniture.picture != null)
                    {
                        System.IO.File.Delete(furniture.picture);

                    }

                    //save the new file
                    postedFile.SaveAs(path + "furniture_" + obj + Path.GetExtension(postedFile.FileName));
                    furniture.picture = Path.GetFileName("furniture_" + obj + Path.GetExtension(postedFile.FileName));
                }

                var task = Task.Run(async () => await serviceFurniture.Update(furniture));
                task.Wait();
                // TODO: Add update logic here

                return RedirectToAction("MyFurnitures");
            }
            catch
            {
                return View(furniture);
            }
        }

        // GET: Furniture/Delete/5
        public ActionResult Delete(int id)
        {
            var task=Task.Run(async()=> await serviceFurniture.Delete(id));
            task.Wait();
            return RedirectToAction("MyFurnitures");
        }
 
    }
}
