using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Data;
using WebShop.Models;
using WebShop.Services;
using WebShop.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace WebShop.Controllers
{
    [Route("{controller}/{action}")]
    public class ProductController : Controller
    {
        ShopContext _dbcontext;
        IProductCount _productCount;
        IDistributedCache _distributedCache;
        ILogger<ProductController> _logger;
        public ProductController(ShopContext dbcontext, IProductCount productCount, IDistributedCache distributedCache, ILogger<ProductController> logger)
        {
            _dbcontext = dbcontext;
            _productCount = productCount;
            _distributedCache = distributedCache;
            _logger = logger;
        }

        [Route("{id}")]
        [ServiceFilter(typeof(ActionResultFilters))]
        public IActionResult TestFilters(int id)
        {
            Product product = _dbcontext.Products.Find(id);
            return Content(product.Name);
        }

        //[Authorize(Policy = "AdminOrUserWithEmailAssertion")]
        //[Authorize]
        public IActionResult Index()
        {
            _logger.LogTrace("Trace log");
            _logger.LogDebug("Debug log");
            _logger.LogInformation("Information log");
            _logger.LogWarning("Warning log");
            _logger.LogError("Error log");
            _logger.LogCritical("Critical log");

            _logger.LogDebug(112, new Exception("greska greška"), "doslo do greske jer je vrednost parametra {id}", 452);

            List<Product> products = _dbcontext.Products.ToList();
            ViewBag.Info = "Partial view can access ViewBag from View that call him!";
            return View(products);
        }

        [HttpGet]
        //[Authorize(Roles = "Support,Admin")]
        [Authorize(Policy = "JustRolesAdminSupport")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Products.Add(product);
                _dbcontext.SaveChanges();
                _productCount.AddProductNumber();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Edit(int id)
        {
            Product product = _dbcontext.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        [Route("{id:int}")]
        public async Task<IActionResult> Edit(Product product, int id)
        {
            Product productToUpdate = _dbcontext.Products.Single(p => p.ProductId == id);

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

        [HttpGet]
        [Route("{id:int}")]
        //[Authorize(Policy = "RequireEmail")]
        [Authorize(Policy = "JustOlderThan18")]
        public IActionResult Delete(int id)
        {
            Product product = _dbcontext.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Route("{id:int}")]
        public IActionResult DeletePost(int id)
        {
            Product product = _dbcontext.Products.Find(id);
            _dbcontext.Products.Remove(product);
            _dbcontext.SaveChanges();
            _productCount.RemoveProductNumber();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Product product = _dbcontext.Products.Where(p => p.ProductId == id).Single();
            return View(product);
        }
    }
}