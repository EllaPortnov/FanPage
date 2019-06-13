using InternetFanPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetFanPage.Controllers
{
    public class HomeController : Controller
    {
        private FanPageContext ct = new FanPageContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Concerts()
        {
            return View(ct.Concerts.AsEnumerable());
        }

        
    }
}