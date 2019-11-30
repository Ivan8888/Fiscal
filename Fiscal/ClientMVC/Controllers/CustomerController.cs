using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClientMVC.Data;
using ClientMVC.Models;

namespace ClientMVC.Controllers
{
    public class CustomerController : Controller
    {
        FiscalContext _context;

        public CustomerController(FiscalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Customer> customers = _context.Customers.ToList();
            return View(customers);
        }
    }
}