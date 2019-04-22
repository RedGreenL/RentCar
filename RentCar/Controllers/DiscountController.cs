using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentCar.Interfaces;
using RentCar.Models;

namespace RentCar.Controllers
{
    [Authorize(Roles = "Administrator")]

    public class DiscountController : Controller
    {
        private readonly IDiscountRepository _discountRep;

        public DiscountController(IDiscountRepository discountRep)
        {
            this._discountRep = discountRep;
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Index()
        {
            return View(_discountRep.GetAll());
        }

        // GET: Discount/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Discount/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Discount/Create
        [HttpPost]
        public ActionResult Create(Discount discount)
        {
            try
            {
                _discountRep.Create(discount);
                _discountRep.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Discount/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_discountRep.GetById(id));
        }

        // POST: Discount/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Discount discount)
        {
            try
            {
                _discountRep.Update(id, discount);
                _discountRep.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            return View(_discountRep.GetById(id));
        }


        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _discountRep.Delete(id);
                _discountRep.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
