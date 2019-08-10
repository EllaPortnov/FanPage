using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InternetFanPage.Services;
using InternetFanPage.Models;
using Accord.MachineLearning.Rules;

namespace InternetFanPage.Controllers
{
    public class ShopController : Controller
    {
        ShopService shopService = new ShopService();

        public ActionResult GetAllProducts()
        {
            return Json(shopService.GetAllProductsFromInventory());
        }

        public ActionResult SalesPerCategory()
        {
            return Json(shopService.SalesPerCategory(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllCategories()
        {
            var Categories = shopService.GetAllCategories();
            return Json(Categories, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllAndNullCategories()
        {
            var Categories = shopService.GetAllAndNullCategories();
            return Json(Categories, JsonRequestBehavior.AllowGet);
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

        //[HttpPost]
        //public ActionResult RecommendProducts(int userId)
        //{
        //    //var salesByUser = shopService.GetAllSalesByUser();

        //    //List<int[]> tempDataset = new List<int[]>();
        //    //int[] currUserSales = null;

        //    //foreach (var userSales in salesByUser)
        //    //{
        //    //    if (userSales.UserID == userId)
        //    //    {
        //    //        currUserSales = userSales.Products.ToArray();
        //    //    }

        //    //    tempDataset.Add(userSales.Products.ToArray());
        //    //}

        //    //int[][] dataset = tempDataset.ToArray();

        //    //// We will use Apriori to determine the frequent item sets of this database.
        //    //// To do this, we will say that an item set is frequent if it appears in at 
        //    //// least 3 transactions of the database: the value 3 is the support threshold.

        //    //// Create a new a-priori learning algorithm with support 3
        //    //Apriori apriori = new Apriori(threshold: 1, confidence: 0.5);

        //    ////// Use the algorithm to learn a set matcher
        //    //AssociationRuleMatcher<int> classifier = apriori.Learn(dataset);

        //    //// Use the classifier to find orders that are similar to 
        //    //// orders where clients have bought items 1 and 2 together:
        //    //int[][] matches = classifier.Decide(currUserSales);

        //    //List<ProductResult> recommededProducts = new List<ProductResult>();

        //    //if (matches.Length > 0)
        //    //{
        //    //    int[] tmpRecommendedProducts = matches[0];
        //    //    foreach (var product in tmpRecommendedProducts)
        //    //    {
        //    //        recommededProducts.Add(shopService.GetProduct(product));
        //    //    }
        //    //}

        //    var recommededProducts = shopService.RecommendProducts(userId);

        //    return Json(recommededProducts);
        //}
    }
}
