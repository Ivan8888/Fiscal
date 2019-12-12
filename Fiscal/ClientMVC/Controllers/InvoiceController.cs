using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClientMVC.Models;
using ClientMVC.Data;

namespace ClientMVC.Controllers
{
    public class InvoiceController : Controller
    {
        private FiscalContext _context;
        public InvoiceController(FiscalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Invoice> invoices = _context.Invoices.ToList();
            return View(invoices);
        }
    }
}