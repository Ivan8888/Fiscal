using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApp.Data;
using TestApp.Models;
using TestApp.Services;
using TestApp.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Caching.Memory;

namespace TestApp.Controllers
{
    [ServiceFilter(typeof(ActionResultFilter))]
    [Route("{controller}/{action}")]
    //[EnableCors("FromGoogle")]
    public class CustomerController : Controller
    {
        //logging enum
        enum log_event_id
        {
            insert = 100,
            update = 200,
            delete = 300
        }

        SiteContext _context;
        ICustomerCount _customerCount;
        ILogger _logger;
        IMemoryCache _memoryCache;
        public CustomerController(SiteContext context, ICustomerCount customerCount, ILogger<CustomerController> logger, IMemoryCache memoryCache)
        {
            _context = context;
            _customerCount = customerCount;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        //[Authorize(Policy = "RequireEmail")]
        //[Authorize(Policy = "JustAdultUsers")]
        [DisableCors]
        public IActionResult Index()
        {
            List<Customer> customers = _context.Customers.ToList();
            return View(customers);
        }

        
        [HttpGet]
        //[Route("MetodZaInsert")]
        [Authorize(Policy = "AdultUserAndBasedOnCustomerCount")]
        public IActionResult Insert()
        {
            string controller = RouteData.Values["controller"].ToString();
            string action = RouteData.Values["action"].ToString();

            return View();
        }

        [HttpPost]
        //[Route("MetodZaInsert")]
        //[Authorize(Policy = "AdultUserAndBasedOnCustomerCount")]
        public IActionResult Insert(Customer customer)
        {
            _logger.LogInformation((int)log_event_id.insert, "Insert customer started.");
            if (ModelState.IsValid)
            {
                _logger.LogDebug((int)log_event_id.insert, "Model is valid.");
                _context.Customers.Add(customer);
                _context.SaveChanges();
                _customerCount.IncreaseCustomerNumber();
                _logger.LogDebug((int)log_event_id.insert, "Insert customer successed.");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _logger.LogDebug((int)log_event_id.insert, "Model is not valid.");
                return View();
            }
        }

        [HttpGet]
        //[Route("{id:int}")]
        public IActionResult Edit(int id)
        {
            string controller = RouteData.Values["controller"].ToString();
            string action = RouteData.Values["action"].ToString();

            string controller1 = Request.RouteValues["controller"].ToString();
            string action1 = Request.RouteValues["action"].ToString();
            //var form = Request.Form;
            string id_string = Request.Query["id"].ToString();

            Customer customer = _context.Customers.SingleOrDefault(c => c.CustomerId == id);
            if(customer != null)
            {
                return View(customer); 
            }

            return NotFound();
        }

        [HttpPost]
        //[Route("{id:int}")]
        public async Task<IActionResult> Edit(Customer customer, int id)
        {
            Customer customerToUpdate = _context.Customers.Single(c => c.CustomerId == id);

            if (await TryUpdateModelAsync<Customer>(customerToUpdate, "", c => c.Name, c => c.Address, c => c.Email))
            {
                try
                {
                    _context.SaveChanges();
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
        public IActionResult Delete(int id)
        {
            Customer customer = _context.Customers.Find(id);
            return View(customer);
        }

        [ActionName("Delete")]
        [Route("{id:int}")]
        public IActionResult DeleteConfirmed(int id)
        {
            Customer customer = _context.Customers.Find(id);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            _customerCount.DecreaseCustomerNumber();

            return RedirectToAction(nameof(Index));
        }

        //deference between caching and managing state is that caching is for all clients, state is for separate clients and it is provided by cookies
        public IActionResult First()
        {
            _memoryCache.Set("first", "first memory cache value");
            TempData["first"] = "first temp data value";

            return Content("This is first action");
        }

        public IActionResult Second()
        {
            string temp_result = (string)TempData["first"];
            string cache_result = (string)_memoryCache.Get("first");

            return Content($"Temp data: {temp_result ?? "No data"} Cache data: {cache_result ?? "No data"}");
        }

        public IActionResult One()
        {
            _memoryCache.Set("one", "one memory cache value");
            TempData["one"] = "one temp data value";

            return Content("This is one action");
        }

        public IActionResult Two()
        {
            string temp_result = (string)TempData["one"];
            string cache_result = (string)_memoryCache.Get("one");

            return Content($"Temp data: {temp_result ?? "No data"} Cache data: {cache_result ?? "No data"}");
        }
    }
}
