using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models;

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
                             select c).SingleOrDefault();
            _context.Entry(customer).Collection(c => c.Invoices).Load();

            return customer;
        }
    }
}