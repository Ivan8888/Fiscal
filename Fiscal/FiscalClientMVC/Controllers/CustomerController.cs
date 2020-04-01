using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FiscalClientMVC.Data;
using FiscalClientMVC.Models;
using Microsoft.EntityFrameworkCore;
using FiscalClientMVC.Services;
using Microsoft.Extensions.Logging;

namespace FiscalClientMVC.Controllers
{
    public class CustomerController : Controller
    {
        FiscalContext _dbcontext;
        ICustomerCount _customerCount;
        ILogger<CustomerController> _logger;

        public CustomerController(FiscalContext context, ICustomerCount customerCount, ILogger<CustomerController> logger)
       {
            _dbcontext = context;
            _customerCount = customerCount;
            _logger = logger;
            _logger.LogDebug("CustomerController constructor.");
            _logger.LogInformation("CustomerController constructor.");

            int id = 5;
            _logger.LogInformation(100, new Exception("This is exception for loging!"), "Getting item {Id} at {RequestTime}", id, DateTime.Now);
        }

        public IActionResult Index()
        {
            _logger.LogDebug("Index action");
            _logger.LogInformation("Index action");
            List<Customer> customers = _dbcontext.Customers.ToList();
            return View(customers);
        }

        public IActionResult Details(int id)
        {
            Customer customer = _dbcontext.Customers.Single(c => c.CustomerId == id);
            return View(customer);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Customers.Add(customer);
                _dbcontext.SaveChanges();
                _customerCount.AddCustomerNumber();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Customer customer = _dbcontext.Customers.Single(c => c.CustomerId == id);
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer customerToUpdate = _dbcontext.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (await TryUpdateModelAsync<Customer>(customerToUpdate, "", c => c.Name, c => c.Address, c => c.Email, c => c.IsRetail))
            {
                try
                {
                    _dbcontext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes, try again.");
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}