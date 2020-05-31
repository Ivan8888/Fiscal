using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Data;
using WebShop.Models;
using Microsoft.EntityFrameworkCore;

namespace WebShop.Controllers
{
    public class InvoiceController : Controller
    {
        ShopContext _dbcontext;
        public InvoiceController(ShopContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IActionResult GetById(int id)
        {
            //explicit loading
            Invoice invoice_explicit = _dbcontext.Invoices.Where(i => i.InvoiceId == id).Single();
            _dbcontext.Entry(invoice_explicit).Reference(c => c.Customer).Load();
            _dbcontext.Entry(invoice_explicit).Collection(i => i.InvoiceIteams).Load();

            var invoice_eager = _dbcontext.Invoices
                                .Include(i => i.Customer)
                                .Include(i => i.InvoiceIteams)
                                .ThenInclude(i => i.Product)
                                .Where(i => i.InvoiceId == id).Single();

            //lazy loading
            var invoices_lazy = _dbcontext.Invoices.Where(i => i.InvoiceId == id).Single();
            string customerName = invoices_lazy.Customer.Name;
            int iteamNumber = invoices_lazy.InvoiceIteams.Count();

            return Content("");
        }
    }
}