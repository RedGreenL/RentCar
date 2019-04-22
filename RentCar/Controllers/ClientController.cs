using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PagedList;
using RentCar.Interfaces;
using RentCar.Models;
using RentCar.Repositories;

namespace RentCar.Controllers
{
    //[Authorize]
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRep;
        private readonly IDistrictRepository _districtRep;
        private readonly IAddressRepository _addressRep;
        private readonly ICityRepository _cityRep;

        public ClientController(IClientRepository clientRep, IDistrictRepository districtRep, IAddressRepository addressRep, ICityRepository cityRep)
        {
            this._clientRep = clientRep;
            this._districtRep = districtRep;
            this._addressRep = addressRep;
            this._cityRep = cityRep;
        }


        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(_clientRep.GetAll().OrderBy(x => x.Name).ToPagedList(pageNumber, pageSize));
        }


        [HttpPost]
        public JsonResult PopulationCities(int id)
        {
            var cities = _cityRep.GetAll().Where(i => i.District_ID == id)
                .Select(a => new {a.Name, a.City_ID});

            JavaScriptSerializer javaSerealizer = new JavaScriptSerializer();
            var result = javaSerealizer.Serialize(cities);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.District = _districtRep.GetAll().ToList();
            return View();
        }


        [HttpPost]
        public ActionResult Create(ClientViewModels clientV, int? cityDropDown)
        {
            try
            {
                var address = new Addres()
                {
                    City_ID = cityDropDown,
                    District_ID = clientV.District_ID,
                    Flat = clientV.Flat,
                    Street = clientV.Street
                };

                _addressRep.Create(address);
                _addressRep.Save();
                var addressId = address.Addres_ID;

                var client = new Client()
                {
                    Name = clientV.Name,
                    Surname = clientV.Surname,
                    Email = clientV.Email,
                    Idnp = clientV.Idnp,
                    Phone = clientV.Phone,
                    Addres_ID = addressId,
                };

                _clientRep.Create(client);
                _clientRep.Save();

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View(e);
            }
        }

        // GET: ClientCRUD/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)     { return RedirectToAction("Index"); }
            
            ViewBag.District = _districtRep.GetAll();
            var client = _clientRep.GetById((int)id);
            var cv = new ClientViewModels()
            {
                City_ID = client.Addres.City_ID ?? 0,
                District_ID = client.Addres.District_ID ?? 0,
                Flat = client.Addres.Flat,
                Street = client.Addres.Street,
                Email = client.Email,
                Idnp = client.Idnp,
                Name = client.Name,
                Phone = client.Phone,
                Surname = client.Surname,
                Client_ID = client.Client_ID,
                Addres_ID = client.Addres_ID
            };

            return View(cv);
        }

        // POST: ClientCRUD/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ClientViewModels clientV, int? cityDropDown)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var address = _addressRep.GetById((int)clientV.Addres_ID);
                        address.District_ID = clientV.District_ID;
                        if(cityDropDown != null) { address.City_ID = cityDropDown; }
                        
                        address.Flat = clientV.Flat;
                        address.Street = clientV.Street;

                    _addressRep.Update(address.Addres_ID, address);
                    _addressRep.Save();

                    var client = _clientRep.GetById(clientV.Client_ID);
                        client.Surname = clientV.Surname;
                        client.Email = clientV.Email;
                        client.Idnp = clientV.Idnp;
                        client.Name = clientV.Name;
                        client.Phone = clientV.Phone;
                        client.Addres_ID = clientV.Addres_ID;

                    _clientRep.Update(clientV.Client_ID, client);
                    _clientRep.Save();


                    return RedirectToAction("Index");
                }

                return RedirectToAction("Edit");

            }
            catch(Exception)
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            return View(_clientRep.GetById(id));
        }

        public ActionResult Delete(int id)
        {
            return View(_clientRep.GetById(id));
        }


        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _clientRep.Delete(id);
                _clientRep.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
