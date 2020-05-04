using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FiscalServer.Models;
using FiscalServer.Data;

namespace FiscalServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [FormatFilter]
    public class ProductController : ControllerBase
    {
        FiscalContext _context;
        public ProductController(FiscalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Product>> Index()
        {
            return _context.Products.ToList();
        }

        [HttpGet("{id}.{format?}")]
        //[Produces("application/xml")]
        public ActionResult<Product> GetById(int id)
        {
            Product product = _context.Products.SingleOrDefault(p => p.ProductId == id);
            return product;
        }
    }
}