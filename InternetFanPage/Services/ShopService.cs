using System;
using System.Collections.Generic;
using System.Linq;
using InternetFanPage.Models;

namespace InternetFanPage.Services
{
    public class ShopService
    {
        public IList<Product> GetAllProductsFromInventory()
        {
            using (var context = new FanPageContext())
            {
                return context.Products.ToList();
            }
        }

        public IList<CategoryResult> GetAllCategories()
        {
            using (var context = new FanPageContext())
            {
                var categories = context.Categories.ToList();

                var productsCount = context.Products.GroupBy(p => p.CategoryID)
                .Select(group => new
                {
                    categoryId = group.FirstOrDefault().CategoryID,
                    count = group.Count()
                }).ToList();

                return categories.Join(productsCount,
                                       c => c.CategoryID,
                                       pc => pc.categoryId,
                                       (c, pc) => new CategoryResult()
                                       {
                                           CategoryID = c.CategoryID,
                                           Name = c.Name,
                                           Count = pc.count
                                       })
                                 .ToList();
            }
        }

        public IList<Product> SearchProducts(string term)
        {
            using (var context = new FanPageContext())
            {
                return context.Products.Where(p => p.Name.Contains(term) || p.Description.Contains(term)).ToList();
            }
        }

        public bool CreateProduct(Product product)
        {
            using (var context = new FanPageContext())
            {
                try
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }
        }

        public bool UpdateProduct(Product product)
        {
            using (var context = new FanPageContext())
            {
                var targetProduct = context.Products.Where(p => p.ProductID == product.ProductID).FirstOrDefault();

                targetProduct.Name = product.Name;
                targetProduct.Description = product.Description;
                targetProduct.Price = product.Price;
                targetProduct.CategoryID = product.CategoryID;

                if (product.Image != null)
                    targetProduct.Image = product.Image;

                try
                {
                    context.Products.Update(targetProduct);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }
        }

        public bool DeleteProduct(int id)
        {
            using (var context = new FanPageContext())
            {
                var targetProduct = context.Products.Where(p => p.ProductID == id).FirstOrDefault();
                context.Products.Remove(targetProduct);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }
        }

        public ProductResult GetProduct(int id)
        {
            using (var context = new FanPageContext())
            {
                return context.Products
                    .Join(context.Categories, p => p.CategoryID, c => c.CategoryID, (product, category) => new ProductResult(product, category))
                    .GroupJoin(context.Inventory, p => p.ProductID, i => i.ProductID, (product, inventory) => new ProductResult(product, inventory))
                    .FirstOrDefault(p => p.ProductID == id);

            }
        }

        public ProductStockResult[] GetProductsStock()
        {
            using (var context = new FanPageContext())
            {
                return context.Inventory.Join(context.Products, i => i.ProductID, p => p.ProductID, (inventory, product) => new
                {
                    Product = product,
                    Inventory = inventory
                })
                .GroupBy(x => x.Product.Name)
                .Select(x => new ProductStockResult()
                {
                    ProductName = x.Key,
                    Count = x.Sum(y => y.Inventory.Quantity)
                })
                .ToArray();
            }
        }

        public IList<Product> GetProductsByCategory(int id)
        {
            using (var context = new FanPageContext())
            {
                return context.Products.Where(p => p.CategoryID == id).ToList();
            }
        }
    }
}
