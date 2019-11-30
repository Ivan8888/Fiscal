using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClientMVC.Models;
using ClientMVC.Data;
using ClientMVC.Services;

namespace ClientMVC.Controllers
{
    public class ProductController : Controller
    {
        FiscalContext _context;
        IProductNumber _productNumber;

        public ProductController(FiscalContext context, IProductNumber productNumber)
        {
            _context = context;
            _productNumber = productNumber;
        }

        public IActionResult Index()
        {
            return View(_context.Products.ToList());
        }

        public IActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult CreateHtml()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateHtml(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                _productNumber.AddProductCount();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}