using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FiscalClientMVC.Data;
using FiscalClientMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace FiscalClientMVC.Controllers
{
    public class CustomerController : Controller
    {
        FiscalContext _dbcontext;

        public CustomerController(FiscalContext context)
        {
            _dbcontext = context;
        }

        public IActionResult Index()
        {
            return View();
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
            _dbcontext.Customers.Add(customer);
            _dbcontext.SaveChanges();

            return Content("Uspešno sačuvano");
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
            if (await TryUpdateModelAsync<Customer>(customerToUpdate, "", c => c.Name, c => c.Address, c => c.Email))
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

            return View(customerToUpdate);
        }
    }
}