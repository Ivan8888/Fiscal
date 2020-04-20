using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FiscalClientMVC.Data;
using FiscalClientMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Caching.Memory;

namespace FiscalClientMVC.Controllers
{
    //[EnableCors("FromGoogle")]
    public class ProductController : Controller
    {
        FiscalContext _dbcontext;
        IMemoryCache _memoryCache;
        const string cache_products_key = "cache_products_key";

        public ProductController(FiscalContext dbcontext, IMemoryCache memoryCache)
        {
            _dbcontext = dbcontext;
            _memoryCache = memoryCache;
        }

        //[Authorize(Policy = "CanView")]
        public IActionResult Index()
        {
            List<Product> products;
            if (!_memoryCache.TryGetValue(cache_products_key, out products))
            {
                products = _dbcontext.Products.ToList();
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                //cache duration will be one minute
                options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                _memoryCache.Set(cache_products_key, products, options);
            }

            return View(products);
        }

        #region Create

        [HttpGet]
        [Authorize(Policy = "CanCreate")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CanCreate")]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Products.Add(product);
                _dbcontext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        #endregion

        #region Edit

        [HttpGet]
        [Authorize(Policy = "CanEdit")]
        public IActionResult Edit(int id)
        {
            Product product = _dbcontext.Products.First(p => p.ProductId == id);
            return View(product);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CanEdit")]
        public async Task<IActionResult> EditPost(Product product)
        {
            Product productToUpdate = _dbcontext.Products.Single(p => p.ProductId == product.ProductId);

            if (await TryUpdateModelAsync<Product>(productToUpdate, "", p => p.Name, p => p.Price))
            {
                try
                {
                    _dbcontext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Unable to save changes, try again.");
                }
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete

        [HttpGet]
        [Authorize(Policy = "CanDelete")]
        public IActionResult Delete(int id)
        {
            Product product = _dbcontext.Products.Single(p => p.ProductId == id);
            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CanDelete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Product product = _dbcontext.Products.Single(p => p.ProductId == id);
            _dbcontext.Products.Remove(product);
            _dbcontext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}