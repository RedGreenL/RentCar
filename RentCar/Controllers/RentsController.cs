using RentCar.Interfaces;
using RentCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using RentCar.ViewModels;
using PagedList;

namespace RentCar.Controllers
{
    public class RentsController : Controller
    {
        private readonly IClientRepository _clientRep;
        private readonly IDistrictRepository _districtRep;
        private readonly IAddressRepository _addressRep;
        private readonly ICityRepository _cityRep;
        private readonly IRentsRepository _rentsRep;
        private readonly ICarRepository _carRep;
        private readonly IDiscountRepository _discountRep;

        public RentsController(IRentsRepository rentsRep, IClientRepository clientRep, IDistrictRepository districtRep, 
            IAddressRepository addressRep, ICityRepository cityRep, ICarRepository carRep, IDiscountRepository discountRep)
        {
            _clientRep = clientRep;
            _districtRep = districtRep;
            _addressRep = addressRep;
            _cityRep = cityRep;
            _rentsRep = rentsRep;
            _carRep = carRep;
            _discountRep = discountRep;
        }
        

        public ActionResult Index(string searchPhone, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (searchPhone != null)
            {
                var findByPhone = _rentsRep.FindByPhone(searchPhone);

                if (findByPhone != null)   { return View(findByPhone.OrderBy(x => x.Client.Name).ToPagedList(pageNumber, pageSize)); }
            }
            return View(_rentsRep.GetAll().OrderBy(x => x.Client.Name).ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Details(int id)
        {
            var rent = _rentsRep.GetById(id);
            var car = _carRep.GetById(rent.Car_ID);
            var client = _clientRep.GetById((int)rent.Client_ID);

            var rentDetails = new RetnsViewModels()
            {
                Brand = car.Brand,
                Model = car.Model,
                Engine = car.Engine,
                Color = car.Color,
                StartData = rent.StartData,
                EndData = rent.EndData,
                Price = rent.Price,
                Driver = rent.Driver,
                Name = client.Name,
                Surname = client.Surname,
                Phone = client.Phone,
                Email = client.Email,
                District = client.Addres.District.Name,
                City = client.Addres.City.Name,
                Street = client.Addres.Street,
                Flat = client.Addres.Flat
            };
            return View(rentDetails);
        }

        // If the user did earlier rental
        // We are finding his
        public ActionResult OldUser(int? carId, string searchPhone)
        {
            if (searchPhone != null)
            {
                var findUser = _clientRep.GetByPhone(searchPhone);
                if (findUser != null)   { ViewBag.Client = findUser; }
            }

            if (carId == null || carId <= 0)
            {
                var nullCar = new Car() { Car_ID = 0, Model = "Select Car" };
                ViewBag.CurentCar = nullCar;
            }
            else
            {
                ViewBag.CurentCar = _carRep.GetAll().Where(i => i.Car_ID == carId).FirstOrDefault();
            }


            ViewBag.Car = _carRep.GetAll();

            return View();
            
        }

        public ActionResult OrderOldClient(Rents rent)
        {
            if (ModelState.IsValid)
            {
                double price = _carRep.GetAll().Where(i => i.Car_ID == rent.Car_ID)
                    .Select(p => p.Price).FirstOrDefault();

                rent.Price = TotalPriceRent(rent.StartData, rent.EndData, price);
                _rentsRep.Create(rent);
                _rentsRep.Save();
                SaveInSession(rent.Car_ID, rent);
            }

            return RedirectToAction("OldUser");
        }

        // GET: Rents/Create
        public ActionResult Create(int? id)
        {

            if(id == null)
            {
                var nullCar = new Car() { Car_ID = 0, Model = "Select Car" };
                ViewBag.CurentCar = nullCar;
            }
            else
            {
                ViewBag.CurentCar = _carRep.GetAll().Where(i => i.Car_ID == id).FirstOrDefault();
                ViewBag.CarInfo = _carRep.GetById((int)id);
            }

            var c =_districtRep.GetAll();
            ViewBag.Car = _carRep.GetAll();
            ViewBag.District = c;
            
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(RetnsViewModels rentV, int cityDropDown )
        {
            try
            {

                // Insert addres for client and return last ID
                int addressId = InsertAddresReturnID(rentV, cityDropDown);

                // Insert client and return last ID
                int clinetId = InsertClientReturnID(rentV, addressId);

                // Get price for selected car
                double price = _carRep.GetAll().Where(i => i.Car_ID == rentV.Car_ID)
                    .Select(p => p.Price).FirstOrDefault();

                var rent = new Rents();
                rent.Car_ID = rentV.Car_ID;
                rent.Client_ID = clinetId;
                rent.Driver = rentV.Driver;
                rent.StartData = rentV.StartData;
                rent.EndData = rentV.EndData;
                rent.Price = TotalPriceRent(rentV.StartData, rentV.EndData, price);
                //if (Request.IsAuthenticated) { rent.Operator_ID = User.Identity.GetUserId().AsInt(); }

                _rentsRep.Create(rent);
                _rentsRep.Save();
                SaveInSession(rentV.Car_ID, rent);

                return RedirectToAction("Index", "Home");
            }
            catch(Exception e)
            {
                return View(e);
            }
        }

        private void SaveInSession(int carID, Rents rent)
        {
            var car = _carRep.GetById(carID);

            ShopViewModels shopView = new ShopViewModels()
            {
                CarID = car.Car_ID,
                Model = car.Model,
                Brand = car.Brand,
                StartData = rent.StartData,
                EndData = rent.EndData,
                Price = rent.Price,
                RentID = rent.Rent_ID
            };

            // Insert in Cookie client's shopping
            if (Session["FreshOrder"] != null)
            {
                var FOrder = new List<ShopViewModels>();
                FOrder.AddRange(Session["FreshOrder"] as List<ShopViewModels>);
                FOrder.Add(shopView);
            }
            else
            {
                var FOrder = new List<ShopViewModels>();
                FOrder.Add(shopView);
                Session["FreshOrder"] = FOrder;
            }
        }

        // Insert client and return ID
        private int InsertClientReturnID(RetnsViewModels rentV, int addressId)
        {
            var client = new Client()
            {
                Name = rentV.Name,
                Surname = rentV.Surname,
                Email = rentV.Email,
                Idnp = rentV.Idnp,
                Phone = rentV.Phone,
                Addres_ID = addressId
            };

            _clientRep.Create(client);
            _clientRep.Save();
            return client.Client_ID;
        }

        private int InsertAddresReturnID(RetnsViewModels rentV, int cityDropDown)
        {
            var address = new Addres()
            {
                City_ID = cityDropDown,
                District_ID = rentV.District_ID,
                Flat = rentV.Flat,
                Street = rentV.Street
            };

            _addressRep.Create(address);
            _addressRep.Save();
            return address.Addres_ID;;
        }

        public double TotalPriceRent(DateTime startDate, DateTime endDate, double price)
        {
            if(price == 0) { return 0; }
            var days = ((endDate - startDate).Days);
            var percent = _discountRep.PercentForDays(days);
            var discount = (percent / 100) * (days * price);
            var totalPrice = (days * price) - discount;

            return totalPrice;
        }


        public ActionResult Delete(int id)
        {
            return View(_rentsRep.GetById(id));
        }

        // POST: Rents/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
               _rentsRep.Delete(id);
               _rentsRep.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
