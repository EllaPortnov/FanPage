using System.Collections.Generic;


namespace InternetFanPage.Models
{
    public class ProductsPageModel
    {

        public IEnumerable<Product> Products
        {
            get;
            set;
        }

        public IEnumerable<CategoryResult> Categories
        {
            get;
            set;
        }
        
        public IEnumerable<ProductResult> Recommended { get; set; }

        public ProductResult TopSale { get; set; }

        public ProductsPageModel()
        {
        }
    }
}