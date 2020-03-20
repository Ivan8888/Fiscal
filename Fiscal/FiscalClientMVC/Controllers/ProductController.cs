using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FiscalClientMVC.Data;

namespace FiscalClientMVC.Controllers
{
    public class ProductController : Controller
    {
        FiscalContext _dbcontext;

        public ProductController(FiscalContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public IActionResult Index()
        {
            return Content(_dbcontext.Products.First(p => p.ProductId == 1).Name);
        }
    }
}