using System;
using System.Text;
using InternetFanPage.Models;
using InternetFanPage.Services;
//using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;

namespace InternetFanPage.Controllers
{
    public class UsersController : Controller
    {
        UserServices userService = new UserServices();

        public ActionResult AttemptLogin(LoginDetails details)
        {
            LoginResult result = userService.AttemptLogin(details);
            if (result.LoginSucceeded)
            {
                User user = userService.GetUser(details.Username);
               // HttpContext.Session.Set("User", Encoding.ASCII.GetBytes(user.FirstName));
               //HttpContext.Session.Set("IsAdmin", BitConverter.GetBytes(user.IsAdmin == 1));
            }
            return View();
        }


        public ActionResult Register(RegisterDetails details)
        {
            return View();
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}