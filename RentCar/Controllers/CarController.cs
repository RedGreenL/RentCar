using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using PagedList;
using RentCar.Interfaces;
using RentCar.Models;


namespace RentCar.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    public class CarController : Controller
    {
        private readonly ICarRepository _carRep;
        private readonly ICategoryRepository _categoryRep;
        private readonly IGearRepository _gearRep;
        private readonly IFuelRepository _fuelRep;

        public CarController(ICarRepository carRep, IFuelRepository fuelRep, IGearRepository gearRep, ICategoryRepository categoryRep)
        {
            this._carRep = carRep;
            this._categoryRep = categoryRep;
            this._fuelRep = fuelRep;
            this._gearRep = gearRep;
        }

        // GET: CarCRUD
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(_carRep.GetAll().OrderBy(x => x.Brand).ToPagedList(pageNumber, pageSize));
        }

        // GET: CarCRUD/Details/5
        public ActionResult Details(int id)
        {
            
            return View(_carRep.GetById(id));
        }
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Create()
        {
            var enumData = from CarColors c in Enum.GetValues(typeof(CarColors))
                select new
                {
                  Name = c.ToString()
                };

            ViewBag.CarColors = enumData;
            ViewBag.Category = _categoryRep.GetAll();
            ViewBag.Gear = _gearRep.GetAll();
            ViewBag.Fuel = _fuelRep.GetAll();
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Create(Car car, HttpPostedFileBase file)
       {

            _carRep.Create(car);
            var lastCarId = _carRep.GetAll().Max(i => i.Car_ID);
            _carRep.UploadImageReturnID(lastCarId, file);

            return RedirectToAction("Index");
        }


        // GET: CarCRUD/Edit/5
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Edit(int id)
        {
            var enumData = from CarColors c in Enum.GetValues(typeof(CarColors))
                select new
                {
                    Name = c.ToString()
                };

            ViewBag.CarColors = enumData;
            ViewBag.Category = _categoryRep.GetAll();
            ViewBag.Gear = _gearRep.GetAll();
            ViewBag.Fuel = _fuelRep.GetAll();

            ViewBag.CategoryCurent = _carRep.GetAll().Where(i => i.Car_ID == id)
                .Select(s => s.Category_ID).FirstOrDefault();

            ViewBag.GearCurent = _carRep.GetAll().Where(i => i.Car_ID == id)
                .Select(s => s.GearType_ID).FirstOrDefault();

            ViewBag.FuelCurent = _carRep.GetAll().Where(i => i.Car_ID == id)
            .Select(s => s.FuelType_ID).FirstOrDefault();
            return View(_carRep.GetById(id));
        }

        // POST: CarCRUD/Edit/5
        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Edit(int id, Car car)
        {
            try
            {
                _carRep.Update(id, car);
                _carRep.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CarCRUD/Delete/5
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Delete(int id)
        {

            return View(_carRep.GetById(id));
        }

        // POST: CarCRUD/Delete/5
        [HttpPost]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Delete(int id, Car car)
        {
            try
            {
                _carRep.Delete(id);
                _carRep.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
