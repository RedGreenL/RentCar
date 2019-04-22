using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using RentCar.Interfaces;
using RentCar.Models;
using RentCar.Repositories;

namespace RentCar.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityRepository _cityRep;

        public CityController(ICityRepository cityRepo)
        {
            this._cityRep = cityRepo;
        }
        
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(_cityRep.GetAll().OrderBy(x => x.Name).ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Create()
        {

            ViewBag.District = _cityRep.GetAllDistricts();
            return View();
        }


        [HttpPost]
        public ActionResult Create(City city)
        {
            try
            {
                _cityRep.Create(city);
                _cityRep.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {

                ViewBag.District = _cityRep.GetAll();
                return View(_cityRep.GetById(id));

        }


        [HttpPost]
        public ActionResult Edit(int id, City city)
        {
            try
            {
                _cityRep.Update(id, city);
                _cityRep.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            return View(_cityRep.GetById(id));
        }

        // POST: CityCRUD/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _cityRep.Delete(id);
                _cityRep.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
