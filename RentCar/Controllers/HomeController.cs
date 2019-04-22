using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using RentCar.Interfaces;
using RentCar.Models;

namespace RentCar.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        private readonly ICarRepository _carRep;
        private readonly ICategoryRepository _categoryRep;
        private readonly IGearRepository _gearRep;
        private readonly IFuelRepository _fuelRep;

        public HomeController(ICarRepository carRep, ICategoryRepository categoryRep, IFuelRepository fuelRep, IGearRepository gearRep)
        {
            this._carRep = carRep;
            this._categoryRep = categoryRep;
            this._fuelRep = fuelRep;
            this._gearRep = gearRep;
            
        }

        public ActionResult Index()
        {
            PopulationDropDownList();
            var car = _context.Car.Where(x =>x.Images.Count> 0).ToList();


            return View(car);
        }

        public ActionResult SelectCarList(int gearId, int categoryId, int fuelId)
        {
            if (gearId > 0)
            {
                return PartialView("_SelectCarList", _carRep.GetAll().Where(i => i.GearType_ID == gearId).ToList());
            }

            if (categoryId > 0)
            {
                return PartialView("_SelectCarList", _carRep.GetAll().Where(i => i.Category_ID == categoryId));
            }
            if (fuelId > 0)
            {
                return PartialView("_SelectCarList", _carRep.GetAll().Where(i => i.FuelType_ID == fuelId));
            }

            return PartialView("_SelectCarList", _carRep.GetAll());
        }

        [NonAction]
        public void PopulationDropDownList()
        {
            ViewBag.Category = _categoryRep.GetAll();
            ViewBag.Fuel = _fuelRep.GetAll();
            ViewBag.Gear = _gearRep.GetAll();
        }
    }
}