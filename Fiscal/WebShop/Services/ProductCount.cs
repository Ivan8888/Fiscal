using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Services
{
    public class ProductCount : IProductCount
    {
        int product_count;
        public ProductCount(ShopContext dbcontext)
        {
            product_count = dbcontext.Products.Count();
        }
        public void AddProductNumber()
        {
            product_count++;
        }

        public int GetProductNumber()
        {
            return product_count;
        }

        public void RemoveProductNumber()
        {
            product_count--;
        }
    }
}
