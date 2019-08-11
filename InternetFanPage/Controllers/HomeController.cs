using InternetFanPage.Models;
using System.Linq;
using System.Web.Mvc;
using InternetFanPage.Services;
using System;

namespace InternetFanPage.Controllers
{
    public class HomeController : Controller
    {
        private FanPageContext ct = new FanPageContext();
        ShopService shopService = new ShopService();
        ProductsPageModel model = new ProductsPageModel();
        ConcertsService concertService = new ConcertsService();

        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult Concerts(string searchTermName, int? searchTermPrice, DateTime? searchTermDateStart, DateTime? searchTermDateEnd)
        {
            //var model = null;
            
            if (searchTermName == null && searchTermPrice == null && searchTermDateStart == null && searchTermDateEnd == null)
            {
                return View(ct.Concerts.AsEnumerable());
            }
            else
            {
                 var model = concertService.searchConcert(searchTermName ?? string.Empty, searchTermPrice, searchTermDateStart, searchTermDateEnd);
                 return View(model);
            }

        }

        public ActionResult Manager()
        {
            return View();
        }

        public ActionResult Shop()
        {
            //if (Session["UserID"] != null)
            //{
            //    int userId = int.Parse(Session["UserID"].ToString());

            //    return View(shopService.RecommendProducts(userId));
            //}

            return View();
        }

        //public ActionResult Products()
        //{

        //    model.Products = shopService.GetAllProductsFromInventory();
        //    model.Categories = shopService.GetAllCategories();

        //    return View(model);
        //}
        public ActionResult UserProducts()
        {
            return View();
        }

        public ActionResult Products(string searchTermName,  int? searchTermPrice,  int categoryId = -1)
        {
            //LoadUserData();
            if (Session["UserID"] != null)
            {
                int userId = (int)Session["UserID"];

                model.Recommended = shopService.RecommendProducts(userId);
            }

            if (categoryId > 0)
            {
                model.Products = shopService.GetProductsByCategory(categoryId);
            }
            else
            {
                model.Products = shopService.SearchProducts(searchTermName ?? string.Empty, searchTermPrice);
            }

            if (model.Categories == null)
            {
                model.Categories = shopService.GetAllCategories();
            }

            return View(model);
        }


        public ActionResult SearchProducts(string termName, int price)
        {
            return View(shopService.SearchProducts(termName, price));
        }

        public ActionResult Error()
        {
           // return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
           return null;
        }

        [HttpGet]
        public ActionResult GetProduct(int id)
        {
            var product = shopService.GetProduct(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return Json(product, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult BuyProduct(int userId,int id)
        {
            return Json(shopService.BuyProduct(userId, id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product product)
        {
            return Json(shopService.UpdateProduct(product));
        }
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            return Json(shopService.AddProduct(product));
        }
        [HttpPost]
        public ActionResult UpdateConsert(Concert concert)
        {
            return Json(shopService.UpdateConcert(concert));
        }
        [HttpPost]
        public ActionResult UpdateInventory(Inventory inventory)
        {
            return Json(shopService.UpdateInventory(inventory));
        }
        [HttpDelete]
        public ActionResult DeleteConcert(int id)
        {
            if (shopService.DeleteConcert(id))
                return Json(id);
            else
                return Json(false);
        }
    }
}