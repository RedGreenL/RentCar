using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using RentCar.Interfaces;
using RentCar.Models;
using RentCar.Repositories;


namespace RentCar.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    public class FuelController : Controller
    {
        private readonly IFuelRepository _fuelRep;

        public FuelController(IFuelRepository fuelRep)
        {
            this._fuelRep = fuelRep;
        }

        public ActionResult Index()
        {
            return View(_fuelRep.GetAll());
        }

        
        public ActionResult Create()
        {
                return View();
        }

        
        [HttpPost]
        public ActionResult Create(FuelType fuel)
        {
            try
            {
                _fuelRep.Create(fuel);
                _fuelRep.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Edit(int id)
        {
            return View(_fuelRep.GetById(id));
        }

        
        [HttpPost]
        public ActionResult Edit(int id, FuelType fuel)
        {
            try
            {
                _fuelRep.Update(id, fuel);
                _fuelRep.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View(_fuelRep.GetById(id));
        }

        // POST: FuelCRUD/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
           try
            {
                _fuelRep.Delete(id);
                _fuelRep.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
