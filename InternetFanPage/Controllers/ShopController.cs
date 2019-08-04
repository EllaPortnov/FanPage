using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InternetFanPage.Services;
using InternetFanPage.Models;

namespace InternetFanPage.Controllers
{
    public class ShopController : Controller
    {
        ShopService shopService = new ShopService();

        public ActionResult GetAllProducts()
        {
            return Json(shopService.GetAllProductsFromInventory());
        }
        [HttpGet]
        public IEnumerable<CategoryResult> GetAllCategories()
        {
            return shopService.GetAllCategories();
        }

        public ActionResult SearchProducts(string term)
        {
            return View(shopService.SearchProducts(term));
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            if (shopService.CreateProduct(product))
                return Json(true);
            else
                return Json(false);
        }

        [HttpDelete]
        public ActionResult DeleteProduct(int id)
        {
            if (shopService.DeleteProduct(id))
                return Json(id);
            else
                return Json(false);
        }

        [HttpGet]
        public ActionResult ProductsStock()
        {
            return Json(shopService.GetProductsStock());
        }
    }
}
