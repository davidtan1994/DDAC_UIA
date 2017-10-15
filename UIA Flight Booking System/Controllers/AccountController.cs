using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UIA_Flight_Booking_System.Models;
using System.Web.Security;
using UIA_Flight_Booking_System.ViewModels;

namespace UIA_Flight_Booking_System.Controllers
{
    public class AccountController : Controller
    {
        private UIAEntities db = new UIAEntities();

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User model)
        {
            var user = (from u in db.Users
                        where u.Username == model.Username
                        select u).FirstOrDefault();

            if (user != null)
            {
                if (user.Username == model.Username.Trim() && user.Password == model.Password)
                {
                    FormsAuthentication.SetAuthCookie(user.Username + "|" + user.UserID, false);
                    return RedirectToAction("Home", "Customer");
                }
            }

            TempData["LoginFail"] = "Login fail";
            return View(model);
        }

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public ActionResult CustomerRegistration()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult CustomerRegistration(CustomerRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(x => x.Username == model.Username))
                {
                    ModelState.AddModelError("Username", "Username already exist");
                    return View(model);
                }
                if (db.Customers.Any(x => x.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email already used to register");
                    return View(model);
                }

                User user = new User()
                {
                    UserID = Guid.NewGuid(),
                    Username = model.Username,
                    Password = model.Password,
                    Role = "Customer"
                };
                db.Users.Add(user);
                db.SaveChanges();

                Customer customer = new Customer()
                {
                    CustomerID = user.UserID,
                    Fname = model.Fname,
                    Lname = model.Lname,
                    Email = model.Email,
                    ContactNum = model.ContactNum,
                    IcNum = model.IcNum,
                    Address = model.Address,
                    DOB = model.DOB,
                    RegistrationDate = DateTime.Today,
                    Gender = model.Gender
                };
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }
    }
}