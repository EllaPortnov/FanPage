using InternetFanPage.Models;
using InternetFanPage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetFanPage.Controllers
{
    public class UserController : Controller
    {
        UserServices userService = new UserServices();

        public ActionResult Register(RegisterDetails details)
        {
            if (userService.Register(details))
                return View();

            return View(500);
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}