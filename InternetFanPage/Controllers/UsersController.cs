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
                Session["User"] = Encoding.ASCII.GetBytes(user.FirstName);
                Session["IsAdmin"] = BitConverter.GetBytes(user.IsAdmin == 1);

            }
            return Json(result);
        }

        public ActionResult Register(RegisterDetails details)
        {
            if (userService.Register(details))
                return Json(true);

            return Json(false);
        }

        public ActionResult CheckName(string username)
        {
            bool a = userService.DoesUserExists(username);
            return Json(a, JsonRequestBehavior.AllowGet);

        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}