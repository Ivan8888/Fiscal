using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClientMVC.Models;
using ClientMVC.Data;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using ClientMVC.Services;

namespace ClientMVC.Controllers
{
    public class ProductController : Controller
    {
        private FiscalContext _context;
        private IProductNumber _productNumber;

        public ProductController(FiscalContext context, IProductNumber productNumber)
        {
            _context = context;
            _productNumber = productNumber;
        }

        public IActionResult Index()
        {
            List<Product> products = _context.Products.ToList();
            return View(products);
        }

        [HttpGet]
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
                _productNumber.AddProductCount();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product product = _context.Products.SingleOrDefault(p => p.ProductId == id);
            if(product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id)
        {
            Product productToUpdate = _context.Products.SingleOrDefault(p => p.ProductId == id);

            if (await TryUpdateModelAsync<Product>(productToUpdate, "", p => p.Name, p => p.Price))
            {
                try
                {
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch(DbUpdateException)
                {
                    ModelState.AddModelError("", "Eror while updating product, try again latter!");
                }
            }

            return View(productToUpdate);
        }

        public IActionResult Delete(int id) 
        {
            Product product = _context.Products.SingleOrDefault(p => p.ProductId == id);
            if(product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}