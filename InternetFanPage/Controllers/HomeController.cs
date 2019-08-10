using InternetFanPage.Models;
using System.Linq;
using System.Web.Mvc;
using InternetFanPage.Services;

namespace InternetFanPage.Controllers
{
    public class HomeController : Controller
    {
        private FanPageContext ct = new FanPageContext();
        ShopService shopService = new ShopService();
        ProductsPageModel model = new ProductsPageModel();

        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult Concerts()
        {
            return View(ct.Concerts.AsEnumerable());
        }

        public ActionResult Manager()
        {
            return View();
        }

        public ActionResult Shop()
        {
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

        public ActionResult Products(string searchTerm, int categoryId = -1)
        {
            //LoadUserData();

            if (categoryId > 0)
            {
                model.Products = shopService.GetProductsByCategory(categoryId);
            }
            else
            {
                model.Products = shopService.SearchProducts(searchTerm ?? string.Empty);
            }

            if (model.Categories == null)
            {
                model.Categories = shopService.GetAllCategories();
            }

            return View(model);
        }


        public ActionResult SearchProducts(string term)
        {
            return View(shopService.SearchProducts(term));
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