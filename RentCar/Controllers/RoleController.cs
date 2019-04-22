using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RentCar.Models;

namespace RentCar.Controllers
{
    [Authorize(Roles = "Administrator")]

    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _db;

        public RoleController()
        {
            _db = new ApplicationDbContext();
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
        }
        // GET: Role
        public ActionResult Index()
        {
            return View(_roleManager.Roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            if (role != null)
            {
                _roleManager.Create(role);
            }
            return RedirectToAction("Index");
        }


        public ActionResult Edit(string id)
        {
            return View(_roleManager.FindById(id));
        }

        // POST: CategoryCRUD/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, IdentityRole role)
        {
            try
            {
                _roleManager.Update(role);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(string id)
        {
            return View(_roleManager.FindById(id));
        }

        // POST: CategoryCRUD/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, IdentityRole role)
        {
            try
            {   
                _roleManager.Delete(_roleManager.FindById(id));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}