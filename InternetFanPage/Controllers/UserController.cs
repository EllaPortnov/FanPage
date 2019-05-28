using InternetFanPage.Models;
using InternetFanPage.Services;
using System;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace InternetFanPage.Controllers
{
    public class UserController : Controller
    {
        UserServices userService = new UserServices();

        public IActionResult AttemptLogin(LoginDetails details)
        {
            LoginResult result = userService.AttemptLogin(details);
            if (result.LoginSucceeded)
            {
                User user = userService.GetUser(details.Username);
                HttpContext.Session.Set("User", Encoding.ASCII.GetBytes(user.FirstName));
                HttpContext.Session.Set("IsAdmin", BitConverter.GetBytes(user.IsAdmin == 1));
            }
            return new JsonResult(result);
        }


        public ActionResult Register(RegisterDetails details)
        {
            if (userService.Register(details))
                return Ok();

            return StatusCode(500);
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}