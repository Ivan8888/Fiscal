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
    public class InvoiceController : ControllerBase
    {
        private FiscalContext _context;
        public InvoiceController(FiscalContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult<List<Invoice>> GetAll()
        {
            try
            {
                var result = _context.Invoices.ToList();
                return result;
            }
            catch
            {
                return StatusCode(500,"Server error!");
            }
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<Invoice> GetById(int id)
        {
            try
            {
                var invoice = _context.Invoices.SingleOrDefault(i => i.InvoiceId == id);
                _context.Entry(invoice).Collection(i => i.InvoiceIteams).Load();
                _context.Entry(invoice).Reference(i => i.Customer).Load();

                return invoice;
            }
            catch
            {
                return StatusCode(500,"Server error!"); 
            }
        }
    }
}