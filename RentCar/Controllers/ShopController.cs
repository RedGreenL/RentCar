using RentCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentCar.Interfaces;
using RentCar.ViewModels;

namespace RentCar.Controllers
{
    public class ShopController : Controller
    {
        private readonly IRentsRepository _rentRep;

        public ShopController(IRentsRepository rentRep)
        {
            this._rentRep = rentRep;
        }
        // GET: Shop
        public ActionResult Index()
        {
            if (Session["FreshOrder"] == null)
            {
                ViewBag.noRents = "You don't have any rents";
                return View();
            }
            List<ShopViewModels> shop = new List<ShopViewModels>();
            shop.AddRange(Session["FreshOrder"] as List<ShopViewModels>);

            return View(shop);
        }

        public ActionResult Delete(int id)
        {
            _rentRep.Delete(id);
            _rentRep.Save();
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}