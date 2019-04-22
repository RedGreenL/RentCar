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
    [Authorize]
    public class DistrictController : Controller
    {
        private readonly IDistrictRepository _districtRep;

        public DistrictController(IDistrictRepository districtRep)
        {
            this._districtRep = districtRep;
        }


        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(_districtRep.GetAll().OrderBy(x => x.Name).ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(District district)
        {
            try
            {
                    _districtRep.Create(district);
                    _districtRep.Save();
                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        // GET: DistrictCRUD/Edit/5
        public ActionResult Edit(int id)
        {
             return View(_districtRep.GetById(id));
        }

        // POST: DistrictCRUD/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, District district)
        {
            try
            {
                _districtRep.Update(id, district);
                _districtRep.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DistrictCRUD/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_districtRep.GetById(id));
        }

        // POST: DistrictCRUD/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _districtRep.Delete(id);
                _districtRep.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
