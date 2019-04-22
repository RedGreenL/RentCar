using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using RentCar.Interfaces;
using RentCar.Models;
using RentCar.ViewModels;

namespace RentCar.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {

        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _db;
        private readonly IAddressRepository _addressRep;
        private readonly IDistrictRepository _districtRep;
        private readonly ICityRepository _cityRep;




        public UserController(IAddressRepository addressRep, IDistrictRepository districtRep, ICityRepository cityRep)
        {
            this._addressRep = addressRep;
            this._districtRep = districtRep;
            this._cityRep = cityRep;
            _db = new ApplicationDbContext();
           _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
           _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));

        }

        
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);




            return View(_userManager.Users.OrderBy(x => x.UserName).ToPagedList(pageNumber, pageSize));

        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.Role = _roleManager.Roles.ToList();
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(UserViewModels userRole)
        {
            try
            {
                var user = new ApplicationUser()
                {
                    Email = userRole.Email,
                    PhoneNumber = userRole.PhoneNumber,
                    UserName = userRole.UserName,
                };

                _userManager.Create(user, userRole.Password);
                _userManager.AddToRole(user.Id, userRole.RoleName);

                return RedirectToAction("Index");
            }
            catch
            {
               
            }
            return View();
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.Role = _roleManager.Roles.ToList();
            var user = _userManager.FindById(id);
            var userRole = _userManager.GetRoles(user.Id);
            UserViewModels userView = new UserViewModels()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                UserId = user.Id,
                RoleName = userRole.FirstOrDefault(),
            };
            return View(userView);
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, UserViewModels userView)
        {
            try
            {
                var user = _userManager.FindById(id);

                user.Email = userView.Email;
                user.PhoneNumber = userView.PhoneNumber;
                user.UserName = userView.UserName;

                //if (userView.Password != null)
                //{
                //    _userManager.ChangePassword(id, user.PasswordHash, userView.Password);
                //}
                _userManager.Update(user);

                string userCurentRole = _userManager.GetRoles(userView.UserId).FirstOrDefault();
                if (userView.RoleName != null)
                {
                    //_userManager.RemoveFromRole(userView.UserId, userCurentRole);
                    _userManager.AddToRole(userView.UserId, userView.RoleName);
                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            return View(_userManager.FindById(id));
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                _userManager.Delete(_userManager.FindById(id));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
