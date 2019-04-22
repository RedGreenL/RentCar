using RentCar.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentCar.Interfaces;

namespace RentCar.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly IRentsRepository _rentsRep;

        public AdminPanelController(IRentsRepository rentsRep)
        {
            this._rentsRep = rentsRep;
        }

        [Authorize]
        public ActionResult AdminPanel()
        {
            try
            {
                ViewBag.Top1Car = _rentsRep.Top1Car();
                ViewBag.RentsLastMonth = _rentsRep.RentsLastMonth();
                ViewBag.MoneyLastMonth = _rentsRep.MoneyLastMonth();
                var TopCars = _rentsRep.TopProductiveCar(5).ToList();
               // if(TopCars.Count == 0) { return View(); }
                return View(TopCars);
            }
            catch (Exception)
            {
                return View();
            }

        }
    }
}