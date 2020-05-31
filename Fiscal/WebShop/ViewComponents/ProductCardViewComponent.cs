using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models;
using WebShop.Data;

namespace WebShop.ViewComponents
{
    public class ProductCardViewComponent : ViewComponent
    {
        ShopContext _dbcontext;

        public ProductCardViewComponent(ShopContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public Task<IViewComponentResult> InvokeAsync(int id)
        {
            Product product = _dbcontext.Products.Find(id);
            return Task.FromResult<IViewComponentResult>(View("Default", product));
        }
    }
}
