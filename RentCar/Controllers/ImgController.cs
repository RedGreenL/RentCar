using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using RentCar.Models;

namespace RentCar.Controllers
{
    public class ImgController : Controller
    {
        // GET: Img
        public ActionResult Index()
        {
            return View();
        }

        // GET: Img/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Img/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Img/Create
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file)
        {
            
            try
            {
                
                ApplicationDbContext obj = new ApplicationDbContext();
                    Images tbl = new Images();
                    var allowedExtensions = new[] {
                        ".Jpg", ".png", ".jpg", "jpeg"
                    };
                    tbl.Path = file.ToString(); //getting complete url  
                    var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                    var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = name + "_" + tbl.Images_ID + ext; //appending the name with id  
                        // store the file inside ~/project folder(Img)  
                        var path = Path.Combine(Server.MapPath("~/Img"), myfile);
                        tbl.Path = path;
                        obj.Image.Add(tbl);
                        obj.SaveChanges();
                        file.SaveAs(path);
                        
                    }
                return RedirectToAction("Index");
            }  
            
            catch
            {
                return View();
            }
        }

        // GET: Img/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Img/Edit/5
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

        // GET: Img/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Img/Delete/5
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
