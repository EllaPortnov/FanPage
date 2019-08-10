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

        public Product RecommendProducts(int? userId)
        {
            // Assume that a large supermarket tracks sales data by stock-keeping unit
            // (SKU) for each item: each item, such as "butter" or "bread", is identified 
            // by a numerical SKU. The supermarket has a database of transactions where each
            // transaction is a set of SKUs that were bought together.

            // Let the database of transactions consist of following itemsets:

            //SortedSet<int>[] dataset =
            //{
            //    // Each row represents a set of items that have been bought 
            //    // together. Each number is a SKU identifier for a product.
            //    new SortedSet<int> { 1, 2, 3, 4 }, // bought 4 items
            //    new SortedSet<int> { 1, 2, 4 },    // bought 3 items
            //    new SortedSet<int> { 1, 2 },       // bought 2 items
            //    new SortedSet<int> { 2, 3, 4 },    // ...
            //    new SortedSet<int> { 2, 3 },
            //    new SortedSet<int> { 3, 4 },
            //    new SortedSet<int> { 2, 4 },
            //};

            var salesByUser = shopService.GetAllSalesByUser();

            //using (var ctx = new FanPageContext())
            //{
            //    // GroupBy to get all of the sales of the same user
            //}

            // We will use Apriori to determine the frequent item sets of this database.
            // To do this, we will say that an item set is frequent if it appears in at 
            // least 3 transactions of the database: the value 3 is the support threshold.

            // Create a new a-priori learning algorithm with support 3
            Apriori apriori = new Apriori(threshold: 3, confidence: 0);

            // Use the algorithm to learn a set matcher
            AssociationRuleMatcher<int> classifier = apriori.Learn(dataset);

            // Use the classifier to find orders that are similar to 
            // orders where clients have bought items 1 and 2 together:
            int[][] matches = classifier.Decide(new[] { 1, 2 });

            // The result should be:
            // 
            //   new int[][]
            //   {
            //       new int[] { 4 },
            //       new int[] { 3 }
            //   };

            // Meaning the most likely product to go alongside the products
            // being bought is item 4, and the second most likely is item 3.

            // We can also obtain the association rules from frequent itemsets:
            AssociationRule<int>[] rules = classifier.Rules;

            // The result will be:
            // {
            //     [1] -> [2]; support: 3, confidence: 1, 
            //     [2] -> [1]; support: 3, confidence: 0.5, 
            //     [2] -> [3]; support: 3, confidence: 0.5, 
            //     [3] -> [2]; support: 3, confidence: 0.75, 
            //     [2] -> [4]; support: 4, confidence: 0.66, 
            //     [4] -> [2]; support: 4, confidence: 0.8, 
            //     [3] -> [4]; support: 3, confidence: 0.75, 
            //     [4] -> [3]; support: 3, confidence: 0.6 
            // };

            return null;
        }
    }
}
