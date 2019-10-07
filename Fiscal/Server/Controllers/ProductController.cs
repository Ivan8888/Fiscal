using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        FiscalContext _context;
        public ProductController(FiscalContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult<List<Product>> GetAll()
        {

            //var result = _context.Products.ToList();
            var products = _context.Products
                            .Include(i => i.InvoiceIteams)
                            .ToList();
            //foreach(Product p in products)
            //{
            //    _context.Entry(p).Collection(p => p.InvoiceIteams).Load();
            //}

            return products;
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public ActionResult<Product> GetById(int id)
        {
            //var result = _context.Products.SingleOrDefault(p => p.ProductId == id);
            var product = (from p in _context.Products
                          where p.ProductId == id
                          select p)
                          .SingleOrDefault();
            //explicit loading
            _context.Entry(product).Collection(p => p.InvoiceIteams).Load();
            
            return product;
        }
    }
}