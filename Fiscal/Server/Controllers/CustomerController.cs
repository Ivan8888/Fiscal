using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private FiscalContext _context;
        public CustomerController(FiscalContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public ActionResult<List<Customer>> GetAll()
        {
            var customers = _context.Customers.ToList();
            return customers;
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<Customer> GetById(int id) 
        {
            var customer = (from c in _context.Customers
                             where c.CustomerId == id
                             select c)
                             .Include(c => c.Invoices)
                             .ThenInclude(c => c.InvoiceIteams)
                             .ThenInclude(c => c.Product)
                             .SingleOrDefault();

            //explicit loading
            //_context.Entry(customer).Collection(c => c.Invoices).Load();
            //foreach(Invoice inv in customer.Invoices)
            //{
            //    _context.Entry(inv).Collection(inv => inv.InvoiceIteams).Load();
            //}

            return customer;
        }
    }
}