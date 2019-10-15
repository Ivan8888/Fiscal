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
            var products = _context.Products
                .Include(p => p.InvoiceIteams)
                .ToList();

            return products;
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = (from p in _context.Products
                          where p.ProductId == id
                          select p)
                          .SingleOrDefault();

            //explicit loading of single element
            _context.Entry(product).Collection(p => p.InvoiceIteams).Load();
            
            return product;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<Product> Create(Product product)
        {
            //_context.Add(product);
            _context.Products.Add(product);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, product);
        }

        [HttpDelete("[action]/{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _context.Products.SingleOrDefault(p => p.ProductId == id);
            if(product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}