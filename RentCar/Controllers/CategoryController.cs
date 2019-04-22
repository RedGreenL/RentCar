using RentCar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using RentCar.Interfaces;
using RentCar.Repositories;


namespace RentCar.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRep;

        public CategoryController(ICategoryRepository categoryRep)
        {
            this._categoryRep = categoryRep;
        }

        // GET: CategoryCRUD
        public ActionResult Index()
        {
            return View(_categoryRep.GetAll());

        }

        // GET: CategoryCRUD/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryCRUD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryCRUD/Create
        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                _categoryRep.Create(category);
                _categoryRep.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryCRUD/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_categoryRep.GetById(id));
        }

        // POST: CategoryCRUD/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                _categoryRep.Update(id, category);
                _categoryRep.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryCRUD/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_categoryRep.GetById(id));
        }

        // POST: CategoryCRUD/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                _categoryRep.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
