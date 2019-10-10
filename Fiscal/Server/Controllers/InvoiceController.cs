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
                var invoices = _context.Invoices
                    .Include(i => i.Customer)
                    .Include(i => i.InvoiceIteams)
                    .ToList();

                return invoices;
            }
            catch
            {
                return StatusCode(500,"Server error!");
            }
        }

        [HttpGet("[action]/{id}")]
        //[Produces("application/xml")]
        public ActionResult<Invoice> GetById(int id)
        {
            try
            {
                var invoice = _context.Invoices
                    .Include(i => i.Customer)
                    .Include(i => i.InvoiceIteams)
                    .ThenInclude(i => i.Product)
                    .SingleOrDefault(i => i.InvoiceId == id);

                //explicit loading
                //_context.Entry(invoice).Collection(i => i.InvoiceIteams).Load();
                //_context.Entry(invoice).Reference(i => i.Customer).Load();
                //foreach(var iteam in invoice.InvoiceIteams)
                //{
                //    _context.Entry(iteam).Reference(i => i.Product).Load();
                //}

                return invoice;
            }
            catch
            {
                return StatusCode(500,"Server error!"); 
            }
        }
    }
}