using _19T1021302.BusinessLayers;
using _19T1021302.DomainModels;
using _19T1021302.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _19T1021302.Web.Controllers
{
    public class CustomerAccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(string email, string password)
		{
            ViewBag.email = email;
            CusAccount cusAccount = null;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                ModelState.AddModelError("", "Vui lòng nhập đủ thông tin");
            if (Hashing.ValidatePassword(password, CustomerAccountService.GetHashedPassword(email)))
                cusAccount = CustomerAccountService.Authorize(email);
            if (cusAccount == null)
            {
                ModelState.AddModelError("", "Sai email hoặc mật khẩu");
                return View();
            }
            string cookieString = Newtonsoft.Json.JsonConvert.SerializeObject(cusAccount);
            FormsAuthentication.SetAuthCookie(cookieString, false);
            return RedirectToAction("Index", "Client");
        }
        [AllowAnonymous]
        public ActionResult Register()
		{
            return View();
		}
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(CusAccount data, string password="")
        {
            if (string.IsNullOrEmpty(data.CustomerName))
                ModelState.AddModelError(nameof(data.CustomerName), "Tên không được để trống");
            if (string.IsNullOrEmpty(data.ContactName))
                ModelState.AddModelError(nameof(data.ContactName), "Tên liên lạc không được để trống");
            if (string.IsNullOrEmpty(data.Country))
                ModelState.AddModelError(nameof(data.Country), "Quốc gia không được để trống");
            if (string.IsNullOrEmpty(data.Email))
                ModelState.AddModelError(nameof(data.Email), "Email không được để trống");
            if (string.IsNullOrEmpty(password))
                ModelState.AddModelError(nameof(password),"Vui lòng nhập mật khẩu");
            if(CustomerAccountService.ExistAccountCustomer(data.Email))
                ModelState.AddModelError(nameof(data.Email), "Email này đã được dùng để đăng kí");
            data.PostalCode = data.PostalCode ?? "";
            data.Confirmed = false;
            data.HashedPassword = Hashing.HashPassword(password);
            if (!ModelState.IsValid)
                return View("Register", data);
            CustomerAccountService.RegisterAccount(data);
            return View( "Login");
        }
    }
}