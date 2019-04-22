using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentCar.Interfaces;
using RentCar.Models;
using RentCar.Repositories;


namespace RentCar.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    public class GearController : Controller
    {
        private readonly IGearRepository _gearRep;

        public GearController(IGearRepository gearRep)
        {
            this._gearRep = gearRep;
        }

        // GET: GearCRUD
        public ActionResult Index()
        {
            return View(_gearRep.GetAll());
        }

        // GET: GearCRUD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GearCRUD/Create
        [HttpPost]
        public ActionResult Create(GearType gear)
        {
            try
            {
                _gearRep.Create(gear);
                _gearRep.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: GearCRUD/Edit/5
        public ActionResult Edit(int id)
        {
             return View(_gearRep.GetById(id));
                
        }

        // POST: GearCRUD/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, GearType gear)
        {
            try
            {
                _gearRep.Update(id, gear);
                _gearRep.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: GearCRUD/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_gearRep.GetById(id));
        }

        // POST: GearCRUD/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _gearRep.Delete(id);
                _gearRep.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
