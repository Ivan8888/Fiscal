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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace FiscalClientMVC.Controllers
{
    //[EnableCors("EnableGoogleCors")]
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
            _logger.LogDebug("In constructor.");
        }

        public IActionResult Index()
        {
            _logger.LogDebug(" - started", nameof(Index), Request.Method);
            List<Customer> customers = _dbcontext.Customers.ToList();
            _logger.LogInformation("{action}:{metod} - number of customers returned is: {number}", nameof(Index), Request.Method, customers.Count());
            return View(customers);
        }

        public IActionResult Details(int id)
        {
            Customer customer = _dbcontext.Customers.Single(c => c.CustomerId == id);
            return View(customer);
        }

        [HttpGet]
        [Authorize(Policy = "CanInsertCustomer")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "CanInsertCustomer")]
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
        [Authorize(Policy = "JustFor18AndAdmin")]
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                _logger.LogError("{action}:{method} - id is null", nameof(Edit), Request.Method);
                return BadRequest();
            }

            Customer customer = _dbcontext.Customers.SingleOrDefault(c => c.CustomerId == id);
            if(customer == null)
            {
                _logger.LogError("{action}:{method} - customer is null", nameof(Edit), Request.Method);
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "JustFor18AndAdmin")]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                _logger.LogError("{action}:{method} - id is null", nameof(EditPost), Request.Method);
                return BadRequest();
            }

            Customer customerToUpdate = _dbcontext.Customers.SingleOrDefault(c => c.CustomerId == id);

            if (customerToUpdate == null)
            {
                _logger.LogError("{action}:{method} - customer is null", nameof(EditPost), Request.Method);
                return NotFound();
            }

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

        [Authorize(Policy = "CanDelete")]
        [DisableCors]
        public IActionResult Delete(int id)
        {
            var customer = _dbcontext.Customers.SingleOrDefault(c => c.CustomerId == id);
            return View(customer);
        }


        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "CanDelete")]
        [DisableCors]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _dbcontext.Customers.SingleOrDefault(c => c.CustomerId == id);
            _dbcontext.Customers.Remove(customer);
            _dbcontext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}