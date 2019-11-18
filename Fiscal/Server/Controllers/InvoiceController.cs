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
                //eager loading
                var invoices = _context.Invoices
                    .Include(i => i.Customer)
                    .Include(i => i.InvoiceItems)
                    .ToList();

                //
                //var invoices = _context.Invoices.ToList();
                //foreach (var inv in invoices)
                //{
                //    _context.Entry(inv).Reference(c => c.Customer).Load();
                //    _context.Entry(inv).Collection(c => c.InvoiceItems).Load();
                //}

                //lazy loading
                //var invoices = _context.Invoices.ToList();
                //string customerName = invoices.First().Customer.Name;
                //int iteamNumber = invoices.First().InvoiceItems.Count();

                return invoices;
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("[action]/{id}")]
        //[Produces("application/xml")]
        public ActionResult<Invoice> GetById(int id)
        {
            try
            {
                //var invoice = _context.Invoices
                //    .Include(i => i.Customer)
                //    .Include(i => i.InvoiceItems)
                //    .ThenInclude(i => i.Product)
                //    .SingleOrDefault(i => i.InvoiceId == id);

                //explicit loading
                var invoice = _context.Invoices.SingleOrDefault(i => i.InvoiceId == id);

                _context.Entry(invoice).Collection(i => i.InvoiceItems).Load();
                _context.Entry(invoice).Reference(i => i.Customer).Load();
                foreach(var iteam in invoice.InvoiceItems)
                {
                    _context.Entry(iteam).Reference(i => i.Product).Load();
                }   

                return invoice;
            }
            catch
            {
                return StatusCode(500,"Server error!"); 
            }
        }
    }
}