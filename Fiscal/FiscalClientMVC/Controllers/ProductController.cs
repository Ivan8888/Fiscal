using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FiscalClientMVC.Data;
using FiscalClientMVC.Models;

namespace FiscalClientMVC.Controllers
{
    public class ProductController : Controller
    {
        FiscalContext _dbcontext;

        public ProductController(FiscalContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product product = _dbcontext.Products.First(p => p.ProductId == id);
            return View(product); 
        }
    }
}